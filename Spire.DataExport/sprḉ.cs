using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000067 RID: 103
[DefaultMember("Item")]
internal class sprḉ : IEnumerable
{
	// Token: 0x06000357 RID: 855 RVA: 0x0001FA58 File Offset: 0x0001EA58
	public sprḉ(spr\u2504 A_0)
	{
		this.ᜂ = A_0;
		this.ᜂ.ᜀ(this);
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0001FA8C File Offset: 0x0001EA8C
	public IEnumerator ᜁ()
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

	// Token: 0x06000359 RID: 857 RVA: 0x0001FAD4 File Offset: 0x0001EAD4
	public int ᜀ(sprặ A_0)
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
		this.ᜁ = false;
		return this.ᜀ.Add(A_0);
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0001FB24 File Offset: 0x0001EB24
	public bool ᜀ(sprᶀ A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			bool flag;
			for (;;)
			{
				int num2;
				int num4;
				switch (num)
				{
				case 0:
				{
					flag = true;
					int num3;
					num2 = num3;
					num = 3;
					continue;
				}
				case 1:
				{
					if (num2 > num4)
					{
						num = 10;
						continue;
					}
					int num3 = num2 + num4 >> 1;
					int num5 = this.ᜀ(num3).ᜆ().ᜀ(A_0);
					num = 9;
					continue;
				}
				case 2:
				{
					int num3;
					num2 = num3 + 1;
					num = 6;
					continue;
				}
				case 3:
					goto IL_F2;
				case 4:
					goto IL_F2;
				case 5:
					if (flag)
					{
						num = 8;
						continue;
					}
					A_1 = num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9F;
					default:
						if (false)
						{
						}
						num = 13;
						continue;
					}
					break;
				case 6:
					goto IL_F2;
				case 8:
					A_1 = this.ᜀ(num2).ᜂ();
					num = 14;
					continue;
				case 9:
				{
					if (true)
					{
					}
					int num5;
					if (num5 < 0)
					{
						num = 2;
						continue;
					}
					int num3;
					num4 = num3 - 1;
					num = 11;
					continue;
				}
				case 10:
					num = 5;
					continue;
				case 11:
				{
					int num5;
					if (num5 == 0)
					{
						num = 0;
						continue;
					}
					goto IL_F2;
				}
				case 12:
					goto IL_9F;
				case 13:
					return flag;
				case 14:
					return flag;
				case 15:
					this.ᜀ();
					num = 12;
					continue;
				}
				if (!this.ᜁ)
				{
					num = 15;
					continue;
				}
				IL_9F:
				flag = false;
				num2 = 0;
				num4 = this.ᜀ.Count - 1;
				num = 4;
				continue;
				IL_F2:
				num = 1;
			}
			return flag;
		}
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0001FCFC File Offset: 0x0001ECFC
	public void ᜂ()
	{
		this.ᜀ.Clear();
		IEnumerator enumerator = this.ᜂ.ᜇ();
		try
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_77;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						goto IL_95;
					}
					break;
				case 3:
				{
					if (!enumerator.MoveNext())
					{
						num = 0;
						continue;
					}
					sprặ a_ = (sprặ)enumerator.Current;
					this.ᜀ(a_);
					num = 2;
					continue;
				}
				}
				IL_5D:
				num = 3;
				continue;
				goto IL_5D;
				IL_77:
				num = 1;
			}
			IL_95:
			if (false)
			{
			}
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
							num = 1;
							continue;
						}
						goto IL_DD;
					case 1:
						disposable.Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_DB;
					}
					break;
				}
			}
			IL_DB:
			IL_DD:;
		}
		if (true)
		{
		}
		this.ᜀ();
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0001FE10 File Offset: 0x0001EE10
	public void ᜃ()
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
		this.ᜀ.Clear();
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0001FE58 File Offset: 0x0001EE58
	public void ᜀ()
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
		this.ᜀ.Sort(new sprḉ.ᜀ());
		this.ᜁ = true;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0001FEAC File Offset: 0x0001EEAC
	public sprặ ᜀ(int A_0)
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
		return this.ᜀ[A_0] as sprặ;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0001FEF8 File Offset: 0x0001EEF8
	public void ᜀ(int A_0, sprặ A_1)
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

	// Token: 0x04000262 RID: 610
	private ArrayList ᜀ = new ArrayList();

	// Token: 0x04000263 RID: 611
	private bool ᜁ;

	// Token: 0x04000264 RID: 612
	private spr\u2504 ᜂ;

	// Token: 0x02000068 RID: 104
	private class ᜀ : IComparer
	{
		// Token: 0x06000360 RID: 864 RVA: 0x0001FF40 File Offset: 0x0001EF40
		int IComparer.ᜀ(object A_0, object A_1)
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
			return (A_0 as sprặ).ᜆ().ᜀ((A_1 as sprặ).ᜆ());
		}
	}
}
