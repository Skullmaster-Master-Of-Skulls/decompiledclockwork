using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000161 RID: 353
	internal static class ImmutableByteArrayInterop
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x0001F440 File Offset: 0x0001D640
		internal static ImmutableArray<byte> DangerousCreateFromUnderlyingArray(ref byte[] array)
		{
			byte[] underlyingArray = array;
			array = null;
			return new ImmutableByteArrayInterop.ByteArrayUnion
			{
				UnderlyingArray = underlyingArray
			}.ImmutableArray;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0001F46C File Offset: 0x0001D66C
		internal static byte[] DangerousGetUnderlyingArray(ImmutableArray<byte> array)
		{
			return new ImmutableByteArrayInterop.ByteArrayUnion
			{
				ImmutableArray = array
			}.UnderlyingArray;
		}

		// Token: 0x020001E0 RID: 480
		[StructLayout(LayoutKind.Explicit)]
		private struct ByteArrayUnion
		{
			// Token: 0x04000B52 RID: 2898
			[FieldOffset(0)]
			internal byte[] UnderlyingArray;

			// Token: 0x04000B53 RID: 2899
			[FieldOffset(0)]
			internal ImmutableArray<byte> ImmutableArray;
		}
	}
}
