using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x020002AD RID: 685
	public abstract class RecipientInformation
	{
		// Token: 0x060019E5 RID: 6629 RVA: 0x00099F0C File Offset: 0x00098F0C
		internal RecipientInformation(AlgorithmIdentifier encAlg, AlgorithmIdentifier macAlg, AlgorithmIdentifier authEncAlg, AlgorithmIdentifier keyEncAlg, Stream data)
		{
			if (!data.CanRead)
			{
				throw new ArgumentException("Expected input stream", "data");
			}
			this.encAlg = encAlg;
			this.macAlg = macAlg;
			this.authEncAlg = authEncAlg;
			this.keyEncAlg = keyEncAlg;
			this.data = data;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00099F68 File Offset: 0x00098F68
		internal AlgorithmIdentifier GetActiveAlgID()
		{
			if (this.encAlg != null)
			{
				return this.encAlg;
			}
			if (this.macAlg != null)
			{
				return this.macAlg;
			}
			return this.authEncAlg;
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00099F8E File Offset: 0x00098F8E
		public RecipientID RecipientID
		{
			get
			{
				return this.rid;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x00099F96 File Offset: 0x00098F96
		public AlgorithmIdentifier KeyEncryptionAlgorithmID
		{
			get
			{
				return this.keyEncAlg;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00099F9E File Offset: 0x00098F9E
		public string KeyEncryptionAlgOid
		{
			get
			{
				return this.keyEncAlg.ObjectID.Id;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00099FB0 File Offset: 0x00098FB0
		public Asn1Object KeyEncryptionAlgParams
		{
			get
			{
				Asn1Encodable parameters = this.keyEncAlg.Parameters;
				if (parameters != null)
				{
					return parameters.ToAsn1Object();
				}
				return null;
			}
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00099FD4 File Offset: 0x00098FD4
		internal CmsTypedStream GetContentFromSessionKey(KeyParameter sKey)
		{
			CmsTypedStream result;
			try
			{
				Stream stream = this.data;
				if (this.encAlg != null)
				{
					IBufferedCipher cipher = CipherUtilities.GetCipher(this.encAlg.ObjectID);
					Asn1Encodable parameters = this.encAlg.Parameters;
					Asn1Object asn1Object = (parameters == null) ? null : parameters.ToAsn1Object();
					ICipherParameters cipherParameters = sKey;
					if (asn1Object != null && !(asn1Object is Asn1Null))
					{
						cipherParameters = ParameterUtilities.GetCipherParameters(this.encAlg.ObjectID, cipherParameters, asn1Object);
					}
					else
					{
						string id = this.encAlg.ObjectID.Id;
						if (id.Equals(CmsEnvelopedGenerator.DesEde3Cbc) || id.Equals("1.3.6.1.4.1.188.7.1.1.2") || id.Equals("1.2.840.113533.7.66.10"))
						{
							cipherParameters = new ParametersWithIV(cipherParameters, new byte[8]);
						}
					}
					cipher.Init(false, cipherParameters);
					stream = new CipherStream(stream, cipher, null);
				}
				if (this.macAlg != null)
				{
					stream = (this.macStream = RecipientInformation.CreateMacStream(this.macAlg, sKey, stream));
				}
				if (this.authEncAlg != null)
				{
					throw new CmsException("AuthEnveloped data decryption not yet implemented");
				}
				result = new CmsTypedStream(stream);
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
				throw new CmsException("error decoding algorithm parameters.", e3);
			}
			return result;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0009A134 File Offset: 0x00099134
		private static MacStream CreateMacStream(AlgorithmIdentifier macAlg, KeyParameter sKey, Stream inStream)
		{
			IMac mac = MacUtilities.GetMac(macAlg.ObjectID);
			mac.Init(sKey);
			return new MacStream(inStream, mac, null);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0009A15C File Offset: 0x0009915C
		public byte[] GetContent(ICipherParameters key)
		{
			byte[] result;
			try
			{
				if (this.data is MemoryStream)
				{
					this.data.Seek(0L, SeekOrigin.Begin);
				}
				result = CmsUtilities.StreamToByteArray(this.GetContentStream(key).ContentStream);
			}
			catch (IOException arg)
			{
				throw new Exception("unable to parse internal stream: " + arg);
			}
			return result;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0009A1BC File Offset: 0x000991BC
		public byte[] GetMac()
		{
			if (this.macStream != null && this.resultMac == null)
			{
				this.resultMac = MacUtilities.DoFinal(this.macStream.ReadMac());
			}
			return Arrays.Clone(this.resultMac);
		}

		// Token: 0x060019EF RID: 6639
		public abstract CmsTypedStream GetContentStream(ICipherParameters key);

		// Token: 0x04001146 RID: 4422
		internal RecipientID rid = new RecipientID();

		// Token: 0x04001147 RID: 4423
		internal AlgorithmIdentifier encAlg;

		// Token: 0x04001148 RID: 4424
		internal AlgorithmIdentifier macAlg;

		// Token: 0x04001149 RID: 4425
		internal AlgorithmIdentifier authEncAlg;

		// Token: 0x0400114A RID: 4426
		internal AlgorithmIdentifier keyEncAlg;

		// Token: 0x0400114B RID: 4427
		internal Stream data;

		// Token: 0x0400114C RID: 4428
		private MacStream macStream;

		// Token: 0x0400114D RID: 4429
		private byte[] resultMac;
	}
}
