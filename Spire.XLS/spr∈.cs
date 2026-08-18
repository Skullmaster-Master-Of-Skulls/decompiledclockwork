using System;
using System.Drawing;
using System.Reflection;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000319 RID: 793
[DefaultMember("Item")]
internal class spr\u2208 : XlsObject, IBorder
{
	// Token: 0x06003114 RID: 12564 RVA: 0x001C6108 File Offset: 0x001C5108
	internal spr\u2208(spr\u1DF5 A_0, object A_1, BordersLineType A_2) : base(A_0, A_1)
	{
		this.ᜀ = A_2;
		this.ᜀ();
	}

	// Token: 0x06003115 RID: 12565 RVA: 0x001C612C File Offset: 0x001C512C
	private void ᜀ()
	{
		int a_ = 14;
		for (;;)
		{
			this.ᜁ = (base.FindParent(typeof(spr\u2366)) as spr\u2366);
			if (this.ᜁ != null)
			{
				return;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_56;
			}
		}
		IL_56:
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㑃❅㩇⽉≋㩍", a_), RecordTableEnumerator.b("ᑃ❅㩇⽉≋㩍灏㵑㙓㱕㵗㥙⡛繝͟͡੣ࡥݧṩ䱫౭ᕯ剱ታ᥵൷ᑹ᡻偽", a_));
	}

	// Token: 0x06003116 RID: 12566 RVA: 0x001C61B8 File Offset: 0x001C51B8
	public IBorder ᜀ(int A_0)
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
		return this.ᜁ.ᜀ(A_0)[this.ᜀ];
	}

	// Token: 0x06003117 RID: 12567 RVA: 0x001C620C File Offset: 0x001C520C
	public int ᜆ()
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
		return this.ᜁ.ᜄ();
	}

	// Token: 0x06003118 RID: 12568 RVA: 0x001C6254 File Offset: 0x001C5254
	public OColor ᜅ()
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
		throw new NotImplementedException();
	}

	// Token: 0x06003119 RID: 12569 RVA: 0x001C6294 File Offset: 0x001C5294
	public ExcelColors ᜁ()
	{
		for (;;)
		{
			int num = this.ᜆ();
			int num2 = 7;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_94;
				case 1:
					goto IL_94;
				case 2:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_AB;
					}
					num2 = 5;
					continue;
				}
				case 3:
					return ExcelColors.Black;
				case 4:
					return ExcelColors.Black;
				case 5:
				{
					int num3;
					ExcelColors knownColor;
					if (knownColor != this.ᜀ(num3).KnownColor)
					{
						num2 = 4;
						continue;
					}
					num3++;
					num2 = 1;
					continue;
				}
				case 6:
				{
					ExcelColors knownColor;
					return knownColor;
				}
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AB;
					default:
					{
						if (false)
						{
						}
						if (num == 0)
						{
							num2 = 3;
							continue;
						}
						ExcelColors knownColor = this.ᜀ(0).KnownColor;
						int num3 = 1;
						num2 = 0;
						continue;
					}
					}
					break;
				}
				break;
				IL_94:
				if (true)
				{
				}
				num2 = 2;
				continue;
				IL_AB:
				num2 = 6;
			}
		}
		return ExcelColors.Black;
	}

	// Token: 0x0600311A RID: 12570 RVA: 0x001C6384 File Offset: 0x001C5384
	public void ᜀ(ExcelColors A_0)
	{
		for (;;)
		{
			int num;
			int num2;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_65:
				this.ᜀ(num).KnownColor = A_0;
				num++;
				num2 = 3;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 0;
				num3 = this.ᜆ();
				num2 = 2;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= num3)
					{
						num2 = 0;
						continue;
					}
					goto IL_65;
				case 2:
					goto IL_4F;
				case 3:
					goto IL_4F;
				}
				break;
				IL_4F:
				num2 = 1;
			}
		}
	}

	// Token: 0x0600311B RID: 12571 RVA: 0x001C6420 File Offset: 0x001C5420
	public Color ᜄ()
	{
		for (;;)
		{
			int num = this.ᜆ();
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					Color color;
					return color;
				}
				case 1:
					goto IL_9D;
				case 2:
					goto IL_A5;
				case 3:
					goto IL_66;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
					{
						if (false)
						{
						}
						if (true)
						{
						}
						if (num == 0)
						{
							num2 = 3;
							continue;
						}
						Color color = this.ᜀ(0).Color;
						int num3 = 1;
						num2 = 2;
						continue;
					}
					}
					break;
				case 5:
					goto IL_A5;
				case 6:
				{
					Color color;
					int num3;
					if (color != this.ᜀ(num3).Color)
					{
						num2 = 1;
						continue;
					}
					num3++;
					num2 = 5;
					continue;
				}
				case 7:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_B4;
					}
					num2 = 6;
					continue;
				}
				}
				break;
				IL_A5:
				num2 = 7;
				continue;
				IL_B4:
				num2 = 0;
			}
		}
		IL_66:
		return spr\u1D39.ᜂ;
		IL_9D:
		return spr\u1D39.ᜂ;
	}

	// Token: 0x0600311C RID: 12572 RVA: 0x001C651C File Offset: 0x001C551C
	public void ᜀ(Color A_0)
	{
		for (;;)
		{
			int num;
			int num2;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_65:
				this.ᜀ(num).Color = A_0;
				num++;
				num2 = 2;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				num3 = this.ᜆ();
				num2 = 0;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_47;
				case 1:
					if (true)
					{
					}
					if (num >= num3)
					{
						num2 = 3;
						continue;
					}
					goto IL_65;
				case 2:
					goto IL_47;
				case 3:
					return;
				}
				break;
				IL_47:
				num2 = 1;
			}
		}
	}

	// Token: 0x0600311D RID: 12573 RVA: 0x001C65B8 File Offset: 0x001C55B8
	public LineStyleType ᜂ()
	{
		for (;;)
		{
			int num = this.ᜆ();
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return LineStyleType.None;
				case 1:
					goto IL_9C;
				case 2:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_AB;
					}
					num2 = 6;
					continue;
				}
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AB;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						if (num == 0)
						{
							num2 = 4;
							continue;
						}
						LineStyleType lineStyle = this.ᜀ(0).LineStyle;
						int num3 = 1;
						num2 = 1;
						continue;
					}
					}
					break;
				case 4:
					return LineStyleType.None;
				case 5:
					goto IL_9C;
				case 6:
				{
					int num3;
					LineStyleType lineStyle;
					if (lineStyle != this.ᜀ(num3).LineStyle)
					{
						num2 = 0;
						continue;
					}
					num3++;
					num2 = 5;
					continue;
				}
				case 7:
				{
					LineStyleType lineStyle;
					return lineStyle;
				}
				}
				break;
				IL_9C:
				num2 = 2;
				continue;
				IL_AB:
				num2 = 7;
			}
		}
		return LineStyleType.None;
	}

	// Token: 0x0600311E RID: 12574 RVA: 0x001C66A8 File Offset: 0x001C56A8
	public void ᜀ(LineStyleType A_0)
	{
		for (;;)
		{
			int num;
			int num2;
			int num3;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5D:
				if (true)
				{
				}
				this.ᜀ(num).LineStyle = A_0;
				num++;
				num2 = 3;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				num3 = this.ᜆ();
				num2 = 2;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= num3)
					{
						num2 = 0;
						continue;
					}
					goto IL_5D;
				case 2:
					goto IL_47;
				case 3:
					goto IL_47;
				}
				break;
				IL_47:
				num2 = 1;
			}
		}
	}

	// Token: 0x0600311F RID: 12575 RVA: 0x001C6744 File Offset: 0x001C5744
	public bool ᜃ()
	{
		for (;;)
		{
			int num = this.ᜆ();
			int num2 = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
				{
					bool showDiagonalLine;
					return showDiagonalLine;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AB;
					default:
					{
						if (false)
						{
						}
						if (num == 0)
						{
							num2 = 7;
							continue;
						}
						bool showDiagonalLine = this.ᜀ(0).ShowDiagonalLine;
						int num3 = 1;
						num2 = 6;
						continue;
					}
					}
					break;
				case 2:
				{
					bool showDiagonalLine;
					int num3;
					if (showDiagonalLine != this.ᜀ(num3).ShowDiagonalLine)
					{
						num2 = 4;
						continue;
					}
					num3++;
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_9C;
				case 4:
					return false;
				case 5:
				{
					int num3;
					if (num3 >= num)
					{
						goto IL_AB;
					}
					num2 = 2;
					continue;
				}
				case 6:
					goto IL_9C;
				case 7:
					return false;
				}
				break;
				IL_9C:
				num2 = 5;
				continue;
				IL_AB:
				num2 = 0;
			}
		}
		return false;
	}

	// Token: 0x06003120 RID: 12576 RVA: 0x001C6834 File Offset: 0x001C5834
	public void ᜀ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = this.ᜆ();
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (num >= num2)
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
							num3 = 1;
							continue;
						}
					}
					else
					{
						this.ᜀ(num).ShowDiagonalLine = A_0;
						num++;
					}
					num3 = 3;
					continue;
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					goto IL_33;
				case 3:
					goto IL_33;
				}
				break;
				IL_33:
				num3 = 0;
			}
		}
	}

	// Token: 0x040015AF RID: 5551
	private BordersLineType ᜀ;

	// Token: 0x040015B0 RID: 5552
	private spr\u2366 ᜁ;
}
