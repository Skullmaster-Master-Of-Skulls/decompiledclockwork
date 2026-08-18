using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000DE7 RID: 3559
	public class PivotGridAdomdConnectionSettings : StateManager
	{
		// Token: 0x170029C1 RID: 10689
		// (get) Token: 0x06008426 RID: 33830 RVA: 0x001E257C File Offset: 0x001E077C
		// (set) Token: 0x06008427 RID: 33831 RVA: 0x001E25C0 File Offset: 0x001E07C0
		[DefaultValue("")]
		public string DataBase
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["DataBase"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["DataBase"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["DataBase"] = value;
			}
		}

		// Token: 0x170029C2 RID: 10690
		// (get) Token: 0x06008428 RID: 33832 RVA: 0x001E25D4 File Offset: 0x001E07D4
		// (set) Token: 0x06008429 RID: 33833 RVA: 0x001E2618 File Offset: 0x001E0818
		[DefaultValue("")]
		public string Cube
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["Cube"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["Cube"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["Cube"] = value;
			}
		}

		// Token: 0x170029C3 RID: 10691
		// (get) Token: 0x0600842A RID: 33834 RVA: 0x001E262C File Offset: 0x001E082C
		// (set) Token: 0x0600842B RID: 33835 RVA: 0x001E2670 File Offset: 0x001E0870
		[DefaultValue("")]
		public string ConnectionString
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["ConnectionString"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["ConnectionString"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["ConnectionString"] = value;
			}
		}
	}
}
