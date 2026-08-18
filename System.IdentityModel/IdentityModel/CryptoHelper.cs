using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.ServiceModel.Security;

namespace System.IdentityModel
{
	// Token: 0x02000031 RID: 49
	internal static class CryptoHelper
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00006380 File Offset: 0x00004580
		public static int CeilingDivide(int dividend, int divisor)
		{
			int num = dividend % divisor;
			int num2 = dividend / divisor;
			if (num > 0)
			{
				num2++;
			}
			return num2;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000063A0 File Offset: 0x000045A0
		internal static byte[] EmptyBuffer
		{
			get
			{
				if (CryptoHelper.emptyBuffer == null)
				{
					byte[] array = new byte[0];
					CryptoHelper.emptyBuffer = array;
				}
				return CryptoHelper.emptyBuffer;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000063C8 File Offset: 0x000045C8
		internal static Rijndael Rijndael
		{
			get
			{
				if (CryptoHelper.rijndael == null)
				{
					Rijndael rijndael = SecurityUtils.RequiresFipsCompliance ? new RijndaelCryptoServiceProvider() : new RijndaelManaged();
					rijndael.Padding = PaddingMode.ISO10126;
					CryptoHelper.rijndael = rijndael;
				}
				return CryptoHelper.rijndael;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00006404 File Offset: 0x00004604
		internal static TripleDES TripleDES
		{
			get
			{
				if (CryptoHelper.tripleDES == null)
				{
					CryptoHelper.tripleDES = new TripleDESCryptoServiceProvider
					{
						Padding = PaddingMode.ISO10126
					};
				}
				return CryptoHelper.tripleDES;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006430 File Offset: 0x00004630
		internal static RandomNumberGenerator RandomNumberGenerator
		{
			get
			{
				if (CryptoHelper.random == null)
				{
					CryptoHelper.random = new RNGCryptoServiceProvider();
				}
				return CryptoHelper.random;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006448 File Offset: 0x00004648
		internal static SymmetricAlgorithm NewDefaultEncryption()
		{
			return CryptoHelper.GetSymmetricAlgorithm(null, "http://www.w3.org/2001/04/xmlenc#aes256-cbc");
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006455 File Offset: 0x00004655
		internal static HashAlgorithm NewSha1HashAlgorithm()
		{
			return CryptoHelper.CreateHashAlgorithm("http://www.w3.org/2000/09/xmldsig#sha1");
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006461 File Offset: 0x00004661
		internal static HashAlgorithm NewSha256HashAlgorithm()
		{
			return CryptoHelper.CreateHashAlgorithm("http://www.w3.org/2001/04/xmlenc#sha256");
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006470 File Offset: 0x00004670
		internal static KeyedHashAlgorithm NewHmacSha1KeyedHashAlgorithm()
		{
			KeyedHashAlgorithm keyedHashAlgorithm = CryptoHelper.GetAlgorithmFromConfig("http://www.w3.org/2000/09/xmldsig#hmac-sha1") as KeyedHashAlgorithm;
			if (keyedHashAlgorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("algorithm", SR.GetString("ID6037", new object[]
				{
					"http://www.w3.org/2000/09/xmldsig#hmac-sha1"
				}));
			}
			return keyedHashAlgorithm;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000064B9 File Offset: 0x000046B9
		internal static KeyedHashAlgorithm NewHmacSha1KeyedHashAlgorithm(byte[] key)
		{
			return CryptoHelper.CreateKeyedHashAlgorithm(key, "http://www.w3.org/2000/09/xmldsig#hmac-sha1");
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000064C6 File Offset: 0x000046C6
		internal static KeyedHashAlgorithm NewHmacSha256KeyedHashAlgorithm(byte[] key)
		{
			return CryptoHelper.CreateKeyedHashAlgorithm(key, "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256");
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000064D4 File Offset: 0x000046D4
		internal static Rijndael NewRijndaelSymmetricAlgorithm()
		{
			Rijndael rijndael = CryptoHelper.GetSymmetricAlgorithm(null, "http://www.w3.org/2001/04/xmlenc#aes128-cbc") as Rijndael;
			if (rijndael != null)
			{
				return rijndael;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CustomCryptoAlgorithmIsNotValidSymmetricAlgorithm", new object[]
			{
				"http://www.w3.org/2001/04/xmlenc#aes128-cbc"
			})));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006520 File Offset: 0x00004720
		internal static ICryptoTransform CreateDecryptor(byte[] key, byte[] iv, string algorithm)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				SymmetricAlgorithm symmetricAlgorithm = algorithmFromConfig as SymmetricAlgorithm;
				if (symmetricAlgorithm != null)
				{
					return symmetricAlgorithm.CreateDecryptor(key, iv);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CustomCryptoAlgorithmIsNotValidSymmetricAlgorithm", new object[]
				{
					algorithm
				})));
			}
			else
			{
				if (algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc")
				{
					return CryptoHelper.TripleDES.CreateDecryptor(key, iv);
				}
				if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedEncryptionAlgorithm", new object[]
					{
						algorithm
					})));
				}
				return CryptoHelper.Rijndael.CreateDecryptor(key, iv);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000065E4 File Offset: 0x000047E4
		internal static ICryptoTransform CreateEncryptor(byte[] key, byte[] iv, string algorithm)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				SymmetricAlgorithm symmetricAlgorithm = algorithmFromConfig as SymmetricAlgorithm;
				if (symmetricAlgorithm != null)
				{
					return symmetricAlgorithm.CreateEncryptor(key, iv);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CustomCryptoAlgorithmIsNotValidSymmetricAlgorithm", new object[]
				{
					algorithm
				})));
			}
			else
			{
				if (algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc")
				{
					return CryptoHelper.TripleDES.CreateEncryptor(key, iv);
				}
				if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedEncryptionAlgorithm", new object[]
					{
						algorithm
					})));
				}
				return CryptoHelper.Rijndael.CreateEncryptor(key, iv);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000066A8 File Offset: 0x000048A8
		internal static HashAlgorithm CreateHashAlgorithm(string algorithm)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				HashAlgorithm hashAlgorithm = algorithmFromConfig as HashAlgorithm;
				if (hashAlgorithm != null)
				{
					return hashAlgorithm;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CustomCryptoAlgorithmIsNotValidHashAlgorithm", new object[]
				{
					algorithm
				})));
			}
			else if (!(algorithm == "SHA") && !(algorithm == "SHA1") && !(algorithm == "System.Security.Cryptography.SHA1") && !(algorithm == "http://www.w3.org/2000/09/xmldsig#sha1"))
			{
				if (!(algorithm == "SHA256") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#sha256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						algorithm
					})));
				}
				if (SecurityUtils.RequiresFipsCompliance)
				{
					return new SHA256CryptoServiceProvider();
				}
				return new SHA256Managed();
			}
			else
			{
				if (SecurityUtils.RequiresFipsCompliance)
				{
					return new SHA1CryptoServiceProvider();
				}
				return new SHA1Managed();
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006788 File Offset: 0x00004988
		internal static KeyedHashAlgorithm CreateKeyedHashAlgorithm(byte[] key, string algorithm)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				KeyedHashAlgorithm keyedHashAlgorithm = algorithmFromConfig as KeyedHashAlgorithm;
				if (keyedHashAlgorithm != null)
				{
					keyedHashAlgorithm.Key = key;
					return keyedHashAlgorithm;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CustomCryptoAlgorithmIsNotValidKeyedHashAlgorithm", new object[]
				{
					algorithm
				})));
			}
			else
			{
				if (algorithm == "http://www.w3.org/2000/09/xmldsig#hmac-sha1")
				{
					return new HMACSHA1(key, !SecurityUtils.RequiresFipsCompliance);
				}
				if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						algorithm
					})));
				}
				if (!SecurityUtils.RequiresFipsCompliance)
				{
					return new HMACSHA256(key);
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CryptoAlgorithmIsNotFipsCompliant", new object[]
				{
					algorithm
				})));
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006858 File Offset: 0x00004A58
		internal static byte[] ComputeHash(byte[] buffer)
		{
			byte[] result;
			using (HashAlgorithm hashAlgorithm = CryptoHelper.NewSha1HashAlgorithm())
			{
				result = hashAlgorithm.ComputeHash(buffer);
			}
			return result;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006890 File Offset: 0x00004A90
		internal static byte[] GenerateDerivedKey(byte[] key, string algorithm, byte[] label, byte[] nonce, int derivedKeySize, int position)
		{
			if (algorithm != "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1" && algorithm != "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1")
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedKeyDerivationAlgorithm", new object[]
				{
					algorithm
				})));
			}
			return new Psha1DerivedKeyGenerator(key).GenerateDerivedKey(label, nonce, derivedKeySize, position);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000068EC File Offset: 0x00004AEC
		internal static int GetIVSize(string algorithm)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			if (algorithmFromConfig != null)
			{
				SymmetricAlgorithm symmetricAlgorithm = algorithmFromConfig as SymmetricAlgorithm;
				if (symmetricAlgorithm != null)
				{
					return symmetricAlgorithm.BlockSize;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CustomCryptoAlgorithmIsNotValidSymmetricAlgorithm", new object[]
				{
					algorithm
				})));
			}
			else
			{
				if (algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc")
				{
					return CryptoHelper.TripleDES.BlockSize;
				}
				if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedEncryptionAlgorithm", new object[]
					{
						algorithm
					})));
				}
				return CryptoHelper.Rijndael.BlockSize;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000069A7 File Offset: 0x00004BA7
		internal static void FillRandomBytes(byte[] buffer)
		{
			CryptoHelper.RandomNumberGenerator.GetBytes(buffer);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000069B4 File Offset: 0x00004BB4
		public static void GenerateRandomBytes(byte[] data)
		{
			CryptoHelper.RandomNumberGenerator.GetNonZeroBytes(data);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000069C4 File Offset: 0x00004BC4
		public static byte[] GenerateRandomBytes(int sizeInBits)
		{
			int num = sizeInBits / 8;
			if (sizeInBits <= 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("sizeInBits", SR.GetString("ID6033", new object[]
				{
					sizeInBits
				})));
			}
			if (num * 8 != sizeInBits)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID6002", new object[]
				{
					sizeInBits
				}), "sizeInBits"));
			}
			byte[] array = new byte[num];
			CryptoHelper.GenerateRandomBytes(array);
			return array;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006A4C File Offset: 0x00004C4C
		internal static SymmetricAlgorithm GetSymmetricAlgorithm(byte[] key, string algorithm)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			SymmetricAlgorithm symmetricAlgorithm;
			if (algorithmFromConfig == null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(algorithm);
				if (num <= 2323908233U)
				{
					if (num <= 811041755U)
					{
						if (num != 712490267U)
						{
							if (num != 811041755U)
							{
								goto IL_156;
							}
							if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc"))
							{
								goto IL_156;
							}
							goto IL_140;
						}
						else
						{
							if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
							{
								goto IL_156;
							}
							goto IL_140;
						}
					}
					else if (num != 1735592375U)
					{
						if (num != 2323908233U)
						{
							goto IL_156;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes256"))
						{
							goto IL_156;
						}
						goto IL_140;
					}
					else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
					{
						goto IL_156;
					}
				}
				else if (num <= 2888462845U)
				{
					if (num != 2551777632U)
					{
						if (num != 2888462845U)
						{
							goto IL_156;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes128"))
						{
							goto IL_156;
						}
						goto IL_140;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes192"))
						{
							goto IL_156;
						}
						goto IL_140;
					}
				}
				else if (num != 3225656034U)
				{
					if (num != 3487232831U)
					{
						goto IL_156;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes"))
					{
						goto IL_156;
					}
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc"))
					{
						goto IL_156;
					}
					goto IL_140;
				}
				symmetricAlgorithm = new TripleDESCryptoServiceProvider();
				goto IL_17A;
				IL_140:
				symmetricAlgorithm = (SecurityUtils.RequiresFipsCompliance ? new RijndaelCryptoServiceProvider() : new RijndaelManaged());
				goto IL_17A;
				IL_156:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedEncryptionAlgorithm", new object[]
				{
					algorithm
				})));
				IL_17A:
				if (key != null)
				{
					symmetricAlgorithm.Key = key;
				}
				return symmetricAlgorithm;
			}
			symmetricAlgorithm = (algorithmFromConfig as SymmetricAlgorithm);
			if (symmetricAlgorithm != null)
			{
				if (key != null)
				{
					symmetricAlgorithm.Key = key;
				}
				return symmetricAlgorithm;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("CustomCryptoAlgorithmIsNotValidSymmetricAlgorithm", new object[]
			{
				algorithm
			})));
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006BDE File Offset: 0x00004DDE
		internal static byte[] CreateSignatureForSha256(AsymmetricSignatureFormatter formatter, HashAlgorithm hash)
		{
			if (SecurityUtils.RequiresFipsCompliance)
			{
				formatter.SetHashAlgorithm("SHA256");
				return formatter.CreateSignature(hash.Hash);
			}
			return formatter.CreateSignature(hash);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006C06 File Offset: 0x00004E06
		internal static bool VerifySignatureForSha256(AsymmetricSignatureDeformatter deformatter, HashAlgorithm hash, byte[] signatureValue)
		{
			if (SecurityUtils.RequiresFipsCompliance)
			{
				deformatter.SetHashAlgorithm("SHA256");
				return deformatter.VerifySignature(hash.Hash, signatureValue);
			}
			return deformatter.VerifySignature(hash, signatureValue);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006C30 File Offset: 0x00004E30
		internal static AsymmetricSignatureFormatter GetSignatureFormatterForSha256(AsymmetricSecurityKey key)
		{
			AsymmetricAlgorithm asymmetricAlgorithm = key.GetAsymmetricAlgorithm("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", true);
			RSACryptoServiceProvider rsacryptoServiceProvider = asymmetricAlgorithm as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return CryptoHelper.GetSignatureFormatterForSha256(rsacryptoServiceProvider);
			}
			return new RSAPKCS1SignatureFormatter(asymmetricAlgorithm);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006C64 File Offset: 0x00004E64
		internal static AsymmetricSignatureFormatter GetSignatureFormatterForSha256(RSACryptoServiceProvider rsaProvider)
		{
			CspParameters cspParameters = new CspParameters();
			cspParameters.ProviderType = 24;
			if (24 == rsaProvider.CspKeyContainerInfo.ProviderType)
			{
				cspParameters.ProviderName = rsaProvider.CspKeyContainerInfo.ProviderName;
			}
			cspParameters.KeyContainerName = rsaProvider.CspKeyContainerInfo.KeyContainerName;
			cspParameters.KeyNumber = (int)rsaProvider.CspKeyContainerInfo.KeyNumber;
			if (rsaProvider.CspKeyContainerInfo.MachineKeyStore)
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			cspParameters.Flags |= CspProviderFlags.UseExistingKey;
			rsaProvider = new RSACryptoServiceProvider(cspParameters);
			return new RSAPKCS1SignatureFormatter(rsaProvider);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006CF8 File Offset: 0x00004EF8
		internal static AsymmetricSignatureDeformatter GetSignatureDeFormatterForSha256(AsymmetricSecurityKey key)
		{
			AsymmetricAlgorithm asymmetricAlgorithm = key.GetAsymmetricAlgorithm("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256", false);
			RSACryptoServiceProvider rsacryptoServiceProvider = asymmetricAlgorithm as RSACryptoServiceProvider;
			if (rsacryptoServiceProvider != null)
			{
				return CryptoHelper.GetSignatureDeFormatterForSha256(rsacryptoServiceProvider);
			}
			return new RSAPKCS1SignatureDeformatter(asymmetricAlgorithm);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006D2C File Offset: 0x00004F2C
		internal static AsymmetricSignatureDeformatter GetSignatureDeFormatterForSha256(RSACryptoServiceProvider rsaProvider)
		{
			CspParameters cspParameters = new CspParameters();
			cspParameters.ProviderType = 24;
			if (24 == rsaProvider.CspKeyContainerInfo.ProviderType)
			{
				cspParameters.ProviderName = rsaProvider.CspKeyContainerInfo.ProviderName;
			}
			cspParameters.KeyNumber = (int)rsaProvider.CspKeyContainerInfo.KeyNumber;
			if (rsaProvider.CspKeyContainerInfo.MachineKeyStore)
			{
				cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
			}
			cspParameters.Flags |= CspProviderFlags.UseExistingKey;
			RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(cspParameters);
			rsacryptoServiceProvider.ImportCspBlob(rsaProvider.ExportCspBlob(false));
			return new RSAPKCS1SignatureDeformatter(rsacryptoServiceProvider);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00006DB8 File Offset: 0x00004FB8
		internal static bool IsAsymmetricAlgorithm(string algorithm)
		{
			object obj = null;
			try
			{
				obj = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			}
			catch (InvalidOperationException)
			{
				obj = null;
			}
			if (obj != null)
			{
				AsymmetricAlgorithm asymmetricAlgorithm = obj as AsymmetricAlgorithm;
				SignatureDescription signatureDescription = obj as SignatureDescription;
				return asymmetricAlgorithm != null || signatureDescription != null;
			}
			return algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1" || algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1" || algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256" || algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p" || algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006E44 File Offset: 0x00005044
		internal static bool IsSymmetricAlgorithm(string algorithm)
		{
			object obj = null;
			try
			{
				obj = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			}
			catch (InvalidOperationException)
			{
				obj = null;
			}
			if (obj != null)
			{
				SymmetricAlgorithm symmetricAlgorithm = obj as SymmetricAlgorithm;
				KeyedHashAlgorithm keyedHashAlgorithm = obj as KeyedHashAlgorithm;
				return symmetricAlgorithm != null || keyedHashAlgorithm != null;
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(algorithm);
			if (num <= 1318943838U)
			{
				if (num <= 636766351U)
				{
					if (num <= 550229268U)
					{
						if (num != 376408642U)
						{
							if (num != 550229268U)
							{
								return false;
							}
							if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
							{
								return false;
							}
							return true;
						}
						else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p"))
						{
							return false;
						}
					}
					else if (num != 600251407U)
					{
						if (num != 636766351U)
						{
							return false;
						}
						if (!(algorithm == "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1"))
						{
							return false;
						}
						return true;
					}
					else
					{
						if (!(algorithm == "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1"))
						{
							return false;
						}
						return true;
					}
				}
				else if (num <= 712490267U)
				{
					if (num != 699966473U)
					{
						if (num != 712490267U)
						{
							return false;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
						{
							return false;
						}
						return true;
					}
					else if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
					{
						return false;
					}
				}
				else if (num != 811041755U)
				{
					if (num != 877368883U)
					{
						if (num != 1318943838U)
						{
							return false;
						}
						if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#hmac-sha1"))
						{
							return false;
						}
						return true;
					}
					else if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1"))
					{
						return false;
					}
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc"))
					{
						return false;
					}
					return true;
				}
			}
			else if (num <= 2551777632U)
			{
				if (num <= 1735592375U)
				{
					if (num != 1611967855U)
					{
						if (num != 1735592375U)
						{
							return false;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
						{
							return false;
						}
						return true;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#des-cbc"))
						{
							return false;
						}
						return true;
					}
				}
				else if (num != 2323908233U)
				{
					if (num != 2551777632U)
					{
						return false;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes192"))
					{
						return false;
					}
					return true;
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes256"))
					{
						return false;
					}
					return true;
				}
			}
			else if (num <= 3225656034U)
			{
				if (num != 2888462845U)
				{
					if (num != 3225656034U)
					{
						return false;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc"))
					{
						return false;
					}
					return true;
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes128"))
					{
						return false;
					}
					return true;
				}
			}
			else if (num != 3487232831U)
			{
				if (num != 3654423024U)
				{
					if (num != 3880483293U)
					{
						return false;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
					{
						return false;
					}
				}
				else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5"))
				{
					return false;
				}
			}
			else
			{
				if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes"))
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007100 File Offset: 0x00005300
		internal static bool IsSymmetricSupportedAlgorithm(string algorithm, int keySize)
		{
			bool result = false;
			object obj = null;
			try
			{
				obj = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			}
			catch (InvalidOperationException)
			{
			}
			if (obj != null)
			{
				SymmetricAlgorithm symmetricAlgorithm = obj as SymmetricAlgorithm;
				KeyedHashAlgorithm keyedHashAlgorithm = obj as KeyedHashAlgorithm;
				if (symmetricAlgorithm != null || keyedHashAlgorithm != null)
				{
					result = true;
				}
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(algorithm);
			if (num > 877368883U)
			{
				if (num <= 2551777632U)
				{
					if (num <= 1735592375U)
					{
						if (num != 1318943838U)
						{
							if (num != 1735592375U)
							{
								return result;
							}
							if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
							{
								return result;
							}
							goto IL_2DB;
						}
						else
						{
							if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#hmac-sha1"))
							{
								return result;
							}
							return true;
						}
					}
					else if (num != 2323908233U)
					{
						if (num != 2551777632U)
						{
							return result;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes192"))
						{
							return result;
						}
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes256"))
						{
							return result;
						}
						goto IL_2D2;
					}
				}
				else if (num <= 3225656034U)
				{
					if (num != 2888462845U)
					{
						if (num != 3225656034U)
						{
							return result;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc"))
						{
							return result;
						}
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes128"))
						{
							return result;
						}
						goto IL_2A6;
					}
				}
				else if (num != 3487232831U)
				{
					if (num != 3654423024U)
					{
						if (num != 3880483293U)
						{
							return result;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
						{
							return result;
						}
						return false;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5"))
						{
							return result;
						}
						return false;
					}
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes"))
					{
						return result;
					}
					goto IL_2DB;
				}
				return keySize >= 192 && keySize <= 256;
				IL_2DB:
				return keySize == 128 || keySize == 192;
			}
			if (num <= 636766351U)
			{
				if (num <= 550229268U)
				{
					if (num != 376408642U)
					{
						if (num != 550229268U)
						{
							return result;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
						{
							return result;
						}
						return true;
					}
					else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p"))
					{
						return result;
					}
				}
				else if (num != 600251407U)
				{
					if (num != 636766351U)
					{
						return result;
					}
					if (!(algorithm == "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1"))
					{
						return result;
					}
					return true;
				}
				else
				{
					if (!(algorithm == "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1"))
					{
						return result;
					}
					return true;
				}
			}
			else if (num <= 712490267U)
			{
				if (num != 699966473U)
				{
					if (num != 712490267U)
					{
						return result;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
					{
						return result;
					}
					goto IL_2D2;
				}
				else if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
				{
					return result;
				}
			}
			else if (num != 811041755U)
			{
				if (num != 877368883U)
				{
					return result;
				}
				if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1"))
				{
					return result;
				}
			}
			else
			{
				if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc"))
				{
					return result;
				}
				goto IL_2A6;
			}
			return false;
			IL_2A6:
			return keySize >= 128 && keySize <= 256;
			IL_2D2:
			return keySize == 256;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007414 File Offset: 0x00005614
		internal static byte[] UnwrapKey(byte[] wrappingKey, byte[] wrappedKey, string algorithm)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			SymmetricAlgorithm symmetricAlgorithm;
			if (algorithmFromConfig != null)
			{
				symmetricAlgorithm = (algorithmFromConfig as SymmetricAlgorithm);
				if (symmetricAlgorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("InvalidCustomKeyWrapAlgorithm", new object[]
					{
						algorithm
					})));
				}
				using (symmetricAlgorithm)
				{
					symmetricAlgorithm.Key = wrappingKey;
					return EncryptedXml.DecryptKey(wrappedKey, symmetricAlgorithm);
				}
			}
			if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes"))
			{
				if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes128") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes192") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedKeyWrapAlgorithm", new object[]
					{
						algorithm
					})));
				}
				symmetricAlgorithm = (SecurityUtils.RequiresFipsCompliance ? new RijndaelCryptoServiceProvider() : new RijndaelManaged());
			}
			else
			{
				symmetricAlgorithm = new TripleDESCryptoServiceProvider();
			}
			byte[] result;
			using (symmetricAlgorithm)
			{
				symmetricAlgorithm.Key = wrappingKey;
				result = EncryptedXml.DecryptKey(wrappedKey, symmetricAlgorithm);
			}
			return result;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007530 File Offset: 0x00005730
		internal static byte[] WrapKey(byte[] wrappingKey, byte[] keyToBeWrapped, string algorithm)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(algorithm);
			SymmetricAlgorithm symmetricAlgorithm;
			if (algorithmFromConfig != null)
			{
				symmetricAlgorithm = (algorithmFromConfig as SymmetricAlgorithm);
				if (symmetricAlgorithm == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("InvalidCustomKeyWrapAlgorithm", new object[]
					{
						algorithm
					})));
				}
				using (symmetricAlgorithm)
				{
					symmetricAlgorithm.Key = wrappingKey;
					return EncryptedXml.EncryptKey(keyToBeWrapped, symmetricAlgorithm);
				}
			}
			if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes"))
			{
				if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes128") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes192") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new InvalidOperationException(SR.GetString("UnsupportedKeyWrapAlgorithm", new object[]
					{
						algorithm
					})));
				}
				symmetricAlgorithm = (SecurityUtils.RequiresFipsCompliance ? new RijndaelCryptoServiceProvider() : new RijndaelManaged());
			}
			else
			{
				symmetricAlgorithm = new TripleDESCryptoServiceProvider();
			}
			byte[] result;
			using (symmetricAlgorithm)
			{
				symmetricAlgorithm.Key = wrappingKey;
				result = EncryptedXml.EncryptKey(keyToBeWrapped, symmetricAlgorithm);
			}
			return result;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000764C File Offset: 0x0000584C
		internal static void ValidateBufferBounds(Array buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (count < 0 || count > buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					buffer.Length
				})));
			}
			if (offset < 0 || offset > buffer.Length - count)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					buffer.Length - count
				})));
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007708 File Offset: 0x00005908
		internal static bool IsEqual(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null || a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007744 File Offset: 0x00005944
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static bool FixedTimeEquals(byte[] a, byte[] b)
		{
			if (a == null && b == null)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			int num = 0;
			int num2 = a.Length;
			for (int i = 0; i < num2; i++)
			{
				num |= (int)(a[i] ^ b[i]);
			}
			return num == 0;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000778C File Offset: 0x0000598C
		private static object GetDefaultAlgorithm(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("algorithm"));
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(algorithm);
			if (num > 1688024611U)
			{
				if (num <= 2888462845U)
				{
					if (num <= 2323908233U)
					{
						if (num != 1735592375U)
						{
							if (num != 2323908233U)
							{
								goto IL_362;
							}
							if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes256"))
							{
								goto IL_362;
							}
							goto IL_2FD;
						}
						else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
						{
							goto IL_362;
						}
					}
					else if (num != 2551777632U)
					{
						if (num != 2888462845U)
						{
							goto IL_362;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes128"))
						{
							goto IL_362;
						}
						goto IL_2FD;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes192"))
						{
							goto IL_362;
						}
						goto IL_2FD;
					}
				}
				else if (num <= 2964519322U)
				{
					if (num != 2918801765U)
					{
						if (num != 2964519322U)
						{
							goto IL_362;
						}
						if (!(algorithm == "SHA256"))
						{
							goto IL_362;
						}
						goto IL_2D7;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/10/xml-exc-c14n#"))
						{
							goto IL_362;
						}
						return new XmlDsigExcC14NTransform();
					}
				}
				else if (num != 3225656034U)
				{
					if (num != 3487232831U)
					{
						if (num != 3772560434U)
						{
							goto IL_362;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#sha512"))
						{
							goto IL_362;
						}
						if (SecurityUtils.RequiresFipsCompliance)
						{
							return new SHA512CryptoServiceProvider();
						}
						return new SHA512Managed();
					}
					else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes"))
					{
						goto IL_362;
					}
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc"))
					{
						goto IL_362;
					}
					goto IL_2FD;
				}
				return new TripleDESCryptoServiceProvider();
			}
			if (num <= 811041755U)
			{
				if (num <= 550229268U)
				{
					if (num != 298426848U)
					{
						if (num != 550229268U)
						{
							goto IL_362;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
						{
							goto IL_362;
						}
						if (!SecurityUtils.RequiresFipsCompliance)
						{
							return new HMACSHA256();
						}
						return null;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#ripemd160"))
						{
							goto IL_362;
						}
						if (!SecurityUtils.RequiresFipsCompliance)
						{
							return new RIPEMD160Managed();
						}
						return null;
					}
				}
				else if (num != 712490267U)
				{
					if (num != 811041755U)
					{
						goto IL_362;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc"))
					{
						goto IL_362;
					}
					goto IL_2FD;
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
					{
						goto IL_362;
					}
					goto IL_2FD;
				}
			}
			else if (num <= 965923590U)
			{
				if (num != 944534123U)
				{
					if (num != 965923590U)
					{
						goto IL_362;
					}
					if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#sha1"))
					{
						goto IL_362;
					}
					if (SecurityUtils.RequiresFipsCompliance)
					{
						return new SHA1CryptoServiceProvider();
					}
					return new SHA1Managed();
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/10/xml-exc-c14n#WithComments"))
					{
						goto IL_362;
					}
					return new XmlDsigExcC14NWithCommentsTransform();
				}
			}
			else if (num != 1318943838U)
			{
				if (num != 1611967855U)
				{
					if (num != 1688024611U)
					{
						goto IL_362;
					}
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#sha256"))
					{
						goto IL_362;
					}
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#des-cbc"))
					{
						goto IL_362;
					}
					return new DESCryptoServiceProvider();
				}
			}
			else
			{
				if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#hmac-sha1"))
				{
					goto IL_362;
				}
				byte[] array = new byte[64];
				new RNGCryptoServiceProvider().GetBytes(array);
				return new HMACSHA1(array, !SecurityUtils.RequiresFipsCompliance);
			}
			IL_2D7:
			if (SecurityUtils.RequiresFipsCompliance)
			{
				return new SHA256CryptoServiceProvider();
			}
			return new SHA256Managed();
			IL_2FD:
			if (SecurityUtils.RequiresFipsCompliance)
			{
				return new RijndaelCryptoServiceProvider();
			}
			return new RijndaelManaged();
			IL_362:
			return null;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007AFC File Offset: 0x00005CFC
		internal static object GetAlgorithmFromConfig(string algorithm)
		{
			if (string.IsNullOrEmpty(algorithm))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("algorithm"));
			}
			object obj = null;
			Func<object> func = null;
			if (!CryptoHelper.algorithmDelegateDictionary.TryGetValue(algorithm, out func))
			{
				object algorithmDictionaryLock = CryptoHelper.AlgorithmDictionaryLock;
				lock (algorithmDictionaryLock)
				{
					if (CryptoHelper.algorithmDelegateDictionary.ContainsKey(algorithm))
					{
						goto IL_137;
					}
					try
					{
						if (!LocalAppContextSwitches.PassUnfilteredAlgorithmsToCryptoConfig && (algorithm == null || algorithm.IndexOfAny(CryptoHelper.s_invalidChars) > 0))
						{
							return null;
						}
						obj = CryptoConfig.CreateFromName(algorithm);
					}
					catch (TargetInvocationException)
					{
						CryptoHelper.algorithmDelegateDictionary[algorithm] = null;
					}
					if (obj == null)
					{
						CryptoHelper.algorithmDelegateDictionary[algorithm] = null;
						goto IL_137;
					}
					object defaultAlgorithm = CryptoHelper.GetDefaultAlgorithm(algorithm);
					if ((!SecurityUtils.RequiresFipsCompliance && obj is SHA1CryptoServiceProvider) || (defaultAlgorithm != null && defaultAlgorithm.GetType() == obj.GetType()))
					{
						CryptoHelper.algorithmDelegateDictionary[algorithm] = null;
						goto IL_137;
					}
					Type type = obj.GetType();
					NewExpression body = Expression.New(type);
					LambdaExpression lambdaExpression = Expression.Lambda<Func<object>>(body, new ParameterExpression[0]);
					func = (lambdaExpression.Compile() as Func<object>);
					if (func != null)
					{
						CryptoHelper.algorithmDelegateDictionary[algorithm] = func;
					}
					return obj;
				}
			}
			if (func != null)
			{
				return func();
			}
			IL_137:
			if (!(algorithm == "SHA256") && !(algorithm == "http://www.w3.org/2001/04/xmlenc#sha256"))
			{
				if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#sha1"))
				{
					if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#hmac-sha1"))
					{
						return null;
					}
					return new HMACSHA1(CryptoHelper.GenerateRandomBytes(64), !SecurityUtils.RequiresFipsCompliance);
				}
				else
				{
					if (SecurityUtils.RequiresFipsCompliance)
					{
						return new SHA1CryptoServiceProvider();
					}
					return new SHA1Managed();
				}
			}
			else
			{
				if (SecurityUtils.RequiresFipsCompliance)
				{
					return new SHA256CryptoServiceProvider();
				}
				return new SHA256Managed();
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public static void ResetAllCertificates(X509Certificate2Collection certificates)
		{
			if (certificates != null)
			{
				for (int i = 0; i < certificates.Count; i++)
				{
					certificates[i].Reset();
				}
			}
		}

		// Token: 0x04000120 RID: 288
		private static byte[] emptyBuffer;

		// Token: 0x04000121 RID: 289
		private static RandomNumberGenerator random;

		// Token: 0x04000122 RID: 290
		private static Rijndael rijndael;

		// Token: 0x04000123 RID: 291
		private static TripleDES tripleDES;

		// Token: 0x04000124 RID: 292
		private static Dictionary<string, Func<object>> algorithmDelegateDictionary = new Dictionary<string, Func<object>>();

		// Token: 0x04000125 RID: 293
		private static object AlgorithmDictionaryLock = new object();

		// Token: 0x04000126 RID: 294
		public const int WindowsVistaMajorNumber = 6;

		// Token: 0x04000127 RID: 295
		private const string SHAString = "SHA";

		// Token: 0x04000128 RID: 296
		private const string SHA1String = "SHA1";

		// Token: 0x04000129 RID: 297
		private const string SHA256String = "SHA256";

		// Token: 0x0400012A RID: 298
		private const string SystemSecurityCryptographySha1String = "System.Security.Cryptography.SHA1";

		// Token: 0x0400012B RID: 299
		private static readonly char[] s_invalidChars = new char[]
		{
			',',
			'`',
			'[',
			'*',
			'&'
		};

		// Token: 0x02000234 RID: 564
		public static class KeyGenerator
		{
			// Token: 0x060011F5 RID: 4597 RVA: 0x0004E700 File Offset: 0x0004C900
			public static byte[] ComputeCombinedKey(byte[] requestorEntropy, byte[] issuerEntropy, int keySizeInBits)
			{
				if (requestorEntropy == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("requestorEntropy");
				}
				if (issuerEntropy == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("issuerEntropy");
				}
				int num = CryptoHelper.KeyGenerator.ValidateKeySizeInBytes(keySizeInBits);
				byte[] array = new byte[num];
				using (KeyedHashAlgorithm keyedHashAlgorithm = CryptoHelper.NewHmacSha1KeyedHashAlgorithm())
				{
					keyedHashAlgorithm.Key = requestorEntropy;
					byte[] array2 = issuerEntropy;
					byte[] array3 = new byte[keyedHashAlgorithm.HashSize / 8 + array2.Length];
					byte[] array4 = null;
					try
					{
						int i = 0;
						while (i < num)
						{
							keyedHashAlgorithm.Initialize();
							array2 = keyedHashAlgorithm.ComputeHash(array2);
							array2.CopyTo(array3, 0);
							issuerEntropy.CopyTo(array3, array2.Length);
							keyedHashAlgorithm.Initialize();
							array4 = keyedHashAlgorithm.ComputeHash(array3);
							int num2 = 0;
							while (num2 < array4.Length && i < num)
							{
								array[i++] = array4[num2];
								num2++;
							}
						}
					}
					catch
					{
						Array.Clear(array, 0, array.Length);
						throw;
					}
					finally
					{
						if (array4 != null)
						{
							Array.Clear(array4, 0, array4.Length);
						}
						Array.Clear(array3, 0, array3.Length);
						keyedHashAlgorithm.Clear();
					}
				}
				return array;
			}

			// Token: 0x060011F6 RID: 4598 RVA: 0x0004E830 File Offset: 0x0004CA30
			public static byte[] GenerateSymmetricKey(int keySizeInBits)
			{
				int num = CryptoHelper.KeyGenerator.ValidateKeySizeInBytes(keySizeInBits);
				byte[] array = new byte[num];
				CryptoHelper.GenerateRandomBytes(array);
				return array;
			}

			// Token: 0x060011F7 RID: 4599 RVA: 0x0004E854 File Offset: 0x0004CA54
			public static byte[] GenerateSymmetricKey(int keySizeInBits, byte[] senderEntropy, out byte[] receiverEntropy)
			{
				if (senderEntropy == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("senderEntropy");
				}
				int num = CryptoHelper.KeyGenerator.ValidateKeySizeInBytes(keySizeInBits);
				receiverEntropy = new byte[num];
				CryptoHelper.KeyGenerator._random.GetNonZeroBytes(receiverEntropy);
				return CryptoHelper.KeyGenerator.ComputeCombinedKey(senderEntropy, receiverEntropy, keySizeInBits);
			}

			// Token: 0x060011F8 RID: 4600 RVA: 0x0004E898 File Offset: 0x0004CA98
			public static byte[] GenerateDESKey(int keySizeInBits)
			{
				int num = CryptoHelper.KeyGenerator.ValidateKeySizeInBytes(keySizeInBits);
				byte[] array = new byte[num];
				int i = 0;
				while (i <= 20)
				{
					CryptoHelper.GenerateRandomBytes(array);
					i++;
					if (!TripleDES.IsWeakKey(array))
					{
						return array;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID6048", new object[]
				{
					20
				})));
			}

			// Token: 0x060011F9 RID: 4601 RVA: 0x0004E8F8 File Offset: 0x0004CAF8
			public static byte[] GenerateDESKey(int keySizeInBits, byte[] senderEntropy, out byte[] receiverEntropy)
			{
				int num = CryptoHelper.KeyGenerator.ValidateKeySizeInBytes(keySizeInBits);
				byte[] array = new byte[num];
				int i = 0;
				while (i <= 20)
				{
					receiverEntropy = new byte[num];
					CryptoHelper.KeyGenerator._random.GetNonZeroBytes(receiverEntropy);
					array = CryptoHelper.KeyGenerator.ComputeCombinedKey(senderEntropy, receiverEntropy, keySizeInBits);
					i++;
					if (!TripleDES.IsWeakKey(array))
					{
						return array;
					}
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("ID6048", new object[]
				{
					20
				})));
			}

			// Token: 0x060011FA RID: 4602 RVA: 0x0004E970 File Offset: 0x0004CB70
			private static int ValidateKeySizeInBytes(int keySizeInBits)
			{
				int num = keySizeInBits / 8;
				if (keySizeInBits <= 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("keySizeInBits", SR.GetString("ID6033", new object[]
					{
						keySizeInBits
					})));
				}
				if (num * 8 != keySizeInBits)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID6002", new object[]
					{
						keySizeInBits
					}), "keySizeInBits"));
				}
				return num;
			}

			// Token: 0x060011FB RID: 4603 RVA: 0x0004E9E8 File Offset: 0x0004CBE8
			public static SecurityKeyIdentifier GetSecurityKeyIdentifier(byte[] secret, EncryptingCredentials wrappingCredentials)
			{
				if (secret == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("secret");
				}
				if (secret.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("secret", SR.GetString("ID6031"));
				}
				if (wrappingCredentials == null || wrappingCredentials.SecurityKey == null)
				{
					return new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
					{
						new BinarySecretKeyIdentifierClause(secret)
					});
				}
				byte[] encryptedKey = wrappingCredentials.SecurityKey.EncryptKey(wrappingCredentials.Algorithm, secret);
				return new SecurityKeyIdentifier(new SecurityKeyIdentifierClause[]
				{
					new EncryptedKeyIdentifierClause(encryptedKey, wrappingCredentials.Algorithm, wrappingCredentials.SecurityKeyIdentifier)
				});
			}

			// Token: 0x04000F30 RID: 3888
			private static RandomNumberGenerator _random = CryptoHelper.RandomNumberGenerator;

			// Token: 0x04000F31 RID: 3889
			private const int _maxKeyIterations = 20;
		}
	}
}
