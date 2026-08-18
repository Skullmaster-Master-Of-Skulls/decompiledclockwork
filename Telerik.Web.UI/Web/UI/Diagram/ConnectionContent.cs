using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000214 RID: 532
	public class ConnectionContent : StateManager, IDefaultCheck
	{
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x00044CE5 File Offset: 0x00042EE5
		// (set) Token: 0x0600138F RID: 5007 RVA: 0x00044D05 File Offset: 0x00042F05
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

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001390 RID: 5008 RVA: 0x00044D18 File Offset: 0x00042F18
		// (set) Token: 0x06001391 RID: 5009 RVA: 0x00044D38 File Offset: 0x00042F38
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

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001392 RID: 5010 RVA: 0x00044D4B File Offset: 0x00042F4B
		// (set) Token: 0x06001393 RID: 5011 RVA: 0x00044D74 File Offset: 0x00042F74
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

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x00044D8C File Offset: 0x00042F8C
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x00044DAC File Offset: 0x00042FAC
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

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x00044DBF File Offset: 0x00042FBF
		// (set) Token: 0x06001397 RID: 5015 RVA: 0x00044DDF File Offset: 0x00042FDF
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

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x00044DF2 File Offset: 0x00042FF2
		// (set) Token: 0x06001399 RID: 5017 RVA: 0x00044E12 File Offset: 0x00043012
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

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x00044E25 File Offset: 0x00043025
		// (set) Token: 0x0600139B RID: 5019 RVA: 0x00044E45 File Offset: 0x00043045
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

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x00044E58 File Offset: 0x00043058
		// (set) Token: 0x0600139D RID: 5021 RVA: 0x00044E78 File Offset: 0x00043078
		[DefaultValue("")]
		public string Visual
		{
			get
			{
				return (string)(base.ViewState["Visual"] ?? "");
			}
			set
			{
				base.ViewState["Visual"] = value;
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x00044E8C File Offset: 0x0004308C
		public bool IsDefault
		{
			get
			{
				return this.Color == "" && this.FontFamily == "" && this.FontSize == 0.0 && this.FontStyle == "" && this.FontWeight == "" && this.Template == "" && this.Text == "" && this.Visual == "";
			}
		}
	}
}
