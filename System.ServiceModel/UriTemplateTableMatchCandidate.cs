using System;

namespace System
{
	// Token: 0x0200000D RID: 13
	internal struct UriTemplateTableMatchCandidate
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003FFF File Offset: 0x000021FF
		public UriTemplateTableMatchCandidate(UriTemplate template, int segmentsCount, object data)
		{
			this.template = template;
			this.segmentsCount = segmentsCount;
			this.data = data;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00004016 File Offset: 0x00002216
		public object Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000401E File Offset: 0x0000221E
		public int SegmentsCount
		{
			get
			{
				return this.segmentsCount;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00004026 File Offset: 0x00002226
		public UriTemplate Template
		{
			get
			{
				return this.template;
			}
		}

		// Token: 0x0400006F RID: 111
		private readonly object data;

		// Token: 0x04000070 RID: 112
		private readonly int segmentsCount;

		// Token: 0x04000071 RID: 113
		private readonly UriTemplate template;
	}
}
