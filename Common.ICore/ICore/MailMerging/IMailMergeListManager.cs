using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.ICore.MailMerging
{
	// Token: 0x02000066 RID: 102
	public interface IMailMergeListManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002BD RID: 701
		IList<MailMergeCodeForExcel> ExtractCodesFromExcelTemplate(BinaryFile ExcelFile);

		// Token: 0x060002BE RID: 702
		BinaryFile MailMergeExcel(BinaryFile ExcelTemplate, DataTable Table);
	}
}
