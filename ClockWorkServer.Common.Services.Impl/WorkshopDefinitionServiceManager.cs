using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Core.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsWorkshops;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.AppointmentsWorkshops;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x020000A3 RID: 163
	public class WorkshopDefinitionServiceManager : IWorkshopDefinition, IService
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001B5BC File Offset: 0x000197BC
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001B5D0 File Offset: 0x000197D0
		public LoadWorkshopDefinitionsResp LoadWorkshopDefinitions(LoadWorkshopDefinitionsReq Request)
		{
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(Request.GetOperationContext());
			IList<WorkshopDefinition> source = workshopDefinitionManager.LoadWorkshopDefinitions();
			LoadWorkshopDefinitionsResp loadWorkshopDefinitionsResp = new LoadWorkshopDefinitionsResp();
			loadWorkshopDefinitionsResp.WorkshopDefinitions = source.ToList<WorkshopDefinition>().ConvertAll<WorkshopDefinitionDTO>((WorkshopDefinition w) => w.ToDTO());
			return loadWorkshopDefinitionsResp;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001B62C File Offset: 0x0001982C
		public DeleteWorkshopDefinitionResp DeleteWorkshopDefinition(DeleteWorkshopDefinitionReq Request)
		{
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(Request.GetOperationContext());
			workshopDefinitionManager.DeleteWorkshopDefinition(Request.WorkshopId);
			return new DeleteWorkshopDefinitionResp();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001B65C File Offset: 0x0001985C
		public CreateWorkshopDefinitionResp CreateWorkshopDefinition(CreateWorkshopDefinitionReq Request)
		{
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(Request.GetOperationContext());
			int workshopId = workshopDefinitionManager.CreateWorkshopDefinition(Request.WorkshopDef.ToDomainObject());
			return new CreateWorkshopDefinitionResp
			{
				WorkshopId = workshopId
			};
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001B69C File Offset: 0x0001989C
		public UpdateWorkshopDefinitionResp UpdateWorkshopDefinition(UpdateWorkshopDefinitionReq Request)
		{
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(Request.GetOperationContext());
			workshopDefinitionManager.UpdateWorkshopDefinition(Request.WorkshopDef.ToDomainObject());
			return new UpdateWorkshopDefinitionResp();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001B6D4 File Offset: 0x000198D4
		public LoadAllWorkshopAppTypesResp LoadAllWorkshopAppTypes(LoadAllWorkshopAppTypesReq Request)
		{
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(Request.GetOperationContext());
			List<AppType> list = workshopDefinitionManager.LoadAllWorkshopAppTypes();
			return new LoadAllWorkshopAppTypesResp
			{
				WorkshopGroups = list.ToDTO()
			};
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001B70C File Offset: 0x0001990C
		public LoadWorkshopDefinitionByIdResp LoadWorkshopDefinitionById(LoadWorkshopDefinitionByIdReq Request)
		{
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(Request.GetOperationContext());
			WorkshopDefinition workshopDefinition = workshopDefinitionManager.LoadWorkshopDefinition(Request.WorkshopId);
			return new LoadWorkshopDefinitionByIdResp
			{
				WorkshopDefinition = workshopDefinition.ToDTO()
			};
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001B74C File Offset: 0x0001994C
		public LoadWorkDefinitionsByAppTypeResp LoadWorkshopDefinitionsByAppType(LoadWorkDefinitionsByAppTypeReq request)
		{
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(request.GetOperationContext());
			IList<WorkshopDefinition> source = workshopDefinitionManager.LoadWorkshopDefinitionsByAppType(request.AppTypeId);
			LoadWorkDefinitionsByAppTypeResp loadWorkDefinitionsByAppTypeResp = new LoadWorkDefinitionsByAppTypeResp();
			loadWorkDefinitionsByAppTypeResp.WorkshopDefinitions = source.ToList<WorkshopDefinition>().ConvertAll<WorkshopDefinitionDTO>((WorkshopDefinition wd) => wd.ToDTO());
			return loadWorkDefinitionsByAppTypeResp;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001B7B0 File Offset: 0x000199B0
		public LoadAppTypesWithWorkshopDefinitionsResp LoadAppTypesWithWorkshopDefinitions(LoadAppTypesWithWorkshopDefinitionsReq Request)
		{
			IWorkshopDefinitionManager workshopDefinitionManager = new WorkshopDefinitionManager(Request.GetOperationContext());
			Forest<WorkshopDefinitionOrAppType> item = workshopDefinitionManager.LoadAppTypesWithWorkshopDefinitions();
			return new LoadAppTypesWithWorkshopDefinitionsResp
			{
				WorkshopAppTypesWithDefinitions = item.ToDTO()
			};
		}
	}
}
