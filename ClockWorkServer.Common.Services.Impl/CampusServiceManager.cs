using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Core;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.ICore;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000028 RID: 40
	public class CampusServiceManager : ICampus, IService
	{
		// Token: 0x060001BD RID: 445 RVA: 0x00008EA8 File Offset: 0x000070A8
		public GetCampusListResp GetCampusList(GetCampusListReq request)
		{
			ICampusManager campusManager = new CampusManager(request.GetOperationContext());
			return new GetCampusListResp
			{
				CampusList = campusManager.GetCampusList().ToDTO()
			};
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008EE0 File Offset: 0x000070E0
		public CreateCampusResp CreateCampus(CreateCampusReq request)
		{
			ICampusManager campusManager = new CampusManager(request.GetOperationContext());
			return new CreateCampusResp
			{
				CampusId = campusManager.CreateCampus(request.Campus.ToDomainObject())
			};
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008F1C File Offset: 0x0000711C
		public UpdateCampusResp UpdateCampus(UpdateCampusReq request)
		{
			ICampusManager campusManager = new CampusManager(request.GetOperationContext());
			campusManager.UpdateCampus(request.Campus.ToDomainObject());
			return new UpdateCampusResp();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008F54 File Offset: 0x00007154
		public DeleteCampusResp DeleteCampus(DeleteCampusReq request)
		{
			ICampusManager campusManager = new CampusManager(request.GetOperationContext());
			campusManager.DeleteCampus(request.CampusId);
			return new DeleteCampusResp();
		}
	}
}
