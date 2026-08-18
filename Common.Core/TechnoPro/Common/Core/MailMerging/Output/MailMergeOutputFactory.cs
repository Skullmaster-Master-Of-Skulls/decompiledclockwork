using System;
using TechnoPro.Common.ICore.MailMerging.Output;
using TechnoPro.Common.Public.Entities.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.Core.MailMerging.Output
{
	// Token: 0x020000CE RID: 206
	public static class MailMergeOutputFactory
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x00036420 File Offset: 0x00034620
		public static IMailMergeOutputManager GetMailMergeOutputManager(eMailMergeDocumentOutputFormat outputFormat, MailMergeOutputOperationContext opContext)
		{
			IMailMergeOutputManager result;
			switch (outputFormat)
			{
			case eMailMergeDocumentOutputFormat.Html:
				result = new HtmlMailMergeOutputManager(opContext);
				break;
			case eMailMergeDocumentOutputFormat.Word:
				result = new WordMailMergeOutputManager(opContext);
				break;
			case eMailMergeDocumentOutputFormat.Email:
				result = new EmailMailMergeOutputManager(opContext);
				break;
			default:
				result = new TextMailMergeOutputManager(opContext);
				break;
			}
			return result;
		}
	}
}
