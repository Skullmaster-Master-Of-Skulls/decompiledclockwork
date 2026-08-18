using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.ICore.ServiceProvidersOriginal
{
	// Token: 0x02000048 RID: 72
	public interface IServiceProviderOriginalProviderManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001CE RID: 462
		ServiceProvider LoadProviderById(int ServiceProviderId);

		// Token: 0x060001CF RID: 463
		ServiceProvider LoadProviderByStudentNumber(string StudentNumber);

		// Token: 0x060001D0 RID: 464
		ServiceProvider LoadProviderByUsername(string Username);

		// Token: 0x060001D1 RID: 465
		ServiceProviderBase LoadProviderBaseById(int ServiceProviderId);

		// Token: 0x060001D2 RID: 466
		ServiceProviderBase LoadProviderBaseByStudentNumber(string StudentNumber);

		// Token: 0x060001D3 RID: 467
		ServiceProviderBase LoadProviderBaseByUsername(string Username);

		// Token: 0x060001D4 RID: 468
		IList<ServiceProvider> LoadProvidersByProviderTypeAndDate(int ServiceProviderTypeId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001D5 RID: 469
		int CreateProvider(ServiceProvider Provider);

		// Token: 0x060001D6 RID: 470
		void DeleteProvider(int ServiceProviderId);

		// Token: 0x060001D7 RID: 471
		void UpdateProvider(ServiceProvider Provider);

		// Token: 0x060001D8 RID: 472
		IList<Appointment> LoadAppointmentsByProviderAndType(int ServiceProviderId, int ServiceProviderType);
	}
}
