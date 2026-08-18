using System;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x0200168C RID: 5772
	internal sealed class FontProperties
	{
		// Token: 0x0600DEF0 RID: 57072 RVA: 0x00311679 File Offset: 0x0030F879
		public FontProperties(string faceName, bool bold, bool italic)
		{
			this.faceName = faceName;
			this.bold = bold;
			this.italic = italic;
		}

		// Token: 0x1700443C RID: 17468
		// (get) Token: 0x0600DEF1 RID: 57073 RVA: 0x00311696 File Offset: 0x0030F896
		public string FaceName
		{
			get
			{
				return this.faceName;
			}
		}

		// Token: 0x1700443D RID: 17469
		// (get) Token: 0x0600DEF2 RID: 57074 RVA: 0x0031169E File Offset: 0x0030F89E
		public bool IsRegular
		{
			get
			{
				return !this.IsBold && !this.IsItalic;
			}
		}

		// Token: 0x1700443E RID: 17470
		// (get) Token: 0x0600DEF3 RID: 57075 RVA: 0x003116B3 File Offset: 0x0030F8B3
		public bool IsBold
		{
			get
			{
				return this.bold;
			}
		}

		// Token: 0x1700443F RID: 17471
		// (get) Token: 0x0600DEF4 RID: 57076 RVA: 0x003116BB File Offset: 0x0030F8BB
		public bool IsItalic
		{
			get
			{
				return this.italic;
			}
		}

		// Token: 0x17004440 RID: 17472
		// (get) Token: 0x0600DEF5 RID: 57077 RVA: 0x003116C3 File Offset: 0x0030F8C3
		public bool IsBoldItalic
		{
			get
			{
				return this.IsBold && this.IsItalic;
			}
		}

		// Token: 0x04004040 RID: 16448
		private string faceName;

		// Token: 0x04004041 RID: 16449
		private bool bold;

		// Token: 0x04004042 RID: 16450
		private bool italic;
	}
}
