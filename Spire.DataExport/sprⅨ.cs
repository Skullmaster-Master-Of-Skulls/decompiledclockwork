using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000148 RID: 328
[DefaultMember("Item")]
internal class spr\u2168 : spr\u2574
{
	// Token: 0x060007F5 RID: 2037 RVA: 0x0004FE34 File Offset: 0x0004EE34
	public spr\u2168(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x0004FE48 File Offset: 0x0004EE48
	public int ᜀ(spr᥋ A_0)
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
		return base.ᜁ(A_0);
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0004FE94 File Offset: 0x0004EE94
	public void ᜁ(int A_0, spr᥋ A_1)
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

	// Token: 0x060007F8 RID: 2040 RVA: 0x0004FEE0 File Offset: 0x0004EEE0
	public bool ᜀ(int A_0, ref int A_1)
	{
		int num2;
		int num;
		int num3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_AB:
			num = num2 + 1;
			num3 = 8;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num3 = 2;
				break;
			}
			break;
		}
		bool result;
		for (;;)
		{
			int num4;
			int num5;
			switch (num3)
			{
			case 0:
				goto IL_178;
			case 1:
				if (num > num4)
				{
					num3 = 0;
					continue;
				}
				num2 = num + num4 >> 1;
				num3 = 16;
				continue;
			case 3:
				goto IL_138;
			case 4:
				if (true)
				{
				}
				if (num5 == 0)
				{
					num3 = 9;
					continue;
				}
				goto IL_159;
			case 5:
				goto IL_159;
			case 6:
				goto IL_159;
			case 7:
				if (this.ᜀ(num2).ᜃ() > A_0)
				{
					num3 = 13;
					continue;
				}
				num5 = 0;
				num3 = 17;
				continue;
			case 8:
				goto IL_159;
			case 9:
				result = true;
				num = num2;
				num3 = 6;
				continue;
			case 10:
				goto IL_154;
			case 11:
				goto IL_11B;
			case 12:
				if (num5 < 0)
				{
					num3 = 10;
					continue;
				}
				num4 = num2 - 1;
				num3 = 4;
				continue;
			case 13:
				num5 = 1;
				num3 = 14;
				continue;
			case 14:
				goto IL_138;
			case 15:
				this.ᜀ();
				num3 = 11;
				continue;
			case 16:
				if (this.ᜀ(num2).ᜃ() < A_0)
				{
					num3 = 18;
					continue;
				}
				num3 = 7;
				continue;
			case 17:
				goto IL_138;
			case 18:
				num5 = -1;
				num3 = 3;
				continue;
			}
			if (!this.ᜀ)
			{
				num3 = 15;
				continue;
			}
			IL_11B:
			result = false;
			num = 0;
			num4 = base.ᜌ() - 1;
			num5 = 0;
			num3 = 5;
			continue;
			IL_138:
			num3 = 12;
			continue;
			IL_159:
			num3 = 1;
		}
		IL_154:
		goto IL_AB;
		IL_178:
		A_1 = num;
		return result;
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00050108 File Offset: 0x0004F108
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
		base.ᜀ(new spr\u2168.ᜀ());
		this.ᜀ = true;
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00050158 File Offset: 0x0004F158
	public new spr᥋ ᜀ(int A_0)
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
		return base.ᜀ(A_0) as spr᥋;
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x000501A0 File Offset: 0x0004F1A0
	public void ᜀ(int A_0, spr᥋ A_1)
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

	// Token: 0x060007FC RID: 2044 RVA: 0x000501E4 File Offset: 0x0004F1E4
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
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 1:
						goto IL_8E;
					case 2:
						num2 = 1;
						continue;
					case 4:
					{
						if (!enumerator.MoveNext())
						{
							num2 = 2;
							continue;
						}
						spr᥋ spr᥋ = (spr᥋)enumerator.Current;
						num += spr᥋.ᜆ();
						num2 = 0;
						continue;
					}
					}
					IL_69:
					num2 = 4;
					continue;
					goto IL_69;
				}
				IL_8E:;
			}
			finally
			{
				int num2;
				IDisposable disposable;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_DD:
					disposable.Dispose();
					num2 = 0;
					break;
				default:
					if (false)
					{
					}
					goto IL_C1;
				}
				for (;;)
				{
					IL_AE:
					switch (num2)
					{
					case 0:
						goto IL_EE;
					case 1:
						if (disposable != null)
						{
							num2 = 2;
							continue;
						}
						goto IL_F0;
					case 2:
						goto IL_DD;
					}
					goto IL_C1;
				}
				IL_EE:
				IL_F0:
				goto EndFinally_5;
				IL_C1:
				disposable = (enumerator as IDisposable);
				num2 = 1;
				goto IL_AE;
				EndFinally_5:;
			}
			return num;
		}
		}
	}

	// Token: 0x04000634 RID: 1588
	private new bool ᜀ;

	// Token: 0x02000149 RID: 329
	private new class ᜀ : IComparer
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x00050300 File Offset: 0x0004F300
		int IComparer.ᜀ(object A_0, object A_1)
		{
			for (;;)
			{
				IL_00:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if ((A_0 as spr᥋).ᜃ() > (A_1 as spr᥋).ᜃ())
						{
							num = 2;
							continue;
						}
						return 0;
					case 2:
						return 1;
					case 3:
						return -1;
					}
					if ((A_0 as spr᥋).ᜃ() < (A_1 as spr᥋).ᜃ())
					{
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 3;
							break;
						}
					}
					else
					{
						num = 0;
					}
				}
			}
			return -1;
		}
	}
}
