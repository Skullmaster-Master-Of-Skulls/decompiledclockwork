using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls.Core.Spreadsheet.Security;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200002B RID: 43
	public class XlsWorksheetsCollection : CollectionExtended<IWorksheet>, IWorksheets, ICloneParent
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x000194C4 File Offset: 0x000184C4
		internal XlsWorksheetsCollection(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 14;
			this.ᜀ = new Dictionary<string, IWorksheet>(StringComparer.CurrentCultureIgnoreCase);
			this.ᜂ = true;
			base..ctor(A_0, A_1);
			this.ᜁ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜁ == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("݃❅♇浉㡋湍㙏㭑㩓㉕硗⩙㵛ⱝ՟ౡၣ䙥ὧթṫխቯᵱ᭳ᵵ噷", a_));
			}
			this.ᜁ.Objects.TabSheetMoved += this.ᜀ;
		}

		// Token: 0x17000115 RID: 277
		public new IWorksheet this[int Index]
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
				return base.InnerList[Index];
			}
		}

		// Token: 0x17000116 RID: 278
		public IWorksheet this[string sheetName]
		{
			get
			{
				IWorksheet result;
				for (;;)
				{
					IL_00:
					switch (0)
					{
					default:
						for (;;)
						{
							result = null;
							int num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									IWorksheet worksheet;
									result = worksheet;
									num = 5;
									continue;
								}
								case 1:
									goto IL_CE;
								case 2:
									goto IL_CE;
								case 3:
								{
									IWorksheet worksheet;
									StringComparer currentCultureIgnoreCase;
									if (currentCultureIgnoreCase.Compare(worksheet.Name, sheetName) == 0)
									{
										num = 0;
										continue;
									}
									int num2;
									num2++;
									if (true)
									{
									}
									num = 1;
									continue;
								}
								case 4:
								{
									if (this.ᜂ)
									{
										num = 8;
										continue;
									}
									List<IWorksheet> innerList = base.InnerList;
									StringComparer currentCultureIgnoreCase = StringComparer.CurrentCultureIgnoreCase;
									int num2 = 0;
									int count = innerList.Count;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_00;
									default:
										if (false)
										{
										}
										num = 2;
										continue;
									}
									break;
								}
								case 5:
									return result;
								case 6:
									return result;
								case 7:
									return result;
								case 8:
									this.ᜀ.TryGetValue(sheetName, out result);
									num = 6;
									continue;
								case 9:
								{
									int num2;
									int count;
									if (num2 >= count)
									{
										num = 7;
										continue;
									}
									List<IWorksheet> innerList;
									IWorksheet worksheet = innerList[num2];
									num = 3;
									continue;
								}
								}
								break;
								IL_CE:
								num = 9;
							}
						}
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000196E8 File Offset: 0x000186E8
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00019808 File Offset: 0x00018808
		public bool UseRangesCache
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num = 4;
					bool useRangesCache2;
					for (;;)
					{
						int num2;
						int count;
						List<IWorksheet> innerList;
						switch (num)
						{
						case 0:
							goto IL_5E;
						case 1:
							if (num2 >= count)
							{
								num = 2;
								continue;
							}
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
								IWorksheet worksheet = innerList[num2];
								bool useRangesCache = worksheet.UseRangesCache;
								num = 5;
								continue;
							}
							}
							break;
						case 2:
							goto IL_DA;
						case 3:
							return false;
						case 5:
						{
							bool useRangesCache;
							if (useRangesCache != useRangesCache2)
							{
								num = 3;
								continue;
							}
							num2++;
							num = 7;
							continue;
						}
						case 6:
							goto IL_BE;
						case 7:
							goto IL_BE;
						}
						if (base.Count == 0)
						{
							num = 0;
							continue;
						}
						innerList = base.InnerList;
						useRangesCache2 = innerList[0].UseRangesCache;
						num2 = 1;
						count = innerList.Count;
						num = 6;
						continue;
						IL_BE:
						num = 1;
					}
					IL_5E:
					return false;
					IL_DA:
					if (true)
					{
					}
					return useRangesCache2;
				}
				}
			}
			set
			{
				switch (0)
				{
				default:
					for (;;)
					{
						List<IWorksheet> innerList = base.InnerList;
						int num = 0;
						int count = innerList.Count;
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
									goto IL_4B;
								default:
									if (true)
									{
									}
									if (false)
									{
									}
									goto IL_42;
								}
								break;
							case 1:
								return;
							case 2:
								goto IL_42;
							case 3:
								goto IL_4B;
							}
							break;
							IL_42:
							num2 = 3;
							continue;
							IL_4B:
							if (num >= count)
							{
								num2 = 1;
							}
							else
							{
								IWorksheet worksheet = innerList[num];
								worksheet.UseRangesCache = value;
								num++;
								num2 = 0;
							}
						}
					}
					return;
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000198BC File Offset: 0x000188BC
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00019900 File Offset: 0x00018900
		public bool UseHashForWorksheetLookup
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
				return this.ᜂ;
			}
			set
			{
				switch (0)
				{
				default:
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_F1;
						case 1:
							if (value)
							{
								num = 2;
								continue;
							}
							this.ᜀ.Clear();
							num = 0;
							continue;
						case 2:
						{
							List<IWorksheet> innerList = base.InnerList;
							int num2 = 0;
							int count = base.Count;
							num = 8;
							continue;
						}
						case 4:
						{
							int num2;
							int count;
							if (num2 >= count)
							{
								num = 5;
								continue;
							}
							List<IWorksheet> innerList;
							IWorksheet worksheet = innerList[num2];
							this.ᜀ.Add(worksheet.Name, worksheet);
							num2++;
							num = 7;
							continue;
						}
						case 5:
							return;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								this.ᜂ = value;
								num = 1;
								continue;
							}
							break;
						case 7:
							goto IL_111;
						case 8:
							goto IL_111;
						}
						if (this.ᜂ != value)
						{
							num = 6;
							continue;
						}
						break;
						IL_111:
						num = 4;
					}
					IL_F1:
					return;
				}
				}
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00019A44 File Offset: 0x00018A44
		internal new IWorksheet ᜁ(IWorksheet A_0)
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
			this.ᜁ.Objects.ᜀ((spr\u252A)A_0);
			base.Add(A_0);
			return A_0;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00019AA0 File Offset: 0x00018AA0
		protected internal void RemoveLocal(string name)
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
			IWorksheet item = this[name];
			base.Remove(item);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00019AEC File Offset: 0x00018AEC
		public void Move(int oldIndex, int newIndex)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					int count;
					switch (num)
					{
					case 0:
						goto IL_F6;
					case 1:
					{
						if (newIndex >= count)
						{
							num = 0;
							continue;
						}
						this.ᜁ.Objects.Move(oldIndex, newIndex);
						XlsWorksheet xlsWorksheet = this[oldIndex] as XlsWorksheet;
						base.InnerList.RemoveAt(oldIndex);
						base.InnerList.Insert(newIndex, xlsWorksheet);
						int num2 = Math.Min(newIndex, oldIndex);
						int num3 = Math.Max(newIndex, oldIndex);
						int num4 = num2;
						num = 3;
						continue;
					}
					case 2:
						if (oldIndex >= 0)
						{
							num = 9;
							continue;
						}
						goto IL_1AC;
					case 3:
						goto IL_167;
					case 4:
					{
						int num3;
						int num4;
						if (num4 <= num3)
						{
							XlsWorksheet xlsWorksheet = this[num4] as XlsWorksheet;
							xlsWorksheet.Index = num4;
							num4++;
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1DC;
						default:
							if (false)
							{
							}
							num = 13;
							continue;
						}
						break;
					}
					case 5:
						if (newIndex >= 0)
						{
							num = 11;
							continue;
						}
						goto IL_153;
					case 6:
						if (oldIndex >= count)
						{
							num = 7;
							continue;
						}
						num = 5;
						continue;
					case 7:
						goto IL_1DC;
					case 8:
						goto IL_167;
					case 9:
						num = 6;
						continue;
					case 11:
						if (true)
						{
						}
						num = 1;
						continue;
					case 12:
						return;
					case 13:
						return;
					}
					if (oldIndex == newIndex)
					{
						num = 12;
						continue;
					}
					count = base.InnerList.Count;
					num = 2;
					continue;
					IL_167:
					num = 4;
				}
				return;
				IL_F6:
				IL_153:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⭄≆㹈Ɋ⍌⭎㑐⭒", a_));
				IL_1AC:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⩄⭆ⵈɊ⍌⭎㑐⭒", a_));
				IL_1DC:
				goto IL_1AC;
			}
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00019CFC File Offset: 0x00018CFC
		public void UpdateSheetIndex(XlsWorksheet sheet, int iOldRealIndex)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int realIndex;
					int num2;
					int num3;
					int num4;
					ITabSheet tabSheet;
					switch (num)
					{
					case 0:
						if (iOldRealIndex > realIndex)
						{
							num = 15;
							continue;
						}
						num = 2;
						continue;
					case 1:
						goto IL_80;
					case 2:
						if (iOldRealIndex < realIndex)
						{
							num = 3;
							continue;
						}
						goto IL_189;
					case 3:
						num2 = realIndex - 1;
						num3 = -1;
						num = 9;
						continue;
					case 5:
						goto IL_184;
					case 6:
						goto IL_A2;
					case 7:
						num4 += num3;
						num = 12;
						continue;
					case 8:
						if (tabSheet is XlsWorksheet)
						{
							num = 14;
							continue;
						}
						num = 13;
						continue;
					case 9:
						goto IL_A2;
					case 10:
						goto IL_E9;
					case 11:
					{
						ITabSheet tabSheet2;
						XlsWorksheet xlsWorksheet = (XlsWorksheet)tabSheet2;
						int index = sheet.Index;
						int index2 = xlsWorksheet.Index;
						this.ᜀ(index, index2);
						num = 5;
						continue;
					}
					case 12:
						if (true)
						{
						}
						goto IL_1E1;
					case 13:
						if (num4 != iOldRealIndex)
						{
							num = 7;
							continue;
						}
						goto IL_E9;
					case 14:
					{
						ITabSheet tabSheet2 = tabSheet;
						num = 10;
						continue;
					}
					case 15:
						num2 = realIndex + 1;
						num3 = 1;
						num = 6;
						continue;
					case 16:
					{
						ITabSheet tabSheet2;
						if (tabSheet2 != null)
						{
							num = 11;
							continue;
						}
						return;
					}
					case 17:
						goto IL_1E1;
					}
					if (sheet == null)
					{
						num = 1;
						continue;
					}
					realIndex = sheet.RealIndex;
					num3 = 0;
					num2 = -1;
					ITabSheets tabSheets = this.ᜁ.TabSheets;
					num = 0;
					continue;
					IL_A2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
					{
						if (false)
						{
						}
						ITabSheet tabSheet2 = null;
						num4 = num2;
						num = 17;
						continue;
					}
					}
					IL_E9:
					num = 16;
					continue;
					IL_1E1:
					tabSheet = tabSheets[num4];
					num = 8;
				}
				IL_80:
				throw new ArgumentNullException(RecordTableEnumerator.b("㉀⭂⁄≆㵈", a_));
				IL_184:
				return;
				IL_189:
				throw new NotImplementedException(RecordTableEnumerator.b("ᙀⱂ㝄ⱆ㩈⍊⡌⩎═獒≔㙖⩘㕚穜⭞䅠๢੤ᅦ౨ཪ䵬๮հ卲ᑴ᭶ᕸ", a_));
			}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00019F48 File Offset: 0x00018F48
		private new void ᜀ(int A_0, int A_1)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int count;
					switch (num)
					{
					case 0:
						if (A_0 >= count)
						{
							num = 11;
							continue;
						}
						num = 3;
						continue;
					case 1:
						return;
					case 2:
						goto IL_E1;
					case 3:
						if (A_1 >= 0)
						{
							num = 8;
							continue;
						}
						goto IL_13E;
					case 4:
						goto IL_152;
					case 5:
						num = 0;
						continue;
					case 6:
						if (true)
						{
						}
						break;
					case 7:
						if (A_0 >= 0)
						{
							num = 5;
							continue;
						}
						goto IL_197;
					case 8:
						num = 9;
						continue;
					case 9:
					{
						if (A_1 >= count)
						{
							num = 2;
							continue;
						}
						XlsWorksheet xlsWorksheet = this[A_0] as XlsWorksheet;
						base.InnerList.RemoveAt(A_0);
						base.InnerList.Insert(A_1, xlsWorksheet);
						int num2 = Math.Min(A_1, A_0);
						int num3 = Math.Max(A_1, A_0);
						int num4 = num2;
						num = 10;
						continue;
					}
					case 10:
						goto IL_152;
					case 11:
						goto IL_1C7;
					case 12:
					{
						int num3;
						int num4;
						if (num4 <= num3)
						{
							XlsWorksheet xlsWorksheet = this[num4] as XlsWorksheet;
							xlsWorksheet.Index = num4;
							num4++;
							num = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1C7;
						default:
							if (false)
							{
							}
							num = 13;
							continue;
						}
						break;
					}
					case 13:
						return;
					}
					if (A_0 == A_1)
					{
						num = 1;
						continue;
					}
					count = base.InnerList.Count;
					num = 7;
					continue;
					IL_152:
					num = 12;
				}
				return;
				IL_E1:
				IL_13E:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅ه⽉㭋ݍ㹏㙑ㅓ⹕", a_));
				IL_197:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅݇♉⡋ݍ㹏㙑ㅓ⹕", a_));
				IL_1C7:
				goto IL_197;
			}
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0001A144 File Offset: 0x00019144
		protected internal IXLSRange FindFirst(string findValue, FindType flags)
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
			return this.FindFirst(findValue, flags, ExcelFindOptions.None);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001A188 File Offset: 0x00019188
		public IXLSRange FindFirst(string findValue, FindType flags, ExcelFindOptions findOptions)
		{
			int a_ = 4;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					bool flag;
					int num2;
					int count;
					IList<IWorksheet> innerList;
					bool flag2;
					bool flag3;
					bool flag4;
					switch (num)
					{
					case 0:
						goto IL_16B;
					case 1:
						if (flag)
						{
							goto IL_1AD;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17F;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 2:
					{
						IXLSRange ixlsrange;
						if (ixlsrange != null)
						{
							num = 15;
							continue;
						}
						num2++;
						num = 4;
						continue;
					}
					case 3:
						goto IL_75;
					case 4:
						goto IL_14A;
					case 5:
						num = 9;
						continue;
					case 6:
						num = 13;
						continue;
					case 7:
						num = 12;
						continue;
					case 8:
					{
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						IXLSRange ixlsrange = (innerList[num2] as XlsWorksheet).ᜀ(findValue, flags, findOptions);
						num = 2;
						continue;
					}
					case 9:
						if (!flag2)
						{
							num = 6;
							continue;
						}
						goto IL_1AD;
					case 11:
						goto IL_18B;
					case 12:
						if (!flag3)
						{
							goto IL_17F;
						}
						goto IL_1AD;
					case 13:
						if (!flag4)
						{
							num = 7;
							continue;
						}
						goto IL_1AD;
					case 14:
						goto IL_14A;
					case 15:
					{
						IXLSRange ixlsrange;
						return ixlsrange;
					}
					}
					if (findValue == null)
					{
						num = 3;
						continue;
					}
					flag = ((flags & FindType.Formula) == FindType.Formula);
					flag2 = ((flags & FindType.Text) == FindType.Text);
					flag4 = ((flags & FindType.FormulaStringValue) == FindType.FormulaStringValue);
					flag3 = ((flags & FindType.Error) == FindType.Error);
					num = 1;
					continue;
					IL_14A:
					num = 8;
					continue;
					IL_17F:
					num = 11;
					continue;
					IL_1AD:
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 14;
				}
				IL_75:
				return null;
				IL_16B:
				return null;
				IL_18B:
				if (true)
				{
				}
				throw new ArgumentException(RecordTableEnumerator.b("樹崻䰽ℿ⽁⅃㉅ⵇ㡉汋❍⍏牑㩓㥕ⱗ穙⩛㽝౟ୡc䡥", a_));
			}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0001A388 File Offset: 0x00019388
		protected internal IXLSRange FindFirst(double findValue, FindType flags)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				IXLSRange ixlsrange;
				for (;;)
				{
					bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
					bool flag2 = (flags & FindType.Number) == FindType.Number;
					int num = 7;
					for (;;)
					{
						int num2;
						int count;
						IList<IWorksheet> innerList;
						switch (num)
						{
						case 0:
							goto IL_105;
						case 1:
							return ixlsrange;
						case 2:
							if (ixlsrange == null)
							{
								num2++;
								num = 5;
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
								num = 1;
								continue;
							}
							break;
						case 3:
							num = 8;
							continue;
						case 4:
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							ixlsrange = ((XlsWorksheet)innerList[num2]).FindOne(findValue, flags);
							if (true)
							{
							}
							num = 2;
							continue;
						case 5:
							goto IL_E8;
						case 6:
							goto IL_E8;
						case 7:
							if (!flag)
							{
								num = 3;
								continue;
							}
							goto IL_140;
						case 8:
							if (!flag2)
							{
								num = 9;
								continue;
							}
							goto IL_140;
						case 9:
							goto IL_122;
						}
						break;
						IL_E8:
						num = 4;
						continue;
						IL_140:
						innerList = base.InnerList;
						num2 = 0;
						count = innerList.Count;
						num = 6;
					}
				}
				return ixlsrange;
				IL_105:
				return null;
				IL_122:
				throw new ArgumentException(RecordTableEnumerator.b("昵夷䠹崻匽┿㙁⅃㑅桇⍉㽋湍㹏㵑⁓癕⹗㭙せ㝝џ䱡", a_));
			}
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0001A4F8 File Offset: 0x000194F8
		protected internal IXLSRange FindFirst(bool findValue)
		{
			switch (0)
			{
			default:
			{
				IXLSRange ixlsrange;
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_AF;
						case 1:
							goto IL_CB;
						case 2:
							if (ixlsrange != null)
							{
								num2 = 3;
								continue;
							}
							for (;;)
							{
								num++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_64;
								}
							}
							IL_64:
							if (false)
							{
							}
							if (true)
							{
							}
							num2 = 0;
							continue;
						case 3:
							return ixlsrange;
						case 4:
							goto IL_AF;
						case 5:
							if (num >= count)
							{
								num2 = 1;
								continue;
							}
							ixlsrange = ((XlsWorksheet)innerList[num]).FindOne(findValue);
							num2 = 2;
							continue;
						}
						break;
						IL_AF:
						num2 = 5;
					}
				}
				return ixlsrange;
				IL_CB:
				return null;
			}
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001A5E0 File Offset: 0x000195E0
		protected internal IXLSRange FindFirst(DateTime findValue)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				IXLSRange ixlsrange;
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_AF;
						case 1:
							goto IL_AF;
						case 2:
							if (num >= count)
							{
								num2 = 5;
								continue;
							}
							ixlsrange = ((XlsWorksheet)innerList[num]).FindOne(findValue);
							num2 = 3;
							continue;
						case 3:
							if (ixlsrange != null)
							{
								num2 = 4;
								continue;
							}
							for (;;)
							{
								num++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_6C;
								}
							}
							IL_6C:
							if (false)
							{
							}
							num2 = 1;
							continue;
						case 4:
							return ixlsrange;
						case 5:
							goto IL_CB;
						}
						break;
						IL_AF:
						num2 = 2;
					}
				}
				return ixlsrange;
				IL_CB:
				return null;
			}
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0001A6C8 File Offset: 0x000196C8
		protected internal IXLSRange FindFirst(TimeSpan findValue)
		{
			switch (0)
			{
			default:
			{
				IXLSRange ixlsrange;
				for (;;)
				{
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_A7;
						case 1:
							return ixlsrange;
						case 2:
							goto IL_A7;
						case 3:
							if (num >= count)
							{
								num2 = 4;
								continue;
							}
							ixlsrange = ((XlsWorksheet)innerList[num]).FindOne(findValue);
							num2 = 5;
							continue;
						case 4:
							goto IL_CB;
						case 5:
							if (ixlsrange != null)
							{
								num2 = 1;
								continue;
							}
							for (;;)
							{
								num++;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_64;
								}
							}
							IL_64:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
						IL_A7:
						if (true)
						{
						}
						num2 = 3;
					}
				}
				return ixlsrange;
				IL_CB:
				return null;
			}
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0001A7B0 File Offset: 0x000197B0
		protected internal CellRange[] FindAll(string findValue, FindType flags)
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
			return this.FindAll(findValue, flags, ExcelFindOptions.None);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001A7F4 File Offset: 0x000197F4
		public CellRange[] FindAll(string findValue, FindType flags, ExcelFindOptions findOptions)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				int num = 16;
				List<CellRange> list;
				for (;;)
				{
					int num2;
					int count;
					IList<IWorksheet> innerList;
					bool flag;
					bool flag2;
					bool flag3;
					bool flag4;
					switch (num)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num = 14;
							continue;
						}
						IWorksheet worksheet = innerList[num2];
						CellRange[] array = ((XlsWorksheet)worksheet).ᜁ(findValue, flags, findOptions);
						num = 7;
						continue;
					}
					case 1:
						if (!flag)
						{
							num = 15;
							continue;
						}
						goto IL_1BE;
					case 2:
						if (!flag2)
						{
							num = 3;
							continue;
						}
						goto IL_1BE;
					case 3:
						num = 5;
						continue;
					case 4:
						goto IL_15B;
					case 5:
						if (!flag3)
						{
							num = 6;
							continue;
						}
						goto IL_1BE;
					case 6:
						num = 1;
						continue;
					case 7:
					{
						CellRange[] array;
						if (array != null)
						{
							num = 10;
							continue;
						}
						goto IL_124;
					}
					case 8:
						goto IL_122;
					case 9:
						goto IL_15B;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_122;
						default:
						{
							if (false)
							{
							}
							CellRange[] array;
							list.AddRange(array);
							num = 13;
							continue;
						}
						}
						break;
					case 11:
						goto IL_79;
					case 12:
						if (!flag4)
						{
							num = 8;
							continue;
						}
						goto IL_1BE;
					case 13:
						goto IL_124;
					case 14:
						goto IL_17C;
					case 15:
						goto IL_19C;
					}
					if (findValue == null)
					{
						num = 11;
						continue;
					}
					flag4 = ((flags & FindType.Formula) == FindType.Formula);
					flag2 = ((flags & FindType.Text) == FindType.Text);
					flag3 = ((flags & FindType.FormulaStringValue) == FindType.FormulaStringValue);
					flag = ((flags & FindType.Error) == FindType.Error);
					num = 12;
					continue;
					IL_122:
					num = 2;
					continue;
					IL_124:
					num2++;
					num = 9;
					continue;
					IL_15B:
					num = 0;
					continue;
					IL_1BE:
					list = new List<CellRange>();
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 4;
				}
				IL_79:
				return null;
				IL_17C:
				if (true)
				{
				}
				return list.ToArray();
				IL_19C:
				throw new ArgumentException(RecordTableEnumerator.b("样嬹主弽ⴿ❁ぃ⍅㩇橉╋㵍灏㱑㭓≕硗ⱙ㵛㉝य़١䩣", a_));
			}
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001AA1C File Offset: 0x00019A1C
		protected internal CellRange[] FindAll(double findValue, FindType flags)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					bool flag = (flags & FindType.FormulaValue) == FindType.FormulaValue;
					bool flag2 = (flags & FindType.Number) == FindType.Number;
					int num = 11;
					for (;;)
					{
						int num2;
						int count;
						IList<IWorksheet> innerList;
						switch (num)
						{
						case 0:
							goto IL_131;
						case 1:
							goto IL_8B;
						case 2:
						{
							CellRange[] array;
							list.AddRange(array);
							num = 8;
							continue;
						}
						case 3:
							num = 4;
							continue;
						case 4:
							goto IL_122;
						case 5:
							goto IL_114;
						case 6:
						{
							if (num2 >= count)
							{
								num = 9;
								continue;
							}
							IWorksheet worksheet = innerList[num2];
							CellRange[] array = ((XlsWorksheet)worksheet).FindAll(findValue, flags);
							num = 12;
							continue;
						}
						case 7:
							goto IL_8B;
						case 8:
							goto IL_133;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_122;
							default:
								if (false)
								{
								}
								num = 10;
								continue;
							}
							break;
						case 10:
							if (list.Count == 0)
							{
								num = 5;
								continue;
							}
							goto IL_1BA;
						case 11:
							if (true)
							{
							}
							if (!flag)
							{
								num = 3;
								continue;
							}
							goto IL_191;
						case 12:
						{
							CellRange[] array;
							if (array != null)
							{
								num = 2;
								continue;
							}
							goto IL_133;
						}
						}
						break;
						IL_8B:
						num = 6;
						continue;
						IL_122:
						if (!flag2)
						{
							num = 0;
							continue;
						}
						goto IL_191;
						IL_133:
						num2++;
						num = 7;
						continue;
						IL_191:
						list = new List<CellRange>();
						innerList = base.InnerList;
						num2 = 0;
						count = innerList.Count;
						num = 1;
					}
				}
				IL_114:
				return null;
				IL_131:
				throw new ArgumentException(RecordTableEnumerator.b("᥈⩊㽌⹎㱐㙒⅔㉖⭘筚㑜ⱞ䅠ൢ੤፦䥨ᵪ౬ͮᡰᝲ孴", a_));
				IL_1BA:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001ABEC File Offset: 0x00019BEC
		protected internal CellRange[] FindAll(bool findValue)
		{
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					list = new List<CellRange>();
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_114;
						case 1:
							goto IL_114;
						case 2:
							goto IL_7E;
						case 3:
						{
							CellRange[] array;
							if (array != null)
							{
								goto IL_BD;
							}
							goto IL_7E;
						}
						case 4:
							num2 = 6;
							continue;
						case 5:
						{
							CellRange[] array;
							list.AddRange(array);
							num2 = 2;
							continue;
						}
						case 6:
							if (list.Count == 0)
							{
								num2 = 7;
								continue;
							}
							goto IL_138;
						case 7:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_BD;
							default:
								goto IL_F3;
							}
							break;
						case 8:
						{
							if (num >= count)
							{
								num2 = 4;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							CellRange[] array = ((XlsWorksheet)worksheet).FindAll(findValue).ToArray();
							num2 = 3;
							continue;
						}
						}
						break;
						IL_7E:
						num++;
						num2 = 1;
						continue;
						IL_BD:
						num2 = 5;
						continue;
						IL_114:
						num2 = 8;
					}
				}
				IL_F3:
				if (false)
				{
				}
				return null;
				IL_138:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001AD38 File Offset: 0x00019D38
		protected internal CellRange[] FindAll(DateTime findValue)
		{
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					list = new List<CellRange>();
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 6;
							continue;
						case 1:
						{
							CellRange[] array;
							if (array != null)
							{
								goto IL_B8;
							}
							goto IL_7E;
						}
						case 2:
						{
							CellRange[] array;
							list.AddRange(array);
							num2 = 3;
							continue;
						}
						case 3:
							if (true)
							{
							}
							goto IL_7E;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_B8;
							default:
								goto IL_E6;
							}
							break;
						case 5:
							goto IL_10F;
						case 6:
							if (list.Count == 0)
							{
								num2 = 4;
								continue;
							}
							goto IL_133;
						case 7:
							goto IL_10F;
						case 8:
						{
							if (num >= count)
							{
								num2 = 0;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							CellRange[] array = ((XlsWorksheet)worksheet).FindAll(findValue);
							num2 = 1;
							continue;
						}
						}
						break;
						IL_7E:
						num++;
						num2 = 7;
						continue;
						IL_B8:
						num2 = 2;
						continue;
						IL_10F:
						num2 = 8;
					}
				}
				IL_E6:
				if (false)
				{
				}
				return null;
				IL_133:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0001AE80 File Offset: 0x00019E80
		protected internal CellRange[] FindAll(TimeSpan findValue)
		{
			switch (0)
			{
			default:
			{
				List<CellRange> list;
				for (;;)
				{
					list = new List<CellRange>();
					IList<IWorksheet> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 4;
					for (;;)
					{
						if (true)
						{
						}
						switch (num2)
						{
						case 0:
						{
							CellRange[] array;
							if (array != null)
							{
								goto IL_C0;
							}
							goto IL_86;
						}
						case 1:
							goto IL_10F;
						case 2:
							if (list.Count == 0)
							{
								num2 = 5;
								continue;
							}
							goto IL_133;
						case 3:
						{
							if (num >= count)
							{
								num2 = 6;
								continue;
							}
							IWorksheet worksheet = innerList[num];
							CellRange[] array = ((XlsWorksheet)worksheet).FindAll(findValue);
							num2 = 0;
							continue;
						}
						case 4:
							goto IL_10F;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C0;
							default:
								goto IL_EE;
							}
							break;
						case 6:
							num2 = 2;
							continue;
						case 7:
							goto IL_86;
						case 8:
						{
							CellRange[] array;
							list.AddRange(array);
							num2 = 7;
							continue;
						}
						}
						break;
						IL_86:
						num++;
						num2 = 1;
						continue;
						IL_C0:
						num2 = 8;
						continue;
						IL_10F:
						num2 = 3;
					}
				}
				IL_EE:
				if (false)
				{
				}
				return null;
				IL_133:
				return list.ToArray();
			}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001AFC8 File Offset: 0x00019FC8
		internal new IWorksheet ᜀ(sprἛ A_0, ExcelParseOptions A_1, bool A_2, Dictionary<int, int> A_3, IDecryptor A_4)
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
			IWorksheet a_ = base.AppImplementation.ᜀ(this, A_0, A_1, A_2, A_3, A_4);
			return this.ᜁ(a_);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001B020 File Offset: 0x0001A020
		public IWorksheet Add(string sheetName)
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
			IWorksheet worksheet = base.AppImplementation.ᜁ(this);
			((XlsWorksheet)worksheet).RealIndex = this.ᜁ.ObjectCount;
			worksheet.Name = sheetName;
			return this.ᜁ(worksheet);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001B08C File Offset: 0x0001A08C
		public IWorksheet AddCopy(int sheetIndex)
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
			return this.AddCopy(sheetIndex, WorksheetCopyType.CopyAll);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001B0D4 File Offset: 0x0001A0D4
		public IWorksheet AddCopy(int sheetIndex, WorksheetCopyType flags)
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
			return this.ᜀ(this[sheetIndex], flags, true);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001B120 File Offset: 0x0001A120
		public IWorksheet AddCopy(IWorksheet sheet)
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
			return this.AddCopy(sheet, WorksheetCopyType.CopyAll);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001B168 File Offset: 0x0001A168
		public IWorksheet AddCopy(IWorksheet sheet, WorksheetCopyType flags)
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
			return this.ᜀ(sheet, flags, false);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001B1AC File Offset: 0x0001A1AC
		private new IWorksheet ᜀ(IWorksheet A_0, WorksheetCopyType A_1, bool A_2)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				XlsWorksheet xlsWorksheet;
				XlsWorksheet xlsWorksheet2;
				Dictionary<string, string> dictionary;
				for (;;)
				{
					xlsWorksheet = (XlsWorksheet)A_0;
					WorksheetVisibility visibility = xlsWorksheet.Visibility;
					int num = 14;
					for (;;)
					{
						XlsWorkbook xlsWorkbook;
						ExcelVersion version2;
						ExcelVersion version3;
						switch (num)
						{
						case 0:
						{
							ExcelVersion version;
							if (version != this.ᜁ.Version)
							{
								num = 18;
								continue;
							}
							goto IL_205;
						}
						case 1:
						{
							ExcelVersion version;
							if (version > this.ᜁ.Version)
							{
								num = 5;
								continue;
							}
							goto IL_205;
						}
						case 2:
							goto IL_200;
						case 3:
							if ((A_1 & WorksheetCopyType.CopyPalette) != WorksheetCopyType.None)
							{
								num = 7;
								continue;
							}
							goto IL_2B9;
						case 4:
						{
							if (A_0.Workbook.Worksheets == this)
							{
								num = 8;
								continue;
							}
							ExcelVersion version = A_0.Workbook.Version;
							num = 0;
							continue;
						}
						case 5:
							goto IL_29B;
						case 6:
							num = 4;
							continue;
						case 7:
							xlsWorkbook.ᜀ(this.ᜁ);
							num = 12;
							continue;
						case 8:
							goto IL_367;
						case 9:
							xlsWorksheet2.Version = version2;
							num = 17;
							continue;
						case 10:
							this.ᜀ(xlsWorksheet, A_1);
							num = 11;
							continue;
						case 11:
							goto IL_A8;
						case 12:
							goto IL_2B9;
						case 13:
							if (A_1 == WorksheetCopyType.CopyShapes)
							{
								num = 10;
								continue;
							}
							goto IL_A8;
						case 14:
							if (!A_2)
							{
								num = 6;
								continue;
							}
							goto IL_16B;
						case 15:
							if (visibility != xlsWorksheet2.Visibility)
							{
								num = 19;
								continue;
							}
							return xlsWorksheet2;
						case 16:
							if (version3 != version2)
							{
								num = 9;
								continue;
							}
							goto IL_142;
						case 17:
							goto IL_142;
						case 18:
							goto IL_271;
						case 19:
							xlsWorksheet2.Visibility = visibility;
							num = 2;
							continue;
						}
						break;
						IL_A8:
						if (true)
						{
						}
						version3 = xlsWorksheet2.Version;
						version2 = this.ᜁ.Version;
						num = 16;
						continue;
						IL_142:
						num = 15;
						continue;
						IL_205:
						Dictionary<int, int> dicFontIndexes;
						Dictionary<int, int> hashExtFormatIndexes;
						Dictionary<string, string> hashStyleNames;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_271:
							num = 1;
							continue;
						default:
							if (false)
							{
							}
							hashStyleNames = this.ᜁ.InnerStyles.ᜀ(A_0.Workbook, StyleMergeType.CreateDiffName, out dicFontIndexes, out hashExtFormatIndexes);
							xlsWorkbook = (XlsWorkbook)xlsWorksheet.Workbook;
							num = 3;
							continue;
						}
						IL_2B9:
						dictionary = new Dictionary<string, string>();
						Dictionary<int, int> hashNameIndexes = new Dictionary<int, int>();
						Dictionary<int, int> a_2 = this.ᜁ.ExternWorkbooks.ᜀ(xlsWorkbook.ExternWorkbooks);
						Dictionary<int, int> hashExternSheets = this.ᜁ.ᜀ(xlsWorkbook.ExternSheet, a_2);
						xlsWorksheet2 = this.ᜀ(A_0.Name, dictionary);
						xlsWorksheet2.CopyFrom(xlsWorksheet, hashStyleNames, dictionary, dicFontIndexes, A_1, hashExtFormatIndexes, hashNameIndexes, hashExternSheets);
						num = 13;
					}
				}
				IL_16B:
				xlsWorksheet2 = base.AppImplementation.ᜁ(this);
				dictionary = new Dictionary<string, string>(1);
				xlsWorksheet2.Name = CollectionExtended<IWorksheet>.GenerateDefaultName(base.List, xlsWorksheet.Name + RecordTableEnumerator.b("愽", a_));
				dictionary.Add(xlsWorksheet.Name, xlsWorksheet2.Name);
				this.ᜁ(xlsWorksheet2);
				xlsWorksheet2.ᜀ(xlsWorksheet, new Dictionary<string, string>(), dictionary, null, A_1);
				return xlsWorksheet2;
				IL_200:
				return xlsWorksheet2;
				IL_29B:
				throw new InvalidOperationException();
				IL_367:
				goto IL_16B;
			}
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001B52C File Offset: 0x0001A52C
		private new void ᜀ(XlsWorksheet A_0, WorksheetCopyType A_1)
		{
			int num = 3;
			Stream stream;
			Stream stream2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (stream != null)
					{
						num = 6;
						continue;
					}
					goto IL_104;
				case 1:
					goto IL_8D;
				case 2:
					if (stream2 != null)
					{
						num = 1;
						continue;
					}
					this.ᜁ.ControlsStream = stream;
					num = 5;
					continue;
				case 3:
					if (true)
					{
					}
					break;
				case 4:
				{
					XlsWorkbook xlsWorkbook;
					stream = xlsWorkbook.ControlsStream;
					goto IL_E6;
				}
				case 5:
					goto IL_104;
				case 6:
					stream2 = this.ᜁ.ControlsStream;
					num = 2;
					continue;
				case 7:
					if (this.ᜀ(A_0))
					{
						num = 4;
						continue;
					}
					goto IL_104;
				case 8:
				{
					XlsWorkbook xlsWorkbook = A_0.Workbook as XlsWorkbook;
					num = 7;
					continue;
				}
				}
				if ((A_1 & WorksheetCopyType.CopyShapes) != WorksheetCopyType.None)
				{
					num = 8;
					continue;
				}
				goto IL_104;
				IL_E6:
				num = 0;
				continue;
				IL_104:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E6;
				default:
					goto IL_11A;
				}
			}
			IL_8D:
			stream.Position = 0L;
			stream2.Position = stream2.Length;
			UtilityMethods.ᜀ(stream, stream2);
			return;
			IL_11A:
			if (false)
			{
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0001B65C File Offset: 0x0001A65C
		private new bool ᜀ(IWorksheet A_0)
		{
			bool result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				switch (0)
				{
				default:
				{
					spr\u1D9B spr_u1D9B = (A_0 as XlsWorksheet).Shapes as spr\u1D9B;
					IEnumerator<IShape> enumerator = spr_u1D9B.GetEnumerator();
					try
					{
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_D7;
							case 1:
								goto IL_E2;
							case 2:
							{
								if (!enumerator.MoveNext())
								{
									num = 5;
									continue;
								}
								XlsShape xlsShape = (XlsShape)enumerator.Current;
								num = 4;
								continue;
							}
							case 3:
								result = true;
								num = 0;
								continue;
							case 4:
							{
								XlsShape xlsShape;
								if (xlsShape.IsActiveX)
								{
									num = 3;
									continue;
								}
								break;
							}
							case 5:
								num = 1;
								continue;
							}
							IL_88:
							num = 2;
							continue;
							goto IL_88;
						}
						IL_D7:
						break;
						IL_E2:
						goto IL_4E;
					}
					finally
					{
						int num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								enumerator.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_122;
							}
							if (enumerator == null)
							{
								break;
							}
							num = 0;
						}
						IL_122:;
					}
					break;
					IL_4E:
					if (true)
					{
					}
					return false;
				}
				}
				break;
			}
			return result;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0001B7A0 File Offset: 0x0001A7A0
		public void AddCopy(IWorksheets worksheets)
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
			this.AddCopy(worksheets, WorksheetCopyType.CopyAll);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0001B7E8 File Offset: 0x0001A7E8
		public void AddCopy(IWorksheets worksheets, WorksheetCopyType flags)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
				{
					int count;
					int num4;
					XlsWorkbook xlsWorkbook;
					switch (num)
					{
					case 0:
						goto IL_183;
					case 1:
					{
						int num2;
						if (num2 >= count)
						{
							goto IL_324;
						}
						XlsWorksheet[] array;
						Dictionary<string, string> dictionary;
						array[num2] = this.ᜀ(worksheets[num2].Name, dictionary);
						num2++;
						num = 3;
						continue;
					}
					case 2:
						goto IL_12E;
					case 3:
						goto IL_310;
					case 4:
						goto IL_310;
					case 5:
					{
						int num3 = 0;
						num = 15;
						continue;
					}
					case 6:
						goto IL_24D;
					case 7:
						flags &= ~WorksheetCopyType.CopyNames;
						num = 2;
						continue;
					case 9:
						goto IL_94;
					case 10:
						goto IL_24D;
					case 11:
						goto IL_215;
					case 12:
					{
						if (num4 >= count)
						{
							num = 14;
							continue;
						}
						if (true)
						{
						}
						XlsWorksheet[] array;
						Dictionary<string, string> dictionary;
						Dictionary<string, string> dictionary2;
						Dictionary<int, int> dictionary3;
						Dictionary<int, int> dictionary4;
						Dictionary<int, int> dictionary5;
						array[num4].ᜀ((XlsWorksheet)worksheets[num4], dictionary2, dictionary, dictionary3, flags, dictionary4, dictionary5);
						num4++;
						num = 10;
						continue;
					}
					case 13:
						num = 20;
						continue;
					case 14:
						return;
					case 15:
						goto IL_183;
					case 16:
					{
						if (worksheets == this)
						{
							num = 17;
							continue;
						}
						XlsWorksheet[] array = new XlsWorksheet[count];
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						Dictionary<int, int> dictionary5 = new Dictionary<int, int>();
						Dictionary<int, int> a_2 = this.ᜁ.ExternWorkbooks.ᜀ(xlsWorkbook.ExternWorkbooks);
						Dictionary<int, int> hashExternSheets = this.ᜁ.ᜀ(xlsWorkbook.ExternSheet, a_2);
						Dictionary<int, int> dictionary3;
						Dictionary<int, int> dictionary4;
						Dictionary<string, string> dictionary2 = this.ᜁ.InnerStyles.ᜀ(worksheets[0].Workbook, StyleMergeType.CreateDiffName, out dictionary3, out dictionary4);
						int num2 = 0;
						num = 4;
						continue;
					}
					case 17:
					{
						int num5 = 0;
						num = 11;
						continue;
					}
					case 18:
						goto IL_215;
					case 19:
					{
						int num3;
						if (num3 >= count)
						{
							num = 7;
							continue;
						}
						XlsWorksheet[] array;
						Dictionary<string, string> dictionary;
						Dictionary<string, string> dictionary2;
						Dictionary<int, int> dictionary3;
						Dictionary<int, int> dictionary4;
						Dictionary<int, int> dictionary5;
						Dictionary<int, int> hashExternSheets;
						array[num3].CopyFrom((XlsWorksheet)worksheets[num3], dictionary2, dictionary, dictionary3, WorksheetCopyType.CopyNames, dictionary4, dictionary5, hashExternSheets);
						num3++;
						num = 0;
						continue;
					}
					case 20:
						if ((flags & WorksheetCopyType.CopyNames) != WorksheetCopyType.None)
						{
							num = 5;
							continue;
						}
						goto IL_12E;
					case 21:
						return;
					case 22:
					{
						int num5;
						if (num5 >= count)
						{
							num = 21;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_324;
						default:
							if (false)
							{
							}
							this.AddCopy(num5);
							num5++;
							num = 18;
							continue;
						}
						break;
					}
					}
					if (worksheets == null)
					{
						num = 9;
						continue;
					}
					count = worksheets.Count;
					xlsWorkbook = ((XlsWorksheetsCollection)worksheets).ᜁ;
					num = 16;
					continue;
					IL_12E:
					num4 = 0;
					num = 6;
					continue;
					IL_183:
					num = 19;
					continue;
					IL_215:
					num = 22;
					continue;
					IL_24D:
					num = 12;
					continue;
					IL_310:
					num = 1;
					continue;
					IL_324:
					num = 13;
				}
				IL_94:
				throw new ArgumentNullException(RecordTableEnumerator.b("う♈㥊♌㱎㥐㙒ご⍖⩘", a_));
			}
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0001BB40 File Offset: 0x0001AB40
		private new XlsWorksheet ᜀ(string A_0, Dictionary<string, string> A_1)
		{
			int a_ = 6;
			XlsWorksheet xlsWorksheet;
			string text;
			for (;;)
			{
				xlsWorksheet = base.AppImplementation.ᜁ(this);
				text = A_0;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_86;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_86;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							A_1 = new Dictionary<string, string>();
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_56;
					case 3:
						if (A_1 == null)
						{
							num = 1;
							continue;
						}
						goto IL_56;
					case 4:
						if (this[A_0] != null)
						{
							num = 5;
							continue;
						}
						goto IL_E8;
					case 5:
						num = 3;
						continue;
					}
					break;
					IL_56:
					text = CollectionExtended<IWorksheet>.GenerateDefaultName(base.List, text + RecordTableEnumerator.b("挻", a_));
					A_1.Add(A_0, text);
					num = 0;
				}
			}
			IL_86:
			IL_E8:
			xlsWorksheet.Name = text;
			this.ᜁ(xlsWorksheet);
			return xlsWorksheet;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0001BC48 File Offset: 0x0001AC48
		public IWorksheet AddCopyBefore(IWorksheet toCopy)
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
			return this.AddCopyBefore(toCopy, toCopy);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001BC8C File Offset: 0x0001AC8C
		public IWorksheet AddCopyBefore(IWorksheet toCopy, IWorksheet sheetAfter)
		{
			int a_ = 3;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					goto IL_34;
				case 2:
					if (sheetAfter == null)
					{
						num = 0;
						continue;
					}
					goto IL_A1;
				}
				if (toCopy == null)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴸吺縼倾ㅀ㩂", a_));
			IL_8B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_34;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀ɂ⍄㍆ⱈ㥊", a_));
			}
			IL_A1:
			int index = sheetAfter.Index;
			XlsWorksheet xlsWorksheet = (XlsWorksheet)this.AddCopy(toCopy);
			xlsWorksheet.MoveWorksheet(index);
			xlsWorksheet.Activate();
			return xlsWorksheet;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001BD5C File Offset: 0x0001AD5C
		public IWorksheet AddCopyAfter(IWorksheet toCopy)
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
			return this.AddCopyAfter(toCopy, toCopy);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001BDA0 File Offset: 0x0001ADA0
		public IWorksheet AddCopyAfter(IWorksheet toCopy, IWorksheet sheetBefore)
		{
			int a_ = 15;
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_58;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					if (sheetBefore == null)
					{
						num = 2;
						continue;
					}
					goto IL_97;
				case 2:
					goto IL_81;
				case 3:
					goto IL_58;
				}
				if (toCopy == null)
				{
					num = 3;
				}
				else
				{
					num = 1;
				}
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅄ⡆ੈ⑊㵌㙎", a_));
			IL_81:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙄⽆ⱈ⹊㥌ൎ㑐㕒㩔╖㱘", a_));
			IL_97:
			int index = sheetBefore.Index;
			XlsWorksheet xlsWorksheet = (XlsWorksheet)this.AddCopy(toCopy);
			xlsWorksheet.MoveWorksheet(index + 1);
			xlsWorksheet.Activate();
			return xlsWorksheet;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0001BE74 File Offset: 0x0001AE74
		protected override void OnInsertComplete(int index, IWorksheet value)
		{
			for (;;)
			{
				(value as XlsWorksheet).NameChanged += this.ᜀ;
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
							goto IL_7D;
						default:
							if (false)
							{
							}
							if (this.ᜂ)
							{
								num = 1;
								continue;
							}
							goto IL_92;
						}
						break;
					case 1:
						this.ᜀ[value.Name] = value;
						goto IL_7D;
					case 2:
						goto IL_88;
					}
					break;
					IL_7D:
					num = 2;
				}
			}
			IL_88:
			if (true)
			{
			}
			IL_92:
			base.OnInsertComplete(index, value);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001BF1C File Offset: 0x0001AF1C
		protected override void OnSetComplete(int index, IWorksheet oldValue, IWorksheet newValue)
		{
			for (;;)
			{
				if (true)
				{
				}
				(oldValue as XlsWorksheet).NameChanged -= this.ᜀ;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ.Remove(oldValue.Name);
						this.ᜀ[newValue.Name] = newValue;
						goto IL_97;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_97;
						default:
							if (false)
							{
							}
							if (this.ᜂ)
							{
								num = 0;
								continue;
							}
							goto IL_A4;
						}
						break;
					case 2:
						goto IL_A2;
					}
					break;
					IL_97:
					num = 2;
				}
			}
			IL_A2:
			IL_A4:
			base.OnSetComplete(index, oldValue, newValue);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001BFD8 File Offset: 0x0001AFD8
		protected override void OnRemoveComplete(int index, IWorksheet value)
		{
			for (;;)
			{
				(value as XlsWorksheet).NameChanged -= this.ᜀ;
				int num = 1;
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
							goto IL_85;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							if (this.ᜂ)
							{
								num = 2;
								continue;
							}
							goto IL_92;
						}
						break;
					case 2:
						this.ᜀ.Remove(value.Name);
						goto IL_85;
					}
					break;
					IL_85:
					num = 0;
				}
			}
			IL_90:
			IL_92:
			base.OnRemoveComplete(index, value);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0001C080 File Offset: 0x0001B080
		protected override void OnClearComplete()
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
			base.OnClearComplete();
			this.ᜀ.Clear();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001C0CC File Offset: 0x0001B0CC
		private new void ᜀ(object A_0, XlsEventArgs A_1)
		{
			int a_ = 14;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜀ.ContainsKey((string)A_1.newValue))
					{
						num = 2;
						continue;
					}
					this.ᜀ.Remove((string)A_1.oldValue);
					this.ᜀ[(string)A_1.newValue] = (IWorksheet)A_0;
					if (true)
					{
					}
					num = 4;
					continue;
				case 1:
					num = 0;
					continue;
				case 2:
					goto IL_F2;
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
						break;
					}
					break;
				case 4:
					goto IL_A1;
				}
				if (!this.ᜂ)
				{
					break;
				}
				num = 1;
			}
			IL_A1:
			return;
			IL_F2:
			throw new ArgumentException(RecordTableEnumerator.b("C㍅㡇♉╋ⵍㅏ♑ㅓ㉕硗㑙㵛㍝՟䉡ୣe䡧ᵩͫᱭ᭯űᱳ፵ᵷ๹屻ᙽꒃ꺍﶑ﲗ몙肟햡쮣풥쎧좩쎫솭\udbaf鲱", a_));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001C1D0 File Offset: 0x0001B1D0
		public IWorksheet Create(string name)
		{
			int a_ = 12;
			while (name == null)
			{
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
					throw new ArgumentNullException(RecordTableEnumerator.b("ⱁ╃⭅ⵇ", a_));
				}
			}
			IWorksheet worksheet = base.AppImplementation.ᜁ(this);
			worksheet.Name = name;
			return this.ᜁ(worksheet);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001C248 File Offset: 0x0001B248
		public IWorksheet Create()
		{
			int a_ = 4;
			IWorksheet worksheet;
			string text;
			for (;;)
			{
				IL_3D:
				if (true)
				{
				}
				worksheet = base.AppImplementation.ᜁ(this);
				int num = base.InnerList.Count;
				text = RecordTableEnumerator.b("椹吻嬽┿㙁", a_) + num;
				int num2 = 3;
				for (;;)
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
						switch (num2)
						{
						case 0:
							goto IL_82;
						case 1:
							goto IL_9E;
						case 2:
							if (this[text] == null)
							{
								num2 = 1;
								continue;
							}
							num++;
							text = RecordTableEnumerator.b("椹吻嬽┿㙁", a_) + num;
							num2 = 0;
							continue;
						case 3:
							goto IL_82;
						}
						goto IL_3D;
					}
					IL_82:
					num2 = 2;
				}
			}
			IL_9E:
			worksheet.Name = text;
			return this.ᜁ(worksheet);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001C338 File Offset: 0x0001B338
		public new void Remove(IWorksheet sheet)
		{
			int a_ = 5;
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
						goto IL_63;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					goto IL_9C;
				case 2:
					if (this.ᜁ.Objects.Count == 1)
					{
						num = 1;
						continue;
					}
					goto IL_B2;
				case 3:
					goto IL_63;
				}
				if (true)
				{
				}
				if (!base.InnerList.Contains(sheet))
				{
					num = 3;
				}
				else
				{
					num = 2;
				}
			}
			IL_63:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䰺刼䴾⩀あⵄ≆ⱈ㽊浌ⱎぐ㵒㭔㡖ⵘ筚㽜㩞䅠բ੤ቦݨཪ䵬ٮὰ卲ɴᡶ୸ၺὼၾꮄ", a_));
			IL_9C:
			throw new ArgumentException(RecordTableEnumerator.b("砺尼儾⽀ⱂㅄ杆㭈⹊⁌⁎❐㙒畔㭖㡘⡚⥜罞ᙠౢᝤ౦ᩨͪ࡬੮հ嵲", a_));
			IL_B2:
			sheet.Remove();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001C408 File Offset: 0x0001B408
		public void Remove(string sheetName)
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
			this.Remove(this[sheetName]);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0001C450 File Offset: 0x0001B450
		public void Remove(int index)
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
			this.Remove(this[index]);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001C498 File Offset: 0x0001B498
		public void UpdateStringIndexes(List<int> newIndexes)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						List<IWorksheet> innerList = base.InnerList;
						int num = 0;
						int count = base.Count;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_4C;
							case 1:
								return;
							case 2:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								XlsWorksheet xlsWorksheet = (XlsWorksheet)innerList[num];
								xlsWorksheet.UpdateStringIndexes(newIndexes);
								num++;
								num2 = 3;
								continue;
							}
							case 3:
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
									goto IL_4C;
								}
								break;
							}
							break;
							IL_4C:
							num2 = 2;
						}
					}
				}
				return;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0001C550 File Offset: 0x0001B550
		protected internal void InnerRemove(int index)
		{
			int a_ = 8;
			switch (0)
			{
			default:
				for (;;)
				{
					int count = base.Count;
					int num = 7;
					for (;;)
					{
						int num3;
						switch (num)
						{
						case 0:
						{
							IWorksheet worksheet;
							if (this.ᜁ.ActiveSheet == worksheet)
							{
								num = 9;
								continue;
							}
							goto IL_23A;
						}
						case 1:
							goto IL_1A5;
						case 2:
							goto IL_23A;
						case 3:
						{
							if (this.ᜁ.Objects.Count == 1)
							{
								num = 12;
								continue;
							}
							IWorksheet worksheet = this[index];
							int realIndex = ((spr\u252A)worksheet).get_RealIndex();
							base.RemoveAt(index);
							XlsWorkbookObjectsCollection objects = this.ᜁ.Objects;
							objects.RemoveAt(realIndex);
							int num2 = realIndex;
							int count2 = objects.Count;
							num = 5;
							continue;
						}
						case 4:
							goto IL_235;
						case 5:
							goto IL_D0;
						case 6:
							goto IL_D0;
						case 7:
							if (index < 0)
							{
								goto IL_155;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_BF;
							default:
								if (false)
								{
								}
								num = 10;
								continue;
							}
							break;
						case 8:
							return;
						case 9:
							this.ᜁ.SetActiveWorksheet(this[0] as XlsWorksheetBase);
							goto IL_BF;
						case 10:
							num = 11;
							continue;
						case 11:
							if (index > count - 1)
							{
								num = 4;
								continue;
							}
							num = 3;
							continue;
						case 12:
							goto IL_124;
						case 13:
							num = 0;
							continue;
						case 14:
							if (num3 >= count)
							{
								num = 8;
								continue;
							}
							((XlsWorksheet)this[num3 - 1]).Index = num3 - 1;
							num3++;
							num = 1;
							continue;
						case 15:
							goto IL_1A5;
						case 16:
						{
							int num2;
							int count2;
							if (num2 >= count2)
							{
								num = 13;
								continue;
							}
							XlsWorkbookObjectsCollection objects;
							objects[num2].set_RealIndex(num2);
							num2++;
							num = 6;
							continue;
						}
						}
						break;
						IL_BF:
						num = 2;
						continue;
						IL_D0:
						num = 16;
						continue;
						IL_1A5:
						num = 14;
						continue;
						IL_23A:
						num3 = index + 1;
						if (true)
						{
						}
						num = 15;
					}
				}
				IL_124:
				throw new ArgumentException(RecordTableEnumerator.b("紽ℿⱁ⩃⥅㱇橉㹋⭍㵏㵑≓㍕硗㙙㵛ⵝᑟ䉡፣॥ᩧũὫ٭ᕯ᝱s塵", a_), RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
				IL_155:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽⸿♁⅃㹅", a_), RecordTableEnumerator.b("栽ℿ⹁ㅃ⍅桇⥉ⵋ⁍㹏㵑⁓癕㩗㽙籛㉝՟ᅡᝣ䙥ᱧɩ൫m偯䉱味᝵ᙷṹ屻᥽겋揄望뚕ﮗ풟財", a_));
				IL_235:
				goto IL_155;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001C808 File Offset: 0x0001B808
		public void InnerAdd(IWorksheet sheet)
		{
			int a_ = 4;
			while (sheet == null)
			{
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䤹吻嬽┿㙁", a_));
				}
			}
			base.Add(sheet);
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0001C86C File Offset: 0x0001B86C
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0001C98C File Offset: 0x0001B98C
		public bool IsRightToLeft
		{
			get
			{
				switch (0)
				{
				default:
					for (;;)
					{
						List<IWorksheet> innerList = base.InnerList;
						bool isRightToLeft = innerList[0].IsRightToLeft;
						int num = 1;
						int count = innerList.Count;
						if (true)
						{
						}
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
							{
								bool isRightToLeft2;
								if (isRightToLeft2 == isRightToLeft)
								{
									num2 = 3;
									continue;
								}
								return false;
							}
							case 1:
							{
								if (num >= count)
								{
									num2 = 4;
									continue;
								}
								ITabSheet tabSheet = innerList[num];
								bool isRightToLeft2 = tabSheet.IsRightToLeft;
								num2 = 0;
								continue;
							}
							case 2:
								goto IL_A9;
							case 3:
								num2 = 7;
								continue;
							case 4:
								return isRightToLeft;
							case 5:
								goto IL_A9;
							case 6:
								goto IL_10B;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									if (false)
									{
									}
									if (!isRightToLeft)
									{
										num2 = 6;
										continue;
									}
									num++;
									num2 = 5;
									continue;
								}
								break;
							}
							break;
							IL_A9:
							num2 = 1;
						}
					}
					return false;
					IL_10B:
					return false;
				}
			}
			set
			{
				switch (0)
				{
				default:
					for (;;)
					{
						for (;;)
						{
							List<IWorksheet> innerList = base.InnerList;
							int num = 0;
							int count = innerList.Count;
							int num2 = 1;
							for (;;)
							{
								switch (num2)
								{
								case 0:
									goto IL_62;
								case 1:
									goto IL_4C;
								case 2:
								{
									if (num >= count)
									{
										num2 = 0;
										continue;
									}
									ITabSheet tabSheet = innerList[num];
									tabSheet.IsRightToLeft = value;
									num++;
									num2 = 3;
									continue;
								}
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
										goto IL_4C;
									}
									break;
								}
								break;
								IL_4C:
								num2 = 2;
							}
						}
					}
					IL_62:
					if (true)
					{
					}
					return;
				}
			}
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001CA40 File Offset: 0x0001BA40
		private new void ᜀ(object A_0, TabSheetMovedEventArgs A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						ITabSheets tabSheets = (ITabSheets)A_0;
						int newIndex = A_1.NewIndex;
						XlsWorksheet xlsWorksheet = tabSheets[newIndex] as XlsWorksheet;
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
							int num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (xlsWorksheet != null)
									{
										if (true)
										{
										}
										num = 2;
										continue;
									}
									return;
								case 1:
									return;
								case 2:
								{
									int oldIndex = A_1.OldIndex;
									this.UpdateSheetIndex(xlsWorksheet, oldIndex);
									num = 1;
									continue;
								}
								}
								break;
							}
							break;
						}
						}
					}
				}
				return;
			}
		}

		// Token: 0x0400008B RID: 139
		private new Dictionary<string, IWorksheet> ᜀ;

		// Token: 0x0400008C RID: 140
		private new XlsWorkbook ᜁ;

		// Token: 0x0400008D RID: 141
		private bool \u2609\u0097\u00AF\u00A1;

		// Token: 0x0400008E RID: 142
		private new bool ᜂ;
	}
}
