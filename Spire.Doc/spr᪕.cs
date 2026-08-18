using System;
using System.Drawing;

// Token: 0x0200034E RID: 846
internal class spr᪕
{
	// Token: 0x06002D2B RID: 11563 RVA: 0x002B5B80 File Offset: 0x002B4B80
	internal spr᪕(spr\u187D A_0)
	{
		this.ᜀ = A_0;
		this.ᜁ = new PointF(spr᪕.ᜂ(A_0, true), spr᪕.ᜂ(A_0, false));
		this.ᜂ = new PointF(spr᪕.ᜁ(A_0, true), spr᪕.ᜁ(A_0, false));
		this.ᜃ = A_0.ᜁ();
	}

	// Token: 0x06002D2C RID: 11564 RVA: 0x002B5BDC File Offset: 0x002B4BDC
	internal PointF ᜀ(float A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 > 1f)
					{
						num = 5;
						continue;
					}
					num = 7;
					continue;
				case 1:
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
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_97;
				case 3:
					goto IL_7B;
				case 4:
					if (A_0 == 1f)
					{
						num = 3;
						continue;
					}
					goto IL_105;
				case 5:
					goto IL_D5;
				case 7:
					if (A_0 == 0f)
					{
						num = 2;
						continue;
					}
					num = 4;
					continue;
				}
				IL_40:
				if (A_0 >= 0f)
				{
					num = 1;
					continue;
				}
				goto IL_F0;
				goto IL_40;
			}
			IL_7B:
			return this.ᜀ.ᜀ();
			IL_97:
			return this.ᜀ.ᜁ();
			IL_D5:
			IL_F0:
			return PointF.Empty;
			IL_105:
			float x = this.ᜀ(A_0, true);
			float y = this.ᜀ(A_0, false);
			return new PointF(x, y);
		}
		}
	}

	// Token: 0x06002D2D RID: 11565 RVA: 0x002B5D08 File Offset: 0x002B4D08
	private float ᜀ(float A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			PointF pointF5;
			PointF pointF6;
			float num4;
			float num5;
			for (;;)
			{
				float num2;
				float num3;
				PointF pointF3;
				switch (num)
				{
				case 1:
				{
					PointF pointF;
					num2 = pointF.Y;
					goto IL_153;
				}
				case 2:
				{
					PointF pointF = this.ᜂ;
					num = 1;
					continue;
				}
				case 3:
					goto IL_97;
				case 4:
				{
					PointF pointF2;
					num3 = pointF2.Y;
					goto IL_118;
				}
				case 5:
					num3 = pointF3.X;
					goto IL_118;
				case 6:
				{
					PointF pointF4;
					num2 = pointF4.X;
					goto IL_153;
				}
				case 7:
					if (!A_1)
					{
						num = 11;
						continue;
					}
					pointF5 = this.ᜃ;
					if (true)
					{
					}
					num = 3;
					continue;
				case 8:
					goto IL_D3;
				case 9:
				{
					PointF pointF2 = this.ᜁ;
					num = 4;
					continue;
				}
				case 10:
				{
					if (!A_1)
					{
						num = 2;
						continue;
					}
					PointF pointF4 = this.ᜂ;
					num = 6;
					continue;
				}
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_97;
					default:
						if (false)
						{
						}
						pointF6 = this.ᜃ;
						num = 8;
						continue;
					}
					break;
				}
				if (!A_1)
				{
					num = 9;
					continue;
				}
				pointF3 = this.ᜁ;
				num = 5;
				continue;
				IL_118:
				num4 = num3;
				num = 10;
				continue;
				IL_153:
				num5 = num2;
				num = 7;
			}
			IL_97:
			float num6 = pointF5.X;
			goto IL_181;
			IL_D3:
			num6 = pointF6.Y;
			IL_181:
			float num7 = num6;
			return num4 * sprὍ.ᜀ(A_0) + A_0 * num5 + num7;
		}
		}
	}

	// Token: 0x06002D2E RID: 11566 RVA: 0x002B5EA8 File Offset: 0x002B4EA8
	private static float ᜂ(spr\u187D A_0, bool A_1)
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
		float[] array = spr᪕.ᜀ(A_0, A_1);
		float num = array[0];
		float num2 = array[1];
		float num3 = array[2];
		return num - 2f * num2 + num3;
	}

	// Token: 0x06002D2F RID: 11567 RVA: 0x002B5F04 File Offset: 0x002B4F04
	private static float ᜁ(spr\u187D A_0, bool A_1)
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
		float[] array = spr᪕.ᜀ(A_0, A_1);
		float num = array[0];
		float num2 = array[1];
		return -2f * num + 2f * num2;
	}

	// Token: 0x06002D30 RID: 11568 RVA: 0x002B5F60 File Offset: 0x002B4F60
	private static float[] ᜀ(spr\u187D A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			float[] array;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_CB;
				default:
				{
					if (false)
					{
					}
					array = new float[3];
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							array[1] = (A_1 ? A_0.ᜂ().X : A_0.ᜂ().Y);
							if (true)
							{
							}
							num = 2;
							continue;
						case 1:
							array[0] = (A_1 ? A_0.ᜁ().X : A_0.ᜁ().Y);
							num = 0;
							continue;
						case 2:
							goto IL_CB;
						}
						break;
					}
					break;
				}
				}
			}
			IL_CB:
			array[2] = (A_1 ? A_0.ᜀ().X : A_0.ᜀ().Y);
			return array;
		}
		}
	}

	// Token: 0x06002D31 RID: 11569 RVA: 0x002B6068 File Offset: 0x002B5068
	internal spr\u187D ᜀ()
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

	// Token: 0x06002D32 RID: 11570 RVA: 0x002B60AC File Offset: 0x002B50AC
	internal PointF ᜂ()
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

	// Token: 0x06002D33 RID: 11571 RVA: 0x002B60F0 File Offset: 0x002B50F0
	internal PointF ᜃ()
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

	// Token: 0x06002D34 RID: 11572 RVA: 0x002B6134 File Offset: 0x002B5134
	internal PointF ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x04002672 RID: 9842
	private readonly spr\u187D ᜀ;

	// Token: 0x04002673 RID: 9843
	private readonly PointF ᜁ;

	// Token: 0x04002674 RID: 9844
	private readonly PointF ᜂ;

	// Token: 0x04002675 RID: 9845
	private readonly PointF ᜃ;
}
