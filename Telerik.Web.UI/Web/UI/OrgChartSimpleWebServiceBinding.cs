using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000C19 RID: 3097
	public class OrgChartSimpleWebServiceBinding
	{
		// Token: 0x17002661 RID: 9825
		// (get) Token: 0x060075F3 RID: 30195 RVA: 0x001B67DF File Offset: 0x001B49DF
		// (set) Token: 0x060075F4 RID: 30196 RVA: 0x001B67FA File Offset: 0x001B49FA
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

		// Token: 0x0400205C RID: 8284
		private OrgChartGroupItemServiceSettings _groupItemServiceSettings;
	}
}
