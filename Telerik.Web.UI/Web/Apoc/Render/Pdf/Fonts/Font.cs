using System;
using Telerik.Pdf;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x02001683 RID: 5763
	internal abstract class Font : IFontMetric
	{
		// Token: 0x17004412 RID: 17426
		// (get) Token: 0x0600DEB4 RID: 57012
		public abstract string Encoding { get; }

		// Token: 0x17004413 RID: 17427
		// (get) Token: 0x0600DEB5 RID: 57013
		public abstract string FontName { get; }

		// Token: 0x17004414 RID: 17428
		// (get) Token: 0x0600DEB6 RID: 57014
		public abstract PdfFontTypeEnum Type { get; }

		// Token: 0x17004415 RID: 17429
		// (get) Token: 0x0600DEB7 RID: 57015
		public abstract PdfFontSubTypeEnum SubType { get; }

		// Token: 0x17004416 RID: 17430
		// (get) Token: 0x0600DEB8 RID: 57016
		public abstract IFontDescriptor Descriptor { get; }

		// Token: 0x17004417 RID: 17431
		// (get) Token: 0x0600DEB9 RID: 57017
		public abstract bool MultiByteFont { get; }

		// Token: 0x0600DEBA RID: 57018
		public abstract int MapCharacter(char c);

		// Token: 0x17004418 RID: 17432
		// (get) Token: 0x0600DEBB RID: 57019
		public abstract int Ascender { get; }

		// Token: 0x17004419 RID: 17433
		// (get) Token: 0x0600DEBC RID: 57020
		public abstract int Descender { get; }

		// Token: 0x1700441A RID: 17434
		// (get) Token: 0x0600DEBD RID: 57021
		public abstract int CapHeight { get; }

		// Token: 0x1700441B RID: 17435
		// (get) Token: 0x0600DEBE RID: 57022
		public abstract int FirstChar { get; }

		// Token: 0x1700441C RID: 17436
		// (get) Token: 0x0600DEBF RID: 57023
		public abstract int LastChar { get; }

		// Token: 0x0600DEC0 RID: 57024
		public abstract int GetWidth(int charIndex);

		// Token: 0x1700441D RID: 17437
		// (get) Token: 0x0600DEC1 RID: 57025
		public abstract int[] Widths { get; }
	}
}
