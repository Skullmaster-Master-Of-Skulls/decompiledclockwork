using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B6F RID: 2927
	[ToolboxItem(false)]
	public class ScaleLabels : StateManager
	{
		// Token: 0x17002435 RID: 9269
		// (get) Token: 0x06006E5A RID: 28250 RVA: 0x001992C5 File Offset: 0x001974C5
		// (set) Token: 0x06006E5B RID: 28251 RVA: 0x001992EA File Offset: 0x001974EA
		[Category("Behavior")]
		[Description("Gets or sets the background color of the labels.")]
		[DefaultValue(typeof(Color), "")]
		public virtual Color BackgroundColor
		{
			get
			{
				return (Color)(base.ViewState["BackgroundColor"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["BackgroundColor"] = value;
			}
		}

		// Token: 0x17002436 RID: 9270
		// (get) Token: 0x06006E5C RID: 28252 RVA: 0x00199302 File Offset: 0x00197502
		// (set) Token: 0x06006E5D RID: 28253 RVA: 0x00199327 File Offset: 0x00197527
		[Description("Gets or sets the text color of the labels.")]
		[DefaultValue(typeof(Color), "")]
		[Category("Behavior")]
		public virtual Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x17002437 RID: 9271
		// (get) Token: 0x06006E5E RID: 28254 RVA: 0x0019933F File Offset: 0x0019753F
		// (set) Token: 0x06006E5F RID: 28255 RVA: 0x0019935F File Offset: 0x0019755F
		[DefaultValue("")]
		[Description("Gets or sets the font size, family, style of the labels.")]
		[Category("Behavior")]
		public virtual string Font
		{
			get
			{
				return ((string)base.ViewState["Font"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Font"] = value;
			}
		}

		// Token: 0x17002438 RID: 9272
		// (get) Token: 0x06006E60 RID: 28256 RVA: 0x00199372 File Offset: 0x00197572
		// (set) Token: 0x06006E61 RID: 28257 RVA: 0x00199392 File Offset: 0x00197592
		[DefaultValue("")]
		[Description("Gets or sets the format string of the labels.")]
		[Category("Behavior")]
		public virtual string Format
		{
			get
			{
				return ((string)base.ViewState["Format"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Format"] = value;
			}
		}

		// Token: 0x17002439 RID: 9273
		// (get) Token: 0x06006E62 RID: 28258 RVA: 0x001993A5 File Offset: 0x001975A5
		// (set) Token: 0x06006E63 RID: 28259 RVA: 0x001993C5 File Offset: 0x001975C5
		[Description("Gets or sets the template of the labels.")]
		[DefaultValue("")]
		[Category("Behavior")]
		public virtual string Template
		{
			get
			{
				return ((string)base.ViewState["Template"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Template"] = value;
			}
		}

		// Token: 0x1700243A RID: 9274
		// (get) Token: 0x06006E64 RID: 28260 RVA: 0x001993D8 File Offset: 0x001975D8
		// (set) Token: 0x06006E65 RID: 28261 RVA: 0x001993F9 File Offset: 0x001975F9
		[DefaultValue(true)]
		[Description("Gets or sets a bool value indicating whether the labels will be visible.")]
		[Category("Behavior")]
		public virtual bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x1700243B RID: 9275
		// (get) Token: 0x06006E66 RID: 28262 RVA: 0x00199411 File Offset: 0x00197611
		// (set) Token: 0x06006E67 RID: 28263 RVA: 0x00199432 File Offset: 0x00197632
		[DefaultValue(ScaleLabelsPosition.Inside)]
		[Category("Behavior")]
		[Description("Gets or sets the position of the labels.")]
		public virtual ScaleLabelsPosition Position
		{
			get
			{
				return (ScaleLabelsPosition)(base.ViewState["Position"] ?? ScaleLabelsPosition.Inside);
			}
			set
			{
				base.ViewState["Position"] = value;
			}
		}
	}
}
