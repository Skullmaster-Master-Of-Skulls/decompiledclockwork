using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x0200010D RID: 269
	internal static class NCryptNative
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x0001CFF0 File Offset: 0x0001B1F0
		[SecuritySafeCritical]
		private static byte[] DecryptData<T>(SafeNCryptKeyHandle key, byte[] data, ref T paddingInfo, AsymmetricPaddingMode paddingMode, NCryptNative.NCryptDecryptor<T> decryptor) where T : struct
		{
			int num = 0;
			NCryptNative.ErrorCode errorCode = decryptor(key, data, data.Length, ref paddingInfo, null, 0, out num, paddingMode);
			if (errorCode != NCryptNative.ErrorCode.Success && errorCode != NCryptNative.ErrorCode.BufferTooSmall)
			{
				throw new CryptographicException((int)errorCode);
			}
			byte[] array = new byte[num];
			errorCode = decryptor(key, data, data.Length, ref paddingInfo, array, array.Length, out num, paddingMode);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			if (array.Length != num)
			{
				byte[] array2 = array;
				Array.Resize<byte>(ref array, num);
				Array.Clear(array2, 0, array2.Length);
			}
			return array;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001D068 File Offset: 0x0001B268
		[SecuritySafeCritical]
		internal static byte[] DecryptDataPkcs1(SafeNCryptKeyHandle key, byte[] data)
		{
			BCryptNative.BCRYPT_PKCS1_PADDING_INFO bcrypt_PKCS1_PADDING_INFO = default(BCryptNative.BCRYPT_PKCS1_PADDING_INFO);
			return NCryptNative.DecryptData<BCryptNative.BCRYPT_PKCS1_PADDING_INFO>(key, data, ref bcrypt_PKCS1_PADDING_INFO, AsymmetricPaddingMode.Pkcs1, new NCryptNative.NCryptDecryptor<BCryptNative.BCRYPT_PKCS1_PADDING_INFO>(NCryptNative.Pkcs1PaddingDecryptionWrapper));
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001D094 File Offset: 0x0001B294
		[SecuritySafeCritical]
		internal static byte[] DecryptDataOaep(SafeNCryptKeyHandle key, byte[] data, string hashAlgorithm)
		{
			BCryptNative.BCRYPT_OAEP_PADDING_INFO bcrypt_OAEP_PADDING_INFO = default(BCryptNative.BCRYPT_OAEP_PADDING_INFO);
			bcrypt_OAEP_PADDING_INFO.pszAlgId = hashAlgorithm;
			return NCryptNative.DecryptData<BCryptNative.BCRYPT_OAEP_PADDING_INFO>(key, data, ref bcrypt_OAEP_PADDING_INFO, AsymmetricPaddingMode.Oaep, new NCryptNative.NCryptDecryptor<BCryptNative.BCRYPT_OAEP_PADDING_INFO>(NCryptNative.UnsafeNativeMethods.NCryptDecrypt));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001D0C7 File Offset: 0x0001B2C7
		[SecurityCritical]
		private static NCryptNative.ErrorCode Pkcs1PaddingDecryptionWrapper(SafeNCryptKeyHandle hKey, byte[] pbInput, int cbInput, ref BCryptNative.BCRYPT_PKCS1_PADDING_INFO pvPadding, byte[] pbOutput, int cbOutput, out int pcbResult, AsymmetricPaddingMode dwFlags)
		{
			return NCryptNative.UnsafeNativeMethods.NCryptDecrypt(hKey, pbInput, cbInput, IntPtr.Zero, pbOutput, cbOutput, out pcbResult, dwFlags);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001D0E0 File Offset: 0x0001B2E0
		[SecuritySafeCritical]
		private static byte[] EncryptData<T>(SafeNCryptKeyHandle key, byte[] data, ref T paddingInfo, AsymmetricPaddingMode paddingMode, NCryptNative.NCryptEncryptor<T> encryptor) where T : struct
		{
			int num = 0;
			NCryptNative.ErrorCode errorCode = encryptor(key, data, data.Length, ref paddingInfo, null, 0, out num, paddingMode);
			if (errorCode != NCryptNative.ErrorCode.Success && errorCode != NCryptNative.ErrorCode.BufferTooSmall)
			{
				throw new CryptographicException((int)errorCode);
			}
			byte[] array = new byte[num];
			errorCode = encryptor(key, data, data.Length, ref paddingInfo, array, array.Length, out num, paddingMode);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return array;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001D13C File Offset: 0x0001B33C
		[SecuritySafeCritical]
		internal static byte[] EncryptDataOaep(SafeNCryptKeyHandle key, byte[] data, string hashAlgorithm)
		{
			BCryptNative.BCRYPT_OAEP_PADDING_INFO bcrypt_OAEP_PADDING_INFO = default(BCryptNative.BCRYPT_OAEP_PADDING_INFO);
			bcrypt_OAEP_PADDING_INFO.pszAlgId = hashAlgorithm;
			return NCryptNative.EncryptData<BCryptNative.BCRYPT_OAEP_PADDING_INFO>(key, data, ref bcrypt_OAEP_PADDING_INFO, AsymmetricPaddingMode.Oaep, new NCryptNative.NCryptEncryptor<BCryptNative.BCRYPT_OAEP_PADDING_INFO>(NCryptNative.UnsafeNativeMethods.NCryptEncrypt));
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001D170 File Offset: 0x0001B370
		[SecuritySafeCritical]
		internal static byte[] EncryptDataPkcs1(SafeNCryptKeyHandle key, byte[] data)
		{
			BCryptNative.BCRYPT_PKCS1_PADDING_INFO bcrypt_PKCS1_PADDING_INFO = default(BCryptNative.BCRYPT_PKCS1_PADDING_INFO);
			return NCryptNative.EncryptData<BCryptNative.BCRYPT_PKCS1_PADDING_INFO>(key, data, ref bcrypt_PKCS1_PADDING_INFO, AsymmetricPaddingMode.Pkcs1, new NCryptNative.NCryptEncryptor<BCryptNative.BCRYPT_PKCS1_PADDING_INFO>(NCryptNative.Pkcs1PaddingEncryptionWrapper));
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001D19B File Offset: 0x0001B39B
		[SecurityCritical]
		private static NCryptNative.ErrorCode Pkcs1PaddingEncryptionWrapper(SafeNCryptKeyHandle hKey, byte[] pbInput, int cbInput, ref BCryptNative.BCRYPT_PKCS1_PADDING_INFO pvPadding, byte[] pbOutput, int cbOutput, out int pcbResult, AsymmetricPaddingMode dwFlags)
		{
			return NCryptNative.UnsafeNativeMethods.NCryptEncrypt(hKey, pbInput, cbInput, IntPtr.Zero, pbOutput, cbOutput, out pcbResult, dwFlags);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001D1B4 File Offset: 0x0001B3B4
		[SecuritySafeCritical]
		private static byte[] SignHash<T>(SafeNCryptKeyHandle key, byte[] hash, ref T paddingInfo, AsymmetricPaddingMode paddingMode, NCryptNative.NCryptHashSigner<T> signer) where T : struct
		{
			int num = 0;
			NCryptNative.ErrorCode errorCode = signer(key, ref paddingInfo, hash, hash.Length, null, 0, out num, paddingMode);
			if (errorCode != NCryptNative.ErrorCode.Success && errorCode != NCryptNative.ErrorCode.BufferTooSmall)
			{
				throw new CryptographicException((int)errorCode);
			}
			byte[] array = new byte[num];
			errorCode = signer(key, ref paddingInfo, hash, hash.Length, array, array.Length, out num, paddingMode);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return array;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001D210 File Offset: 0x0001B410
		[SecuritySafeCritical]
		internal static byte[] SignHashPkcs1(SafeNCryptKeyHandle key, byte[] hash, string hashAlgorithm)
		{
			BCryptNative.BCRYPT_PKCS1_PADDING_INFO bcrypt_PKCS1_PADDING_INFO = default(BCryptNative.BCRYPT_PKCS1_PADDING_INFO);
			bcrypt_PKCS1_PADDING_INFO.pszAlgId = hashAlgorithm;
			return NCryptNative.SignHash<BCryptNative.BCRYPT_PKCS1_PADDING_INFO>(key, hash, ref bcrypt_PKCS1_PADDING_INFO, AsymmetricPaddingMode.Pkcs1, new NCryptNative.NCryptHashSigner<BCryptNative.BCRYPT_PKCS1_PADDING_INFO>(NCryptNative.UnsafeNativeMethods.NCryptSignHash));
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001D244 File Offset: 0x0001B444
		[SecuritySafeCritical]
		internal static byte[] SignHashPss(SafeNCryptKeyHandle key, byte[] hash, string hashAlgorithm, int saltBytes)
		{
			BCryptNative.BCRYPT_PSS_PADDING_INFO bcrypt_PSS_PADDING_INFO = default(BCryptNative.BCRYPT_PSS_PADDING_INFO);
			bcrypt_PSS_PADDING_INFO.pszAlgId = hashAlgorithm;
			bcrypt_PSS_PADDING_INFO.cbSalt = saltBytes;
			return NCryptNative.SignHash<BCryptNative.BCRYPT_PSS_PADDING_INFO>(key, hash, ref bcrypt_PSS_PADDING_INFO, AsymmetricPaddingMode.Pss, new NCryptNative.NCryptHashSigner<BCryptNative.BCRYPT_PSS_PADDING_INFO>(NCryptNative.UnsafeNativeMethods.NCryptSignHash));
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001D280 File Offset: 0x0001B480
		[SecuritySafeCritical]
		private static bool VerifySignature<T>(SafeNCryptKeyHandle key, byte[] hash, byte[] signature, ref T paddingInfo, AsymmetricPaddingMode paddingMode, NCryptNative.NCryptSignatureVerifier<T> verifier) where T : struct
		{
			NCryptNative.ErrorCode errorCode = verifier(key, ref paddingInfo, hash, hash.Length, signature, signature.Length, paddingMode);
			return errorCode == NCryptNative.ErrorCode.Success;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001D2A8 File Offset: 0x0001B4A8
		[SecuritySafeCritical]
		internal static bool VerifySignaturePkcs1(SafeNCryptKeyHandle key, byte[] hash, string hashAlgorithm, byte[] signature)
		{
			BCryptNative.BCRYPT_PKCS1_PADDING_INFO bcrypt_PKCS1_PADDING_INFO = default(BCryptNative.BCRYPT_PKCS1_PADDING_INFO);
			bcrypt_PKCS1_PADDING_INFO.pszAlgId = hashAlgorithm;
			return NCryptNative.VerifySignature<BCryptNative.BCRYPT_PKCS1_PADDING_INFO>(key, hash, signature, ref bcrypt_PKCS1_PADDING_INFO, AsymmetricPaddingMode.Pkcs1, new NCryptNative.NCryptSignatureVerifier<BCryptNative.BCRYPT_PKCS1_PADDING_INFO>(NCryptNative.UnsafeNativeMethods.NCryptVerifySignature));
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001D2DC File Offset: 0x0001B4DC
		[SecuritySafeCritical]
		internal static bool VerifySignaturePss(SafeNCryptKeyHandle key, byte[] hash, string hashAlgorithm, int saltBytes, byte[] signature)
		{
			BCryptNative.BCRYPT_PSS_PADDING_INFO bcrypt_PSS_PADDING_INFO = default(BCryptNative.BCRYPT_PSS_PADDING_INFO);
			bcrypt_PSS_PADDING_INFO.pszAlgId = hashAlgorithm;
			bcrypt_PSS_PADDING_INFO.cbSalt = saltBytes;
			return NCryptNative.VerifySignature<BCryptNative.BCRYPT_PSS_PADDING_INFO>(key, hash, signature, ref bcrypt_PSS_PADDING_INFO, AsymmetricPaddingMode.Pss, new NCryptNative.NCryptSignatureVerifier<BCryptNative.BCRYPT_PSS_PADDING_INFO>(NCryptNative.UnsafeNativeMethods.NCryptVerifySignature));
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001D31C File Offset: 0x0001B51C
		internal static bool NCryptSupported
		{
			[SecuritySafeCritical]
			get
			{
				if (!NCryptNative.s_haveNcryptSupported)
				{
					using (SafeLibraryHandle safeLibraryHandle = Microsoft.Win32.UnsafeNativeMethods.LoadLibraryEx("ncrypt", IntPtr.Zero, 0))
					{
						NCryptNative.s_ncryptSupported = !safeLibraryHandle.IsInvalid;
						NCryptNative.s_haveNcryptSupported = true;
					}
				}
				return NCryptNative.s_ncryptSupported;
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001D380 File Offset: 0x0001B580
		internal static byte[] BuildEccPublicBlob(string algorithm, BigInteger x, BigInteger y)
		{
			BCryptNative.KeyBlobMagicNumber value;
			int keySize;
			BCryptNative.MapAlgorithmIdToMagic(algorithm, out value, out keySize);
			byte[] array = NCryptNative.ReverseBytes(NCryptNative.FillKeyParameter(x.ToByteArray(), keySize));
			byte[] array2 = NCryptNative.ReverseBytes(NCryptNative.FillKeyParameter(y.ToByteArray(), keySize));
			byte[] array3 = new byte[8 + array.Length + array2.Length];
			Buffer.BlockCopy(BitConverter.GetBytes((int)value), 0, array3, 0, 4);
			Buffer.BlockCopy(BitConverter.GetBytes(array.Length), 0, array3, 4, 4);
			Buffer.BlockCopy(array, 0, array3, 8, array.Length);
			Buffer.BlockCopy(array2, 0, array3, 8 + array.Length, array2.Length);
			return array3;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001D410 File Offset: 0x0001B610
		[SecurityCritical]
		internal static SafeNCryptKeyHandle CreatePersistedKey(SafeNCryptProviderHandle provider, string algorithm, string name, CngKeyCreationOptions options)
		{
			SafeNCryptKeyHandle result = null;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptCreatePersistedKey(provider, out result, algorithm, name, 0, options);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001D438 File Offset: 0x0001B638
		[SecurityCritical]
		internal static void DeleteKey(SafeNCryptKeyHandle key)
		{
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptDeleteKey(key, 0);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			key.SetHandleAsInvalid();
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001D460 File Offset: 0x0001B660
		[SecurityCritical]
		private unsafe static byte[] DeriveKeyMaterial(SafeNCryptSecretHandle secretAgreement, string kdf, string hashAlgorithm, byte[] hmacKey, byte[] secretPrepend, byte[] secretAppend, NCryptNative.SecretAgreementFlags flags)
		{
			List<NCryptNative.NCryptBuffer> list = new List<NCryptNative.NCryptBuffer>();
			IntPtr intPtr = IntPtr.Zero;
			RuntimeHelpers.PrepareConstrainedRegions();
			byte[] result;
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					intPtr = Marshal.StringToCoTaskMemUni(hashAlgorithm);
				}
				list.Add(new NCryptNative.NCryptBuffer
				{
					cbBuffer = (hashAlgorithm.Length + 1) * 2,
					BufferType = NCryptNative.BufferType.KdfHashAlgorithm,
					pvBuffer = intPtr
				});
				try
				{
					fixed (byte[] array = hmacKey)
					{
						byte* ptr;
						if (hmacKey == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						fixed (byte[] array2 = secretPrepend)
						{
							byte* ptr2;
							if (secretPrepend == null || array2.Length == 0)
							{
								ptr2 = null;
							}
							else
							{
								ptr2 = &array2[0];
							}
							fixed (byte[] array3 = secretAppend)
							{
								byte* ptr3;
								if (secretAppend == null || array3.Length == 0)
								{
									ptr3 = null;
								}
								else
								{
									ptr3 = &array3[0];
								}
								if (ptr != null)
								{
									list.Add(new NCryptNative.NCryptBuffer
									{
										cbBuffer = hmacKey.Length,
										BufferType = NCryptNative.BufferType.KdfHmacKey,
										pvBuffer = new IntPtr((void*)ptr)
									});
								}
								if (ptr2 != null)
								{
									list.Add(new NCryptNative.NCryptBuffer
									{
										cbBuffer = secretPrepend.Length,
										BufferType = NCryptNative.BufferType.KdfSecretPrepend,
										pvBuffer = new IntPtr((void*)ptr2)
									});
								}
								if (ptr3 != null)
								{
									list.Add(new NCryptNative.NCryptBuffer
									{
										cbBuffer = secretAppend.Length,
										BufferType = NCryptNative.BufferType.KdfSecretAppend,
										pvBuffer = new IntPtr((void*)ptr3)
									});
								}
								result = NCryptNative.DeriveKeyMaterial(secretAgreement, kdf, list.ToArray(), flags);
							}
						}
					}
				}
				finally
				{
					byte[] array = null;
					byte[] array2 = null;
					byte[] array3 = null;
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return result;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001D638 File Offset: 0x0001B838
		[SecurityCritical]
		private unsafe static byte[] DeriveKeyMaterial(SafeNCryptSecretHandle secretAgreement, string kdf, NCryptNative.NCryptBuffer[] parameters, NCryptNative.SecretAgreementFlags flags)
		{
			NCryptNative.NCryptBuffer* value;
			if (parameters == null || parameters.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &parameters[0];
			}
			NCryptNative.NCryptBufferDesc ncryptBufferDesc = default(NCryptNative.NCryptBufferDesc);
			ncryptBufferDesc.ulVersion = 0;
			ncryptBufferDesc.cBuffers = parameters.Length;
			ncryptBufferDesc.pBuffers = new IntPtr((void*)value);
			int num = 0;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptDeriveKey(secretAgreement, kdf, ref ncryptBufferDesc, null, 0, out num, flags);
			if (errorCode != NCryptNative.ErrorCode.Success && errorCode != NCryptNative.ErrorCode.BufferTooSmall)
			{
				throw new CryptographicException((int)errorCode);
			}
			byte[] array = new byte[num];
			errorCode = NCryptNative.UnsafeNativeMethods.NCryptDeriveKey(secretAgreement, kdf, ref ncryptBufferDesc, array, array.Length, out num, flags);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return array;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001D6D5 File Offset: 0x0001B8D5
		[SecurityCritical]
		internal static byte[] DeriveKeyMaterialHash(SafeNCryptSecretHandle secretAgreement, string hashAlgorithm, byte[] secretPrepend, byte[] secretAppend, NCryptNative.SecretAgreementFlags flags)
		{
			return NCryptNative.DeriveKeyMaterial(secretAgreement, "HASH", hashAlgorithm, null, secretPrepend, secretAppend, flags);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001D6E8 File Offset: 0x0001B8E8
		[SecurityCritical]
		internal static byte[] DeriveKeyMaterialHmac(SafeNCryptSecretHandle secretAgreement, string hashAlgorithm, byte[] hmacKey, byte[] secretPrepend, byte[] secretAppend, NCryptNative.SecretAgreementFlags flags)
		{
			return NCryptNative.DeriveKeyMaterial(secretAgreement, "HMAC", hashAlgorithm, hmacKey, secretPrepend, secretAppend, flags);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001D6FC File Offset: 0x0001B8FC
		[SecurityCritical]
		internal unsafe static byte[] DeriveKeyMaterialTls(SafeNCryptSecretHandle secretAgreement, byte[] label, byte[] seed, NCryptNative.SecretAgreementFlags flags)
		{
			NCryptNative.NCryptBuffer[] array = new NCryptNative.NCryptBuffer[2];
			byte* value;
			if (label == null || label.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &label[0];
			}
			byte* value2;
			if (seed == null || seed.Length == 0)
			{
				value2 = null;
			}
			else
			{
				value2 = &seed[0];
			}
			array[0] = new NCryptNative.NCryptBuffer
			{
				cbBuffer = label.Length,
				BufferType = NCryptNative.BufferType.KdfTlsLabel,
				pvBuffer = new IntPtr((void*)value)
			};
			array[1] = new NCryptNative.NCryptBuffer
			{
				cbBuffer = seed.Length,
				BufferType = NCryptNative.BufferType.KdfTlsSeed,
				pvBuffer = new IntPtr((void*)value2)
			};
			return NCryptNative.DeriveKeyMaterial(secretAgreement, "TLS_PRF", array, flags);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001D7B0 File Offset: 0x0001B9B0
		[SecurityCritical]
		internal static SafeNCryptSecretHandle DeriveSecretAgreement(SafeNCryptKeyHandle privateKey, SafeNCryptKeyHandle otherPartyPublicKey)
		{
			SafeNCryptSecretHandle result;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptSecretAgreement(privateKey, otherPartyPublicKey, out result, 0);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001D7D4 File Offset: 0x0001B9D4
		[SecurityCritical]
		internal static byte[] ExportKey(SafeNCryptKeyHandle key, string format)
		{
			int num = 0;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptExportKey(key, IntPtr.Zero, format, IntPtr.Zero, null, 0, out num, 0);
			if (errorCode != NCryptNative.ErrorCode.Success && errorCode != NCryptNative.ErrorCode.BufferTooSmall)
			{
				throw new CryptographicException((int)errorCode);
			}
			byte[] array = new byte[num];
			errorCode = NCryptNative.UnsafeNativeMethods.NCryptExportKey(key, IntPtr.Zero, format, IntPtr.Zero, array, array.Length, out num, 0);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return array;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001D838 File Offset: 0x0001BA38
		private static byte[] FillKeyParameter(byte[] key, int keySize)
		{
			int num = keySize / 8 + ((keySize % 8 == 0) ? 0 : 1);
			if (key.Length == num)
			{
				return key;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(key, 0, array, 0, Math.Min(key.Length, array.Length));
			return array;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001D878 File Offset: 0x0001BA78
		[SecurityCritical]
		internal static void FinalizeKey(SafeNCryptKeyHandle key)
		{
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptFinalizeKey(key, 0);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001D898 File Offset: 0x0001BA98
		[SecurityCritical]
		internal static byte[] GetProperty(SafeNCryptHandle ncryptObject, string propertyName, CngPropertyOptions propertyOptions, out bool foundProperty)
		{
			int num = 0;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptGetProperty(ncryptObject, propertyName, null, 0, out num, propertyOptions);
			if (errorCode != NCryptNative.ErrorCode.Success && errorCode != NCryptNative.ErrorCode.BufferTooSmall && errorCode != NCryptNative.ErrorCode.NotFound)
			{
				throw new CryptographicException((int)errorCode);
			}
			foundProperty = (errorCode != NCryptNative.ErrorCode.NotFound);
			byte[] array = null;
			if (errorCode != NCryptNative.ErrorCode.NotFound && num > 0)
			{
				array = new byte[num];
				errorCode = NCryptNative.UnsafeNativeMethods.NCryptGetProperty(ncryptObject, propertyName, array, array.Length, out num, propertyOptions);
				if (errorCode != NCryptNative.ErrorCode.Success)
				{
					throw new CryptographicException((int)errorCode);
				}
				foundProperty = true;
			}
			return array;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001D910 File Offset: 0x0001BB10
		[SecurityCritical]
		internal static int GetPropertyAsDWord(SafeNCryptHandle ncryptObject, string propertyName, CngPropertyOptions propertyOptions)
		{
			bool flag;
			byte[] property = NCryptNative.GetProperty(ncryptObject, propertyName, propertyOptions, out flag);
			if (!flag || property == null)
			{
				return 0;
			}
			return BitConverter.ToInt32(property, 0);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001D938 File Offset: 0x0001BB38
		[SecurityCritical]
		internal static NCryptNative.ErrorCode GetPropertyAsInt(SafeNCryptHandle ncryptObject, string propertyName, CngPropertyOptions propertyOptions, ref int propertyValue)
		{
			int num;
			return NCryptNative.UnsafeNativeMethods.NCryptGetProperty(ncryptObject, propertyName, ref propertyValue, 4, out num, propertyOptions);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001D958 File Offset: 0x0001BB58
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static IntPtr GetPropertyAsIntPtr(SafeNCryptHandle ncryptObject, string propertyName, CngPropertyOptions propertyOptions)
		{
			int size = IntPtr.Size;
			IntPtr zero = IntPtr.Zero;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptGetProperty(ncryptObject, propertyName, out zero, IntPtr.Size, out size, propertyOptions);
			if (errorCode == NCryptNative.ErrorCode.NotFound)
			{
				return IntPtr.Zero;
			}
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return zero;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001D99C File Offset: 0x0001BB9C
		[SecurityCritical]
		internal unsafe static string GetPropertyAsString(SafeNCryptHandle ncryptObject, string propertyName, CngPropertyOptions propertyOptions)
		{
			bool flag;
			byte[] property = NCryptNative.GetProperty(ncryptObject, propertyName, propertyOptions, out flag);
			if (!flag || property == null)
			{
				return null;
			}
			if (property.Length == 0)
			{
				return string.Empty;
			}
			byte[] array;
			byte* value;
			if ((array = property) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			return Marshal.PtrToStringUni(new IntPtr((void*)value));
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x0001D9EC File Offset: 0x0001BBEC
		[SecurityCritical]
		internal unsafe static T GetPropertyAsStruct<T>(SafeNCryptHandle ncryptObject, string propertyName, CngPropertyOptions propertyOptions) where T : struct
		{
			bool flag;
			byte[] property = NCryptNative.GetProperty(ncryptObject, propertyName, propertyOptions, out flag);
			if (!flag || property == null)
			{
				return Activator.CreateInstance<T>();
			}
			byte[] array;
			byte* value;
			if ((array = property) == null || array.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &array[0];
			}
			return (T)((object)Marshal.PtrToStructure(new IntPtr((void*)value), typeof(T)));
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001DA44 File Offset: 0x0001BC44
		[SecurityCritical]
		internal static SafeNCryptKeyHandle ImportKey(SafeNCryptProviderHandle provider, byte[] keyBlob, string format)
		{
			SafeNCryptKeyHandle result = null;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptImportKey(provider, IntPtr.Zero, format, IntPtr.Zero, out result, keyBlob, keyBlob.Length, 0);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x0001DA78 File Offset: 0x0001BC78
		[SecurityCritical]
		internal static SafeNCryptKeyHandle ImportKey(SafeNCryptProviderHandle provider, byte[] keyBlob, string format, IntPtr pParametersList)
		{
			SafeNCryptKeyHandle result = null;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptImportKey(provider, IntPtr.Zero, format, pParametersList, out result, keyBlob, keyBlob.Length, 0);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001DAA8 File Offset: 0x0001BCA8
		[SecurityCritical]
		internal static SafeNCryptKeyHandle OpenKey(SafeNCryptProviderHandle provider, string name, CngKeyOpenOptions options)
		{
			SafeNCryptKeyHandle result = null;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptOpenKey(provider, out result, name, 0, options);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x0001DAD0 File Offset: 0x0001BCD0
		[SecurityCritical]
		internal static SafeNCryptProviderHandle OpenStorageProvider(string providerName)
		{
			SafeNCryptProviderHandle result = null;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptOpenStorageProvider(out result, providerName, 0);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return result;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001DAF4 File Offset: 0x0001BCF4
		private static byte[] ReverseBytes(byte[] buffer)
		{
			return NCryptNative.ReverseBytes(buffer, 0, buffer.Length, false);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001DB01 File Offset: 0x0001BD01
		private static byte[] ReverseBytes(byte[] buffer, int offset, int count)
		{
			return NCryptNative.ReverseBytes(buffer, offset, count, false);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x0001DB0C File Offset: 0x0001BD0C
		private static byte[] ReverseBytes(byte[] buffer, int offset, int count, bool padWithZeroByte)
		{
			byte[] array;
			if (padWithZeroByte)
			{
				array = new byte[count + 1];
			}
			else
			{
				array = new byte[count];
			}
			int num = offset + count - 1;
			for (int i = 0; i < count; i++)
			{
				array[i] = buffer[num - i];
			}
			return array;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x0001DB49 File Offset: 0x0001BD49
		[SecurityCritical]
		internal static void SetProperty(SafeNCryptHandle ncryptObject, string propertyName, int value, CngPropertyOptions propertyOptions)
		{
			NCryptNative.SetProperty(ncryptObject, propertyName, BitConverter.GetBytes(value), propertyOptions);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001DB5C File Offset: 0x0001BD5C
		[SecurityCritical]
		internal static void SetProperty(SafeNCryptHandle ncryptObject, string propertyName, string value, CngPropertyOptions propertyOptions)
		{
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptSetProperty(ncryptObject, propertyName, value, (value.Length + 1) * 2, propertyOptions);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001DB88 File Offset: 0x0001BD88
		[SecurityCritical]
		internal unsafe static void SetProperty<T>(SafeNCryptHandle ncryptObject, string propertyName, T value, CngPropertyOptions propertyOptions) where T : struct
		{
			byte[] array = new byte[Marshal.SizeOf(typeof(T))];
			byte[] array2;
			byte* value2;
			if ((array2 = array) == null || array2.Length == 0)
			{
				value2 = null;
			}
			else
			{
				value2 = &array2[0];
			}
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					Marshal.StructureToPtr(value, new IntPtr((void*)value2), false);
					flag = true;
				}
				NCryptNative.SetProperty(ncryptObject, propertyName, array, propertyOptions);
			}
			finally
			{
				if (flag)
				{
					Marshal.DestroyStructure(new IntPtr((void*)value2), typeof(T));
				}
			}
			array2 = null;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001DC28 File Offset: 0x0001BE28
		[SecurityCritical]
		internal static void SetProperty(SafeNCryptHandle ncryptObject, string propertyName, byte[] value, CngPropertyOptions propertyOptions)
		{
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptSetProperty(ncryptObject, propertyName, value, (value != null) ? value.Length : 0, propertyOptions);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001DC54 File Offset: 0x0001BE54
		[SecurityCritical]
		internal static byte[] SignHash(SafeNCryptKeyHandle key, byte[] hash)
		{
			int num = 0;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptSignHash(key, IntPtr.Zero, hash, hash.Length, null, 0, out num, 0);
			if (errorCode != NCryptNative.ErrorCode.Success && errorCode != NCryptNative.ErrorCode.BufferTooSmall)
			{
				throw new CryptographicException((int)errorCode);
			}
			byte[] array = new byte[num];
			errorCode = NCryptNative.UnsafeNativeMethods.NCryptSignHash(key, IntPtr.Zero, hash, hash.Length, array, array.Length, out num, 0);
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			return array;
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0001DCB4 File Offset: 0x0001BEB4
		[SecurityCritical]
		internal static byte[] SignHash(SafeNCryptKeyHandle key, byte[] hash, int expectedSize)
		{
			byte[] array = new byte[expectedSize];
			int num = 0;
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptSignHash(key, IntPtr.Zero, hash, hash.Length, array, array.Length, out num, 0);
			if (errorCode == NCryptNative.ErrorCode.BufferTooSmall)
			{
				array = new byte[num];
				errorCode = NCryptNative.UnsafeNativeMethods.NCryptSignHash(key, IntPtr.Zero, hash, hash.Length, array, array.Length, out num, 0);
			}
			if (errorCode != NCryptNative.ErrorCode.Success)
			{
				throw new CryptographicException((int)errorCode);
			}
			Array.Resize<byte>(ref array, num);
			return array;
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x0001DD1C File Offset: 0x0001BF1C
		internal static void UnpackEccPublicBlob(byte[] blob, out BigInteger x, out BigInteger y)
		{
			int num = BitConverter.ToInt32(blob, 4);
			x = new BigInteger(NCryptNative.ReverseBytes(blob, 8, num, true));
			y = new BigInteger(NCryptNative.ReverseBytes(blob, 8 + num, num, true));
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x0001DD5C File Offset: 0x0001BF5C
		[SecurityCritical]
		internal static bool VerifySignature(SafeNCryptKeyHandle key, byte[] hash, byte[] signature)
		{
			NCryptNative.ErrorCode errorCode = NCryptNative.UnsafeNativeMethods.NCryptVerifySignature(key, IntPtr.Zero, hash, hash.Length, signature, signature.Length, 0);
			return errorCode == NCryptNative.ErrorCode.Success;
		}

		// Token: 0x040006A3 RID: 1699
		private static volatile bool s_haveNcryptSupported;

		// Token: 0x040006A4 RID: 1700
		private static volatile bool s_ncryptSupported;

		// Token: 0x02000348 RID: 840
		internal enum BufferType
		{
			// Token: 0x04000F00 RID: 3840
			KdfHashAlgorithm,
			// Token: 0x04000F01 RID: 3841
			KdfSecretPrepend,
			// Token: 0x04000F02 RID: 3842
			KdfSecretAppend,
			// Token: 0x04000F03 RID: 3843
			KdfHmacKey,
			// Token: 0x04000F04 RID: 3844
			KdfTlsLabel,
			// Token: 0x04000F05 RID: 3845
			KdfTlsSeed
		}

		// Token: 0x02000349 RID: 841
		internal enum ErrorCode
		{
			// Token: 0x04000F07 RID: 3847
			Success,
			// Token: 0x04000F08 RID: 3848
			BadSignature = -2146893818,
			// Token: 0x04000F09 RID: 3849
			NotFound = -2146893807,
			// Token: 0x04000F0A RID: 3850
			KeyDoesNotExist = -2146893802,
			// Token: 0x04000F0B RID: 3851
			BufferTooSmall = -2146893784,
			// Token: 0x04000F0C RID: 3852
			NoMoreItems = -2146893782
		}

		// Token: 0x0200034A RID: 842
		internal static class KeyPropertyName
		{
			// Token: 0x04000F0D RID: 3853
			internal const string Algorithm = "Algorithm Name";

			// Token: 0x04000F0E RID: 3854
			internal const string AlgorithmGroup = "Algorithm Group";

			// Token: 0x04000F0F RID: 3855
			internal const string ExportPolicy = "Export Policy";

			// Token: 0x04000F10 RID: 3856
			internal const string KeyType = "Key Type";

			// Token: 0x04000F11 RID: 3857
			internal const string KeyUsage = "Key Usage";

			// Token: 0x04000F12 RID: 3858
			internal const string Length = "Length";

			// Token: 0x04000F13 RID: 3859
			internal const string Name = "Name";

			// Token: 0x04000F14 RID: 3860
			internal const string ParentWindowHandle = "HWND Handle";

			// Token: 0x04000F15 RID: 3861
			internal const string PublicKeyLength = "PublicKeyLength";

			// Token: 0x04000F16 RID: 3862
			internal const string ProviderHandle = "Provider Handle";

			// Token: 0x04000F17 RID: 3863
			internal const string UIPolicy = "UI Policy";

			// Token: 0x04000F18 RID: 3864
			internal const string UniqueName = "Unique Name";

			// Token: 0x04000F19 RID: 3865
			internal const string UseContext = "Use Context";

			// Token: 0x04000F1A RID: 3866
			internal const string ClrIsEphemeral = "CLR IsEphemeral";
		}

		// Token: 0x0200034B RID: 843
		internal static class ProviderPropertyName
		{
			// Token: 0x04000F1B RID: 3867
			internal const string Name = "Name";
		}

		// Token: 0x0200034C RID: 844
		[Flags]
		internal enum SecretAgreementFlags
		{
			// Token: 0x04000F1D RID: 3869
			None = 0,
			// Token: 0x04000F1E RID: 3870
			UseSecretAsHmacKey = 1
		}

		// Token: 0x0200034D RID: 845
		internal struct NCRYPT_UI_POLICY
		{
			// Token: 0x04000F1F RID: 3871
			public int dwVersion;

			// Token: 0x04000F20 RID: 3872
			public CngUIProtectionLevels dwFlags;

			// Token: 0x04000F21 RID: 3873
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszCreationTitle;

			// Token: 0x04000F22 RID: 3874
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszFriendlyName;

			// Token: 0x04000F23 RID: 3875
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pszDescription;
		}

		// Token: 0x0200034E RID: 846
		internal struct NCryptBuffer
		{
			// Token: 0x04000F24 RID: 3876
			public int cbBuffer;

			// Token: 0x04000F25 RID: 3877
			public NCryptNative.BufferType BufferType;

			// Token: 0x04000F26 RID: 3878
			public IntPtr pvBuffer;
		}

		// Token: 0x0200034F RID: 847
		internal struct NCryptBufferDesc
		{
			// Token: 0x04000F27 RID: 3879
			public int ulVersion;

			// Token: 0x04000F28 RID: 3880
			public int cBuffers;

			// Token: 0x04000F29 RID: 3881
			public IntPtr pBuffers;
		}

		// Token: 0x02000350 RID: 848
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical(SecurityCriticalScope.Everything)]
		internal static class UnsafeNativeMethods
		{
			// Token: 0x06001B48 RID: 6984
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptCreatePersistedKey(SafeNCryptProviderHandle hProvider, out SafeNCryptKeyHandle phKey, string pszAlgId, string pszKeyName, int dwLegacyKeySpec, CngKeyCreationOptions dwFlags);

			// Token: 0x06001B49 RID: 6985
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptDeleteKey(SafeNCryptKeyHandle hKey, int flags);

			// Token: 0x06001B4A RID: 6986
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptDeriveKey(SafeNCryptSecretHandle hSharedSecret, string pwszKDF, [In] ref NCryptNative.NCryptBufferDesc pParameterList, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbDerivedKey, int cbDerivedKey, out int pcbResult, NCryptNative.SecretAgreementFlags dwFlags);

			// Token: 0x06001B4B RID: 6987
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptExportKey(SafeNCryptKeyHandle hKey, IntPtr hExportKey, string pszBlobType, IntPtr pParameterList, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, int dwFlags);

			// Token: 0x06001B4C RID: 6988
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptFinalizeKey(SafeNCryptKeyHandle hKey, int dwFlags);

			// Token: 0x06001B4D RID: 6989
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptGetProperty(SafeNCryptHandle hObject, string pszProperty, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, CngPropertyOptions dwFlags);

			// Token: 0x06001B4E RID: 6990
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptGetProperty(SafeNCryptHandle hObject, string pszProperty, ref int pbOutput, int cbOutput, out int pcbResult, CngPropertyOptions dwFlags);

			// Token: 0x06001B4F RID: 6991
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptGetProperty(SafeNCryptHandle hObject, string pszProperty, out IntPtr pbOutput, int cbOutput, out int pcbResult, CngPropertyOptions dwFlags);

			// Token: 0x06001B50 RID: 6992
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptImportKey(SafeNCryptProviderHandle hProvider, IntPtr hImportKey, string pszBlobType, IntPtr pParameterList, out SafeNCryptKeyHandle phKey, [MarshalAs(UnmanagedType.LPArray)] byte[] pbData, int cbData, int dwFlags);

			// Token: 0x06001B51 RID: 6993
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptOpenKey(SafeNCryptProviderHandle hProvider, out SafeNCryptKeyHandle phKey, string pszKeyName, int dwLegacyKeySpec, CngKeyOpenOptions dwFlags);

			// Token: 0x06001B52 RID: 6994
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptOpenStorageProvider(out SafeNCryptProviderHandle phProvider, string pszProviderName, int dwFlags);

			// Token: 0x06001B53 RID: 6995
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptSecretAgreement(SafeNCryptKeyHandle hPrivKey, SafeNCryptKeyHandle hPubKey, out SafeNCryptSecretHandle phSecret, int dwFlags);

			// Token: 0x06001B54 RID: 6996
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptSetProperty(SafeNCryptHandle hObject, string pszProperty, [MarshalAs(UnmanagedType.LPArray)] byte[] pbInput, int cbInput, CngPropertyOptions dwFlags);

			// Token: 0x06001B55 RID: 6997
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptSetProperty(SafeNCryptHandle hObject, string pszProperty, string pbInput, int cbInput, CngPropertyOptions dwFlags);

			// Token: 0x06001B56 RID: 6998
			[DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
			internal static extern NCryptNative.ErrorCode NCryptSetProperty(SafeNCryptHandle hObject, string pszProperty, IntPtr pbInput, int cbInput, CngPropertyOptions dwFlags);

			// Token: 0x06001B57 RID: 6999
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptSignHash(SafeNCryptKeyHandle hKey, IntPtr pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] byte[] pbHashValue, int cbHashValue, [MarshalAs(UnmanagedType.LPArray)] byte[] pbSignature, int cbSignature, out int pcbResult, int dwFlags);

			// Token: 0x06001B58 RID: 7000
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptVerifySignature(SafeNCryptKeyHandle hKey, IntPtr pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] byte[] pbHashValue, int cbHashValue, [MarshalAs(UnmanagedType.LPArray)] byte[] pbSignature, int cbSignature, int dwFlags);

			// Token: 0x06001B59 RID: 7001
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptSignHash(SafeNCryptKeyHandle hKey, [In] ref BCryptNative.BCRYPT_PKCS1_PADDING_INFO pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbHashValue, int cbHashValue, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbSignature, int cbSignature, out int pcbResult, AsymmetricPaddingMode dwFlags);

			// Token: 0x06001B5A RID: 7002
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptSignHash(SafeNCryptKeyHandle hKey, [In] ref BCryptNative.BCRYPT_PSS_PADDING_INFO pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbHashValue, int cbHashValue, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbSignature, int cbSignature, out int pcbResult, AsymmetricPaddingMode dwFlags);

			// Token: 0x06001B5B RID: 7003
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptVerifySignature(SafeNCryptKeyHandle hKey, [In] ref BCryptNative.BCRYPT_PKCS1_PADDING_INFO pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbHashValue, int cbHashValue, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSignature, int cbSignature, AsymmetricPaddingMode dwFlags);

			// Token: 0x06001B5C RID: 7004
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptVerifySignature(SafeNCryptKeyHandle hKey, [In] ref BCryptNative.BCRYPT_PSS_PADDING_INFO pPaddingInfo, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbHashValue, int cbHashValue, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSignature, int cbSignature, AsymmetricPaddingMode dwFlags);

			// Token: 0x06001B5D RID: 7005
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptDecrypt(SafeNCryptKeyHandle hKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, [In] ref BCryptNative.BCRYPT_OAEP_PADDING_INFO pvPadding, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, AsymmetricPaddingMode dwFlags);

			// Token: 0x06001B5E RID: 7006
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptDecrypt(SafeNCryptKeyHandle hKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, IntPtr pvPaddingZero, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, AsymmetricPaddingMode dwFlags);

			// Token: 0x06001B5F RID: 7007
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptEncrypt(SafeNCryptKeyHandle hKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, [In] ref BCryptNative.BCRYPT_OAEP_PADDING_INFO pvPadding, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, AsymmetricPaddingMode dwFlags);

			// Token: 0x06001B60 RID: 7008
			[DllImport("ncrypt.dll")]
			internal static extern NCryptNative.ErrorCode NCryptEncrypt(SafeNCryptKeyHandle hKey, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbInput, int cbInput, IntPtr pvPaddingZero, [MarshalAs(UnmanagedType.LPArray)] [Out] byte[] pbOutput, int cbOutput, out int pcbResult, AsymmetricPaddingMode dwFlags);
		}

		// Token: 0x02000351 RID: 849
		// (Invoke) Token: 0x06001B62 RID: 7010
		[SecuritySafeCritical]
		private delegate NCryptNative.ErrorCode NCryptDecryptor<T>(SafeNCryptKeyHandle hKey, byte[] pbInput, int cbInput, ref T pvPadding, byte[] pbOutput, int cbOutput, out int pcbResult, AsymmetricPaddingMode dwFlags);

		// Token: 0x02000352 RID: 850
		// (Invoke) Token: 0x06001B66 RID: 7014
		[SecuritySafeCritical]
		private delegate NCryptNative.ErrorCode NCryptEncryptor<T>(SafeNCryptKeyHandle hKey, byte[] pbInput, int cbInput, ref T pvPadding, byte[] pbOutput, int cbOutput, out int pcbResult, AsymmetricPaddingMode dwFlags);

		// Token: 0x02000353 RID: 851
		// (Invoke) Token: 0x06001B6A RID: 7018
		[SecuritySafeCritical]
		private delegate NCryptNative.ErrorCode NCryptHashSigner<T>(SafeNCryptKeyHandle hKey, ref T pvPaddingInfo, byte[] pbHashValue, int cbHashValue, byte[] pbSignature, int cbSignature, out int pcbResult, AsymmetricPaddingMode dwFlags);

		// Token: 0x02000354 RID: 852
		// (Invoke) Token: 0x06001B6E RID: 7022
		[SecuritySafeCritical]
		private delegate NCryptNative.ErrorCode NCryptSignatureVerifier<T>(SafeNCryptKeyHandle hKey, ref T pvPaddingInfo, byte[] pbHashValue, int cbHashValue, byte[] pbSignature, int cbSignature, AsymmetricPaddingMode dwFlags) where T : struct;
	}
}
