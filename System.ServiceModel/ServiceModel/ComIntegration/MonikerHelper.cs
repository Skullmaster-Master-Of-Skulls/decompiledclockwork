using System;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000236 RID: 566
	internal static class MonikerHelper
	{
		// Token: 0x02000B11 RID: 2833
		internal enum MonikerAttribute
		{
			// Token: 0x04003FAB RID: 16299
			Address,
			// Token: 0x04003FAC RID: 16300
			Contract,
			// Token: 0x04003FAD RID: 16301
			Wsdl,
			// Token: 0x04003FAE RID: 16302
			SpnIdentity,
			// Token: 0x04003FAF RID: 16303
			UpnIdentity,
			// Token: 0x04003FB0 RID: 16304
			DnsIdentity,
			// Token: 0x04003FB1 RID: 16305
			Binding,
			// Token: 0x04003FB2 RID: 16306
			BindingConfiguration,
			// Token: 0x04003FB3 RID: 16307
			MexAddress,
			// Token: 0x04003FB4 RID: 16308
			MexBinding,
			// Token: 0x04003FB5 RID: 16309
			MexBindingConfiguration,
			// Token: 0x04003FB6 RID: 16310
			BindingNamespace,
			// Token: 0x04003FB7 RID: 16311
			ContractNamespace,
			// Token: 0x04003FB8 RID: 16312
			MexSpnIdentity,
			// Token: 0x04003FB9 RID: 16313
			MexUpnIdentity,
			// Token: 0x04003FBA RID: 16314
			MexDnsIdentity,
			// Token: 0x04003FBB RID: 16315
			Serializer
		}

		// Token: 0x02000B12 RID: 2834
		internal struct KeywordInfo
		{
			// Token: 0x06006F75 RID: 28533 RVA: 0x0019DD1C File Offset: 0x0019BF1C
			internal KeywordInfo(string name, MonikerHelper.MonikerAttribute attrib)
			{
				this.Name = name;
				this.Attrib = attrib;
			}

			// Token: 0x04003FBC RID: 16316
			internal string Name;

			// Token: 0x04003FBD RID: 16317
			internal MonikerHelper.MonikerAttribute Attrib;

			// Token: 0x04003FBE RID: 16318
			internal static readonly MonikerHelper.KeywordInfo[] KeywordCollection = new MonikerHelper.KeywordInfo[]
			{
				new MonikerHelper.KeywordInfo("address", MonikerHelper.MonikerAttribute.Address),
				new MonikerHelper.KeywordInfo("contract", MonikerHelper.MonikerAttribute.Contract),
				new MonikerHelper.KeywordInfo("wsdl", MonikerHelper.MonikerAttribute.Wsdl),
				new MonikerHelper.KeywordInfo("spnidentity", MonikerHelper.MonikerAttribute.SpnIdentity),
				new MonikerHelper.KeywordInfo("upnidentity", MonikerHelper.MonikerAttribute.UpnIdentity),
				new MonikerHelper.KeywordInfo("dnsidentity", MonikerHelper.MonikerAttribute.DnsIdentity),
				new MonikerHelper.KeywordInfo("binding", MonikerHelper.MonikerAttribute.Binding),
				new MonikerHelper.KeywordInfo("bindingconfiguration", MonikerHelper.MonikerAttribute.BindingConfiguration),
				new MonikerHelper.KeywordInfo("mexaddress", MonikerHelper.MonikerAttribute.MexAddress),
				new MonikerHelper.KeywordInfo("mexbindingconfiguration", MonikerHelper.MonikerAttribute.MexBindingConfiguration),
				new MonikerHelper.KeywordInfo("mexbinding", MonikerHelper.MonikerAttribute.MexBinding),
				new MonikerHelper.KeywordInfo("bindingnamespace", MonikerHelper.MonikerAttribute.BindingNamespace),
				new MonikerHelper.KeywordInfo("contractnamespace", MonikerHelper.MonikerAttribute.ContractNamespace),
				new MonikerHelper.KeywordInfo("mexspnidentity", MonikerHelper.MonikerAttribute.MexSpnIdentity),
				new MonikerHelper.KeywordInfo("mexupnidentity", MonikerHelper.MonikerAttribute.MexUpnIdentity),
				new MonikerHelper.KeywordInfo("mexdnsidentity", MonikerHelper.MonikerAttribute.MexDnsIdentity),
				new MonikerHelper.KeywordInfo("serializer", MonikerHelper.MonikerAttribute.Serializer)
			};
		}
	}
}
