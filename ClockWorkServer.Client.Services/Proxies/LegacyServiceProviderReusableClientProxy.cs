using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C9 RID: 201
	public class LegacyServiceProviderReusableClientProxy : WCFTokenBasedReusableClientProxy<ILegacyServiceProvider>, ILegacyServiceProvider, IService
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x00014AFE File Offset: 0x00012CFE
		public LegacyServiceProviderReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00014B09 File Offset: 0x00012D09
		public LegacyServiceProviderReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00014B18 File Offset: 0x00012D18
		public LoadRequestDetailNotesAndSpecialInstructionsResp LoadRequestDetailNotesAndSpecialInstructions(LoadRequestDetailNotesAndSpecialInstructionsReq Request)
		{
			return this.WrapServiceMethod<LoadRequestDetailNotesAndSpecialInstructionsResp>(() => this.Proxy.LoadRequestDetailNotesAndSpecialInstructions(Request));
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00014B50 File Offset: 0x00012D50
		public UpdateRequestDetailNotesAndSpecialInstructionsResp UpdateRequestDetailNotesAndSpecialInstructions(UpdateRequestDetailNotesAndSpecialInstructionsReq Request)
		{
			return this.WrapServiceMethod<UpdateRequestDetailNotesAndSpecialInstructionsResp>(() => this.Proxy.UpdateRequestDetailNotesAndSpecialInstructions(Request));
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00014B88 File Offset: 0x00012D88
		public UpdateServiceProviderRequestResp UpdateServiceProviderRequest(UpdateServiceProviderRequestReq Request)
		{
			return this.WrapServiceMethod<UpdateServiceProviderRequestResp>(() => this.Proxy.UpdateServiceProviderRequest(Request));
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00014BC0 File Offset: 0x00012DC0
		public UpdateServiceProviderRequestNotesResp UpdateServiceProviderRequestNotes(UpdateServiceProviderRequestNotesReq Request)
		{
			return this.WrapServiceMethod<UpdateServiceProviderRequestNotesResp>(() => this.Proxy.UpdateServiceProviderRequestNotes(Request));
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00014BF8 File Offset: 0x00012DF8
		public UpdateServiceProviderResp UpdateServiceProvider(UpdateServiceProviderReq Request)
		{
			return this.WrapServiceMethod<UpdateServiceProviderResp>(() => this.Proxy.UpdateServiceProvider(Request));
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00014C30 File Offset: 0x00012E30
		public CreateServiceProviderResp CreateServiceProvider(CreateServiceProviderReq Request)
		{
			return this.WrapServiceMethod<CreateServiceProviderResp>(() => this.Proxy.CreateServiceProvider(Request));
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00014C68 File Offset: 0x00012E68
		public LoadProviderResp LoadProvider(LoadProviderReq Request)
		{
			return this.WrapServiceMethod<LoadProviderResp>(() => this.Proxy.LoadProvider(Request));
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00014CA0 File Offset: 0x00012EA0
		public LoadProviderIdByStudentNumberResp LoadProviderIdByStudentNumber(LoadProviderIdByStudentNumberReq Request)
		{
			return this.WrapServiceMethod<LoadProviderIdByStudentNumberResp>(() => this.Proxy.LoadProviderIdByStudentNumber(Request));
		}
	}
}
