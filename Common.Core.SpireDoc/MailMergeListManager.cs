using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.DAO.SpireDoc.Impl;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.Core.SpireDoc
{
	// Token: 0x02000003 RID: 3
	public class MailMergeListManager : IMailMergeListManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000279B File Offset: 0x0000099B
		public MailMergeListManager()
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000027A5 File Offset: 0x000009A5
		public MailMergeListManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000027B7 File Offset: 0x000009B7
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000027BF File Offset: 0x000009BF
		public OperationContext OpContext { get; set; }

		// Token: 0x06000010 RID: 16 RVA: 0x000027C8 File Offset: 0x000009C8
		public IList<MailMergeCodeForExcel> ExtractCodesFromExcelTemplate(BinaryFile ExcelFile)
		{
			MailMergingListDAO mailMergingListDAO = new MailMergingListDAO(this.OpContext);
			return mailMergingListDAO.ExtractCodesFromExcelTemplate(ExcelFile);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000027ED File Offset: 0x000009ED
		public BinaryFile MailMergeExcel(BinaryFile ExcelTemplate, DataTable Table)
		{
			throw new NotImplementedException();
		}
	}
}
