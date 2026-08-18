using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace System.Web.Optimization
{
	// Token: 0x02000012 RID: 18
	public class BundleResponse
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00003CA5 File Offset: 0x00001EA5
		public BundleResponse()
		{
			this.CreationDate = DateTimeOffset.UtcNow;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public BundleResponse(string content, IEnumerable<BundleFile> files) : this()
		{
			this.Content = content;
			this.Files = files;
			this.Cacheability = HttpCacheability.Public;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003CD5 File Offset: 0x00001ED5
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003CDD File Offset: 0x00001EDD
		public string Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
				this._contentHash = null;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003CED File Offset: 0x00001EED
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003CF5 File Offset: 0x00001EF5
		public string ContentType { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003CFE File Offset: 0x00001EFE
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003D06 File Offset: 0x00001F06
		public DateTimeOffset CreationDate { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003D0F File Offset: 0x00001F0F
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003D17 File Offset: 0x00001F17
		public HttpCacheability Cacheability { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003D20 File Offset: 0x00001F20
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003D28 File Offset: 0x00001F28
		public IEnumerable<BundleFile> Files
		{
			get
			{
				return this._files;
			}
			set
			{
				this._files = value;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003D34 File Offset: 0x00001F34
		internal static string ComputeHash(string input)
		{
			string result;
			using (SHA256 sha = BundleResponse.CreateHashAlgorithm())
			{
				byte[] input2 = sha.ComputeHash(Encoding.Unicode.GetBytes(input));
				result = HttpServerUtility.UrlTokenEncode(input2);
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003D80 File Offset: 0x00001F80
		internal string GetContentHashCode()
		{
			if (this._contentHash == null)
			{
				if (string.IsNullOrEmpty(this.Content))
				{
					this._contentHash = string.Empty;
				}
				else
				{
					this._contentHash = BundleResponse.ComputeHash(this.Content);
				}
			}
			return this._contentHash;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003DBB File Offset: 0x00001FBB
		private static bool AllowOnlyFipsAlgorithms
		{
			get
			{
				return !BundleResponse._isMonoRuntime && CryptoConfig.AllowOnlyFipsAlgorithms;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003DCB File Offset: 0x00001FCB
		private static SHA256 CreateHashAlgorithm()
		{
			if (BundleResponse.AllowOnlyFipsAlgorithms)
			{
				return new SHA256CryptoServiceProvider();
			}
			return new SHA256Managed();
		}

		// Token: 0x04000034 RID: 52
		private string _content;

		// Token: 0x04000035 RID: 53
		private string _contentHash;

		// Token: 0x04000036 RID: 54
		private IEnumerable<BundleFile> _files;

		// Token: 0x04000037 RID: 55
		private static readonly bool _isMonoRuntime = Type.GetType("Mono.Runtime") != null;
	}
}
