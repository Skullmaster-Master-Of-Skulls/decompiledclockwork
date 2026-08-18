using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x02000039 RID: 57
	public class PivotTableCollection : CollectionExtended<object>, ICloneParent, IEnumerable<XlsPivotTable>, IPivotTables
	{
		// Token: 0x17000150 RID: 336
		public IPivotTable this[int index]
		{
			get
			{
				int a_ = 14;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (index >= base.Count)
						{
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						}
						goto Block_2;
					case 1:
						goto IL_6C;
					case 3:
						num = 0;
						continue;
					}
					goto IL_29;
					IL_35:
					num = 3;
					continue;
					IL_29:
					if (true)
					{
					}
					if (index >= 0)
					{
						goto IL_35;
					}
					break;
				}
				IL_3F:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃ⡅ⱇ⽉㑋", a_));
				IL_6C:
				goto IL_3F;
				Block_2:
				if (false)
				{
				}
				return (IPivotTable)base.InnerList[index];
			}
		}

		// Token: 0x17000151 RID: 337
		public IPivotTable this[string name]
		{
			get
			{
				IPivotTable result = null;
				using (List<object>.Enumerator enumerator = base.InnerList.GetEnumerator())
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							IPivotTable pivotTable;
							if (pivotTable.Name == name)
							{
								num = 6;
								continue;
							}
							break;
						}
						case 1:
							IL_90:
							goto IL_92;
						case 2:
							goto IL_92;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							IPivotTable pivotTable = (IPivotTable)enumerator.Current;
							num = 0;
							continue;
						}
						case 5:
							goto IL_C1;
						case 6:
						{
							IPivotTable pivotTable;
							result = pivotTable;
							num = 1;
							continue;
						}
						}
						IL_6B:
						num = 3;
						continue;
						goto IL_6B;
						IL_92:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_90;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 5;
							break;
						}
					}
					IL_C1:;
				}
				return result;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00023F1C File Offset: 0x00022F1C
		public XlsWorksheet ParentWorksheet
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
				return base.FindParent(typeof(XlsWorksheet)) as XlsWorksheet;
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00023F6C File Offset: 0x00022F6C
		internal PivotTableCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00023F84 File Offset: 0x00022F84
		public int Parse(IList data, int iPos)
		{
			int a_ = 15;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_CC;
				case 2:
					num = 6;
					continue;
				case 3:
					goto IL_4C;
				case 4:
				{
					BiffRecordRaw biffRecordRaw;
					if (biffRecordRaw.TypeCode != TBIFFRecord.PivotViewDefinition)
					{
						num = 7;
						continue;
					}
					XlsPivotTable xlsPivotTable = new XlsPivotTable(base.AppImplementation, this);
					iPos = xlsPivotTable.Parse(data, iPos);
					biffRecordRaw = (BiffRecordRaw)data[iPos];
					base.Add(xlsPivotTable);
					num = 1;
					continue;
				}
				case 5:
					if (iPos >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_72;
				case 6:
				{
					if (iPos > data.Count - 1)
					{
						num = 8;
						continue;
					}
					BiffRecordRaw biffRecordRaw = (BiffRecordRaw)data[iPos];
					num = 9;
					continue;
				}
				case 7:
					goto IL_EF;
				case 8:
					goto IL_72;
				case 9:
					goto IL_CC;
				}
				if (data == null)
				{
					num = 3;
					continue;
				}
				IL_B0:
				num = 5;
				continue;
				IL_72:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_B0;
				default:
					goto IL_88;
				}
				IL_CC:
				num = 4;
			}
			IL_4C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⅄♆㵈⩊", a_));
			IL_88:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⱄᝆ♈㡊", a_), RecordTableEnumerator.b("ፄ♆╈㹊⡌潎㉐㉒㭔㥖㙘⽚絜㵞Ѡ䍢।ɦᩨᡪ䵬᭮ᥰቲ᭴坶䥸孺ᱼᅾꎂ歷뎒ﾖ붜ﮞ삠힢쒤覦캪쎬좮얰\udbb2閴骶馸誺", a_));
			IL_EF:
			if (true)
			{
			}
			return iPos;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00024100 File Offset: 0x00023100
		[CLSCompliant(false)]
		internal new void ᜀ(RecordArrayList A_0)
		{
			int a_ = 18;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					if (true)
					{
					}
					XlsPivotTable xlsPivotTable = (XlsPivotTable)base.InnerList[num2];
					xlsPivotTable.Serialize(A_0);
					num2++;
					num = 3;
					continue;
				}
				case 2:
					return;
				case 3:
					goto IL_6B;
				case 4:
					goto IL_3C;
				case 5:
					goto IL_B3;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num2 = 0;
					int count = base.Count;
					num = 5;
					continue;
				}
				}
				IL_B3:
				num = 1;
				continue;
				IL_6B:
				goto IL_B3;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩇⽉⽋⅍≏㙑❓", a_));
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000241EC File Offset: 0x000231EC
		internal new void ᜁ(XlsPivotTable A_0)
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
			base.Add(A_0);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00024230 File Offset: 0x00023230
		public PivotTable Add(string name, CellRange location, PivotCache cache)
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
			PivotTable pivotTable = new PivotTable((spr\u2158)base.AppImplementation, this, cache.Index, location);
			pivotTable.Name = name;
			pivotTable.Cache.IsRefreshOnLoad = true;
			this.ᜁ(pivotTable);
			return pivotTable;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000242A0 File Offset: 0x000232A0
		public PivotTablesCollection Clone(XlsWorksheet worksheet, Dictionary<string, string> hashWorksheetNames)
		{
			int num = 0;
			switch (num)
			{
			default:
			{
				PivotTablesCollection pivotTablesCollection;
				for (;;)
				{
					if (true)
					{
					}
					pivotTablesCollection = new PivotTablesCollection((spr\u2158)worksheet.AppImplementation, worksheet);
					XlsWorkbook parentWorkbook = worksheet.ParentWorkbook;
					int num2 = 0;
					int count = base.Count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							return pivotTablesCollection;
						case 1:
							goto IL_82;
						case 2:
							goto IL_82;
						case 3:
						{
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							XlsPivotTable xlsPivotTable = (XlsPivotTable)this[num2];
							xlsPivotTable = xlsPivotTable.ᜀ(pivotTablesCollection, hashWorksheetNames);
							pivotTablesCollection.ᜁ(xlsPivotTable);
							num2++;
							num = 2;
							continue;
						}
						}
						break;
						IL_82:
						num = 3;
					}
				}
				return pivotTablesCollection;
			}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0002437C File Offset: 0x0002337C
		public void Remove(string name)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					XlsPivotTable xlsPivotTable = null;
					List<object>.Enumerator enumerator = base.InnerList.GetEnumerator();
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							try
							{
								num2 = 5;
								for (;;)
								{
									XlsPivotTable xlsPivotTable2;
									switch (num2)
									{
									case 0:
										xlsPivotTable = xlsPivotTable2;
										num2 = 1;
										continue;
									case 1:
										goto IL_120;
									case 2:
										goto IL_E7;
									case 3:
										goto IL_12C;
									case 4:
										goto IL_120;
									case 7:
										if (!xlsPivotTable2.Name.Equals(name))
										{
											num++;
											num2 = 6;
											continue;
										}
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_E7;
										default:
											if (false)
											{
											}
											num2 = 0;
											continue;
										}
										break;
									}
									goto IL_91;
									IL_E7:
									if (!enumerator.MoveNext())
									{
										num2 = 4;
										continue;
									}
									xlsPivotTable2 = (XlsPivotTable)enumerator.Current;
									num2 = 7;
									continue;
									IL_DE:
									num2 = 2;
									continue;
									IL_91:
									goto IL_DE;
									IL_120:
									num2 = 3;
								}
								IL_12C:
								goto IL_42;
							}
							finally
							{
								((IDisposable)enumerator).Dispose();
							}
							goto IL_13F;
							IL_42:
							num2 = 2;
							continue;
						case 1:
							goto IL_13F;
						case 2:
							if (xlsPivotTable != null)
							{
								num2 = 1;
								continue;
							}
							return;
						case 3:
							return;
						}
						break;
						IL_13F:
						if (true)
						{
						}
						base.InnerList.RemoveAt(num);
						this.ᜀ(xlsPivotTable);
						num2 = 3;
					}
				}
				return;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0002450C File Offset: 0x0002350C
		internal new void ᜀ(int A_0)
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
			string name = this[A_0].Name;
			this.Remove(name);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0002455C File Offset: 0x0002355C
		private new void ᜀ(XlsPivotTable A_0)
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
			XlsWorkbook workbook = A_0.Workbook;
			workbook.PivotCaches;
			workbook.ᜃ(A_0.CacheIndex);
			A_0.ᜄ();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000245B8 File Offset: 0x000235B8
		internal new void ᜀ()
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
			base.Clear();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000245FC File Offset: 0x000235FC
		public new IEnumerator<XlsPivotTable> GetEnumerator()
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
			PivotTableCollection.ᜁ ᜁ = new PivotTableCollection.ᜁ(0);
			ᜁ.ᜂ = this;
			return ᜁ;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00024648 File Offset: 0x00023648
		IEnumerator IEnumerable.GetEnumerator()
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
			PivotTableCollection.ᜀ ᜀ = new PivotTableCollection.ᜀ(0);
			ᜀ.ᜂ = this;
			return ᜀ;
		}

		// Token: 0x02000231 RID: 561
		[CompilerGenerated]
		private new sealed class ᜁ : IEnumerator<XlsPivotTable>
		{
			// Token: 0x0600223A RID: 8762 RVA: 0x00132234 File Offset: 0x00131234
			bool IEnumerator.ᜂ()
			{
				bool result;
				try
				{
					for (;;)
					{
						int num = this.ᜁ;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_144;
							case 1:
								if (!this.ᜄ.MoveNext())
								{
									if (true)
									{
									}
									num2 = 5;
									continue;
								}
								this.ᜃ = (XlsPivotTable)this.ᜄ.Current;
								this.ᜀ = this.ᜃ;
								this.ᜁ = 2;
								result = true;
								num2 = 7;
								continue;
							case 2:
								goto IL_144;
							case 3:
								switch (num)
								{
								case 0:
									this.ᜁ = -1;
									this.ᜄ = this.ᜂ.InnerList.GetEnumerator();
									this.ᜁ = 1;
									num2 = 8;
									continue;
								case 1:
									goto IL_144;
								case 2:
									this.ᜁ = 1;
									num2 = 6;
									continue;
								default:
									num2 = 9;
									continue;
								}
								break;
							case 4:
								goto IL_151;
							case 5:
								this.ᜀ();
								num2 = 0;
								continue;
							case 6:
								goto IL_5B;
							case 7:
								goto IL_B8;
							case 8:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_144;
								default:
									if (false)
									{
									}
									goto IL_5B;
								}
								break;
							case 9:
								num2 = 2;
								continue;
							}
							break;
							IL_5B:
							num2 = 1;
							continue;
							IL_144:
							result = false;
							num2 = 4;
						}
					}
					IL_B8:
					IL_151:;
				}
				catch
				{
					this.ᜁ();
					throw;
				}
				return result;
			}

			// Token: 0x0600223B RID: 8763 RVA: 0x001323C4 File Offset: 0x001313C4
			[DebuggerHidden]
			XlsPivotTable IEnumerator<XlsPivotTable>.ᜃ()
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
				return this.ᜀ;
			}

			// Token: 0x0600223C RID: 8764 RVA: 0x00132408 File Offset: 0x00131408
			[DebuggerHidden]
			void IEnumerator.ᜄ()
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

			// Token: 0x0600223D RID: 8765 RVA: 0x00132448 File Offset: 0x00131448
			void IDisposable.ᜁ()
			{
				switch (this.ᜁ)
				{
				case 1:
				case 2:
					try
					{
						return;
					}
					finally
					{
						this.ᜀ();
					}
					break;
				default:
					IL_17:
					break;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_17;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					return;
				}
			}

			// Token: 0x0600223E RID: 8766 RVA: 0x001324B8 File Offset: 0x001314B8
			[DebuggerHidden]
			object IEnumerator.ᜅ()
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

			// Token: 0x0600223F RID: 8767 RVA: 0x001324FC File Offset: 0x001314FC
			[DebuggerHidden]
			public ᜁ(int A_0)
			{
				this.ᜁ = A_0;
			}

			// Token: 0x06002240 RID: 8768 RVA: 0x00132518 File Offset: 0x00131518
			private void ᜀ()
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
				this.ᜁ = -1;
				((IDisposable)this.ᜄ).Dispose();
			}

			// Token: 0x040011E6 RID: 4582
			private XlsPivotTable ᜀ;

			// Token: 0x040011E7 RID: 4583
			private int ᜁ;

			// Token: 0x040011E8 RID: 4584
			public PivotTableCollection ᜂ;

			// Token: 0x040011E9 RID: 4585
			public XlsPivotTable ᜃ;

			// Token: 0x040011EA RID: 4586
			public List<object>.Enumerator ᜄ;
		}

		// Token: 0x02000232 RID: 562
		[CompilerGenerated]
		private new sealed class ᜀ : IEnumerator<object>
		{
			// Token: 0x06002241 RID: 8769 RVA: 0x0013256C File Offset: 0x0013156C
			bool IEnumerator.ᜂ()
			{
				bool result;
				try
				{
					for (;;)
					{
						int num = this.ᜁ;
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								if (true)
								{
								}
								num2 = 6;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									switch (num)
									{
									case 0:
										this.ᜁ = -1;
										this.ᜄ = this.ᜂ.InnerList.GetEnumerator();
										this.ᜁ = 1;
										num2 = 3;
										continue;
									case 1:
										goto IL_14A;
									case 2:
										this.ᜁ = 1;
										num2 = 4;
										continue;
									default:
										num2 = 0;
										continue;
									}
									break;
								}
								break;
							case 2:
								if (!this.ᜄ.MoveNext())
								{
									num2 = 7;
									continue;
								}
								this.ᜃ = (XlsPivotTable)this.ᜄ.Current;
								this.ᜀ = this.ᜃ;
								this.ᜁ = 2;
								result = true;
								num2 = 9;
								continue;
							case 3:
								goto IL_7F;
							case 4:
								goto IL_7F;
							case 5:
								goto IL_157;
							case 6:
								goto IL_14A;
							case 7:
								this.ᜀ();
								num2 = 8;
								continue;
							case 8:
								goto IL_14A;
							case 9:
								goto IL_DA;
							}
							break;
							IL_7F:
							num2 = 2;
							continue;
							IL_14A:
							result = false;
							num2 = 5;
						}
					}
					IL_DA:
					IL_157:;
				}
				catch
				{
					this.ᜁ();
					throw;
				}
				return result;
			}

			// Token: 0x06002242 RID: 8770 RVA: 0x00132700 File Offset: 0x00131700
			[DebuggerHidden]
			object IEnumerator<object>.ᜃ()
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
				return this.ᜀ;
			}

			// Token: 0x06002243 RID: 8771 RVA: 0x00132744 File Offset: 0x00131744
			[DebuggerHidden]
			void IEnumerator.ᜄ()
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

			// Token: 0x06002244 RID: 8772 RVA: 0x00132784 File Offset: 0x00131784
			void IDisposable.ᜁ()
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
					switch (this.ᜁ)
					{
					case 1:
					case 2:
						try
						{
							return;
						}
						finally
						{
							if (true)
							{
							}
							this.ᜀ();
						}
						break;
					}
					break;
				}
			}

			// Token: 0x06002245 RID: 8773 RVA: 0x001327F4 File Offset: 0x001317F4
			[DebuggerHidden]
			object IEnumerator.ᜅ()
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

			// Token: 0x06002246 RID: 8774 RVA: 0x00132838 File Offset: 0x00131838
			[DebuggerHidden]
			public ᜀ(int A_0)
			{
				this.ᜁ = A_0;
			}

			// Token: 0x06002247 RID: 8775 RVA: 0x00132854 File Offset: 0x00131854
			private void ᜀ()
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
				this.ᜁ = -1;
				((IDisposable)this.ᜄ).Dispose();
			}

			// Token: 0x040011EB RID: 4587
			private object ᜀ;

			// Token: 0x040011EC RID: 4588
			private int ᜁ;

			// Token: 0x040011ED RID: 4589
			public PivotTableCollection ᜂ;

			// Token: 0x040011EE RID: 4590
			public XlsPivotTable ᜃ;

			// Token: 0x040011EF RID: 4591
			public List<object>.Enumerator ᜄ;
		}
	}
}
