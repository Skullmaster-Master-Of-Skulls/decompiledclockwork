using System;
using System.Collections.Generic;

namespace WebGrease.Activities
{
	// Token: 0x02000004 RID: 4
	internal class MinifyCssResult
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000247C File Offset: 0x0000067C
		public MinifyCssResult(IEnumerable<ContentItem> css, IEnumerable<ContentItem> spritedImages, IEnumerable<ContentItem> hashedImages)
		{
			this.Css = css;
			this.SpritedImages = spritedImages;
			this.HashedImages = hashedImages;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002499 File Offset: 0x00000699
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000024A1 File Offset: 0x000006A1
		internal IEnumerable<ContentItem> Css { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000024AA File Offset: 0x000006AA
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000024B2 File Offset: 0x000006B2
		internal IEnumerable<ContentItem> SpritedImages { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000024BB File Offset: 0x000006BB
		// (set) Token: 0x06000014 RID: 20 RVA: 0x000024C3 File Offset: 0x000006C3
		internal IEnumerable<ContentItem> HashedImages { get; private set; }
	}
}
