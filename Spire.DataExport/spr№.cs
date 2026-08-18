using System;
using System.Collections;

// Token: 0x0200004C RID: 76
internal class spr\u2116 : sprᠺ
{
	// Token: 0x06000270 RID: 624 RVA: 0x000168EC File Offset: 0x000158EC
	public spr\u2116(ushort A_0, ushort A_1, ushort A_2) : base(A_1, A_2)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00016914 File Offset: 0x00015914
	public override void ᜀ(sprḗ A_0)
	{
		sprᮌ.ᜀ(this.ᜀ, base.ᜆ(), base.ᜄ(), base.ᜅ() + this.ᜁ, A_0);
		IEnumerator enumerator = this.ᜂ.ᜂ();
		try
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (enumerator.MoveNext())
					{
						if (true)
						{
						}
						sprᠺ sprᠺ = (sprᠺ)enumerator.Current;
						sprᠺ.ᜀ(A_0);
						num = 2;
						continue;
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
						num = 3;
						continue;
					}
					break;
				case 3:
					num = 4;
					continue;
				case 4:
					goto IL_BC;
				}
				IL_7E:
				num = 1;
				continue;
				IL_57:
				goto IL_7E;
				goto IL_57;
			}
			IL_BC:;
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
						goto IL_FE;
					case 2:
						goto IL_FC;
					}
					break;
				}
			}
			IL_FC:
			IL_FE:;
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00016A3C File Offset: 0x00015A3C
	public override void ᜀ(byte[] A_0, ref int A_1)
	{
		sprᮌ.ᜀ(this.ᜀ, base.ᜆ(), base.ᜄ(), base.ᜅ() + this.ᜁ, A_0, ref A_1);
		IEnumerator enumerator = this.ᜂ.ᜂ();
		try
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 2:
					num = 4;
					continue;
				case 3:
					if (enumerator.MoveNext())
					{
						sprᠺ sprᠺ = (sprᠺ)enumerator.Current;
						sprᠺ.ᜀ(A_0, ref A_1);
						num = 1;
						continue;
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
					break;
				case 4:
					goto IL_B6;
				}
				IL_78:
				num = 3;
				continue;
				IL_58:
				goto IL_78;
				goto IL_58;
			}
			IL_B6:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F6;
					case 1:
						disposable.Dispose();
						num = 0;
						continue;
					case 2:
						if (disposable != null)
						{
							num = 1;
							continue;
						}
						goto IL_F8;
					}
					break;
				}
			}
			IL_F6:
			IL_F8:;
		}
		if (true)
		{
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00016B64 File Offset: 0x00015B64
	public override int ᜀ()
	{
		switch (0)
		{
		default:
		{
			int num = sizeof(spr\u1CC5);
			IEnumerator enumerator = this.ᜂ.ᜂ();
			try
			{
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (enumerator.MoveNext())
						{
							sprᠺ sprᠺ = (sprᠺ)enumerator.Current;
							num += sprᠺ.ᜀ();
							num2 = 4;
							continue;
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
							num2 = 3;
							continue;
						}
						break;
					case 2:
						goto IL_AC;
					case 3:
						num2 = 2;
						continue;
					}
					IL_6B:
					num2 = 0;
					continue;
					IL_49:
					goto IL_6B;
					goto IL_49;
				}
				IL_AC:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (disposable != null)
							{
								num2 = 2;
								continue;
							}
							goto IL_F2;
						case 1:
							goto IL_F0;
						case 2:
							disposable.Dispose();
							num2 = 1;
							continue;
						}
						break;
					}
				}
				IL_F0:
				IL_F2:;
			}
			if (true)
			{
			}
			return num;
		}
		}
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00016C88 File Offset: 0x00015C88
	public ushort ᜂ()
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

	// Token: 0x06000275 RID: 629 RVA: 0x00016CCC File Offset: 0x00015CCC
	public void ᜀ(ushort A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x00016D10 File Offset: 0x00015D10
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
		return this.ᜁ;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00016D54 File Offset: 0x00015D54
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
		this.ᜁ = A_0;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00016D98 File Offset: 0x00015D98
	public sprᣏ ᜃ()
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

	// Token: 0x040000BE RID: 190
	private new ushort ᜀ;

	// Token: 0x040000BF RID: 191
	private int ᜁ;

	// Token: 0x040000C0 RID: 192
	private sprᣏ ᜂ = new sprᣏ();
}
