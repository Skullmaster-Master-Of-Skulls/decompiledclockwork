using System;
using System.Collections;
using Spire.Doc;

// Token: 0x0200021D RID: 541
internal class sprῠ
{
	// Token: 0x06001959 RID: 6489 RVA: 0x0018B450 File Offset: 0x0018A450
	internal sprῠ()
	{
		this.ᜁ.Push(null);
	}

	// Token: 0x0600195A RID: 6490 RVA: 0x0018B47C File Offset: 0x0018A47C
	internal void ᜂ(spr\u1C3B A_0)
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
		this.ᜁ.Push(sprῠ.ᜀ(A_0) ? A_0 : this.ᜁ.Peek());
	}

	// Token: 0x0600195B RID: 6491 RVA: 0x0018B4DC File Offset: 0x0018A4DC
	internal void ᜀ()
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
		this.ᜁ.Pop();
	}

	// Token: 0x0600195C RID: 6492 RVA: 0x0018B524 File Offset: 0x0018A524
	internal spr\u2262 ᜃ(spr\u2262 A_0)
	{
		int num = 3;
		spr\u1C3B spr_u1C3B;
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
					goto IL_75;
				}
				break;
			case 1:
				goto IL_5D;
			case 2:
				if (spr_u1C3B != null)
				{
					num = 1;
					continue;
				}
				goto IL_8F;
			}
			IL_20:
			if (!A_0.ᜇ())
			{
				num = 0;
				continue;
			}
			spr_u1C3B = (spr\u1C3B)this.ᜁ.Peek();
			num = 2;
			continue;
			goto IL_20;
		}
		IL_5D:
		return sprῠ.ᜁ(spr_u1C3B);
		IL_75:
		if (true)
		{
		}
		if (false)
		{
		}
		return A_0;
		IL_8F:
		return spr\u2262.ᜋ;
	}

	// Token: 0x0600195D RID: 6493 RVA: 0x0018B5C8 File Offset: 0x0018A5C8
	internal static spr\u2262 ᜂ(spr\u2262 A_0)
	{
		if (A_0.ᜇ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return sprῠ.ᜂ;
			}
		}
		return A_0;
	}

	// Token: 0x0600195E RID: 6494 RVA: 0x0018B614 File Offset: 0x0018A614
	internal static spr\u2262 ᜁ(spr\u2262 A_0)
	{
		if (A_0.ᜇ())
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				return spr\u2262.ᜋ;
			}
		}
		return A_0;
	}

	// Token: 0x0600195F RID: 6495 RVA: 0x0018B660 File Offset: 0x0018A660
	private static spr\u2262 ᜁ(spr\u1C3B A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				TextureStyle textureStyle = A_0.ᜈ();
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (textureStyle == TextureStyle.TextureNil)
						{
							num = 10;
							continue;
						}
						num = 2;
						continue;
					case 1:
					{
						if (A_0.ᜄ().ᜇ())
						{
							goto IL_8D;
						}
						float num2 = sprῠ.ᜀ(A_0.ᜄ());
						num = 4;
						continue;
					}
					case 2:
					{
						if (spr\u1AEB.ᜀ(textureStyle))
						{
							num = 5;
							continue;
						}
						float num3 = (float)spr\u1AEB.ᜂ(textureStyle);
						spr\u2262 a_ = sprῠ.ᜁ(A_0.ᜑ());
						float num4 = sprῠ.ᜀ(a_) * num3;
						float num5 = 1f - num3;
						spr\u2262 a_2 = sprῠ.ᜂ(A_0.ᜄ());
						float num6 = sprῠ.ᜀ(a_2) * num5;
						float num7 = num4 + num6;
						num = 6;
						continue;
					}
					case 3:
						num = 0;
						continue;
					case 4:
					{
						float num2;
						if (num2 <= 0.238f)
						{
							num = 8;
							continue;
						}
						goto IL_6E;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8D;
						default:
							goto IL_171;
						}
						break;
					case 6:
					{
						float num7;
						if (num7 <= 0.238f)
						{
							num = 7;
							continue;
						}
						goto IL_1BF;
					}
					case 7:
						goto IL_111;
					case 8:
						goto IL_1B1;
					case 9:
						if (textureStyle != TextureStyle.TextureNone)
						{
							num = 3;
							continue;
						}
						goto IL_74;
					case 10:
						goto IL_74;
					case 11:
						goto IL_A1;
					}
					break;
					IL_74:
					num = 1;
					continue;
					IL_8D:
					if (true)
					{
					}
					num = 11;
				}
			}
			IL_6E:
			return spr\u2262.ᜋ;
			IL_A1:
			return spr\u2262.ᜋ;
			IL_111:
			return spr\u2262.ឌ;
			IL_171:
			if (false)
			{
			}
			return spr\u2262.ᜋ;
			IL_1B1:
			return spr\u2262.ឌ;
			IL_1BF:
			return spr\u2262.ᜋ;
		}
	}

	// Token: 0x06001960 RID: 6496 RVA: 0x0018B83C File Offset: 0x0018A83C
	private static bool ᜀ(spr\u1C3B A_0)
	{
		if (A_0 == null)
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
				return false;
			}
		}
		if (true)
		{
		}
		return A_0.ᜐ();
	}

	// Token: 0x06001961 RID: 6497 RVA: 0x0018B884 File Offset: 0x0018A884
	private static float ᜀ(spr\u2262 A_0)
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
		float num = 0.3f * (float)A_0.ᜃ() / 255f;
		float num2 = 0.59f * (float)A_0.ᜆ() / 255f;
		float num3 = 0.11f * (float)A_0.ᜄ() / 255f;
		return num + num2 + num3;
	}

	// Token: 0x06001962 RID: 6498 RVA: 0x0018B904 File Offset: 0x0018A904
	// Note: this type is marked as 'beforefieldinit'.
	static sprῠ()
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
		sprῠ.ᜂ = spr\u2262.ឌ;
	}

	// Token: 0x04001CEF RID: 7407
	private const float ᜀ = 0.238f;

	// Token: 0x04001CF0 RID: 7408
	private readonly Stack ᜁ = new Stack();

	// Token: 0x04001CF1 RID: 7409
	private static readonly spr\u2262 ᜂ;
}
