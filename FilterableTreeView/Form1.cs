using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilterableTreeView
{
    /// <summary>
    /// This form NodeId value is entered from database 
    /// There is an difference between Form2 example that is used to parentNodeId insteadof parentNode(TreeNodeEx)
    /// Please don't forget AddNode function have to be used
    /// </summary>
    public partial class Form1 : Form
    {
        FilterableTreeView<TreeNodeEx> filterableTreeView;
        public Form1()
        {
            InitializeComponent();

            filterableTreeView = new FilterableTreeView<TreeNodeEx>();
            panel2.Controls.Add(filterableTreeView);
            filterableTreeView.Dock = DockStyle.Fill;
            LoadDummyData();
        }

        private void LoadDummyData()
        {//check the all node added to its parent
            DataSet dataSet = new DataSet();
            dataSet.ReadXml("Data.xml");
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                TreeNodeEx treeNodeEx = new TreeNodeEx();
                treeNodeEx.Text = dataSet.Tables[0].Rows[i]["Name"].ToString();
                treeNodeEx.NodeId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["CategoryId"].ToString());
                treeNodeEx.ParentNodeId = Convert.ToInt32(dataSet.Tables[0].Rows[i]["ParentCategoryId"].ToString());
                filterableTreeView.AddNode(treeNodeEx.ParentNodeId, treeNodeEx);

            }
        }
    }
}
