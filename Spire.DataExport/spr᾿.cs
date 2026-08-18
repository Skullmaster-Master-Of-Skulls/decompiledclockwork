using System;
using System.Collections;
using System.Reflection;

// Token: 0x0200013C RID: 316
[DefaultMember("Item")]
internal class spr\u1FBF : spr\u2574
{
	// Token: 0x060007BF RID: 1983 RVA: 0x0004DC74 File Offset: 0x0004CC74
	public spr\u1FBF(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x0004DC88 File Offset: 0x0004CC88
	private void ᜀ(object A_0, EventArgs A_1)
	{
		int num = 1;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_13B;
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
					goto IL_129;
				case 2:
					goto IL_13B;
				}
				if (A_0 is spr᱁)
				{
					num = 0;
					continue;
				}
				return;
			}
			IL_129:
			IEnumerator enumerator = base.ᜇ();
			num = 2;
			continue;
			IL_13B:
			try
			{
				num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_DE;
					case 1:
					{
						spr᱁ spr᱁;
						base.ᜀ(spr᱁);
						num = 6;
						continue;
					}
					case 2:
					{
						spr᱁ spr᱁;
						if (spr᱁ == A_0)
						{
							num = 1;
							continue;
						}
						break;
					}
					case 4:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						spr᱁ spr᱁ = (spr᱁)enumerator.Current;
						num = 2;
						continue;
					}
					case 5:
						goto IL_E6;
					case 6:
						goto IL_DE;
					}
					IL_90:
					num = 4;
					continue;
					goto IL_90;
					IL_DE:
					num = 5;
				}
				IL_E6:
				break;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							disposable.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_126;
						case 2:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_128;
						}
						break;
					}
				}
				IL_126:
				IL_128:;
			}
			goto IL_129;
		}
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x0004DDE8 File Offset: 0x0004CDE8
	public int ᜀ(spr᱁ A_0)
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
		int num = base.ᜁ(A_0);
		this.ᜀ(num).ᜀ(new EventHandler(this.ᜀ));
		return num;
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x0004DE44 File Offset: 0x0004CE44
	public void ᜁ(int A_0, spr᱁ A_1)
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
		base.ᜁ(A_0, A_1);
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x0004DE88 File Offset: 0x0004CE88
	public int ᜀ(string A_0)
	{
		int result;
		for (;;)
		{
			result = -1;
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					if (num >= base.ᜌ())
					{
						num2 = 1;
						continue;
					}
					num2 = 4;
					continue;
				case 1:
					return result;
				case 2:
					goto IL_9B;
				case 3:
					goto IL_9B;
				case 4:
					if (string.Compare(A_0, this.ᜀ(num).\u170D()) == 0)
					{
						num2 = 6;
						continue;
					}
					num++;
					num2 = 2;
					continue;
				case 5:
					return result;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
						if (false)
						{
						}
						result = num;
						num2 = 5;
						continue;
					}
					break;
				}
				break;
				IL_9B:
				num2 = 0;
			}
		}
		return result;
	}

	// Token: 0x060007C4 RID: 1988 RVA: 0x0004DF5C File Offset: 0x0004CF5C
	public void ᜀ(sprḗ A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			IEnumerator enumerator = base.ᜇ();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9C;
					case 1:
						goto IL_7A;
					case 2:
						num = 0;
						continue;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 2;
							continue;
						}
						spr᱁ spr᱁ = (spr᱁)enumerator.Current;
						spr᱁.ᜀ(A_0);
						num = 1;
						continue;
					}
					}
					goto IL_53;
					IL_7A:
					num = 3;
					continue;
					IL_53:
					if (true)
					{
					}
					goto IL_7A;
				}
				IL_9C:;
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
							goto IL_DC;
						case 1:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_DE;
						case 2:
							disposable.Dispose();
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_DC:
				IL_DE:;
			}
			break;
		}
		}
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x0004E058 File Offset: 0x0004D058
	public new spr᱁ ᜀ(int A_0)
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
		return base.ᜀ(A_0) as spr᱁;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x0004E0A0 File Offset: 0x0004D0A0
	public void ᜀ(int A_0, spr᱁ A_1)
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
}
