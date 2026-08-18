using System;
using TechnoPro.Common.DAO.Impl.ServiceProvider;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.ICore.ServiceProviders;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.ServiceProvider
{
	// Token: 0x0200004C RID: 76
	public class ServiceProviderMatchingManager : IServiceProviderMatchingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000325 RID: 805 RVA: 0x00011B91 File Offset: 0x0000FD91
		public ServiceProviderMatchingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderMatchingDAO(opContext);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00011BAF File Offset: 0x0000FDAF
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00011BB7 File Offset: 0x0000FDB7
		public OperationContext OpContext { get; set; }

		// Token: 0x04000097 RID: 151
		public IServiceProviderMatchingDAO dao;
	}
}
