using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000007 RID: 7
	public interface IServiceProviderOriginalProviderDAO : IBaseOperationContext<ServiceProvidersOperationContext>
	{
		// Token: 0x06000003 RID: 3
		ServiceProvider LoadProviderById(int ServiceProviderId);

		// Token: 0x06000004 RID: 4
		ServiceProvider LoadProviderByUsername(string Username);

		// Token: 0x06000005 RID: 5
		ServiceProvider LoadProviderByStudentNumber(string StudentNumber);

		// Token: 0x06000006 RID: 6
		ServiceProviderBase LoadProviderBaseByStudentNumber(string StudentNumber);

		// Token: 0x06000007 RID: 7
		ServiceProviderBase LoadProviderBaseById(int ServiceProviderId);

		// Token: 0x06000008 RID: 8
		ServiceProviderBase LoadProviderBaseByUsername(string Username);

		// Token: 0x06000009 RID: 9
		IList<ServiceProvider> LoadProvidersByProviderTypeAndDate(int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate);
	}
}
