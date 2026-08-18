using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x0200010D RID: 269
	public class PgpEncryptedDataGenerator : IStreamGenerator
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0003703A File Offset: 0x0003603A
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm)
		{
			this.defAlgorithm = encAlgorithm;
			this.rand = new SecureRandom();
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0003705F File Offset: 0x0003605F
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm, bool withIntegrityPacket)
		{
			this.defAlgorithm = encAlgorithm;
			this.withIntegrityPacket = withIntegrityPacket;
			this.rand = new SecureRandom();
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0003708B File Offset: 0x0003608B
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm, SecureRandom rand)
		{
			this.defAlgorithm = encAlgorithm;
			this.rand = rand;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000370AC File Offset: 0x000360AC
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm, bool withIntegrityPacket, SecureRandom rand)
		{
			this.defAlgorithm = encAlgorithm;
			this.rand = rand;
			this.withIntegrityPacket = withIntegrityPacket;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000370D4 File Offset: 0x000360D4
		public PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag encAlgorithm, SecureRandom rand, bool oldFormat)
		{
			this.defAlgorithm = encAlgorithm;
			this.rand = rand;
			this.oldFormat = oldFormat;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000370FC File Offset: 0x000360FC
		public void AddMethod(char[] passPhrase)
		{
			byte[] array = new byte[8];
			this.rand.NextBytes(array);
			S2k s2k = new S2k(HashAlgorithmTag.Sha1, array, 96);
			this.methods.Add(new PgpEncryptedDataGenerator.PbeMethod(this.defAlgorithm, s2k, PgpUtilities.MakeKeyFromPassPhrase(this.defAlgorithm, s2k, passPhrase)));
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0003714B File Offset: 0x0003614B
		public void AddMethod(PgpPublicKey key)
		{
			if (!key.IsEncryptionKey)
			{
				throw new ArgumentException("passed in key not an encryption key!");
			}
			this.methods.Add(new PgpEncryptedDataGenerator.PubMethod(key));
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00037174 File Offset: 0x00036174
		private void AddCheckSum(byte[] sessionInfo)
		{
			int num = 0;
			for (int i = 1; i < sessionInfo.Length - 2; i++)
			{
				num += (int)sessionInfo[i];
			}
			sessionInfo[sessionInfo.Length - 2] = (byte)(num >> 8);
			sessionInfo[sessionInfo.Length - 1] = (byte)num;
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x000371B0 File Offset: 0x000361B0
		private byte[] CreateSessionInfo(SymmetricKeyAlgorithmTag algorithm, KeyParameter key)
		{
			byte[] key2 = key.GetKey();
			byte[] array = new byte[key2.Length + 3];
			array[0] = (byte)algorithm;
			key2.CopyTo(array, 1);
			this.AddCheckSum(array);
			return array;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000371E4 File Offset: 0x000361E4
		private Stream Open(Stream outStr, long length, byte[] buffer)
		{
			if (this.cOut != null)
			{
				throw new InvalidOperationException("generator already in open state");
			}
			if (this.methods.Count == 0)
			{
				throw new InvalidOperationException("No encryption methods specified");
			}
			if (outStr == null)
			{
				throw new ArgumentNullException("outStr");
			}
			this.pOut = new BcpgOutputStream(outStr);
			KeyParameter keyParameter;
			if (this.methods.Count == 1)
			{
				if (this.methods[0] is PgpEncryptedDataGenerator.PbeMethod)
				{
					PgpEncryptedDataGenerator.PbeMethod pbeMethod = (PgpEncryptedDataGenerator.PbeMethod)this.methods[0];
					keyParameter = pbeMethod.GetKey();
				}
				else
				{
					keyParameter = PgpUtilities.MakeRandomKey(this.defAlgorithm, this.rand);
					byte[] si = this.CreateSessionInfo(this.defAlgorithm, keyParameter);
					PgpEncryptedDataGenerator.PubMethod pubMethod = (PgpEncryptedDataGenerator.PubMethod)this.methods[0];
					try
					{
						pubMethod.AddSessionInfo(si, this.rand);
					}
					catch (Exception exception)
					{
						throw new PgpException("exception encrypting session key", exception);
					}
				}
				this.pOut.WritePacket((ContainedPacket)this.methods[0]);
			}
			else
			{
				keyParameter = PgpUtilities.MakeRandomKey(this.defAlgorithm, this.rand);
				byte[] si2 = this.CreateSessionInfo(this.defAlgorithm, keyParameter);
				for (int num = 0; num != this.methods.Count; num++)
				{
					PgpEncryptedDataGenerator.EncMethod encMethod = (PgpEncryptedDataGenerator.EncMethod)this.methods[num];
					try
					{
						encMethod.AddSessionInfo(si2, this.rand);
					}
					catch (Exception exception2)
					{
						throw new PgpException("exception encrypting session key", exception2);
					}
					this.pOut.WritePacket(encMethod);
				}
			}
			string text = PgpUtilities.GetSymmetricCipherName(this.defAlgorithm);
			if (text == null)
			{
				throw new PgpException("null cipher specified");
			}
			Stream result;
			try
			{
				if (this.withIntegrityPacket)
				{
					text += "/CFB/NoPadding";
				}
				else
				{
					text += "/OpenPGPCFB/NoPadding";
				}
				this.c = CipherUtilities.GetCipher(text);
				byte[] iv = new byte[this.c.GetBlockSize()];
				this.c.Init(true, new ParametersWithRandom(new ParametersWithIV(keyParameter, iv), this.rand));
				if (buffer == null)
				{
					if (this.withIntegrityPacket)
					{
						this.pOut = new BcpgOutputStream(outStr, PacketTag.SymmetricEncryptedIntegrityProtected, length + (long)this.c.GetBlockSize() + 2L + 1L + 22L);
						this.pOut.WriteByte(1);
					}
					else
					{
						this.pOut = new BcpgOutputStream(outStr, PacketTag.SymmetricKeyEncrypted, length + (long)this.c.GetBlockSize() + 2L, this.oldFormat);
					}
				}
				else if (this.withIntegrityPacket)
				{
					this.pOut = new BcpgOutputStream(outStr, PacketTag.SymmetricEncryptedIntegrityProtected, buffer);
					this.pOut.WriteByte(1);
				}
				else
				{
					this.pOut = new BcpgOutputStream(outStr, PacketTag.SymmetricKeyEncrypted, buffer);
				}
				int blockSize = this.c.GetBlockSize();
				byte[] array = new byte[blockSize + 2];
				this.rand.NextBytes(array, 0, blockSize);
				Array.Copy(array, array.Length - 4, array, array.Length - 2, 2);
				Stream stream = this.cOut = new CipherStream(this.pOut, null, this.c);
				if (this.withIntegrityPacket)
				{
					string digestName = PgpUtilities.GetDigestName(HashAlgorithmTag.Sha1);
					IDigest digest = DigestUtilities.GetDigest(digestName);
					stream = (this.digestOut = new DigestStream(stream, null, digest));
				}
				stream.Write(array, 0, array.Length);
				result = new WrappedGeneratorStream(this, stream);
			}
			catch (Exception exception3)
			{
				throw new PgpException("Exception creating cipher", exception3);
			}
			return result;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00037580 File Offset: 0x00036580
		public Stream Open(Stream outStr, long length)
		{
			return this.Open(outStr, length, null);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0003758B File Offset: 0x0003658B
		public Stream Open(Stream outStr, byte[] buffer)
		{
			return this.Open(outStr, 0L, buffer);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00037598 File Offset: 0x00036598
		public void Close()
		{
			if (this.cOut != null)
			{
				if (this.digestOut != null)
				{
					BcpgOutputStream bcpgOutputStream = new BcpgOutputStream(this.digestOut, PacketTag.ModificationDetectionCode, 20L);
					bcpgOutputStream.Flush();
					this.digestOut.Flush();
					byte[] array = DigestUtilities.DoFinal(this.digestOut.WriteDigest());
					this.cOut.Write(array, 0, array.Length);
				}
				this.cOut.Flush();
				try
				{
					this.pOut.Write(this.c.DoFinal());
					this.pOut.Finish();
				}
				catch (Exception ex)
				{
					throw new IOException(ex.Message, ex);
				}
				this.cOut = null;
				this.pOut = null;
			}
		}

		// Token: 0x04000857 RID: 2135
		private BcpgOutputStream pOut;

		// Token: 0x04000858 RID: 2136
		private CipherStream cOut;

		// Token: 0x04000859 RID: 2137
		private IBufferedCipher c;

		// Token: 0x0400085A RID: 2138
		private bool withIntegrityPacket;

		// Token: 0x0400085B RID: 2139
		private bool oldFormat;

		// Token: 0x0400085C RID: 2140
		private DigestStream digestOut;

		// Token: 0x0400085D RID: 2141
		private readonly ArrayList methods = new ArrayList();

		// Token: 0x0400085E RID: 2142
		private readonly SymmetricKeyAlgorithmTag defAlgorithm;

		// Token: 0x0400085F RID: 2143
		private readonly SecureRandom rand;

		// Token: 0x0200010E RID: 270
		private abstract class EncMethod : ContainedPacket
		{
			// Token: 0x06000A69 RID: 2665
			public abstract void AddSessionInfo(byte[] si, SecureRandom random);

			// Token: 0x04000860 RID: 2144
			protected byte[] sessionInfo;

			// Token: 0x04000861 RID: 2145
			protected SymmetricKeyAlgorithmTag encAlgorithm;

			// Token: 0x04000862 RID: 2146
			protected KeyParameter key;
		}

		// Token: 0x0200010F RID: 271
		private class PbeMethod : PgpEncryptedDataGenerator.EncMethod
		{
			// Token: 0x06000A6B RID: 2667 RVA: 0x00037660 File Offset: 0x00036660
			internal PbeMethod(SymmetricKeyAlgorithmTag encAlgorithm, S2k s2k, KeyParameter key)
			{
				this.encAlgorithm = encAlgorithm;
				this.s2k = s2k;
				this.key = key;
			}

			// Token: 0x06000A6C RID: 2668 RVA: 0x0003767D File Offset: 0x0003667D
			public KeyParameter GetKey()
			{
				return this.key;
			}

			// Token: 0x06000A6D RID: 2669 RVA: 0x00037688 File Offset: 0x00036688
			public override void AddSessionInfo(byte[] si, SecureRandom random)
			{
				string symmetricCipherName = PgpUtilities.GetSymmetricCipherName(this.encAlgorithm);
				IBufferedCipher cipher = CipherUtilities.GetCipher(symmetricCipherName + "/CFB/NoPadding");
				byte[] iv = new byte[cipher.GetBlockSize()];
				cipher.Init(true, new ParametersWithRandom(new ParametersWithIV(this.key, iv), random));
				this.sessionInfo = cipher.DoFinal(si, 0, si.Length - 2);
			}

			// Token: 0x06000A6E RID: 2670 RVA: 0x000376EC File Offset: 0x000366EC
			public override void Encode(BcpgOutputStream pOut)
			{
				SymmetricKeyEncSessionPacket p = new SymmetricKeyEncSessionPacket(this.encAlgorithm, this.s2k, this.sessionInfo);
				pOut.WritePacket(p);
			}

			// Token: 0x04000863 RID: 2147
			private S2k s2k;
		}

		// Token: 0x02000110 RID: 272
		private class PubMethod : PgpEncryptedDataGenerator.EncMethod
		{
			// Token: 0x06000A6F RID: 2671 RVA: 0x00037718 File Offset: 0x00036718
			internal PubMethod(PgpPublicKey pubKey)
			{
				this.pubKey = pubKey;
			}

			// Token: 0x06000A70 RID: 2672 RVA: 0x00037728 File Offset: 0x00036728
			public override void AddSessionInfo(byte[] si, SecureRandom random)
			{
				PublicKeyAlgorithmTag algorithm = this.pubKey.Algorithm;
				IBufferedCipher cipher;
				switch (algorithm)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
					cipher = CipherUtilities.GetCipher("RSA//PKCS1Padding");
					break;
				default:
					switch (algorithm)
					{
					case PublicKeyAlgorithmTag.ElGamalEncrypt:
					case PublicKeyAlgorithmTag.ElGamalGeneral:
						cipher = CipherUtilities.GetCipher("ElGamal/ECB/PKCS1Padding");
						goto IL_8E;
					case PublicKeyAlgorithmTag.Dsa:
						throw new PgpException("Can't use DSA for encryption.");
					case PublicKeyAlgorithmTag.ECDsa:
						throw new PgpException("Can't use ECDSA for encryption.");
					}
					throw new PgpException("unknown asymmetric algorithm: " + this.pubKey.Algorithm);
				}
				IL_8E:
				AsymmetricKeyParameter key = this.pubKey.GetKey();
				cipher.Init(true, new ParametersWithRandom(key, random));
				byte[] array = cipher.DoFinal(si);
				PublicKeyAlgorithmTag algorithm2 = this.pubKey.Algorithm;
				switch (algorithm2)
				{
				case PublicKeyAlgorithmTag.RsaGeneral:
				case PublicKeyAlgorithmTag.RsaEncrypt:
					this.data = new BigInteger[]
					{
						new BigInteger(1, array)
					};
					return;
				default:
				{
					if (algorithm2 != PublicKeyAlgorithmTag.ElGamalEncrypt && algorithm2 != PublicKeyAlgorithmTag.ElGamalGeneral)
					{
						throw new PgpException("unknown asymmetric algorithm: " + this.encAlgorithm);
					}
					int num = array.Length / 2;
					this.data = new BigInteger[]
					{
						new BigInteger(1, array, 0, num),
						new BigInteger(1, array, num, num)
					};
					return;
				}
				}
			}

			// Token: 0x06000A71 RID: 2673 RVA: 0x00037878 File Offset: 0x00036878
			public override void Encode(BcpgOutputStream pOut)
			{
				PublicKeyEncSessionPacket p = new PublicKeyEncSessionPacket(this.pubKey.KeyId, this.pubKey.Algorithm, this.data);
				pOut.WritePacket(p);
			}

			// Token: 0x04000864 RID: 2148
			internal PgpPublicKey pubKey;

			// Token: 0x04000865 RID: 2149
			internal BigInteger[] data;
		}
	}
}
