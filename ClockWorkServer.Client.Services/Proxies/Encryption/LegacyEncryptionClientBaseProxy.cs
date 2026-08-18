using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Encryption;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies.Encryption
{
	// Token: 0x02000171 RID: 369
	internal class LegacyEncryptionClientBaseProxy : ClientBase<ILegacyEncryption>, ILegacyEncryption, IService
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x00025370 File Offset: 0x00023570
		public LegacyEncryptionClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x0002537B File Offset: 0x0002357B
		public LegacyEncryptionClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00025388 File Offset: 0x00023588
		public EncryptResp Encrypt(EncryptReq Request)
		{
			return base.Channel.Encrypt(Request);
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000253A8 File Offset: 0x000235A8
		public DecryptResp Decrypt(DecryptReq Request)
		{
			return base.Channel.Decrypt(Request);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000253C8 File Offset: 0x000235C8
		public EncryptOrDecryptNameDataTableBatchResp EncryptOrDecryptNameDataTableBatch(EncryptOrDecryptNameDataTableBatchReq Request)
		{
			return base.Channel.EncryptOrDecryptNameDataTableBatch(Request);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000253E8 File Offset: 0x000235E8
		public EncryptDataResp EncryptData(EncryptDataReq Request)
		{
			return base.Channel.EncryptData(Request);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x00025408 File Offset: 0x00023608
		public DecryptDataResp DecryptData(DecryptDataReq Request)
		{
			return base.Channel.DecryptData(Request);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x00025428 File Offset: 0x00023628
		public EncodeUrlVariableResp EncodeUrlVariable(EncodeUrlVariableReq Request)
		{
			return base.Channel.EncodeUrlVariable(Request);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00025448 File Offset: 0x00023648
		public DecryptLegacyDataItemsNeedingDecryptionResp DecryptLegacyDataItemsNeedingDecryption(DecryptLegacyDataItemsNeedingDecryptionReq Request)
		{
			return base.Channel.DecryptLegacyDataItemsNeedingDecryption(Request);
		}
	}
}
