using System;
using System.IdentityModel;
using System.Security.Cryptography;

namespace System.ServiceModel.Security
{
	// Token: 0x0200034A RID: 842
	internal static class CryptoHelper
	{
		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x00070E48 File Offset: 0x0006F048
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

		// Token: 0x06001E9C RID: 7836 RVA: 0x00070E6E File Offset: 0x0006F06E
		internal static HashAlgorithm NewSha1HashAlgorithm()
		{
			return CryptoHelper.CreateHashAlgorithm("http://www.w3.org/2000/09/xmldsig#sha1");
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x00070E7A File Offset: 0x0006F07A
		internal static HashAlgorithm NewSha256HashAlgorithm()
		{
			return CryptoHelper.CreateHashAlgorithm("http://www.w3.org/2001/04/xmlenc#sha256");
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00070E88 File Offset: 0x0006F088
		internal static HashAlgorithm CreateHashAlgorithm(string digestMethod)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(digestMethod);
			if (algorithmFromConfig != null)
			{
				HashAlgorithm hashAlgorithm = algorithmFromConfig as HashAlgorithm;
				if (hashAlgorithm != null)
				{
					return hashAlgorithm;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("CustomCryptoAlgorithmIsNotValidHashAlgorithm", new object[]
				{
					digestMethod
				})));
			}
			else if (!(digestMethod == "http://www.w3.org/2000/09/xmldsig#sha1"))
			{
				if (!(digestMethod == "http://www.w3.org/2001/04/xmlenc#sha256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						digestMethod
					})));
				}
				if (SecurityUtilsEx.RequiresFipsCompliance)
				{
					return new SHA256CryptoServiceProvider();
				}
				return new SHA256Managed();
			}
			else
			{
				if (SecurityUtilsEx.RequiresFipsCompliance)
				{
					return new SHA1CryptoServiceProvider();
				}
				return new SHA1Managed();
			}
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x00070F34 File Offset: 0x0006F134
		internal static HashAlgorithm CreateHashForAsymmetricSignature(string signatureMethod)
		{
			object algorithmFromConfig = CryptoHelper.GetAlgorithmFromConfig(signatureMethod);
			if (algorithmFromConfig != null)
			{
				SignatureDescription signatureDescription = algorithmFromConfig as SignatureDescription;
				HashAlgorithm hashAlgorithm;
				if (signatureDescription != null)
				{
					hashAlgorithm = signatureDescription.CreateDigest();
					if (hashAlgorithm != null)
					{
						return hashAlgorithm;
					}
				}
				hashAlgorithm = (algorithmFromConfig as HashAlgorithm);
				if (hashAlgorithm != null)
				{
					return hashAlgorithm;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("CustomCryptoAlgorithmIsNotValidAsymmetricSignature", new object[]
				{
					signatureMethod
				})));
			}
			else if (!(signatureMethod == "http://www.w3.org/2000/09/xmldsig#rsa-sha1") && !(signatureMethod == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
			{
				if (!(signatureMethod == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new MessageSecurityException(SR.GetString("UnsupportedCryptoAlgorithm", new object[]
					{
						signatureMethod
					})));
				}
				if (SecurityUtilsEx.RequiresFipsCompliance)
				{
					return new SHA256CryptoServiceProvider();
				}
				return new SHA256Managed();
			}
			else
			{
				if (SecurityUtilsEx.RequiresFipsCompliance)
				{
					return new SHA1CryptoServiceProvider();
				}
				return new SHA1Managed();
			}
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00071004 File Offset: 0x0006F204
		internal static byte[] ExtractIVAndDecrypt(SymmetricAlgorithm algorithm, byte[] cipherText, int offset, int count)
		{
			if (cipherText == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("cipherText");
			}
			if (count < 0 || count > cipherText.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					cipherText.Length
				})));
			}
			if (offset < 0 || offset > cipherText.Length - count)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", SR.GetString("ValueMustBeInRange", new object[]
				{
					0,
					cipherText.Length - count
				})));
			}
			int num = algorithm.BlockSize / 8;
			byte[] array = new byte[num];
			Buffer.BlockCopy(cipherText, offset, array, 0, array.Length);
			algorithm.Padding = PaddingMode.ISO10126;
			algorithm.Mode = CipherMode.CBC;
			byte[] result;
			try
			{
				using (ICryptoTransform cryptoTransform = algorithm.CreateDecryptor(algorithm.Key, array))
				{
					result = cryptoTransform.TransformFinalBlock(cipherText, offset + array.Length, count - array.Length);
				}
			}
			catch (CryptographicException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(SR.GetString("DecryptionFailed"), innerException));
			}
			return result;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00071140 File Offset: 0x0006F340
		internal static void FillRandomBytes(byte[] buffer)
		{
			CryptoHelper.random.GetBytes(buffer);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00071150 File Offset: 0x0006F350
		private static CryptoHelper.CryptoAlgorithmType GetAlgorithmType(string algorithm)
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
			if (obj == null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(algorithm);
				if (num <= 877368883U)
				{
					if (num <= 636766351U)
					{
						if (num <= 550229268U)
						{
							if (num != 376408642U)
							{
								if (num != 550229268U)
								{
									return CryptoHelper.CryptoAlgorithmType.Unknown;
								}
								if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
								{
									return CryptoHelper.CryptoAlgorithmType.Unknown;
								}
								return CryptoHelper.CryptoAlgorithmType.Symmetric;
							}
							else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p"))
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
						}
						else if (num != 600251407U)
						{
							if (num != 636766351U)
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
							if (!(algorithm == "http://docs.oasis-open.org/ws-sx/ws-secureconversation/200512/dk/p_sha1"))
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
							return CryptoHelper.CryptoAlgorithmType.Symmetric;
						}
						else
						{
							if (!(algorithm == "http://schemas.xmlsoap.org/ws/2005/02/sc/dk/p_sha1"))
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
							return CryptoHelper.CryptoAlgorithmType.Symmetric;
						}
					}
					else if (num <= 712490267U)
					{
						if (num != 699966473U)
						{
							if (num != 712490267U)
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
							if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
							return CryptoHelper.CryptoAlgorithmType.Symmetric;
						}
						else if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
					}
					else if (num != 811041755U)
					{
						if (num != 877368883U)
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#rsa-sha1"))
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes128-cbc"))
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						return CryptoHelper.CryptoAlgorithmType.Symmetric;
					}
				}
				else if (num <= 2551777632U)
				{
					if (num <= 1735592375U)
					{
						if (num != 1318943838U)
						{
							if (num != 1735592375U)
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
							if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
							return CryptoHelper.CryptoAlgorithmType.Symmetric;
						}
						else
						{
							if (!(algorithm == "http://www.w3.org/2000/09/xmldsig#hmac-sha1"))
							{
								return CryptoHelper.CryptoAlgorithmType.Unknown;
							}
							return CryptoHelper.CryptoAlgorithmType.Symmetric;
						}
					}
					else if (num != 2323908233U)
					{
						if (num != 2551777632U)
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes192"))
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						return CryptoHelper.CryptoAlgorithmType.Symmetric;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes256"))
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						return CryptoHelper.CryptoAlgorithmType.Symmetric;
					}
				}
				else if (num <= 3225656034U)
				{
					if (num != 2888462845U)
					{
						if (num != 3225656034U)
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#aes192-cbc"))
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						return CryptoHelper.CryptoAlgorithmType.Symmetric;
					}
					else
					{
						if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-aes128"))
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						return CryptoHelper.CryptoAlgorithmType.Symmetric;
					}
				}
				else if (num != 3487232831U)
				{
					if (num != 3654423024U)
					{
						if (num != 3880483293U)
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
						if (!(algorithm == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
						{
							return CryptoHelper.CryptoAlgorithmType.Unknown;
						}
					}
					else if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#rsa-1_5"))
					{
						return CryptoHelper.CryptoAlgorithmType.Unknown;
					}
				}
				else
				{
					if (!(algorithm == "http://www.w3.org/2001/04/xmlenc#kw-tripledes"))
					{
						return CryptoHelper.CryptoAlgorithmType.Unknown;
					}
					return CryptoHelper.CryptoAlgorithmType.Symmetric;
				}
				return CryptoHelper.CryptoAlgorithmType.Asymmetric;
			}
			SymmetricAlgorithm symmetricAlgorithm = obj as SymmetricAlgorithm;
			KeyedHashAlgorithm keyedHashAlgorithm = obj as KeyedHashAlgorithm;
			if (symmetricAlgorithm != null || keyedHashAlgorithm != null)
			{
				return CryptoHelper.CryptoAlgorithmType.Symmetric;
			}
			AsymmetricAlgorithm asymmetricAlgorithm = obj as AsymmetricAlgorithm;
			SignatureDescription signatureDescription = obj as SignatureDescription;
			if (asymmetricAlgorithm != null || signatureDescription != null)
			{
				return CryptoHelper.CryptoAlgorithmType.Asymmetric;
			}
			return CryptoHelper.CryptoAlgorithmType.Unknown;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x0007141C File Offset: 0x0006F61C
		internal static byte[] GenerateIVAndEncrypt(SymmetricAlgorithm algorithm, byte[] plainText, int offset, int count)
		{
			byte[] array;
			byte[] array2;
			CryptoHelper.GenerateIVAndEncrypt(algorithm, new ArraySegment<byte>(plainText, offset, count), out array, out array2);
			byte[] array3 = DiagnosticUtility.Utility.AllocateByteArray(checked(array.Length + array2.Length));
			Buffer.BlockCopy(array, 0, array3, 0, array.Length);
			Buffer.BlockCopy(array2, 0, array3, array.Length, array2.Length);
			return array3;
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00071468 File Offset: 0x0006F668
		internal static void GenerateIVAndEncrypt(SymmetricAlgorithm algorithm, ArraySegment<byte> plainText, out byte[] iv, out byte[] cipherText)
		{
			int num = algorithm.BlockSize / 8;
			iv = new byte[num];
			CryptoHelper.FillRandomBytes(iv);
			algorithm.Padding = PaddingMode.PKCS7;
			algorithm.Mode = CipherMode.CBC;
			using (ICryptoTransform cryptoTransform = algorithm.CreateEncryptor(algorithm.Key, iv))
			{
				cipherText = cryptoTransform.TransformFinalBlock(plainText.Array, plainText.Offset, plainText.Count);
			}
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x000714E4 File Offset: 0x0006F6E4
		internal static bool IsEqual(byte[] a, byte[] b)
		{
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

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0007151A File Offset: 0x0006F71A
		internal static bool IsSymmetricAlgorithm(string algorithm)
		{
			return CryptoHelper.GetAlgorithmType(algorithm) == CryptoHelper.CryptoAlgorithmType.Symmetric;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00071528 File Offset: 0x0006F728
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
				obj = null;
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
							goto IL_2BD;
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
						goto IL_2B4;
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
						goto IL_2A2;
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
					goto IL_2BD;
				}
				return keySize == 192;
				IL_2BD:
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
					goto IL_2B4;
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
				goto IL_2A2;
			}
			return false;
			IL_2A2:
			return keySize == 128;
			IL_2B4:
			return keySize == 256;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x0007181C File Offset: 0x0006FA1C
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

		// Token: 0x06001EA9 RID: 7849 RVA: 0x000718D8 File Offset: 0x0006FAD8
		internal static void ValidateSymmetricKeyLength(int keyLength, SecurityAlgorithmSuite algorithmSuite)
		{
			if (!algorithmSuite.IsSymmetricKeyLengthSupported(keyLength))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new ArgumentOutOfRangeException("algorithmSuite", SR.GetString("UnsupportedKeyLength", new object[]
				{
					keyLength,
					algorithmSuite.ToString()
				})));
			}
			if (keyLength % 8 != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new ArgumentOutOfRangeException("algorithmSuite", SR.GetString("KeyLengthMustBeMultipleOfEight", new object[]
				{
					keyLength
				})));
			}
		}

		// Token: 0x04001EAC RID: 7852
		private static byte[] emptyBuffer;

		// Token: 0x04001EAD RID: 7853
		private static readonly RandomNumberGenerator random = new RNGCryptoServiceProvider();

		// Token: 0x02000B7F RID: 2943
		private enum CryptoAlgorithmType
		{
			// Token: 0x04004103 RID: 16643
			Unknown,
			// Token: 0x04004104 RID: 16644
			Symmetric,
			// Token: 0x04004105 RID: 16645
			Asymmetric
		}
	}
}
