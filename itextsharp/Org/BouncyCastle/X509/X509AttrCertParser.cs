using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.X509
{
	// Token: 0x0200053A RID: 1338
	public class X509AttrCertParser
	{
		// Token: 0x06002E11 RID: 11793 RVA: 0x0011CD5C File Offset: 0x0011BD5C
		private IX509AttributeCertificate ReadDerCertificate(Asn1InputStream dIn)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)dIn.ReadObject();
			if (asn1Sequence.Count > 1 && asn1Sequence[0] is DerObjectIdentifier && asn1Sequence[0].Equals(PkcsObjectIdentifiers.SignedData))
			{
				this.sData = SignedData.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)asn1Sequence[1], true)).Certificates;
				return this.GetCertificate();
			}
			return new X509V2AttributeCertificate(AttributeCertificate.GetInstance(asn1Sequence));
		}

		// Token: 0x06002E12 RID: 11794 RVA: 0x0011CDD4 File Offset: 0x0011BDD4
		private IX509AttributeCertificate GetCertificate()
		{
			if (this.sData != null)
			{
				while (this.sDataObjectCount < this.sData.Count)
				{
					object obj = this.sData[this.sDataObjectCount++];
					if (obj is Asn1TaggedObject && ((Asn1TaggedObject)obj).TagNo == 2)
					{
						return new X509V2AttributeCertificate(AttributeCertificate.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)obj, false)));
					}
				}
			}
			return null;
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x0011CE4C File Offset: 0x0011BE4C
		private IX509AttributeCertificate ReadPemCertificate(Stream inStream)
		{
			Asn1Sequence asn1Sequence = X509AttrCertParser.PemAttrCertParser.ReadPemObject(inStream);
			if (asn1Sequence != null)
			{
				return new X509V2AttributeCertificate(AttributeCertificate.GetInstance(asn1Sequence));
			}
			return null;
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x0011CE75 File Offset: 0x0011BE75
		public IX509AttributeCertificate ReadAttrCert(byte[] input)
		{
			return this.ReadAttrCert(new MemoryStream(input, false));
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x0011CE84 File Offset: 0x0011BE84
		public ICollection ReadAttrCerts(byte[] input)
		{
			return this.ReadAttrCerts(new MemoryStream(input, false));
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x0011CE94 File Offset: 0x0011BE94
		public IX509AttributeCertificate ReadAttrCert(Stream inStream)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (!inStream.CanRead)
			{
				throw new ArgumentException("inStream must be read-able", "inStream");
			}
			if (this.currentStream == null)
			{
				this.currentStream = inStream;
				this.sData = null;
				this.sDataObjectCount = 0;
			}
			else if (this.currentStream != inStream)
			{
				this.currentStream = inStream;
				this.sData = null;
				this.sDataObjectCount = 0;
			}
			IX509AttributeCertificate result;
			try
			{
				if (this.sData != null)
				{
					if (this.sDataObjectCount != this.sData.Count)
					{
						result = this.GetCertificate();
					}
					else
					{
						this.sData = null;
						this.sDataObjectCount = 0;
						result = null;
					}
				}
				else
				{
					PushbackStream pushbackStream = new PushbackStream(inStream);
					int num = pushbackStream.ReadByte();
					if (num < 0)
					{
						result = null;
					}
					else
					{
						pushbackStream.Unread(num);
						if (num != 48)
						{
							result = this.ReadPemCertificate(pushbackStream);
						}
						else
						{
							result = this.ReadDerCertificate(new Asn1InputStream(pushbackStream));
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new CertificateException(ex.ToString());
			}
			return result;
		}

		// Token: 0x06002E17 RID: 11799 RVA: 0x0011CF94 File Offset: 0x0011BF94
		public ICollection ReadAttrCerts(Stream inStream)
		{
			IList list = new ArrayList();
			IX509AttributeCertificate value;
			while ((value = this.ReadAttrCert(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x04001FEF RID: 8175
		private static readonly PemParser PemAttrCertParser = new PemParser("ATTRIBUTE CERTIFICATE");

		// Token: 0x04001FF0 RID: 8176
		private Asn1Set sData;

		// Token: 0x04001FF1 RID: 8177
		private int sDataObjectCount;

		// Token: 0x04001FF2 RID: 8178
		private Stream currentStream;
	}
}
