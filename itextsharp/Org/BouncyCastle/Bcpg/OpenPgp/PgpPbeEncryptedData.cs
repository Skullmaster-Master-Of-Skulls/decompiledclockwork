using System;
using System.IO;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x020001E6 RID: 486
	public class PgpPbeEncryptedData : PgpEncryptedData
	{
		// Token: 0x0600130D RID: 4877 RVA: 0x0006D337 File Offset: 0x0006C337
		internal PgpPbeEncryptedData(SymmetricKeyEncSessionPacket keyData, InputStreamPacket encData) : base(encData)
		{
			this.keyData = keyData;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0006D347 File Offset: 0x0006C347
		public override Stream GetInputStream()
		{
			return this.encData.GetInputStream();
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0006D354 File Offset: 0x0006C354
		public Stream GetDataStream(char[] passPhrase)
		{
			Stream encStream;
			try
			{
				SymmetricKeyAlgorithmTag symmetricKeyAlgorithmTag = this.keyData.EncAlgorithm;
				KeyParameter parameters = PgpUtilities.MakeKeyFromPassPhrase(symmetricKeyAlgorithmTag, this.keyData.S2k, passPhrase);
				byte[] secKeyData = this.keyData.GetSecKeyData();
				if (secKeyData != null && secKeyData.Length > 0)
				{
					IBufferedCipher cipher = CipherUtilities.GetCipher(PgpUtilities.GetSymmetricCipherName(symmetricKeyAlgorithmTag) + "/CFB/NoPadding");
					cipher.Init(false, new ParametersWithIV(parameters, new byte[cipher.GetBlockSize()]));
					byte[] array = cipher.DoFinal(secKeyData);
					symmetricKeyAlgorithmTag = (SymmetricKeyAlgorithmTag)array[0];
					parameters = ParameterUtilities.CreateKeyParameter(PgpUtilities.GetSymmetricCipherName(symmetricKeyAlgorithmTag), array, 1, array.Length - 1);
				}
				IBufferedCipher bufferedCipher = this.CreateStreamCipher(symmetricKeyAlgorithmTag);
				byte[] array2 = new byte[bufferedCipher.GetBlockSize()];
				bufferedCipher.Init(false, new ParametersWithIV(parameters, array2));
				this.encStream = BcpgInputStream.Wrap(new CipherStream(this.encData.GetInputStream(), bufferedCipher, null));
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
				bool flag = array2[array2.Length - 2] == (byte)num && array2[array2.Length - 1] == (byte)num2;
				bool flag2 = num == 0 && num2 == 0;
				if (!flag && !flag2)
				{
					throw new PgpDataValidationException("quick check failed.");
				}
				encStream = this.encStream;
			}
			catch (PgpException ex)
			{
				throw ex;
			}
			catch (Exception exception)
			{
				throw new PgpException("Exception creating cipher", exception);
			}
			return encStream;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0006D558 File Offset: 0x0006C558
		private IBufferedCipher CreateStreamCipher(SymmetricKeyAlgorithmTag keyAlgorithm)
		{
			string str = (this.encData is SymmetricEncIntegrityPacket) ? "CFB" : "OpenPGPCFB";
			string algorithm = PgpUtilities.GetSymmetricCipherName(keyAlgorithm) + "/" + str + "/NoPadding";
			return CipherUtilities.GetCipher(algorithm);
		}

		// Token: 0x04000D5F RID: 3423
		private readonly SymmetricKeyEncSessionPacket keyData;
	}
}
