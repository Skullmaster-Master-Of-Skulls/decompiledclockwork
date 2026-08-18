using System;
using System.Collections.Generic;

namespace System
{
	// Token: 0x02000016 RID: 22
	internal class UriTemplatePathPartiallyEquivalentSet
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x00005886 File Offset: 0x00003A86
		public UriTemplatePathPartiallyEquivalentSet(int segmentsCount)
		{
			this.segmentsCount = segmentsCount;
			this.kvps = new List<KeyValuePair<UriTemplate, object>>();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000058A0 File Offset: 0x00003AA0
		public List<KeyValuePair<UriTemplate, object>> Items
		{
			get
			{
				return this.kvps;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000058A8 File Offset: 0x00003AA8
		public int SegmentsCount
		{
			get
			{
				return this.segmentsCount;
			}
		}

		// Token: 0x04000089 RID: 137
		private List<KeyValuePair<UriTemplate, object>> kvps;

		// Token: 0x0400008A RID: 138
		private int segmentsCount;
	}
}
