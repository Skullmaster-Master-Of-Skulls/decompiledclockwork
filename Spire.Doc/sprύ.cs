using System;
using System.Drawing;
using Spire.Doc.Documents;

// Token: 0x02000217 RID: 535
internal class sprύ
{
	// Token: 0x0600192F RID: 6447 RVA: 0x001899F8 File Offset: 0x001889F8
	internal static void ᜀ(sprṏ A_0, PointF A_1, PointF A_2)
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
		spr\u1B70 spr_u1B = new spr\u1B70(A_0.ᜇ());
		spr_u1B.ᜀ(A_0.ᜎ());
		spr\u1926 spr_u = new spr\u1926();
		spr_u1B.ᜁ(spr_u);
		spr_u.ᜁ(A_1, A_2);
		A_0.ᜏ().ᜁ(spr_u1B);
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x00189A70 File Offset: 0x00188A70
	internal static float ᜀ(spr\u2587 A_0)
	{
		for (;;)
		{
			IL_14:
			BorderStyle borderStyle = A_0.ᜈ();
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch (borderStyle)
						{
						case BorderStyle.Single:
						case BorderStyle.Dot:
						case BorderStyle.DashLargeGap:
						case BorderStyle.DotDash:
						case BorderStyle.DotDotDash:
						case BorderStyle.Wave:
						case BorderStyle.DoubleWave:
						case BorderStyle.DashSmallGap:
						case BorderStyle.DashDotStroker:
							goto IL_F3;
						case BorderStyle.Thick:
						case BorderStyle.Hairline:
							goto IL_AC;
						case BorderStyle.Double:
						case BorderStyle.Triple:
						case BorderStyle.ThinThickSmallGap:
						case BorderStyle.ThinThinSmallGap:
						case BorderStyle.ThinThickThinSmallGap:
						case BorderStyle.ThinThickMediumGap:
						case BorderStyle.ThickThinMediumGap:
						case BorderStyle.ThickThickThinMediumGap:
						case BorderStyle.ThinThickLargeGap:
						case BorderStyle.ThickThinLargeGap:
						case BorderStyle.ThinThickThinLargeGap:
						case BorderStyle.Emboss3D:
						case BorderStyle.Engrave3D:
							goto IL_CE;
						case (BorderStyle)4:
							goto IL_FA;
						case BorderStyle.Outset:
						case BorderStyle.Inset:
							goto IL_ED;
						default:
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_EB;
					case 2:
						num = 1;
						continue;
					}
					goto IL_14;
				}
				IL_AC:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_C2;
				}
			}
		}
		IL_C2:
		if (false)
		{
		}
		return 1f;
		IL_CE:
		return (float)A_0.\u1715();
		IL_EB:
		goto IL_FA;
		IL_ED:
		return 1f;
		IL_F3:
		return A_0.\u171E();
		IL_FA:
		return A_0.\u171E();
	}
}
