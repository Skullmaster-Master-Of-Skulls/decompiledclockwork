using System;
using System.ComponentModel.Design;
using System.Design;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000C8 RID: 200
	internal class FormViewActionList : DesignerActionList
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x00023AFC File Offset: 0x00021CFC
		public FormViewActionList(FormViewDesigner formViewDesigner) : base(formViewDesigner.Component)
		{
			this._formViewDesigner = formViewDesigner;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00023B11 File Offset: 0x00021D11
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x00023B19 File Offset: 0x00021D19
		internal bool AllowDynamicData
		{
			get
			{
				return this._allowDynamicData;
			}
			set
			{
				this._allowDynamicData = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00023B22 File Offset: 0x00021D22
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00023B2A File Offset: 0x00021D2A
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

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00003B0F File Offset: 0x00001D0F
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00003937 File Offset: 0x00001B37
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

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00023B33 File Offset: 0x00021D33
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00023B40 File Offset: 0x00021D40
		public bool EnableDynamicData
		{
			get
			{
				return this._formViewDesigner.EnableDynamicData;
			}
			set
			{
				this._formViewDesigner.EnableDynamicData = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00023B4E File Offset: 0x00021D4E
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00023B5B File Offset: 0x00021D5B
		public bool EnablePaging
		{
			get
			{
				return this._formViewDesigner.EnablePaging;
			}
			set
			{
				this._formViewDesigner.EnablePaging = value;
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00023B6C File Offset: 0x00021D6C
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			if (this.AllowDynamicData)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnableDynamicData", SR.GetString("FormView_EnableDynamicData"), "Behavior", SR.GetString("FormView_EnableDynamicDataDesc")));
			}
			if (this.AllowPaging)
			{
				designerActionItemCollection.Add(new DesignerActionPropertyItem("EnablePaging", SR.GetString("FormView_EnablePaging"), "Behavior", SR.GetString("FormView_EnablePagingDesc")));
			}
			return designerActionItemCollection;
		}

		// Token: 0x040003DE RID: 990
		private FormViewDesigner _formViewDesigner;

		// Token: 0x040003DF RID: 991
		private bool _allowDynamicData;

		// Token: 0x040003E0 RID: 992
		private bool _allowPaging;
	}
}
