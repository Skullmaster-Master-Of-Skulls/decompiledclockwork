using System;
using Telerik.Web.Apoc.DataTypes;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x02001418 RID: 5144
	internal class FOText : FONode
	{
		// Token: 0x0600D2C9 RID: 53961 RVA: 0x002EC74C File Offset: 0x002EA94C
		public FOText(char[] chars, int s, int e, FObj parent) : base(parent)
		{
			this.start = 0;
			this.ca = new char[e - s];
			for (int i = s; i < e; i++)
			{
				this.ca[i - s] = chars[i];
			}
			this.length = e - s;
		}

		// Token: 0x0600D2CA RID: 53962 RVA: 0x002EC798 File Offset: 0x002EA998
		public void setUnderlined(bool ul)
		{
			this.underlined = ul;
		}

		// Token: 0x0600D2CB RID: 53963 RVA: 0x002EC7A1 File Offset: 0x002EA9A1
		public void setOverlined(bool ol)
		{
			this.overlined = ol;
		}

		// Token: 0x0600D2CC RID: 53964 RVA: 0x002EC7AA File Offset: 0x002EA9AA
		public void setLineThrough(bool lt)
		{
			this.lineThrough = lt;
		}

		// Token: 0x0600D2CD RID: 53965 RVA: 0x002EC7B4 File Offset: 0x002EA9B4
		public bool willCreateArea()
		{
			this.whiteSpaceCollapse = this.parent.properties.GetProperty("white-space-collapse").GetEnum();
			if (this.whiteSpaceCollapse == 27 && this.length > 0)
			{
				return true;
			}
			for (int i = this.start; i < this.start + this.length; i++)
			{
				char c = this.ca[i];
				if (c != ' ' && c != '\n' && c != '\r' && c != '\u001f' && c != '\t')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600D2CE RID: 53966 RVA: 0x002EC838 File Offset: 0x002EAA38
		public override bool MayPrecedeMarker()
		{
			for (int i = 0; i < this.ca.Length; i++)
			{
				char c = this.ca[i];
				if (c != ' ' || c != '\n' || c != '\r' || c != '\t' || c != '\u001f')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600D2CF RID: 53967 RVA: 0x002EC880 File Offset: 0x002EAA80
		public override Status Layout(Area area)
		{
			BlockArea blockArea = area as BlockArea;
			if (blockArea == null)
			{
				ApocDriver.ActiveDriver.FireApocError("Text outside block area" + new string(this.ca, this.start, this.length));
				return new Status(1);
			}
			if (this.marker == -1000)
			{
				string @string = this.parent.properties.GetProperty("font-family").GetString();
				string string2 = this.parent.properties.GetProperty("font-style").GetString();
				string string3 = this.parent.properties.GetProperty("font-weight").GetString();
				int fontSize = this.parent.properties.GetProperty("font-size").GetLength().MValue();
				int @enum = this.parent.properties.GetProperty("font-variant").GetEnum();
				int letterSpacing = this.parent.properties.GetProperty("letter-spacing").GetLength().MValue();
				this.fs = new FontState(area.getFontInfo(), @string, string2, string3, fontSize, @enum, letterSpacing);
				ColorType colorType = this.parent.properties.GetProperty("color").GetColorType();
				this.red = colorType.Red;
				this.green = colorType.Green;
				this.blue = colorType.Blue;
				this.verticalAlign = this.parent.properties.GetProperty("vertical-align").GetEnum();
				this.wrapOption = this.parent.properties.GetProperty("wrap-option").GetEnum();
				this.whiteSpaceCollapse = this.parent.properties.GetProperty("white-space-collapse").GetEnum();
				this.ts = new TextState();
				this.ts.setUnderlined(this.underlined);
				this.ts.setOverlined(this.overlined);
				this.ts.setLineThrough(this.lineThrough);
				this.marker = this.start;
			}
			int marker = this.marker;
			this.marker = FOText.addText(blockArea, this.fs, this.red, this.green, this.blue, this.wrapOption, this.GetLinkSet(), this.whiteSpaceCollapse, this.ca, this.marker, this.length, this.ts, this.verticalAlign);
			if (this.marker == -1)
			{
				return new Status(1);
			}
			if (this.marker != marker)
			{
				return new Status(3);
			}
			return new Status(2);
		}

		// Token: 0x0600D2D0 RID: 53968 RVA: 0x002ECB14 File Offset: 0x002EAD14
		public static int addText(BlockArea ba, FontState fontState, float red, float green, float blue, int wrapOption, LinkSet ls, int whiteSpaceCollapse, char[] data, int start, int end, TextState textState, int vAlign)
		{
			if (fontState.FontVariant == 69)
			{
				FontState fontState2;
				try
				{
					int fontSize = (int)((double)fontState.FontSize * 0.8);
					fontState2 = new FontState(fontState.FontInfo, fontState.FontFamily, fontState.FontStyle, fontState.FontWeight, fontSize, 52);
				}
				catch (ApocException ex)
				{
					fontState2 = fontState;
					ApocDriver.ActiveDriver.FireApocError("Error creating small-caps FontState: " + ex.Message);
				}
				int i = start;
				while (i < end)
				{
					int num = i;
					char c = data[i];
					bool flag = char.IsLetter(c) && char.IsLower(c);
					while (flag == (char.IsLetter(c) && char.IsLower(c)))
					{
						if (flag)
						{
							data[i] = char.ToUpper(c);
						}
						i++;
						if (i == end)
						{
							break;
						}
						c = data[i];
					}
					FontState fontState3;
					if (flag)
					{
						fontState3 = fontState2;
					}
					else
					{
						fontState3 = fontState;
					}
					int num2 = FOText.addRealText(ba, fontState3, red, green, blue, wrapOption, ls, whiteSpaceCollapse, data, num, i, textState, vAlign);
					if (num2 != -1)
					{
						return num2;
					}
				}
				return -1;
			}
			return FOText.addRealText(ba, fontState, red, green, blue, wrapOption, ls, whiteSpaceCollapse, data, start, end, textState, vAlign);
		}

		// Token: 0x0600D2D1 RID: 53969 RVA: 0x002ECC4C File Offset: 0x002EAE4C
		protected static int addRealText(BlockArea ba, FontState fontState, float red, float green, float blue, int wrapOption, LinkSet ls, int whiteSpaceCollapse, char[] data, int start, int end, TextState textState, int vAlign)
		{
			LineArea lineArea = ba.getCurrentLineArea();
			if (lineArea == null)
			{
				return start;
			}
			lineArea.changeFont(fontState);
			lineArea.changeColor(red, green, blue);
			lineArea.changeWrapOption(wrapOption);
			lineArea.changeWhiteSpaceCollapse(whiteSpaceCollapse);
			lineArea.changeVerticalAlign(vAlign);
			ba.setupLinkSet(ls);
			for (int num = lineArea.addText(data, start, end, ls, textState); num != -1; num = lineArea.addText(data, num, end, ls, textState))
			{
				lineArea = ba.createNextLineArea();
				if (lineArea == null)
				{
					return num;
				}
				lineArea.changeFont(fontState);
				lineArea.changeColor(red, green, blue);
				lineArea.changeWrapOption(wrapOption);
				lineArea.changeWhiteSpaceCollapse(whiteSpaceCollapse);
				ba.setupLinkSet(ls);
			}
			return -1;
		}

		// Token: 0x04003908 RID: 14600
		protected char[] ca;

		// Token: 0x04003909 RID: 14601
		protected int start;

		// Token: 0x0400390A RID: 14602
		protected int length;

		// Token: 0x0400390B RID: 14603
		private FontState fs;

		// Token: 0x0400390C RID: 14604
		private float red;

		// Token: 0x0400390D RID: 14605
		private float green;

		// Token: 0x0400390E RID: 14606
		private float blue;

		// Token: 0x0400390F RID: 14607
		private int wrapOption;

		// Token: 0x04003910 RID: 14608
		private int whiteSpaceCollapse;

		// Token: 0x04003911 RID: 14609
		private int verticalAlign;

		// Token: 0x04003912 RID: 14610
		protected bool underlined;

		// Token: 0x04003913 RID: 14611
		protected bool overlined;

		// Token: 0x04003914 RID: 14612
		protected bool lineThrough;

		// Token: 0x04003915 RID: 14613
		private TextState ts;
	}
}
