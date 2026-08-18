using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x020001AC RID: 428
	internal class HttpDigestChallenge
	{
		// Token: 0x060010DF RID: 4319 RVA: 0x0005A9F0 File Offset: 0x00058BF0
		internal void SetFromRequest(HttpWebRequest httpWebRequest)
		{
			this.HostName = httpWebRequest.ChallengedUri.Host;
			this.Method = httpWebRequest.CurrentMethod.Name;
			if (httpWebRequest.CurrentMethod.ConnectRequest)
			{
				this.Uri = httpWebRequest.RequestUri.GetParts(UriComponents.HostAndPort, UriFormat.UriEscaped);
			}
			else
			{
				this.Uri = "/" + httpWebRequest.GetRemoteResourceUri().GetParts(UriComponents.Path, UriFormat.UriEscaped);
			}
			this.ChallengedUri = httpWebRequest.ChallengedUri;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x0005AA70 File Offset: 0x00058C70
		internal HttpDigestChallenge CopyAndIncrementNonce()
		{
			HttpDigestChallenge httpDigestChallenge = null;
			lock (this)
			{
				httpDigestChallenge = (base.MemberwiseClone() as HttpDigestChallenge);
				this.NonceCount++;
			}
			httpDigestChallenge.MD5provider = new MD5CryptoServiceProvider();
			return httpDigestChallenge;
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x0005AAD0 File Offset: 0x00058CD0
		public bool defineAttribute(string name, string value)
		{
			name = name.Trim().ToLower(CultureInfo.InvariantCulture);
			if (name.Equals("algorithm"))
			{
				this.Algorithm = value;
			}
			else if (name.Equals("cnonce"))
			{
				this.ClientNonce = value;
			}
			else if (name.Equals("nc"))
			{
				this.NonceCount = int.Parse(value, NumberFormatInfo.InvariantInfo);
			}
			else if (name.Equals("nonce"))
			{
				this.Nonce = value;
			}
			else if (name.Equals("opaque"))
			{
				this.Opaque = value;
			}
			else if (name.Equals("qop"))
			{
				this.QualityOfProtection = value;
				this.QopPresent = (this.QualityOfProtection != null && this.QualityOfProtection.Length > 0);
			}
			else if (name.Equals("realm"))
			{
				this.Realm = value;
			}
			else if (name.Equals("domain"))
			{
				this.Domain = value;
			}
			else if (!name.Equals("response"))
			{
				if (name.Equals("stale"))
				{
					this.Stale = value.ToLower(CultureInfo.InvariantCulture).Equals("true");
				}
				else if (name.Equals("uri"))
				{
					this.Uri = value;
				}
				else if (name.Equals("charset"))
				{
					this.Charset = value;
				}
				else if (!name.Equals("cipher") && !name.Equals("username"))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x0005AC60 File Offset: 0x00058E60
		internal string ToBlob()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(HttpDigest.pair("realm", this.Realm, true));
			if (this.Algorithm != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("algorithm", this.Algorithm, true));
			}
			if (this.Charset != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("charset", this.Charset, false));
			}
			if (this.Nonce != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("nonce", this.Nonce, true));
			}
			if (this.Uri != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("uri", this.Uri, true));
			}
			if (this.ClientNonce != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("cnonce", this.ClientNonce, true));
			}
			if (this.NonceCount > 0)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("nc", this.NonceCount.ToString("x8", NumberFormatInfo.InvariantInfo), true));
			}
			if (this.QualityOfProtection != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("qop", this.QualityOfProtection, true));
			}
			if (this.Opaque != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("opaque", this.Opaque, true));
			}
			if (this.Domain != null)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("domain", this.Domain, true));
			}
			if (this.Stale)
			{
				stringBuilder.Append(",");
				stringBuilder.Append(HttpDigest.pair("stale", "true", true));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040013B8 RID: 5048
		internal string HostName;

		// Token: 0x040013B9 RID: 5049
		internal string Realm;

		// Token: 0x040013BA RID: 5050
		internal Uri ChallengedUri;

		// Token: 0x040013BB RID: 5051
		internal string Uri;

		// Token: 0x040013BC RID: 5052
		internal string Nonce;

		// Token: 0x040013BD RID: 5053
		internal string Opaque;

		// Token: 0x040013BE RID: 5054
		internal bool Stale;

		// Token: 0x040013BF RID: 5055
		internal string Algorithm;

		// Token: 0x040013C0 RID: 5056
		internal string Method;

		// Token: 0x040013C1 RID: 5057
		internal string Domain;

		// Token: 0x040013C2 RID: 5058
		internal string QualityOfProtection;

		// Token: 0x040013C3 RID: 5059
		internal string ClientNonce;

		// Token: 0x040013C4 RID: 5060
		internal int NonceCount;

		// Token: 0x040013C5 RID: 5061
		internal string Charset;

		// Token: 0x040013C6 RID: 5062
		internal string ServiceName;

		// Token: 0x040013C7 RID: 5063
		internal string ChannelBinding;

		// Token: 0x040013C8 RID: 5064
		internal bool UTF8Charset;

		// Token: 0x040013C9 RID: 5065
		internal bool QopPresent;

		// Token: 0x040013CA RID: 5066
		internal MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();
	}
}
