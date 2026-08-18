using System;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000056 RID: 86
	public class ServiceProviderOriginalRequestDetailManager : IServiceProviderOriginalRequestDetailManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00012112 File Offset: 0x00010312
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0001211A File Offset: 0x0001031A
		public IServiceProviderOriginalRequestDetailDAO dao { get; set; }

		// Token: 0x06000387 RID: 903 RVA: 0x00012123 File Offset: 0x00010323
		public ServiceProviderOriginalRequestDetailManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderOriginalRequestDetailDAO(this.OpContext.GetProviderTypes());
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0001214C File Offset: 0x0001034C
		// (set) Token: 0x06000389 RID: 905 RVA: 0x00012154 File Offset: 0x00010354
		public OperationContext OpContext { get; set; }

		// Token: 0x0600038A RID: 906 RVA: 0x00012160 File Offset: 0x00010360
		public ServiceProviderRequestDetail LoadServiceRequestDetailByRequestId(int serviceProviderRequestId)
		{
			return this.dao.LoadServiceRequestDetailByRequestId(serviceProviderRequestId);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateRequestDetail(ServiceProviderRequestDetail Detail)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteRequestDetail(int ServiceProviderRequestDetailId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateRequestDetail(ServiceProviderRequestDetail Detail)
		{
			throw new NotImplementedException();
		}
	}
}
