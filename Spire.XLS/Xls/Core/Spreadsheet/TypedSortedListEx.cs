using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000527 RID: 1319
	public class TypedSortedListEx<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary where TKey : IComparable
	{
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x06005094 RID: 20628 RVA: 0x0032947C File Offset: 0x0032847C
		// (set) Token: 0x06005095 RID: 20629 RVA: 0x003294C0 File Offset: 0x003284C0
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
				TKey[] destinationArray;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value < this.ᜃ)
						{
							num = 5;
							continue;
						}
						num = 7;
						continue;
					case 1:
						goto IL_A9;
					case 2:
						destinationArray = new TKey[value];
						num = 3;
						continue;
					case 3:
						if (this.ᜃ > 0)
						{
							num = 6;
							continue;
						}
						goto IL_122;
					case 4:
						num = 0;
						continue;
					case 5:
						goto IL_120;
					case 6:
						Array.Copy(this.ᜁ, 0, destinationArray, 0, this.ᜃ);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E7;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 7:
						if (value > 0)
						{
							num = 2;
							continue;
						}
						goto IL_E7;
					case 9:
						goto IL_FF;
					}
					if (value != this.ᜁ.Length)
					{
						num = 4;
						continue;
					}
					return;
					IL_E7:
					this.ᜁ = new TKey[16];
					num = 9;
				}
				IL_A9:
				goto IL_122;
				IL_FF:
				return;
				IL_120:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㑁╃⩅㵇⽉", a_));
				IL_122:
				this.ᜁ = destinationArray;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06005096 RID: 20630 RVA: 0x00329618 File Offset: 0x00328618
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

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06005097 RID: 20631 RVA: 0x0032965C File Offset: 0x0032865C
		public virtual IList<TKey> Keys
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

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06005098 RID: 20632 RVA: 0x003296A0 File Offset: 0x003286A0
		public virtual IList<TValue> Values
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

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06005099 RID: 20633 RVA: 0x003296E4 File Offset: 0x003286E4
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

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600509A RID: 20634 RVA: 0x00329720 File Offset: 0x00328720
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

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x0600509B RID: 20635 RVA: 0x0032975C File Offset: 0x0032875C
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

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x0600509C RID: 20636 RVA: 0x00329798 File Offset: 0x00328798
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

		// Token: 0x17000D3C RID: 3388
		public virtual TValue this[TKey key]
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
				TValue result;
				this.ᜂ.TryGetValue(key, out result);
				return result;
			}
			set
			{
				int a_ = 9;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_7C;
					case 1:
						goto IL_49;
					case 2:
						goto IL_D2;
					case 3:
						if (this.ᜂ.ContainsKey(key))
						{
							goto IL_94;
						}
						this.Add(key, value);
						num = 2;
						continue;
					case 4:
						if (true)
						{
						}
						break;
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
							this.ᜂ[key] = value;
							num = 0;
							continue;
						}
						break;
					}
					if (key == null)
					{
						num = 1;
						continue;
					}
					num = 3;
					continue;
					IL_94:
					num = 5;
				}
				IL_49:
				throw new ArgumentNullException(RecordTableEnumerator.b("吾⑀㩂", a_));
				IL_7C:
				IL_D2:
				this.ᜄ++;
			}
		}

		// Token: 0x0600509F RID: 20639 RVA: 0x00329910 File Offset: 0x00328910
		public TypedSortedListEx()
		{
			this.ᜁ = new TKey[16];
			this.ᜂ = new Dictionary<TKey, TValue>(16);
			this.ᜅ = Comparer<TKey>.Default;
		}

		// Token: 0x060050A0 RID: 20640 RVA: 0x00329948 File Offset: 0x00328948
		public TypedSortedListEx(int initialCapacity)
		{
			int a_ = 18;
			base..ctor();
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ⅇ⑉╋㩍㥏㍑㡓ᕕ㥗⩙㵛㵝य़ᙡᵣ", a_));
			}
			this.ᜁ = new TKey[initialCapacity];
			this.ᜂ = new Dictionary<TKey, TValue>(initialCapacity);
			this.ᜅ = Comparer<TKey>.Default;
		}

		// Token: 0x060050A1 RID: 20641 RVA: 0x003299A4 File Offset: 0x003289A4
		public TypedSortedListEx(IComparer<TKey> comparer) : this()
		{
			if (comparer != null)
			{
				this.ᜅ = comparer;
			}
		}

		// Token: 0x060050A2 RID: 20642 RVA: 0x003299C8 File Offset: 0x003289C8
		public TypedSortedListEx(IComparer<TKey> comparer, int capacity) : this(comparer)
		{
			this.Capacity = capacity;
		}

		// Token: 0x060050A3 RID: 20643 RVA: 0x003299E4 File Offset: 0x003289E4
		public TypedSortedListEx(IDictionary<TKey, TValue> d) : this(d, null)
		{
		}

		// Token: 0x060050A4 RID: 20644 RVA: 0x003299FC File Offset: 0x003289FC
		public TypedSortedListEx(IDictionary<TKey, TValue> d, IComparer<TKey> comparer)
		{
			int a_ = 9;
			this..ctor(comparer, (d != null) ? d.Count : 0);
			if (d == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("嬾", a_));
			}
			d.Keys.CopyTo(this.ᜁ, 0);
			this.ᜂ = new Dictionary<TKey, TValue>(d);
			Array.Sort<TKey>(this.ᜁ, comparer);
			this.ᜃ = d.Count;
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x00329A78 File Offset: 0x00328A78
		public static TypedSortedListEx<TKey, TValue> Synchronized(TypedSortedListEx<TKey, TValue> list)
		{
			int a_ = 12;
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
					throw new NotImplementedException();
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⹁ⵃ㕅㱇", a_));
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x00329ADC File Offset: 0x00328ADC
		public virtual void Add(TKey key, TValue value)
		{
			int a_ = 4;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_41;
				case 2:
					goto IL_7F;
				case 3:
					if (this.ᜂ.ContainsKey(key))
					{
						num = 2;
						continue;
					}
					goto IL_B1;
				}
				if (true)
				{
				}
				if (key == null)
				{
					num = 1;
				}
				else
				{
					num = 3;
				}
			}
			IL_41:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_7F:
				throw new ArgumentException(RecordTableEnumerator.b("縹䤻丽ⰿ⭁❃❅㱇⽉⡋", a_));
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("儹夻䜽", a_));
			}
			IL_B1:
			int num2 = Array.BinarySearch<TKey>(this.ᜁ, 0, this.ᜃ, key, this.ᜅ);
			this.ᜀ(~num2, key, value);
		}

		// Token: 0x060050A7 RID: 20647 RVA: 0x00329BC0 File Offset: 0x00328BC0
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
			this.ᜁ = new TKey[16];
			this.ᜂ = new Dictionary<TKey, TValue>(16);
		}

		// Token: 0x060050A8 RID: 20648 RVA: 0x00329C2C File Offset: 0x00328C2C
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
			TypedSortedListEx<TKey, TValue> typedSortedListEx = new TypedSortedListEx<TKey, TValue>(this.ᜃ);
			Array.Copy(this.ᜁ, 0, typedSortedListEx.ᜁ, 0, this.ᜃ);
			typedSortedListEx.ᜂ = new Dictionary<TKey, TValue>(this.ᜂ);
			typedSortedListEx.ᜃ = this.ᜃ;
			typedSortedListEx.ᜄ = this.ᜄ;
			typedSortedListEx.ᜅ = this.ᜅ;
			return typedSortedListEx;
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x00329CC4 File Offset: 0x00328CC4
		public TypedSortedListEx<TKey, TValue> CloneAll()
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				TypedSortedListEx<TKey, TValue> typedSortedListEx;
				for (;;)
				{
					int count = this.Count;
					typedSortedListEx = (TypedSortedListEx<TKey, TValue>)base.MemberwiseClone();
					typedSortedListEx.ᜁ = new TKey[count];
					typedSortedListEx.ᜂ = new Dictionary<TKey, TValue>(count);
					typedSortedListEx.ᜆ = null;
					typedSortedListEx.ᜇ = null;
					typedSortedListEx.ᜃ = 0;
					int num = 0;
					int num2 = 6;
					for (;;)
					{
						ICloneable cloneable;
						TValue tvalue;
						switch (num2)
						{
						case 0:
							goto IL_8B;
						case 1:
							if (cloneable != null)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_13C;
								}
								if (false)
								{
								}
								num2 = 4;
								continue;
							}
							goto IL_8B;
						case 2:
							goto IL_13C;
						case 3:
							return typedSortedListEx;
						case 4:
							tvalue = (TValue)((object)cloneable.Clone());
							num2 = 0;
							continue;
						case 5:
							goto IL_130;
						case 6:
							goto IL_130;
						}
						break;
						IL_8B:
						TKey key;
						typedSortedListEx.Add(key, tvalue);
						num++;
						num2 = 5;
						continue;
						IL_13C:
						if (num >= count)
						{
							num2 = 3;
							continue;
						}
						key = this.GetKey(num);
						tvalue = this.ᜂ[key];
						cloneable = (tvalue as ICloneable);
						num2 = 1;
						continue;
						IL_130:
						num2 = 2;
					}
				}
				return typedSortedListEx;
			}
			}
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x00329E20 File Offset: 0x00328E20
		public virtual bool Contains(TKey key)
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

		// Token: 0x060050AB RID: 20651 RVA: 0x00329E68 File Offset: 0x00328E68
		public virtual bool ContainsKey(TKey key)
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

		// Token: 0x060050AC RID: 20652 RVA: 0x00329EB0 File Offset: 0x00328EB0
		public virtual bool ContainsValue(TValue value)
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

		// Token: 0x060050AD RID: 20653 RVA: 0x00329EF8 File Offset: 0x00328EF8
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			int a_ = 3;
			int num = 9;
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
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_142;
					case 2:
						goto IL_B2;
					case 3:
						if (arrayIndex < 0)
						{
							num = 11;
							continue;
						}
						num = 10;
						continue;
					case 4:
						goto IL_78;
					case 5:
						if (array.Rank != 1)
						{
							num = 6;
							continue;
						}
						num = 3;
						continue;
					case 6:
						goto IL_D9;
					case 7:
					{
						int num2;
						if (num2 >= this.Count)
						{
							num = 0;
							continue;
						}
						KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this.ᜁ[num2], this.ᜂ[this.ᜁ[num2]]);
						array.SetValue(keyValuePair, num2 + arrayIndex);
						num2++;
						num = 8;
						continue;
					}
					case 8:
						goto IL_142;
					case 10:
						goto IL_97;
					case 11:
						goto IL_180;
					}
					if (array == null)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
					IL_142:
					num = 7;
					continue;
				}
				IL_97:
				if (array.Length - arrayIndex < this.Count)
				{
					num = 2;
				}
				else
				{
					int num2 = 0;
					num = 1;
				}
			}
			IL_78:
			throw new ArgumentNullException(RecordTableEnumerator.b("堸䤺似帾㡀", a_));
			IL_B2:
			throw new ArgumentException();
			IL_D9:
			throw new ArgumentException(RecordTableEnumerator.b("堸䤺似帾㡀", a_));
			IL_180:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("堸䤺似帾㡀ੂ⭄⍆ⱈ㍊", a_));
		}

		// Token: 0x060050AE RID: 20654 RVA: 0x0032A0BC File Offset: 0x003290BC
		public virtual TValue GetByIndex(int index)
		{
			int a_ = 6;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_76;
				case 1:
					num = 2;
					continue;
				case 2:
					if (index >= this.ᜃ)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_94;
				}
				if (index < 0)
				{
					break;
				}
				num = 1;
			}
			IL_37:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("唻倽␿❁㱃", a_));
			IL_76:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_94:
				return this.ᜂ[this.ᜁ[index]];
			default:
				if (false)
				{
				}
				goto IL_37;
			}
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0032A174 File Offset: 0x00329174
		public virtual TKey GetKey(int index)
		{
			int a_ = 8;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (index >= this.ᜃ)
					{
						num = 2;
						continue;
					}
					goto IL_94;
				case 2:
					goto IL_6E;
				case 3:
					num = 1;
					continue;
				}
				if (index < 0)
				{
					break;
				}
				num = 3;
			}
			IL_37:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("圽⸿♁⅃㹅", a_));
			IL_6E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_94:
				return this.ᜁ[index];
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_37;
			}
		}

		// Token: 0x060050B0 RID: 20656 RVA: 0x0032A224 File Offset: 0x00329224
		public virtual IList<TKey> GetKeyList()
		{
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜆ = new TypedSortedListEx<TKey, TValue>.ᜁ(this);
					goto IL_68;
				case 1:
					goto IL_70;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_68:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					if (this.ᜆ != null)
					{
						goto IL_72;
					}
					num = 0;
					break;
				}
			}
			IL_70:
			IL_72:
			return this.ᜆ;
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x0032A2AC File Offset: 0x003292AC
		public virtual IList<TValue> GetValueList()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜇ = new TypedSortedListEx<TKey, TValue>.ᜃ(this);
					goto IL_53;
				case 2:
					goto IL_45;
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
				if (this.ᜇ == null)
				{
					num = 1;
					continue;
				}
				this.ᜇ.ᜀ();
				num = 2;
				continue;
				IL_53:
				num = 3;
			}
			IL_45:
			goto IL_8B;
			IL_7B:
			if (true)
			{
			}
			if (false)
			{
			}
			IL_8B:
			return this.ᜇ;
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0032A34C File Offset: 0x0032934C
		public virtual int IndexOfKey(TKey key)
		{
			int a_ = 6;
			int num = 1;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (num2 < 0)
					{
						num = 2;
						continue;
					}
					goto IL_A7;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_5B;
					}
					break;
				case 3:
					goto IL_39;
				}
				if (key == null)
				{
					num = 3;
				}
				else
				{
					num2 = Array.BinarySearch<TKey>(this.ᜁ, 0, this.ᜃ, key, this.ᜅ);
					num = 0;
				}
			}
			IL_39:
			throw new ArgumentNullException(RecordTableEnumerator.b("圻嬽㤿", a_));
			IL_5B:
			if (false)
			{
			}
			return -1;
			IL_A7:
			if (true)
			{
			}
			return num2;
		}

		// Token: 0x060050B3 RID: 20659 RVA: 0x0032A40C File Offset: 0x0032940C
		public virtual int IndexOfValue(TValue value)
		{
			object obj;
			for (;;)
			{
				obj = null;
				IDictionaryEnumerator dictionaryEnumerator = this.ᜂ.GetEnumerator();
				dictionaryEnumerator.Reset();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!dictionaryEnumerator.Value.Equals(value))
						{
							goto IL_63;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D0;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 1:
						if (obj == null)
						{
							num = 2;
							continue;
						}
						goto IL_EF;
					case 2:
						return -1;
					case 3:
						goto IL_D0;
					case 4:
						goto IL_63;
					case 5:
						goto IL_4B;
					case 6:
						if (!dictionaryEnumerator.MoveNext())
						{
							num = 5;
							continue;
						}
						num = 0;
						continue;
					case 7:
						goto IL_4B;
					}
					break;
					IL_4B:
					num = 1;
					continue;
					IL_63:
					num = 6;
					continue;
					IL_D0:
					if (true)
					{
					}
					obj = dictionaryEnumerator.Key;
					num = 7;
				}
			}
			return -1;
			IL_EF:
			return Array.IndexOf(this.ᜁ, obj, 0, this.ᜃ);
		}

		// Token: 0x060050B4 RID: 20660 RVA: 0x0032A51C File Offset: 0x0032951C
		public virtual void RemoveAt(int index)
		{
			int a_ = 1;
			int num = 2;
			TKey key;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					num = 1;
					continue;
				case 1:
					if (index >= this.ᜃ)
					{
						num = 5;
						continue;
					}
					this.ᜃ--;
					key = this.ᜁ[index];
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10C;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					if (index < this.ᜃ)
					{
						num = 4;
						continue;
					}
					goto IL_10C;
				case 4:
					Array.Copy(this.ᜁ, index + 1, this.ᜁ, index, this.ᜃ - index);
					num = 6;
					continue;
				case 5:
					goto IL_E0;
				case 6:
					goto IL_10A;
				}
				if (index < 0)
				{
					break;
				}
				num = 0;
			}
			IL_AD:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("帶圸强堼䜾", a_));
			IL_E0:
			goto IL_AD;
			IL_10A:
			IL_10C:
			this.ᜁ[this.ᜃ] = default(TKey);
			this.ᜂ.Remove(key);
			this.ᜄ++;
		}

		// Token: 0x060050B5 RID: 20661 RVA: 0x0032A66C File Offset: 0x0032966C
		public virtual bool Remove(TKey key)
		{
			bool result;
			for (;;)
			{
				IL_18:
				int num = this.IndexOfKey(key);
				for (;;)
				{
					IL_20:
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							return result;
						case 1:
							goto IL_66;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_20;
							default:
								if (false)
								{
								}
								this.RemoveAt(num);
								result = true;
								num2 = 0;
								continue;
							}
							break;
						case 3:
							if (num >= 0)
							{
								num2 = 2;
								continue;
							}
							result = false;
							num2 = 1;
							continue;
						}
						goto IL_18;
					}
				}
			}
			IL_66:
			if (true)
			{
			}
			return result;
		}

		// Token: 0x060050B6 RID: 20662 RVA: 0x0032A700 File Offset: 0x00329700
		public virtual void SetByIndex(int index, TValue value)
		{
			int a_ = 16;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_6E;
				case 2:
					if (index >= this.ᜃ)
					{
						num = 1;
						continue;
					}
					goto IL_94;
				case 3:
					num = 2;
					continue;
				}
				if (index < 0)
				{
					break;
				}
				num = 3;
			}
			IL_37:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅♇⹉⥋㙍", a_));
			IL_6E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_94:
				this.ᜂ[this.ᜁ[index]] = value;
				this.ᜄ++;
				return;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_37;
			}
		}

		// Token: 0x060050B7 RID: 20663 RVA: 0x0032A7C8 File Offset: 0x003297C8
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

		// Token: 0x060050B8 RID: 20664 RVA: 0x0032A810 File Offset: 0x00329810
		IEnumerator IEnumerable.GetEnumerator()
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
			throw new NotImplementedException();
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x0032A850 File Offset: 0x00329850
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
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
			return new TypedSortedListEx<TKey, TValue>.ᜂ(this, 0, this.ᜃ);
		}

		// Token: 0x060050BA RID: 20666 RVA: 0x0032A898 File Offset: 0x00329898
		private void ᜀ(int A_0, TKey A_1, TValue A_2)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7B;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (false)
						{
						}
						if (A_0 < this.ᜃ)
						{
							num = 2;
							continue;
						}
						goto IL_D0;
					}
					break;
				case 2:
					Array.Copy(this.ᜁ, A_0, this.ᜁ, A_0 + 1, this.ᜃ - A_0);
					num = 3;
					continue;
				case 3:
					goto IL_79;
				case 4:
					this.ᜀ(this.ᜃ + 1);
					num = 0;
					continue;
				}
				if (this.ᜃ == this.ᜁ.Length)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				IL_7B:
				num = 1;
			}
			IL_79:
			IL_D0:
			this.ᜁ[A_0] = A_1;
			this.ᜂ[A_1] = A_2;
			this.ᜃ++;
			this.ᜄ++;
		}

		// Token: 0x060050BB RID: 20667 RVA: 0x0032A9AC File Offset: 0x003299AC
		private void ᜀ(int A_0)
		{
			int num = 0;
			int num2;
			for (;;)
			{
				int num3;
				switch (num)
				{
				case 1:
					if (num2 < A_0)
					{
						num = 5;
						continue;
					}
					goto IL_BA;
				case 2:
					goto IL_82;
				case 3:
					if (true)
					{
					}
					num3 = this.ᜁ.Length * 2;
					goto IL_84;
				case 4:
					num3 = 16;
					goto IL_84;
				case 5:
					num2 = A_0;
					num = 2;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_85;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				}
				if (this.ᜁ.Length != 0)
				{
					num = 6;
					continue;
				}
				num = 4;
				continue;
				IL_85:
				num = 1;
				continue;
				IL_84:
				num2 = num3;
				goto IL_85;
			}
			IL_82:
			IL_BA:
			this.Capacity = num2;
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x0032AA7C File Offset: 0x00329A7C
		public void Add(object key, object value)
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
			TKey key2 = (TKey)((object)key);
			TValue value2 = (TValue)((object)value);
			this.Add(key2, value2);
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x0032AAD0 File Offset: 0x00329AD0
		public bool Contains(object key)
		{
			int num = 1;
			bool result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_44;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5F;
					default:
						goto IL_7D;
					}
					break;
				case 3:
				{
					TKey key2 = (TKey)((object)key);
					result = this.ContainsKey(key2);
					goto IL_5F;
				}
				}
				if (true)
				{
				}
				if (key is TKey)
				{
					num = 3;
					continue;
				}
				result = false;
				num = 0;
				continue;
				IL_5F:
				num = 2;
			}
			IL_44:
			return result;
			IL_7D:
			if (false)
			{
			}
			return result;
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x0032AB64 File Offset: 0x00329B64
		IDictionaryEnumerator IDictionary.GetEnumerator()
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
			return ((IDictionary)this.ᜂ).GetEnumerator();
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x0032ABAC File Offset: 0x00329BAC
		ICollection IDictionary.Keys
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
				return this.ᜂ.Keys;
			}
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x0032ABF4 File Offset: 0x00329BF4
		public void Remove(object key)
		{
			for (;;)
			{
				IL_00:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							this.Remove((TKey)((object)key));
							num = 0;
							continue;
						}
						break;
					}
					if (true)
					{
					}
					if (!(key is TKey))
					{
						return;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060050C1 RID: 20673 RVA: 0x0032AC74 File Offset: 0x00329C74
		ICollection IDictionary.Values
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
				return this.ᜂ.Values;
			}
		}

		// Token: 0x17000D3F RID: 3391
		public object this[object key]
		{
			get
			{
				int num = 3;
				TValue tvalue;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_86;
					case 1:
						tvalue = default(TValue);
						num = 0;
						continue;
					case 2:
						goto IL_68;
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
						if (key is TKey)
						{
							num = 2;
							continue;
						}
						break;
					}
					num = 1;
				}
				IL_68:
				TValue tvalue2 = this[(TKey)((object)key)];
				goto IL_89;
				IL_86:
				tvalue2 = tvalue;
				IL_89:
				return tvalue2;
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
				this[(TKey)((object)key)] = (TValue)((object)value);
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060050C4 RID: 20676 RVA: 0x0032ADA8 File Offset: 0x00329DA8
		ICollection<TKey> IDictionary<!0, !1>.Keys
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
				return this.Keys;
			}
		}

		// Token: 0x060050C5 RID: 20677 RVA: 0x0032ADEC File Offset: 0x00329DEC
		public bool TryGetValue(TKey key, out TValue value)
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
			return this.ᜂ.TryGetValue(key, out value);
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060050C6 RID: 20678 RVA: 0x0032AE34 File Offset: 0x00329E34
		ICollection<TValue> IDictionary<!0, !1>.Values
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
				return this.ᜂ.Values;
			}
		}

		// Token: 0x060050C7 RID: 20679 RVA: 0x0032AE7C File Offset: 0x00329E7C
		public void Add(KeyValuePair<TKey, TValue> item)
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
			this.Add(item.Key, item.Value);
		}

		// Token: 0x060050C8 RID: 20680 RVA: 0x0032AECC File Offset: 0x00329ECC
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			int num = 1;
			bool result;
			for (;;)
			{
				TValue tvalue;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_31;
					default:
						if (false)
						{
						}
						result = tvalue.Equals(item.Value);
						num = 3;
						continue;
					}
					break;
				case 2:
					return result;
				case 3:
					return result;
				}
				goto IL_20;
				IL_31:
				num = 0;
				continue;
				IL_20:
				if (this.TryGetValue(item.Key, out tvalue))
				{
					goto IL_31;
				}
				result = false;
				num = 2;
			}
			return result;
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x0032AF78 File Offset: 0x00329F78
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
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
			((ICollection<KeyValuePair<TKey, TValue>>)this.ᜂ).CopyTo(array, arrayIndex);
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x0032AFC0 File Offset: 0x00329FC0
		public bool Remove(KeyValuePair<TKey, TValue> item)
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
			return this.Remove(item.Key);
		}

		// Token: 0x04002421 RID: 9249
		private const int ᜀ = 16;

		// Token: 0x04002422 RID: 9250
		private TKey[] ᜁ;

		// Token: 0x04002423 RID: 9251
		private Dictionary<TKey, TValue> ᜂ;

		// Token: 0x04002424 RID: 9252
		private int ᜃ;

		// Token: 0x04002425 RID: 9253
		private int ᜄ;

		// Token: 0x04002426 RID: 9254
		private IComparer<TKey> ᜅ;

		// Token: 0x04002427 RID: 9255
		private TypedSortedListEx<TKey, TValue>.ᜁ ᜆ;

		// Token: 0x04002428 RID: 9256
		private TypedSortedListEx<TKey, TValue>.ᜃ ᜇ;

		// Token: 0x02000601 RID: 1537
		[Serializable]
		private class ᜂ : IEnumerator<KeyValuePair<ᜀ, ᜁ>>, ICloneable
		{
			// Token: 0x06005B01 RID: 23297 RVA: 0x0038CAE0 File Offset: 0x0038BAE0
			internal ᜂ(TypedSortedListEx<ᜀ, ᜁ> A_0, int A_1, int A_2)
			{
				this.ᜀ = A_0;
				this.ᜃ = A_1;
				this.ᜄ = A_1;
				this.ᜅ = A_1 + A_2;
				this.ᜆ = this.ᜀ.ᜄ;
				this.ᜇ = false;
			}

			// Token: 0x06005B02 RID: 23298 RVA: 0x0038CB2C File Offset: 0x0038BB2C
			public void Dispose()
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
			}

			// Token: 0x06005B03 RID: 23299 RVA: 0x0038CB68 File Offset: 0x0038BB68
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

			// Token: 0x06005B04 RID: 23300 RVA: 0x0038CBAC File Offset: 0x0038BBAC
			public virtual ᜀ ᜃ()
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.ᜇ)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_91;
					case 1:
						goto IL_61;
					case 2:
						goto IL_89;
					}
					if (this.ᜆ != this.ᜀ.ᜄ)
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
							num = 1;
							break;
						}
					}
					else
					{
						num = 0;
					}
				}
				IL_61:
				throw new InvalidOperationException();
				IL_89:
				throw new InvalidOperationException();
				IL_91:
				return this.ᜁ;
			}

			// Token: 0x06005B05 RID: 23301 RVA: 0x0038CC50 File Offset: 0x0038BC50
			public virtual bool MoveNext()
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜃ < this.ᜅ)
						{
							num = 1;
							continue;
						}
						goto IL_E6;
					case 1:
						goto IL_D6;
					case 2:
						goto IL_61;
					}
					if (this.ᜆ != this.ᜀ.ᜄ)
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
							num = 2;
							break;
						}
					}
					else
					{
						num = 0;
					}
				}
				IL_61:
				throw new InvalidOperationException();
				IL_D6:
				if (true)
				{
				}
				this.ᜁ = this.ᜀ.ᜁ[this.ᜃ];
				this.ᜂ = this.ᜀ.ᜂ[this.ᜁ];
				this.ᜃ++;
				this.ᜇ = true;
				return true;
				IL_E6:
				this.ᜁ = default(ᜀ);
				this.ᜂ = default(ᜁ);
				this.ᜇ = false;
				return false;
			}

			// Token: 0x06005B06 RID: 23302 RVA: 0x0038CD64 File Offset: 0x0038BD64
			public virtual KeyValuePair<ᜀ, ᜁ> ᜂ()
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.ᜇ)
						{
							num = 1;
							continue;
						}
						goto IL_91;
					case 1:
						goto IL_89;
					case 2:
						goto IL_61;
					}
					if (this.ᜆ != this.ᜀ.ᜄ)
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
							num = 2;
							break;
						}
					}
					else
					{
						if (true)
						{
						}
						num = 0;
					}
				}
				IL_61:
				throw new InvalidOperationException();
				IL_89:
				throw new InvalidOperationException();
				IL_91:
				return new KeyValuePair<ᜀ, ᜁ>(this.ᜁ, this.ᜂ);
			}

			// Token: 0x06005B07 RID: 23303 RVA: 0x0038CE14 File Offset: 0x0038BE14
			public virtual KeyValuePair<ᜀ, ᜁ> ᜁ()
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_36;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.ᜇ)
				{
					return new KeyValuePair<ᜀ, ᜁ>(this.ᜁ, this.ᜂ);
				}
				IL_36:
				throw new InvalidOperationException();
			}

			// Token: 0x06005B08 RID: 23304 RVA: 0x0038CE70 File Offset: 0x0038BE70
			object IEnumerator.ᜄ()
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_36;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.ᜇ)
				{
					return new KeyValuePair<ᜀ, ᜁ>(this.ᜁ, this.ᜂ);
				}
				IL_36:
				throw new InvalidOperationException();
			}

			// Token: 0x06005B09 RID: 23305 RVA: 0x0038CED4 File Offset: 0x0038BED4
			public virtual object ᜅ()
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.ᜇ)
						{
							num = 3;
							continue;
						}
						goto IL_91;
					case 2:
						goto IL_61;
					case 3:
						goto IL_89;
					}
					if (this.ᜆ != this.ᜀ.ᜄ)
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
							num = 2;
							break;
						}
					}
					else
					{
						num = 0;
					}
				}
				IL_61:
				throw new InvalidOperationException();
				IL_89:
				if (true)
				{
				}
				throw new InvalidOperationException();
				IL_91:
				return this.ᜂ;
			}

			// Token: 0x06005B0A RID: 23306 RVA: 0x0038CF80 File Offset: 0x0038BF80
			public virtual void Reset()
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_41;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.ᜆ == this.ᜀ.ᜄ)
				{
					this.ᜃ = this.ᜄ;
					this.ᜇ = false;
					this.ᜁ = default(ᜀ);
					this.ᜂ = default(ᜁ);
					return;
				}
				IL_41:
				throw new InvalidOperationException();
			}

			// Token: 0x04002C74 RID: 11380
			private TypedSortedListEx<ᜀ, ᜁ> ᜀ;

			// Token: 0x04002C75 RID: 11381
			private ᜀ ᜁ;

			// Token: 0x04002C76 RID: 11382
			private ᜁ ᜂ;

			// Token: 0x04002C77 RID: 11383
			private int ᜃ;

			// Token: 0x04002C78 RID: 11384
			private int ᜄ;

			// Token: 0x04002C79 RID: 11385
			private int ᜅ;

			// Token: 0x04002C7A RID: 11386
			private int ᜆ;

			// Token: 0x04002C7B RID: 11387
			private bool ᜇ;
		}

		// Token: 0x02000602 RID: 1538
		private class ᜀ : IEnumerator<!0>
		{
			// Token: 0x06005B0B RID: 23307 RVA: 0x0038D004 File Offset: 0x0038C004
			public ᜀ(TypedSortedListEx<ᜀ, ᜁ> A_0)
			{
				int a_ = 18;
				this.ᜁ = -1;
				base..ctor();
				if (A_0 == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("⑇⍉㽋㩍", a_));
				}
				this.ᜀ = A_0;
				this.ᜂ = this.ᜀ.ᜄ;
			}

			// Token: 0x06005B0C RID: 23308 RVA: 0x0038D058 File Offset: 0x0038C058
			public ᜀ ᜀ()
			{
				int a_ = 4;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						IL_5E:
						if (this.ᜁ >= this.ᜀ.ᜃ)
						{
							num = 2;
							continue;
						}
						goto IL_D9;
					case 2:
						goto IL_B7;
					case 4:
						if (this.ᜁ >= 0)
						{
							num = 0;
							continue;
						}
						goto IL_B7;
					case 5:
						goto IL_4C;
					}
					if (this.ᜂ != this.ᜀ.ᜄ)
					{
						num = 5;
						continue;
					}
					num = 4;
					continue;
					IL_B7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
					default:
						goto IL_CD;
					}
				}
				IL_4C:
				if (true)
				{
				}
				throw new InvalidOperationException(RecordTableEnumerator.b("樹崻䰽┿ⱁぃ晅⭇╉⁋≍㕏ㅑ⁓㽕㝗㑙籛⥝şᅡ䑣եg୩ɫ७ᕯᙱ", a_));
				IL_CD:
				if (false)
				{
				}
				throw new InvalidOperationException();
				IL_D9:
				return this.ᜀ.ᜁ[this.ᜁ];
			}

			// Token: 0x06005B0D RID: 23309 RVA: 0x0038D154 File Offset: 0x0038C154
			public void Dispose()
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
			}

			// Token: 0x06005B0E RID: 23310 RVA: 0x0038D190 File Offset: 0x0038C190
			object IEnumerator.ᜁ()
			{
				int a_ = 4;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 3;
						continue;
					case 1:
						goto IL_4C;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4C;
						default:
							goto IL_B7;
						}
						break;
					case 3:
						if (this.ᜁ >= this.ᜀ.ᜃ)
						{
							num = 4;
							continue;
						}
						num = 1;
						continue;
					case 4:
						goto IL_9F;
					}
					if (this.ᜁ >= 0)
					{
						num = 0;
						continue;
					}
					break;
					IL_4C:
					if (this.ᜂ == this.ᜀ.ᜄ)
					{
						goto IL_D9;
					}
					num = 2;
				}
				IL_69:
				throw new InvalidOperationException();
				IL_9F:
				goto IL_69;
				IL_B7:
				if (true)
				{
				}
				if (false)
				{
				}
				throw new InvalidOperationException(RecordTableEnumerator.b("樹崻䰽┿ⱁぃ晅⭇╉⁋≍㕏ㅑ⁓㽕㝗㑙籛⥝şᅡ䑣եg୩ɫ७ᕯᙱ", a_));
				IL_D9:
				return this.ᜀ.ᜁ[this.ᜁ];
			}

			// Token: 0x06005B0F RID: 23311 RVA: 0x0038D294 File Offset: 0x0038C294
			public bool MoveNext()
			{
				int a_ = 0;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_D4:
					num = 4;
					break;
				default:
					if (false)
					{
					}
					num = 5;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_80;
					case 1:
						goto IL_80;
					case 2:
						if (true)
						{
						}
						this.ᜁ = 0;
						num = 0;
						continue;
					case 3:
						if (this.ᜁ < 0)
						{
							num = 2;
							continue;
						}
						this.ᜁ++;
						num = 1;
						continue;
					case 4:
						goto IL_DF;
					case 6:
						goto IL_A6;
					case 7:
						if (this.ᜁ >= this.ᜀ.ᜃ)
						{
							num = 6;
							continue;
						}
						goto IL_128;
					case 8:
						goto IL_7E;
					}
					if (this.ᜂ != this.ᜀ.ᜄ)
					{
						num = 8;
						continue;
					}
					num = 3;
					continue;
					IL_80:
					num = 7;
				}
				IL_7E:
				throw new InvalidOperationException(RecordTableEnumerator.b("昵夷䠹夻倽㐿扁❃⥅⑇♉⥋ⵍ⑏㭑㭓㡕硗ⵙ㵛ⵝ䁟šౣݥ٧൩५੭", a_));
				IL_A6:
				this.ᜁ = -1;
				goto IL_D4;
				IL_DF:
				IL_128:
				return this.ᜁ >= 0;
			}

			// Token: 0x06005B10 RID: 23312 RVA: 0x0038D3D8 File Offset: 0x0038C3D8
			public void Reset()
			{
				int a_ = 3;
				if (this.ᜂ != this.ᜀ.ᜄ)
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
						if (true)
						{
						}
						throw new InvalidOperationException(RecordTableEnumerator.b("椸娺似娾⽀㝂敄⑆♈❊⅌⩎㉐❒㱔㡖㝘筚⩜㹞በ䍢٤སࡨժ੬੮ᕰ", a_));
					}
				}
				this.ᜁ = -1;
			}

			// Token: 0x04002C7C RID: 11388
			private TypedSortedListEx<ᜀ, ᜁ> ᜀ;

			// Token: 0x04002C7D RID: 11389
			private int ᜁ;

			// Token: 0x04002C7E RID: 11390
			private int ᜂ;
		}

		// Token: 0x02000603 RID: 1539
		[DefaultMember("Item")]
		[Serializable]
		private class ᜁ : IList<ᜀ>
		{
			// Token: 0x06005B11 RID: 23313 RVA: 0x0038D44C File Offset: 0x0038C44C
			internal ᜁ(TypedSortedListEx<ᜀ, ᜁ> A_0)
			{
				this.ᜀ = A_0;
			}

			// Token: 0x06005B12 RID: 23314 RVA: 0x0038D468 File Offset: 0x0038C468
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

			// Token: 0x06005B13 RID: 23315 RVA: 0x0038D4B0 File Offset: 0x0038C4B0
			public virtual bool get_IsReadOnly()
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

			// Token: 0x06005B14 RID: 23316 RVA: 0x0038D4EC File Offset: 0x0038C4EC
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

			// Token: 0x06005B15 RID: 23317 RVA: 0x0038D528 File Offset: 0x0038C528
			public virtual bool ᜃ()
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

			// Token: 0x06005B16 RID: 23318 RVA: 0x0038D570 File Offset: 0x0038C570
			public virtual object ᜂ()
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

			// Token: 0x06005B17 RID: 23319 RVA: 0x0038D5B8 File Offset: 0x0038C5B8
			public void Add(ᜀ key)
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

			// Token: 0x06005B18 RID: 23320 RVA: 0x0038D5F8 File Offset: 0x0038C5F8
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
				throw new NotSupportedException();
			}

			// Token: 0x06005B19 RID: 23321 RVA: 0x0038D638 File Offset: 0x0038C638
			public virtual bool Contains(ᜀ key)
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
				return this.ᜀ.ContainsKey(key);
			}

			// Token: 0x06005B1A RID: 23322 RVA: 0x0038D680 File Offset: 0x0038C680
			public virtual void CopyTo(ᜀ[] array, int arrayIndex)
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
					if (array != null)
					{
						Array.Copy(this.ᜀ.ᜁ, 0, array, arrayIndex, this.ᜀ.Count);
						return;
					}
					break;
				}
				throw new ArgumentException(RecordTableEnumerator.b("⁀ㅂ㝄♆え", a_));
			}

			// Token: 0x06005B1B RID: 23323 RVA: 0x0038D6FC File Offset: 0x0038C6FC
			public virtual void ᜀ(Array A_0, int A_1)
			{
				int a_ = 5;
				int num = 0;
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
						case 1:
							goto IL_5A;
						case 2:
							if (A_0.Rank != 1)
							{
								goto IL_81;
							}
							goto IL_A7;
						case 3:
							goto IL_91;
						}
						if (A_0 == null)
						{
							num = 1;
							continue;
						}
						num = 2;
						continue;
					}
					IL_81:
					if (true)
					{
					}
					num = 3;
				}
				IL_5A:
				throw new ArgumentNullException(RecordTableEnumerator.b("娺似䴾⁀㩂", a_));
				IL_91:
				throw new ArgumentException(RecordTableEnumerator.b("娺似䴾⁀㩂", a_));
				IL_A7:
				Array.Copy(this.ᜀ.ᜁ, 0, A_0, A_1, this.ᜀ.Count);
			}

			// Token: 0x06005B1C RID: 23324 RVA: 0x0038D7D0 File Offset: 0x0038C7D0
			public virtual void ᜁ(int A_0, ᜀ A_1)
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

			// Token: 0x06005B1D RID: 23325 RVA: 0x0038D810 File Offset: 0x0038C810
			public virtual ᜀ ᜀ(int A_0)
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
				return this.ᜀ.GetKey(A_0);
			}

			// Token: 0x06005B1E RID: 23326 RVA: 0x0038D858 File Offset: 0x0038C858
			public virtual void ᜀ(int A_0, ᜀ A_1)
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

			// Token: 0x06005B1F RID: 23327 RVA: 0x0038D898 File Offset: 0x0038C898
			IEnumerator IEnumerable.ᜀ()
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
				return new TypedSortedListEx<ᜀ, ᜁ>.ᜀ(this.ᜀ);
			}

			// Token: 0x06005B20 RID: 23328 RVA: 0x0038D8E0 File Offset: 0x0038C8E0
			public IEnumerator<ᜀ> GetEnumerator()
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
				return new TypedSortedListEx<ᜀ, ᜁ>.ᜀ(this.ᜀ);
			}

			// Token: 0x06005B21 RID: 23329 RVA: 0x0038D928 File Offset: 0x0038C928
			public virtual int ᜀ(ᜀ A_0)
			{
				int a_ = 6;
				int num = 0;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_55;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 1:
						goto IL_5D;
					case 2:
						return -1;
					case 3:
						if (num2 < 0)
						{
							num = 2;
							continue;
						}
						return num2;
					}
					goto IL_4D;
					IL_55:
					num = 1;
					continue;
					IL_4D:
					if (A_0 == null)
					{
						goto IL_55;
					}
					num2 = Array.BinarySearch<ᜀ>(this.ᜀ.ᜁ, 0, this.ᜀ.Count, A_0, this.ᜀ.ᜅ);
					num = 3;
				}
				IL_5D:
				throw new ArgumentNullException(RecordTableEnumerator.b("圻嬽㤿", a_));
			}

			// Token: 0x06005B22 RID: 23330 RVA: 0x0038D9F4 File Offset: 0x0038C9F4
			public bool Remove(ᜀ key)
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

			// Token: 0x06005B23 RID: 23331 RVA: 0x0038DA34 File Offset: 0x0038CA34
			public virtual void ᜁ(int A_0)
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

			// Token: 0x04002C7F RID: 11391
			private TypedSortedListEx<ᜀ, ᜁ> ᜀ;
		}

		// Token: 0x02000604 RID: 1540
		[DefaultMember("Item")]
		[Serializable]
		private class ᜃ : IList<ᜁ>
		{
			// Token: 0x06005B24 RID: 23332 RVA: 0x0038DA74 File Offset: 0x0038CA74
			internal ᜃ(TypedSortedListEx<ᜀ, ᜁ> A_0)
			{
				this.ᜀ = A_0;
				this.ᜀ();
			}

			// Token: 0x06005B25 RID: 23333 RVA: 0x0038DA94 File Offset: 0x0038CA94
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
				this.ᜁ = new ᜁ[count];
				this.ᜀ.ᜂ.Values.CopyTo(this.ᜁ, 0);
				ᜀ[] array = new ᜀ[count];
				this.ᜀ.ᜂ.Keys.CopyTo(array, 0);
				Array.Sort<ᜀ, ᜁ>(array, this.ᜁ, this.ᜀ.ᜅ);
			}

			// Token: 0x06005B26 RID: 23334 RVA: 0x0038DB38 File Offset: 0x0038CB38
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

			// Token: 0x06005B27 RID: 23335 RVA: 0x0038DB80 File Offset: 0x0038CB80
			public virtual bool get_IsReadOnly()
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

			// Token: 0x06005B28 RID: 23336 RVA: 0x0038DBBC File Offset: 0x0038CBBC
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

			// Token: 0x06005B29 RID: 23337 RVA: 0x0038DBF8 File Offset: 0x0038CBF8
			public virtual bool ᜄ()
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

			// Token: 0x06005B2A RID: 23338 RVA: 0x0038DC40 File Offset: 0x0038CC40
			public virtual object ᜃ()
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

			// Token: 0x06005B2B RID: 23339 RVA: 0x0038DC88 File Offset: 0x0038CC88
			public virtual void Add(ᜁ value)
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

			// Token: 0x06005B2C RID: 23340 RVA: 0x0038DCC8 File Offset: 0x0038CCC8
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
				throw new NotSupportedException();
			}

			// Token: 0x06005B2D RID: 23341 RVA: 0x0038DD08 File Offset: 0x0038CD08
			public virtual bool Contains(ᜁ value)
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
				return this.ᜀ.ContainsValue(value);
			}

			// Token: 0x06005B2E RID: 23342 RVA: 0x0038DD50 File Offset: 0x0038CD50
			public virtual void CopyTo(ᜁ[] array, int arrayIndex)
			{
				int a_ = 16;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6D;
					case 1:
						if (array.Rank != 1)
						{
							num = 0;
							continue;
						}
						goto IL_A7;
					case 2:
						goto IL_3E;
					}
					if (array == null)
					{
						num = 2;
					}
					else
					{
						num = 1;
					}
				}
				IL_3E:
				throw new ArgumentNullException(RecordTableEnumerator.b("❅㩇㡉ⵋ㝍", a_));
				IL_6D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					throw new ArgumentException(RecordTableEnumerator.b("❅㩇㡉㹋⽍⥏", a_));
				}
				IL_A7:
				Array.Copy(this.ᜁ, 0, array, arrayIndex, this.ᜀ.Count);
			}

			// Token: 0x06005B2F RID: 23343 RVA: 0x0038DE20 File Offset: 0x0038CE20
			public virtual void ᜀ(Array A_0, int A_1)
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						num = 3;
						continue;
					case 2:
						if (true)
						{
						}
						break;
					case 3:
						if (A_0.Rank == 1)
						{
							goto IL_7C;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_70;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (A_0 == null)
					{
						goto IL_7C;
					}
					num = 1;
				}
				IL_70:
				throw new ArgumentException();
				IL_7C:
				Array.Copy(this.ᜁ, 0, A_0, A_1, this.ᜀ.Count);
			}

			// Token: 0x06005B30 RID: 23344 RVA: 0x0038DEC4 File Offset: 0x0038CEC4
			public virtual void ᜁ(int A_0, ᜁ A_1)
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

			// Token: 0x06005B31 RID: 23345 RVA: 0x0038DF04 File Offset: 0x0038CF04
			public virtual ᜁ ᜀ(int A_0)
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

			// Token: 0x06005B32 RID: 23346 RVA: 0x0038DF4C File Offset: 0x0038CF4C
			public virtual void ᜀ(int A_0, ᜁ A_1)
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

			// Token: 0x06005B33 RID: 23347 RVA: 0x0038DF94 File Offset: 0x0038CF94
			IEnumerator IEnumerable.ᜁ()
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
				throw new NotImplementedException();
			}

			// Token: 0x06005B34 RID: 23348 RVA: 0x0038DFD4 File Offset: 0x0038CFD4
			public virtual IEnumerator<ᜁ> GetEnumerator()
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
				return ((IEnumerable<ᜁ>)this.ᜁ).GetEnumerator();
			}

			// Token: 0x06005B35 RID: 23349 RVA: 0x0038E020 File Offset: 0x0038D020
			public virtual int ᜀ(ᜁ A_0)
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
				return Array.IndexOf<ᜁ>(this.ᜁ, A_0, 0, this.ᜀ.Count);
			}

			// Token: 0x06005B36 RID: 23350 RVA: 0x0038E074 File Offset: 0x0038D074
			public virtual bool Remove(ᜁ value)
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

			// Token: 0x06005B37 RID: 23351 RVA: 0x0038E0B4 File Offset: 0x0038D0B4
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

			// Token: 0x04002C80 RID: 11392
			private TypedSortedListEx<ᜀ, ᜁ> ᜀ;

			// Token: 0x04002C81 RID: 11393
			private ᜁ[] ᜁ;
		}
	}
}
