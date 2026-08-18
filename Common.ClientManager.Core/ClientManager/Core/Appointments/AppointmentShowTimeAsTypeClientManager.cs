using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Appointments
{
	// Token: 0x02000085 RID: 133
	public class AppointmentShowTimeAsTypeClientManager : IAppointmentShowTimeAsTypeClientManager, IWebService
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x000157AC File Offset: 0x000139AC
		public IList<AppShowTimeAsTypeDTO> LoadAllShowTimeAsTypes()
		{
			LoadAllShowTimeAsTypesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllShowTimeAsTypesReq>();
			return ClientServiceFactory.GetClientInstance<IAppointmentShowTimeAsType>().LoadAllShowTimeAsTypes(request).ShowTimeAsTypes;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000157DC File Offset: 0x000139DC
		public AppShowTimeAsTypeDTO LoadShowTimeAsTypeByAppCode(int AppCode)
		{
			LoadShowTimeAsTypeByAppCodeReq loadShowTimeAsTypeByAppCodeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadShowTimeAsTypeByAppCodeReq>();
			loadShowTimeAsTypeByAppCodeReq.AppCode = AppCode;
			return ClientServiceFactory.GetClientInstance<IAppointmentShowTimeAsType>().LoadShowTimeAsTypeByAppCode(loadShowTimeAsTypeByAppCodeReq).ShowTimeAsType;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00015814 File Offset: 0x00013A14
		public AppShowTimeAsTypeDTO LoadShowTimeAsTypeById(int AppointmentShowTimeAsId)
		{
			LoadShowTimeAsTypeByIdReq loadShowTimeAsTypeByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadShowTimeAsTypeByIdReq>();
			loadShowTimeAsTypeByIdReq.AppointmentShowTimeAsId = AppointmentShowTimeAsId;
			return ClientServiceFactory.GetClientInstance<IAppointmentShowTimeAsType>().LoadShowTimeAsTypeById(loadShowTimeAsTypeByIdReq).ShowTimeAsType;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001584C File Offset: 0x00013A4C
		public void DeleteShowTimeAsTypeByAppCode(int AppCode)
		{
			DeleteShowTimeAsTypeByAppCodeReq deleteShowTimeAsTypeByAppCodeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteShowTimeAsTypeByAppCodeReq>();
			deleteShowTimeAsTypeByAppCodeReq.AppCode = AppCode;
			ClientServiceFactory.GetClientInstance<IAppointmentShowTimeAsType>().DeleteShowTimeAsTypeByAppCode(deleteShowTimeAsTypeByAppCodeReq);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001587C File Offset: 0x00013A7C
		public void DeleteShowTimeAsTypeById(int AppointmentShowTimeAsId)
		{
			DeleteShowTimeAsTypeByIdReq deleteShowTimeAsTypeByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteShowTimeAsTypeByIdReq>();
			deleteShowTimeAsTypeByIdReq.AppointmentShowTimeAsId = AppointmentShowTimeAsId;
			ClientServiceFactory.GetClientInstance<IAppointmentShowTimeAsType>().DeleteShowTimeAsTypeById(deleteShowTimeAsTypeByIdReq);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000158AC File Offset: 0x00013AAC
		public void UpdateShowTimeAsType(AppShowTimeAsTypeDTO ShowTimeAsType)
		{
			UpdateShowTimeAsTypeReq updateShowTimeAsTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateShowTimeAsTypeReq>();
			updateShowTimeAsTypeReq.AppShowTimeAsType = ShowTimeAsType;
			ClientServiceFactory.GetClientInstance<IAppointmentShowTimeAsType>().UpdateShowTimeAsType(updateShowTimeAsTypeReq);
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000158DC File Offset: 0x00013ADC
		public int CreateShowTimeAsType(AppShowTimeAsTypeDTO ShowTimeAsType)
		{
			CreateShowTimeAsTypeReq createShowTimeAsTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateShowTimeAsTypeReq>();
			createShowTimeAsTypeReq.ShowTimeAsType = ShowTimeAsType;
			return ClientServiceFactory.GetClientInstance<IAppointmentShowTimeAsType>().CreateShowTimeAsType(createShowTimeAsTypeReq).AppCode;
		}
	}
}
