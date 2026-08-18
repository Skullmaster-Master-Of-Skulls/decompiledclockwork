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
	// Token: 0x020000A4 RID: 164
	public class AppointmentIconServiceManager : IAppointmentIcon, IService
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x0001B7E8 File Offset: 0x000199E8
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001B7FC File Offset: 0x000199FC
		public LoadAppointmentIconsByAppointmentResp LoadAppointmentIconsByAppointment(LoadAppointmentIconsByAppointmentReq Request)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(Request.GetOperationContext());
			IList<AppointmentIcon> list = appointmentIconManager.LoadAppointmentIconsByAppointment(Request.AppointmentId);
			LoadAppointmentIconsByAppointmentResp loadAppointmentIconsByAppointmentResp = new LoadAppointmentIconsByAppointmentResp();
			IList<AppointmentIconDTO> icons;
			if (list != null)
			{
				icons = list.ToList<AppointmentIcon>().ConvertAll<AppointmentIconDTO>((AppointmentIcon f) => f.ToDTO());
			}
			else
			{
				icons = null;
			}
			loadAppointmentIconsByAppointmentResp.Icons = icons;
			return loadAppointmentIconsByAppointmentResp;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001B864 File Offset: 0x00019A64
		public LoadAppointmentIconResp LoadAppointmentIcon(LoadAppointmentIconReq Request)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(Request.GetOperationContext());
			AppointmentIcon appointmentIcon = appointmentIconManager.LoadAppointmentIcon(Request.AppointmentId, Request.IconNum);
			return new LoadAppointmentIconResp
			{
				Icon = ((appointmentIcon == null) ? null : appointmentIcon.ToDTO())
			};
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		public void DeleteAppointmentIconsNotInList(DeleteAppointmentIconsNotInListReq Request)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(Request.GetOperationContext());
			appointmentIconManager.DeleteAppointmentIconsNotInList(false, Request.AppointmentId, Request.IconNums);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		public InsertOrUpdateAppointmentIconResp InsertOrUpdateAppointmentIcon(InsertOrUpdateAppointmentIconReq Request)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(Request.GetOperationContext());
			int id = appointmentIconManager.InsertOrUpdateAppointmentIcon(false, Request.AppointmentId, Request.AppIcon.ToDomainObject());
			return new InsertOrUpdateAppointmentIconResp
			{
				Id = id
			};
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001B924 File Offset: 0x00019B24
		public LoadAppointmentIconByIconInfoIdResp LoadAppointmentIconByIconInfoId(LoadAppointmentIconByIconInfoIdReq Request)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(Request.GetOperationContext());
			AppointmentIcon appointmentIcon = appointmentIconManager.LoadAppointmentIcon(Request.IconInfoId);
			return new LoadAppointmentIconByIconInfoIdResp
			{
				Icon = ((appointmentIcon == null) ? null : appointmentIcon.ToDTO())
			};
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001B968 File Offset: 0x00019B68
		public void DeleteAppointmentIcon(DeleteAppointmentIconReq Request)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(Request.GetOperationContext());
			appointmentIconManager.DeleteAppointmentIcon(false, Request.AppointmentId, Request.IconNum);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001B998 File Offset: 0x00019B98
		public LoadAllIconInfosResp LoadAllIconInfos(LoadAllIconInfosReq Request)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(Request.GetOperationContext());
			IList<IconInfo> list = appointmentIconManager.LoadAllIconInfos();
			LoadAllIconInfosResp loadAllIconInfosResp = new LoadAllIconInfosResp();
			IList<IconInfoDTO> iconInfos;
			if (list != null)
			{
				iconInfos = list.ToList<IconInfo>().ConvertAll<IconInfoDTO>((IconInfo g) => g.ToDTO());
			}
			else
			{
				iconInfos = null;
			}
			loadAllIconInfosResp.IconInfos = iconInfos;
			return loadAllIconInfosResp;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001B9FC File Offset: 0x00019BFC
		public LoadAppointmentIconByIconNumResp LoadAppointmentIconByIconNum(LoadAppointmentIconByIconNumReq Request)
		{
			IAppointmentIconManager appointmentIconManager = new AppointmentIconManager(Request.GetOperationContext());
			AppointmentIcon appointmentIcon = appointmentIconManager.LoadAppointmentIconByIconNum(Request.IconNum);
			return new LoadAppointmentIconByIconNumResp
			{
				Icon = ((appointmentIcon == null) ? null : appointmentIcon.ToDTO())
			};
		}
	}
}
