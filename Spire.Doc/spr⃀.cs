using System;
using System.Xml;
using Spire.Doc.Convertors.Sgml;

// Token: 0x020002A0 RID: 672
internal class spr\u20C0
{
	// Token: 0x060023C2 RID: 9154 RVA: 0x00242C14 File Offset: 0x00241C14
	public void ᜀ(string A_0, XmlNodeType A_1, string A_2)
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
		this.ᜁ = A_2;
		this.ᜅ = A_0;
		this.ᜀ = A_1;
		this.ᜂ = XmlSpace.None;
		this.ᜃ = null;
		this.ᜄ = true;
		this.ᜉ.ᜀ(0);
		this.ᜆ = null;
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x00242C8C File Offset: 0x00241C8C
	public spr\u245C ᜀ(string A_0, string A_1, char A_2, bool A_3)
	{
		spr\u245C spr_u245C;
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜉ.ᜀ();
			int num3 = 8;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					spr_u245C = (spr\u245C)this.ᜉ.ᜂ(num);
					num3 = 1;
					continue;
				case 1:
					if (string.Equals(spr_u245C.ᜀ, A_0, A_3 ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
					{
						num3 = 6;
						continue;
					}
					if (true)
					{
					}
					num++;
					num3 = 4;
					continue;
				case 2:
					goto IL_128;
				case 3:
					spr_u245C = (spr\u245C)this.ᜉ.ᜃ();
					num3 = 7;
					continue;
				case 4:
					goto IL_12D;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14F;
					default:
						if (false)
						{
						}
						spr_u245C = new spr\u245C();
						this.ᜉ.ᜀ(this.ᜉ.ᜀ() - 1, spr_u245C);
						num3 = 2;
						continue;
					}
					break;
				case 6:
					goto IL_F9;
				case 7:
					if (spr_u245C == null)
					{
						num3 = 5;
						continue;
					}
					goto IL_14F;
				case 8:
					goto IL_12D;
				}
				break;
				IL_12D:
				num3 = 0;
			}
		}
		IL_F9:
		return null;
		IL_128:
		IL_14F:
		spr_u245C.ᜀ(A_0, A_1, A_2);
		return spr_u245C;
	}

	// Token: 0x060023C4 RID: 9156 RVA: 0x00242DF4 File Offset: 0x00241DF4
	public void ᜁ(string A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = this.ᜉ.ᜀ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
				{
					if (num >= num2)
					{
						num3 = 4;
						continue;
					}
					spr\u245C spr_u245C = (spr\u245C)this.ᜉ.ᜂ(num);
					num3 = 2;
					continue;
				}
				case 1:
					goto IL_B0;
				case 2:
				{
					spr\u245C spr_u245C;
					if (string.Equals(spr_u245C.ᜀ, A_0, StringComparison.OrdinalIgnoreCase))
					{
						num3 = 1;
						continue;
					}
					num++;
					num3 = 5;
					continue;
				}
				case 3:
					goto IL_36;
				case 4:
					return;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					}
					if (false)
					{
					}
					goto IL_B2;
				}
				break;
				IL_B2:
				if (true)
				{
				}
				num3 = 0;
				continue;
				IL_36:
				goto IL_B2;
			}
		}
		IL_B0:
		this.ᜉ.ᜁ(num);
	}

	// Token: 0x060023C5 RID: 9157 RVA: 0x00242ED8 File Offset: 0x00241ED8
	public void ᜀ(spr\u20C0 A_0)
	{
		switch (0)
		{
		default:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			for (;;)
			{
				int num = 0;
				int num2 = A_0.ᜉ.ᜀ();
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						if (num >= num2)
						{
							num3 = 2;
							continue;
						}
						spr\u245C spr_u245C = (spr\u245C)A_0.ᜉ.ᜂ(num);
						spr\u245C spr_u245C2 = this.ᜀ(spr_u245C.ᜀ, spr_u245C.ᜁ(), spr_u245C.ᜂ, false);
						spr_u245C2.ᜁ = spr_u245C.ᜁ;
						num++;
						if (true)
						{
						}
						num3 = 1;
						continue;
					}
					case 1:
						goto IL_66;
					case 2:
						return;
					case 3:
						goto IL_66;
					}
					break;
					IL_66:
					num3 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x060023C6 RID: 9158 RVA: 0x00242FB8 File Offset: 0x00241FB8
	public int ᜀ()
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
		return this.ᜉ.ᜀ();
	}

	// Token: 0x060023C7 RID: 9159 RVA: 0x00243000 File Offset: 0x00242000
	public int ᜀ(string A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int num2 = this.ᜉ.ᜀ();
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return -1;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						goto IL_AF;
					}
					break;
				case 2:
					return num;
				case 3:
					goto IL_36;
				case 4:
				{
					spr\u245C spr_u245C;
					if (string.Equals(spr_u245C.ᜀ, A_0, StringComparison.OrdinalIgnoreCase))
					{
						num3 = 2;
						continue;
					}
					num++;
					if (true)
					{
					}
					num3 = 1;
					continue;
				}
				case 5:
				{
					if (num >= num2)
					{
						num3 = 0;
						continue;
					}
					spr\u245C spr_u245C = (spr\u245C)this.ᜉ.ᜂ(num);
					num3 = 4;
					continue;
				}
				}
				break;
				IL_AF:
				num3 = 5;
				continue;
				IL_36:
				goto IL_AF;
			}
		}
		return num;
	}

	// Token: 0x060023C8 RID: 9160 RVA: 0x002430DC File Offset: 0x002420DC
	public spr\u245C ᜀ(int A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 1;
				continue;
			case 1:
				if (A_0 < this.ᜉ.ᜀ())
				{
					num = 2;
					continue;
				}
				goto IL_93;
			case 2:
				goto IL_91;
			}
			if (true)
			{
			}
			if (A_0 < 0)
			{
				goto IL_93;
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
				num = 0;
				break;
			}
		}
		IL_91:
		return (spr\u245C)this.ᜉ.ᜂ(A_0);
		IL_93:
		return null;
	}

	// Token: 0x0400216B RID: 8555
	internal XmlNodeType ᜀ;

	// Token: 0x0400216C RID: 8556
	internal string ᜁ;

	// Token: 0x0400216D RID: 8557
	internal XmlSpace ᜂ;

	// Token: 0x0400216E RID: 8558
	internal string ᜃ;

	// Token: 0x0400216F RID: 8559
	internal bool ᜄ;

	// Token: 0x04002170 RID: 8560
	internal string ᜅ;

	// Token: 0x04002171 RID: 8561
	internal spr\u1D66 ᜆ;

	// Token: 0x04002172 RID: 8562
	internal State ᜇ;

	// Token: 0x04002173 RID: 8563
	internal bool ᜈ;

	// Token: 0x04002174 RID: 8564
	private sprᢐ ᜉ = new sprᢐ(10);
}
