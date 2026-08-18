using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000EA RID: 234
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngKeyBlobFormat : IEquatable<CngKeyBlobFormat>
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x000182D8 File Offset: 0x000164D8
		public CngKeyBlobFormat(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidKeyBlobFormat", new object[]
				{
					format
				}), "format");
			}
			this.m_format = format;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00018327 File Offset: 0x00016527
		public string Format
		{
			get
			{
				return this.m_format;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001832F File Offset: 0x0001652F
		public static bool operator ==(CngKeyBlobFormat left, CngKeyBlobFormat right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00018340 File Offset: 0x00016540
		public static bool operator !=(CngKeyBlobFormat left, CngKeyBlobFormat right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00018354 File Offset: 0x00016554
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngKeyBlobFormat);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00018362 File Offset: 0x00016562
		public bool Equals(CngKeyBlobFormat other)
		{
			return other != null && this.m_format.Equals(other.Format);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001837A File Offset: 0x0001657A
		public override int GetHashCode()
		{
			return this.m_format.GetHashCode();
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00018387 File Offset: 0x00016587
		public override string ToString()
		{
			return this.m_format;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x0001838F File Offset: 0x0001658F
		public static CngKeyBlobFormat EccPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccPrivate == null)
				{
					CngKeyBlobFormat.s_eccPrivate = new CngKeyBlobFormat("ECCPRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_eccPrivate;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x000183B8 File Offset: 0x000165B8
		public static CngKeyBlobFormat EccPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccPublic == null)
				{
					CngKeyBlobFormat.s_eccPublic = new CngKeyBlobFormat("ECCPUBLICBLOB");
				}
				return CngKeyBlobFormat.s_eccPublic;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x000183E1 File Offset: 0x000165E1
		public static CngKeyBlobFormat EccFullPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccFullPrivate == null)
				{
					CngKeyBlobFormat.s_eccFullPrivate = new CngKeyBlobFormat("ECCFULLPRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_eccFullPrivate;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0001840A File Offset: 0x0001660A
		public static CngKeyBlobFormat EccFullPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccFullPublic == null)
				{
					CngKeyBlobFormat.s_eccFullPublic = new CngKeyBlobFormat("ECCFULLPUBLICBLOB");
				}
				return CngKeyBlobFormat.s_eccFullPublic;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00018433 File Offset: 0x00016633
		public static CngKeyBlobFormat GenericPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_genericPrivate == null)
				{
					CngKeyBlobFormat.s_genericPrivate = new CngKeyBlobFormat("PRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_genericPrivate;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001845C File Offset: 0x0001665C
		public static CngKeyBlobFormat GenericPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_genericPublic == null)
				{
					CngKeyBlobFormat.s_genericPublic = new CngKeyBlobFormat("PUBLICBLOB");
				}
				return CngKeyBlobFormat.s_genericPublic;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00018485 File Offset: 0x00016685
		public static CngKeyBlobFormat OpaqueTransportBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_opaqueTransport == null)
				{
					CngKeyBlobFormat.s_opaqueTransport = new CngKeyBlobFormat("OpaqueTransport");
				}
				return CngKeyBlobFormat.s_opaqueTransport;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x000184AE File Offset: 0x000166AE
		public static CngKeyBlobFormat Pkcs8PrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_pkcs8Private == null)
				{
					CngKeyBlobFormat.s_pkcs8Private = new CngKeyBlobFormat("PKCS8_PRIVATEKEY");
				}
				return CngKeyBlobFormat.s_pkcs8Private;
			}
		}

		// Token: 0x04000613 RID: 1555
		private static volatile CngKeyBlobFormat s_eccPrivate;

		// Token: 0x04000614 RID: 1556
		private static volatile CngKeyBlobFormat s_eccPublic;

		// Token: 0x04000615 RID: 1557
		private static volatile CngKeyBlobFormat s_eccFullPrivate;

		// Token: 0x04000616 RID: 1558
		private static volatile CngKeyBlobFormat s_eccFullPublic;

		// Token: 0x04000617 RID: 1559
		private static volatile CngKeyBlobFormat s_genericPrivate;

		// Token: 0x04000618 RID: 1560
		private static volatile CngKeyBlobFormat s_genericPublic;

		// Token: 0x04000619 RID: 1561
		private static volatile CngKeyBlobFormat s_opaqueTransport;

		// Token: 0x0400061A RID: 1562
		private static volatile CngKeyBlobFormat s_pkcs8Private;

		// Token: 0x0400061B RID: 1563
		private string m_format;
	}
}
