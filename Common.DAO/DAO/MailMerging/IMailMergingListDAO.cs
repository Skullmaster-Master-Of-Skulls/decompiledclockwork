using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.DAO.MailMerging
{
	// Token: 0x02000055 RID: 85
	public interface IMailMergingListDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001D4 RID: 468
		IList<MailMergeCodeForExcel> ExtractCodesFromExcelTemplate(BinaryFile ExcelFile);

		// Token: 0x060001D5 RID: 469
		BinaryFile MailMergeExcel(BinaryFile ExcelTemplate, IList<MailMergeContextWithCustomDictionary> ContextsWithDictionaries);
	}
}
