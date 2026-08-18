using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B1 RID: 1457
	public class Stroke : StateManager, IDefaultCheck
	{
		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x0600340A RID: 13322 RVA: 0x000ACCCA File Offset: 0x000AAECA
		// (set) Token: 0x0600340B RID: 13323 RVA: 0x000ACCEA File Offset: 0x000AAEEA
		[DefaultValue("")]
		public string Color
		{
			get
			{
				return (string)(base.ViewState["Color"] ?? "");
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000ACCFD File Offset: 0x000AAEFD
		// (set) Token: 0x0600340D RID: 13325 RVA: 0x000ACD1D File Offset: 0x000AAF1D
		[DefaultValue("solid")]
		public string DashType
		{
			get
			{
				return (string)(base.ViewState["DashType"] ?? "solid");
			}
			set
			{
				base.ViewState["DashType"] = value;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000ACD30 File Offset: 0x000AAF30
		// (set) Token: 0x0600340F RID: 13327 RVA: 0x000ACD59 File Offset: 0x000AAF59
		[DefaultValue(0.0)]
		public double Opacity
		{
			get
			{
				return (double)(base.ViewState["Opacity"] ?? 0.0);
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000ACD71 File Offset: 0x000AAF71
		// (set) Token: 0x06003411 RID: 13329 RVA: 0x000ACD9A File Offset: 0x000AAF9A
		[DefaultValue(1.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 1.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000ACDB4 File Offset: 0x000AAFB4
		public bool IsDefault
		{
			get
			{
				return this.Color == "" && this.DashType == "solid" && this.Opacity == 0.0 && this.Width == 1.0;
			}
		}
	}
}
