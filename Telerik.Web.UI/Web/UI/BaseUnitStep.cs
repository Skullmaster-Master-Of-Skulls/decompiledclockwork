using System;

namespace Telerik.Web.UI
{
	// Token: 0x020003A9 RID: 937
	public class BaseUnitStep : StateManager
	{
		// Token: 0x06002301 RID: 8961 RVA: 0x000752FD File Offset: 0x000734FD
		public BaseUnitStep()
		{
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x00075305 File Offset: 0x00073505
		public BaseUnitStep(int value)
		{
			this.Value = value;
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x00075314 File Offset: 0x00073514
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x00075335 File Offset: 0x00073535
		public int Value
		{
			get
			{
				return (int)(base.ViewState["Value"] ?? 0);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}
	}
}
