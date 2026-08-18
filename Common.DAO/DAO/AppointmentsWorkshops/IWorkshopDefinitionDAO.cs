using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.Common.DAO.AppointmentsWorkshops
{
	// Token: 0x020000B3 RID: 179
	public interface IWorkshopDefinitionDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004D0 RID: 1232
		IList<WorkshopDefinition> LoadWorkshopDefinitions(IList<int> AllowedAppointmentTypes);

		// Token: 0x060004D1 RID: 1233
		WorkshopDefinition LoadWorkshopDefinitionById(int WorkshopId);

		// Token: 0x060004D2 RID: 1234
		int CreateWorkshopDefinition(WorkshopDefinition workshopDefinition);

		// Token: 0x060004D3 RID: 1235
		void UpdateWorkshopDefinition(WorkshopDefinition workshopDefinition);

		// Token: 0x060004D4 RID: 1236
		void DeleteWorkshopDefinition(int workshopEventId);
	}
}
