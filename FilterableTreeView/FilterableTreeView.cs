using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilterableTreeView
{
    /// <summary>
    /// T is a generic TreeNodeEx typed class which derived TreeNode
    /// Also you can add another property image,icon etc...
    /// You may easily adjust it for yourself
    /// </summary>
    /// 
    public partial class FilterableTreeView<T> : UserControl where T : TreeNodeEx
    {



        object LOCK_OBJECT = new object();//lock obj
        public FilterableTreeView()
        {
            InitializeComponent();
            TreeDictionary = new Dictionary<int, T>();
            FilteredList = new Dictionary<int, T>();

        }

        /// <summary>
        /// All treeview node will be store in this dictionary type collection
        /// 
        /// </summary>
        private readonly Dictionary<int, T> TreeDictionary;

        /// <summary>
        /// FilteredList stores selected nodes by filtering  text property
        /// </summary>
        private Dictionary<int, T> FilteredList;

        /// <summary>
        /// AddNode function provides nodeId to node and it adds the node to the parent node or root of treeview
        /// If you want,you  may set your child node id from db by primary key or autoincrement field 
        /// Every node must have unique nodeid and have parentnodeid  
        /// </summary>
        /// <param name="parent">parent is parent node of the child node.If the node is root node parent node must be null.</param>
        /// <param name="child">child is currently being added node</param>
        /// <returns></returns>
        public int AddNode(int parentNodeId, T child)
        {
            lock (LOCK_OBJECT)//Provides multithread safe
            {
                if (child.NodeId < 0)
                {
                    child.NodeId = TreeDictionary.Count;
                }

                TreeDictionary.Add(child.NodeId, child);

                if (parentNodeId == 0)//Root node
                {
                    child.ParentNodeId = 0;
                    return treeView.Nodes.Add(child);

                }
                else//Child node
                {
                    T tmp = GetTNode(parentNodeId);
                    child.ParentNodeId = tmp.NodeId;
                    return tmp.Nodes.Add(child);
                }
            }
        }

        /// <summary>
        /// If you added another property you should assign them at here
        /// </summary>
        /// <param name="ex">Currently shown node on the treeview</param>
        /// <returns></returns>
        public T CloneExist(T ex)
        {

            T treeNode = (T)Activator.CreateInstance(typeof(T));
            treeNode.Text = ex.Text;
            treeNode.NodeId = ex.NodeId;
            treeNode.ParentNodeId = ex.ParentNodeId;
            return treeNode;
        }


        T GetTNode(int nodeId)
        {

            return TreeDictionary[nodeId];
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            //Check the filtertextbox text is not null and length bigger than 3
            //Also if the filtertextbox has no char then it should show all nodes without filtering 
            if (FilterTextBox.Text.Trim() == String.Empty || FilterTextBox.Text.Length < 3)
            {
                if (FilterTextBox.Text.Trim() == String.Empty)
                {
                    LoadData(TreeDictionary);
                }
                return;
            }

            //Create new filteredlist for Every filter text changed
            //So old filteredlist will be empty
            FilteredList = new Dictionary<int, T>();

            //easily to check text of the all node contains filter text by looping 
            //Also we need to add deep copy of the node to the filteredlist 
            //Because We shouldn't try to add a node that is already added to the treeview
            foreach (T item in TreeDictionary.Values)
            {
                if (item.Text.Contains(FilterTextBox.Text) == true)
                {

                    FilteredList.Add(item.NodeId, CloneExist(item));
                }
            }


            //Now we got all nodes which contains filter text
            //But we need to add all parent nodes which are not exist in the filteredlist  
            //Example let's assume path is Electronic\Computer\Notebook and filter text is 'book'
            //Then , treeview should show fullpath of Notebook like above
            //If this node already root node there is nothing to do
            for (int i = 0; i < FilteredList.Values.Count; i++)
            {
                T tmp = FilteredList.Values.ToList()[i];
                while (true)
                {
                    if (tmp.ParentNodeId == 0) break;
                    else
                    {
                        T parent = GetTNode(tmp.ParentNodeId);
                        if (FilteredList.ContainsKey(parent.NodeId) == false)
                        {
                            FilteredList.Add(parent.NodeId, CloneExist(parent));
                        }
                        tmp = parent;
                    }
                }
            }
            //After populating all node it is time to show nodes on the treeview
            LoadData(FilteredList);
        }

        //Data to TreeView
        private void LoadData(Dictionary<int, T> pairs)
        {
            treeView.Nodes.Clear();
            treeView.BeginUpdate();//For speeding up
            foreach (T item in pairs.Values)
            {
                if (item.ParentNodeId == 0)
                {
                    treeView.Nodes.Add(item);
                }
                else
                {
                    T tmp = pairs.Values.Where(c => c.NodeId == item.ParentNodeId).FirstOrDefault();
                    if (tmp.Nodes.Contains(item) == false)
                    {
                        tmp.Nodes.Add(item);
                    }
                }
            }
            treeView.EndUpdate();
            treeView.ExpandAll();
        }
    }
}
