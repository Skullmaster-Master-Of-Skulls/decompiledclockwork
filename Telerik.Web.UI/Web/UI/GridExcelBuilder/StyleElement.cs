using System;
using System.Drawing;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B29 RID: 6953
	public class StyleElement : ElementBase
	{
		// Token: 0x06010D15 RID: 68885 RVA: 0x003BBA90 File Offset: 0x003B9C90
		public StyleElement(string id)
		{
			this.Id = id;
		}

		// Token: 0x06010D16 RID: 68886 RVA: 0x003BBA9F File Offset: 0x003B9C9F
		public StyleElement() : this(string.Empty)
		{
		}

		// Token: 0x170051EB RID: 20971
		// (get) Token: 0x06010D17 RID: 68887 RVA: 0x003BBAAC File Offset: 0x003B9CAC
		public AlignmentStyleElement AlignmentElement
		{
			get
			{
				AlignmentStyleElement result;
				if ((result = this._alignmentElement) == null)
				{
					result = (this._alignmentElement = new AlignmentStyleElement());
				}
				return result;
			}
		}

		// Token: 0x170051EC RID: 20972
		// (get) Token: 0x06010D18 RID: 68888 RVA: 0x003BBAD4 File Offset: 0x003B9CD4
		public virtual IBorderStylesCollection Borders
		{
			get
			{
				IBorderStylesCollection result;
				if ((result = this._bordersCollection) == null)
				{
					result = (this._bordersCollection = new BorderStylesCollection());
				}
				return result;
			}
		}

		// Token: 0x170051ED RID: 20973
		// (get) Token: 0x06010D19 RID: 68889 RVA: 0x003BBAFC File Offset: 0x003B9CFC
		public NumberFormatStyleElement NumberFormat
		{
			get
			{
				NumberFormatStyleElement result;
				if ((result = this._numberFormat) == null)
				{
					result = (this._numberFormat = new NumberFormatStyleElement());
				}
				return result;
			}
		}

		// Token: 0x170051EE RID: 20974
		// (get) Token: 0x06010D1A RID: 68890 RVA: 0x003BBB24 File Offset: 0x003B9D24
		public virtual InteriorStyleElement InteriorStyle
		{
			get
			{
				InteriorStyleElement result;
				if ((result = this._interiorStyle) == null)
				{
					result = (this._interiorStyle = new InteriorStyleElement());
				}
				return result;
			}
		}

		// Token: 0x170051EF RID: 20975
		// (get) Token: 0x06010D1B RID: 68891 RVA: 0x003BBB4C File Offset: 0x003B9D4C
		public virtual FontStyleElement FontStyle
		{
			get
			{
				FontStyleElement result;
				if ((result = this._fontStyle) == null)
				{
					result = (this._fontStyle = new FontStyleElement());
				}
				return result;
			}
		}

		// Token: 0x170051F0 RID: 20976
		// (get) Token: 0x06010D1C RID: 68892 RVA: 0x003BBB74 File Offset: 0x003B9D74
		public CellProtectionElement CellProtection
		{
			get
			{
				CellProtectionElement result;
				if ((result = this._cellProtection) == null)
				{
					result = (this._cellProtection = new CellProtectionElement());
				}
				return result;
			}
		}

		// Token: 0x170051F1 RID: 20977
		// (get) Token: 0x06010D1D RID: 68893 RVA: 0x003BBB99 File Offset: 0x003B9D99
		// (set) Token: 0x06010D1E RID: 68894 RVA: 0x003BBBA1 File Offset: 0x003B9DA1
		public string Id { get; set; }

		// Token: 0x170051F2 RID: 20978
		// (get) Token: 0x06010D1F RID: 68895 RVA: 0x003BBBAA File Offset: 0x003B9DAA
		protected override string StartTag
		{
			get
			{
				return "<Style{0}>";
			}
		}

		// Token: 0x170051F3 RID: 20979
		// (get) Token: 0x06010D20 RID: 68896 RVA: 0x003BBBB1 File Offset: 0x003B9DB1
		protected override string EndTag
		{
			get
			{
				return "</Style>";
			}
		}

		// Token: 0x06010D21 RID: 68897 RVA: 0x003BBBB8 File Offset: 0x003B9DB8
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.Id.Trim() == string.Empty && !base.Attributes.Contains("ss:ID"))
			{
				throw new Exception("Id must be set");
			}
			if (this.Id.Trim() != string.Empty)
			{
				base.Attributes.Add("ss:ID", this.Id.Trim());
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x06010D22 RID: 68898 RVA: 0x003BBC34 File Offset: 0x003B9E34
		protected override void RenderChildElements(StringBuilder sb)
		{
			((IElement)this.AlignmentElement).Render(sb);
			if (this.Borders.Count > 0)
			{
				sb.Append("<Borders>");
				foreach (object obj in this.Borders)
				{
					BorderStyles borderStyles = (BorderStyles)obj;
					if (borderStyles != null && borderStyles.PositionType != PositionType.None)
					{
						((IElement)borderStyles).Render(sb);
					}
				}
				sb.Append("</Borders>");
			}
			((IElement)this.FontStyle).Render(sb);
			if (this.InteriorStyle.Color != Color.Empty)
			{
				((IElement)this.InteriorStyle).Render(sb);
			}
			((IElement)this.NumberFormat).Render(sb);
			this.CellProtection.Render(sb);
			base.RenderChildElements(sb);
		}

		// Token: 0x04004B3A RID: 19258
		private AlignmentStyleElement _alignmentElement;

		// Token: 0x04004B3B RID: 19259
		private IBorderStylesCollection _bordersCollection;

		// Token: 0x04004B3C RID: 19260
		private CellProtectionElement _cellProtection;

		// Token: 0x04004B3D RID: 19261
		private FontStyleElement _fontStyle;

		// Token: 0x04004B3E RID: 19262
		private InteriorStyleElement _interiorStyle;

		// Token: 0x04004B3F RID: 19263
		private NumberFormatStyleElement _numberFormat;
	}
}
