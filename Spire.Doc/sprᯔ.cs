using System;

// Token: 0x020003CA RID: 970
internal class sprᯔ
{
	// Token: 0x06003693 RID: 13971 RVA: 0x003314C4 File Offset: 0x003304C4
	internal sprᯔ(string A_0, string A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x06003694 RID: 13972 RVA: 0x003314E8 File Offset: 0x003304E8
	internal string ᜀ()
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

	// Token: 0x06003695 RID: 13973 RVA: 0x0033152C File Offset: 0x0033052C
	internal string ᜁ()
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

	// Token: 0x06003696 RID: 13974 RVA: 0x00331570 File Offset: 0x00330570
	internal static sprᯔ ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			string a_;
			string text;
			for (;;)
			{
				string[] array = A_0.Split(new char[]
				{
					':'
				});
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (array.Length < 2)
						{
							num = 4;
							continue;
						}
						a_ = array[0].Trim();
						text = array[1].Trim();
						num = 5;
						continue;
					case 1:
						num = 2;
						continue;
					case 2:
						if (!spr\u1CC6.ᜋ(text))
						{
							num = 3;
							continue;
						}
						goto IL_DF;
					case 3:
						goto IL_81;
					case 4:
						goto IL_5B;
					case 5:
						if (!spr\u1CC6.ᜋ(a_))
						{
							goto IL_DD;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DF;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_5B:
			return null;
			IL_81:
			IL_DD:
			return null;
			IL_DF:
			return new sprᯔ(a_, text);
		}
		}
	}

	// Token: 0x040029CE RID: 10702
	private readonly string ᜀ;

	// Token: 0x040029CF RID: 10703
	private readonly string ᜁ;
}
