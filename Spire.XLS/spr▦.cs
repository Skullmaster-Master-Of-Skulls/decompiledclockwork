using System;
using System.Drawing;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000568 RID: 1384
[spr\u2593(TBIFFRecord.MergeCells)]
[CLSCompliant(false)]
internal class spr\u25A6 : BiffRecordRaw
{
	// Token: 0x06005343 RID: 21315 RVA: 0x0033F038 File Offset: 0x0033E038
	public ushort ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x06005344 RID: 21316 RVA: 0x0033F07C File Offset: 0x0033E07C
	public new spr\u25A6.ᜀ[] ᜃ()
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

	// Token: 0x06005345 RID: 21317 RVA: 0x0033F0C0 File Offset: 0x0033E0C0
	public void ᜀ(spr\u25A6.ᜀ[] A_0)
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
		this.ᜄ = A_0;
		this.ᜃ = (ushort)this.ᜄ.Length;
	}

	// Token: 0x06005346 RID: 21318 RVA: 0x0033F114 File Offset: 0x0033E114
	public virtual int ᜂ()
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

	// Token: 0x06005347 RID: 21319 RVA: 0x0033F150 File Offset: 0x0033E150
	public spr\u25A6()
	{
	}

	// Token: 0x06005348 RID: 21320 RVA: 0x0033F164 File Offset: 0x0033E164
	public spr\u25A6(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06005349 RID: 21321 RVA: 0x0033F17C File Offset: 0x0033E17C
	public spr\u25A6(int A_0) : base(A_0)
	{
	}

	// Token: 0x0600534A RID: 21322 RVA: 0x0033F190 File Offset: 0x0033E190
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			this.ᜃ = A_0.ReadUInt16(A_1);
			A_1 += 2;
			this.ᜄ = new spr\u25A6.ᜀ[(int)this.ᜃ];
			this.ᜀ();
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_4D;
				case 1:
					goto IL_4D;
				case 2:
					if (num >= (int)this.ᜃ)
					{
						num2 = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						this.ᜄ[num] = new spr\u25A6.ᜀ((int)A_0.ReadUInt16(A_1), (int)A_0.ReadUInt16(A_1 + 2), (int)A_0.ReadUInt16(A_1 + 4), (int)A_0.ReadUInt16(A_1 + 6));
						num++;
						A_1 += 8;
						break;
					}
					num2 = 1;
					continue;
				case 3:
					return;
				}
				break;
				IL_4D:
				num2 = 2;
			}
		}
	}

	// Token: 0x0600534B RID: 21323 RVA: 0x0033F27C File Offset: 0x0033E27C
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			A_0.WriteUInt16(A_1, this.ᜃ);
			this.m_iLength = this.GetStoreSize(A_2);
			A_1 += 2;
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= (int)this.ᜃ)
					{
						num2 = 0;
						continue;
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
						A_0.WriteUInt16(A_1, (ushort)this.ᜄ[num].ᜂ());
						A_0.WriteUInt16(A_1 + 2, (ushort)this.ᜄ[num].ᜇ());
						A_0.WriteUInt16(A_1 + 4, (ushort)this.ᜄ[num].ᜅ());
						A_0.WriteUInt16(A_1 + 6, (ushort)this.ᜄ[num].ᜃ());
						num++;
						A_1 += 8;
						break;
					}
					num2 = 3;
					continue;
				case 2:
					if (true)
					{
					}
					goto IL_4B;
				case 3:
					goto IL_4B;
				}
				break;
				IL_4B:
				num2 = 1;
			}
		}
	}

	// Token: 0x0600534C RID: 21324 RVA: 0x0033F38C File Offset: 0x0033E38C
	private void ᜀ()
	{
		int a_ = 13;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A6;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_8F;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 3:
				goto IL_8F;
			}
			if (true)
			{
			}
			if (this.m_iLength == (int)(this.ᜃ * 8 + 2))
			{
				num = 2;
				continue;
			}
			break;
			IL_8F:
			if ((this.m_iLength - 2) % 8 == 0)
			{
				return;
			}
			num = 0;
		}
		IL_73:
		throw new sprῩ(RecordTableEnumerator.b("โ⁄㕆⹈⹊์⩎㵐㽒♔Ֆ㱘㡚㉜ⵞՠ", a_));
		IL_A6:
		goto IL_73;
	}

	// Token: 0x0600534D RID: 21325 RVA: 0x0033F444 File Offset: 0x0033E444
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
		return 2 + this.ᜄ.Length * 8;
	}

	// Token: 0x0600534E RID: 21326 RVA: 0x0033F48C File Offset: 0x0033E48C
	public void ᜀ(int A_0, int A_1, spr\u25A6.ᜀ[] A_2)
	{
		int a_ = 4;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_84;
			case 2:
				goto IL_50;
			case 3:
				if ((int)this.ᜃ == A_1)
				{
					goto IL_15E;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 4:
				goto IL_C8;
			case 5:
				num = 7;
				continue;
			case 6:
				goto IL_A4;
			case 7:
			{
				if (true)
				{
				}
				int num2;
				if (A_0 + A_1 > num2)
				{
					num = 1;
					continue;
				}
				num = 3;
				continue;
			}
			case 8:
				if (A_0 < 0)
				{
					num = 6;
					continue;
				}
				num = 9;
				continue;
			case 9:
				if (A_1 >= 0)
				{
					num = 5;
					continue;
				}
				goto IL_52;
			case 10:
				this.ᜄ = new spr\u25A6.ᜀ[A_1];
				this.ᜃ = (ushort)A_1;
				num = 4;
				continue;
			}
			if (A_2 == null)
			{
				num = 2;
			}
			else
			{
				int num2 = A_2.Length;
				num = 8;
			}
		}
		IL_50:
		throw new ArgumentNullException(RecordTableEnumerator.b("嬹主䰽ሿ❁⍃⽅❇⑉㽋", a_));
		IL_52:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匹渻嬽✿⭁⭃⡅㭇ॉ⍋㭍㹏♑", a_));
		IL_84:
		goto IL_52;
		IL_A4:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匹漻䨽ℿぁぃཅ♇⹉⥋㙍", a_));
		IL_C8:
		IL_15E:
		Array.Copy(A_2, A_0, this.ᜄ, 0, A_1);
	}

	// Token: 0x040026FF RID: 9983
	public new const int ᜀ = 1027;

	// Token: 0x04002700 RID: 9984
	private const int ᜁ = 2;

	// Token: 0x04002701 RID: 9985
	private const int ᜂ = 8;

	// Token: 0x04002702 RID: 9986
	[spr\u2429(0, 2)]
	private new ushort ᜃ;

	// Token: 0x04002703 RID: 9987
	private spr\u25A6.ᜀ[] ᜄ;

	// Token: 0x02000569 RID: 1385
	[CLSCompliant(false)]
	internal new class ᜀ : ICloneable
	{
		// Token: 0x0600534F RID: 21327 RVA: 0x0033F608 File Offset: 0x0033E608
		private ᜀ()
		{
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x0033F61C File Offset: 0x0033E61C
		public ᜀ(spr\u25A6.ᜀ A_0) : this(A_0.ᜂ(), A_0.ᜇ(), A_0.ᜅ(), A_0.ᜃ())
		{
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x0033F648 File Offset: 0x0033E648
		public ᜀ(int A_0, int A_1, int A_2, int A_3)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
			this.ᜂ = A_2;
			this.ᜃ = A_3;
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0033F678 File Offset: 0x0033E678
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
			return this.ᜀ;
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0033F6BC File Offset: 0x0033E6BC
		public int ᜇ()
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

		// Token: 0x06005354 RID: 21332 RVA: 0x0033F700 File Offset: 0x0033E700
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

		// Token: 0x06005355 RID: 21333 RVA: 0x0033F744 File Offset: 0x0033E744
		public int ᜅ()
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
			return this.ᜂ;
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x0033F788 File Offset: 0x0033E788
		public int ᜃ()
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
			return this.ᜃ;
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x0033F7CC File Offset: 0x0033E7CC
		public void ᜀ(int A_0)
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

		// Token: 0x06005358 RID: 21336 RVA: 0x0033F810 File Offset: 0x0033E810
		public int ᜆ()
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
			return (this.ᜁ - this.ᜀ + 1) * (this.ᜃ - this.ᜂ + 1);
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0033F86C File Offset: 0x0033E86C
		public void ᜀ(int A_0, int A_1)
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
			this.ᜁ += A_0;
			this.ᜀ += A_0;
			this.ᜂ += A_1;
			this.ᜃ += A_1;
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x0033F8E0 File Offset: 0x0033E8E0
		internal Rectangle ᜁ()
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
			return Rectangle.FromLTRB(this.ᜂ, this.ᜀ, this.ᜃ, this.ᜁ);
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x0033F938 File Offset: 0x0033E938
		public object ᜀ()
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
			return base.MemberwiseClone();
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x0033F97C File Offset: 0x0033E97C
		public static bool ᜀ(spr\u25A6.ᜀ A_0, spr\u25A6.ᜀ A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					goto IL_76;
				case 3:
					goto IL_74;
				case 4:
					if (A_1 == null)
					{
						num = 3;
						continue;
					}
					goto IL_B4;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_74;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 6:
					num = 4;
					continue;
				case 7:
					if (A_0 != null)
					{
						num = 6;
						continue;
					}
					return false;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				IL_76:
				num = 7;
			}
			IL_74:
			return false;
			IL_B4:
			return A_0.Equals(A_1);
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0033FA44 File Offset: 0x0033EA44
		public virtual bool ᜀ(object A_0)
		{
			int a_ = 13;
			int num = 2;
			spr\u25A6.ᜀ ᜀ;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (ᜀ == null)
					{
						goto IL_E3;
					}
					num = 5;
					continue;
				case 1:
					num = 4;
					continue;
				case 3:
					if (this.ᜃ != ᜀ.ᜃ)
					{
						return false;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_E3;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					if (this.ᜀ == ᜀ.ᜀ)
					{
						num = 7;
						continue;
					}
					return false;
				case 5:
					if (this.ᜂ == ᜀ.ᜂ)
					{
						num = 9;
						continue;
					}
					return false;
				case 6:
					goto IL_EE;
				case 7:
					goto IL_125;
				case 8:
					return false;
				case 9:
					num = 3;
					continue;
				}
				if (A_0 == null)
				{
					num = 8;
					continue;
				}
				ᜀ = (A_0 as spr\u25A6.ᜀ);
				num = 0;
				continue;
				IL_E3:
				num = 6;
			}
			return false;
			IL_EE:
			throw new ArgumentException(RecordTableEnumerator.b("ⱂ❄ⵆ", a_));
			IL_125:
			return this.ᜁ == ᜀ.ᜁ;
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x0033FB90 File Offset: 0x0033EB90
		public virtual int ᜄ()
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
			return this.ᜂ.GetHashCode() | this.ᜃ.GetHashCode() | this.ᜁ.GetHashCode() | this.ᜀ.GetHashCode();
		}

		// Token: 0x04002704 RID: 9988
		private int ᜀ;

		// Token: 0x04002705 RID: 9989
		private int ᜁ;

		// Token: 0x04002706 RID: 9990
		private int ᜂ;

		// Token: 0x04002707 RID: 9991
		private int ᜃ;
	}
}
