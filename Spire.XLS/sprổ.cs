using System;
using System.Globalization;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000323 RID: 803
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.RK)]
internal class sprỔ : spr\u22C6, spr\u2230, spr\u1929
{
	// Token: 0x06003181 RID: 12673 RVA: 0x001CA80C File Offset: 0x001C980C
	public int ᜄ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜇ;
	}

	// Token: 0x06003182 RID: 12674 RVA: 0x001CA850 File Offset: 0x001C9850
	public void ᜄ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜇ = A_0;
		this.ᜈ = ((A_0 & 1) != 0);
		this.ᜉ = ((A_0 & 2) != 0);
	}

	// Token: 0x06003183 RID: 12675 RVA: 0x001CA8B0 File Offset: 0x001C98B0
	public new double ᜀ()
	{
		double num3;
		double num4;
		for (;;)
		{
			IL_3C:
			long num = (long)(this.ᜇ >> 2);
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_73;
					case 1:
						return num3;
					case 2:
						num3 = (double)num;
						num2 = 4;
						continue;
					case 3:
						if (this.ᜃ())
						{
							num2 = 2;
							continue;
						}
						num4 = spr\u2620.ᜀ(num << 34);
						num2 = 0;
						continue;
					case 4:
						if (!this.ᜆ())
						{
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						goto IL_88;
					case 5:
						return num4;
					}
					goto IL_3C;
				}
				IL_73:
				if (this.ᜆ())
				{
					goto IL_CD;
				}
				num2 = 5;
			}
		}
		return num4;
		IL_88:
		return num3 / 100.0;
		IL_CD:
		return num4 / 100.0;
	}

	// Token: 0x06003184 RID: 12676 RVA: 0x001CA998 File Offset: 0x001C9998
	public new void ᜁ(double A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜂ(A_0);
	}

	// Token: 0x06003185 RID: 12677 RVA: 0x001CA9DC File Offset: 0x001C99DC
	public virtual int ᜉ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return 10;
	}

	// Token: 0x06003186 RID: 12678 RVA: 0x001CAA1C File Offset: 0x001C9A1C
	public virtual int ᜂ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return 10;
	}

	// Token: 0x06003187 RID: 12679 RVA: 0x001CAA5C File Offset: 0x001C9A5C
	public virtual int ᜁ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return 10;
	}

	// Token: 0x06003188 RID: 12680 RVA: 0x001CAA9C File Offset: 0x001C9A9C
	public new bool ᜃ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜉ;
	}

	// Token: 0x06003189 RID: 12681 RVA: 0x001CAAE0 File Offset: 0x001C9AE0
	public new void ᜀ(bool A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜉ = A_0;
	}

	// Token: 0x0600318A RID: 12682 RVA: 0x001CAB24 File Offset: 0x001C9B24
	public bool ᜆ()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return this.ᜈ;
	}

	// Token: 0x0600318B RID: 12683 RVA: 0x001CAB68 File Offset: 0x001C9B68
	public new void ᜁ(bool A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜈ = A_0;
	}

	// Token: 0x0600318D RID: 12685 RVA: 0x001CABC0 File Offset: 0x001C9BC0
	protected override void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜇ = A_0.ReadInt32(A_1);
		this.ᜉ = A_0.ReadBit(A_1, 1);
		this.ᜈ = A_0.ReadBit(A_1, 0);
	}

	// Token: 0x0600318E RID: 12686 RVA: 0x001CAC24 File Offset: 0x001C9C24
	protected override void ᜁ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.WriteInt32(A_1, this.ᜇ);
		A_0.WriteBit(A_1, this.ᜉ, 1);
		A_0.WriteBit(A_1, this.ᜈ, 0);
	}

	// Token: 0x0600318F RID: 12687 RVA: 0x001CAC88 File Offset: 0x001C9C88
	public override int ᜀ(ExcelVersion A_0)
	{
		int num;
		for (;;)
		{
			num = 10;
			if (true)
			{
			}
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (A_0 != ExcelVersion.Version97to2003)
					{
						num2 = 1;
						continue;
					}
					return num;
				case 1:
					for (;;)
					{
						num += 4;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_4E;
						}
					}
					IL_4E:
					if (false)
					{
					}
					num2 = 2;
					continue;
				case 2:
					return num;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x06003190 RID: 12688 RVA: 0x001CAD00 File Offset: 0x001C9D00
	public new void ᜁ(string A_0)
	{
		int num = 2;
		for (;;)
		{
			double a_;
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (true)
				{
				}
				this.ᜂ(a_);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			if (!double.TryParse(A_0, NumberStyles.Any, null, out a_))
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06003191 RID: 12689 RVA: 0x001CAD84 File Offset: 0x001C9D84
	public new void ᜂ(double A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜇ = sprỔ.ᜀ(A_0);
		this.ᜈ = ((this.ᜇ & 1) != 0);
		this.ᜉ = ((this.ᜇ & 2) != 0);
	}

	// Token: 0x06003192 RID: 12690 RVA: 0x001CADF4 File Offset: 0x001C9DF4
	public void ᜅ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		this.ᜇ = A_0;
		this.ᜈ = ((this.ᜇ & 1) != 0);
		this.ᜉ = ((this.ᜇ & 2) != 0);
	}

	// Token: 0x06003193 RID: 12691 RVA: 0x001CAE60 File Offset: 0x001C9E60
	public new void ᜀ(sprᨾ.ᜀ A_0)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜂ = A_0.ᜀ();
		this.ᜇ = A_0.ᜁ();
		this.ᜉ = ((this.ᜇ & 2) == 2);
		this.ᜈ = ((this.ᜇ & 1) == 1);
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x001CAED8 File Offset: 0x001C9ED8
	public sprᨾ.ᜀ ᜅ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜇ |= 1;
				num = 1;
				continue;
			case 1:
				goto IL_78;
			case 2:
				if (this.ᜉ)
				{
					num = 5;
					continue;
				}
				goto IL_AD;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_70;
				}
				break;
			case 5:
				this.ᜇ |= 2;
				num = 4;
				continue;
			}
			if (this.ᜈ)
			{
				num = 0;
				continue;
			}
			IL_78:
			num = 2;
		}
		IL_70:
		if (false)
		{
		}
		IL_AD:
		if (true)
		{
		}
		return new sprᨾ.ᜀ(this.ᜂ, this.ᜇ);
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x001CAFAC File Offset: 0x001C9FAC
	public new static int ᜀ(string A_0)
	{
		int a_ = 7;
		int num = 3;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				goto IL_3C;
			case 2:
			{
				double a_2;
				if (double.TryParse(A_0, NumberStyles.Any, null, out a_2))
				{
					num = 0;
					continue;
				}
				return int.MaxValue;
			}
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 2;
			}
		}
		IL_3C:
		throw new ArgumentNullException(RecordTableEnumerator.b("䬼帾ⵀ㙂⁄", a_));
		IL_6F:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_3C;
		default:
		{
			if (false)
			{
			}
			double a_2;
			return sprỔ.ᜀ(a_2);
		}
		}
		return int.MaxValue;
	}

	// Token: 0x06003196 RID: 12694 RVA: 0x001CB060 File Offset: 0x001CA060
	public new static int ᜀ(double A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 18;
			int num5;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					A_0 *= 100.0;
					long num2 = spr\u2620.ᜀ(A_0);
					num = 5;
					continue;
				}
				case 1:
				{
					int num3 = (int)Math.Round(A_0, 0);
					num = 7;
					continue;
				}
				case 2:
				{
					int num4;
					if (num4 <= 1073741823)
					{
						num = 8;
						continue;
					}
					goto IL_1F7;
				}
				case 3:
				{
					int num4;
					if (A_0 - (double)num4 == 0.0)
					{
						num = 23;
						continue;
					}
					goto IL_1F7;
				}
				case 4:
					goto IL_372;
				case 5:
				{
					long num2;
					if ((num2 & 17179869183L) == 0L)
					{
						num = 11;
						continue;
					}
					goto IL_1D7;
				}
				case 6:
				{
					int num3;
					if (num3 > 0)
					{
						num = 22;
						continue;
					}
					goto IL_217;
				}
				case 7:
				{
					int num3;
					if (A_0 - (double)num3 == 0.0)
					{
						num = 31;
						continue;
					}
					goto IL_217;
				}
				case 8:
				{
					int num4;
					num5 = num4 << 2;
					num5 |= 3;
					num = 27;
					continue;
				}
				case 9:
				{
					long num2;
					num5 = sprỔ.ᜀ(num2, false);
					bool flag = false;
					num = 30;
					continue;
				}
				case 10:
				{
					int num3;
					num5 = num3 << 2;
					num5 |= 2;
					bool flag = false;
					num = 4;
					continue;
				}
				case 11:
				{
					long num2;
					num5 = sprỔ.ᜀ(num2, true);
					bool flag = false;
					num = 12;
					continue;
				}
				case 12:
					goto IL_1D7;
				case 13:
					num = 21;
					continue;
				case 14:
				{
					int num4 = (int)Math.Round(A_0, 0);
					num = 3;
					continue;
				}
				case 15:
					return int.MaxValue;
				case 16:
				{
					bool flag;
					if (!flag)
					{
						num = 19;
						continue;
					}
					return int.MaxValue;
				}
				case 17:
				{
					bool flag;
					if (flag)
					{
						num = 14;
						continue;
					}
					goto IL_1F7;
				}
				case 19:
					goto IL_215;
				case 20:
				{
					int num3;
					if (num3 <= 1073741823)
					{
						num = 10;
						continue;
					}
					goto IL_217;
				}
				case 21:
				{
					if (A_0 < -536870912.0)
					{
						num = 15;
						continue;
					}
					long num2 = spr\u2620.ᜀ(A_0);
					num5 = 0;
					bool flag = true;
					num = 28;
					continue;
				}
				case 22:
					num = 20;
					continue;
				case 23:
					num = 29;
					continue;
				case 24:
				{
					bool flag;
					if (flag)
					{
						num = 0;
						continue;
					}
					goto IL_1D7;
				}
				case 25:
				{
					bool flag;
					if (flag)
					{
						num = 1;
						continue;
					}
					goto IL_217;
				}
				case 26:
					num = 2;
					continue;
				case 27:
					goto IL_1F7;
				case 28:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_372;
					default:
					{
						if (false)
						{
						}
						long num2;
						if ((num2 & 17179869183L) == 0L)
						{
							num = 9;
							continue;
						}
						goto IL_237;
					}
					}
					break;
				case 29:
				{
					int num4;
					if (num4 > 0)
					{
						num = 26;
						continue;
					}
					goto IL_1F7;
				}
				case 30:
					goto IL_237;
				case 31:
					num = 6;
					continue;
				}
				if (A_0 <= 536870912.0)
				{
					num = 13;
					continue;
				}
				return int.MaxValue;
				IL_1D7:
				num = 17;
				continue;
				IL_1F7:
				num = 16;
				continue;
				IL_217:
				num = 24;
				continue;
				IL_372:
				goto IL_217;
				IL_237:
				num = 25;
			}
			IL_215:
			if (true)
			{
			}
			return num5;
		}
		}
	}

	// Token: 0x06003197 RID: 12695 RVA: 0x001CB3F4 File Offset: 0x001CA3F4
	public new static double ᜃ(int A_0)
	{
		switch (0)
		{
		default:
		{
			double num3;
			double num4;
			for (;;)
			{
				bool flag = (A_0 & 1) != 0;
				bool flag2 = (A_0 & 2) != 0;
				long num = (long)(A_0 >> 2);
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return num3;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (!flag)
							{
								num2 = 2;
								continue;
							}
							goto IL_E4;
						}
						break;
					case 2:
						return num4;
					case 3:
						if (!flag)
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						goto IL_A1;
					case 4:
						num3 = (double)num;
						num2 = 3;
						continue;
					case 5:
						if (flag2)
						{
							num2 = 4;
							continue;
						}
						num4 = spr\u2620.ᜀ(num << 34);
						num2 = 1;
						continue;
					}
					break;
				}
			}
			return num4;
			IL_A1:
			return num3 / 100.0;
			IL_E4:
			return num4 / 100.0;
		}
		}
	}

	// Token: 0x06003198 RID: 12696 RVA: 0x001CB4F4 File Offset: 0x001CA4F4
	private new static int ᜀ(long A_0, bool A_1)
	{
		int num;
		for (;;)
		{
			num = (int)(A_0 >> 32);
			int num2 = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					num |= 1;
					num2 = 2;
					continue;
				case 1:
					if (A_1)
					{
						num2 = 0;
						continue;
					}
					return num;
				case 2:
					goto IL_4D;
				}
				break;
			}
		}
		IL_4D:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		return num;
	}

	// Token: 0x06003199 RID: 12697 RVA: 0x001CB570 File Offset: 0x001CA570
	public new static double ᜂ(int A_0)
	{
		int num = 5;
		double num2;
		for (;;)
		{
			double num3;
			switch (num)
			{
			case 0:
				return num2;
			case 1:
				if ((A_0 & 1) > 0)
				{
					num = 9;
					continue;
				}
				return num2;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_106;
				}
				if (false)
				{
				}
				if (!spr\u17FF.ᜂ())
				{
					num = 7;
					continue;
				}
				num = 3;
				continue;
			case 3:
				if (true)
				{
				}
				num3 = sprỔ.ᜁ(A_0);
				goto IL_108;
			case 4:
				num2 = (double)(A_0 >> 2);
				num = 6;
				continue;
			case 6:
				goto IL_106;
			case 7:
				num = 8;
				continue;
			case 8:
				num3 = sprỔ.ᜀ(A_0);
				goto IL_108;
			case 9:
				num2 /= 100.0;
				num = 0;
				continue;
			case 10:
				goto IL_BD;
			}
			if ((A_0 & 2) > 0)
			{
				num = 4;
				continue;
			}
			num = 2;
			continue;
			IL_BD:
			num = 1;
			continue;
			IL_106:
			goto IL_BD;
			IL_108:
			num2 = num3;
			num = 10;
		}
		return num2;
	}

	// Token: 0x0600319A RID: 12698 RVA: 0x001CB694 File Offset: 0x001CA694
	private new unsafe static double ᜁ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		double result = 0.0;
		int* ptr = (int*)(&result);
		ptr[1] = (int)((long)A_0 & (long)((ulong)-4));
		*ptr = 0;
		return result;
	}

	// Token: 0x0600319B RID: 12699 RVA: 0x001CB6F0 File Offset: 0x001CA6F0
	private new static double ᜀ(int A_0)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		byte[] array = new byte[8];
		byte[] bytes = BitConverter.GetBytes((long)A_0 & (long)((ulong)-4));
		Buffer.BlockCopy(bytes, 0, array, 4, 4);
		return BitConverter.ToDouble(array, 0);
	}

	// Token: 0x0600319C RID: 12700 RVA: 0x001CB750 File Offset: 0x001CA750
	public new static int ᜂ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			A_1 += 10;
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2 != ExcelVersion.Version97to2003)
					{
						num = 2;
						continue;
					}
					goto IL_6C;
				case 1:
					goto IL_4E;
				case 2:
					A_1 += 4;
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_4E:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			break;
		}
		IL_6C:
		return A_0.ReadInt32(A_1);
	}

	// Token: 0x0600319D RID: 12701 RVA: 0x001CB7D0 File Offset: 0x001CA7D0
	public double ᜈ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ();
	}

	// Token: 0x0600319E RID: 12702 RVA: 0x001CB814 File Offset: 0x001CA814
	public object ᜇ()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return this.ᜀ();
	}

	// Token: 0x0600319F RID: 12703 RVA: 0x001CB85C File Offset: 0x001CA85C
	public new void ᜀ(object A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜁ((double)A_0);
	}

	// Token: 0x040015C9 RID: 5577
	internal new const int ᜀ = 10;

	// Token: 0x040015CA RID: 5578
	internal new const int ᜁ = 14;

	// Token: 0x040015CB RID: 5579
	internal new const int ᜂ = 6;

	// Token: 0x040015CC RID: 5580
	internal new const int ᜃ = 10;

	// Token: 0x040015CD RID: 5581
	public const uint ᜄ = 4294967292U;

	// Token: 0x040015CE RID: 5582
	private const int ᜅ = 536870912;

	// Token: 0x040015CF RID: 5583
	private const int ᜆ = -536870912;

	// Token: 0x040015D0 RID: 5584
	[spr\u2429(6, 4, true)]
	private int ᜇ;

	// Token: 0x040015D1 RID: 5585
	[spr\u2429(6, 0, TFieldType.Bit)]
	private new bool ᜈ;

	// Token: 0x040015D2 RID: 5586
	[spr\u2429(6, 1, TFieldType.Bit)]
	private bool ᜉ;
}
