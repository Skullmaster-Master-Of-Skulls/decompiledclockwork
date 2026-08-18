using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Campus;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Campus
{
	// Token: 0x0200007B RID: 123
	public class CampusClientManager : ICampusClientManager, IWebService
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x00014870 File Offset: 0x00012A70
		public IList<SchoolCampusDTO> GetCampusList()
		{
			GetCampusListReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetCampusListReq>();
			return ClientServiceFactory.GetClientInstance<ICampus>().GetCampusList(request).CampusList;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x000148A0 File Offset: 0x00012AA0
		public int CreateCampus(SchoolCampusDTO campus)
		{
			CreateCampusReq createCampusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateCampusReq>();
			createCampusReq.Campus = campus;
			return ClientServiceFactory.GetClientInstance<ICampus>().CreateCampus(createCampusReq).CampusId;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000148D8 File Offset: 0x00012AD8
		public void UpdateCampus(SchoolCampusDTO campus)
		{
			UpdateCampusReq updateCampusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCampusReq>();
			updateCampusReq.Campus = campus;
			ClientServiceFactory.GetClientInstance<ICampus>().UpdateCampus(updateCampusReq);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00014908 File Offset: 0x00012B08
		public void DeleteCampus(int campusId)
		{
			DeleteCampusReq deleteCampusReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteCampusReq>();
			deleteCampusReq.CampusId = campusId;
			ClientServiceFactory.GetClientInstance<ICampus>().DeleteCampus(deleteCampusReq);
		}
	}
}
