using System;
using System.Collections.Generic;
using Spire.Xls.Charts;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Charts
{
	// Token: 0x0200017F RID: 383
	public class XlsChartLegendEntry : XlsObject, IChartLegendEntry
	{
		// Token: 0x06001228 RID: 4648 RVA: 0x000B0AE4 File Offset: 0x000AFAE4
		internal XlsChartLegendEntry(spr\u1DF5 A_0, object A_1, int A_2) : base(A_0, A_1)
		{
			this.ᜀ = (spr\u1A75)spr\u175E.ᜀ(TBIFFRecord.ChartLegendxn);
			this.ᜁ = new ChartTextArea((spr\u2158)A_0, this);
			this.ᜃ = A_2;
			this.SetParents();
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x000B0B30 File Offset: 0x000AFB30
		internal XlsChartLegendEntry(spr\u1DF5 A_0, object A_1, int A_2, IList<BiffRecordRaw> A_3, ref int A_4)
		{
			int a_ = 16;
			base..ctor(A_0, A_1);
			if (A_3 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("≅⥇㹉ⵋ", a_));
			}
			this.ᜃ = A_2;
			this.SetParents();
			this.ᜀ(A_3, ref A_4);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x000B0B80 File Offset: 0x000AFB80
		internal void ᜀ(IList<BiffRecordRaw> A_0, ref int A_1)
		{
			int a_ = 11;
			int num = 14;
			for (;;)
			{
				BiffRecordRaw biffRecordRaw;
				TBIFFRecord typeCode;
				switch (num)
				{
				case 0:
					goto IL_134;
				case 1:
				{
					if (biffRecordRaw.TypeCode != TBIFFRecord.Begin)
					{
						num = 13;
						continue;
					}
					A_1++;
					int num2 = 1;
					num = 10;
					continue;
				}
				case 2:
					num = 8;
					continue;
				case 3:
					goto IL_A6;
				case 4:
					switch (typeCode)
					{
					case TBIFFRecord.Begin:
						A_1 = BiffRecordRaw.SkipBeginEndBlock(A_0, A_1);
						num = 3;
						continue;
					case TBIFFRecord.End:
					{
						int num2;
						num2--;
						num = 5;
						continue;
					}
					default:
						num = 2;
						continue;
					}
					break;
				case 5:
					goto IL_A6;
				case 6:
					goto IL_16D;
				case 7:
				{
					int num2;
					if (num2 == 0)
					{
						num = 15;
						continue;
					}
					biffRecordRaw = A_0[A_1];
					typeCode = biffRecordRaw.TypeCode;
					num = 6;
					continue;
				}
				case 8:
					goto IL_A6;
				case 9:
					num = 4;
					continue;
				case 10:
					goto IL_134;
				case 11:
					goto IL_A6;
				case 12:
					goto IL_64;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_16D;
					default:
						goto IL_97;
					}
					break;
				case 15:
					return;
				}
				if (A_0 == null)
				{
					num = 12;
					continue;
				}
				biffRecordRaw = A_0[A_1];
				biffRecordRaw.CheckTypeCode(TBIFFRecord.ChartLegendxn);
				this.ᜀ = (spr\u1A75)A_0[A_1];
				A_1++;
				biffRecordRaw = A_0[A_1];
				num = 1;
				continue;
				IL_A6:
				A_1++;
				num = 0;
				continue;
				IL_134:
				num = 7;
				continue;
				IL_16D:
				if (typeCode != TBIFFRecord.ChartText)
				{
					num = 9;
				}
				else
				{
					this.ᜁ = new ChartTextArea((spr\u2158)base.ReservedHandle, this);
					A_1 = this.ᜁ.ᜀ(A_0, A_1) - 1;
					num = 11;
				}
			}
			IL_64:
			throw new ArgumentNullException(RecordTableEnumerator.b("╀≂ㅄ♆", a_));
			IL_97:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x000B0DB0 File Offset: 0x000AFDB0
		protected internal void SetParents()
		{
			int a_ = 13;
			for (;;)
			{
				this.ᜂ = (ChartLegendEntriesColl)base.FindParent(typeof(ChartLegendEntriesColl));
				if (this.ᜂ == null)
				{
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_60;
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("ፂ⑄㕆ⱈ╊㥌潎㹐ㅒ㽔㉖㩘⽚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᕲᩴɶ᝸ὺ卼", a_));
			IL_60:
			if (false)
			{
			}
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000B0E30 File Offset: 0x000AFE30
		internal void ᜀ(IList<IRecordStorage> A_0)
		{
			int a_ = 2;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_71;
				case 1:
					if (!this.IsFormatted)
					{
						num = 5;
						continue;
					}
					goto IL_73;
				case 2:
					if (!this.IsDeleted)
					{
						num = 4;
						continue;
					}
					goto IL_73;
				case 3:
					if (this.ᜁ != null)
					{
						goto IL_9C;
					}
					return;
				case 4:
					return;
				case 5:
					num = 2;
					continue;
				case 6:
					A_0.Add(spr\u175E.ᜀ(TBIFFRecord.Begin));
					this.ᜁ.ᜀ(A_0, true);
					A_0.Add(spr\u175E.ᜀ(TBIFFRecord.End));
					num = 7;
					continue;
				case 7:
					return;
				}
				if (A_0 == null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9C;
					}
					if (false)
					{
					}
					num = 0;
					continue;
				}
				if (true)
				{
				}
				num = 1;
				continue;
				IL_73:
				A_0.Add((BiffRecordRaw)this.ᜀ.Clone());
				num = 3;
				continue;
				IL_9C:
				num = 6;
			}
			IL_71:
			throw new ArgumentException(RecordTableEnumerator.b("䨷弹弻儽㈿♁㝃", a_));
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x000B0F84 File Offset: 0x000AFF84
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x000B0FCC File Offset: 0x000AFFCC
		public bool IsDeleted
		{
			get
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
				return this.ᜀ.ᜁ();
			}
			set
			{
				int a_ = 0;
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4D;
						default:
							if (false)
							{
							}
							if (!this.ᜂ.CanDelete(this.ᜃ))
							{
								num = 6;
								continue;
							}
							goto IL_57;
						}
						break;
					case 2:
						if (value)
						{
							num = 0;
							continue;
						}
						goto IL_AD;
					case 3:
						num = 2;
						continue;
					case 4:
						if (!this.ᜁ.ParentWorkbook.Loading)
						{
							num = 5;
							continue;
						}
						goto IL_57;
					case 5:
						goto IL_8D;
					case 6:
						num = 4;
						continue;
					case 7:
						goto IL_C4;
					case 8:
						goto IL_AD;
					}
					goto IL_41;
					IL_4D:
					num = 3;
					continue;
					IL_41:
					if (this.IsDeleted != value)
					{
						goto IL_4D;
					}
					return;
					IL_57:
					this.IsFormatted = !value;
					num = 8;
					continue;
					IL_AD:
					this.ᜀ.ᜁ(value);
					num = 7;
				}
				IL_8D:
				throw new ApplicationException(RecordTableEnumerator.b("爵崷嘹夻䨽┿扁⡃❅㭇㹉汋≍㕏㕑ㅓ㡕㱗穙㥛そᑟၡᵣ䙥๧୩իɭᕯᙱ婳", a_));
				IL_C4:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x000B1114 File Offset: 0x000B0114
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x000B115C File Offset: 0x000B015C
		public bool IsFormatted
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
				return this.ᜀ.ᜀ();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_46;
					case 2:
						if (true)
						{
						}
						num = 3;
						continue;
					case 3:
						if (value)
						{
							num = 5;
							continue;
						}
						goto IL_46;
					case 4:
						return;
					case 5:
						this.ᜁ = new ChartTextArea((spr\u2158)base.ReservedHandle, this);
						this.ᜀ.ᜁ(false);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (value != this.IsFormatted)
					{
						num = 2;
						continue;
					}
					break;
					IL_46:
					this.ᜀ.ᜀ(value);
					num = 4;
				}
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x000B1238 File Offset: 0x000B0238
		public IChartTextArea TextArea
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
				this.ᜀ.ᜁ(false);
				this.ᜀ.ᜀ(true);
				return this.ᜁ;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x000B1294 File Offset: 0x000B0294
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x000B12DC File Offset: 0x000B02DC
		public int LegendEntityIndex
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
				return (int)this.ᜀ.ᜃ();
			}
			set
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
				this.ᜀ.ᜀ((ushort)value);
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x000B1324 File Offset: 0x000B0324
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x000B1368 File Offset: 0x000B0368
		public int Index
		{
			get
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x000B13AC File Offset: 0x000B03AC
		public void Clear()
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
			this.IsFormatted = false;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000B13F0 File Offset: 0x000B03F0
		public void Delete()
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
			this.IsDeleted = true;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000B1434 File Offset: 0x000B0434
		public XlsChartLegendEntry Clone(object parent, Dictionary<int, int> dicIndexes, Dictionary<string, string> dicNewSheetNames)
		{
			XlsChartLegendEntry xlsChartLegendEntry;
			for (;;)
			{
				xlsChartLegendEntry = (XlsChartLegendEntry)base.MemberwiseClone();
				xlsChartLegendEntry.SetParent(parent);
				xlsChartLegendEntry.SetParents();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						xlsChartLegendEntry.ᜁ = (XlsChartTextArea)this.ᜁ.Clone(xlsChartLegendEntry, dicIndexes, dicNewSheetNames);
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_47;
						default:
							goto IL_90;
						}
						break;
					case 2:
						if (this.ᜁ != null)
						{
							goto IL_47;
						}
						goto IL_98;
					}
					break;
					IL_47:
					num = 0;
				}
			}
			IL_90:
			if (false)
			{
			}
			IL_98:
			xlsChartLegendEntry.ᜀ = (spr\u1A75)spr\u1CD3.ᜀ(this.ᜀ);
			return xlsChartLegendEntry;
		}

		// Token: 0x04000E4A RID: 3658
		private int \u25D9\u0083\u008B\u008F;

		// Token: 0x04000E4B RID: 3659
		private int \u2460\u0089\u00A4\u0084;

		// Token: 0x04000E4C RID: 3660
		private long \u2593\u009C\u00A8\u0090;

		// Token: 0x04000E4D RID: 3661
		private byte[] \u25D8\u00A0\u00AB\u00AE;

		// Token: 0x04000E4E RID: 3662
		private spr\u1A75 ᜀ;

		// Token: 0x04000E4F RID: 3663
		private XlsChartTextArea ᜁ;

		// Token: 0x04000E50 RID: 3664
		private ChartLegendEntriesColl ᜂ;

		// Token: 0x04000E51 RID: 3665
		private int[] \u2593\u0091\u0084\u0093;

		// Token: 0x04000E52 RID: 3666
		private int ᜃ;
	}
}
