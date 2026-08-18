using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x0200002C RID: 44
	public class MyTreeView : TreeView
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000DF9C File Offset: 0x0000CF9C
		public MyTreeView()
		{
			this.AllowDrop = true;
			MenuItem[] menuItems = new MenuItem[]
			{
				new MenuItem("Move to &Root", new EventHandler(this.DefaultContextMenu_MoveToRoot_Eventhandler))
				{
					Name = "mniMoveToRoot"
				},
				new MenuItem("-"),
				new MenuItem("Move &Up", new EventHandler(this.DefaultContextMenu_MoveUp_Eventhandler))
				{
					Name = "mniMoveUp"
				},
				new MenuItem("Move &Down", new EventHandler(this.DefaultContextMenu_MoveDown_Eventhandler))
				{
					Name = "mniMoveDown"
				},
				new MenuItem("-"),
				new MenuItem("&Expand", new EventHandler(this.DefaultContextMenu_Expand_Eventhandler))
				{
					Name = "mniExpand"
				},
				new MenuItem("&Collapse", new EventHandler(this.DefaultContextMenu_Collapse_Eventhandler))
				{
					Name = "mniCollapse"
				},
				new MenuItem("-"),
				new MenuItem("E&xpand All", new EventHandler(this.DefaultContextMenu_ExpandAll_Eventhandler))
				{
					Name = "mniExpandAll"
				},
				new MenuItem("C&ollapse All", new EventHandler(this.DefaultContextMenu_CollapseAll_Eventhandler))
				{
					Name = "mniCollapseAll"
				}
			};
			this._defaultContextMenu = new ContextMenu(menuItems);
			this._defaultContextMenu.Popup += this._defaultContextMenu_Popup;
			this.ContextMenu = this._defaultContextMenu;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000E154 File Offset: 0x0000D154
		public void AddContextMenuItems(ContextMenuStrip cms)
		{
			this._defaultContextMenu.MenuItems.Add(new MenuItem("-"));
			foreach (object obj in cms.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				MenuItem item = new MenuItem(toolStripItem.Text);
				this._defaultContextMenu.MenuItems.Add(item);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000E1F0 File Offset: 0x0000D1F0
		public void SetMenuItem(string menuItemText, EventHandler eh)
		{
			foreach (object obj in this._defaultContextMenu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				if (menuItem.Text.CompareTo(menuItemText) == 0)
				{
					menuItem.Click += eh;
					break;
				}
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000E278 File Offset: 0x0000D278
		private void _defaultContextMenu_Popup(object sender, EventArgs e)
		{
			for (int i = 0; i < this._defaultContextMenu.MenuItems.Count - 1; i++)
			{
				this._defaultContextMenu.MenuItems[i].Enabled = (base.SelectedNode != null);
			}
			if (base.SelectedNode != null)
			{
				this._defaultContextMenu.MenuItems["mniMoveToRoot"].Enabled = (base.SelectedNode.Parent != null);
				this._defaultContextMenu.MenuItems["mniMoveUp"].Enabled = (base.SelectedNode.PrevNode != null);
				this._defaultContextMenu.MenuItems["mniMoveDown"].Enabled = (base.SelectedNode.NextNode != null);
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000E360 File Offset: 0x0000D360
		protected void DefaultContextMenu_MoveToRoot_Eventhandler(object sender, EventArgs e)
		{
			base.Nodes.Add((TreeNode)base.SelectedNode.Clone());
			base.SelectedNode.Remove();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000E38B File Offset: 0x0000D38B
		protected void DefaultContextMenu_MoveUp_Eventhandler(object sender, EventArgs e)
		{
			this.MoveNode(base.SelectedNode, MyTreeView.MoveDirection.MoveUp);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000E39C File Offset: 0x0000D39C
		protected void DefaultContextMenu_MoveDown_Eventhandler(object sender, EventArgs e)
		{
			this.MoveNode(base.SelectedNode, MyTreeView.MoveDirection.MoveDown);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000E3AD File Offset: 0x0000D3AD
		protected void DefaultContextMenu_Expand_Eventhandler(object sender, EventArgs e)
		{
			base.SelectedNode.Expand();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000E3BC File Offset: 0x0000D3BC
		protected void DefaultContextMenu_Collapse_Eventhandler(object sender, EventArgs e)
		{
			base.SelectedNode.Collapse();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000E3CB File Offset: 0x0000D3CB
		protected void DefaultContextMenu_ExpandAll_Eventhandler(object sender, EventArgs e)
		{
			base.ExpandAll();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000E3D5 File Offset: 0x0000D3D5
		protected void DefaultContextMenu_CollapseAll_Eventhandler(object sender, EventArgs e)
		{
			base.CollapseAll();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000E3DF File Offset: 0x0000D3DF
		protected override void OnItemDrag(ItemDragEventArgs e)
		{
			this._draggedNode = (TreeNode)e.Item;
			base.SelectedNode = this._draggedNode;
			base.DoDragDrop(e.Item, DragDropEffects.Move);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000E410 File Offset: 0x0000D410
		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			if (drgevent.AllowedEffect.Equals(DragDropEffects.Move))
			{
				TreeNode treeNode = this.DroppedOnNode(drgevent);
				if (treeNode != null)
				{
					treeNode.Nodes.Add((TreeNode)this._draggedNode.Clone());
				}
				else
				{
					base.Nodes.Add((TreeNode)this._draggedNode.Clone());
				}
				this._draggedNode.Remove();
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000E493 File Offset: 0x0000D493
		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			drgevent.Effect = DragDropEffects.None;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000E4A0 File Offset: 0x0000D4A0
		protected override void OnDragOver(DragEventArgs drgevent)
		{
			TreeNode treeNode = this.DroppedOnNode(drgevent);
			if (treeNode != null && treeNode.NextVisibleNode != null)
			{
				if (!treeNode.NextVisibleNode.IsVisible)
				{
					treeNode.NextVisibleNode.EnsureVisible();
				}
				else if (treeNode.PrevVisibleNode != null && !treeNode.PrevVisibleNode.IsVisible)
				{
					treeNode.PrevVisibleNode.EnsureVisible();
				}
			}
			if (this.SameBranch(this._draggedNode, treeNode))
			{
				drgevent.Effect = DragDropEffects.None;
			}
			else
			{
				if (treeNode != null)
				{
					treeNode.Expand();
				}
				drgevent.Effect = DragDropEffects.Move;
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000E547 File Offset: 0x0000D547
		protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
		{
			gfbevent.UseDefaultCursors = true;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000E554 File Offset: 0x0000D554
		private void MoveNode(TreeNode node, MyTreeView.MoveDirection move)
		{
			int num = -1;
			switch (move)
			{
			case MyTreeView.MoveDirection.MoveUp:
				if (node.PrevNode != null)
				{
					num = node.PrevNode.Index;
				}
				break;
			case MyTreeView.MoveDirection.MoveDown:
				if (node.NextNode != null)
				{
					num = node.NextNode.Index + 1;
				}
				break;
			}
			if (num != -1)
			{
				if (node.Parent != null)
				{
					node.Parent.Nodes.Insert(num, (TreeNode)node.Clone());
					base.SelectedNode = node.Parent.Nodes[num];
				}
				else
				{
					base.Nodes.Insert(num, (TreeNode)node.Clone());
					base.SelectedNode = base.Nodes[num];
				}
				node.Remove();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000E634 File Offset: 0x0000D634
		private TreeNode DroppedOnNode(DragEventArgs drgevent)
		{
			Point pt = base.PointToClient(new Point(drgevent.X, drgevent.Y));
			return base.GetNodeAt(pt);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000E668 File Offset: 0x0000D668
		private bool SameBranch(TreeNode draggedNode, TreeNode dropNode)
		{
			return dropNode != null && (draggedNode.Equals(dropNode) || this.SameBranch(draggedNode, dropNode.Parent));
		}

		// Token: 0x04000181 RID: 385
		private TreeNode _draggedNode = null;

		// Token: 0x04000182 RID: 386
		private ContextMenu _defaultContextMenu = null;

		// Token: 0x0200002D RID: 45
		private enum MoveDirection
		{
			// Token: 0x04000184 RID: 388
			MoveUp,
			// Token: 0x04000185 RID: 389
			MoveDown
		}
	}
}
