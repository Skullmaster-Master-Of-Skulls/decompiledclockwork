using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200046D RID: 1133
	public class PgpPublicKeyEncryptedData : PgpEncryptedData
	{
		// Token: 0x060026A5 RID: 9893 RVA: 0x000EA37C File Offset: 0x000E937C
		internal PgpPublicKeyEncryptedData(PublicKeyEncSessionPacket keyData, InputStreamPacket encData) : base(encData)
		{
			this.keyData = keyData;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000EA38C File Offset: 0x000E938C
		private static IBufferedCipher GetKeyCipher(PublicKeyAlgorithmTag algorithm)
		{
			IBufferedCipher cipher;
			try
			{
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
					cipher = CipherUtilities.GetCipher("RSA//PKCS1Padding");
					break;
				default:
					if (algorithm != PublicKeyAlgorithmTag.ElGamalEncrypt && algorithm != PublicKeyAlgorithmTag.ElGamalGeneral)
					{
						throw new PgpException("unknown asymmetric algorithm: " + algorithm);
					}
					cipher = CipherUtilities.GetCipher("ElGamal/ECB/PKCS1Padding");
					break;
				}
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception creating cipher", exception);
			}
			return cipher;
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000EA414 File Offset: 0x000E9414
		private bool ConfirmCheckSum(byte[] sessionInfo)
		{
			int num = 0;
			for (int num2 = 1; num2 != sessionInfo.Length - 2; num2++)
			{
				num += (int)(sessionInfo[num2] & byte.MaxValue);
			}
			return sessionInfo[sessionInfo.Length - 2] == (byte)(num >> 8) && sessionInfo[sessionInfo.Length - 1] == (byte)num;
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000EA459 File Offset: 0x000E9459
		public long KeyId
		{
			get
			{
				return this.keyData.KeyId;
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000EA468 File Offset: 0x000E9468
		public SymmetricKeyAlgorithmTag GetSymmetricAlgorithm(PgpPrivateKey privKey)
		{
			byte[] array = this.fetchSymmetricKeyData(privKey);
			return (SymmetricKeyAlgorithmTag)array[0];
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000EA480 File Offset: 0x000E9480
		public Stream GetDataStream(PgpPrivateKey privKey)
		{
			byte[] array = this.fetchSymmetricKeyData(privKey);
			string symmetricCipherName = PgpUtilities.GetSymmetricCipherName((SymmetricKeyAlgorithmTag)array[0]);
			string text = symmetricCipherName;
			IBufferedCipher cipher;
			try
			{
				if (this.encData is SymmetricEncIntegrityPacket)
				{
					text += "/CFB/NoPadding";
				}
				else
				{
					text += "/OpenPGPCFB/NoPadding";
				}
				cipher = CipherUtilities.GetCipher(text);
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("exception creating cipher", exception);
			}
			if (cipher == null)
			{
				return this.encData.GetInputStream();
			}
			Stream encStream;
			try
			{
				KeyParameter parameters = ParameterUtilities.CreateKeyParameter(symmetricCipherName, array, 1, array.Length - 3);
				byte[] array2 = new byte[cipher.GetBlockSize()];
				cipher.Init(false, new ParametersWithIV(parameters, array2));
				this.encStream = BcpgInputStream.Wrap(new CipherStream(this.encData.GetInputStream(), cipher, null));
				if (this.encData is SymmetricEncIntegrityPacket)
				{
					this.truncStream = new PgpEncryptedData.TruncatedStream(this.encStream);
					string digestName = PgpUtilities.GetDigestName(HashAlgorithmTag.Sha1);
					IDigest digest = DigestUtilities.GetDigest(digestName);
					this.encStream = new DigestStream(this.truncStream, digest, null);
				}
				if (Streams.ReadFully(this.encStream, array2, 0, array2.Length) < array2.Length)
				{
					throw new EndOfStreamException("unexpected end of stream.");
				}
				int num = this.encStream.ReadByte();
				int num2 = this.encStream.ReadByte();
				if (num < 0 || num2 < 0)
				{
					throw new EndOfStreamException("unexpected end of stream.");
				}
				encStream = this.encStream;
			}
			catch (PgpException ex2)
			{
				throw ex2;
			}
			catch (Exception exception2)
			{
				throw new PgpException("Exception starting decryption", exception2);
			}
			return encStream;
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000EA628 File Offset: 0x000E9628
		private byte[] fetchSymmetricKeyData(PgpPrivateKey privKey)
		{
			IBufferedCipher keyCipher = PgpPublicKeyEncryptedData.GetKeyCipher(this.keyData.Algorithm);
			try
			{
				keyCipher.Init(false, privKey.Key);
			}
			catch (InvalidKeyException exception)
			{
				throw new PgpException("error setting asymmetric cipher", exception);
			}
			BigInteger[] encSessionKey = this.keyData.GetEncSessionKey();
			if (this.keyData.Algorithm == PublicKeyAlgorithmTag.RsaEncrypt || this.keyData.Algorithm == PublicKeyAlgorithmTag.RsaGeneral)
			{
				keyCipher.ProcessBytes(encSessionKey[0].ToByteArrayUnsigned());
			}
			else
			{
				ElGamalPrivateKeyParameters elGamalPrivateKeyParameters = (ElGamalPrivateKeyParameters)privKey.Key;
				int num = (elGamalPrivateKeyParameters.Parameters.P.BitLength + 7) / 8;
				byte[] array = encSessionKey[0].ToByteArray();
				int num2 = array.Length - num;
				if (num2 >= 0)
				{
					keyCipher.ProcessBytes(array, num2, num);
				}
				else
				{
					byte[] input = new byte[-num2];
					keyCipher.ProcessBytes(input);
					keyCipher.ProcessBytes(array);
				}
				array = encSessionKey[1].ToByteArray();
				num2 = array.Length - num;
				if (num2 >= 0)
				{
					keyCipher.ProcessBytes(array, num2, num);
				}
				else
				{
					byte[] input2 = new byte[-num2];
					keyCipher.ProcessBytes(input2);
					keyCipher.ProcessBytes(array);
				}
			}
			byte[] array2;
			try
			{
				array2 = keyCipher.DoFinal();
			}
			catch (Exception exception2)
			{
				throw new PgpException("exception decrypting secret key", exception2);
			}
			if (!this.ConfirmCheckSum(array2))
			{
				throw new PgpKeyValidationException("key checksum failed");
			}
			return array2;
		}

		// Token: 0x04001AB1 RID: 6833
		private PublicKeyEncSessionPacket keyData;
	}
}
