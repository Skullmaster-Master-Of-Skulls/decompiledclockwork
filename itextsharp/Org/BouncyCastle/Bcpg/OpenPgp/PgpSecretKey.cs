using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000234 RID: 564
	public class PgpSecretKey
	{
		// Token: 0x060015FF RID: 5631 RVA: 0x00080B32 File Offset: 0x0007FB32
		internal PgpSecretKey(SecretKeyPacket secret, PgpPublicKey pub)
		{
			this.secret = secret;
			this.pub = pub;
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00080B48 File Offset: 0x0007FB48
		internal PgpSecretKey(PgpPrivateKey privKey, PgpPublicKey pubKey, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, bool useSha1, SecureRandom rand) : this(privKey, pubKey, encAlgorithm, passPhrase, useSha1, rand, false)
		{
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00080B5C File Offset: 0x0007FB5C
		internal PgpSecretKey(PgpPrivateKey privKey, PgpPublicKey pubKey, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, bool useSha1, SecureRandom rand, bool isMasterKey)
		{
			this.pub = pubKey;
			PublicKeyAlgorithmTag algorithm = pubKey.Algorithm;
			BcpgObject bcpgObject;
			switch (algorithm)
			{
			case PublicKeyAlgorithmTag.RsaGeneral:
			case PublicKeyAlgorithmTag.RsaEncrypt:
			case PublicKeyAlgorithmTag.RsaSign:
			{
				RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)privKey.Key;
				bcpgObject = new RsaSecretBcpgKey(rsaPrivateCrtKeyParameters.Exponent, rsaPrivateCrtKeyParameters.P, rsaPrivateCrtKeyParameters.Q);
				break;
			}
			default:
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.ElGamalEncrypt:
				case PublicKeyAlgorithmTag.ElGamalGeneral:
				{
					ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = (ElGamalPrivateKeyParameters)privKey.Key;
					bcpgObject = new ElGamalSecretBcpgKey(elGamalPrivateKeyParameters.X);
					goto IL_AF;
				}
				case PublicKeyAlgorithmTag.Dsa:
				{
					DsaPrivateKeyParameters dsaPrivateKeyParameters = (DsaPrivateKeyParameters)privKey.Key;
					bcpgObject = new DsaSecretBcpgKey(dsaPrivateKeyParameters.X);
					goto IL_AF;
				}
				}
				throw new PgpException("unknown key class");
			}
			try
			{
				IL_AF:
				MemoryStream memoryStream = new MemoryStream();
				BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(memoryStream);
				bcpgOutputStream.WriteObject(bcpgObject);
				byte[] array = memoryStream.ToArray();
				byte[] buffer = PgpSecretKey.Checksum(useSha1, array, array.Length);
				bcpgOutputStream.Write(buffer);
				byte[] array2 = memoryStream.ToArray();
				if (encAlgorithm == SymmetricKeyAlgorithmTag.Null)
				{
					if (isMasterKey)
					{
						this.secret = new SecretKeyPacket(this.pub.publicPk, encAlgorithm, null, null, array2);
					}
					else
					{
						this.secret = new SecretSubkeyPacket(this.pub.publicPk, encAlgorithm, null, null, array2);
					}
				}
				else
				{
					S2k s2k;
					byte[] iv;
					byte[] secKeyData = PgpSecretKey.EncryptKeyData(array2, encAlgorithm, passPhrase, rand, out s2k, out iv);
					int s2kUsage = useSha1 ? 254 : 255;
					if (isMasterKey)
					{
						this.secret = new SecretKeyPacket(this.pub.publicPk, encAlgorithm, s2kUsage, s2k, iv, secKeyData);
					}
					else
					{
						this.secret = new SecretSubkeyPacket(this.pub.publicPk, encAlgorithm, s2kUsage, s2k, iv, secKeyData);
					}
				}
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception encrypting key", exception);
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00080D38 File Offset: 0x0007FD38
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, keyPair, id, encAlgorithm, passPhrase, false, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00080D59 File Offset: 0x0007FD59
		public PgpSecretKey(int certificationLevel, PgpKeyPair keyPair, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(keyPair.PrivateKey, PgpSecretKey.certifiedPublicKey(certificationLevel, keyPair, id, hashedPackets, unhashedPackets), encAlgorithm, passPhrase, useSha1, rand, true)
		{
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00080D7C File Offset: 0x0007FD7C
		private static PgpPublicKey certifiedPublicKey(int certificationLevel, PgpKeyPair keyPair, string id, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets)
		{
			PgpSignatureGenerator pgpSignatureGenerator;
			try
			{
				pgpSignatureGenerator = new PgpSignatureGenerator(keyPair.PublicKey.Algorithm, HashAlgorithmTag.Sha1);
			}
			catch (Exception ex)
			{
				throw new PgpException("Creating signature generator: " + ex.Message, ex);
			}
			pgpSignatureGenerator.InitSign(certificationLevel, keyPair.PrivateKey);
			pgpSignatureGenerator.SetHashedSubpackets(hashedPackets);
			pgpSignatureGenerator.SetUnhashedSubpackets(unhashedPackets);
			PgpPublicKey result;
			try
			{
				PgpSignature certification = pgpSignatureGenerator.GenerateCertification(id, keyPair.PublicKey);
				result = PgpPublicKey.AddCertification(keyPair.PublicKey, id, certification);
			}
			catch (Exception ex2)
			{
				throw new PgpException("Exception doing certification: " + ex2.Message, ex2);
			}
			return result;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00080E28 File Offset: 0x0007FE28
		public PgpSecretKey(int certificationLevel, PublicKeyAlgorithmTag algorithm, AsymmetricKeyParameter pubKey, AsymmetricKeyParameter privKey, DateTime time, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, new PgpKeyPair(algorithm, pubKey, privKey, time), id, encAlgorithm, passPhrase, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00080E54 File Offset: 0x0007FE54
		public PgpSecretKey(int certificationLevel, PublicKeyAlgorithmTag algorithm, AsymmetricKeyParameter pubKey, AsymmetricKeyParameter privKey, DateTime time, string id, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, bool useSha1, PgpSignatureSubpacketVector hashedPackets, PgpSignatureSubpacketVector unhashedPackets, SecureRandom rand) : this(certificationLevel, new PgpKeyPair(algorithm, pubKey, privKey, time), id, encAlgorithm, passPhrase, useSha1, hashedPackets, unhashedPackets, rand)
		{
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00080E84 File Offset: 0x0007FE84
		public bool IsSigningKey
		{
			get
			{
				PublicKeyAlgorithmTag algorithm = this.pub.Algorithm;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaSign:
					break;
				case PublicKeyAlgorithmTag.RsaEncrypt:
					return false;
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.Dsa:
					case PublicKeyAlgorithmTag.ECDsa:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
						break;
					case PublicKeyAlgorithmTag.EC:
						return false;
					default:
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00080ECF File Offset: 0x0007FECF
		public bool IsMasterKey
		{
			get
			{
				return this.pub.IsMasterKey;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00080EDC File Offset: 0x0007FEDC
		public SymmetricKeyAlgorithmTag KeyEncryptionAlgorithm
		{
			get
			{
				return this.secret.EncAlgorithm;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00080EE9 File Offset: 0x0007FEE9
		public long KeyId
		{
			get
			{
				return this.pub.KeyId;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00080EF6 File Offset: 0x0007FEF6
		public PgpPublicKey PublicKey
		{
			get
			{
				return this.pub;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x00080EFE File Offset: 0x0007FEFE
		public IEnumerable UserIds
		{
			get
			{
				return this.pub.GetUserIds();
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00080F0B File Offset: 0x0007FF0B
		public IEnumerable UserAttributes
		{
			get
			{
				return this.pub.GetUserAttributes();
			}
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00080F18 File Offset: 0x0007FF18
		private byte[] ExtractKeyData(char[] passPhrase)
		{
			SymmetricKeyAlgorithmTag encAlgorithm = this.secret.EncAlgorithm;
			byte[] secretKeyData = this.secret.GetSecretKeyData();
			if (encAlgorithm == SymmetricKeyAlgorithmTag.Null)
			{
				return secretKeyData;
			}
			IBufferedCipher bufferedCipher = null;
			try
			{
				string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(encAlgorithm);
				bufferedCipher = CipherUtilities.GetCipher(symmetricCipherName + "/CFB/NoPadding");
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception creating cipher", exception);
			}
			byte[] result;
			try
			{
				KeyParameter parameters = PgpUtilities.MakeKeyFromPassPhrase(this.secret.EncAlgorithm, this.secret.S2k, passPhrase);
				byte[] iv = this.secret.GetIV();
				byte[] array;
				if (this.secret.PublicKeyPacket.Version == 4)
				{
					bufferedCipher.Init(false, new ParametersWithIV(parameters, iv));
					array = bufferedCipher.DoFinal(secretKeyData);
					bool flag = this.secret.S2kUsage == 254;
					byte[] array2 = PgpSecretKey.Checksum(flag, array, flag ? (array.Length - 20) : (array.Length - 2));
					for (int num = 0; num != array2.Length; num++)
					{
						if (array2[num] != array[array.Length - array2.Length + num])
						{
							throw new PgpException(string.Concat(new object[]
							{
								"Checksum mismatch at ",
								num,
								" of ",
								array2.Length
							}));
						}
					}
				}
				else
				{
					array = new byte[secretKeyData.Length];
					int num2 = 0;
					for (int num3 = 0; num3 != 4; num3++)
					{
						bufferedCipher.Init(false, new ParametersWithIV(parameters, iv));
						int num4 = (((int)secretKeyData[num2] << 8 | (int)(secretKeyData[num2 + 1] & byte.MaxValue)) + 7) / 8;
						array[num2] = secretKeyData[num2];
						array[num2 + 1] = secretKeyData[num2 + 1];
						num2 += 2;
						bufferedCipher.DoFinal(secretKeyData, num2, num4, array, num2);
						num2 += num4;
						if (num3 != 3)
						{
							Array.Copy(secretKeyData, num2 - iv.Length, iv, 0, iv.Length);
						}
					}
					int num5 = ((int)secretKeyData[num2] << 8 & 65280) | (int)(secretKeyData[num2 + 1] & byte.MaxValue);
					int num6 = 0;
					for (int i = 0; i < array.Length - 2; i++)
					{
						num6 += (int)(array[i] & byte.MaxValue);
					}
					num6 &= 65535;
					if (num6 != num5)
					{
						throw new PgpException("Checksum mismatch: passphrase wrong, expected " + num5.ToString("X") + " found " + num6.ToString("X"));
					}
				}
				result = array;
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception2)
			{
				throw new PgpException("Exception decrypting key", exception2);
			}
			return result;
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x000811D8 File Offset: 0x000801D8
		public PgpPrivateKey ExtractPrivateKey(char[] passPhrase)
		{
			byte[] secretKeyData = this.secret.GetSecretKeyData();
			if (secretKeyData == null || secretKeyData.Length < 1)
			{
				return null;
			}
			PublicKeyPacket publicKeyPacket = this.secret.PublicKeyPacket;
			PgpPrivateKey result;
			try
			{
				byte[] buffer = this.ExtractKeyData(passPhrase);
				BcpgInputStream bcpgIn = BcpgInputStream.Wrap(new MemoryStream(buffer, false));
				PublicKeyAlgorithmTag algorithm = publicKeyPacket.Algorithm;
				AsymmetricKeyParameter privateKey;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
				case PublicKeyAlgorithmTag.RsaSign:
				{
					RsaPublicBcpgKey rsaPublicBcpgKey = (RsaPublicBcpgKey)publicKeyPacket.Key;
					RsaSecretBcpgKey rsaSecretBcpgKey = new RsaSecretBcpgKey(bcpgIn);
					RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = new RsaPrivateCrtKeyParameters(rsaSecretBcpgKey.Modulus, rsaPublicBcpgKey.PublicExponent, rsaSecretBcpgKey.PrivateExponent, rsaSecretBcpgKey.PrimeP, rsaSecretBcpgKey.PrimeQ, rsaSecretBcpgKey.PrimeExponentP, rsaSecretBcpgKey.PrimeExponentQ, rsaSecretBcpgKey.CrtCoefficient);
					privateKey = rsaPrivateCrtKeyParameters;
					break;
				}
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.ElGamalEncrypt:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
					{
						ElGamalPublicBcpgKey elGamalPublicBcpgKey = (ElGamalPublicBcpgKey)publicKeyPacket.Key;
						ElGamalSecretBcpgKey elGamalSecretBcpgKey = new ElGamalSecretBcpgKey(bcpgIn);
						ElGamalParameters parameters = new ElGamalParameters(elGamalPublicBcpgKey.P, elGamalPublicBcpgKey.G);
						privateKey = new ElGamalPrivateKeyParameters(elGamalSecretBcpgKey.X, parameters);
						goto IL_15F;
					}
					case PublicKeyAlgorithmTag.Dsa:
					{
						DsaPublicBcpgKey dsaPublicBcpgKey = (DsaPublicBcpgKey)publicKeyPacket.Key;
						DsaSecretBcpgKey dsaSecretBcpgKey = new DsaSecretBcpgKey(bcpgIn);
						DsaParameters parameters2 = new DsaParameters(dsaPublicBcpgKey.P, dsaPublicBcpgKey.Q, dsaPublicBcpgKey.G);
						privateKey = new DsaPrivateKeyParameters(dsaSecretBcpgKey.X, parameters2);
						goto IL_15F;
					}
					}
					throw new PgpException("unknown public key algorithm encountered");
				}
				IL_15F:
				result = new PgpPrivateKey(privateKey, this.KeyId);
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception constructing key", exception);
			}
			return result;
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000813A0 File Offset: 0x000803A0
		private static byte[] Checksum(bool useSha1, byte[] bytes, int length)
		{
			if (useSha1)
			{
				try
				{
					IDigest digest = DigestUtilities.GetDigest("SHA1");
					digest.BlockUpdate(bytes, 0, length);
					return DigestUtilities.DoFinal(digest);
				}
				catch (Exception exception)
				{
					throw new PgpException("Can't find SHA-1", exception);
				}
			}
			int num = 0;
			for (int num2 = 0; num2 != length; num2++)
			{
				num += (int)bytes[num2];
			}
			return new byte[]
			{
				(byte)(num >> 8),
				(byte)num
			};
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0008141C File Offset: 0x0008041C
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Encode(memoryStream);
			return memoryStream.ToArray();
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0008143C File Offset: 0x0008043C
		public void Encode(Stream outStr)
		{
			BcpgOutputStream bcpgOutputStream = BcpgOutputStream.Wrap(outStr);
			bcpgOutputStream.WritePacket(this.secret);
			if (this.pub.trustPk != null)
			{
				bcpgOutputStream.WritePacket(this.pub.trustPk);
			}
			if (this.pub.subSigs == null)
			{
				foreach (object obj in this.pub.keySigs)
				{
					PgpSignature pgpSignature = (PgpSignature)obj;
					pgpSignature.Encode(bcpgOutputStream);
				}
				for (int num = 0; num != this.pub.ids.Count; num++)
				{
					object obj2 = this.pub.ids[num];
					if (obj2 is string)
					{
						string id = (string)obj2;
						bcpgOutputStream.WritePacket(new UserIdPacket(id));
					}
					else
					{
						PgpUserAttributeSubpacketVector pgpUserAttributeSubpacketVector = (PgpUserAttributeSubpacketVector)obj2;
						bcpgOutputStream.WritePacket(new UserAttributePacket(pgpUserAttributeSubpacketVector.ToSubpacketArray()));
					}
					if (this.pub.idTrusts[num] != null)
					{
						bcpgOutputStream.WritePacket((ContainedPacket)this.pub.idTrusts[num]);
					}
					foreach (object obj3 in ((ArrayList)this.pub.idSigs[num]))
					{
						PgpSignature pgpSignature2 = (PgpSignature)obj3;
						pgpSignature2.Encode(bcpgOutputStream);
					}
				}
				return;
			}
			foreach (object obj4 in this.pub.subSigs)
			{
				PgpSignature pgpSignature3 = (PgpSignature)obj4;
				pgpSignature3.Encode(bcpgOutputStream);
			}
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00081638 File Offset: 0x00080638
		public static PgpSecretKey CopyWithNewPassword(PgpSecretKey key, char[] oldPassPhrase, char[] newPassPhrase, SymmetricKeyAlgorithmTag newEncAlgorithm, SecureRandom rand)
		{
			byte[] array = key.ExtractKeyData(oldPassPhrase);
			int s2kUsage = key.secret.S2kUsage;
			byte[] iv = null;
			S2k s2k = null;
			byte[] array2;
			if (newEncAlgorithm == SymmetricKeyAlgorithmTag.Null)
			{
				s2kUsage = 0;
				if (key.secret.S2kUsage == 254)
				{
					array2 = new byte[array.Length - 18];
					Array.Copy(array, 0, array2, 0, array2.Length - 2);
					byte[] array3 = PgpSecretKey.Checksum(false, array2, array2.Length - 2);
					array2[array2.Length - 2] = array3[0];
					array2[array2.Length - 1] = array3[1];
				}
				else
				{
					array2 = array;
				}
			}
			else
			{
				try
				{
					array2 = PgpSecretKey.EncryptKeyData(array, newEncAlgorithm, newPassPhrase, rand, out s2k, out iv);
				}
				catch (PgpException ex)
				{
					throw ex;
				}
				catch (Exception exception)
				{
					throw new PgpException("Exception encrypting key", exception);
				}
			}
			SecretKeyPacket secretKeyPacket;
			if (key.secret is SecretSubkeyPacket)
			{
				secretKeyPacket = new SecretSubkeyPacket(key.secret.PublicKeyPacket, newEncAlgorithm, s2kUsage, s2k, iv, array2);
			}
			else
			{
				secretKeyPacket = new SecretKeyPacket(key.secret.PublicKeyPacket, newEncAlgorithm, s2kUsage, s2k, iv, array2);
			}
			return new PgpSecretKey(secretKeyPacket, key.pub);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00081750 File Offset: 0x00080750
		public static PgpSecretKey ReplacePublicKey(PgpSecretKey secretKey, PgpPublicKey publicKey)
		{
			if (publicKey.KeyId != secretKey.KeyId)
			{
				throw new ArgumentException("KeyId's do not match");
			}
			return new PgpSecretKey(secretKey.secret, publicKey);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00081778 File Offset: 0x00080778
		private static byte[] EncryptKeyData(byte[] rawKeyData, SymmetricKeyAlgorithmTag encAlgorithm, char[] passPhrase, SecureRandom random, out S2k s2k, out byte[] iv)
		{
			IBufferedCipher cipher;
			try
			{
				string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(encAlgorithm);
				cipher = CipherUtilities.GetCipher(symmetricCipherName + "/CFB/NoPadding");
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception creating cipher", exception);
			}
			byte[] array = new byte[8];
			random.NextBytes(array);
			s2k = new S2k(HashAlgorithmTag.Sha1, array, 96);
			KeyParameter parameters = PgpUtilities.MakeKeyFromPassPhrase(encAlgorithm, s2k, passPhrase);
			iv = new byte[cipher.GetBlockSize()];
			random.NextBytes(iv);
			cipher.Init(true, new ParametersWithRandom(new ParametersWithIV(parameters, iv), random));
			return cipher.DoFinal(rawKeyData);
		}

		// Token: 0x04000F3D RID: 3901
		private readonly SecretKeyPacket secret;

		// Token: 0x04000F3E RID: 3902
		private readonly PgpPublicKey pub;
	}
}
