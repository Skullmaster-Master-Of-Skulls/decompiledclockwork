using System;

namespace Renci.SshNet.Security.Cryptography
{
	// Token: 0x0200007D RID: 125
	public abstract class Cipher
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060006CA RID: 1738
		public abstract byte MinimumSize { get; }

		// Token: 0x060006CB RID: 1739 RVA: 0x000151E4 File Offset: 0x000133E4
		public byte[] Encrypt(byte[] input)
		{
			return this.Encrypt(input, 0, input.Length);
		}

		// Token: 0x060006CC RID: 1740
		public abstract byte[] Encrypt(byte[] input, int offset, int length);

		// Token: 0x060006CD RID: 1741
		public abstract byte[] Decrypt(byte[] input);

		// Token: 0x060006CE RID: 1742 RVA: 0x000151F1 File Offset: 0x000133F1
		protected static void UInt32ToBigEndian(uint number, byte[] buffer)
		{
			buffer[0] = (byte)(number >> 24);
			buffer[1] = (byte)(number >> 16);
			buffer[2] = (byte)(number >> 8);
			buffer[3] = (byte)number;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001520F File Offset: 0x0001340F
		protected static void UInt32ToBigEndian(uint number, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)(number >> 24);
			buffer[offset + 1] = (byte)(number >> 16);
			buffer[offset + 2] = (byte)(number >> 8);
			buffer[offset + 3] = (byte)number;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00015233 File Offset: 0x00013433
		protected static uint BigEndianToUInt32(byte[] buffer)
		{
			return (uint)((int)buffer[0] << 24 | (int)buffer[1] << 16 | (int)buffer[2] << 8 | (int)buffer[3]);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001524C File Offset: 0x0001344C
		protected static uint BigEndianToUInt32(byte[] buffer, int offset)
		{
			return (uint)((int)buffer[offset] << 24 | (int)buffer[offset + 1] << 16 | (int)buffer[offset + 2] << 8 | (int)buffer[offset + 3]);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001526C File Offset: 0x0001346C
		protected static ulong BigEndianToUInt64(byte[] buffer)
		{
			ulong num = (ulong)Cipher.BigEndianToUInt32(buffer);
			uint num2 = Cipher.BigEndianToUInt32(buffer, 4);
			return num << 32 | (ulong)num2;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00015290 File Offset: 0x00013490
		protected static ulong BigEndianToUInt64(byte[] buffer, int offset)
		{
			ulong num = (ulong)Cipher.BigEndianToUInt32(buffer, offset);
			uint num2 = Cipher.BigEndianToUInt32(buffer, offset + 4);
			return num << 32 | (ulong)num2;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000152B5 File Offset: 0x000134B5
		protected static void UInt64ToBigEndian(ulong number, byte[] buffer)
		{
			Cipher.UInt32ToBigEndian((uint)(number >> 32), buffer);
			Cipher.UInt32ToBigEndian((uint)number, buffer, 4);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000152CB File Offset: 0x000134CB
		protected static void UInt64ToBigEndian(ulong number, byte[] buffer, int offset)
		{
			Cipher.UInt32ToBigEndian((uint)(number >> 32), buffer, offset);
			Cipher.UInt32ToBigEndian((uint)number, buffer, offset + 4);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x000152E4 File Offset: 0x000134E4
		protected static void UInt32ToLittleEndian(uint number, byte[] buffer)
		{
			buffer[0] = (byte)number;
			buffer[1] = (byte)(number >> 8);
			buffer[2] = (byte)(number >> 16);
			buffer[3] = (byte)(number >> 24);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00015302 File Offset: 0x00013502
		protected static void UInt32ToLittleEndian(uint number, byte[] buffer, int offset)
		{
			buffer[offset] = (byte)number;
			buffer[offset + 1] = (byte)(number >> 8);
			buffer[offset + 2] = (byte)(number >> 16);
			buffer[offset + 3] = (byte)(number >> 24);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00015326 File Offset: 0x00013526
		protected static uint LittleEndianToUInt32(byte[] buffer)
		{
			return (uint)((int)buffer[0] | (int)buffer[1] << 8 | (int)buffer[2] << 16 | (int)buffer[3] << 24);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001533F File Offset: 0x0001353F
		protected static uint LittleEndianToUInt32(byte[] buffer, int offset)
		{
			return (uint)((int)buffer[offset] | (int)buffer[offset + 1] << 8 | (int)buffer[offset + 2] << 16 | (int)buffer[offset + 3] << 24);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00015360 File Offset: 0x00013560
		protected static ulong LittleEndianToUInt64(byte[] buffer)
		{
			uint num = Cipher.LittleEndianToUInt32(buffer);
			return (ulong)Cipher.LittleEndianToUInt32(buffer, 4) << 32 | (ulong)num;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00015384 File Offset: 0x00013584
		protected static ulong LittleEndianToUInt64(byte[] buffer, int offset)
		{
			uint num = Cipher.LittleEndianToUInt32(buffer, offset);
			return (ulong)Cipher.LittleEndianToUInt32(buffer, offset + 4) << 32 | (ulong)num;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000153A9 File Offset: 0x000135A9
		protected static void UInt64ToLittleEndian(ulong number, byte[] buffer)
		{
			Cipher.UInt32ToLittleEndian((uint)number, buffer);
			Cipher.UInt32ToLittleEndian((uint)(number >> 32), buffer, 4);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000153BF File Offset: 0x000135BF
		protected static void UInt64ToLittleEndian(ulong number, byte[] buffer, int offset)
		{
			Cipher.UInt32ToLittleEndian((uint)number, buffer, offset);
			Cipher.UInt32ToLittleEndian((uint)(number >> 32), buffer, offset + 4);
		}
	}
}
