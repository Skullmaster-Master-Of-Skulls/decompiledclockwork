using System;
using System.Runtime.InteropServices;

namespace System.Linq.Parallel
{
	// Token: 0x020001F0 RID: 496
	internal static class Scheduling
	{
		// Token: 0x06000FF9 RID: 4089 RVA: 0x000386B0 File Offset: 0x000368B0
		internal static int GetDefaultDegreeOfParallelism()
		{
			return Scheduling.DefaultDegreeOfParallelism;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000386B8 File Offset: 0x000368B8
		internal static int GetDefaultChunkSize<T>()
		{
			int result;
			if (typeof(T).IsValueType)
			{
				if (typeof(T).StructLayoutAttribute.Value == LayoutKind.Explicit)
				{
					result = Math.Max(1, 512 / Marshal.SizeOf(typeof(T)));
				}
				else
				{
					result = 128;
				}
			}
			else
			{
				result = 512 / IntPtr.Size;
			}
			return result;
		}

		// Token: 0x0400091A RID: 2330
		internal const bool DefaultPreserveOrder = false;

		// Token: 0x0400091B RID: 2331
		internal static int DefaultDegreeOfParallelism = Math.Min(Environment.ProcessorCount, 512);

		// Token: 0x0400091C RID: 2332
		internal const int DEFAULT_BOUNDED_BUFFER_CAPACITY = 512;

		// Token: 0x0400091D RID: 2333
		internal const int DEFAULT_BYTES_PER_CHUNK = 512;

		// Token: 0x0400091E RID: 2334
		internal const int ZOMBIED_PRODUCER_TIMEOUT = -1;

		// Token: 0x0400091F RID: 2335
		internal const int MAX_SUPPORTED_DOP = 512;
	}
}
