using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000357 RID: 855
	public class CmsEnvelopedDataStreamGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x06001EC3 RID: 7875 RVA: 0x000B9832 File Offset: 0x000B8832
		public CmsEnvelopedDataStreamGenerator()
		{
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000B983A File Offset: 0x000B883A
		public CmsEnvelopedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000B9843 File Offset: 0x000B8843
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000B984C File Offset: 0x000B884C
		public void SetBerEncodeRecipients(bool berEncodeRecipientSet)
		{
			this._berEncodeRecipientSet = berEncodeRecipientSet;
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001EC7 RID: 7879 RVA: 0x000B9858 File Offset: 0x000B8858
		private DerInteger Version
		{
			get
			{
				int value = (this._originatorInfo != null || this._unprotectedAttributes != null) ? 2 : 0;
				return new DerInteger(value);
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000B9880 File Offset: 0x000B8880
		private Stream Open(Stream outStream, string encryptionOid, CipherKeyGenerator keyGen)
		{
			byte[] array = keyGen.GenerateKey();
			KeyParameter keyParameter = ParameterUtilities.CreateKeyParameter(encryptionOid, array);
			Asn1Encodable asn1Params = this.GenerateAsn1Parameters(encryptionOid, array);
			ICipherParameters cipherParameters;
			AlgorithmIdentifier algorithmIdentifier = this.GetAlgorithmIdentifier(encryptionOid, keyParameter, asn1Params, out cipherParameters);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this.recipientInfoGenerators)
			{
				RecipientInfoGenerator recipientInfoGenerator = (RecipientInfoGenerator)obj;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						recipientInfoGenerator.Generate(keyParameter, this.rand)
					});
				}
				catch (InvalidKeyException e)
				{
					throw new CmsException("key inappropriate for algorithm.", e);
				}
				catch (GeneralSecurityException e2)
				{
					throw new CmsException("error making encrypted content.", e2);
				}
			}
			return this.Open(outStream, algorithmIdentifier, cipherParameters, asn1EncodableVector);
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x000B9974 File Offset: 0x000B8974
		private Stream Open(Stream outStream, AlgorithmIdentifier encAlgID, ICipherParameters cipherParameters, Asn1EncodableVector recipientInfos)
		{
			Stream result;
			try
			{
				BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
				berSequenceGenerator.AddObject(CmsObjectIdentifiers.EnvelopedData);
				BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
				berSequenceGenerator2.AddObject(this.Version);
				Stream rawOutputStream = berSequenceGenerator2.GetRawOutputStream();
				Asn1Generator asn1Generator = this._berEncodeRecipientSet ? new BerSetGenerator(rawOutputStream) : new DerSetGenerator(rawOutputStream);
				foreach (object obj in recipientInfos)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					asn1Generator.AddObject(obj2);
				}
				asn1Generator.Close();
				BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(rawOutputStream);
				berSequenceGenerator3.AddObject(CmsObjectIdentifiers.Data);
				berSequenceGenerator3.AddObject(encAlgID);
				Stream stream = CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, false, this._bufferSize);
				IBufferedCipher cipher = CipherUtilities.GetCipher(encAlgID.ObjectID);
				cipher.Init(true, new ParametersWithRandom(cipherParameters, this.rand));
				CipherStream outStream2 = new CipherStream(stream, null, cipher);
				result = new CmsEnvelopedDataStreamGenerator.CmsEnvelopedDataOutputStream(outStream2, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (IOException e3)
			{
				throw new CmsException("exception decoding algorithm parameters.", e3);
			}
			return result;
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x000B9AE4 File Offset: 0x000B8AE4
		public Stream Open(Stream outStream, string encryptionOid)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
			return this.Open(outStream, encryptionOid, keyGenerator);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x000B9B18 File Offset: 0x000B8B18
		public Stream Open(Stream outStream, string encryptionOid, int keySize)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
			return this.Open(outStream, encryptionOid, keyGenerator);
		}

		// Token: 0x04001548 RID: 5448
		private object _originatorInfo;

		// Token: 0x04001549 RID: 5449
		private object _unprotectedAttributes;

		// Token: 0x0400154A RID: 5450
		private int _bufferSize;

		// Token: 0x0400154B RID: 5451
		private bool _berEncodeRecipientSet;

		// Token: 0x02000358 RID: 856
		private class CmsEnvelopedDataOutputStream : BaseOutputStream
		{
			// Token: 0x06001ECC RID: 7884 RVA: 0x000B9B47 File Offset: 0x000B8B47
			public CmsEnvelopedDataOutputStream(CipherStream outStream, BerSequenceGenerator cGen, BerSequenceGenerator envGen, BerSequenceGenerator eiGen)
			{
				this._out = outStream;
				this._cGen = cGen;
				this._envGen = envGen;
				this._eiGen = eiGen;
			}

			// Token: 0x06001ECD RID: 7885 RVA: 0x000B9B6C File Offset: 0x000B8B6C
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x06001ECE RID: 7886 RVA: 0x000B9B7A File Offset: 0x000B8B7A
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x06001ECF RID: 7887 RVA: 0x000B9B8A File Offset: 0x000B8B8A
			public override void Close()
			{
				this._out.Close();
				this._eiGen.Close();
				this._envGen.Close();
				this._cGen.Close();
				base.Close();
			}

			// Token: 0x0400154C RID: 5452
			private readonly CipherStream _out;

			// Token: 0x0400154D RID: 5453
			private readonly BerSequenceGenerator _cGen;

			// Token: 0x0400154E RID: 5454
			private readonly BerSequenceGenerator _envGen;

			// Token: 0x0400154F RID: 5455
			private readonly BerSequenceGenerator _eiGen;
		}
	}
}
