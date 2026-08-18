using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsWorkshops
{
	// Token: 0x02000082 RID: 130
	public interface IWorkshopDefinitionClientManager : IWebService
	{
		// Token: 0x060003D1 RID: 977
		int CreateWorkshopDefinition(WorkshopDefinitionDTO workshopDefinition);

		// Token: 0x060003D2 RID: 978
		void UpdateWorkshopDefinition(WorkshopDefinitionDTO workshopDefinition);

		// Token: 0x060003D3 RID: 979
		void DeleteWorkshopDefinition(int workshopEventId);

		// Token: 0x060003D4 RID: 980
		Forest<WorkshopDefinitionOrAppTypeDTO> LoadAppTypesWithWorkshopDefinitions();

		// Token: 0x060003D5 RID: 981
		IList<WorkshopDefinition> LoadWorkshopDefinitionsByAppType(int appTypeId);

		// Token: 0x060003D6 RID: 982
		IList<AppTypeDTO> LoadAllWorkshopAppTypes();

		// Token: 0x060003D7 RID: 983
		WorkshopDefinitionDTO LoadWorkshopDefinitionById(int workshopDefinitionId);
	}
}
