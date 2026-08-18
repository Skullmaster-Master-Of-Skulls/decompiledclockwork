using System;
using System.Collections;
using System.Reflection;

// Token: 0x0200006C RID: 108
[DefaultMember("Item")]
internal class spr\u21B9 : sprᠪ
{
	// Token: 0x06000379 RID: 889 RVA: 0x000208F8 File Offset: 0x0001F8F8
	public spr\u21B9(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0002090C File Offset: 0x0001F90C
	public int ᜀ(sprẴ A_0)
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
		this.ᜀ = false;
		return base.ᜀ(A_0);
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00020958 File Offset: 0x0001F958
	public void ᜁ(int A_0, sprẴ A_1)
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
		this.ᜀ = false;
		base.ᜁ(A_0, A_1);
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000209A4 File Offset: 0x0001F9A4
	public bool ᜀ(uint A_0, ref int A_1)
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
				IL_166:
				num = 11;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			bool result;
			int num2;
			for (;;)
			{
				int num4;
				int num5;
				switch (num)
				{
				case 1:
				{
					result = true;
					int num3;
					num2 = num3;
					num = 2;
					continue;
				}
				case 2:
					goto IL_153;
				case 3:
					goto IL_153;
				case 4:
					num4 = 1;
					num = 18;
					continue;
				case 5:
					goto IL_132;
				case 6:
					goto IL_132;
				case 7:
				{
					if (num4 < 0)
					{
						num = 13;
						continue;
					}
					int num3;
					num5 = num3 - 1;
					num = 8;
					continue;
				}
				case 8:
					if (true)
					{
					}
					if (num4 == 0)
					{
						num = 1;
						continue;
					}
					goto IL_153;
				case 9:
				{
					int num3;
					if (this.ᜀ(num3).ᜁ() > A_0)
					{
						num = 4;
						continue;
					}
					num4 = 0;
					num = 6;
					continue;
				}
				case 10:
				{
					if (num2 > num5)
					{
						goto IL_166;
					}
					int num3 = num2 + num5 >> 1;
					num = 15;
					continue;
				}
				case 11:
					goto IL_172;
				case 12:
					goto IL_10B;
				case 13:
				{
					int num3;
					num2 = num3 + 1;
					num = 16;
					continue;
				}
				case 14:
					this.ᜀ();
					num = 12;
					continue;
				case 15:
				{
					int num3;
					if (this.ᜀ(num3).ᜁ() < A_0)
					{
						num = 17;
						continue;
					}
					num = 9;
					continue;
				}
				case 16:
					goto IL_153;
				case 17:
					num4 = -1;
					num = 5;
					continue;
				case 18:
					goto IL_132;
				}
				if (!this.ᜀ)
				{
					num = 14;
					continue;
				}
				IL_10B:
				result = false;
				num2 = 0;
				num5 = base.ᜌ() - 1;
				num4 = 0;
				num = 3;
				continue;
				IL_132:
				num = 7;
				continue;
				IL_153:
				num = 10;
			}
			IL_172:
			A_1 = num2;
			return result;
		}
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00020BC8 File Offset: 0x0001FBC8
	public new void ᜀ()
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
		base.ᜀ(new spr\u21B9.ᜀ());
		this.ᜀ = true;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00020C18 File Offset: 0x0001FC18
	public new sprẴ ᜀ(int A_0)
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
		return base.ᜀ(A_0) as sprẴ;
	}

	// Token: 0x0600037F RID: 895 RVA: 0x00020C60 File Offset: 0x0001FC60
	public void ᜀ(int A_0, sprẴ A_1)
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
		base.ᜀ(A_0, A_1);
	}

	// Token: 0x04000271 RID: 625
	private new bool ᜀ;

	// Token: 0x0200006D RID: 109
	private new class ᜀ : IComparer
	{
		// Token: 0x06000380 RID: 896 RVA: 0x00020CA4 File Offset: 0x0001FCA4
		int IComparer.ᜀ(object A_0, object A_1)
		{
			int num = 1;
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
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return 1;
					case 2:
						return -1;
					case 3:
						if ((A_0 as sprẴ).ᜁ() > (A_1 as sprẴ).ᜁ())
						{
							num = 0;
							continue;
						}
						return 0;
					}
					break;
				}
				IL_4E:
				if ((A_0 as sprẴ).ᜁ() < (A_1 as sprẴ).ᜁ())
				{
					num = 2;
					continue;
				}
				num = 3;
				continue;
				goto IL_4E;
			}
			return -1;
		}
	}
}
