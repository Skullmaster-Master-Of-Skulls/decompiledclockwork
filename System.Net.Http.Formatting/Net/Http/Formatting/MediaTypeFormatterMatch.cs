using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200004D RID: 77
	public class MediaTypeFormatterMatch
	{
		// Token: 0x060002CC RID: 716 RVA: 0x0000A7DC File Offset: 0x000089DC
		public MediaTypeFormatterMatch(MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, double? quality, MediaTypeFormatterMatchRanking ranking)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			this.Formatter = formatter;
			this.MediaType = ((mediaType != null) ? mediaType.Clone<MediaTypeHeaderValue>() : MediaTypeConstants.ApplicationOctetStreamMediaType);
			double? num = quality;
			this.Quality = ((num != null) ? num.GetValueOrDefault() : 1.0);
			this.Ranking = ranking;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000A847 File Offset: 0x00008A47
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000A84F File Offset: 0x00008A4F
		public MediaTypeFormatter Formatter { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000A858 File Offset: 0x00008A58
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000A860 File Offset: 0x00008A60
		public MediaTypeHeaderValue MediaType { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000A869 File Offset: 0x00008A69
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000A871 File Offset: 0x00008A71
		public double Quality { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000A87A File Offset: 0x00008A7A
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000A882 File Offset: 0x00008A82
		public MediaTypeFormatterMatchRanking Ranking { get; private set; }
	}
}
