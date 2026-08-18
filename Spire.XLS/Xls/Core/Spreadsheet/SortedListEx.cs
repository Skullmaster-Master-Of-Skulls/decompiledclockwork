using System;
using System.Collections;
using System.Reflection;
using System.Threading;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200062C RID: 1580
	[Serializable]
	public class SortedListEx : IDictionary, ICloneable
	{
		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x0600609E RID: 24734 RVA: 0x003D2684 File Offset: 0x003D1684
		// (set) Token: 0x0600609F RID: 24735 RVA: 0x003D26C8 File Offset: 0x003D16C8
		public virtual int Capacity
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
				return this.ᜁ.Length;
			}
			set
			{
				int a_ = 12;
				int num = 8;
				object[] destinationArray;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value < this.ᜃ)
						{
							num = 3;
							continue;
						}
						num = 2;
						continue;
					case 1:
						destinationArray = new object[value];
						goto IL_A8;
					case 2:
						if (value > 0)
						{
							num = 1;
							continue;
						}
						this.ᜁ = new object[16];
						num = 5;
						continue;
					case 3:
						goto IL_116;
					case 4:
						goto IL_9F;
					case 5:
						goto IL_F5;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A8;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 7:
						if (true)
						{
						}
						Array.Copy(this.ᜁ, 0, destinationArray, 0, this.ᜃ);
						num = 4;
						continue;
					case 9:
						if (this.ᜃ > 0)
						{
							num = 7;
							continue;
						}
						goto IL_118;
					}
					if (value != this.ᜁ.Length)
					{
						num = 6;
						continue;
					}
					return;
					IL_A8:
					num = 9;
				}
				IL_9F:
				goto IL_118;
				IL_F5:
				return;
				IL_116:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㑁╃⩅㵇⽉", a_));
				IL_118:
				this.ᜁ = destinationArray;
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x060060A0 RID: 24736 RVA: 0x003D2820 File Offset: 0x003D1820
		public virtual int Count
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
				return this.ᜃ;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x060060A1 RID: 24737 RVA: 0x003D2864 File Offset: 0x003D1864
		public virtual ICollection Keys
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
				return this.GetKeyList();
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x060060A2 RID: 24738 RVA: 0x003D28A8 File Offset: 0x003D18A8
		public virtual ICollection Values
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
				return this.GetValueList();
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x060060A3 RID: 24739 RVA: 0x003D28EC File Offset: 0x003D18EC
		public virtual bool IsReadOnly
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
				return false;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x060060A4 RID: 24740 RVA: 0x003D2928 File Offset: 0x003D1928
		public virtual bool IsFixedSize
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
				return false;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x060060A5 RID: 24741 RVA: 0x003D2964 File Offset: 0x003D1964
		public virtual bool IsSynchronized
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
				return false;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x060060A6 RID: 24742 RVA: 0x003D29A0 File Offset: 0x003D19A0
		public virtual object SyncRoot
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
				return this;
			}
		}

		// Token: 0x17000FED RID: 4077
		public virtual object this[object key]
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
				return this.ᜂ[key];
			}
			set
			{
				int a_ = 1;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜂ.ContainsKey(key))
						{
							goto IL_99;
						}
						this.Add(key, value);
						num = 2;
						continue;
					case 1:
						this.ᜂ[key] = value;
						num = 3;
						continue;
					case 2:
						goto IL_CD;
					case 3:
						goto IL_5B;
					case 4:
						if (true)
						{
						}
						break;
					case 5:
						goto IL_44;
					}
					if (key == null)
					{
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
						num = 0;
						continue;
					}
					IL_99:
					num = 1;
				}
				IL_44:
				throw new ArgumentNullException(RecordTableEnumerator.b("尶尸䈺", a_));
				IL_5B:
				IL_CD:
				this.ᜄ++;
			}
		}

		// Token: 0x060060A9 RID: 24745 RVA: 0x003D2B10 File Offset: 0x003D1B10
		public SortedListEx()
		{
			this.ᜁ = new object[16];
			this.ᜂ = new Hashtable(16);
			this.ᜅ = Comparer.Default;
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x003D2B48 File Offset: 0x003D1B48
		public SortedListEx(int initialCapacity)
		{
			int a_ = 17;
			base..ctor();
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⹆❈≊㥌♎ぐ㽒ᙔ㙖⥘㩚㹜㙞ᕠᩢ", a_));
			}
			this.ᜁ = new object[initialCapacity];
			this.ᜂ = new Hashtable(initialCapacity);
			this.ᜅ = Comparer.Default;
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x003D2BA4 File Offset: 0x003D1BA4
		public SortedListEx(IComparer comparer) : this()
		{
			if (comparer != null)
			{
				this.ᜅ = comparer;
			}
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x003D2BC8 File Offset: 0x003D1BC8
		public SortedListEx(IComparer comparer, int capacity) : this(comparer)
		{
			this.Capacity = capacity;
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x003D2BE4 File Offset: 0x003D1BE4
		public SortedListEx(IDictionary d) : this(d, null)
		{
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x003D2BFC File Offset: 0x003D1BFC
		public SortedListEx(IDictionary d, IComparer comparer)
		{
			int a_ = 3;
			this..ctor(comparer, (d != null) ? d.Count : 0);
			if (d == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("崸", a_));
			}
			d.Keys.CopyTo(this.ᜁ, 0);
			this.ᜂ = new Hashtable(d);
			Array.Sort(this.ᜁ, comparer);
			this.ᜃ = d.Count;
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x003D2C78 File Offset: 0x003D1C78
		public static SortedListEx Synchronized(SortedListEx list)
		{
			int a_ = 4;
			if (list != null)
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
					return new SortedListEx.ᜀ(list);
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("嘹唻䴽㐿", a_));
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x003D2CDC File Offset: 0x003D1CDC
		public virtual void Add(object key, object value)
		{
			int a_ = 18;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3C;
				case 2:
					if (this.ᜂ.ContainsKey(key))
					{
						num = 3;
						continue;
					}
					goto IL_AF;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6E;
					default:
						goto IL_54;
					}
					break;
				}
				if (true)
				{
				}
				if (key == null)
				{
					num = 0;
					continue;
				}
				IL_6E:
				num = 2;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("⍇⽉㕋", a_));
			IL_54:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("ే㽉㱋≍㥏ㅑ㕓≕㵗㹙", a_));
			IL_AF:
			int num2 = Array.BinarySearch(this.ᜁ, 0, this.ᜃ, key, this.ᜅ);
			this.ᜀ(~num2, key, value);
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x003D2DBC File Offset: 0x003D1DBC
		public virtual void Clear()
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
			this.ᜄ++;
			this.ᜃ = 0;
			this.ᜁ = new object[16];
			this.ᜂ = new Hashtable(16);
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x003D2E28 File Offset: 0x003D1E28
		public virtual object Clone()
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
			SortedListEx sortedListEx = new SortedListEx(this.ᜃ);
			Array.Copy(this.ᜁ, 0, sortedListEx.ᜁ, 0, this.ᜃ);
			sortedListEx.ᜂ = new Hashtable(this.ᜂ);
			sortedListEx.ᜃ = this.ᜃ;
			sortedListEx.ᜄ = this.ᜄ;
			sortedListEx.ᜅ = this.ᜅ;
			return sortedListEx;
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x003D2EC0 File Offset: 0x003D1EC0
		public SortedListEx CloneAll()
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				SortedListEx sortedListEx;
				for (;;)
				{
					int count = this.Count;
					sortedListEx = (SortedListEx)base.MemberwiseClone();
					sortedListEx.ᜁ = new object[count];
					sortedListEx.ᜂ = new Hashtable(count);
					sortedListEx.ᜆ = null;
					sortedListEx.ᜇ = null;
					sortedListEx.ᜃ = 0;
					int num = 0;
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 5;
								continue;
							}
							object key = this.GetKey(num);
							object obj = this.ᜂ[key];
							ICloneable cloneable = obj as ICloneable;
							num2 = 2;
							continue;
						}
						case 1:
							goto IL_120;
						case 2:
						{
							ICloneable cloneable;
							if (cloneable != null)
							{
								num2 = 6;
								continue;
							}
							goto IL_95;
						}
						case 3:
							goto IL_120;
						case 4:
							IL_E1:
							goto IL_95;
						case 5:
							return sortedListEx;
						case 6:
						{
							ICloneable cloneable;
							object obj = cloneable.Clone();
							num2 = 4;
							continue;
						}
						}
						break;
						IL_95:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E1;
						default:
						{
							if (false)
							{
							}
							object key;
							object obj;
							sortedListEx.Add(key, obj);
							num++;
							num2 = 1;
							continue;
						}
						}
						IL_120:
						num2 = 0;
					}
				}
				return sortedListEx;
			}
			}
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x003D300C File Offset: 0x003D200C
		public virtual bool Contains(object key)
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
			return this.ᜂ.ContainsKey(key);
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x003D3054 File Offset: 0x003D2054
		public virtual bool ContainsKey(object key)
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
			return this.ᜂ.ContainsKey(key);
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x003D309C File Offset: 0x003D209C
		public virtual bool ContainsValue(object value)
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
			return this.ᜂ.ContainsValue(value);
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x003D30E4 File Offset: 0x003D20E4
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			int a_ = 9;
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (arrayIndex < 0)
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
				case 1:
					goto IL_17C;
				case 2:
					if (array.Rank != 1)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 3:
					goto IL_134;
				case 4:
				{
					if (array.Length - arrayIndex < this.Count)
					{
						num = 8;
						continue;
					}
					int num2 = 0;
					num = 3;
					continue;
				}
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C;
					default:
						goto IL_CD;
					}
					break;
				case 6:
					goto IL_5C;
				case 7:
					return;
				case 8:
					goto IL_90;
				case 9:
				{
					int num2;
					if (num2 >= this.Count)
					{
						num = 7;
						continue;
					}
					DictionaryEntry dictionaryEntry = new DictionaryEntry(this.ᜁ[num2], this.ᜂ[this.ᜁ[num2]]);
					array.SetValue(dictionaryEntry, num2 + arrayIndex);
					num2++;
					num = 10;
					continue;
				}
				case 10:
					goto IL_134;
				}
				goto IL_49;
				IL_4C:
				if (true)
				{
				}
				num = 6;
				continue;
				IL_49:
				if (array == null)
				{
					goto IL_4C;
				}
				num = 2;
				continue;
				IL_134:
				num = 9;
			}
			IL_5C:
			throw new ArgumentNullException(RecordTableEnumerator.b("帾㍀ㅂ⑄㹆", a_));
			IL_90:
			throw new ArgumentException();
			IL_CD:
			if (false)
			{
			}
			throw new ArgumentException();
			IL_17C:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帾㍀ㅂ⑄㹆H╊⥌⩎⥐", a_));
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x003D328C File Offset: 0x003D228C
		public virtual object GetByIndex(int index)
		{
			int a_ = 8;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_41;
				case 2:
					if (index >= this.ᜃ)
					{
						num = 1;
						continue;
					}
					goto IL_94;
				}
				if (index >= 0)
				{
					num = 0;
					continue;
				}
				IL_41:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_57;
				}
			}
			IL_57:
			if (false)
			{
			}
			if (true)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽⸿♁⅃㹅", a_));
			IL_94:
			return this.ᜂ[this.ᜁ[index]];
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x003D3340 File Offset: 0x003D2340
		public virtual object GetKey(int index)
		{
			int a_ = 18;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (index >= this.ᜃ)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_94;
				case 3:
					goto IL_41;
				}
				if (index >= 0)
				{
					num = 0;
					continue;
				}
				IL_41:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_57;
				}
			}
			IL_57:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ⑉⡋⭍⡏", a_));
			IL_94:
			return this.ᜁ[index];
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x003D33EC File Offset: 0x003D23EC
		public virtual IList GetKeyList()
		{
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_70;
				case 2:
					goto IL_5A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5A:
					this.ᜆ = new SortedListEx.ᜃ(this);
					num = 0;
					break;
				default:
					if (false)
					{
					}
					if (this.ᜆ != null)
					{
						goto IL_72;
					}
					num = 2;
					break;
				}
			}
			IL_70:
			IL_72:
			return this.ᜆ;
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x003D3474 File Offset: 0x003D2474
		public virtual IList GetValueList()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_89;
				case 2:
					if (true)
					{
					}
					this.ᜇ = new SortedListEx.ᜁ(this);
					num = 1;
					continue;
				case 3:
					goto IL_61;
				}
				if (this.ᜇ == null)
				{
					num = 2;
				}
				else
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
						this.ᜇ.ᜀ();
						break;
					}
					num = 3;
				}
			}
			IL_61:
			IL_89:
			return this.ᜇ;
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x003D3514 File Offset: 0x003D2514
		public virtual int IndexOfKey(object key)
		{
			int a_ = 19;
			int num = 0;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (num2 < 0)
					{
						num = 3;
						continue;
					}
					goto IL_A2;
				case 2:
					goto IL_5A;
				case 3:
					return -1;
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
					if (key == null)
					{
						num = 2;
						continue;
					}
					break;
				}
				num2 = Array.BinarySearch(this.ᜁ, 0, this.ᜃ, key, this.ᜅ);
				num = 1;
			}
			IL_5A:
			throw new ArgumentNullException(RecordTableEnumerator.b("≈⹊㑌", a_));
			IL_A2:
			if (true)
			{
			}
			return num2;
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x003D35CC File Offset: 0x003D25CC
		public virtual int IndexOfValue(object value)
		{
			object obj;
			for (;;)
			{
				obj = null;
				IDictionaryEnumerator enumerator = this.ᜂ.GetEnumerator();
				enumerator.Reset();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_50;
					case 1:
						if (enumerator.Value.Equals(value))
						{
							num = 3;
							continue;
						}
						goto IL_81;
					case 2:
						goto IL_81;
					case 3:
						if (true)
						{
						}
						obj = enumerator.Key;
						num = 0;
						continue;
					case 4:
						return -1;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_94;
						default:
							if (false)
							{
							}
							if (obj == null)
							{
								num = 4;
								continue;
							}
							goto IL_E8;
						}
						break;
					case 6:
						goto IL_50;
					case 7:
						if (!enumerator.MoveNext())
						{
							goto IL_94;
						}
						num = 1;
						continue;
					}
					break;
					IL_50:
					num = 5;
					continue;
					IL_81:
					num = 7;
					continue;
					IL_94:
					num = 6;
				}
			}
			return -1;
			IL_E8:
			return Array.IndexOf<object>(this.ᜁ, obj, 0, this.ᜃ);
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x003D36D4 File Offset: 0x003D26D4
		public virtual void RemoveAt(int index)
		{
			int a_ = 16;
			int num = 6;
			object key;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 4;
					continue;
				case 1:
					goto IL_CF;
				case 2:
					Array.Copy(this.ᜁ, index + 1, this.ᜁ, index, this.ᜃ - index);
					num = 3;
					continue;
				case 3:
					goto IL_103;
				case 4:
					if (index >= this.ᜃ)
					{
						num = 1;
						continue;
					}
					this.ᜃ--;
					key = this.ᜁ[index];
					num = 5;
					continue;
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
						if (index < this.ᜃ)
						{
							num = 2;
							continue;
						}
						goto IL_105;
					}
					break;
				}
				IL_35:
				if (index >= 0)
				{
					num = 0;
					continue;
				}
				break;
				goto IL_35;
			}
			IL_9C:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅♇⹉⥋㙍", a_));
			IL_CF:
			goto IL_9C;
			IL_103:
			IL_105:
			this.ᜁ[this.ᜃ] = null;
			this.ᜂ.Remove(key);
			this.ᜄ++;
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x003D3810 File Offset: 0x003D2810
		public virtual void Remove(object key)
		{
			for (;;)
			{
				int num = this.IndexOfKey(key);
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
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
							if (num < 0)
							{
								return;
							}
							break;
						}
						num2 = 2;
						continue;
					case 2:
						if (true)
						{
						}
						this.RemoveAt(num);
						num2 = 0;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x003D3890 File Offset: 0x003D2890
		public virtual void SetByIndex(int index, object value)
		{
			int a_ = 10;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_41;
				case 2:
					if (index >= this.ᜃ)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_94;
				case 3:
					num = 2;
					continue;
				}
				if (index >= 0)
				{
					num = 3;
					continue;
				}
				IL_41:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_57;
				}
			}
			IL_57:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ⱁ⁃⍅ぇ", a_));
			IL_94:
			this.ᜂ[this.ᜁ[index]] = value;
			this.ᜄ++;
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x003D3954 File Offset: 0x003D2954
		public virtual void TrimToSize()
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
			this.Capacity = this.ᜃ;
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x003D399C File Offset: 0x003D299C
		public virtual IDictionaryEnumerator GetEnumerator()
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
			return new SortedListEx.ᜂ(this, 0, this.ᜃ, 3);
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x003D39E8 File Offset: 0x003D29E8
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
			return new SortedListEx.ᜂ(this, 0, this.ᜃ, 3);
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x003D3A34 File Offset: 0x003D2A34
		private void ᜀ(int A_0, object A_1, object A_2)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜀ(this.ᜃ + 1);
					num = 5;
					continue;
				case 2:
					if (A_0 >= this.ᜃ)
					{
						goto IL_D0;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4A;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					goto IL_4A;
				case 4:
					goto IL_6F;
				case 5:
					goto IL_71;
				}
				if (this.ᜃ == this.ᜁ.Length)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_71;
				IL_4A:
				Array.Copy(this.ᜁ, A_0, this.ᜁ, A_0 + 1, this.ᜃ - A_0);
				num = 4;
				continue;
				IL_71:
				num = 2;
			}
			IL_6F:
			IL_D0:
			this.ᜁ[A_0] = A_1;
			this.ᜂ[A_1] = A_2;
			this.ᜃ++;
			this.ᜄ++;
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x003D3B44 File Offset: 0x003D2B44
		private void ᜀ(int A_0)
		{
			int num = 3;
			int num3;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_8F;
				case 1:
					if (true)
					{
					}
					num2 = this.ᜁ.Length * 2;
					goto IL_91;
				case 2:
					goto IL_9D;
				case 4:
					num3 = A_0;
					num = 0;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9D;
					}
					if (false)
					{
					}
					num = 1;
					continue;
				case 6:
					num2 = 16;
					goto IL_91;
				}
				if (this.ᜁ.Length != 0)
				{
					num = 5;
					continue;
				}
				num = 6;
				continue;
				IL_91:
				num3 = num2;
				num = 2;
				continue;
				IL_9D:
				if (num3 >= A_0)
				{
					break;
				}
				num = 4;
			}
			IL_8F:
			this.Capacity = num3;
		}

		// Token: 0x04002E52 RID: 11858
		private const int ᜀ = 16;

		// Token: 0x04002E53 RID: 11859
		private object[] ᜁ;

		// Token: 0x04002E54 RID: 11860
		private Hashtable ᜂ;

		// Token: 0x04002E55 RID: 11861
		private int ᜃ;

		// Token: 0x04002E56 RID: 11862
		private int ᜄ;

		// Token: 0x04002E57 RID: 11863
		private IComparer ᜅ;

		// Token: 0x04002E58 RID: 11864
		private SortedListEx.ᜃ ᜆ;

		// Token: 0x04002E59 RID: 11865
		private SortedListEx.ᜁ ᜇ;

		// Token: 0x0200062D RID: 1581
		[DefaultMember("Item")]
		[Serializable]
		private class ᜀ : SortedListEx
		{
			// Token: 0x060060C6 RID: 24774 RVA: 0x003D3C18 File Offset: 0x003D2C18
			internal ᜀ(SortedListEx A_0)
			{
				this.ᜀ = A_0;
				this.ᜁ = A_0.SyncRoot;
			}

			// Token: 0x060060C7 RID: 24775 RVA: 0x003D3C40 File Offset: 0x003D2C40
			public virtual int ᜃ()
			{
				int capacity;
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
					lock (this.ᜁ)
					{
						capacity = this.ᜀ.Capacity;
					}
					break;
				}
				return capacity;
			}

			// Token: 0x060060C8 RID: 24776 RVA: 0x003D3CB0 File Offset: 0x003D2CB0
			public override int get_Count()
			{
				int count;
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
					lock (this.ᜁ)
					{
						count = this.ᜀ.Count;
					}
					break;
				}
				return count;
			}

			// Token: 0x060060C9 RID: 24777 RVA: 0x003D3D20 File Offset: 0x003D2D20
			public override object get_SyncRoot()
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

			// Token: 0x060060CA RID: 24778 RVA: 0x003D3D64 File Offset: 0x003D2D64
			public virtual bool ᜇ()
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
				return this.ᜀ.IsReadOnly;
			}

			// Token: 0x060060CB RID: 24779 RVA: 0x003D3DAC File Offset: 0x003D2DAC
			public virtual bool ᜈ()
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
				return this.ᜀ.IsFixedSize;
			}

			// Token: 0x060060CC RID: 24780 RVA: 0x003D3DF4 File Offset: 0x003D2DF4
			public override bool get_IsSynchronized()
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
				return true;
			}

			// Token: 0x060060CD RID: 24781 RVA: 0x003D3E30 File Offset: 0x003D2E30
			public virtual object ᜅ(object A_0)
			{
				object result;
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
					lock (this.ᜁ)
					{
						result = this.ᜀ[A_0];
					}
					break;
				}
				return result;
			}

			// Token: 0x060060CE RID: 24782 RVA: 0x003D3EA4 File Offset: 0x003D2EA4
			public virtual void ᜀ(object A_0, object A_1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				lock (this.ᜁ)
				{
					if (true)
					{
					}
					this.ᜀ[A_0] = A_1;
				}
			}

			// Token: 0x060060CF RID: 24783 RVA: 0x003D3F14 File Offset: 0x003D2F14
			public virtual void ᜁ(object A_0, object A_1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				object obj;
				Monitor.Enter(obj = this.ᜁ);
				try
				{
					this.ᜀ.Add(A_0, A_1);
				}
				finally
				{
					if (true)
					{
					}
					Monitor.Exit(obj);
				}
			}

			// Token: 0x060060D0 RID: 24784 RVA: 0x003D3F84 File Offset: 0x003D2F84
			public virtual void ᜅ()
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
					lock (this.ᜁ)
					{
						this.ᜀ.Clear();
					}
					break;
				}
			}

			// Token: 0x060060D1 RID: 24785 RVA: 0x003D3FF4 File Offset: 0x003D2FF4
			public virtual object ᜁ()
			{
				object result;
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
					lock (this.ᜁ)
					{
						result = this.ᜀ.Clone();
					}
					break;
				}
				return result;
			}

			// Token: 0x060060D2 RID: 24786 RVA: 0x003D4064 File Offset: 0x003D3064
			public virtual bool ᜀ(object A_0)
			{
				bool result;
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
					lock (this.ᜁ)
					{
						result = this.ᜀ.Contains(A_0);
					}
					break;
				}
				return result;
			}

			// Token: 0x060060D3 RID: 24787 RVA: 0x003D40D8 File Offset: 0x003D30D8
			public virtual bool ᜁ(object A_0)
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
					if (true)
					{
					}
					lock (this.ᜁ)
					{
						result = this.ᜀ.ContainsKey(A_0);
					}
					break;
				}
				return result;
			}

			// Token: 0x060060D4 RID: 24788 RVA: 0x003D414C File Offset: 0x003D314C
			public virtual bool ᜃ(object A_0)
			{
				if (true)
				{
				}
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
					lock (this.ᜁ)
					{
						result = this.ᜀ.ContainsValue(A_0);
					}
					break;
				}
				return result;
			}

			// Token: 0x060060D5 RID: 24789 RVA: 0x003D41C0 File Offset: 0x003D31C0
			public override void CopyTo(Array array, int index)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				object obj;
				Monitor.Enter(obj = this.ᜁ);
				try
				{
					this.ᜀ.CopyTo(array, index);
				}
				finally
				{
					if (true)
					{
					}
					Monitor.Exit(obj);
				}
			}

			// Token: 0x060060D6 RID: 24790 RVA: 0x003D4230 File Offset: 0x003D3230
			public virtual object ᜁ(int A_0)
			{
				object byIndex;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					lock (this.ᜁ)
					{
						byIndex = this.ᜀ.GetByIndex(A_0);
					}
					break;
				}
				if (true)
				{
				}
				return byIndex;
			}

			// Token: 0x060060D7 RID: 24791 RVA: 0x003D42A4 File Offset: 0x003D32A4
			public virtual IDictionaryEnumerator ᜀ()
			{
				IDictionaryEnumerator enumerator;
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
					lock (this.ᜁ)
					{
						enumerator = this.ᜀ.GetEnumerator();
					}
					break;
				}
				return enumerator;
			}

			// Token: 0x060060D8 RID: 24792 RVA: 0x003D4314 File Offset: 0x003D3314
			public virtual object ᜀ(int A_0)
			{
				object key;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return key;
				}
				if (false)
				{
				}
				lock (this.ᜁ)
				{
					if (true)
					{
					}
					key = this.ᜀ.GetKey(A_0);
				}
				return key;
			}

			// Token: 0x060060D9 RID: 24793 RVA: 0x003D4388 File Offset: 0x003D3388
			public virtual IList ᜂ()
			{
				if (true)
				{
				}
				IList keyList;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					lock (this.ᜁ)
					{
						keyList = this.ᜀ.GetKeyList();
					}
					break;
				}
				return keyList;
			}

			// Token: 0x060060DA RID: 24794 RVA: 0x003D43F8 File Offset: 0x003D33F8
			public virtual IList ᜄ()
			{
				IList valueList;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return valueList;
				}
				if (false)
				{
				}
				object obj;
				Monitor.Enter(obj = this.ᜁ);
				try
				{
					valueList = this.ᜀ.GetValueList();
				}
				finally
				{
					if (true)
					{
					}
					Monitor.Exit(obj);
				}
				return valueList;
			}

			// Token: 0x060060DB RID: 24795 RVA: 0x003D4468 File Offset: 0x003D3468
			public virtual int ᜆ(object A_0)
			{
				int result;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					lock (this.ᜁ)
					{
						result = this.ᜀ.IndexOfKey(A_0);
					}
					break;
				}
				if (true)
				{
				}
				return result;
			}

			// Token: 0x060060DC RID: 24796 RVA: 0x003D44DC File Offset: 0x003D34DC
			public virtual int ᜂ(object A_0)
			{
				int result;
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
					lock (this.ᜁ)
					{
						result = this.ᜀ.IndexOfValue(A_0);
					}
					break;
				}
				return result;
			}

			// Token: 0x060060DD RID: 24797 RVA: 0x003D4550 File Offset: 0x003D3550
			public virtual void ᜂ(int A_0)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				object obj;
				Monitor.Enter(obj = this.ᜁ);
				try
				{
					this.ᜀ.RemoveAt(A_0);
				}
				finally
				{
					if (true)
					{
					}
					Monitor.Exit(obj);
				}
			}

			// Token: 0x060060DE RID: 24798 RVA: 0x003D45C0 File Offset: 0x003D35C0
			public virtual void ᜄ(object A_0)
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
					lock (this.ᜁ)
					{
						this.ᜀ.Remove(A_0);
					}
					break;
				}
			}

			// Token: 0x060060DF RID: 24799 RVA: 0x003D4630 File Offset: 0x003D3630
			public virtual void ᜀ(int A_0, object A_1)
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
					lock (this.ᜁ)
					{
						this.ᜀ.SetByIndex(A_0, A_1);
					}
					break;
				}
			}

			// Token: 0x060060E0 RID: 24800 RVA: 0x003D46A0 File Offset: 0x003D36A0
			public virtual void ᜆ()
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				lock (this.ᜁ)
				{
					if (true)
					{
					}
					this.ᜀ.TrimToSize();
				}
			}

			// Token: 0x04002E5A RID: 11866
			private SortedListEx ᜀ;

			// Token: 0x04002E5B RID: 11867
			private object ᜁ;
		}

		// Token: 0x0200062E RID: 1582
		[Serializable]
		private class ᜂ : IDictionaryEnumerator, ICloneable
		{
			// Token: 0x060060E1 RID: 24801 RVA: 0x003D4710 File Offset: 0x003D3710
			internal ᜂ(SortedListEx A_0, int A_1, int A_2, int A_3)
			{
				this.ᜃ = A_0;
				this.ᜆ = A_1;
				this.ᜇ = A_1;
				this.ᜈ = A_1 + A_2;
				this.ᜉ = A_0.ᜄ;
				this.ᜋ = A_3;
				this.ᜊ = false;
			}

			// Token: 0x060060E2 RID: 24802 RVA: 0x003D475C File Offset: 0x003D375C
			public object ᜀ()
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
				return base.MemberwiseClone();
			}

			// Token: 0x060060E3 RID: 24803 RVA: 0x003D47A0 File Offset: 0x003D37A0
			public virtual object ᜁ()
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (!this.ᜊ)
							{
								num = 3;
								continue;
							}
							goto IL_91;
						}
						break;
					case 2:
						goto IL_4D;
					case 3:
						goto IL_89;
					}
					if (this.ᜉ != this.ᜃ.ᜄ)
					{
						if (true)
						{
						}
						num = 2;
					}
					else
					{
						num = 1;
					}
				}
				IL_4D:
				throw new InvalidOperationException();
				IL_89:
				throw new InvalidOperationException();
				IL_91:
				return this.ᜄ;
			}

			// Token: 0x060060E4 RID: 24804 RVA: 0x003D4844 File Offset: 0x003D3844
			public virtual bool MoveNext()
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_AC;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_93;
						default:
							goto IL_C4;
						}
						break;
					case 3:
						goto IL_93;
					}
					if (this.ᜉ != this.ᜃ.ᜄ)
					{
						num = 2;
						continue;
					}
					num = 3;
					continue;
					IL_93:
					if (this.ᜆ >= this.ᜈ)
					{
						goto IL_E2;
					}
					num = 1;
				}
				IL_AC:
				this.ᜄ = this.ᜃ.ᜁ[this.ᜆ];
				this.ᜅ = this.ᜃ.ᜂ[this.ᜄ];
				this.ᜆ++;
				this.ᜊ = true;
				return true;
				IL_C4:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new InvalidOperationException();
				IL_E2:
				this.ᜄ = null;
				this.ᜅ = null;
				this.ᜊ = false;
				return false;
			}

			// Token: 0x060060E5 RID: 24805 RVA: 0x003D494C File Offset: 0x003D394C
			public virtual DictionaryEntry ᜂ()
			{
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
							goto IL_4B;
						default:
							goto IL_73;
						}
						break;
					case 2:
						goto IL_5B;
					case 3:
						goto IL_4B;
					}
					if (this.ᜉ != this.ᜃ.ᜄ)
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
					IL_4B:
					if (this.ᜊ)
					{
						goto IL_91;
					}
					num = 2;
				}
				IL_5B:
				throw new InvalidOperationException();
				IL_73:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new InvalidOperationException();
				IL_91:
				return new DictionaryEntry(this.ᜄ, this.ᜅ);
			}

			// Token: 0x060060E6 RID: 24806 RVA: 0x003D49FC File Offset: 0x003D39FC
			public virtual object get_Current()
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_5D;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_73;
						default:
							goto IL_AA;
						}
						break;
					case 3:
						if (this.ᜋ == 2)
						{
							num = 1;
							continue;
						}
						goto IL_B2;
					case 4:
						goto IL_42;
					case 5:
						if (true)
						{
						}
						if (this.ᜋ == 1)
						{
							num = 2;
							continue;
						}
						num = 3;
						continue;
					}
					if (!this.ᜊ)
					{
						num = 4;
						continue;
					}
					IL_73:
					num = 5;
				}
				IL_42:
				throw new InvalidOperationException();
				IL_5D:
				return this.ᜅ;
				IL_AA:
				if (false)
				{
				}
				return this.ᜄ;
				IL_B2:
				return new DictionaryEntry(this.ᜄ, this.ᜅ);
			}

			// Token: 0x060060E7 RID: 24807 RVA: 0x003D4AD4 File Offset: 0x003D3AD4
			public virtual object ᜃ()
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_63;
					case 2:
						goto IL_53;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_53;
						default:
							goto IL_7B;
						}
						break;
					}
					if (this.ᜉ != this.ᜃ.ᜄ)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					num = 2;
					continue;
					IL_53:
					if (this.ᜊ)
					{
						goto IL_91;
					}
					num = 1;
				}
				IL_63:
				throw new InvalidOperationException();
				IL_7B:
				if (false)
				{
				}
				throw new InvalidOperationException();
				IL_91:
				return this.ᜅ;
			}

			// Token: 0x060060E8 RID: 24808 RVA: 0x003D4B78 File Offset: 0x003D3B78
			public virtual void Reset()
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
					if (this.ᜉ == this.ᜃ.ᜄ)
					{
						this.ᜆ = this.ᜇ;
						this.ᜊ = false;
						this.ᜄ = null;
						this.ᜅ = null;
						return;
					}
					break;
				}
				throw new InvalidOperationException();
			}

			// Token: 0x04002E5C RID: 11868
			internal const int ᜀ = 1;

			// Token: 0x04002E5D RID: 11869
			internal const int ᜁ = 2;

			// Token: 0x04002E5E RID: 11870
			internal const int ᜂ = 3;

			// Token: 0x04002E5F RID: 11871
			private SortedListEx ᜃ;

			// Token: 0x04002E60 RID: 11872
			private object ᜄ;

			// Token: 0x04002E61 RID: 11873
			private object ᜅ;

			// Token: 0x04002E62 RID: 11874
			private int ᜆ;

			// Token: 0x04002E63 RID: 11875
			private int ᜇ;

			// Token: 0x04002E64 RID: 11876
			private int ᜈ;

			// Token: 0x04002E65 RID: 11877
			private int ᜉ;

			// Token: 0x04002E66 RID: 11878
			private bool ᜊ;

			// Token: 0x04002E67 RID: 11879
			private int ᜋ;
		}

		// Token: 0x0200062F RID: 1583
		[DefaultMember("Item")]
		[Serializable]
		private class ᜃ : IList
		{
			// Token: 0x060060E9 RID: 24809 RVA: 0x003D4BF0 File Offset: 0x003D3BF0
			internal ᜃ(SortedListEx A_0)
			{
				this.ᜀ = A_0;
			}

			// Token: 0x060060EA RID: 24810 RVA: 0x003D4C0C File Offset: 0x003D3C0C
			public virtual int get_Count()
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
				return this.ᜀ.ᜃ;
			}

			// Token: 0x060060EB RID: 24811 RVA: 0x003D4C54 File Offset: 0x003D3C54
			public virtual bool ᜀ()
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
				return true;
			}

			// Token: 0x060060EC RID: 24812 RVA: 0x003D4C90 File Offset: 0x003D3C90
			public virtual bool ᜁ()
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
				return true;
			}

			// Token: 0x060060ED RID: 24813 RVA: 0x003D4CCC File Offset: 0x003D3CCC
			public virtual bool get_IsSynchronized()
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
				return this.ᜀ.IsSynchronized;
			}

			// Token: 0x060060EE RID: 24814 RVA: 0x003D4D14 File Offset: 0x003D3D14
			public virtual object get_SyncRoot()
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
				return this.ᜀ.SyncRoot;
			}

			// Token: 0x060060EF RID: 24815 RVA: 0x003D4D5C File Offset: 0x003D3D5C
			public virtual int ᜃ(object A_0)
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
				throw new NotSupportedException();
			}

			// Token: 0x060060F0 RID: 24816 RVA: 0x003D4D9C File Offset: 0x003D3D9C
			public virtual void ᜂ()
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
				throw new NotSupportedException();
			}

			// Token: 0x060060F1 RID: 24817 RVA: 0x003D4DDC File Offset: 0x003D3DDC
			public virtual bool ᜀ(object A_0)
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
				return this.ᜀ.Contains(A_0);
			}

			// Token: 0x060060F2 RID: 24818 RVA: 0x003D4E24 File Offset: 0x003D3E24
			public virtual void CopyTo(Array array, int arrayIndex)
			{
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
							goto IL_74;
						}
						break;
					case 2:
						if (array.Rank != 1)
						{
							num = 0;
							continue;
						}
						goto IL_7C;
					case 3:
						num = 2;
						continue;
					}
					if (true)
					{
					}
					if (array == null)
					{
						goto IL_7C;
					}
					num = 3;
				}
				IL_74:
				if (false)
				{
				}
				throw new ArgumentException();
				IL_7C:
				Array.Copy(this.ᜀ.ᜁ, 0, array, arrayIndex, this.ᜀ.Count);
			}

			// Token: 0x060060F3 RID: 24819 RVA: 0x003D4ECC File Offset: 0x003D3ECC
			public virtual void ᜁ(int A_0, object A_1)
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

			// Token: 0x060060F4 RID: 24820 RVA: 0x003D4F0C File Offset: 0x003D3F0C
			public virtual object ᜀ(int A_0)
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
				return this.ᜀ.GetKey(A_0);
			}

			// Token: 0x060060F5 RID: 24821 RVA: 0x003D4F54 File Offset: 0x003D3F54
			public virtual void ᜀ(int A_0, object A_1)
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
				throw new NotSupportedException();
			}

			// Token: 0x060060F6 RID: 24822 RVA: 0x003D4F94 File Offset: 0x003D3F94
			public virtual IEnumerator GetEnumerator()
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
				return new SortedListEx.ᜂ(this.ᜀ, 0, this.ᜀ.Count, 1);
			}

			// Token: 0x060060F7 RID: 24823 RVA: 0x003D4FE8 File Offset: 0x003D3FE8
			public virtual int ᜁ(object A_0)
			{
				int a_ = 2;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
					{
						int num2;
						if (num2 >= 0)
						{
							num = 2;
							continue;
						}
						return -1;
					}
					case 2:
					{
						int num2;
						return num2;
					}
					case 3:
						goto IL_58;
					}
					IL_29:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (A_0 == null)
						{
							num = 3;
						}
						else
						{
							int num2 = Array.BinarySearch(this.ᜀ.ᜁ, 0, this.ᜀ.Count, A_0, this.ᜀ.ᜅ);
							num = 1;
						}
						break;
					}
				}
				IL_58:
				throw new ArgumentNullException(RecordTableEnumerator.b("匷弹䔻", a_));
			}

			// Token: 0x060060F8 RID: 24824 RVA: 0x003D50B8 File Offset: 0x003D40B8
			public virtual void ᜂ(object A_0)
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
				throw new NotSupportedException();
			}

			// Token: 0x060060F9 RID: 24825 RVA: 0x003D50F8 File Offset: 0x003D40F8
			public virtual void ᜁ(int A_0)
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
				throw new NotSupportedException();
			}

			// Token: 0x04002E68 RID: 11880
			private SortedListEx ᜀ;
		}

		// Token: 0x02000630 RID: 1584
		[DefaultMember("Item")]
		[Serializable]
		private class ᜁ : IList
		{
			// Token: 0x060060FA RID: 24826 RVA: 0x003D5138 File Offset: 0x003D4138
			internal ᜁ(SortedListEx A_0)
			{
				this.ᜀ = A_0;
				this.ᜀ();
			}

			// Token: 0x060060FB RID: 24827 RVA: 0x003D5158 File Offset: 0x003D4158
			public virtual void ᜀ()
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
				int count = this.ᜀ.Count;
				this.ᜁ = new object[count];
				this.ᜀ.ᜂ.Values.CopyTo(this.ᜁ, 0);
				object[] array = new object[count];
				this.ᜀ.ᜂ.Keys.CopyTo(array, 0);
				Array.Sort(array, this.ᜁ, this.ᜀ.ᜅ);
			}

			// Token: 0x060060FC RID: 24828 RVA: 0x003D51FC File Offset: 0x003D41FC
			public virtual int get_Count()
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
				return this.ᜀ.ᜃ;
			}

			// Token: 0x060060FD RID: 24829 RVA: 0x003D5244 File Offset: 0x003D4244
			public virtual bool ᜁ()
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
				return true;
			}

			// Token: 0x060060FE RID: 24830 RVA: 0x003D5280 File Offset: 0x003D4280
			public virtual bool ᜂ()
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
				return true;
			}

			// Token: 0x060060FF RID: 24831 RVA: 0x003D52BC File Offset: 0x003D42BC
			public virtual bool get_IsSynchronized()
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
				return this.ᜀ.IsSynchronized;
			}

			// Token: 0x06006100 RID: 24832 RVA: 0x003D5304 File Offset: 0x003D4304
			public virtual object get_SyncRoot()
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
				return this.ᜀ.SyncRoot;
			}

			// Token: 0x06006101 RID: 24833 RVA: 0x003D534C File Offset: 0x003D434C
			public virtual int ᜃ(object A_0)
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
				throw new NotSupportedException();
			}

			// Token: 0x06006102 RID: 24834 RVA: 0x003D538C File Offset: 0x003D438C
			public virtual void ᜃ()
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
				throw new NotSupportedException();
			}

			// Token: 0x06006103 RID: 24835 RVA: 0x003D53CC File Offset: 0x003D43CC
			public virtual bool ᜀ(object A_0)
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
				return this.ᜀ.ContainsValue(A_0);
			}

			// Token: 0x06006104 RID: 24836 RVA: 0x003D5414 File Offset: 0x003D4414
			public virtual void CopyTo(Array array, int arrayIndex)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (true)
						{
						}
						if (array.Rank != 1)
						{
							num = 3;
							continue;
						}
						goto IL_72;
					case 3:
						goto IL_70;
					}
					if (array == null)
					{
						goto IL_72;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_72;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
				IL_70:
				throw new ArgumentException();
				IL_72:
				Array.Copy(this.ᜁ, 0, array, arrayIndex, this.ᜀ.Count);
			}

			// Token: 0x06006105 RID: 24837 RVA: 0x003D54B8 File Offset: 0x003D44B8
			public virtual void ᜁ(int A_0, object A_1)
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
				throw new NotSupportedException();
			}

			// Token: 0x06006106 RID: 24838 RVA: 0x003D54F8 File Offset: 0x003D44F8
			public virtual object ᜀ(int A_0)
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
				return this.ᜀ.GetByIndex(A_0);
			}

			// Token: 0x06006107 RID: 24839 RVA: 0x003D5540 File Offset: 0x003D4540
			public virtual void ᜀ(int A_0, object A_1)
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
				this.ᜀ.SetByIndex(A_0, A_1);
			}

			// Token: 0x06006108 RID: 24840 RVA: 0x003D5588 File Offset: 0x003D4588
			public virtual IEnumerator GetEnumerator()
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
				return new SortedListEx.ᜂ(this.ᜀ, 0, this.ᜀ.Count, 2);
			}

			// Token: 0x06006109 RID: 24841 RVA: 0x003D55DC File Offset: 0x003D45DC
			public virtual int ᜁ(object A_0)
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
				return Array.IndexOf(this.ᜁ, A_0, 0, this.ᜀ.Count);
			}

			// Token: 0x0600610A RID: 24842 RVA: 0x003D5630 File Offset: 0x003D4630
			public virtual void ᜂ(object A_0)
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
				throw new NotSupportedException();
			}

			// Token: 0x0600610B RID: 24843 RVA: 0x003D5670 File Offset: 0x003D4670
			public virtual void ᜁ(int A_0)
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

			// Token: 0x04002E69 RID: 11881
			private SortedListEx ᜀ;

			// Token: 0x04002E6A RID: 11882
			private Array ᜁ;
		}
	}
}
