using System;
using System.Collections.Generic;
using System.IO;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000029 RID: 41
	public class XlsAddInFunctionsCollection : CollectionExtended<XlsAddInFunction>, IAddInFunctions
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x000185B8 File Offset: 0x000175B8
		internal XlsAddInFunctionsCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
			this.ᜃ.ExternWorkbooks.Inserted += this.ᜀ;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x000185FC File Offset: 0x000175FC
		private new void ᜀ()
		{
			int a_ = 3;
			this.ᜃ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜃ == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("吸携弼倾⹀⡂", a_));
				}
			}
		}

		// Token: 0x17000112 RID: 274
		public IAddInFunction this[int index]
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
				return base.List[index];
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000186C4 File Offset: 0x000176C4
		public int Add(string fileName, string functionName)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				int num = 15;
				XlsExternWorkbook xlsExternWorkbook;
				int num3;
				for (;;)
				{
					int num2;
					XlsExternBookCollection externWorkbooks;
					switch (num)
					{
					case 0:
						goto IL_1B2;
					case 1:
						if (fileName == null)
						{
							num = 7;
							continue;
						}
						num = 6;
						continue;
					case 2:
						goto IL_1B7;
					case 3:
						if (fileName != null)
						{
							num = 11;
							continue;
						}
						goto IL_22D;
					case 4:
						goto IL_114;
					case 5:
						num2 = this.ᜄ.Index;
						goto IL_116;
					case 6:
						num2 = this.ᜂ[fileName];
						goto IL_116;
					case 7:
						num = 5;
						continue;
					case 8:
						goto IL_85;
					case 9:
						xlsExternWorkbook.IsAddInFunctions = true;
						num = 17;
						continue;
					case 10:
						if (functionName.Length == 0)
						{
							num = 4;
							continue;
						}
						num = 3;
						continue;
					case 11:
						fileName = Path.GetFullPath(fileName);
						if (true)
						{
						}
						num = 12;
						continue;
					case 12:
						goto IL_22D;
					case 13:
						if (this.Contains(fileName))
						{
							num = 16;
							continue;
						}
						num3 = externWorkbooks.Add(fileName);
						num = 18;
						continue;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_239;
						default:
							if (false)
							{
							}
							if (fileName == null)
							{
								num = 9;
								continue;
							}
							goto IL_17F;
						}
						break;
					case 16:
						num = 1;
						continue;
					case 17:
						goto IL_17F;
					case 18:
						goto IL_1B7;
					case 19:
						if (xlsExternWorkbook.ExternNames.ᜀ(functionName))
						{
							num = 0;
							continue;
						}
						goto IL_27E;
					}
					if (functionName == null)
					{
						num = 8;
						continue;
					}
					num = 10;
					continue;
					IL_116:
					num3 = num2;
					num = 2;
					continue;
					IL_17F:
					num = 19;
					continue;
					IL_1B7:
					xlsExternWorkbook = externWorkbooks[num3];
					num = 14;
					continue;
					IL_239:
					num = 13;
					continue;
					IL_22D:
					externWorkbooks = this.ᜃ.ExternWorkbooks;
					goto IL_239;
				}
				IL_85:
				throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ㱈╊⹌㭎㡐㱒㭔ᥖ㡘㙚㡜", a_));
				IL_114:
				throw new ArgumentException(RecordTableEnumerator.b("ᑆ㵈㥊⑌ⅎ㙐獒㙔㙖㝘㕚㉜⭞䅠Ţd䝦౨٪ᵬ᭮ࡰ嵲", a_), RecordTableEnumerator.b("ⅆ㱈╊⹌㭎㡐㱒㭔ᥖ㡘㙚㡜", a_));
				IL_1B2:
				throw new ApplicationException(RecordTableEnumerator.b("͆㱈㭊⅌♎㉐㉒⅔㉖㵘筚㭜⩞འbᅤ๦٨ժ䵬ݮၰr啴ᕶᱸṺ፼彾ꖊ", a_));
				IL_27E:
				int a_2 = xlsExternWorkbook.ExternNames.ᜃ(functionName);
				base.Add(new XlsAddInFunction(base.ReservedHandle, this, num3, a_2));
				return base.Count - 1;
			}
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00018978 File Offset: 0x00017978
		public int Add(string functionName)
		{
			int a_ = 16;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8C;
				case 1:
					if (functionName.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_B4;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_4A;
					}
					break;
				}
				goto IL_29;
				IL_2C:
				num = 2;
				continue;
				IL_29:
				if (functionName == null)
				{
					goto IL_2C;
				}
				num = 1;
			}
			IL_4A:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⁅㵇⑉⽋㩍㥏㵑㩓ᡕ㥗㝙㥛", a_));
			IL_8C:
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("ᕅ㱇㡉╋⁍㝏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥൧ݩᱫᩭ९山", a_), RecordTableEnumerator.b("⁅㵇⑉⽋㩍㥏㵑㩓ᡕ㥗㝙㥛", a_));
			IL_B4:
			int a_2 = this.ᜃ.InnerNamesColection.ᜀ(functionName);
			XlsAddInFunction item = new ExcelAddInFunction((spr\u2158)base.ReservedHandle, this, -1, a_2);
			base.Add(item);
			return base.Count - 1;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00018A70 File Offset: 0x00017A70
		public void Add(int bookIndex, int nameIndex)
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
			XlsAddInFunction item = new ExcelAddInFunction((spr\u2158)base.ReservedHandle, this, bookIndex, nameIndex);
			base.Add(item);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00018AC8 File Offset: 0x00017AC8
		public new void RemoveAt(int index)
		{
			int a_ = 11;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 2;
					continue;
				case 2:
					if (index > base.Count - 1)
					{
						num = 3;
						continue;
					}
					goto IL_A7;
				case 3:
					goto IL_A5;
				}
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
					if (index < 0)
					{
						goto IL_65;
					}
					break;
				}
				num = 1;
			}
			IL_65:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀ⵂ⅄≆ㅈ", a_), RecordTableEnumerator.b("ᝀ≂⥄㉆ⱈ歊⹌⹎㽐㵒㩔⍖祘㥚㡜罞ൠ٢ᙤᑦ䥨Ὢլ๮ὰ卲䕴坶ᡸᕺ᥼彾ﶈﾌ꾎ﮒ練릘쾠힢认", a_));
			IL_A5:
			goto IL_65;
			IL_A7:
			XlsAddInFunction xlsAddInFunction = base.List[index];
			XlsExternWorkbook xlsExternWorkbook = this.ᜃ.ExternWorkbooks[xlsAddInFunction.BookIndex];
			xlsExternWorkbook.ExternNames.RemoveAt(xlsAddInFunction.NameIndex);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00018BB4 File Offset: 0x00017BB4
		public bool Contains(string workbookName)
		{
			if (workbookName == null)
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
					return this.ᜄ != null;
				}
			}
			return this.ᜂ.ContainsKey(workbookName);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00018C10 File Offset: 0x00017C10
		public void CopyFrom(XlsAddInFunctionsCollection addinFunctions)
		{
			switch (0)
			{
			default:
			{
				int num = 4;
				for (;;)
				{
					int num2;
					int count;
					List<XlsAddInFunction> innerList;
					List<XlsAddInFunction> innerList2;
					switch (num)
					{
					case 0:
						goto IL_7E;
					case 1:
						goto IL_E7;
					case 2:
					{
						int index = addinFunctions.ᜄ.Index;
						this.ᜄ = this.ᜃ.ExternWorkbooks[index];
						if (true)
						{
						}
						num = 0;
						continue;
					}
					case 3:
						goto IL_E7;
					case 5:
						if (num2 < count)
						{
							XlsAddInFunction xlsAddInFunction = innerList[num2];
							xlsAddInFunction = (XlsAddInFunction)xlsAddInFunction.Clone(this);
							innerList2.Add(xlsAddInFunction);
							num2++;
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					case 6:
						return;
					}
					if (addinFunctions.ᜄ != null)
					{
						num = 2;
						continue;
					}
					IL_7E:
					innerList = addinFunctions.InnerList;
					innerList2 = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 3;
					continue;
					IL_E7:
					num = 5;
				}
				return;
			}
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00018D44 File Offset: 0x00017D44
		private new void ᜀ(object A_0, CollectionChangeEventArgs<XlsExternWorkbook> A_1)
		{
			int a_ = 15;
			XlsExternWorkbook value;
			for (;;)
			{
				IL_51:
				value = A_1.Value;
				if (true)
				{
				}
				int num = 4;
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
						switch (num)
						{
						case 0:
						{
							string url;
							if (url != RecordTableEnumerator.b("敄", a_))
							{
								num = 3;
								continue;
							}
							return;
						}
						case 1:
							goto IL_11F;
						case 2:
						{
							string url;
							if (url == null)
							{
								num = 1;
								continue;
							}
							num = 0;
							continue;
						}
						case 3:
						{
							string url;
							this.ᜂ.Add(url, value.Index);
							goto IL_EF;
						}
						case 4:
							if (!value.IsInternalReference)
							{
								num = 6;
								continue;
							}
							return;
						case 5:
						{
							string url = value.URL;
							num = 2;
							continue;
						}
						case 6:
							num = 8;
							continue;
						case 7:
							goto IL_FA;
						case 8:
							if (value.IsAddInFunctions)
							{
								num = 5;
								continue;
							}
							return;
						}
						goto IL_51;
					}
					IL_EF:
					num = 7;
				}
			}
			IL_FA:
			return;
			IL_11F:
			this.ᜄ = value;
		}

		// Token: 0x04000080 RID: 128
		private float \u25D9\u0097\u0099\u0086;

		// Token: 0x04000081 RID: 129
		private bool[] \u25D8ª\u00A7\u0086;

		// Token: 0x04000082 RID: 130
		private float \u2609\u00A2\u0097\u008B;

		// Token: 0x04000083 RID: 131
		private new const string ᜀ = "\u0001";

		// Token: 0x04000084 RID: 132
		private int[] \u2460\u0089\u008A\u009C;

		// Token: 0x04000085 RID: 133
		private new const int ᜁ = -1;

		// Token: 0x04000086 RID: 134
		private new Dictionary<string, int> ᜂ = new Dictionary<string, int>();

		// Token: 0x04000087 RID: 135
		private bool \u25D8ª\u00AF\u00AC;

		// Token: 0x04000088 RID: 136
		private long[] \u2460\u00A5\u0082\u0092;

		// Token: 0x04000089 RID: 137
		private XlsWorkbook ᜃ;

		// Token: 0x0400008A RID: 138
		private XlsExternWorkbook ᜄ;
	}
}
