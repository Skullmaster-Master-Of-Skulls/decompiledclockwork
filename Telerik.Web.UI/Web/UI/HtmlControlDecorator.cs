using System;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000147 RID: 327
	public class HtmlControlDecorator
	{
		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002E701 File Offset: 0x0002C901
		public HtmlControlDecorator(HtmlControl c)
		{
			this.control = c;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002E710 File Offset: 0x0002C910
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0002E731 File Offset: 0x0002C931
		public Color BackColor
		{
			get
			{
				return ColorTranslator.FromHtml(this.control.Style["background-color"].ToString());
			}
			set
			{
				this.control.Style["background-color"] = ColorTranslator.ToHtml(value);
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002E74E File Offset: 0x0002C94E
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x0002E76F File Offset: 0x0002C96F
		public Color BorderColor
		{
			get
			{
				return ColorTranslator.FromHtml(this.control.Style["border-color"].ToString());
			}
			set
			{
				this.control.Style["border-color"] = ColorTranslator.ToHtml(value);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0002E78C File Offset: 0x0002C98C
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0002E7A8 File Offset: 0x0002C9A8
		public Unit BorderWidth
		{
			get
			{
				return Unit.Parse(this.control.Style["border-width"]);
			}
			set
			{
				this.control.Style["border-width"] = value.ToString();
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0002E7CC File Offset: 0x0002C9CC
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x0002E7E8 File Offset: 0x0002C9E8
		public string CssClass
		{
			get
			{
				return this.control.Attributes["class"].ToString();
			}
			set
			{
				if (value != null)
				{
					this.control.Attributes["class"] = value;
					return;
				}
				this.control.Attributes["class"] = string.Empty;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0002E81E File Offset: 0x0002CA1E
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x0002E83F File Offset: 0x0002CA3F
		public Color ForeColor
		{
			get
			{
				return ColorTranslator.FromHtml(this.control.Style["color"].ToString());
			}
			set
			{
				this.control.Style["color"] = ColorTranslator.ToHtml(value);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0002E85C File Offset: 0x0002CA5C
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x0002E878 File Offset: 0x0002CA78
		public Unit Height
		{
			get
			{
				return Unit.Parse(this.control.Style["height"]);
			}
			set
			{
				this.control.Style["height"] = value.ToString();
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0002E89C File Offset: 0x0002CA9C
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x0002E8B8 File Offset: 0x0002CAB8
		public Unit Width
		{
			get
			{
				return Unit.Parse(this.control.Style["width"]);
			}
			set
			{
				this.control.Style["width"] = value.ToString();
			}
		}

		// Token: 0x04000333 RID: 819
		private readonly HtmlControl control;
	}
}
