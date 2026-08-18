using System;

namespace System.Net.Http
{
	// Token: 0x02000015 RID: 21
	[__DynamicallyInvokable]
	public class HttpMethod : IEquatable<HttpMethod>
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000059A7 File Offset: 0x00003BA7
		[__DynamicallyInvokable]
		public static HttpMethod Get
		{
			[__DynamicallyInvokable]
			get
			{
				return HttpMethod.getMethod;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600011F RID: 287 RVA: 0x000059AE File Offset: 0x00003BAE
		[__DynamicallyInvokable]
		public static HttpMethod Put
		{
			[__DynamicallyInvokable]
			get
			{
				return HttpMethod.putMethod;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000059B5 File Offset: 0x00003BB5
		[__DynamicallyInvokable]
		public static HttpMethod Post
		{
			[__DynamicallyInvokable]
			get
			{
				return HttpMethod.postMethod;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000121 RID: 289 RVA: 0x000059BC File Offset: 0x00003BBC
		[__DynamicallyInvokable]
		public static HttpMethod Delete
		{
			[__DynamicallyInvokable]
			get
			{
				return HttpMethod.deleteMethod;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000059C3 File Offset: 0x00003BC3
		[__DynamicallyInvokable]
		public static HttpMethod Head
		{
			[__DynamicallyInvokable]
			get
			{
				return HttpMethod.headMethod;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000059CA File Offset: 0x00003BCA
		[__DynamicallyInvokable]
		public static HttpMethod Options
		{
			[__DynamicallyInvokable]
			get
			{
				return HttpMethod.optionsMethod;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000059D1 File Offset: 0x00003BD1
		[__DynamicallyInvokable]
		public static HttpMethod Trace
		{
			[__DynamicallyInvokable]
			get
			{
				return HttpMethod.traceMethod;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000059D8 File Offset: 0x00003BD8
		[__DynamicallyInvokable]
		public string Method
		{
			[__DynamicallyInvokable]
			get
			{
				return this.method;
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000059E0 File Offset: 0x00003BE0
		[__DynamicallyInvokable]
		public HttpMethod(string method)
		{
			if (string.IsNullOrEmpty(method))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "method");
			}
			if (HttpRuleParser.GetTokenLength(method, 0) != method.Length)
			{
				throw new FormatException(SR.net_http_httpmethod_format_error);
			}
			this.method = method;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005A2C File Offset: 0x00003C2C
		[__DynamicallyInvokable]
		public bool Equals(HttpMethod other)
		{
			return other != null && (this.method == other.method || string.Compare(this.method, other.method, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005A58 File Offset: 0x00003C58
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as HttpMethod);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005A66 File Offset: 0x00003C66
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.method.ToUpperInvariant().GetHashCode();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00005A78 File Offset: 0x00003C78
		[__DynamicallyInvokable]
		public override string ToString()
		{
			return this.method.ToString();
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005A85 File Offset: 0x00003C85
		[__DynamicallyInvokable]
		public static bool operator ==(HttpMethod left, HttpMethod right)
		{
			if (left == null)
			{
				return right == null;
			}
			if (right == null)
			{
				return left == null;
			}
			return left.Equals(right);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005A9E File Offset: 0x00003C9E
		[__DynamicallyInvokable]
		public static bool operator !=(HttpMethod left, HttpMethod right)
		{
			return !(left == right);
		}

		// Token: 0x040000A4 RID: 164
		private string method;

		// Token: 0x040000A5 RID: 165
		private static readonly HttpMethod getMethod = new HttpMethod("GET");

		// Token: 0x040000A6 RID: 166
		private static readonly HttpMethod putMethod = new HttpMethod("PUT");

		// Token: 0x040000A7 RID: 167
		private static readonly HttpMethod postMethod = new HttpMethod("POST");

		// Token: 0x040000A8 RID: 168
		private static readonly HttpMethod deleteMethod = new HttpMethod("DELETE");

		// Token: 0x040000A9 RID: 169
		private static readonly HttpMethod headMethod = new HttpMethod("HEAD");

		// Token: 0x040000AA RID: 170
		private static readonly HttpMethod optionsMethod = new HttpMethod("OPTIONS");

		// Token: 0x040000AB RID: 171
		private static readonly HttpMethod traceMethod = new HttpMethod("TRACE");
	}
}
