using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.Output
{
	// Token: 0x020002C8 RID: 712
	[Serializable]
	public enum eAllowedExtensionGroup
	{
		// Token: 0x040011E5 RID: 4581
		[AllowedExtensionGroup]
		Unknown,
		// Token: 0x040011E6 RID: 4582
		[AllowedExtensionGroup("Common.MergedDocumentWord.dll", "TechnoPro.Common.MergedDocumentWord.WordMergedDocument", "", new string[]
		{
			".doc",
			".docx",
			".rtf",
			".txt"
		})]
		AllowedWordExtensions,
		// Token: 0x040011E7 RID: 4583
		[AllowedExtensionGroup("Common.MergedDocumentText.dll", "TechnoPro.Common.MergedDocumentText.TextMergedDocument", "", new string[]
		{
			".html",
			".htm",
			".txt"
		})]
		AllowedTextExtensions,
		// Token: 0x040011E8 RID: 4584
		[AllowedExtensionGroup("Common.MergedDocumentPdf.dll", "TechnoPro.Common.MergedDocumentPdf.PdfMergedDocument", "Pdf Template", new string[]
		{
			".pdf"
		})]
		AllowedPdfExtensions
	}
}
