using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020005B0 RID: 1456
	internal class CmsAuthEnvelopedData
	{
		// Token: 0x0600323D RID: 12861 RVA: 0x00138681 File Offset: 0x00137681
		public CmsAuthEnvelopedData(byte[] authEnvData) : this(CmsUtilities.ReadContentInfo(authEnvData))
		{
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x0013868F File Offset: 0x0013768F
		public CmsAuthEnvelopedData(Stream authEnvData) : this(CmsUtilities.ReadContentInfo(authEnvData))
		{
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x001386A0 File Offset: 0x001376A0
		public CmsAuthEnvelopedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			AuthEnvelopedData instance = AuthEnvelopedData.GetInstance(contentInfo.Content);
			this.originator = instance.OriginatorInfo;
			EncryptedContentInfo authEncryptedContentInfo = instance.AuthEncryptedContentInfo;
			this.authEncAlg = authEncryptedContentInfo.ContentEncryptionAlgorithm;
			byte[] octets = authEncryptedContentInfo.EncryptedContent.GetOctets();
			IList recipientInfos = CmsEnvelopedHelper.ReadRecipientInfos(instance.RecipientInfos, octets, null, null, this.authEncAlg);
			this.recipientInfoStore = new RecipientInformationStore(recipientInfos);
			this.authAttrs = instance.AuthAttrs;
			this.mac = instance.Mac.GetOctets();
			this.unauthAttrs = instance.UnauthAttrs;
		}

		// Token: 0x0400226E RID: 8814
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x0400226F RID: 8815
		internal ContentInfo contentInfo;

		// Token: 0x04002270 RID: 8816
		private OriginatorInfo originator;

		// Token: 0x04002271 RID: 8817
		private AlgorithmIdentifier authEncAlg;

		// Token: 0x04002272 RID: 8818
		private Asn1Set authAttrs;

		// Token: 0x04002273 RID: 8819
		private byte[] mac;

		// Token: 0x04002274 RID: 8820
		private Asn1Set unauthAttrs;
	}
}
