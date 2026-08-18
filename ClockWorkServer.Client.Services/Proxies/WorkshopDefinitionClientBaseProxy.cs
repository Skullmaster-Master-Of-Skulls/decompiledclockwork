using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000043 RID: 67
	internal class WorkshopDefinitionClientBaseProxy : ClientBase<IWorkshopDefinition>, IWorkshopDefinition, IService
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000A128 File Offset: 0x00008328
		public WorkshopDefinitionClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000A133 File Offset: 0x00008333
		public WorkshopDefinitionClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A140 File Offset: 0x00008340
		public CreateWorkshopDefinitionResp CreateWorkshopDefinition(CreateWorkshopDefinitionReq Request)
		{
			return base.Channel.CreateWorkshopDefinition(Request);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000A160 File Offset: 0x00008360
		public DeleteWorkshopDefinitionResp DeleteWorkshopDefinition(DeleteWorkshopDefinitionReq Request)
		{
			return base.Channel.DeleteWorkshopDefinition(Request);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000A180 File Offset: 0x00008380
		public LoadWorkshopDefinitionsResp LoadWorkshopDefinitions(LoadWorkshopDefinitionsReq Request)
		{
			return base.Channel.LoadWorkshopDefinitions(Request);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public UpdateWorkshopDefinitionResp UpdateWorkshopDefinition(UpdateWorkshopDefinitionReq Request)
		{
			return base.Channel.UpdateWorkshopDefinition(Request);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000A1C0 File Offset: 0x000083C0
		public LoadAllWorkshopAppTypesResp LoadAllWorkshopAppTypes(LoadAllWorkshopAppTypesReq Request)
		{
			return base.Channel.LoadAllWorkshopAppTypes(Request);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000A1E0 File Offset: 0x000083E0
		public LoadWorkshopDefinitionByIdResp LoadWorkshopDefinitionById(LoadWorkshopDefinitionByIdReq Request)
		{
			return base.Channel.LoadWorkshopDefinitionById(Request);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000A200 File Offset: 0x00008400
		public LoadWorkDefinitionsByAppTypeResp LoadWorkshopDefinitionsByAppType(LoadWorkDefinitionsByAppTypeReq request)
		{
			return base.Channel.LoadWorkshopDefinitionsByAppType(request);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000A220 File Offset: 0x00008420
		public LoadAppTypesWithWorkshopDefinitionsResp LoadAppTypesWithWorkshopDefinitions(LoadAppTypesWithWorkshopDefinitionsReq Request)
		{
			return base.Channel.LoadAppTypesWithWorkshopDefinitions(Request);
		}
	}
}
