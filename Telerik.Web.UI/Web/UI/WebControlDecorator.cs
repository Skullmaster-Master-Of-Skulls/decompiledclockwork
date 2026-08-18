using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000148 RID: 328
	[ParseChildren(true)]
	public class WebControlDecorator
	{
		// Token: 0x06000D06 RID: 3334 RVA: 0x0002E8DC File Offset: 0x0002CADC
		public WebControlDecorator(WebControl c)
		{
			this.control = c;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0002E8EB File Offset: 0x0002CAEB
		// (set) Token: 0x06000D08 RID: 3336 RVA: 0x0002E8F8 File Offset: 0x0002CAF8
		public string AccessKey
		{
			get
			{
				return this.control.AccessKey;
			}
			set
			{
				this.control.AccessKey = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0002E906 File Offset: 0x0002CB06
		// (set) Token: 0x06000D0A RID: 3338 RVA: 0x0002E913 File Offset: 0x0002CB13
		public Color BackColor
		{
			get
			{
				return this.control.BackColor;
			}
			set
			{
				this.control.BackColor = value;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0002E921 File Offset: 0x0002CB21
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x0002E92E File Offset: 0x0002CB2E
		public Color BorderColor
		{
			get
			{
				return this.control.BorderColor;
			}
			set
			{
				this.control.BorderColor = value;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0002E93C File Offset: 0x0002CB3C
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x0002E949 File Offset: 0x0002CB49
		public BorderStyle BorderStyle
		{
			get
			{
				return this.control.BorderStyle;
			}
			set
			{
				this.control.BorderStyle = value;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0002E957 File Offset: 0x0002CB57
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x0002E964 File Offset: 0x0002CB64
		public Unit BorderWidth
		{
			get
			{
				return this.control.BorderWidth;
			}
			set
			{
				this.control.BorderWidth = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0002E972 File Offset: 0x0002CB72
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x0002E97F File Offset: 0x0002CB7F
		public string CssClass
		{
			get
			{
				return this.control.CssClass;
			}
			set
			{
				this.control.CssClass = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0002E98D File Offset: 0x0002CB8D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FontInfo Font
		{
			get
			{
				return this.control.Font;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0002E99A File Offset: 0x0002CB9A
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0002E9A7 File Offset: 0x0002CBA7
		public Color ForeColor
		{
			get
			{
				return this.control.ForeColor;
			}
			set
			{
				this.control.ForeColor = value;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0002E9B5 File Offset: 0x0002CBB5
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0002E9C2 File Offset: 0x0002CBC2
		public Unit Height
		{
			get
			{
				return this.control.Height;
			}
			set
			{
				this.control.Height = value;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0002E9D0 File Offset: 0x0002CBD0
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0002E9DD File Offset: 0x0002CBDD
		public short TabIndex
		{
			get
			{
				return this.control.TabIndex;
			}
			set
			{
				this.control.TabIndex = value;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0002E9EB File Offset: 0x0002CBEB
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x0002E9F8 File Offset: 0x0002CBF8
		public string ToolTip
		{
			get
			{
				return this.control.ToolTip;
			}
			set
			{
				this.control.ToolTip = value;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002EA06 File Offset: 0x0002CC06
		// (set) Token: 0x06000D1D RID: 3357 RVA: 0x0002EA13 File Offset: 0x0002CC13
		public Unit Width
		{
			get
			{
				return this.control.Width;
			}
			set
			{
				this.control.Width = value;
			}
		}

		// Token: 0x04000334 RID: 820
		private readonly WebControl control;
	}
}
