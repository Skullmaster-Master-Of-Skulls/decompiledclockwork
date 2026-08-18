using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200044B RID: 1099
	public class ShapeContent : StateManager, IDefaultCheck
	{
		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06002795 RID: 10133 RVA: 0x00080A16 File Offset: 0x0007EC16
		// (set) Token: 0x06002796 RID: 10134 RVA: 0x00080A36 File Offset: 0x0007EC36
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

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x06002797 RID: 10135 RVA: 0x00080A49 File Offset: 0x0007EC49
		// (set) Token: 0x06002798 RID: 10136 RVA: 0x00080A69 File Offset: 0x0007EC69
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

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x06002799 RID: 10137 RVA: 0x00080A7C File Offset: 0x0007EC7C
		// (set) Token: 0x0600279A RID: 10138 RVA: 0x00080A9C File Offset: 0x0007EC9C
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

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x00080AAF File Offset: 0x0007ECAF
		// (set) Token: 0x0600279C RID: 10140 RVA: 0x00080AD8 File Offset: 0x0007ECD8
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

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x00080AF0 File Offset: 0x0007ECF0
		// (set) Token: 0x0600279E RID: 10142 RVA: 0x00080B10 File Offset: 0x0007ED10
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

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x00080B23 File Offset: 0x0007ED23
		// (set) Token: 0x060027A0 RID: 10144 RVA: 0x00080B43 File Offset: 0x0007ED43
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

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x00080B56 File Offset: 0x0007ED56
		// (set) Token: 0x060027A2 RID: 10146 RVA: 0x00080B76 File Offset: 0x0007ED76
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

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x00080B89 File Offset: 0x0007ED89
		// (set) Token: 0x060027A4 RID: 10148 RVA: 0x00080BA9 File Offset: 0x0007EDA9
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

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x00080BBC File Offset: 0x0007EDBC
		public bool IsDefault
		{
			get
			{
				return this.Align == "" && this.Color == "" && this.FontFamily == "" && this.FontSize == 0.0 && this.FontStyle == "" && this.FontWeight == "" && this.Text == "" && this.Html == "";
			}
		}
	}
}
