using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.DocumentForPrint;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.DAO.MailMerging
{
	// Token: 0x02000054 RID: 84
	public interface IMailMergingDocDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001D0 RID: 464
		IList<string> ExtractUniqueCodes(byte[] fileByteArray, eAllowedExtensionGroup fileType, bool isLicensed);

		// Token: 0x060001D1 RID: 465
		BinaryFile OutputFile(byte[] templateBytes, eAllowedExtensionGroup fileType, string fileName, bool isLicensed, List<List<MailMergeCode>> MultipleCodes, eFileFormat OutputFileFormat);

		// Token: 0x060001D2 RID: 466
		BinaryFile OutputFileMailingLabels(BinaryFile Template, List<List<MailMergeCode>> MultipleCodes, eFileFormat OutputFileFormat);

		// Token: 0x060001D3 RID: 467
		BinaryFile GenerateDocumentFromPrintCodes(IList<DocumentPrintItem> PrintItems, string FileName, eFileFormat OutputFormat);
	}
}
