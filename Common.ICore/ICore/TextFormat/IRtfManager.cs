using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.ICore.TextFormat
{
	// Token: 0x02000029 RID: 41
	public interface IRtfManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000128 RID: 296
		string CreatePointOfContactFromEmail(TPMailMessage Email, Dictionary<string, int> AttachmentFileIds);

		// Token: 0x06000129 RID: 297
		string ConvertFromRtf(string Rtf);

		// Token: 0x0600012A RID: 298
		string ConvertToRtf(string PlainText);
	}
}
