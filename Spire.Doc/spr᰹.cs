using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.CompoundFile.Doc;
using Spire.Layouting;

// Token: 0x0200028B RID: 651
internal class spr\u1C39
{
	// Token: 0x0600227F RID: 8831 RVA: 0x00237F08 File Offset: 0x00236F08
	private double[] ᜃ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_40:
			if (this.ᜂ != null)
			{
				goto IL_6C;
			}
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 0;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_24;
			case 1:
				goto IL_6A;
			case 2:
				this.ᜀ();
				num = 1;
				continue;
			}
			goto IL_40;
		}
		IL_24:
		if (true)
		{
		}
		goto IL_40;
		IL_6A:
		IL_6C:
		return this.ᜂ;
	}

	// Token: 0x06002280 RID: 8832 RVA: 0x00237F88 File Offset: 0x00236F88
	public static Graphics ᜂ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_40:
			if (spr\u1C39.ᜁ != null)
			{
				goto IL_76;
			}
			num = 1;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_74;
			case 1:
				spr\u1C39.ᜁ = Graphics.FromImage(new Bitmap(1, 1));
				num = 0;
				continue;
			case 2:
				goto IL_24;
			}
			goto IL_40;
		}
		IL_24:
		if (true)
		{
		}
		goto IL_40;
		IL_74:
		IL_76:
		return spr\u1C39.ᜁ;
	}

	// Token: 0x06002281 RID: 8833 RVA: 0x00238010 File Offset: 0x00237010
	public static spr\u1C39 ᜁ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_38:
			if (spr\u1C39.ᜃ != null)
			{
				goto IL_6F;
			}
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6D;
			case 2:
				spr\u1C39.ᜃ = new spr\u1C39();
				if (true)
				{
				}
				num = 0;
				continue;
			}
			break;
		}
		goto IL_38;
		IL_6D:
		IL_6F:
		return spr\u1C39.ᜃ;
	}

	// Token: 0x06002282 RID: 8834 RVA: 0x00238094 File Offset: 0x00237094
	public spr\u1C39() : this(spr\u1C39.ᜂ())
	{
	}

	// Token: 0x06002283 RID: 8835 RVA: 0x002380AC File Offset: 0x002370AC
	public spr\u1C39(Graphics A_0)
	{
		this.ᜀ(A_0);
	}

	// Token: 0x06002284 RID: 8836 RVA: 0x002380C8 File Offset: 0x002370C8
	public double ᜀ(double A_0, PrintUnits A_1, PrintUnits A_2)
	{
		while (A_1 == A_2)
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
				return A_0;
			}
		}
		return this.ᜁ(this.ᜀ(A_0, A_1), A_2);
	}

	// Token: 0x06002285 RID: 8837 RVA: 0x0023811C File Offset: 0x0023711C
	public float ᜀ(float A_0, PrintUnits A_1)
	{
		while (A_1 == PrintUnits.Pixel)
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
				return A_0;
			}
		}
		return (float)((double)A_0 * this.ᜂ[(int)A_1]);
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x0023816C File Offset: 0x0023716C
	public double ᜀ(double A_0, PrintUnits A_1)
	{
		while (A_1 == PrintUnits.Pixel)
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
				return A_0;
			}
		}
		return A_0 * this.ᜂ[(int)A_1];
	}

	// Token: 0x06002287 RID: 8839 RVA: 0x002381BC File Offset: 0x002371BC
	public RectangleF ᜀ(RectangleF A_0, PrintUnits A_1)
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
		float x = this.ᜀ(A_0.X, A_1);
		float y = this.ᜀ(A_0.Y, A_1);
		float width = this.ᜀ(A_0.Width, A_1);
		float height = this.ᜀ(A_0.Height, A_1);
		return new RectangleF(x, y, width, height);
	}

	// Token: 0x06002288 RID: 8840 RVA: 0x00238240 File Offset: 0x00237240
	public PointF ᜀ(PointF A_0, PrintUnits A_1)
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
		float x = this.ᜀ(A_0.X, A_1);
		float y = this.ᜀ(A_0.Y, A_1);
		return new PointF(x, y);
	}

	// Token: 0x06002289 RID: 8841 RVA: 0x002382A4 File Offset: 0x002372A4
	public SizeF ᜀ(SizeF A_0, PrintUnits A_1)
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
		float width = this.ᜀ(A_0.Width, A_1);
		float height = this.ᜀ(A_0.Height, A_1);
		return new SizeF(width, height);
	}

	// Token: 0x0600228A RID: 8842 RVA: 0x00238308 File Offset: 0x00237308
	public float ᜁ(float A_0, PrintUnits A_1)
	{
		while (A_1 == PrintUnits.Pixel)
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
				return A_0;
			}
		}
		return (float)((double)A_0 / this.ᜂ[(int)A_1]);
	}

	// Token: 0x0600228B RID: 8843 RVA: 0x00238358 File Offset: 0x00237358
	public double ᜁ(double A_0, PrintUnits A_1)
	{
		for (;;)
		{
			if (true)
			{
			}
			if (A_1 != PrintUnits.Pixel)
			{
				goto IL_36;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_2C;
			}
		}
		IL_2C:
		if (false)
		{
		}
		return A_0;
		IL_36:
		return A_0 / this.ᜂ[(int)A_1];
	}

	// Token: 0x0600228C RID: 8844 RVA: 0x002383A8 File Offset: 0x002373A8
	public RectangleF ᜁ(RectangleF A_0, PrintUnits A_1)
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
		float x = this.ᜁ(A_0.X, A_1);
		float y = this.ᜁ(A_0.Y, A_1);
		float width = this.ᜁ(A_0.Width, A_1);
		float height = this.ᜁ(A_0.Height, A_1);
		return new RectangleF(x, y, width, height);
	}

	// Token: 0x0600228D RID: 8845 RVA: 0x0023842C File Offset: 0x0023742C
	public PointF ᜁ(PointF A_0, PrintUnits A_1)
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
		float x = this.ᜁ(A_0.X, A_1);
		float y = this.ᜁ(A_0.Y, A_1);
		return new PointF(x, y);
	}

	// Token: 0x0600228E RID: 8846 RVA: 0x00238490 File Offset: 0x00237490
	public SizeF ᜀ(Size A_0, PrintUnits A_1)
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
		float width = this.ᜁ((float)A_0.Width, A_1);
		float height = this.ᜁ((float)A_0.Height, A_1);
		return new SizeF(width, height);
	}

	// Token: 0x0600228F RID: 8847 RVA: 0x002384F4 File Offset: 0x002374F4
	public SizeF ᜁ(SizeF A_0, PrintUnits A_1)
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
		float width = this.ᜁ(A_0.Width, A_1);
		float height = this.ᜁ(A_0.Height, A_1);
		return new SizeF(width, height);
	}

	// Token: 0x06002290 RID: 8848 RVA: 0x00238558 File Offset: 0x00237558
	public float ᜀ(float A_0, PrintUnits A_1, float A_2)
	{
		if (true)
		{
		}
		if (A_1 == PrintUnits.Pixel)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_36;
			}
			if (false)
			{
			}
			return A_0;
		}
		IL_36:
		double[] array = this.ᜀ(A_2);
		return (float)((double)A_0 * array[(int)A_1]);
	}

	// Token: 0x06002291 RID: 8849 RVA: 0x002385AC File Offset: 0x002375AC
	public double ᜀ(double A_0, PrintUnits A_1, float A_2)
	{
		if (A_1 == PrintUnits.Pixel)
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
				return A_0;
			}
		}
		double[] array = this.ᜀ(A_2);
		return A_0 * array[(int)A_1];
	}

	// Token: 0x06002292 RID: 8850 RVA: 0x002385FC File Offset: 0x002375FC
	public float ᜁ(float A_0, PrintUnits A_1, float A_2)
	{
		if (A_1 == PrintUnits.Pixel)
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
				return A_0;
			}
		}
		double[] array = this.ᜀ(A_2);
		return (float)((double)A_0 / array[(int)A_1]);
	}

	// Token: 0x06002293 RID: 8851 RVA: 0x00238650 File Offset: 0x00237650
	public double ᜁ(double A_0, PrintUnits A_1, float A_2)
	{
		if (A_1 == PrintUnits.Pixel)
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
				return A_0;
			}
		}
		if (true)
		{
		}
		double[] array = this.ᜀ(A_2);
		return A_0 / array[(int)A_1];
	}

	// Token: 0x06002294 RID: 8852 RVA: 0x002386A0 File Offset: 0x002376A0
	public SizeF ᜀ(SizeF A_0, PrintUnits A_1, float A_2)
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
		float width = this.ᜁ(A_0.Width, A_1, A_2);
		float height = this.ᜁ(A_0.Height, A_1, A_2);
		return new SizeF(width, height);
	}

	// Token: 0x06002295 RID: 8853 RVA: 0x00238704 File Offset: 0x00237704
	private double[] ᜀ(float A_0)
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
		return new double[]
		{
			(double)(A_0 / 75f),
			(double)(A_0 / 300f),
			(double)A_0,
			(double)(A_0 / 25.4f),
			(double)(A_0 / 2.54f),
			1.0,
			(double)(A_0 / 72f)
		};
	}

	// Token: 0x06002296 RID: 8854 RVA: 0x00238790 File Offset: 0x00237790
	private void ᜀ()
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
		double num = 96.0;
		this.ᜂ = new double[]
		{
			num / 75.0,
			num / 300.0,
			num,
			num / 25.399999618530273,
			num / 2.5399999618530273,
			1.0,
			num / 72.0
		};
	}

	// Token: 0x06002297 RID: 8855 RVA: 0x0023883C File Offset: 0x0023783C
	private void ᜀ(Graphics A_0)
	{
		int a_ = 14;
		if (true)
		{
		}
		if (A_0 == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			throw new ArgumentNullException(ClipboardData.b("፳", a_));
		}
		IL_50:
		Point[] array = new Point[]
		{
			new Point(1, 1)
		};
		GraphicsContainer container = A_0.BeginContainer(new RectangleF(0f, 0f, 1f, 1f), new RectangleF(0f, 0f, 1f, 1f), GraphicsUnit.Pixel);
		A_0.PageUnit = GraphicsUnit.Inch;
		A_0.TransformPoints(CoordinateSpace.Device, CoordinateSpace.Page, array);
		A_0.EndContainer(container);
		double num = (double)array[0].X;
		this.ᜂ = new double[]
		{
			num / 75.0,
			num / 300.0,
			num,
			num / 25.399999618530273,
			num / 2.5399999618530273,
			1.0,
			num / 72.0
		};
	}

	// Token: 0x04002111 RID: 8465
	internal const int ᜀ = 96;

	// Token: 0x04002112 RID: 8466
	[ThreadStatic]
	private static Graphics ᜁ;

	// Token: 0x04002113 RID: 8467
	private double[] ᜂ;

	// Token: 0x04002114 RID: 8468
	[ThreadStatic]
	private static spr\u1C39 ᜃ;
}
