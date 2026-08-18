using System;
using System.Collections;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security.Certificates;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Store;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x02000027 RID: 39
	internal class CmsUtilities
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000083A4 File Offset: 0x000073A4
		internal static int MaximumMemory
		{
			get
			{
				long num = 2147483647L;
				if (num > 2147483647L)
				{
					return int.MaxValue;
				}
				return (int)num;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000083C9 File Offset: 0x000073C9
		internal static ContentInfo ReadContentInfo(byte[] input)
		{
			return CmsUtilities.ReadContentInfo(new Asn1InputStream(input));
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000083D6 File Offset: 0x000073D6
		internal static ContentInfo ReadContentInfo(Stream input)
		{
			return CmsUtilities.ReadContentInfo(new Asn1InputStream(input, CmsUtilities.MaximumMemory));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000083E8 File Offset: 0x000073E8
		private static ContentInfo ReadContentInfo(Asn1InputStream aIn)
		{
			ContentInfo instance;
			try
			{
				instance = ContentInfo.GetInstance(aIn.ReadObject());
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading content.", e);
			}
			catch (InvalidCastException e2)
			{
				throw new CmsException("Malformed content.", e2);
			}
			catch (ArgumentException e3)
			{
				throw new CmsException("Malformed content.", e3);
			}
			return instance;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008454 File Offset: 0x00007454
		public static byte[] StreamToByteArray(Stream inStream)
		{
			return Streams.ReadAll(inStream);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000845C File Offset: 0x0000745C
		public static byte[] StreamToByteArray(Stream inStream, int limit)
		{
			return Streams.ReadAllLimited(inStream, limit);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008468 File Offset: 0x00007468
		public static IList GetCertificatesFromStore(IX509Store certStore)
		{
			IList result;
			try
			{
				IList list = new ArrayList();
				if (certStore != null)
				{
					foreach (object obj in certStore.GetMatches(null))
					{
						X509Certificate x509Certificate = (X509Certificate)obj;
						list.Add(X509CertificateStructure.GetInstance(Asn1Object.FromByteArray(x509Certificate.GetEncoded())));
					}
				}
				result = list;
			}
			catch (CertificateEncodingException e)
			{
				throw new CmsException("error encoding certs", e);
			}
			catch (Exception e2)
			{
				throw new CmsException("error processing certs", e2);
			}
			return result;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000851C File Offset: 0x0000751C
		public static IList GetCrlsFromStore(IX509Store crlStore)
		{
			IList result;
			try
			{
				IList list = new ArrayList();
				if (crlStore != null)
				{
					foreach (object obj in crlStore.GetMatches(null))
					{
						X509Crl x509Crl = (X509Crl)obj;
						list.Add(CertificateList.GetInstance(Asn1Object.FromByteArray(x509Crl.GetEncoded())));
					}
				}
				result = list;
			}
			catch (CrlException e)
			{
				throw new CmsException("error encoding crls", e);
			}
			catch (Exception e2)
			{
				throw new CmsException("error processing crls", e2);
			}
			return result;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000085D0 File Offset: 0x000075D0
		public static Asn1Set CreateBerSetFromList(IList berObjects)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in berObjects)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Encodable
				});
			}
			return new BerSet(asn1EncodableVector);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008644 File Offset: 0x00007644
		public static Asn1Set CreateDerSetFromList(IList derObjects)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[0]);
			foreach (object obj in derObjects)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Encodable
				});
			}
			return new DerSet(asn1EncodableVector);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000086B8 File Offset: 0x000076B8
		internal static Stream CreateBerOctetOutputStream(Stream s, int tagNo, bool isExplicit, int bufferSize)
		{
			BerOctetStringGenerator berOctetStringGenerator = new BerOctetStringGenerator(s, tagNo, isExplicit);
			return berOctetStringGenerator.GetOctetOutputStream(bufferSize);
		}
	}
}
