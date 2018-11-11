using FilterableTreeView.Concrete_Control;
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
    /// This example shows how to add a node to the exist node
    /// After creating a parent node you can add a node to the parent node by using AddNode function
    /// Please don't forget AddNode function have to be used
    /// </summary>
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            DataSet dataSet = new DataSet();
            dataSet.ReadXml("Data.xml");
            for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
            {
                int parentId = Int32.Parse(dataSet.Tables[0].Rows[i]["ParentCategoryId"].ToString());
                if (parentId != 0 && myFilterableTreeView1.TreeDictionary.ContainsKey(parentId) == true)
                {

                    MyTreeNodeEx parentNode = myFilterableTreeView1.TreeDictionary[parentId];
                    MyTreeNodeEx node = new MyTreeNodeEx();
                    node.NodeId = Int32.Parse(dataSet.Tables[0].Rows[i]["CategoryId"].ToString());
                    node.ParentNodeId = parentId;
                    node.Text = dataSet.Tables[0].Rows[i]["Name"].ToString();
                    myFilterableTreeView1.AddNode(parentNode, node);//Add node to the exist node which is the parentNode
                }
                else if (parentId == 0)
                {
                    MyTreeNodeEx node = new MyTreeNodeEx();
                    node.NodeId = Int32.Parse(dataSet.Tables[0].Rows[i]["CategoryId"].ToString());
                    node.ParentNodeId = parentId;
                    node.Text = dataSet.Tables[0].Rows[i]["Name"].ToString();
                    myFilterableTreeView1.AddNode(parentId, node);//Add root of treeView
                }


            }
            Console.WriteLine(myFilterableTreeView1.TreeDictionary.Count);
        }
    }
}
