using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003DB RID: 987
internal class sprᣄ
{
	// Token: 0x06003775 RID: 14197 RVA: 0x0033C960 File Offset: 0x0033B960
	private sprᣄ()
	{
		this.ᜁ = new Dictionary<int, string>();
		this.ᜂ = new Dictionary<int, string>();
	}

	// Token: 0x06003776 RID: 14198 RVA: 0x0033C98C File Offset: 0x0033B98C
	internal static sprᣄ ᜀ()
	{
		int num = 2;
		for (;;)
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
				switch (num)
				{
				case 0:
					sprᣄ.ᜀ = new sprᣄ();
					num = 1;
					continue;
				case 1:
					goto IL_6D;
				}
				break;
			}
			IL_4A:
			if (sprᣄ.ᜀ == null)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_4A;
		}
		IL_6D:
		return sprᣄ.ᜀ;
	}

	// Token: 0x06003777 RID: 14199 RVA: 0x0033CA10 File Offset: 0x0033BA10
	internal Dictionary<int, string> ᜂ()
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

	// Token: 0x06003778 RID: 14200 RVA: 0x0033CA54 File Offset: 0x0033BA54
	internal Dictionary<int, string> ᜁ()
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

	// Token: 0x06003779 RID: 14201 RVA: 0x0033CA98 File Offset: 0x0033BA98
	internal bool ᜀ(string A_0)
	{
		bool result;
		for (;;)
		{
			IL_2A:
			result = false;
			IDictionaryEnumerator dictionaryEnumerator = this.ᜁ.GetEnumerator();
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7D:
				result = true;
				num = 2;
				break;
			default:
				if (false)
				{
				}
				num = 4;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (dictionaryEnumerator.Value.Equals(A_0))
					{
						num = 5;
						continue;
					}
					goto IL_63;
				case 1:
					if (!dictionaryEnumerator.MoveNext())
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				case 2:
					return result;
				case 3:
					return result;
				case 4:
					goto IL_63;
				case 5:
					goto IL_B8;
				}
				goto IL_2A;
				IL_63:
				num = 1;
			}
			IL_B8:
			goto IL_7D;
		}
		return result;
	}

	// Token: 0x040029F5 RID: 10741
	[ThreadStatic]
	private static sprᣄ ᜀ;

	// Token: 0x040029F6 RID: 10742
	private Dictionary<int, string> ᜁ;

	// Token: 0x040029F7 RID: 10743
	private Dictionary<int, string> ᜂ;
}
