using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Legacy
{
	// Token: 0x0200003C RID: 60
	public class LegacyDynamicDataRestClientManager : BearerTokenRestProxy<ILegacyDynamicDataClientManager>, ILegacyDynamicDataClientManager, IWebService
	{
		// Token: 0x0600022B RID: 555 RVA: 0x000072DD File Offset: 0x000054DD
		public LegacyDynamicDataRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000072E7 File Offset: 0x000054E7
		public LegacyDynamicDataRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000072F2 File Offset: 0x000054F2
		public IList<DynamicDataDecryptedPreviewItemDTO> GetDynamicDataDecryptedPreviewItems(int ScreenNum, int ControlId)
		{
			return base.GetMany<DynamicDataDecryptedPreviewItemDTO>(string.Format("legacydynamicdata/dynamicdatadecryptedpreviewitems/screennum/{0}/controlid/{1}", ScreenNum, ControlId), true);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007314 File Offset: 0x00005514
		public int ReverseEncryptionOnData(int ScreenNum, int ControlId, bool newEncrypted)
		{
			ReverseEncryptionOnDataReq reverseEncryptionOnDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReverseEncryptionOnDataReq>();
			reverseEncryptionOnDataReq.ScreenNum = ScreenNum;
			reverseEncryptionOnDataReq.ControlId = ControlId;
			reverseEncryptionOnDataReq.NewEncrypted = newEncrypted;
			return base.Post<ReverseEncryptionOnDataReq, int>(reverseEncryptionOnDataReq, "legacydynamicdata/reverseencryptionondata");
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000734D File Offset: 0x0000554D
		public string LookupStaffSignatureBase64(int pid)
		{
			return base.Get<string>(string.Format("legacydynamicdata/lookupstaffsignaturebase64/personid/{0}", pid), true);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00002BEE File Offset: 0x00000DEE
		public void SaveLegacyStudentNote(LegacyStudentNoteDTO note)
		{
			throw new NotImplementedException();
		}
	}
}
