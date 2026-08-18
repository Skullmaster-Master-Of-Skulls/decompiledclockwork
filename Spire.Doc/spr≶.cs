using System;
using System.Collections.Generic;
using System.IO;

// Token: 0x0200013E RID: 318
[CLSCompliant(false)]
internal abstract class spr\u2276
{
	// Token: 0x06000832 RID: 2098 RVA: 0x0005BC98 File Offset: 0x0005AC98
	internal int ᜇ()
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
		return this.ᜈ;
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0005BCDC File Offset: 0x0005ACDC
	internal spr\u2276(Stream A_0, sprᾱ A_1) : this()
	{
		this.ᜀ(A_0, A_1);
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0005BCF8 File Offset: 0x0005ACF8
	internal spr\u2276()
	{
		this.ᜅ();
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0005BD18 File Offset: 0x0005AD18
	internal bool ᜆ(int A_0)
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
		return this.ᜂ.Contains(A_0);
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0005BD60 File Offset: 0x0005AD60
	internal bool ᜉ(int A_0)
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
		return this.ᜁ.Contains(A_0);
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0005BDA8 File Offset: 0x0005ADA8
	internal virtual void ᜀ(Stream A_0, sprᾱ A_1)
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
		this.ᜀ = A_1;
		this.ᜅ = new BinaryReader(A_0);
		this.ᜀ();
		this.ᜁ();
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0005BE04 File Offset: 0x0005AE04
	internal virtual void ᜁ(Stream A_0, sprᾱ A_1)
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
		this.ᜀ = A_1;
		this.ᜆ = new BinaryWriter(A_0);
		this.ᜋ = this.ᜀ.\u1774() + this.ᜀ.ព() + this.ᜀ.ណ() + this.ᜀ.\u1753() + this.ᜀ.ᝋ() + this.ᜀ.\u1752() + this.ᜀ.\u17D1();
		this.ᜂ();
		this.ᜃ();
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0005BEB8 File Offset: 0x0005AEB8
	internal virtual void ᜈ(int A_0)
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
		this.ᜁ.Add(A_0);
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0005BF00 File Offset: 0x0005AF00
	internal virtual int ᜊ(int A_0)
	{
		if (this.ᜁ.Count == 0)
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
				return 0;
			}
		}
		if (true)
		{
		}
		return this.ᜁ[A_0];
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0005BF58 File Offset: 0x0005AF58
	protected virtual void ᜁ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_4D;
			case 2:
				IL_2D:
				this.ᜋ(this.ᜇ);
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if (this.ᜇ != -1)
			{
				num = 2;
				continue;
			}
			IL_4D:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2D;
			default:
				goto IL_63;
			}
		}
		IL_63:
		if (false)
		{
		}
	}

	// Token: 0x0600083C RID: 2108
	protected abstract void ᜃ();

	// Token: 0x0600083D RID: 2109 RVA: 0x0005BFD8 File Offset: 0x0005AFD8
	protected void ᜁ(int A_0, int A_1)
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
		spr\u2432.ᜀ(this.ᜅ, A_0, A_1, new spr\u1ACD(this.ᜀ));
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0005C030 File Offset: 0x0005B030
	protected void ᜋ(int A_0)
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
		this.ᜂ.Add(A_0);
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0005C078 File Offset: 0x0005B078
	protected virtual void ᜅ()
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜁ = new List<int>();
		this.ᜂ = new List<int>();
		this.ᜃ = new List<sprᝦ>();
		this.ᜄ = new List<short>();
	}

	// Token: 0x06000840 RID: 2112
	protected abstract void ᜀ();

	// Token: 0x06000841 RID: 2113 RVA: 0x0005C0E0 File Offset: 0x0005B0E0
	protected void ᜅ(int A_0)
	{
		for (;;)
		{
			this.ᜈ = A_0 - 1;
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_3F;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_35;
					default:
						if (false)
						{
						}
						if (num >= A_0)
						{
							num2 = 3;
							continue;
						}
						this.ᜈ(this.ᜅ.ReadInt32());
						num++;
						num2 = 0;
						continue;
					}
					break;
				case 2:
					goto IL_35;
				case 3:
					return;
				}
				break;
				IL_3F:
				num2 = 1;
				continue;
				IL_35:
				if (true)
				{
				}
				goto IL_3F;
			}
		}
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0005C180 File Offset: 0x0005B180
	protected void ᜆ()
	{
		using (List<int>.Enumerator enumerator = this.ᜁ.GetEnumerator())
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!enumerator.MoveNext())
					{
						num = 3;
						continue;
					}
					goto IL_34;
				case 2:
					goto IL_9C;
				case 3:
					num = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_34;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				goto IL_32;
				IL_34:
				if (true)
				{
				}
				int value = enumerator.Current;
				this.ᜆ.Write(value);
				num = 4;
				continue;
				IL_76:
				num = 0;
				continue;
				IL_32:
				goto IL_76;
			}
			IL_9C:;
		}
	}

	// Token: 0x06000843 RID: 2115
	protected abstract void ᜂ();

	// Token: 0x06000844 RID: 2116 RVA: 0x0005C254 File Offset: 0x0005B254
	protected virtual void ᜇ(int A_0)
	{
		for (;;)
		{
			List<int>.Enumerator enumerator = this.ᜂ.GetEnumerator();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (this.ᜂ.Count > 0)
					{
						num = 3;
						continue;
					}
					return;
				case 1:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								goto IL_82;
							case 2:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_82;
								default:
									if (false)
									{
									}
									break;
								}
								break;
							case 3:
								num = 4;
								continue;
							case 4:
								goto IL_DF;
							}
							goto IL_80;
							IL_82:
							int value = enumerator.Current;
							this.ᜆ.Write(value);
							num = 2;
							continue;
							IL_BC:
							num = 0;
							continue;
							IL_80:
							goto IL_BC;
						}
						IL_DF:
						goto IL_2E;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_F2;
					IL_2E:
					num = 0;
					continue;
				case 2:
					return;
				case 3:
					goto IL_F2;
				}
				break;
				IL_F2:
				this.ᜆ.Write(A_0);
				num = 2;
			}
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0005C388 File Offset: 0x0005B388
	protected virtual void ᜀ(BinaryReader A_0, int A_1, int A_2)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜋ(A_1);
		this.ᜇ = A_2;
	}

	// Token: 0x04001321 RID: 4897
	protected sprᾱ ᜀ;

	// Token: 0x04001322 RID: 4898
	protected List<int> ᜁ;

	// Token: 0x04001323 RID: 4899
	protected List<int> ᜂ;

	// Token: 0x04001324 RID: 4900
	protected List<sprᝦ> ᜃ;

	// Token: 0x04001325 RID: 4901
	protected List<short> ᜄ;

	// Token: 0x04001326 RID: 4902
	protected BinaryReader ᜅ;

	// Token: 0x04001327 RID: 4903
	protected BinaryWriter ᜆ;

	// Token: 0x04001328 RID: 4904
	private int ᜇ = -1;

	// Token: 0x04001329 RID: 4905
	protected int ᜈ;

	// Token: 0x0400132A RID: 4906
	protected int ᜉ;

	// Token: 0x0400132B RID: 4907
	protected int ᜊ;

	// Token: 0x0400132C RID: 4908
	protected int ᜋ;
}
