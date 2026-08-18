using System;
using System.Text.RegularExpressions;

// Token: 0x02000007 RID: 7
internal class f
{
	// Token: 0x06000029 RID: 41 RVA: 0x00003760 File Offset: 0x00001960
	private f()
	{
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003774 File Offset: 0x00001974
	public static f a(string A_0)
	{
		f f;
		for (;;)
		{
			Match match;
			int num;
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
				f = new f();
				f.a = 0;
				f.b = 0;
				match = global::a.a.Match(A_0);
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (match.Success)
					{
						num = 2;
						continue;
					}
					return f;
				case 1:
					num = 0;
					continue;
				case 2:
					f.a = int.Parse(match.Groups[1].Value);
					f.b = int.Parse(match.Groups[2].Value);
					num = 4;
					continue;
				case 3:
					if (match != null)
					{
						num = 1;
						continue;
					}
					return f;
				case 4:
					return f;
				}
				break;
			}
		}
		return f;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00003868 File Offset: 0x00001A68
	internal int b()
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
		return this.a;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x000038AC File Offset: 0x00001AAC
	internal int a()
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
		return this.b;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000038F0 File Offset: 0x00001AF0
	public int a(int A_0, int A_1)
	{
		for (;;)
		{
			int num = this.b() - A_0;
			if (num != 0)
			{
				return num;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2E;
			}
		}
		IL_2E:
		if (false)
		{
		}
		if (true)
		{
		}
		return this.a() - A_1;
	}

	// Token: 0x04000010 RID: 16
	private int a;

	// Token: 0x04000011 RID: 17
	private int b;
}
