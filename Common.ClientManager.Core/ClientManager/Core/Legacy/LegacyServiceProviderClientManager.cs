using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Legacy
{
	// Token: 0x0200004B RID: 75
	public class LegacyServiceProviderClientManager : ILegacyServiceProviderClientManager, IWebService
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000C09C File Offset: 0x0000A29C
		public LegacyRequestDetailNotesAndSpecialInstructionsDTO LoadRequestDetailNotesAndSpecialInstructions(int RequestId)
		{
			LoadRequestDetailNotesAndSpecialInstructionsReq loadRequestDetailNotesAndSpecialInstructionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRequestDetailNotesAndSpecialInstructionsReq>();
			loadRequestDetailNotesAndSpecialInstructionsReq.RequestId = RequestId;
			return ClientServiceFactory.GetClientInstance<ILegacyServiceProvider>().LoadRequestDetailNotesAndSpecialInstructions(loadRequestDetailNotesAndSpecialInstructionsReq).DetailNotesAndSpecialInstructions;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C0D4 File Offset: 0x0000A2D4
		public void UpdateRequestDetailNotesAndSpecialInstructions(LegacyRequestDetailNotesAndSpecialInstructionsDTO notesAndSpecialInstructions)
		{
			UpdateRequestDetailNotesAndSpecialInstructionsReq updateRequestDetailNotesAndSpecialInstructionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequestDetailNotesAndSpecialInstructionsReq>();
			updateRequestDetailNotesAndSpecialInstructionsReq.NotesAndSpecialInstructions = notesAndSpecialInstructions;
			ClientServiceFactory.GetClientInstance<ILegacyServiceProvider>().UpdateRequestDetailNotesAndSpecialInstructions(updateRequestDetailNotesAndSpecialInstructionsReq);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C104 File Offset: 0x0000A304
		public void UpdateRequest(LegacyServiceProviderRequestDetailDTO RequestDetail)
		{
			UpdateServiceProviderRequestReq updateServiceProviderRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateServiceProviderRequestReq>();
			updateServiceProviderRequestReq.RequestDetail = RequestDetail;
			ClientServiceFactory.GetClientInstance<ILegacyServiceProvider>().UpdateServiceProviderRequest(updateServiceProviderRequestReq);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C134 File Offset: 0x0000A334
		public void UpdateRequestNotes(int RequestId, string notes)
		{
			UpdateServiceProviderRequestNotesReq updateServiceProviderRequestNotesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateServiceProviderRequestNotesReq>();
			updateServiceProviderRequestNotesReq.RequestId = RequestId;
			updateServiceProviderRequestNotesReq.Notes = notes;
			ClientServiceFactory.GetClientInstance<ILegacyServiceProvider>().UpdateServiceProviderRequestNotes(updateServiceProviderRequestNotesReq);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C16C File Offset: 0x0000A36C
		public void UpdateProvider(ServiceProviderDTO provider)
		{
			UpdateServiceProviderReq updateServiceProviderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateServiceProviderReq>();
			updateServiceProviderReq.Provider = provider;
			ClientServiceFactory.GetClientInstance<ILegacyServiceProvider>().UpdateServiceProvider(updateServiceProviderReq);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C19C File Offset: 0x0000A39C
		public int CreateProvider(ServiceProviderDTO provider)
		{
			CreateServiceProviderReq createServiceProviderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateServiceProviderReq>();
			createServiceProviderReq.Provider = provider;
			return ClientServiceFactory.GetClientInstance<ILegacyServiceProvider>().CreateServiceProvider(createServiceProviderReq).ServiceProviderId;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000C1D4 File Offset: 0x0000A3D4
		public ServiceProviderDTO LoadProvider(int serviceProviderId)
		{
			LoadProviderReq loadProviderReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderReq>();
			loadProviderReq.ServiceProviderId = serviceProviderId;
			return ClientServiceFactory.GetClientInstance<ILegacyServiceProvider>().LoadProvider(loadProviderReq).Provider;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000C20C File Offset: 0x0000A40C
		public int LoadProviderIdByStudentNumber(string snum)
		{
			LoadProviderIdByStudentNumberReq loadProviderIdByStudentNumberReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadProviderIdByStudentNumberReq>();
			loadProviderIdByStudentNumberReq.StudentNumber = snum;
			return ClientServiceFactory.GetClientInstance<ILegacyServiceProvider>().LoadProviderIdByStudentNumber(loadProviderIdByStudentNumberReq).ServiceProviderId;
		}
	}
}
