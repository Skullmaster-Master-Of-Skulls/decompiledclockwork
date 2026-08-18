using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Reflection.Internal
{
	// Token: 0x0200014F RID: 335
	internal sealed class ByteSequenceComparer : IEqualityComparer<byte[]>, IEqualityComparer<ImmutableArray<byte>>
	{
		// Token: 0x06000A8D RID: 2701 RVA: 0x00005A68 File Offset: 0x00003C68
		private ByteSequenceComparer()
		{
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0001E608 File Offset: 0x0001C808
		internal static bool Equals(ImmutableArray<byte> x, ImmutableArray<byte> y)
		{
			if (x == y)
			{
				return true;
			}
			if (x.IsDefault || y.IsDefault || x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0001E66C File Offset: 0x0001C86C
		internal static bool Equals(byte[] left, int leftStart, byte[] right, int rightStart, int length)
		{
			if (left == null || right == null)
			{
				return left == right;
			}
			if (left == right && leftStart == rightStart)
			{
				return true;
			}
			for (int i = 0; i < length; i++)
			{
				if (left[leftStart + i] != right[rightStart + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		internal static bool Equals(byte[] left, byte[] right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null || left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0001E6E8 File Offset: 0x0001C8E8
		internal static int GetHashCode(byte[] x)
		{
			return Hash.GetFNVHashCode(x);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
		internal static int GetHashCode(ImmutableArray<byte> x)
		{
			return Hash.GetFNVHashCode(x);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
		bool IEqualityComparer<byte[]>.Equals(byte[] x, byte[] y)
		{
			return ByteSequenceComparer.Equals(x, y);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0001E701 File Offset: 0x0001C901
		int IEqualityComparer<byte[]>.GetHashCode(byte[] x)
		{
			return ByteSequenceComparer.GetHashCode(x);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001E709 File Offset: 0x0001C909
		bool IEqualityComparer<ImmutableArray<byte>>.Equals(ImmutableArray<byte> x, ImmutableArray<byte> y)
		{
			return ByteSequenceComparer.Equals(x, y);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001E712 File Offset: 0x0001C912
		int IEqualityComparer<ImmutableArray<byte>>.GetHashCode(ImmutableArray<byte> x)
		{
			return ByteSequenceComparer.GetHashCode(x);
		}

		// Token: 0x040008EC RID: 2284
		internal static readonly ByteSequenceComparer Instance = new ByteSequenceComparer();
	}
}
