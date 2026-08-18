using System;
using System.Drawing;
using Spire.CompoundFile.Doc;

// Token: 0x02000163 RID: 355
internal class spr\u23C4
{
	// Token: 0x06000A20 RID: 2592 RVA: 0x00084B98 File Offset: 0x00083B98
	private spr\u23C4()
	{
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x00084BAC File Offset: 0x00083BAC
	public static double \u171C(double A_0)
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
		return spr\u23C4.ᜇ(A_0, 96.0);
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x00084BF8 File Offset: 0x00083BF8
	public static double ᜇ(double A_0, double A_1)
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
		return A_0 / 72.0 * A_1;
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00084C40 File Offset: 0x00083C40
	public static int \u171B(double A_0)
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
		return spr\u2109.ᜂ(spr\u23C4.\u171C(A_0));
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x00084C88 File Offset: 0x00083C88
	public static int ᜆ(double A_0, double A_1)
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
		return spr\u2109.ᜂ(spr\u23C4.ᜇ(A_0, A_1));
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00084CD0 File Offset: 0x00083CD0
	public static int ᜅ(double A_0, double A_1)
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
		return Math.Max((int)Math.Ceiling(spr\u23C4.ᜇ(A_0, A_1)), 1);
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00084D20 File Offset: 0x00083D20
	public static Size ᜀ(SizeF A_0, float A_1, double A_2)
	{
		int a_ = 3;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_73;
			case 1:
				if (A_2 <= 0.0)
				{
					num = 0;
					continue;
				}
				goto IL_AF;
			case 3:
				goto IL_41;
			}
			if (A_1 <= 0f)
			{
				if (true)
				{
				}
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		for (;;)
		{
			IL_41:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_8B;
			}
		}
		IL_8B:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(ClipboardData.b("ᩨࡪ౬ͮᑰ", a_));
		IL_73:
		throw new ArgumentOutOfRangeException(ClipboardData.b("൨᭪Ѭ", a_));
		IL_AF:
		return new Size(spr\u23C4.ᜆ((double)(A_0.Width * A_1), A_2), spr\u23C4.ᜆ((double)(A_0.Height * A_1), A_2));
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00084E04 File Offset: 0x00083E04
	public static double \u171A(double A_0)
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
		return A_0 / 0.002834645882776876;
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00084E4C File Offset: 0x00083E4C
	public static int \u1719(double A_0)
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
		return spr\u2109.ᜂ(spr\u23C4.\u171A(A_0));
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00084E94 File Offset: 0x00083E94
	public static double \u1718(double A_0)
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
		return spr\u23C4.ᜄ(A_0, 96.0);
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00084EE0 File Offset: 0x00083EE0
	public static RectangleF ᜅ(RectangleF A_0)
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
		return RectangleF.FromLTRB((float)spr\u23C4.\u1718((double)A_0.Left), (float)spr\u23C4.\u1718((double)A_0.Top), (float)spr\u23C4.\u1718((double)A_0.Right), (float)spr\u23C4.\u1718((double)A_0.Bottom));
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00084F58 File Offset: 0x00083F58
	public static double ᜄ(double A_0, double A_1)
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
		return A_0 / A_1 * 72.0;
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x00084FA0 File Offset: 0x00083FA0
	public static int ᜃ(double A_0, double A_1)
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
		return spr\u2109.ᜂ(A_0 / A_1 * 1440.0);
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x00084FF0 File Offset: 0x00083FF0
	public static int ᜀ(double A_0, double A_1, double A_2)
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
		return spr\u2109.ᜂ(A_0 * A_2 / A_1);
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x00085038 File Offset: 0x00084038
	public static double \u1717(double A_0)
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
		return A_0 * 72.0;
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x00085080 File Offset: 0x00084080
	public static double \u1716(double A_0)
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
		return A_0 / 72.0;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x000850C8 File Offset: 0x000840C8
	public static double \u1715(double A_0)
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
		return A_0 * 2.834645669291339;
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x00085110 File Offset: 0x00084110
	public static int \u1714(double A_0)
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
		return spr\u2109.ᜂ(A_0 * 56.69291338582678);
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0008515C File Offset: 0x0008415C
	public static int \u1713(double A_0)
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
		return spr\u2109.ᜂ(spr\u23C4.\u1712(A_0));
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x000851A4 File Offset: 0x000841A4
	public static double \u1712(double A_0)
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
		return spr\u23C4.ᜂ(A_0, 96.0);
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x000851F0 File Offset: 0x000841F0
	public static double ᜂ(double A_0, double A_1)
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
		return A_0 * A_1 / 25.4;
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00085238 File Offset: 0x00084238
	public static double ᜑ(double A_0)
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
		return spr\u23C4.ᜁ(A_0, 96.0);
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00085284 File Offset: 0x00084284
	public static double ᜁ(double A_0, double A_1)
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
		return A_0 * A_1;
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x000852C4 File Offset: 0x000842C4
	public static double ᜐ(double A_0)
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
		return A_0 * 28.34645669291339;
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x0008530C File Offset: 0x0008430C
	public static double ᜏ(double A_0)
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
		return A_0 * 12.0;
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00085354 File Offset: 0x00084354
	public static int \u1715(int A_0)
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
		return spr\u2109.ᜂ((double)A_0 * 240.0 / 100.0);
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x000853AC File Offset: 0x000843AC
	public static double ᜎ(double A_0)
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
		return A_0 / 12.0;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x000853F4 File Offset: 0x000843F4
	public static int \u170D(double A_0)
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
		return spr\u2109.ᜂ(A_0 * 2.0);
	}

	// Token: 0x06000A3C RID: 2620 RVA: 0x00085440 File Offset: 0x00084440
	public static double \u1714(int A_0)
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
		return (double)A_0 / 2.0;
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x00085488 File Offset: 0x00084488
	public static int ᜌ(double A_0)
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
		return spr\u2109.ᜂ(A_0 * 8.0);
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x000854D4 File Offset: 0x000844D4
	public static double \u1713(int A_0)
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
		return (double)A_0 / 8.0;
	}

	// Token: 0x06000A3F RID: 2623 RVA: 0x0008551C File Offset: 0x0008451C
	public static int \u1712(int A_0)
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
		return spr\u2109.ᜂ((double)A_0 * 2.5);
	}

	// Token: 0x06000A40 RID: 2624 RVA: 0x00085568 File Offset: 0x00084568
	public static int ᜋ(double A_0)
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
		return spr\u2109.ᜂ(A_0 * 20.0);
	}

	// Token: 0x06000A41 RID: 2625 RVA: 0x000855B4 File Offset: 0x000845B4
	public static double ᜑ(int A_0)
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
		return (double)A_0 / 20.0;
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x000855FC File Offset: 0x000845FC
	public static double ᜊ(double A_0)
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
		return A_0 / 20.0;
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x00085644 File Offset: 0x00084644
	public static double ᜐ(int A_0)
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
		return (double)A_0 / 56.69291338582678;
	}

	// Token: 0x06000A44 RID: 2628 RVA: 0x0008568C File Offset: 0x0008468C
	public static double ᜏ(int A_0)
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
		return (double)A_0 / 240.0;
	}

	// Token: 0x06000A45 RID: 2629 RVA: 0x000856D4 File Offset: 0x000846D4
	public static int ᜎ(int A_0)
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
		return spr\u23C4.\u171B(spr\u23C4.ᜑ(A_0));
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0008571C File Offset: 0x0008471C
	public static int ᜉ(double A_0)
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
		return spr\u2109.ᜂ(A_0 * 1440.0);
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x00085768 File Offset: 0x00084768
	public static int \u170D(int A_0)
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
		return spr\u2109.ᜂ((double)A_0 / 635.0);
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x000857B4 File Offset: 0x000847B4
	public static int ᜌ(int A_0)
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
		return spr\u2109.ᜂ((double)A_0 * 635.0);
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x00085800 File Offset: 0x00084800
	public static int ᜈ(double A_0)
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
		return spr\u2109.ᜂ(A_0 * 12700.0);
	}

	// Token: 0x06000A4A RID: 2634 RVA: 0x0008584C File Offset: 0x0008484C
	public static RectangleF ᜄ(RectangleF A_0)
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
		return RectangleF.FromLTRB((float)spr\u23C4.ᜈ((double)A_0.Left), (float)spr\u23C4.ᜈ((double)A_0.Top), (float)spr\u23C4.ᜈ((double)A_0.Right), (float)spr\u23C4.ᜈ((double)A_0.Bottom));
	}

	// Token: 0x06000A4B RID: 2635 RVA: 0x000858C4 File Offset: 0x000848C4
	public static double ᜋ(int A_0)
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
		return (double)A_0 / 12700.0;
	}

	// Token: 0x06000A4C RID: 2636 RVA: 0x0008590C File Offset: 0x0008490C
	public static double ᜇ(double A_0)
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
		return A_0 / 12700.0;
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x00085954 File Offset: 0x00084954
	public static RectangleF ᜃ(RectangleF A_0)
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
		return new RectangleF((float)spr\u23C4.ᜇ((double)A_0.Left), (float)spr\u23C4.ᜇ((double)A_0.Top), (float)spr\u23C4.ᜇ((double)A_0.Width), (float)spr\u23C4.ᜇ((double)A_0.Height));
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x000859CC File Offset: 0x000849CC
	public static double ᜊ(int A_0)
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
		return (double)spr\u23C4.ᜈ(spr\u23C4.\u1718((double)A_0));
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x00085A14 File Offset: 0x00084A14
	public static double ᜀ(int A_0, double A_1)
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
		return (double)spr\u23C4.ᜈ(spr\u23C4.ᜄ((double)A_0, A_1));
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x00085A60 File Offset: 0x00084A60
	public static RectangleF ᜀ(RectangleF A_0, double A_1)
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
		return RectangleF.FromLTRB((float)spr\u23C4.ᜀ((int)A_0.Left, A_1), (float)spr\u23C4.ᜀ((int)A_0.Top, A_1), (float)spr\u23C4.ᜀ((int)A_0.Right, A_1), (float)spr\u23C4.ᜀ((int)A_0.Bottom, A_1));
	}

	// Token: 0x06000A51 RID: 2641 RVA: 0x00085ADC File Offset: 0x00084ADC
	public static RectangleF ᜂ(RectangleF A_0)
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
		return new RectangleF((float)spr\u23C4.ᜊ((int)A_0.Left), (float)spr\u23C4.ᜊ((int)A_0.Top), (float)spr\u23C4.ᜊ((int)A_0.Width), (float)spr\u23C4.ᜊ((int)A_0.Height));
	}

	// Token: 0x06000A52 RID: 2642 RVA: 0x00085B54 File Offset: 0x00084B54
	public static double ᜉ(int A_0)
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
		return (double)A_0 / 914400.0;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x00085B9C File Offset: 0x00084B9C
	public static float ᜈ(int A_0)
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
		return (float)spr\u23C4.\u171C((double)A_0 / 12700.0);
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x00085BEC File Offset: 0x00084BEC
	public static RectangleF ᜁ(RectangleF A_0)
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
		return RectangleF.FromLTRB(spr\u23C4.ᜈ((int)A_0.Left), spr\u23C4.ᜈ((int)A_0.Top), spr\u23C4.ᜈ((int)A_0.Right), spr\u23C4.ᜈ((int)A_0.Bottom));
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x00085C60 File Offset: 0x00084C60
	public static double ᜇ(int A_0)
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
		return (double)A_0 / 36000.00000000001;
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x00085CA8 File Offset: 0x00084CA8
	public static int ᜀ(double A_0, double A_1)
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
		return spr\u2109.ᜂ(A_1 * A_0 / 72000.0);
	}

	// Token: 0x06000A57 RID: 2647 RVA: 0x00085CF8 File Offset: 0x00084CF8
	public static int ᜆ(int A_0)
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
		return spr\u23C4.ᜀ((double)A_0, 96.0);
	}

	// Token: 0x06000A58 RID: 2648 RVA: 0x00085D44 File Offset: 0x00084D44
	public static int ᜆ(double A_0)
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
		return spr\u2109.ᜂ(A_0 * 1000.0);
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x00085D90 File Offset: 0x00084D90
	public static float ᜅ(int A_0)
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
		return (float)A_0 / 1000f;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x00085DD4 File Offset: 0x00084DD4
	public static int ᜄ(int A_0)
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
		return A_0 * 50;
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x00085E14 File Offset: 0x00084E14
	public static int ᜃ(int A_0)
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
		return A_0 / 50;
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00085E54 File Offset: 0x00084E54
	public static int ᜅ(double A_0)
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
		return spr\u2109.ᜂ(A_0 * 500.0);
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00085EA0 File Offset: 0x00084EA0
	public static float ᜂ(int A_0)
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
		return (float)A_0 / 500f;
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x00085EE4 File Offset: 0x00084EE4
	public static RectangleF ᜀ(Rectangle A_0)
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
		return new RectangleF(spr\u23C4.ᜅ(A_0.X), spr\u23C4.ᜅ(A_0.Y), spr\u23C4.ᜅ(A_0.Width), spr\u23C4.ᜅ(A_0.Height));
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00085F54 File Offset: 0x00084F54
	public static PointF ᜀ(Point A_0)
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
		return new PointF(spr\u23C4.ᜅ(A_0.X), spr\u23C4.ᜅ(A_0.Y));
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x00085FAC File Offset: 0x00084FAC
	public static SizeF ᜀ(Size A_0)
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
		return new SizeF(spr\u23C4.ᜅ(A_0.Width), spr\u23C4.ᜅ(A_0.Height));
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x00086004 File Offset: 0x00085004
	public static Rectangle ᜀ(RectangleF A_0)
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
		return new Rectangle(spr\u23C4.ᜆ((double)A_0.X), spr\u23C4.ᜆ((double)A_0.Y), spr\u23C4.ᜆ((double)A_0.Width), spr\u23C4.ᜆ((double)A_0.Height));
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x00086078 File Offset: 0x00085078
	public static Point ᜀ(PointF A_0)
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
		return new Point(spr\u23C4.ᜆ((double)A_0.X), spr\u23C4.ᜆ((double)A_0.Y));
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x000860D4 File Offset: 0x000850D4
	public static float ᜁ(int A_0)
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
		return (float)A_0 / 1000f / 12f;
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x00086120 File Offset: 0x00085120
	public static Color ᜀ(Color A_0)
	{
		while (!A_0.IsEmpty)
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
				if (true)
				{
				}
				return Color.FromArgb((int)A_0.R, (int)A_0.G, (int)A_0.B);
			}
		}
		return A_0;
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x00086184 File Offset: 0x00085184
	public static double ᜀ(int A_0)
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
		return (double)A_0 / 65536.0;
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x000861CC File Offset: 0x000851CC
	public static int ᜄ(double A_0)
	{
		int num = 1;
		for (;;)
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
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_75;
				case 2:
					goto IL_B8;
				case 3:
					goto IL_A2;
				case 4:
					A_0 = -32768.99998474121;
					num = 0;
					continue;
				case 5:
					if (A_0 < -32768.99998474121)
					{
						num = 4;
						continue;
					}
					goto IL_BA;
				}
				if (A_0 > 32767.99998474121)
				{
					num = 3;
					continue;
				}
				num = 5;
				continue;
			}
			IL_A2:
			A_0 = 32767.99998474121;
			num = 2;
		}
		IL_75:
		IL_B8:
		IL_BA:
		return spr\u2109.ᜂ(A_0 * 65536.0);
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x000862A4 File Offset: 0x000852A4
	public static double ᜃ(double A_0)
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
		return spr\u2109.ᜄ(A_0 / 60000.0);
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x000862F0 File Offset: 0x000852F0
	public static double ᜂ(double A_0)
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
		return spr\u2109.ᜃ(A_0) * 60000.0;
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x0008633C File Offset: 0x0008533C
	public static double ᜁ(double A_0)
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
		return A_0 * 60000.0;
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00086384 File Offset: 0x00085384
	public static double ᜀ(double A_0)
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
		return A_0 / 60000.0;
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x000863CC File Offset: 0x000853CC
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u23C4()
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
		spr\u23C4.ᜠ = 1.3333334f;
		spr\u23C4.ᜡ = (float)(1.0 / (double)spr\u23C4.ᜠ);
	}

	// Token: 0x0400139E RID: 5022
	public const double ᜀ = 32767.99998474121;

	// Token: 0x0400139F RID: 5023
	public const double ᜁ = -32768.99998474121;

	// Token: 0x040013A0 RID: 5024
	public const double ᜂ = 25.4;

	// Token: 0x040013A1 RID: 5025
	public const double ᜃ = 72.0;

	// Token: 0x040013A2 RID: 5026
	public const float ᜄ = 0.71999997f;

	// Token: 0x040013A3 RID: 5027
	public const float ᜅ = 0.072000004f;

	// Token: 0x040013A4 RID: 5028
	private const double ᜆ = 2.834645669291339;

	// Token: 0x040013A5 RID: 5029
	public const float ᜇ = 0.28346458f;

	// Token: 0x040013A6 RID: 5030
	public const float ᜈ = 0.028346457f;

	// Token: 0x040013A7 RID: 5031
	public const float ᜉ = 0.0028346458f;

	// Token: 0x040013A8 RID: 5032
	private const double ᜊ = 28.34645669291339;

	// Token: 0x040013A9 RID: 5033
	private const double ᜋ = 12.0;

	// Token: 0x040013AA RID: 5034
	public const double ᜌ = 20.0;

	// Token: 0x040013AB RID: 5035
	public const double \u170D = 1440.0;

	// Token: 0x040013AC RID: 5036
	private const double ᜎ = 56.69291338582678;

	// Token: 0x040013AD RID: 5037
	private const double ᜏ = 240.0;

	// Token: 0x040013AE RID: 5038
	public const double ᜐ = 0.05;

	// Token: 0x040013AF RID: 5039
	public const double ᜑ = 12700.0;

	// Token: 0x040013B0 RID: 5040
	private const double \u1712 = 914400.0;

	// Token: 0x040013B1 RID: 5041
	private const double \u1713 = 36000.00000000001;

	// Token: 0x040013B2 RID: 5042
	private const double \u1714 = 635.0;

	// Token: 0x040013B3 RID: 5043
	public const int \u1715 = 1000;

	// Token: 0x040013B4 RID: 5044
	private const int \u1716 = 72000;

	// Token: 0x040013B5 RID: 5045
	public const int \u1717 = 50;

	// Token: 0x040013B6 RID: 5046
	private const int \u1718 = 500;

	// Token: 0x040013B7 RID: 5047
	public const double \u1719 = 1584.0;

	// Token: 0x040013B8 RID: 5048
	public const double \u171A = 20116800.0;

	// Token: 0x040013B9 RID: 5049
	public const double \u171B = 0.75;

	// Token: 0x040013BA RID: 5050
	public const int \u171C = 31680;

	// Token: 0x040013BB RID: 5051
	public const int \u171D = 15;

	// Token: 0x040013BC RID: 5052
	public const int \u171E = 1584000;

	// Token: 0x040013BD RID: 5053
	public const double \u171F = 60000.0;

	// Token: 0x040013BE RID: 5054
	public static float ᜠ;

	// Token: 0x040013BF RID: 5055
	public static float ᜡ;
}
