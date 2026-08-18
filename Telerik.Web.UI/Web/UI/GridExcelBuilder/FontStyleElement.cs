using System;
using System.Drawing;
using System.Text;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B1A RID: 6938
	public class FontStyleElement : ElementBase
	{
		// Token: 0x170051C5 RID: 20933
		// (get) Token: 0x06010C9F RID: 68767 RVA: 0x003BA196 File Offset: 0x003B8396
		// (set) Token: 0x06010CA0 RID: 68768 RVA: 0x003BA19E File Offset: 0x003B839E
		public bool Underline
		{
			get
			{
				return this._isUnderline;
			}
			set
			{
				this._isUnderline = value;
			}
		}

		// Token: 0x170051C6 RID: 20934
		// (get) Token: 0x06010CA1 RID: 68769 RVA: 0x003BA1A7 File Offset: 0x003B83A7
		// (set) Token: 0x06010CA2 RID: 68770 RVA: 0x003BA1AF File Offset: 0x003B83AF
		public double Size
		{
			get
			{
				return this._size;
			}
			set
			{
				if (value <= 0.0)
				{
					throw new ArgumentOutOfRangeException("Size must be greater then 0");
				}
				this._size = value;
			}
		}

		// Token: 0x170051C7 RID: 20935
		// (get) Token: 0x06010CA3 RID: 68771 RVA: 0x003BA1CF File Offset: 0x003B83CF
		// (set) Token: 0x06010CA4 RID: 68772 RVA: 0x003BA1D7 File Offset: 0x003B83D7
		public bool Italic
		{
			get
			{
				return this._isItalic;
			}
			set
			{
				this._isItalic = value;
			}
		}

		// Token: 0x170051C8 RID: 20936
		// (get) Token: 0x06010CA5 RID: 68773 RVA: 0x003BA1E0 File Offset: 0x003B83E0
		// (set) Token: 0x06010CA6 RID: 68774 RVA: 0x003BA1FB File Offset: 0x003B83FB
		public string FontName
		{
			get
			{
				if (this._fontName == null)
				{
					this._fontName = string.Empty;
				}
				return this._fontName;
			}
			set
			{
				this._fontName = value;
			}
		}

		// Token: 0x170051C9 RID: 20937
		// (get) Token: 0x06010CA7 RID: 68775 RVA: 0x003BA204 File Offset: 0x003B8404
		// (set) Token: 0x06010CA8 RID: 68776 RVA: 0x003BA20C File Offset: 0x003B840C
		public Color Color
		{
			get
			{
				return this._color;
			}
			set
			{
				this._color = value;
			}
		}

		// Token: 0x170051CA RID: 20938
		// (get) Token: 0x06010CA9 RID: 68777 RVA: 0x003BA215 File Offset: 0x003B8415
		// (set) Token: 0x06010CAA RID: 68778 RVA: 0x003BA21D File Offset: 0x003B841D
		public bool Bold
		{
			get
			{
				return this._isBold;
			}
			set
			{
				this._isBold = value;
			}
		}

		// Token: 0x170051CB RID: 20939
		// (get) Token: 0x06010CAB RID: 68779 RVA: 0x003BA226 File Offset: 0x003B8426
		protected override string StartTag
		{
			get
			{
				return "<Font{0}>";
			}
		}

		// Token: 0x170051CC RID: 20940
		// (get) Token: 0x06010CAC RID: 68780 RVA: 0x003BA22D File Offset: 0x003B842D
		protected override string EndTag
		{
			get
			{
				return "</Font>";
			}
		}

		// Token: 0x06010CAD RID: 68781 RVA: 0x003BA234 File Offset: 0x003B8434
		protected override void AppendAttributes(StringBuilder sb)
		{
			if (this.Bold)
			{
				base.Attributes.Add("ss:Bold", Convert.ToInt16(this.Bold).ToString());
			}
			if (this.Italic)
			{
				base.Attributes.Add("ss:Italic", Convert.ToInt16(this.Italic).ToString());
			}
			if (this.Underline)
			{
				base.Attributes.Add("ss:Underline", "Single");
			}
			if (this.Color != Color.Empty)
			{
				base.Attributes.Add("ss:Color", Utils.ConvertColor(this.Color));
			}
			if (this.FontName.Trim().Length > 0 && this.FontName.Trim().ToLower() != "arial")
			{
				base.Attributes.Add("ss:FontName", this.FontName.Trim());
			}
			if (this.Size != 10.0)
			{
				base.Attributes.Add("ss:Size", this.Size.ToString());
			}
			base.AppendAttributes(sb);
		}

		// Token: 0x04004AED RID: 19181
		private bool _isUnderline;

		// Token: 0x04004AEE RID: 19182
		private double _size = 10.0;

		// Token: 0x04004AEF RID: 19183
		private bool _isItalic;

		// Token: 0x04004AF0 RID: 19184
		private string _fontName;

		// Token: 0x04004AF1 RID: 19185
		private Color _color;

		// Token: 0x04004AF2 RID: 19186
		private bool _isBold;
	}
}
