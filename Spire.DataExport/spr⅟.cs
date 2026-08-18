using System;
using System.Collections;
using System.Reflection;

// Token: 0x020000FE RID: 254
[DefaultMember("Item")]
internal class spr\u215F : IEnumerable
{
	// Token: 0x06000565 RID: 1381 RVA: 0x00034464 File Offset: 0x00033464
	public IEnumerator ᜀ()
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
		return this.ᜀ.GetEnumerator();
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x000344AC File Offset: 0x000334AC
	public int ᜀ(sprḓ A_0)
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
		return this.ᜀ.Add(A_0);
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x000344F4 File Offset: 0x000334F4
	public int ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			IEnumerator enumerator = this.ᜀ();
			int result;
			try
			{
				int num = 3;
				for (;;)
				{
					sprḓ sprḓ;
					switch (num)
					{
					case 0:
						goto IL_DD;
					case 1:
						result = (int)sprḓ.ᜀ();
						num = 4;
						continue;
					case 2:
						num = 0;
						continue;
					case 4:
						goto IL_CF;
					case 5:
						if (string.Compare(sprḓ.ᜁ(), A_0) == 0)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_6F;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
						}
						break;
					case 6:
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						goto IL_6F;
					}
					IL_53:
					num = 6;
					continue;
					goto IL_53;
					IL_6F:
					sprḓ = (sprḓ)enumerator.Current;
					num = 5;
				}
				IL_CF:
				return result;
				IL_DD:
				return -1;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_12E;
						case 1:
							goto IL_124;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_124:
				if (true)
				{
				}
				IL_12E:;
			}
			return result;
		}
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00034644 File Offset: 0x00033644
	public int ᜁ(string A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					if (num >= this.ᜁ())
					{
						num2 = 3;
						continue;
					}
					num2 = 2;
					continue;
				case 1:
					goto IL_6C;
				case 2:
					if (string.Compare(this.ᜀ(num).ᜁ(), A_0) == 0)
					{
						num2 = 5;
						continue;
					}
					num++;
					num2 = 4;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_42;
					default:
						goto IL_A6;
					}
					break;
				case 4:
					goto IL_42;
				case 5:
					return num;
				}
				break;
				IL_6C:
				num2 = 0;
				continue;
				IL_42:
				goto IL_6C;
			}
		}
		return num;
		IL_A6:
		if (false)
		{
		}
		return -1;
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x00034700 File Offset: 0x00033700
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
		return this.ᜀ.Count;
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00034748 File Offset: 0x00033748
	public sprḓ ᜀ(int A_0)
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
		return this.ᜀ[A_0] as sprḓ;
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x00034794 File Offset: 0x00033794
	public void ᜀ(int A_0, sprḓ A_1)
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

	// Token: 0x04000580 RID: 1408
	private ArrayList ᜀ = new ArrayList();
}
