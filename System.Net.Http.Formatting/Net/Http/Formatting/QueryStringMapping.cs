using System;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004A RID: 74
	public class QueryStringMapping : MediaTypeMapping
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000A4D9 File Offset: 0x000086D9
		public QueryStringMapping(string queryStringParameterName, string queryStringParameterValue, string mediaType) : base(mediaType)
		{
			this.Initialize(queryStringParameterName, queryStringParameterValue);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000A4EA File Offset: 0x000086EA
		public QueryStringMapping(string queryStringParameterName, string queryStringParameterValue, MediaTypeHeaderValue mediaType) : base(mediaType)
		{
			this.Initialize(queryStringParameterName, queryStringParameterValue);
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A4FB File Offset: 0x000086FB
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000A503 File Offset: 0x00008703
		public string QueryStringParameterName { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000A50C File Offset: 0x0000870C
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000A514 File Offset: 0x00008714
		public string QueryStringParameterValue { get; private set; }

		// Token: 0x060002BA RID: 698 RVA: 0x0000A520 File Offset: 0x00008720
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			NameValueCollection queryString = QueryStringMapping.GetQueryString(request.RequestUri);
			if (!this.DoesQueryStringMatch(queryString))
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000A564 File Offset: 0x00008764
		private static NameValueCollection GetQueryString(Uri uri)
		{
			if (uri == null)
			{
				throw Error.InvalidOperation(Resources.NonNullUriRequiredForMediaTypeMapping, new object[]
				{
					QueryStringMapping._queryStringMappingType.Name
				});
			}
			return new FormDataCollection(uri).ReadAsNameValueCollection();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A5A5 File Offset: 0x000087A5
		private void Initialize(string queryStringParameterName, string queryStringParameterValue)
		{
			if (string.IsNullOrWhiteSpace(queryStringParameterName))
			{
				throw Error.ArgumentNull("queryStringParameterName");
			}
			if (string.IsNullOrWhiteSpace(queryStringParameterValue))
			{
				throw Error.ArgumentNull("queryStringParameterValue");
			}
			this.QueryStringParameterName = queryStringParameterName.Trim();
			this.QueryStringParameterValue = queryStringParameterValue.Trim();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000A5E8 File Offset: 0x000087E8
		private bool DoesQueryStringMatch(NameValueCollection queryString)
		{
			if (queryString != null)
			{
				foreach (string text in queryString.AllKeys)
				{
					if (string.Equals(text, this.QueryStringParameterName, StringComparison.OrdinalIgnoreCase))
					{
						string a = queryString[text];
						if (string.Equals(a, this.QueryStringParameterValue, StringComparison.OrdinalIgnoreCase))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x040000BC RID: 188
		private static readonly Type _queryStringMappingType = typeof(QueryStringMapping);
	}
}
