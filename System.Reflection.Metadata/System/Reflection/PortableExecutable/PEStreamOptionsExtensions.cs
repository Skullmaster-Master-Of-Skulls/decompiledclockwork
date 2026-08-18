using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200002B RID: 43
	internal static class PEStreamOptionsExtensions
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000721C File Offset: 0x0000541C
		public static bool IsValid(this PEStreamOptions options)
		{
			return (options & ~(PEStreamOptions.LeaveOpen | PEStreamOptions.PrefetchMetadata | PEStreamOptions.PrefetchEntireImage)) == PEStreamOptions.Default;
		}
	}
}
