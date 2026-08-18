using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000478 RID: 1144
	public sealed class X509BasicConstraintsExtension : X509Extension
	{
		// Token: 0x06002A72 RID: 10866 RVA: 0x000C183C File Offset: 0x000BFA3C
		public X509BasicConstraintsExtension() : base("2.5.29.19")
		{
			this.m_decoded = true;
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x000C1850 File Offset: 0x000BFA50
		public X509BasicConstraintsExtension(bool certificateAuthority, bool hasPathLengthConstraint, int pathLengthConstraint, bool critical) : base("2.5.29.19", X509BasicConstraintsExtension.EncodeExtension(certificateAuthority, hasPathLengthConstraint, pathLengthConstraint), critical)
		{
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x000C1867 File Offset: 0x000BFA67
		public X509BasicConstraintsExtension(AsnEncodedData encodedBasicConstraints, bool critical) : base("2.5.29.19", encodedBasicConstraints.RawData, critical)
		{
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002A75 RID: 10869 RVA: 0x000C187B File Offset: 0x000BFA7B
		public bool CertificateAuthority
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return this.m_isCA;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x000C1891 File Offset: 0x000BFA91
		public bool HasPathLengthConstraint
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return this.m_hasPathLenConstraint;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002A77 RID: 10871 RVA: 0x000C18A7 File Offset: 0x000BFAA7
		public int PathLengthConstraint
		{
			get
			{
				if (!this.m_decoded)
				{
					this.DecodeExtension();
				}
				return this.m_pathLenConstraint;
			}
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000C18BD File Offset: 0x000BFABD
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.m_decoded = false;
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000C18D0 File Offset: 0x000BFAD0
		private void DecodeExtension()
		{
			uint num = 0U;
			SafeLocalAllocHandle safeLocalAllocHandle = null;
			if (base.Oid.Value == "2.5.29.10")
			{
				if (!CAPI.DecodeObject(new IntPtr(13L), this.m_rawData, out safeLocalAllocHandle, out num))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				CAPIBase.CERT_BASIC_CONSTRAINTS_INFO cert_BASIC_CONSTRAINTS_INFO = (CAPIBase.CERT_BASIC_CONSTRAINTS_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_BASIC_CONSTRAINTS_INFO));
				byte[] array = new byte[1];
				Marshal.Copy(cert_BASIC_CONSTRAINTS_INFO.SubjectType.pbData, array, 0, 1);
				this.m_isCA = ((array[0] & 128) != 0);
				this.m_hasPathLenConstraint = cert_BASIC_CONSTRAINTS_INFO.fPathLenConstraint;
				this.m_pathLenConstraint = (int)cert_BASIC_CONSTRAINTS_INFO.dwPathLenConstraint;
			}
			else
			{
				if (!CAPI.DecodeObject(new IntPtr(15L), this.m_rawData, out safeLocalAllocHandle, out num))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
				CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO cert_BASIC_CONSTRAINTS2_INFO = (CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO)Marshal.PtrToStructure(safeLocalAllocHandle.DangerousGetHandle(), typeof(CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO));
				this.m_isCA = (cert_BASIC_CONSTRAINTS2_INFO.fCA != 0);
				this.m_hasPathLenConstraint = (cert_BASIC_CONSTRAINTS2_INFO.fPathLenConstraint != 0);
				this.m_pathLenConstraint = (int)cert_BASIC_CONSTRAINTS2_INFO.dwPathLenConstraint;
			}
			this.m_decoded = true;
			safeLocalAllocHandle.Dispose();
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000C1A0C File Offset: 0x000BFC0C
		private unsafe static byte[] EncodeExtension(bool certificateAuthority, bool hasPathLengthConstraint, int pathLengthConstraint)
		{
			CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO cert_BASIC_CONSTRAINTS2_INFO = default(CAPIBase.CERT_BASIC_CONSTRAINTS2_INFO);
			cert_BASIC_CONSTRAINTS2_INFO.fCA = (certificateAuthority ? 1 : 0);
			cert_BASIC_CONSTRAINTS2_INFO.fPathLenConstraint = (hasPathLengthConstraint ? 1 : 0);
			if (hasPathLengthConstraint)
			{
				if (pathLengthConstraint < 0)
				{
					throw new ArgumentOutOfRangeException("pathLengthConstraint", SR.GetString("Arg_OutOfRange_NeedNonNegNum"));
				}
				cert_BASIC_CONSTRAINTS2_INFO.dwPathLenConstraint = (uint)pathLengthConstraint;
			}
			byte[] result = null;
			if (!CAPI.EncodeObject("2.5.29.19", new IntPtr((void*)(&cert_BASIC_CONSTRAINTS2_INFO)), out result))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return result;
		}

		// Token: 0x0400263B RID: 9787
		private bool m_isCA;

		// Token: 0x0400263C RID: 9788
		private bool m_hasPathLenConstraint;

		// Token: 0x0400263D RID: 9789
		private int m_pathLenConstraint;

		// Token: 0x0400263E RID: 9790
		private bool m_decoded;
	}
}
