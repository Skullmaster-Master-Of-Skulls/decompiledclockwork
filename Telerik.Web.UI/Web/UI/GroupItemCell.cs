using System;

namespace Telerik.Web.UI
{
	// Token: 0x020011A1 RID: 4513
	public class GroupItemCell : GridTableHeaderCell
	{
		// Token: 0x17003BE3 RID: 15331
		// (get) Token: 0x0600B96F RID: 47471 RVA: 0x00290BEB File Offset: 0x0028EDEB
		// (set) Token: 0x0600B970 RID: 47472 RVA: 0x00290BF3 File Offset: 0x0028EDF3
		public string HierarchicalIndex
		{
			get
			{
				return this._hierarchicalIndex;
			}
			set
			{
				this._hierarchicalIndex = value;
			}
		}

		// Token: 0x17003BE4 RID: 15332
		// (get) Token: 0x0600B971 RID: 47473 RVA: 0x00290BFC File Offset: 0x0028EDFC
		// (set) Token: 0x0600B972 RID: 47474 RVA: 0x00290C04 File Offset: 0x0028EE04
		public string DataField
		{
			get
			{
				return this._dataField;
			}
			set
			{
				this._dataField = value;
			}
		}

		// Token: 0x040030FC RID: 12540
		private string _hierarchicalIndex = "-1";

		// Token: 0x040030FD RID: 12541
		private string _dataField = "";
	}
}
