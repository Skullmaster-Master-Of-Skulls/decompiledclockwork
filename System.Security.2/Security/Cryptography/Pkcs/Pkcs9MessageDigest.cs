using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000074 RID: 116
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class Pkcs9MessageDigest : Pkcs9AttributeObject
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x00016EBF File Offset: 0x000150BF
		internal Pkcs9MessageDigest(byte[] encodedMessageDigest) : base(Oid.FromOidValue("1.2.840.113549.1.9.4", OidGroup.ExtensionOrAttribute), encodedMessageDigest)
		{
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00016ED3 File Offset: 0x000150D3
		public Pkcs9MessageDigest() : base(Oid.FromOidValue("1.2.840.113549.1.9.4", OidGroup.ExtensionOrAttribute))
		{
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00016EE6 File Offset: 0x000150E6
		public byte[] MessageDigest
		{
			get
			{
				if (!this.m_decoded && base.RawData != null)
				{
					this.Decode();
				}
				return this.m_messageDigest;
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00016F04 File Offset: 0x00015104
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00016F14 File Offset: 0x00015114
		private void Decode()
		{
			this.m_messageDigest = PkcsUtils.DecodeOctetBytes(base.RawData);
			this.m_decoded = true;
		}

		// Token: 0x040004D2 RID: 1234
		private byte[] m_messageDigest;

		// Token: 0x040004D3 RID: 1235
		private bool m_decoded;
	}
}
