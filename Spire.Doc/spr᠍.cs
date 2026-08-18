using System;
using System.Collections.Generic;

// Token: 0x02000258 RID: 600
internal class spr\u180D
{
	// Token: 0x06001E09 RID: 7689 RVA: 0x001DAA28 File Offset: 0x001D9A28
	internal static Random ᜃ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_72;
			case 2:
				for (;;)
				{
					spr\u180D.ᜀ = new Random(1000);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_64;
					}
				}
				IL_64:
				if (false)
				{
				}
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (spr\u180D.ᜀ != null)
			{
				break;
			}
			num = 2;
		}
		IL_72:
		return spr\u180D.ᜀ;
	}

	// Token: 0x06001E0A RID: 7690 RVA: 0x001DAAB0 File Offset: 0x001D9AB0
	internal static Dictionary<int, int> ᜂ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6D;
			case 1:
				for (;;)
				{
					spr\u180D.ᜃ = new Dictionary<int, int>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_5F;
					}
				}
				IL_5F:
				if (false)
				{
				}
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (spr\u180D.ᜃ != null)
			{
				break;
			}
			num = 1;
		}
		IL_6D:
		return spr\u180D.ᜃ;
	}

	// Token: 0x06001E0B RID: 7691 RVA: 0x001DAB34 File Offset: 0x001D9B34
	internal static List<int> ᜁ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					spr\u180D.ᜁ = new List<int>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_57;
					}
				}
				IL_57:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 1;
				continue;
			case 1:
				goto IL_6D;
			}
			if (spr\u180D.ᜁ != null)
			{
				break;
			}
			num = 0;
		}
		IL_6D:
		return spr\u180D.ᜁ;
	}

	// Token: 0x06001E0C RID: 7692 RVA: 0x001DABB8 File Offset: 0x001D9BB8
	internal static List<int> ᜀ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_6D;
			case 2:
				for (;;)
				{
					spr\u180D.ᜂ = new List<int>();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_57;
					}
				}
				IL_57:
				if (false)
				{
				}
				if (true)
				{
				}
				num = 1;
				continue;
			}
			if (spr\u180D.ᜂ != null)
			{
				break;
			}
			num = 2;
		}
		IL_6D:
		return spr\u180D.ᜂ;
	}

	// Token: 0x06001E0D RID: 7693 RVA: 0x001DAC3C File Offset: 0x001D9C3C
	internal static int ᜁ(int A_0)
	{
		int num = 5;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				return num2;
			case 1:
				return num2;
			case 2:
				return num2;
			case 3:
				if (!spr\u180D.ᜂ().ContainsKey(A_0))
				{
					num = 4;
					continue;
				}
				num2 = spr\u180D.ᜂ()[A_0];
				num = 7;
				continue;
			case 4:
				num2 = spr\u180D.ᜃ().Next();
				spr\u180D.ᜂ().Add(A_0, num2);
				spr\u180D.ᜁ().Add(num2);
				num = 0;
				continue;
			case 6:
				return A_0;
			case 7:
				if (spr\u180D.ᜀ(num2))
				{
					num = 8;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_58;
				default:
					if (false)
					{
					}
					num2 = spr\u180D.ᜃ().Next();
					spr\u180D.ᜁ().Add(num2);
					num = 2;
					continue;
				}
				break;
			case 8:
				goto IL_58;
			}
			if (spr\u180D.ᜀ().Contains(A_0))
			{
				num = 6;
				continue;
			}
			num2 = -1;
			if (true)
			{
			}
			num = 3;
			continue;
			IL_58:
			spr\u180D.ᜁ().Add(num2);
			num = 1;
		}
		return A_0;
	}

	// Token: 0x06001E0E RID: 7694 RVA: 0x001DAD94 File Offset: 0x001D9D94
	private static bool ᜀ(int A_0)
	{
		bool result;
		for (;;)
		{
			result = true;
			int num = 4;
			for (;;)
			{
				List<int>.Enumerator enumerator;
				switch (num)
				{
				case 0:
					if (spr\u180D.ᜁ.Count > 0)
					{
						num = 1;
						continue;
					}
					return result;
				case 1:
					goto IL_F2;
				case 2:
					try
					{
						num = 1;
						for (;;)
						{
							int num2;
							switch (num)
							{
							case 0:
								goto IL_D7;
							case 2:
								result = false;
								num = 0;
								continue;
							case 3:
								goto IL_D7;
							case 4:
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								goto IL_73;
							case 5:
								goto IL_E2;
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_73;
								default:
									if (false)
									{
									}
									if (num2 == A_0)
									{
										num = 2;
										continue;
									}
									break;
								}
								break;
							}
							goto IL_71;
							IL_73:
							num2 = enumerator.Current;
							num = 6;
							continue;
							IL_AD:
							num = 4;
							continue;
							IL_71:
							goto IL_AD;
							IL_D7:
							num = 5;
						}
						IL_E2:
						return result;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_F2;
				case 3:
					num = 0;
					continue;
				case 4:
					if (true)
					{
					}
					if (spr\u180D.ᜁ != null)
					{
						num = 3;
						continue;
					}
					return result;
				}
				break;
				IL_F2:
				enumerator = spr\u180D.ᜁ.GetEnumerator();
				num = 2;
			}
		}
		return result;
	}

	// Token: 0x06001E0F RID: 7695 RVA: 0x001DAEF0 File Offset: 0x001D9EF0
	internal static int ᜀ(int A_0, bool A_1)
	{
		int num = 5;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				if (!spr\u180D.ᜂ().ContainsKey(A_0))
				{
					num = 3;
					continue;
				}
				spr\u180D.ᜂ()[A_0] = num2;
				num = 4;
				continue;
			case 1:
				goto IL_66;
			case 2:
				goto IL_70;
			case 3:
				spr\u180D.ᜂ().Add(A_0, num2);
				num = 1;
				continue;
			case 4:
				goto IL_103;
			case 6:
				if (spr\u180D.ᜂ().ContainsKey(A_0))
				{
					num = 7;
					continue;
				}
				goto IL_70;
			case 7:
				num = 8;
				continue;
			case 8:
				if (A_1)
				{
					num = 2;
					continue;
				}
				goto IL_123;
			case 9:
				return A_0;
			}
			if (spr\u180D.ᜀ().Contains(A_0))
			{
				num = 9;
				continue;
			}
			num = 6;
			continue;
			IL_70:
			num2 = spr\u180D.ᜃ().Next();
			num = 0;
		}
		return A_0;
		IL_66:
		if (true)
		{
		}
		IL_CC:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_103:
			goto IL_CC;
		default:
		{
			if (false)
			{
			}
			int num2;
			return num2;
		}
		}
		IL_123:
		return spr\u180D.ᜂ()[A_0];
	}

	// Token: 0x04001F97 RID: 8087
	[ThreadStatic]
	private static Random ᜀ;

	// Token: 0x04001F98 RID: 8088
	[ThreadStatic]
	private static List<int> ᜁ;

	// Token: 0x04001F99 RID: 8089
	[ThreadStatic]
	private static List<int> ᜂ;

	// Token: 0x04001F9A RID: 8090
	[ThreadStatic]
	private static Dictionary<int, int> ᜃ;
}
