using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000224 RID: 548
	public class Content : StateManager, IDefaultCheck
	{
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00045E3A File Offset: 0x0004403A
		// (set) Token: 0x060013F4 RID: 5108 RVA: 0x00045E5A File Offset: 0x0004405A
		[DefaultValue("")]
		public string Align
		{
			get
			{
				return (string)(base.ViewState["Align"] ?? "");
			}
			set
			{
				base.ViewState["Align"] = value;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00045E6D File Offset: 0x0004406D
		// (set) Token: 0x060013F6 RID: 5110 RVA: 0x00045E8D File Offset: 0x0004408D
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

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x00045EA0 File Offset: 0x000440A0
		// (set) Token: 0x060013F8 RID: 5112 RVA: 0x00045EC0 File Offset: 0x000440C0
		[DefaultValue("")]
		public string FontFamily
		{
			get
			{
				return (string)(base.ViewState["FontFamily"] ?? "");
			}
			set
			{
				base.ViewState["FontFamily"] = value;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00045ED3 File Offset: 0x000440D3
		// (set) Token: 0x060013FA RID: 5114 RVA: 0x00045EFC File Offset: 0x000440FC
		[DefaultValue(0.0)]
		public double FontSize
		{
			get
			{
				return (double)(base.ViewState["FontSize"] ?? 0.0);
			}
			set
			{
				base.ViewState["FontSize"] = value;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x00045F14 File Offset: 0x00044114
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x00045F34 File Offset: 0x00044134
		[DefaultValue("")]
		public string FontStyle
		{
			get
			{
				return (string)(base.ViewState["FontStyle"] ?? "");
			}
			set
			{
				base.ViewState["FontStyle"] = value;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x00045F47 File Offset: 0x00044147
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x00045F67 File Offset: 0x00044167
		[DefaultValue("")]
		public string FontWeight
		{
			get
			{
				return (string)(base.ViewState["FontWeight"] ?? "");
			}
			set
			{
				base.ViewState["FontWeight"] = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00045F7A File Offset: 0x0004417A
		// (set) Token: 0x06001400 RID: 5120 RVA: 0x00045F9A File Offset: 0x0004419A
		[DefaultValue("")]
		public string Template
		{
			get
			{
				return (string)(base.ViewState["Template"] ?? "");
			}
			set
			{
				base.ViewState["Template"] = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00045FAD File Offset: 0x000441AD
		// (set) Token: 0x06001402 RID: 5122 RVA: 0x00045FCD File Offset: 0x000441CD
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? "");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x00045FE0 File Offset: 0x000441E0
		// (set) Token: 0x06001404 RID: 5124 RVA: 0x00046000 File Offset: 0x00044200
		public string Html
		{
			get
			{
				return (string)(base.ViewState["Html"] ?? "");
			}
			set
			{
				base.ViewState["Html"] = value;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00046014 File Offset: 0x00044214
		public bool IsDefault
		{
			get
			{
				return this.Align == "" && this.Color == "" && this.FontFamily == "" && this.FontSize == 0.0 && this.FontStyle == "" && this.FontWeight == "" && this.Template == "" && this.Text == "" && this.Html == "";
			}
		}
	}
}
