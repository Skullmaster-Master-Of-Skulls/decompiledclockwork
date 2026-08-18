using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000E6 RID: 230
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngAlgorithm : IEquatable<CngAlgorithm>
	{
		// Token: 0x06000701 RID: 1793 RVA: 0x0001704C File Offset: 0x0001524C
		public CngAlgorithm(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (algorithm.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidAlgorithmName", new object[]
				{
					algorithm
				}), "algorithm");
			}
			this.m_algorithm = algorithm;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001709B File Offset: 0x0001529B
		public string Algorithm
		{
			get
			{
				return this.m_algorithm;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x000170A3 File Offset: 0x000152A3
		public static bool operator ==(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000170B4 File Offset: 0x000152B4
		public static bool operator !=(CngAlgorithm left, CngAlgorithm right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000170C8 File Offset: 0x000152C8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithm);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000170D6 File Offset: 0x000152D6
		public bool Equals(CngAlgorithm other)
		{
			return other != null && this.m_algorithm.Equals(other.Algorithm);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x000170EE File Offset: 0x000152EE
		public override int GetHashCode()
		{
			return this.m_algorithm.GetHashCode();
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x000170FB File Offset: 0x000152FB
		public override string ToString()
		{
			return this.m_algorithm;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00017103 File Offset: 0x00015303
		public static CngAlgorithm Rsa
		{
			get
			{
				if (CngAlgorithm.s_rsa == null)
				{
					CngAlgorithm.s_rsa = new CngAlgorithm("RSA");
				}
				return CngAlgorithm.s_rsa;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001712C File Offset: 0x0001532C
		public static CngAlgorithm ECDiffieHellman
		{
			get
			{
				if (CngAlgorithm.s_ecdh == null)
				{
					CngAlgorithm.s_ecdh = new CngAlgorithm("ECDH");
				}
				return CngAlgorithm.s_ecdh;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00017155 File Offset: 0x00015355
		public static CngAlgorithm ECDiffieHellmanP256
		{
			get
			{
				if (CngAlgorithm.s_ecdhp256 == null)
				{
					CngAlgorithm.s_ecdhp256 = new CngAlgorithm("ECDH_P256");
				}
				return CngAlgorithm.s_ecdhp256;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001717E File Offset: 0x0001537E
		public static CngAlgorithm ECDiffieHellmanP384
		{
			get
			{
				if (CngAlgorithm.s_ecdhp384 == null)
				{
					CngAlgorithm.s_ecdhp384 = new CngAlgorithm("ECDH_P384");
				}
				return CngAlgorithm.s_ecdhp384;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x000171A7 File Offset: 0x000153A7
		public static CngAlgorithm ECDiffieHellmanP521
		{
			get
			{
				if (CngAlgorithm.s_ecdhp521 == null)
				{
					CngAlgorithm.s_ecdhp521 = new CngAlgorithm("ECDH_P521");
				}
				return CngAlgorithm.s_ecdhp521;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x000171D0 File Offset: 0x000153D0
		public static CngAlgorithm ECDsa
		{
			get
			{
				if (CngAlgorithm.s_ecdsa == null)
				{
					CngAlgorithm.s_ecdsa = new CngAlgorithm("ECDSA");
				}
				return CngAlgorithm.s_ecdsa;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x000171F9 File Offset: 0x000153F9
		public static CngAlgorithm ECDsaP256
		{
			get
			{
				if (CngAlgorithm.s_ecdsap256 == null)
				{
					CngAlgorithm.s_ecdsap256 = new CngAlgorithm("ECDSA_P256");
				}
				return CngAlgorithm.s_ecdsap256;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00017222 File Offset: 0x00015422
		public static CngAlgorithm ECDsaP384
		{
			get
			{
				if (CngAlgorithm.s_ecdsap384 == null)
				{
					CngAlgorithm.s_ecdsap384 = new CngAlgorithm("ECDSA_P384");
				}
				return CngAlgorithm.s_ecdsap384;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001724B File Offset: 0x0001544B
		public static CngAlgorithm ECDsaP521
		{
			get
			{
				if (CngAlgorithm.s_ecdsap521 == null)
				{
					CngAlgorithm.s_ecdsap521 = new CngAlgorithm("ECDSA_P521");
				}
				return CngAlgorithm.s_ecdsap521;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00017274 File Offset: 0x00015474
		public static CngAlgorithm MD5
		{
			get
			{
				if (CngAlgorithm.s_md5 == null)
				{
					CngAlgorithm.s_md5 = new CngAlgorithm("MD5");
				}
				return CngAlgorithm.s_md5;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0001729D File Offset: 0x0001549D
		public static CngAlgorithm Sha1
		{
			get
			{
				if (CngAlgorithm.s_sha1 == null)
				{
					CngAlgorithm.s_sha1 = new CngAlgorithm("SHA1");
				}
				return CngAlgorithm.s_sha1;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x000172C6 File Offset: 0x000154C6
		public static CngAlgorithm Sha256
		{
			get
			{
				if (CngAlgorithm.s_sha256 == null)
				{
					CngAlgorithm.s_sha256 = new CngAlgorithm("SHA256");
				}
				return CngAlgorithm.s_sha256;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x000172EF File Offset: 0x000154EF
		public static CngAlgorithm Sha384
		{
			get
			{
				if (CngAlgorithm.s_sha384 == null)
				{
					CngAlgorithm.s_sha384 = new CngAlgorithm("SHA384");
				}
				return CngAlgorithm.s_sha384;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00017318 File Offset: 0x00015518
		public static CngAlgorithm Sha512
		{
			get
			{
				if (CngAlgorithm.s_sha512 == null)
				{
					CngAlgorithm.s_sha512 = new CngAlgorithm("SHA512");
				}
				return CngAlgorithm.s_sha512;
			}
		}

		// Token: 0x040005F9 RID: 1529
		private static volatile CngAlgorithm s_ecdh;

		// Token: 0x040005FA RID: 1530
		private static volatile CngAlgorithm s_ecdhp256;

		// Token: 0x040005FB RID: 1531
		private static volatile CngAlgorithm s_ecdhp384;

		// Token: 0x040005FC RID: 1532
		private static volatile CngAlgorithm s_ecdhp521;

		// Token: 0x040005FD RID: 1533
		private static volatile CngAlgorithm s_ecdsa;

		// Token: 0x040005FE RID: 1534
		private static volatile CngAlgorithm s_ecdsap256;

		// Token: 0x040005FF RID: 1535
		private static volatile CngAlgorithm s_ecdsap384;

		// Token: 0x04000600 RID: 1536
		private static volatile CngAlgorithm s_ecdsap521;

		// Token: 0x04000601 RID: 1537
		private static volatile CngAlgorithm s_md5;

		// Token: 0x04000602 RID: 1538
		private static volatile CngAlgorithm s_sha1;

		// Token: 0x04000603 RID: 1539
		private static volatile CngAlgorithm s_sha256;

		// Token: 0x04000604 RID: 1540
		private static volatile CngAlgorithm s_sha384;

		// Token: 0x04000605 RID: 1541
		private static volatile CngAlgorithm s_sha512;

		// Token: 0x04000606 RID: 1542
		private static volatile CngAlgorithm s_rsa;

		// Token: 0x04000607 RID: 1543
		private string m_algorithm;
	}
}
