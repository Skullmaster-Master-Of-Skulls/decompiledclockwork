using System;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.ICore.MailMerging.Output
{
	// Token: 0x0200006A RID: 106
	public interface IMailMergeOutputManager : IBaseOperationContext<MailMergeOutputOperationContext>
	{
		// Token: 0x060002E6 RID: 742
		object OutputMailMergeCodes();
	}
}
