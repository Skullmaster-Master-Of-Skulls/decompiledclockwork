using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000C12 RID: 3090
	public class OrgChartGroupEnabledWebServiceBinding
	{
		// Token: 0x1700264C RID: 9804
		// (get) Token: 0x060075C1 RID: 30145 RVA: 0x001B6497 File Offset: 0x001B4697
		// (set) Token: 0x060075C2 RID: 30146 RVA: 0x001B64B2 File Offset: 0x001B46B2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartGroupItemServiceSettings GroupItemServiceSettings
		{
			get
			{
				if (this._groupItemServiceSettings == null)
				{
					this._groupItemServiceSettings = new OrgChartGroupItemServiceSettings();
				}
				return this._groupItemServiceSettings;
			}
			set
			{
				this._groupItemServiceSettings = value;
			}
		}

		// Token: 0x1700264D RID: 9805
		// (get) Token: 0x060075C3 RID: 30147 RVA: 0x001B64BB File Offset: 0x001B46BB
		// (set) Token: 0x060075C4 RID: 30148 RVA: 0x001B64D6 File Offset: 0x001B46D6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public OrgChartNodeServiceSettings NodeServiceSettings
		{
			get
			{
				if (this._nodeServiceSettings == null)
				{
					this._nodeServiceSettings = new OrgChartNodeServiceSettings();
				}
				return this._nodeServiceSettings;
			}
			set
			{
				this._nodeServiceSettings = value;
			}
		}

		// Token: 0x04002048 RID: 8264
		private OrgChartGroupItemServiceSettings _groupItemServiceSettings;

		// Token: 0x04002049 RID: 8265
		private OrgChartNodeServiceSettings _nodeServiceSettings;
	}
}
