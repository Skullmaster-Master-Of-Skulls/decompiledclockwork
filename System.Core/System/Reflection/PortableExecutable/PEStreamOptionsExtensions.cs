using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200004F RID: 79
	internal static class PEStreamOptionsExtensions
	{
		// Token: 0x06000221 RID: 545 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public static bool IsValid(this PEStreamOptions options)
		{
			return (options & ~(PEStreamOptions.LeaveOpen | PEStreamOptions.PrefetchMetadata | PEStreamOptions.PrefetchEntireImage | PEStreamOptions.IsLoadedImage)) == PEStreamOptions.Default;
		}
	}
}
