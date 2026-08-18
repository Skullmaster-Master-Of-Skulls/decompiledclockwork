using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200000F RID: 15
	[MeansImplicitUse]
	internal sealed class PublicAPIAttribute : Attribute
	{
		// Token: 0x06000029 RID: 41 RVA: 0x0000226F File Offset: 0x0000046F
		public PublicAPIAttribute()
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002277 File Offset: 0x00000477
		public PublicAPIAttribute([NotNull] string comment)
		{
			this.Comment = comment;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002286 File Offset: 0x00000486
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000228E File Offset: 0x0000048E
		[NotNull]
		public string Comment { get; private set; }
	}
}
