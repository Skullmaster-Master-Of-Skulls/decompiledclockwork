using System;

namespace OracleInternal.Common
{
	// Token: 0x020000B4 RID: 180
	internal class RepConversion
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x00042414 File Offset: 0x00040614
		internal static byte nibbleToHex(byte nibble)
		{
			nibble &= 15;
			return (nibble < 10) ? (nibble + 48) : (nibble - 10 + 65);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00042430 File Offset: 0x00040630
		internal static void bArray2nibbles(byte[] array, byte[] nibbles)
		{
			for (int i = 0; i < array.Length; i++)
			{
				nibbles[i * 2] = RepConversion.nibbleToHex((byte)((array[i] & 240) >> 4));
				nibbles[i * 2 + 1] = RepConversion.nibbleToHex(array[i] & 15);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00042474 File Offset: 0x00040674
		public static byte[] ToBinArray(string hexStr)
		{
			byte[] array = new byte[hexStr.Length / 2];
			for (int i = 0; i < hexStr.Length / 2; i++)
			{
				byte b = Convert.ToByte(hexStr.Substring(2 * i, 1), 16);
				byte b2 = Convert.ToByte(hexStr.Substring(2 * i + 1, 1), 16);
				int num = (int)b2 | (int)b << 4;
				array[i] = (byte)num;
			}
			return array;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000424D8 File Offset: 0x000406D8
		internal static int LeftShiftFirstNibble(byte val)
		{
			return (int)(val & byte.MaxValue) << 24;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x000424E4 File Offset: 0x000406E4
		internal static int LeftShiftSecondNibble(byte val)
		{
			return (int)(val & byte.MaxValue) << 16;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x000424F0 File Offset: 0x000406F0
		internal static int LeftShiftThirdNibble(byte val)
		{
			return (int)(val & byte.MaxValue) << 8;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000424FC File Offset: 0x000406FC
		internal static int LeftShiftFourthNibble(byte val)
		{
			return (int)(val & byte.MaxValue);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00042508 File Offset: 0x00040708
		internal static byte RightShiftFirstNibble(int val)
		{
			return (byte)(val >> 24 & 255);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00042518 File Offset: 0x00040718
		internal static byte RightShiftSecondNibble(int val)
		{
			return (byte)(val >> 16 & 255);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00042528 File Offset: 0x00040728
		internal static byte RightShiftThirdNibble(int val)
		{
			return (byte)(val >> 8 & 255);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00042534 File Offset: 0x00040734
		internal static byte RightShiftFourthNibble(int val)
		{
			return (byte)(val & 255);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00042540 File Offset: 0x00040740
		internal static ushort GetRegHour(byte val)
		{
			return (ushort)((val & 127) << 6);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0004254C File Offset: 0x0004074C
		internal static byte GetRegMin(byte val)
		{
			return (byte)((val & 252) >> 2);
		}
	}
}
