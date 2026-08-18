using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Encryption;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies.Encryption
{
	// Token: 0x02000170 RID: 368
	public class LegacyEncryptionReusableClientProxy : WCFTokenBasedReusableClientProxy<ILegacyEncryption>, ILegacyEncryption, IService
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x000251CE File Offset: 0x000233CE
		public LegacyEncryptionReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000251D9 File Offset: 0x000233D9
		public LegacyEncryptionReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000251E8 File Offset: 0x000233E8
		public EncryptResp Encrypt(EncryptReq Request)
		{
			return this.WrapServiceMethod<EncryptResp>(() => this.Proxy.Encrypt(Request));
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x00025220 File Offset: 0x00023420
		public DecryptResp Decrypt(DecryptReq Request)
		{
			return this.WrapServiceMethod<DecryptResp>(() => this.Proxy.Decrypt(Request));
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00025258 File Offset: 0x00023458
		public EncryptOrDecryptNameDataTableBatchResp EncryptOrDecryptNameDataTableBatch(EncryptOrDecryptNameDataTableBatchReq Request)
		{
			return this.WrapServiceMethod<EncryptOrDecryptNameDataTableBatchResp>(() => this.Proxy.EncryptOrDecryptNameDataTableBatch(Request));
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00025290 File Offset: 0x00023490
		public EncryptDataResp EncryptData(EncryptDataReq Request)
		{
			return this.WrapServiceMethod<EncryptDataResp>(() => this.Proxy.EncryptData(Request));
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000252C8 File Offset: 0x000234C8
		public DecryptDataResp DecryptData(DecryptDataReq Request)
		{
			return this.WrapServiceMethod<DecryptDataResp>(() => this.Proxy.DecryptData(Request));
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00025300 File Offset: 0x00023500
		public EncodeUrlVariableResp EncodeUrlVariable(EncodeUrlVariableReq Request)
		{
			return this.WrapServiceMethod<EncodeUrlVariableResp>(() => this.Proxy.EncodeUrlVariable(Request));
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00025338 File Offset: 0x00023538
		public DecryptLegacyDataItemsNeedingDecryptionResp DecryptLegacyDataItemsNeedingDecryption(DecryptLegacyDataItemsNeedingDecryptionReq Request)
		{
			return this.WrapServiceMethod<DecryptLegacyDataItemsNeedingDecryptionResp>(() => this.Proxy.DecryptLegacyDataItemsNeedingDecryption(Request));
		}
	}
}
