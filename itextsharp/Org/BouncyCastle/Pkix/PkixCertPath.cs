using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x020001DE RID: 478
	public class PkixCertPath
	{
		// Token: 0x060012D2 RID: 4818 RVA: 0x0006B81C File Offset: 0x0006A81C
		private static IList SortCerts(IList certs)
		{
			if (certs.Count < 2)
			{
				return certs;
			}
			X509Name issuerDN = ((X509Certificate)certs[0]).IssuerDN;
			bool flag = true;
			for (int num = 1; num != certs.Count; num++)
			{
				X509Certificate x509Certificate = (X509Certificate)certs[num];
				if (!issuerDN.Equivalent(x509Certificate.SubjectDN, true))
				{
					flag = false;
					break;
				}
				issuerDN = ((X509Certificate)certs[num]).IssuerDN;
			}
			if (flag)
			{
				return certs;
			}
			IList list = new ArrayList(certs.Count);
			IList result = new ArrayList(certs);
			for (int i = 0; i < certs.Count; i++)
			{
				X509Certificate x509Certificate2 = (X509Certificate)certs[i];
				bool flag2 = false;
				X509Name subjectDN = x509Certificate2.SubjectDN;
				foreach (object obj in certs)
				{
					X509Certificate x509Certificate3 = (X509Certificate)obj;
					if (x509Certificate3.IssuerDN.Equivalent(subjectDN, true))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					list.Add(x509Certificate2);
					certs.RemoveAt(i);
				}
			}
			if (list.Count > 1)
			{
				return result;
			}
			for (int num2 = 0; num2 != list.Count; num2++)
			{
				issuerDN = ((X509Certificate)list[num2]).IssuerDN;
				for (int j = 0; j < certs.Count; j++)
				{
					X509Certificate x509Certificate4 = (X509Certificate)certs[j];
					if (issuerDN.Equivalent(x509Certificate4.SubjectDN, true))
					{
						list.Add(x509Certificate4);
						certs.RemoveAt(j);
						break;
					}
				}
			}
			if (certs.Count > 0)
			{
				return result;
			}
			return list;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0006B9E0 File Offset: 0x0006A9E0
		public PkixCertPath(ICollection certificates)
		{
			this.certificates = PkixCertPath.SortCerts(new ArrayList(certificates));
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0006BA04 File Offset: 0x0006AA04
		public PkixCertPath(Stream inStream) : this(inStream, "PkiPath")
		{
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0006BA14 File Offset: 0x0006AA14
		public PkixCertPath(Stream inStream, string encoding)
		{
			try
			{
				if (encoding.ToUpper().Equals("PkiPath".ToUpper()))
				{
					Asn1InputStream asn1InputStream = new Asn1InputStream(inStream);
					Asn1Object asn1Object = asn1InputStream.ReadObject();
					if (!(asn1Object is Asn1Sequence))
					{
						throw new CertificateException("input stream does not contain a ASN1 SEQUENCE while reading PkiPath encoded data to load CertPath");
					}
					IEnumerator enumerator = ((Asn1Sequence)asn1Object).GetEnumerator();
					this.certificates = new ArrayList();
					while (enumerator.MoveNext())
					{
						MemoryStream memoryStream = new MemoryStream();
						DerOutputStream derOutputStream = new DerOutputStream(memoryStream);
						derOutputStream.WriteObject((Asn1Encodable)enumerator.Current);
						derOutputStream.Close();
						Stream inStream2 = new MemoryStream(memoryStream.ToArray(), false);
						this.certificates.Insert(0, new X509CertificateParser().ReadCertificate(inStream2));
					}
				}
				else
				{
					if (!encoding.ToUpper().Equals("PKCS7") && !encoding.ToUpper().Equals("PEM"))
					{
						throw new CertificateException("unsupported encoding: " + encoding);
					}
					this.certificates = new ArrayList();
					X509CertificateParser x509CertificateParser = new X509CertificateParser();
					X509Certificate value;
					while ((value = x509CertificateParser.ReadCertificate(inStream)) != null)
					{
						this.certificates.Add(value);
					}
				}
			}
			catch (IOException ex)
			{
				throw new CertificateException("IOException throw while decoding CertPath:\n" + ex.ToString());
			}
			this.certificates = PkixCertPath.SortCerts(this.certificates);
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0006BB90 File Offset: 0x0006AB90
		public virtual IEnumerable Encodings
		{
			get
			{
				return new EnumerableProxy(PkixCertPath.certPathEncodings);
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0006BB9C File Offset: 0x0006AB9C
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			PkixCertPath pkixCertPath = obj as PkixCertPath;
			if (pkixCertPath == null)
			{
				return false;
			}
			IList list = this.Certificates;
			IList list2 = pkixCertPath.Certificates;
			if (list.Count != list2.Count)
			{
				return false;
			}
			IEnumerator enumerator = list.GetEnumerator();
			IEnumerator enumerator2 = list.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator2.MoveNext();
				if (!object.Equals(enumerator.Current, enumerator2.Current))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0006BC11 File Offset: 0x0006AC11
		public override int GetHashCode()
		{
			return this.Certificates.GetHashCode();
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0006BC20 File Offset: 0x0006AC20
		public virtual byte[] GetEncoded()
		{
			foreach (object obj in this.Encodings)
			{
				if (obj is string)
				{
					return this.GetEncoded((string)obj);
				}
			}
			return null;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0006BC88 File Offset: 0x0006AC88
		public virtual byte[] GetEncoded(string encoding)
		{
			if (string.Compare(encoding, "PkiPath", true) == 0)
			{
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
				for (int i = this.certificates.Count - 1; i >= 0; i--)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						this.ToAsn1Object((X509Certificate)this.certificates[i])
					});
				}
				return this.ToDerEncoded(new DerSequence(asn1EncodableVector));
			}
			if (string.Compare(encoding, "PKCS7", true) == 0)
			{
				ContentInfo contentInfo = new ContentInfo(PkcsObjectIdentifiers.Data, null);
				Asn1EncodableVector asn1EncodableVector2 = new Asn1EncodableVector(new Asn1Encodable[0]);
				for (int num = 0; num != this.certificates.Count; num++)
				{
					asn1EncodableVector2.Add(new Asn1Encodable[]
					{
						this.ToAsn1Object((X509Certificate)this.certificates[num])
					});
				}
				SignedData content = new SignedData(new DerInteger(1), new DerSet(), contentInfo, new DerSet(asn1EncodableVector2), null, new DerSet());
				return this.ToDerEncoded(new ContentInfo(PkcsObjectIdentifiers.SignedData, content));
			}
			if (string.Compare(encoding, "PEM", true) == 0)
			{
				MemoryStream memoryStream = new MemoryStream();
				PemWriter pemWriter = new PemWriter(new StreamWriter(memoryStream));
				try
				{
					for (int num2 = 0; num2 != this.certificates.Count; num2++)
					{
						pemWriter.WriteObject(this.certificates[num2]);
					}
					pemWriter.Writer.Close();
				}
				catch (Exception)
				{
					throw new CertificateEncodingException("can't encode certificate for PEM encoded path");
				}
				return memoryStream.ToArray();
			}
			throw new CertificateEncodingException("unsupported encoding: " + encoding);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0006BE34 File Offset: 0x0006AE34
		public virtual IList Certificates
		{
			get
			{
				return new ArrayList(this.certificates);
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0006BE44 File Offset: 0x0006AE44
		private Asn1Object ToAsn1Object(X509Certificate cert)
		{
			Asn1Object result;
			try
			{
				result = Asn1Object.FromByteArray(cert.GetEncoded());
			}
			catch (Exception e)
			{
				throw new CertificateEncodingException("Exception while encoding certificate", e);
			}
			return result;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0006BE80 File Offset: 0x0006AE80
		private byte[] ToDerEncoded(Asn1Encodable obj)
		{
			byte[] encoded;
			try
			{
				encoded = obj.GetEncoded("DER");
			}
			catch (IOException e)
			{
				throw new CertificateEncodingException("Exception thrown", e);
			}
			return encoded;
		}

		// Token: 0x04000D4D RID: 3405
		internal static readonly IList certPathEncodings = new ArrayList
		{
			"PkiPath",
			"PEM",
			"PKCS7"
		};

		// Token: 0x04000D4E RID: 3406
		private IList certificates = new ArrayList();
	}
}
