using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace System.Net
{
	// Token: 0x020004D6 RID: 1238
	internal class HttpDigestChallenge
	{
		// Token: 0x06002686 RID: 9862 RVA: 0x0009CD7C File Offset: 0x0009BD7C
		internal void SetFromRequest(HttpWebRequest httpWebRequest)
		{
			this.HostName = httpWebRequest.ChallengedUri.Host;
			this.Method = httpWebRequest.CurrentMethod.Name;
			this.Uri = httpWebRequest.Address.AbsolutePath;
			this.ChallengedUri = httpWebRequest.ChallengedUri;
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0009CDC8 File Offset: 0x0009BDC8
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

		// Token: 0x06002688 RID: 9864 RVA: 0x0009CE20 File Offset: 0x0009BE20
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

		// Token: 0x06002689 RID: 9865 RVA: 0x0009CFB0 File Offset: 0x0009BFB0
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

		// Token: 0x040025FD RID: 9725
		internal string HostName;

		// Token: 0x040025FE RID: 9726
		internal string Realm;

		// Token: 0x040025FF RID: 9727
		internal Uri ChallengedUri;

		// Token: 0x04002600 RID: 9728
		internal string Uri;

		// Token: 0x04002601 RID: 9729
		internal string Nonce;

		// Token: 0x04002602 RID: 9730
		internal string Opaque;

		// Token: 0x04002603 RID: 9731
		internal bool Stale;

		// Token: 0x04002604 RID: 9732
		internal string Algorithm;

		// Token: 0x04002605 RID: 9733
		internal string Method;

		// Token: 0x04002606 RID: 9734
		internal string Domain;

		// Token: 0x04002607 RID: 9735
		internal string QualityOfProtection;

		// Token: 0x04002608 RID: 9736
		internal string ClientNonce;

		// Token: 0x04002609 RID: 9737
		internal int NonceCount;

		// Token: 0x0400260A RID: 9738
		internal string Charset;

		// Token: 0x0400260B RID: 9739
		internal string ServiceName;

		// Token: 0x0400260C RID: 9740
		internal string ChannelBinding;

		// Token: 0x0400260D RID: 9741
		internal bool UTF8Charset;

		// Token: 0x0400260E RID: 9742
		internal bool QopPresent;

		// Token: 0x0400260F RID: 9743
		internal MD5CryptoServiceProvider MD5provider = new MD5CryptoServiceProvider();
	}
}
