using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000017 RID: 23
	public class SittingServiceManager : ISitting, IService
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000656C File Offset: 0x0000476C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006580 File Offset: 0x00004780
		public void UpdateSitting(UpdateSittingReq request)
		{
			ISittingManager sittingManager = new SittingManager(request.GetOperationContext());
			sittingManager.UpdateSitting(request.Sitting.ToDomainObject());
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000065AC File Offset: 0x000047AC
		public CreateSittingResp CreateSitting(CreateSittingReq Request)
		{
			ISittingManager sittingManager = new SittingManager(Request.GetOperationContext());
			int sittingId = sittingManager.CreateSitting(Request.Sitting.ToDomainObject());
			return new CreateSittingResp
			{
				SittingId = sittingId
			};
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000065EC File Offset: 0x000047EC
		public LoadSittingTestsResp LoadSittingTests(LoadSittingTestsReq request)
		{
			ISittingManager sittingManager = new SittingManager(request.GetOperationContext());
			throw new NotImplementedException();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000660C File Offset: 0x0000480C
		public LoadSittingsResp LoadSittings(LoadSittingsReq request)
		{
			ISittingManager sittingManager = new SittingManager(request.GetOperationContext());
			IList<Sitting> list = sittingManager.LoadSittingsByDate(request.Day);
			LoadSittingsResp loadSittingsResp = new LoadSittingsResp();
			List<SittingDTO> sittings;
			if (list != null)
			{
				sittings = list.ToList<Sitting>().ConvertAll<SittingDTO>((Sitting g) => g.ToDTO());
			}
			else
			{
				sittings = null;
			}
			loadSittingsResp.Sittings = sittings;
			return loadSittingsResp;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006674 File Offset: 0x00004874
		public LoadSittingByIdResp LoadSittingById(LoadSittingByIdReq request)
		{
			ISittingManager sittingManager = new SittingManager(request.GetOperationContext());
			Sitting sitting = sittingManager.LoadSittingById(request.SittingId);
			return new LoadSittingByIdResp
			{
				Sitting = sitting.ToDTO()
			};
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000066B4 File Offset: 0x000048B4
		public GetSittingEffectiveTimeRangeResp GetSittingEffectiveTimeRange(GetSittingEffectiveTimeRangeReq request)
		{
			ISittingManager sittingManager = new SittingManager(request.GetOperationContext());
			throw new NotImplementedException();
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000066D4 File Offset: 0x000048D4
		public LoadSittingsByDateRangeResp LoadSittingsByDateRange(LoadSittingsByDateRangeReq Request)
		{
			ISittingManager sittingManager = new SittingManager(Request.GetOperationContext());
			IList<Sitting> list = sittingManager.LoadSittingsByDateRange(Request.StartDate, Request.EndDate);
			LoadSittingsByDateRangeResp loadSittingsByDateRangeResp = new LoadSittingsByDateRangeResp();
			IList<SittingDTO> sittings;
			if (list != null)
			{
				sittings = list.ToList<Sitting>().ConvertAll<SittingDTO>((Sitting g) => g.ToDTO());
			}
			else
			{
				sittings = null;
			}
			loadSittingsByDateRangeResp.Sittings = sittings;
			return loadSittingsByDateRangeResp;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006744 File Offset: 0x00004944
		public void ClearSittingOnAppointment(ClearSittingOnAppointmentReq Request)
		{
			ISittingManager sittingManager = new SittingManager(Request.GetOperationContext());
			sittingManager.ClearSittingOnAppointment(Request.AppointmentIds);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000676C File Offset: 0x0000496C
		public void SetSittingOnAppointment(SetSittingOnAppointmentReq Request)
		{
			ISittingManager sittingManager = new SittingManager(Request.GetOperationContext());
			sittingManager.SetSittingOnAppointment(Request.AppointmentIdWithSittingIds);
		}
	}
}
