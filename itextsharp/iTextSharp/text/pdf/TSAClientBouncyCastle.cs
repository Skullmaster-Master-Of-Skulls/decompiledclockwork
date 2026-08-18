using System;
using System.IO;
using System.Net;
using System.Text;
using System.util;
using iTextSharp.text.error_messages;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Tsp;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000587 RID: 1415
	public class TSAClientBouncyCastle : ITSAClient
	{
		// Token: 0x0600301A RID: 12314 RVA: 0x00129590 File Offset: 0x00128590
		public TSAClientBouncyCastle(string url) : this(url, null, null, 4096)
		{
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x001295A0 File Offset: 0x001285A0
		public TSAClientBouncyCastle(string url, string username, string password) : this(url, username, password, 4096)
		{
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x001295B0 File Offset: 0x001285B0
		public TSAClientBouncyCastle(string url, string username, string password, int tokSzEstimate)
		{
			this.tsaURL = url;
			this.tsaUsername = username;
			this.tsaPassword = password;
			this.tokSzEstimate = tokSzEstimate;
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x001295D5 File Offset: 0x001285D5
		public int GetTokenSizeEstimate()
		{
			return this.tokSzEstimate;
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x001295DD File Offset: 0x001285DD
		public byte[] GetTimeStampToken(PdfPKCS7 caller, byte[] imprint)
		{
			return this.GetTimeStampToken(imprint);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x001295E8 File Offset: 0x001285E8
		protected internal byte[] GetTimeStampToken(byte[] imprint)
		{
			TimeStampRequestGenerator timeStampRequestGenerator = new TimeStampRequestGenerator();
			timeStampRequestGenerator.SetCertReq(true);
			BigInteger nonce = BigInteger.ValueOf(DateTime.Now.Ticks + (long)Environment.TickCount);
			TimeStampRequest timeStampRequest = timeStampRequestGenerator.Generate(X509ObjectIdentifiers.IdSha1.Id, imprint, nonce);
			byte[] encoded = timeStampRequest.GetEncoded();
			byte[] tsaresponse = this.GetTSAResponse(encoded);
			TimeStampResponse timeStampResponse = new TimeStampResponse(tsaresponse);
			timeStampResponse.Validate(timeStampRequest);
			PkiFailureInfo failInfo = timeStampResponse.GetFailInfo();
			int num = (failInfo == null) ? 0 : failInfo.IntValue;
			if (num != 0)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("invalid.tsa.1.response.code.2", this.tsaURL, num));
			}
			TimeStampToken timeStampToken = timeStampResponse.TimeStampToken;
			if (timeStampToken == null)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("tsa.1.failed.to.return.time.stamp.token.2", this.tsaURL, timeStampResponse.GetStatusString()));
			}
			TimeStampTokenInfo timeStampInfo = timeStampToken.TimeStampInfo;
			byte[] encoded2 = timeStampToken.GetEncoded();
			this.tokSzEstimate = encoded2.Length + 32;
			return encoded2;
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x001296D8 File Offset: 0x001286D8
		protected internal virtual byte[] GetTSAResponse(byte[] requestBytes)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.tsaURL);
			httpWebRequest.ContentLength = (long)requestBytes.Length;
			httpWebRequest.ContentType = "application/timestamp-query";
			httpWebRequest.Method = "POST";
			if (this.tsaUsername != null && !this.tsaUsername.Equals(""))
			{
				string text = this.tsaUsername + ":" + this.tsaPassword;
				text = Convert.ToBase64String(Encoding.Default.GetBytes(text));
				httpWebRequest.Headers["Authorization"] = "Basic " + text;
			}
			Stream requestStream = httpWebRequest.GetRequestStream();
			requestStream.Write(requestBytes, 0, requestBytes.Length);
			requestStream.Close();
			HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			if (httpWebResponse.StatusCode != HttpStatusCode.OK)
			{
				throw new IOException(MessageLocalization.GetComposedMessage("invalid.http.response.1", (int)httpWebResponse.StatusCode));
			}
			Stream responseStream = httpWebResponse.GetResponseStream();
			MemoryStream memoryStream = new MemoryStream();
			byte[] array = new byte[1024];
			int count;
			while ((count = responseStream.Read(array, 0, array.Length)) > 0)
			{
				memoryStream.Write(array, 0, count);
			}
			responseStream.Close();
			httpWebResponse.Close();
			byte[] array2 = memoryStream.ToArray();
			string contentEncoding = httpWebResponse.ContentEncoding;
			if (contentEncoding != null && Util.EqualsIgnoreCase(contentEncoding, "base64"))
			{
				array2 = Convert.FromBase64String(Encoding.ASCII.GetString(array2));
			}
			return array2;
		}

		// Token: 0x0400210B RID: 8459
		protected string tsaURL;

		// Token: 0x0400210C RID: 8460
		protected string tsaUsername;

		// Token: 0x0400210D RID: 8461
		protected string tsaPassword;

		// Token: 0x0400210E RID: 8462
		protected int tokSzEstimate;
	}
}
