using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200023A RID: 570
	public class DiagramGradientStop : StateManager
	{
		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00047E8A File Offset: 0x0004608A
		// (set) Token: 0x060014D6 RID: 5334 RVA: 0x00047EB3 File Offset: 0x000460B3
		[DefaultValue(0.0)]
		public double Offset
		{
			get
			{
				return (double)(base.ViewState["Offset"] ?? 0.0);
			}
			set
			{
				base.ViewState["Offset"] = value;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00047ECB File Offset: 0x000460CB
		// (set) Token: 0x060014D8 RID: 5336 RVA: 0x00047EEB File Offset: 0x000460EB
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

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00047EFE File Offset: 0x000460FE
		// (set) Token: 0x060014DA RID: 5338 RVA: 0x00047F27 File Offset: 0x00046127
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
	}
}
