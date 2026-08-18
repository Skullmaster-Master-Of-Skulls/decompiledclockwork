using System;
using System.Text;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008D4 RID: 2260
	internal static class PasswordBasedEncryption
	{
		// Token: 0x06005263 RID: 21091 RVA: 0x00127B87 File Offset: 0x00126B87
		private static CryptographicException AlgorithmKdfRequiresChars()
		{
			return new CryptographicException("The KDF requires a char-based password input.");
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x00127B94 File Offset: 0x00126B94
		internal static int Decrypt(ref AlgorithmIdentifierAsn algorithmIdentifier, ReadOnlySpan<char> password, ReadOnlySpan<byte> passwordBytes, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			bool flag = false;
			HashAlgorithmName hashAlgorithm;
			SymmetricAlgorithm symmetricAlgorithm;
			if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithMD5AndDESCBC))
			{
				hashAlgorithm = HashAlgorithmName.MD5;
				symmetricAlgorithm = DES.Create();
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithMD5AndRC2CBC))
			{
				hashAlgorithm = HashAlgorithmName.MD5;
				symmetricAlgorithm = RC2.Create();
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithSha1AndDESCBC))
			{
				hashAlgorithm = HashAlgorithmName.SHA1;
				symmetricAlgorithm = DES.Create();
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithSha1AndRC2CBC))
			{
				hashAlgorithm = HashAlgorithmName.SHA1;
				symmetricAlgorithm = RC2.Create();
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd3Key3Des))
			{
				hashAlgorithm = HashAlgorithmName.SHA1;
				symmetricAlgorithm = TripleDES.Create();
				flag = true;
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd2Key3Des))
			{
				hashAlgorithm = HashAlgorithmName.SHA1;
				symmetricAlgorithm = TripleDES.Create();
				symmetricAlgorithm.KeySize = 128;
				flag = true;
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd128BitRC2))
			{
				hashAlgorithm = HashAlgorithmName.SHA1;
				symmetricAlgorithm = RC2.Create();
				symmetricAlgorithm.KeySize = 128;
				flag = true;
			}
			else if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd40BitRC2))
			{
				hashAlgorithm = HashAlgorithmName.SHA1;
				symmetricAlgorithm = RC2.Create();
				symmetricAlgorithm.KeySize = 40;
				flag = true;
			}
			else
			{
				if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PasswordBasedEncryptionScheme2))
				{
					return PasswordBasedEncryption.Pbes2Decrypt(algorithmIdentifier.Parameters, password, passwordBytes, encryptedData, destination);
				}
				throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
			}
			int result;
			using (symmetricAlgorithm)
			{
				if (flag)
				{
					if (password.Length == 0 && passwordBytes.Length > 0)
					{
						throw PasswordBasedEncryption.AlgorithmKdfRequiresChars();
					}
					result = PasswordBasedEncryption.Pkcs12PbeDecrypt(algorithmIdentifier, password, hashAlgorithm, symmetricAlgorithm, encryptedData, destination);
				}
				else
				{
					using (IncrementalHash incrementalHash = IncrementalHash.CreateHash(hashAlgorithm))
					{
						Span<byte> span = new byte[128];
						ReadOnlySpan<byte> password2 = default(ReadOnlySpan<byte>);
						byte[] array = null;
						Encoding encoding = null;
						if (passwordBytes.Length > 0 || password.Length == 0)
						{
							password2 = passwordBytes;
						}
						else
						{
							encoding = Encoding.UTF8;
							int num = Utility.EncodingGetByteCount(encoding, password);
							if (num > span.Length)
							{
								array = CryptoPool.Rent(num);
								span = new Span<byte>(array, 0, num);
							}
							else
							{
								span = span.Slice(0, num);
							}
						}
						byte[] array2;
						if ((array2 = span.DangerousGetArrayForPinning()) != null)
						{
							int num2 = array2.Length;
						}
						if (encoding != null)
						{
							int length = Utility.EncodingGetBytes(encoding, password, span);
							span = span.Slice(0, length);
							password2 = span;
						}
						try
						{
							result = PasswordBasedEncryption.Pbes1Decrypt(algorithmIdentifier.Parameters, password2, incrementalHash, symmetricAlgorithm, encryptedData, destination);
						}
						finally
						{
							CryptographicOperations.ZeroMemory(span);
							if (array != null)
							{
								CryptoPool.Return(array, 0);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x00127E84 File Offset: 0x00126E84
		private static int Pbes2Decrypt(ReadOnlyMemory<byte>? algorithmParameters, ReadOnlySpan<char> password, ReadOnlySpan<byte> passwordBytes, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			Span<byte> span = new byte[128];
			ReadOnlySpan<byte> password2 = default(ReadOnlySpan<byte>);
			byte[] array = null;
			Encoding encoding = null;
			if (passwordBytes.Length > 0 || password.Length == 0)
			{
				password2 = passwordBytes;
			}
			else
			{
				encoding = Encoding.UTF8;
				int num = Utility.EncodingGetByteCount(encoding, password);
				if (num > span.Length)
				{
					array = CryptoPool.Rent(num);
					span = new Span<byte>(array, 0, num);
				}
				else
				{
					span = span.Slice(0, num);
				}
			}
			byte[] array2;
			if ((array2 = span.DangerousGetArrayForPinning()) != null)
			{
				int num2 = array2.Length;
			}
			if (encoding != null)
			{
				int length = Utility.EncodingGetBytes(encoding, password, span);
				span = span.Slice(0, length);
				password2 = span;
			}
			int result;
			try
			{
				result = PasswordBasedEncryption.Pbes2Decrypt(algorithmParameters, password2, encryptedData, destination);
			}
			finally
			{
				if (array != null)
				{
					CryptoPool.Return(array, span.Length);
				}
			}
			return result;
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x00127F60 File Offset: 0x00126F60
		private static int Pbes2Decrypt(ReadOnlyMemory<byte>? algorithmParameters, ReadOnlySpan<byte> password, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			if (algorithmParameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			PBES2Params pbes2Params = PBES2Params.Decode(algorithmParameters.Value, AsnEncodingRules.BER);
			if (!Helpers.SequenceEqual(pbes2Params.KeyDerivationFunc.Algorithm, Oids.Pbkdf2))
			{
				throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
			}
			int? requestedKeyLength;
			int iterations;
			ReadOnlyMemory<byte> readOnlyMemory;
			HashAlgorithmName hashAlgorithmName = PasswordBasedEncryption.OpenPbkdf2(pbes2Params.KeyDerivationFunc.Parameters, out requestedKeyLength, out iterations, out readOnlyMemory);
			Span<byte> span = new byte[16];
			SymmetricAlgorithm symmetricAlgorithm = PasswordBasedEncryption.OpenCipher(pbes2Params.EncryptionScheme, requestedKeyLength, ref span);
			int result;
			using (symmetricAlgorithm)
			{
				byte[] array = new byte[password.Length];
				byte[] array2 = new byte[readOnlyMemory.Length];
				password.CopyTo(array);
				readOnlyMemory.CopyTo(array2);
				byte[] array3 = Pbkdf2.Derive(hashAlgorithmName.Name, array, array2, iterations, symmetricAlgorithm.KeySize / 8);
				try
				{
					result = PasswordBasedEncryption.Decrypt(symmetricAlgorithm, array3, span, encryptedData, destination);
				}
				finally
				{
					CryptographicOperations.ZeroMemory(array);
					CryptographicOperations.ZeroMemory(array2);
					CryptographicOperations.ZeroMemory(array3);
				}
			}
			return result;
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x001280AC File Offset: 0x001270AC
		private static SymmetricAlgorithm OpenCipher(AlgorithmIdentifierAsn encryptionScheme, int? requestedKeyLength, ref Span<byte> iv)
		{
			byte[] algorithm = encryptionScheme.Algorithm;
			if (Helpers.SequenceEqual(algorithm, Oids.Aes128Cbc) || Helpers.SequenceEqual(algorithm, Oids.Aes192Cbc) || Helpers.SequenceEqual(algorithm, Oids.Aes256Cbc))
			{
				int num;
				if (Helpers.SequenceEqual(algorithm, Oids.Aes128Cbc))
				{
					num = 16;
				}
				else if (Helpers.SequenceEqual(algorithm, Oids.Aes192Cbc))
				{
					num = 24;
				}
				else
				{
					if (!Helpers.SequenceEqual(algorithm, Oids.Aes256Cbc))
					{
						throw new CryptographicException();
					}
					num = 32;
				}
				if (requestedKeyLength != null && requestedKeyLength != num)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				PasswordBasedEncryption.ReadIvParameter(encryptionScheme.Parameters, 16, ref iv);
				Rijndael rijndael = Rijndael.Create();
				rijndael.KeySize = num * 8;
				return rijndael;
			}
			else if (Helpers.SequenceEqual(algorithm, Oids.TripleDesCbc))
			{
				if (requestedKeyLength != null && requestedKeyLength != 24)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				PasswordBasedEncryption.ReadIvParameter(encryptionScheme.Parameters, 8, ref iv);
				return TripleDES.Create();
			}
			else if (Helpers.SequenceEqual(algorithm, Oids.Rc2Cbc))
			{
				if (encryptionScheme.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (requestedKeyLength == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				Rc2CbcParameters rc2CbcParameters = Rc2CbcParameters.Decode(encryptionScheme.Parameters.Value, AsnEncodingRules.BER);
				if (rc2CbcParameters.Iv.Length != 8)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				RC2 rc = RC2.Create();
				rc.KeySize = requestedKeyLength.Value * 8;
				rc.EffectiveKeySize = rc2CbcParameters.GetEffectiveKeyBits();
				rc2CbcParameters.Iv.Span.CopyTo(iv);
				iv = iv.Slice(0, rc2CbcParameters.Iv.Length);
				return rc;
			}
			else
			{
				if (!Helpers.SequenceEqual(algorithm, Oids.DesCbc))
				{
					throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
				}
				if (requestedKeyLength != null && requestedKeyLength != 8)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				PasswordBasedEncryption.ReadIvParameter(encryptionScheme.Parameters, 8, ref iv);
				return DES.Create();
			}
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x001282EC File Offset: 0x001272EC
		private static void ReadIvParameter(ReadOnlyMemory<byte>? encryptionSchemeParameters, int length, ref Span<byte> iv)
		{
			if (encryptionSchemeParameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			try
			{
				ReadOnlySpan<byte> span = encryptionSchemeParameters.Value.Span;
				int num;
				int num2;
				bool flag = AsnDecoder.TryReadOctetString(span, iv, AsnEncodingRules.BER, out num, out num2, null);
				if (!flag || num2 != length || num != span.Length)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				iv = iv.Slice(0, num2);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x00128388 File Offset: 0x00127388
		private static HashAlgorithmName OpenPbkdf2(ReadOnlyMemory<byte>? parameters, out int? requestedKeyLength, out int iterationCount, out ReadOnlyMemory<byte> saltMemory)
		{
			if (parameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			Pbkdf2Params pbkdf2Params = Pbkdf2Params.Decode(parameters.Value, AsnEncodingRules.BER);
			if (pbkdf2Params.Salt.OtherSource != null)
			{
				throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
			}
			if (pbkdf2Params.Salt.Specified == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			HashAlgorithmName result;
			if (Helpers.SequenceEqual(pbkdf2Params.Prf.Algorithm, Oids.HmacWithSha1))
			{
				result = HashAlgorithmName.SHA1;
			}
			else if (Helpers.SequenceEqual(pbkdf2Params.Prf.Algorithm, Oids.HmacWithSha256))
			{
				result = HashAlgorithmName.SHA256;
			}
			else if (Helpers.SequenceEqual(pbkdf2Params.Prf.Algorithm, Oids.HmacWithSha384))
			{
				result = HashAlgorithmName.SHA384;
			}
			else
			{
				if (!Helpers.SequenceEqual(pbkdf2Params.Prf.Algorithm, Oids.HmacWithSha512))
				{
					throw new CryptographicException("The algorithm is unknown, not valid for the requested usage, or was not handled.");
				}
				result = HashAlgorithmName.SHA512;
			}
			if (!pbkdf2Params.Prf.HasNullEquivalentParameters())
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			requestedKeyLength = pbkdf2Params.KeyLength;
			iterationCount = PasswordBasedEncryption.NormalizeIterationCount(pbkdf2Params.IterationCount, null);
			saltMemory = pbkdf2Params.Salt.Specified.Value;
			return result;
		}

		// Token: 0x0600526A RID: 21098 RVA: 0x001284D4 File Offset: 0x001274D4
		private static int Pbes1Decrypt(ReadOnlyMemory<byte>? algorithmParameters, ReadOnlySpan<byte> password, IncrementalHash hasher, SymmetricAlgorithm cipher, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			if (algorithmParameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			PBEParameter pbeparameter = PBEParameter.Decode(algorithmParameters.Value, AsnEncodingRules.BER);
			if (pbeparameter.Salt.Length != 8)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (pbeparameter.IterationCount < 1)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			int iterationCount = PasswordBasedEncryption.NormalizeIterationCount(pbeparameter.IterationCount, null);
			Span<byte> span = new byte[16];
			int result;
			try
			{
				PasswordBasedEncryption.Pbkdf1(hasher, password, pbeparameter.Salt.Span, iterationCount, span);
				Span<byte> span2 = span.Slice(0, 8);
				Span<byte> span3 = span.Slice(8, 8);
				result = PasswordBasedEncryption.Decrypt(cipher, span2, span3, encryptedData, destination);
			}
			finally
			{
				CryptographicOperations.ZeroMemory(span);
			}
			return result;
		}

		// Token: 0x0600526B RID: 21099 RVA: 0x001285B4 File Offset: 0x001275B4
		private static int Pkcs12PbeDecrypt(AlgorithmIdentifierAsn algorithmIdentifier, ReadOnlySpan<char> password, HashAlgorithmName hashAlgorithm, SymmetricAlgorithm cipher, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			if (algorithmIdentifier.Parameters == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (cipher.KeySize > 256 || cipher.BlockSize > 256)
			{
				throw new CryptographicException();
			}
			PBEParameter pbeparameter = PBEParameter.Decode(algorithmIdentifier.Parameters.Value, AsnEncodingRules.BER);
			int iterationCount = PasswordBasedEncryption.NormalizeIterationCount(pbeparameter.IterationCount, new int?(600000));
			Span<byte> span = new byte[cipher.BlockSize / 8];
			Span<byte> span2 = new byte[cipher.KeySize / 8];
			ReadOnlySpan<byte> span3 = pbeparameter.Salt.Span;
			int result;
			try
			{
				Pkcs12Kdf.DeriveIV(password, hashAlgorithm, iterationCount, span3, span);
				Pkcs12Kdf.DeriveCipherKey(password, hashAlgorithm, iterationCount, span3, span2);
				result = PasswordBasedEncryption.Decrypt(cipher, span2, span, encryptedData, destination);
			}
			finally
			{
				CryptographicOperations.ZeroMemory(span2);
				CryptographicOperations.ZeroMemory(span);
			}
			return result;
		}

		// Token: 0x0600526C RID: 21100 RVA: 0x001286A8 File Offset: 0x001276A8
		private static int Decrypt(SymmetricAlgorithm cipher, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv, ReadOnlySpan<byte> encryptedData, Span<byte> destination)
		{
			byte[] array = new byte[key.Length];
			byte[] array2 = new byte[iv.Length];
			byte[] array3 = CryptoPool.Rent(encryptedData.Length);
			byte[] array4 = CryptoPool.Rent(destination.Length);
			byte[] array5;
			if ((array5 = array) != null)
			{
				int num = array5.Length;
			}
			byte[] array6;
			if ((array6 = array2) != null)
			{
				int num2 = array6.Length;
			}
			byte[] array7;
			if ((array7 = array3) != null)
			{
				int num3 = array7.Length;
			}
			byte[] array8;
			if ((array8 = array4) != null)
			{
				int num4 = array8.Length;
			}
			int result;
			try
			{
				key.CopyTo(array);
				iv.CopyTo(array2);
				using (ICryptoTransform cryptoTransform = cipher.CreateDecryptor(array, array2))
				{
					encryptedData.CopyTo(array3);
					int num5 = cryptoTransform.TransformBlock(array3, 0, encryptedData.Length, array4, 0);
					new ReadOnlySpan<byte>(array4, 0, num5).CopyTo(destination);
					byte[] array9 = cryptoTransform.TransformFinalBlock(PasswordBasedEncryption.s_Empty, 0, 0);
					byte[] array10;
					if ((array10 = array9) != null)
					{
						int num6 = array10.Length;
					}
					Span<byte> buffer = new Span<byte>(array9);
					buffer.CopyTo(destination.Slice(num5));
					CryptographicOperations.ZeroMemory(buffer);
					result = num5 + array9.Length;
				}
			}
			finally
			{
				CryptographicOperations.ZeroMemory(array);
				CryptographicOperations.ZeroMemory(array2);
				CryptoPool.Return(array3, encryptedData.Length);
				CryptoPool.Return(array4, destination.Length);
			}
			return result;
		}

		// Token: 0x0600526D RID: 21101 RVA: 0x00128810 File Offset: 0x00127810
		private static void Pbkdf1(IncrementalHash hasher, ReadOnlySpan<byte> password, ReadOnlySpan<byte> salt, int iterationCount, Span<byte> dk)
		{
			Span<byte> span = new byte[20];
			hasher.AppendData(password);
			hasher.AppendData(salt);
			int num;
			if (!hasher.TryGetHashAndReset(span, out num))
			{
				throw new CryptographicException();
			}
			span = span.Slice(0, num);
			KdfWorkLimiter.RecordIterations(iterationCount);
			for (int i = 1; i < iterationCount; i++)
			{
				hasher.AppendData(span);
				if (!hasher.TryGetHashAndReset(span, out num) || num != span.Length)
				{
					throw new CryptographicException();
				}
			}
			span.Slice(0, dk.Length).CopyTo(dk);
			CryptographicOperations.ZeroMemory(span);
		}

		// Token: 0x0600526E RID: 21102 RVA: 0x001288AB File Offset: 0x001278AB
		internal static int NormalizeIterationCount(int iterationCount, int? iterationLimit)
		{
			if (iterationCount <= 0 || (iterationLimit != null && iterationCount > iterationLimit.Value))
			{
				throw new CryptographicException("Value was invalid.");
			}
			return iterationCount;
		}

		// Token: 0x04002A75 RID: 10869
		internal const int IterationLimit = 600000;

		// Token: 0x04002A76 RID: 10870
		private static readonly byte[] s_Empty = new byte[0];
	}
}
