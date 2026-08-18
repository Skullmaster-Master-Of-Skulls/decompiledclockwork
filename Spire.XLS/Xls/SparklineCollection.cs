using System;
using System.Collections.Generic;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls
{
	// Token: 0x0200015C RID: 348
	public class SparklineCollection : List<ISparkline>, ISparklines
	{
		// Token: 0x06000F8A RID: 3978 RVA: 0x0009DBB8 File Offset: 0x0009CBB8
		public Sparkline Add()
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
			Sparkline sparkline = new Sparkline();
			base.Add(sparkline);
			return sparkline;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0009DC04 File Offset: 0x0009CC04
		public void Add(CellRange dataRange, CellRange referenceRange)
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
			this.Add(dataRange, referenceRange, false);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0009DC48 File Offset: 0x0009CC48
		public void RefreshRanges(CellRange dataRange, CellRange referenceRange)
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
			this.RefreshRanges(dataRange, referenceRange, false);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0009DC8C File Offset: 0x0009CC8C
		public void Add(CellRange dataRange, CellRange referenceRange, bool isVertical)
		{
			int a_ = 14;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (referenceRange.Rows.Length != dataRange.Columns.Length)
					{
						num = 5;
						continue;
					}
					goto IL_130;
				case 1:
					num = 7;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_130;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					num = 0;
					continue;
				case 4:
					goto IL_91;
				case 5:
					goto IL_E2;
				case 6:
					if (referenceRange.Rows.Length <= dataRange.Rows.Length)
					{
						num = 1;
						continue;
					}
					goto IL_EC;
				case 7:
					if (referenceRange.Columns.Length > dataRange.Columns.Length)
					{
						num = 4;
						continue;
					}
					goto IL_130;
				}
				if (isVertical)
				{
					num = 3;
				}
				else
				{
					num = 6;
				}
			}
			IL_91:
			goto IL_EC;
			IL_E2:
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙃❅♇ⵉ⥋", a_), RecordTableEnumerator.b("၃⹅ⵇ橉㹋⭍㙏㝑♓㍕㙗㥙㥛繝य़ᅡ䑣ࡥݧṩ䱫ᡭᅯṱᵳት噷", a_));
			IL_EC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㙃❅♇ⵉ⥋", a_), RecordTableEnumerator.b("၃⹅ⵇ橉㹋⭍㙏㝑♓㍕㙗㥙㥛繝य़ᅡ䑣ࡥݧṩ䱫ᡭᅯṱᵳት噷", a_));
			IL_130:
			this.ᜀ(dataRange, referenceRange, isVertical);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0009DDD4 File Offset: 0x0009CDD4
		public void RefreshRanges(CellRange dataRange, CellRange referenceRange, bool isVertical)
		{
			int a_ = 10;
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
						goto IL_130;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					goto IL_91;
				case 2:
					if (referenceRange.Rows.Length != dataRange.Columns.Length)
					{
						num = 7;
						continue;
					}
					goto IL_130;
				case 3:
					if (true)
					{
					}
					num = 4;
					continue;
				case 4:
					if (referenceRange.Columns.Length > dataRange.Columns.Length)
					{
						num = 1;
						continue;
					}
					goto IL_130;
				case 5:
					num = 2;
					continue;
				case 6:
					if (referenceRange.Rows.Length <= dataRange.Rows.Length)
					{
						num = 3;
						continue;
					}
					goto IL_EC;
				case 7:
					goto IL_EA;
				}
				if (isVertical)
				{
					num = 5;
				}
				else
				{
					num = 6;
				}
			}
			IL_91:
			goto IL_EC;
			IL_EA:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㈿⍁⩃ⅅⵇ", a_), RecordTableEnumerator.b("ᐿ⩁⅃晅㩇⽉⩋⭍≏㝑㩓㕕㵗穙籛ⱝşౡͣͥ䡧ͩὫ乭ṯᵱs噵๷᭹ၻ᝽겁", a_));
			IL_EC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㈿⍁⩃ⅅⵇ", a_), RecordTableEnumerator.b("ᐿ⩁⅃晅㩇⽉⩋⭍≏㝑㩓㕕㵗穙⹛㽝๟աţ䙥ŧᥩ䱫mὯٱ味u᥷ᙹᕻ᩽깿", a_));
			IL_130:
			base.Clear();
			this.ᜀ(dataRange, referenceRange, isVertical);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0009DF20 File Offset: 0x0009CF20
		public void Clear(Sparkline sparkline)
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
			base.Remove(sparkline);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0009DF64 File Offset: 0x0009CF64
		internal void ᜀ(IXLSRange A_0, IXLSRange A_1, bool A_2)
		{
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					int num2;
					int num5;
					int num8;
					int num9;
					switch (num)
					{
					case 0:
						num = 20;
						continue;
					case 1:
						if (A_1.Columns.Length > 1)
						{
							num = 10;
							continue;
						}
						goto IL_141;
					case 2:
						if (num2 < A_0.Rows.Length)
						{
							num = 3;
							continue;
						}
						return;
					case 3:
						num = 8;
						continue;
					case 4:
					{
						int num3;
						if (num3 >= A_1.Columns.Length)
						{
							num = 16;
							continue;
						}
						int num4;
						base.Add(new Sparkline
						{
							DataRange = (CellRange)A_0.Columns[num4],
							RefRange = (CellRange)A_1.Columns[num3]
						});
						num4++;
						num3++;
						num = 27;
						continue;
					}
					case 5:
						goto IL_2AB;
					case 6:
						goto IL_39F;
					case 7:
						num = 23;
						continue;
					case 8:
						if (num5 < A_1.Rows.Length)
						{
							base.Add(new Sparkline
							{
								DataRange = (CellRange)A_0.Rows[num2],
								RefRange = (CellRange)A_1.Rows[num5]
							});
							num2++;
							num5++;
							num = 14;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_EE;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 9:
						if (A_1.Rows.Length == 1)
						{
							num = 31;
							continue;
						}
						goto IL_374;
					case 10:
						num = 17;
						continue;
					case 11:
						if (A_1.Columns.Length > 1)
						{
							num = 19;
							continue;
						}
						goto IL_374;
					case 12:
						num = 11;
						continue;
					case 13:
						if (true)
						{
						}
						goto IL_116;
					case 14:
						goto IL_1C1;
					case 15:
						goto IL_1C1;
					case 16:
						return;
					case 17:
						if (A_1.Rows.Length == 1)
						{
							num = 26;
							continue;
						}
						goto IL_141;
					case 19:
						num = 9;
						continue;
					case 20:
					{
						int num6;
						if (num6 >= A_1.Columns.Length)
						{
							num = 21;
							continue;
						}
						int num7;
						base.Add(new Sparkline
						{
							DataRange = (CellRange)A_0.Rows[num7],
							RefRange = (CellRange)A_1.Columns[num6]
						});
						num7++;
						num6++;
						num = 22;
						continue;
					}
					case 21:
						return;
					case 22:
						goto IL_C2;
					case 23:
						if (num8 >= A_1.Rows.Length)
						{
							num = 33;
							continue;
						}
						base.Add(new Sparkline
						{
							DataRange = (CellRange)A_0.Columns[num9],
							RefRange = (CellRange)A_1.Rows[num8]
						});
						num9++;
						num8++;
						num = 32;
						continue;
					case 24:
						num = 4;
						continue;
					case 25:
					{
						int num4;
						if (num4 < A_0.Columns.Length)
						{
							num = 24;
							continue;
						}
						return;
					}
					case 26:
					{
						int num7 = 0;
						int num6 = 0;
						num = 28;
						continue;
					}
					case 27:
						goto IL_116;
					case 28:
						goto IL_C2;
					case 29:
					{
						int num7;
						if (num7 < A_0.Rows.Length)
						{
							num = 0;
							continue;
						}
						return;
					}
					case 30:
						if (num9 < A_0.Columns.Length)
						{
							num = 7;
							continue;
						}
						return;
					case 31:
					{
						int num4 = 0;
						int num3 = 0;
						num = 13;
						continue;
					}
					case 32:
						goto IL_39F;
					case 33:
						return;
					}
					if (A_2)
					{
						num = 12;
						continue;
					}
					goto IL_EE;
					IL_C2:
					num = 29;
					continue;
					IL_EE:
					num = 1;
					continue;
					IL_116:
					num = 25;
					continue;
					IL_141:
					num2 = 0;
					num5 = 0;
					num = 15;
					continue;
					IL_1C1:
					num = 2;
					continue;
					IL_374:
					num9 = 0;
					num8 = 0;
					num = 6;
					continue;
					IL_39F:
					num = 30;
				}
				return;
				IL_2AB:
				return;
			}
			}
		}

		// Token: 0x04000DA6 RID: 3494
		private float \u2609\u0090\u0099\u0095;

		// Token: 0x04000DA7 RID: 3495
		private bool \u2460\u008D\u00AC\u009F;

		// Token: 0x04000DA8 RID: 3496
		private long \u25D8\u00A5\u0087\u009A;

		// Token: 0x04000DA9 RID: 3497
		private string[] \u2609\u00A9ª\u0094;

		// Token: 0x04000DAA RID: 3498
		internal SparklineGroup ᜀ;
	}
}
