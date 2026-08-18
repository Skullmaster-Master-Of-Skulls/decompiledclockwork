using System;
using System.ComponentModel.Design;
using System.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C4 RID: 196
	internal class DetailsViewActionList : DesignerActionList
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00021922 File Offset: 0x0001FB22
		public DetailsViewActionList(DetailsViewDesigner detailsViewDesigner) : base(detailsViewDesigner.Component)
		{
			this._detailsViewDesigner = detailsViewDesigner;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00021937 File Offset: 0x0001FB37
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0002193F File Offset: 0x0001FB3F
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00021948 File Offset: 0x0001FB48
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00021950 File Offset: 0x0001FB50
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

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00021959 File Offset: 0x0001FB59
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00021961 File Offset: 0x0001FB61
		internal bool AllowInserting
		{
			get
			{
				return this._allowInserting;
			}
			set
			{
				this._allowInserting = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0002196A File Offset: 0x0001FB6A
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00021972 File Offset: 0x0001FB72
		internal bool AllowMoveDown
		{
			get
			{
				return this._allowMoveDown;
			}
			set
			{
				this._allowMoveDown = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0002197B File Offset: 0x0001FB7B
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00021983 File Offset: 0x0001FB83
		internal bool AllowMoveUp
		{
			get
			{
				return this._allowMoveUp;
			}
			set
			{
				this._allowMoveUp = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0002198C File Offset: 0x0001FB8C
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00021994 File Offset: 0x0001FB94
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

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0002199D File Offset: 0x0001FB9D
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x000219A5 File Offset: 0x0001FBA5
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

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600064E RID: 1614 RVA: 0x00003B0F File Offset: 0x00001D0F
		// (set) Token: 0x0600064F RID: 1615 RVA: 0x00003937 File Offset: 0x00001B37
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

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x000219AE File Offset: 0x0001FBAE
		// (set) Token: 0x06000651 RID: 1617 RVA: 0x000219BB File Offset: 0x0001FBBB
		public bool EnableDeleting
		{
			get
			{
				return this._detailsViewDesigner.EnableDeleting;
			}
			set
			{
				this._detailsViewDesigner.EnableDeleting = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x000219C9 File Offset: 0x0001FBC9
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x000219D6 File Offset: 0x0001FBD6
		public bool EnableEditing
		{
			get
			{
				return this._detailsViewDesigner.EnableEditing;
			}
			set
			{
				this._detailsViewDesigner.EnableEditing = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x000219E4 File Offset: 0x0001FBE4
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x000219F1 File Offset: 0x0001FBF1
		public bool EnableInserting
		{
			get
			{
				return this._detailsViewDesigner.EnableInserting;
			}
			set
			{
				this._detailsViewDesigner.EnableInserting = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x000219FF File Offset: 0x0001FBFF
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00021A0C File Offset: 0x0001FC0C
		public bool EnablePaging
		{
			get
			{
				return this._detailsViewDesigner.EnablePaging;
			}
			set
			{
				this._detailsViewDesigner.EnablePaging = value;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00021A1A File Offset: 0x0001FC1A
		public void AddNewField()
		{
			this._detailsViewDesigner.AddNewField();
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00021A27 File Offset: 0x0001FC27
		public void EditFields()
		{
			this._detailsViewDesigner.EditFields();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00021A34 File Offset: 0x0001FC34
		public void MoveFieldUp()
		{
			this._detailsViewDesigner.MoveUp();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00021A41 File Offset: 0x0001FC41
		public void MoveFieldDown()
		{
			this._detailsViewDesigner.MoveDown();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00021A4E File Offset: 0x0001FC4E
		public void RemoveField()
		{
			this._detailsViewDesigner.RemoveField();
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00021A5C File Offset: 0x0001FC5C
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			designerActionItemCollection.Add(new DesignerActionMethodItem(this, "EditFields", SR.GetString("DetailsView_EditFieldsVerb"), "Action", SR.GetString("DetailsView_EditFieldsDesc")));
			designerActionItemCollection.Add(new DesignerActionMethodItem(this, "AddNewField", SR.GetString("DetailsView_AddNewFieldVerb"), "Action", SR.GetString("DetailsView_AddNewFieldDesc")));
			if (this.AllowMoveUp)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "MoveFieldUp", SR.GetString("DetailsView_MoveFieldUpVerb"), "Action", SR.GetString("DetailsView_MoveFieldUpDesc")));
			}
			if (this.AllowMoveDown)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "MoveFieldDown", SR.GetString("DetailsView_MoveFieldDownVerb"), "Action", SR.GetString("DetailsView_MoveFieldDownDesc")));
			}
			if (this.AllowRemoveField)
			{
				designerActionItemCollection.Add(new DesignerActionMethodItem(this, "RemoveField", SR.GetString("DetailsView_RemoveFieldVerb"), "Action", SR.GetString("DetailsView_RemoveFieldDesc")));
			}
			if (this.AllowPaging)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnablePaging", SR.GetString("DetailsView_EnablePaging"), "Behavior", SR.GetString("DetailsView_EnablePagingDesc")));
			}
			if (this.AllowInserting)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnableInserting", SR.GetString("DetailsView_EnableInserting"), "Behavior", SR.GetString("DetailsView_EnableInsertingDesc")));
			}
			if (this.AllowEditing)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnableEditing", SR.GetString("DetailsView_EnableEditing"), "Behavior", SR.GetString("DetailsView_EnableEditingDesc")));
			}
			if (this.AllowDeleting)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnableDeleting", SR.GetString("DetailsView_EnableDeleting"), "Behavior", SR.GetString("DetailsView_EnableDeletingDesc")));
			}
			return designerActionItemCollection;
		}

		// Token: 0x0400039E RID: 926
		private DetailsViewDesigner _detailsViewDesigner;

		// Token: 0x0400039F RID: 927
		private bool _allowDeleting;

		// Token: 0x040003A0 RID: 928
		private bool _allowEditing;

		// Token: 0x040003A1 RID: 929
		private bool _allowInserting;

		// Token: 0x040003A2 RID: 930
		private bool _allowPaging;

		// Token: 0x040003A3 RID: 931
		private bool _allowRemoveField;

		// Token: 0x040003A4 RID: 932
		private bool _allowMoveUp;

		// Token: 0x040003A5 RID: 933
		private bool _allowMoveDown;
	}
}
