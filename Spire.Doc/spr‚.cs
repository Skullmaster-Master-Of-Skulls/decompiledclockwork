using System;
using System.Collections.Generic;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;

// Token: 0x0200023D RID: 573
internal class spr\u201A : spr\u23F8
{
	// Token: 0x06001B55 RID: 6997 RVA: 0x001C725C File Offset: 0x001C625C
	internal spr\u201A()
	{
		this.ᜆ = new sprᡜ();
		this.ᜇ = new sprᣚ();
	}

	// Token: 0x06001B56 RID: 6998 RVA: 0x001C7290 File Offset: 0x001C6290
	internal spr\u201A(sprᾱ A_0, Stream A_1)
	{
		this.ᜆ = new sprᡜ();
		this.ᜇ = new sprᣚ();
		this.ᜀ(A_0, A_1);
		this.ᜁ(A_0, A_1);
		this.ᜂ(A_0, A_1);
	}

	// Token: 0x06001B57 RID: 6999 RVA: 0x001C72DC File Offset: 0x001C62DC
	internal void ᜀ(sprᾱ A_0, Stream A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 4;
								continue;
							case 1:
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
								break;
							case 3:
							{
								List<sprហ>.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								sprហ sprហ = enumerator.Current;
								sprហ.ᜃ(A_1);
								num = 1;
								continue;
							}
							case 4:
								goto IL_EB;
							}
							IL_8F:
							num = 3;
							continue;
							goto IL_8F;
						}
						IL_EB:
						return;
					}
					finally
					{
						List<sprហ>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_FE;
				case 1:
					goto IL_FE;
				case 3:
				{
					if (true)
					{
					}
					A_1.Position = (long)A_0.\u177D();
					int num2 = (int)spr\u23F8.ᜂ(A_1);
					int num3 = 0;
					num = 1;
					continue;
				}
				case 4:
				{
					int num2;
					int num3;
					if (num3 >= num2)
					{
						num = 6;
						continue;
					}
					this.ᜆ.Add(new sprហ(A_1));
					num3++;
					num = 5;
					continue;
				}
				case 5:
					goto IL_FE;
				case 6:
				{
					List<sprហ>.Enumerator enumerator = this.ᜆ.GetEnumerator();
					num = 0;
					continue;
				}
				}
				if (A_0.\u17C0() != 0)
				{
					num = 3;
					continue;
				}
				break;
				IL_FE:
				num = 4;
			}
			return;
		}
		}
	}

	// Token: 0x06001B58 RID: 7000 RVA: 0x001C747C File Offset: 0x001C647C
	internal void ᜁ(sprᾱ A_0, Stream A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					List<sprℼ>.Enumerator enumerator = this.ᜇ.GetEnumerator();
					num = 4;
					continue;
				}
				case 2:
					goto IL_FE;
				case 3:
				{
					if (true)
					{
					}
					A_1.Position = (long)A_0.ᜢ();
					int num2 = spr\u23F8.ᜁ(A_1);
					int num3 = 0;
					num = 6;
					continue;
				}
				case 4:
					try
					{
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
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
								break;
							case 1:
								num = 2;
								continue;
							case 2:
								goto IL_EB;
							case 4:
							{
								List<sprℼ>.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								sprℼ sprℼ = enumerator.Current;
								sprℼ.ᜀ(A_1);
								num = 0;
								continue;
							}
							}
							IL_8F:
							num = 4;
							continue;
							goto IL_8F;
						}
						IL_EB:
						return;
					}
					finally
					{
						List<sprℼ>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_FE;
				case 5:
				{
					int num2;
					int num3;
					if (num3 >= num2)
					{
						num = 0;
						continue;
					}
					this.ᜇ.Add(new sprℼ(A_1));
					num3++;
					num = 2;
					continue;
				}
				case 6:
					goto IL_FE;
				}
				if (A_0.ឲ() != 0)
				{
					num = 3;
					continue;
				}
				break;
				IL_FE:
				num = 5;
			}
			return;
		}
		}
	}

	// Token: 0x06001B59 RID: 7001 RVA: 0x001C761C File Offset: 0x001C661C
	internal void ᜂ(sprᾱ A_0, Stream A_1)
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
		A_1.Position = (long)A_0.ᜂ();
	}

	// Token: 0x06001B5A RID: 7002 RVA: 0x001C7664 File Offset: 0x001C6664
	internal int ᜀ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			int num2;
			for (;;)
			{
				List<sprℼ>.Enumerator enumerator;
				switch (num)
				{
				case 1:
					return 0;
				case 2:
					try
					{
						num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								sprℼ sprℼ = enumerator.Current;
								sprℼ.ᜁ(A_0);
								num = 2;
								continue;
							}
							case 1:
								num = 4;
								continue;
							case 4:
								goto IL_107;
							}
							IL_C7:
							num = 0;
							continue;
							goto IL_C7;
						}
						IL_107:
						goto IL_1B9;
					}
					finally
					{
						if (true)
						{
						}
						((IDisposable)enumerator).Dispose();
					}
					goto Block_3;
				case 3:
					goto IL_122;
				}
				if (this.ᜇ.Count == 0)
				{
					num = 1;
					continue;
				}
				num2 = (int)A_0.Position;
				spr\u23F8.ᜁ(A_0, this.ᜇ.Count);
				List<sprℼ>.Enumerator enumerator2 = this.ᜇ.GetEnumerator();
				num = 3;
				continue;
				Block_3:
				try
				{
					IL_122:
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator2.MoveNext())
							{
								num = 1;
								continue;
							}
							sprℼ sprℼ2 = enumerator2.Current;
							sprℼ2.ᜂ(A_0);
							num = 2;
							continue;
						}
						case 1:
							num = 4;
							continue;
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
								break;
							}
							break;
						case 4:
							goto IL_1A6;
						}
						IL_14A:
						num = 0;
						continue;
						goto IL_14A;
					}
					IL_1A6:
					goto IL_48;
				}
				finally
				{
					((IDisposable)enumerator2).Dispose();
				}
				goto IL_1B9;
				IL_48:
				enumerator = this.ᜇ.GetEnumerator();
				num = 2;
			}
			return 0;
			IL_1B9:
			return (int)A_0.Position - num2;
		}
		}
	}

	// Token: 0x06001B5B RID: 7003 RVA: 0x001C7850 File Offset: 0x001C6850
	internal int ᜁ(Stream A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				List<sprហ>.Enumerator enumerator;
				int result;
				switch (num)
				{
				case 1:
					try
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_11B;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								sprហ sprហ = enumerator.Current;
								sprហ.ᜁ(A_0);
								num = 4;
								continue;
							}
							case 3:
								num = 1;
								continue;
							}
							IL_DB:
							num = 2;
							continue;
							goto IL_DB;
						}
						IL_11B:
						return result;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto Block_3;
				case 2:
					goto IL_12E;
				case 3:
					return 0;
				}
				if (this.ᜆ.Count == 0)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				int num2 = (int)A_0.Position;
				spr\u23F8.ᜀ(A_0, (short)this.ᜆ.Count);
				List<sprហ>.Enumerator enumerator2 = this.ᜆ.GetEnumerator();
				num = 2;
				continue;
				Block_3:
				try
				{
					IL_12E:
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator2.MoveNext())
							{
								num = 4;
								continue;
							}
							sprហ sprហ2 = enumerator2.Current;
							sprហ2.ᜂ(A_0);
							num = 1;
							continue;
						}
						case 1:
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
							break;
						case 2:
							goto IL_1B2;
						case 4:
							num = 2;
							continue;
						}
						IL_156:
						num = 0;
						continue;
						goto IL_156;
					}
					IL_1B2:
					goto IL_50;
				}
				finally
				{
					((IDisposable)enumerator2).Dispose();
				}
				return result;
				IL_50:
				result = (int)A_0.Position - num2;
				enumerator = this.ᜆ.GetEnumerator();
				num = 1;
			}
			return 0;
		}
		}
	}

	// Token: 0x06001B5C RID: 7004 RVA: 0x001C7A40 File Offset: 0x001C6A40
	internal int ᜂ(Stream A_0)
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
		byte[] array = new byte[8];
		array[0] = (array[1] = byte.MaxValue);
		array[2] = 1;
		A_0.Write(array, 0, array.Length);
		return array.Length;
	}

	// Token: 0x06001B5D RID: 7005 RVA: 0x001C7AA4 File Offset: 0x001C6AA4
	internal short ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A2:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_4D;
			}
			float num2;
			sprហ sprហ;
			int num3;
			for (;;)
			{
				IL_2C:
				switch (num)
				{
				case 0:
					goto IL_86;
				case 1:
					goto IL_AB;
				case 2:
					if (true)
					{
					}
					if (num2 >= 4.5f)
					{
						goto IL_A2;
					}
					sprហ.ᜅ().Add(this.ᜀ((int)(1440f * num2), num3++, ListPatternType.Arabic, ListNumberAlignment.Left));
					sprហ.ᜅ().Add(this.ᜀ((int)(1440.0 * ((double)num2 + 0.5)), num3++, ListPatternType.LowLetter, ListNumberAlignment.Right));
					sprហ.ᜅ().Add(this.ᜀ((int)(1440f * (num2 + 1f)), num3++, ListPatternType.LowRoman, ListNumberAlignment.Left));
					num2 += 1.5f;
					num = 3;
					continue;
				case 3:
					goto IL_86;
				}
				goto IL_4D;
				IL_86:
				num = 2;
			}
			IL_AB:
			sprℼ sprℼ = new sprℼ();
			sprℼ.ᜀ(sprហ.ᜄ());
			this.ᜇ.Add(sprℼ);
			return Convert.ToInt16(this.ᜇ.Count);
			IL_4D:
			sprហ = new sprហ(this.ᜅ);
			this.ᜅ++;
			this.ᜆ.Add(sprហ);
			num3 = 0;
			num2 = 0.5f;
			num = 0;
			goto IL_2C;
		}
		}
	}

	// Token: 0x06001B5E RID: 7006 RVA: 0x001C7C20 File Offset: 0x001C6C20
	internal sprហ ᜀ(int A_0)
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
		sprℼ sprℼ = this.ᜇ.ᜀ(A_0 - 1);
		return this.ᜆ.ᜂ(sprℼ.ᜁ());
	}

	// Token: 0x06001B5F RID: 7007 RVA: 0x001C7C7C File Offset: 0x001C6C7C
	internal spr\u201A ᜀ()
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
		return base.MemberwiseClone() as spr\u201A;
	}

	// Token: 0x06001B60 RID: 7008 RVA: 0x001C7CC4 File Offset: 0x001C6CC4
	internal short ᜅ()
	{
		int a_ = 16;
		sprហ sprហ;
		for (;;)
		{
			IL_4F:
			sprហ = new sprហ(this.ᜅ);
			this.ᜅ++;
			this.ᜆ.Add(sprហ);
			float num = 0.5f;
			int num2 = 3;
			for (;;)
			{
				if (true)
				{
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
					switch (num2)
					{
					case 0:
						goto IL_85;
					case 1:
						goto IL_A3;
					case 2:
						if (num >= 4.5f)
						{
							goto IL_98;
						}
						sprហ.ᜅ().Add(this.ᜀ((int)(1440f * num), ClipboardData.b("솅", a_)));
						sprហ.ᜅ().Add(this.ᜀ((int)(1440.0 * ((double)num + 0.5)), ClipboardData.b("᥵", a_)));
						sprហ.ᜅ().Add(this.ᜀ((int)(1440f * (num + 1f)), ClipboardData.b("톅", a_)));
						num += 1.5f;
						num2 = 0;
						continue;
					case 3:
						goto IL_85;
					}
					goto IL_4F;
					IL_85:
					num2 = 2;
					continue;
				}
				IL_98:
				num2 = 1;
			}
		}
		IL_A3:
		sprℼ sprℼ = new sprℼ();
		sprℼ.ᜀ(sprហ.ᜄ());
		this.ᜇ.Add(sprℼ);
		return Convert.ToInt16(this.ᜇ.Count);
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x001C7E4C File Offset: 0x001C6E4C
	internal short ᜁ(sprហ A_0, ListFormat A_1, spr\u2305 A_2)
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
		this.ᜆ.Add(A_0);
		return this.ᜀ(A_0, A_1, A_2);
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x001C7E9C File Offset: 0x001C6E9C
	internal short ᜀ(sprហ A_0, ListFormat A_1, spr\u2305 A_2)
	{
		sprℼ sprℼ;
		for (;;)
		{
			sprℼ = new sprℼ();
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					spr\u177D spr_u177D;
					if (spr_u177D != null)
					{
						num = 3;
						continue;
					}
					goto IL_B0;
				}
				case 1:
					goto IL_70;
				case 2:
				{
					string a_ = A_1.LFOStyleName;
					spr\u177D spr_u177D = A_1.Document.ListOverrides.ᜀ(a_);
					goto IL_8B;
				}
				case 3:
				{
					spr\u177D spr_u177D;
					sprἹ.ᜀ(spr_u177D, sprℼ, A_2);
					num = 1;
					continue;
				}
				case 4:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8B;
					default:
						if (false)
						{
						}
						if (A_1.LFOStyleName != null)
						{
							num = 2;
							continue;
						}
						goto IL_B0;
					}
					break;
				}
				break;
				IL_8B:
				num = 0;
			}
		}
		IL_70:
		IL_B0:
		sprℼ.ᜀ(A_0.ᜄ());
		this.ᜇ.Add(sprℼ);
		return Convert.ToInt16(this.ᜇ.Count);
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x001C7F84 File Offset: 0x001C6F84
	internal sprᣚ ᜄ()
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
		return this.ᜇ;
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x001C7FC8 File Offset: 0x001C6FC8
	internal sprᡜ ᜂ()
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
		return this.ᜆ;
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x001C800C File Offset: 0x001C700C
	private spr\u225B ᜀ(int A_0, string A_1)
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
		return new spr\u225B();
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x001C8050 File Offset: 0x001C7050
	private spr\u225B ᜀ(int A_0, int A_1, ListPatternType A_2, ListNumberAlignment A_3)
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
		return new spr\u225B();
	}

	// Token: 0x04001EB9 RID: 7865
	private new const int ᜀ = 1720085641;

	// Token: 0x04001EBA RID: 7866
	private new const string ᜁ = "";

	// Token: 0x04001EBB RID: 7867
	private new const string ᜂ = "o";

	// Token: 0x04001EBC RID: 7868
	private new const string ᜃ = "";

	// Token: 0x04001EBD RID: 7869
	private new const int ᜄ = 1440;

	// Token: 0x04001EBE RID: 7870
	private new int ᜅ = 1720085641;

	// Token: 0x04001EBF RID: 7871
	private sprᡜ ᜆ;

	// Token: 0x04001EC0 RID: 7872
	private sprᣚ ᜇ;
}
