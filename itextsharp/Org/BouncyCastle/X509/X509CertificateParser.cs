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
	// Token: 0x02000465 RID: 1125
	public class X509CertificateParser
	{
		// Token: 0x0600263A RID: 9786 RVA: 0x000E79D8 File Offset: 0x000E69D8
		private X509Certificate ReadDerCertificate(Asn1InputStream dIn)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)dIn.ReadObject();
			if (asn1Sequence.Count > 1 && asn1Sequence[0] is DerObjectIdentifier && asn1Sequence[0].Equals(PkcsObjectIdentifiers.SignedData))
			{
				this.sData = SignedData.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)asn1Sequence[1], true)).Certificates;
				return this.GetCertificate();
			}
			return this.CreateX509Certificate(X509CertificateStructure.GetInstance(asn1Sequence));
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000E7A50 File Offset: 0x000E6A50
		private X509Certificate GetCertificate()
		{
			if (this.sData != null)
			{
				while (this.sDataObjectCount < this.sData.Count)
				{
					object obj = this.sData[this.sDataObjectCount++];
					if (obj is Asn1Sequence)
					{
						return this.CreateX509Certificate(X509CertificateStructure.GetInstance(obj));
					}
				}
			}
			return null;
		}

		// Token: 0x0600263C RID: 9788 RVA: 0x000E7AB0 File Offset: 0x000E6AB0
		private X509Certificate ReadPemCertificate(Stream inStream)
		{
			Asn1Sequence asn1Sequence = X509CertificateParser.PemCertParser.ReadPemObject(inStream);
			if (asn1Sequence != null)
			{
				return this.CreateX509Certificate(X509CertificateStructure.GetInstance(asn1Sequence));
			}
			return null;
		}

		// Token: 0x0600263D RID: 9789 RVA: 0x000E7ADA File Offset: 0x000E6ADA
		protected virtual X509Certificate CreateX509Certificate(X509CertificateStructure c)
		{
			return new X509Certificate(c);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000E7AE2 File Offset: 0x000E6AE2
		public X509Certificate ReadCertificate(byte[] input)
		{
			return this.ReadCertificate(new MemoryStream(input, false));
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000E7AF1 File Offset: 0x000E6AF1
		public ICollection ReadCertificates(byte[] input)
		{
			return this.ReadCertificates(new MemoryStream(input, false));
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000E7B00 File Offset: 0x000E6B00
		public X509Certificate ReadCertificate(Stream inStream)
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
			X509Certificate result;
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
			catch (Exception exception)
			{
				throw new CertificateException("Failed to read certificate", exception);
			}
			return result;
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x000E7C00 File Offset: 0x000E6C00
		public ICollection ReadCertificates(Stream inStream)
		{
			IList list = new ArrayList();
			X509Certificate value;
			while ((value = this.ReadCertificate(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x04001A9E RID: 6814
		private static readonly PemParser PemCertParser = new PemParser("CERTIFICATE");

		// Token: 0x04001A9F RID: 6815
		private Asn1Set sData;

		// Token: 0x04001AA0 RID: 6816
		private int sDataObjectCount;

		// Token: 0x04001AA1 RID: 6817
		private Stream currentStream;
	}
}
