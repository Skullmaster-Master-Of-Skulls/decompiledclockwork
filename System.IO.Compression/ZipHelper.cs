using System;

namespace System.IO.Compression
{
	// Token: 0x02000010 RID: 16
	internal static class ZipHelper
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004EEB File Offset: 0x000030EB
		internal static bool EndsWithDirChar(string test)
		{
			return Path.GetFileName(test) == "";
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004F00 File Offset: 0x00003100
		internal static bool RequiresUnicode(string test)
		{
			foreach (char c in test)
			{
				if (c > '\u007f')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00004F30 File Offset: 0x00003130
		internal static void ReadBytes(Stream stream, byte[] buffer, int bytesToRead)
		{
			int i = bytesToRead;
			int num = 0;
			while (i > 0)
			{
				int num2 = stream.Read(buffer, num, i);
				if (num2 == 0)
				{
					throw new IOException(Messages.UnexpectedEndOfStream);
				}
				num += num2;
				i -= num2;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004F68 File Offset: 0x00003168
		internal static DateTime DosTimeToDateTime(uint dateTime)
		{
			int year = (int)(1980U + (dateTime >> 25));
			int month = (int)(dateTime >> 21 & 15U);
			int day = (int)(dateTime >> 16 & 31U);
			int hour = (int)(dateTime >> 11 & 31U);
			int minute = (int)(dateTime >> 5 & 63U);
			int second = (int)((dateTime & 31U) * 2U);
			DateTime result;
			try
			{
				result = new DateTime(year, month, day, hour, minute, second, 0);
			}
			catch (ArgumentOutOfRangeException)
			{
				result = ZipHelper.InvalidDateIndicator;
			}
			catch (ArgumentException)
			{
				result = ZipHelper.InvalidDateIndicator;
			}
			return result;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004FEC File Offset: 0x000031EC
		internal static uint DateTimeToDosTime(DateTime dateTime)
		{
			int num = dateTime.Year - 1980 & 127;
			num = (num << 4) + dateTime.Month;
			num = (num << 5) + dateTime.Day;
			num = (num << 5) + dateTime.Hour;
			num = (num << 6) + dateTime.Minute;
			return (uint)((num << 5) + dateTime.Second / 2);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000504C File Offset: 0x0000324C
		internal static bool SeekBackwardsToSignature(Stream stream, uint signatureToFind)
		{
			int num = 0;
			uint num2 = 0U;
			byte[] array = new byte[32];
			bool flag = false;
			bool flag2 = false;
			while (!flag2 && !flag)
			{
				flag = ZipHelper.SeekBackwardsAndRead(stream, array, out num);
				while (num >= 0 && !flag2)
				{
					num2 = (num2 << 8 | (uint)array[num]);
					if (num2 == signatureToFind)
					{
						flag2 = true;
					}
					else
					{
						num--;
					}
				}
			}
			if (!flag2)
			{
				return false;
			}
			stream.Seek((long)num, SeekOrigin.Current);
			return true;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000050B0 File Offset: 0x000032B0
		internal static void AdvanceToPosition(this Stream stream, long position)
		{
			int num2;
			for (long num = position - stream.Position; num != 0L; num -= (long)num2)
			{
				int count = (num > 64L) ? 64 : ((int)num);
				num2 = stream.Read(new byte[64], 0, count);
				if (num2 == 0)
				{
					throw new IOException(Messages.UnexpectedEndOfStream);
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000050FC File Offset: 0x000032FC
		private static bool SeekBackwardsAndRead(Stream stream, byte[] buffer, out int bufferPointer)
		{
			if (stream.Position >= (long)buffer.Length)
			{
				stream.Seek((long)(-(long)buffer.Length), SeekOrigin.Current);
				ZipHelper.ReadBytes(stream, buffer, buffer.Length);
				stream.Seek((long)(-(long)buffer.Length), SeekOrigin.Current);
				bufferPointer = buffer.Length - 1;
				return false;
			}
			int num = (int)stream.Position;
			stream.Seek(0L, SeekOrigin.Begin);
			ZipHelper.ReadBytes(stream, buffer, num);
			stream.Seek(0L, SeekOrigin.Begin);
			bufferPointer = num - 1;
			return true;
		}

		// Token: 0x04000083 RID: 131
		internal const uint Mask32Bit = 4294967295U;

		// Token: 0x04000084 RID: 132
		internal const ushort Mask16Bit = 65535;

		// Token: 0x04000085 RID: 133
		private const int BackwardsSeekingBufferSize = 32;

		// Token: 0x04000086 RID: 134
		internal const int ValidZipDate_YearMin = 1980;

		// Token: 0x04000087 RID: 135
		internal const int ValidZipDate_YearMax = 2107;

		// Token: 0x04000088 RID: 136
		private static readonly DateTime InvalidDateIndicator = new DateTime(1980, 1, 1, 0, 0, 0);
	}
}
