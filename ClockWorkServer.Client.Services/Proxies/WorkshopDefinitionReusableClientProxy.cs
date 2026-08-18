using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000042 RID: 66
	public class WorkshopDefinitionReusableClientProxy : WCFTokenBasedReusableClientProxy<IWorkshopDefinition>, IWorkshopDefinition, IService
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00009F4E File Offset: 0x0000814E
		public WorkshopDefinitionReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009F59 File Offset: 0x00008159
		public WorkshopDefinitionReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00009F68 File Offset: 0x00008168
		public CreateWorkshopDefinitionResp CreateWorkshopDefinition(CreateWorkshopDefinitionReq Request)
		{
			return this.WrapServiceMethod<CreateWorkshopDefinitionResp>(() => this.Proxy.CreateWorkshopDefinition(Request));
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00009FA0 File Offset: 0x000081A0
		public DeleteWorkshopDefinitionResp DeleteWorkshopDefinition(DeleteWorkshopDefinitionReq Request)
		{
			return this.WrapServiceMethod<DeleteWorkshopDefinitionResp>(() => this.Proxy.DeleteWorkshopDefinition(Request));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00009FD8 File Offset: 0x000081D8
		public LoadWorkshopDefinitionsResp LoadWorkshopDefinitions(LoadWorkshopDefinitionsReq Request)
		{
			return this.WrapServiceMethod<LoadWorkshopDefinitionsResp>(() => this.Proxy.LoadWorkshopDefinitions(Request));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000A010 File Offset: 0x00008210
		public UpdateWorkshopDefinitionResp UpdateWorkshopDefinition(UpdateWorkshopDefinitionReq Request)
		{
			return this.WrapServiceMethod<UpdateWorkshopDefinitionResp>(() => this.Proxy.UpdateWorkshopDefinition(Request));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000A048 File Offset: 0x00008248
		public LoadAllWorkshopAppTypesResp LoadAllWorkshopAppTypes(LoadAllWorkshopAppTypesReq Request)
		{
			return this.WrapServiceMethod<LoadAllWorkshopAppTypesResp>(() => this.Proxy.LoadAllWorkshopAppTypes(Request));
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000A080 File Offset: 0x00008280
		public LoadWorkshopDefinitionByIdResp LoadWorkshopDefinitionById(LoadWorkshopDefinitionByIdReq Request)
		{
			return this.WrapServiceMethod<LoadWorkshopDefinitionByIdResp>(() => this.Proxy.LoadWorkshopDefinitionById(Request));
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public LoadWorkDefinitionsByAppTypeResp LoadWorkshopDefinitionsByAppType(LoadWorkDefinitionsByAppTypeReq request)
		{
			return this.WrapServiceMethod<LoadWorkDefinitionsByAppTypeResp>(() => this.Proxy.LoadWorkshopDefinitionsByAppType(request));
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000A0F0 File Offset: 0x000082F0
		public LoadAppTypesWithWorkshopDefinitionsResp LoadAppTypesWithWorkshopDefinitions(LoadAppTypesWithWorkshopDefinitionsReq Request)
		{
			return this.WrapServiceMethod<LoadAppTypesWithWorkshopDefinitionsResp>(() => this.Proxy.LoadAppTypesWithWorkshopDefinitions(Request));
		}
	}
}
