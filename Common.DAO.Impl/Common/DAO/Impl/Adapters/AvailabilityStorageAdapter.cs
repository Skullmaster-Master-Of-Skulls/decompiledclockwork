using System;
using System.Collections;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Entity.AvailabilitySchedule;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.Common.DAO.Impl.Adapters
{
	// Token: 0x0200017A RID: 378
	public static class AvailabilityStorageAdapter
	{
		// Token: 0x06000B5A RID: 2906 RVA: 0x00078A18 File Offset: 0x00076C18
		public static AvailabilityTimeStorage ConvertTimespanRangesToCompressedTimes(this IList<Range<TimeSpan>> items)
		{
			BitArray bitArray = new BitArray(288);
			BitArray bitArray2 = new BitArray(288);
			foreach (Range<TimeSpan> range in items)
			{
				int num = Convert.ToInt32(range.Start.TotalMinutes / 5.0);
				int num2 = Convert.ToInt32(range.End.TotalMinutes / 5.0);
				bool flag = num2 >= 288;
				if (flag)
				{
					num2 = 287;
				}
				for (int i = num; i <= num2; i++)
				{
					bitArray[i] = true;
				}
				bitArray2[num2] = true;
			}
			return new AvailabilityTimeStorage
			{
				AvailabilityBytes = bitArray.ConvertBitArrayToByteArray(),
				AvailabilityBoundariesBytes = bitArray2.ConvertBitArrayToByteArray()
			};
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00078B28 File Offset: 0x00076D28
		private static byte[] ConvertBitArrayToByteArray(this BitArray bitArray)
		{
			byte[] array = new byte[bitArray.Length / 8];
			for (int i = 0; i < array.Length; i++)
			{
				byte b = 0;
				for (int j = 0; j < 8; j++)
				{
					bool flag = bitArray[i * 8 + j];
					if (flag)
					{
						b |= AvailabilityStorageAdapter.TwoPowers[j];
					}
				}
				array[i] = b;
			}
			return array;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00078B98 File Offset: 0x00076D98
		public static IList<Range<TimeSpan>> ConvertCompressedTimesToTimespanRanges(this AvailabilityTimeStorage availability)
		{
			List<Range<TimeSpan>> list = new List<Range<TimeSpan>>();
			bool flag = availability == null;
			IList<Range<TimeSpan>> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				byte[] availabilityBytes = availability.AvailabilityBytes;
				byte[] availabilityBoundariesBytes = availability.AvailabilityBoundariesBytes;
				TimeSpan? timeSpan = null;
				for (int i = 0; i < 36; i++)
				{
					bool flag2 = i >= availabilityBytes.Length;
					if (flag2)
					{
						break;
					}
					byte b = availabilityBytes[i];
					int j = 0;
					while (j < 8)
					{
						bool flag3 = ((int)b & (int)Math.Pow(2.0, (double)j)) > 0;
						bool flag4 = flag3;
						if (flag4)
						{
							bool flag5 = timeSpan == null;
							if (flag5)
							{
								timeSpan = new TimeSpan?(TimeSpan.FromMinutes((double)((i * 8 + j) * 5)));
							}
							else
							{
								bool flag6 = availabilityBoundariesBytes != null;
								if (flag6)
								{
									byte b2 = availabilityBoundariesBytes[i];
									bool flag7 = ((int)b2 & (int)Math.Pow(2.0, (double)j)) > 0;
									bool flag8 = !flag7;
									if (!flag8)
									{
										TimeSpan timeSpan2 = TimeSpan.FromMinutes((double)((i * 8 + j) * 5));
										list.Add(new Range<TimeSpan>(timeSpan.Value, timeSpan2));
										timeSpan = new TimeSpan?(timeSpan2);
									}
								}
							}
						}
						else
						{
							bool flag9 = timeSpan != null;
							if (flag9)
							{
								TimeSpan timeSpan3 = TimeSpan.FromMinutes((double)((i * 8 + (j - 1)) * 5));
								bool flag10 = timeSpan3 != timeSpan.Value;
								if (flag10)
								{
									list.Add(new Range<TimeSpan>(timeSpan.Value, timeSpan3));
								}
								timeSpan = null;
							}
						}
						IL_16C:
						j++;
						continue;
						goto IL_16C;
					}
				}
				bool flag11 = timeSpan != null;
				if (flag11)
				{
					TimeSpan end = TimeSpan.FromDays(1.0).Add(-TimeSpan.FromMinutes(1.0));
					bool flag12 = list.Count < 1;
					if (flag12)
					{
						list.Add(new Range<TimeSpan>(timeSpan.Value, end));
					}
					else
					{
						list[list.Count - 1].End = end;
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0400071C RID: 1820
		private const int MinsPerBit = 5;

		// Token: 0x0400071D RID: 1821
		private const int MinsInDay = 1440;

		// Token: 0x0400071E RID: 1822
		private const int ByteLen = 36;

		// Token: 0x0400071F RID: 1823
		private static readonly byte[] TwoPowers = new byte[]
		{
			1,
			2,
			4,
			8,
			16,
			32,
			64,
			128
		};
	}
}
