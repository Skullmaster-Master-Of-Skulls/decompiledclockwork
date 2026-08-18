using System;
using System.ComponentModel.Design;
using System.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000CB RID: 203
	internal class GridViewActionList : DesignerActionList
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x000251DE File Offset: 0x000233DE
		public GridViewActionList(GridViewDesigner gridViewDesigner) : base(gridViewDesigner.Component)
		{
			this._gridViewDesigner = gridViewDesigner;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x000251F3 File Offset: 0x000233F3
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x000251FB File Offset: 0x000233FB
		internal bool AllowDeleting
		{
			get
			{
				return this._allowDeleting;
			}
			set
			{
				this._allowDeleting = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00025204 File Offset: 0x00023404
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x0002520C File Offset: 0x0002340C
		internal bool AllowEditing
		{
			get
			{
				return this._allowEditing;
			}
			set
			{
				this._allowEditing = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x00025215 File Offset: 0x00023415
		// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0002521D File Offset: 0x0002341D
		internal bool AllowMoveLeft
		{
			get
			{
				return this._allowMoveLeft;
			}
			set
			{
				this._allowMoveLeft = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00025226 File Offset: 0x00023426
		// (set) Token: 0x060006C3 RID: 1731 RVA: 0x0002522E File Offset: 0x0002342E
		internal bool AllowMoveRight
		{
			get
			{
				return this._allowMoveRight;
			}
			set
			{
				this._allowMoveRight = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x00025237 File Offset: 0x00023437
		// (set) Token: 0x060006C5 RID: 1733 RVA: 0x0002523F File Offset: 0x0002343F
		internal bool AllowPaging
		{
			get
			{
				return this._allowPaging;
			}
			set
			{
				this._allowPaging = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00025248 File Offset: 0x00023448
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00025250 File Offset: 0x00023450
		internal bool AllowRemoveField
		{
			get
			{
				return this._allowRemoveField;
			}
			set
			{
				this._allowRemoveField = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00025259 File Offset: 0x00023459
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00025261 File Offset: 0x00023461
		internal bool AllowSelection
		{
			get
			{
				return this._allowSelection;
			}
			set
			{
				this._allowSelection = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0002526A File Offset: 0x0002346A
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00025272 File Offset: 0x00023472
		internal bool AllowSorting
		{
			get
			{
				return this._allowSorting;
			}
			set
			{
				this._allowSorting = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00003B0F File Offset: 0x00001D0F
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00003937 File Offset: 0x00001B37
		public override bool AutoShow
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0002527B File Offset: 0x0002347B
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00025288 File Offset: 0x00023488
		public bool EnableDeleting
		{
			get
			{
				return this._gridViewDesigner.EnableDeleting;
			}
			set
			{
				this._gridViewDesigner.EnableDeleting = value;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00025296 File Offset: 0x00023496
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x000252A3 File Offset: 0x000234A3
		public bool EnableEditing
		{
			get
			{
				return this._gridViewDesigner.EnableEditing;
			}
			set
			{
				this._gridViewDesigner.EnableEditing = value;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000252B1 File Offset: 0x000234B1
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x000252BE File Offset: 0x000234BE
		public bool EnablePaging
		{
			get
			{
				return this._gridViewDesigner.EnablePaging;
			}
			set
			{
				this._gridViewDesigner.EnablePaging = value;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x000252CC File Offset: 0x000234CC
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x000252D9 File Offset: 0x000234D9
		public bool EnableSelection
		{
			get
			{
				return this._gridViewDesigner.EnableSelection;
			}
			set
			{
				this._gridViewDesigner.EnableSelection = value;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x000252E7 File Offset: 0x000234E7
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x000252F4 File Offset: 0x000234F4
		public bool EnableSorting
		{
			get
			{
				return this._gridViewDesigner.EnableSorting;
			}
			set
			{
				this._gridViewDesigner.EnableSorting = value;
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00025302 File Offset: 0x00023502
		public void AddNewField()
		{
			this._gridViewDesigner.AddNewField();
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0002530F File Offset: 0x0002350F
		public void EditFields()
		{
			this._gridViewDesigner.EditFields();
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0002531C File Offset: 0x0002351C
		public void MoveFieldLeft()
		{
			this._gridViewDesigner.MoveLeft();
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00025329 File Offset: 0x00023529
		public void MoveFieldRight()
		{
			this._gridViewDesigner.MoveRight();
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00025336 File Offset: 0x00023536
		public void RemoveField()
		{
			this._gridViewDesigner.RemoveField();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00025344 File Offset: 0x00023544
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditFields", SR.GetString("GridView_EditFieldsVerb"), "Action", SR.GetString("GridView_EditFieldsDesc")));
			designerActionItemCollection.Add(new DesignerActionMethodItem(this, "AddNewField", SR.GetString("GridView_AddNewFieldVerb"), "Action", SR.GetString("GridView_AddNewFieldDesc")));
			if (this.AllowMoveLeft)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "MoveFieldLeft", SR.GetString("GridView_MoveFieldLeftVerb"), "Action", SR.GetString("GridView_MoveFieldLeftDesc")));
			}
			if (this.AllowMoveRight)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "MoveFieldRight", SR.GetString("GridView_MoveFieldRightVerb"), "Action", SR.GetString("GridView_MoveFieldRightDesc")));
			}
			if (this.AllowRemoveField)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "RemoveField", SR.GetString("GridView_RemoveFieldVerb"), "Action", SR.GetString("GridView_RemoveFieldDesc")));
			}
			if (this.AllowPaging)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnablePaging", SR.GetString("GridView_EnablePaging"), "Behavior", SR.GetString("GridView_EnablePagingDesc")));
			}
			if (this.AllowSorting)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnableSorting", SR.GetString("GridView_EnableSorting"), "Behavior", SR.GetString("GridView_EnableSortingDesc")));
			}
			if (this.AllowEditing)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnableEditing", SR.GetString("GridView_EnableEditing"), "Behavior", SR.GetString("GridView_EnableEditingDesc")));
			}
			if (this.AllowDeleting)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnableDeleting", SR.GetString("GridView_EnableDeleting"), "Behavior", SR.GetString("GridView_EnableDeletingDesc")));
			}
			if (this.AllowSelection)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnableSelection", SR.GetString("GridView_EnableSelection"), "Behavior", SR.GetString("GridView_EnableSelectionDesc")));
			}
			return designerActionItemCollection;
		}

		// Token: 0x04000416 RID: 1046
		private GridViewDesigner _gridViewDesigner;

		// Token: 0x04000417 RID: 1047
		private bool _allowDeleting;

		// Token: 0x04000418 RID: 1048
		private bool _allowEditing;

		// Token: 0x04000419 RID: 1049
		private bool _allowSorting;

		// Token: 0x0400041A RID: 1050
		private bool _allowPaging;

		// Token: 0x0400041B RID: 1051
		private bool _allowSelection;

		// Token: 0x0400041C RID: 1052
		private bool _allowRemoveField;

		// Token: 0x0400041D RID: 1053
		private bool _allowMoveLeft;

		// Token: 0x0400041E RID: 1054
		private bool _allowMoveRight;
	}
}
