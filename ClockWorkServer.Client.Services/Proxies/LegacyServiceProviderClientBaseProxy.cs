using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000CA RID: 202
	internal class LegacyServiceProviderClientBaseProxy : ClientBase<ILegacyServiceProvider>, ILegacyServiceProvider, IService
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x00014CD8 File Offset: 0x00012ED8
		public LegacyServiceProviderClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00014CE3 File Offset: 0x00012EE3
		public LegacyServiceProviderClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00014CF0 File Offset: 0x00012EF0
		public LoadRequestDetailNotesAndSpecialInstructionsResp LoadRequestDetailNotesAndSpecialInstructions(LoadRequestDetailNotesAndSpecialInstructionsReq Request)
		{
			return base.Channel.LoadRequestDetailNotesAndSpecialInstructions(Request);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00014D10 File Offset: 0x00012F10
		public UpdateRequestDetailNotesAndSpecialInstructionsResp UpdateRequestDetailNotesAndSpecialInstructions(UpdateRequestDetailNotesAndSpecialInstructionsReq Request)
		{
			return base.Channel.UpdateRequestDetailNotesAndSpecialInstructions(Request);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00014D30 File Offset: 0x00012F30
		public UpdateServiceProviderRequestResp UpdateServiceProviderRequest(UpdateServiceProviderRequestReq Request)
		{
			return base.Channel.UpdateServiceProviderRequest(Request);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00014D50 File Offset: 0x00012F50
		public UpdateServiceProviderRequestNotesResp UpdateServiceProviderRequestNotes(UpdateServiceProviderRequestNotesReq Request)
		{
			return base.Channel.UpdateServiceProviderRequestNotes(Request);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00014D70 File Offset: 0x00012F70
		public UpdateServiceProviderResp UpdateServiceProvider(UpdateServiceProviderReq Request)
		{
			return base.Channel.UpdateServiceProvider(Request);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00014D90 File Offset: 0x00012F90
		public CreateServiceProviderResp CreateServiceProvider(CreateServiceProviderReq Request)
		{
			return base.Channel.CreateServiceProvider(Request);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00014DB0 File Offset: 0x00012FB0
		public LoadProviderResp LoadProvider(LoadProviderReq Request)
		{
			return base.Channel.LoadProvider(Request);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00014DD0 File Offset: 0x00012FD0
		public LoadProviderIdByStudentNumberResp LoadProviderIdByStudentNumber(LoadProviderIdByStudentNumberReq Request)
		{
			return base.Channel.LoadProviderIdByStudentNumber(Request);
		}
	}
}
