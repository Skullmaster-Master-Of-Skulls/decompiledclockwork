using System;
using System.Reflection;

// Token: 0x0200029E RID: 670
[DefaultMember("Item")]
internal class sprᢐ
{
	// Token: 0x060023B5 RID: 9141 RVA: 0x0024272C File Offset: 0x0024172C
	public sprᢐ(int A_0)
	{
		this.ᜃ = A_0;
	}

	// Token: 0x060023B6 RID: 9142 RVA: 0x00242748 File Offset: 0x00241748
	public int ᜀ()
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

	// Token: 0x060023B7 RID: 9143 RVA: 0x0024278C File Offset: 0x0024178C
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
		this.ᜂ = A_0;
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x002427D0 File Offset: 0x002417D0
	public int ᜁ()
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
		return this.ᜁ;
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x00242814 File Offset: 0x00241814
	public object ᜂ(int A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_0 >= this.ᜁ)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_6B;
				}
				break;
			case 1:
				goto IL_53;
			case 3:
				num = 0;
				continue;
			}
			if (A_0 < 0)
			{
				break;
			}
			num = 3;
		}
		IL_38:
		return null;
		IL_53:
		goto IL_38;
		IL_6B:
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜀ[A_0];
	}

	// Token: 0x060023BA RID: 9146 RVA: 0x002428A4 File Offset: 0x002418A4
	public void ᜀ(int A_0, object A_1)
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
		this.ᜀ[A_0] = A_1;
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x002428E8 File Offset: 0x002418E8
	public object ᜂ()
	{
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
			this.ᜂ--;
			if (this.ᜂ > 0)
			{
				return this.ᜀ[this.ᜂ - 1];
			}
			break;
		}
		return null;
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x00242950 File Offset: 0x00241950
	public object ᜃ()
	{
		for (;;)
		{
			IL_00:
			int num = 1;
			for (;;)
			{
				object[] destinationArray;
				int num2;
				switch (num)
				{
				case 0:
					Array.Copy(this.ᜀ, destinationArray, this.ᜁ);
					num = 3;
					continue;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						num2 = this.ᜁ + this.ᜃ;
						destinationArray = new object[num2];
						num = 4;
						continue;
					}
					break;
				case 3:
					goto IL_4B;
				case 4:
					if (this.ᜀ != null)
					{
						num = 0;
						continue;
					}
					goto IL_4B;
				case 5:
					goto IL_61;
				}
				if (this.ᜂ == this.ᜁ)
				{
					num = 2;
					continue;
				}
				goto IL_DD;
				IL_4B:
				this.ᜁ = num2;
				this.ᜀ = destinationArray;
				num = 5;
			}
		}
		IL_61:
		IL_DD:
		return this.ᜀ[this.ᜂ++];
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x00242A54 File Offset: 0x00241A54
	public void ᜁ(int A_0)
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
		this.ᜀ[A_0] = null;
		Array.Copy(this.ᜀ, A_0 + 1, this.ᜀ, A_0, this.ᜂ - A_0 - 1);
		this.ᜂ--;
	}

	// Token: 0x04002163 RID: 8547
	private object[] ᜀ;

	// Token: 0x04002164 RID: 8548
	private int ᜁ;

	// Token: 0x04002165 RID: 8549
	private int ᜂ;

	// Token: 0x04002166 RID: 8550
	private int ᜃ;
}
