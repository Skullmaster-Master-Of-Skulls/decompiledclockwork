using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020005A3 RID: 1443
[spr\u2593(TBIFFRecord.DV)]
[CLSCompliant(false)]
internal class sprᡣ : spr\u251F, ICloneable
{
	// Token: 0x06005771 RID: 22385 RVA: 0x00379C40 File Offset: 0x00378C40
	public uint ᜏ()
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

	// Token: 0x06005772 RID: 22386 RVA: 0x00379C84 File Offset: 0x00378C84
	public bool \u1712()
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
		return this.ᜈ;
	}

	// Token: 0x06005773 RID: 22387 RVA: 0x00379CC8 File Offset: 0x00378CC8
	public new void ᜃ(bool A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06005774 RID: 22388 RVA: 0x00379D0C File Offset: 0x00378D0C
	public new bool ᜃ()
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
		return this.ᜉ;
	}

	// Token: 0x06005775 RID: 22389 RVA: 0x00379D50 File Offset: 0x00378D50
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

	// Token: 0x06005776 RID: 22390 RVA: 0x00379D94 File Offset: 0x00378D94
	public bool ᜄ()
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
		return this.ᜊ;
	}

	// Token: 0x06005777 RID: 22391 RVA: 0x00379DD8 File Offset: 0x00378DD8
	public void ᜁ(bool A_0)
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
		this.ᜊ = A_0;
	}

	// Token: 0x06005778 RID: 22392 RVA: 0x00379E1C File Offset: 0x00378E1C
	public bool ᜇ()
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
		return this.ᜋ;
	}

	// Token: 0x06005779 RID: 22393 RVA: 0x00379E60 File Offset: 0x00378E60
	public void ᜄ(bool A_0)
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
		this.ᜋ = A_0;
	}

	// Token: 0x0600577A RID: 22394 RVA: 0x00379EA4 File Offset: 0x00378EA4
	public bool ᜁ()
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
		return this.ᜌ;
	}

	// Token: 0x0600577B RID: 22395 RVA: 0x00379EE8 File Offset: 0x00378EE8
	public void ᜂ(bool A_0)
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
		this.ᜌ = A_0;
	}

	// Token: 0x0600577C RID: 22396 RVA: 0x00379F2C File Offset: 0x00378F2C
	public CellDataType \u170D()
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
		return (CellDataType)BiffRecordRaw.ᜀ(this.ᜇ, 15U);
	}

	// Token: 0x0600577D RID: 22397 RVA: 0x00379F74 File Offset: 0x00378F74
	public new void ᜀ(CellDataType A_0)
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
		BiffRecordRaw.ᜀ(ref this.ᜇ, 15U, (uint)A_0);
	}

	// Token: 0x0600577E RID: 22398 RVA: 0x00379FC0 File Offset: 0x00378FC0
	public AlertStyleType ᜋ()
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
		return (AlertStyleType)(BiffRecordRaw.ᜀ(this.ᜇ, 112U) >> 4);
	}

	// Token: 0x0600577F RID: 22399 RVA: 0x0037A00C File Offset: 0x0037900C
	public new void ᜀ(AlertStyleType A_0)
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
		BiffRecordRaw.ᜀ(ref this.ᜇ, 112U, (uint)((uint)A_0 << 4));
	}

	// Token: 0x06005780 RID: 22400 RVA: 0x0037A058 File Offset: 0x00379058
	public ValidationComparisonOperator ᜎ()
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
		return (ValidationComparisonOperator)(BiffRecordRaw.ᜀ(this.ᜇ, 15728640U) >> 20);
	}

	// Token: 0x06005781 RID: 22401 RVA: 0x0037A0A8 File Offset: 0x003790A8
	public new void ᜀ(ValidationComparisonOperator A_0)
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
		BiffRecordRaw.ᜀ(ref this.ᜇ, 15728640U, (uint)((uint)A_0 << 20));
	}

	// Token: 0x06005782 RID: 22402 RVA: 0x0037A0F8 File Offset: 0x003790F8
	public string ᜊ()
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
		return this.\u170D;
	}

	// Token: 0x06005783 RID: 22403 RVA: 0x0037A13C File Offset: 0x0037913C
	public new void ᜃ(string A_0)
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
		this.\u170D = A_0;
	}

	// Token: 0x06005784 RID: 22404 RVA: 0x0037A180 File Offset: 0x00379180
	public string ᜈ()
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
		return this.ᜏ;
	}

	// Token: 0x06005785 RID: 22405 RVA: 0x0037A1C4 File Offset: 0x003791C4
	public void ᜄ(string A_0)
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
		this.ᜏ = A_0;
	}

	// Token: 0x06005786 RID: 22406 RVA: 0x0037A208 File Offset: 0x00379208
	public string ᜐ()
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
		return this.ᜑ;
	}

	// Token: 0x06005787 RID: 22407 RVA: 0x0037A24C File Offset: 0x0037924C
	public void ᜅ(string A_0)
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
		this.ᜑ = A_0;
	}

	// Token: 0x06005788 RID: 22408 RVA: 0x0037A290 File Offset: 0x00379290
	public new string ᜀ()
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
		return this.\u1713;
	}

	// Token: 0x06005789 RID: 22409 RVA: 0x0037A2D4 File Offset: 0x003792D4
	public void ᜂ(string A_0)
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
		this.\u1713 = A_0;
	}

	// Token: 0x0600578A RID: 22410 RVA: 0x0037A318 File Offset: 0x00379318
	public Ptg[] \u1713()
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
		return this.\u1717;
	}

	// Token: 0x0600578B RID: 22411 RVA: 0x0037A35C File Offset: 0x0037935C
	public void ᜁ(Ptg[] A_0)
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
		this.\u1717 = A_0;
	}

	// Token: 0x0600578C RID: 22412 RVA: 0x0037A3A0 File Offset: 0x003793A0
	public Ptg[] \u1714()
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
		return this.\u1718;
	}

	// Token: 0x0600578D RID: 22413 RVA: 0x0037A3E4 File Offset: 0x003793E4
	public new void ᜀ(Ptg[] A_0)
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
		this.\u1718 = A_0;
	}

	// Token: 0x0600578E RID: 22414 RVA: 0x0037A428 File Offset: 0x00379428
	public ushort ᜌ()
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
		return this.\u1715;
	}

	// Token: 0x0600578F RID: 22415 RVA: 0x0037A46C File Offset: 0x0037946C
	public TAddr[] ᜑ()
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
		return this.\u1716.ToArray();
	}

	// Token: 0x06005790 RID: 22416 RVA: 0x0037A4B4 File Offset: 0x003794B4
	public void ᜁ(TAddr[] A_0)
	{
		for (;;)
		{
			this.\u1716.Clear();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_76;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6E;
					default:
						if (false)
						{
						}
						if (A_0 != null)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_78;
					}
					break;
				case 2:
					this.\u1716.AddRange(A_0);
					goto IL_6E;
				}
				break;
				IL_6E:
				num = 0;
			}
		}
		IL_76:
		IL_78:
		this.\u1715 = (ushort)this.\u1716.Count;
	}

	// Token: 0x06005791 RID: 22417 RVA: 0x0037A54C File Offset: 0x0037954C
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
		return 12;
	}

	// Token: 0x06005792 RID: 22418 RVA: 0x0037A58C File Offset: 0x0037958C
	public sprᡣ()
	{
	}

	// Token: 0x06005793 RID: 22419 RVA: 0x0037A5EC File Offset: 0x003795EC
	public sprᡣ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005794 RID: 22420 RVA: 0x0037A650 File Offset: 0x00379650
	public sprᡣ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06005795 RID: 22421 RVA: 0x0037A6B0 File Offset: 0x003796B0
	public override void ᜂ()
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1B4:
				num = 4;
				break;
			default:
				if (false)
				{
				}
				goto IL_4B;
			}
			int num2;
			int num3;
			for (;;)
			{
				IL_2C:
				switch (num)
				{
				case 0:
					goto IL_1C2;
				case 1:
					num = 3;
					continue;
				case 2:
					if (num2 >= (int)this.\u1715)
					{
						num = 1;
						continue;
					}
					this.\u1716.Add(base.\u1716(num3));
					num2++;
					num3 += 8;
					num = 5;
					continue;
				case 3:
					goto IL_1A3;
				case 4:
					goto IL_1C0;
				case 5:
					goto IL_1C2;
				}
				goto IL_4B;
				IL_1C2:
				num = 2;
			}
			IL_1A3:
			if (true)
			{
			}
			if (num3 != this.m_iLength)
			{
				goto IL_1B4;
			}
			return;
			IL_1C0:
			throw new sprῩ();
			IL_4B:
			this.ᜇ = base.\u1714(0);
			this.ᜈ = base.ᜁ(0, 7);
			this.ᜉ = base.ᜁ(1, 0);
			this.ᜊ = base.ᜁ(1, 1);
			this.ᜋ = base.ᜁ(2, 2);
			this.ᜌ = base.ᜁ(2, 3);
			num3 = 4;
			this.ᜃ(this.ᜀ(base.ᜀ(ref num3, out this.ᜎ)));
			this.ᜄ(this.ᜀ(base.ᜀ(ref num3, out this.ᜐ)));
			this.ᜅ(this.ᜀ(base.ᜀ(ref num3, out this.\u1712)));
			this.ᜂ(this.ᜀ(base.ᜀ(ref num3, out this.\u1714)));
			ushort num4 = base.ᜌ(num3);
			num3 += 4;
			byte[] a_ = base.ᜃ(num3, (int)num4);
			num3 += (int)num4;
			ushort num5 = base.ᜌ(num3);
			num3 += 4;
			byte[] a_2 = base.ᜃ(num3, (int)num5);
			num3 += (int)num5;
			spr\u24E5 spr_u24E = new spr\u24E5(a_);
			this.\u1717 = FormulaUtil.ᜀ(spr_u24E, (int)num4, ExcelVersion.Version97to2003);
			spr_u24E.ᜀ(a_2);
			this.\u1718 = FormulaUtil.ᜀ(spr_u24E, (int)num5, ExcelVersion.Version97to2003);
			this.\u1715 = base.ᜌ(num3);
			num3 += 2;
			this.\u1716.Clear();
			num2 = 0;
			num = 0;
			goto IL_2C;
		}
		}
	}

	// Token: 0x06005796 RID: 22422 RVA: 0x0037A8E0 File Offset: 0x003798E0
	public override void ᜀ(ExcelVersion A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.m_iLength = this.GetStoreSize(A_0);
				this.ᜀ = new byte[this.m_iLength];
				base.ᜀ(0, this.ᜇ);
				base.ᜀ(0, this.ᜈ, 7);
				base.ᜀ(1, this.ᜉ, 0);
				base.ᜀ(1, this.ᜊ, 1);
				base.ᜀ(2, this.ᜋ, 2);
				base.ᜀ(2, this.ᜌ, 3);
				int num = 4;
				base.ᜁ(ref num, this.ᜁ(this.\u170D), this.ᜎ);
				base.ᜁ(ref num, this.ᜁ(this.ᜏ), this.ᜐ);
				base.ᜁ(ref num, this.ᜁ(this.ᜑ), this.\u1712);
				base.ᜁ(ref num, this.ᜁ(this.\u1713), this.\u1714);
				byte[] array = FormulaUtil.ᜀ(this.\u1717, A_0);
				int num2 = 11;
				for (;;)
				{
					ushort num3;
					ushort num4;
					ushort num5;
					ushort num6;
					int num7;
					byte[] array2;
					switch (num2)
					{
					case 0:
						goto IL_210;
					case 1:
						goto IL_1E6;
					case 2:
						goto IL_24C;
					case 3:
						num3 = (ushort)array.Length;
						goto IL_192;
					case 4:
						if (num4 > 0)
						{
							num2 = 12;
							continue;
						}
						goto IL_1E6;
					case 5:
						num2 = 15;
						continue;
					case 6:
						goto IL_1FF;
					case 7:
						num2 = 8;
						continue;
					case 8:
						num5 = 0;
						goto IL_2C0;
					case 9:
						if (num6 > 0)
						{
							num2 = 17;
							continue;
						}
						goto IL_24C;
					case 10:
						if (true)
						{
						}
						if (num7 >= (int)this.\u1715)
						{
							num2 = 13;
							continue;
						}
						base.ᜀ(num, this.\u1716[num7]);
						num7++;
						num += 8;
						num2 = 0;
						continue;
					case 11:
						if (array == null)
						{
							num2 = 5;
							continue;
						}
						num2 = 3;
						continue;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1FF;
						default:
							if (false)
							{
							}
							base.ᜀ(num, array, 0, (int)num4);
							num += (int)num4;
							num2 = 1;
							continue;
						}
						break;
					case 13:
						return;
					case 14:
						goto IL_210;
					case 15:
						num3 = 0;
						goto IL_192;
					case 16:
						num5 = (ushort)array2.Length;
						goto IL_2C0;
					case 17:
						base.ᜀ(num, array2, 0, (int)num6);
						num += (int)num6;
						num2 = 2;
						continue;
					}
					break;
					IL_192:
					num4 = num3;
					base.ᜀ(num, num4);
					num += 2;
					base.ᜀ(num, 0);
					num += 2;
					num2 = 4;
					continue;
					IL_1E6:
					array2 = FormulaUtil.ᜀ(this.\u1718, A_0);
					num2 = 6;
					continue;
					IL_1FF:
					if (array2 == null)
					{
						num2 = 7;
						continue;
					}
					num2 = 16;
					continue;
					IL_210:
					num2 = 10;
					continue;
					IL_24C:
					base.ᜀ(num, this.\u1715);
					num += 2;
					num7 = 0;
					num2 = 14;
					continue;
					IL_2C0:
					num6 = num5;
					base.ᜀ(num, num6);
					num += 2;
					base.ᜀ(num, 0);
					num += 2;
					num2 = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06005797 RID: 22423 RVA: 0x0037AC1C File Offset: 0x00379C1C
	public new void ᜀ(TAddr A_0)
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
		this.\u1716.Add(A_0);
		this.\u1715 += 1;
	}

	// Token: 0x06005798 RID: 22424 RVA: 0x0037AC74 File Offset: 0x00379C74
	public new void ᜀ(TAddr[] A_0)
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
		this.\u1716.AddRange(A_0);
		this.\u1715 = (ushort)this.\u1716.Count;
	}

	// Token: 0x06005799 RID: 22425 RVA: 0x0037ACD0 File Offset: 0x00379CD0
	public new void ᜀ(ICollection<TAddr> A_0)
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
		this.\u1716.AddRange(A_0);
		this.\u1715 = (ushort)this.\u1716.Count;
	}

	// Token: 0x0600579A RID: 22426 RVA: 0x0037AD2C File Offset: 0x00379D2C
	public new static int ᜀ(Ptg[] A_0, ExcelVersion A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num4;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num3;
					switch (num)
					{
					case 0:
						goto IL_134;
					case 1:
						goto IL_134;
					case 3:
						if (A_2)
						{
							num = 5;
							continue;
						}
						goto IL_F7;
					case 4:
						return 0;
					case 5:
					{
						Ptg ptg;
						sprḝ sprḝ = ptg as sprḝ;
						num = 10;
						continue;
					}
					case 6:
					{
						int num2;
						return num2;
					}
					case 7:
					{
						sprḝ sprḝ;
						int num2;
						num2 += sprḝ.ᜀ();
						num = 8;
						continue;
					}
					case 8:
						goto IL_F7;
					case 9:
					{
						if (num3 >= num4)
						{
							num = 6;
							continue;
						}
						if (true)
						{
						}
						Ptg ptg = A_0[num3];
						int num2;
						num2 += ptg.GetSize(A_1);
						num = 3;
						continue;
					}
					case 10:
					{
						sprḝ sprḝ;
						if (sprḝ != null)
						{
							num = 7;
							continue;
						}
						goto IL_F7;
					}
					case 11:
						return 0;
					case 12:
						goto IL_E6;
					}
					if (A_0 == null)
					{
						num = 4;
						continue;
					}
					num4 = A_0.Length;
					num = 12;
					continue;
					IL_F7:
					num3++;
					num = 1;
					continue;
					IL_134:
					num = 9;
					continue;
				}
				}
				IL_E6:
				if (num4 == 0)
				{
					num = 11;
				}
				else
				{
					int num2 = 0;
					int num3 = 0;
					num = 0;
				}
			}
			return 0;
		}
		}
	}

	// Token: 0x0600579B RID: 22427 RVA: 0x0037AEA4 File Offset: 0x00379EA4
	public override int ᜁ(ExcelVersion A_0)
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
		return 14 + base.Get16BitStringSize(this.ᜁ(this.\u170D), this.ᜎ) + base.Get16BitStringSize(this.ᜁ(this.ᜏ), this.ᜐ) + base.Get16BitStringSize(this.ᜁ(this.ᜑ), this.\u1712) + base.Get16BitStringSize(this.ᜁ(this.\u1713), this.\u1714) + sprᡣ.ᜀ(this.\u1717, A_0, true) + sprᡣ.ᜀ(this.\u1718, A_0, true) + (int)(this.\u1715 * 8);
	}

	// Token: 0x0600579C RID: 22428 RVA: 0x0037AF6C File Offset: 0x00379F6C
	private string ᜁ(string A_0)
	{
		int a_ = 13;
		int num = 4;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				num = 2;
				continue;
			case 1:
				return A_0;
			case 2:
				if (A_0.Length == 0)
				{
					num = 3;
					continue;
				}
				return A_0;
			case 3:
				goto IL_42;
			case 4:
				IL_11:
				break;
			}
			if (A_0 != null)
			{
				num = 0;
				continue;
			}
			IL_42:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_11;
			default:
				if (false)
				{
				}
				A_0 = RecordTableEnumerator.b("䍂", a_);
				num = 1;
				break;
			}
		}
		return A_0;
	}

	// Token: 0x0600579D RID: 22429 RVA: 0x0037B01C File Offset: 0x0037A01C
	private new string ᜀ(string A_0)
	{
		int a_ = 18;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return A_0;
			case 1:
				A_0 = string.Empty;
				num = 0;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return A_0;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (!(A_0 == RecordTableEnumerator.b("䡇", a_)))
				{
					return A_0;
				}
				num = 1;
				break;
			}
		}
		return A_0;
	}

	// Token: 0x0600579E RID: 22430 RVA: 0x0037B0B0 File Offset: 0x0037A0B0
	public object ᜅ()
	{
		sprᡣ sprᡣ;
		for (;;)
		{
			sprᡣ = (sprᡣ)base.Clone();
			sprᡣ.\u1717 = spr\u1CD3.ᜀ(this.\u1717);
			sprᡣ.\u1718 = spr\u1CD3.ᜀ(this.\u1718);
			int count = this.\u1716.Count;
			sprᡣ.\u1716 = new List<TAddr>(count);
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_7E;
				case 1:
					goto IL_6A;
				case 2:
					goto IL_6A;
				case 3:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					sprᡣ.\u1716.Add(this.\u1716[num]);
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				}
				break;
				IL_6A:
				num2 = 3;
			}
		}
		IL_7E:
		if (true)
		{
		}
		return sprᡣ;
	}

	// Token: 0x0600579F RID: 22431 RVA: 0x0037B194 File Offset: 0x0037A194
	public override bool ᜀ(object A_0)
	{
		sprᡣ sprᡣ;
		for (;;)
		{
			sprᡣ = (A_0 as sprᡣ);
			int num = 17;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_E7;
				case 2:
					goto IL_251;
				case 3:
					if (sprᡣ.ᜐ() == this.ᜐ())
					{
						num = 13;
						continue;
					}
					goto IL_1D5;
				case 4:
					if (sprᡣ.ᜈ() == this.ᜈ())
					{
						num = 28;
						continue;
					}
					goto IL_1D5;
				case 5:
					if (sprᡣ.ᜄ() == this.ᜄ())
					{
						num = 12;
						continue;
					}
					goto IL_1D5;
				case 6:
					if (sprᡣ.\u170D() == this.\u170D())
					{
						num = 18;
						continue;
					}
					goto IL_1D5;
				case 7:
					num = 1;
					continue;
				case 8:
					return false;
				case 9:
					goto IL_1E0;
				case 10:
					if (sprᡣ.ᜇ() == this.ᜇ())
					{
						num = 19;
						continue;
					}
					goto IL_1D5;
				case 11:
					num = 6;
					continue;
				case 12:
					num = 10;
					continue;
				case 13:
					num = 15;
					continue;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E7;
					default:
						if (false)
						{
						}
						num = 24;
						continue;
					}
					break;
				case 15:
					if (sprᡣ.ᜀ() == this.ᜀ())
					{
						num = 22;
						continue;
					}
					goto IL_1D5;
				case 16:
					if (sprᡣ.\u1712() == this.\u1712())
					{
						num = 26;
						continue;
					}
					goto IL_1D5;
				case 17:
					if (sprᡣ == null)
					{
						num = 8;
						continue;
					}
					num = 16;
					continue;
				case 18:
					num = 21;
					continue;
				case 19:
					num = 20;
					continue;
				case 20:
					if (sprᡣ.ᜁ() == this.ᜁ())
					{
						num = 11;
						continue;
					}
					goto IL_1D5;
				case 21:
					if (sprᡣ.ᜋ() == this.ᜋ())
					{
						num = 7;
						continue;
					}
					goto IL_1D5;
				case 22:
					num = 23;
					continue;
				case 23:
					if (Ptg.CompareArrays(sprᡣ.\u1713(), this.\u1713()))
					{
						num = 0;
						continue;
					}
					goto IL_1D5;
				case 24:
					if (sprᡣ.ᜊ() == this.ᜊ())
					{
						num = 29;
						continue;
					}
					goto IL_1D5;
				case 25:
					if (sprᡣ.ᜃ() == this.ᜃ())
					{
						num = 27;
						continue;
					}
					goto IL_1D5;
				case 26:
					if (true)
					{
					}
					num = 25;
					continue;
				case 27:
					num = 5;
					continue;
				case 28:
					num = 3;
					continue;
				case 29:
					num = 4;
					continue;
				}
				break;
				IL_E7:
				if (sprᡣ.ᜎ() == this.ᜎ())
				{
					num = 14;
					continue;
				}
				IL_1D5:
				num = 9;
			}
		}
		return false;
		IL_1E0:
		return false;
		IL_251:
		return Ptg.CompareArrays(sprᡣ.\u1714(), this.\u1714());
	}

	// Token: 0x060057A0 RID: 22432 RVA: 0x0037B4F8 File Offset: 0x0037A4F8
	public virtual int ᜆ()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			int num5;
			int num6;
			int num7;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D3;
					default:
						goto IL_A8;
					}
					break;
				case 1:
					num2 = 0;
					goto IL_10D;
				case 2:
					num2 = this.\u1717.Length;
					goto IL_10D;
				case 3:
					num3 = this.\u1718.Length;
					goto IL_13F;
				case 4:
					if (num4 >= this.ᜑ().Length)
					{
						num = 0;
						continue;
					}
					if (true)
					{
					}
					num5 += this.ᜑ().GetValue(num4).GetHashCode();
					num4++;
					goto IL_D3;
				case 5:
					num3 = 0;
					goto IL_13F;
				case 7:
					num = 2;
					continue;
				case 8:
					goto IL_75;
				case 9:
					goto IL_75;
				case 10:
					if (this.\u1718 != null)
					{
						num = 11;
						continue;
					}
					num = 5;
					continue;
				case 11:
					num = 3;
					continue;
				}
				if (this.\u1717 != null)
				{
					num = 7;
					continue;
				}
				num = 1;
				continue;
				IL_75:
				num = 4;
				continue;
				IL_D3:
				num = 9;
				continue;
				IL_10D:
				num6 = num2;
				num = 10;
				continue;
				IL_13F:
				num7 = num3;
				num5 = 0;
				num4 = 0;
				num = 8;
			}
			IL_A8:
			if (false)
			{
			}
			return this.\u1712().GetHashCode() ^ this.ᜃ().GetHashCode() ^ this.ᜄ().GetHashCode() ^ this.ᜇ().GetHashCode() ^ this.ᜁ().GetHashCode() ^ this.\u170D().GetHashCode() ^ this.ᜋ().GetHashCode() ^ this.ᜎ().GetHashCode() ^ this.ᜊ().GetHashCode() ^ this.ᜈ().GetHashCode() ^ this.ᜐ().GetHashCode() ^ this.ᜀ().GetHashCode() ^ num6.GetHashCode() ^ num7.GetHashCode() ^ this.ᜌ().GetHashCode() ^ num5.GetHashCode();
		}
		}
	}

	// Token: 0x04002992 RID: 10642
	public new const uint ᜀ = 15U;

	// Token: 0x04002993 RID: 10643
	public new const uint ᜁ = 112U;

	// Token: 0x04002994 RID: 10644
	public new const uint ᜂ = 15728640U;

	// Token: 0x04002995 RID: 10645
	public new const int ᜃ = 4;

	// Token: 0x04002996 RID: 10646
	public new const int ᜄ = 20;

	// Token: 0x04002997 RID: 10647
	public new const string ᜅ = "\0";

	// Token: 0x04002998 RID: 10648
	private new const int ᜆ = 14;

	// Token: 0x04002999 RID: 10649
	[spr\u2429(0, 4)]
	private uint ᜇ;

	// Token: 0x0400299A RID: 10650
	[spr\u2429(0, 7, TFieldType.Bit)]
	private bool ᜈ;

	// Token: 0x0400299B RID: 10651
	[spr\u2429(1, 0, TFieldType.Bit)]
	private bool ᜉ = true;

	// Token: 0x0400299C RID: 10652
	[spr\u2429(1, 1, TFieldType.Bit)]
	private new bool ᜊ;

	// Token: 0x0400299D RID: 10653
	[spr\u2429(2, 2, TFieldType.Bit)]
	private new bool ᜋ = true;

	// Token: 0x0400299E RID: 10654
	[spr\u2429(2, 3, TFieldType.Bit)]
	private new bool ᜌ = true;

	// Token: 0x0400299F RID: 10655
	private new string \u170D = string.Empty;

	// Token: 0x040029A0 RID: 10656
	private new bool ᜎ;

	// Token: 0x040029A1 RID: 10657
	private new string ᜏ = string.Empty;

	// Token: 0x040029A2 RID: 10658
	private new bool ᜐ;

	// Token: 0x040029A3 RID: 10659
	private new string ᜑ = string.Empty;

	// Token: 0x040029A4 RID: 10660
	private new bool \u1712;

	// Token: 0x040029A5 RID: 10661
	private new string \u1713 = string.Empty;

	// Token: 0x040029A6 RID: 10662
	private new bool \u1714;

	// Token: 0x040029A7 RID: 10663
	private new ushort \u1715;

	// Token: 0x040029A8 RID: 10664
	private new List<TAddr> \u1716 = new List<TAddr>();

	// Token: 0x040029A9 RID: 10665
	private new Ptg[] \u1717;

	// Token: 0x040029AA RID: 10666
	private Ptg[] \u1718;
}
