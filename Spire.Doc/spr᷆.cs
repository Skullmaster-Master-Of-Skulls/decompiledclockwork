using System;
using Spire.Doc.Fields.Shape;

// Token: 0x02000293 RID: 659
internal class spr\u1DC6
{
	// Token: 0x0600230E RID: 8974 RVA: 0x0023C430 File Offset: 0x0023B430
	private spr\u1DC6()
	{
	}

	// Token: 0x0600230F RID: 8975 RVA: 0x0023C444 File Offset: 0x0023B444
	internal static void ᜀ(spr\u2588 A_0)
	{
		switch (0)
		{
		default:
		{
			sprỬ[] array;
			for (;;)
			{
				EsShapePath esShapePath = EsShapePath.LinesClosed;
				object obj = A_0.ᜁ(324);
				int num = 18;
				for (;;)
				{
					spr\u2055[] array2;
					ShapeType shapeType;
					switch (num)
					{
					case 0:
						goto IL_BA;
					case 1:
						goto IL_13B;
					case 2:
					{
						EsShapePath esShapePath2;
						switch (esShapePath2)
						{
						case EsShapePath.Lines:
							array = spr\u1DC6.ᜀ(array2);
							num = 0;
							continue;
						case EsShapePath.LinesClosed:
							array = new sprỬ[]
							{
								new sprỬ(PathType.MoveTo, 0),
								new sprỬ(PathType.LineTo, array2.Length - 1),
								new sprỬ(PathType.Close, 1),
								new sprỬ(PathType.End, 0)
							};
							num = 21;
							continue;
						case EsShapePath.Curves:
						{
							if (true)
							{
							}
							array = new sprỬ[3];
							array[0] = new sprỬ(PathType.MoveTo, 0);
							int a_ = (array2.Length - 1) / 3;
							array[1] = new sprỬ(PathType.CurveTo, a_);
							array[2] = new sprỬ(PathType.End, 0);
							num = 10;
							continue;
						}
						case EsShapePath.CurvesClosed:
						{
							array = new sprỬ[4];
							array[0] = new sprỬ(PathType.MoveTo, 0);
							int a_2 = (array2.Length - 1) / 3;
							array[1] = new sprỬ(PathType.CurveTo, a_2);
							array[2] = new sprỬ(PathType.Close, 1);
							array[3] = new sprỬ(PathType.End, 0);
							num = 14;
							continue;
						}
						default:
							num = 20;
							continue;
						}
						break;
					}
					case 3:
						if (array2.Length == 0)
						{
							num = 7;
							continue;
						}
						array = (sprỬ[])A_0.ᜁ(326);
						num = 6;
						continue;
					case 4:
					{
						if (shapeType == ShapeType.Arc)
						{
							num = 11;
							continue;
						}
						EsShapePath esShapePath2 = esShapePath;
						num = 2;
						continue;
					}
					case 5:
						goto IL_ED;
					case 6:
						if (array != null)
						{
							num = 12;
							continue;
						}
						goto IL_192;
					case 7:
						goto IL_1EA;
					case 8:
						if (array2 != null)
						{
							num = 16;
							continue;
						}
						return;
					case 9:
						goto IL_18D;
					case 10:
						goto IL_136;
					case 11:
						array = spr\u1DC6.ᜀ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_292;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 12:
						num = 13;
						continue;
					case 13:
						if (array.Length > 0)
						{
							goto IL_292;
						}
						goto IL_192;
					case 14:
						goto IL_234;
					case 15:
						array = spr\u1DC6.ᜀ(array2);
						num = 9;
						continue;
					case 16:
						num = 3;
						continue;
					case 17:
						return;
					case 18:
						if (obj != null)
						{
							num = 19;
							continue;
						}
						goto IL_13B;
					case 19:
						esShapePath = (EsShapePath)obj;
						A_0.Remove(324);
						num = 1;
						continue;
					case 20:
						num = 15;
						continue;
					case 21:
						goto IL_278;
					}
					break;
					IL_13B:
					array2 = (spr\u2055[])A_0.ᜁ(325);
					num = 8;
					continue;
					IL_192:
					shapeType = (ShapeType)A_0.ᜁ(4155);
					num = 4;
					continue;
					IL_292:
					num = 17;
				}
			}
			IL_BA:
			IL_ED:
			IL_136:
			IL_18D:
			goto IL_32C;
			IL_1EA:
			return;
			IL_234:
			IL_278:
			IL_32C:
			A_0.ᜁ(326, array);
			return;
		}
		}
	}

	// Token: 0x06002310 RID: 8976 RVA: 0x0023C78C File Offset: 0x0023B78C
	private static sprỬ[] ᜀ()
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
		spr\u2588 spr_u = sprᢴ.ᜀ(ShapeType.Arc);
		return (sprỬ[])spr_u.ᜁ(326);
	}

	// Token: 0x06002311 RID: 8977 RVA: 0x0023C7E0 File Offset: 0x0023B7E0
	private static sprỬ[] ᜀ(spr\u2055[] A_0)
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
		return new sprỬ[]
		{
			new sprỬ(PathType.MoveTo, 0),
			new sprỬ(PathType.LineTo, A_0.Length - 1),
			new sprỬ(PathType.End, 0)
		};
	}

	// Token: 0x06002312 RID: 8978 RVA: 0x0023C848 File Offset: 0x0023B848
	internal static sprỬ ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			IL_18:
			num = (A_0 & 57344) >> 13;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_96;
				case 1:
					num2 = 3;
					continue;
				case 2:
					if (num != 5)
					{
						num2 = 1;
						continue;
					}
					goto IL_43;
				case 3:
					if (num == 6)
					{
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					goto IL_98;
				}
				goto IL_18;
			}
			IL_43:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_59;
			}
			IL_96:
			goto IL_43;
		}
		IL_59:
		if (false)
		{
		}
		num = (A_0 & 65280) >> 8;
		return new sprỬ((PathType)num, A_0 & 255);
		IL_98:
		return new sprỬ((PathType)num, A_0 & 8191);
	}

	// Token: 0x06002313 RID: 8979 RVA: 0x0023C8FC File Offset: 0x0023B8FC
	internal static int ᜀ(sprỬ A_0)
	{
		int num = 0;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num2 = A_0.ᜅ();
				num2 |= (int)((int)A_0.ᜀ() << 8);
				num = 2;
				continue;
			case 2:
				return num2;
			case 3:
				return num2;
			}
			if (A_0.ᜀ() >= PathType.EscapeBase)
			{
				num = 1;
			}
			else
			{
				num2 = A_0.ᜅ();
				num2 |= (int)((int)A_0.ᜀ() << 13);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 3;
					break;
				}
			}
		}
		return num2;
	}

	// Token: 0x04002138 RID: 8504
	private const int ᜀ = 5;

	// Token: 0x04002139 RID: 8505
	private const int ᜁ = 6;
}
