using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueryBeautifier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFormat_Click(object sender, EventArgs e)
        {
            txtDiff.Text = "";
            txtRight.ForeColor = Color.Black;
            txtLeft.Text = string.Join(" = ",string.Join("\n", txtLeft.Text.Split('&')).Replace(" ","").Split('='));
            txtRight.Text = string.Join(" = ", string.Join("\n", txtRight.Text.Split('&')).Replace(" ", "").Split('='));

            if (chkSort.Checked)
            {
                txtLeft.Text = string.Join("\n", txtLeft.Text.Split('\n').OrderBy(x => x).ToArray());
                txtRight.Text = string.Join("\n", txtRight.Text.Split('\n').OrderBy(x => x).ToArray());
            }

            Dictionary<string, string> leftDict = new Dictionary<string, string>();
            Dictionary<string, string> rightDict = new Dictionary<string, string>();

            foreach (string pair in txtLeft.Text.Split('\n'))
            {
                var arr = pair.Split('=');
                if(arr.Length == 2)
                    leftDict[arr[0]] = arr[1];
            }

            foreach (string pair in txtRight.Text.Split('\n'))
            {
                var arr = pair.Split('=');
                if (arr.Length == 2)
                    rightDict[arr[0]] = arr[1];
            }

            if (chkMatches.Checked)
            {
                if (txtLeft.Text == txtRight.Text)
                    txtRight.ForeColor = Color.Green;
                else
                {
                    List<string> leftList = new List<string>(leftDict.Keys);
                    List<string> rightList = new List<string>(rightDict.Keys);

                    List<string> intersectionList = leftList.AsQueryable().Intersect(rightList).ToList();
                    List<string> nonIntersectionList = leftList.Except(rightList).Union(rightList.Except(leftList)).ToList();

                    int matchCount = intersectionList.Select(k => leftDict[k] != rightDict[k]).Count();
                    if (matchCount > 0)
                    {
                        txtDiff.Text += "Matches:\n";
                    }

                    foreach(string key in intersectionList)
                    {
                        var x = leftDict[key];
                        var y = rightDict[key];
                        if (leftDict[key] != rightDict[key])
                        {
                            txtDiff.Text += $"{key} -> ";
                            txtDiff.Text += $"Left: {leftDict[key]} \t Right: {rightDict[key]}\n";
                        }
                    }

                    if(matchCount>0)
                        txtDiff.Text += "\n";

                    if (nonIntersectionList.Count > 0)
                    {
                        txtDiff.Text += "Not Match:\n";
                    }

                    foreach(string key in nonIntersectionList)
                    {
                        txtDiff.Text += $"{key} -> ";
                        if(leftList.Contains(key))
                            txtDiff.Text += $"Left: {leftDict[key]}\n";
                        if(rightList.Contains(key))
                            txtDiff.Text += $"Right: {rightDict[key]}\n";
                    }
                }
            }
        }

        private void txtRight_Changed(object sender, EventArgs e)
        {
            if(txtRight.ForeColor == Color.Green)
                txtRight.ForeColor = Color.Black;
        }
    }
}
