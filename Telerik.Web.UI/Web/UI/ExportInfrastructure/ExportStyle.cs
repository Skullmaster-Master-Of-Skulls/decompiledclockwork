using System;
using System.Drawing;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Export;

namespace Telerik.Web.UI.ExportInfrastructure
{
	// Token: 0x02000A4A RID: 2634
	public class ExportStyle : IEquatable<ExportStyle>
	{
		// Token: 0x17002192 RID: 8594
		// (get) Token: 0x060065E9 RID: 26089 RVA: 0x0017D82C File Offset: 0x0017BA2C
		// (set) Token: 0x060065EA RID: 26090 RVA: 0x0017D834 File Offset: 0x0017BA34
		public virtual Color BackColor { get; set; }

		// Token: 0x17002193 RID: 8595
		// (get) Token: 0x060065EB RID: 26091 RVA: 0x0017D83D File Offset: 0x0017BA3D
		// (set) Token: 0x060065EC RID: 26092 RVA: 0x0017D845 File Offset: 0x0017BA45
		public virtual Color BorderBottomColor { get; set; }

		// Token: 0x17002194 RID: 8596
		// (get) Token: 0x060065ED RID: 26093 RVA: 0x0017D84E File Offset: 0x0017BA4E
		// (set) Token: 0x060065EE RID: 26094 RVA: 0x0017D856 File Offset: 0x0017BA56
		public virtual Color BorderLeftColor { get; set; }

		// Token: 0x17002195 RID: 8597
		// (get) Token: 0x060065EF RID: 26095 RVA: 0x0017D85F File Offset: 0x0017BA5F
		// (set) Token: 0x060065F0 RID: 26096 RVA: 0x0017D867 File Offset: 0x0017BA67
		public virtual Color BorderRightColor { get; set; }

		// Token: 0x17002196 RID: 8598
		// (get) Token: 0x060065F1 RID: 26097 RVA: 0x0017D870 File Offset: 0x0017BA70
		// (set) Token: 0x060065F2 RID: 26098 RVA: 0x0017D878 File Offset: 0x0017BA78
		public virtual Color BorderTopColor { get; set; }

		// Token: 0x17002197 RID: 8599
		// (get) Token: 0x060065F3 RID: 26099 RVA: 0x0017D881 File Offset: 0x0017BA81
		// (set) Token: 0x060065F4 RID: 26100 RVA: 0x0017D889 File Offset: 0x0017BA89
		public virtual BorderStyle BorderBottomStyle { get; set; }

		// Token: 0x17002198 RID: 8600
		// (get) Token: 0x060065F5 RID: 26101 RVA: 0x0017D892 File Offset: 0x0017BA92
		// (set) Token: 0x060065F6 RID: 26102 RVA: 0x0017D89A File Offset: 0x0017BA9A
		public virtual BorderStyle BorderLeftStyle { get; set; }

		// Token: 0x17002199 RID: 8601
		// (get) Token: 0x060065F7 RID: 26103 RVA: 0x0017D8A3 File Offset: 0x0017BAA3
		// (set) Token: 0x060065F8 RID: 26104 RVA: 0x0017D8AB File Offset: 0x0017BAAB
		public virtual BorderStyle BorderRightStyle { get; set; }

		// Token: 0x1700219A RID: 8602
		// (get) Token: 0x060065F9 RID: 26105 RVA: 0x0017D8B4 File Offset: 0x0017BAB4
		// (set) Token: 0x060065FA RID: 26106 RVA: 0x0017D8BC File Offset: 0x0017BABC
		public virtual BorderStyle BorderTopStyle { get; set; }

		// Token: 0x1700219B RID: 8603
		// (get) Token: 0x060065FB RID: 26107 RVA: 0x0017D8C5 File Offset: 0x0017BAC5
		// (set) Token: 0x060065FC RID: 26108 RVA: 0x0017D8CD File Offset: 0x0017BACD
		public virtual Unit BorderBottomWidth { get; set; }

		// Token: 0x1700219C RID: 8604
		// (get) Token: 0x060065FD RID: 26109 RVA: 0x0017D8D6 File Offset: 0x0017BAD6
		// (set) Token: 0x060065FE RID: 26110 RVA: 0x0017D8DE File Offset: 0x0017BADE
		public virtual Unit BorderLeftWidth { get; set; }

		// Token: 0x1700219D RID: 8605
		// (get) Token: 0x060065FF RID: 26111 RVA: 0x0017D8E7 File Offset: 0x0017BAE7
		// (set) Token: 0x06006600 RID: 26112 RVA: 0x0017D8EF File Offset: 0x0017BAEF
		public virtual Unit BorderRightWidth { get; set; }

		// Token: 0x1700219E RID: 8606
		// (get) Token: 0x06006601 RID: 26113 RVA: 0x0017D8F8 File Offset: 0x0017BAF8
		// (set) Token: 0x06006602 RID: 26114 RVA: 0x0017D900 File Offset: 0x0017BB00
		public virtual Unit BorderTopWidth { get; set; }

		// Token: 0x1700219F RID: 8607
		// (get) Token: 0x06006603 RID: 26115 RVA: 0x0017D909 File Offset: 0x0017BB09
		// (set) Token: 0x06006604 RID: 26116 RVA: 0x0017D929 File Offset: 0x0017BB29
		public virtual FontInfo Font
		{
			get
			{
				if (this._font == null)
				{
					this._font = new Style().Font;
				}
				return this._font;
			}
			set
			{
				this._font = value;
			}
		}

		// Token: 0x170021A0 RID: 8608
		// (get) Token: 0x06006605 RID: 26117 RVA: 0x0017D932 File Offset: 0x0017BB32
		// (set) Token: 0x06006606 RID: 26118 RVA: 0x0017D93A File Offset: 0x0017BB3A
		public virtual Color ForeColor { get; set; }

		// Token: 0x170021A1 RID: 8609
		// (get) Token: 0x06006607 RID: 26119 RVA: 0x0017D943 File Offset: 0x0017BB43
		// (set) Token: 0x06006608 RID: 26120 RVA: 0x0017D94B File Offset: 0x0017BB4B
		public virtual HorizontalAlign HorizontalAlign { get; set; }

		// Token: 0x170021A2 RID: 8610
		// (get) Token: 0x06006609 RID: 26121 RVA: 0x0017D954 File Offset: 0x0017BB54
		// (set) Token: 0x0600660A RID: 26122 RVA: 0x0017D95C File Offset: 0x0017BB5C
		public virtual VerticalAlign VerticalAlign { get; set; }

		// Token: 0x170021A3 RID: 8611
		// (get) Token: 0x0600660B RID: 26123 RVA: 0x0017D965 File Offset: 0x0017BB65
		public virtual bool IsEmpty
		{
			get
			{
				return this.Equals(new ExportStyle());
			}
		}

		// Token: 0x0600660C RID: 26124 RVA: 0x0017D974 File Offset: 0x0017BB74
		internal virtual void ImportStyle(ExportStyle style)
		{
			if (!style.BackColor.IsEmpty && this.BackColor.IsEmpty)
			{
				this.BackColor = style.BackColor;
			}
			if (!style.ForeColor.IsEmpty && this.ForeColor.IsEmpty)
			{
				this.ForeColor = style.ForeColor;
			}
			if (!Utils.IsEmptyFontStyle(style.Font) && Utils.IsEmptyFontStyle(this.Font))
			{
				this.Font = style.Font;
			}
			if (style.HorizontalAlign != HorizontalAlign.NotSet && this.HorizontalAlign == HorizontalAlign.NotSet)
			{
				this.HorizontalAlign = style.HorizontalAlign;
			}
			if (style.VerticalAlign != VerticalAlign.NotSet && this.VerticalAlign == VerticalAlign.NotSet)
			{
				this.VerticalAlign = style.VerticalAlign;
			}
		}

		// Token: 0x0600660D RID: 26125 RVA: 0x0017DA38 File Offset: 0x0017BC38
		internal void ImportBorderStyle(ExportStyle style, CellBorderPosition position)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			switch (position)
			{
			case CellBorderPosition.Left:
				flag2 = (flag = (flag3 = true));
				break;
			case CellBorderPosition.Right:
				flag2 = (flag = (flag4 = true));
				break;
			case CellBorderPosition.Top:
				flag3 = (flag = (flag4 = true));
				break;
			case CellBorderPosition.Bottom:
				flag3 = (flag2 = (flag4 = true));
				break;
			case CellBorderPosition.ColumnMiddle:
				flag4 = (flag3 = true);
				break;
			case CellBorderPosition.RowMiddle:
				flag2 = (flag = true);
				break;
			case CellBorderPosition.TableLeft:
				flag3 = true;
				break;
			case CellBorderPosition.TableRight:
				flag4 = true;
				break;
			case CellBorderPosition.TableTop:
				flag = true;
				break;
			case CellBorderPosition.TableBottom:
				flag2 = true;
				break;
			case CellBorderPosition.TableTopLeft:
				flag = true;
				flag3 = true;
				break;
			case CellBorderPosition.TableTopRight:
				flag = true;
				flag4 = true;
				break;
			case CellBorderPosition.TableBottomLeft:
				flag2 = true;
				flag3 = true;
				break;
			case CellBorderPosition.TableBottomRight:
				flag2 = true;
				flag4 = true;
				break;
			}
			if (flag3)
			{
				if (!style.BorderLeftColor.IsEmpty && this.BorderLeftColor.IsEmpty)
				{
					this.BorderLeftColor = style.BorderLeftColor;
				}
				if (style.BorderLeftStyle != BorderStyle.NotSet && this.BorderLeftStyle == BorderStyle.NotSet)
				{
					this.BorderLeftStyle = style.BorderLeftStyle;
				}
			}
			if (flag4)
			{
				if (!style.BorderRightColor.IsEmpty && this.BorderRightColor.IsEmpty)
				{
					this.BorderRightColor = style.BorderRightColor;
				}
				if (style.BorderRightStyle != BorderStyle.NotSet && this.BorderRightStyle == BorderStyle.NotSet)
				{
					this.BorderRightStyle = style.BorderRightStyle;
				}
			}
			if (flag)
			{
				if (!style.BorderTopColor.IsEmpty && this.BorderTopColor.IsEmpty)
				{
					this.BorderTopColor = style.BorderTopColor;
				}
				if (style.BorderTopStyle != BorderStyle.NotSet && this.BorderTopStyle == BorderStyle.NotSet)
				{
					this.BorderTopStyle = style.BorderTopStyle;
				}
			}
			if (flag2)
			{
				if (!style.BorderBottomColor.IsEmpty && this.BorderBottomColor.IsEmpty)
				{
					this.BorderBottomColor = style.BorderBottomColor;
				}
				if (style.BorderBottomStyle != BorderStyle.NotSet && this.BorderBottomStyle == BorderStyle.NotSet)
				{
					this.BorderBottomStyle = style.BorderBottomStyle;
				}
			}
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x0017DC18 File Offset: 0x0017BE18
		public bool Equals(ExportStyle other)
		{
			return this.BackColor == other.BackColor && this.BorderBottomColor == other.BorderBottomColor && this.BorderBottomStyle == other.BorderBottomStyle && this.BorderBottomWidth == other.BorderBottomWidth && this.BorderLeftColor == other.BorderLeftColor && this.BorderLeftStyle == other.BorderLeftStyle && this.BorderLeftWidth == other.BorderLeftWidth && this.BorderRightColor == other.BorderRightColor && this.BorderRightStyle == other.BorderRightStyle && this.BorderRightWidth == other.BorderRightWidth && this.BorderTopColor == other.BorderTopColor && this.BorderTopStyle == other.BorderTopStyle && this.BorderTopWidth == other.BorderTopWidth && Utils.AreFontStylesEqual(this.Font, other.Font) && this.ForeColor == other.ForeColor && this.HorizontalAlign == other.HorizontalAlign && this.VerticalAlign == other.VerticalAlign;
		}

		// Token: 0x170021A4 RID: 8612
		// (get) Token: 0x0600660F RID: 26127 RVA: 0x0017DD68 File Offset: 0x0017BF68
		public bool HasBorderStyles
		{
			get
			{
				return !this.BorderBottomColor.IsEmpty || !this.BorderTopColor.IsEmpty || !this.BorderLeftColor.IsEmpty || !this.BorderRightColor.IsEmpty;
			}
		}

		// Token: 0x040018A2 RID: 6306
		private FontInfo _font;
	}
}
