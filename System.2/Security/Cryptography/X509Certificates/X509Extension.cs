using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000475 RID: 1141
	public class X509Extension : AsnEncodedData
	{
		// Token: 0x06002A62 RID: 10850 RVA: 0x000C1548 File Offset: 0x000BF748
		internal X509Extension(string oid) : base(new Oid(oid, OidGroup.ExtensionOrAttribute, false))
		{
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x000C1558 File Offset: 0x000BF758
		internal X509Extension(IntPtr pExtension)
		{
			CAPIBase.CERT_EXTENSION cert_EXTENSION = (CAPIBase.CERT_EXTENSION)Marshal.PtrToStructure(pExtension, typeof(CAPIBase.CERT_EXTENSION));
			this.m_critical = cert_EXTENSION.fCritical;
			string pszObjId = cert_EXTENSION.pszObjId;
			this.m_oid = new Oid(pszObjId, OidGroup.ExtensionOrAttribute, false);
			byte[] array = new byte[cert_EXTENSION.Value.cbData];
			if (cert_EXTENSION.Value.pbData != IntPtr.Zero)
			{
				Marshal.Copy(cert_EXTENSION.Value.pbData, array, 0, array.Length);
			}
			this.m_rawData = array;
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x000C15E6 File Offset: 0x000BF7E6
		protected X509Extension()
		{
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x000C15EE File Offset: 0x000BF7EE
		public X509Extension(string oid, byte[] rawData, bool critical) : this(new Oid(oid, OidGroup.ExtensionOrAttribute, true), rawData, critical)
		{
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000C1600 File Offset: 0x000BF800
		public X509Extension(AsnEncodedData encodedExtension, bool critical) : this(encodedExtension.Oid, encodedExtension.RawData, critical)
		{
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x000C1618 File Offset: 0x000BF818
		public X509Extension(Oid oid, byte[] rawData, bool critical) : base(oid, rawData)
		{
			if (base.Oid == null || base.Oid.Value == null)
			{
				throw new ArgumentNullException("oid");
			}
			if (base.Oid.Value.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Arg_EmptyOrNullString"), "oid.Value");
			}
			this.m_critical = critical;
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x000C167B File Offset: 0x000BF87B
		// (set) Token: 0x06002A69 RID: 10857 RVA: 0x000C1683 File Offset: 0x000BF883
		public bool Critical
		{
			get
			{
				return this.m_critical;
			}
			set
			{
				this.m_critical = value;
			}
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x000C168C File Offset: 0x000BF88C
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			X509Extension x509Extension = asnEncodedData as X509Extension;
			if (x509Extension == null)
			{
				throw new ArgumentException(SR.GetString("Cryptography_X509_ExtensionMismatch"));
			}
			base.CopyFrom(asnEncodedData);
			this.m_critical = x509Extension.Critical;
		}

		// Token: 0x0400262D RID: 9773
		private bool m_critical;
	}
}
