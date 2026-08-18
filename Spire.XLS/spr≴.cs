using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200042E RID: 1070
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartSeriesList)]
internal class spr\u2274 : BiffRecordRaw
{
	// Token: 0x060040BC RID: 16572 RVA: 0x00244A54 File Offset: 0x00243A54
	public ushort ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x060040BD RID: 16573 RVA: 0x00244A98 File Offset: 0x00243A98
	public ushort[] ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x060040BE RID: 16574 RVA: 0x00244ADC File Offset: 0x00243ADC
	public void ᜀ(ushort[] A_0)
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
		this.ᜂ = A_0;
		this.ᜁ = (ushort)((A_0 != null) ? A_0.Length : 0);
	}

	// Token: 0x060040BF RID: 16575 RVA: 0x00244B34 File Offset: 0x00243B34
	public virtual int ᜃ()
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
		return 2;
	}

	// Token: 0x060040C0 RID: 16576 RVA: 0x00244B70 File Offset: 0x00243B70
	public spr\u2274()
	{
	}

	// Token: 0x060040C1 RID: 16577 RVA: 0x00244B84 File Offset: 0x00243B84
	public spr\u2274(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060040C2 RID: 16578 RVA: 0x00244B9C File Offset: 0x00243B9C
	public spr\u2274(int A_0) : base(A_0)
	{
	}

	// Token: 0x060040C3 RID: 16579 RVA: 0x00244BB0 File Offset: 0x00243BB0
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		int a_ = 3;
		for (;;)
		{
			this.ᜁ = A_0.ReadUInt16(A_1);
			A_1 += 2;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if ((int)(this.ᜁ * 2 + 2) != this.m_iLength)
					{
						num = 1;
						continue;
					}
					this.ᜂ = new ushort[(int)this.ᜁ];
					int num2 = 0;
					num = 3;
					continue;
				}
				case 1:
					goto IL_67;
				case 2:
				{
					int num2;
					if (num2 >= (int)this.ᜁ)
					{
						num = 5;
						continue;
					}
					this.ᜂ[num2] = A_0.ReadUInt16(A_1);
					num2++;
					A_1 += 2;
					goto IL_81;
				}
				case 3:
					goto IL_BF;
				case 4:
					goto IL_BF;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_81;
					default:
						goto IL_FC;
					}
					break;
				}
				break;
				IL_81:
				num = 4;
				continue;
				IL_BF:
				if (true)
				{
				}
				num = 2;
			}
		}
		IL_67:
		throw new sprῩ(RecordTableEnumerator.b("稸区尼䴾㕀གⱄ㑆㵈᥊⡌ⱎ㹐⅒ㅔ", a_));
		IL_FC:
		if (false)
		{
		}
	}

	// Token: 0x060040C4 RID: 16580 RVA: 0x00244CC4 File Offset: 0x00243CC4
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			for (;;)
			{
				A_0.WriteUInt16(A_1, this.ᜁ);
				this.m_iLength = 2;
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_4A;
					case 1:
						goto IL_4A;
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (num >= (int)this.ᜁ)
							{
								num2 = 2;
								continue;
							}
							A_0.WriteUInt16(A_1 + this.m_iLength, this.ᜂ[num]);
							num++;
							this.m_iLength += 2;
							num2 = 1;
							continue;
						}
						break;
					}
					break;
					IL_4A:
					num2 = 3;
				}
			}
		}
	}

	// Token: 0x060040C5 RID: 16581 RVA: 0x00244D88 File Offset: 0x00243D88
	public virtual int ᜀ(ExcelVersion A_0)
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
		return (int)(2 + 2 * this.ᜁ);
	}

	// Token: 0x060040C6 RID: 16582 RVA: 0x00244DD0 File Offset: 0x00243DD0
	public static bool ᜁ(spr\u2274 A_0, spr\u2274 A_1)
	{
		switch (0)
		{
		default:
		{
			bool flag3;
			for (;;)
			{
				bool flag = object.Equals(A_0, null);
				bool flag2 = object.Equals(A_1, null);
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						num = 10;
						continue;
					case 1:
						num = 6;
						continue;
					case 2:
						num = 8;
						continue;
					case 3:
					{
						int num2;
						int num3;
						if (num2 < num3)
						{
							num = 0;
							continue;
						}
						return flag3;
					}
					case 4:
						goto IL_13B;
					case 5:
						goto IL_AC;
					case 6:
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
							if (flag2)
							{
								num = 12;
								continue;
							}
							break;
						}
						flag3 = (A_0.ᜁ == A_1.ᜁ);
						int num2 = 0;
						int num3 = (int)A_0.ᜁ;
						num = 5;
						continue;
					}
					case 7:
						if (flag)
						{
							num = 2;
							continue;
						}
						goto IL_179;
					case 8:
						if (flag2)
						{
							num = 9;
							continue;
						}
						goto IL_179;
					case 9:
						return true;
					case 10:
					{
						if (!flag3)
						{
							num = 4;
							continue;
						}
						int num2;
						flag3 = (A_0.ᜂ[num2] == A_1.ᜂ[num2]);
						num2++;
						num = 11;
						continue;
					}
					case 11:
						goto IL_AC;
					case 12:
						goto IL_174;
					case 13:
						if (!flag)
						{
							num = 1;
							continue;
						}
						return false;
					}
					break;
					IL_AC:
					num = 3;
					continue;
					IL_179:
					num = 13;
				}
			}
			return false;
			IL_13B:
			return flag3;
			IL_174:
			return false;
		}
		}
	}

	// Token: 0x060040C7 RID: 16583 RVA: 0x00244F78 File Offset: 0x00243F78
	public static bool ᜀ(spr\u2274 A_0, spr\u2274 A_1)
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
		return !spr\u2274.ᜁ(A_0, A_1);
	}

	// Token: 0x060040C8 RID: 16584 RVA: 0x00244FC0 File Offset: 0x00243FC0
	public virtual object ᜂ()
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
		spr\u2274 spr_u = (spr\u2274)base.Clone();
		spr_u.ᜂ = spr\u1CD3.ᜀ(this.ᜂ);
		return spr_u;
	}

	// Token: 0x04001CE8 RID: 7400
	public new const int ᜀ = 2;

	// Token: 0x04001CE9 RID: 7401
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x04001CEA RID: 7402
	private ushort[] ᜂ;
}
