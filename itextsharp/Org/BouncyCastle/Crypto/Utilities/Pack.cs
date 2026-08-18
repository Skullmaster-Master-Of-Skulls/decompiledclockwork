using System;

namespace Org.BouncyCastle.Crypto.Utilities
{
	// Token: 0x02000186 RID: 390
	internal sealed class Pack
	{
		// Token: 0x06000F37 RID: 3895 RVA: 0x000580D3 File Offset: 0x000570D3
		private Pack()
		{
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000580DB File Offset: 0x000570DB
		internal static void UInt32_To_BE(uint n, byte[] bs)
		{
			bs[0] = (byte)(n >> 24);
			bs[1] = (byte)(n >> 16);
			bs[2] = (byte)(n >> 8);
			bs[3] = (byte)n;
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000580F9 File Offset: 0x000570F9
		internal static void UInt32_To_BE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 24);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)n;
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00058128 File Offset: 0x00057128
		internal static uint BE_To_UInt32(byte[] bs)
		{
			uint num = (uint)((uint)bs[0] << 24);
			num |= (uint)((uint)bs[1] << 16);
			num |= (uint)((uint)bs[2] << 8);
			return num | (uint)bs[3];
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00058154 File Offset: 0x00057154
		internal static uint BE_To_UInt32(byte[] bs, int off)
		{
			uint num = (uint)((uint)bs[off] << 24);
			num |= (uint)((uint)bs[++off] << 16);
			num |= (uint)((uint)bs[++off] << 8);
			return num | (uint)bs[++off];
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00058190 File Offset: 0x00057190
		internal static ulong BE_To_UInt64(byte[] bs)
		{
			uint num = Pack.BE_To_UInt32(bs);
			uint num2 = Pack.BE_To_UInt32(bs, 4);
			return (ulong)num << 32 | (ulong)num2;
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000581B4 File Offset: 0x000571B4
		internal static ulong BE_To_UInt64(byte[] bs, int off)
		{
			uint num = Pack.BE_To_UInt32(bs, off);
			uint num2 = Pack.BE_To_UInt32(bs, off + 4);
			return (ulong)num << 32 | (ulong)num2;
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000581DB File Offset: 0x000571DB
		internal static void UInt64_To_BE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs);
			Pack.UInt32_To_BE((uint)n, bs, 4);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000581F1 File Offset: 0x000571F1
		internal static void UInt64_To_BE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_BE((uint)(n >> 32), bs, off);
			Pack.UInt32_To_BE((uint)n, bs, off + 4);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0005820A File Offset: 0x0005720A
		internal static void UInt32_To_LE(uint n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
			bs[2] = (byte)(n >> 16);
			bs[3] = (byte)(n >> 24);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00058228 File Offset: 0x00057228
		internal static void UInt32_To_LE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00058258 File Offset: 0x00057258
		internal static uint LE_To_UInt32(byte[] bs)
		{
			uint num = (uint)bs[0];
			num |= (uint)((uint)bs[1] << 8);
			num |= (uint)((uint)bs[2] << 16);
			return num | (uint)((uint)bs[3] << 24);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x00058284 File Offset: 0x00057284
		internal static uint LE_To_UInt32(byte[] bs, int off)
		{
			uint num = (uint)bs[off];
			num |= (uint)((uint)bs[++off] << 8);
			num |= (uint)((uint)bs[++off] << 16);
			return num | (uint)((uint)bs[++off] << 24);
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x000582C0 File Offset: 0x000572C0
		internal static ulong LE_To_UInt64(byte[] bs)
		{
			uint num = Pack.LE_To_UInt32(bs);
			uint num2 = Pack.LE_To_UInt32(bs, 4);
			return (ulong)num2 << 32 | (ulong)num;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x000582E4 File Offset: 0x000572E4
		internal static ulong LE_To_UInt64(byte[] bs, int off)
		{
			uint num = Pack.LE_To_UInt32(bs, off);
			uint num2 = Pack.LE_To_UInt32(bs, off + 4);
			return (ulong)num2 << 32 | (ulong)num;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0005830B File Offset: 0x0005730B
		internal static void UInt64_To_LE(ulong n, byte[] bs)
		{
			Pack.UInt32_To_LE((uint)n, bs);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, 4);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00058321 File Offset: 0x00057321
		internal static void UInt64_To_LE(ulong n, byte[] bs, int off)
		{
			Pack.UInt32_To_LE((uint)n, bs, off);
			Pack.UInt32_To_LE((uint)(n >> 32), bs, off + 4);
		}
	}
}
