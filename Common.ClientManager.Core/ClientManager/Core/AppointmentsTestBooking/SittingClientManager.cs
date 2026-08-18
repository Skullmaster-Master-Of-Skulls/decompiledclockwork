using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200008E RID: 142
	public class SittingClientManager : ISittingClientManager, IWebService
	{
		// Token: 0x0600051D RID: 1309 RVA: 0x00016DA4 File Offset: 0x00014FA4
		public int CreateSitting(SittingDTO Sitting)
		{
			CreateSittingReq createSittingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateSittingReq>();
			createSittingReq.Sitting = Sitting;
			return ClientServiceFactory.GetClientInstance<ISitting>().CreateSitting(createSittingReq).SittingId;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00016DDC File Offset: 0x00014FDC
		public void UpdateSitting(SittingDTO Sitting)
		{
			UpdateSittingReq updateSittingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateSittingReq>();
			updateSittingReq.Sitting = Sitting;
			ClientServiceFactory.GetClientInstance<ISitting>().UpdateSitting(updateSittingReq);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00016E0C File Offset: 0x0001500C
		public SittingDTO LoadSittingById(int SittingId)
		{
			LoadSittingByIdReq loadSittingByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadSittingByIdReq>();
			loadSittingByIdReq.SittingId = SittingId;
			return ClientServiceFactory.GetClientInstance<ISitting>().LoadSittingById(loadSittingByIdReq).Sitting;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00016E44 File Offset: 0x00015044
		public IList<SittingDTO> LoadSittingsByDateRange(DateTime StartDate, DateTime EndDate)
		{
			LoadSittingsByDateRangeReq loadSittingsByDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadSittingsByDateRangeReq>();
			loadSittingsByDateRangeReq.StartDate = StartDate;
			loadSittingsByDateRangeReq.EndDate = EndDate;
			return ClientServiceFactory.GetClientInstance<ISitting>().LoadSittingsByDateRange(loadSittingsByDateRangeReq).Sittings;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00016E84 File Offset: 0x00015084
		public void ClearSittingsOnAppointments(params int[] AppointmentIds)
		{
			ClearSittingOnAppointmentReq clearSittingOnAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearSittingOnAppointmentReq>();
			clearSittingOnAppointmentReq.AppointmentIds = AppointmentIds;
			ClientServiceFactory.GetClientInstance<ISitting>().ClearSittingOnAppointment(clearSittingOnAppointmentReq);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00016EB1 File Offset: 0x000150B1
		public void SetSittingOnAppointment(int AppointmentId, int SittingId)
		{
			this.SetSittingsOnAppointments(new Dictionary<int, int>
			{
				{
					AppointmentId,
					SittingId
				}
			});
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00016ECC File Offset: 0x000150CC
		public void SetSittingsOnAppointments(IDictionary<int, int> AppointmentIdWithSittingIds)
		{
			SetSittingOnAppointmentReq setSittingOnAppointmentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SetSittingOnAppointmentReq>();
			setSittingOnAppointmentReq.AppointmentIdWithSittingIds = AppointmentIdWithSittingIds;
			ClientServiceFactory.GetClientInstance<ISitting>().SetSittingOnAppointment(setSittingOnAppointmentReq);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000387F File Offset: 0x00001A7F
		public IList<int> LoadBookingAppointmentIdsBySitting(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0000387F File Offset: 0x00001A7F
		public void DeleteSitting(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0000387F File Offset: 0x00001A7F
		public IList<BasicTestDTO> LoadBasicBookingsBySitting(int SittingId)
		{
			throw new NotImplementedException();
		}
	}
}
