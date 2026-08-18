using System;
using System.Collections;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001D2 RID: 466
	public class FontList : IEnumerable
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x0009D828 File Offset: 0x0009C828
		public IEnumerator GetEnumerator()
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
			return this.ᜀ.GetEnumerator();
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0009D870 File Offset: 0x0009C870
		public int Add(CellFont Item)
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
			return this.ᜀ.Add(Item);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0009D8B8 File Offset: 0x0009C8B8
		public int FontIndexByFont(CellFont Font)
		{
			int num;
			for (;;)
			{
				for (;;)
				{
					num = 0;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (this[num].IsEqual(Font))
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 4;
							continue;
						case 1:
							goto IL_70;
						case 2:
							return -1;
						case 3:
							goto IL_72;
						case 4:
							goto IL_72;
						case 5:
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
								if (num >= this.Count)
								{
									num2 = 2;
									continue;
								}
								num2 = 0;
								continue;
							}
							break;
						}
						break;
						IL_72:
						num2 = 5;
					}
				}
			}
			IL_70:
			return this[num].FontIndex;
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0009D97C File Offset: 0x0009C97C
		public int ListIndexByFont(CellFont Font)
		{
			int num;
			for (;;)
			{
				for (;;)
				{
					num = 0;
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return -1;
						case 1:
							return num;
						case 2:
							goto IL_67;
						case 3:
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
								if (num >= this.Count)
								{
									num2 = 0;
									continue;
								}
								num2 = 4;
								continue;
							}
							break;
						case 4:
							if (this[num].IsEqual(Font))
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 2;
							continue;
						case 5:
							goto IL_67;
						}
						break;
						IL_67:
						num2 = 3;
					}
				}
			}
			return num;
		}

		// Token: 0x170001B8 RID: 440
		public CellFont this[int Index]
		{
			get
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
				return this.ᜀ[Index] as CellFont;
			}
			set
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
				this.ᜀ[Index] = value;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0009DAC8 File Offset: 0x0009CAC8
		public int Count
		{
			get
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
				return this.ᜀ.Count;
			}
		}

		// Token: 0x04000AB7 RID: 2743
		private string \u2460\u0088\u00AE\u0088;

		// Token: 0x04000AB8 RID: 2744
		private long[] \u2609\u00A2\u0090\u00AB;

		// Token: 0x04000AB9 RID: 2745
		private string \u25D8\u00A3\u00A1\u00AC;

		// Token: 0x04000ABA RID: 2746
		private int[] \u2460\u00A8\u0094\u0097;

		// Token: 0x04000ABB RID: 2747
		private int \u2460\u009B\u009A\u0084;

		// Token: 0x04000ABC RID: 2748
		private float[] \u2460\u00AD\u00B0\u00B0;

		// Token: 0x04000ABD RID: 2749
		private ArrayList ᜀ = new ArrayList();
	}
}
