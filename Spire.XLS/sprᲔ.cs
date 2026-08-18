using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000483 RID: 1155
[spr\u2400(FormulaToken.tArea1)]
[CLSCompliant(false)]
[spr\u2400(FormulaToken.tArea3)]
[spr\u2400(FormulaToken.tArea2)]
internal class sprᲔ : Ptg, spr\u2590, spr\u1CD5
{
	// Token: 0x060046D6 RID: 18134 RVA: 0x002AEC10 File Offset: 0x002ADC10
	public sprᲔ()
	{
	}

	// Token: 0x060046D7 RID: 18135 RVA: 0x002AEC24 File Offset: 0x002ADC24
	public sprᲔ(string A_0, IWorkbook A_1)
	{
		int a_ = 16;
		base..ctor();
		Match match = FormulaUtil.CellRangeRegex.Match(A_0);
		string value = match.Groups[RecordTableEnumerator.b("Յ❇♉㥋⍍㹏捑", a_)].Value;
		string value2 = match.Groups[RecordTableEnumerator.b("ᑅ❇㵉絋", a_)].Value;
		string value3 = match.Groups[RecordTableEnumerator.b("Յ❇♉㥋⍍㹏恑", a_)].Value;
		string value4 = match.Groups[RecordTableEnumerator.b("ᑅ❇㵉繋", a_)].Value;
		if (!match.Success)
		{
			throw new ArgumentException();
		}
		this.ᜀ(0, 0, value2, value, value4, value3, false, A_1);
	}

	// Token: 0x060046D8 RID: 18136 RVA: 0x002AECE8 File Offset: 0x002ADCE8
	public sprᲔ(DataProvider A_0, int A_1, ExcelVersion A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x060046D9 RID: 18137 RVA: 0x002AED00 File Offset: 0x002ADD00
	public sprᲔ(sprᲔ A_0)
	{
		this.ᜀ = A_0.ᜀ;
		this.ᜁ = A_0.ᜁ;
		this.ᜂ = A_0.ᜂ;
		this.ᜃ = A_0.ᜃ;
		this.ᜄ = A_0.ᜄ;
		this.ᜅ = A_0.ᜅ;
	}

	// Token: 0x060046DA RID: 18138 RVA: 0x002AED5C File Offset: 0x002ADD5C
	public sprᲔ(int A_0, int A_1, int A_2, int A_3, byte A_4, byte A_5)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_2;
		this.ᜂ = A_1;
		this.ᜄ = A_3;
		this.ᜃ = A_4;
		this.ᜅ = A_5;
	}

	// Token: 0x060046DB RID: 18139 RVA: 0x002AED9C File Offset: 0x002ADD9C
	public sprᲔ(int A_0, int A_1, string A_2, string A_3, string A_4, string A_5, bool A_6, IWorkbook A_7)
	{
		this.ᜀ(A_0, A_1, A_2, A_3, A_4, A_5, A_6, A_7);
	}

	// Token: 0x060046DC RID: 18140 RVA: 0x002AEDC4 File Offset: 0x002ADDC4
	public int ᜋ()
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
		return this.ᜀ;
	}

	// Token: 0x060046DD RID: 18141 RVA: 0x002AEE08 File Offset: 0x002ADE08
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
		this.ᜀ = A_0;
	}

	// Token: 0x060046DE RID: 18142 RVA: 0x002AEE4C File Offset: 0x002ADE4C
	public bool ᜏ()
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
		return sprᦊ.ᜀ(this.ᜃ, 128);
	}

	// Token: 0x060046DF RID: 18143 RVA: 0x002AEE98 File Offset: 0x002ADE98
	public void ᜃ(bool A_0)
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
		this.ᜃ = sprᦊ.ᜀ(this.ᜃ, 128, A_0);
	}

	// Token: 0x060046E0 RID: 18144 RVA: 0x002AEEEC File Offset: 0x002ADEEC
	public bool ᜈ()
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
		return sprᦊ.ᜀ(this.ᜃ, 64);
	}

	// Token: 0x060046E1 RID: 18145 RVA: 0x002AEF34 File Offset: 0x002ADF34
	public void ᜀ(bool A_0)
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
		this.ᜃ = sprᦊ.ᜀ(this.ᜃ, 64, A_0);
	}

	// Token: 0x060046E2 RID: 18146 RVA: 0x002AEF84 File Offset: 0x002ADF84
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
		return this.ᜂ;
	}

	// Token: 0x060046E3 RID: 18147 RVA: 0x002AEFC8 File Offset: 0x002ADFC8
	public void ᜃ(int A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x060046E4 RID: 18148 RVA: 0x002AF00C File Offset: 0x002AE00C
	public int ᜉ()
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

	// Token: 0x060046E5 RID: 18149 RVA: 0x002AF050 File Offset: 0x002AE050
	public void ᜁ(int A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x060046E6 RID: 18150 RVA: 0x002AF094 File Offset: 0x002AE094
	public bool ᜇ()
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
		return sprᦊ.ᜀ(this.ᜅ, 128);
	}

	// Token: 0x060046E7 RID: 18151 RVA: 0x002AF0E0 File Offset: 0x002AE0E0
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
		this.ᜅ = sprᦊ.ᜀ(this.ᜅ, 128, A_0);
	}

	// Token: 0x060046E8 RID: 18152 RVA: 0x002AF134 File Offset: 0x002AE134
	public bool ᜌ()
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
		return sprᦊ.ᜀ(this.ᜅ, 64);
	}

	// Token: 0x060046E9 RID: 18153 RVA: 0x002AF17C File Offset: 0x002AE17C
	public void ᜂ(bool A_0)
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
		this.ᜅ = sprᦊ.ᜀ(this.ᜅ, 64, A_0);
	}

	// Token: 0x060046EA RID: 18154 RVA: 0x002AF1CC File Offset: 0x002AE1CC
	public int ᜂ()
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
		return this.ᜄ;
	}

	// Token: 0x060046EB RID: 18155 RVA: 0x002AF210 File Offset: 0x002AE210
	public void ᜅ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x060046EC RID: 18156 RVA: 0x002AF254 File Offset: 0x002AE254
	protected byte \u170D()
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
		return this.ᜃ;
	}

	// Token: 0x060046ED RID: 18157 RVA: 0x002AF298 File Offset: 0x002AE298
	protected void ᜀ(byte A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x060046EE RID: 18158 RVA: 0x002AF2DC File Offset: 0x002AE2DC
	protected byte ᜐ()
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
		return this.ᜅ;
	}

	// Token: 0x060046EF RID: 18159 RVA: 0x002AF320 File Offset: 0x002AE320
	protected void ᜁ(byte A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x060046F0 RID: 18160 RVA: 0x002AF364 File Offset: 0x002AE364
	protected void ᜀ(int A_0, int A_1, string A_2, string A_3, string A_4, string A_5, bool A_6, IWorkbook A_7)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			int num3;
			int num4;
			for (;;)
			{
				for (;;)
				{
					bool a_;
					num = sprᦊ.ᜁ(A_1, A_3, A_6, out a_);
					this.ᜀ(a_);
					num2 = sprᦊ.ᜀ(A_0, A_2, A_6, out a_);
					this.ᜃ(a_);
					num3 = sprᦊ.ᜁ(A_1, A_5, A_6, out a_);
					this.ᜂ(a_);
					num4 = sprᦊ.ᜀ(A_0, A_4, A_6, out a_);
					this.ᜁ(a_);
					int num5 = 2;
					for (;;)
					{
						switch (num5)
						{
						case 0:
							if (num == -1)
							{
								num5 = 4;
								continue;
							}
							goto IL_16E;
						case 1:
							if (num4 == -1)
							{
								num5 = 5;
								continue;
							}
							goto IL_F4;
						case 2:
							if (num2 == -1)
							{
								num5 = 7;
								continue;
							}
							goto IL_F4;
						case 3:
							goto IL_16C;
						case 4:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num5 = 6;
								continue;
							}
							break;
						case 5:
							num2 = 0;
							num4 = A_7.MaxRowCount - 1;
							num5 = 3;
							continue;
						case 6:
							if (num3 == -1)
							{
								num5 = 8;
								continue;
							}
							goto IL_16E;
						case 7:
							num5 = 1;
							continue;
						case 8:
							num = 0;
							num3 = A_7.MaxColumnCount - 1;
							num5 = 9;
							continue;
						case 9:
							goto IL_151;
						}
						break;
						IL_F4:
						num5 = 0;
					}
				}
			}
			IL_151:
			IL_16C:
			IL_16E:
			this.ᜀ = num2;
			this.ᜁ = num4;
			this.ᜂ = num;
			this.ᜄ = num3;
			return;
		}
		}
	}

	// Token: 0x060046F1 RID: 18161 RVA: 0x002AF4FC File Offset: 0x002AE4FC
	public virtual int ᜀ()
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
		return sprᲔ.ᜀ(this.TokenCode);
	}

	// Token: 0x060046F2 RID: 18162 RVA: 0x002AF544 File Offset: 0x002AE544
	public virtual FormulaToken ᜅ()
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
		int a_ = this.ᜀ();
		return spr\u255F.ᜀ(a_);
	}

	// Token: 0x060046F3 RID: 18163 RVA: 0x002AF58C File Offset: 0x002AE58C
	protected bool ᜂ(IWorkbook A_0)
	{
		while (this.ᜋ() == this.ᜉ())
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
				if (true)
				{
				}
				return this.ᜀ(A_0);
			}
		}
		return false;
	}

	// Token: 0x060046F4 RID: 18164 RVA: 0x002AF5E0 File Offset: 0x002AE5E0
	protected bool ᜀ(IWorkbook A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (this.ᜄ() == 0)
				{
					num = 5;
					continue;
				}
				goto IL_A8;
			case 3:
				return false;
			case 4:
				if (this.ᜏ() == this.ᜇ())
				{
					goto IL_8B;
				}
				goto IL_A8;
			case 5:
				goto IL_73;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8B:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 3;
				}
				else
				{
					num = 4;
				}
				break;
			}
		}
		return false;
		IL_73:
		return this.ᜂ() == A_0.MaxColumnCount - 1;
		IL_A8:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x060046F5 RID: 18165 RVA: 0x002AF6A0 File Offset: 0x002AE6A0
	protected bool ᜁ(IWorkbook A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_59;
			case 2:
				goto IL_73;
			case 3:
				if (this.ᜋ() == 0)
				{
					num = 2;
					continue;
				}
				return false;
			case 4:
				num = 3;
				continue;
			case 5:
				if (this.ᜈ() == this.ᜌ())
				{
					goto IL_8B;
				}
				return false;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8B:
				num = 4;
				continue;
			}
			if (false)
			{
			}
			if (A_0 == null)
			{
				num = 1;
			}
			else
			{
				num = 5;
			}
		}
		IL_59:
		if (true)
		{
		}
		return false;
		IL_73:
		return this.ᜉ() == A_0.MaxRowCount - 1;
	}

	// Token: 0x060046F6 RID: 18166 RVA: 0x002AF764 File Offset: 0x002AE764
	protected bool ᜃ(IWorkbook A_0)
	{
		while (this.ᜄ() == this.ᜂ())
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return this.ᜁ(A_0);
		}
		return false;
	}

	// Token: 0x060046F7 RID: 18167 RVA: 0x002AF7B8 File Offset: 0x002AE7B8
	public virtual sprᲔ ᜃ()
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
		return new spr\u255F(this);
	}

	// Token: 0x060046F8 RID: 18168 RVA: 0x002AF7FC File Offset: 0x002AE7FC
	public virtual void ᜀ(DataProvider A_0, ref int A_1, ExcelVersion A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_2 != ExcelVersion.Version2007)
					{
						num = 5;
						continue;
					}
					goto IL_12A;
				case 1:
					goto IL_5F;
				case 3:
					goto IL_44;
				case 4:
					if (A_2 == ExcelVersion.Version2010)
					{
						num = 1;
						continue;
					}
					goto IL_1A9;
				case 5:
					if (true)
					{
					}
					num = 4;
					continue;
				}
				if (A_2 == ExcelVersion.Version97to2003)
				{
					num = 3;
				}
				else
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
						num = 0;
						break;
					}
				}
			}
			IL_44:
			this.ᜀ = (int)A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜁ = (int)A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜂ = (int)A_0.ReadByte(A_1++);
			this.ᜃ = A_0.ReadByte(A_1++);
			this.ᜄ = (int)A_0.ReadByte(A_1++);
			this.ᜅ = A_0.ReadByte(A_1++);
			return;
			IL_5F:
			IL_12A:
			this.ᜀ = A_0.ReadInt32(A_1);
			A_1 += 4;
			this.ᜁ = A_0.ReadInt32(A_1);
			A_1 += 4;
			this.ᜂ = A_0.ReadInt32(A_1);
			A_1 += 4;
			this.ᜃ = A_0.ReadByte(A_1++);
			this.ᜄ = A_0.ReadInt32(A_1);
			A_1 += 4;
			this.ᜅ = A_0.ReadByte(A_1++);
			return;
			IL_1A9:
			throw new NotImplementedException();
		}
		}
	}

	// Token: 0x060046F9 RID: 18169 RVA: 0x002AF9B8 File Offset: 0x002AE9B8
	public static FormulaToken ᜀ(int A_0)
	{
		int a_ = 14;
		for (;;)
		{
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_81;
				case 1:
					switch (A_0)
					{
					case 1:
						return FormulaToken.tArea1;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_63;
						}
						break;
					case 3:
						return FormulaToken.tArea3;
					}
					num = 2;
					continue;
				case 2:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_63:
		if (false)
		{
		}
		return FormulaToken.tArea2;
		IL_81:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃ⡅ⱇ⽉㑋", a_));
	}

	// Token: 0x060046FA RID: 18170 RVA: 0x002AFA60 File Offset: 0x002AEA60
	public static int ᜀ(FormulaToken A_0)
	{
		int a_ = 1;
		for (;;)
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B8;
					default:
						if (false)
						{
						}
						if (A_0 != FormulaToken.tArea1)
						{
							num = 3;
							continue;
						}
						return 1;
					}
					break;
				case 1:
					num = 5;
					continue;
				case 2:
					num = 4;
					continue;
				case 3:
					num = 6;
					continue;
				case 4:
					goto IL_74;
				case 5:
					if (A_0 != FormulaToken.tArea3)
					{
						num = 2;
						continue;
					}
					return 3;
				case 6:
					if (A_0 != FormulaToken.tArea2)
					{
						num = 1;
						continue;
					}
					return 2;
				}
				break;
			}
		}
		return 2;
		IL_74:
		IL_B8:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶圸强堼䜾", a_));
	}

	// Token: 0x060046FB RID: 18171 RVA: 0x002AFB38 File Offset: 0x002AEB38
	public virtual int ᜁ(ExcelVersion A_0)
	{
		int a_ = 3;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7F;
				case 1:
					switch (A_0)
					{
					case ExcelVersion.Version97to2003:
						return 9;
					case ExcelVersion.Version2007:
					case ExcelVersion.Version2010:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_59;
						}
						break;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_59:
		if (true)
		{
		}
		if (false)
		{
		}
		return 19;
		IL_7F:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("伸帺似䰾⡀ⱂ⭄", a_));
	}

	// Token: 0x060046FC RID: 18172 RVA: 0x002AFBDC File Offset: 0x002AEBDC
	public virtual string ᜀ(FormulaUtil A_0, int A_1, int A_2, bool A_3, NumberFormatInfo A_4, bool A_5)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				bool flag;
				bool flag2;
				XlsWorkbook xlsWorkbook;
				switch (num)
				{
				case 0:
					if (flag)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				case 1:
					goto IL_233;
				case 2:
					if (flag2)
					{
						num = 13;
						continue;
					}
					goto IL_2E4;
				case 4:
					num = 9;
					continue;
				case 5:
					if (A_3)
					{
						if (true)
						{
						}
						num = 10;
						continue;
					}
					goto IL_1F8;
				case 6:
					num = 5;
					continue;
				case 7:
					goto IL_1F6;
				case 8:
					if (flag2)
					{
						num = 4;
						continue;
					}
					goto IL_215;
				case 9:
					if (A_3)
					{
						num = 7;
						continue;
					}
					goto IL_215;
				case 10:
					goto IL_AA;
				case 11:
					num = 12;
					continue;
				case 12:
					xlsWorkbook = null;
					goto IL_AC;
				case 13:
					goto IL_2C3;
				case 14:
					xlsWorkbook = (XlsWorkbook)A_0.ParentWorkbook;
					goto IL_AC;
				case 15:
					if (flag)
					{
						num = 6;
						continue;
					}
					goto IL_1F8;
				}
				if (A_0 == null)
				{
					num = 11;
					continue;
				}
				num = 14;
				continue;
				IL_AC:
				XlsWorkbook a_2 = xlsWorkbook;
				flag = this.ᜀ(a_2);
				flag2 = this.ᜁ(a_2);
				num = 15;
				continue;
				IL_1F8:
				num = 8;
				continue;
				IL_215:
				num = 0;
			}
			IL_AA:
			return sprᦊ.ᜀ(A_1, RecordTableEnumerator.b("欸", a_), this.ᜋ(), this.ᜏ()) + RecordTableEnumerator.b("̸", a_) + sprᦊ.ᜀ(A_1, RecordTableEnumerator.b("欸", a_), this.ᜉ(), this.ᜏ());
			IL_1F6:
			goto IL_254;
			IL_233:
			(this.ᜋ() + 1).ToString();
			return RecordTableEnumerator.b("ᴸ", a_) + (this.ᜋ() + 1).ToString() + RecordTableEnumerator.b("̸Ἲ", a_) + (this.ᜉ() + 1).ToString();
			IL_254:
			return sprᦊ.ᜀ(A_2, RecordTableEnumerator.b("稸", a_), this.ᜄ(), this.ᜈ()) + RecordTableEnumerator.b("̸", a_) + sprᦊ.ᜀ(A_2, RecordTableEnumerator.b("稸", a_), this.ᜂ(), this.ᜈ());
			IL_2C3:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_254;
			default:
				if (false)
				{
				}
				sprṔ.ᜀ(this.ᜄ() + 1);
				return RecordTableEnumerator.b("ᴸ", a_) + sprṔ.ᜀ(this.ᜄ() + 1) + RecordTableEnumerator.b("̸Ἲ", a_) + sprṔ.ᜀ(this.ᜂ() + 1);
			}
			IL_2E4:
			return sprᦊ.ᜀ(A_1, A_2, this.ᜋ(), this.ᜄ(), this.ᜏ(), this.ᜈ(), A_3) + RecordTableEnumerator.b("̸", a_) + sprᦊ.ᜀ(A_1, A_2, this.ᜉ(), this.ᜂ(), this.ᜇ(), this.ᜌ(), A_3);
		}
		}
	}

	// Token: 0x060046FD RID: 18173 RVA: 0x002AFF24 File Offset: 0x002AEF24
	public virtual byte[] ᜀ(ExcelVersion A_0)
	{
		byte[] array;
		int num;
		for (;;)
		{
			array = base.ToByteArray(A_0);
			num = 1;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_182;
				case 1:
					if (this.ᜁ <= 65535)
					{
						num2 = 14;
						continue;
					}
					goto IL_8D;
				case 2:
					if (A_0 == ExcelVersion.Version97to2003)
					{
						num2 = 13;
						continue;
					}
					num2 = 7;
					continue;
				case 3:
					goto IL_8D;
				case 4:
					if (true)
					{
					}
					goto IL_187;
				case 5:
					goto IL_21D;
				case 6:
					if (this.ᜂ <= 255)
					{
						num2 = 15;
						continue;
					}
					goto IL_8D;
				case 7:
					if (A_0 != ExcelVersion.Version2007)
					{
						num2 = 10;
						continue;
					}
					goto IL_187;
				case 8:
					num2 = 1;
					continue;
				case 9:
					if (this.ᜀ <= 65535)
					{
						num2 = 8;
						continue;
					}
					goto IL_8D;
				case 10:
					IL_8B:
					num2 = 11;
					continue;
				case 11:
					if (A_0 == ExcelVersion.Version2010)
					{
						num2 = 4;
						continue;
					}
					goto IL_272;
				case 12:
					goto IL_120;
				case 13:
					num2 = 9;
					continue;
				case 14:
					num2 = 6;
					continue;
				case 15:
					num2 = 16;
					continue;
				case 16:
					if (this.ᜄ > 255)
					{
						num2 = 3;
						continue;
					}
					goto IL_120;
				}
				break;
				IL_187:
				BitConverter.GetBytes(this.ᜀ).CopyTo(array, num);
				num += 4;
				BitConverter.GetBytes(this.ᜁ).CopyTo(array, num);
				num += 4;
				BitConverter.GetBytes(this.ᜂ).CopyTo(array, num);
				num += 4;
				array[num++] = this.ᜃ;
				BitConverter.GetBytes(this.ᜄ).CopyTo(array, num);
				num += 4;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8B;
				default:
					if (false)
					{
					}
					num2 = 5;
					continue;
				}
				IL_8D:
				FormulaToken formulaToken = this.ᜅ();
				array[0] = (byte)formulaToken;
				num2 = 12;
				continue;
				IL_120:
				BitConverter.GetBytes((ushort)this.ᜀ).CopyTo(array, num);
				num += 2;
				BitConverter.GetBytes((ushort)this.ᜁ).CopyTo(array, num);
				num += 2;
				array[num++] = (byte)this.ᜂ;
				array[num++] = this.ᜃ;
				array[num++] = (byte)this.ᜄ;
				num2 = 0;
			}
		}
		IL_182:
		IL_21D:
		IL_272:
		array[num] = this.ᜅ;
		return array;
	}

	// Token: 0x060046FE RID: 18174 RVA: 0x002B01B0 File Offset: 0x002AF1B0
	public virtual Ptg ᜀ(int A_0, int A_1, XlsWorkbook A_2)
	{
		switch (0)
		{
		default:
		{
			sprᲔ sprᲔ;
			int num2;
			int num4;
			int num5;
			int num6;
			for (;;)
			{
				sprᲔ = (sprᲔ)base.Offset(A_0, A_1, A_2);
				int num = 7;
				for (;;)
				{
					int num3;
					int num7;
					int num8;
					int num9;
					switch (num)
					{
					case 0:
						if (num2 > A_2.MaxColumnCount - 1)
						{
							num = 26;
							continue;
						}
						goto IL_381;
					case 1:
						num = 16;
						continue;
					case 2:
						num3 = this.ᜄ();
						goto IL_128;
					case 3:
						num = 10;
						continue;
					case 4:
						num = 2;
						continue;
					case 5:
						num = 31;
						continue;
					case 6:
						num = 13;
						continue;
					case 7:
						if (!this.ᜏ())
						{
							num = 5;
							continue;
						}
						num = 24;
						continue;
					case 8:
						num = 25;
						continue;
					case 9:
						if (num4 <= A_2.MaxRowCount - 1)
						{
							num = 3;
							continue;
						}
						goto IL_34B;
					case 10:
						if (num2 >= 0)
						{
							num = 19;
							continue;
						}
						goto IL_34B;
					case 11:
						if (num5 <= A_2.MaxRowCount - 1)
						{
							num = 8;
							continue;
						}
						goto IL_34B;
					case 12:
						num = 21;
						continue;
					case 13:
						if (num6 <= A_2.MaxColumnCount - 1)
						{
							num = 12;
							continue;
						}
						goto IL_34B;
					case 14:
						if (num5 >= 0)
						{
							num = 23;
							continue;
						}
						goto IL_34B;
					case 15:
						num7 = this.ᜉ() + A_0;
						goto IL_301;
					case 16:
						num8 = this.ᜂ();
						goto IL_2CA;
					case 17:
						num7 = this.ᜉ();
						goto IL_301;
					case 18:
						num3 = this.ᜄ() + A_1;
						goto IL_128;
					case 19:
						num = 0;
						continue;
					case 20:
						num8 = this.ᜂ() + A_1;
						goto IL_2CA;
					case 21:
						if (num4 >= 0)
						{
							num = 22;
							continue;
						}
						goto IL_34B;
					case 22:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E7;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 23:
						num = 11;
						continue;
					case 24:
						num9 = this.ᜋ() + A_0;
						goto IL_276;
					case 25:
						if (num6 >= 0)
						{
							num = 6;
							continue;
						}
						goto IL_34B;
					case 26:
						goto IL_20C;
					case 27:
						if (!this.ᜈ())
						{
							num = 4;
							continue;
						}
						goto IL_E7;
					case 28:
						num = 17;
						continue;
					case 29:
						if (!this.ᜌ())
						{
							num = 1;
							continue;
						}
						if (true)
						{
						}
						num = 20;
						continue;
					case 30:
						if (!this.ᜇ())
						{
							num = 28;
							continue;
						}
						num = 15;
						continue;
					case 31:
						num9 = this.ᜋ();
						goto IL_276;
					}
					break;
					IL_E7:
					num = 18;
					continue;
					IL_128:
					num6 = num3;
					num = 30;
					continue;
					IL_276:
					num5 = num9;
					num = 27;
					continue;
					IL_2CA:
					num2 = num8;
					num = 14;
					continue;
					IL_301:
					num4 = num7;
					num = 29;
				}
			}
			IL_20C:
			IL_34B:
			FormulaToken a_ = this.ᜅ();
			return FormulaUtil.ᜀ(a_, new object[]
			{
				this
			});
			IL_381:
			sprᲔ.ᜄ(num5);
			sprᲔ.ᜃ(num6);
			sprᲔ.ᜁ(num4);
			sprᲔ.ᜅ(num2);
			return sprᲔ;
		}
		}
	}

	// Token: 0x060046FF RID: 18175 RVA: 0x002B055C File Offset: 0x002AF55C
	public virtual Ptg ᜀ(int A_0, int A_1, int A_2, int A_3, Rectangle A_4, int A_5, Rectangle A_6, out bool A_7, XlsWorkbook A_8)
	{
		sprᲔ sprᲔ;
		for (;;)
		{
			sprᲔ = (sprᲔ)base.Offset(A_0, A_1, A_2, A_3, A_4, A_5, A_6, out A_7, A_8);
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_3 != A_5)
					{
						num = 3;
						continue;
					}
					return sprᲔ;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return sprᲔ;
					default:
						if (false)
						{
						}
						if (A_0 == A_5)
						{
							num = 5;
							continue;
						}
						return sprᲔ;
					}
					break;
				case 2:
					if (A_0 == A_3)
					{
						num = 4;
						continue;
					}
					num = 1;
					continue;
				case 3:
					num = 6;
					continue;
				case 4:
					goto IL_6D;
				case 5:
					num = 0;
					continue;
				case 6:
					if (Ptg.RectangleContains(A_6, A_1, A_2))
					{
						num = 7;
						continue;
					}
					return sprᲔ;
				case 7:
					goto IL_90;
				}
				break;
			}
		}
		IL_6D:
		return this.ᜂ(A_3, A_4, A_5, A_6, ref A_7, A_8);
		IL_90:
		int a_ = A_6.Top - A_4.Top;
		int a_2 = A_6.Left - A_4.Left;
		A_7 = true;
		return sprᲔ.ᜀ(sprᲔ, A_3, A_4, A_5, a_, a_2);
	}

	// Token: 0x06004700 RID: 18176 RVA: 0x002B06A0 File Offset: 0x002AF6A0
	public virtual Ptg ᜁ(IWorkbook A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u2596 spr_u;
			int a_2;
			int a_3;
			short a_4;
			for (;;)
			{
				FormulaToken a_ = spr\u2596.ᜀ(this.ᜀ());
				spr_u = (spr\u2596)FormulaUtil.ᜁ(a_);
				bool flag = this.ᜀ(A_0);
				bool flag2 = this.ᜁ(A_0);
				int num = 19;
				for (;;)
				{
					int num2;
					short num3;
					int num4;
					switch (num)
					{
					case 0:
						goto IL_170;
					case 1:
						num2 = this.ᜉ() - A_1;
						goto IL_1FF;
					case 2:
						num3 = (short)(this.ᜄ() - A_2);
						goto IL_27E;
					case 3:
						num3 = (short)this.ᜄ();
						goto IL_27E;
					case 4:
						if (this.ᜌ())
						{
							num = 8;
							continue;
						}
						goto IL_1E7;
					case 5:
						num = 6;
						continue;
					case 6:
						if (flag2)
						{
							num = 0;
							continue;
						}
						num = 1;
						continue;
					case 7:
						if (flag)
						{
							num = 16;
							continue;
						}
						num = 2;
						continue;
					case 8:
						num = 12;
						continue;
					case 9:
						goto IL_1F3;
					case 10:
						num2 = this.ᜉ();
						goto IL_1FF;
					case 11:
						num = 7;
						continue;
					case 12:
						if (flag)
						{
							num = 18;
							continue;
						}
						num = 14;
						continue;
					case 13:
						num4 = this.ᜋ();
						goto IL_1C0;
					case 14:
						goto IL_190;
					case 15:
						num4 = this.ᜋ() - A_1;
						goto IL_1C0;
					case 16:
						goto IL_269;
					case 17:
						goto IL_15C;
					case 18:
						goto IL_1E7;
					case 19:
						if (this.ᜏ())
						{
							num = 20;
							continue;
						}
						goto IL_15C;
					case 20:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_190;
						default:
							if (false)
							{
							}
							num = 22;
							continue;
						}
						break;
					case 21:
						if (this.ᜇ())
						{
							num = 5;
							continue;
						}
						goto IL_170;
					case 22:
						if (flag2)
						{
							num = 17;
							continue;
						}
						num = 15;
						continue;
					case 23:
						if (this.ᜈ())
						{
							num = 11;
							continue;
						}
						goto IL_269;
					}
					break;
					IL_15C:
					num = 13;
					continue;
					IL_170:
					num = 10;
					continue;
					IL_1C0:
					a_2 = num4;
					num = 23;
					continue;
					IL_1E7:
					num = 9;
					continue;
					IL_1FF:
					a_3 = num2;
					num = 4;
					continue;
					IL_269:
					num = 3;
					continue;
					IL_27E:
					a_4 = num3;
					num = 21;
				}
			}
			IL_190:
			if (true)
			{
			}
			short num5 = (short)(this.ᜂ() - A_2);
			goto IL_2A8;
			IL_1F3:
			num5 = (short)this.ᜂ();
			IL_2A8:
			short a_5 = num5;
			spr_u.ᜄ(a_2);
			spr_u.ᜁ(a_4);
			spr_u.ᜁ(a_3);
			spr_u.ᜀ(a_5);
			spr_u.ᜀ(this.\u170D());
			spr_u.ᜁ(this.ᜐ());
			return spr_u;
		}
		}
	}

	// Token: 0x06004701 RID: 18177 RVA: 0x002B0990 File Offset: 0x002AF990
	public sprᲔ ᜄ(bool A_0)
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				int num;
				int num2;
				UtilityMethods.ᜀ(out num, out num2, ExcelVersion.Version2007);
				int num3;
				int num4;
				UtilityMethods.ᜀ(out num3, out num4, ExcelVersion.Version97to2003);
				int num5 = 21;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_334;
						default:
							if (false)
							{
							}
							this.ᜁ(num - 1);
							num5 = 23;
							continue;
						}
						break;
					case 1:
						num5 = 22;
						continue;
					case 2:
						if (this.ᜄ() == 0)
						{
							num5 = 25;
							continue;
						}
						goto IL_11C;
					case 3:
						if (this.ᜉ() == num3 - 1)
						{
							num5 = 0;
							continue;
						}
						return this;
					case 4:
						num5 = 24;
						continue;
					case 5:
						if (this.ᜋ() <= num3)
						{
							num5 = 8;
							continue;
						}
						goto IL_334;
					case 6:
						num5 = 2;
						continue;
					case 7:
						goto IL_2D9;
					case 8:
						num5 = 17;
						continue;
					case 9:
						if (this.ᜋ() == 0)
						{
							num5 = 4;
							continue;
						}
						goto IL_2DE;
					case 10:
						this.ᜁ(num3 - 1);
						num5 = 12;
						continue;
					case 11:
						num5 = 5;
						continue;
					case 12:
						goto IL_297;
					case 13:
						goto IL_117;
					case 14:
						num5 = 3;
						continue;
					case 15:
						if (this.ᜂ() == num2 - 1)
						{
							num5 = 16;
							continue;
						}
						goto IL_11C;
					case 16:
						this.ᜅ(num4 - 1);
						num5 = 7;
						continue;
					case 17:
						if (this.ᜉ() > num3)
						{
							num5 = 13;
							continue;
						}
						return this;
					case 18:
						goto IL_263;
					case 19:
						if (this.ᜋ() == 0)
						{
							num5 = 14;
							continue;
						}
						return this;
					case 20:
						if (this.ᜄ() <= num4)
						{
							num5 = 1;
							continue;
						}
						goto IL_334;
					case 21:
						if (A_0)
						{
							num5 = 6;
							continue;
						}
						num5 = 27;
						continue;
					case 22:
						if (this.ᜂ() <= num4)
						{
							num5 = 11;
							continue;
						}
						goto IL_334;
					case 23:
						goto IL_27D;
					case 24:
						if (this.ᜉ() == num - 1)
						{
							num5 = 10;
							continue;
						}
						goto IL_2DE;
					case 25:
						num5 = 15;
						continue;
					case 26:
						if (this.ᜂ() == num4 - 1)
						{
							num5 = 29;
							continue;
						}
						goto IL_144;
					case 27:
						if (this.ᜄ() == 0)
						{
							num5 = 28;
							continue;
						}
						goto IL_144;
					case 28:
						num5 = 26;
						continue;
					case 29:
						this.ᜅ(num2 - 1);
						num5 = 18;
						continue;
					}
					break;
					IL_11C:
					num5 = 9;
					continue;
					IL_144:
					num5 = 19;
					continue;
					IL_2DE:
					num5 = 20;
				}
			}
			IL_117:
			goto IL_334;
			IL_263:
			IL_27D:
			IL_297:
			IL_2D9:
			return this;
			IL_334:
			return this.ᜃ();
		}
	}

	// Token: 0x06004702 RID: 18178 RVA: 0x002B0CDC File Offset: 0x002AFCDC
	private Ptg ᜀ(sprᲔ A_0, int A_1, Rectangle A_2, int A_3, int A_4, int A_5)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			for (;;)
			{
				num = A_1;
				bool flag = this.ᜄ(A_2);
				num2 = A_0.ᜋ();
				num3 = A_0.ᜄ();
				num4 = A_0.ᜉ();
				num5 = A_0.ᜂ();
				if (true)
				{
				}
				int num6 = 0;
				for (;;)
				{
					switch (num6)
					{
					case 0:
						if (flag)
						{
							num6 = 2;
							continue;
						}
						goto IL_B9;
					case 1:
						goto IL_B7;
					case 2:
						num = A_3;
						num2 += A_4;
						num3 += A_5;
						num4 += A_4;
						num5 += A_5;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num6 = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_B7:
			IL_B9:
			FormulaToken a_ = spr\u1BFD.ᜀ(this.ᜀ());
			return FormulaUtil.ᜀ(a_, new object[]
			{
				num,
				num2,
				num3,
				num4,
				num5,
				this.ᜃ,
				this.ᜅ
			});
		}
		}
	}

	// Token: 0x06004703 RID: 18179 RVA: 0x002B0E14 File Offset: 0x002AFE14
	private bool ᜄ(Rectangle A_0)
	{
		if (Ptg.RectangleContains(A_0, this.ᜋ(), this.ᜄ()))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_36;
				}
			}
			IL_36:
			if (false)
			{
			}
			return Ptg.RectangleContains(A_0, this.ᜉ(), this.ᜂ());
		}
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06004704 RID: 18180 RVA: 0x002B0E7C File Offset: 0x002AFE7C
	private Ptg ᜀ(int A_0, int A_1, int A_2, int A_3, ref bool A_4, XlsWorkbook A_5)
	{
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ += A_2;
				this.ᜁ += A_2;
				this.ᜂ += A_3;
				this.ᜄ += A_3;
				A_4 = true;
				num = 8;
				continue;
			case 1:
				num = 4;
				continue;
			case 2:
				if (this.ᜁ + A_2 <= A_5.MaxRowCount - 1)
				{
					num = 7;
					continue;
				}
				goto IL_94;
			case 3:
				goto IL_1F2;
			case 4:
				if (this.ᜂ + A_3 >= 0)
				{
					num = 9;
					continue;
				}
				goto IL_94;
			case 5:
				if (A_0 == A_1)
				{
					num = 0;
					continue;
				}
				goto IL_DF;
			case 6:
				goto IL_214;
			case 7:
				num = 3;
				continue;
			case 8:
				return this;
			case 9:
				num = 2;
				continue;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1F2:
				if (this.ᜄ + A_3 > A_5.MaxColumnCount - 1)
				{
					num = 6;
				}
				else
				{
					num = 5;
				}
				break;
			default:
				if (false)
				{
				}
				if (this.ᜁ + A_2 < 0)
				{
					goto IL_94;
				}
				num = 1;
				break;
			}
		}
		IL_94:
		FormulaToken a_ = this.ᜅ();
		return FormulaUtil.ᜀ(a_, this.ToString(A_5.FormulaUtil), A_5);
		IL_DF:
		A_4 = true;
		FormulaToken a_2 = spr\u1BFD.ᜀ(this.ᜀ());
		return FormulaUtil.ᜀ(a_2, new object[]
		{
			A_1,
			this.ᜀ + A_2,
			this.ᜂ + A_3,
			this.ᜁ + A_2,
			this.ᜄ + A_3,
			this.ᜃ,
			this.ᜅ
		});
		IL_214:
		goto IL_94;
	}

	// Token: 0x06004705 RID: 18181 RVA: 0x002B10A4 File Offset: 0x002B00A4
	private Ptg ᜁ(int A_0, int A_1, int A_2, int A_3, ref bool A_4, IWorkbook A_5)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			for (;;)
			{
				num = this.ᜀ + A_2;
				num2 = this.ᜂ + A_3;
				int num3 = 7;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_21B;
					case 1:
						if (num <= A_5.MaxRowCount - 1)
						{
							num3 = 14;
							continue;
						}
						goto IL_1E3;
					case 2:
						if (num2 > A_5.MaxColumnCount - 1)
						{
							num3 = 0;
							continue;
						}
						num3 = 5;
						continue;
					case 3:
						this.ᜀ = num;
						this.ᜂ = num2;
						A_4 = true;
						num3 = 8;
						continue;
					case 4:
						if (num2 >= 0)
						{
							num3 = 9;
							continue;
						}
						goto IL_1E3;
					case 5:
						if (num <= this.ᜁ)
						{
							num3 = 12;
							continue;
						}
						return this;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A1;
						default:
							if (false)
							{
							}
							if (num2 > this.ᜄ)
							{
								num3 = 13;
								continue;
							}
							num3 = 11;
							continue;
						}
						break;
					case 7:
						if (num >= 0)
						{
							num3 = 10;
							continue;
						}
						goto IL_1E3;
					case 8:
						return this;
					case 9:
						num3 = 1;
						continue;
					case 10:
						num3 = 4;
						continue;
					case 11:
						if (A_0 == A_1)
						{
							goto IL_A1;
						}
						goto IL_168;
					case 12:
						num3 = 6;
						continue;
					case 13:
						goto IL_EC;
					case 14:
						if (true)
						{
						}
						num3 = 2;
						continue;
					}
					break;
					IL_A1:
					num3 = 3;
				}
			}
			IL_EC:
			return this;
			IL_168:
			A_4 = true;
			FormulaToken a_ = spr\u1BFD.ᜀ(this.ᜀ());
			return FormulaUtil.ᜀ(a_, new object[]
			{
				A_1,
				num,
				num2,
				this.ᜁ,
				this.ᜄ,
				this.ᜃ,
				this.ᜅ
			});
			IL_1E3:
			FormulaToken a_2 = this.ᜅ();
			return FormulaUtil.ᜀ(a_2, this.ToString());
			IL_21B:
			goto IL_1E3;
		}
		}
	}

	// Token: 0x06004706 RID: 18182 RVA: 0x002B12F8 File Offset: 0x002B02F8
	private Ptg ᜀ(int A_0, int A_1, int A_2, int A_3, ref bool A_4, IWorkbook A_5)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			for (;;)
			{
				num = this.ᜁ + A_2;
				num2 = this.ᜄ + A_3;
				int num3 = 8;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						num3 = 1;
						continue;
					case 1:
						if (num2 >= 0)
						{
							num3 = 2;
							continue;
						}
						goto IL_1E3;
					case 2:
						num3 = 7;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A1;
						default:
							if (false)
							{
							}
							if (num2 < this.ᜂ)
							{
								num3 = 14;
								continue;
							}
							num3 = 4;
							continue;
						}
						break;
					case 4:
						if (A_0 == A_1)
						{
							goto IL_A1;
						}
						goto IL_168;
					case 5:
						return this;
					case 6:
						num3 = 3;
						continue;
					case 7:
						if (num <= A_5.MaxRowCount - 1)
						{
							num3 = 11;
							continue;
						}
						goto IL_1E3;
					case 8:
						if (num >= 0)
						{
							num3 = 0;
							continue;
						}
						goto IL_1E3;
					case 9:
						goto IL_21B;
					case 10:
						if (num2 > A_5.MaxColumnCount - 1)
						{
							num3 = 9;
							continue;
						}
						num3 = 13;
						continue;
					case 11:
						num3 = 10;
						continue;
					case 12:
						this.ᜁ = num;
						this.ᜄ = num2;
						A_4 = true;
						if (true)
						{
						}
						num3 = 5;
						continue;
					case 13:
						if (num >= this.ᜀ)
						{
							num3 = 6;
							continue;
						}
						return this;
					case 14:
						goto IL_EC;
					}
					break;
					IL_A1:
					num3 = 12;
				}
			}
			IL_EC:
			return this;
			IL_168:
			A_4 = true;
			FormulaToken a_ = spr\u1BFD.ᜀ(this.ᜀ());
			return FormulaUtil.ᜀ(a_, new object[]
			{
				A_1,
				this.ᜀ,
				this.ᜂ,
				num,
				num2,
				this.ᜃ,
				this.ᜅ
			});
			IL_1E3:
			FormulaToken a_2 = this.ᜅ();
			return FormulaUtil.ᜀ(a_2, this.ToString());
			IL_21B:
			goto IL_1E3;
		}
		}
	}

	// Token: 0x06004707 RID: 18183 RVA: 0x002B154C File Offset: 0x002B054C
	private bool ᜃ(Rectangle A_0)
	{
		if (Ptg.RectangleContains(A_0, this.ᜋ(), this.ᜄ()))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_36;
				}
			}
			IL_36:
			if (true)
			{
			}
			if (false)
			{
			}
			return Ptg.RectangleContains(A_0, this.ᜋ(), this.ᜂ());
		}
		return false;
	}

	// Token: 0x06004708 RID: 18184 RVA: 0x002B15B4 File Offset: 0x002B05B4
	private bool ᜂ(Rectangle A_0)
	{
		if (Ptg.RectangleContains(A_0, this.ᜉ(), this.ᜄ()))
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_36;
				}
			}
			IL_36:
			if (true)
			{
			}
			if (false)
			{
			}
			return Ptg.RectangleContains(A_0, this.ᜉ(), this.ᜂ());
		}
		return false;
	}

	// Token: 0x06004709 RID: 18185 RVA: 0x002B161C File Offset: 0x002B061C
	private bool ᜁ(Rectangle A_0)
	{
		if (!Ptg.RectangleContains(A_0, this.ᜋ(), this.ᜄ()))
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
				return false;
			}
		}
		if (true)
		{
		}
		return Ptg.RectangleContains(A_0, this.ᜉ(), this.ᜄ());
	}

	// Token: 0x0600470A RID: 18186 RVA: 0x002B1684 File Offset: 0x002B0684
	private bool ᜀ(Rectangle A_0)
	{
		if (!Ptg.RectangleContains(A_0, this.ᜋ(), this.ᜂ()))
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
				return false;
			}
		}
		if (true)
		{
		}
		return Ptg.RectangleContains(A_0, this.ᜉ(), this.ᜂ());
	}

	// Token: 0x0600470B RID: 18187 RVA: 0x002B16EC File Offset: 0x002B06EC
	private Ptg ᜂ(int A_0, Rectangle A_1, int A_2, Rectangle A_3, ref bool A_4, XlsWorkbook A_5)
	{
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			for (;;)
			{
				num = A_3.Top - A_1.Top;
				num2 = A_3.Left - A_1.Left;
				int num3 = 15;
				for (;;)
				{
					bool flag;
					bool flag2;
					bool flag3;
					bool flag4;
					bool flag5;
					bool flag6;
					switch (num3)
					{
					case 0:
						goto IL_136;
					case 1:
						return this;
					case 2:
						num3 = 14;
						continue;
					case 3:
						goto IL_462;
					case 4:
						if (A_0 == A_2)
						{
							num3 = 28;
							continue;
						}
						goto IL_269;
					case 5:
						if (num2 == 0)
						{
							num3 = 0;
							continue;
						}
						goto IL_3F9;
					case 6:
						if (num == 0)
						{
							num3 = 25;
							continue;
						}
						goto IL_138;
					case 7:
						if (A_0 == A_2)
						{
							num3 = 37;
							continue;
						}
						goto IL_3F9;
					case 8:
						num3 = 36;
						continue;
					case 9:
						flag = Ptg.RectangleContains(A_3, this.ᜉ(), this.ᜂ());
						goto IL_1EE;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_136;
						default:
							if (false)
							{
							}
							num3 = 16;
							continue;
						}
						break;
					case 11:
						if (!flag2)
						{
							num3 = 3;
							continue;
						}
						goto IL_118;
					case 12:
						num3 = 35;
						continue;
					case 13:
						if (!flag3)
						{
							num3 = 24;
							continue;
						}
						goto IL_118;
					case 14:
						flag = Ptg.RectangleContains(A_1, this.ᜉ(), this.ᜂ());
						goto IL_1EE;
					case 15:
						if (num == 0)
						{
							num3 = 8;
							continue;
						}
						goto IL_269;
					case 16:
						flag4 = Ptg.RectangleContains(A_3, this.ᜋ(), this.ᜄ());
						goto IL_41C;
					case 17:
						if (flag5)
						{
							num3 = 31;
							continue;
						}
						return this;
					case 18:
						if (A_0 == A_2)
						{
							num3 = 29;
							continue;
						}
						goto IL_138;
					case 19:
						flag4 = Ptg.RectangleContains(A_1, this.ᜋ(), this.ᜄ());
						goto IL_41C;
					case 20:
						goto IL_3F4;
					case 21:
						if (num >= 0)
						{
							num3 = 2;
							continue;
						}
						num3 = 9;
						continue;
					case 22:
						if (this.ᜋ() == 0)
						{
							num3 = 1;
							continue;
						}
						goto IL_307;
					case 23:
						num3 = 4;
						continue;
					case 24:
						num3 = 26;
						continue;
					case 25:
						num3 = 18;
						continue;
					case 26:
						if (!flag6)
						{
							num3 = 33;
							continue;
						}
						goto IL_118;
					case 27:
						if (num >= 0)
						{
							num3 = 10;
							continue;
						}
						num3 = 19;
						continue;
					case 28:
						return this;
					case 29:
						goto IL_303;
					case 30:
						if (!flag3)
						{
							if (true)
							{
							}
							num3 = 12;
							continue;
						}
						goto IL_212;
					case 31:
						num3 = 11;
						continue;
					case 32:
						num3 = 22;
						continue;
					case 33:
						num3 = 34;
						continue;
					case 34:
						if (A_5.MaxRowCount - 1 == this.ᜉ())
						{
							num3 = 32;
							continue;
						}
						goto IL_307;
					case 35:
						if (flag6)
						{
							num3 = 20;
							continue;
						}
						return this;
					case 36:
						if (num2 == 0)
						{
							num3 = 23;
							continue;
						}
						goto IL_269;
					case 37:
						goto IL_1C8;
					}
					break;
					IL_118:
					num3 = 5;
					continue;
					IL_136:
					num3 = 7;
					continue;
					IL_138:
					num3 = 30;
					continue;
					IL_1EE:
					flag6 = flag;
					num3 = 13;
					continue;
					IL_269:
					num3 = 27;
					continue;
					IL_307:
					Rectangle rect = Rectangle.FromLTRB(this.ᜄ(), this.ᜋ(), this.ᜂ(), this.ᜉ());
					flag5 = Ptg.RectangleContains(rect, A_3.Top, rect.Left);
					flag2 = Ptg.RectangleContains(rect, A_3.Bottom, rect.Right);
					num3 = 17;
					continue;
					IL_3F9:
					num3 = 6;
					continue;
					IL_41C:
					flag3 = flag4;
					num3 = 21;
				}
			}
			return this;
			IL_1C8:
			return this.ᜁ(A_0, A_1, num, A_3, ref A_4, A_5);
			IL_212:
			return this.ᜀ(A_0, A_2, num, num2, ref A_4, A_5);
			IL_303:
			return this.ᜀ(A_0, A_1, num2, A_3, ref A_4, A_5);
			IL_3F4:
			goto IL_212;
			IL_462:
			return this;
		}
		}
	}

	// Token: 0x0600470C RID: 18188 RVA: 0x002B1B64 File Offset: 0x002B0B64
	private Ptg ᜁ(int A_0, Rectangle A_1, int A_2, Rectangle A_3, ref bool A_4, XlsWorkbook A_5)
	{
		switch (0)
		{
		default:
		{
			int num = 34;
			for (;;)
			{
				bool flag;
				bool flag2;
				bool flag3;
				bool flag4;
				Rectangle a;
				Rectangle rectangle;
				switch (num)
				{
				case 0:
					if (A_2 >= 0)
					{
						num = 12;
						continue;
					}
					num = 1;
					continue;
				case 1:
					flag = this.ᜂ(A_3);
					goto IL_394;
				case 2:
					goto IL_134;
				case 3:
					goto IL_108;
				case 4:
					return this;
				case 5:
					goto IL_27E;
				case 6:
					num = 23;
					continue;
				case 7:
					num = 21;
					continue;
				case 8:
					if (flag2)
					{
						num = 10;
						continue;
					}
					num = 33;
					continue;
				case 9:
					if (flag2)
					{
						num = 11;
						continue;
					}
					goto IL_17B;
				case 10:
					goto IL_196;
				case 11:
					num = 32;
					continue;
				case 12:
					num = 35;
					continue;
				case 13:
					flag2 = !this.ᜃ(A_3);
					num = 14;
					continue;
				case 14:
					goto IL_306;
				case 15:
					num = 17;
					continue;
				case 16:
					flag3 = this.ᜃ(A_1);
					goto IL_2C2;
				case 17:
					flag3 = this.ᜃ(A_3);
					goto IL_2C2;
				case 18:
					num = 27;
					continue;
				case 19:
					this.ᜁ((int)((ushort)(A_3.Top - 1)));
					num = 29;
					continue;
				case 20:
					if (this.ᜀ >= A_1.Y)
					{
						num = 22;
						continue;
					}
					flag2 = false;
					num = 30;
					continue;
				case 21:
					if (A_2 > 0)
					{
						num = 18;
						continue;
					}
					goto IL_306;
				case 22:
					if (true)
					{
					}
					flag2 = true;
					num = 2;
					continue;
				case 23:
					if (this.ᜉ() != A_3.Bottom)
					{
						num = 24;
						continue;
					}
					return this;
				case 24:
					num = 28;
					continue;
				case 25:
					goto IL_23F;
				case 26:
					if (A_1.X <= A_3.X)
					{
						num = 7;
						continue;
					}
					goto IL_306;
				case 27:
					if (!flag2)
					{
						num = 13;
						continue;
					}
					goto IL_306;
				case 28:
					if (this.ᜋ() == A_3.Top)
					{
						num = 25;
						continue;
					}
					num = 31;
					continue;
				case 29:
					return this;
				case 30:
					goto IL_134;
				case 31:
					if (A_2 < 0)
					{
						num = 19;
						continue;
					}
					this.ᜄ((int)((ushort)(A_3.Bottom + 1)));
					num = 4;
					continue;
				case 32:
					if (!flag4)
					{
						goto IL_17B;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_108;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 33:
					if (flag4)
					{
						num = 36;
						continue;
					}
					rectangle = Rectangle.Intersect(a, A_3);
					num = 3;
					continue;
				case 35:
					flag = this.ᜂ(A_1);
					goto IL_394;
				case 36:
					goto IL_210;
				}
				if (A_2 >= 0)
				{
					num = 15;
					continue;
				}
				num = 16;
				continue;
				IL_108:
				if (!rectangle.IsEmpty)
				{
					num = 6;
					continue;
				}
				return this;
				IL_134:
				num = 0;
				continue;
				IL_17B:
				num = 8;
				continue;
				IL_2C2:
				flag2 = flag3;
				num = 26;
				continue;
				IL_306:
				num = 20;
				continue;
				IL_394:
				flag4 = flag;
				a = Rectangle.FromLTRB(this.ᜄ(), this.ᜋ(), this.ᜂ(), this.ᜉ());
				num = 9;
			}
			IL_196:
			return this.ᜃ(A_0, A_2, A_1, A_3, ref A_4, A_5);
			IL_210:
			return this.ᜂ(A_0, A_2, A_1, A_3, ref A_4, A_5);
			IL_23F:
			return this;
			IL_27E:
			return this.ᜀ(A_0, A_0, A_2, 0, ref A_4, A_5);
		}
		}
	}

	// Token: 0x0600470D RID: 18189 RVA: 0x002B1F94 File Offset: 0x002B0F94
	private Ptg ᜃ(int A_0, int A_1, Rectangle A_2, Rectangle A_3, ref bool A_4, IWorkbook A_5)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_3.Top <= A_2.Bottom)
				{
					num = 3;
					continue;
				}
				this.ᜄ((int)((ushort)(this.ᜋ() + A_1)));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_73;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 2:
				goto IL_73;
			case 3:
				this.ᜄ((int)((ushort)(A_2.Top + A_1)));
				num = 6;
				continue;
			case 4:
				goto IL_3C;
			case 5:
				return this;
			case 6:
				goto IL_FE;
			case 7:
				if (this.ᜋ() + A_1 <= this.ᜉ())
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				return this;
			}
			if (A_1 < 0)
			{
				num = 4;
				continue;
			}
			num = 7;
			continue;
			IL_73:
			num = 0;
		}
		IL_3C:
		return this.ᜁ(A_0, A_0, A_1, 0, ref A_4, A_5);
		IL_FE:
		return this;
	}

	// Token: 0x0600470E RID: 18190 RVA: 0x002B20A4 File Offset: 0x002B10A4
	private Ptg ᜂ(int A_0, int A_1, Rectangle A_2, Rectangle A_3, ref bool A_4, IWorkbook A_5)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (true)
				{
				}
				goto IL_32;
			case 2:
				goto IL_30;
			case 3:
				return this;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_32;
				default:
					if (false)
					{
					}
					if (this.ᜉ() + A_1 >= this.ᜋ())
					{
						num = 1;
						continue;
					}
					return this;
				}
				break;
			}
			if (A_1 > 0)
			{
				num = 2;
				continue;
			}
			num = 4;
			continue;
			IL_32:
			this.ᜁ((int)((ushort)(this.ᜉ() + A_1)));
			num = 3;
		}
		IL_30:
		return this.ᜀ(A_0, A_0, A_1, 0, ref A_4, A_5);
	}

	// Token: 0x0600470F RID: 18191 RVA: 0x002B2160 File Offset: 0x002B1160
	private Ptg ᜀ(int A_0, Rectangle A_1, int A_2, Rectangle A_3, ref bool A_4, XlsWorkbook A_5)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = this.ᜁ(A_1);
				bool flag2 = this.ᜀ(A_1);
				Rectangle rectangle = Rectangle.FromLTRB(this.ᜄ(), this.ᜋ(), this.ᜂ(), this.ᜉ());
				int num = 25;
				for (;;)
				{
					Rectangle rectangle2;
					switch (num)
					{
					case 0:
						if (A_2 < 0)
						{
							num = 15;
							continue;
						}
						this.ᜃ((int)((byte)(A_3.Right + 1)));
						num = 7;
						continue;
					case 1:
						if (flag2)
						{
							num = 20;
							continue;
						}
						goto IL_133;
					case 2:
						goto IL_2E3;
					case 3:
						goto IL_30D;
					case 4:
						if (!flag2)
						{
							num = 24;
							continue;
						}
						goto IL_182;
					case 5:
						num = 14;
						continue;
					case 6:
						if (!rectangle2.IsEmpty)
						{
							num = 5;
							continue;
						}
						return this;
					case 7:
						goto IL_235;
					case 8:
						goto IL_14E;
					case 9:
						goto IL_17D;
					case 10:
						if (!flag)
						{
							num = 17;
							continue;
						}
						goto IL_182;
					case 11:
						if (flag2)
						{
							num = 16;
							continue;
						}
						num = 10;
						continue;
					case 12:
						num = 1;
						continue;
					case 13:
						if (this.ᜂ() != A_3.Right)
						{
							num = 22;
							continue;
						}
						return this;
					case 14:
						if (this.ᜀ(A_3, rectangle))
						{
							num = 19;
							continue;
						}
						goto IL_CD;
					case 15:
						this.ᜅ((int)((byte)(A_3.Left - 1)));
						num = 9;
						continue;
					case 16:
						goto IL_1F3;
					case 17:
						num = 4;
						continue;
					case 18:
						if (flag)
						{
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17D;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 11;
							continue;
						}
						break;
					case 19:
						num = 23;
						continue;
					case 20:
						goto IL_278;
					case 21:
						if (this.ᜄ() == A_3.Left)
						{
							num = 2;
							continue;
						}
						num = 0;
						continue;
					case 22:
						num = 21;
						continue;
					case 23:
						if (this.ᜁ(A_1, rectangle))
						{
							num = 3;
							continue;
						}
						goto IL_CD;
					case 24:
						return this;
					case 25:
						if (flag)
						{
							num = 12;
							continue;
						}
						goto IL_133;
					}
					break;
					IL_CD:
					num = 13;
					continue;
					IL_133:
					num = 18;
					continue;
					IL_182:
					rectangle2 = Rectangle.Intersect(rectangle, A_3);
					num = 6;
				}
			}
			return this;
			IL_14E:
			return this.ᜁ(A_0, A_2, A_1, A_3, ref A_4, A_5);
			IL_17D:
			return this;
			IL_1F3:
			return this.ᜀ(A_0, A_2, A_1, A_3, ref A_4, A_5);
			IL_235:
			return this;
			IL_278:
			return this.ᜀ(A_0, A_0, 0, A_2, ref A_4, A_5);
			IL_2E3:
			return this;
			IL_30D:
			return this.ᜃ();
		}
	}

	// Token: 0x06004710 RID: 18192 RVA: 0x002B2484 File Offset: 0x002B1484
	private bool ᜁ(Rectangle A_0, Rectangle A_1)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0.Left <= A_1.Right)
				{
					num = 3;
					continue;
				}
				return true;
			case 1:
				num = 2;
				continue;
			case 2:
				goto IL_B4;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B4;
				default:
					goto IL_63;
				}
				break;
			case 5:
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (A_0.Top <= A_1.Bottom)
			{
				num = 1;
				continue;
			}
			return true;
			IL_B4:
			if (A_0.Bottom < A_1.Top)
			{
				return true;
			}
			num = 5;
		}
		IL_63:
		if (false)
		{
		}
		return A_0.Right < A_1.Left;
	}

	// Token: 0x06004711 RID: 18193 RVA: 0x002B2564 File Offset: 0x002B1564
	private bool ᜀ(Rectangle A_0, Rectangle A_1)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_AF;
			case 1:
				num = 2;
				continue;
			case 2:
				if (A_0.Top <= A_1.Top)
				{
					num = 5;
					continue;
				}
				goto IL_CC;
			case 3:
				num = 0;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_AF;
				default:
					goto IL_5B;
				}
				break;
			}
			if (A_0.Left <= A_1.Left)
			{
				num = 3;
				continue;
			}
			goto IL_CC;
			IL_AF:
			if (A_0.Right < A_1.Right)
			{
				goto IL_CC;
			}
			num = 1;
		}
		IL_5B:
		if (false)
		{
		}
		return A_0.Bottom >= A_1.Bottom;
		IL_CC:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06004712 RID: 18194 RVA: 0x002B2648 File Offset: 0x002B1648
	private Ptg ᜁ(int A_0, int A_1, Rectangle A_2, Rectangle A_3, ref bool A_4, IWorkbook A_5)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				goto IL_DF;
			case 2:
				this.ᜃ((int)((byte)(A_2.Right + 1)));
				num = 1;
				continue;
			case 3:
				if (A_3.Left <= A_2.Right)
				{
					goto IL_79;
				}
				this.ᜃ((int)((byte)(this.ᜄ() + A_1)));
				num = 4;
				continue;
			case 4:
				return this;
			case 6:
				goto IL_46;
			case 7:
				if (true)
				{
				}
				if (this.ᜄ() + A_1 <= this.ᜂ())
				{
					num = 0;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_79;
				default:
					goto IL_F7;
				}
				break;
			}
			if (A_1 < 0)
			{
				num = 6;
				continue;
			}
			num = 7;
			continue;
			IL_79:
			num = 2;
		}
		IL_46:
		return this.ᜁ(A_0, A_0, 0, A_1, ref A_4, A_5);
		IL_DF:
		return this;
		IL_F7:
		if (false)
		{
		}
		return this;
	}

	// Token: 0x06004713 RID: 18195 RVA: 0x002B2754 File Offset: 0x002B1754
	private Ptg ᜀ(int A_0, int A_1, Rectangle A_2, Rectangle A_3, ref bool A_4, IWorkbook A_5)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (A_3.Right <= A_2.Left)
				{
					if (true)
					{
					}
					num = 7;
					continue;
				}
				this.ᜅ((int)((byte)(this.ᜂ() + A_1)));
				num = 6;
				continue;
			case 2:
				goto IL_63;
			case 3:
				num = 1;
				continue;
			case 4:
				goto IL_49;
			case 5:
				if (this.ᜂ() + A_1 >= this.ᜄ())
				{
					num = 3;
					continue;
				}
				return this;
			case 6:
				return this;
			case 7:
				this.ᜅ((int)((byte)(A_2.Left + 1)));
				num = 2;
				continue;
			}
			if (A_1 > 0)
			{
				num = 4;
			}
			else
			{
				num = 5;
			}
		}
		IL_49:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_63:
			break;
		default:
			if (false)
			{
			}
			return this.ᜀ(A_0, A_0, 0, A_1, ref A_4, A_5);
		}
		return this;
	}

	// Token: 0x06004714 RID: 18196 RVA: 0x002B286C File Offset: 0x002B186C
	public IXLSRange ᜀ(IWorkbook A_0, IWorksheet A_1)
	{
		int a_ = 19;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜄ() > this.ᜂ())
				{
					num = 3;
					continue;
				}
				goto IL_C6;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_84;
				}
				break;
			case 3:
			{
				int a_2 = this.ᜂ();
				this.ᜅ(this.ᜄ());
				this.ᜃ(a_2);
				num = 1;
				continue;
			}
			case 4:
				goto IL_4A;
			}
			if (true)
			{
			}
			if (A_1 == null)
			{
				num = 4;
			}
			else
			{
				num = 0;
			}
		}
		IL_4A:
		throw new ArgumentNullException(RecordTableEnumerator.b("㩈⍊⡌⩎═", a_));
		IL_84:
		if (false)
		{
		}
		IL_C6:
		return A_1[this.ᜋ() + 1, this.ᜄ() + 1, this.ᜉ() + 1, this.ᜂ() + 1];
	}

	// Token: 0x06004715 RID: 18197 RVA: 0x002B2968 File Offset: 0x002B1968
	public Rectangle ᜎ()
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
		return Rectangle.FromLTRB(this.ᜄ(), this.ᜋ(), this.ᜂ(), this.ᜉ());
	}

	// Token: 0x06004716 RID: 18198 RVA: 0x002B29C0 File Offset: 0x002B19C0
	public Ptg ᜅ(Rectangle A_0)
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
		sprᲔ sprᲔ = (sprᲔ)base.Clone();
		sprᲔ.ᜃ(A_0.Left);
		sprᲔ.ᜅ(A_0.Right);
		sprᲔ.ᜄ(A_0.Top);
		sprᲔ.ᜁ(A_0.Bottom);
		return sprᲔ;
	}

	// Token: 0x06004717 RID: 18199 RVA: 0x002B2A3C File Offset: 0x002B1A3C
	public virtual Ptg ᜊ()
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
		FormulaToken a_ = this.ᜅ();
		return FormulaUtil.ᜀ(a_, new object[]
		{
			this
		});
	}

	// Token: 0x06004718 RID: 18200 RVA: 0x002B2A90 File Offset: 0x002B1A90
	public Ptg ᜂ(int A_0)
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
		int a_ = sprᲔ.ᜀ(this.TokenCode);
		FormulaToken tokenCode = spr\u1BFD.ᜀ(a_);
		return new spr\u1BFD(A_0, this.ᜋ(), this.ᜄ(), this.ᜉ(), this.ᜂ(), this.\u170D(), this.ᜐ())
		{
			TokenCode = tokenCode
		};
	}

	// Token: 0x0400204C RID: 8268
	private int ᜀ;

	// Token: 0x0400204D RID: 8269
	private int ᜁ;

	// Token: 0x0400204E RID: 8270
	private int ᜂ;

	// Token: 0x0400204F RID: 8271
	protected byte ᜃ;

	// Token: 0x04002050 RID: 8272
	private int ᜄ;

	// Token: 0x04002051 RID: 8273
	protected byte ᜅ;
}
