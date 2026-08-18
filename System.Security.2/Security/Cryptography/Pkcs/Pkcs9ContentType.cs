using System;
using System.Security.Permissions;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000073 RID: 115
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class Pkcs9ContentType : Pkcs9AttributeObject
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x00016DFE File Offset: 0x00014FFE
		internal Pkcs9ContentType(byte[] encodedContentType) : base(Oid.FromOidValue("1.2.840.113549.1.9.3", OidGroup.ExtensionOrAttribute), encodedContentType)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00016E12 File Offset: 0x00015012
		public Pkcs9ContentType() : base(Oid.FromOidValue("1.2.840.113549.1.9.3", OidGroup.ExtensionOrAttribute))
		{
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00016E25 File Offset: 0x00015025
		public Oid ContentType
		{
			get
			{
				if (!this.m_decoded && base.RawData != null)
				{
					this.Decode();
				}
				return this.m_contentType;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00016E43 File Offset: 0x00015043
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00016E54 File Offset: 0x00015054
		private void Decode()
		{
			if (base.RawData.Length < 2 || (int)base.RawData[1] != base.RawData.Length - 2)
			{
				throw new CryptographicException(-2146885630);
			}
			if (base.RawData[0] != 6)
			{
				throw new CryptographicException(-2146881269);
			}
			this.m_contentType = new Oid(PkcsUtils.DecodeObjectIdentifier(base.RawData, 2));
			this.m_decoded = true;
		}

		// Token: 0x040004D0 RID: 1232
		private Oid m_contentType;

		// Token: 0x040004D1 RID: 1233
		private bool m_decoded;
	}
}
