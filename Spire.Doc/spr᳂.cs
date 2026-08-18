using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// Token: 0x02000278 RID: 632
internal class spr\u1CC2 : sprᢿ
{
	// Token: 0x060021CD RID: 8653 RVA: 0x002327E0 File Offset: 0x002317E0
	public override void ᜀ(spr\u1B70 A_0)
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
		this.ᜀ = new GraphicsPath(A_0.ᜃ());
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x0023282C File Offset: 0x0023182C
	public override void ᜀ(spr\u1926 A_0)
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
		this.ᜀ.StartFigure();
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x00232874 File Offset: 0x00231874
	public override void ᜁ(spr\u1926 A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				IL_08:
				break;
			case 1:
				goto IL_43;
			case 2:
				this.ᜀ.CloseFigure();
				num = 1;
				continue;
			}
			if (A_0.ᜁ())
			{
				num = 2;
				continue;
			}
			IL_43:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_08;
			default:
				goto IL_59;
			}
		}
		IL_59:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x002328F4 File Offset: 0x002318F4
	public override void ᜂ(spr\u1B70 A_0)
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
	}

	// Token: 0x060021D1 RID: 8657 RVA: 0x00232930 File Offset: 0x00231930
	public override void ᜀ(sprᴎ A_0)
	{
		switch (0)
		{
		default:
		{
			PointF[] array;
			for (;;)
			{
				array = (A_0.ᜀ().ToArray(typeof(PointF)) as PointF[]);
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_87;
					case 1:
						goto IL_110;
					case 2:
					{
						PointF left;
						array[array.Length - 1] = new PointF(left.X + 0.5f, left.Y);
						num = 1;
						continue;
					}
					case 3:
						goto IL_138;
					case 4:
						num = 9;
						continue;
					case 5:
					{
						PointF left;
						int num2;
						if (left != array[num2])
						{
							num = 13;
							continue;
						}
						num2++;
						num = 0;
						continue;
					}
					case 6:
					{
						bool flag = true;
						PointF left = array[0];
						int num2 = 1;
						num = 8;
						continue;
					}
					case 7:
						if (array != null)
						{
							num = 4;
							continue;
						}
						goto IL_1CA;
					case 8:
						goto IL_87;
					case 9:
						if (array.Length > 1)
						{
							num = 6;
							continue;
						}
						goto IL_1CA;
					case 10:
						goto IL_138;
					case 11:
					{
						int num2;
						if (num2 >= array.Length)
						{
							num = 10;
							continue;
						}
						num = 5;
						continue;
					}
					case 12:
					{
						bool flag;
						if (!flag)
						{
							goto IL_1CA;
						}
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1CA;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					case 13:
					{
						bool flag = false;
						num = 3;
						continue;
					}
					}
					break;
					IL_87:
					num = 11;
					continue;
					IL_138:
					num = 12;
				}
			}
			IL_110:
			IL_1CA:
			this.ᜀ.AddLines(array);
			return;
		}
		}
	}

	// Token: 0x060021D2 RID: 8658 RVA: 0x00232B14 File Offset: 0x00231B14
	public override void ᜀ(spr\u17F0 A_0)
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
		this.ᜀ.AddBezier(A_0.ᜀ().ᜂ(), A_0.ᜀ().ᜄ(), A_0.ᜀ().ᜃ(), A_0.ᜀ().ᜀ());
	}

	// Token: 0x060021D3 RID: 8659 RVA: 0x00232B94 File Offset: 0x00231B94
	public GraphicsPath ᜀ()
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

	// Token: 0x040020BB RID: 8379
	private new GraphicsPath ᜀ;
}
