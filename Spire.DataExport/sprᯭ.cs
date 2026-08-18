using System;
using System.Collections;

// Token: 0x02000100 RID: 256
internal class spr\u1BED
{
	// Token: 0x06000588 RID: 1416 RVA: 0x00035568 File Offset: 0x00034568
	public spr\u1BED()
	{
		this.ᜀ = new spr\u2116(61442, 15, 0);
		this.ᜁ = new spr\u2116(61443, 15, 0);
		this.ᜂ = new sprᣏ();
		this.ᜀ.ᜃ().ᜂ(new spr᧙(0, 1));
		this.ᜀ.ᜃ().ᜂ(this.ᜁ);
		spr\u2116 spr_u = new spr\u2116(61444, 15, 0);
		spr_u.ᜃ().ᜂ(new sprṖ(1, 0));
		spr_u.ᜃ().ᜂ(new spr\u2401(2, 0, 1024, 5));
		this.ᜁ.ᜃ().ᜂ(spr_u);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00035628 File Offset: 0x00034628
	public int ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_DA;
						try
						{
							for (;;)
							{
								IL_DA:
								num2 = 1;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_11E;
									case 2:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num2 = 3;
											continue;
										}
										sprᠺ sprᠺ = (sprᠺ)enumerator.Current;
										num += sprᠺ.ᜀ();
										num2 = 0;
										continue;
									}
									case 3:
										num2 = 4;
										continue;
									case 4:
										goto IL_163;
									}
									goto IL_100;
									IL_11E:
									num2 = 2;
									continue;
									IL_100:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_DA;
									default:
										if (false)
										{
										}
										goto IL_11E;
									}
								}
							}
							IL_163:
							goto IL_56;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num2 = 0;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										if (disposable != null)
										{
											num2 = 1;
											continue;
										}
										goto IL_1AC;
									case 1:
										disposable.Dispose();
										num2 = 2;
										continue;
									case 2:
										goto IL_1AA;
									}
									break;
								}
							}
							IL_1AA:
							IL_1AC:;
						}
						return num;
						IL_56:
						this.ᜀ.ᜀ(num);
						this.ᜁ.ᜀ(num);
						num = this.ᜀ.ᜀ() + this.ᜂ.ᜁ(0).ᜀ();
						num2 = 4;
						continue;
					case 1:
						return num;
					case 2:
					{
						IEnumerator enumerator = this.ᜂ.ᜂ();
						num2 = 0;
						continue;
					}
					case 3:
						if (true)
						{
						}
						if (A_0 == 0)
						{
							num2 = 2;
							continue;
						}
						num = this.ᜂ.ᜁ(A_0).ᜀ();
						num2 = 1;
						continue;
					case 4:
						return num;
					}
					break;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x000357F4 File Offset: 0x000347F4
	public void ᜀ(sprḗ A_0, int A_1)
	{
		for (;;)
		{
			spr\u1DCF a_;
			a_.ᜀ = 236;
			a_.ᜁ = (ushort)this.ᜀ(A_1);
			byte[] array = spr\u1DCF.ᜀ(a_);
			A_0.ᜁ(array, array.Length);
			if (A_1 != 0)
			{
				goto IL_7F;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_5A;
			}
		}
		IL_5A:
		if (false)
		{
		}
		this.ᜀ.ᜀ(A_0);
		this.ᜂ.ᜁ(0).ᜀ(A_0);
		return;
		IL_7F:
		this.ᜂ.ᜁ(A_1).ᜀ(A_0);
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00035894 File Offset: 0x00034894
	public byte[] ᜁ(int A_0)
	{
		byte[] array;
		for (;;)
		{
			int num = this.ᜀ(A_0);
			array = new byte[num];
			int num2 = 0;
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return array;
				case 1:
					return array;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜀ.ᜀ(array, ref num2);
						this.ᜂ.ᜁ(0).ᜀ(array, ref num2);
						num3 = 1;
						continue;
					}
					break;
				case 3:
					if (A_0 == 0)
					{
						num3 = 2;
						continue;
					}
					this.ᜂ.ᜁ(A_0).ᜀ(array, ref num2);
					goto IL_52;
				}
				break;
				IL_52:
				num3 = 0;
			}
		}
		return array;
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x0003595C File Offset: 0x0003495C
	public spr\u2116 ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x000359A0 File Offset: 0x000349A0
	public spr\u2116 ᜁ()
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

	// Token: 0x0600058E RID: 1422 RVA: 0x000359E4 File Offset: 0x000349E4
	public sprᣏ ᜂ()
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

	// Token: 0x04000589 RID: 1417
	private spr\u2116 ᜀ;

	// Token: 0x0400058A RID: 1418
	private spr\u2116 ᜁ;

	// Token: 0x0400058B RID: 1419
	private sprᣏ ᜂ;
}
