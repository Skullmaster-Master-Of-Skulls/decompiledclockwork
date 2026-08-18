using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000368 RID: 872
	internal class TreeViewDesigner : ControlDesigner
	{
		// Token: 0x060023D3 RID: 9171 RVA: 0x000DFEBC File Offset: 0x000DE0BC
		public TreeViewDesigner()
		{
			base.AutoResizeHandles = true;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x000DFED8 File Offset: 0x000DE0D8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.treeView != null)
			{
				this.treeView.AfterExpand -= this.TreeViewInvalidate;
				this.treeView.AfterCollapse -= this.TreeViewInvalidate;
				this.treeView = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x000DFF2C File Offset: 0x000DE12C
		protected override bool GetHitTest(Point point)
		{
			point = this.Control.PointToClient(point);
			this.tvhit.pt_x = point.X;
			this.tvhit.pt_y = point.Y;
			NativeMethods.SendMessage(this.Control.Handle, 4369, 0, this.tvhit);
			return this.tvhit.flags == 16;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x000DFF9C File Offset: 0x000DE19C
		public override void Initialize(IComponent component)
		{
			base.Initialize(component);
			this.treeView = (component as TreeView);
			if (this.treeView != null)
			{
				this.treeView.AfterExpand += this.TreeViewInvalidate;
				this.treeView.AfterCollapse += this.TreeViewInvalidate;
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x000DFFF2 File Offset: 0x000DE1F2
		private void TreeViewInvalidate(object sender, TreeViewEventArgs e)
		{
			if (this.treeView != null)
			{
				this.treeView.Invalidate();
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x000E0007 File Offset: 0x000DE207
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.Add(new TreeViewActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x04001A40 RID: 6720
		private NativeMethods.TV_HITTESTINFO tvhit = new NativeMethods.TV_HITTESTINFO();

		// Token: 0x04001A41 RID: 6721
		private DesignerActionListCollection _actionLists;

		// Token: 0x04001A42 RID: 6722
		private TreeView treeView;
	}
}
