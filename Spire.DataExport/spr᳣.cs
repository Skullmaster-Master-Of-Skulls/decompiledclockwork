using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000041 RID: 65
[DefaultMember("Item")]
internal class spr\u1CE3 : spr\u2555
{
	// Token: 0x06000209 RID: 521 RVA: 0x00012E7C File Offset: 0x00011E7C
	public spr\u1CE3(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00012E90 File Offset: 0x00011E90
	private void ᜀ(object A_0, EventArgs A_1)
	{
		int num = 0;
		for (;;)
		{
			IEnumerator enumerator;
			switch (num)
			{
			case 1:
				goto IL_10D;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					try
					{
						num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								spr\u1DEE spr_u1DEE;
								if (spr_u1DEE == A_0)
								{
									num = 4;
									continue;
								}
								break;
							}
							case 1:
								goto IL_CA;
							case 2:
								goto IL_C2;
							case 3:
							{
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								spr\u1DEE spr_u1DEE = (spr\u1DEE)enumerator.Current;
								num = 0;
								continue;
							}
							case 4:
							{
								spr\u1DEE spr_u1DEE;
								base.ᜀ(spr_u1DEE);
								num = 5;
								continue;
							}
							case 5:
								goto IL_C2;
							}
							IL_74:
							num = 3;
							continue;
							goto IL_74;
							IL_C2:
							num = 1;
						}
						IL_CA:
						return;
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
									goto IL_10A;
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
									goto IL_10C;
								}
								break;
							}
						}
						IL_10A:
						IL_10C:;
					}
					goto IL_10D;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 is spr\u1DEE)
			{
				num = 1;
				continue;
			}
			break;
			IL_10D:
			enumerator = base.ᜇ();
			num = 2;
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00012FF0 File Offset: 0x00011FF0
	protected virtual void ᜁ(spr\u1DEE A_0)
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
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0001302C File Offset: 0x0001202C
	protected virtual void ᜀ(spr\u1DEE A_0)
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
	}

	// Token: 0x0600020D RID: 525 RVA: 0x00013068 File Offset: 0x00012068
	public int ᜂ(spr\u1DEE A_0)
	{
		int result;
		for (;;)
		{
			IL_1E:
			this.ᜀ = false;
			result = base.ᜀ(A_0);
			A_0.ᜀ(new EventHandler(this.ᜀ));
			this.ᜁ(A_0);
			for (;;)
			{
				IL_46:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_46;
						default:
							if (false)
							{
							}
							if (base.ᜌ() == 1)
							{
								num = 2;
								continue;
							}
							return result;
						}
						break;
					case 2:
						this.ᜀ(A_0);
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_1E;
				}
			}
		}
		return result;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x00013110 File Offset: 0x00012110
	public void ᜁ(int A_0, spr\u1DEE A_1)
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
		A_1.ᜀ(new EventHandler(this.ᜀ));
	}

	// Token: 0x0600020F RID: 527 RVA: 0x0001316C File Offset: 0x0001216C
	protected bool ᜄ()
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

	// Token: 0x06000210 RID: 528 RVA: 0x000131B0 File Offset: 0x000121B0
	protected void ᜀ(bool A_0)
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

	// Token: 0x06000211 RID: 529 RVA: 0x000131F4 File Offset: 0x000121F4
	public new spr\u1DEE ᜀ(int A_0)
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
		return base.ᜀ(A_0) as spr\u1DEE;
	}

	// Token: 0x06000212 RID: 530 RVA: 0x0001323C File Offset: 0x0001223C
	public void ᜀ(int A_0, spr\u1DEE A_1)
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

	// Token: 0x0400009C RID: 156
	private new bool ᜀ;
}
