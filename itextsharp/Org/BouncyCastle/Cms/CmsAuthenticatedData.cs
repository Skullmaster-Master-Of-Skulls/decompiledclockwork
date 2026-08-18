using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000250 RID: 592
	public class CmsAuthenticatedData
	{
		// Token: 0x0600169E RID: 5790 RVA: 0x00083422 File Offset: 0x00082422
		public CmsAuthenticatedData(byte[] authData) : this(CmsUtilities.ReadContentInfo(authData))
		{
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00083430 File Offset: 0x00082430
		public CmsAuthenticatedData(Stream authData) : this(CmsUtilities.ReadContentInfo(authData))
		{
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00083440 File Offset: 0x00082440
		public CmsAuthenticatedData(ContentInfo contentInfo)
		{
			this.contentInfo = contentInfo;
			AuthenticatedData instance = AuthenticatedData.GetInstance(contentInfo.Content);
			ContentInfo encapsulatedContentInfo = instance.EncapsulatedContentInfo;
			this.macAlg = instance.MacAlgorithm;
			this.mac = instance.Mac.GetOctets();
			byte[] octets = Asn1OctetString.GetInstance(encapsulatedContentInfo.Content).GetOctets();
			IList recipientInfos = CmsEnvelopedHelper.ReadRecipientInfos(instance.RecipientInfos, octets, null, this.macAlg, null);
			this.authAttrs = instance.AuthAttrs;
			this.recipientInfoStore = new RecipientInformationStore(recipientInfos);
			this.unauthAttrs = instance.UnauthAttrs;
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x000834D4 File Offset: 0x000824D4
		public byte[] GetMac()
		{
			return Arrays.Clone(this.mac);
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000834E1 File Offset: 0x000824E1
		public AlgorithmIdentifier MacAlgorithmID
		{
			get
			{
				return this.macAlg;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x000834E9 File Offset: 0x000824E9
		public string MacAlgOid
		{
			get
			{
				return this.macAlg.ObjectID.Id;
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000834FB File Offset: 0x000824FB
		public RecipientInformationStore GetRecipientInfos()
		{
			return this.recipientInfoStore;
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00083503 File Offset: 0x00082503
		public ContentInfo ContentInfo
		{
			get
			{
				return this.contentInfo;
			}
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x0008350B File Offset: 0x0008250B
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetAuthAttrs()
		{
			if (this.authAttrs == null)
			{
				return null;
			}
			return new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.authAttrs);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00083522 File Offset: 0x00082522
		public Org.BouncyCastle.Asn1.Cms.AttributeTable GetUnauthAttrs()
		{
			if (this.unauthAttrs == null)
			{
				return null;
			}
			return new Org.BouncyCastle.Asn1.Cms.AttributeTable(this.unauthAttrs);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00083539 File Offset: 0x00082539
		public byte[] GetEncoded()
		{
			return this.contentInfo.GetEncoded();
		}

		// Token: 0x04000F7C RID: 3964
		internal RecipientInformationStore recipientInfoStore;

		// Token: 0x04000F7D RID: 3965
		internal ContentInfo contentInfo;

		// Token: 0x04000F7E RID: 3966
		private AlgorithmIdentifier macAlg;

		// Token: 0x04000F7F RID: 3967
		private Asn1Set authAttrs;

		// Token: 0x04000F80 RID: 3968
		private Asn1Set unauthAttrs;

		// Token: 0x04000F81 RID: 3969
		private byte[] mac;
	}
}
