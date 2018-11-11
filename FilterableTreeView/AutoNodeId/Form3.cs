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
    /// Shows auto generated NodeId example
    /// Please Don't forget ,have to be used AddNode function
    /// </summary>
    public partial class Form3 : Form
    {
        DataSet dataSet;
        public Form3()
        {
            InitializeComponent();
            #region Sample data populate which is chained class
            dataSet = new DataSet();
            dataSet.ReadXml("Data.xml");
            List<CategoryClass> lst = new List<CategoryClass>();

            MyClass.Deserialize(lst, "SerializedData.xml");
            for (int i = 0; i < lst.Count; i++)
            {
                MyTreeNodeEx myTree = new MyTreeNodeEx();
                myTree.Text = lst[i].Name;
                myFilterableTreeView1.AddNode(null, myTree);
                AddChildNode(myTree, lst[i].SubCategories);
            }

            #endregion
        }

        private void AddChildNode(MyTreeNodeEx myTree, List<CategoryClass> subCategories)
        {
            if (subCategories != null)
            {
                for (int i = 0; i < subCategories.Count; i++)
                {
                    MyTreeNodeEx ex = new MyTreeNodeEx();
                    ex.Text = subCategories[i].Name;
                    myFilterableTreeView1.AddNode(myTree, ex);
                    AddChildNode(ex, subCategories[i].SubCategories);
                    //TODO you are at here
                }
            }
        }
    }
}
