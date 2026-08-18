using System;
using System.Collections.Generic;
using WebGrease.ImageAssemble;

namespace WebGrease.Css.ImageAssemblyAnalysis
{
	// Token: 0x0200018D RID: 397
	internal sealed class ImageAssemblyScanOutput
	{
		// Token: 0x0600148B RID: 5259 RVA: 0x0007840A File Offset: 0x0007660A
		internal ImageAssemblyScanOutput()
		{
			this.ImageReferencesToAssemble = new List<InputImage>();
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0007841D File Offset: 0x0007661D
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x00078425 File Offset: 0x00076625
		internal ImageAssemblyScanInput ImageAssemblyScanInput { get; set; }

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0007842E File Offset: 0x0007662E
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00078436 File Offset: 0x00076636
		internal IList<InputImage> ImageReferencesToAssemble { get; private set; }
	}
}
