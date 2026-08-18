using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008D5 RID: 2261
	[SuppressUnmanagedCodeSecurity]
	internal static class Pbkdf2
	{
		// Token: 0x06005270 RID: 21104 RVA: 0x001288E0 File Offset: 0x001278E0
		static Pbkdf2()
		{
			int num = Pbkdf2.BCryptOpenAlgorithmProvider(out Pbkdf2._sha1, "SHA1", "Microsoft Primitive Provider", OpenAlgorithmProviderFlags.BCRYPT_ALG_HANDLE_HMAC_FLAG);
			if (num != 0)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "A provider could not be found for algorithm '{0}'.", new object[]
				{
					"SHA1"
				}));
			}
			num = Pbkdf2.BCryptOpenAlgorithmProvider(out Pbkdf2._sha256, "SHA256", "Microsoft Primitive Provider", OpenAlgorithmProviderFlags.BCRYPT_ALG_HANDLE_HMAC_FLAG);
			if (num != 0)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "A provider could not be found for algorithm '{0}'.", new object[]
				{
					"SHA256"
				}));
			}
			num = Pbkdf2.BCryptOpenAlgorithmProvider(out Pbkdf2._sha384, "SHA384", "Microsoft Primitive Provider", OpenAlgorithmProviderFlags.BCRYPT_ALG_HANDLE_HMAC_FLAG);
			if (num != 0)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "A provider could not be found for algorithm '{0}'.", new object[]
				{
					"SHA384"
				}));
			}
			num = Pbkdf2.BCryptOpenAlgorithmProvider(out Pbkdf2._sha512, "SHA512", "Microsoft Primitive Provider", OpenAlgorithmProviderFlags.BCRYPT_ALG_HANDLE_HMAC_FLAG);
			if (num != 0)
			{
				throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "A provider could not be found for algorithm '{0}'.", new object[]
				{
					"SHA512"
				}));
			}
		}

		// Token: 0x06005271 RID: 21105
		[DllImport("bcrypt.dll")]
		private static extern int BCryptOpenAlgorithmProvider(out SafeBCryptAlgorithmHandle phAlgorithm, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszAlgId, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszImplementation, [In] OpenAlgorithmProviderFlags dwFlags);

		// Token: 0x06005272 RID: 21106 RVA: 0x001289E8 File Offset: 0x001279E8
		internal unsafe static byte[] Derive(string hashAlgorithm, byte[] password, byte[] salt, int iterations, int length)
		{
			if (length <= 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (iterations <= 0)
			{
				throw new ArgumentOutOfRangeException("iterations");
			}
			KdfWorkLimiter.RecordIterations(iterations);
			byte[] array = new byte[length];
			if (hashAlgorithm != null)
			{
				SafeBCryptAlgorithmHandle hPrf;
				if (!(hashAlgorithm == "SHA1"))
				{
					if (!(hashAlgorithm == "SHA256"))
					{
						if (!(hashAlgorithm == "SHA384"))
						{
							if (!(hashAlgorithm == "SHA512"))
							{
								goto IL_8D;
							}
							hPrf = Pbkdf2._sha512;
						}
						else
						{
							hPrf = Pbkdf2._sha384;
						}
					}
					else
					{
						hPrf = Pbkdf2._sha256;
					}
				}
				else
				{
					hPrf = Pbkdf2._sha1;
				}
				fixed (byte* ptr = password)
				{
					fixed (byte* ptr2 = salt)
					{
						fixed (byte* ptr3 = array)
						{
							byte b = 0;
							int num = Pbkdf2.BCryptDeriveKeyPBKDF2(hPrf, (ptr != null) ? ptr : (&b), password.Length, (ptr2 != null) ? ptr2 : (&b), salt.Length, (ulong)((long)iterations), ptr3, array.Length, 0U);
							if (num != 0)
							{
								throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "A call to BCryptDeriveKeyPBKDF2 failed with code '{0}'.", new object[]
								{
									num
								}));
							}
						}
					}
				}
				return array;
			}
			IL_8D:
			throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "'{0}' is not a known hash algorithm.", new object[]
			{
				hashAlgorithm
			}));
		}

		// Token: 0x06005273 RID: 21107
		[DllImport("bcrypt.dll")]
		internal unsafe static extern int BCryptDeriveKeyPBKDF2(SafeBCryptAlgorithmHandle hPrf, byte* pbPassword, int cbPassword, byte* pbSalt, int cbSalt, ulong cIterations, byte* pbDerivedKey, int cbDerivedKey, uint dwFlags);

		// Token: 0x04002A77 RID: 10871
		internal const string BCRYPT_LIB = "bcrypt.dll";

		// Token: 0x04002A78 RID: 10872
		private const string MS_PRIMITIVE_PROVIDER = "Microsoft Primitive Provider";

		// Token: 0x04002A79 RID: 10873
		private const int NtStatusSuccess = 0;

		// Token: 0x04002A7A RID: 10874
		internal static readonly SafeBCryptAlgorithmHandle _sha1;

		// Token: 0x04002A7B RID: 10875
		internal static readonly SafeBCryptAlgorithmHandle _sha256;

		// Token: 0x04002A7C RID: 10876
		internal static readonly SafeBCryptAlgorithmHandle _sha384;

		// Token: 0x04002A7D RID: 10877
		internal static readonly SafeBCryptAlgorithmHandle _sha512;
	}
}
