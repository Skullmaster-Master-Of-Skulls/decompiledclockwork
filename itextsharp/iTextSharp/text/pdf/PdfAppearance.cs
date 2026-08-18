using System;
using System.Collections.Generic;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004E3 RID: 1251
	public class PdfAppearance : PdfTemplate
	{
		// Token: 0x06002ACE RID: 10958 RVA: 0x0010410C File Offset: 0x0010310C
		static PdfAppearance()
		{
			PdfAppearance.stdFieldFontNames["Courier-BoldOblique"] = new PdfName("CoBO");
			PdfAppearance.stdFieldFontNames["Courier-Bold"] = new PdfName("CoBo");
			PdfAppearance.stdFieldFontNames["Courier-Oblique"] = new PdfName("CoOb");
			PdfAppearance.stdFieldFontNames["Courier"] = new PdfName("Cour");
			PdfAppearance.stdFieldFontNames["Helvetica-BoldOblique"] = new PdfName("HeBO");
			PdfAppearance.stdFieldFontNames["Helvetica-Bold"] = new PdfName("HeBo");
			PdfAppearance.stdFieldFontNames["Helvetica-Oblique"] = new PdfName("HeOb");
			PdfAppearance.stdFieldFontNames["Helvetica"] = PdfName.HELV;
			PdfAppearance.stdFieldFontNames["Symbol"] = new PdfName("Symb");
			PdfAppearance.stdFieldFontNames["Times-BoldItalic"] = new PdfName("TiBI");
			PdfAppearance.stdFieldFontNames["Times-Bold"] = new PdfName("TiBo");
			PdfAppearance.stdFieldFontNames["Times-Italic"] = new PdfName("TiIt");
			PdfAppearance.stdFieldFontNames["Times-Roman"] = new PdfName("TiRo");
			PdfAppearance.stdFieldFontNames["ZapfDingbats"] = PdfName.ZADB;
			PdfAppearance.stdFieldFontNames["HYSMyeongJo-Medium"] = new PdfName("HySm");
			PdfAppearance.stdFieldFontNames["HYGoThic-Medium"] = new PdfName("HyGo");
			PdfAppearance.stdFieldFontNames["HeiseiKakuGo-W5"] = new PdfName("KaGo");
			PdfAppearance.stdFieldFontNames["HeiseiMin-W3"] = new PdfName("KaMi");
			PdfAppearance.stdFieldFontNames["MHei-Medium"] = new PdfName("MHei");
			PdfAppearance.stdFieldFontNames["MSung-Light"] = new PdfName("MSun");
			PdfAppearance.stdFieldFontNames["STSong-Light"] = new PdfName("STSo");
			PdfAppearance.stdFieldFontNames["MSungStd-Light"] = new PdfName("MSun");
			PdfAppearance.stdFieldFontNames["STSongStd-Light"] = new PdfName("STSo");
			PdfAppearance.stdFieldFontNames["HYSMyeongJoStd-Medium"] = new PdfName("HySm");
			PdfAppearance.stdFieldFontNames["KozMinPro-Regular"] = new PdfName("KaMi");
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0010438A File Offset: 0x0010338A
		internal PdfAppearance()
		{
			this.separator = 32;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0010439A File Offset: 0x0010339A
		internal PdfAppearance(PdfIndirectReference iref)
		{
			this.thisReference = iref;
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x001043A9 File Offset: 0x001033A9
		internal PdfAppearance(PdfWriter wr) : base(wr)
		{
			this.separator = 32;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x001043BA File Offset: 0x001033BA
		public static PdfAppearance CreateAppearance(PdfWriter writer, float width, float height)
		{
			return PdfAppearance.CreateAppearance(writer, width, height, null);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x001043C8 File Offset: 0x001033C8
		internal static PdfAppearance CreateAppearance(PdfWriter writer, float width, float height, PdfName forcedName)
		{
			PdfAppearance pdfAppearance = new PdfAppearance(writer);
			pdfAppearance.Width = width;
			pdfAppearance.Height = height;
			writer.AddDirectTemplateSimple(pdfAppearance, forcedName);
			return pdfAppearance;
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x001043F4 File Offset: 0x001033F4
		public override void SetFontAndSize(BaseFont bf, float size)
		{
			this.CheckWriter();
			this.state.size = size;
			if (bf.FontType == 4)
			{
				this.state.fontDetails = new FontDetails(null, ((DocumentFont)bf).IndirectReference, bf);
			}
			else
			{
				this.state.fontDetails = this.writer.AddSimple(bf);
			}
			PdfName pdfName;
			PdfAppearance.stdFieldFontNames.TryGetValue(bf.PostscriptFontName, out pdfName);
			if (pdfName == null)
			{
				if (bf.Subset && bf.FontType == 3)
				{
					pdfName = this.state.fontDetails.FontName;
				}
				else
				{
					pdfName = new PdfName(bf.PostscriptFontName);
					this.state.fontDetails.Subset = false;
				}
			}
			PageResources pageResources = this.PageResources;
			pageResources.AddFont(pdfName, this.state.fontDetails.IndirectReference);
			this.content.Append(pdfName.GetBytes()).Append(' ').Append(size).Append(" Tf").Append_i(this.separator);
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x001044FC File Offset: 0x001034FC
		public override PdfContentByte Duplicate
		{
			get
			{
				PdfAppearance pdfAppearance = new PdfAppearance();
				pdfAppearance.writer = this.writer;
				pdfAppearance.pdf = this.pdf;
				pdfAppearance.thisReference = this.thisReference;
				pdfAppearance.pageResources = this.pageResources;
				pdfAppearance.bBox = new Rectangle(this.bBox);
				pdfAppearance.group = this.group;
				pdfAppearance.layer = this.layer;
				if (this.matrix != null)
				{
					pdfAppearance.matrix = new PdfArray(this.matrix);
				}
				pdfAppearance.separator = this.separator;
				return pdfAppearance;
			}
		}

		// Token: 0x04001DA1 RID: 7585
		public static Dictionary<string, PdfName> stdFieldFontNames = new Dictionary<string, PdfName>();
	}
}
