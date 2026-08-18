using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Layouting;

// Token: 0x02000318 RID: 792
internal class sprᳯ
{
	// Token: 0x06002B1F RID: 11039 RVA: 0x002A5598 File Offset: 0x002A4598
	public double ᜁ()
	{
		switch (0)
		{
		default:
		{
			double num;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D2;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int emHeight = this.ᜂ.FontFamily.GetEmHeight(this.ᜂ.Style);
					int cellAscent = this.ᜂ.FontFamily.GetCellAscent(this.ᜂ.Style);
					num = (double)(this.ᜂ.SizeInPoints * (float)cellAscent) / (double)emHeight;
					GraphicsUnit pageUnit = this.ᜁ.PageUnit;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 1;
							continue;
						case 1:
							goto IL_F0;
						case 2:
							switch (pageUnit)
							{
							case GraphicsUnit.Pixel:
								goto IL_D5;
							case GraphicsUnit.Point:
								goto IL_D2;
							default:
								num2 = 0;
								continue;
							}
							break;
						}
						break;
					}
					break;
				}
				}
			}
			IL_D2:
			return num;
			IL_D5:
			return this.ᜃ.ᜀ(num, PrintUnits.Point);
			IL_F0:
			throw new NotImplementedException();
		}
		}
	}

	// Token: 0x06002B20 RID: 11040 RVA: 0x002A569C File Offset: 0x002A469C
	public double ᜀ()
	{
		switch (0)
		{
		default:
		{
			double num;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D2;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int emHeight = this.ᜂ.FontFamily.GetEmHeight(this.ᜂ.Style);
					int cellDescent = this.ᜂ.FontFamily.GetCellDescent(this.ᜂ.Style);
					num = (double)(this.ᜂ.SizeInPoints * (float)cellDescent) / (double)emHeight;
					GraphicsUnit pageUnit = this.ᜁ.PageUnit;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_F0;
						case 1:
							switch (pageUnit)
							{
							case GraphicsUnit.Pixel:
								goto IL_D5;
							case GraphicsUnit.Point:
								goto IL_D2;
							default:
								num2 = 2;
								continue;
							}
							break;
						case 2:
							num2 = 0;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
			IL_D2:
			return num;
			IL_D5:
			return this.ᜃ.ᜀ(num, PrintUnits.Point);
			IL_F0:
			throw new NotImplementedException();
		}
		}
	}

	// Token: 0x06002B21 RID: 11041 RVA: 0x002A57A0 File Offset: 0x002A47A0
	protected Graphics ᜂ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_76;
			case 2:
			{
				Bitmap image = new Bitmap(1, 1);
				sprᳯ.ᜀ = Graphics.FromImage(image);
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
					num = 0;
					continue;
				}
				break;
			}
			}
			if (sprᳯ.ᜀ != null)
			{
				break;
			}
			num = 2;
		}
		IL_76:
		return sprᳯ.ᜀ;
	}

	// Token: 0x06002B22 RID: 11042 RVA: 0x002A582C File Offset: 0x002A482C
	public sprᳯ()
	{
	}

	// Token: 0x06002B23 RID: 11043 RVA: 0x002A5840 File Offset: 0x002A4840
	public sprᳯ(Font A_0)
	{
		this.ᜀ(null, A_0);
	}

	// Token: 0x06002B24 RID: 11044 RVA: 0x002A585C File Offset: 0x002A485C
	public sprᳯ(Font A_0, Graphics A_1)
	{
		this.ᜀ(A_1, A_0);
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x002A5878 File Offset: 0x002A4878
	public void ᜀ(Graphics A_0, Font A_1)
	{
		int a_ = 18;
		if (A_1 != null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_0E;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ = A_1;
			this.ᜀ(A_0);
			return;
		}
		IL_0E:
		throw new ArgumentNullException(ClipboardData.b("ṷᕹቻ੽", a_));
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x002A58E4 File Offset: 0x002A48E4
	public void ᜀ(Graphics A_0)
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
		this.ᜁ = ((A_0 == null) ? this.ᜂ() : A_0);
		this.ᜃ = new spr\u1C39(this.ᜁ);
	}

	// Token: 0x0400252A RID: 9514
	[ThreadStatic]
	private static Graphics ᜀ;

	// Token: 0x0400252B RID: 9515
	private Graphics ᜁ;

	// Token: 0x0400252C RID: 9516
	private Font ᜂ;

	// Token: 0x0400252D RID: 9517
	private spr\u1C39 ᜃ;
}
