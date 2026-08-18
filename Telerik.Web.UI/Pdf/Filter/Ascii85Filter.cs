using System;

namespace Telerik.Pdf.Filter
{
	// Token: 0x02001601 RID: 5633
	public class Ascii85Filter : IFilter
	{
		// Token: 0x17004338 RID: 17208
		// (get) Token: 0x0600DB91 RID: 56209 RVA: 0x003005AD File Offset: 0x002FE7AD
		public PdfObject Name
		{
			get
			{
				return PdfName.Names.ASCII85Decode;
			}
		}

		// Token: 0x17004339 RID: 17209
		// (get) Token: 0x0600DB92 RID: 56210 RVA: 0x003005B4 File Offset: 0x002FE7B4
		public PdfObject DecodeParms
		{
			get
			{
				return PdfNull.Null;
			}
		}

		// Token: 0x1700433A RID: 17210
		// (get) Token: 0x0600DB93 RID: 56211 RVA: 0x003005BB File Offset: 0x002FE7BB
		public bool HasDecodeParams
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600DB94 RID: 56212 RVA: 0x003005C0 File Offset: 0x002FE7C0
		public byte[] Encode(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			int num = data.Length;
			int num2 = num / 4;
			int num3 = num - num2 * 4;
			byte[] array = new byte[num2 * 5 + ((num3 == 0) ? 0 : (num3 + 1)) + 2];
			int num4 = 0;
			int num5 = 0;
			for (int i = 0; i < num2; i++)
			{
				uint num6 = (uint)(((int)data[num4++] << 24) + ((int)data[num4++] << 16) + ((int)data[num4++] << 8) + (int)data[num4++]);
				if (num6 == 0U)
				{
					array[num5++] = 122;
				}
				else
				{
					byte b = (byte)(num6 % 85U + 33U);
					num6 /= 85U;
					byte b2 = (byte)(num6 % 85U + 33U);
					num6 /= 85U;
					byte b3 = (byte)(num6 % 85U + 33U);
					num6 /= 85U;
					byte b4 = (byte)(num6 % 85U + 33U);
					num6 /= 85U;
					byte b5 = (byte)(num6 + 33U);
					array[num5++] = b5;
					array[num5++] = b4;
					array[num5++] = b3;
					array[num5++] = b2;
					array[num5++] = b;
				}
			}
			if (num3 == 1)
			{
				uint num7 = (uint)((uint)data[num4++] << 24);
				num7 /= 614125U;
				byte b6 = (byte)(num7 % 85U + 33U);
				num7 /= 85U;
				byte b7 = (byte)(num7 + 33U);
				array[num5++] = b7;
				array[num5++] = b6;
			}
			else if (num3 == 2)
			{
				uint num8 = (uint)(((int)data[num4++] << 24) + ((int)data[num4++] << 16));
				num8 /= 7225U;
				byte b8 = (byte)(num8 % 85U + 33U);
				num8 /= 85U;
				byte b9 = (byte)(num8 % 85U + 33U);
				num8 /= 85U;
				byte b10 = (byte)(num8 + 33U);
				array[num5++] = b10;
				array[num5++] = b9;
				array[num5++] = b8;
			}
			else if (num3 == 3)
			{
				uint num9 = (uint)(((int)data[num4++] << 24) + ((int)data[num4++] << 16) + ((int)data[num4++] << 8));
				num9 /= 85U;
				byte b11 = (byte)(num9 % 85U + 33U);
				num9 /= 85U;
				byte b12 = (byte)(num9 % 85U + 33U);
				num9 /= 85U;
				byte b13 = (byte)(num9 % 85U + 33U);
				num9 /= 85U;
				byte b14 = (byte)(num9 + 33U);
				array[num5++] = b14;
				array[num5++] = b13;
				array[num5++] = b12;
				array[num5++] = b11;
			}
			array[num5++] = 126;
			array[num5++] = 62;
			if (num5 < array.Length)
			{
				byte[] array2 = new byte[num5];
				Array.Copy(array, array2, num5);
				array = array2;
			}
			return array;
		}
	}
}
