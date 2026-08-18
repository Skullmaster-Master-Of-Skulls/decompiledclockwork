using System;
using System.Collections.Generic;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02001ABE RID: 6846
	public class SitemapProtocolExporter
	{
		// Token: 0x17005089 RID: 20617
		// (get) Token: 0x06010902 RID: 67842 RVA: 0x003B1F3D File Offset: 0x003B013D
		// (set) Token: 0x06010903 RID: 67843 RVA: 0x003B1F45 File Offset: 0x003B0145
		public int SitemapNodeLimit
		{
			get
			{
				return this._sitemapNodeLimit;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value", "Please specify a positive value.");
				}
				this._sitemapNodeLimit = value;
			}
		}

		// Token: 0x1700508A RID: 20618
		// (get) Token: 0x06010904 RID: 67844 RVA: 0x003B1F62 File Offset: 0x003B0162
		// (set) Token: 0x06010905 RID: 67845 RVA: 0x003B1F6A File Offset: 0x003B016A
		public int SitemapByteLimit
		{
			get
			{
				return this._sitemapByteLimit;
			}
			set
			{
				this._sitemapByteLimit = value;
			}
		}

		// Token: 0x06010906 RID: 67846 RVA: 0x003B1F73 File Offset: 0x003B0173
		public SitemapProtocolExporter()
		{
			this._sitemaps = new List<string>();
		}

		// Token: 0x06010907 RID: 67847 RVA: 0x003B1F9C File Offset: 0x003B019C
		public string[] GetSitemaps()
		{
			if (this._sitemapProtocolBuilder == null)
			{
				return this._sitemaps.ToArray();
			}
			string[] array = new string[this._sitemaps.Count + 1];
			for (int i = 0; i < this._sitemaps.Count; i++)
			{
				array[i] = string.Format("{0}{1}{2}", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">", this._sitemaps[i], "</urlset>");
			}
			array[this._sitemaps.Count] = string.Format("{0}{1}{2}", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">", this._sitemapProtocolBuilder.ToString(), "</urlset>");
			return array;
		}

		// Token: 0x06010908 RID: 67848 RVA: 0x003B2038 File Offset: 0x003B0238
		public void AddNode(string url)
		{
			if (this._sitemapNodeLimit == 0)
			{
				return;
			}
			string text = string.Format("<url><loc>{0}</loc></url>", url);
			this.VerifySitemapByteLimit(text);
			if (this._sitemaps.Count == 0 && this._sitemapProtocolBuilder == null)
			{
				this._sitemapProtocolBuilder = new StringBuilder();
			}
			if (++this._nodesCount > this._sitemapNodeLimit || "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">".Length + this._sitemapProtocolBuilder.Length + text.Length + "</urlset>".Length > this._sitemapByteLimit)
			{
				this.VerifySitemapByteLimit(text);
				this._sitemaps.Add(this._sitemapProtocolBuilder.ToString());
				this._sitemapProtocolBuilder = new StringBuilder();
			}
			this._sitemapProtocolBuilder.Append(text);
		}

		// Token: 0x06010909 RID: 67849 RVA: 0x003B20FF File Offset: 0x003B02FF
		private void VerifySitemapByteLimit(string nodeSegment)
		{
			if ("<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">".Length + nodeSegment.Length + "</urlset>".Length > this._sitemapByteLimit)
			{
				throw new ArgumentException("The added Node does not fit in a new Sitemap Protocol file. Please increase the value of the SitemapByteLimit property.", "url");
			}
		}

		// Token: 0x04004A00 RID: 18944
		private const string ParemeterNameUrl = "url";

		// Token: 0x04004A01 RID: 18945
		private const string NodeLimitMustBePositiveValueExceptionMessage = "Please specify a positive value.";

		// Token: 0x04004A02 RID: 18946
		private const string NodeDoesNotFitInNewSitemapProtocolFileExceptionMessage = "The added Node does not fit in a new Sitemap Protocol file. Please increase the value of the SitemapByteLimit property.";

		// Token: 0x04004A03 RID: 18947
		private const string SitemapProtocolStartingSegment = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">";

		// Token: 0x04004A04 RID: 18948
		private const string SitemapProtocolNodeSegmentTemplate = "<url><loc>{0}</loc></url>";

		// Token: 0x04004A05 RID: 18949
		private const string SitemapProtocolEndingSegment = "</urlset>";

		// Token: 0x04004A06 RID: 18950
		private const string SitemapProtocolTemplate = "{0}{1}{2}";

		// Token: 0x04004A07 RID: 18951
		private StringBuilder _sitemapProtocolBuilder;

		// Token: 0x04004A08 RID: 18952
		private List<string> _sitemaps;

		// Token: 0x04004A09 RID: 18953
		private int _sitemapNodeLimit = 50000;

		// Token: 0x04004A0A RID: 18954
		private int _nodesCount;

		// Token: 0x04004A0B RID: 18955
		private int _sitemapByteLimit = 10485760;
	}
}
