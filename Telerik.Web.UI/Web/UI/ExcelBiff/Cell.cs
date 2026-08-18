using System;
using System.Drawing;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A76 RID: 2678
	internal class Cell
	{
		// Token: 0x06006712 RID: 26386 RVA: 0x00181E15 File Offset: 0x00180015
		public Cell(Worksheet worksheet)
		{
			this.worksheet = worksheet;
		}

		// Token: 0x170021EA RID: 8682
		// (get) Token: 0x06006713 RID: 26387 RVA: 0x00181E24 File Offset: 0x00180024
		public Worksheet Worksheet
		{
			get
			{
				return this.worksheet;
			}
		}

		// Token: 0x170021EB RID: 8683
		// (get) Token: 0x06006714 RID: 26388 RVA: 0x00181E2C File Offset: 0x0018002C
		// (set) Token: 0x06006715 RID: 26389 RVA: 0x00181E34 File Offset: 0x00180034
		public bool Eaten
		{
			get
			{
				return this.eaten;
			}
			set
			{
				this.eaten = value;
			}
		}

		// Token: 0x170021EC RID: 8684
		// (get) Token: 0x06006716 RID: 26390 RVA: 0x00181E3D File Offset: 0x0018003D
		// (set) Token: 0x06006717 RID: 26391 RVA: 0x00181E45 File Offset: 0x00180045
		public object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x170021ED RID: 8685
		// (get) Token: 0x06006718 RID: 26392 RVA: 0x00181E4E File Offset: 0x0018004E
		// (set) Token: 0x06006719 RID: 26393 RVA: 0x00181E56 File Offset: 0x00180056
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
			}
		}

		// Token: 0x170021EE RID: 8686
		// (get) Token: 0x0600671A RID: 26394 RVA: 0x00181E5F File Offset: 0x0018005F
		// (set) Token: 0x0600671B RID: 26395 RVA: 0x00181E67 File Offset: 0x00180067
		public Color BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}
			set
			{
				this.backgroundColor = value;
			}
		}

		// Token: 0x170021EF RID: 8687
		// (get) Token: 0x0600671C RID: 26396 RVA: 0x00181E70 File Offset: 0x00180070
		// (set) Token: 0x0600671D RID: 26397 RVA: 0x00181E78 File Offset: 0x00180078
		public Color Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		// Token: 0x170021F0 RID: 8688
		// (get) Token: 0x0600671E RID: 26398 RVA: 0x00181E81 File Offset: 0x00180081
		// (set) Token: 0x0600671F RID: 26399 RVA: 0x00181E89 File Offset: 0x00180089
		public string FontName
		{
			get
			{
				return this.fontName;
			}
			set
			{
				this.fontName = value;
			}
		}

		// Token: 0x170021F1 RID: 8689
		// (get) Token: 0x06006720 RID: 26400 RVA: 0x00181E92 File Offset: 0x00180092
		// (set) Token: 0x06006721 RID: 26401 RVA: 0x00181E9A File Offset: 0x0018009A
		public float FontSizeInPoints
		{
			get
			{
				return this.fontSizeInPoints;
			}
			set
			{
				this.fontSizeInPoints = value;
			}
		}

		// Token: 0x170021F2 RID: 8690
		// (get) Token: 0x06006722 RID: 26402 RVA: 0x00181EA3 File Offset: 0x001800A3
		// (set) Token: 0x06006723 RID: 26403 RVA: 0x00181EAB File Offset: 0x001800AB
		public bool FontBold
		{
			get
			{
				return this.fontBold;
			}
			set
			{
				this.fontBold = value;
			}
		}

		// Token: 0x170021F3 RID: 8691
		// (get) Token: 0x06006724 RID: 26404 RVA: 0x00181EB4 File Offset: 0x001800B4
		// (set) Token: 0x06006725 RID: 26405 RVA: 0x00181EBC File Offset: 0x001800BC
		public bool FontItalic
		{
			get
			{
				return this.fontItalic;
			}
			set
			{
				this.fontItalic = value;
			}
		}

		// Token: 0x170021F4 RID: 8692
		// (get) Token: 0x06006726 RID: 26406 RVA: 0x00181EC5 File Offset: 0x001800C5
		// (set) Token: 0x06006727 RID: 26407 RVA: 0x00181ECD File Offset: 0x001800CD
		public bool FontUnderline
		{
			get
			{
				return this.fontUnderline;
			}
			set
			{
				this.fontUnderline = value;
			}
		}

		// Token: 0x170021F5 RID: 8693
		// (get) Token: 0x06006728 RID: 26408 RVA: 0x00181ED6 File Offset: 0x001800D6
		// (set) Token: 0x06006729 RID: 26409 RVA: 0x00181EDE File Offset: 0x001800DE
		public bool FontStrikeout
		{
			get
			{
				return this.fontStrikeout;
			}
			set
			{
				this.fontStrikeout = value;
			}
		}

		// Token: 0x170021F6 RID: 8694
		// (get) Token: 0x0600672A RID: 26410 RVA: 0x00181EE7 File Offset: 0x001800E7
		// (set) Token: 0x0600672B RID: 26411 RVA: 0x00181EEF File Offset: 0x001800EF
		public bool RTL
		{
			get
			{
				return this.rtl;
			}
			set
			{
				this.rtl = value;
			}
		}

		// Token: 0x170021F7 RID: 8695
		// (get) Token: 0x0600672C RID: 26412 RVA: 0x00181EF8 File Offset: 0x001800F8
		// (set) Token: 0x0600672D RID: 26413 RVA: 0x00181F00 File Offset: 0x00180100
		public double RotationAngle
		{
			get
			{
				return this.rotationAngle;
			}
			set
			{
				this.rotationAngle = value;
			}
		}

		// Token: 0x170021F8 RID: 8696
		// (get) Token: 0x0600672E RID: 26414 RVA: 0x00181F09 File Offset: 0x00180109
		// (set) Token: 0x0600672F RID: 26415 RVA: 0x00181F11 File Offset: 0x00180111
		public HorizontalAlignment HorizontalAlignment
		{
			get
			{
				return this.horizontalAlignment;
			}
			set
			{
				this.horizontalAlignment = value;
			}
		}

		// Token: 0x170021F9 RID: 8697
		// (get) Token: 0x06006730 RID: 26416 RVA: 0x00181F1A File Offset: 0x0018011A
		// (set) Token: 0x06006731 RID: 26417 RVA: 0x00181F22 File Offset: 0x00180122
		public VerticalAlignment VerticalAlignment
		{
			get
			{
				return this.verticalAlignment;
			}
			set
			{
				this.verticalAlignment = value;
			}
		}

		// Token: 0x170021FA RID: 8698
		// (get) Token: 0x06006732 RID: 26418 RVA: 0x00181F2B File Offset: 0x0018012B
		// (set) Token: 0x06006733 RID: 26419 RVA: 0x00181F33 File Offset: 0x00180133
		public bool TextWrap
		{
			get
			{
				return this.textWrap;
			}
			set
			{
				this.textWrap = value;
			}
		}

		// Token: 0x170021FB RID: 8699
		// (get) Token: 0x06006734 RID: 26420 RVA: 0x00181F3C File Offset: 0x0018013C
		// (set) Token: 0x06006735 RID: 26421 RVA: 0x00181F44 File Offset: 0x00180144
		public BorderStyle TopBorderStyle
		{
			get
			{
				return this.topBorderStyle;
			}
			set
			{
				this.topBorderStyle = value;
			}
		}

		// Token: 0x170021FC RID: 8700
		// (get) Token: 0x06006736 RID: 26422 RVA: 0x00181F4D File Offset: 0x0018014D
		// (set) Token: 0x06006737 RID: 26423 RVA: 0x00181F55 File Offset: 0x00180155
		public Color TopBorderColor
		{
			get
			{
				return this.topBorderColor;
			}
			set
			{
				this.topBorderColor = value;
			}
		}

		// Token: 0x170021FD RID: 8701
		// (get) Token: 0x06006738 RID: 26424 RVA: 0x00181F5E File Offset: 0x0018015E
		// (set) Token: 0x06006739 RID: 26425 RVA: 0x00181F66 File Offset: 0x00180166
		public BorderStyle BottomBorderStyle
		{
			get
			{
				return this.bottomBorderStyle;
			}
			set
			{
				this.bottomBorderStyle = value;
			}
		}

		// Token: 0x170021FE RID: 8702
		// (get) Token: 0x0600673A RID: 26426 RVA: 0x00181F6F File Offset: 0x0018016F
		// (set) Token: 0x0600673B RID: 26427 RVA: 0x00181F77 File Offset: 0x00180177
		public Color BottomBorderColor
		{
			get
			{
				return this.bottomBorderColor;
			}
			set
			{
				this.bottomBorderColor = value;
			}
		}

		// Token: 0x170021FF RID: 8703
		// (get) Token: 0x0600673C RID: 26428 RVA: 0x00181F80 File Offset: 0x00180180
		// (set) Token: 0x0600673D RID: 26429 RVA: 0x00181F88 File Offset: 0x00180188
		public BorderStyle LeftBorderStyle
		{
			get
			{
				return this.leftBorderStyle;
			}
			set
			{
				this.leftBorderStyle = value;
			}
		}

		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x0600673E RID: 26430 RVA: 0x00181F91 File Offset: 0x00180191
		// (set) Token: 0x0600673F RID: 26431 RVA: 0x00181F99 File Offset: 0x00180199
		public Color LeftBorderColor
		{
			get
			{
				return this.leftBorderColor;
			}
			set
			{
				this.leftBorderColor = value;
			}
		}

		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x06006740 RID: 26432 RVA: 0x00181FA2 File Offset: 0x001801A2
		// (set) Token: 0x06006741 RID: 26433 RVA: 0x00181FAA File Offset: 0x001801AA
		public BorderStyle RightBorderStyle
		{
			get
			{
				return this.rightBorderStyle;
			}
			set
			{
				this.rightBorderStyle = value;
			}
		}

		// Token: 0x17002202 RID: 8706
		// (get) Token: 0x06006742 RID: 26434 RVA: 0x00181FB3 File Offset: 0x001801B3
		// (set) Token: 0x06006743 RID: 26435 RVA: 0x00181FBB File Offset: 0x001801BB
		public Color RightBorderColor
		{
			get
			{
				return this.rightBorderColor;
			}
			set
			{
				this.rightBorderColor = value;
			}
		}

		// Token: 0x06006744 RID: 26436 RVA: 0x00181FC4 File Offset: 0x001801C4
		public BiffCell CreateBiffCell()
		{
			BiffCell biffCell;
			if (this.eaten)
			{
				biffCell = new BlankCell();
			}
			else if (this.value == null)
			{
				biffCell = new BlankCell();
			}
			else if (this.value is string)
			{
				biffCell = this.worksheet.Workbook.CreateStringCell((string)this.value);
			}
			else if (this.value is char)
			{
				biffCell = this.worksheet.Workbook.CreateStringCell(this.value.ToString());
			}
			else
			{
				if (!(this.value is sbyte) && !(this.value is byte) && !(this.value is short) && !(this.value is ushort) && !(this.value is int) && !(this.value is uint) && !(this.value is long) && !(this.value is ulong) && !(this.value is float) && !(this.value is double))
				{
					if (!(this.value is decimal))
					{
						if (this.value is DateTime)
						{
							DateTime dtValue = (DateTime)this.value;
							biffCell = new DateCell(dtValue);
							goto IL_189;
						}
						biffCell = this.worksheet.Workbook.CreateStringCell(this.value.ToString());
						goto IL_189;
					}
				}
				double num;
				try
				{
					num = Convert.ToDouble(this.value);
				}
				catch (InvalidCastException ex)
				{
					return this.worksheet.Workbook.CreateStringCell(ex.Message);
				}
				biffCell = new NumberCell(num);
			}
			IL_189:
			biffCell.XFIndex = StyleHandler.GetXFIndex(this);
			return biffCell;
		}

		// Token: 0x040019E7 RID: 6631
		private Worksheet worksheet;

		// Token: 0x040019E8 RID: 6632
		private bool eaten;

		// Token: 0x040019E9 RID: 6633
		private object value;

		// Token: 0x040019EA RID: 6634
		private string format;

		// Token: 0x040019EB RID: 6635
		private Color backgroundColor;

		// Token: 0x040019EC RID: 6636
		private Color color;

		// Token: 0x040019ED RID: 6637
		private string fontName;

		// Token: 0x040019EE RID: 6638
		private float fontSizeInPoints;

		// Token: 0x040019EF RID: 6639
		private bool fontBold;

		// Token: 0x040019F0 RID: 6640
		private bool fontItalic;

		// Token: 0x040019F1 RID: 6641
		private bool fontUnderline;

		// Token: 0x040019F2 RID: 6642
		private bool fontStrikeout;

		// Token: 0x040019F3 RID: 6643
		private bool rtl;

		// Token: 0x040019F4 RID: 6644
		private double rotationAngle;

		// Token: 0x040019F5 RID: 6645
		private HorizontalAlignment horizontalAlignment;

		// Token: 0x040019F6 RID: 6646
		private VerticalAlignment verticalAlignment;

		// Token: 0x040019F7 RID: 6647
		private bool textWrap;

		// Token: 0x040019F8 RID: 6648
		private BorderStyle topBorderStyle;

		// Token: 0x040019F9 RID: 6649
		private Color topBorderColor;

		// Token: 0x040019FA RID: 6650
		private BorderStyle bottomBorderStyle;

		// Token: 0x040019FB RID: 6651
		private Color bottomBorderColor;

		// Token: 0x040019FC RID: 6652
		private BorderStyle leftBorderStyle;

		// Token: 0x040019FD RID: 6653
		private Color leftBorderColor;

		// Token: 0x040019FE RID: 6654
		private BorderStyle rightBorderStyle;

		// Token: 0x040019FF RID: 6655
		private Color rightBorderColor;
	}
}
