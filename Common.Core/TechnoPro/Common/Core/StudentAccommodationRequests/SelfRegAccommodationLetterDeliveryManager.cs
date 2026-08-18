using System;
using TechnoPro.Common.ICore.StudentAccommodationRequests;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.StudentAccommodationRequests
{
	// Token: 0x0200003C RID: 60
	public class SelfRegAccommodationLetterDeliveryManager : ISelfRegAccommodationLetterDeliveryManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000CD32 File Offset: 0x0000AF32
		public SelfRegAccommodationLetterDeliveryManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000CD44 File Offset: 0x0000AF44
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public OperationContext OpContext { get; set; }
	}
}
