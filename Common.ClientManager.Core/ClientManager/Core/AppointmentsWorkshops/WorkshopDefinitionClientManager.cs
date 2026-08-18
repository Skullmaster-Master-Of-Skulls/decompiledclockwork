using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsWorkshops;
using TechnoPro.Common.Core.Mappers.AppointmentsWorkshops;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsWorkshops
{
	// Token: 0x02000088 RID: 136
	public class WorkshopDefinitionClientManager : IWorkshopDefinitionClientManager, IWebService
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x00016000 File Offset: 0x00014200
		public int CreateWorkshopDefinition(WorkshopDefinitionDTO workshopDefinition)
		{
			CreateWorkshopDefinitionReq createWorkshopDefinitionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateWorkshopDefinitionReq>();
			createWorkshopDefinitionReq.WorkshopDef = workshopDefinition;
			return ClientServiceFactory.GetClientInstance<IWorkshopDefinition>().CreateWorkshopDefinition(createWorkshopDefinitionReq).WorkshopId;
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00016038 File Offset: 0x00014238
		public void UpdateWorkshopDefinition(WorkshopDefinitionDTO workshopDefinition)
		{
			UpdateWorkshopDefinitionReq updateWorkshopDefinitionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateWorkshopDefinitionReq>();
			updateWorkshopDefinitionReq.WorkshopDef = workshopDefinition;
			ClientServiceFactory.GetClientInstance<IWorkshopDefinition>().UpdateWorkshopDefinition(updateWorkshopDefinitionReq);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00016068 File Offset: 0x00014268
		public void DeleteWorkshopDefinition(int workshopEventId)
		{
			DeleteWorkshopDefinitionReq deleteWorkshopDefinitionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteWorkshopDefinitionReq>();
			deleteWorkshopDefinitionReq.WorkshopId = workshopEventId;
			ClientServiceFactory.GetClientInstance<IWorkshopDefinition>().DeleteWorkshopDefinition(deleteWorkshopDefinitionReq);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00016098 File Offset: 0x00014298
		public IList<AppTypeDTO> LoadAllWorkshopAppTypes()
		{
			LoadAllWorkshopAppTypesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllWorkshopAppTypesReq>();
			return ClientServiceFactory.GetClientInstance<IWorkshopDefinition>().LoadAllWorkshopAppTypes(request).WorkshopGroups;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000160C8 File Offset: 0x000142C8
		public WorkshopDefinitionDTO LoadWorkshopDefinitionById(int workshopDefinitionId)
		{
			LoadWorkshopDefinitionByIdReq loadWorkshopDefinitionByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadWorkshopDefinitionByIdReq>();
			loadWorkshopDefinitionByIdReq.WorkshopId = workshopDefinitionId;
			return ClientServiceFactory.GetClientInstance<IWorkshopDefinition>().LoadWorkshopDefinitionById(loadWorkshopDefinitionByIdReq).WorkshopDefinition;
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00016100 File Offset: 0x00014300
		public IList<WorkshopDefinition> LoadWorkshopDefinitionsByAppType(int appTypeId)
		{
			LoadWorkDefinitionsByAppTypeReq loadWorkDefinitionsByAppTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadWorkDefinitionsByAppTypeReq>();
			loadWorkDefinitionsByAppTypeReq.AppTypeId = appTypeId;
			return (from w in ClientServiceFactory.GetClientInstance<IWorkshopDefinition>().LoadWorkshopDefinitionsByAppType(loadWorkDefinitionsByAppTypeReq).WorkshopDefinitions
			select w.ToDomainObject()).ToList<WorkshopDefinition>();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00016160 File Offset: 0x00014360
		public Forest<WorkshopDefinitionOrAppTypeDTO> LoadAppTypesWithWorkshopDefinitions()
		{
			LoadAppTypesWithWorkshopDefinitionsReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppTypesWithWorkshopDefinitionsReq>();
			return ClientServiceFactory.GetClientInstance<IWorkshopDefinition>().LoadAppTypesWithWorkshopDefinitions(request).WorkshopAppTypesWithDefinitions;
		}
	}
}
