using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.ICore.AppointmentsWorkshops
{
	// Token: 0x020000C3 RID: 195
	public interface IWorkshopDefinitionManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060005DA RID: 1498
		int CreateWorkshopDefinition(WorkshopDefinition workshopDefinition);

		// Token: 0x060005DB RID: 1499
		void UpdateWorkshopDefinition(WorkshopDefinition workshopDefinition);

		// Token: 0x060005DC RID: 1500
		void DeleteWorkshopDefinition(int workshopEventId);

		// Token: 0x060005DD RID: 1501
		WorkshopDefinition LoadWorkshopDefinition(int workshopDefinitionId);

		// Token: 0x060005DE RID: 1502
		IList<WorkshopDefinition> LoadWorkshopDefinitions();

		// Token: 0x060005DF RID: 1503
		IList<WorkshopDefinition> LoadWorkshopDefinitionsByAppType(int appTypeId);

		// Token: 0x060005E0 RID: 1504
		List<AppType> LoadAllWorkshopAppTypes();

		// Token: 0x060005E1 RID: 1505
		Forest<WorkshopDefinitionOrAppType> LoadAppTypesWithWorkshopDefinitions();
	}
}
