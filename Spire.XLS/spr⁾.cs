using System;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000356 RID: 854
internal class spr\u207E : XlsObject, IPageSetup
{
	// Token: 0x060033D7 RID: 13271 RVA: 0x001DDA64 File Offset: 0x001DCA64
	internal spr\u207E(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		this.ᜀ();
	}

	// Token: 0x060033D8 RID: 13272 RVA: 0x001DDA80 File Offset: 0x001DCA80
	private void ᜀ()
	{
		int a_ = 3;
		for (;;)
		{
			this.ᜀ = (base.FindParent(typeof(spr\u233D)) as spr\u233D);
			if (this.ᜀ != null)
			{
				return;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_42;
			}
		}
		IL_42:
		if (true)
		{
		}
		if (false)
		{
		}
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䤸娺似娾⽀㝂", a_), RecordTableEnumerator.b("椸娺似娾⽀㝂敄⡆⭈⅊⡌ⱎ═獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦ཨѪᡬŮᕰ嵲", a_));
	}

	// Token: 0x060033D9 RID: 13273 RVA: 0x001DDB0C File Offset: 0x001DCB0C
	public bool ᜠ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool autoFirstPageNumber = this.ᜀ[0].PageSetup.AutoFirstPageNumber;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
						{
							if (false)
							{
							}
							bool autoFirstPageNumber2;
							if (autoFirstPageNumber2 != autoFirstPageNumber)
							{
								num2 = 1;
								continue;
							}
							if (true)
							{
							}
							num++;
							num2 = 2;
							continue;
						}
						}
						break;
					case 1:
						goto IL_C7;
					case 2:
						goto IL_C9;
					case 3:
						goto IL_C9;
					case 4:
						return autoFirstPageNumber;
					case 5:
					{
						if (num >= count)
						{
							num2 = 4;
							continue;
						}
						bool autoFirstPageNumber2 = this.ᜀ[num].PageSetup.AutoFirstPageNumber;
						num2 = 0;
						continue;
					}
					}
					break;
					IL_C9:
					num2 = 5;
				}
			}
			return false;
			IL_C7:
			return false;
		}
	}

	// Token: 0x060033DA RID: 13274 RVA: 0x001DDC0C File Offset: 0x001DCC0C
	public void ᜄ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ[num].PageSetup.AutoFirstPageNumber = A_0;
					num++;
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
						num2 = 3;
						continue;
					}
					break;
				case 1:
					goto IL_30;
				case 2:
					return;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 0;
			}
		}
	}

	// Token: 0x060033DB RID: 13275 RVA: 0x001DDCB8 File Offset: 0x001DCCB8
	public int \u170D()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int fitToPagesTall = this.ᜀ[0].PageSetup.FitToPagesTall;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_C5;
					case 1:
						goto IL_C5;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return int.MinValue;
						default:
						{
							if (false)
							{
							}
							int fitToPagesTall2;
							if (fitToPagesTall2 != fitToPagesTall)
							{
								num2 = 5;
								continue;
							}
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
					case 3:
						return fitToPagesTall;
					case 4:
					{
						if (true)
						{
						}
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						int fitToPagesTall2 = this.ᜀ[num].PageSetup.FitToPagesTall;
						num2 = 2;
						continue;
					}
					case 5:
						goto IL_C3;
					}
					break;
					IL_C5:
					num2 = 4;
				}
			}
			return int.MinValue;
			IL_C3:
			return int.MinValue;
		}
	}

	// Token: 0x060033DC RID: 13276 RVA: 0x001DDDBC File Offset: 0x001DCDBC
	public void ᜁ(int A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ[num].PageSetup.FitToPagesTall = A_0;
					num++;
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_30;
				case 2:
					return;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 0;
			}
		}
	}

	// Token: 0x060033DD RID: 13277 RVA: 0x001DDE68 File Offset: 0x001DCE68
	public int \u1713()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int fitToPagesWide = this.ᜀ[0].PageSetup.FitToPagesWide;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return fitToPagesWide;
					case 1:
						goto IL_CB;
					case 2:
						goto IL_CD;
					case 3:
						goto IL_CD;
					case 4:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						int fitToPagesWide2 = this.ᜀ[num].PageSetup.FitToPagesWide;
						num2 = 5;
						continue;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return int.MinValue;
						default:
						{
							if (false)
							{
							}
							int fitToPagesWide2;
							if (fitToPagesWide2 != fitToPagesWide)
							{
								num2 = 1;
								continue;
							}
							if (true)
							{
							}
							num++;
							num2 = 2;
							continue;
						}
						}
						break;
					}
					break;
					IL_CD:
					num2 = 4;
				}
			}
			return int.MinValue;
			IL_CB:
			return int.MinValue;
		}
	}

	// Token: 0x060033DE RID: 13278 RVA: 0x001DDF6C File Offset: 0x001DCF6C
	public void ᜀ(int A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ[num].PageSetup.FitToPagesWide = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 1:
					if (true)
					{
					}
					goto IL_38;
				case 2:
					return;
				case 3:
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 0;
			}
		}
	}

	// Token: 0x060033DF RID: 13279 RVA: 0x001DE018 File Offset: 0x001DD018
	public bool ᜥ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool isPrintGridlines = this.ᜀ[0].PageSetup.IsPrintGridlines;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_C4;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							bool isPrintGridlines2;
							if (isPrintGridlines2 != isPrintGridlines)
							{
								num2 = 0;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						}
						}
						break;
					case 2:
						goto IL_C6;
					case 3:
						return isPrintGridlines;
					case 4:
						goto IL_C6;
					case 5:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						bool isPrintGridlines2 = this.ᜀ[num].PageSetup.IsPrintGridlines;
						num2 = 1;
						continue;
					}
					}
					break;
					IL_C6:
					num2 = 5;
				}
			}
			return false;
			IL_C4:
			return false;
		}
	}

	// Token: 0x060033E0 RID: 13280 RVA: 0x001DE114 File Offset: 0x001DD114
	public void ᜅ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_38;
				case 2:
					goto IL_38;
				case 3:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ[num].PageSetup.IsPrintGridlines = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					break;
				}
				break;
				IL_38:
				num2 = 3;
			}
		}
	}

	// Token: 0x060033E1 RID: 13281 RVA: 0x001DE1C0 File Offset: 0x001DD1C0
	public bool \u171E()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool isPrintHeadings = this.ᜀ[0].PageSetup.IsPrintHeadings;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_BC;
					case 1:
						goto IL_BE;
					case 2:
						goto IL_BE;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
						{
							if (false)
							{
							}
							bool isPrintHeadings2;
							if (isPrintHeadings2 != isPrintHeadings)
							{
								num2 = 0;
								continue;
							}
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
					case 4:
						return isPrintHeadings;
					case 5:
					{
						if (num >= count)
						{
							num2 = 4;
							continue;
						}
						bool isPrintHeadings2 = this.ᜀ[num].PageSetup.IsPrintHeadings;
						num2 = 3;
						continue;
					}
					}
					break;
					IL_BE:
					if (true)
					{
					}
					num2 = 5;
				}
			}
			return false;
			IL_BC:
			return false;
		}
	}

	// Token: 0x060033E2 RID: 13282 RVA: 0x001DE2BC File Offset: 0x001DD2BC
	public void ᜀ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ[num].PageSetup.IsPrintHeadings = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 1;
						continue;
					}
					break;
				case 1:
					goto IL_38;
				case 2:
					return;
				case 3:
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 0;
			}
		}
	}

	// Token: 0x060033E3 RID: 13283 RVA: 0x001DE368 File Offset: 0x001DD368
	public string ᜡ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string printArea = this.ᜀ[0].PageSetup.PrintArea;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_CE;
					case 1:
						goto IL_CC;
					case 2:
						return printArea;
					case 3:
						goto IL_CE;
					case 4:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						string printArea2 = this.ᜀ[num].PageSetup.PrintArea;
						num2 = 5;
						continue;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_76;
						default:
						{
							if (false)
							{
							}
							string printArea2;
							if (printArea2 != printArea)
							{
								num2 = 1;
								continue;
							}
							num++;
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						}
						break;
					}
					break;
					IL_CE:
					num2 = 4;
				}
			}
			IL_76:
			return null;
			IL_CC:
			goto IL_76;
		}
	}

	// Token: 0x060033E4 RID: 13284 RVA: 0x001DE46C File Offset: 0x001DD46C
	public void ᜅ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			if (true)
			{
			}
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_38;
				case 1:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ[num].PageSetup.PrintArea = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 2:
					return;
				case 3:
					goto IL_38;
				}
				break;
				IL_38:
				num2 = 1;
			}
		}
	}

	// Token: 0x060033E5 RID: 13285 RVA: 0x001DE518 File Offset: 0x001DD518
	public string ᜪ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string printTitleColumns = this.ᜀ[0].PageSetup.PrintTitleColumns;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_CE;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_76;
						default:
						{
							if (false)
							{
							}
							string printTitleColumns2;
							if (printTitleColumns2 != printTitleColumns)
							{
								num2 = 5;
								continue;
							}
							if (true)
							{
							}
							num++;
							num2 = 0;
							continue;
						}
						}
						break;
					case 2:
						return printTitleColumns;
					case 3:
						goto IL_CE;
					case 4:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						string printTitleColumns2 = this.ᜀ[num].PageSetup.PrintTitleColumns;
						num2 = 1;
						continue;
					}
					case 5:
						goto IL_CC;
					}
					break;
					IL_CE:
					num2 = 4;
				}
			}
			IL_76:
			return null;
			IL_CC:
			goto IL_76;
		}
	}

	// Token: 0x060033E6 RID: 13286 RVA: 0x001DE61C File Offset: 0x001DD61C
	public void ᜃ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_30;
				case 1:
					return;
				case 2:
					if (true)
					{
					}
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					this.ᜀ[num].PageSetup.PrintTitleColumns = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 3:
					goto IL_30;
				}
				break;
				IL_30:
				num2 = 2;
			}
		}
	}

	// Token: 0x060033E7 RID: 13287 RVA: 0x001DE6C8 File Offset: 0x001DD6C8
	public string \u1718()
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				string printTitleRows = this.ᜀ[0].PageSetup.PrintTitleRows;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return printTitleRows;
					case 1:
						goto IL_CB;
					case 2:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						string printTitleRows2 = this.ᜀ[num].PageSetup.PrintTitleRows;
						num2 = 3;
						continue;
					}
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_76;
						default:
						{
							if (false)
							{
							}
							string printTitleRows2;
							if (printTitleRows2 != printTitleRows)
							{
								num2 = 5;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						}
						}
						break;
					case 4:
						goto IL_CB;
					case 5:
						goto IL_C9;
					}
					break;
					IL_CB:
					num2 = 2;
				}
			}
			IL_76:
			return null;
			IL_C9:
			goto IL_76;
		}
	}

	// Token: 0x060033E8 RID: 13288 RVA: 0x001DE7CC File Offset: 0x001DD7CC
	public void ᜄ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ[num].PageSetup.PrintTitleRows = A_0;
					num++;
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
						num2 = 2;
						continue;
					}
					break;
				case 1:
					goto IL_30;
				case 2:
					goto IL_30;
				case 3:
					return;
				}
				break;
				IL_30:
				num2 = 0;
			}
		}
	}

	// Token: 0x060033E9 RID: 13289 RVA: 0x001DE878 File Offset: 0x001DD878
	public bool ᜧ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool isSummaryRowBelow = this.ᜀ[0].PageSetup.IsSummaryRowBelow;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_C7;
					case 1:
						goto IL_C9;
					case 2:
						return isSummaryRowBelow;
					case 3:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						bool isSummaryRowBelow2 = this.ᜀ[num].PageSetup.IsSummaryRowBelow;
						num2 = 5;
						continue;
					}
					case 4:
						goto IL_C9;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
						{
							if (false)
							{
							}
							bool isSummaryRowBelow2;
							if (isSummaryRowBelow2 != isSummaryRowBelow)
							{
								num2 = 0;
								continue;
							}
							num++;
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						}
						break;
					}
					break;
					IL_C9:
					num2 = 3;
				}
			}
			return false;
			IL_C7:
			return false;
		}
	}

	// Token: 0x060033EA RID: 13290 RVA: 0x001DE978 File Offset: 0x001DD978
	public void ᜉ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_30;
				case 1:
					goto IL_30;
				case 2:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ[num].PageSetup.IsSummaryRowBelow = A_0;
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 3:
					goto IL_44;
				}
				break;
				IL_30:
				num2 = 2;
			}
		}
		IL_44:
		if (true)
		{
		}
	}

	// Token: 0x060033EB RID: 13291 RVA: 0x001DEA24 File Offset: 0x001DDA24
	public bool \u171D()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				bool isSummaryColumnRight = this.ᜀ[0].PageSetup.IsSummaryColumnRight;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_C7;
					case 1:
						goto IL_C9;
					case 2:
						return isSummaryColumnRight;
					case 3:
						goto IL_C9;
					case 4:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						bool isSummaryColumnRight2 = this.ᜀ[num].PageSetup.IsSummaryColumnRight;
						num2 = 5;
						continue;
					}
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
						{
							if (false)
							{
							}
							bool isSummaryColumnRight2;
							if (isSummaryColumnRight2 != isSummaryColumnRight)
							{
								num2 = 0;
								continue;
							}
							num++;
							num2 = 1;
							continue;
						}
						}
						break;
					}
					break;
					IL_C9:
					num2 = 4;
				}
			}
			return false;
			IL_C7:
			return false;
		}
	}

	// Token: 0x060033EC RID: 13292 RVA: 0x001DEB24 File Offset: 0x001DDB24
	public void ᜈ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (false)
						{
						}
						goto IL_30;
					}
					break;
				case 1:
					return;
				case 2:
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					if (true)
					{
					}
					this.ᜀ[num].PageSetup.IsSummaryColumnRight = A_0;
					num++;
					num2 = 0;
					continue;
				case 3:
					goto IL_2E;
				}
				break;
				IL_30:
				num2 = 2;
				continue;
				IL_2E:
				goto IL_30;
			}
		}
	}

	// Token: 0x060033ED RID: 13293 RVA: 0x001DEBCC File Offset: 0x001DDBCC
	public bool \u171A()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_37:
				bool isFitToPage = this.ᜀ[0].PageSetup.IsFitToPage;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_AD;
					case 1:
						if (true)
						{
						}
						num2 = 6;
						continue;
					case 2:
					{
						if (num >= count)
						{
							num2 = 7;
							continue;
						}
						bool isFitToPage2 = this.ᜀ[num].PageSetup.IsFitToPage;
						num2 = 4;
						continue;
					}
					case 3:
						goto IL_AD;
					case 4:
					{
						bool isFitToPage2;
						if (isFitToPage2 == isFitToPage)
						{
							num2 = 1;
							continue;
						}
						return false;
					}
					case 5:
						goto IL_117;
					case 6:
					{
						bool isFitToPage2;
						if (!isFitToPage2)
						{
							num2 = 5;
							continue;
						}
						num++;
						num2 = 0;
						continue;
					}
					case 7:
						return isFitToPage;
					}
					break;
					IL_AD:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						if (false)
						{
						}
						num2 = 2;
						break;
					}
				}
			}
			return false;
			IL_117:
			return false;
		}
	}

	// Token: 0x060033EE RID: 13294 RVA: 0x001DECF8 File Offset: 0x001DDCF8
	public void ᜆ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_36;
				case 1:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ[num].PageSetup.IsFitToPage = A_0;
					num++;
					num2 = 2;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						goto IL_38;
					}
					break;
				case 3:
					return;
				}
				break;
				IL_38:
				num2 = 1;
				continue;
				IL_36:
				goto IL_38;
			}
		}
	}

	// Token: 0x060033EF RID: 13295 RVA: 0x001DEDA0 File Offset: 0x001DDDA0
	public bool ᜋ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool blackAndWhite = this.ᜀ[0].PageSetup.BlackAndWhite;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_D3;
					case 1:
					{
						bool blackAndWhite2;
						if (blackAndWhite2 != blackAndWhite)
						{
							num2 = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 2;
						continue;
					}
					case 2:
						goto IL_D3;
					case 3:
						return blackAndWhite;
					case 4:
					{
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						bool blackAndWhite2 = this.ᜀ[num].PageSetup.BlackAndWhite;
						num2 = 1;
						continue;
					}
					case 5:
						goto IL_C9;
					}
					break;
					IL_D3:
					num2 = 4;
				}
			}
			IL_C9:
			if (true)
			{
			}
			return false;
		}
	}

	// Token: 0x060033F0 RID: 13296 RVA: 0x001DEEA0 File Offset: 0x001DDEA0
	public void ᜁ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_44;
				case 1:
					goto IL_2E;
				case 2:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ[num].PageSetup.BlackAndWhite = A_0;
					num++;
					num2 = 3;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (false)
						{
						}
						goto IL_30;
					}
					break;
				}
				break;
				IL_30:
				num2 = 2;
				continue;
				IL_2E:
				goto IL_30;
			}
		}
		IL_44:
		if (true)
		{
		}
	}

	// Token: 0x060033F1 RID: 13297 RVA: 0x001DEF48 File Offset: 0x001DDF48
	public double ᜏ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				double bottomMargin = this.ᜀ[0].PageSetup.BottomMargin;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						double bottomMargin2;
						if (bottomMargin2 != bottomMargin)
						{
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 5;
						continue;
					}
					case 1:
						goto IL_D1;
					case 2:
					{
						if (true)
						{
						}
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						double bottomMargin2 = this.ᜀ[num].PageSetup.BottomMargin;
						num2 = 0;
						continue;
					}
					case 3:
						return bottomMargin;
					case 4:
						goto IL_D3;
					case 5:
						goto IL_D3;
					}
					break;
					IL_D3:
					num2 = 2;
				}
			}
			IL_D1:
			return double.MinValue;
		}
	}

	// Token: 0x060033F2 RID: 13298 RVA: 0x001DF050 File Offset: 0x001DE050
	public void ᜅ(double A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ[num].PageSetup.BottomMargin = A_0;
					num++;
					num2 = 2;
					continue;
				case 1:
					goto IL_36;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						goto IL_38;
					}
					break;
				case 3:
					return;
				}
				break;
				IL_38:
				num2 = 0;
				continue;
				IL_36:
				goto IL_38;
			}
		}
	}

	// Token: 0x060033F3 RID: 13299 RVA: 0x001DF0F8 File Offset: 0x001DE0F8
	public string \u1714()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string centerFooter = this.ᜀ[0].PageSetup.CenterFooter;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						string centerFooter2 = this.ᜀ[num].PageSetup.CenterFooter;
						num2 = 1;
						continue;
					}
					case 1:
					{
						string centerFooter2;
						if (centerFooter2 != centerFooter)
						{
							if (true)
							{
							}
							num2 = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 4;
						continue;
					}
					case 2:
						return centerFooter;
					case 3:
						goto IL_D8;
					case 4:
						goto IL_D8;
					case 5:
						goto IL_D6;
					}
					break;
					IL_D8:
					num2 = 0;
				}
			}
			IL_D6:
			return null;
		}
	}

	// Token: 0x060033F4 RID: 13300 RVA: 0x001DF1FC File Offset: 0x001DE1FC
	public void ᜆ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_2E;
				case 1:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					this.ᜀ[num].PageSetup.CenterFooter = A_0;
					num++;
					num2 = 3;
					continue;
				case 2:
					return;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_30;
					}
					break;
				}
				break;
				IL_30:
				num2 = 1;
				continue;
				IL_2E:
				goto IL_30;
			}
		}
	}

	// Token: 0x060033F5 RID: 13301 RVA: 0x001DF2A4 File Offset: 0x001DE2A4
	public Image ᜂ()
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
		return null;
	}

	// Token: 0x060033F6 RID: 13302 RVA: 0x001DF2E0 File Offset: 0x001DE2E0
	public void ᜁ(Image A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_36;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						goto IL_38;
					}
					break;
				case 2:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ[num].PageSetup.CenterFooterImage = A_0;
					num++;
					num2 = 1;
					continue;
				case 3:
					return;
				}
				break;
				IL_38:
				num2 = 2;
				continue;
				IL_36:
				goto IL_38;
			}
		}
	}

	// Token: 0x060033F7 RID: 13303 RVA: 0x001DF388 File Offset: 0x001DE388
	public string ᜣ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string centerHeader = this.ᜀ[0].PageSetup.CenterHeader;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return centerHeader;
					case 1:
						goto IL_D8;
					case 2:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						string centerHeader2 = this.ᜀ[num].PageSetup.CenterHeader;
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					case 3:
					{
						string centerHeader2;
						if (centerHeader2 != centerHeader)
						{
							num2 = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 1;
						continue;
					}
					case 4:
						goto IL_D6;
					case 5:
						goto IL_D8;
					}
					break;
					IL_D8:
					num2 = 2;
				}
			}
			IL_D6:
			return null;
		}
	}

	// Token: 0x060033F8 RID: 13304 RVA: 0x001DF48C File Offset: 0x001DE48C
	public void ᜀ(string A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ[num].PageSetup.CenterHeader = A_0;
					num++;
					num2 = 3;
					continue;
				case 2:
					goto IL_36;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						goto IL_38;
					}
					break;
				}
				break;
				IL_38:
				num2 = 1;
				continue;
				IL_36:
				goto IL_38;
			}
		}
	}

	// Token: 0x060033F9 RID: 13305 RVA: 0x001DF534 File Offset: 0x001DE534
	public Image ᜬ()
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
		return null;
	}

	// Token: 0x060033FA RID: 13306 RVA: 0x001DF570 File Offset: 0x001DE570
	public void ᜅ(Image A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (false)
						{
						}
						goto IL_30;
					}
					break;
				case 1:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ[num].PageSetup.CenterHeaderImage = A_0;
					num++;
					num2 = 0;
					continue;
				case 2:
					goto IL_2E;
				case 3:
					goto IL_44;
				}
				break;
				IL_30:
				num2 = 1;
				continue;
				IL_2E:
				goto IL_30;
			}
		}
		IL_44:
		if (true)
		{
		}
	}

	// Token: 0x060033FB RID: 13307 RVA: 0x001DF618 File Offset: 0x001DE618
	public bool ᜑ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				if (true)
				{
				}
				bool centerHorizontally = this.ᜀ[0].PageSetup.CenterHorizontally;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return centerHorizontally;
					case 1:
						goto IL_D3;
					case 2:
					{
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						bool centerHorizontally2 = this.ᜀ[num].PageSetup.CenterHorizontally;
						num2 = 3;
						continue;
					}
					case 3:
					{
						bool centerHorizontally2;
						if (centerHorizontally2 != centerHorizontally)
						{
							num2 = 5;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 1;
						continue;
					}
					case 4:
						goto IL_D3;
					case 5:
						return false;
					}
					break;
					IL_D3:
					num2 = 2;
				}
			}
			return false;
		}
	}

	// Token: 0x060033FC RID: 13308 RVA: 0x001DF718 File Offset: 0x001DE718
	public void ᜊ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (false)
						{
						}
						goto IL_30;
					}
					break;
				case 1:
					if (num >= count)
					{
						if (true)
						{
						}
						num2 = 3;
						continue;
					}
					this.ᜀ[num].PageSetup.CenterHorizontally = A_0;
					num++;
					num2 = 0;
					continue;
				case 2:
					goto IL_2E;
				case 3:
					return;
				}
				break;
				IL_30:
				num2 = 1;
				continue;
				IL_2E:
				goto IL_30;
			}
		}
	}

	// Token: 0x060033FD RID: 13309 RVA: 0x001DF7C0 File Offset: 0x001DE7C0
	public bool ᜈ()
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
			for (;;)
			{
				bool centerVertically = this.ᜀ[0].PageSetup.CenterVertically;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return false;
					case 1:
					{
						bool centerVertically2;
						if (centerVertically2 != centerVertically)
						{
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 4;
						continue;
					}
					case 2:
						return centerVertically;
					case 3:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						bool centerVertically2 = this.ᜀ[num].PageSetup.CenterVertically;
						num2 = 1;
						continue;
					}
					case 4:
						goto IL_D3;
					case 5:
						goto IL_D3;
					}
					break;
					IL_D3:
					num2 = 3;
				}
			}
			return false;
		}
	}

	// Token: 0x060033FE RID: 13310 RVA: 0x001DF8C0 File Offset: 0x001DE8C0
	public void ᜂ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_2E;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (false)
						{
						}
						goto IL_38;
					}
					break;
				case 2:
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					this.ᜀ[num].PageSetup.CenterVertically = A_0;
					num++;
					num2 = 1;
					continue;
				case 3:
					return;
				}
				break;
				IL_38:
				num2 = 2;
				continue;
				IL_2E:
				if (true)
				{
				}
				goto IL_38;
			}
		}
	}

	// Token: 0x060033FF RID: 13311 RVA: 0x001DF968 File Offset: 0x001DE968
	public int ᜐ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int copies = this.ᜀ[0].PageSetup.Copies;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int copies2;
						if (copies2 != copies)
						{
							num2 = 2;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 5;
						continue;
					}
					case 1:
						goto IL_CF;
					case 2:
						return int.MinValue;
					case 3:
						return copies;
					case 4:
					{
						if (num >= count)
						{
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						int copies2 = this.ᜀ[num].PageSetup.Copies;
						num2 = 0;
						continue;
					}
					case 5:
						goto IL_CF;
					}
					break;
					IL_CF:
					num2 = 4;
				}
			}
			return int.MinValue;
		}
	}

	// Token: 0x06003400 RID: 13312 RVA: 0x001DFA6C File Offset: 0x001DEA6C
	public void ᜄ(int A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						if (false)
						{
						}
						goto IL_38;
					}
					break;
				case 1:
					return;
				case 2:
					goto IL_36;
				case 3:
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					this.ᜀ[num].PageSetup.Copies = A_0;
					num++;
					num2 = 0;
					continue;
				}
				break;
				IL_38:
				num2 = 3;
				continue;
				IL_36:
				goto IL_38;
			}
		}
	}

	// Token: 0x06003401 RID: 13313 RVA: 0x001DFB14 File Offset: 0x001DEB14
	public bool ᜎ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool draft = this.ᜀ[0].PageSetup.Draft;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						bool draft2;
						if (draft2 != draft)
						{
							num2 = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 3;
						continue;
					}
					case 1:
						goto IL_D1;
					case 2:
						goto IL_D3;
					case 3:
						goto IL_D3;
					case 4:
					{
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						bool draft2 = this.ᜀ[num].PageSetup.Draft;
						num2 = 0;
						continue;
					}
					case 5:
						return draft;
					}
					break;
					IL_D3:
					num2 = 4;
				}
			}
			IL_D1:
			if (true)
			{
			}
			return false;
		}
	}

	// Token: 0x06003402 RID: 13314 RVA: 0x001DFC14 File Offset: 0x001DEC14
	public void ᜇ(bool A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					this.ᜀ[num].PageSetup.Draft = A_0;
					num++;
					num2 = 3;
					continue;
				case 1:
					goto IL_44;
				case 2:
					goto IL_2E;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					default:
						if (false)
						{
						}
						goto IL_30;
					}
					break;
				}
				break;
				IL_30:
				num2 = 0;
				continue;
				IL_2E:
				goto IL_30;
			}
		}
		IL_44:
		if (true)
		{
		}
	}

	// Token: 0x06003403 RID: 13315 RVA: 0x001DFCBC File Offset: 0x001DECBC
	public int ᜤ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int firstPageNumber = this.ᜀ[0].PageSetup.FirstPageNumber;
				int num = 1;
				int count = this.ᜀ.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (num >= count)
						{
							num2 = 5;
							continue;
						}
						if (true)
						{
						}
						int firstPageNumber2 = this.ᜀ[num].PageSetup.FirstPageNumber;
						num2 = 2;
						continue;
					}
					case 1:
						goto IL_D7;
					case 2:
					{
						int firstPageNumber2;
						if (firstPageNumber2 != firstPageNumber)
						{
							num2 = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num++;
							break;
						}
						num2 = 3;
						continue;
					}
					case 3:
						goto IL_D7;
					case 4:
						return int.MinValue;
					case 5:
						return firstPageNumber;
					}
					break;
					IL_D7:
					num2 = 0;
				}
			}
			return int.MinValue;
		}
	}

	// Token: 0x06003404 RID: 13316 RVA: 0x001DFDC0 File Offset: 0x001DEDC0
	public void ᜅ(int A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6A:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				return;
			case 1:
				if (num2 >= count)
				{
					goto IL_6A;
				}
				this.ᜀ[num2].PageSetup.FirstPageNumber = A_0;
				num2++;
				num = 3;
				continue;
			case 2:
				if (true)
				{
				}
				goto IL_5E;
			case 3:
				goto IL_5E;
			}
			goto IL_3E;
			IL_5E:
			num = 1;
		}
		return;
		IL_3E:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 2;
		goto IL_28;
	}

	// Token: 0x06003405 RID: 13317 RVA: 0x001DFE68 File Offset: 0x001DEE68
	public double ᜄ()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (false)
		{
		}
		for (;;)
		{
			double footerMarginInch = this.ᜀ[0].PageSetup.FooterMarginInch;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					goto IL_DB;
				case 1:
					goto IL_DB;
				case 2:
				{
					double footerMarginInch2;
					if (footerMarginInch2 != footerMarginInch)
					{
						num2 = 3;
						continue;
					}
					num++;
					num2 = 1;
					continue;
				}
				case 3:
					goto IL_D9;
				case 4:
					return footerMarginInch;
				case 5:
				{
					if (num >= count)
					{
						num2 = 4;
						continue;
					}
					double footerMarginInch2 = this.ᜀ[num].PageSetup.FooterMarginInch;
					num2 = 2;
					continue;
				}
				}
				break;
				IL_DB:
				num2 = 5;
			}
		}
		IL_D9:
		return double.MinValue;
	}

	// Token: 0x06003406 RID: 13318 RVA: 0x001DFF70 File Offset: 0x001DEF70
	public void ᜀ(double A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6A:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				goto IL_5E;
			case 2:
				return;
			case 3:
				if (num2 >= count)
				{
					goto IL_6A;
				}
				this.ᜀ[num2].PageSetup.FooterMarginInch = A_0;
				num2++;
				num = 1;
				continue;
			}
			goto IL_3E;
			IL_5E:
			num = 3;
		}
		return;
		IL_3E:
		if (true)
		{
		}
		num2 = 0;
		count = this.ᜀ.Count;
		num = 0;
		goto IL_28;
	}

	// Token: 0x06003407 RID: 13319 RVA: 0x001E0018 File Offset: 0x001DF018
	public double \u1712()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (false)
		{
		}
		if (true)
		{
		}
		for (;;)
		{
			double headerMarginInch = this.ᜀ[0].PageSetup.HeaderMarginInch;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_D9;
				case 1:
				{
					double headerMarginInch2;
					if (headerMarginInch2 != headerMarginInch)
					{
						num2 = 0;
						continue;
					}
					num++;
					num2 = 2;
					continue;
				}
				case 2:
					goto IL_DB;
				case 3:
				{
					if (num >= count)
					{
						num2 = 4;
						continue;
					}
					double headerMarginInch2 = this.ᜀ[num].PageSetup.HeaderMarginInch;
					num2 = 1;
					continue;
				}
				case 4:
					return headerMarginInch;
				case 5:
					goto IL_DB;
				}
				break;
				IL_DB:
				num2 = 3;
			}
		}
		IL_D9:
		return double.MinValue;
	}

	// Token: 0x06003408 RID: 13320 RVA: 0x001E0120 File Offset: 0x001DF120
	public void ᜄ(double A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6A:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				goto IL_5E;
			case 2:
				return;
			case 3:
				if (num2 >= count)
				{
					goto IL_6A;
				}
				this.ᜀ[num2].PageSetup.HeaderMarginInch = A_0;
				num2++;
				num = 1;
				continue;
			}
			goto IL_3E;
			IL_5E:
			num = 3;
		}
		return;
		IL_3E:
		if (true)
		{
		}
		num2 = 0;
		count = this.ᜀ.Count;
		num = 0;
		goto IL_28;
	}

	// Token: 0x06003409 RID: 13321 RVA: 0x001E01C8 File Offset: 0x001DF1C8
	public string ᜁ()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (false)
		{
		}
		for (;;)
		{
			string leftFooter = this.ᜀ[0].PageSetup.LeftFooter;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					if (num >= count)
					{
						num2 = 4;
						continue;
					}
					string leftFooter2 = this.ᜀ[num].PageSetup.LeftFooter;
					num2 = 1;
					continue;
				}
				case 1:
				{
					string leftFooter2;
					if (leftFooter2 != leftFooter)
					{
						num2 = 3;
						continue;
					}
					num++;
					num2 = 5;
					continue;
				}
				case 2:
					goto IL_D5;
				case 3:
					goto IL_C1;
				case 4:
					return leftFooter;
				case 5:
					goto IL_D5;
				}
				break;
				IL_D5:
				num2 = 0;
			}
		}
		IL_C1:
		if (true)
		{
		}
		return null;
	}

	// Token: 0x0600340A RID: 13322 RVA: 0x001E02CC File Offset: 0x001DF2CC
	public void ᜂ(string A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_56;
			case 1:
				return;
			case 2:
				goto IL_56;
			case 3:
				if (num2 >= count)
				{
					goto IL_62;
				}
				this.ᜀ[num2].PageSetup.LeftFooter = A_0;
				num2++;
				if (true)
				{
				}
				num = 2;
				continue;
			}
			goto IL_3E;
			IL_56:
			num = 3;
		}
		return;
		IL_3E:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 0;
		goto IL_28;
	}

	// Token: 0x0600340B RID: 13323 RVA: 0x001E0374 File Offset: 0x001DF374
	public Image ᜊ()
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
		return null;
	}

	// Token: 0x0600340C RID: 13324 RVA: 0x001E03B0 File Offset: 0x001DF3B0
	public void ᜀ(Image A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6A:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			if (true)
			{
			}
			goto IL_46;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 0:
				goto IL_5E;
			case 1:
				if (num2 >= count)
				{
					goto IL_6A;
				}
				this.ᜀ[num2].PageSetup.LeftFooterImage = A_0;
				num2++;
				num = 0;
				continue;
			case 2:
				return;
			case 3:
				goto IL_5E;
			}
			goto IL_46;
			IL_5E:
			num = 1;
		}
		return;
		IL_46:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 3;
		goto IL_30;
	}

	// Token: 0x0600340D RID: 13325 RVA: 0x001E0458 File Offset: 0x001DF458
	public string \u1715()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (false)
		{
		}
		string leftHeader;
		for (;;)
		{
			leftHeader = this.ᜀ[0].PageSetup.LeftHeader;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_E9;
				case 1:
				{
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					string leftHeader2 = this.ᜀ[num].PageSetup.LeftHeader;
					num2 = 4;
					continue;
				}
				case 2:
					goto IL_CD;
				case 3:
					goto IL_CD;
				case 4:
				{
					string leftHeader2;
					if (leftHeader2 != leftHeader)
					{
						num2 = 5;
						continue;
					}
					num++;
					num2 = 3;
					continue;
				}
				case 5:
					goto IL_C1;
				}
				break;
				IL_CD:
				num2 = 1;
			}
		}
		IL_C1:
		return null;
		IL_E9:
		if (true)
		{
		}
		return leftHeader;
	}

	// Token: 0x0600340E RID: 13326 RVA: 0x001E055C File Offset: 0x001DF55C
	public void ᜁ(string A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6A:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				goto IL_56;
			case 1:
				if (true)
				{
				}
				if (num2 >= count)
				{
					goto IL_6A;
				}
				this.ᜀ[num2].PageSetup.LeftHeader = A_0;
				num2++;
				num = 0;
				continue;
			case 2:
				return;
			case 3:
				goto IL_56;
			}
			goto IL_3E;
			IL_56:
			num = 1;
		}
		return;
		IL_3E:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 3;
		goto IL_28;
	}

	// Token: 0x0600340F RID: 13327 RVA: 0x001E0604 File Offset: 0x001DF604
	public Image ᜨ()
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
		return null;
	}

	// Token: 0x06003410 RID: 13328 RVA: 0x001E0640 File Offset: 0x001DF640
	public void ᜃ(Image A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6A:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_5E;
			case 2:
				if (num2 >= count)
				{
					goto IL_6A;
				}
				this.ᜀ[num2].PageSetup.LeftHeaderImage = A_0;
				num2++;
				num = 1;
				continue;
			case 3:
				goto IL_5E;
			}
			goto IL_46;
			IL_5E:
			num = 2;
		}
		return;
		IL_46:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 3;
		goto IL_28;
	}

	// Token: 0x06003411 RID: 13329 RVA: 0x001E06E8 File Offset: 0x001DF6E8
	public double ᜆ()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2C;
				}
				break;
			}
		}
		IL_2C:
		if (false)
		{
		}
		for (;;)
		{
			double leftMargin = this.ᜀ[0].PageSetup.LeftMargin;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_DB;
				case 1:
				{
					double leftMargin2;
					if (leftMargin2 != leftMargin)
					{
						num2 = 4;
						continue;
					}
					num++;
					num2 = 2;
					continue;
				}
				case 2:
					goto IL_DB;
				case 3:
					return leftMargin;
				case 4:
					goto IL_D9;
				case 5:
				{
					if (num >= count)
					{
						num2 = 3;
						continue;
					}
					double leftMargin2 = this.ᜀ[num].PageSetup.LeftMargin;
					num2 = 1;
					continue;
				}
				}
				break;
				IL_DB:
				num2 = 5;
			}
		}
		IL_D9:
		return double.MinValue;
	}

	// Token: 0x06003412 RID: 13330 RVA: 0x001E07F0 File Offset: 0x001DF7F0
	public void ᜁ(double A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_56;
			case 2:
				goto IL_56;
			case 3:
				if (num2 >= count)
				{
					goto IL_62;
				}
				this.ᜀ[num2].PageSetup.LeftMargin = A_0;
				num2++;
				if (true)
				{
				}
				num = 1;
				continue;
			}
			goto IL_3E;
			IL_56:
			num = 3;
		}
		return;
		IL_3E:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 2;
		goto IL_28;
	}

	// Token: 0x06003413 RID: 13331 RVA: 0x001E0898 File Offset: 0x001DF898
	public OrderType ᜫ()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (false)
		{
		}
		for (;;)
		{
			OrderType order = this.ᜀ[0].PageSetup.Order;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return order;
				case 1:
				{
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					OrderType order2 = this.ᜀ[num].PageSetup.Order;
					num2 = 2;
					continue;
				}
				case 2:
				{
					OrderType order2;
					if (order2 != order)
					{
						num2 = 4;
						continue;
					}
					num++;
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_C8;
				case 4:
					return OrderType.DownThenOver;
				case 5:
					goto IL_C8;
				}
				break;
				IL_C8:
				if (true)
				{
				}
				num2 = 1;
			}
		}
		return OrderType.DownThenOver;
	}

	// Token: 0x06003414 RID: 13332 RVA: 0x001E0994 File Offset: 0x001DF994
	public void ᜀ(OrderType A_0)
	{
		if (true)
		{
		}
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_6A:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_30:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_5E;
			case 2:
				if (num2 >= count)
				{
					goto IL_6A;
				}
				this.ᜀ[num2].PageSetup.Order = A_0;
				num2++;
				num = 3;
				continue;
			case 3:
				goto IL_5E;
			}
			goto IL_46;
			IL_5E:
			num = 2;
		}
		return;
		IL_46:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 1;
		goto IL_30;
	}

	// Token: 0x06003415 RID: 13333 RVA: 0x001E0A3C File Offset: 0x001DFA3C
	public PageOrientationType \u171C()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (false)
		{
		}
		for (;;)
		{
			PageOrientationType orientation = this.ᜀ[0].PageSetup.Orientation;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					if (num >= count)
					{
						num2 = 5;
						continue;
					}
					PageOrientationType orientation2 = this.ᜀ[num].PageSetup.Orientation;
					num2 = 2;
					continue;
				}
				case 1:
					goto IL_D3;
				case 2:
				{
					PageOrientationType orientation2;
					if (orientation2 != orientation)
					{
						num2 = 3;
						continue;
					}
					num++;
					num2 = 1;
					continue;
				}
				case 3:
					goto IL_D1;
				case 4:
					goto IL_D3;
				case 5:
					return orientation;
				}
				break;
				IL_D3:
				num2 = 0;
			}
		}
		IL_D1:
		if (true)
		{
		}
		return PageOrientationType.Portrait;
	}

	// Token: 0x06003416 RID: 13334 RVA: 0x001E0B3C File Offset: 0x001DFB3C
	public void ᜀ(PageOrientationType A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_56;
			case 2:
				if (num2 >= count)
				{
					goto IL_62;
				}
				if (true)
				{
				}
				this.ᜀ[num2].PageSetup.Orientation = A_0;
				num2++;
				num = 1;
				continue;
			case 3:
				goto IL_56;
			}
			goto IL_3E;
			IL_56:
			num = 2;
		}
		return;
		IL_3E:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 3;
		goto IL_28;
	}

	// Token: 0x06003417 RID: 13335 RVA: 0x001E0BE4 File Offset: 0x001DFBE4
	public PaperSizeType ᜉ()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (true)
		{
		}
		if (false)
		{
		}
		for (;;)
		{
			PaperSizeType paperSize = this.ᜀ[0].PageSetup.PaperSize;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return paperSize;
				case 1:
				{
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					PaperSizeType paperSize2 = this.ᜀ[num].PageSetup.PaperSize;
					num2 = 5;
					continue;
				}
				case 2:
					goto IL_D4;
				case 3:
					return PaperSizeType.PaperA4;
				case 4:
					goto IL_D4;
				case 5:
				{
					PaperSizeType paperSize2;
					if (paperSize2 != paperSize)
					{
						num2 = 3;
						continue;
					}
					num++;
					num2 = 4;
					continue;
				}
				}
				break;
				IL_D4:
				num2 = 1;
			}
		}
		return PaperSizeType.PaperA4;
	}

	// Token: 0x06003418 RID: 13336 RVA: 0x001E0CE4 File Offset: 0x001DFCE4
	public void ᜀ(PaperSizeType A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_62:
			num = 2;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				if (num2 >= count)
				{
					goto IL_62;
				}
				this.ᜀ[num2].PageSetup.PaperSize = A_0;
				num2++;
				num = 3;
				continue;
			case 1:
				goto IL_56;
			case 2:
				goto IL_6A;
			case 3:
				goto IL_56;
			}
			goto IL_3E;
			IL_56:
			num = 0;
		}
		IL_6A:
		if (true)
		{
		}
		return;
		IL_3E:
		num2 = 0;
		count = this.ᜀ.Count;
		num = 1;
		goto IL_28;
	}

	// Token: 0x06003419 RID: 13337 RVA: 0x001E0D8C File Offset: 0x001DFD8C
	public PrintCommentType ᜢ()
	{
		for (;;)
		{
			switch (0)
			{
			default:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_24;
				}
				break;
			}
		}
		IL_24:
		if (false)
		{
		}
		for (;;)
		{
			PrintCommentType printComments = this.ᜀ[0].PageSetup.PrintComments;
			int num = 1;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_D3;
				case 1:
					return printComments;
				case 2:
				{
					PrintCommentType printComments2;
					if (printComments2 != printComments)
					{
						num2 = 4;
						continue;
					}
					if (true)
					{
					}
					num++;
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_D3;
				case 4:
					return PrintCommentType.InPlace;
				case 5:
				{
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					PrintCommentType printComments2 = this.ᜀ[num].PageSetup.PrintComments;
					num2 = 2;
					continue;
				}
				}
				break;
				IL_D3:
				num2 = 5;
			}
		}
		return PrintCommentType.InPlace;
	}

	// Token: 0x0600341A RID: 13338 RVA: 0x001E0E8C File Offset: 0x001DFE8C
	public void ᜀ(PrintCommentType A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			IL_3C:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						this.ᜀ[num].PageSetup.PrintComments = A_0;
						num++;
						num2 = 3;
						continue;
					case 1:
						goto IL_54;
					case 2:
						return;
					case 3:
						goto IL_54;
					}
					goto IL_3C;
					IL_54:
					num2 = 0;
					break;
				}
			}
		}
	}

	// Token: 0x0600341B RID: 13339 RVA: 0x001E0F38 File Offset: 0x001DFF38
	public PrintErrorsType ᜅ()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					PrintErrorsType printErrors = this.ᜀ[0].PageSetup.PrintErrors;
					int num = 1;
					int count = this.ᜀ.Count;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							PrintErrorsType printErrors2;
							if (printErrors2 != printErrors)
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 2;
							continue;
						}
						case 1:
							return PrintErrorsType.Displayed;
						case 2:
							goto IL_AC;
						case 3:
							goto IL_AC;
						case 4:
							return printErrors;
						case 5:
							if (num < count)
							{
								PrintErrorsType printErrors2 = this.ᜀ[num].PageSetup.PrintErrors;
								num2 = 0;
								continue;
							}
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num2 = 4;
								continue;
							}
							break;
						}
						break;
						IL_AC:
						num2 = 5;
					}
				}
				break;
			}
		}
		return PrintErrorsType.Displayed;
	}

	// Token: 0x0600341C RID: 13340 RVA: 0x001E1034 File Offset: 0x001E0034
	public void ᜀ(PrintErrorsType A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 1;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						if (true)
						{
						}
						this.ᜀ[num].PageSetup.PrintErrors = A_0;
						num++;
						num2 = 3;
						continue;
					case 1:
						goto IL_4C;
					case 2:
						return;
					case 3:
						goto IL_4C;
					}
					goto IL_34;
					IL_4C:
					num2 = 0;
					break;
				}
			}
		}
	}

	// Token: 0x0600341D RID: 13341 RVA: 0x001E10E0 File Offset: 0x001E00E0
	public bool \u171F()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					bool printNotes = this.ᜀ[0].PageSetup.PrintNotes;
					int num = 1;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							bool printNotes2;
							if (printNotes2 != printNotes)
							{
								num2 = 4;
								continue;
							}
							num++;
							num2 = 5;
							continue;
						}
						case 1:
							if (true)
							{
							}
							goto IL_B7;
						case 2:
							return printNotes;
						case 3:
							if (num < count)
							{
								bool printNotes2 = this.ᜀ[num].PageSetup.PrintNotes;
								num2 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num2 = 2;
								continue;
							}
							break;
						case 4:
							return false;
						case 5:
							goto IL_B7;
						}
						break;
						IL_B7:
						num2 = 3;
					}
				}
				break;
			}
		}
		return false;
	}

	// Token: 0x0600341E RID: 13342 RVA: 0x001E11E0 File Offset: 0x001E01E0
	public void ᜃ(bool A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_4C;
					case 1:
						return;
					case 2:
						goto IL_4C;
					case 3:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						this.ᜀ[num].PageSetup.PrintNotes = A_0;
						num++;
						num2 = 2;
						continue;
					}
					goto IL_34;
					IL_4C:
					if (true)
					{
					}
					num2 = 3;
					break;
				}
			}
		}
	}

	// Token: 0x0600341F RID: 13343 RVA: 0x001E128C File Offset: 0x001E028C
	public int ᜇ()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					int printQuality = this.ᜀ[0].PageSetup.PrintQuality;
					int num = 1;
					int count = this.ᜀ.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_B3;
						case 1:
							goto IL_B3;
						case 2:
							return int.MinValue;
						case 3:
						{
							int printQuality2;
							if (printQuality2 != printQuality)
							{
								num2 = 2;
								continue;
							}
							num++;
							num2 = 1;
							continue;
						}
						case 4:
							return printQuality;
						case 5:
							if (num < count)
							{
								int printQuality2 = this.ᜀ[num].PageSetup.PrintQuality;
								num2 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num2 = 4;
								continue;
							}
							break;
						}
						break;
						IL_B3:
						num2 = 5;
					}
				}
				break;
			}
		}
		return int.MinValue;
	}

	// Token: 0x06003420 RID: 13344 RVA: 0x001E1390 File Offset: 0x001E0390
	public void ᜃ(int A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			IL_3C:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						return;
					case 1:
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						this.ᜀ[num].PageSetup.PrintQuality = A_0;
						num++;
						num2 = 2;
						continue;
					case 2:
						goto IL_54;
					case 3:
						goto IL_54;
					}
					goto IL_3C;
					IL_54:
					num2 = 1;
					break;
				}
			}
		}
	}

	// Token: 0x06003421 RID: 13345 RVA: 0x001E143C File Offset: 0x001E043C
	public string \u1716()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					string rightFooter = this.ᜀ[0].PageSetup.RightFooter;
					int num = 1;
					int count = this.ᜀ.Count;
					int num2 = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_BC;
						case 1:
						{
							string rightFooter2;
							if (rightFooter2 != rightFooter)
							{
								num2 = 2;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						}
						case 2:
							goto IL_BA;
						case 3:
							return rightFooter;
						case 4:
							goto IL_BC;
						case 5:
							if (num < count)
							{
								string rightFooter2 = this.ᜀ[num].PageSetup.RightFooter;
								num2 = 1;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num2 = 3;
								continue;
							}
							break;
						}
						break;
						IL_BC:
						num2 = 5;
					}
				}
				break;
			}
		}
		IL_BA:
		return null;
	}

	// Token: 0x06003422 RID: 13346 RVA: 0x001E1540 File Offset: 0x001E0540
	public void ᜈ(string A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_94;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_60;
					case 1:
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						this.ᜀ[num].PageSetup.RightFooter = A_0;
						num++;
						num2 = 2;
						continue;
					case 2:
						goto IL_4C;
					case 3:
						goto IL_4C;
					}
					goto IL_34;
					IL_4C:
					num2 = 1;
					break;
				}
			}
		}
		IL_60:
		IL_94:
		if (true)
		{
		}
	}

	// Token: 0x06003423 RID: 13347 RVA: 0x001E15EC File Offset: 0x001E05EC
	public string ᜦ()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					string rightHeader = this.ᜀ[0].PageSetup.RightHeader;
					int num = 1;
					int count = this.ᜀ.Count;
					int num2 = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							return rightHeader;
						case 1:
							if (num < count)
							{
								string rightHeader2 = this.ᜀ[num].PageSetup.RightHeader;
								num2 = 5;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						case 2:
							goto IL_BC;
						case 3:
							goto IL_BA;
						case 4:
							goto IL_BC;
						case 5:
						{
							string rightHeader2;
							if (rightHeader2 != rightHeader)
							{
								num2 = 3;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						}
						}
						break;
						IL_BC:
						num2 = 1;
					}
				}
				break;
			}
		}
		IL_BA:
		return null;
	}

	// Token: 0x06003424 RID: 13348 RVA: 0x001E16F0 File Offset: 0x001E06F0
	public void ᜇ(string A_0)
	{
		for (;;)
		{
			IL_3C:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						this.ᜀ[num].PageSetup.RightHeader = A_0;
						num++;
						num2 = 1;
						continue;
					case 1:
						goto IL_54;
					case 2:
						goto IL_54;
					case 3:
						return;
					}
					goto IL_3C;
					IL_54:
					num2 = 0;
					break;
				}
			}
		}
	}

	// Token: 0x06003425 RID: 13349 RVA: 0x001E179C File Offset: 0x001E079C
	public Image ᜌ()
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
		return null;
	}

	// Token: 0x06003426 RID: 13350 RVA: 0x001E17D8 File Offset: 0x001E07D8
	public void ᜂ(Image A_0)
	{
		for (;;)
		{
			IL_3C:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_54;
					case 1:
						return;
					case 2:
						goto IL_54;
					case 3:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						this.ᜀ[num].PageSetup.RightFooterImage = A_0;
						num++;
						num2 = 0;
						continue;
					}
					goto IL_3C;
					IL_54:
					num2 = 3;
					break;
				}
			}
		}
	}

	// Token: 0x06003427 RID: 13351 RVA: 0x001E1884 File Offset: 0x001E0884
	public Image \u1717()
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
		return null;
	}

	// Token: 0x06003428 RID: 13352 RVA: 0x001E18C0 File Offset: 0x001E08C0
	public void ᜄ(Image A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_54;
					case 1:
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						this.ᜀ[num].PageSetup.RightHeaderImage = A_0;
						num++;
						num2 = 2;
						continue;
					case 2:
						goto IL_54;
					case 3:
						return;
					}
					goto IL_34;
					IL_54:
					num2 = 1;
					break;
				}
			}
		}
	}

	// Token: 0x06003429 RID: 13353 RVA: 0x001E196C File Offset: 0x001E096C
	public double ᜃ()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					double rightMargin = this.ᜀ[0].PageSetup.RightMargin;
					int num = 1;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
							goto IL_BF;
						case 1:
							goto IL_BF;
						case 2:
							if (num < count)
							{
								double rightMargin2 = this.ᜀ[num].PageSetup.RightMargin;
								num2 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num2 = 5;
								continue;
							}
							break;
						case 3:
						{
							double rightMargin2;
							if (rightMargin2 != rightMargin)
							{
								num2 = 4;
								continue;
							}
							num++;
							num2 = 0;
							continue;
						}
						case 4:
							goto IL_BD;
						case 5:
							return rightMargin;
						}
						break;
						IL_BF:
						num2 = 2;
					}
				}
				break;
			}
		}
		IL_BD:
		return double.MinValue;
	}

	// Token: 0x0600342A RID: 13354 RVA: 0x001E1A74 File Offset: 0x001E0A74
	public void ᜂ(double A_0)
	{
		for (;;)
		{
			IL_34:
			if (true)
			{
			}
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_54;
					case 1:
						return;
					case 2:
						goto IL_54;
					case 3:
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						this.ᜀ[num].PageSetup.RightMargin = A_0;
						num++;
						num2 = 2;
						continue;
					}
					goto IL_34;
					IL_54:
					num2 = 3;
					break;
				}
			}
		}
	}

	// Token: 0x0600342B RID: 13355 RVA: 0x001E1B20 File Offset: 0x001E0B20
	public double \u1719()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					double topMargin = this.ᜀ[0].PageSetup.TopMargin;
					int num = 1;
					int count = this.ᜀ.Count;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							double topMargin2;
							if (topMargin2 != topMargin)
							{
								num2 = 3;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						}
						case 1:
							goto IL_BF;
						case 2:
							if (num < count)
							{
								double topMargin2 = this.ᜀ[num].PageSetup.TopMargin;
								num2 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num2 = 5;
								continue;
							}
							break;
						case 3:
							goto IL_BD;
						case 4:
							goto IL_BF;
						case 5:
							return topMargin;
						}
						break;
						IL_BF:
						num2 = 2;
					}
				}
				break;
			}
		}
		IL_BD:
		return double.MinValue;
	}

	// Token: 0x0600342C RID: 13356 RVA: 0x001E1C28 File Offset: 0x001E0C28
	public void ᜃ(double A_0)
	{
		for (;;)
		{
			IL_3C:
			int num = 0;
			int count = this.ᜀ.Count;
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_54;
					case 2:
						if (num >= count)
						{
							num2 = 0;
							continue;
						}
						this.ᜀ[num].PageSetup.TopMargin = A_0;
						num++;
						num2 = 1;
						continue;
					case 3:
						goto IL_54;
					}
					goto IL_3C;
					IL_54:
					num2 = 2;
					break;
				}
			}
		}
	}

	// Token: 0x0600342D RID: 13357 RVA: 0x001E1CD4 File Offset: 0x001E0CD4
	public int \u171B()
	{
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					int zoom = this.ᜀ[0].PageSetup.Zoom;
					int num = 1;
					int count = this.ᜀ.Count;
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return zoom;
						case 1:
							return int.MinValue;
						case 2:
						{
							if (true)
							{
							}
							int zoom2;
							if (zoom2 != zoom)
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 3;
							continue;
						}
						case 3:
							goto IL_BB;
						case 4:
							if (num < count)
							{
								int zoom2 = this.ᜀ[num].PageSetup.Zoom;
								num2 = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						case 5:
							goto IL_BB;
						}
						break;
						IL_BB:
						num2 = 4;
					}
				}
				break;
			}
		}
		return int.MinValue;
	}

	// Token: 0x0600342E RID: 13358 RVA: 0x001E1DD8 File Offset: 0x001E0DD8
	public void ᜂ(int A_0)
	{
		for (;;)
		{
			IL_34:
			int num = 0;
			int count = this.ᜀ.Count;
			if (true)
			{
			}
			int num2 = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					switch (num2)
					{
					case 0:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						this.ᜀ[num].PageSetup.Zoom = A_0;
						num++;
						num2 = 1;
						continue;
					case 1:
						goto IL_54;
					case 2:
						return;
					case 3:
						goto IL_54;
					}
					goto IL_34;
					IL_54:
					num2 = 0;
					break;
				}
			}
		}
	}

	// Token: 0x0600342F RID: 13359 RVA: 0x001E1E84 File Offset: 0x001E0E84
	public Bitmap ᜩ()
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
		throw new NotSupportedException();
	}

	// Token: 0x06003430 RID: 13360 RVA: 0x001E1EC4 File Offset: 0x001E0EC4
	public void ᜀ(Bitmap A_0)
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
		throw new NotSupportedException();
	}

	// Token: 0x040016E3 RID: 5859
	private spr\u233D ᜀ;
}
