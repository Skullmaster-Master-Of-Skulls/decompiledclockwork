using System;
using System.Runtime.InteropServices;
using Internal.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography
{
	// Token: 0x020000F7 RID: 247
	internal static class ECCng
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x0001A1F4 File Offset: 0x000183F4
		internal static ECParameters ExportExplicitParameters(CngKey key, bool includePrivateParameters)
		{
			ECParameters result = default(ECParameters);
			ECCng.ExportExplicitParameters(key, includePrivateParameters, ref result);
			return result;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001A214 File Offset: 0x00018414
		internal static void ExportExplicitParameters(CngKey key, bool includePrivateParameters, ref ECParameters ecparams)
		{
			byte[] ecBlob = ECCng.ExportFullKeyBlob(key, includePrivateParameters);
			ECCng.ExportPrimeCurveParameters(ref ecparams, ecBlob, includePrivateParameters);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001A234 File Offset: 0x00018434
		internal static ECParameters ExportParameters(CngKey key, bool includePrivateParameters)
		{
			ECParameters result = default(ECParameters);
			ECCng.ExportParameters(key, includePrivateParameters, ref result);
			return result;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001A254 File Offset: 0x00018454
		internal static void ExportParameters(CngKey key, bool includePrivateParameters, ref ECParameters ecparams)
		{
			string curveName = key.GetCurveName();
			if (string.IsNullOrEmpty(curveName))
			{
				byte[] ecBlob = ECCng.ExportFullKeyBlob(key, includePrivateParameters);
				ECCng.ExportPrimeCurveParameters(ref ecparams, ecBlob, includePrivateParameters);
				return;
			}
			byte[] ecBlob2 = ECCng.ExportKeyBlob(key, includePrivateParameters);
			ECCng.ExportNamedCurveParameters(ref ecparams, ecBlob2, includePrivateParameters);
			ecparams.Curve = ECCurve.CreateFromFriendlyName(curveName);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001A2A0 File Offset: 0x000184A0
		internal static byte[] ExportKeyBlob(CngKey key, bool includePrivateParameters, out CngKeyBlobFormat format, out string curveName)
		{
			curveName = key.GetCurveName();
			bool flag = false;
			if (string.IsNullOrEmpty(curveName))
			{
				curveName = null;
				flag = true;
				format = (includePrivateParameters ? CngKeyBlobFormat.EccFullPrivateBlob : CngKeyBlobFormat.EccFullPublicBlob);
			}
			else
			{
				format = (includePrivateParameters ? CngKeyBlobFormat.EccPrivateBlob : CngKeyBlobFormat.EccPublicBlob);
			}
			byte[] array = key.Export(format);
			if (flag)
			{
				ECCng.FixupGenericBlob(array);
			}
			return array;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001A2FC File Offset: 0x000184FC
		internal static CngKey ImportECDsaParameters(ref ECParameters ecparams)
		{
			CngKeyBlobFormat format;
			string curveName;
			byte[] blob = ECCng.ECDsaParametersToBlob(ref ecparams, out format, out curveName);
			return ECCng.ImportKeyBlob(blob, curveName, format, ecparams.Curve.CurveType);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001A328 File Offset: 0x00018528
		internal static CngKey ImportEcdhParameters(ref ECParameters ecparams)
		{
			CngKeyBlobFormat format;
			string curveName;
			byte[] blob = ECCng.EcdhParametersToBlob(ref ecparams, out format, out curveName);
			return ECCng.ImportKeyBlob(blob, curveName, format, ecparams.Curve.CurveType);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001A353 File Offset: 0x00018553
		internal static byte[] ECDsaParametersToBlob(ref ECParameters parameters, out CngKeyBlobFormat format, out string curveName)
		{
			return ECCng.ParametersToBlob(ref parameters, ECCng.s_ecdsaNamedMagicResolver, ECCng.s_ecdsaExplicitMagicResolver, out format, out curveName);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001A367 File Offset: 0x00018567
		internal static byte[] EcdhParametersToBlob(ref ECParameters parameters, out CngKeyBlobFormat format, out string curveName)
		{
			return ECCng.ParametersToBlob(ref parameters, ECCng.s_ecdhNamedMagicResolver, ECCng.s_ecdhExplicitMagicResolver, out format, out curveName);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001A37C File Offset: 0x0001857C
		[SecuritySafeCritical]
		internal static SafeNCryptKeyHandle ImportKeyBlob(string blobType, byte[] keyBlob, string curveName, SafeNCryptProviderHandle provider)
		{
			Interop.BCrypt.BCryptBufferDesc bcryptBufferDesc = default(Interop.BCrypt.BCryptBufferDesc);
			Interop.BCrypt.BCryptBuffer bcryptBuffer = default(Interop.BCrypt.BCryptBuffer);
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			SafeNCryptKeyHandle result;
			try
			{
				intPtr3 = Marshal.StringToHGlobalUni(curveName);
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(bcryptBufferDesc));
				intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(bcryptBuffer));
				bcryptBuffer.cbBuffer = (curveName.Length + 1) * 2;
				bcryptBuffer.BufferType = Interop.BCrypt.NCryptBufferDescriptors.NCRYPTBUFFER_ECC_CURVE_NAME;
				bcryptBuffer.pvBuffer = intPtr3;
				Marshal.StructureToPtr(bcryptBuffer, intPtr2, false);
				bcryptBufferDesc.cBuffers = 1;
				bcryptBufferDesc.pBuffers = intPtr2;
				bcryptBufferDesc.ulVersion = 0;
				Marshal.StructureToPtr(bcryptBufferDesc, intPtr, false);
				result = NCryptNative.ImportKey(provider, keyBlob, blobType, intPtr);
			}
			catch (CryptographicException ex)
			{
				if (ex.HResult == -2146893785)
				{
					throw new PlatformNotSupportedException(SR.GetString("Cryptography_CurveNotSupported", new object[]
					{
						curveName
					}), ex);
				}
				throw;
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
				Marshal.FreeHGlobal(intPtr2);
				Marshal.FreeHGlobal(intPtr3);
			}
			return result;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001A49C File Offset: 0x0001869C
		private static Interop.BCrypt.KeyBlobMagicNumber ECDsaCurveNameToMagicNumber(string name, bool includePrivateParameters)
		{
			string algorithm = CngKey.EcdsaCurveNameToAlgorithm(name).Algorithm;
			if (!(algorithm == "ECDSA_P256"))
			{
				if (!(algorithm == "ECDSA_P384"))
				{
					if (!(algorithm == "ECDSA_P521"))
					{
						if (!includePrivateParameters)
						{
							return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_GENERIC_MAGIC;
						}
						return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_GENERIC_MAGIC;
					}
					else
					{
						if (!includePrivateParameters)
						{
							return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P521_MAGIC;
						}
						return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P521_MAGIC;
					}
				}
				else
				{
					if (!includePrivateParameters)
					{
						return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P384_MAGIC;
					}
					return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P384_MAGIC;
				}
			}
			else
			{
				if (!includePrivateParameters)
				{
					return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P256_MAGIC;
				}
				return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P256_MAGIC;
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001A51C File Offset: 0x0001871C
		private static Interop.BCrypt.KeyBlobMagicNumber EcdhCurveNameToMagicNumber(string name, bool includePrivateParameters)
		{
			string algorithm = CngKey.EcdhCurveNameToAlgorithm(name).Algorithm;
			if (!(algorithm == "ECDH_P256"))
			{
				if (!(algorithm == "ECDH_P384"))
				{
					if (!(algorithm == "ECDH_P521"))
					{
						if (!includePrivateParameters)
						{
							return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_GENERIC_MAGIC;
						}
						return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_GENERIC_MAGIC;
					}
					else
					{
						if (!includePrivateParameters)
						{
							return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P521_MAGIC;
						}
						return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P521_MAGIC;
					}
				}
				else
				{
					if (!includePrivateParameters)
					{
						return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P384_MAGIC;
					}
					return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P384_MAGIC;
				}
			}
			else
			{
				if (!includePrivateParameters)
				{
					return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P256_MAGIC;
				}
				return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P256_MAGIC;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001A59C File Offset: 0x0001879C
		[SecuritySafeCritical]
		private unsafe static byte[] GetNamedCurveBlob(ref ECParameters parameters, Func<string, bool, Interop.BCrypt.KeyBlobMagicNumber> magicResolver)
		{
			bool flag = parameters.D != null;
			int num = sizeof(Interop.BCrypt.BCRYPT_ECCKEY_BLOB) + parameters.Q.X.Length + parameters.Q.Y.Length;
			if (flag)
			{
				num += parameters.D.Length;
			}
			byte[] array = new byte[num];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Interop.BCrypt.BCRYPT_ECCKEY_BLOB* ptr2 = (Interop.BCrypt.BCRYPT_ECCKEY_BLOB*)ptr;
			ptr2->Magic = magicResolver(parameters.Curve.Oid.FriendlyName, flag);
			ptr2->cbKey = parameters.Q.X.Length;
			array2 = null;
			int num2 = sizeof(Interop.BCrypt.BCRYPT_ECCKEY_BLOB);
			Interop.BCrypt.Emit(array, ref num2, parameters.Q.X);
			Interop.BCrypt.Emit(array, ref num2, parameters.Q.Y);
			if (flag)
			{
				Interop.BCrypt.Emit(array, ref num2, parameters.D);
			}
			return array;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001A680 File Offset: 0x00018880
		[SecuritySafeCritical]
		private unsafe static byte[] GetPrimeCurveBlob(ref ECParameters parameters, Func<bool, Interop.BCrypt.KeyBlobMagicNumber> magicResolver)
		{
			bool flag = parameters.D != null;
			ECCurve curve = parameters.Curve;
			int num = sizeof(Interop.BCrypt.BCRYPT_ECCFULLKEY_BLOB) + curve.Prime.Length + curve.A.Length + curve.B.Length + curve.G.X.Length + curve.G.Y.Length + curve.Order.Length + curve.Cofactor.Length + ((curve.Seed == null) ? 0 : curve.Seed.Length) + parameters.Q.X.Length + parameters.Q.Y.Length;
			if (flag)
			{
				num += parameters.D.Length;
			}
			byte[] array = new byte[num];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Interop.BCrypt.BCRYPT_ECCFULLKEY_BLOB* ptr2 = (Interop.BCrypt.BCRYPT_ECCFULLKEY_BLOB*)ptr;
			ptr2->Version = 1;
			ptr2->Magic = magicResolver(flag);
			ptr2->cbCofactor = curve.Cofactor.Length;
			ptr2->cbFieldLength = parameters.Q.X.Length;
			ptr2->cbSeed = ((curve.Seed == null) ? 0 : curve.Seed.Length);
			ptr2->cbSubgroupOrder = curve.Order.Length;
			ptr2->CurveGenerationAlgId = ECCng.GetHashAlgorithmId(curve.Hash);
			ptr2->CurveType = ECCng.ConvertToCurveTypeEnum(curve.CurveType);
			array2 = null;
			int num2 = sizeof(Interop.BCrypt.BCRYPT_ECCFULLKEY_BLOB);
			Interop.BCrypt.Emit(array, ref num2, curve.Prime);
			Interop.BCrypt.Emit(array, ref num2, curve.A);
			Interop.BCrypt.Emit(array, ref num2, curve.B);
			Interop.BCrypt.Emit(array, ref num2, curve.G.X);
			Interop.BCrypt.Emit(array, ref num2, curve.G.Y);
			Interop.BCrypt.Emit(array, ref num2, curve.Order);
			Interop.BCrypt.Emit(array, ref num2, curve.Cofactor);
			if (curve.Seed != null)
			{
				Interop.BCrypt.Emit(array, ref num2, curve.Seed);
			}
			Interop.BCrypt.Emit(array, ref num2, parameters.Q.X);
			Interop.BCrypt.Emit(array, ref num2, parameters.Q.Y);
			if (flag)
			{
				Interop.BCrypt.Emit(array, ref num2, parameters.D);
			}
			return array;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		[SecuritySafeCritical]
		private unsafe static void ExportNamedCurveParameters(ref ECParameters ecParams, byte[] ecBlob, bool includePrivateParameters)
		{
			Interop.BCrypt.KeyBlobMagicNumber magic = (Interop.BCrypt.KeyBlobMagicNumber)BitConverter.ToInt32(ecBlob, 0);
			ECCng.CheckMagicValueOfKey(magic, includePrivateParameters);
			if (ecBlob.Length < sizeof(Interop.BCrypt.BCRYPT_ECCKEY_BLOB))
			{
				throw Interop.NCrypt.ErrorCode.E_FAIL.ToCryptographicException();
			}
			fixed (byte[] array = ecBlob)
			{
				byte* ptr;
				if (ecBlob == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Interop.BCrypt.BCRYPT_ECCKEY_BLOB* ptr2 = (Interop.BCrypt.BCRYPT_ECCKEY_BLOB*)ptr;
				int num = sizeof(Interop.BCrypt.BCRYPT_ECCKEY_BLOB);
				ecParams.Q = new ECPoint
				{
					X = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbKey),
					Y = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbKey)
				};
				if (includePrivateParameters)
				{
					ecParams.D = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbKey);
				}
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001A94C File Offset: 0x00018B4C
		[SecuritySafeCritical]
		internal unsafe static void ExportPrimeCurveParameters(ref ECParameters ecParams, byte[] ecBlob, bool includePrivateParameters)
		{
			Interop.BCrypt.KeyBlobMagicNumber magic = (Interop.BCrypt.KeyBlobMagicNumber)BitConverter.ToInt32(ecBlob, 0);
			ECCng.CheckMagicValueOfKey(magic, includePrivateParameters);
			if (ecBlob.Length < sizeof(Interop.BCrypt.BCRYPT_ECCFULLKEY_BLOB))
			{
				throw Interop.NCrypt.ErrorCode.E_FAIL.ToCryptographicException();
			}
			fixed (byte[] array = ecBlob)
			{
				byte* ptr;
				if (ecBlob == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				Interop.BCrypt.BCRYPT_ECCFULLKEY_BLOB* ptr2 = (Interop.BCrypt.BCRYPT_ECCFULLKEY_BLOB*)ptr;
				ECCurve curve = default(ECCurve);
				curve.CurveType = ECCng.ConvertToCurveTypeEnum(ptr2->CurveType);
				curve.Hash = ECCng.GetHashAlgorithmName(ptr2->CurveGenerationAlgId);
				int num = sizeof(Interop.BCrypt.BCRYPT_ECCFULLKEY_BLOB);
				curve.Prime = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbFieldLength);
				curve.A = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbFieldLength);
				curve.B = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbFieldLength);
				curve.G = new ECPoint
				{
					X = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbFieldLength),
					Y = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbFieldLength)
				};
				curve.Order = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbSubgroupOrder);
				curve.Cofactor = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbCofactor);
				curve.Seed = ((ptr2->cbSeed == 0) ? null : Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbSeed));
				ecParams.Q = new ECPoint
				{
					X = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbFieldLength),
					Y = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbFieldLength)
				};
				if (includePrivateParameters)
				{
					ecParams.D = Interop.BCrypt.Consume(ecBlob, ref num, ptr2->cbSubgroupOrder);
				}
				ecParams.Curve = curve;
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001AAEC File Offset: 0x00018CEC
		[SecuritySafeCritical]
		internal unsafe static byte[] GetPrimeCurveParameterBlob(ref ECCurve curve)
		{
			int num = sizeof(Interop.BCrypt.BCRYPT_ECC_PARAMETER_HEADER) + curve.Prime.Length + curve.A.Length + curve.B.Length + curve.G.X.Length + curve.G.Y.Length + curve.Order.Length + curve.Cofactor.Length + ((curve.Seed == null) ? 0 : curve.Seed.Length);
			byte[] array = new byte[num];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Interop.BCrypt.BCRYPT_ECC_PARAMETER_HEADER* ptr2 = (Interop.BCrypt.BCRYPT_ECC_PARAMETER_HEADER*)ptr;
			ptr2->Version = 1;
			ptr2->cbCofactor = curve.Cofactor.Length;
			ptr2->cbFieldLength = curve.A.Length;
			ptr2->cbSeed = ((curve.Seed == null) ? 0 : curve.Seed.Length);
			ptr2->cbSubgroupOrder = curve.Order.Length;
			ptr2->CurveGenerationAlgId = ECCng.GetHashAlgorithmId(curve.Hash);
			ptr2->CurveType = ECCng.ConvertToCurveTypeEnum(curve.CurveType);
			array2 = null;
			int num2 = sizeof(Interop.BCrypt.BCRYPT_ECC_PARAMETER_HEADER);
			Interop.BCrypt.Emit(array, ref num2, curve.Prime);
			Interop.BCrypt.Emit(array, ref num2, curve.A);
			Interop.BCrypt.Emit(array, ref num2, curve.B);
			Interop.BCrypt.Emit(array, ref num2, curve.G.X);
			Interop.BCrypt.Emit(array, ref num2, curve.G.Y);
			Interop.BCrypt.Emit(array, ref num2, curve.Order);
			Interop.BCrypt.Emit(array, ref num2, curve.Cofactor);
			if (curve.Seed != null)
			{
				Interop.BCrypt.Emit(array, ref num2, curve.Seed);
			}
			return array;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001AC82 File Offset: 0x00018E82
		private static void CheckMagicValueOfKey(Interop.BCrypt.KeyBlobMagicNumber magic, bool includePrivateParameters)
		{
			if (includePrivateParameters)
			{
				if (!ECCng.IsMagicValueOfKeyPrivate(magic))
				{
					throw new CryptographicException(SR.GetString("Cryptography_NotValidPrivateKey"));
				}
			}
			else if (!ECCng.IsMagicValueOfKeyPublic(magic))
			{
				throw new CryptographicException(SR.GetString("Cryptography_NotValidPublicOrPrivateKey"));
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001ACB8 File Offset: 0x00018EB8
		private static bool IsMagicValueOfKeyPrivate(Interop.BCrypt.KeyBlobMagicNumber magic)
		{
			if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P384_MAGIC)
			{
				if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P256_MAGIC)
				{
					if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P256_MAGIC && magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P256_MAGIC)
					{
						return false;
					}
				}
				else if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P384_MAGIC && magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P384_MAGIC)
				{
					return false;
				}
			}
			else if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P521_MAGIC)
			{
				if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P521_MAGIC && magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P521_MAGIC)
				{
					return false;
				}
			}
			else if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_GENERIC_MAGIC && magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_GENERIC_MAGIC)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001AD28 File Offset: 0x00018F28
		private static bool IsMagicValueOfKeyPublic(Interop.BCrypt.KeyBlobMagicNumber magic)
		{
			if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P384_MAGIC)
			{
				if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P256_MAGIC)
				{
					if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P256_MAGIC && magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P256_MAGIC)
					{
						goto IL_60;
					}
				}
				else if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P384_MAGIC && magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P384_MAGIC)
				{
					goto IL_60;
				}
			}
			else if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P521_MAGIC)
			{
				if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P521_MAGIC && magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P521_MAGIC)
				{
					goto IL_60;
				}
			}
			else if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_GENERIC_MAGIC && magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_GENERIC_MAGIC)
			{
				goto IL_60;
			}
			return true;
			IL_60:
			return ECCng.IsMagicValueOfKeyPrivate(magic);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001AD9B File Offset: 0x00018F9B
		private static Interop.BCrypt.ECC_CURVE_TYPE_ENUM ConvertToCurveTypeEnum(ECCurve.ECCurveType value)
		{
			return (Interop.BCrypt.ECC_CURVE_TYPE_ENUM)value;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001ADA0 File Offset: 0x00018FA0
		private static ECCurve.ECCurveType ConvertToCurveTypeEnum(Interop.BCrypt.ECC_CURVE_TYPE_ENUM value)
		{
			return (ECCurve.ECCurveType)value;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001ADB0 File Offset: 0x00018FB0
		private static Interop.BCrypt.ECC_CURVE_ALG_ID_ENUM GetHashAlgorithmId(HashAlgorithmName? name)
		{
			if (name == null || string.IsNullOrEmpty(name.Value.Name))
			{
				return Interop.BCrypt.ECC_CURVE_ALG_ID_ENUM.BCRYPT_NO_CURVE_GENERATION_ALG_ID;
			}
			Interop.Crypt32.CRYPT_OID_INFO crypt_OID_INFO = Interop.Crypt32.FindOidInfo(Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_NAME_KEY, name.Value.Name, OidGroup.HashAlgorithm, false);
			if (crypt_OID_INFO.AlgId == -1)
			{
				throw new CryptographicException(SR.GetString("Cryptography_UnknownHashAlgorithm", new object[]
				{
					name.Value.Name
				}));
			}
			return (Interop.BCrypt.ECC_CURVE_ALG_ID_ENUM)crypt_OID_INFO.AlgId;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001AE30 File Offset: 0x00019030
		private static HashAlgorithmName? GetHashAlgorithmName(Interop.BCrypt.ECC_CURVE_ALG_ID_ENUM hashId)
		{
			Interop.Crypt32.CRYPT_OID_INFO crypt_OID_INFO = Interop.Crypt32.FindAlgIdOidInfo((int)hashId);
			if (crypt_OID_INFO.AlgId == -1)
			{
				return null;
			}
			return new HashAlgorithmName?(new HashAlgorithmName(crypt_OID_INFO.Name));
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001AE68 File Offset: 0x00019068
		[SecuritySafeCritical]
		private unsafe static void FixupGenericBlob(byte[] blob)
		{
			if (blob.Length > sizeof(Interop.BCrypt.BCRYPT_ECCKEY_BLOB))
			{
				fixed (byte[] array = blob)
				{
					byte* ptr;
					if (blob == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					Interop.BCrypt.BCRYPT_ECCKEY_BLOB* ptr2 = (Interop.BCrypt.BCRYPT_ECCKEY_BLOB*)ptr;
					Interop.BCrypt.KeyBlobMagicNumber magic = ptr2->Magic;
					if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P384_MAGIC)
					{
						if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P256_MAGIC)
						{
							if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P256_MAGIC)
							{
								if (magic == Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P256_MAGIC)
								{
									goto IL_CC;
								}
								if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P256_MAGIC)
								{
									goto IL_E4;
								}
								goto IL_BF;
							}
						}
						else
						{
							if (magic == Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P256_MAGIC)
							{
								goto IL_D9;
							}
							if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P384_MAGIC)
							{
								if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P384_MAGIC)
								{
									goto IL_E4;
								}
								goto IL_CC;
							}
						}
					}
					else if (magic <= Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P521_MAGIC)
					{
						if (magic == Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P384_MAGIC)
						{
							goto IL_BF;
						}
						if (magic == Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P384_MAGIC)
						{
							goto IL_D9;
						}
						if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_P521_MAGIC)
						{
							goto IL_E4;
						}
					}
					else
					{
						if (magic == Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_P521_MAGIC)
						{
							goto IL_CC;
						}
						if (magic == Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_P521_MAGIC)
						{
							goto IL_BF;
						}
						if (magic != Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_P521_MAGIC)
						{
							goto IL_E4;
						}
						goto IL_D9;
					}
					ptr2->Magic = Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_GENERIC_MAGIC;
					goto IL_E4;
					IL_BF:
					ptr2->Magic = Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_GENERIC_MAGIC;
					goto IL_E4;
					IL_CC:
					ptr2->Magic = Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_GENERIC_MAGIC;
					goto IL_E4;
					IL_D9:
					ptr2->Magic = Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_GENERIC_MAGIC;
					IL_E4:;
				}
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001AF5C File Offset: 0x0001915C
		private static CngKey ImportKeyBlob(byte[] blob, string curveName, CngKeyBlobFormat format, ECCurve.ECCurveType curveType)
		{
			CngKey result;
			try
			{
				CngKey cngKey = CngKey.Import(blob, curveName, format);
				cngKey.ExportPolicy |= CngExportPolicies.AllowPlaintextExport;
				result = cngKey;
			}
			catch (CryptographicException ex)
			{
				if (curveType != ECCurve.ECCurveType.Named && ex.HResult == -2146893783)
				{
					throw new PlatformNotSupportedException(SR.GetString("Cryptography_CurveNotSupported", new object[]
					{
						curveType
					}), ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001AFCC File Offset: 0x000191CC
		private static byte[] ExportKeyBlob(CngKey key, bool includePrivateParameters)
		{
			CngKeyBlobFormat format = includePrivateParameters ? CngKeyBlobFormat.EccPrivateBlob : CngKeyBlobFormat.EccPublicBlob;
			return key.Export(format);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001AFF0 File Offset: 0x000191F0
		private static byte[] ExportFullKeyBlob(CngKey key, bool includePrivateParameters)
		{
			CngKeyBlobFormat format = includePrivateParameters ? CngKeyBlobFormat.EccFullPrivateBlob : CngKeyBlobFormat.EccFullPublicBlob;
			return key.Export(format);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001B014 File Offset: 0x00019214
		private static byte[] ParametersToBlob(ref ECParameters parameters, Func<string, bool, Interop.BCrypt.KeyBlobMagicNumber> namedCurveResolver, Func<bool, Interop.BCrypt.KeyBlobMagicNumber> explicitCurveResolver, out CngKeyBlobFormat format, out string curveName)
		{
			parameters.Validate();
			ECCurve curve = parameters.Curve;
			bool flag = parameters.D != null;
			if (curve.IsPrime)
			{
				curveName = null;
				format = (flag ? CngKeyBlobFormat.EccFullPrivateBlob : CngKeyBlobFormat.EccFullPublicBlob);
				return ECCng.GetPrimeCurveBlob(ref parameters, explicitCurveResolver);
			}
			if (!curve.IsNamed)
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_CurveNotSupported", new object[]
				{
					curve.CurveType.ToString()
				}));
			}
			curveName = curve.Oid.FriendlyName;
			if (string.IsNullOrEmpty(curveName))
			{
				throw new PlatformNotSupportedException(SR.GetString("Cryptography_InvalidCurveOid", new object[]
				{
					curve.Oid.Value.ToString()
				}));
			}
			format = (flag ? CngKeyBlobFormat.EccPrivateBlob : CngKeyBlobFormat.EccPublicBlob);
			return ECCng.GetNamedCurveBlob(ref parameters, namedCurveResolver);
		}

		// Token: 0x04000653 RID: 1619
		private static readonly Func<string, bool, Interop.BCrypt.KeyBlobMagicNumber> s_ecdhNamedMagicResolver = (string curveName, bool includePrivate) => ECCng.EcdhCurveNameToMagicNumber(curveName, includePrivate);

		// Token: 0x04000654 RID: 1620
		private static readonly Func<bool, Interop.BCrypt.KeyBlobMagicNumber> s_ecdhExplicitMagicResolver = delegate(bool includePrivate)
		{
			if (!includePrivate)
			{
				return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PUBLIC_GENERIC_MAGIC;
			}
			return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDH_PRIVATE_GENERIC_MAGIC;
		};

		// Token: 0x04000655 RID: 1621
		private static readonly Func<string, bool, Interop.BCrypt.KeyBlobMagicNumber> s_ecdsaNamedMagicResolver = (string curveName, bool includePrivate) => ECCng.ECDsaCurveNameToMagicNumber(curveName, includePrivate);

		// Token: 0x04000656 RID: 1622
		private static readonly Func<bool, Interop.BCrypt.KeyBlobMagicNumber> s_ecdsaExplicitMagicResolver = delegate(bool includePrivate)
		{
			if (!includePrivate)
			{
				return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PUBLIC_GENERIC_MAGIC;
			}
			return Interop.BCrypt.KeyBlobMagicNumber.BCRYPT_ECDSA_PRIVATE_GENERIC_MAGIC;
		};
	}
}
