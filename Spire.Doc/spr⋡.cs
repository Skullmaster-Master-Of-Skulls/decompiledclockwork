using System;
using System.Collections.Generic;
using Spire.Doc.Documents;

// Token: 0x02000145 RID: 325
internal class spr\u22E1
{
	// Token: 0x060008C1 RID: 2241 RVA: 0x0006FDB4 File Offset: 0x0006EDB4
	public spr\u22E1()
	{
		this.ᜀ = new Dictionary<string, sprᳮ>();
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x0006FDE8 File Offset: 0x0006EDE8
	public int ᜃ()
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

	// Token: 0x060008C3 RID: 2243 RVA: 0x0006FE30 File Offset: 0x0006EE30
	public void ᜀ(string A_0, sprᳮ A_1)
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
		this.ᜀ.Add(A_0, A_1);
		this.ᜁ = this.ᜀ();
		this.ᜃ = Math.Max(this.ᜃ, A_1.ᜁ());
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x0006FE9C File Offset: 0x0006EE9C
	public float ᜂ()
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
		return this.ᜁ();
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x0006FEE0 File Offset: 0x0006EEE0
	public float ᜁ(string A_0)
	{
		float num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				num = 0f;
				Dictionary<string, sprᳮ>.Enumerator enumerator = this.ᜀ.GetEnumerator();
				try
				{
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 6;
							continue;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 0;
								continue;
							}
							KeyValuePair<string, sprᳮ> keyValuePair = enumerator.Current;
							spr\u2569 spr_u = keyValuePair.Value.ᜁ(A_0);
							num2 = 2;
							continue;
						}
						case 2:
						{
							spr\u2569 spr_u;
							if (spr_u.ᜀ() == CellMerge.None)
							{
								num2 = 3;
								continue;
							}
							break;
						}
						case 3:
						{
							spr\u2569 spr_u;
							num = Math.Max(num, spr_u.ᜁ());
							num2 = 5;
							continue;
						}
						case 6:
							goto IL_EE;
						}
						IL_AA:
						num2 = 1;
						continue;
						goto IL_AA;
					}
					IL_EE:;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				break;
			}
			}
			break;
		}
		return num;
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x00070004 File Offset: 0x0006F004
	public sprᳮ ᜀ(string A_0)
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
		return this.ᜀ[A_0];
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x0007004C File Offset: 0x0006F04C
	private float ᜁ()
	{
		switch (0)
		{
		default:
		{
			float num;
			for (;;)
			{
				num = 0f;
				int num2 = 0;
				if (true)
				{
				}
				int num3 = 2;
				for (;;)
				{
					List<float>.Enumerator enumerator;
					switch (num3)
					{
					case 0:
						goto IL_DE;
					case 1:
						try
						{
							num3 = 2;
							for (;;)
							{
								switch (num3)
								{
								case 0:
									goto IL_CB;
								case 3:
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
										if (!enumerator.MoveNext())
										{
											num3 = 4;
											continue;
										}
										float num4 = enumerator.Current;
										num += num4;
										break;
									}
									}
									num3 = 1;
									continue;
								case 4:
									num3 = 0;
									continue;
								}
								IL_71:
								num3 = 3;
								continue;
								goto IL_71;
							}
							IL_CB:
							return num;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_DE;
					case 2:
						goto IL_12F;
					case 3:
						if (num2 >= this.ᜃ)
						{
							num3 = 0;
							continue;
						}
						this.ᜂ.Add(this.ᜁ(num2.ToString()));
						num2++;
						num3 = 4;
						continue;
					case 4:
						goto IL_12F;
					}
					break;
					IL_DE:
					enumerator = this.ᜂ.GetEnumerator();
					num3 = 1;
					continue;
					IL_12F:
					num3 = 3;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x000701BC File Offset: 0x0006F1BC
	private float ᜀ()
	{
		float num = this.ᜁ;
		Dictionary<string, sprᳮ>.Enumerator enumerator = this.ᜀ.GetEnumerator();
		try
		{
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 1:
					num2 = 3;
					continue;
				case 2:
					if (enumerator.MoveNext())
					{
						KeyValuePair<string, sprᳮ> keyValuePair = enumerator.Current;
						num = Math.Max(num, keyValuePair.Value.ᜂ());
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
					}
					num2 = 1;
					continue;
				case 3:
					goto IL_9F;
				}
				IL_7C:
				num2 = 2;
				continue;
				goto IL_7C;
			}
			IL_9F:;
		}
		finally
		{
			if (true)
			{
			}
			((IDisposable)enumerator).Dispose();
		}
		return num;
	}

	// Token: 0x04001352 RID: 4946
	private Dictionary<string, sprᳮ> ᜀ = new Dictionary<string, sprᳮ>();

	// Token: 0x04001353 RID: 4947
	private float ᜁ;

	// Token: 0x04001354 RID: 4948
	private List<float> ᜂ = new List<float>();

	// Token: 0x04001355 RID: 4949
	private int ᜃ;
}
