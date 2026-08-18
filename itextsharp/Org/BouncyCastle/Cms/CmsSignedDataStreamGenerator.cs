using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000437 RID: 1079
	public class CmsSignedDataStreamGenerator : CmsSignedGenerator
	{
		// Token: 0x060024AB RID: 9387 RVA: 0x000DF2E8 File Offset: 0x000DE2E8
		public CmsSignedDataStreamGenerator()
		{
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000DF31C File Offset: 0x000DE31C
		public CmsSignedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000DF351 File Offset: 0x000DE351
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x000DF35A File Offset: 0x000DE35A
		public void AddDigests(params string[] digestOids)
		{
			this.AddDigests(digestOids);
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x000DF364 File Offset: 0x000DE364
		public void AddDigests(IEnumerable digestOids)
		{
			foreach (object obj in digestOids)
			{
				string digestOid = (string)obj;
				this.ConfigureDigest(digestOid);
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x000DF3B8 File Offset: 0x000DE3B8
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid)
		{
			this.AddSigner(privateKey, cert, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000DF3C9 File Offset: 0x000DE3C9
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid)
		{
			this.AddSigner(privateKey, cert, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x000DF3DC File Offset: 0x000DE3DC
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000DF3F5 File Offset: 0x000DE3F5
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, cert, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000DF410 File Offset: 0x000DE410
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.AddSigner(privateKey, cert, base.GetEncOid(privateKey, digestOid), digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000DF428 File Offset: 0x000DE428
		public void AddSigner(AsymmetricKeyParameter privateKey, X509Certificate cert, string encryptionOid, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.ConfigureDigest(digestOid);
			this._signerInfs.Add(new CmsSignedDataStreamGenerator.SignerInf(this, privateKey, CmsSignedGenerator.GetSignerIdentifier(cert), digestOid, encryptionOid, signedAttrGenerator, unsignedAttrGenerator));
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000DF45D File Offset: 0x000DE45D
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid)
		{
			this.AddSigner(privateKey, subjectKeyID, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000DF46E File Offset: 0x000DE46E
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOid, string digestOid)
		{
			this.AddSigner(privateKey, subjectKeyID, encryptionOid, digestOid, new DefaultSignedAttributeTableGenerator(), null);
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000DF481 File Offset: 0x000DE481
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid, Org.BouncyCastle.Asn1.Cms.AttributeTable signedAttr, Org.BouncyCastle.Asn1.Cms.AttributeTable unsignedAttr)
		{
			this.AddSigner(privateKey, subjectKeyID, digestOid, new DefaultSignedAttributeTableGenerator(signedAttr), new SimpleAttributeTableGenerator(unsignedAttr));
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000DF49A File Offset: 0x000DE49A
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.AddSigner(privateKey, subjectKeyID, base.GetEncOid(privateKey, digestOid), digestOid, signedAttrGenerator, unsignedAttrGenerator);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000DF4B4 File Offset: 0x000DE4B4
		public void AddSigner(AsymmetricKeyParameter privateKey, byte[] subjectKeyID, string encryptionOid, string digestOid, CmsAttributeTableGenerator signedAttrGenerator, CmsAttributeTableGenerator unsignedAttrGenerator)
		{
			this.ConfigureDigest(digestOid);
			this._signerInfs.Add(new CmsSignedDataStreamGenerator.SignerInf(this, privateKey, CmsSignedGenerator.GetSignerIdentifier(subjectKeyID), digestOid, encryptionOid, signedAttrGenerator, unsignedAttrGenerator));
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000DF4E9 File Offset: 0x000DE4E9
		internal override void AddSignerCallback(SignerInformation si)
		{
			this.RegisterDigestOid(si.DigestAlgorithmID.ObjectID.Id);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000DF501 File Offset: 0x000DE501
		public Stream Open(Stream outStream)
		{
			return this.Open(outStream, false);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000DF50B File Offset: 0x000DE50B
		public Stream Open(Stream outStream, bool encapsulate)
		{
			return this.Open(outStream, CmsSignedGenerator.Data, encapsulate);
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000DF51A File Offset: 0x000DE51A
		public Stream Open(Stream outStream, bool encapsulate, Stream dataOutputStream)
		{
			return this.Open(outStream, CmsSignedGenerator.Data, encapsulate, dataOutputStream);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000DF52A File Offset: 0x000DE52A
		public Stream Open(Stream outStream, string signedContentType, bool encapsulate)
		{
			return this.Open(outStream, signedContentType, encapsulate, null);
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000DF538 File Offset: 0x000DE538
		public Stream Open(Stream outStream, string signedContentType, bool encapsulate, Stream dataOutputStream)
		{
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			if (!outStream.CanWrite)
			{
				throw new ArgumentException("Expected writeable stream", "outStream");
			}
			if (dataOutputStream != null && !dataOutputStream.CanWrite)
			{
				throw new ArgumentException("Expected writeable stream", "dataOutputStream");
			}
			this._messageDigestsLocked = true;
			BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
			berSequenceGenerator.AddObject(CmsObjectIdentifiers.SignedData);
			BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
			berSequenceGenerator2.AddObject(this.CalculateVersion(signedContentType));
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in this._messageDigestOids)
			{
				string identifier = (string)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new AlgorithmIdentifier(new DerObjectIdentifier(identifier), DerNull.Instance)
				});
			}
			byte[] encoded = new DerSet(asn1EncodableVector).GetEncoded();
			berSequenceGenerator2.GetRawOutputStream().Write(encoded, 0, encoded.Length);
			BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(berSequenceGenerator2.GetRawOutputStream());
			berSequenceGenerator3.AddObject(new DerObjectIdentifier(signedContentType));
			Stream s = encapsulate ? CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, true, this._bufferSize) : null;
			Stream safeTeeOutputStream = CmsSignedDataStreamGenerator.GetSafeTeeOutputStream(dataOutputStream, s);
			Stream outStream2 = CmsSignedDataStreamGenerator.AttachDigestsToOutputStream(this._messageDigests.Values, safeTeeOutputStream);
			return new CmsSignedDataStreamGenerator.CmsSignedDataOutputStream(this, outStream2, signedContentType, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000DF6BC File Offset: 0x000DE6BC
		private void RegisterDigestOid(string digestOid)
		{
			if (this._messageDigestsLocked)
			{
				if (!this._messageDigestOids.Contains(digestOid))
				{
					throw new InvalidOperationException("Cannot register new digest OIDs after the data stream is opened");
				}
			}
			else
			{
				this._messageDigestOids.Add(digestOid);
			}
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000DF6EC File Offset: 0x000DE6EC
		private void ConfigureDigest(string digestOid)
		{
			this.RegisterDigestOid(digestOid);
			string digestAlgName = CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(digestOid);
			if ((IDigest)this._messageDigests[digestAlgName] == null)
			{
				if (this._messageDigestsLocked)
				{
					throw new InvalidOperationException("Cannot configure new digests after the data stream is opened");
				}
				IDigest digestInstance = CmsSignedDataStreamGenerator.Helper.GetDigestInstance(digestAlgName);
				this._messageDigests[digestAlgName] = digestInstance;
			}
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000DF750 File Offset: 0x000DE750
		private DerInteger CalculateVersion(string contentOid)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			if (this._certs != null)
			{
				foreach (object obj in this._certs)
				{
					if (obj is Asn1TaggedObject)
					{
						Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
						if (asn1TaggedObject.TagNo == 1)
						{
							flag3 = true;
						}
						else if (asn1TaggedObject.TagNo == 2)
						{
							flag4 = true;
						}
						else if (asn1TaggedObject.TagNo == 3)
						{
							flag = true;
							break;
						}
					}
				}
			}
			if (flag)
			{
				return new DerInteger(5);
			}
			if (this._crls != null)
			{
				foreach (object obj2 in this._crls)
				{
					if (obj2 is Asn1TaggedObject)
					{
						flag2 = true;
						break;
					}
				}
			}
			if (flag2)
			{
				return new DerInteger(5);
			}
			if (flag4)
			{
				return new DerInteger(4);
			}
			if (flag3)
			{
				return new DerInteger(3);
			}
			if (contentOid.Equals(CmsSignedGenerator.Data) && !this.CheckForVersion3(this._signers))
			{
				return new DerInteger(1);
			}
			return new DerInteger(3);
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000DF89C File Offset: 0x000DE89C
		private bool CheckForVersion3(IList signerInfos)
		{
			foreach (object obj in signerInfos)
			{
				SignerInformation signerInformation = (SignerInformation)obj;
				SignerInfo instance = SignerInfo.GetInstance(signerInformation.ToSignerInfo());
				if (instance.Version.Value.IntValue == 3)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000DF914 File Offset: 0x000DE914
		private static Stream AttachDigestsToOutputStream(ICollection digests, Stream s)
		{
			Stream stream = s;
			foreach (object obj in digests)
			{
				IDigest dig = (IDigest)obj;
				stream = CmsSignedDataStreamGenerator.GetSafeTeeOutputStream(stream, new CmsSignedGenerator.DigOutputStream(dig));
			}
			return stream;
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000DF974 File Offset: 0x000DE974
		private static Stream GetSafeOutputStream(Stream s)
		{
			if (s == null)
			{
				return new CmsSignedDataStreamGenerator.NullOutputStream();
			}
			return s;
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000DF980 File Offset: 0x000DE980
		private static Stream GetSafeTeeOutputStream(Stream s1, Stream s2)
		{
			if (s1 == null)
			{
				return CmsSignedDataStreamGenerator.GetSafeOutputStream(s2);
			}
			if (s2 == null)
			{
				return CmsSignedDataStreamGenerator.GetSafeOutputStream(s1);
			}
			return new CmsSignedDataStreamGenerator.TeeOutputStream(s1, s2);
		}

		// Token: 0x04001999 RID: 6553
		private static readonly CmsSignedHelper Helper = CmsSignedHelper.Instance;

		// Token: 0x0400199A RID: 6554
		private readonly ArrayList _signerInfs = new ArrayList();

		// Token: 0x0400199B RID: 6555
		private readonly ISet _messageDigestOids = new HashSet();

		// Token: 0x0400199C RID: 6556
		private readonly Hashtable _messageDigests = new Hashtable();

		// Token: 0x0400199D RID: 6557
		private readonly Hashtable _messageHashes = new Hashtable();

		// Token: 0x0400199E RID: 6558
		private bool _messageDigestsLocked;

		// Token: 0x0400199F RID: 6559
		private int _bufferSize;

		// Token: 0x02000438 RID: 1080
		private class SignerInf
		{
			// Token: 0x060024C9 RID: 9417 RVA: 0x000DF9A9 File Offset: 0x000DE9A9
			internal SignerInf(CmsSignedDataStreamGenerator outer, AsymmetricKeyParameter key, SignerIdentifier signerIdentifier, string digestOID, string encOID, CmsAttributeTableGenerator sAttr, CmsAttributeTableGenerator unsAttr)
			{
				this.outer = outer;
				this._key = key;
				this._signerIdentifier = signerIdentifier;
				this._digestOID = digestOID;
				this._encOID = encOID;
				this._sAttr = sAttr;
				this._unsAttr = unsAttr;
			}

			// Token: 0x17000650 RID: 1616
			// (get) Token: 0x060024CA RID: 9418 RVA: 0x000DF9E6 File Offset: 0x000DE9E6
			internal AlgorithmIdentifier DigestAlgorithmID
			{
				get
				{
					return new AlgorithmIdentifier(new DerObjectIdentifier(this._digestOID), DerNull.Instance);
				}
			}

			// Token: 0x060024CB RID: 9419 RVA: 0x000DFA00 File Offset: 0x000DEA00
			internal SignerInfo ToSignerInfo(DerObjectIdentifier contentType)
			{
				string digestAlgName = CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(this._digestOID);
				string encryptionAlgName = CmsSignedDataStreamGenerator.Helper.GetEncryptionAlgName(this._encOID);
				string algorithm = digestAlgName + "with" + encryptionAlgName;
				AlgorithmIdentifier digestAlgorithmID = this.DigestAlgorithmID;
				byte[] array = (byte[])this.outer._messageHashes[CmsSignedDataStreamGenerator.Helper.GetDigestAlgName(this._digestOID)];
				this.outer._digests[this._digestOID] = array.Clone();
				byte[] array2 = array;
				Asn1Set asn1Set = null;
				ISigner signatureInstance;
				if (this._sAttr != null)
				{
					IDictionary baseParameters = this.outer.GetBaseParameters(contentType, digestAlgorithmID, array);
					Org.BouncyCastle.Asn1.Cms.AttributeTable attributes = this._sAttr.GetAttributes(baseParameters);
					asn1Set = this.outer.GetAttributeSet(attributes);
					array2 = asn1Set.GetEncoded("DER");
					signatureInstance = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance(algorithm);
				}
				else if (encryptionAlgName.Equals("RSA"))
				{
					DigestInfo digestInfo = new DigestInfo(digestAlgorithmID, array);
					array2 = digestInfo.GetEncoded("DER");
					signatureInstance = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance("RSA");
				}
				else
				{
					if (!encryptionAlgName.Equals("DSA"))
					{
						throw new SignatureException("algorithm: " + encryptionAlgName + " not supported in base signatures.");
					}
					signatureInstance = CmsSignedDataStreamGenerator.Helper.GetSignatureInstance("NONEwithDSA");
				}
				signatureInstance.Init(true, new ParametersWithRandom(this._key, this.outer.rand));
				signatureInstance.BlockUpdate(array2, 0, array2.Length);
				byte[] array3 = signatureInstance.GenerateSignature();
				Asn1Set unauthenticatedAttributes = null;
				if (this._unsAttr != null)
				{
					IDictionary baseParameters2 = this.outer.GetBaseParameters(contentType, digestAlgorithmID, array);
					baseParameters2[CmsAttributeTableParameter.Signature] = array3.Clone();
					Org.BouncyCastle.Asn1.Cms.AttributeTable attributes2 = this._unsAttr.GetAttributes(baseParameters2);
					unauthenticatedAttributes = this.outer.GetAttributeSet(attributes2);
				}
				Asn1Encodable defaultX509Parameters = SignerUtilities.GetDefaultX509Parameters(algorithm);
				AlgorithmIdentifier encAlgorithmIdentifier = CmsSignedGenerator.GetEncAlgorithmIdentifier(new DerObjectIdentifier(this._encOID), defaultX509Parameters);
				return new SignerInfo(this._signerIdentifier, digestAlgorithmID, asn1Set, encAlgorithmIdentifier, new DerOctetString(array3), unauthenticatedAttributes);
			}

			// Token: 0x040019A0 RID: 6560
			private readonly CmsSignedDataStreamGenerator outer;

			// Token: 0x040019A1 RID: 6561
			private readonly AsymmetricKeyParameter _key;

			// Token: 0x040019A2 RID: 6562
			private readonly SignerIdentifier _signerIdentifier;

			// Token: 0x040019A3 RID: 6563
			private readonly string _digestOID;

			// Token: 0x040019A4 RID: 6564
			private readonly string _encOID;

			// Token: 0x040019A5 RID: 6565
			private readonly CmsAttributeTableGenerator _sAttr;

			// Token: 0x040019A6 RID: 6566
			private readonly CmsAttributeTableGenerator _unsAttr;
		}

		// Token: 0x02000439 RID: 1081
		private class NullOutputStream : BaseOutputStream
		{
			// Token: 0x060024CC RID: 9420 RVA: 0x000DFC08 File Offset: 0x000DEC08
			public override void WriteByte(byte b)
			{
			}

			// Token: 0x060024CD RID: 9421 RVA: 0x000DFC0A File Offset: 0x000DEC0A
			public override void Write(byte[] buffer, int offset, int count)
			{
			}
		}

		// Token: 0x0200043A RID: 1082
		private class TeeOutputStream : BaseOutputStream
		{
			// Token: 0x060024CF RID: 9423 RVA: 0x000DFC14 File Offset: 0x000DEC14
			public TeeOutputStream(Stream dataOutputStream, Stream digStream)
			{
				this.s1 = dataOutputStream;
				this.s2 = digStream;
			}

			// Token: 0x060024D0 RID: 9424 RVA: 0x000DFC2A File Offset: 0x000DEC2A
			public override void Write(byte[] buffer, int offset, int count)
			{
				this.s1.Write(buffer, offset, count);
				this.s2.Write(buffer, offset, count);
			}

			// Token: 0x060024D1 RID: 9425 RVA: 0x000DFC48 File Offset: 0x000DEC48
			public override void WriteByte(byte b)
			{
				this.s1.WriteByte(b);
				this.s2.WriteByte(b);
			}

			// Token: 0x060024D2 RID: 9426 RVA: 0x000DFC62 File Offset: 0x000DEC62
			public override void Close()
			{
				this.s1.Close();
				this.s2.Close();
			}

			// Token: 0x040019A7 RID: 6567
			private readonly Stream s1;

			// Token: 0x040019A8 RID: 6568
			private readonly Stream s2;
		}

		// Token: 0x0200043B RID: 1083
		private class CmsSignedDataOutputStream : BaseOutputStream
		{
			// Token: 0x060024D3 RID: 9427 RVA: 0x000DFC7A File Offset: 0x000DEC7A
			public CmsSignedDataOutputStream(CmsSignedDataStreamGenerator outer, Stream outStream, string contentOID, BerSequenceGenerator sGen, BerSequenceGenerator sigGen, BerSequenceGenerator eiGen)
			{
				this.outer = outer;
				this._out = outStream;
				this._contentOID = new DerObjectIdentifier(contentOID);
				this._sGen = sGen;
				this._sigGen = sigGen;
				this._eiGen = eiGen;
			}

			// Token: 0x060024D4 RID: 9428 RVA: 0x000DFCB4 File Offset: 0x000DECB4
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x060024D5 RID: 9429 RVA: 0x000DFCC2 File Offset: 0x000DECC2
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x060024D6 RID: 9430 RVA: 0x000DFCD4 File Offset: 0x000DECD4
			public override void Close()
			{
				this._out.Close();
				this._eiGen.Close();
				this.outer._digests.Clear();
				if (this.outer._certs.Count > 0)
				{
					Asn1Set obj = CmsUtilities.CreateBerSetFromList(this.outer._certs);
					CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new BerTaggedObject(false, 0, obj));
				}
				if (this.outer._crls.Count > 0)
				{
					Asn1Set obj2 = CmsUtilities.CreateBerSetFromList(this.outer._crls);
					CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new BerTaggedObject(false, 1, obj2));
				}
				foreach (object obj3 in this.outer._messageDigests)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					this.outer._messageHashes.Add(dictionaryEntry.Key, DigestUtilities.DoFinal((IDigest)dictionaryEntry.Value));
				}
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
				foreach (object obj4 in this.outer._signers)
				{
					SignerInformation signerInformation = (SignerInformation)obj4;
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						signerInformation.ToSignerInfo()
					});
				}
				foreach (object obj5 in this.outer._signerInfs)
				{
					CmsSignedDataStreamGenerator.SignerInf signerInf = (CmsSignedDataStreamGenerator.SignerInf)obj5;
					try
					{
						asn1EncodableVector.Add(new Asn1Encodable[]
						{
							signerInf.ToSignerInfo(this._contentOID)
						});
					}
					catch (IOException arg)
					{
						throw new CmsStreamException("encoding error." + arg);
					}
					catch (InvalidKeyException e)
					{
						throw new CmsStreamException("key inappropriate for signature.", e);
					}
					catch (SignatureException arg2)
					{
						throw new CmsStreamException("error creating signature." + arg2);
					}
					catch (CertificateEncodingException arg3)
					{
						throw new CmsStreamException("error creating sid." + arg3);
					}
					catch (SecurityUtilityException arg4)
					{
						throw new CmsStreamException("unknown signature algorithm." + arg4);
					}
				}
				CmsSignedDataStreamGenerator.CmsSignedDataOutputStream.WriteToGenerator(this._sigGen, new DerSet(asn1EncodableVector));
				this._sigGen.Close();
				this._sGen.Close();
				base.Close();
			}

			// Token: 0x060024D7 RID: 9431 RVA: 0x000DFFA4 File Offset: 0x000DEFA4
			private static void WriteToGenerator(Asn1Generator ag, Asn1Encodable ae)
			{
				byte[] encoded = ae.GetEncoded();
				ag.GetRawOutputStream().Write(encoded, 0, encoded.Length);
			}

			// Token: 0x040019A9 RID: 6569
			private readonly CmsSignedDataStreamGenerator outer;

			// Token: 0x040019AA RID: 6570
			private Stream _out;

			// Token: 0x040019AB RID: 6571
			private DerObjectIdentifier _contentOID;

			// Token: 0x040019AC RID: 6572
			private BerSequenceGenerator _sGen;

			// Token: 0x040019AD RID: 6573
			private BerSequenceGenerator _sigGen;

			// Token: 0x040019AE RID: 6574
			private BerSequenceGenerator _eiGen;
		}
	}
}
