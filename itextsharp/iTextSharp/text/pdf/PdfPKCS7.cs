using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using iTextSharp.text.error_messages;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Tsp;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x020000C1 RID: 193
	public class PdfPKCS7
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x0001EFA8 File Offset: 0x0001DFA8
		static PdfPKCS7()
		{
			PdfPKCS7.digestNames["1.2.840.113549.2.5"] = "MD5";
			PdfPKCS7.digestNames["1.2.840.113549.2.2"] = "MD2";
			PdfPKCS7.digestNames["1.3.14.3.2.26"] = "SHA1";
			PdfPKCS7.digestNames["2.16.840.1.101.3.4.2.4"] = "SHA224";
			PdfPKCS7.digestNames["2.16.840.1.101.3.4.2.1"] = "SHA256";
			PdfPKCS7.digestNames["2.16.840.1.101.3.4.2.2"] = "SHA384";
			PdfPKCS7.digestNames["2.16.840.1.101.3.4.2.3"] = "SHA512";
			PdfPKCS7.digestNames["1.3.36.3.2.2"] = "RIPEMD128";
			PdfPKCS7.digestNames["1.3.36.3.2.1"] = "RIPEMD160";
			PdfPKCS7.digestNames["1.3.36.3.2.3"] = "RIPEMD256";
			PdfPKCS7.digestNames["1.2.840.113549.1.1.4"] = "MD5";
			PdfPKCS7.digestNames["1.2.840.113549.1.1.2"] = "MD2";
			PdfPKCS7.digestNames["1.2.840.113549.1.1.5"] = "SHA1";
			PdfPKCS7.digestNames["1.2.840.113549.1.1.14"] = "SHA224";
			PdfPKCS7.digestNames["1.2.840.113549.1.1.11"] = "SHA256";
			PdfPKCS7.digestNames["1.2.840.113549.1.1.12"] = "SHA384";
			PdfPKCS7.digestNames["1.2.840.113549.1.1.13"] = "SHA512";
			PdfPKCS7.digestNames["1.2.840.113549.2.5"] = "MD5";
			PdfPKCS7.digestNames["1.2.840.113549.2.2"] = "MD2";
			PdfPKCS7.digestNames["1.2.840.10040.4.3"] = "SHA1";
			PdfPKCS7.digestNames["2.16.840.1.101.3.4.3.1"] = "SHA224";
			PdfPKCS7.digestNames["2.16.840.1.101.3.4.3.2"] = "SHA256";
			PdfPKCS7.digestNames["2.16.840.1.101.3.4.3.3"] = "SHA384";
			PdfPKCS7.digestNames["2.16.840.1.101.3.4.3.4"] = "SHA512";
			PdfPKCS7.digestNames["1.3.36.3.3.1.3"] = "RIPEMD128";
			PdfPKCS7.digestNames["1.3.36.3.3.1.2"] = "RIPEMD160";
			PdfPKCS7.digestNames["1.3.36.3.3.1.4"] = "RIPEMD256";
			PdfPKCS7.algorithmNames["1.2.840.113549.1.1.1"] = "RSA";
			PdfPKCS7.algorithmNames["1.2.840.10040.4.1"] = "DSA";
			PdfPKCS7.algorithmNames["1.2.840.113549.1.1.2"] = "RSA";
			PdfPKCS7.algorithmNames["1.2.840.113549.1.1.4"] = "RSA";
			PdfPKCS7.algorithmNames["1.2.840.113549.1.1.5"] = "RSA";
			PdfPKCS7.algorithmNames["1.2.840.113549.1.1.14"] = "RSA";
			PdfPKCS7.algorithmNames["1.2.840.113549.1.1.11"] = "RSA";
			PdfPKCS7.algorithmNames["1.2.840.113549.1.1.12"] = "RSA";
			PdfPKCS7.algorithmNames["1.2.840.113549.1.1.13"] = "RSA";
			PdfPKCS7.algorithmNames["1.2.840.10040.4.3"] = "DSA";
			PdfPKCS7.algorithmNames["2.16.840.1.101.3.4.3.1"] = "DSA";
			PdfPKCS7.algorithmNames["2.16.840.1.101.3.4.3.2"] = "DSA";
			PdfPKCS7.algorithmNames["1.3.36.3.3.1.3"] = "RSA";
			PdfPKCS7.algorithmNames["1.3.36.3.3.1.2"] = "RSA";
			PdfPKCS7.algorithmNames["1.3.36.3.3.1.4"] = "RSA";
			PdfPKCS7.allowedDigests["MD5"] = "1.2.840.113549.2.5";
			PdfPKCS7.allowedDigests["MD2"] = "1.2.840.113549.2.2";
			PdfPKCS7.allowedDigests["SHA1"] = "1.3.14.3.2.26";
			PdfPKCS7.allowedDigests["SHA224"] = "2.16.840.1.101.3.4.2.4";
			PdfPKCS7.allowedDigests["SHA256"] = "2.16.840.1.101.3.4.2.1";
			PdfPKCS7.allowedDigests["SHA384"] = "2.16.840.1.101.3.4.2.2";
			PdfPKCS7.allowedDigests["SHA512"] = "2.16.840.1.101.3.4.2.3";
			PdfPKCS7.allowedDigests["MD-5"] = "1.2.840.113549.2.5";
			PdfPKCS7.allowedDigests["MD-2"] = "1.2.840.113549.2.2";
			PdfPKCS7.allowedDigests["SHA-1"] = "1.3.14.3.2.26";
			PdfPKCS7.allowedDigests["SHA-224"] = "2.16.840.1.101.3.4.2.4";
			PdfPKCS7.allowedDigests["SHA-256"] = "2.16.840.1.101.3.4.2.1";
			PdfPKCS7.allowedDigests["SHA-384"] = "2.16.840.1.101.3.4.2.2";
			PdfPKCS7.allowedDigests["SHA-512"] = "2.16.840.1.101.3.4.2.3";
			PdfPKCS7.allowedDigests["RIPEMD128"] = "1.3.36.3.2.2";
			PdfPKCS7.allowedDigests["RIPEMD-128"] = "1.3.36.3.2.2";
			PdfPKCS7.allowedDigests["RIPEMD160"] = "1.3.36.3.2.1";
			PdfPKCS7.allowedDigests["RIPEMD-160"] = "1.3.36.3.2.1";
			PdfPKCS7.allowedDigests["RIPEMD256"] = "1.3.36.3.2.3";
			PdfPKCS7.allowedDigests["RIPEMD-256"] = "1.3.36.3.2.3";
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001F4AC File Offset: 0x0001E4AC
		public static string GetDigest(string oid)
		{
			string result;
			if (PdfPKCS7.digestNames.TryGetValue(oid, out result))
			{
				return result;
			}
			return oid;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001F4CC File Offset: 0x0001E4CC
		public static string GetAlgorithm(string oid)
		{
			string result;
			if (PdfPKCS7.algorithmNames.TryGetValue(oid, out result))
			{
				return result;
			}
			return oid;
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001F4EB File Offset: 0x0001E4EB
		public TimeStampToken TimeStampToken
		{
			get
			{
				return this.timeStampToken;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001F4F3 File Offset: 0x0001E4F3
		public DateTime TimeStampDate
		{
			get
			{
				if (this.timeStampToken == null)
				{
					return DateTime.MaxValue;
				}
				return this.timeStampToken.TimeStampInfo.GenTime;
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001F514 File Offset: 0x0001E514
		public PdfPKCS7(byte[] contentsKey, byte[] certsKey)
		{
			X509CertificateParser x509CertificateParser = new X509CertificateParser();
			this.certs = new List<X509Certificate>();
			foreach (object obj in x509CertificateParser.ReadCertificates(certsKey))
			{
				X509Certificate item = (X509Certificate)obj;
				this.certs.Add(item);
			}
			this.signCerts = this.certs;
			this.signCert = this.certs[0];
			this.crls = new List<X509Crl>();
			Asn1InputStream asn1InputStream = new Asn1InputStream(new MemoryStream(contentsKey));
			this.digest = ((DerOctetString)asn1InputStream.ReadObject()).GetOctets();
			this.sig = SignerUtilities.GetSigner("SHA1withRSA");
			this.sig.Init(false, this.signCert.GetPublicKey());
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001F600 File Offset: 0x0001E600
		public BasicOcspResp Ocsp
		{
			get
			{
				return this.basicResp;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001F608 File Offset: 0x0001E608
		private void FindCRL(Asn1Sequence seq)
		{
			this.crls = new List<X509Crl>();
			for (int i = 0; i < seq.Count; i++)
			{
				X509CrlParser x509CrlParser = new X509CrlParser();
				X509Crl item = x509CrlParser.ReadCrl(seq[i].GetDerEncoded());
				this.crls.Add(item);
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001F658 File Offset: 0x0001E658
		private void FindOcsp(Asn1Sequence seq)
		{
			this.basicResp = null;
			while (!(seq[0] is DerObjectIdentifier) || !((DerObjectIdentifier)seq[0]).Id.Equals(OcspObjectIdentifiers.PkixOcspBasic.Id))
			{
				bool flag = true;
				int i = 0;
				while (i < seq.Count)
				{
					if (seq[i] is Asn1Sequence)
					{
						seq = (Asn1Sequence)seq[0];
						flag = false;
						break;
					}
					if (seq[i] is Asn1TaggedObject)
					{
						Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
						if (asn1TaggedObject.GetObject() is Asn1Sequence)
						{
							seq = (Asn1Sequence)asn1TaggedObject.GetObject();
							flag = false;
							break;
						}
						return;
					}
					else
					{
						i++;
					}
				}
				if (flag)
				{
					return;
				}
			}
			DerOctetString derOctetString = (DerOctetString)seq[1];
			Asn1InputStream asn1InputStream = new Asn1InputStream(derOctetString.GetOctets());
			BasicOcspResponse instance = BasicOcspResponse.GetInstance(asn1InputStream.ReadObject());
			this.basicResp = new BasicOcspResp(instance);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001F748 File Offset: 0x0001E748
		public PdfPKCS7(byte[] contentsKey)
		{
			Asn1InputStream asn1InputStream = new Asn1InputStream(new MemoryStream(contentsKey));
			Asn1Object asn1Object;
			try
			{
				asn1Object = asn1InputStream.ReadObject();
			}
			catch
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("can.t.decode.pkcs7signeddata.object"));
			}
			if (!(asn1Object is Asn1Sequence))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("not.a.valid.pkcs.7.object.not.a.sequence"));
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)asn1Object;
			DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)asn1Sequence[0];
			if (!derObjectIdentifier.Id.Equals("1.2.840.113549.1.7.2"))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("not.a.valid.pkcs.7.object.not.signed.data"));
			}
			Asn1Sequence asn1Sequence2 = (Asn1Sequence)((DerTaggedObject)asn1Sequence[1]).GetObject();
			this.version = ((DerInteger)asn1Sequence2[0]).Value.IntValue;
			this.digestalgos = new Dictionary<string, object>();
			foreach (object obj in ((Asn1Set)asn1Sequence2[1]))
			{
				Asn1Sequence asn1Sequence3 = (Asn1Sequence)obj;
				DerObjectIdentifier derObjectIdentifier2 = (DerObjectIdentifier)asn1Sequence3[0];
				this.digestalgos[derObjectIdentifier2.Id] = null;
			}
			X509CertificateParser x509CertificateParser = new X509CertificateParser();
			this.certs = new List<X509Certificate>();
			foreach (object obj2 in x509CertificateParser.ReadCertificates(contentsKey))
			{
				X509Certificate item = (X509Certificate)obj2;
				this.certs.Add(item);
			}
			this.crls = new List<X509Crl>();
			Asn1Sequence asn1Sequence4 = (Asn1Sequence)asn1Sequence2[2];
			if (asn1Sequence4.Count > 1)
			{
				DerOctetString derOctetString = (DerOctetString)((DerTaggedObject)asn1Sequence4[1]).GetObject();
				this.RSAdata = derOctetString.GetOctets();
			}
			int num = 3;
			while (asn1Sequence2[num] is DerTaggedObject)
			{
				num++;
			}
			Asn1Set asn1Set = (Asn1Set)asn1Sequence2[num];
			if (asn1Set.Count != 1)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("this.pkcs.7.object.has.multiple.signerinfos.only.one.is.supported.at.this.time"));
			}
			Asn1Sequence asn1Sequence5 = (Asn1Sequence)asn1Set[0];
			this.signerversion = ((DerInteger)asn1Sequence5[0]).Value.IntValue;
			Asn1Sequence asn1Sequence6 = (Asn1Sequence)asn1Sequence5[1];
			Org.BouncyCastle.Asn1.X509.X509Name instance = Org.BouncyCastle.Asn1.X509.X509Name.GetInstance(asn1Sequence6[0]);
			BigInteger value = ((DerInteger)asn1Sequence6[1]).Value;
			foreach (X509Certificate x509Certificate in this.certs)
			{
				if (instance.Equivalent(x509Certificate.IssuerDN) && value.Equals(x509Certificate.SerialNumber))
				{
					this.signCert = x509Certificate;
					break;
				}
			}
			if (this.signCert == null)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("can.t.find.signing.certificate.with.serial.1", instance.ToString() + " / " + value.ToString(16)));
			}
			this.CalcSignCertificateChain();
			this.digestAlgorithm = ((DerObjectIdentifier)((Asn1Sequence)asn1Sequence5[2])[0]).Id;
			num = 3;
			if (asn1Sequence5[num] is Asn1TaggedObject)
			{
				Asn1TaggedObject obj3 = (Asn1TaggedObject)asn1Sequence5[num];
				Asn1Set instance2 = Asn1Set.GetInstance(obj3, false);
				this.sigAttr = instance2.GetEncoded("DER");
				for (int i = 0; i < instance2.Count; i++)
				{
					Asn1Sequence asn1Sequence7 = (Asn1Sequence)instance2[i];
					if (((DerObjectIdentifier)asn1Sequence7[0]).Id.Equals("1.2.840.113549.1.9.4"))
					{
						Asn1Set asn1Set2 = (Asn1Set)asn1Sequence7[1];
						this.digestAttr = ((DerOctetString)asn1Set2[0]).GetOctets();
					}
					else if (((DerObjectIdentifier)asn1Sequence7[0]).Id.Equals("1.2.840.113583.1.1.8"))
					{
						Asn1Set asn1Set3 = (Asn1Set)asn1Sequence7[1];
						Asn1Sequence asn1Sequence8 = (Asn1Sequence)asn1Set3[0];
						for (int j = 0; j < asn1Sequence8.Count; j++)
						{
							Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Sequence8[j];
							if (asn1TaggedObject.TagNo == 1)
							{
								Asn1Sequence seq = (Asn1Sequence)asn1TaggedObject.GetObject();
								this.FindOcsp(seq);
							}
							if (asn1TaggedObject.TagNo == 0)
							{
								Asn1Sequence seq2 = (Asn1Sequence)asn1TaggedObject.GetObject();
								this.FindCRL(seq2);
							}
						}
					}
				}
				if (this.digestAttr == null)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("authenticated.attribute.is.missing.the.digest"));
				}
				num++;
			}
			this.digestEncryptionAlgorithm = ((DerObjectIdentifier)((Asn1Sequence)asn1Sequence5[num++])[0]).Id;
			this.digest = ((DerOctetString)asn1Sequence5[num++]).GetOctets();
			if (num < asn1Sequence5.Count && asn1Sequence5[num] is DerTaggedObject)
			{
				DerTaggedObject obj4 = (DerTaggedObject)asn1Sequence5[num];
				Asn1Set instance3 = Asn1Set.GetInstance(obj4, false);
				Org.BouncyCastle.Asn1.Cms.AttributeTable attributeTable = new Org.BouncyCastle.Asn1.Cms.AttributeTable(instance3);
				Org.BouncyCastle.Asn1.Cms.Attribute attribute = attributeTable[PkcsObjectIdentifiers.IdAASignatureTimeStampToken];
				if (attribute != null && attribute.AttrValues.Count > 0)
				{
					Asn1Set attrValues = attribute.AttrValues;
					Asn1Sequence instance4 = Asn1Sequence.GetInstance(attrValues[0]);
					Org.BouncyCastle.Asn1.Cms.ContentInfo instance5 = Org.BouncyCastle.Asn1.Cms.ContentInfo.GetInstance(instance4);
					this.timeStampToken = new TimeStampToken(instance5);
				}
			}
			if (this.RSAdata != null || this.digestAttr != null)
			{
				this.messageDigest = this.GetHashClass();
			}
			this.sig = SignerUtilities.GetSigner(this.GetDigestAlgorithm());
			this.sig.Init(false, this.signCert.GetPublicKey());
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001FD1C File Offset: 0x0001ED1C
		public PdfPKCS7(ICipherParameters privKey, X509Certificate[] certChain, object[] crlList, string hashAlgorithm, bool hasRSAdata)
		{
			this.privKey = privKey;
			if (!PdfPKCS7.allowedDigests.TryGetValue(hashAlgorithm.ToUpper(CultureInfo.InvariantCulture), out this.digestAlgorithm))
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("unknown.hash.algorithm.1", hashAlgorithm));
			}
			this.version = (this.signerversion = 1);
			this.certs = new List<X509Certificate>();
			this.crls = new List<X509Crl>();
			this.digestalgos = new Dictionary<string, object>();
			this.digestalgos[this.digestAlgorithm] = null;
			this.signCert = certChain[0];
			for (int i = 0; i < certChain.Length; i++)
			{
				this.certs.Add(certChain[i]);
			}
			if (privKey != null)
			{
				if (privKey is RsaKeyParameters)
				{
					this.digestEncryptionAlgorithm = "1.2.840.113549.1.1.1";
				}
				else
				{
					if (!(privKey is DsaKeyParameters))
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("unknown.key.algorithm.1", privKey.ToString()));
					}
					this.digestEncryptionAlgorithm = "1.2.840.10040.4.1";
				}
			}
			if (hasRSAdata)
			{
				this.RSAdata = new byte[0];
				this.messageDigest = this.GetHashClass();
			}
			if (privKey != null)
			{
				this.sig = SignerUtilities.GetSigner(this.GetDigestAlgorithm());
				this.sig.Init(true, privKey);
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001FE4E File Offset: 0x0001EE4E
		public void Update(byte[] buf, int off, int len)
		{
			if (this.RSAdata != null || this.digestAttr != null)
			{
				this.messageDigest.BlockUpdate(buf, off, len);
				return;
			}
			this.sig.BlockUpdate(buf, off, len);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001FE80 File Offset: 0x0001EE80
		public bool Verify()
		{
			if (this.verified)
			{
				return this.verifyResult;
			}
			if (this.sigAttr != null)
			{
				byte[] array = new byte[this.messageDigest.GetDigestSize()];
				this.sig.BlockUpdate(this.sigAttr, 0, this.sigAttr.Length);
				if (this.RSAdata != null)
				{
					this.messageDigest.DoFinal(array, 0);
					this.messageDigest.BlockUpdate(array, 0, array.Length);
				}
				this.messageDigest.DoFinal(array, 0);
				this.verifyResult = (Arrays.AreEqual(array, this.digestAttr) && this.sig.VerifySignature(this.digest));
			}
			else
			{
				if (this.RSAdata != null)
				{
					byte[] array2 = new byte[this.messageDigest.GetDigestSize()];
					this.messageDigest.DoFinal(array2, 0);
					this.sig.BlockUpdate(array2, 0, array2.Length);
				}
				this.verifyResult = this.sig.VerifySignature(this.digest);
			}
			this.verified = true;
			return this.verifyResult;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001FF8C File Offset: 0x0001EF8C
		public bool VerifyTimestampImprint()
		{
			if (this.timeStampToken == null)
			{
				return false;
			}
			MessageImprint messageImprint = this.timeStampToken.TimeStampInfo.TstInfo.MessageImprint;
			byte[] a = PdfEncryption.DigestComputeHash("SHA1", this.digest);
			byte[] hashedMessage = messageImprint.GetHashedMessage();
			return Arrays.AreEqual(a, hashedMessage);
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001FFDC File Offset: 0x0001EFDC
		public X509Certificate[] Certificates
		{
			get
			{
				X509Certificate[] array = new X509Certificate[this.certs.Count];
				this.certs.CopyTo(array);
				return array;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00020008 File Offset: 0x0001F008
		public X509Certificate[] SignCertificateChain
		{
			get
			{
				X509Certificate[] array = new X509Certificate[this.signCerts.Count];
				this.signCerts.CopyTo(array);
				return array;
			}
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00020034 File Offset: 0x0001F034
		private void CalcSignCertificateChain()
		{
			List<X509Certificate> list = new List<X509Certificate>();
			list.Add(this.signCert);
			List<X509Certificate> list2 = new List<X509Certificate>(this.certs);
			for (int i = 0; i < list2.Count; i++)
			{
				if (this.signCert.Equals(list2[i]))
				{
					list2.RemoveAt(i);
					i--;
				}
			}
			bool flag = true;
			while (flag)
			{
				X509Certificate x509Certificate = list[list.Count - 1];
				flag = false;
				for (int j = 0; j < list2.Count; j++)
				{
					try
					{
						x509Certificate.Verify(list2[j].GetPublicKey());
						flag = true;
						list.Add(list2[j]);
						list2.RemoveAt(j);
						break;
					}
					catch
					{
					}
				}
			}
			this.signCerts = list;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00020108 File Offset: 0x0001F108
		public List<X509Crl> CRLs
		{
			get
			{
				return this.crls;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00020110 File Offset: 0x0001F110
		public X509Certificate SigningCertificate
		{
			get
			{
				return this.signCert;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00020118 File Offset: 0x0001F118
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00020120 File Offset: 0x0001F120
		public int SigningInfoVersion
		{
			get
			{
				return this.signerversion;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00020128 File Offset: 0x0001F128
		public string GetDigestAlgorithm()
		{
			string algorithm = PdfPKCS7.GetAlgorithm(this.digestEncryptionAlgorithm);
			if (algorithm == null)
			{
				algorithm = this.digestEncryptionAlgorithm;
			}
			return this.GetHashAlgorithm() + "with" + algorithm;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0002015C File Offset: 0x0001F15C
		public string GetHashAlgorithm()
		{
			return PdfPKCS7.GetDigest(this.digestAlgorithm);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00020169 File Offset: 0x0001F169
		internal IDigest GetHashClass()
		{
			return DigestUtilities.GetDigest(this.GetHashAlgorithm());
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00020178 File Offset: 0x0001F178
		public static string VerifyCertificate(X509Certificate cert, X509Crl[] crls, DateTime calendar)
		{
			try
			{
				if (!cert.IsValid(calendar))
				{
					return "The certificate has expired or is not yet valid";
				}
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}
			return null;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000201B8 File Offset: 0x0001F1B8
		public static object[] VerifyCertificates(X509Certificate[] certs, List<X509Certificate> keystore, X509Crl[] crls, DateTime calendar)
		{
			for (int i = 0; i < certs.Length; i++)
			{
				X509Certificate x509Certificate = certs[i];
				string text = PdfPKCS7.VerifyCertificate(x509Certificate, crls, calendar);
				if (text != null)
				{
					return new object[]
					{
						x509Certificate,
						text
					};
				}
				foreach (X509Certificate x509Certificate2 in keystore)
				{
					try
					{
						if (PdfPKCS7.VerifyCertificate(x509Certificate2, crls, calendar) == null)
						{
							try
							{
								x509Certificate.Verify(x509Certificate2.GetPublicKey());
								return null;
							}
							catch
							{
							}
						}
					}
					catch
					{
					}
				}
				int j;
				for (j = 0; j < certs.Length; j++)
				{
					if (j != i)
					{
						X509Certificate x509Certificate3 = certs[j];
						try
						{
							x509Certificate.Verify(x509Certificate3.GetPublicKey());
							break;
						}
						catch
						{
						}
					}
				}
				if (j == certs.Length)
				{
					return new object[]
					{
						x509Certificate,
						"Cannot be verified against the KeyStore or the certificate chain"
					};
				}
			}
			return new object[]
			{
				null,
				"Invalid state. Possible circular certificate chain"
			};
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000202E8 File Offset: 0x0001F2E8
		public static bool VerifyOcspCertificates(BasicOcspResp ocsp, List<X509Certificate> keystore)
		{
			try
			{
				foreach (X509Certificate x509Certificate in keystore)
				{
					try
					{
						if (ocsp.Verify(x509Certificate.GetPublicKey()))
						{
							return true;
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00020368 File Offset: 0x0001F368
		public static bool VerifyTimestampCertificates(TimeStampToken ts, List<X509Certificate> keystore)
		{
			try
			{
				foreach (X509Certificate cert in keystore)
				{
					try
					{
						ts.Validate(cert);
						return true;
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000203E0 File Offset: 0x0001F3E0
		public static string GetOCSPURL(X509Certificate certificate)
		{
			try
			{
				Asn1Object extensionValue = PdfPKCS7.GetExtensionValue(certificate, X509Extensions.AuthorityInfoAccess.Id);
				if (extensionValue == null)
				{
					return null;
				}
				Asn1Sequence asn1Sequence = (Asn1Sequence)extensionValue;
				int i = 0;
				while (i < asn1Sequence.Count)
				{
					Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Sequence[i];
					if (asn1Sequence2.Count == 2 && asn1Sequence2[0] is DerObjectIdentifier && ((DerObjectIdentifier)asn1Sequence2[0]).Id.Equals("1.3.6.1.5.5.7.48.1"))
					{
						string stringFromGeneralName = PdfPKCS7.GetStringFromGeneralName((Asn1Object)asn1Sequence2[1]);
						if (stringFromGeneralName == null)
						{
							return "";
						}
						return stringFromGeneralName;
					}
					else
					{
						i++;
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x000204A4 File Offset: 0x0001F4A4
		public bool IsRevocationValid()
		{
			if (this.basicResp == null)
			{
				return false;
			}
			if (this.signCerts.Count < 2)
			{
				return false;
			}
			try
			{
				X509Certificate[] signCertificateChain = this.SignCertificateChain;
				SingleResp singleResp = this.basicResp.Responses[0];
				CertificateID certID = singleResp.GetCertID();
				X509Certificate signingCertificate = this.SigningCertificate;
				X509Certificate issuerCert = signCertificateChain[1];
				CertificateID certificateID = new CertificateID("1.3.14.3.2.26", issuerCert, signingCertificate.SerialNumber);
				return certificateID.Equals(certID);
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x0002052C File Offset: 0x0001F52C
		private static Asn1Object GetExtensionValue(X509Certificate cert, string oid)
		{
			byte[] derEncoded = cert.GetExtensionValue(new DerObjectIdentifier(oid)).GetDerEncoded();
			if (derEncoded == null)
			{
				return null;
			}
			Asn1InputStream asn1InputStream = new Asn1InputStream(new MemoryStream(derEncoded));
			Asn1OctetString asn1OctetString = (Asn1OctetString)asn1InputStream.ReadObject();
			asn1InputStream = new Asn1InputStream(new MemoryStream(asn1OctetString.GetOctets()));
			return asn1InputStream.ReadObject();
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00020580 File Offset: 0x0001F580
		private static string GetStringFromGeneralName(Asn1Object names)
		{
			DerTaggedObject obj = (DerTaggedObject)names;
			return Encoding.GetEncoding(1252).GetString(Asn1OctetString.GetInstance(obj, false).GetOctets());
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000205B0 File Offset: 0x0001F5B0
		private static Asn1Object GetIssuer(byte[] enc)
		{
			Asn1InputStream asn1InputStream = new Asn1InputStream(new MemoryStream(enc));
			Asn1Sequence asn1Sequence = (Asn1Sequence)asn1InputStream.ReadObject();
			return (Asn1Object)asn1Sequence[(asn1Sequence[0] is DerTaggedObject) ? 3 : 2];
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000205F4 File Offset: 0x0001F5F4
		private static Asn1Object GetSubject(byte[] enc)
		{
			Asn1InputStream asn1InputStream = new Asn1InputStream(new MemoryStream(enc));
			Asn1Sequence asn1Sequence = (Asn1Sequence)asn1InputStream.ReadObject();
			return (Asn1Object)asn1Sequence[(asn1Sequence[0] is DerTaggedObject) ? 5 : 4];
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00020636 File Offset: 0x0001F636
		public static PdfPKCS7.X509Name GetIssuerFields(X509Certificate cert)
		{
			return new PdfPKCS7.X509Name((Asn1Sequence)PdfPKCS7.GetIssuer(cert.GetTbsCertificate()));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0002064D File Offset: 0x0001F64D
		public static PdfPKCS7.X509Name GetSubjectFields(X509Certificate cert)
		{
			return new PdfPKCS7.X509Name((Asn1Sequence)PdfPKCS7.GetSubject(cert.GetTbsCertificate()));
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00020664 File Offset: 0x0001F664
		public byte[] GetEncodedPKCS1()
		{
			if (this.externalDigest != null)
			{
				this.digest = this.externalDigest;
			}
			else
			{
				this.digest = this.sig.GenerateSignature();
			}
			MemoryStream memoryStream = new MemoryStream();
			Asn1OutputStream asn1OutputStream = new Asn1OutputStream(memoryStream);
			asn1OutputStream.WriteObject(new DerOctetString(this.digest));
			asn1OutputStream.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000206C4 File Offset: 0x0001F6C4
		public void SetExternalDigest(byte[] digest, byte[] RSAdata, string digestEncryptionAlgorithm)
		{
			this.externalDigest = digest;
			this.externalRSAdata = RSAdata;
			if (digestEncryptionAlgorithm == null)
			{
				return;
			}
			if (digestEncryptionAlgorithm.Equals("RSA"))
			{
				this.digestEncryptionAlgorithm = "1.2.840.113549.1.1.1";
				return;
			}
			if (digestEncryptionAlgorithm.Equals("DSA"))
			{
				this.digestEncryptionAlgorithm = "1.2.840.10040.4.1";
				return;
			}
			throw new ArgumentException(MessageLocalization.GetComposedMessage("unknown.key.algorithm.1", digestEncryptionAlgorithm));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00020725 File Offset: 0x0001F725
		public byte[] GetEncodedPKCS7()
		{
			return this.GetEncodedPKCS7(null, DateTime.Now, null, null);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00020735 File Offset: 0x0001F735
		public byte[] GetEncodedPKCS7(byte[] secondDigest, DateTime signingTime)
		{
			return this.GetEncodedPKCS7(secondDigest, signingTime, null, null);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00020744 File Offset: 0x0001F744
		public byte[] GetEncodedPKCS7(byte[] secondDigest, DateTime signingTime, ITSAClient tsaClient, byte[] ocsp)
		{
			if (this.externalDigest != null)
			{
				this.digest = this.externalDigest;
				if (this.RSAdata != null)
				{
					this.RSAdata = this.externalRSAdata;
				}
			}
			else if (this.externalRSAdata != null && this.RSAdata != null)
			{
				this.RSAdata = this.externalRSAdata;
				this.sig.BlockUpdate(this.RSAdata, 0, this.RSAdata.Length);
				this.digest = this.sig.GenerateSignature();
			}
			else
			{
				if (this.RSAdata != null)
				{
					this.RSAdata = new byte[this.messageDigest.GetDigestSize()];
					this.messageDigest.DoFinal(this.RSAdata, 0);
					this.sig.BlockUpdate(this.RSAdata, 0, this.RSAdata.Length);
				}
				this.digest = this.sig.GenerateSignature();
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (string identifier in this.digestalgos.Keys)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSequence(new Asn1EncodableVector(new Asn1Encodable[0])
					{
						new Asn1Encodable[]
						{
							new DerObjectIdentifier(identifier)
						},
						new Asn1Encodable[]
						{
							DerNull.Instance
						}
					})
				});
			}
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				new DerObjectIdentifier("1.2.840.113549.1.7.1")
			});
			if (this.RSAdata != null)
			{
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(0, new DerOctetString(this.RSAdata))
				});
			}
			DerSequence derSequence = new DerSequence(asn1EncodableVector2);
			asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (X509Certificate x509Certificate in this.certs)
			{
				Asn1InputStream asn1InputStream = new Asn1InputStream(new MemoryStream(x509Certificate.GetEncoded()));
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					asn1InputStream.ReadObject()
				});
			}
			DerSet obj = new DerSet(asn1EncodableVector2);
			Asn1EncodableVector asn1EncodableVector3 = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector3.Add(new Asn1Encodable[]
			{
				new DerInteger(this.signerversion)
			});
			asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				PdfPKCS7.GetIssuer(this.signCert.GetTbsCertificate())
			});
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				new DerInteger(this.signCert.SerialNumber)
			});
			asn1EncodableVector3.Add(new Asn1Encodable[]
			{
				new DerSequence(asn1EncodableVector2)
			});
			asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				new DerObjectIdentifier(this.digestAlgorithm)
			});
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				DerNull.Instance
			});
			asn1EncodableVector3.Add(new Asn1Encodable[]
			{
				new DerSequence(asn1EncodableVector2)
			});
			if (secondDigest != null)
			{
				asn1EncodableVector3.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.GetAuthenticatedAttributeSet(secondDigest, signingTime, ocsp))
				});
			}
			asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				new DerObjectIdentifier(this.digestEncryptionAlgorithm)
			});
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				DerNull.Instance
			});
			asn1EncodableVector3.Add(new Asn1Encodable[]
			{
				new DerSequence(asn1EncodableVector2)
			});
			asn1EncodableVector3.Add(new Asn1Encodable[]
			{
				new DerOctetString(this.digest)
			});
			if (tsaClient != null)
			{
				byte[] imprint = PdfEncryption.DigestComputeHash("SHA1", this.digest);
				byte[] array = tsaClient.GetTimeStampToken(this, imprint);
				if (array != null)
				{
					Asn1EncodableVector asn1EncodableVector4 = this.BuildUnauthenticatedAttributes(array);
					if (asn1EncodableVector4 != null)
					{
						asn1EncodableVector3.Add(new Asn1Encodable[]
						{
							new DerTaggedObject(false, 1, new DerSet(asn1EncodableVector4))
						});
					}
				}
			}
			Asn1EncodableVector asn1EncodableVector5 = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector5.Add(new Asn1Encodable[]
			{
				new DerInteger(this.version)
			});
			asn1EncodableVector5.Add(new Asn1Encodable[]
			{
				new DerSet(asn1EncodableVector)
			});
			asn1EncodableVector5.Add(new Asn1Encodable[]
			{
				derSequence
			});
			asn1EncodableVector5.Add(new Asn1Encodable[]
			{
				new DerTaggedObject(false, 0, obj)
			});
			asn1EncodableVector5.Add(new Asn1Encodable[]
			{
				new DerSet(new DerSequence(asn1EncodableVector3))
			});
			Asn1EncodableVector asn1EncodableVector6 = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector6.Add(new Asn1Encodable[]
			{
				new DerObjectIdentifier("1.2.840.113549.1.7.2")
			});
			asn1EncodableVector6.Add(new Asn1Encodable[]
			{
				new DerTaggedObject(0, new DerSequence(asn1EncodableVector5))
			});
			MemoryStream memoryStream = new MemoryStream();
			Asn1OutputStream asn1OutputStream = new Asn1OutputStream(memoryStream);
			asn1OutputStream.WriteObject(new DerSequence(asn1EncodableVector6));
			asn1OutputStream.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00020CC0 File Offset: 0x0001FCC0
		private Asn1EncodableVector BuildUnauthenticatedAttributes(byte[] timeStampToken)
		{
			if (timeStampToken == null)
			{
				return null;
			}
			string identifier = "1.2.840.113549.1.9.16.2.14";
			Asn1InputStream asn1InputStream = new Asn1InputStream(new MemoryStream(timeStampToken));
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				new DerObjectIdentifier(identifier)
			});
			Asn1Sequence obj = (Asn1Sequence)asn1InputStream.ReadObject();
			asn1EncodableVector2.Add(new Asn1Encodable[]
			{
				new DerSet(obj)
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(asn1EncodableVector2)
			});
			return asn1EncodableVector;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00020D59 File Offset: 0x0001FD59
		public byte[] GetAuthenticatedAttributeBytes(byte[] secondDigest, DateTime signingTime, byte[] ocsp)
		{
			return this.GetAuthenticatedAttributeSet(secondDigest, signingTime, ocsp).GetEncoded("DER");
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00020D70 File Offset: 0x0001FD70
		private DerSet GetAuthenticatedAttributeSet(byte[] secondDigest, DateTime signingTime, byte[] ocsp)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(new Asn1EncodableVector(new Asn1Encodable[0])
				{
					new Asn1Encodable[]
					{
						new DerObjectIdentifier("1.2.840.113549.1.9.3")
					},
					new Asn1Encodable[]
					{
						new DerSet(new DerObjectIdentifier("1.2.840.113549.1.7.1"))
					}
				})
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(new Asn1EncodableVector(new Asn1Encodable[0])
				{
					new Asn1Encodable[]
					{
						new DerObjectIdentifier("1.2.840.113549.1.9.5")
					},
					new Asn1Encodable[]
					{
						new DerSet(new DerUtcTime(signingTime))
					}
				})
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerSequence(new Asn1EncodableVector(new Asn1Encodable[0])
				{
					new Asn1Encodable[]
					{
						new DerObjectIdentifier("1.2.840.113549.1.9.4")
					},
					new Asn1Encodable[]
					{
						new DerSet(new DerOctetString(secondDigest))
					}
				})
			});
			if (ocsp != null)
			{
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					new DerObjectIdentifier("1.2.840.113583.1.1.8")
				});
				DerOctetString derOctetString = new DerOctetString(ocsp);
				Asn1EncodableVector asn1EncodableVector3 = new Asn1EncodableVector(new Asn1Encodable[0]);
				Asn1EncodableVector asn1EncodableVector4 = new Asn1EncodableVector(new Asn1Encodable[0]);
				asn1EncodableVector4.Add(new Asn1Encodable[]
				{
					OcspObjectIdentifiers.PkixOcspBasic
				});
				asn1EncodableVector4.Add(new Asn1Encodable[]
				{
					derOctetString
				});
				DerEnumerated derEnumerated = new DerEnumerated(0);
				asn1EncodableVector3.Add(new Asn1Encodable[]
				{
					new DerSequence(new Asn1EncodableVector(new Asn1Encodable[0])
					{
						new Asn1Encodable[]
						{
							derEnumerated
						},
						new Asn1Encodable[]
						{
							new DerTaggedObject(true, 0, new DerSequence(asn1EncodableVector4))
						}
					})
				});
				asn1EncodableVector2.Add(new Asn1Encodable[]
				{
					new DerSet(new DerSequence(new DerTaggedObject(true, 1, new DerSequence(asn1EncodableVector3))))
				});
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerSequence(asn1EncodableVector2)
				});
			}
			return new DerSet(asn1EncodableVector);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00020FEB File Offset: 0x0001FFEB
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x00020FF3 File Offset: 0x0001FFF3
		public string Reason
		{
			get
			{
				return this.reason;
			}
			set
			{
				this.reason = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00020FFC File Offset: 0x0001FFFC
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x00021004 File Offset: 0x00020004
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0002100D File Offset: 0x0002000D
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x00021015 File Offset: 0x00020015
		public DateTime SignDate
		{
			get
			{
				return this.signDate;
			}
			set
			{
				this.signDate = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0002101E File Offset: 0x0002001E
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00021026 File Offset: 0x00020026
		public string SignName
		{
			get
			{
				return this.signName;
			}
			set
			{
				this.signName = value;
			}
		}

		// Token: 0x040002E6 RID: 742
		private const string ID_PKCS7_DATA = "1.2.840.113549.1.7.1";

		// Token: 0x040002E7 RID: 743
		private const string ID_PKCS7_SIGNED_DATA = "1.2.840.113549.1.7.2";

		// Token: 0x040002E8 RID: 744
		private const string ID_RSA = "1.2.840.113549.1.1.1";

		// Token: 0x040002E9 RID: 745
		private const string ID_DSA = "1.2.840.10040.4.1";

		// Token: 0x040002EA RID: 746
		private const string ID_CONTENT_TYPE = "1.2.840.113549.1.9.3";

		// Token: 0x040002EB RID: 747
		private const string ID_MESSAGE_DIGEST = "1.2.840.113549.1.9.4";

		// Token: 0x040002EC RID: 748
		private const string ID_SIGNING_TIME = "1.2.840.113549.1.9.5";

		// Token: 0x040002ED RID: 749
		private const string ID_ADBE_REVOCATION = "1.2.840.113583.1.1.8";

		// Token: 0x040002EE RID: 750
		private byte[] sigAttr;

		// Token: 0x040002EF RID: 751
		private byte[] digestAttr;

		// Token: 0x040002F0 RID: 752
		private int version;

		// Token: 0x040002F1 RID: 753
		private int signerversion;

		// Token: 0x040002F2 RID: 754
		private Dictionary<string, object> digestalgos;

		// Token: 0x040002F3 RID: 755
		private List<X509Certificate> certs;

		// Token: 0x040002F4 RID: 756
		private List<X509Crl> crls;

		// Token: 0x040002F5 RID: 757
		private List<X509Certificate> signCerts;

		// Token: 0x040002F6 RID: 758
		private X509Certificate signCert;

		// Token: 0x040002F7 RID: 759
		private byte[] digest;

		// Token: 0x040002F8 RID: 760
		private IDigest messageDigest;

		// Token: 0x040002F9 RID: 761
		private string digestAlgorithm;

		// Token: 0x040002FA RID: 762
		private string digestEncryptionAlgorithm;

		// Token: 0x040002FB RID: 763
		private ISigner sig;

		// Token: 0x040002FC RID: 764
		private ICipherParameters privKey;

		// Token: 0x040002FD RID: 765
		private byte[] RSAdata;

		// Token: 0x040002FE RID: 766
		private bool verified;

		// Token: 0x040002FF RID: 767
		private bool verifyResult;

		// Token: 0x04000300 RID: 768
		private byte[] externalDigest;

		// Token: 0x04000301 RID: 769
		private byte[] externalRSAdata;

		// Token: 0x04000302 RID: 770
		private string reason;

		// Token: 0x04000303 RID: 771
		private string location;

		// Token: 0x04000304 RID: 772
		private DateTime signDate;

		// Token: 0x04000305 RID: 773
		private string signName;

		// Token: 0x04000306 RID: 774
		private TimeStampToken timeStampToken;

		// Token: 0x04000307 RID: 775
		private static readonly Dictionary<string, string> digestNames = new Dictionary<string, string>();

		// Token: 0x04000308 RID: 776
		private static readonly Dictionary<string, string> algorithmNames = new Dictionary<string, string>();

		// Token: 0x04000309 RID: 777
		private static readonly Dictionary<string, string> allowedDigests = new Dictionary<string, string>();

		// Token: 0x0400030A RID: 778
		private BasicOcspResp basicResp;

		// Token: 0x020000C2 RID: 194
		public class X509Name
		{
			// Token: 0x06000663 RID: 1635 RVA: 0x00021030 File Offset: 0x00020030
			static X509Name()
			{
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.C] = "C";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.O] = "O";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.T] = "T";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.OU] = "OU";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.CN] = "CN";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.L] = "L";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.ST] = "ST";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.SN] = "SN";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.EmailAddress] = "E";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.DC] = "DC";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.UID] = "UID";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.SURNAME] = "SURNAME";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.GIVENNAME] = "GIVENNAME";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.INITIALS] = "INITIALS";
				PdfPKCS7.X509Name.DefaultSymbols[PdfPKCS7.X509Name.GENERATION] = "GENERATION";
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00021270 File Offset: 0x00020270
			public X509Name(Asn1Sequence seq)
			{
				foreach (object obj in seq)
				{
					Asn1Set asn1Set = (Asn1Set)obj;
					for (int i = 0; i < asn1Set.Count; i++)
					{
						Asn1Sequence asn1Sequence = (Asn1Sequence)asn1Set[i];
						string key;
						if (asn1Sequence[0] is DerObjectIdentifier && PdfPKCS7.X509Name.DefaultSymbols.TryGetValue((DerObjectIdentifier)asn1Sequence[0], out key))
						{
							List<string> list;
							if (!this.values.TryGetValue(key, out list))
							{
								list = new List<string>();
								this.values[key] = list;
							}
							list.Add(((DerStringBase)asn1Sequence[1]).GetString());
						}
					}
				}
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x0002133C File Offset: 0x0002033C
			public X509Name(string dirName)
			{
				PdfPKCS7.X509NameTokenizer x509NameTokenizer = new PdfPKCS7.X509NameTokenizer(dirName);
				while (x509NameTokenizer.HasMoreTokens())
				{
					string text = x509NameTokenizer.NextToken();
					int num = text.IndexOf('=');
					if (num == -1)
					{
						throw new ArgumentException(MessageLocalization.GetComposedMessage("badly.formated.directory.string"));
					}
					string key = text.Substring(0, num).ToUpper(CultureInfo.InvariantCulture);
					string item = text.Substring(num + 1);
					List<string> list;
					if (!this.values.TryGetValue(key, out list))
					{
						list = new List<string>();
						this.values[key] = list;
					}
					list.Add(item);
				}
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x000213DC File Offset: 0x000203DC
			public string GetField(string name)
			{
				List<string> list;
				if (!this.values.TryGetValue(name, out list))
				{
					return null;
				}
				if (list.Count != 0)
				{
					return list[0];
				}
				return null;
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x0002140C File Offset: 0x0002040C
			public List<string> GetFieldArray(string name)
			{
				List<string> result;
				if (this.values.TryGetValue(name, out result))
				{
					return result;
				}
				return null;
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x0002142C File Offset: 0x0002042C
			public Dictionary<string, List<string>> GetFields()
			{
				return this.values;
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x00021434 File Offset: 0x00020434
			public override string ToString()
			{
				return this.values.ToString();
			}

			// Token: 0x0400030B RID: 779
			public static DerObjectIdentifier C = new DerObjectIdentifier("2.5.4.6");

			// Token: 0x0400030C RID: 780
			public static DerObjectIdentifier O = new DerObjectIdentifier("2.5.4.10");

			// Token: 0x0400030D RID: 781
			public static DerObjectIdentifier OU = new DerObjectIdentifier("2.5.4.11");

			// Token: 0x0400030E RID: 782
			public static DerObjectIdentifier T = new DerObjectIdentifier("2.5.4.12");

			// Token: 0x0400030F RID: 783
			public static DerObjectIdentifier CN = new DerObjectIdentifier("2.5.4.3");

			// Token: 0x04000310 RID: 784
			public static DerObjectIdentifier SN = new DerObjectIdentifier("2.5.4.5");

			// Token: 0x04000311 RID: 785
			public static DerObjectIdentifier L = new DerObjectIdentifier("2.5.4.7");

			// Token: 0x04000312 RID: 786
			public static DerObjectIdentifier ST = new DerObjectIdentifier("2.5.4.8");

			// Token: 0x04000313 RID: 787
			public static DerObjectIdentifier SURNAME = new DerObjectIdentifier("2.5.4.4");

			// Token: 0x04000314 RID: 788
			public static DerObjectIdentifier GIVENNAME = new DerObjectIdentifier("2.5.4.42");

			// Token: 0x04000315 RID: 789
			public static DerObjectIdentifier INITIALS = new DerObjectIdentifier("2.5.4.43");

			// Token: 0x04000316 RID: 790
			public static DerObjectIdentifier GENERATION = new DerObjectIdentifier("2.5.4.44");

			// Token: 0x04000317 RID: 791
			public static DerObjectIdentifier UNIQUE_IDENTIFIER = new DerObjectIdentifier("2.5.4.45");

			// Token: 0x04000318 RID: 792
			public static DerObjectIdentifier EmailAddress = new DerObjectIdentifier("1.2.840.113549.1.9.1");

			// Token: 0x04000319 RID: 793
			public static DerObjectIdentifier E = PdfPKCS7.X509Name.EmailAddress;

			// Token: 0x0400031A RID: 794
			public static DerObjectIdentifier DC = new DerObjectIdentifier("0.9.2342.19200300.100.1.25");

			// Token: 0x0400031B RID: 795
			public static DerObjectIdentifier UID = new DerObjectIdentifier("0.9.2342.19200300.100.1.1");

			// Token: 0x0400031C RID: 796
			public static Dictionary<DerObjectIdentifier, string> DefaultSymbols = new Dictionary<DerObjectIdentifier, string>();

			// Token: 0x0400031D RID: 797
			public Dictionary<string, List<string>> values = new Dictionary<string, List<string>>();
		}

		// Token: 0x020000C3 RID: 195
		public class X509NameTokenizer
		{
			// Token: 0x0600066A RID: 1642 RVA: 0x00021441 File Offset: 0x00020441
			public X509NameTokenizer(string oid)
			{
				this.oid = oid;
				this.index = -1;
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x00021462 File Offset: 0x00020462
			public bool HasMoreTokens()
			{
				return this.index != this.oid.Length;
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x0002147C File Offset: 0x0002047C
			public string NextToken()
			{
				if (this.index == this.oid.Length)
				{
					return null;
				}
				int num = this.index + 1;
				bool flag = false;
				bool flag2 = false;
				this.buf.Length = 0;
				while (num != this.oid.Length)
				{
					char c = this.oid[num];
					if (c == '"')
					{
						if (!flag2)
						{
							flag = !flag;
						}
						else
						{
							this.buf.Append(c);
						}
						flag2 = false;
					}
					else if (flag2 || flag)
					{
						this.buf.Append(c);
						flag2 = false;
					}
					else if (c == '\\')
					{
						flag2 = true;
					}
					else
					{
						if (c == ',')
						{
							break;
						}
						this.buf.Append(c);
					}
					num++;
				}
				this.index = num;
				return this.buf.ToString().Trim();
			}

			// Token: 0x0400031E RID: 798
			private string oid;

			// Token: 0x0400031F RID: 799
			private int index;

			// Token: 0x04000320 RID: 800
			private StringBuilder buf = new StringBuilder();
		}
	}
}
