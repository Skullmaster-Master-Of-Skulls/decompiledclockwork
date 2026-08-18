using System;
using System.Reflection;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000595 RID: 1429
[DefaultMember("Item")]
internal class spr\u223C
{
	// Token: 0x060056DE RID: 22238 RVA: 0x00376884 File Offset: 0x00375884
	public spr\u223C()
	{
	}

	// Token: 0x060056DF RID: 22239 RVA: 0x00376898 File Offset: 0x00375898
	public spr\u223C(int A_0)
	{
		int a_ = 4;
		base..ctor();
		if (A_0 <= 0)
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("匹缻儽㔿ⱁぃ", a_));
		}
		this.ᜁ = A_0;
		this.ᜀ = new sprᱧ[this.ᜁ];
	}

	// Token: 0x060056E0 RID: 22240 RVA: 0x003768E8 File Offset: 0x003758E8
	public sprᱧ ᜁ(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_48:
			num = 3;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 < this.ᜁ)
				{
					num = 1;
					continue;
				}
				goto IL_80;
			case 1:
				goto IL_7E;
			case 2:
				goto IL_24;
			case 3:
				num = 0;
				continue;
			}
			goto IL_44;
		}
		IL_24:
		if (true)
		{
		}
		IL_44:
		if (A_0 >= 0)
		{
			goto IL_48;
		}
		goto IL_80;
		IL_7E:
		return this.ᜀ[A_0];
		IL_80:
		return null;
	}

	// Token: 0x060056E1 RID: 22241 RVA: 0x00376978 File Offset: 0x00375978
	public void ᜀ(int A_0, sprᱧ A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6E;
			case 2:
				this.ᜂ(A_0 + 1);
				goto IL_66;
			}
			if (true)
			{
			}
			if (this.ᜁ > A_0)
			{
				break;
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
				num = 2;
				continue;
			}
			IL_66:
			num = 0;
		}
		IL_6E:
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x060056E2 RID: 22242 RVA: 0x00376A00 File Offset: 0x00375A00
	public void ᜂ(int A_0)
	{
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
			int num = 1;
			for (;;)
			{
				sprᱧ[] array;
				switch (num)
				{
				case 0:
				{
					int num2;
					this.ᜁ = ((A_0 >= num2) ? A_0 : num2);
					array = new sprᱧ[this.ᜁ];
					num = 2;
					continue;
				}
				case 2:
					if (this.ᜀ != null)
					{
						num = 4;
						continue;
					}
					goto IL_B1;
				case 3:
					goto IL_C3;
				case 4:
					this.ᜀ.CopyTo(array, 0);
					num = 5;
					continue;
				case 5:
					goto IL_B1;
				case 6:
				{
					int num2 = this.ᜁ * 2;
					num = 0;
					continue;
				}
				}
				if (A_0 > this.ᜁ)
				{
					num = 6;
					continue;
				}
				return;
				IL_B1:
				this.ᜀ = array;
				num = 3;
			}
			IL_C3:
			if (true)
			{
			}
			break;
		}
		}
	}

	// Token: 0x060056E3 RID: 22243 RVA: 0x00376AF4 File Offset: 0x00375AF4
	public void ᜀ(int A_0)
	{
		int a_ = 11;
		for (;;)
		{
			IL_09:
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
						if (false)
						{
						}
						if (A_0 < this.ᜁ)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				case 1:
				{
					if (true)
					{
					}
					sprᱧ[] destinationArray = new sprᱧ[A_0];
					Array.Copy(this.ᜀ, 0, destinationArray, 0, A_0);
					this.ᜀ = destinationArray;
					this.ᜁ = A_0;
					num = 4;
					continue;
				}
				case 3:
					goto IL_39;
				case 4:
					return;
				}
				if (A_0 < 0)
				{
					num = 3;
				}
				else
				{
					num = 0;
				}
			}
		}
		IL_39:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀B⩄㉆❈㽊", a_));
	}

	// Token: 0x060056E4 RID: 22244 RVA: 0x00376BCC File Offset: 0x00375BCC
	public void ᜀ(int A_0, int A_1, int A_2)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_97:
			goto IL_5A;
		default:
			if (false)
			{
			}
			goto IL_34;
		}
		int num;
		int num2;
		int num3;
		for (;;)
		{
			IL_1E:
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (num2 >= num3)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				this.ᜀ[num2] = null;
				num2++;
				num = 2;
				continue;
			case 2:
				goto IL_97;
			case 3:
				goto IL_58;
			}
			goto IL_34;
		}
		IL_58:
		goto IL_5A;
		IL_34:
		Array.Copy(this.ᜀ, A_0, this.ᜀ, A_0 + A_1, A_2);
		num2 = A_0;
		num3 = A_0 + A_1;
		num = 3;
		goto IL_1E;
		IL_5A:
		num = 1;
		goto IL_1E;
	}

	// Token: 0x04002949 RID: 10569
	private sprᱧ[] ᜀ;

	// Token: 0x0400294A RID: 10570
	private int ᜁ;
}
