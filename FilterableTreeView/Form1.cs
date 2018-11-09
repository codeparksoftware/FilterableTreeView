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
        {
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
