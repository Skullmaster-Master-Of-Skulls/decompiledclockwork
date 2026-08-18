using System;
using System.Collections;
using System.IO;

// Token: 0x020003A5 RID: 933
internal class spr\u23BB : sprụ
{
	// Token: 0x060034D7 RID: 13527 RVA: 0x0030CD34 File Offset: 0x0030BD34
	spr\u239B sprụ.ᜀ(int A_0)
	{
		int num = 0;
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
				case 1:
					goto IL_6B;
				case 2:
					goto IL_5F;
				}
				break;
			}
			IL_4A:
			if (this.ᜀ.Count == 0)
			{
				num = 2;
				continue;
			}
			num = 1;
			continue;
			goto IL_4A;
		}
		IL_5F:
		return null;
		IL_6B:
		return (spr\u239B)this.ᜀ[((A_0 > this.ᜀ.Count) ? this.ᜀ.Count : A_0) - 1];
	}

	// Token: 0x060034D8 RID: 13528 RVA: 0x0030CDE0 File Offset: 0x0030BDE0
	int sprụ.ᜀ(spr\u239B A_0)
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
		this.ᜀ.Add(A_0);
		return this.ᜀ.Count;
	}

	// Token: 0x060034D9 RID: 13529 RVA: 0x0030CE34 File Offset: 0x0030BE34
	internal void ᜀ(BinaryWriter A_0, int A_1)
	{
		IEnumerator enumerator = this.ᜀ.GetEnumerator();
		try
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_75;
			default:
				if (false)
				{
				}
				num = 4;
				break;
			}
			for (;;)
			{
				IL_34:
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					spr\u239B spr_u239B = (spr\u239B)enumerator.Current;
					spr_u239B.ᜇ = (uint)A_1;
					spr_u239B.ᜃ(A_0);
					num = 1;
					continue;
				}
				case 3:
					goto IL_97;
				}
				break;
			}
			goto IL_75;
			IL_97:
			goto IL_DA;
			IL_75:
			num = 2;
			goto IL_34;
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
						goto IL_D7;
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
						goto IL_D9;
					}
					break;
				}
			}
			IL_D7:
			IL_D9:;
		}
		IL_DA:
		if (true)
		{
		}
	}

	// Token: 0x04002876 RID: 10358
	private readonly ArrayList ᜀ = new ArrayList();
}
