using System;
using System.Collections.Generic;
using System.Threading;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000037 RID: 55
	public class XlsWorkbookObjectsCollection : CollectionExtended<object>, ITabSheets
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x00022CE0 File Offset: 0x00021CE0
		internal XlsWorkbookObjectsCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00022D08 File Offset: 0x00021D08
		internal new void ᜀ(spr\u252A A_0)
		{
			int a_ = 19;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_90;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_52;
					}
					break;
				case 2:
					if (A_0.get_Name() == null)
					{
						num = 0;
						continue;
					}
					goto IL_A6;
				}
				IL_29:
				if (A_0 == null)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				num = 2;
				continue;
				goto IL_29;
			}
			IL_52:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㹈⑊㽌⑎㍐㱒㩔㱖", a_));
			IL_90:
			throw new ArgumentNullException(RecordTableEnumerator.b("❈⩊⁌⩎", a_));
			IL_A6:
			int count = base.List.Count;
			A_0.set_RealIndex(count);
			this.ᜀ.Add(A_0.get_Name(), count);
			base.InnerList.Add(A_0);
			A_0.add_NameChanged(new XlsEventHandler(this.ᜀ));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00022E00 File Offset: 0x00021E00
		public void Move(int iOldIndex, int iNewIndex)
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						if (this.ᜂ != null)
						{
							num = 5;
							continue;
						}
						return;
					case 1:
						goto IL_14C;
					case 2:
						goto IL_14C;
					case 3:
						if (num2 > num3)
						{
							num = 4;
							continue;
						}
						this[num2].set_RealIndex(num2);
						num2++;
						num = 1;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10B;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							this.ᜁ.MoveSheetIndex(iOldIndex, iNewIndex);
							this.ᜁ.UpdateActiveSheetAfterMove(iOldIndex, iNewIndex);
							num = 0;
							continue;
						}
						break;
					case 5:
					{
						TabSheetMovedEventArgs args = new TabSheetMovedEventArgs(iOldIndex, iNewIndex);
						this.ᜂ(this, args);
						num = 8;
						continue;
					}
					case 6:
						return;
					case 8:
						return;
					}
					if (iOldIndex == iNewIndex)
					{
						num = 6;
						continue;
					}
					IL_10B:
					spr\u252A item = this[iOldIndex];
					base.InnerList.RemoveAt(iOldIndex);
					base.InnerList.Insert(iNewIndex, item);
					int num4 = Math.Min(iNewIndex, iOldIndex);
					num3 = Math.Max(iNewIndex, iOldIndex);
					num2 = num4;
					num = 2;
					continue;
					IL_14C:
					num = 3;
				}
				return;
			}
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00022F80 File Offset: 0x00021F80
		public void MoveBefore(ITabSheet sheetToMove, ITabSheet sheetForPlacement)
		{
			switch (0)
			{
			default:
			{
				int realIndex;
				int realIndex2;
				for (;;)
				{
					spr\u252A spr_u252A = (spr\u252A)sheetToMove;
					spr\u252A spr_u252A2 = (spr\u252A)sheetForPlacement;
					realIndex = spr_u252A.get_RealIndex();
					realIndex2 = spr_u252A2.get_RealIndex();
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_81:
						num = 1;
						break;
					default:
						if (false)
						{
						}
						num = 2;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_A1;
						case 1:
							goto IL_8A;
						case 2:
							if (realIndex <= realIndex2)
							{
								num = 3;
								continue;
							}
							goto IL_81;
						case 3:
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_8A:
				if (true)
				{
				}
				int num2 = realIndex2;
				goto IL_A6;
				IL_A1:
				num2 = realIndex2 - 1;
				IL_A6:
				int iNewIndex = num2;
				this.Move(realIndex, iNewIndex);
				return;
			}
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00023040 File Offset: 0x00022040
		public void MoveAfter(ITabSheet sheetToMove, ITabSheet sheetForPlacement)
		{
			switch (0)
			{
			default:
			{
				int realIndex;
				int realIndex2;
				for (;;)
				{
					spr\u252A spr_u252A = (spr\u252A)sheetToMove;
					spr\u252A spr_u252A2 = (spr\u252A)sheetForPlacement;
					realIndex = spr_u252A.get_RealIndex();
					realIndex2 = spr_u252A2.get_RealIndex();
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_81:
						num = 3;
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
							goto IL_A3;
						case 1:
							if (realIndex <= realIndex2)
							{
								num = 2;
								continue;
							}
							goto IL_81;
						case 2:
							if (true)
							{
							}
							num = 0;
							continue;
						case 3:
							goto IL_8A;
						}
						break;
					}
				}
				IL_8A:
				int num2 = realIndex2 + 1;
				goto IL_A6;
				IL_A3:
				num2 = realIndex2;
				IL_A6:
				int iNewIndex = num2;
				this.Move(realIndex, iNewIndex);
				return;
			}
			}
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00023100 File Offset: 0x00022100
		public void DisposeInternalData()
		{
			for (;;)
			{
				int num = 0;
				int count = base.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_2B;
					case 1:
						goto IL_2B;
					case 2:
					{
						if (num >= count)
						{
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						XlsWorksheetBase xlsWorksheetBase = base.InnerList[num] as XlsWorksheetBase;
						xlsWorksheetBase.Dispose();
						num++;
						num2 = 0;
						continue;
					}
					case 3:
						return;
					}
					break;
					IL_2B:
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
						break;
					}
				}
			}
		}

		// Token: 0x1700014C RID: 332
		internal spr\u252A this[int A_0]
		{
			get
			{
				int a_ = 14;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						goto IL_81;
					case 2:
						goto IL_9A;
					}
					if (A_0 < 0)
					{
						break;
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
						if (false)
						{
						}
						num = 0;
						continue;
					}
					IL_81:
					if (A_0 < base.List.Count)
					{
						goto IL_9C;
					}
					num = 2;
				}
				IL_65:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⵃ⡅ⱇ⽉㑋", a_));
				IL_9A:
				goto IL_65;
				IL_9C:
				return base.List[A_0] as spr\u252A;
			}
		}

		// Token: 0x1700014D RID: 333
		public INamedObject this[string name]
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					int a_;
					if (this.ᜀ.TryGetValue(name, out a_))
					{
						return this[a_];
					}
					break;
				}
				}
				return null;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003DE RID: 990 RVA: 0x000232BC File Offset: 0x000222BC
		public IWorkbook Workbook
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
				return this.ᜁ;
			}
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00023300 File Offset: 0x00022300
		public override object Clone(object parent)
		{
			switch (0)
			{
			default:
			{
				XlsWorkbookObjectsCollection xlsWorkbookObjectsCollection;
				for (;;)
				{
					xlsWorkbookObjectsCollection = new XlsWorkbookObjectsCollection(base.AppImplementation, parent);
					List<object> innerList = base.InnerList;
					IList<object> list = xlsWorkbookObjectsCollection.List;
					xlsWorkbookObjectsCollection.ᜁ.Objects = xlsWorkbookObjectsCollection;
					int num = 0;
					int count = innerList.Count;
					int num2 = 2;
					for (;;)
					{
						int num3;
						int count2;
						switch (num2)
						{
						case 0:
							goto IL_12B;
						case 1:
							if (num3 >= count2)
							{
								num2 = 6;
								continue;
							}
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_16B;
							default:
							{
								if (false)
								{
								}
								XlsWorksheetBase xlsWorksheetBase = innerList[num3] as XlsWorksheetBase;
								XlsWorksheetBase a_ = list[num3] as XlsWorksheetBase;
								xlsWorksheetBase.ᜁ(a_);
								num3++;
								num2 = 5;
								continue;
							}
							}
							break;
						case 2:
							goto IL_14E;
						case 3:
						{
							if (num >= count)
							{
								num2 = 4;
								continue;
							}
							XlsWorksheetBase xlsWorksheetBase2 = innerList[num] as XlsWorksheetBase;
							object item = xlsWorksheetBase2.Clone(xlsWorkbookObjectsCollection, false);
							list.Add(item);
							num++;
							num2 = 7;
							continue;
						}
						case 4:
							goto IL_16B;
						case 5:
							goto IL_12B;
						case 6:
							return xlsWorkbookObjectsCollection;
						case 7:
							goto IL_14E;
						}
						break;
						IL_12B:
						num2 = 1;
						continue;
						IL_14E:
						num2 = 3;
						continue;
						IL_16B:
						num3 = 0;
						count2 = innerList.Count;
						num2 = 0;
					}
				}
				return xlsWorkbookObjectsCollection;
			}
			}
		}

		// Token: 0x1700014F RID: 335
		ITabSheet ITabSheets.this[int index]
		{
			get
			{
				int a_ = 12;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A5;
					case 1:
						num = 2;
						continue;
					case 2:
						goto IL_87;
					}
					if (index < 0)
					{
						break;
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
						num = 1;
						continue;
					}
					IL_87:
					if (true)
					{
					}
					if (index <= base.Count - 1)
					{
						goto IL_A7;
					}
					num = 0;
				}
				IL_5D:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭁⩃≅ⵇ㉉", a_), RecordTableEnumerator.b("ᑁ╃⩅㵇⽉汋ⵍㅏ㱑㩓㥕ⱗ穙㹛㭝䁟๡ţᕥ᭧䩩ᡫ٭ᅯᱱ味䙵塷᭹ቻ᩽ꁿﺉﲍ낏ﲓ몙\udf9b햟첡킣蚥薧誩鶫肭", a_));
				IL_A5:
				goto IL_5D;
				IL_A7:
				return (ITabSheet)base.InnerList[index];
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00023548 File Offset: 0x00022548
		private new void ᜀ()
		{
			int a_ = 13;
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5A;
			}
			if (false)
			{
			}
			this.ᜁ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜁ != null)
			{
				return;
			}
			IL_5A:
			throw new ArgumentNullException(RecordTableEnumerator.b("ፂ⑄㕆ⱈ╊㥌潎㹐ㅒ㽔㉖㩘⽚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰ᕲᩴɶ᝸ὺ卼", a_));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000235C8 File Offset: 0x000225C8
		protected override void OnInsertComplete(int index, object value)
		{
			for (;;)
			{
				spr\u252A spr_u252A = (spr\u252A)value;
				spr_u252A.add_NameChanged(new XlsEventHandler(this.ᜀ));
				this.ᜀ[spr_u252A.get_Name()] = index;
				int num = index;
				int count = base.List.Count;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_63;
					case 1:
						goto IL_63;
					case 2:
						goto IL_99;
					case 3:
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						this[index].set_RealIndex(num);
						num++;
						num2 = 1;
						continue;
					}
					break;
					IL_63:
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
						break;
					}
				}
			}
			IL_99:
			this.ᜁ.IncreaseSheetIndex(index);
			base.OnInsertComplete(index, value);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000236AC File Offset: 0x000226AC
		protected override void OnSetComplete(int index, object oldValue, object newValue)
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
			XlsWorksheet xlsWorksheet = (XlsWorksheet)oldValue;
			XlsWorksheet xlsWorksheet2 = (XlsWorksheet)newValue;
			xlsWorksheet.NameChanged -= this.ᜀ;
			this.ᜀ.Remove(xlsWorksheet.Name);
			this.ᜀ[xlsWorksheet2.Name] = index;
			base.OnSetComplete(index, oldValue, newValue);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00023734 File Offset: 0x00022734
		protected override void OnRemoveComplete(int index, object value)
		{
			switch (0)
			{
			default:
			{
				int num3;
				for (;;)
				{
					spr\u252A spr_u252A = (spr\u252A)value;
					spr_u252A.remove_NameChanged(new XlsEventHandler(this.ᜀ));
					this.ᜀ.Remove(spr_u252A.get_Name());
					int count = base.List.Count;
					int num = index;
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
								goto IL_11F;
							}
							if (false)
							{
							}
							if (true)
							{
							}
							this.ᜁ.DecreaseSheetIndex(index);
							num3 = this.ᜁ.ActiveSheetIndex;
							num2 = 4;
							continue;
						case 1:
							goto IL_147;
						case 2:
							if (index == count)
							{
								num2 = 9;
								continue;
							}
							goto IL_1BB;
						case 3:
							goto IL_149;
						case 4:
							if (index >= this.ᜁ.ActiveSheetIndex)
							{
								num2 = 6;
								continue;
							}
							goto IL_12D;
						case 5:
							goto IL_149;
						case 6:
							num2 = 10;
							continue;
						case 7:
							num2 = 2;
							continue;
						case 8:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							spr\u252A spr_u252A2 = this[index];
							spr_u252A2.set_RealIndex(num);
							this.ᜀ[spr_u252A2.get_Name()] = num;
							num++;
							goto IL_11F;
						}
						case 9:
							goto IL_12D;
						case 10:
							if (index == this.ᜁ.ActiveSheetIndex)
							{
								num2 = 7;
								continue;
							}
							goto IL_1BB;
						}
						break;
						IL_11F:
						num2 = 5;
						continue;
						IL_12D:
						num3--;
						this.ᜀ(num3);
						num2 = 1;
						continue;
						IL_149:
						num2 = 8;
					}
				}
				IL_147:
				IL_1BB:
				(this[num3] as ITabSheet).Activate();
				base.OnRemoveComplete(index, value);
				return;
			}
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00023918 File Offset: 0x00022918
		private new void ᜀ(int A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 17;
				int num4;
				for (;;)
				{
					int num3;
					switch (num)
					{
					case 0:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 7;
							continue;
						}
						num = 14;
						continue;
					}
					case 1:
						if ((this[num3] as ITabSheet).Visibility == WorksheetVisibility.Visible)
						{
							num = 11;
							continue;
						}
						num3--;
						num = 16;
						continue;
					case 2:
						goto IL_BC;
					case 3:
						if (num4 == -1)
						{
							num = 10;
							continue;
						}
						goto IL_204;
					case 4:
						if (num3 < 0)
						{
							num = 9;
							continue;
						}
						num = 1;
						continue;
					case 5:
						goto IL_87;
					case 6:
						goto IL_1CB;
					case 7:
						goto IL_19C;
					case 8:
						goto IL_17D;
					case 9:
						goto IL_BC;
					case 10:
					{
						int num2 = A_0 + 1;
						int count = base.Count;
						num = 8;
						continue;
					}
					case 11:
						num4 = num3;
						num = 2;
						continue;
					case 12:
						goto IL_1CD;
					case 13:
						goto IL_17D;
					case 14:
					{
						int num2;
						if ((this[num2] as ITabSheet).Visibility == WorksheetVisibility.Visible)
						{
							num = 15;
							continue;
						}
						if (true)
						{
						}
						num2--;
						num = 13;
						continue;
					}
					case 15:
					{
						int num2;
						num4 = num2;
						num = 6;
						continue;
					}
					case 16:
						goto IL_1CD;
					}
					if ((this[A_0] as ITabSheet).Visibility == WorksheetVisibility.Visible)
					{
						num = 5;
						continue;
					}
					num4 = -1;
					num3 = A_0 - 1;
					num = 12;
					continue;
					IL_BC:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					IL_17D:
					num = 0;
					continue;
					IL_1CD:
					num = 4;
				}
				IL_87:
				this.ᜁ.ActiveSheetIndex = A_0;
				return;
				IL_19C:
				IL_1CB:
				IL_204:
				this.ᜁ.ActiveSheetIndex = num4;
				return;
			}
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00023B38 File Offset: 0x00022B38
		protected override void OnClearComplete()
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
			base.OnClearComplete();
			this.ᜀ.Clear();
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00023B84 File Offset: 0x00022B84
		private new void ᜀ(object A_0, XlsEventArgs A_1)
		{
			int a_ = 9;
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
				string key = (string)A_1.newValue;
				if (!this.ᜀ.ContainsKey(key))
				{
					string key2 = (string)A_1.oldValue;
					int value = this.ᜀ[key2];
					this.ᜀ.Remove(key2);
					this.ᜀ[key] = value;
					return;
				}
				break;
			}
			}
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("焾⁀⹂⁄杆♈ⵊ浌㡎㹐⅒㹔⑖ㅘ㹚㡜⭞䅠๢ၤᑦᵨ䭪ཬ੮兰ٲ᭴Ṷࡸ๺᡼彾ꖄꦈﲊﶎ敖杖떚", a_));
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003E8 RID: 1000 RVA: 0x00023C2C File Offset: 0x00022C2C
		// (remove) Token: 0x060003E9 RID: 1001 RVA: 0x00023CC0 File Offset: 0x00022CC0
		public event TabSheetMovedEventHandler TabSheetMoved
		{
			add
			{
				for (;;)
				{
					TabSheetMovedEventHandler tabSheetMovedEventHandler = this.ᜂ;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_78;
							default:
								if (false)
								{
								}
								goto IL_53;
							}
							break;
						case 2:
							goto IL_78;
						}
						break;
						IL_53:
						TabSheetMovedEventHandler tabSheetMovedEventHandler2 = tabSheetMovedEventHandler;
						TabSheetMovedEventHandler value2 = (TabSheetMovedEventHandler)Delegate.Combine(tabSheetMovedEventHandler2, value);
						tabSheetMovedEventHandler = Interlocked.CompareExchange<TabSheetMovedEventHandler>(ref this.ᜂ, value2, tabSheetMovedEventHandler2);
						num = 2;
						continue;
						IL_78:
						if (tabSheetMovedEventHandler != tabSheetMovedEventHandler2)
						{
							goto IL_53;
						}
						num = 0;
					}
				}
			}
			remove
			{
				for (;;)
				{
					TabSheetMovedEventHandler tabSheetMovedEventHandler = this.ᜂ;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_70;
						case 1:
							goto IL_7C;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_70;
							default:
								if (false)
								{
								}
								goto IL_4B;
							}
							break;
						}
						break;
						IL_4B:
						TabSheetMovedEventHandler tabSheetMovedEventHandler2 = tabSheetMovedEventHandler;
						TabSheetMovedEventHandler value2 = (TabSheetMovedEventHandler)Delegate.Remove(tabSheetMovedEventHandler2, value);
						tabSheetMovedEventHandler = Interlocked.CompareExchange<TabSheetMovedEventHandler>(ref this.ᜂ, value2, tabSheetMovedEventHandler2);
						num = 0;
						continue;
						IL_70:
						if (tabSheetMovedEventHandler != tabSheetMovedEventHandler2)
						{
							goto IL_4B;
						}
						num = 1;
					}
				}
				IL_7C:
				if (true)
				{
				}
			}
		}

		// Token: 0x040000A6 RID: 166
		private new Dictionary<string, int> ᜀ = new Dictionary<string, int>();

		// Token: 0x040000A7 RID: 167
		private new XlsWorkbook ᜁ;

		// Token: 0x040000A8 RID: 168
		private long \u2609\u00A9\u00A6\u0084;

		// Token: 0x040000A9 RID: 169
		private new TabSheetMovedEventHandler ᜂ;
	}
}
