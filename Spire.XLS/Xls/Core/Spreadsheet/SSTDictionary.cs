using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000633 RID: 1587
	public class SSTDictionary : spr\u1D46, IDisposable
	{
		// Token: 0x17000FEE RID: 4078
		internal int this[spr\u223A A_0]
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
				this.Parse();
				return this.ᜀ(A_0);
			}
		}

		// Token: 0x17000FEF RID: 4079
		internal spr\u223A this[int A_0]
		{
			get
			{
				spr\u223A spr_u223A;
				for (;;)
				{
					IL_14:
					object sstcontentByIndex = this.GetSSTContentByIndex(A_0);
					spr_u223A = (sstcontentByIndex as spr\u223A);
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜉ.ᜁ((string)sstcontentByIndex);
							spr_u223A = this.ᜉ;
							num = 1;
							continue;
						case 1:
							goto IL_6A;
						case 2:
							if (spr_u223A == null)
							{
								num = 0;
								continue;
							}
							return spr_u223A;
						}
						goto IL_14;
					}
					IL_6A:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_80;
					}
				}
				IL_80:
				if (false)
				{
				}
				return spr_u223A;
			}
		}

		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06006113 RID: 24851 RVA: 0x003D58D8 File Offset: 0x003D48D8
		public object[] Keys
		{
			get
			{
				object[] array;
				for (;;)
				{
					IL_34:
					int count = this.Count;
					array = new object[count];
					int num = 0;
					for (;;)
					{
						IL_44:
						int num2 = 1;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_44;
							default:
								if (false)
								{
								}
								switch (num2)
								{
								case 0:
									goto IL_4E;
								case 1:
									goto IL_4E;
								case 2:
									return array;
								case 3:
									if (num >= count)
									{
										num2 = 2;
										continue;
									}
									if (true)
									{
									}
									array[num] = this.ᜄ[num];
									num++;
									num2 = 0;
									continue;
								}
								goto IL_34;
								IL_4E:
								num2 = 3;
								break;
							}
						}
					}
				}
				return array;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06006114 RID: 24852 RVA: 0x003D597C File Offset: 0x003D497C
		public int Count
		{
			get
			{
				if (this.ᜈ)
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
						return this.ᜄ.Count;
					}
				}
				return (int)this.ᜇ.ᜇ();
			}
		}

		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06006115 RID: 24853 RVA: 0x003D59D8 File Offset: 0x003D49D8
		public XlsWorkbook Workbook
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
				return this.ᜆ;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06006116 RID: 24854 RVA: 0x003D5A1C File Offset: 0x003D4A1C
		// (set) Token: 0x06006117 RID: 24855 RVA: 0x003D5A60 File Offset: 0x003D4A60
		[CLSCompliant(false)]
		internal sprỪ OriginalSST
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
				return this.ᜇ;
			}
			set
			{
				int a_ = 5;
				for (;;)
				{
					IL_09:
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜊ.EnsureCapacity((int)(this.ᜇ.ᜇ() * 4U));
							this.ᜊ.ZeroMemory();
							this.ᜋ = (int)this.ᜇ.ᜇ();
							num = 2;
							continue;
						case 1:
							if (this.ᜇ.ᜇ() != 0U)
							{
								num = 0;
								continue;
							}
							return;
						case 2:
							return;
						case 3:
							goto IL_40;
						}
						if (value == null)
						{
							if (true)
							{
							}
							num = 3;
						}
						else
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_09;
							default:
								if (false)
								{
								}
								this.ᜈ = false;
								this.ᜇ = value;
								num = 1;
								break;
							}
						}
					}
				}
				IL_40:
				throw new ArgumentNullException(RecordTableEnumerator.b("琺似嘾♀⩂⭄♆╈ᡊṌ᭎", a_));
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06006118 RID: 24856 RVA: 0x003D5B60 File Offset: 0x003D4B60
		// (set) Token: 0x06006119 RID: 24857 RVA: 0x003D5BA4 File Offset: 0x003D4BA4
		public bool UseHashForSearching
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
				return this.ᜌ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (this.ᜈ)
						{
							num = 4;
							continue;
						}
						return;
					case 2:
						goto IL_94;
					case 3:
						goto IL_4A;
					case 4:
						this.ᜀ();
						num = 5;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4A;
						default:
							goto IL_BD;
						}
						break;
					case 6:
						if (!value)
						{
							num = 2;
							continue;
						}
						num = 1;
						continue;
					}
					if (this.ᜌ != value)
					{
						num = 3;
						continue;
					}
					return;
					IL_4A:
					if (true)
					{
					}
					this.ᜌ = value;
					num = 6;
				}
				IL_94:
				this.ᜃ.Clear();
				return;
				IL_BD:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000FF5 RID: 4085
		// (get) Token: 0x0600611A RID: 24858 RVA: 0x003D5C78 File Offset: 0x003D4C78
		public int ActiveCount
		{
			get
			{
				if (this.ᜈ)
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
						return this.ᜄ.Count - this.ᜅ.Count;
					}
				}
				return (int)this.ᜇ.ᜇ();
			}
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x003D5CE0 File Offset: 0x003D4CE0
		public SSTDictionary(XlsWorkbook book)
		{
			this.ᜆ = book;
			this.ᜊ = spr\u17FF.ᜀ(this.ᜆ.HeapHandle);
			this.ᜊ.ZeroMemory();
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x003D5D68 File Offset: 0x003D4D68
		public object GetSSTContentByIndex(int index)
		{
			int a_ = 17;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_76;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_D8;
				case 3:
					num = 0;
					continue;
				case 4:
					if (index >= this.Count)
					{
						num = 6;
						continue;
					}
					num = 5;
					continue;
				case 5:
					if (!this.ᜈ)
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				case 6:
					goto IL_CB;
				case 7:
					num = 4;
					continue;
				}
				if (index < 0)
				{
					goto IL_E6;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D8;
				default:
					if (false)
					{
					}
					num = 7;
					break;
				}
			}
			IL_76:
			return this.ᜇ.ᜊ()[index];
			IL_CB:
			goto IL_E6;
			IL_D8:
			return this.ᜄ[index];
			IL_E6:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆❈⽊⡌㝎", a_));
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x003D5E74 File Offset: 0x003D4E74
		public void Clear()
		{
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
					case 1:
						return;
					case 2:
						if (this.ᜈ)
						{
							num = 4;
							continue;
						}
						this.ᜇ = null;
						this.ᜈ = true;
						num = 1;
						continue;
					case 3:
						num = 2;
						continue;
					case 4:
						goto IL_AB;
					}
					if (true)
					{
					}
					if (this.ᜆ == null)
					{
						return;
					}
					num = 3;
				}
				IL_AB:
				this.ᜄ.Clear();
				this.ᜃ.Clear();
				return;
			}
			}
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x003D5F30 File Offset: 0x003D4F30
		public Dictionary<int, object> GetStringIndexes(string value)
		{
			switch (0)
			{
			default:
			{
				Dictionary<int, object> dictionary;
				for (;;)
				{
					dictionary = new Dictionary<int, object>();
					int num = 1;
					for (;;)
					{
						int num2;
						spr\u223A spr_u223A;
						string text;
						object obj;
						int count;
						object obj2;
						string text2;
						object[] array;
						switch (num)
						{
						case 0:
							num = 19;
							continue;
						case 1:
							if (!this.ᜈ)
							{
								num = 20;
								continue;
							}
							num = 17;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9F;
							default:
								if (false)
								{
								}
								dictionary.Add(num2, null);
								num = 6;
								continue;
							}
							break;
						case 3:
							text = spr_u223A.ᜏ();
							goto IL_1E8;
						case 4:
							obj = this.ᜄ[num2];
							goto IL_17C;
						case 5:
							if (num2 >= count)
							{
								num = 10;
								continue;
							}
							num = 12;
							continue;
						case 6:
							goto IL_DD;
						case 7:
							num = 11;
							continue;
						case 8:
							num = 13;
							continue;
						case 9:
							if (spr_u223A == null)
							{
								num = 7;
								continue;
							}
							num = 3;
							continue;
						case 10:
							return dictionary;
						case 11:
							text = (string)obj2;
							goto IL_1E8;
						case 12:
							if (!this.ᜅ.ContainsKey(num2))
							{
								num = 8;
								continue;
							}
							goto IL_DD;
						case 13:
							if (!this.ᜈ)
							{
								num = 0;
								continue;
							}
							num = 4;
							continue;
						case 14:
							goto IL_231;
						case 15:
							goto IL_231;
						case 16:
							if (text2.IndexOf(value, 0, StringComparison.CurrentCultureIgnoreCase) != -1)
							{
								num = 2;
								continue;
							}
							goto IL_DD;
						case 17:
							array = null;
							goto IL_1CE;
						case 18:
							array = this.ᜇ.ᜊ();
							goto IL_1CE;
						case 19:
							goto IL_9F;
						case 20:
							num = 18;
							continue;
						}
						break;
						IL_DD:
						if (true)
						{
						}
						num2++;
						num = 15;
						continue;
						IL_17C:
						obj2 = obj;
						spr_u223A = (obj2 as spr\u223A);
						num = 9;
						continue;
						IL_9F:
						object[] array2;
						obj = array2[num2];
						goto IL_17C;
						IL_1CE:
						array2 = array;
						num2 = 0;
						count = this.Count;
						num = 14;
						continue;
						IL_1E8:
						text2 = text;
						num = 16;
						continue;
						IL_231:
						num = 5;
					}
				}
				return dictionary;
			}
			}
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x003D619C File Offset: 0x003D519C
		public void AddIncrease(int index)
		{
			if (index != -1)
			{
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
					int num = this.ᜀ(index);
					this.ᜀ(index, num + 1);
					return;
				}
				}
			}
			if (true)
			{
			}
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x003D61F0 File Offset: 0x003D51F0
		public int AddIncrease(object key)
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
			return this.AddIncrease(key, true);
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x003D6234 File Offset: 0x003D5234
		public int AddIncrease(object key, bool bIncrease)
		{
			int num = 15;
			int num2;
			for (;;)
			{
				int num3;
				int a_;
				switch (num)
				{
				case 0:
					this.ᜃ[key] = num2;
					num = 6;
					continue;
				case 1:
					if (this.ᜌ)
					{
						num = 0;
						continue;
					}
					goto IL_8B;
				case 2:
					num2 = this.ᜄ.Count;
					this.ᜄ.Add(key);
					num = 1;
					continue;
				case 3:
					if (bIncrease)
					{
						num = 17;
						continue;
					}
					return num2;
				case 4:
					goto IL_256;
				case 5:
					num3 = 1;
					goto IL_27C;
				case 6:
					goto IL_8B;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_256;
					default:
						if (false)
						{
						}
						num = 20;
						continue;
					}
					break;
				case 8:
					if (this.ᜅ.Count == 0)
					{
						num = 2;
						continue;
					}
					num2 = this.ᜅ.Values[0];
					this.ᜄ[num2] = key;
					num = 14;
					continue;
				case 9:
					if (!bIncrease)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 10:
					return num2;
				case 11:
					return num2;
				case 12:
					this.AddIncrease(num2);
					num = 11;
					continue;
				case 13:
					return num2;
				case 14:
					if (this.ᜌ)
					{
						num = 16;
						continue;
					}
					goto IL_1BB;
				case 16:
					this.ᜃ[key] = num2;
					num = 18;
					continue;
				case 17:
					this.ᜀ(num2, a_);
					num = 10;
					continue;
				case 18:
					goto IL_1BB;
				case 19:
					if (this.ᜃ.TryGetValue(key, out num2))
					{
						num = 7;
						continue;
					}
					this.ᜁ(key);
					num = 9;
					continue;
				case 20:
					if (bIncrease)
					{
						num = 12;
						continue;
					}
					return num2;
				case 21:
					this.Parse();
					num = 22;
					continue;
				case 22:
					goto IL_166;
				case 23:
					num3 = 0;
					goto IL_27C;
				}
				if (true)
				{
				}
				if (bIncrease)
				{
					num = 21;
					continue;
				}
				goto IL_166;
				IL_8B:
				num = 3;
				continue;
				IL_166:
				num = 19;
				continue;
				IL_1BB:
				this.ᜅ.RemoveAt(0);
				this.ᜀ(num2, a_);
				num = 13;
				continue;
				IL_256:
				num = 23;
				continue;
				IL_27C:
				a_ = num3;
				num = 8;
			}
			return num2;
		}

		// Token: 0x06006122 RID: 24866 RVA: 0x003D64EC File Offset: 0x003D54EC
		private void ᜁ(object A_0)
		{
			int a_ = 17;
			for (;;)
			{
				string text = A_0 as string;
				int num = 0;
				int num2 = 8;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (this.ᜆ.Version == ExcelVersion.Version97to2003)
						{
							if (true)
							{
							}
							num2 = 1;
							continue;
						}
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E1;
						default:
							if (false)
							{
							}
							num2 = 4;
							continue;
						}
						break;
					case 2:
						goto IL_E1;
					case 3:
						num = (A_0 as spr\u223A).ᜏ().Length;
						num2 = 9;
						continue;
					case 4:
						if (num > 32767)
						{
							num2 = 7;
							continue;
						}
						return;
					case 5:
						num = text.Length;
						num2 = 6;
						continue;
					case 6:
						goto IL_F1;
					case 7:
						goto IL_7F;
					case 8:
						if (text != null)
						{
							num2 = 5;
							continue;
						}
						num2 = 2;
						continue;
					case 9:
						goto IL_F1;
					}
					break;
					IL_E1:
					if (A_0 != null)
					{
						num2 = 3;
						continue;
					}
					IL_F1:
					num2 = 0;
				}
			}
			IL_7F:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ፆⱈ㍊㥌潎㵐㙒㭔ざⵘ㍚絜㱞`ൢ୤ࡦᵨ䭪ཬ੮兰Ṳᩴնᱸ孺ॼ᝾ꖄ", a_) + 32767);
		}

		// Token: 0x06006123 RID: 24867 RVA: 0x003D6638 File Offset: 0x003D5638
		public void RemoveDecrease(object key)
		{
			int a_ = 2;
			this.Parse();
			int num = this.ᜀ(key);
			if (num != -1)
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
					this.RemoveDecrease(num);
					return;
				}
			}
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("簷匹弻䨽⤿ⵁ⩃❅㩇㍉汋⩍㽏㝑❓癕㙗㕙⡛繝͟ൡ੣ብ१ͩɫ乭ͯɱᅳᕵᅷᱹᕻ᭽ꊁ慎ꪏ늑뎓", a_) + key + RecordTableEnumerator.b("ἷ", a_));
		}

		// Token: 0x06006124 RID: 24868 RVA: 0x003D66C0 File Offset: 0x003D56C0
		public void RemoveDecrease(int iIndex)
		{
			int a_ = 1;
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_C5;
				case 1:
				{
					int num2;
					if (num2 <= 0)
					{
						num = 3;
						continue;
					}
					goto IL_14F;
				}
				case 2:
				{
					if (iIndex >= this.Count)
					{
						num = 4;
						continue;
					}
					int num2 = this.ᜀ(iIndex);
					num2--;
					this.ᜀ(iIndex, num2);
					num = 1;
					continue;
				}
				case 3:
					goto IL_A2;
				case 4:
					goto IL_C3;
				case 5:
					goto IL_EA;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A2;
					}
					if (false)
					{
					}
					this.ᜃ.Remove(this.ᜄ[iIndex]);
					num = 0;
					continue;
				case 7:
					if (this.ᜌ)
					{
						num = 6;
						continue;
					}
					goto IL_C5;
				case 8:
					num = 2;
					continue;
				}
				if (iIndex >= 0)
				{
					num = 8;
					continue;
				}
				break;
				IL_A2:
				this.Parse();
				num = 7;
				continue;
				IL_C5:
				this.ᜅ[iIndex] = iIndex;
				this.ᜄ[iIndex] = null;
				num = 5;
			}
			IL_C3:
			goto IL_EC;
			IL_EA:
			goto IL_14F;
			IL_EC:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶瀸唺夼娾㥀", a_));
			IL_14F:
			if (true)
			{
			}
		}

		// Token: 0x06006125 RID: 24869 RVA: 0x003D6824 File Offset: 0x003D5824
		public void DecreaseOnly(int index)
		{
			int a_ = 17;
			int num;
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
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (index > this.ᜄ.Count)
					{
						num = 3;
						continue;
					}
					goto IL_99;
				case 1:
					num = 0;
					continue;
				case 3:
					goto IL_97;
				}
				if (index < 0)
				{
					break;
				}
				num = 1;
			}
			IL_65:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆❈⽊⡌㝎", a_));
			IL_97:
			goto IL_65;
			IL_99:
			int num2 = this.ᜀ(index);
			this.ᜀ(index, num2 - 1);
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x003D68DC File Offset: 0x003D58DC
		public bool Contains(object key)
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
			this.Parse();
			return this.ᜀ(key) != -1;
		}

		// Token: 0x06006127 RID: 24871 RVA: 0x003D692C File Offset: 0x003D592C
		[CLSCompliant(false)]
		public void SerializeDataToList(RecordArrayList records)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					IL_36:
					this.ᜁ();
					num = 2;
					continue;
				case 2:
					goto IL_48;
				}
				if (this.ᜈ)
				{
					num = 1;
					continue;
				}
				IL_48:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_36;
				default:
					goto IL_5E;
				}
			}
			IL_5E:
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜀ(records);
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x003D69AC File Offset: 0x003D59AC
		public int GetStringCount(int index)
		{
			if (true)
			{
			}
			if (index != -1)
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
					this.Parse();
					return this.ᜀ(index);
				}
			}
			return 2;
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x003D69FC File Offset: 0x003D59FC
		internal spr\u223A ᜂ(int A_0)
		{
			if (A_0 != -1)
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
					return this[A_0];
				}
			}
			throw new NotImplementedException();
		}

		// Token: 0x0600612A RID: 24874 RVA: 0x003D6A4C File Offset: 0x003D5A4C
		public int AddCopy(int index, SSTDictionary sourceSST, Dictionary<int, int> dicFontIndexes)
		{
			int a_ = 12;
			object obj;
			for (;;)
			{
				IL_09:
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						spr\u223A spr_u223A;
						if (spr_u223A != null)
						{
							num = 2;
							continue;
						}
						goto IL_B4;
					}
					case 2:
					{
						spr\u223A spr_u223A;
						obj = spr_u223A.ᜁ(dicFontIndexes);
						num = 3;
						continue;
					}
					case 3:
						goto IL_4A;
					case 4:
						goto IL_38;
					}
					if (sourceSST == null)
					{
						num = 4;
					}
					else
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_09;
						default:
						{
							if (false)
							{
							}
							this.Parse();
							sourceSST.Parse();
							obj = sourceSST.ᜄ[index];
							spr\u223A spr_u223A = obj as spr\u223A;
							num = 1;
							break;
						}
						}
					}
				}
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("ㅁ⭃㍅㩇⥉⥋ᵍ͏ّ", a_));
			IL_4A:
			IL_B4:
			if (true)
			{
			}
			return this.AddIncrease(obj, true);
		}

		// Token: 0x0600612B RID: 24875 RVA: 0x003D6B28 File Offset: 0x003D5B28
		public List<int> StartWith(string strStart)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
					{
						List<int> list;
						return list;
					}
					case 2:
						goto IL_10C;
					case 3:
						if (strStart.Length == 0)
						{
							num = 9;
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
							if (true)
							{
							}
							List<int> list = new List<int>();
							num2 = 0;
							int count = this.Count;
							num = 8;
							continue;
						}
						}
						break;
					case 4:
					{
						int count;
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						spr\u223A spr_u223A = this[num2];
						num = 6;
						continue;
					}
					case 5:
					{
						List<int> list;
						list.Add(num2);
						num = 2;
						continue;
					}
					case 6:
					{
						spr\u223A spr_u223A;
						if (spr_u223A.ᜏ().StartsWith(strStart))
						{
							num = 5;
							continue;
						}
						goto IL_10C;
					}
					case 7:
						goto IL_61;
					case 8:
						goto IL_D7;
					case 9:
						goto IL_D2;
					case 10:
						goto IL_D7;
					}
					IL_55:
					if (strStart == null)
					{
						num = 7;
						continue;
					}
					num = 3;
					continue;
					goto IL_55;
					IL_D7:
					num = 4;
					continue;
					IL_10C:
					num2++;
					num = 10;
				}
				IL_61:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭇㹉㹋ᵍ⑏㍑♓≕", a_));
				IL_D2:
				throw new ArgumentException(RecordTableEnumerator.b("㭇㹉㹋ᵍ⑏㍑♓≕硗睙籛ⵝᑟၡൣࡥཧ䩩ཫ཭ṯᱱ᭳ɵ塷᡹᥻幽ꒉ", a_));
			}
			}
		}

		// Token: 0x0600612C RID: 24876 RVA: 0x003D6CAC File Offset: 0x003D5CAC
		public object Clone(XlsWorkbook book)
		{
			SSTDictionary sstdictionary;
			for (;;)
			{
				sstdictionary = (SSTDictionary)base.MemberwiseClone();
				sstdictionary.ᜆ = book;
				sstdictionary.ᜃ = spr\u1CD3.ᜀ(this.ᜃ);
				sstdictionary.ᜄ = spr\u1CD3.ᜀ<object>(this.ᜄ);
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							sstdictionary.ᜅ = new SortedList<int, int>(this.ᜅ);
							break;
						}
						num = 2;
						continue;
					case 1:
						if (this.ᜅ != null)
						{
							num = 0;
							continue;
						}
						goto IL_AF;
					case 2:
						goto IL_AD;
					}
					break;
				}
			}
			IL_AD:
			IL_AF:
			sstdictionary.ᜇ = (sprỪ)spr\u1CD3.ᜀ(this.ᜇ);
			sstdictionary.ᜊ = spr\u17FF.ᜀ(book.HeapHandle);
			sstdictionary.ᜊ.EnsureCapacity(this.ᜊ.Capacity);
			this.ᜊ.CopyTo(0, sstdictionary.ᜊ, 0, this.ᜊ.Capacity);
			return sstdictionary;
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x003D6DC4 File Offset: 0x003D5DC4
		public void UpdateRefCounts(int size)
		{
			for (;;)
			{
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5C:
					if (true)
					{
					}
					this.ᜊ.EnsureCapacity(num);
					this.ᜊ.ZeroMemory();
					num2 = 0;
					break;
				default:
					if (false)
					{
					}
					num = size * 4;
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
						goto IL_5C;
					case 2:
						if (this.ᜊ.Capacity < num)
						{
							num2 = 1;
							continue;
						}
						return;
					}
					break;
				}
			}
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x003D6E5C File Offset: 0x003D5E5C
		public void UpdateRefCounts()
		{
			for (;;)
			{
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_61:
					this.ᜊ.EnsureCapacity(num);
					this.ᜊ.ZeroMemory();
					if (true)
					{
					}
					num2 = 0;
					break;
				default:
					if (false)
					{
					}
					num = this.Count * 4;
					num2 = 1;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						if (this.ᜊ.Capacity < num)
						{
							num2 = 2;
							continue;
						}
						return;
					case 2:
						goto IL_61;
					}
					break;
				}
			}
		}

		// Token: 0x0600612F RID: 24879 RVA: 0x003D6EF8 File Offset: 0x003D5EF8
		public void RemoveUnnecessaryStrings()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.Parse();
					this.ᜊ.ZeroMemory();
					this.ᜆ.ᜦ();
					int num = 0;
					int count = this.Count;
					int num2 = 5;
					for (;;)
					{
						object obj;
						switch (num2)
						{
						case 0:
							goto IL_156;
						case 1:
							if (this.ᜌ)
							{
								num2 = 4;
								continue;
							}
							goto IL_7F;
						case 2:
							if (num < count)
							{
								int num3 = this.ᜀ(num);
								num2 = 8;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_156;
							default:
								if (false)
								{
								}
								num2 = 3;
								continue;
							}
							break;
						case 3:
							goto IL_131;
						case 4:
							this.ᜃ.Remove(obj);
							num2 = 10;
							continue;
						case 5:
							if (true)
							{
							}
							goto IL_F9;
						case 6:
							if (obj != null)
							{
								num2 = 7;
								continue;
							}
							goto IL_15B;
						case 7:
							num2 = 1;
							continue;
						case 8:
						{
							int num3;
							if (num3 == 0)
							{
								num2 = 0;
								continue;
							}
							goto IL_15B;
						}
						case 9:
							goto IL_F9;
						case 10:
							goto IL_7F;
						case 11:
							goto IL_15B;
						}
						break;
						IL_7F:
						this.ᜄ[num] = null;
						this.ᜅ[num] = num;
						num2 = 11;
						continue;
						IL_F9:
						num2 = 2;
						continue;
						IL_156:
						obj = this.ᜄ[num];
						num2 = 6;
						continue;
						IL_15B:
						num++;
						num2 = 9;
					}
				}
				IL_131:
				this.ᜁ();
				return;
			}
		}

		// Token: 0x06006130 RID: 24880 RVA: 0x003D70A0 File Offset: 0x003D60A0
		private void ᜀ(int A_0, int A_1, int A_2, List<int> A_3)
		{
			for (;;)
			{
				IL_2C:
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_AD:
					if (!this.ᜌ)
					{
						goto IL_56;
					}
					num = 4;
					break;
				default:
					if (false)
					{
					}
					num2 = A_0 + 1;
					num = 2;
					break;
				}
				int num3;
				for (;;)
				{
					IL_02:
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_82;
					case 2:
						goto IL_CC;
					case 3:
						goto IL_CC;
					case 4:
					{
						object obj;
						this.ᜃ[obj] = num3;
						num = 1;
						continue;
					}
					case 5:
					{
						if (num2 >= A_1)
						{
							num = 0;
							continue;
						}
						object obj = this.ᜄ[num2];
						num3 = num2 - A_2;
						this.ᜄ[num3] = obj;
						num = 6;
						continue;
					}
					case 6:
						goto IL_AD;
					}
					goto IL_2C;
					IL_CC:
					num = 5;
				}
				IL_82:
				IL_56:
				A_3[num2] = num3;
				num2++;
				num = 3;
				goto IL_02;
			}
		}

		// Token: 0x06006131 RID: 24881 RVA: 0x003D7198 File Offset: 0x003D6198
		private void ᜁ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int count = this.Count;
					int count2 = this.ᜅ.Count;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							IList<int> values = this.ᜅ.Values;
							int num2 = 1;
							int count3 = this.ᜅ.Count;
							goto IL_C2;
						}
						case 1:
						{
							int num3;
							if (num3 >= count)
							{
								num = 0;
								continue;
							}
							List<int> list;
							list.Add(num3);
							num3++;
							num = 8;
							continue;
						}
						case 2:
							goto IL_173;
						case 3:
						{
							int num2;
							int count3;
							if (num2 < count3)
							{
								IList<int> values;
								int num4 = values[num2];
								List<int> list;
								int num5;
								this.ᜀ(num5, num4, num2, list);
								num5 = num4;
								num2++;
								num = 2;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C2;
							default:
								if (false)
								{
								}
								num = 7;
								continue;
							}
							break;
						}
						case 4:
							if (true)
							{
							}
							goto IL_1B5;
						case 5:
							goto IL_173;
						case 6:
							if (count2 > 0)
							{
								num = 9;
								continue;
							}
							return;
						case 7:
						{
							List<int> list;
							int num5;
							this.ᜀ(num5, count, this.ᜅ.Count, list);
							num5 = count - count2;
							this.ᜄ.RemoveRange(num5, count2);
							this.ᜅ.Clear();
							this.ᜆ.UpdateStringIndexes(list);
							num = 10;
							continue;
						}
						case 8:
							goto IL_1B5;
						case 9:
						{
							int num5 = this.ᜅ.Values[0];
							List<int> list = new List<int>(count + 1);
							int num3 = 0;
							num = 4;
							continue;
						}
						case 10:
							return;
						}
						break;
						IL_C2:
						num = 5;
						continue;
						IL_173:
						num = 3;
						continue;
						IL_1B5:
						num = 1;
					}
				}
				return;
			}
		}

		// Token: 0x06006132 RID: 24882 RVA: 0x003D738C File Offset: 0x003D638C
		private void ᜀ(RecordArrayList A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 5;
				spr\u24AD spr_u24AD;
				for (;;)
				{
					int num2;
					int num3;
					int num4;
					sprỪ sprỪ;
					int num5;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						goto IL_121;
					case 1:
						goto IL_121;
					case 2:
						goto IL_8A;
					case 3:
						if (num2 < 8)
						{
							num = 10;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9F;
						default:
							if (false)
							{
							}
							num = 9;
							continue;
						}
						break;
					case 4:
						if (num3 >= spr_u24AD.ᜄ().Length)
						{
							goto IL_9F;
						}
						spr_u24AD.ᜄ()[num3] = new spr\u19CA();
						num3++;
						num = 6;
						continue;
					case 6:
						goto IL_8A;
					case 7:
						goto IL_AB;
					case 8:
						num4 = num2;
						goto IL_17A;
					case 9:
						num = 8;
						continue;
					case 10:
						num4 = 8;
						goto IL_17A;
					case 11:
						sprỪ = (sprỪ)spr\u175E.ᜀ(TBIFFRecord.SST);
						sprỪ.ᜀ(this.Keys);
						num5 = this.ᜄ.Count;
						sprỪ.ᜀ((uint)num5);
						num = 1;
						continue;
					}
					if (this.ᜈ)
					{
						num = 11;
						continue;
					}
					sprỪ = this.ᜇ;
					num5 = (int)sprỪ.ᜃ();
					num = 0;
					continue;
					IL_8A:
					num = 4;
					continue;
					IL_9F:
					num = 7;
					continue;
					IL_121:
					A_0.ᜀ(sprỪ);
					num2 = num5 / 126;
					num = 3;
					continue;
					IL_17A:
					num2 = num4;
					spr_u24AD = (spr\u24AD)spr\u175E.ᜀ(TBIFFRecord.ExtSST);
					spr_u24AD.ᜀ((ushort)num2);
					spr_u24AD.ᜀ(new spr\u19CA[(ulong)sprỪ.ᜇ() / (ulong)((long)num2) + 1UL]);
					spr_u24AD.ᜀ(sprỪ);
					num3 = 0;
					num = 2;
				}
				IL_AB:
				A_0.ᜀ(spr_u24AD);
				return;
			}
			}
		}

		// Token: 0x06006133 RID: 24883 RVA: 0x003D7574 File Offset: 0x003D6574
		private void ᜀ(int A_0, int A_1)
		{
			int num;
			for (;;)
			{
				num = A_0 * 4;
				int num2 = num + 4;
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_B1;
					case 1:
						if (num2 > this.ᜊ.Capacity)
						{
							num3 = 2;
							continue;
						}
						goto IL_D3;
					case 2:
						num3 = 3;
						continue;
					case 3:
						if (this.ᜆ != null)
						{
							num3 = 4;
							continue;
						}
						goto IL_55;
					case 4:
						this.ᜊ.EnsureCapacity(num2, this.ᜆ.MaxImportRows);
						num3 = 0;
						continue;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_55;
						default:
							goto IL_7F;
						}
						break;
					}
					break;
					IL_55:
					this.ᜊ.EnsureCapacity(num2);
					num3 = 5;
				}
			}
			IL_7F:
			if (true)
			{
			}
			if (false)
			{
			}
			IL_B1:
			IL_D3:
			this.ᜊ.WriteInt32(num, A_1);
		}

		// Token: 0x06006134 RID: 24884 RVA: 0x003D7664 File Offset: 0x003D6664
		private int ᜀ(int A_0)
		{
			if (this.ᜆ.AppImplementation.ᜯ() == DataProviderType.ByteArray)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_35;
					}
				}
				IL_35:
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜊ.ReadInt32(A_0 * 2);
			}
			return this.ᜊ.ReadInt32(A_0 * 4);
		}

		// Token: 0x06006135 RID: 24885 RVA: 0x003D76D4 File Offset: 0x003D66D4
		private int ᜀ(spr\u223A A_0)
		{
			switch (0)
			{
			default:
			{
				int result;
				for (;;)
				{
					result = -1;
					int num = 18;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (A_0.ᜆ() == 0)
							{
								num = 9;
								continue;
							}
							return result;
						case 1:
						{
							object obj;
							if (obj != null)
							{
								num = 4;
								continue;
							}
							goto IL_206;
						}
						case 2:
							return result;
						case 3:
							if (A_0.ᜆ() == 0)
							{
								num = 22;
								continue;
							}
							goto IL_162;
						case 4:
							num = 3;
							continue;
						case 5:
							return result;
						case 6:
							goto IL_188;
						case 7:
						{
							object obj;
							if (!(A_0.ᜏ() == (string)obj))
							{
								num = 19;
								continue;
							}
							goto IL_131;
						}
						case 8:
						{
							object obj;
							if (A_0.ᜁ(obj) == 0)
							{
								num = 23;
								continue;
							}
							goto IL_206;
						}
						case 9:
							num = 13;
							continue;
						case 10:
							return result;
						case 11:
						{
							object obj;
							if (obj is string)
							{
								num = 16;
								continue;
							}
							goto IL_162;
						}
						case 12:
							goto IL_A7;
						case 13:
							if (this.ᜃ.ContainsKey(A_0.ᜏ()))
							{
								num = 15;
								continue;
							}
							return result;
						case 14:
							result = this.ᜃ[A_0];
							num = 10;
							continue;
						case 15:
							result = this.ᜃ[A_0.ᜏ()];
							num = 17;
							continue;
						case 16:
							num = 7;
							continue;
						case 17:
							return result;
						case 18:
						{
							if (this.ᜌ)
							{
								num = 12;
								continue;
							}
							num2 = 0;
							int count = this.ᜄ.Count;
							num = 21;
							continue;
						}
						case 19:
							if (true)
							{
							}
							goto IL_162;
						case 20:
							if (this.ᜃ.ContainsKey(A_0))
							{
								num = 14;
								continue;
							}
							num = 0;
							continue;
						case 21:
							goto IL_188;
						case 22:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A7;
							default:
								if (false)
								{
								}
								num = 11;
								continue;
							}
							break;
						case 23:
							goto IL_131;
						case 24:
						{
							int count;
							if (num2 >= count)
							{
								num = 2;
								continue;
							}
							object obj = this.ᜄ[num2];
							num = 1;
							continue;
						}
						}
						break;
						IL_A7:
						num = 20;
						continue;
						IL_131:
						result = num2;
						num = 5;
						continue;
						IL_162:
						num = 8;
						continue;
						IL_188:
						num = 24;
						continue;
						IL_206:
						num2++;
						num = 6;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06006136 RID: 24886 RVA: 0x003D79B0 File Offset: 0x003D69B0
		private int ᜀ(object A_0)
		{
			switch (0)
			{
			default:
			{
				int result;
				for (;;)
				{
					result = -1;
					int num = 13;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							return result;
						case 1:
							result = this.ᜃ[A_0];
							if (true)
							{
							}
							num = 0;
							continue;
						case 2:
							goto IL_A9;
						case 3:
							return result;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A9;
							default:
								if (false)
								{
								}
								num = 9;
								continue;
							}
							break;
						case 5:
							result = num2;
							num = 3;
							continue;
						case 6:
						{
							object obj;
							if (obj.Equals(A_0))
							{
								num = 5;
								continue;
							}
							goto IL_13B;
						}
						case 7:
							return result;
						case 8:
						{
							object obj;
							if (obj != null)
							{
								num = 2;
								continue;
							}
							goto IL_13B;
						}
						case 9:
							if (this.ᜃ.ContainsKey(A_0))
							{
								num = 1;
								continue;
							}
							return result;
						case 10:
							goto IL_F2;
						case 11:
							goto IL_F2;
						case 12:
						{
							int count;
							if (num2 >= count)
							{
								num = 7;
								continue;
							}
							object obj = this.ᜄ[num2];
							num = 8;
							continue;
						}
						case 13:
						{
							if (this.ᜌ)
							{
								num = 4;
								continue;
							}
							num2 = 0;
							int count = this.ᜄ.Count;
							num = 11;
							continue;
						}
						}
						break;
						IL_A9:
						num = 6;
						continue;
						IL_F2:
						num = 12;
						continue;
						IL_13B:
						num2++;
						num = 10;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06006137 RID: 24887 RVA: 0x003D7B58 File Offset: 0x003D6B58
		private void ᜀ()
		{
			if (true)
			{
			}
			for (;;)
			{
				IL_2C:
				object obj;
				int num;
				int num2;
				int count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_9D:
					if (obj == null)
					{
						goto IL_60;
					}
					num = 4;
					break;
				default:
					if (false)
					{
					}
					num2 = 0;
					count = this.ᜄ.Count;
					num = 1;
					break;
				}
				for (;;)
				{
					IL_0A:
					switch (num)
					{
					case 0:
						if (num2 >= count)
						{
							num = 5;
							continue;
						}
						obj = this.ᜄ[num2];
						num = 3;
						continue;
					case 1:
						goto IL_B7;
					case 2:
						goto IL_B7;
					case 3:
						goto IL_9D;
					case 4:
						this.ᜃ[obj] = num2;
						num = 6;
						continue;
					case 5:
						return;
					case 6:
						goto IL_83;
					}
					goto IL_2C;
					IL_B7:
					num = 0;
				}
				IL_83:
				IL_60:
				num2++;
				num = 2;
				goto IL_0A;
			}
		}

		// Token: 0x06006138 RID: 24888 RVA: 0x003D7C38 File Offset: 0x003D6C38
		public void Parse()
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7F;
						default:
							if (false)
							{
							}
							goto IL_134;
						}
						break;
					case 1:
						goto IL_8D;
					case 2:
					{
						Dictionary<int, int> dictionary = new Dictionary<int, int>();
						object[] array = this.ᜇ.ᜊ();
						num2 = 0;
						num = 1;
						continue;
					}
					case 3:
					{
						Dictionary<int, int> dictionary;
						if (dictionary.Count > 0)
						{
							goto IL_7F;
						}
						goto IL_DB;
					}
					case 4:
						goto IL_8D;
					case 5:
					{
						object[] array;
						if (num2 >= array.Length)
						{
							if (true)
							{
							}
							num = 6;
							continue;
						}
						int num3 = this.AddIncrease(array[num2], false);
						num = 11;
						continue;
					}
					case 6:
						num = 3;
						continue;
					case 8:
						return;
					case 9:
					{
						Dictionary<int, int> dictionary;
						this.ᜀ(dictionary);
						num = 12;
						continue;
					}
					case 10:
					{
						Dictionary<int, int> dictionary;
						int num3;
						dictionary.Add(num2, num3);
						num = 0;
						continue;
					}
					case 11:
					{
						int num3;
						if (num2 != num3)
						{
							num = 10;
							continue;
						}
						goto IL_134;
					}
					case 12:
						goto IL_DB;
					}
					if (!this.ᜈ)
					{
						num = 2;
						continue;
					}
					break;
					IL_7F:
					num = 9;
					continue;
					IL_8D:
					num = 5;
					continue;
					IL_DB:
					this.ᜈ = true;
					this.ᜇ = null;
					num = 8;
					continue;
					IL_134:
					num2++;
					num = 4;
				}
				return;
			}
			}
		}

		// Token: 0x06006139 RID: 24889 RVA: 0x003D7DCC File Offset: 0x003D6DCC
		internal void ᜀ(Dictionary<int, int> A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IWorksheets worksheets = this.ᜆ.Worksheets;
					int num = 0;
					int count = worksheets.Count;
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
								goto IL_54;
							default:
								goto IL_7D;
							}
							break;
						case 1:
							if (true)
							{
							}
							goto IL_47;
						case 2:
						{
							if (num >= count)
							{
								goto IL_54;
							}
							XlsWorksheet xlsWorksheet = (XlsWorksheet)worksheets[num];
							xlsWorksheet.ᜀ(A_0, new spr\u202C(this.AddIncrease));
							num++;
							num2 = 1;
							continue;
						}
						case 3:
							goto IL_47;
						}
						break;
						IL_47:
						num2 = 2;
						continue;
						IL_54:
						num2 = 0;
					}
				}
				IL_7D:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x0600613A RID: 24890 RVA: 0x003D7E98 File Offset: 0x003D6E98
		public void Dispose()
		{
			int num = 0;
			for (;;)
			{
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
					switch (num)
					{
					case 1:
						return;
					case 2:
						goto IL_50;
					}
					if (this.ᜆ != null)
					{
						num = 2;
						continue;
					}
					return;
				}
				IL_50:
				this.ᜃ = null;
				this.ᜄ = null;
				this.ᜅ = null;
				this.ᜆ = null;
				this.ᜇ = null;
				this.ᜉ = null;
				this.ᜊ.Dispose();
				this.ᜊ = null;
				GC.SuppressFinalize(this);
				num = 1;
			}
		}

		// Token: 0x0600613B RID: 24891 RVA: 0x003D7F50 File Offset: 0x003D6F50
		protected override void Finalize()
		{
			try
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.Dispose();
			}
			finally
			{
				if (true)
				{
				}
				base.Finalize();
			}
		}

		// Token: 0x04002E6B RID: 11883
		private const int ᜀ = 20;

		// Token: 0x04002E6C RID: 11884
		public const int DEF_EMPTY_STRING_INDEX = -1;

		// Token: 0x04002E6D RID: 11885
		private byte \u2609\u0087\u00AC\u00A3;

		// Token: 0x04002E6E RID: 11886
		private const int ᜁ = 2;

		// Token: 0x04002E6F RID: 11887
		private const int ᜂ = 32767;

		// Token: 0x04002E70 RID: 11888
		private Dictionary<object, int> ᜃ = new Dictionary<object, int>(20);

		// Token: 0x04002E71 RID: 11889
		private List<object> ᜄ = new List<object>(20);

		// Token: 0x04002E72 RID: 11890
		private SortedList<int, int> ᜅ = new SortedList<int, int>(20);

		// Token: 0x04002E73 RID: 11891
		private XlsWorkbook ᜆ;

		// Token: 0x04002E74 RID: 11892
		private sprỪ ᜇ;

		// Token: 0x04002E75 RID: 11893
		private bool ᜈ = true;

		// Token: 0x04002E76 RID: 11894
		private spr\u223A ᜉ = new spr\u223A(0);

		// Token: 0x04002E77 RID: 11895
		private DataProvider ᜊ;

		// Token: 0x04002E78 RID: 11896
		private int ᜋ = 1000;

		// Token: 0x04002E79 RID: 11897
		private bool ᜌ = true;
	}
}
