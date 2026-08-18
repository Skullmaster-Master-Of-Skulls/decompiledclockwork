using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.Parameter)]
	internal class PathReferenceAttribute : Attribute
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000022A7 File Offset: 0x000004A7
		public PathReferenceAttribute()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000022AF File Offset: 0x000004AF
		public PathReferenceAttribute([PathReference] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000022BE File Offset: 0x000004BE
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000022C6 File Offset: 0x000004C6
		[NotNull]
		public string BasePath { get; private set; }
	}
}
