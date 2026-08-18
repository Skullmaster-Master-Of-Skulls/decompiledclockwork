using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000526 RID: 1318
	public class PdfPage : PdfDictionary
	{
		// Token: 0x06002CE1 RID: 11489 RVA: 0x00111FBC File Offset: 0x00110FBC
		internal PdfPage(PdfRectangle mediaBox, Dictionary<string, PdfRectangle> boxSize, PdfDictionary resources, int rotate) : base(PdfDictionary.PAGE)
		{
			this.mediaBox = mediaBox;
			base.Put(PdfName.MEDIABOX, mediaBox);
			base.Put(PdfName.RESOURCES, resources);
			if (rotate != 0)
			{
				base.Put(PdfName.ROTATE, new PdfNumber(rotate));
			}
			for (int i = 0; i < PdfPage.boxStrings.Length; i++)
			{
				if (boxSize.ContainsKey(PdfPage.boxStrings[i]))
				{
					base.Put(PdfPage.boxNames[i], boxSize[PdfPage.boxStrings[i]]);
				}
			}
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x00112044 File Offset: 0x00111044
		internal PdfPage(PdfRectangle mediaBox, Dictionary<string, PdfRectangle> boxSize, PdfDictionary resources) : this(mediaBox, boxSize, resources, 0)
		{
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x00112050 File Offset: 0x00111050
		public bool IsParent()
		{
			return false;
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x00112053 File Offset: 0x00111053
		internal void Add(PdfIndirectReference contents)
		{
			base.Put(PdfName.CONTENTS, contents);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x00112061 File Offset: 0x00111061
		internal PdfRectangle RotateMediaBox()
		{
			this.mediaBox = this.mediaBox.Rotate;
			base.Put(PdfName.MEDIABOX, this.mediaBox);
			return this.mediaBox;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x0011208B File Offset: 0x0011108B
		internal PdfRectangle MediaBox
		{
			get
			{
				return this.mediaBox;
			}
		}

		// Token: 0x04001F03 RID: 7939
		private static string[] boxStrings = new string[]
		{
			"crop",
			"trim",
			"art",
			"bleed"
		};

		// Token: 0x04001F04 RID: 7940
		private static PdfName[] boxNames = new PdfName[]
		{
			PdfName.CROPBOX,
			PdfName.TRIMBOX,
			PdfName.ARTBOX,
			PdfName.BLEEDBOX
		};

		// Token: 0x04001F05 RID: 7941
		public static PdfNumber PORTRAIT = new PdfNumber(0);

		// Token: 0x04001F06 RID: 7942
		public static PdfNumber LANDSCAPE = new PdfNumber(90);

		// Token: 0x04001F07 RID: 7943
		public static PdfNumber INVERTEDPORTRAIT = new PdfNumber(180);

		// Token: 0x04001F08 RID: 7944
		public static PdfNumber SEASCAPE = new PdfNumber(270);

		// Token: 0x04001F09 RID: 7945
		private PdfRectangle mediaBox;
	}
}
