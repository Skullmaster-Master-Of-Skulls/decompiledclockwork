using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020008E4 RID: 2276
	internal struct PfxAsn
	{
		// Token: 0x060052B7 RID: 21175 RVA: 0x00129C49 File Offset: 0x00128C49
		internal static void Decode(ref AsnValueReader reader, ReadOnlyMemory<byte> rebind, out PfxAsn decoded)
		{
			PfxAsn.Decode(ref reader, Asn1Tag.Sequence, rebind, out decoded);
		}

		// Token: 0x060052B8 RID: 21176 RVA: 0x00129C58 File Offset: 0x00128C58
		internal static void Decode(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PfxAsn decoded)
		{
			try
			{
				PfxAsn.DecodeCore(ref reader, expectedTag, rebind, out decoded);
			}
			catch (InvalidOperationException inner)
			{
				throw new CryptographicException("ASN1 corrupted data.", inner);
			}
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x00129C90 File Offset: 0x00128C90
		private static void DecodeCore(ref AsnValueReader reader, Asn1Tag expectedTag, ReadOnlyMemory<byte> rebind, out PfxAsn decoded)
		{
			decoded = default(PfxAsn);
			AsnValueReader asnValueReader = reader.ReadSequence(new Asn1Tag?(expectedTag));
			if (!asnValueReader.TryReadInt32(out decoded.Version))
			{
				asnValueReader.ThrowIfNotEmpty();
			}
			ContentInfoAsn.Decode(ref asnValueReader, rebind, out decoded.AuthSafe);
			if (asnValueReader.HasData && asnValueReader.PeekTag().HasSameClassAndValue(Asn1Tag.Sequence))
			{
				MacData value;
				System.Security.Cryptography.X509Certificates.MacData.Decode(ref asnValueReader, rebind, out value);
				decoded.MacData = new MacData?(value);
			}
			asnValueReader.ThrowIfNotEmpty();
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x00129D14 File Offset: 0x00128D14
		internal ulong CountTotalIterations()
		{
			ulong num = 0UL;
			if (!Helpers.SequenceEqual(this.AuthSafe.ContentType, Oids.Pkcs7Data))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			ReadOnlyMemory<byte> rebind = Helpers.DecodeOctetStringAsMemory(this.AuthSafe.Content);
			AsnValueReader asnValueReader = new AsnValueReader(rebind.Span, AsnEncodingRules.BER);
			AsnValueReader asnValueReader2 = asnValueReader.ReadSequence();
			asnValueReader.ThrowIfNotEmpty();
			bool flag = false;
			checked
			{
				while (asnValueReader2.HasData)
				{
					ContentInfoAsn contentInfo;
					ContentInfoAsn.Decode(ref asnValueReader2, rebind, out contentInfo);
					ArraySegment<byte>? arraySegment = null;
					try
					{
						ReadOnlyMemory<byte> rebind2;
						if (!Helpers.SequenceEqual(contentInfo.ContentType, Oids.Pkcs7Data))
						{
							if (!Helpers.SequenceEqual(contentInfo.ContentType, Oids.Pkcs7Encrypted))
							{
								throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"));
							}
							if (flag)
							{
								throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"));
							}
							uint num2;
							ArraySegment<byte> arraySegment2 = PfxAsn.DecryptContentInfo(contentInfo, out num2);
							rebind2 = arraySegment2;
							arraySegment = new ArraySegment<byte>?(arraySegment2);
							flag = true;
							num += unchecked((ulong)num2);
						}
						else
						{
							rebind2 = Helpers.DecodeOctetStringAsMemory(contentInfo.Content);
						}
						AsnValueReader asnValueReader3 = new AsnValueReader(rebind2.Span, AsnEncodingRules.BER);
						AsnValueReader asnValueReader4 = asnValueReader3.ReadSequence();
						asnValueReader3.ThrowIfNotEmpty();
						while (asnValueReader4.HasData)
						{
							SafeBagAsn safeBagAsn;
							SafeBagAsn.Decode(ref asnValueReader4, rebind2, out safeBagAsn);
							if (Helpers.SequenceEqual(safeBagAsn.BagId, Oids.Pkcs12ShroudedKeyBag))
							{
								AsnValueReader asnValueReader5 = new AsnValueReader(safeBagAsn.BagValue.Span, AsnEncodingRules.BER);
								EncryptedPrivateKeyInfoAsn encryptedPrivateKeyInfoAsn;
								EncryptedPrivateKeyInfoAsn.Decode(ref asnValueReader5, safeBagAsn.BagValue, out encryptedPrivateKeyInfoAsn);
								num += unchecked((ulong)PfxAsn.IterationsFromParameters(ref encryptedPrivateKeyInfoAsn.EncryptionAlgorithm));
							}
						}
					}
					finally
					{
						if (arraySegment != null)
						{
							CryptoPool.Return(arraySegment.Value);
						}
					}
				}
				if (this.MacData != null)
				{
					if (this.MacData.Value.IterationCount < 0)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					num += unchecked((ulong)(checked((uint)this.MacData.Value.IterationCount)));
				}
				return num;
			}
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x00129F14 File Offset: 0x00128F14
		private static ArraySegment<byte> DecryptContentInfo(ContentInfoAsn contentInfo, out uint iterations)
		{
			char[] array = new char[0];
			byte[] array2 = new byte[0];
			char[] array3 = null;
			byte[] array4 = null;
			EncryptedDataAsn encryptedDataAsn = EncryptedDataAsn.Decode(contentInfo.Content, AsnEncodingRules.BER);
			if (encryptedDataAsn.Version != 0 && encryptedDataAsn.Version != 2)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (!Helpers.SequenceEqual(encryptedDataAsn.EncryptedContentInfo.ContentType, Oids.Pkcs7Data))
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			if (encryptedDataAsn.EncryptedContentInfo.EncryptedContent == null)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			iterations = PfxAsn.IterationsFromParameters(ref encryptedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm);
			if (iterations > 600000U)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"));
			}
			int length = encryptedDataAsn.EncryptedContentInfo.EncryptedContent.Value.Length;
			byte[] array5 = new byte[length];
			int num = 0;
			try
			{
				num = PasswordBasedEncryption.Decrypt(ref encryptedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm, array, array2, encryptedDataAsn.EncryptedContentInfo.EncryptedContent.Value.Span, array5);
				AsnValueReader asnValueReader = new AsnValueReader(new ReadOnlySpan<byte>(array5, 0, num), AsnEncodingRules.BER);
				asnValueReader.ReadSequence();
				asnValueReader.ThrowIfNotEmpty();
			}
			catch
			{
				num = PasswordBasedEncryption.Decrypt(ref encryptedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm, array3, array4, encryptedDataAsn.EncryptedContentInfo.EncryptedContent.Value.Span, array5);
				AsnValueReader asnValueReader2 = new AsnValueReader(new ReadOnlySpan<byte>(array5, 0, num), AsnEncodingRules.BER);
				asnValueReader2.ReadSequence();
				asnValueReader2.ThrowIfNotEmpty();
			}
			finally
			{
				if (num == 0)
				{
					CryptographicOperations.ZeroMemory(array5);
				}
			}
			return new ArraySegment<byte>(array5, 0, num);
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0012A0F8 File Offset: 0x001290F8
		private static uint IterationsFromParameters(ref AlgorithmIdentifierAsn algorithmIdentifier)
		{
			if (Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PasswordBasedEncryptionScheme2))
			{
				if (algorithmIdentifier.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				PBES2Params pbes2Params = PBES2Params.Decode(algorithmIdentifier.Parameters.Value, AsnEncodingRules.BER);
				if (!Helpers.SequenceEqual(pbes2Params.KeyDerivationFunc.Algorithm, Oids.Pbkdf2))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (pbes2Params.KeyDerivationFunc.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				Pbkdf2Params pbkdf2Params = Pbkdf2Params.Decode(pbes2Params.KeyDerivationFunc.Parameters.Value, AsnEncodingRules.BER);
				if (pbkdf2Params.IterationCount < 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (uint)pbkdf2Params.IterationCount;
			}
			else
			{
				if (!Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithMD5AndDESCBC) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithMD5AndRC2CBC) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithSha1AndDESCBC) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.PbeWithSha1AndRC2CBC) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd3Key3Des) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd2Key3Des) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd128BitRC2) && !Helpers.SequenceEqual(algorithmIdentifier.Algorithm, Oids.Pkcs12PbeWithShaAnd40BitRC2))
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				if (algorithmIdentifier.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				PBEParameter pbeparameter = PBEParameter.Decode(algorithmIdentifier.Parameters.Value, AsnEncodingRules.BER);
				if (pbeparameter.IterationCount < 0)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				return (uint)pbeparameter.IterationCount;
			}
		}

		// Token: 0x04002AA1 RID: 10913
		private const uint MaxIterationWork = 600000U;

		// Token: 0x04002AA2 RID: 10914
		internal int Version;

		// Token: 0x04002AA3 RID: 10915
		internal ContentInfoAsn AuthSafe;

		// Token: 0x04002AA4 RID: 10916
		internal MacData? MacData;
	}
}
