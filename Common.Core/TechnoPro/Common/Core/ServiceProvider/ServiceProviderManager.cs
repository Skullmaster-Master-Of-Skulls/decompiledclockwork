using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.ServiceProvider;
using TechnoPro.Common.DAO.ServiceProvider;
using TechnoPro.Common.ICore.ServiceProviders;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.ServiceProvider
{
	// Token: 0x0200004B RID: 75
	public class ServiceProviderManager : IServiceProviderManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000317 RID: 791 RVA: 0x00011B62 File Offset: 0x0000FD62
		public ServiceProviderManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderDAO(opContext);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00011B80 File Offset: 0x0000FD80
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00011B88 File Offset: 0x0000FD88
		public OperationContext OpContext { get; set; }

		// Token: 0x0600031A RID: 794 RVA: 0x000072EA File Offset: 0x000054EA
		public SPProvider LoadProviderById(int SPProviderId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000072EA File Offset: 0x000054EA
		public SPProvider LoadProviderByStudent_no(string Student_no)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000072EA File Offset: 0x000054EA
		public SPProvider LoadProviderByUserName(string UserName)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000072EA File Offset: 0x000054EA
		public SPProvider LoadProviderByExternalId(string ExternalId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateProvider(SPProvider Provider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateProvider(SPProvider Provider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000072EA File Offset: 0x000054EA
		public bool DeleteProvider(int SPProviderId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000072EA File Offset: 0x000054EA
		public int AddProviderCourseRegistration(SPProviderCourseRegistration CourseRegistration)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateProviderCourseRegistration(SPProviderCourseRegistration CourseRegistration)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteProviderCourseRegistration(int SPProviderCourseRegistrationId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<SPProvider> LoadAllProvidersWithAtLeastOneActiveApplication(DateTime StartDate, DateTime EndDate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000095 RID: 149
		public IServiceProviderDAO dao;
	}
}
