using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

namespace Spire.DataExport.Collections
{
	// Token: 0x02000207 RID: 519
	public class StringListCollection : CollectionBase, ICloneable
	{
		// Token: 0x06000F8B RID: 3979 RVA: 0x000A68F4 File Offset: 0x000A58F4
		public object Clone()
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				StringListCollection stringListCollection = new StringListCollection();
				stringListCollection.Duplicates = this.Duplicates;
				stringListCollection.Sorted = this.Sorted;
				IEnumerator enumerator = base.GetEnumerator();
				try
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 3;
							continue;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							string item = (string)enumerator.Current;
							stringListCollection.Add(item);
							num = 1;
							continue;
						}
						case 3:
							goto IL_B3;
						}
						IL_8E:
						num = 2;
						continue;
						goto IL_8E;
					}
					IL_B3:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_F7;
							case 1:
								disposable.Dispose();
								num = 0;
								continue;
							case 2:
								if (disposable != null)
								{
									num = 1;
									continue;
								}
								goto IL_115;
							}
							break;
						}
					}
					IL_F7:
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
					IL_115:;
				}
				return stringListCollection;
			}
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x000A6A28 File Offset: 0x000A5A28
		public int Add(object Item)
		{
			int a_ = 15;
			Duplicates duplicates;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_73:
				duplicates = this.ᜁ;
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_4D;
			}
			int num2;
			for (;;)
			{
				IL_27:
				switch (num)
				{
				case 0:
					goto IL_B5;
				case 1:
					switch (duplicates)
					{
					case Duplicates.Ignore:
						return num2;
					case Duplicates.Accept:
						goto IL_117;
					case Duplicates.Error:
						goto IL_B7;
					default:
						num = 2;
						continue;
					}
					break;
				case 2:
					num = 5;
					continue;
				case 3:
					num2 = base.InnerList.Count;
					num = 0;
					continue;
				case 4:
					if (this.Find((string)Item, ref num2))
					{
						num = 6;
						continue;
					}
					goto IL_117;
				case 5:
					goto IL_115;
				case 6:
					goto IL_105;
				case 7:
					if (!this.ᜀ)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					num = 4;
					continue;
				}
				goto IL_4D;
			}
			return num2;
			IL_B5:
			goto IL_117;
			IL_B7:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("截䌬央倰弲尴匶瘸䬺堼䴾⁀㝂ⱄ⡆❈ᑊौ㩎⅐㽒㱔㑖㡘⽚㡜౞ᕠᅢ౤०๨", a_)));
			IL_105:
			goto IL_73;
			IL_115:
			IL_117:
			base.InnerList.Insert(num2, Item);
			return num2;
			IL_4D:
			num2 = 0;
			num = 7;
			goto IL_27;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x000A6B5C File Offset: 0x000A5B5C
		public void AddRange(object[] Items)
		{
			int num = 0;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 1:
					goto IL_68;
				case 2:
					return;
				case 3:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F;
					default:
						goto IL_9E;
					}
					break;
				case 4:
					if (num2 >= Items.Length)
					{
						num = 3;
						continue;
					}
					goto IL_3F;
				case 5:
					goto IL_68;
				}
				if (Items == null)
				{
					num = 2;
					continue;
				}
				num2 = 0;
				num = 1;
				continue;
				IL_3F:
				object item = Items[num2];
				this.Add(item);
				num2++;
				num = 5;
				continue;
				IL_68:
				num = 4;
			}
			return;
			IL_9E:
			if (false)
			{
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x000A6C10 File Offset: 0x000A5C10
		public void Insert(int Index, object Item)
		{
			int a_ = 7;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜀ)
					{
						num = 1;
						continue;
					}
					goto IL_F7;
				case 1:
					goto IL_76;
				case 2:
					goto IL_D7;
				case 3:
					num = 4;
					continue;
				case 4:
					if (Index > base.InnerList.Count)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
				if (Index < 0)
				{
					goto IL_78;
				}
				num = 3;
			}
			IL_76:
			throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("樢䬤儦䠨䜪䐬䬮縰䌲倴䔶堸伺吼倾⽀᱂ᙄ⡆㭈㽊⡌⭎ᵐ㩒♔⍖", a_)));
			IL_78:
			throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("樢䬤儦䠨䜪䐬䬮縰䌲倴䔶堸伺吼倾⽀᱂ౄ⥆ⵈ⹊㕌N⑐❒ᩔㅖ᭘㑚⡜ㅞՠၢ", a_)), Index));
			IL_D7:
			goto IL_78;
			IL_F7:
			base.InnerList.Insert(Index, Item);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000A6D24 File Offset: 0x000A5D24
		public void Delete(int Index)
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
			base.InnerList.RemoveAt(Index);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000A6D6C File Offset: 0x000A5D6C
		public bool Find(string Str, ref int Index)
		{
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
				num = base.InnerList.BinarySearch(Str, new StringListCollection.ᜀ(SortOrder.Ascending));
				if (num >= 0)
				{
					Index = num;
					return true;
				}
				break;
			}
			Index = ~num;
			return false;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000A6DCC File Offset: 0x000A5DCC
		public int IndexOf(string Str)
		{
			int result;
			for (;;)
			{
				result = 0;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						IL_4C:
						result = base.InnerList.IndexOf(Str);
						num = 4;
						continue;
					case 1:
						result = -1;
						num = 5;
						continue;
					case 2:
						if (!this.ᜀ)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						num = 3;
						continue;
					case 3:
						if (!this.Find(Str, ref result))
						{
							num = 1;
							continue;
						}
						goto IL_8E;
					case 4:
						goto IL_8E;
					case 5:
						goto IL_8E;
					}
					break;
					IL_8E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C;
					default:
						goto IL_A4;
					}
				}
			}
			IL_A4:
			if (false)
			{
			}
			return result;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000A6E84 File Offset: 0x000A5E84
		public int IndexOfName(string Name)
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_6F;
					case 1:
						goto IL_65;
					case 2:
						if (num >= base.InnerList.Count)
						{
							num2 = 4;
							continue;
						}
						num2 = 3;
						continue;
					case 3:
						if (string.Compare(Name, this.GetName(num)) == 0)
						{
							num2 = 1;
							continue;
						}
						num++;
						goto IL_3A;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3A;
						default:
							goto IL_A8;
						}
						break;
					case 5:
						goto IL_6F;
					}
					break;
					IL_3A:
					num2 = 0;
					continue;
					IL_6F:
					num2 = 2;
				}
			}
			IL_65:
			if (true)
			{
			}
			return num;
			IL_A8:
			if (false)
			{
			}
			return -1;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000A6F40 File Offset: 0x000A5F40
		public int IndexOfValue(string Value)
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return num;
					case 1:
						if (true)
						{
						}
						goto IL_6F;
					case 2:
						goto IL_6F;
					case 3:
						if (num >= base.InnerList.Count)
						{
							num2 = 4;
							continue;
						}
						num2 = 5;
						continue;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42;
						default:
							goto IL_A8;
						}
						break;
					case 5:
						if (string.Compare(Value, this.GetValueByIndex(num)) == 0)
						{
							num2 = 0;
							continue;
						}
						num++;
						goto IL_42;
					}
					break;
					IL_42:
					num2 = 2;
					continue;
					IL_6F:
					num2 = 3;
				}
			}
			return num;
			IL_A8:
			if (false)
			{
			}
			return -1;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000A6FFC File Offset: 0x000A5FFC
		public string GetName(int Index)
		{
			int a_ = 14;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_55:
				if (Index < 0)
				{
					goto IL_F9;
				}
				num = 6;
				break;
			default:
				if (false)
				{
				}
				num = 7;
				break;
			}
			string[] array;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					string text;
					if (text.IndexOf('=') > 0)
					{
						num = 2;
						continue;
					}
					goto IL_122;
				}
				case 1:
				{
					if (true)
					{
					}
					if (Index >= base.InnerList.Count)
					{
						num = 5;
						continue;
					}
					string text = this[Index];
					num = 0;
					continue;
				}
				case 2:
				{
					string text;
					array = text.Split(new char[]
					{
						'='
					});
					num = 4;
					continue;
				}
				case 3:
					goto IL_93;
				case 4:
					if (array.Length == 2)
					{
						num = 3;
						continue;
					}
					goto IL_122;
				case 5:
					goto IL_F3;
				case 6:
					num = 1;
					continue;
				}
				break;
			}
			goto IL_55;
			IL_93:
			return array[0];
			IL_F3:
			goto IL_F9;
			IL_122:
			return string.Empty;
			IL_F9:
			throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("挩䈫堭儯帱崳刵眷䨹夻䰽ℿ㙁ⵃ⥅♇ᕉՋ⁍㑏㝑ⱓᥕⵗ⹙፛㡝≟ൡᅣࡥ౧ᥩ", a_)), Index));
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000A7130 File Offset: 0x000A6130
		public void SetName(int Index, string Name)
		{
			for (;;)
			{
				IL_1C:
				int num;
				string name;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_82:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					name = this.GetName(Index);
					num = 1;
					break;
				}
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (name.Length > 0)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						goto IL_59;
					}
					goto IL_1C;
				}
				IL_59:
				string value = Name + '=' + this.GetValueByIndex(Index);
				this[Index] = value;
				goto IL_82;
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000A71CC File Offset: 0x000A61CC
		public string GetValueByIndex(int Index)
		{
			int a_ = 2;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_55:
				if (Index < 0)
				{
					goto IL_FC;
				}
				num = 5;
				break;
			default:
				if (false)
				{
				}
				num = 0;
				break;
			}
			string[] array;
			for (;;)
			{
				switch (num)
				{
				case 1:
				{
					string text;
					array = text.Split(new char[]
					{
						'='
					});
					num = 4;
					continue;
				}
				case 2:
					goto IL_F6;
				case 3:
				{
					string text;
					if (text.IndexOf('=') > 0)
					{
						num = 1;
						continue;
					}
					return text;
				}
				case 4:
				{
					if (array.Length == 2)
					{
						num = 7;
						continue;
					}
					string text;
					return text;
				}
				case 5:
					if (true)
					{
					}
					num = 6;
					continue;
				case 6:
				{
					if (Index >= base.InnerList.Count)
					{
						num = 2;
						continue;
					}
					string text = this[Index];
					num = 3;
					continue;
				}
				case 7:
					goto IL_9B;
				}
				break;
			}
			goto IL_55;
			IL_9B:
			return array[1];
			IL_F6:
			IL_FC:
			throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("圝丟吡䔣䨥䄧丩挫席唯䀱唳䈵儷唹刻愽िⱁ⁃⍅ぇՉ㥋㩍὏㑑ᙓ㥕ⵗ㑙㡛ⵝ", a_)), Index));
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000A7300 File Offset: 0x000A6300
		public void SetValueByIndex(int Index, string Value)
		{
			string text;
			for (;;)
			{
				text = this[Index];
				string valueByIndex = this.GetValueByIndex(Index);
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
							goto IL_4D;
						default:
							goto IL_6B;
						}
						break;
					case 1:
						if (true)
						{
						}
						if (string.Compare(text, valueByIndex) != 0)
						{
							num = 2;
							continue;
						}
						text = Value;
						goto IL_4D;
					case 2:
						text = this.GetName(Index) + '=' + Value;
						num = 3;
						continue;
					case 3:
						goto IL_93;
					}
					break;
					IL_4D:
					num = 0;
				}
			}
			IL_6B:
			if (false)
			{
			}
			IL_93:
			this[Index] = text;
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000A73B4 File Offset: 0x000A63B4
		public string GetValue(string Name)
		{
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
				num = this.IndexOfName(Name);
				if (num <= -1)
				{
					return string.Empty;
				}
				break;
			}
			return this.GetValueByIndex(num);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000A740C File Offset: 0x000A640C
		public void SetValue(string Name, string Value)
		{
			for (;;)
			{
				IL_14:
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_77:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num2 = this.IndexOfName(Name);
					if (true)
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
						goto IL_54;
					case 1:
						if (num2 > -1)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						return;
					}
					goto IL_14;
				}
				IL_54:
				string value = Name + '=' + Value;
				this[num2] = value;
				goto IL_77;
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000A749C File Offset: 0x000A649C
		public void Sort(SortOrder SortOrder)
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
			base.InnerList.Sort(new StringListCollection.ᜀ(SortOrder));
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000A74E8 File Offset: 0x000A64E8
		public string[] GetStrings()
		{
			string[] array;
			for (;;)
			{
				array = new string[base.Count];
				int num = 0;
				int num2 = 3;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_30;
					case 1:
						if (num < base.Count)
						{
							if (true)
							{
							}
							array[num] = this[num];
							num++;
							num2 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 2:
						return array;
					case 3:
						goto IL_30;
					}
					break;
					IL_30:
					num2 = 1;
				}
			}
			return array;
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000A758C File Offset: 0x000A658C
		public void SetStrings(string[] Strings)
		{
			for (;;)
			{
				base.Clear();
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num < Strings.Length)
						{
							string item = Strings[num];
							this.Add(item);
							num++;
							num2 = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 1:
						goto IL_2C;
					case 2:
						return;
					case 3:
						goto IL_2C;
					}
					break;
					IL_2C:
					if (true)
					{
					}
					num2 = 0;
				}
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000A7628 File Offset: 0x000A6628
		public void SetStrings(string[] Strings, bool Sorted)
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
			this.Sorted = Sorted;
			this.SetStrings(Strings);
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x000A7674 File Offset: 0x000A6674
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x000A76B8 File Offset: 0x000A66B8
		public bool Sorted
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
				return this.ᜀ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value)
						{
							num = 4;
							continue;
						}
						goto IL_3B;
					case 1:
						num = 0;
						continue;
					case 2:
						return;
					case 4:
						if (true)
						{
						}
						this.Sort(SortOrder.Ascending);
						num = 5;
						continue;
					case 5:
						goto IL_3B;
					}
					if (value != this.ᜀ)
					{
						num = 1;
						continue;
					}
					break;
					IL_3B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜀ = value;
						num = 2;
						break;
					}
				}
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x000A776C File Offset: 0x000A676C
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x000A77B0 File Offset: 0x000A67B0
		public Duplicates Duplicates
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
				return this.ᜁ;
			}
			set
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
				this.ᜁ = value;
			}
		}

		// Token: 0x17000217 RID: 535
		public string this[int Index]
		{
			get
			{
				int a_ = 15;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_35;
					case 1:
						if (Index < base.InnerList.Count)
						{
							goto IL_B1;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_AF;
					}
					if (Index >= 0)
					{
						num = 0;
						continue;
					}
					break;
					IL_35:
					num = 1;
				}
				IL_37:
				throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("截䌬央倰弲尴匶瘸䬺堼䴾⁀㝂ⱄ⡆❈ᑊьⅎ㕐㙒ⵔᡖⱘ⽚ቜ㥞⍠ౢၤ०൨ᡪ", a_)), Index));
				IL_AF:
				goto IL_37;
				IL_B1:
				return (string)base.InnerList[Index];
			}
			set
			{
				int a_ = 6;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_A7;
					case 1:
						goto IL_35;
					case 3:
						if (Index < base.InnerList.Count)
						{
							goto IL_A9;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_35;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (Index >= 0)
					{
						num = 1;
						continue;
					}
					break;
					IL_35:
					num = 3;
				}
				IL_37:
				throw new ArgumentOutOfRangeException(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("次䨣倥䤧䘩䔫䨭缯䈱儳䐵夷丹唻儽⸿ᵁൃ⡅ⱇ⽉㑋ō╏♑᭓さᩗ㕙⥛そџᅡ", a_)), Index));
				IL_A7:
				goto IL_37;
				IL_A9:
				if (true)
				{
				}
				base.InnerList[Index] = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x000A7990 File Offset: 0x000A6990
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x000A7A7C File Offset: 0x000A6A7C
		public string Text
		{
			get
			{
				int a_ = 11;
				switch (0)
				{
				default:
				{
					StringBuilder stringBuilder;
					for (;;)
					{
						string[] strings = this.GetStrings();
						stringBuilder = new StringBuilder(strings.Length);
						string[] array = strings;
						int num = 0;
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
									goto IL_60;
								default:
									goto IL_7F;
								}
								break;
							case 1:
								goto IL_50;
							case 2:
							{
								if (num >= array.Length)
								{
									goto IL_60;
								}
								string arg = array[num];
								stringBuilder.AppendFormat(HyperlinksCollectionEditor.b("尦ᤨ嘪嘬Ḯ䰰", a_), arg, HyperlinksCollectionEditor.b("⨦⌨", a_));
								num++;
								if (true)
								{
								}
								num2 = 1;
								continue;
							}
							case 3:
								goto IL_50;
							}
							break;
							IL_50:
							num2 = 2;
							continue;
							IL_60:
							num2 = 0;
						}
					}
					IL_7F:
					if (false)
					{
					}
					return stringBuilder.ToString();
				}
				}
			}
			set
			{
				int a_ = 8;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				string value2 = value.TrimEnd(new char[]
				{
					'\r',
					'\n'
				});
				StringBuilder stringBuilder = new StringBuilder(value2);
				stringBuilder.Replace(HyperlinksCollectionEditor.b("⤣Ⱕ", a_), HyperlinksCollectionEditor.b("␣", a_));
				string text = stringBuilder.ToString();
				char[] separator = new char[1];
				string[] strings = text.Split(separator);
				this.SetStrings(strings);
			}
		}

		// Token: 0x04000B81 RID: 2945
		private bool ᜀ;

		// Token: 0x04000B82 RID: 2946
		private Duplicates ᜁ;

		// Token: 0x02000208 RID: 520
		private class ᜀ : IComparer
		{
			// Token: 0x06000FA6 RID: 4006 RVA: 0x000A7B20 File Offset: 0x000A6B20
			public ᜀ(SortOrder A_0)
			{
				this.ᜀ = A_0;
			}

			// Token: 0x06000FA7 RID: 4007 RVA: 0x000A7B3C File Offset: 0x000A6B3C
			int IComparer.ᜁ(object A_0, object A_1)
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_79;
					case 2:
						return 0;
					case 3:
						if (this.ᜀ != SortOrder.Ascending)
						{
							num = 0;
							continue;
						}
						goto IL_87;
					}
					if (this.ᜀ != SortOrder.None)
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
							num = 3;
							continue;
						}
					}
					if (true)
					{
					}
					num = 2;
				}
				return 0;
				IL_79:
				return -this.ᜀ(A_0, A_1);
				IL_87:
				return this.ᜀ(A_0, A_1);
			}

			// Token: 0x06000FA8 RID: 4008 RVA: 0x000A7BD8 File Offset: 0x000A6BD8
			private int ᜀ(object A_0, object A_1)
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
				return string.Compare((string)A_0, (string)A_1);
			}

			// Token: 0x04000B83 RID: 2947
			private SortOrder ᜀ;
		}
	}
}
