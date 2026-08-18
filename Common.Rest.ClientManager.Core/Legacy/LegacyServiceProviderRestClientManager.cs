using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Legacy
{
	// Token: 0x0200003D RID: 61
	public class LegacyServiceProviderRestClientManager : BearerTokenRestProxy<ILegacyServiceProviderClientManager>, ILegacyServiceProviderClientManager, IWebService
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00007366 File Offset: 0x00005566
		public LegacyServiceProviderRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007370 File Offset: 0x00005570
		public LegacyServiceProviderRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000737B File Offset: 0x0000557B
		public LegacyRequestDetailNotesAndSpecialInstructionsDTO LoadRequestDetailNotesAndSpecialInstructions(int RequestId)
		{
			return base.Get<LegacyRequestDetailNotesAndSpecialInstructionsDTO>(string.Format("legacyserviceprovider/requestdetailnotesandspecialinstructions/requestid/{0}", RequestId), true);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007394 File Offset: 0x00005594
		public void UpdateRequestDetailNotesAndSpecialInstructions(LegacyRequestDetailNotesAndSpecialInstructionsDTO notesAndSpecialInstructions)
		{
			base.Put<LegacyRequestDetailNotesAndSpecialInstructionsDTO>(notesAndSpecialInstructions, "legacyserviceprovider/requestdetailnotesandspecialinstructions");
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000073A2 File Offset: 0x000055A2
		public void UpdateRequest(LegacyServiceProviderRequestDetailDTO RequestDetail)
		{
			base.Put<LegacyServiceProviderRequestDetailDTO>(RequestDetail, "legacyserviceprovider/serviceproviderrequest");
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000073B0 File Offset: 0x000055B0
		public void UpdateRequestNotes(int RequestId, string notes)
		{
			UpdateServiceProviderRequestNotesReq updateServiceProviderRequestNotesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateServiceProviderRequestNotesReq>();
			updateServiceProviderRequestNotesReq.RequestId = RequestId;
			updateServiceProviderRequestNotesReq.Notes = notes;
			base.Put<UpdateServiceProviderRequestNotesReq>(updateServiceProviderRequestNotesReq, "legacyserviceprovider/serviceproviderrequestnotes");
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000073E2 File Offset: 0x000055E2
		public void UpdateProvider(ServiceProviderDTO provider)
		{
			base.Put<ServiceProviderDTO>(provider, "legacyserviceprovider");
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000073F0 File Offset: 0x000055F0
		public int CreateProvider(ServiceProviderDTO provider)
		{
			return base.Post<ServiceProviderDTO, int>(provider, "legacyserviceprovider");
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000073FE File Offset: 0x000055FE
		public ServiceProviderDTO LoadProvider(int serviceProviderId)
		{
			return base.Get<ServiceProviderDTO>(string.Format("legacyserviceprovider/serviceproviderid/{0}", serviceProviderId), true);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007417 File Offset: 0x00005617
		public int LoadProviderIdByStudentNumber(string snum)
		{
			return base.Get<int>(string.Format("legacyserviceprovider/providerid/studentnumber/{0}", snum), true);
		}
	}
}
