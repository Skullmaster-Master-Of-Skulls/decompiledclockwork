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
	// Token: 0x0200043D RID: 1085
	public class CmsAuthenticatedDataStreamGenerator : CmsAuthenticatedGenerator
	{
		// Token: 0x060024DA RID: 9434 RVA: 0x000DFFD9 File Offset: 0x000DEFD9
		public CmsAuthenticatedDataStreamGenerator()
		{
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x000DFFE1 File Offset: 0x000DEFE1
		public CmsAuthenticatedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000DFFEA File Offset: 0x000DEFEA
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x000DFFF3 File Offset: 0x000DEFF3
		public void SetBerEncodeRecipients(bool berEncodeRecipientSet)
		{
			this._berEncodeRecipientSet = berEncodeRecipientSet;
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000DFFFC File Offset: 0x000DEFFC
		private Stream Open(Stream outStr, string macOid, CipherKeyGenerator keyGen)
		{
			byte[] array = keyGen.GenerateKey();
			KeyParameter keyParameter = ParameterUtilities.CreateKeyParameter(macOid, array);
			Asn1Encodable asn1Params = this.GenerateAsn1Parameters(macOid, array);
			ICipherParameters cipherParameters;
			AlgorithmIdentifier algorithmIdentifier = this.GetAlgorithmIdentifier(macOid, keyParameter, asn1Params, out cipherParameters);
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
			return this.Open(outStr, algorithmIdentifier, keyParameter, asn1EncodableVector);
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000E00F0 File Offset: 0x000DF0F0
		protected Stream Open(Stream outStr, AlgorithmIdentifier macAlgId, ICipherParameters cipherParameters, Asn1EncodableVector recipientInfos)
		{
			Stream result;
			try
			{
				BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStr);
				berSequenceGenerator.AddObject(CmsObjectIdentifiers.AuthenticatedData);
				BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
				berSequenceGenerator2.AddObject(new DerInteger(AuthenticatedData.CalculateVersion(null)));
				Stream rawOutputStream = berSequenceGenerator2.GetRawOutputStream();
				Asn1Generator asn1Generator = this._berEncodeRecipientSet ? new BerSetGenerator(rawOutputStream) : new DerSetGenerator(rawOutputStream);
				foreach (object obj in recipientInfos)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					asn1Generator.AddObject(obj2);
				}
				asn1Generator.Close();
				berSequenceGenerator2.AddObject(macAlgId);
				BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(rawOutputStream);
				berSequenceGenerator3.AddObject(CmsObjectIdentifiers.Data);
				Stream stream = CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, false, this._bufferSize);
				IMac mac = MacUtilities.GetMac(macAlgId.ObjectID);
				mac.Init(cipherParameters);
				MacStream macStream = new MacStream(stream, null, mac);
				result = new CmsAuthenticatedDataStreamGenerator.CmsAuthenticatedDataOutputStream(macStream, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
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

		// Token: 0x060024E0 RID: 9440 RVA: 0x000E0258 File Offset: 0x000DF258
		public Stream Open(Stream outStr, string encryptionOid)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
			return this.Open(outStr, encryptionOid, keyGenerator);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x000E028C File Offset: 0x000DF28C
		public Stream Open(Stream outStr, string encryptionOid, int keySize)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
			return this.Open(outStr, encryptionOid, keyGenerator);
		}

		// Token: 0x040019AF RID: 6575
		private int _bufferSize;

		// Token: 0x040019B0 RID: 6576
		private bool _berEncodeRecipientSet;

		// Token: 0x0200043E RID: 1086
		private class CmsAuthenticatedDataOutputStream : BaseOutputStream
		{
			// Token: 0x060024E2 RID: 9442 RVA: 0x000E02BB File Offset: 0x000DF2BB
			public CmsAuthenticatedDataOutputStream(MacStream macStream, BerSequenceGenerator cGen, BerSequenceGenerator authGen, BerSequenceGenerator eiGen)
			{
				this.macStream = macStream;
				this.cGen = cGen;
				this.authGen = authGen;
				this.eiGen = eiGen;
			}

			// Token: 0x060024E3 RID: 9443 RVA: 0x000E02E0 File Offset: 0x000DF2E0
			public override void WriteByte(byte b)
			{
				this.macStream.WriteByte(b);
			}

			// Token: 0x060024E4 RID: 9444 RVA: 0x000E02EE File Offset: 0x000DF2EE
			public override void Write(byte[] bytes, int off, int len)
			{
				this.macStream.Write(bytes, off, len);
			}

			// Token: 0x060024E5 RID: 9445 RVA: 0x000E0300 File Offset: 0x000DF300
			public override void Close()
			{
				this.macStream.Close();
				this.eiGen.Close();
				byte[] str = MacUtilities.DoFinal(this.macStream.WriteMac());
				this.authGen.AddObject(new DerOctetString(str));
				this.authGen.Close();
				this.cGen.Close();
			}

			// Token: 0x040019B1 RID: 6577
			private readonly MacStream macStream;

			// Token: 0x040019B2 RID: 6578
			private readonly BerSequenceGenerator cGen;

			// Token: 0x040019B3 RID: 6579
			private readonly BerSequenceGenerator authGen;

			// Token: 0x040019B4 RID: 6580
			private readonly BerSequenceGenerator eiGen;
		}
	}
}
