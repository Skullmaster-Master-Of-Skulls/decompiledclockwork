using System;
using System.Drawing;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;

// Token: 0x02000370 RID: 880
internal class spr᱙
{
	// Token: 0x06003157 RID: 12631 RVA: 0x002D9A54 File Offset: 0x002D8A54
	public static void ᜀ(spr\u252D A_0, TextBoxFormat A_1)
	{
		for (;;)
		{
			A_1.HorizontalPosition = (float)A_0.\u1713() / 20f;
			A_1.VerticalPosition = (float)A_0.ᜠ() / 20f;
			A_1.Width = (float)A_0.\u1712() / 20f;
			A_1.Height = (float)A_0.\u171F() / 20f;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_1.InternalMargin.ᜃ(A_0.ᜅ() / 12700f);
					num = 6;
					continue;
				case 1:
					if (A_0.ᜅ() != 4294967295U)
					{
						num = 0;
						continue;
					}
					goto IL_F8;
				case 2:
					A_1.InternalMargin.ᜂ(A_0.ᜆ() / 12700f);
					num = 10;
					continue;
				case 3:
					goto IL_AB;
				case 4:
					if (A_0.ᜉ() != 4294967295U)
					{
						num = 3;
						continue;
					}
					goto IL_186;
				case 5:
					if (A_0.ᜆ() != 4294967295U)
					{
						num = 2;
						continue;
					}
					goto IL_119;
				case 6:
					goto IL_F8;
				case 7:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AB;
					default:
						if (false)
						{
						}
						goto IL_186;
					}
					break;
				case 8:
					if (A_0.ᜊ() != 4294967295U)
					{
						num = 11;
						continue;
					}
					goto IL_1DA;
				case 9:
					goto IL_15E;
				case 10:
					goto IL_119;
				case 11:
					A_1.InternalMargin.ᜀ(A_0.ᜊ() / 12700f);
					num = 9;
					continue;
				}
				break;
				IL_AB:
				A_1.InternalMargin.ᜁ(A_0.ᜉ() / 12700f);
				num = 7;
				continue;
				IL_F8:
				num = 4;
				continue;
				IL_119:
				num = 1;
				continue;
				IL_186:
				num = 8;
			}
		}
		IL_15E:
		IL_1DA:
		A_1.HorizontalAlignment = A_0.ᜐ();
		A_1.VerticalAlignment = A_0.ᜏ();
		A_1.HorizontalOrigin = A_0.\u1719();
		A_1.VerticalOrigin = A_0.\u1714();
		A_1.FillColor = A_0.ᜋ();
		A_1.LineColor = A_0.ᜂ();
		A_1.LineDashing = A_0.ᜄ();
		A_1.LineStyle = A_0.ᜌ();
		A_1.LineWidth = A_0.ᜁ();
		A_1.NoLine = A_0.ᜈ();
		A_1.TextWrappingStyle = A_0.\u1716();
		A_1.TextWrappingType = A_0.\u171E();
		A_1.WrappingMode = A_0.ᜎ();
		A_1.IsBelowText = A_0.\u1715();
		A_1.TextBoxIdentificator = A_0.ᜀ();
		A_1.IsHeaderTextBox = A_0.ᜢ();
		A_1.TextBoxShapeID = A_0.ᜡ();
	}

	// Token: 0x06003158 RID: 12632 RVA: 0x002D9D08 File Offset: 0x002D8D08
	public static void ᜀ(TextBoxFormat A_0, spr\u252D A_1)
	{
		for (;;)
		{
			A_1.ᜂ((int)Math.Round((double)(A_0.HorizontalPosition * 20f)));
			A_1.ᜇ((int)Math.Round((double)(A_0.VerticalPosition * 20f)));
			A_1.ᜆ((int)Math.Round((double)(A_0.Width * 20f)));
			A_1.ᜃ((int)Math.Round((double)(A_0.Height * 20f)));
			A_1.ᜀ(A_0.HorizontalOrigin);
			A_1.ᜀ(A_0.VerticalOrigin);
			A_1.ᜀ(A_0.HorizontalAlignment);
			A_1.ᜀ(A_0.VerticalAlignment);
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_17B;
				case 1:
					A_1.ᜂ((uint)Math.Round((double)(A_0.InternalMargin.ᜃ() * 12700f)));
					num = 6;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FC;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_204;
					}
					break;
				case 3:
					if (A_0.InternalMargin.ᜂ() != 3.685f)
					{
						num = 7;
						continue;
					}
					goto IL_204;
				case 4:
					goto IL_1D1;
				case 5:
					A_1.ᜃ((uint)Math.Round((double)(A_0.InternalMargin.ᜀ() * 12700f)));
					num = 4;
					continue;
				case 6:
					goto IL_14E;
				case 7:
					goto IL_FC;
				case 8:
					if (A_0.InternalMargin.ᜀ() != 3.685f)
					{
						num = 5;
						continue;
					}
					goto IL_269;
				case 9:
					if (A_0.InternalMargin.ᜄ() != 7.087f)
					{
						num = 11;
						continue;
					}
					goto IL_17B;
				case 10:
					if (A_0.InternalMargin.ᜃ() != 7.087f)
					{
						num = 1;
						continue;
					}
					goto IL_14E;
				case 11:
					A_1.ᜀ((uint)Math.Round((double)(A_0.InternalMargin.ᜄ() * 12700f)));
					num = 0;
					continue;
				}
				break;
				IL_FC:
				A_1.ᜁ((uint)Math.Round((double)(A_0.InternalMargin.ᜂ() * 12700f)));
				num = 2;
				continue;
				IL_14E:
				num = 3;
				continue;
				IL_17B:
				num = 10;
				continue;
				IL_204:
				num = 8;
			}
		}
		IL_1D1:
		IL_269:
		A_1.ᜁ(A_0.FillColor);
		A_1.ᜀ(A_0.LineColor);
		A_1.ᜀ(A_0.LineDashing);
		A_1.ᜀ(A_0.LineStyle);
		A_1.ᜀ(A_0.LineWidth);
		A_1.ᜀ(A_0.NoLine);
		A_1.ᜀ(A_0.TextWrappingStyle);
		A_1.ᜀ(A_0.TextWrappingType);
		A_1.ᜀ(A_0.WrappingMode);
		A_1.ᜃ(A_0.IsBelowText);
		A_1.ᜄ(A_0.TextBoxShapeID);
		A_1.ᜁ(A_0.TextBoxIdentificator);
	}

	// Token: 0x06003159 RID: 12633 RVA: 0x002DA010 File Offset: 0x002D9010
	internal static void ᜀ(spr\u2459 A_0, sprᨼ A_1, TextBoxFormat A_2, bool A_3)
	{
		for (;;)
		{
			A_2.HorizontalPosition = (float)A_1.\u1713() / 20f;
			A_2.VerticalPosition = (float)A_1.ᜠ() / 20f;
			int num = 39;
			for (;;)
			{
				uint num2;
				bool flag;
				switch (num)
				{
				case 0:
					goto IL_207;
				case 1:
					num2 = A_0.ᜁ(447);
					num = 28;
					continue;
				case 2:
					spr᱙.ᜁ(A_0, A_2);
					num = 18;
					continue;
				case 3:
					goto IL_229;
				case 4:
					goto IL_398;
				case 5:
					A_2.LineDashing = (LineDashing)num2;
					num = 3;
					continue;
				case 6:
					if (num2 != 4294967295U)
					{
						num = 45;
						continue;
					}
					goto IL_207;
				case 7:
					A_2.WrappingMode = (WrapMode)num2;
					num = 47;
					continue;
				case 8:
					goto IL_212;
				case 9:
					goto IL_476;
				case 10:
					if (num2 != 4294967295U)
					{
						num = 32;
						continue;
					}
					goto IL_476;
				case 11:
					A_2.HorizontalOrigin = A_1.\u1719();
					A_2.VerticalOrigin = A_1.\u1714();
					num = 27;
					continue;
				case 12:
					goto IL_33B;
				case 13:
					A_2.LayoutFlowAlt = (TextDirection)num2;
					num = 16;
					continue;
				case 14:
					A_2.IsBelowText = ((num2 & 32U) == 32U);
					num = 20;
					continue;
				case 15:
					if (num2 != 4294967295U)
					{
						num = 30;
						continue;
					}
					goto IL_196;
				case 16:
					goto IL_3C6;
				case 17:
					A_2.LineStyle = (TextBoxLineStyle)num2;
					num = 19;
					continue;
				case 18:
					goto IL_287;
				case 19:
					goto IL_2CB;
				case 20:
					goto IL_5EB;
				case 21:
					if (num2 != 4294967295U)
					{
						num = 17;
						continue;
					}
					goto IL_2CB;
				case 22:
					if (!flag)
					{
						num = 38;
						continue;
					}
					goto IL_511;
				case 23:
					if (num2 != 4294967295U)
					{
						num = 13;
						continue;
					}
					goto IL_3C6;
				case 24:
					A_2.FillColor = Color.White;
					num = 36;
					continue;
				case 25:
					if (num2 != 4294967295U)
					{
						num = 37;
						continue;
					}
					goto IL_33B;
				case 26:
					A_2.FillColor = sprṡ.ᜀ(num2);
					num = 0;
					continue;
				case 27:
					goto IL_53F;
				case 28:
					if ((num2 & 16U) == 16U)
					{
						num = 24;
						continue;
					}
					goto IL_3F1;
				case 29:
					goto IL_196;
				case 30:
					A_2.LineWidth = num2 / 12700f;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_212;
					default:
						if (false)
						{
						}
						num = 29;
						continue;
					}
					break;
				case 31:
					if (A_0.\u1713() != null)
					{
						num = 2;
						continue;
					}
					goto IL_60E;
				case 32:
					A_2.LineColor = sprṡ.ᜀ(num2);
					num = 9;
					continue;
				case 33:
					if ((num2 & 1048576U) == 1048576U)
					{
						num = 41;
						continue;
					}
					goto IL_511;
				case 34:
					A_2.TextAnchor = (ShapeVerticalAlignment)num2;
					num = 4;
					continue;
				case 35:
					goto IL_511;
				case 36:
					goto IL_3F1;
				case 37:
					A_2.NoLine = ((num2 & 8U) == 0U);
					num = 12;
					continue;
				case 38:
					num = 33;
					continue;
				case 39:
					if (!A_3)
					{
						num = 11;
						continue;
					}
					goto IL_53F;
				case 40:
					if (num2 != 4294967295U)
					{
						num = 7;
						continue;
					}
					goto IL_30D;
				case 41:
					A_2.FillColor = Color.Empty;
					num = 35;
					continue;
				case 42:
					if (num2 != 4294967295U)
					{
						num = 34;
						continue;
					}
					goto IL_398;
				case 43:
					goto IL_5EB;
				case 44:
					if (num2 != 4294967295U)
					{
						num = 5;
						continue;
					}
					goto IL_229;
				case 45:
					num = 48;
					continue;
				case 46:
					if (num2 != 4294967295U)
					{
						num = 14;
						continue;
					}
					A_2.IsBelowText = false;
					num = 43;
					continue;
				case 47:
					goto IL_30D;
				case 48:
					if (true)
					{
					}
					if (A_2.FillEfects.Type == BackgroundType.NoBackground)
					{
						num = 26;
						continue;
					}
					goto IL_207;
				}
				break;
				IL_196:
				num2 = A_0.ᜁ(461);
				num = 21;
				continue;
				IL_207:
				num = 8;
				continue;
				IL_212:
				if (num2 == 4294967295U)
				{
					num = 1;
					continue;
				}
				goto IL_3F1;
				IL_229:
				num2 = A_0.ᜁ(133);
				num = 40;
				continue;
				IL_2CB:
				num2 = A_0.ᜁ(462);
				num = 44;
				continue;
				IL_30D:
				num2 = A_0.ᜁ(136);
				num = 23;
				continue;
				IL_33B:
				A_2.TextBoxIdentificator = A_0.ᜁ(128);
				num2 = A_0.ᜁ(959);
				num = 46;
				continue;
				IL_398:
				num2 = A_0.ᜁ(385);
				num = 6;
				continue;
				IL_3C6:
				num2 = A_0.ᜁ(135);
				num = 42;
				continue;
				IL_3F1:
				num2 = A_0.ᜁ(447);
				flag = ((num2 & 16U) == 16U);
				num = 22;
				continue;
				IL_476:
				num2 = A_0.ᜁ(511);
				num = 25;
				continue;
				IL_511:
				num2 = A_0.ᜁ(448);
				num = 10;
				continue;
				IL_53F:
				A_2.Width = (float)A_1.\u1712() / 20f;
				A_2.Height = (float)A_1.\u171F() / 20f;
				A_2.TextWrappingStyle = A_1.\u1716();
				A_2.TextWrappingType = A_1.\u171E();
				A_2.IsHeaderTextBox = A_1.ᜢ();
				A_2.TextBoxShapeID = A_1.ᜡ();
				A_2.ᜀ(A_0, A_2.Document);
				num2 = A_0.ᜁ(459);
				num = 15;
				continue;
				IL_5EB:
				num = 31;
			}
		}
		IL_287:
		IL_60E:
		spr᱙.ᜀ(A_0, A_2);
	}

	// Token: 0x0600315A RID: 12634 RVA: 0x002DA634 File Offset: 0x002D9634
	internal static void ᜀ(sprᨼ A_0, TextBoxFormat A_1)
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
		A_0.ᜂ((int)Math.Round((double)(A_1.HorizontalPosition * 20f)));
		A_0.ᜇ((int)Math.Round((double)(A_1.VerticalPosition * 20f)));
		A_0.ᜆ((int)Math.Round((double)(A_1.Width * 20f)));
		A_0.ᜃ((int)Math.Round((double)(A_1.Height * 20f)));
		A_0.ᜀ(A_1.HorizontalOrigin);
		A_0.ᜀ(A_1.VerticalOrigin);
		A_0.ᜀ(A_1.TextWrappingStyle);
		A_0.ᜀ(A_1.TextWrappingType);
		A_0.ᜃ(A_1.IsBelowText);
		A_0.ᜄ(A_1.TextBoxShapeID);
	}

	// Token: 0x0600315B RID: 12635 RVA: 0x002DA71C File Offset: 0x002D971C
	private static void ᜁ(spr\u2459 A_0, TextBoxFormat A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				A_1.HorizontalOrigin = (HorizontalOrigin)A_0.\u1713().ᜉ();
				num = 8;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A2;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					A_1.HorizontalAlignment = (ShapeHorizontalAlignment)A_0.\u1713().ᜁ();
					num = 9;
					continue;
				}
				break;
			case 3:
				A_1.VerticalOrigin = (VerticalOrigin)A_0.\u1713().ᜅ();
				num = 11;
				continue;
			case 4:
				if (A_0.\u1713().ᜉ() != 4294967295U)
				{
					num = 1;
					continue;
				}
				goto IL_132;
			case 5:
				A_1.VerticalAlignment = (ShapeVerticalAlignment)A_0.\u1713().ᜎ();
				num = 7;
				continue;
			case 6:
				if (A_0.\u1713().ᜎ() != 4294967295U)
				{
					num = 5;
					continue;
				}
				goto IL_A2;
			case 7:
				goto IL_A2;
			case 8:
				goto IL_132;
			case 9:
				goto IL_C8;
			case 10:
				if (A_0.\u1713().ᜅ() != 4294967295U)
				{
					num = 3;
					continue;
				}
				return;
			case 11:
				return;
			}
			if (A_0.\u1713().ᜁ() != 4294967295U)
			{
				num = 2;
				continue;
			}
			goto IL_C8;
			IL_A2:
			num = 4;
			continue;
			IL_C8:
			num = 6;
			continue;
			IL_132:
			num = 10;
		}
	}

	// Token: 0x0600315C RID: 12636 RVA: 0x002DA8A4 File Offset: 0x002D98A4
	private static void ᜀ(spr\u2459 A_0, TextBoxFormat A_1)
	{
		for (;;)
		{
			uint num = A_0.ᜁ(129);
			int num2 = 11;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F2;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_134;
					}
					break;
				case 1:
					goto IL_A2;
				case 2:
					goto IL_F2;
				case 3:
					return;
				case 4:
					if (num != 4294967295U)
					{
						num2 = 2;
						continue;
					}
					return;
				case 5:
					A_1.InternalMargin.ᜁ(num / 12700f);
					num2 = 0;
					continue;
				case 6:
					goto IL_CA;
				case 7:
					if (num != 4294967295U)
					{
						num2 = 10;
						continue;
					}
					goto IL_A2;
				case 8:
					if (num != 4294967295U)
					{
						num2 = 5;
						continue;
					}
					goto IL_134;
				case 9:
					A_1.InternalMargin.ᜂ(num / 12700f);
					num2 = 6;
					continue;
				case 10:
					A_1.InternalMargin.ᜃ(num / 12700f);
					num2 = 1;
					continue;
				case 11:
					if (num != 4294967295U)
					{
						num2 = 9;
						continue;
					}
					goto IL_CA;
				}
				break;
				IL_A2:
				num = A_0.ᜁ(130);
				num2 = 8;
				continue;
				IL_CA:
				num = A_0.ᜁ(131);
				num2 = 7;
				continue;
				IL_F2:
				A_1.InternalMargin.ᜀ(num / 12700f);
				num2 = 3;
				continue;
				IL_134:
				num = A_0.ᜁ(132);
				num2 = 4;
			}
		}
	}
}
