using System;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000033 RID: 51
	public class XlsWorksheetChartsCollection : CollectionExtended<object>, IChartShapes
	{
		// Token: 0x060003A2 RID: 930 RVA: 0x00020D74 File Offset: 0x0001FD74
		internal XlsWorksheetChartsCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00020D90 File Offset: 0x0001FD90
		protected internal IChart AddChart()
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
			IChart chart = this.ᜀ.Shapes.Add();
			base.InnerList.Add(chart);
			return chart;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00020DEC File Offset: 0x0001FDEC
		protected internal IChartShape InnerAddChart(IChartShape chart)
		{
			int a_ = 18;
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
				if (chart == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("⭇≉ⵋ㱍⑏", a_));
				}
				break;
			}
			base.InnerList.Add(chart);
			return chart;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00020E58 File Offset: 0x0001FE58
		private new void ᜀ()
		{
			int a_ = 9;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				this.ᜀ = (base.FindParent(typeof(XlsWorksheetBase), true) as XlsWorksheetBase);
				if (this.ᜀ == null)
				{
					if (true)
					{
					}
					throw new ArgumentNullException(RecordTableEnumerator.b("漾⁀ㅂ⁄⥆㵈歊≌ⵎ㭐㙒㙔⍖祘㡚㱜ㅞའౢᅤ䝦୨๪䵬८Ṱٲ᭴፶坸", a_));
				}
				break;
			}
		}

		// Token: 0x1700013D RID: 317
		public IChartShape this[int index]
		{
			get
			{
				int a_ = 12;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_97;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_97;
					case 3:
						if (index >= base.InnerList.Count)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_99;
					}
					if (index < 0)
					{
						break;
					}
					num = 1;
				}
				IL_53:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁⩃≅ⵇ㉉", a_));
				IL_97:
				goto IL_53;
				IL_99:
				return base.InnerList[index] as IChartShape;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00020F90 File Offset: 0x0001FF90
		protected internal IChartShape Add()
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
			IChartShape chart = this.ᜀ.Shapes.Add();
			return this.InnerAddChart(chart);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00020FE4 File Offset: 0x0001FFE4
		public new void RemoveAt(int index)
		{
			int a_ = 15;
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (index >= base.Count)
					{
						num = 2;
						continue;
					}
					goto IL_94;
				case 2:
					goto IL_92;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_92;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (index < 0)
				{
					break;
				}
				num = 0;
			}
			IL_5B:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱄ⥆ⵈ⹊㕌", a_));
			IL_92:
			goto IL_5B;
			IL_94:
			IChartShape chartShape = base.InnerList[index] as IChartShape;
			base.InnerList.RemoveAt(index);
			chartShape.Remove();
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000210AC File Offset: 0x000200AC
		protected internal IChartShape AddChart(IChartShape chart)
		{
			int a_ = 11;
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
				if (chart == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("≀⭂⑄㕆㵈", a_));
				}
				break;
			}
			this.InnerAddChart(chart);
			this.ᜀ.InnerShapes.AddShape((XlsShape)chart);
			return chart;
		}

		// Token: 0x0400009E RID: 158
		private byte \u25D8\u0096\u0094\u0090;

		// Token: 0x0400009F RID: 159
		private new XlsWorksheetBase ᜀ;
	}
}
