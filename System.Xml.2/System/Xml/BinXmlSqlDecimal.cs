using System;
using System.Diagnostics;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000124 RID: 292
	internal struct BinXmlSqlDecimal
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x000541D5 File Offset: 0x000523D5
		public bool IsPositive
		{
			get
			{
				return this.m_bSign == 0;
			}
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000541E0 File Offset: 0x000523E0
		public BinXmlSqlDecimal(byte[] data, int offset, bool trim)
		{
			byte b = data[offset];
			if (b <= 11)
			{
				if (b == 7)
				{
					this.m_bLen = 1;
					goto IL_50;
				}
				if (b == 11)
				{
					this.m_bLen = 2;
					goto IL_50;
				}
			}
			else
			{
				if (b == 15)
				{
					this.m_bLen = 3;
					goto IL_50;
				}
				if (b == 19)
				{
					this.m_bLen = 4;
					goto IL_50;
				}
			}
			throw new XmlException("XmlBinary_InvalidSqlDecimal", null);
			IL_50:
			this.m_bPrec = data[offset + 1];
			this.m_bScale = data[offset + 2];
			this.m_bSign = ((data[offset + 3] == 0) ? 1 : 0);
			this.m_data1 = BinXmlSqlDecimal.UIntFromByteArray(data, offset + 4);
			this.m_data2 = ((this.m_bLen > 1) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 8) : 0U);
			this.m_data3 = ((this.m_bLen > 2) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 12) : 0U);
			this.m_data4 = ((this.m_bLen > 3) ? BinXmlSqlDecimal.UIntFromByteArray(data, offset + 16) : 0U);
			if (this.m_bLen == 4 && this.m_data4 == 0U)
			{
				this.m_bLen = 3;
			}
			if (this.m_bLen == 3 && this.m_data3 == 0U)
			{
				this.m_bLen = 2;
			}
			if (this.m_bLen == 2 && this.m_data2 == 0U)
			{
				this.m_bLen = 1;
			}
			if (trim)
			{
				this.TrimTrailingZeros();
			}
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00054318 File Offset: 0x00052518
		public void Write(Stream strm)
		{
			strm.WriteByte(this.m_bLen * 4 + 3);
			strm.WriteByte(this.m_bPrec);
			strm.WriteByte(this.m_bScale);
			strm.WriteByte((this.m_bSign == 0) ? 1 : 0);
			this.WriteUI4(this.m_data1, strm);
			if (this.m_bLen > 1)
			{
				this.WriteUI4(this.m_data2, strm);
				if (this.m_bLen > 2)
				{
					this.WriteUI4(this.m_data3, strm);
					if (this.m_bLen > 3)
					{
						this.WriteUI4(this.m_data4, strm);
					}
				}
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000543B0 File Offset: 0x000525B0
		private void WriteUI4(uint val, Stream strm)
		{
			strm.WriteByte((byte)(val & 255U));
			strm.WriteByte((byte)(val >> 8 & 255U));
			strm.WriteByte((byte)(val >> 16 & 255U));
			strm.WriteByte((byte)(val >> 24 & 255U));
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00054400 File Offset: 0x00052600
		private static uint UIntFromByteArray(byte[] data, int offset)
		{
			int num = (int)data[offset];
			num |= (int)data[offset + 1] << 8;
			num |= (int)data[offset + 2] << 16;
			return (uint)(num | (int)data[offset + 3] << 24);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00054432 File Offset: 0x00052632
		private bool FZero()
		{
			return this.m_data1 == 0U && this.m_bLen <= 1;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0005444A File Offset: 0x0005264A
		private void StoreFromWorkingArray(uint[] rguiData)
		{
			this.m_data1 = rguiData[0];
			this.m_data2 = rguiData[1];
			this.m_data3 = rguiData[2];
			this.m_data4 = rguiData[3];
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x00054470 File Offset: 0x00052670
		private bool FGt10_38(uint[] rglData)
		{
			return (ulong)rglData[3] >= 1262177448UL && ((ulong)rglData[3] > 1262177448UL || (ulong)rglData[2] > 1518781562UL || ((ulong)rglData[2] == 1518781562UL && (ulong)rglData[1] >= 160047680UL));
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x000544C4 File Offset: 0x000526C4
		private static void MpDiv1(uint[] rgulU, ref int ciulU, uint iulD, out uint iulR)
		{
			uint num = 0U;
			ulong num2 = (ulong)iulD;
			int i = ciulU;
			while (i > 0)
			{
				i--;
				ulong num3 = ((ulong)num << 32) + (ulong)rgulU[i];
				rgulU[i] = (uint)(num3 / num2);
				num = (uint)(num3 - (ulong)rgulU[i] * num2);
			}
			iulR = num;
			BinXmlSqlDecimal.MpNormalize(rgulU, ref ciulU);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00054509 File Offset: 0x00052709
		private static void MpNormalize(uint[] rgulU, ref int ciulU)
		{
			while (ciulU > 1 && rgulU[ciulU - 1] == 0U)
			{
				ciulU--;
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00054520 File Offset: 0x00052720
		internal void AdjustScale(int digits, bool fRound)
		{
			bool flag = false;
			int i = digits;
			if (i + (int)this.m_bScale < 0)
			{
				throw new XmlException("SqlTypes_ArithTruncation", null);
			}
			if (i + (int)this.m_bScale > (int)BinXmlSqlDecimal.NUMERIC_MAX_PRECISION)
			{
				throw new XmlException("SqlTypes_ArithOverflow", null);
			}
			byte bScale = (byte)(i + (int)this.m_bScale);
			byte bPrec = (byte)Math.Min((int)BinXmlSqlDecimal.NUMERIC_MAX_PRECISION, Math.Max(1, i + (int)this.m_bPrec));
			if (i > 0)
			{
				this.m_bScale = bScale;
				this.m_bPrec = bPrec;
				while (i > 0)
				{
					uint num;
					if (i >= 9)
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[8];
						i -= 9;
					}
					else
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[i - 1];
						i = 0;
					}
					this.MultByULong(num);
				}
			}
			else if (i < 0)
			{
				uint num;
				uint num2;
				do
				{
					if (i <= -9)
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[8];
						i += 9;
					}
					else
					{
						num = BinXmlSqlDecimal.x_rgulShiftBase[-i - 1];
						i = 0;
					}
					num2 = this.DivByULong(num);
				}
				while (i < 0);
				flag = (num2 >= num / 2U);
				this.m_bScale = bScale;
				this.m_bPrec = bPrec;
			}
			if (flag && fRound)
			{
				this.AddULong(1U);
				return;
			}
			if (this.FZero())
			{
				this.m_bSign = 0;
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x00054644 File Offset: 0x00052844
		private void AddULong(uint ulAdd)
		{
			ulong num = (ulong)ulAdd;
			int bLen = (int)this.m_bLen;
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			int num2 = 0;
			for (;;)
			{
				num += (ulong)array[num2];
				array[num2] = (uint)num;
				num >>= 32;
				if (num == 0UL)
				{
					break;
				}
				num2++;
				if (num2 >= bLen)
				{
					goto Block_2;
				}
			}
			this.StoreFromWorkingArray(array);
			return;
			Block_2:
			if (num2 == BinXmlSqlDecimal.x_cNumeMax)
			{
				throw new XmlException("SqlTypes_ArithOverflow", null);
			}
			array[num2] = (uint)num;
			this.m_bLen += 1;
			if (this.FGt10_38(array))
			{
				throw new XmlException("SqlTypes_ArithOverflow", null);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000546F0 File Offset: 0x000528F0
		private void MultByULong(uint uiMultiplier)
		{
			int bLen = (int)this.m_bLen;
			ulong num = 0UL;
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			for (int i = 0; i < bLen; i++)
			{
				ulong num2 = (ulong)array[i];
				ulong num3 = num2 * (ulong)uiMultiplier;
				num += num3;
				if (num < num3)
				{
					num3 = BinXmlSqlDecimal.x_ulInt32Base;
				}
				else
				{
					num3 = 0UL;
				}
				array[i] = (uint)num;
				num = (num >> 32) + num3;
			}
			if (num != 0UL)
			{
				if (bLen == BinXmlSqlDecimal.x_cNumeMax)
				{
					throw new XmlException("SqlTypes_ArithOverflow", null);
				}
				array[bLen] = (uint)num;
				this.m_bLen += 1;
			}
			if (this.FGt10_38(array))
			{
				throw new XmlException("SqlTypes_ArithOverflow", null);
			}
			this.StoreFromWorkingArray(array);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000547BC File Offset: 0x000529BC
		internal uint DivByULong(uint iDivisor)
		{
			ulong num = (ulong)iDivisor;
			ulong num2 = 0UL;
			bool flag = true;
			if (num == 0UL)
			{
				throw new XmlException("SqlTypes_DivideByZero", null);
			}
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			for (int i = (int)this.m_bLen; i > 0; i--)
			{
				num2 = (num2 << 32) + (ulong)array[i - 1];
				uint num3 = (uint)(num2 / num);
				array[i - 1] = num3;
				num2 %= num;
				flag = (flag && num3 == 0U);
				if (flag)
				{
					this.m_bLen -= 1;
				}
			}
			this.StoreFromWorkingArray(array);
			if (flag)
			{
				this.m_bLen = 1;
			}
			return (uint)num2;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00054873 File Offset: 0x00052A73
		private static byte CLenFromPrec(byte bPrec)
		{
			return BinXmlSqlDecimal.rgCLenFromPrec[(int)(bPrec - 1)];
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0005487E File Offset: 0x00052A7E
		private static char ChFromDigit(uint uiDigit)
		{
			return (char)(uiDigit + 48U);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00054888 File Offset: 0x00052A88
		public decimal ToDecimal()
		{
			if (this.m_data4 != 0U || this.m_bScale > 28)
			{
				throw new XmlException("SqlTypes_ArithOverflow", null);
			}
			return new decimal((int)this.m_data1, (int)this.m_data2, (int)this.m_data3, !this.IsPositive, this.m_bScale);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x000548DC File Offset: 0x00052ADC
		private void TrimTrailingZeros()
		{
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			int bLen = (int)this.m_bLen;
			if (bLen == 1 && array[0] == 0U)
			{
				this.m_bScale = 0;
				return;
			}
			while (this.m_bScale > 0 && (bLen > 1 || array[0] != 0U))
			{
				uint num;
				BinXmlSqlDecimal.MpDiv1(array, ref bLen, 10U, out num);
				if (num != 0U)
				{
					break;
				}
				this.m_data1 = array[0];
				this.m_data2 = array[1];
				this.m_data3 = array[2];
				this.m_data4 = array[3];
				this.m_bScale -= 1;
			}
			if (this.m_bLen == 4 && this.m_data4 == 0U)
			{
				this.m_bLen = 3;
			}
			if (this.m_bLen == 3 && this.m_data3 == 0U)
			{
				this.m_bLen = 2;
			}
			if (this.m_bLen == 2 && this.m_data2 == 0U)
			{
				this.m_bLen = 1;
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000549C8 File Offset: 0x00052BC8
		public override string ToString()
		{
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			int bLen = (int)this.m_bLen;
			char[] array2 = new char[(int)(BinXmlSqlDecimal.NUMERIC_MAX_PRECISION + 1)];
			int i = 0;
			while (bLen > 1 || array[0] != 0U)
			{
				uint uiDigit;
				BinXmlSqlDecimal.MpDiv1(array, ref bLen, 10U, out uiDigit);
				array2[i++] = BinXmlSqlDecimal.ChFromDigit(uiDigit);
			}
			while (i <= (int)this.m_bScale)
			{
				array2[i++] = BinXmlSqlDecimal.ChFromDigit(0U);
			}
			bool isPositive = this.IsPositive;
			int num = isPositive ? i : (i + 1);
			if (this.m_bScale > 0)
			{
				num++;
			}
			char[] array3 = new char[num];
			int num2 = 0;
			if (!isPositive)
			{
				array3[num2++] = '-';
			}
			while (i > 0)
			{
				if (i-- == (int)this.m_bScale)
				{
					array3[num2++] = '.';
				}
				array3[num2++] = array2[i];
			}
			return new string(array3);
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x00054AC8 File Offset: 0x00052CC8
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			uint[] array = new uint[]
			{
				this.m_data1,
				this.m_data2,
				this.m_data3,
				this.m_data4
			};
			uint num = array[(int)(this.m_bLen - 1)];
			for (int i = (int)this.m_bLen; i < BinXmlSqlDecimal.x_cNumeMax; i++)
			{
			}
		}

		// Token: 0x040005D3 RID: 1491
		internal byte m_bLen;

		// Token: 0x040005D4 RID: 1492
		internal byte m_bPrec;

		// Token: 0x040005D5 RID: 1493
		internal byte m_bScale;

		// Token: 0x040005D6 RID: 1494
		internal byte m_bSign;

		// Token: 0x040005D7 RID: 1495
		internal uint m_data1;

		// Token: 0x040005D8 RID: 1496
		internal uint m_data2;

		// Token: 0x040005D9 RID: 1497
		internal uint m_data3;

		// Token: 0x040005DA RID: 1498
		internal uint m_data4;

		// Token: 0x040005DB RID: 1499
		private static readonly byte NUMERIC_MAX_PRECISION = 38;

		// Token: 0x040005DC RID: 1500
		private static readonly byte MaxPrecision = BinXmlSqlDecimal.NUMERIC_MAX_PRECISION;

		// Token: 0x040005DD RID: 1501
		private static readonly byte MaxScale = BinXmlSqlDecimal.NUMERIC_MAX_PRECISION;

		// Token: 0x040005DE RID: 1502
		private static readonly int x_cNumeMax = 4;

		// Token: 0x040005DF RID: 1503
		private static readonly long x_lInt32Base = 4294967296L;

		// Token: 0x040005E0 RID: 1504
		private static readonly ulong x_ulInt32Base = 4294967296UL;

		// Token: 0x040005E1 RID: 1505
		private static readonly ulong x_ulInt32BaseForMod = BinXmlSqlDecimal.x_ulInt32Base - 1UL;

		// Token: 0x040005E2 RID: 1506
		internal static readonly ulong x_llMax = 9223372036854775807UL;

		// Token: 0x040005E3 RID: 1507
		private static readonly double DUINT_BASE = (double)BinXmlSqlDecimal.x_lInt32Base;

		// Token: 0x040005E4 RID: 1508
		private static readonly double DUINT_BASE2 = BinXmlSqlDecimal.DUINT_BASE * BinXmlSqlDecimal.DUINT_BASE;

		// Token: 0x040005E5 RID: 1509
		private static readonly double DUINT_BASE3 = BinXmlSqlDecimal.DUINT_BASE2 * BinXmlSqlDecimal.DUINT_BASE;

		// Token: 0x040005E6 RID: 1510
		private static readonly uint[] x_rgulShiftBase = new uint[]
		{
			10U,
			100U,
			1000U,
			10000U,
			100000U,
			1000000U,
			10000000U,
			100000000U,
			1000000000U
		};

		// Token: 0x040005E7 RID: 1511
		private static readonly byte[] rgCLenFromPrec = new byte[]
		{
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4,
			4
		};
	}
}
