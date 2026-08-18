using System;
using System.Drawing;
using Spire.Xls.Core;

// Token: 0x020004F3 RID: 1267
internal class spr\u19EA
{
	// Token: 0x06004D72 RID: 19826 RVA: 0x002F4C18 File Offset: 0x002F3C18
	public bool? ᜀ(ICombinedRange A_0)
	{
		bool? result;
		for (;;)
		{
			Rectangle[] rectangles = A_0.GetRectangles();
			result = null;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					result = new bool?(true);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 1:
					goto IL_61;
				case 2:
					if (this.ᜀ.ᜁ(rectangles))
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					num = 4;
					continue;
				case 3:
					return result;
				case 4:
					if (this.ᜁ.ᜁ(rectangles))
					{
						num = 1;
						continue;
					}
					return result;
				case 5:
					return result;
				}
				break;
				IL_61:
				result = new bool?(false);
				num = 3;
			}
		}
		return result;
	}

	// Token: 0x06004D73 RID: 19827 RVA: 0x002F4CF0 File Offset: 0x002F3CF0
	public void ᜀ(ICombinedRange A_0, bool? A_1)
	{
		Rectangle[] rectangles;
		for (;;)
		{
			rectangles = A_0.GetRectangles();
			int num = 1;
			for (;;)
			{
				bool? flag;
				bool flag2;
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_37;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						if (false)
						{
						}
						if (flag.GetValueOrDefault())
						{
							num = 3;
							continue;
						}
						num = 5;
						continue;
					}
					break;
				case 3:
					if (true)
					{
					}
					num = 7;
					continue;
				case 4:
					goto IL_AD;
				case 5:
					flag2 = false;
					goto IL_A0;
				case 6:
					this.ᜀ.ᜀ(rectangles);
					this.ᜁ.ᜀ(rectangles);
					num = 0;
					continue;
				case 7:
					flag2 = (flag != null);
					goto IL_A0;
				}
				break;
				IL_37:
				if (A_1 == null)
				{
					num = 6;
					continue;
				}
				flag = A_1;
				num = 2;
				continue;
				IL_A0:
				if (!flag2)
				{
					goto IL_109;
				}
				num = 4;
			}
		}
		return;
		IL_AD:
		this.ᜀ.ᜄ(rectangles);
		this.ᜁ.ᜀ(rectangles);
		return;
		IL_109:
		this.ᜀ.ᜀ(rectangles);
		this.ᜁ.ᜄ(rectangles);
	}

	// Token: 0x06004D74 RID: 19828 RVA: 0x002F4E20 File Offset: 0x002F3E20
	public void ᜀ()
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
		this.ᜀ.ᜃ();
		this.ᜁ.ᜃ();
	}

	// Token: 0x04002328 RID: 9000
	private spr\u2530 ᜀ = new spr\u2530();

	// Token: 0x04002329 RID: 9001
	private spr\u2530 ᜁ = new spr\u2530();
}
