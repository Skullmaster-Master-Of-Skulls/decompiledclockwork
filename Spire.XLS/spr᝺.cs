using System;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000596 RID: 1430
internal class spr\u177A
{
	// Token: 0x060056E5 RID: 22245 RVA: 0x00376C74 File Offset: 0x00375C74
	public spr\u177A() : this(512)
	{
	}

	// Token: 0x060056E6 RID: 22246 RVA: 0x00376C8C File Offset: 0x00375C8C
	public spr\u177A(bool A_0)
	{
		this.ᜅ = true;
		base..ctor();
		this.ᜅ = A_0;
	}

	// Token: 0x060056E7 RID: 22247 RVA: 0x00376CB0 File Offset: 0x00375CB0
	public spr\u177A(int A_0)
	{
		this.ᜅ = true;
		base..ctor();
		this.ᜀ(A_0);
	}

	// Token: 0x060056E8 RID: 22248 RVA: 0x00376CD4 File Offset: 0x00375CD4
	public spr\u177A(byte[] A_0)
	{
		int a_ = 16;
		this.ᜅ = true;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇㡉ࡋ⽍⑏㍑", a_));
		}
		this.ᜀ(A_0.Length);
		this.ᜀ(A_0);
	}

	// Token: 0x060056E9 RID: 22249 RVA: 0x00376D20 File Offset: 0x00375D20
	public void ᜀ(byte A_0)
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
		this.ᜀ(1);
		this.ᜃ[this.ᜄ] = A_0;
		this.ᜄ++;
	}

	// Token: 0x060056EA RID: 22250 RVA: 0x00376D80 File Offset: 0x00375D80
	public void ᜀ(byte[] A_0)
	{
		int a_ = 17;
		int num = 1;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_5C;
			case 2:
				goto IL_AA;
			case 3:
				this.ᜀ(num2);
				Buffer.BlockCopy(A_0, 0, this.ᜃ, this.ᜄ, num2);
				this.ᜄ += num2;
				num = 4;
				continue;
			case 4:
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_AA:
				if (num2 <= 0)
				{
					return;
				}
				num = 3;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 0;
				}
				else
				{
					num2 = A_0.Length;
					num = 2;
				}
				break;
			}
		}
		IL_5C:
		throw new ArgumentNullException(RecordTableEnumerator.b("♆㭈㥊᥌⁎ၐ㝒ㅔ", a_));
	}

	// Token: 0x060056EB RID: 22251 RVA: 0x00376E5C File Offset: 0x00375E5C
	public void ᜀ(spr\u177A A_0)
	{
		int a_ = 5;
		int num = 2;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				this.ᜀ(num2);
				Buffer.BlockCopy(A_0.ᜃ, 0, this.ᜃ, this.ᜄ, num2);
				this.ᜄ += num2;
				num = 3;
				continue;
			case 1:
				goto IL_AA;
			case 3:
				return;
			case 4:
				goto IL_54;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_AA:
				if (num2 <= 0)
				{
					return;
				}
				if (true)
				{
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					num = 4;
				}
				else
				{
					num2 = A_0.ᜁ();
					num = 1;
				}
				break;
			}
		}
		IL_54:
		throw new ArgumentNullException(RecordTableEnumerator.b("场吼䰾㕀", a_));
	}

	// Token: 0x060056EC RID: 22252 RVA: 0x00376F40 File Offset: 0x00375F40
	public void ᜀ(int A_0, byte[] A_1, int A_2, int A_3)
	{
		int a_ = 12;
		int num = 5;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (A_0 >= 0)
				{
					num = 7;
					continue;
				}
				goto IL_6A;
			case 1:
				goto IL_138;
			case 2:
				num = 6;
				continue;
			case 3:
				goto IL_4C;
			case 4:
				goto IL_109;
			case 6:
				if (A_2 + A_3 > num2)
				{
					num = 4;
					continue;
				}
				goto IL_13D;
			case 7:
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
					num = 8;
					continue;
				}
				break;
			case 8:
				if (A_0 + A_3 > this.ᜄ)
				{
					num = 1;
					continue;
				}
				num = 9;
				continue;
			case 9:
				if (A_2 >= 0)
				{
					num = 2;
					continue;
				}
				goto IL_C4;
			}
			IL_41:
			if (A_1 == null)
			{
				num = 3;
				continue;
			}
			num2 = A_1.Length;
			num = 0;
			continue;
			goto IL_41;
		}
		IL_4C:
		throw new ArgumentNullException(RecordTableEnumerator.b("⍁㙃㑅ే⽉㽋㩍", a_));
		IL_6A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁ᝃ㉅⥇㡉㡋ݍ㹏㙑ㅓ⹕", a_));
		IL_C4:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁C⍅㭇㹉Ջ⁍㑏㝑ⱓ", a_));
		IL_109:
		goto IL_C4;
		IL_138:
		goto IL_6A;
		IL_13D:
		Buffer.BlockCopy(this.ᜃ, A_0, A_1, A_2, A_3);
	}

	// Token: 0x060056ED RID: 22253 RVA: 0x0037709C File Offset: 0x0037609C
	public void ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 16;
			byte[] dst;
			for (;;)
			{
				int num2;
				int num3;
				int num4;
				int num5;
				int num6;
				switch (num)
				{
				case 0:
					if (num2 < num3)
					{
						num = 17;
						continue;
					}
					goto IL_1AE;
				case 1:
					goto IL_122;
				case 2:
					if (!this.ᜅ)
					{
						num = 3;
						continue;
					}
					goto IL_1AE;
				case 3:
					num = 15;
					continue;
				case 4:
					num4 = 0;
					goto IL_92;
				case 5:
					goto IL_87;
				case 6:
					if (num5 >= num3)
					{
						num = 9;
						continue;
					}
					num2 = num3;
					num = 2;
					continue;
				case 7:
					num6 = 20;
					goto IL_16D;
				case 8:
					num6 = num5 * 2;
					goto IL_16D;
				case 9:
					return;
				case 10:
					if (true)
					{
					}
					num = 8;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_87;
					default:
						if (false)
						{
						}
						Buffer.BlockCopy(this.ᜃ, 0, dst, 0, num5);
						num = 1;
						continue;
					}
					break;
				case 12:
					num = 5;
					continue;
				case 13:
					if (num5 > 0)
					{
						num = 11;
						continue;
					}
					goto IL_1D6;
				case 14:
					goto IL_1AE;
				case 15:
					if (num5 != 0)
					{
						num = 10;
						continue;
					}
					num = 7;
					continue;
				case 17:
					num2 = num3;
					num = 14;
					continue;
				}
				if (this.ᜃ != null)
				{
					num = 12;
					continue;
				}
				num = 4;
				continue;
				IL_92:
				num5 = num4;
				num3 = this.ᜄ + A_0;
				num = 6;
				continue;
				IL_87:
				num4 = this.ᜃ.Length;
				goto IL_92;
				IL_16D:
				num2 = num6;
				num = 0;
				continue;
				IL_1AE:
				dst = new byte[num2];
				num = 13;
			}
			return;
			IL_122:
			IL_1D6:
			this.ᜃ = dst;
			return;
		}
		}
	}

	// Token: 0x060056EE RID: 22254 RVA: 0x00377288 File Offset: 0x00376288
	internal byte[] ᜀ()
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

	// Token: 0x060056EF RID: 22255 RVA: 0x003772CC File Offset: 0x003762CC
	public int ᜁ()
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

	// Token: 0x0400294B RID: 10571
	private const int ᜀ = 512;

	// Token: 0x0400294C RID: 10572
	private const int ᜁ = 512;

	// Token: 0x0400294D RID: 10573
	private const int ᜂ = 20;

	// Token: 0x0400294E RID: 10574
	private byte[] ᜃ;

	// Token: 0x0400294F RID: 10575
	private int ᜄ;

	// Token: 0x04002950 RID: 10576
	private bool ᜅ;
}
