using System;
using System.Collections;
using System.Reflection;

// Token: 0x0200010F RID: 271
[DefaultMember("Item")]
internal class sprᥞ : spr\u2574
{
	// Token: 0x06000638 RID: 1592 RVA: 0x0003C0A0 File Offset: 0x0003B0A0
	public sprᥞ(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0003C0B4 File Offset: 0x0003B0B4
	public int ᜀ(sprᲩ A_0)
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
		return base.ᜁ(A_0);
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0003C0F8 File Offset: 0x0003B0F8
	public void ᜁ(int A_0, sprᲩ A_1)
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
		base.ᜁ(A_0, A_1);
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0003C13C File Offset: 0x0003B13C
	public void ᜀ(sprḗ A_0)
	{
		IEnumerator enumerator = base.ᜇ();
		try
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_68:
				num = 0;
				break;
			default:
				if (false)
				{
				}
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (!enumerator.MoveNext())
					{
						num = 2;
						continue;
					}
					sprᲩ sprᲩ = (sprᲩ)enumerator.Current;
					sprᲩ.ᜀ(A_0);
					num = 3;
					continue;
				}
				case 2:
					num = 4;
					continue;
				case 4:
					goto IL_8A;
				}
				break;
			}
			goto IL_68;
			IL_8A:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						disposable.Dispose();
						num = 2;
						continue;
					case 1:
						if (disposable != null)
						{
							num = 0;
							continue;
						}
						goto IL_CC;
					case 2:
						goto IL_CA;
					}
					break;
				}
			}
			IL_CA:
			IL_CC:;
		}
		if (true)
		{
		}
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0003C238 File Offset: 0x0003B238
	public new sprᲩ ᜀ(int A_0)
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
		return base.ᜀ(A_0) as sprᲩ;
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0003C280 File Offset: 0x0003B280
	public void ᜀ(int A_0, sprᲩ A_1)
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

	// Token: 0x0600063E RID: 1598 RVA: 0x0003C2C4 File Offset: 0x0003B2C4
	public int ᜁ()
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 0;
			IEnumerator enumerator = base.ᜇ();
			try
			{
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_8F:
					num2 = 2;
					break;
				default:
					if (false)
					{
					}
					num2 = 0;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 2:
					{
						if (!enumerator.MoveNext())
						{
							num2 = 3;
							continue;
						}
						sprᲩ sprᲩ = (sprᲩ)enumerator.Current;
						num += sprᲩ.ᜁ();
						num2 = 1;
						continue;
					}
					case 3:
						num2 = 4;
						continue;
					case 4:
						goto IL_B4;
					}
					break;
				}
				goto IL_8F;
				IL_B4:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_F8;
						case 1:
							disposable.Dispose();
							num2 = 0;
							continue;
						case 2:
							if (disposable != null)
							{
								num2 = 1;
								continue;
							}
							goto IL_FA;
						}
						break;
					}
				}
				IL_F8:
				IL_FA:;
			}
			return num;
		}
		}
	}
}
