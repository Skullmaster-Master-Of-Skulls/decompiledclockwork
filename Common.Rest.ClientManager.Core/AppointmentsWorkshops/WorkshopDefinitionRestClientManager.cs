using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.ClientManager.ICore.AppointmentsWorkshops;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsWorkshops
{
	// Token: 0x02000073 RID: 115
	public class WorkshopDefinitionRestClientManager : BearerTokenRestProxy<IWorkshopDefinitionClientManager>, IWorkshopDefinitionClientManager, IWebService
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x0000CB3A File Offset: 0x0000AD3A
		public WorkshopDefinitionRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000CB44 File Offset: 0x0000AD44
		public WorkshopDefinitionRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000CB4F File Offset: 0x0000AD4F
		public int CreateWorkshopDefinition(WorkshopDefinitionDTO workshopDefinition)
		{
			return base.Post<WorkshopDefinitionDTO, int>(workshopDefinition, "workshopdefinition");
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000CB5D File Offset: 0x0000AD5D
		public void UpdateWorkshopDefinition(WorkshopDefinitionDTO workshopDefinition)
		{
			base.Put<WorkshopDefinitionDTO>(workshopDefinition, "workshopdefinition");
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000CB6B File Offset: 0x0000AD6B
		public void DeleteWorkshopDefinition(int workshopEventId)
		{
			base.Delete(string.Format("workshopdefinition/workshopeventid/{0}", workshopEventId));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000CB83 File Offset: 0x0000AD83
		public Forest<WorkshopDefinitionOrAppTypeDTO> LoadAppTypesWithWorkshopDefinitions()
		{
			return base.Get<LoadAppTypesWithWorkshopDefinitionsResp>("workshopdefinition/withapptype", true).WorkshopAppTypesWithDefinitions;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000CB96 File Offset: 0x0000AD96
		public IList<WorkshopDefinition> LoadWorkshopDefinitionsByAppType(int appTypeId)
		{
			return base.GetMany<WorkshopDefinition>(string.Format("workshopdefinition/apptype/{0}", appTypeId), true);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000CBAF File Offset: 0x0000ADAF
		public IList<AppTypeDTO> LoadAllWorkshopAppTypes()
		{
			return base.GetMany<AppTypeDTO>("workshopdefinition/allworkshopapptypes", true);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000CBBD File Offset: 0x0000ADBD
		public WorkshopDefinitionDTO LoadWorkshopDefinitionById(int workshopDefinitionId)
		{
			return base.Get<WorkshopDefinitionDTO>(string.Format("workshopdefinition/workshopdefinitionid/{0}", workshopDefinitionId), true);
		}
	}
}
