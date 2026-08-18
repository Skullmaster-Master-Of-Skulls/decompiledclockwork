using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;

namespace System
{
	// Token: 0x0200000C RID: 12
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class UriTemplateMatch
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003E38 File Offset: 0x00002038
		// (set) Token: 0x06000042 RID: 66 RVA: 0x00003E8A File Offset: 0x0000208A
		public Uri BaseUri
		{
			get
			{
				if (this.baseUri == null && this.originalBaseUri != null)
				{
					this.baseUri = UriTemplate.RewriteUri(this.originalBaseUri, this.requestProp.Headers[HttpRequestHeader.Host]);
				}
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
				this.originalBaseUri = null;
				this.requestProp = null;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003EA1 File Offset: 0x000020A1
		public NameValueCollection BoundVariables
		{
			get
			{
				if (this.boundVariables == null)
				{
					this.boundVariables = new NameValueCollection();
				}
				return this.boundVariables;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003EBC File Offset: 0x000020BC
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003EC4 File Offset: 0x000020C4
		public object Data
		{
			get
			{
				return this.data;
			}
			set
			{
				this.data = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003ECD File Offset: 0x000020CD
		public NameValueCollection QueryParameters
		{
			get
			{
				if (this.queryParameters == null)
				{
					this.PopulateQueryParameters();
				}
				return this.queryParameters;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003EE3 File Offset: 0x000020E3
		public Collection<string> RelativePathSegments
		{
			get
			{
				if (this.relativePathSegments == null)
				{
					this.relativePathSegments = new Collection<string>();
				}
				return this.relativePathSegments;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00003EFE File Offset: 0x000020FE
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00003F06 File Offset: 0x00002106
		public Uri RequestUri
		{
			get
			{
				return this.requestUri;
			}
			set
			{
				this.requestUri = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003F0F File Offset: 0x0000210F
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00003F17 File Offset: 0x00002117
		public UriTemplate Template
		{
			get
			{
				return this.template;
			}
			set
			{
				this.template = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00003F20 File Offset: 0x00002120
		public Collection<string> WildcardPathSegments
		{
			get
			{
				if (this.wildcardPathSegments == null)
				{
					this.PopulateWildcardSegments();
				}
				return this.wildcardPathSegments;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003F36 File Offset: 0x00002136
		internal void SetQueryParameters(NameValueCollection queryParameters)
		{
			this.queryParameters = new NameValueCollection(queryParameters);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003F44 File Offset: 0x00002144
		internal void SetRelativePathSegments(Collection<string> segments)
		{
			this.relativePathSegments = segments;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003F4D File Offset: 0x0000214D
		internal void SetWildcardPathSegmentsStart(int startOffset)
		{
			this.wildcardSegmentsStartOffset = startOffset;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003F56 File Offset: 0x00002156
		internal void SetBaseUri(Uri originalBaseUri, HttpRequestMessageProperty requestProp)
		{
			this.baseUri = null;
			this.originalBaseUri = originalBaseUri;
			this.requestProp = requestProp;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003F6D File Offset: 0x0000216D
		private void PopulateQueryParameters()
		{
			if (this.requestUri != null)
			{
				this.queryParameters = UriTemplateHelpers.ParseQueryString(this.requestUri.Query);
				return;
			}
			this.queryParameters = new NameValueCollection();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003FA0 File Offset: 0x000021A0
		private void PopulateWildcardSegments()
		{
			if (this.wildcardSegmentsStartOffset != -1)
			{
				this.wildcardPathSegments = new Collection<string>();
				for (int i = this.wildcardSegmentsStartOffset; i < this.RelativePathSegments.Count; i++)
				{
					this.wildcardPathSegments.Add(this.RelativePathSegments[i]);
				}
				return;
			}
			this.wildcardPathSegments = new Collection<string>();
		}

		// Token: 0x04000064 RID: 100
		private Uri baseUri;

		// Token: 0x04000065 RID: 101
		private NameValueCollection boundVariables;

		// Token: 0x04000066 RID: 102
		private object data;

		// Token: 0x04000067 RID: 103
		private NameValueCollection queryParameters;

		// Token: 0x04000068 RID: 104
		private Collection<string> relativePathSegments;

		// Token: 0x04000069 RID: 105
		private Uri requestUri;

		// Token: 0x0400006A RID: 106
		private UriTemplate template;

		// Token: 0x0400006B RID: 107
		private Collection<string> wildcardPathSegments;

		// Token: 0x0400006C RID: 108
		private int wildcardSegmentsStartOffset = -1;

		// Token: 0x0400006D RID: 109
		private Uri originalBaseUri;

		// Token: 0x0400006E RID: 110
		private HttpRequestMessageProperty requestProp;
	}
}
