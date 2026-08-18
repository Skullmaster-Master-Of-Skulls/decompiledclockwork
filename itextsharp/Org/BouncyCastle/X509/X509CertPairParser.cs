using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities.IO;

namespace Org.BouncyCastle.X509
{
	// Token: 0x02000293 RID: 659
	public class X509CertPairParser
	{
		// Token: 0x060018E8 RID: 6376 RVA: 0x00092BDC File Offset: 0x00091BDC
		private X509CertificatePair ReadDerCrossCertificatePair(Stream inStream)
		{
			Asn1InputStream asn1InputStream = new Asn1InputStream(inStream);
			Asn1Sequence obj = (Asn1Sequence)asn1InputStream.ReadObject();
			CertificatePair instance = CertificatePair.GetInstance(obj);
			return new X509CertificatePair(instance);
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x00092C09 File Offset: 0x00091C09
		public X509CertificatePair ReadCertPair(byte[] input)
		{
			return this.ReadCertPair(new MemoryStream(input, false));
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00092C18 File Offset: 0x00091C18
		public ICollection ReadCertPairs(byte[] input)
		{
			return this.ReadCertPairs(new MemoryStream(input, false));
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00092C28 File Offset: 0x00091C28
		public X509CertificatePair ReadCertPair(Stream inStream)
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
			}
			else if (this.currentStream != inStream)
			{
				this.currentStream = inStream;
			}
			X509CertificatePair result;
			try
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
					result = this.ReadDerCrossCertificatePair(pushbackStream);
				}
			}
			catch (Exception ex)
			{
				throw new CertificateException(ex.ToString());
			}
			return result;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00092CC4 File Offset: 0x00091CC4
		public ICollection ReadCertPairs(Stream inStream)
		{
			IList list = new ArrayList();
			X509CertificatePair value;
			while ((value = this.ReadCertPair(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x040010CC RID: 4300
		private Stream currentStream;
	}
}
