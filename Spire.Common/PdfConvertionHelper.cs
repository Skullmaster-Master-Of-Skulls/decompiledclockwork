using System;
using System.IO;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Xls.Converter;

namespace Spire.Xls
{
	// Token: 0x02000028 RID: 40
	public static class PdfConvertionHelper
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00015088 File Offset: 0x00013288
		public static void SaveToFile(this Workbook workbook, string fileName, PdfConverterSettings settings)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			workbook.SaveToPdf(fileName, null);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000150CC File Offset: 0x000132CC
		public static void SaveToStream(this Workbook workbook, Stream stream, PdfConverterSettings settings)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			workbook.SaveToPdf(stream, null);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00015110 File Offset: 0x00013310
		public static PdfDocument SaveToPdf(this Workbook workbook)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return workbook.SaveToPdf(null);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00015154 File Offset: 0x00013354
		public static void SaveToPdf(this Workbook workbook, string fileName)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			workbook.SaveToPdf(fileName, null);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00015198 File Offset: 0x00013398
		public static void SaveToPdf(this Workbook workbook, Stream stream)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			workbook.SaveToPdf(stream, null);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000151DC File Offset: 0x000133DC
		public static void SaveToPdf(this Workbook workbook, string fileName, PdfConverterSettings settings)
		{
			PdfDocument pdfDocument = workbook.SaveToPdf(settings);
			try
			{
				if (true)
				{
				}
				pdfDocument.SaveToFile(fileName);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						((IDisposable)pdfDocument).Dispose();
						goto IL_4A;
					case 2:
						goto IL_54;
					}
					if (pdfDocument != null)
					{
						num = 0;
						continue;
					}
					goto IL_54;
					IL_4A:
					num = 2;
					continue;
					IL_54:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4A;
					default:
						goto IL_6A;
					}
				}
				IL_6A:
				if (false)
				{
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00015274 File Offset: 0x00013474
		public static void SaveToPdf(this Workbook workbook, Stream stream, PdfConverterSettings settings)
		{
			PdfDocument pdfDocument = workbook.SaveToPdf(settings);
			try
			{
				pdfDocument.SaveToStream(stream);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_54;
					case 1:
						((IDisposable)pdfDocument).Dispose();
						goto IL_4A;
					}
					if (true)
					{
					}
					if (pdfDocument != null)
					{
						num = 1;
						continue;
					}
					goto IL_54;
					IL_4A:
					num = 0;
					continue;
					IL_54:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4A;
					default:
						goto IL_6A;
					}
				}
				IL_6A:
				if (false)
				{
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0001530C File Offset: 0x0001350C
		public static PdfDocument SaveToPdf(this Workbook workbook, PdfConverterSettings settings)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return PdfConvertionHelper.SaveExcelToPdf(workbook, settings);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00015350 File Offset: 0x00013550
		public static PdfDocument SaveExcelToPdf(Workbook workbook)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return PdfConvertionHelper.SaveExcelToPdf(workbook, null);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00015394 File Offset: 0x00013594
		public static PdfDocument SaveExcelToPdf(Workbook workbook, PdfConverterSettings settings)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						PageSetup pageSetup;
						if (pageSetup == null)
						{
							num = 5;
							continue;
						}
						PdfUnitConvertor pdfUnitConvertor = new PdfUnitConvertor();
						PdfPageSettings pageSettings;
						pageSettings.Width = (float)pageSetup.PageWidth;
						pageSettings.Height = (float)pageSetup.PageHeight;
						float left = pdfUnitConvertor.ConvertUnits((float)pageSetup.LeftMargin, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
						float top = pdfUnitConvertor.ConvertUnits((float)pageSetup.TopMargin, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
						float right = pdfUnitConvertor.ConvertUnits((float)pageSetup.RightMargin, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
						float bottom = pdfUnitConvertor.ConvertUnits((float)pageSetup.BottomMargin, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
						pageSettings.SetMargins(left, top, right, bottom);
						num = 8;
						continue;
					}
					case 1:
						if (settings != null)
						{
							num = 11;
							continue;
						}
						goto IL_1D5;
					case 3:
						num = 1;
						continue;
					case 4:
						goto IL_F8;
					case 5:
					{
						if (true)
						{
						}
						PdfPageSettings pageSettings;
						pageSettings.Size = PdfPageSize.A4;
						pageSettings.SetMargins(51f, 54f, 51f, 54f);
						num = 7;
						continue;
					}
					case 6:
					{
						settings = PdfConverterSettings.Default;
						PdfPageSettings pageSettings = settings.TemplateDocument.PageSettings;
						PageSetup pageSetup = workbook.Worksheets[0].PageSetup;
						num = 0;
						continue;
					}
					case 7:
						goto IL_1CC;
					case 8:
						goto IL_179;
					case 9:
						if (settings == null)
						{
							num = 6;
							continue;
						}
						goto IL_1E5;
					case 10:
						if (settings.TemplateDocument == null)
						{
							num = 4;
							continue;
						}
						goto IL_1CE;
					case 11:
						num = 10;
						continue;
					}
					if (workbook.Worksheets.Count == 0)
					{
						num = 3;
					}
					else
					{
						num = 9;
					}
				}
				IL_F8:
				goto IL_1D5;
				IL_179:
				IL_1CC:
				goto IL_1E5;
				IL_1CE:
				return settings.TemplateDocument;
				IL_1D5:
				return new PdfDocument();
				IL_1E5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_179;
				default:
				{
					if (false)
					{
					}
					PdfConverter pdfConverter = new PdfConverter(workbook);
					return pdfConverter.Convert(settings);
				}
				}
				break;
			}
			}
		}
	}
}
