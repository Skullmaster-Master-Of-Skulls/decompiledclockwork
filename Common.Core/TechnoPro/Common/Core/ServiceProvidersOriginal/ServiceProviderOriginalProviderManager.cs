using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.ServiceProvidersOriginal;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.ServiceProvidersOriginal
{
	// Token: 0x02000055 RID: 85
	public class ServiceProviderOriginalProviderManager : IServiceProviderOriginalProviderManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00012007 File Offset: 0x00010207
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0001200F File Offset: 0x0001020F
		public IServiceProviderOriginalProviderDAO dao { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00012018 File Offset: 0x00010218
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00012020 File Offset: 0x00010220
		public OperationContext OpContext { get; set; }

		// Token: 0x06000379 RID: 889 RVA: 0x00012029 File Offset: 0x00010229
		public ServiceProviderOriginalProviderManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ServiceProviderOriginalProviderDAO(this.OpContext.GetProviderTypes());
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00012054 File Offset: 0x00010254
		public ServiceProviderBase LoadProviderBaseById(int ServiceProviderId)
		{
			return this.dao.LoadProviderBaseById(ServiceProviderId);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00012074 File Offset: 0x00010274
		public ServiceProviderBase LoadProviderBaseByStudentNumber(string StudentNumber)
		{
			return this.dao.LoadProviderBaseByStudentNumber(StudentNumber);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00012094 File Offset: 0x00010294
		public ServiceProviderBase LoadProviderBaseByUsername(string Username)
		{
			return this.dao.LoadProviderBaseByUsername(Username);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<ServiceProvider> LoadProvidersByProviderTypeAndDate(int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000120B4 File Offset: 0x000102B4
		public ServiceProvider LoadProviderById(int ServiceProviderId)
		{
			return this.dao.LoadProviderById(ServiceProviderId);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000120D4 File Offset: 0x000102D4
		public ServiceProvider LoadProviderByStudentNumber(string StudentNumber)
		{
			return this.dao.LoadProviderByStudentNumber(StudentNumber);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000120F4 File Offset: 0x000102F4
		public ServiceProvider LoadProviderByUsername(string Username)
		{
			return this.dao.LoadProviderByUsername(Username);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000072EA File Offset: 0x000054EA
		public int CreateProvider(ServiceProvider Provider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000072EA File Offset: 0x000054EA
		public void DeleteProvider(int ServiceProviderId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000072EA File Offset: 0x000054EA
		public void UpdateProvider(ServiceProvider Provider)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<Appointment> LoadAppointmentsByProviderAndType(int ServiceProviderId, int ServiceProviderType)
		{
			throw new NotImplementedException();
		}
	}
}
