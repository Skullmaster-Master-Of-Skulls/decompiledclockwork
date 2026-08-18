using System;
using System.Security.Cryptography;

namespace System.Web.Security.Cryptography
{
	// Token: 0x020005FF RID: 1535
	internal static class CryptoAlgorithms
	{
		// Token: 0x06004D8F RID: 19855 RVA: 0x0010D72B File Offset: 0x0010B92B
		internal static Aes CreateAes()
		{
			return new AesCryptoServiceProvider();
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x0010D732 File Offset: 0x0010B932
		[Obsolete("DES is deprecated and MUST NOT be used by new features. Consider using AES instead.")]
		internal static DES CreateDES()
		{
			return new DESCryptoServiceProvider();
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x0010D739 File Offset: 0x0010B939
		internal static HMACSHA1 CreateHMACSHA1()
		{
			return new HMACSHA1();
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x0010D740 File Offset: 0x0010B940
		internal static HMACSHA256 CreateHMACSHA256()
		{
			return new HMACSHA256();
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x0010D747 File Offset: 0x0010B947
		internal static HMACSHA384 CreateHMACSHA384()
		{
			return new HMACSHA384();
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x0010D74E File Offset: 0x0010B94E
		internal static HMACSHA512 CreateHMACSHA512()
		{
			return new HMACSHA512();
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x0010D755 File Offset: 0x0010B955
		internal static HMACSHA512 CreateHMACSHA512(byte[] key)
		{
			return new HMACSHA512(key);
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x0010D75D File Offset: 0x0010B95D
		[Obsolete("MD5 is deprecated and MUST NOT be used by new features. Consider using a SHA-2 algorithm instead.")]
		internal static MD5 CreateMD5()
		{
			return new MD5Cng();
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x0010D764 File Offset: 0x0010B964
		[Obsolete("SHA1 is deprecated and MUST NOT be used by new features. Consider using a SHA-2 algorithm instead.")]
		internal static SHA1 CreateSHA1()
		{
			return new SHA1Cng();
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x0010D76B File Offset: 0x0010B96B
		internal static SHA256 CreateSHA256()
		{
			return new SHA256Cng();
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x0010D772 File Offset: 0x0010B972
		internal static SHA384 CreateSHA384()
		{
			return new SHA384Cng();
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x0010D779 File Offset: 0x0010B979
		internal static SHA512 CreateSHA512()
		{
			return new SHA512Cng();
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x0010D780 File Offset: 0x0010B980
		[Obsolete("3DES is deprecated and MUST NOT be used by new features. Consider using AES instead.")]
		internal static TripleDES CreateTripleDES()
		{
			return new TripleDESCryptoServiceProvider();
		}
	}
}
