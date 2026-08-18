using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000020 RID: 32
	public class TreeViewMS : TreeView
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000AF09 File Offset: 0x00009F09
		public TreeViewMS()
		{
			this.m_coll = new ArrayList();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000AF1F File Offset: 0x00009F1F
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000AF2C File Offset: 0x00009F2C
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000AF44 File Offset: 0x00009F44
		public ArrayList SelectedNodes
		{
			get
			{
				return this.m_coll;
			}
			set
			{
				this.removePaintFromNodes();
				this.m_coll.Clear();
				this.m_coll = value;
				this.paintSelectedNodes();
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000AF68 File Offset: 0x00009F68
		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			base.OnBeforeSelect(e);
			bool flag = Control.ModifierKeys == Keys.Control;
			bool flag2 = Control.ModifierKeys == Keys.Shift;
			if (flag && this.m_coll.Contains(e.Node))
			{
				e.Cancel = true;
				this.removePaintFromNodes();
				this.m_coll.Remove(e.Node);
				this.paintSelectedNodes();
			}
			else
			{
				this.m_lastNode = e.Node;
				if (!flag2)
				{
					this.m_firstNode = e.Node;
				}
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000B000 File Offset: 0x0000A000
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect(e);
			bool flag = Control.ModifierKeys == Keys.Control;
			bool flag2 = Control.ModifierKeys == Keys.Shift;
			if (flag)
			{
				if (!this.m_coll.Contains(e.Node))
				{
					this.m_coll.Add(e.Node);
				}
				else
				{
					this.removePaintFromNodes();
					this.m_coll.Remove(e.Node);
				}
				this.paintSelectedNodes();
			}
			else if (flag2)
			{
				Queue queue = new Queue();
				TreeNode treeNode = this.m_firstNode;
				TreeNode treeNode2 = e.Node;
				bool flag3 = this.isParent(this.m_firstNode, e.Node);
				if (!flag3)
				{
					flag3 = this.isParent(treeNode2, treeNode);
					if (flag3)
					{
						TreeNode treeNode3 = treeNode;
						treeNode = treeNode2;
						treeNode2 = treeNode3;
					}
				}
				if (flag3)
				{
					for (TreeNode treeNode4 = treeNode2; treeNode4 != treeNode.Parent; treeNode4 = treeNode4.Parent)
					{
						if (!this.m_coll.Contains(treeNode4))
						{
							queue.Enqueue(treeNode4);
						}
					}
				}
				else if ((treeNode.Parent == null && treeNode2.Parent == null) || (treeNode.Parent != null && treeNode.Parent.Nodes.Contains(treeNode2)))
				{
					int i = treeNode.Index;
					int index = treeNode2.Index;
					if (index < i)
					{
						TreeNode treeNode3 = treeNode;
						treeNode = treeNode2;
						treeNode2 = treeNode3;
						i = treeNode.Index;
						index = treeNode2.Index;
					}
					TreeNode treeNode4 = treeNode;
					while (i <= index)
					{
						if (!this.m_coll.Contains(treeNode4))
						{
							queue.Enqueue(treeNode4);
						}
						treeNode4 = treeNode4.NextNode;
						i++;
					}
				}
				else
				{
					if (!this.m_coll.Contains(treeNode))
					{
						queue.Enqueue(treeNode);
					}
					if (!this.m_coll.Contains(treeNode2))
					{
						queue.Enqueue(treeNode2);
					}
				}
				this.m_coll.AddRange(queue);
				this.paintSelectedNodes();
				this.m_firstNode = e.Node;
			}
			else
			{
				if (this.m_coll != null && this.m_coll.Count > 0)
				{
					this.removePaintFromNodes();
					this.m_coll.Clear();
				}
				this.m_coll.Add(e.Node);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000B2B0 File Offset: 0x0000A2B0
		protected bool isParent(TreeNode parentNode, TreeNode childNode)
		{
			bool result;
			if (parentNode == childNode)
			{
				result = true;
			}
			else
			{
				TreeNode treeNode = childNode;
				bool flag = false;
				while (!flag && treeNode != null)
				{
					treeNode = treeNode.Parent;
					flag = (treeNode == parentNode);
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000B2F8 File Offset: 0x0000A2F8
		protected void paintSelectedNodes()
		{
			foreach (object obj in this.m_coll)
			{
				TreeNode treeNode = (TreeNode)obj;
				treeNode.BackColor = SystemColors.Highlight;
				treeNode.ForeColor = SystemColors.HighlightText;
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000B370 File Offset: 0x0000A370
		protected void removePaintFromNodes()
		{
			if (this.m_coll.Count != 0)
			{
				TreeNode treeNode = (TreeNode)this.m_coll[0];
				Color backColor = treeNode.TreeView.BackColor;
				Color foreColor = treeNode.TreeView.ForeColor;
				foreach (object obj in this.m_coll)
				{
					TreeNode treeNode2 = (TreeNode)obj;
					treeNode2.BackColor = backColor;
					treeNode2.ForeColor = foreColor;
				}
			}
		}

		// Token: 0x04000152 RID: 338
		protected ArrayList m_coll;

		// Token: 0x04000153 RID: 339
		protected TreeNode m_lastNode;

		// Token: 0x04000154 RID: 340
		protected TreeNode m_firstNode;
	}
}
