using System;
using System.Collections.Generic;
using System.Drawing;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.Public.Interfaces
{
	// Token: 0x020000C3 RID: 195
	public interface IMergedDocument
	{
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004E1 RID: 1249
		// (set) Token: 0x060004E2 RID: 1250
		bool IsLicensed { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004E3 RID: 1251
		object Document { get; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004E4 RID: 1252
		eFileFormat OutputFileFormat { get; }

		// Token: 0x060004E5 RID: 1253
		void LoadDocument(byte[] bytes, eFileFormat outputFileFormat);

		// Token: 0x060004E6 RID: 1254
		BinaryFile SaveDocument(string fileNameNoExtension);

		// Token: 0x060004E7 RID: 1255
		IList<string> ExtractUniqueCodes(byte[] fileBytes);

		// Token: 0x060004E8 RID: 1256
		void MergeImageField(MailMergeCode code, string codeName, Image image, byte[] imageBytes);

		// Token: 0x060004E9 RID: 1257
		void MergeStringField(MailMergeCode code, string codeName, string codeValue);

		// Token: 0x060004EA RID: 1258
		void MergeBooleanField(MailMergeCode code, string codeName, MailMergeCheckedItem item);

		// Token: 0x060004EB RID: 1259
		void AppendDocument(IMergedDocument documentToAppend);

		// Token: 0x060004EC RID: 1260
		void MergeDocument(MailMergeCode code, string codeName, IMergedDocument documentToMergeIn);
	}
}
