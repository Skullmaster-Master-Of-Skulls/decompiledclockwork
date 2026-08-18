using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000BFC RID: 3068
	public class OrgChartGroupEnabledBinding
	{
		// Token: 0x1700260D RID: 9741
		// (get) Token: 0x060074CD RID: 29901 RVA: 0x001B3341 File Offset: 0x001B1541
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartNodeBindingSettings NodeBindingSettings
		{
			get
			{
				if (this._nodeSettings == null)
				{
					this._nodeSettings = new OrgChartNodeBindingSettings();
				}
				return this._nodeSettings;
			}
		}

		// Token: 0x1700260E RID: 9742
		// (get) Token: 0x060074CE RID: 29902 RVA: 0x001B335C File Offset: 0x001B155C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartGroupItemBindingSettings GroupItemBindingSettings
		{
			get
			{
				if (this._itemSettings == null)
				{
					this._itemSettings = new OrgChartGroupItemBindingSettings();
				}
				return this._itemSettings;
			}
		}

		// Token: 0x04001FCB RID: 8139
		private OrgChartNodeBindingSettings _nodeSettings;

		// Token: 0x04001FCC RID: 8140
		private OrgChartGroupItemBindingSettings _itemSettings;
	}
}
