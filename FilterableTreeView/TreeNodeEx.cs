using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FilterableTreeView
{
    public class TreeNodeEx : TreeNode
    {
        /// <summary>
        /// Every Node  must have unique Id 
        /// And NodeId indicates this unique Id
        /// </summary>
        public int NodeId { get; set; } = -1;
        public int ParentNodeId { get; set; } = -1;
    }
}
