using System;
using System.Collections;
using System.IO;
using System.Net;
using iTextSharp.text.error_messages;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Ocsp;
using Org.BouncyCastle.X509;

namespace iTextSharp.text.pdf
{
	// Token: 0x020002D2 RID: 722
	public class OcspClientBouncyCastle : IOcspClient
	{
		// Token: 0x06001AEC RID: 6892 RVA: 0x0009EDD4 File Offset: 0x0009DDD4
		public OcspClientBouncyCastle(X509Certificate checkCert, X509Certificate rootCert, string url)
		{
			this.checkCert = checkCert;
			this.rootCert = rootCert;
			this.url = url;
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x0009EDF4 File Offset: 0x0009DDF4
		private static OcspReq GenerateOCSPRequest(X509Certificate issuerCert, BigInteger serialNumber)
		{
			CertificateID certId = new CertificateID("1.3.14.3.2.26", issuerCert, serialNumber);
			OcspReqGenerator ocspReqGenerator = new OcspReqGenerator();
			ocspReqGenerator.AddRequest(certId);
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			arrayList.Add(OcspObjectIdentifiers.PkixOcspNonce);
			arrayList2.Add(new X509Extension(false, new DerOctetString(new DerOctetString(PdfEncryption.CreateDocumentId()).GetEncoded())));
			ocspReqGenerator.SetRequestExtensions(new X509Extensions(arrayList, arrayList2));
			return ocspReqGenerator.Generate();
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x0009EE68 File Offset: 0x0009DE68
		public byte[] GetEncoded()
		{
			OcspReq ocspReq = OcspClientBouncyCastle.GenerateOCSPRequest(this.rootCert, this.checkCert.SerialNumber);
			byte[] encoded = ocspReq.GetEncoded();
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.url);
			httpWebRequest.ContentLength = (long)encoded.Length;
			httpWebRequest.ContentType = "application/ocsp-request";
			httpWebRequest.Accept = "application/ocsp-response";
			httpWebRequest.Method = "POST";
			Stream requestStream = httpWebRequest.GetRequestStream();
			requestStream.Write(encoded, 0, encoded.Length);
			requestStream.Close();
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			if (httpWebResponse.StatusCode != HttpStatusCode.OK)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("invalid.http.response.1", (int)httpWebResponse.StatusCode));
			}
			Stream responseStream = httpWebResponse.GetResponseStream();
			OcspResp ocspResp = new OcspResp(responseStream);
			responseStream.Close();
			httpWebResponse.Close();
			if (ocspResp.Status != 0)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("invalid.status.1", ocspResp.Status));
			}
			BasicOcspResp basicOcspResp = (BasicOcspResp)ocspResp.GetResponseObject();
			if (basicOcspResp != null)
			{
				SingleResp[] responses = basicOcspResp.Responses;
				if (responses.Length == 1)
				{
					SingleResp singleResp = responses[0];
					object certStatus = singleResp.GetCertStatus();
					if (certStatus == CertificateStatus.Good)
					{
						return basicOcspResp.GetEncoded();
					}
					if (certStatus is RevokedStatus)
					{
						throw new IOException(MessageLocalization.GetComposedMessage("ocsp.status.is.revoked"));
					}
					throw new IOException(MessageLocalization.GetComposedMessage("ocsp.status.is.unknown"));
				}
			}
			return null;
		}

		// Token: 0x040011F2 RID: 4594
		private X509Certificate rootCert;

		// Token: 0x040011F3 RID: 4595
		private X509Certificate checkCert;

		// Token: 0x040011F4 RID: 4596
		private string url;
	}
}
