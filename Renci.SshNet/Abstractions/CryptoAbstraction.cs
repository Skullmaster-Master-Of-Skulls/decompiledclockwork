using System;
using System.Security.Cryptography;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet.Abstractions
{
	// Token: 0x02000114 RID: 276
	internal static class CryptoAbstraction
	{
		// Token: 0x06000BF9 RID: 3065 RVA: 0x00027101 File Offset: 0x00025301
		public static void GenerateRandom(byte[] data)
		{
			CryptoAbstraction.Randomizer.GetBytes(data);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002710E File Offset: 0x0002530E
		public static RandomNumberGenerator CreateRandomNumberGenerator()
		{
			return RandomNumberGenerator.Create();
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00027115 File Offset: 0x00025315
		public static MD5 CreateMD5()
		{
			return MD5.Create();
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002711C File Offset: 0x0002531C
		public static SHA1 CreateSHA1()
		{
			return SHA1.Create();
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00027123 File Offset: 0x00025323
		public static SHA256 CreateSHA256()
		{
			return SHA256.Create();
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002712A File Offset: 0x0002532A
		public static SHA384 CreateSHA384()
		{
			return SHA384.Create();
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00027131 File Offset: 0x00025331
		public static SHA512 CreateSHA512()
		{
			return SHA512.Create();
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00027138 File Offset: 0x00025338
		public static RIPEMD160 CreateRIPEMD160()
		{
			return RIPEMD160.Create();
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002713F File Offset: 0x0002533F
		public static System.Security.Cryptography.HMACMD5 CreateHMACMD5(byte[] key)
		{
			return new System.Security.Cryptography.HMACMD5(key);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00027147 File Offset: 0x00025347
		public static Renci.SshNet.Security.Cryptography.HMACMD5 CreateHMACMD5(byte[] key, int hashSize)
		{
			return new Renci.SshNet.Security.Cryptography.HMACMD5(key, hashSize);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00027150 File Offset: 0x00025350
		public static System.Security.Cryptography.HMACSHA1 CreateHMACSHA1(byte[] key)
		{
			return new System.Security.Cryptography.HMACSHA1(key);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00027158 File Offset: 0x00025358
		public static Renci.SshNet.Security.Cryptography.HMACSHA1 CreateHMACSHA1(byte[] key, int hashSize)
		{
			return new Renci.SshNet.Security.Cryptography.HMACSHA1(key, hashSize);
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00027161 File Offset: 0x00025361
		public static System.Security.Cryptography.HMACSHA256 CreateHMACSHA256(byte[] key)
		{
			return new System.Security.Cryptography.HMACSHA256(key);
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00027169 File Offset: 0x00025369
		public static Renci.SshNet.Security.Cryptography.HMACSHA256 CreateHMACSHA256(byte[] key, int hashSize)
		{
			return new Renci.SshNet.Security.Cryptography.HMACSHA256(key, hashSize);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00027172 File Offset: 0x00025372
		public static System.Security.Cryptography.HMACSHA384 CreateHMACSHA384(byte[] key)
		{
			return new System.Security.Cryptography.HMACSHA384(key);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0002717A File Offset: 0x0002537A
		public static Renci.SshNet.Security.Cryptography.HMACSHA384 CreateHMACSHA384(byte[] key, int hashSize)
		{
			return new Renci.SshNet.Security.Cryptography.HMACSHA384(key, hashSize);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00027183 File Offset: 0x00025383
		public static System.Security.Cryptography.HMACSHA512 CreateHMACSHA512(byte[] key)
		{
			return new System.Security.Cryptography.HMACSHA512(key);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002718B File Offset: 0x0002538B
		public static Renci.SshNet.Security.Cryptography.HMACSHA512 CreateHMACSHA512(byte[] key, int hashSize)
		{
			return new Renci.SshNet.Security.Cryptography.HMACSHA512(key, hashSize);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00027194 File Offset: 0x00025394
		public static HMACRIPEMD160 CreateHMACRIPEMD160(byte[] key)
		{
			return new HMACRIPEMD160(key);
		}

		// Token: 0x0400047E RID: 1150
		private static readonly RandomNumberGenerator Randomizer = CryptoAbstraction.CreateRandomNumberGenerator();
	}
}
