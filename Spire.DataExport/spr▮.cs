using System;
using System.Collections;
using System.Text;

// Token: 0x02000065 RID: 101
internal class spr\u25AE
{
	// Token: 0x0600033F RID: 831 RVA: 0x0001EEAC File Offset: 0x0001DEAC
	public string ᜃ()
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

	// Token: 0x06000340 RID: 832 RVA: 0x0001EEF0 File Offset: 0x0001DEF0
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
		return this.ᜃ;
	}

	// Token: 0x06000341 RID: 833 RVA: 0x0001EF34 File Offset: 0x0001DF34
	public bool ᜈ()
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
		return this.ᜂ.Length == this.ᜃ + 1;
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0001EF84 File Offset: 0x0001DF84
	public spr\u25AE(string A_0)
	{
		this.ᜂ = A_0;
		this.ᜃ = -1;
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0001EFA8 File Offset: 0x0001DFA8
	public void ᜂ()
	{
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (this.ᜁ() != ' ')
				{
					num = 3;
					continue;
				}
				if (true)
				{
				}
				this.ᜀ();
				num = 5;
				continue;
			case 3:
				return;
			case 4:
				if (!this.ᜈ())
				{
					num = 0;
					continue;
				}
				return;
			}
			IL_6C:
			num = 4;
			continue;
			goto IL_6C;
		}
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0001F058 File Offset: 0x0001E058
	public char ᜅ()
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
		return this.ᜂ[this.ᜃ];
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0001F0A4 File Offset: 0x0001E0A4
	public char ᜁ(char A_0)
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
		return this.ᜀ(A_0, true);
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0001F0E8 File Offset: 0x0001E0E8
	public char ᜀ(char A_0, bool A_1)
	{
		int num = 2;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return '@';
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_58;
				case 1:
					this.ᜂ();
					num = 0;
					continue;
				case 3:
					goto IL_71;
				case 4:
					if (this.ᜁ() != A_0)
					{
						num = 3;
						continue;
					}
					goto IL_8D;
				}
				if (A_1)
				{
					num = 1;
					break;
				}
				IL_58:
				num = 4;
				break;
			}
		}
		return '@';
		IL_71:
		return '@';
		IL_8D:
		return this.ᜀ();
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0001F188 File Offset: 0x0001E188
	public char ᜁ(char[] A_0)
	{
		char c;
		for (;;)
		{
			c = '@';
			int num = 0;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_77;
				case 1:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 2:
					return c;
				case 3:
					if (num >= A_0.Length)
					{
						num2 = 2;
						continue;
					}
					c = this.ᜁ(A_0[num]);
					num2 = 4;
					continue;
				case 4:
					if (c == '@')
					{
						num2 = 1;
						continue;
					}
					return c;
				case 5:
					goto IL_2F;
				}
				break;
				IL_2F:
				num2 = 3;
				continue;
				IL_77:
				goto IL_2F;
			}
		}
		return c;
	}

	// Token: 0x06000348 RID: 840 RVA: 0x0001F240 File Offset: 0x0001E240
	public char ᜇ()
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
		char result = this.ᜀ();
		this.ᜂ();
		return result;
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0001F28C File Offset: 0x0001E28C
	public char ᜀ()
	{
		while (this.ᜈ())
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
				return '@';
			}
		}
		this.ᜃ++;
		return this.ᜂ[this.ᜃ];
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0001F2F4 File Offset: 0x0001E2F4
	public char ᜁ()
	{
		for (;;)
		{
			if (true)
			{
			}
			if (this.ᜈ())
			{
				return '@';
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_28;
			}
		}
		IL_28:
		if (false)
		{
		}
		return this.ᜂ[this.ᜃ + 1];
	}

	// Token: 0x0600034B RID: 843 RVA: 0x0001F350 File Offset: 0x0001E350
	public char ᜀ(int A_0)
	{
		while (this.ᜃ + 1 + A_0 < this.ᜂ.Length)
		{
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
				return this.ᜂ[this.ᜃ + 1 + A_0];
			}
		}
		return '@';
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0001F3BC File Offset: 0x0001E3BC
	public string ᜀ(char A_0)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					goto IL_52;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A4;
					default:
						if (false)
						{
						}
						if (this.ᜁ() != A_0)
						{
							num = 2;
							continue;
						}
						goto IL_B0;
					}
					break;
				case 2:
					num = 3;
					continue;
				case 3:
					if (this.ᜈ())
					{
						num = 5;
						continue;
					}
					stringBuilder.Append(this.ᜀ());
					num = 4;
					continue;
				case 4:
					goto IL_A4;
				case 5:
					goto IL_50;
				}
				break;
				IL_52:
				num = 1;
				continue;
				IL_A4:
				goto IL_52;
			}
		}
		IL_50:
		IL_B0:
		this.ᜂ();
		return stringBuilder.ToString();
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0001F488 File Offset: 0x0001E488
	public string ᜀ(char[] A_0)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			ArrayList arrayList = new ArrayList(A_0);
			stringBuilder = new StringBuilder();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_62;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B5;
					default:
						if (false)
						{
						}
						if (!this.ᜈ())
						{
							num = 5;
							continue;
						}
						goto IL_C1;
					}
					break;
				case 2:
					if (arrayList.Contains(this.ᜁ()))
					{
						num = 0;
						continue;
					}
					stringBuilder.Append(this.ᜀ());
					num = 3;
					continue;
				case 3:
					goto IL_B5;
				case 4:
					goto IL_64;
				case 5:
					if (true)
					{
					}
					num = 2;
					continue;
				}
				break;
				IL_64:
				num = 1;
				continue;
				IL_B5:
				goto IL_64;
			}
		}
		IL_62:
		IL_C1:
		this.ᜂ();
		return stringBuilder.ToString();
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0001F564 File Offset: 0x0001E564
	public string ᜆ()
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
		return this.ᜀ(true);
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0001F5A8 File Offset: 0x0001E5A8
	public string ᜀ(bool A_0)
	{
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				IL_77:
				if (true)
				{
				}
				bool flag = false;
				stringBuilder = new StringBuilder();
				char c = this.ᜁ();
				bool flag2 = c == '.';
				for (;;)
				{
					IL_94:
					int num = 12;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1E1;
						case 1:
							if (!char.IsDigit(c))
							{
								num = 5;
								continue;
							}
							goto IL_E7;
						case 2:
							if (!flag2)
							{
								num = 13;
								continue;
							}
							goto IL_E7;
						case 3:
							if (c == '.')
							{
								num = 18;
								continue;
							}
							goto IL_BC;
						case 4:
							if (!flag)
							{
								num = 6;
								continue;
							}
							goto IL_12A;
						case 5:
							num = 2;
							continue;
						case 6:
							goto IL_E7;
						case 7:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_94;
							default:
								if (false)
								{
								}
								if (A_0)
								{
									num = 9;
									continue;
								}
								goto IL_296;
							}
							break;
						case 8:
							goto IL_244;
						case 9:
							this.ᜂ();
							num = 22;
							continue;
						case 10:
							goto IL_12A;
						case 11:
							if (flag2)
							{
								num = 8;
								continue;
							}
							goto IL_12A;
						case 12:
							if (char.IsLetter(c))
							{
								num = 0;
								continue;
							}
							num = 16;
							continue;
						case 13:
							goto IL_12A;
						case 14:
							num = 10;
							continue;
						case 15:
							if (this.ᜁ() != '_')
							{
								num = 14;
								continue;
							}
							goto IL_1E1;
						case 16:
							if (!char.IsDigit(c))
							{
								num = 17;
								continue;
							}
							goto IL_244;
						case 17:
							num = 11;
							continue;
						case 18:
							num = 19;
							continue;
						case 19:
							if (!flag2)
							{
								num = 23;
								continue;
							}
							goto IL_12A;
						case 20:
							if (!char.IsLetterOrDigit(this.ᜁ()))
							{
								num = 21;
								continue;
							}
							goto IL_1E1;
						case 21:
							num = 15;
							continue;
						case 22:
							goto IL_1BC;
						case 23:
							goto IL_BC;
						}
						goto IL_77;
						IL_BC:
						flag2 = (c == '.');
						num = 1;
						continue;
						IL_E7:
						stringBuilder.Append(c);
						this.ᜀ();
						c = this.ᜁ();
						num = 3;
						continue;
						IL_12A:
						num = 7;
						continue;
						IL_1E1:
						stringBuilder.Append(c);
						this.ᜀ();
						c = this.ᜁ();
						num = 20;
						continue;
						IL_244:
						num = 4;
					}
				}
			}
			IL_1BC:
			IL_296:
			return stringBuilder.ToString();
		}
		}
	}

	// Token: 0x0400025B RID: 603
	public const char ᜀ = '@';

	// Token: 0x0400025C RID: 604
	public const int ᜁ = -2147483648;

	// Token: 0x0400025D RID: 605
	private string ᜂ;

	// Token: 0x0400025E RID: 606
	private int ᜃ;
}
