using System;
using System.Collections;
using System.Drawing;

// Token: 0x02000302 RID: 770
internal static class spr\u215C
{
	// Token: 0x060029E6 RID: 10726 RVA: 0x0029C1E8 File Offset: 0x0029B1E8
	static spr\u215C()
	{
		try
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
			spr\u215C.ᜀ = new Font(string.Empty, 1f).FontFamily;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060029E7 RID: 10727 RVA: 0x0029C254 File Offset: 0x0029B254
	public static Font ᜀ(string A_0, float A_1)
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
		return spr\u215C.ᜀ(A_0, A_1, FontStyle.Regular);
	}

	// Token: 0x060029E8 RID: 10728 RVA: 0x0029C298 File Offset: 0x0029B298
	public static Font ᜀ(string A_0, float A_1, FontStyle A_2)
	{
		Font result;
		try
		{
			result = new Font(A_0, A_1, A_2);
		}
		catch (Exception)
		{
			if (A_1 > 0f)
			{
				goto IL_6A;
			}
			IL_30:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_6A:
				FontFamily a_ = null;
				try
				{
					a_ = new FontFamily(A_0);
					goto IL_16;
				}
				catch (Exception)
				{
					goto IL_16;
				}
				goto IL_30;
				IL_16:
				Font font = spr\u215C.ᜀ(a_, A_1, A_2);
				if (font == null)
				{
					font = spr\u215C.ᜀ(spr\u215C.ᜀ, A_1, A_2);
				}
				result = font;
				break;
			}
			default:
				if (false)
				{
				}
				result = new Font(A_0, 9f, A_2);
				break;
			}
		}
		if (true)
		{
		}
		return result;
	}

	// Token: 0x060029E9 RID: 10729 RVA: 0x0029C348 File Offset: 0x0029B348
	public static Font ᜀ(FontFamily A_0, float A_1, FontStyle A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			FontStyle fontStyle;
			FontStyle fontStyle3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D6;
				case 1:
					try
					{
						num = 5;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								FontStyle fontStyle2;
								fontStyle = fontStyle2;
								num = 11;
								continue;
							}
							case 1:
								num = 6;
								continue;
							case 2:
								goto IL_209;
							case 3:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 7;
									continue;
								}
								FontStyle fontStyle2 = (FontStyle)enumerator.Current;
								num = 8;
								continue;
							}
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									if (fontStyle != FontStyle.Regular)
									{
										goto IL_1B9;
									}
									break;
								}
								num = 0;
								continue;
							case 6:
							{
								FontStyle fontStyle2;
								if ((fontStyle2 & A_2) == fontStyle2)
								{
									num = 9;
									continue;
								}
								goto IL_209;
							}
							case 7:
								num = 10;
								continue;
							case 8:
							{
								FontStyle fontStyle2;
								if (A_0.IsStyleAvailable(fontStyle2))
								{
									num = 1;
									continue;
								}
								break;
							}
							case 9:
							{
								FontStyle fontStyle2;
								fontStyle3 |= fontStyle2;
								num = 2;
								continue;
							}
							case 10:
								goto IL_24E;
							}
							IL_1B9:
							num = 3;
							continue;
							goto IL_1B9;
							IL_209:
							num = 4;
						}
						IL_24E:
						goto IL_E3;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
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
									goto IL_299;
								case 2:
									if (disposable != null)
									{
										num = 0;
										continue;
									}
									goto IL_29B;
								}
								break;
							}
						}
						IL_299:
						IL_29B:;
					}
					goto IL_29C;
					IL_E3:
					if (true)
					{
					}
					num = 6;
					continue;
				case 2:
				{
					if (A_1 <= 0f)
					{
						num = 0;
						continue;
					}
					fontStyle3 = FontStyle.Regular;
					fontStyle = FontStyle.Regular;
					IEnumerator enumerator = Enum.GetValues(typeof(FontStyle)).GetEnumerator();
					num = 1;
					continue;
				}
				case 4:
					num = 10;
					continue;
				case 5:
					if (fontStyle != FontStyle.Regular)
					{
						num = 7;
						continue;
					}
					goto IL_E1;
				case 6:
					if (fontStyle3 == FontStyle.Regular)
					{
						num = 4;
						continue;
					}
					try
					{
						return new Font(A_0, A_1, fontStyle3);
					}
					catch (Exception)
					{
						return null;
					}
					goto IL_AC;
				case 7:
					goto IL_72;
				case 8:
					goto IL_8F;
				case 9:
					goto IL_58;
				case 10:
					if (A_0.IsStyleAvailable(FontStyle.Regular))
					{
						num = 8;
						continue;
					}
					num = 5;
					continue;
				}
				if (A_0 == null)
				{
					num = 9;
					continue;
				}
				IL_AC:
				num = 2;
			}
			IL_58:
			goto IL_29C;
			IL_72:
			return new Font(A_0, A_1, fontStyle);
			IL_8F:
			return new Font(A_0, A_1, fontStyle3);
			IL_D6:
			return null;
			IL_E1:
			return null;
			IL_29C:
			return null;
		}
		}
	}

	// Token: 0x040024BD RID: 9405
	private static FontFamily ᜀ;
}
