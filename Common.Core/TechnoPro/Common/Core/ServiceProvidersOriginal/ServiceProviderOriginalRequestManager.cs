using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000057 RID: 87
	public class ServiceProviderOriginalRequestManager : IServiceProviderOriginalRequestManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600038E RID: 910 RVA: 0x0001217E File Offset: 0x0001037E
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00012186 File Offset: 0x00010386
		public IServiceProviderOriginalRequestDAO dao { get; set; }

		// Token: 0x06000390 RID: 912 RVA: 0x0001218F File Offset: 0x0001038F
		public ServiceProviderOriginalRequestManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderOriginalRequestDAO(this.OpContext.GetProviderTypes());
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000121B8 File Offset: 0x000103B8
		// (set) Token: 0x06000392 RID: 914 RVA: 0x000121C0 File Offset: 0x000103C0
		public OperationContext OpContext { get; set; }

		// Token: 0x06000393 RID: 915 RVA: 0x000072EA File Offset: 0x000054EA
		public ServiceRequest LoadRequestById(int ServiceProviderRequestId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceRequest> LoadRequestsByDate(DateTime StartDate, DateTime EndDate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceRequest> LoadRequestsByDateAndType(DateTime StartDate, DateTime EndDate, params int[] ServiceProviderTypeId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateRequest(ServiceRequest Request)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000397 RID: 919 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteRequest(int ServiceProviderRequestId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateRequest(ServiceRequest Request)
		{
			throw new NotImplementedException();
		}
	}
}
