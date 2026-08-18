using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000071 RID: 113
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class Pkcs9DocumentName : Pkcs9AttributeObject
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x00016CB6 File Offset: 0x00014EB6
		public Pkcs9DocumentName() : base(new Oid("1.3.6.1.4.1.311.88.2.1"))
		{
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00016CC8 File Offset: 0x00014EC8
		public Pkcs9DocumentName(string documentName) : base("1.3.6.1.4.1.311.88.2.1", Pkcs9DocumentName.Encode(documentName))
		{
			this.m_documentName = documentName;
			this.m_decoded = true;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00016CE9 File Offset: 0x00014EE9
		public Pkcs9DocumentName(byte[] encodedDocumentName) : base("1.3.6.1.4.1.311.88.2.1", encodedDocumentName)
		{
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00016CF7 File Offset: 0x00014EF7
		public string DocumentName
		{
			get
			{
				if (!this.m_decoded && base.RawData != null)
				{
					this.Decode();
				}
				return this.m_documentName;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00016D15 File Offset: 0x00014F15
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00016D25 File Offset: 0x00014F25
		private void Decode()
		{
			this.m_documentName = PkcsUtils.DecodeOctetString(base.RawData);
			this.m_decoded = true;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00016D3F File Offset: 0x00014F3F
		private static byte[] Encode(string documentName)
		{
			if (string.IsNullOrEmpty(documentName))
			{
				throw new ArgumentNullException("documentName");
			}
			return PkcsUtils.EncodeOctetString(documentName);
		}

		// Token: 0x040004CC RID: 1228
		private string m_documentName;

		// Token: 0x040004CD RID: 1229
		private bool m_decoded;
	}
}
