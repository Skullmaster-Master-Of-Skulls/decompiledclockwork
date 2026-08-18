using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005C RID: 92
	internal static class MetadataStreamOptionsExtensions
	{
		// Token: 0x06000299 RID: 665 RVA: 0x000071DF File Offset: 0x000053DF
		public static bool IsValid(this MetadataStreamOptions options)
		{
			return (options & ~(MetadataStreamOptions.LeaveOpen | MetadataStreamOptions.PrefetchMetadata)) == MetadataStreamOptions.Default;
		}
	}
}
