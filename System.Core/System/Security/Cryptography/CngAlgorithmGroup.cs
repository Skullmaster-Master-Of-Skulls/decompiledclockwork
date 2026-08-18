using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x020000E7 RID: 231
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngAlgorithmGroup : IEquatable<CngAlgorithmGroup>
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x00017344 File Offset: 0x00015544
		public CngAlgorithmGroup(string algorithmGroup)
		{
			if (algorithmGroup == null)
			{
				throw new ArgumentNullException("algorithmGroup");
			}
			if (algorithmGroup.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Cryptography_InvalidAlgorithmGroup", new object[]
				{
					algorithmGroup
				}), "algorithmGroup");
			}
			this.m_algorithmGroup = algorithmGroup;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00017393 File Offset: 0x00015593
		public string AlgorithmGroup
		{
			get
			{
				return this.m_algorithmGroup;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001739B File Offset: 0x0001559B
		public static bool operator ==(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x000173AC File Offset: 0x000155AC
		public static bool operator !=(CngAlgorithmGroup left, CngAlgorithmGroup right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x000173C0 File Offset: 0x000155C0
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngAlgorithmGroup);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000173CE File Offset: 0x000155CE
		public bool Equals(CngAlgorithmGroup other)
		{
			return other != null && this.m_algorithmGroup.Equals(other.AlgorithmGroup);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x000173E6 File Offset: 0x000155E6
		public override int GetHashCode()
		{
			return this.m_algorithmGroup.GetHashCode();
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000173F3 File Offset: 0x000155F3
		public override string ToString()
		{
			return this.m_algorithmGroup;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x000173FB File Offset: 0x000155FB
		public static CngAlgorithmGroup DiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.s_dh == null)
				{
					CngAlgorithmGroup.s_dh = new CngAlgorithmGroup("DH");
				}
				return CngAlgorithmGroup.s_dh;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00017424 File Offset: 0x00015624
		public static CngAlgorithmGroup Dsa
		{
			get
			{
				if (CngAlgorithmGroup.s_dsa == null)
				{
					CngAlgorithmGroup.s_dsa = new CngAlgorithmGroup("DSA");
				}
				return CngAlgorithmGroup.s_dsa;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x0001744D File Offset: 0x0001564D
		public static CngAlgorithmGroup ECDiffieHellman
		{
			get
			{
				if (CngAlgorithmGroup.s_ecdh == null)
				{
					CngAlgorithmGroup.s_ecdh = new CngAlgorithmGroup("ECDH");
				}
				return CngAlgorithmGroup.s_ecdh;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00017476 File Offset: 0x00015676
		public static CngAlgorithmGroup ECDsa
		{
			get
			{
				if (CngAlgorithmGroup.s_ecdsa == null)
				{
					CngAlgorithmGroup.s_ecdsa = new CngAlgorithmGroup("ECDSA");
				}
				return CngAlgorithmGroup.s_ecdsa;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001749F File Offset: 0x0001569F
		public static CngAlgorithmGroup Rsa
		{
			get
			{
				if (CngAlgorithmGroup.s_rsa == null)
				{
					CngAlgorithmGroup.s_rsa = new CngAlgorithmGroup("RSA");
				}
				return CngAlgorithmGroup.s_rsa;
			}
		}

		// Token: 0x04000608 RID: 1544
		private static volatile CngAlgorithmGroup s_dh;

		// Token: 0x04000609 RID: 1545
		private static volatile CngAlgorithmGroup s_dsa;

		// Token: 0x0400060A RID: 1546
		private static volatile CngAlgorithmGroup s_ecdh;

		// Token: 0x0400060B RID: 1547
		private static volatile CngAlgorithmGroup s_ecdsa;

		// Token: 0x0400060C RID: 1548
		private static volatile CngAlgorithmGroup s_rsa;

		// Token: 0x0400060D RID: 1549
		private string m_algorithmGroup;
	}
}
