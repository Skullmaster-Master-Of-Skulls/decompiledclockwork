using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200113D RID: 4413
	public class GridDataInsertItem : GridDataItem, IGridInsertItem
	{
		// Token: 0x0600B3D2 RID: 46034 RVA: 0x00273E10 File Offset: 0x00272010
		public GridDataInsertItem(GridTableView ownerTableView) : base(ownerTableView, -1, -1, GridItemType.EditItem)
		{
		}

		// Token: 0x17003A1B RID: 14875
		// (get) Token: 0x0600B3D3 RID: 46035 RVA: 0x00273E1C File Offset: 0x0027201C
		public override bool IsInEditMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17003A1C RID: 14876
		// (get) Token: 0x0600B3D4 RID: 46036 RVA: 0x00273E1F File Offset: 0x0027201F
		// (set) Token: 0x0600B3D5 RID: 46037 RVA: 0x00273E22 File Offset: 0x00272022
		public override bool Edit
		{
			get
			{
				return true;
			}
			set
			{
				if (!value)
				{
					base.OwnerTableView.IsItemInserted = false;
				}
			}
		}

		// Token: 0x17003A1D RID: 14877
		// (get) Token: 0x0600B3D6 RID: 46038 RVA: 0x00273E33 File Offset: 0x00272033
		public override bool HasChildItems
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17003A1E RID: 14878
		// (get) Token: 0x0600B3D7 RID: 46039 RVA: 0x00273E36 File Offset: 0x00272036
		public override bool CanExpand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600B3D8 RID: 46040 RVA: 0x00273E3C File Offset: 0x0027203C
		public override void Initialize(GridColumn[] columns)
		{
			base.Initialize(columns);
			if (base.OwnerTableView.OwnerGrid.ClientSettings.AllowKeyboardNavigation && base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile)
			{
				base.Attributes["onkeypress"] = string.Format("$find('{0}')._handlerKeyDownInInserItem(event);", base.OwnerTableView.ClientID);
			}
		}
	}
}
