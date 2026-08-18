using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000020 RID: 32
	public class AppointmentShowTimeAsTypeServiceManager : IAppointmentShowTimeAsType, IService
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00007810 File Offset: 0x00005A10
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007824 File Offset: 0x00005A24
		public LoadAllShowTimeAsTypesResp LoadAllShowTimeAsTypes(LoadAllShowTimeAsTypesReq Request)
		{
			IAppointmentShowTimeAsManager appointmentShowTimeAsManager = new AppointmentShowTimeAsManager(Request.GetOperationContext());
			IList<AppShowTimeAsType> list = appointmentShowTimeAsManager.LoadAllShowTimeAsTypes();
			LoadAllShowTimeAsTypesResp loadAllShowTimeAsTypesResp = new LoadAllShowTimeAsTypesResp();
			IList<AppShowTimeAsTypeDTO> showTimeAsTypes;
			if (list != null)
			{
				showTimeAsTypes = list.ToList<AppShowTimeAsType>().ConvertAll<AppShowTimeAsTypeDTO>((AppShowTimeAsType f) => f.ToDTO());
			}
			else
			{
				showTimeAsTypes = null;
			}
			loadAllShowTimeAsTypesResp.ShowTimeAsTypes = showTimeAsTypes;
			return loadAllShowTimeAsTypesResp;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007888 File Offset: 0x00005A88
		public LoadShowTimeAsTypeByAppCodeResp LoadShowTimeAsTypeByAppCode(LoadShowTimeAsTypeByAppCodeReq Request)
		{
			IAppointmentShowTimeAsManager appointmentShowTimeAsManager = new AppointmentShowTimeAsManager(Request.GetOperationContext());
			AppShowTimeAsType appShowTimeAsType = appointmentShowTimeAsManager.LoadShowTimeAsTypeByAppCode(Request.AppCode);
			return new LoadShowTimeAsTypeByAppCodeResp
			{
				ShowTimeAsType = ((appShowTimeAsType == null) ? null : appShowTimeAsType.ToDTO())
			};
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000078CC File Offset: 0x00005ACC
		public LoadShowTimeAsTypeByIdResp LoadShowTimeAsTypeById(LoadShowTimeAsTypeByIdReq Request)
		{
			IAppointmentShowTimeAsManager appointmentShowTimeAsManager = new AppointmentShowTimeAsManager(Request.GetOperationContext());
			AppShowTimeAsType appShowTimeAsType = appointmentShowTimeAsManager.LoadShowTimeAsTypeById(Request.AppointmentShowTimeAsId);
			return new LoadShowTimeAsTypeByIdResp
			{
				ShowTimeAsType = ((appShowTimeAsType == null) ? null : appShowTimeAsType.ToDTO())
			};
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00007910 File Offset: 0x00005B10
		public void DeleteShowTimeAsTypeByAppCode(DeleteShowTimeAsTypeByAppCodeReq Request)
		{
			IAppointmentShowTimeAsManager appointmentShowTimeAsManager = new AppointmentShowTimeAsManager(Request.GetOperationContext());
			appointmentShowTimeAsManager.DeleteShowTimeAsTypeByAppCode(Request.AppCode);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00007938 File Offset: 0x00005B38
		public void DeleteShowTimeAsTypeById(DeleteShowTimeAsTypeByIdReq Request)
		{
			IAppointmentShowTimeAsManager appointmentShowTimeAsManager = new AppointmentShowTimeAsManager(Request.GetOperationContext());
			appointmentShowTimeAsManager.DeleteShowTimeAsTypeById(Request.AppointmentShowTimeAsId);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00007960 File Offset: 0x00005B60
		public void UpdateShowTimeAsType(UpdateShowTimeAsTypeReq Request)
		{
			IAppointmentShowTimeAsManager appointmentShowTimeAsManager = new AppointmentShowTimeAsManager(Request.GetOperationContext());
			appointmentShowTimeAsManager.UpdateShowTimeAsType(Request.AppShowTimeAsType.ToDomainObject());
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000798C File Offset: 0x00005B8C
		public CreateShowTimeAsTypeResp CreateShowTimeAsType(CreateShowTimeAsTypeReq Request)
		{
			IAppointmentShowTimeAsManager appointmentShowTimeAsManager = new AppointmentShowTimeAsManager(Request.GetOperationContext());
			int appCode = appointmentShowTimeAsManager.CreateShowTimeAsType(Request.ShowTimeAsType.ToDomainObject());
			return new CreateShowTimeAsTypeResp
			{
				AppCode = appCode
			};
		}
	}
}
