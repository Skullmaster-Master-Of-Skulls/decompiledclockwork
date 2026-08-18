using System;
using System.Collections;
using System.IO;
using System.Text;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000AE2 RID: 2786
	internal sealed class SST : BaseBiffRecord, IRecord
	{
		// Token: 0x060068CD RID: 26829 RVA: 0x00188E04 File Offset: 0x00187004
		public SST(SSTHelper sstHelper) : base(252)
		{
			this.cstTotal = sstHelper.TotalStringCount;
			this.rgb = sstHelper.StringList;
			this.cstUnique = (uint)this.rgb.Count;
			this.listOfIndexes = sstHelper.ListOfIndexes;
			if (sstHelper.TotalStringLength + 8 + 4 > 8227)
			{
				base.Length = (ushort)(sstHelper.SSTStringLength + 8);
			}
			else
			{
				base.Length = (ushort)(sstHelper.TotalStringLength + 8);
			}
			this.totalBufferSize = sstHelper.TotalStringLength + 8 + this.listOfIndexes.Count * 4;
		}

		// Token: 0x060068CE RID: 26830 RVA: 0x00188E9E File Offset: 0x0018709E
		public byte[] GetData()
		{
			return null;
		}

		// Token: 0x060068CF RID: 26831 RVA: 0x00188EA4 File Offset: 0x001870A4
		public byte[] GetHeaderData()
		{
			int num = 0;
			byte[] array = new byte[12];
			byte[] bytes = BitConverter.GetBytes(base.RecordType);
			bytes.CopyTo(array, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(base.Length);
			bytes.CopyTo(array, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.cstTotal);
			bytes.CopyTo(array, num);
			num += bytes.Length;
			bytes = BitConverter.GetBytes(this.cstUnique);
			bytes.CopyTo(array, num);
			num += bytes.Length;
			return array;
		}

		// Token: 0x060068D0 RID: 26832 RVA: 0x00188F28 File Offset: 0x00187128
		public INSTINF[] WriteRecordAndGetOffsets(Stream stream, uint sSTAddress)
		{
			bool flag = true;
			INSTINF[] array = null;
			ushort num = 12;
			ushort num2 = 0;
			int num3 = 0;
			int num4 = 0;
			byte[] array2 = this.GetHeaderData();
			stream.Write(array2, 0, array2.Length);
			if (this.rgb != null)
			{
				byte[] array3 = new byte[this.totalBufferSize - array2.Length];
				int num5 = 0;
				int num6 = 0;
				array = new INSTINF[this.cstUnique / 8U + 1U];
				array[0].cb = num;
				UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
				ushort num7 = 4110;
				ushort num8 = 4111;
				foreach (object obj in this.listOfIndexes)
				{
					RecordSize recordSize = (RecordSize)obj;
					array2 = new byte[recordSize.Length];
					int num9 = 0;
					int num10 = num6;
					while (num10 < recordSize.Index || recordSize.CharIndex > 0)
					{
						if (recordSize.CharIndex > 0)
						{
							num10--;
						}
						string text = (string)this.rgb[num10];
						if (recordSize.CharIndex == 0)
						{
							ushort num11 = (ushort)text.Length;
							BitConverter.GetBytes(num11).CopyTo(array2, num9);
							num9 += 2;
							array2[num9] = 1;
							num9++;
							if (num11 > num7)
							{
								text = text.Substring(0, (int)num7);
								num11 = num7;
							}
							byte[] bytes = unicodeEncoding.GetBytes(text);
							bytes.CopyTo(array2, num9);
							num9 += bytes.Length;
							num2 += num11 * 2 + 3;
							num3++;
							if (num3 % 8 == 0)
							{
								num4++;
								array[num4].cb = (ushort)((long)(num2 + num) + (long)((ulong)sSTAddress));
								array[num4].ib = 0U;
								array[num4].reserved = 0;
								num += num2;
							}
						}
						else
						{
							array2[num9] = 1;
							num9++;
							ushort num11 = (ushort)(text.Length - recordSize.CharIndex);
							if (num11 > num8)
							{
								text = text.Substring(recordSize.CharIndex, (int)num8);
								num11 = num8;
							}
							else
							{
								text = text.Substring(recordSize.CharIndex);
							}
							byte[] bytes = unicodeEncoding.GetBytes(text);
							bytes.CopyTo(array2, num9);
							num9 += bytes.Length;
							num2 += num11 * 2 + 1;
							recordSize.CharIndex = 0;
						}
						num10++;
					}
					if (flag)
					{
						Array.Copy(array2, 0, array3, 0, array2.Length);
						num5 += array2.Length;
					}
					else
					{
						byte[] data = new Continue(array2).GetData();
						Array.Copy(data, 0, array3, num5, data.Length);
						num5 += data.Length;
					}
					num6 = recordSize.Index;
					flag = false;
				}
				stream.Write(array3, 0, array3.Length);
			}
			return array;
		}

		// Token: 0x04001C00 RID: 7168
		private const ushort type = 252;

		// Token: 0x04001C01 RID: 7169
		internal const int MaxBaseLength = 4;

		// Token: 0x04001C02 RID: 7170
		private const int MaxBucketSize = 8;

		// Token: 0x04001C03 RID: 7171
		internal const int MaxRecordLength = 8227;

		// Token: 0x04001C04 RID: 7172
		internal const int MaxUnicodeHeader = 3;

		// Token: 0x04001C05 RID: 7173
		private uint cstTotal;

		// Token: 0x04001C06 RID: 7174
		private uint cstUnique;

		// Token: 0x04001C07 RID: 7175
		private ArrayList rgb;

		// Token: 0x04001C08 RID: 7176
		private ArrayList listOfIndexes;

		// Token: 0x04001C09 RID: 7177
		private int totalBufferSize;
	}
}
