using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020002F8 RID: 760
internal class sprỬ
{
	// Token: 0x060029A0 RID: 10656 RVA: 0x00295A30 File Offset: 0x00294A30
	internal sprỬ() : this(PathType.Unknown, 0)
	{
	}

	// Token: 0x060029A1 RID: 10657 RVA: 0x00295A48 File Offset: 0x00294A48
	internal sprỬ(PathType A_0, int A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
	}

	// Token: 0x060029A2 RID: 10658 RVA: 0x00295A6C File Offset: 0x00294A6C
	internal PathType ᜀ()
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

	// Token: 0x060029A3 RID: 10659 RVA: 0x00295AB0 File Offset: 0x00294AB0
	internal int ᜅ()
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

	// Token: 0x060029A4 RID: 10660 RVA: 0x00295AF4 File Offset: 0x00294AF4
	public virtual string ᜄ()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return string.Format(ClipboardData.b("㡧୩ᡫ٭⑯ୱѳ፵䉷婹ݻ乽ﵿ꺁ꒃ얅ﾉ揄ꪏ늑ꞕ", a_), this.ᜀ(), this.ᜅ());
	}

	// Token: 0x060029A5 RID: 10661 RVA: 0x00295B64 File Offset: 0x00294B64
	internal int ᜁ()
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
		return this.ᜃ() + this.ᜂ() * this.ᜅ();
	}

	// Token: 0x060029A6 RID: 10662 RVA: 0x00295BB4 File Offset: 0x00294BB4
	internal int ᜃ()
	{
		PathType pathType = this.ᜀ();
		if (pathType == PathType.MoveTo)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2D;
				}
			}
			IL_2D:
			if (true)
			{
			}
			if (false)
			{
			}
			return 1;
		}
		return 0;
	}

	// Token: 0x060029A7 RID: 10663 RVA: 0x00295C00 File Offset: 0x00294C00
	internal int ᜂ()
	{
		int a_ = 6;
		for (;;)
		{
			PathType pathType = this.ᜀ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (pathType)
					{
					case PathType.LineTo:
						return 1;
					case PathType.CurveTo:
						return 3;
					case PathType.MoveTo:
					case PathType.Close:
					case PathType.End:
						return 0;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return 0;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					switch (pathType)
					{
					case PathType.AngleEllipseTo:
					case PathType.AngleEllipse:
					case PathType.ArcTo:
					case PathType.Arc:
					case PathType.ClockwiseArcTo:
					case PathType.ClockwiseArc:
					case PathType.EllipticalQuadrantX:
					case PathType.EllipticalQuadrantY:
					case PathType.QuadraticBezier:
						return 1;
					case PathType.NoFill:
					case PathType.NoLine:
					case PathType.EscapeAutoLine:
					case PathType.EscapeAutoCurve:
					case PathType.EscapeCornerLine:
					case PathType.EscapeCornerCurve:
					case PathType.EscapeSmoothLine:
					case PathType.EscapeSmoothCurve:
					case PathType.EscapeSymmetricLine:
					case PathType.EscapeSymmetricCurve:
					case PathType.EscapeFreeForm:
						return 0;
					case PathType.FillColor:
					case PathType.LineColor:
						return 1;
					default:
						num = 3;
						continue;
					}
					break;
				case 3:
					num = 4;
					continue;
				case 4:
					goto IL_88;
				}
				break;
			}
		}
		return 1;
		IL_88:
		throw new InvalidOperationException(ClipboardData.b("㥫mɯ᝱ᝳ᥵ίᑹᕻѽꒃﺏ뒓ﾕ몙ﮛﮝ쾟쾡솣튥\udaa7펩貫\udead톯욱\udcb3颵", a_));
	}

	// Token: 0x04002413 RID: 9235
	private readonly PathType ᜀ;

	// Token: 0x04002414 RID: 9236
	private readonly int ᜁ;
}
