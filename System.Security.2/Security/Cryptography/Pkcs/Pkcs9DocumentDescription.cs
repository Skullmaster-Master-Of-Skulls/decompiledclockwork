using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000072 RID: 114
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class Pkcs9DocumentDescription : Pkcs9AttributeObject
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x00016D5A File Offset: 0x00014F5A
		public Pkcs9DocumentDescription() : base(new Oid("1.3.6.1.4.1.311.88.2.2"))
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00016D6C File Offset: 0x00014F6C
		public Pkcs9DocumentDescription(string documentDescription) : base("1.3.6.1.4.1.311.88.2.2", Pkcs9DocumentDescription.Encode(documentDescription))
		{
			this.m_documentDescription = documentDescription;
			this.m_decoded = true;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00016D8D File Offset: 0x00014F8D
		public Pkcs9DocumentDescription(byte[] encodedDocumentDescription) : base("1.3.6.1.4.1.311.88.2.2", encodedDocumentDescription)
		{
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00016D9B File Offset: 0x00014F9B
		public string DocumentDescription
		{
			get
			{
				if (!this.m_decoded && base.RawData != null)
				{
					this.Decode();
				}
				return this.m_documentDescription;
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00016DB9 File Offset: 0x00014FB9
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00016DC9 File Offset: 0x00014FC9
		private void Decode()
		{
			this.m_documentDescription = PkcsUtils.DecodeOctetString(base.RawData);
			this.m_decoded = true;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00016DE3 File Offset: 0x00014FE3
		private static byte[] Encode(string documentDescription)
		{
			if (string.IsNullOrEmpty(documentDescription))
			{
				throw new ArgumentNullException("documentDescription");
			}
			return PkcsUtils.EncodeOctetString(documentDescription);
		}

		// Token: 0x040004CE RID: 1230
		private string m_documentDescription;

		// Token: 0x040004CF RID: 1231
		private bool m_decoded;
	}
}
