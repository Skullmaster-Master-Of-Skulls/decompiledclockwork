using System;
using System.Collections.ObjectModel;

namespace WebGrease.Css.ImageAssemblyAnalysis
{
	// Token: 0x0200018C RID: 396
	public sealed class ImageAssemblyScanInput
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x000783D2 File Offset: 0x000765D2
		public ImageAssemblyScanInput(string bucketName, ReadOnlyCollection<string> imagesInBucket)
		{
			this.BucketName = bucketName;
			this.ImagesInBucket = imagesInBucket;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x000783E8 File Offset: 0x000765E8
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x000783F0 File Offset: 0x000765F0
		public string BucketName { get; private set; }

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x000783F9 File Offset: 0x000765F9
		// (set) Token: 0x0600148A RID: 5258 RVA: 0x00078401 File Offset: 0x00076601
		public ReadOnlyCollection<string> ImagesInBucket { get; private set; }
	}
}
