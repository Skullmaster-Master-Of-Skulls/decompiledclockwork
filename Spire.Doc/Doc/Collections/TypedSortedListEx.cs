using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Spire.CompoundFile.Doc;

namespace Spire.Doc.Collections
{
	// Token: 0x02000536 RID: 1334
	public class TypedSortedListEx<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary where TKey : IComparable
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600459A RID: 17818 RVA: 0x00409E94 File Offset: 0x00408E94
		// (set) Token: 0x0600459B RID: 17819 RVA: 0x00409ED8 File Offset: 0x00408ED8
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
				int a_ = 14;
				int num = 2;
				TKey[] destinationArray;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 6;
						continue;
					case 1:
						if (true)
						{
						}
						destinationArray = new TKey[value];
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_122;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 3:
						goto IL_FF;
					case 4:
						if (this.ᜃ > 0)
						{
							num = 9;
							continue;
						}
						goto IL_122;
					case 5:
						goto IL_120;
					case 6:
						if (value < this.ᜃ)
						{
							num = 5;
							continue;
						}
						num = 8;
						continue;
					case 7:
						goto IL_78;
					case 8:
						if (value > 0)
						{
							num = 1;
							continue;
						}
						this.ᜁ = new TKey[16];
						num = 3;
						continue;
					case 9:
						Array.Copy(this.ᜁ, 0, destinationArray, 0, this.ᜃ);
						num = 7;
						continue;
					}
					if (value == this.ᜁ.Length)
					{
						return;
					}
					num = 0;
				}
				IL_78:
				goto IL_122;
				IL_FF:
				return;
				IL_120:
				throw new ArgumentOutOfRangeException(ClipboardData.b("ɳ᝵ᑷཹ᥻", a_));
				IL_122:
				this.ᜁ = destinationArray;
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600459C RID: 17820 RVA: 0x0040A030 File Offset: 0x00409030
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

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600459D RID: 17821 RVA: 0x0040A074 File Offset: 0x00409074
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

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600459E RID: 17822 RVA: 0x0040A0B8 File Offset: 0x004090B8
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

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600459F RID: 17823 RVA: 0x0040A0FC File Offset: 0x004090FC
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060045A0 RID: 17824 RVA: 0x0040A138 File Offset: 0x00409138
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

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060045A1 RID: 17825 RVA: 0x0040A174 File Offset: 0x00409174
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

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060045A2 RID: 17826 RVA: 0x0040A1B0 File Offset: 0x004091B0
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

		// Token: 0x17000547 RID: 1351
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
				int a_ = 15;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜂ[key] = value;
						num = 2;
						continue;
					case 1:
						goto IL_49;
					case 2:
						goto IL_60;
					case 3:
						goto IL_CF;
					case 4:
						if (true)
						{
						}
						break;
					case 5:
						if (this.ᜂ.ContainsKey(key))
						{
							num = 0;
							continue;
						}
						this.Add(key, value);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					if (key == null)
					{
						num = 1;
					}
					else
					{
						num = 5;
					}
				}
				IL_49:
				throw new ArgumentNullException(ClipboardData.b("Ṵቶx", a_));
				IL_60:
				IL_CF:
				this.ᜄ++;
			}
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x0040A324 File Offset: 0x00409324
		public TypedSortedListEx()
		{
			this.ᜁ = new TKey[16];
			this.ᜂ = new Dictionary<TKey, TValue>(16);
			this.ᜅ = Comparer<TKey>.Default;
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x0040A35C File Offset: 0x0040935C
		public TypedSortedListEx(int initialCapacity)
		{
			int a_ = 11;
			base..ctor();
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException(ClipboardData.b("ᡰᵲᱴͶၸ᩺ᅼ㱾ﾊ", a_));
			}
			this.ᜁ = new TKey[initialCapacity];
			this.ᜂ = new Dictionary<TKey, TValue>(initialCapacity);
			this.ᜅ = Comparer<TKey>.Default;
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x0040A3B8 File Offset: 0x004093B8
		public TypedSortedListEx(IComparer<TKey> comparer) : this()
		{
			if (comparer != null)
			{
				this.ᜅ = comparer;
			}
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x0040A3DC File Offset: 0x004093DC
		public TypedSortedListEx(IComparer<TKey> comparer, int capacity) : this(comparer)
		{
			this.Capacity = capacity;
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x0040A3F8 File Offset: 0x004093F8
		public TypedSortedListEx(IDictionary<TKey, TValue> d) : this(d, null)
		{
		}

		// Token: 0x060045AA RID: 17834 RVA: 0x0040A410 File Offset: 0x00409410
		public TypedSortedListEx(IDictionary<TKey, TValue> d, IComparer<TKey> comparer)
		{
			int a_ = 8;
			this..ctor(comparer, (d != null) ? d.Count : 0);
			if (d == null)
			{
				throw new ArgumentNullException(ClipboardData.b("੭", a_));
			}
			d.Keys.CopyTo(this.ᜁ, 0);
			this.ᜂ = new Dictionary<TKey, TValue>(d);
			Array.Sort<TKey>(this.ᜁ, comparer);
			this.ᜃ = d.Count;
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x0040A48C File Offset: 0x0040948C
		public static TypedSortedListEx<TKey, TValue> Synchronized(TypedSortedListEx<TKey, TValue> list)
		{
			int a_ = 6;
			if (list == null)
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
					throw new ArgumentNullException(ClipboardData.b("kݭͯٱ", a_));
				}
			}
			throw new NotImplementedException();
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x0040A4F0 File Offset: 0x004094F0
		public virtual void Add(TKey key, TValue value)
		{
			int a_ = 9;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_75;
				case 2:
					goto IL_41;
				case 3:
					if (!this.ᜂ.ContainsKey(key))
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_A1;
						}
					}
					num = 0;
					continue;
				}
				if (true)
				{
				}
				if (key == null)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
			}
			IL_41:
			throw new ArgumentNullException(ClipboardData.b("Ѯᑰੲ", a_));
			IL_75:
			throw new ArgumentException(ClipboardData.b("⭮ѰͲᥴṶ᩸᩺ॼ᩾", a_));
			IL_A1:
			if (false)
			{
			}
			int num2 = Array.BinarySearch<TKey>(this.ᜁ, 0, this.ᜃ, key, this.ᜅ);
			this.ᜀ(~num2, key, value);
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x0040A5D4 File Offset: 0x004095D4
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

		// Token: 0x060045AE RID: 17838 RVA: 0x0040A640 File Offset: 0x00409640
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

		// Token: 0x060045AF RID: 17839 RVA: 0x0040A6D8 File Offset: 0x004096D8
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
					int num2 = 4;
					for (;;)
					{
						TKey key;
						TValue tvalue;
						switch (num2)
						{
						case 0:
						{
							IDocCloneable docCloneable;
							if (docCloneable != null)
							{
								num2 = 5;
								continue;
							}
							goto IL_92;
						}
						case 1:
							if (num < count)
							{
								key = this.GetKey(num);
								tvalue = this.ᜂ[key];
								IDocCloneable docCloneable = tvalue as IDocCloneable;
								num2 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_90;
							default:
								if (false)
								{
								}
								num2 = 2;
								continue;
							}
							break;
						case 2:
							return typedSortedListEx;
						case 3:
							goto IL_10B;
						case 4:
							goto IL_90;
						case 5:
						{
							IDocCloneable docCloneable;
							tvalue = (TValue)((object)docCloneable.Clone());
							num2 = 6;
							continue;
						}
						case 6:
							goto IL_92;
						}
						break;
						IL_92:
						typedSortedListEx.Add(key, tvalue);
						num++;
						num2 = 3;
						continue;
						IL_10B:
						num2 = 1;
						continue;
						IL_90:
						goto IL_10B;
					}
				}
				return typedSortedListEx;
			}
			}
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x0040A82C File Offset: 0x0040982C
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

		// Token: 0x060045B1 RID: 17841 RVA: 0x0040A874 File Offset: 0x00409874
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

		// Token: 0x060045B2 RID: 17842 RVA: 0x0040A8BC File Offset: 0x004098BC
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

		// Token: 0x060045B3 RID: 17843 RVA: 0x0040A904 File Offset: 0x00409904
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			int a_ = 8;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (array.Rank != 1)
					{
						num = 4;
						continue;
					}
					num = 3;
					continue;
				case 1:
					goto IL_149;
				case 2:
					goto IL_9D;
				case 3:
					goto IL_175;
				case 4:
					goto IL_E0;
				case 5:
					return;
				case 6:
					goto IL_149;
				case 7:
				{
					if (array.Length - arrayIndex < this.Count)
					{
						num = 2;
						continue;
					}
					int num2 = 0;
					num = 1;
					continue;
				}
				case 8:
				{
					int num2;
					if (num2 >= this.Count)
					{
						num = 5;
						continue;
					}
					KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(this.ᜁ[num2], this.ᜂ[this.ᜁ[num2]]);
					array.SetValue(keyValuePair, num2 + arrayIndex);
					num2++;
					num = 6;
					continue;
				}
				case 9:
					goto IL_187;
				case 11:
					goto IL_5C;
				}
				if (array == null)
				{
					if (true)
					{
					}
					num = 11;
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
				IL_175:
				if (arrayIndex < 0)
				{
					num = 9;
					continue;
				}
				num = 7;
				continue;
				IL_149:
				num = 8;
			}
			IL_5C:
			throw new ArgumentNullException(ClipboardData.b("཭ɯqᕳཱུ", a_));
			IL_9D:
			throw new ArgumentException();
			IL_E0:
			throw new ArgumentException(ClipboardData.b("཭ɯqᕳཱུ", a_));
			IL_187:
			throw new ArgumentOutOfRangeException(ClipboardData.b("཭ɯqᕳཱུㅷᑹ᡻᭽", a_));
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x0040AAC4 File Offset: 0x00409AC4
		public virtual TValue GetByIndex(int index)
		{
			int a_ = 17;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_76;
				case 2:
					if (index >= this.ᜃ)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2D;
					default:
						goto IL_8E;
					}
					break;
				case 3:
					num = 2;
					continue;
				}
				goto IL_29;
				IL_2D:
				num = 3;
				continue;
				IL_29:
				if (index >= 0)
				{
					goto IL_2D;
				}
				break;
			}
			IL_37:
			throw new ArgumentOutOfRangeException(ClipboardData.b("Ṷ᝸ὺ᡼ݾ", a_));
			IL_76:
			goto IL_37;
			IL_8E:
			if (false)
			{
			}
			return this.ᜂ[this.ᜁ[index]];
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x0040AB7C File Offset: 0x00409B7C
		public virtual TKey GetKey(int index)
		{
			int a_ = 18;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (index >= this.ᜃ)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2D;
					default:
						goto IL_86;
					}
					break;
				case 2:
					num = 1;
					continue;
				case 3:
					goto IL_6E;
				}
				goto IL_29;
				IL_2D:
				num = 2;
				continue;
				IL_29:
				if (index >= 0)
				{
					goto IL_2D;
				}
				break;
			}
			IL_37:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᅷᑹ᡻᭽", a_));
			IL_6E:
			goto IL_37;
			IL_86:
			if (true)
			{
			}
			if (false)
			{
			}
			return this.ᜁ[index];
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x0040AC2C File Offset: 0x00409C2C
		public virtual IList<TKey> GetKeyList()
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
					this.ᜆ = new TypedSortedListEx<TKey, TValue>.ᜃ(this);
					num = 2;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_60;
					}
					break;
				}
				IL_24:
				if (this.ᜆ == null)
				{
					num = 0;
					continue;
				}
				goto IL_72;
				goto IL_24;
			}
			IL_60:
			if (false)
			{
			}
			IL_72:
			return this.ᜆ;
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x0040ACB4 File Offset: 0x00409CB4
		public virtual IList<TValue> GetValueList()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5D;
				case 1:
					goto IL_5D;
				case 2:
					goto IL_47;
				}
				if (this.ᜇ == null)
				{
					num = 2;
					continue;
				}
				this.ᜇ.ᜀ();
				num = 0;
				continue;
				IL_47:
				this.ᜇ = new TypedSortedListEx<TKey, TValue>.ᜂ(this);
				num = 1;
				continue;
				IL_5D:
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_47;
				default:
					goto IL_7B;
				}
			}
			IL_7B:
			if (false)
			{
			}
			return this.ᜇ;
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x0040AD54 File Offset: 0x00409D54
		public virtual int IndexOfKey(TKey key)
		{
			int a_ = 17;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_6B;
				case 2:
				{
					int num2;
					if (num2 < 0)
					{
						num = 1;
						continue;
					}
					goto IL_A7;
				}
				case 3:
					goto IL_39;
				}
				if (key == null)
				{
					num = 3;
				}
				else
				{
					int num2 = Array.BinarySearch<TKey>(this.ᜁ, 0, this.ᜃ, key, this.ᜅ);
					num = 2;
				}
			}
			IL_39:
			throw new ArgumentNullException(ClipboardData.b("ᱶᱸɺ", a_));
			IL_6B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_A7:
				if (true)
				{
				}
				int num2;
				return num2;
			}
			default:
				if (false)
				{
				}
				return -1;
			}
		}

		// Token: 0x060045B9 RID: 17849 RVA: 0x0040AE14 File Offset: 0x00409E14
		public virtual int IndexOfValue(TValue value)
		{
			object obj;
			for (;;)
			{
				obj = null;
				IDictionaryEnumerator dictionaryEnumerator = this.ᜂ.GetEnumerator();
				dictionaryEnumerator.Reset();
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return -1;
					case 1:
						goto IL_55;
					case 2:
						obj = dictionaryEnumerator.Key;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_55;
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
					case 3:
						if (dictionaryEnumerator.Value.Equals(value))
						{
							num = 2;
							continue;
						}
						goto IL_6D;
					case 4:
						goto IL_55;
					case 5:
						if (!dictionaryEnumerator.MoveNext())
						{
							num = 4;
							continue;
						}
						num = 3;
						continue;
					case 6:
						goto IL_6D;
					case 7:
						if (obj == null)
						{
							num = 0;
							continue;
						}
						goto IL_F2;
					}
					break;
					IL_55:
					num = 7;
					continue;
					IL_6D:
					num = 5;
				}
			}
			return -1;
			IL_F2:
			return Array.IndexOf(this.ᜁ, obj, 0, this.ᜃ);
		}

		// Token: 0x060045BA RID: 17850 RVA: 0x0040AF28 File Offset: 0x00409F28
		public virtual void RemoveAt(int index)
		{
			int a_ = 18;
			int num = 0;
			TKey key;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (index >= this.ᜃ)
					{
						num = 3;
						continue;
					}
					this.ᜃ--;
					key = this.ᜁ[index];
					goto IL_70;
				case 2:
					if (true)
					{
					}
					num = 1;
					continue;
				case 3:
					goto IL_C1;
				case 4:
					if (index < this.ᜃ)
					{
						num = 5;
						continue;
					}
					goto IL_109;
				case 5:
					Array.Copy(this.ᜁ, index + 1, this.ᜁ, index, this.ᜃ - index);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 6:
					goto IL_107;
				}
				if (index >= 0)
				{
					num = 2;
					continue;
				}
				break;
				IL_70:
				num = 4;
			}
			IL_8E:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ᅷᑹ᡻᭽", a_));
			IL_C1:
			goto IL_8E;
			IL_107:
			IL_109:
			this.ᜁ[this.ᜃ] = default(TKey);
			this.ᜂ.Remove(key);
			this.ᜄ++;
		}

		// Token: 0x060045BB RID: 17851 RVA: 0x0040B074 File Offset: 0x0040A074
		public virtual bool Remove(TKey key)
		{
			bool result;
			for (;;)
			{
				int num = this.IndexOfKey(key);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return result;
					case 1:
						this.RemoveAt(num);
						result = true;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 2:
						if (num >= 0)
						{
							num2 = 1;
							continue;
						}
						result = false;
						num2 = 3;
						continue;
					case 3:
						return result;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x060045BC RID: 17852 RVA: 0x0040B108 File Offset: 0x0040A108
		public virtual void SetByIndex(int index, TValue value)
		{
			int a_ = 19;
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
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2D;
					default:
						goto IL_86;
					}
					break;
				case 3:
					num = 2;
					continue;
				}
				goto IL_29;
				IL_2D:
				num = 3;
				continue;
				IL_29:
				if (index >= 0)
				{
					goto IL_2D;
				}
				break;
			}
			IL_37:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ၸᕺ᥼᩾呂", a_));
			IL_6E:
			goto IL_37;
			IL_86:
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ[this.ᜁ[index]] = value;
			this.ᜄ++;
		}

		// Token: 0x060045BD RID: 17853 RVA: 0x0040B1D0 File Offset: 0x0040A1D0
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

		// Token: 0x060045BE RID: 17854 RVA: 0x0040B218 File Offset: 0x0040A218
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

		// Token: 0x060045BF RID: 17855 RVA: 0x0040B258 File Offset: 0x0040A258
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
			return new TypedSortedListEx<TKey, TValue>.ᜁ(this, 0, this.ᜃ);
		}

		// Token: 0x060045C0 RID: 17856 RVA: 0x0040B2A0 File Offset: 0x0040A2A0
		private void ᜀ(int A_0, TKey A_1, TValue A_2)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
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
						goto IL_7B;
					}
					break;
				case 3:
					this.ᜀ(this.ᜃ + 1);
					num = 1;
					continue;
				case 4:
					if (A_0 < this.ᜃ)
					{
						num = 0;
						continue;
					}
					goto IL_D0;
				case 5:
					goto IL_79;
				}
				if (this.ᜃ == this.ᜁ.Length)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				IL_7B:
				num = 4;
				continue;
				IL_97:
				Array.Copy(this.ᜁ, A_0, this.ᜁ, A_0 + 1, this.ᜃ - A_0);
				num = 5;
			}
			IL_79:
			IL_D0:
			this.ᜁ[A_0] = A_1;
			this.ᜂ[A_1] = A_2;
			this.ᜃ++;
			this.ᜄ++;
		}

		// Token: 0x060045C1 RID: 17857 RVA: 0x0040B3B4 File Offset: 0x0040A3B4
		private void ᜀ(int A_0)
		{
			int num = 0;
			int num3;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 1:
					num2 = 16;
					goto IL_72;
				case 2:
					num2 = this.ᜁ.Length * 2;
					goto IL_72;
				case 3:
					goto IL_68;
				case 4:
					num = 2;
					continue;
				case 5:
					goto IL_5E;
				case 6:
					if (num3 >= A_0)
					{
						goto IL_B7;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5E;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				if (this.ᜁ.Length != 0)
				{
					num = 4;
					continue;
				}
				num = 1;
				continue;
				IL_5E:
				num3 = A_0;
				num = 3;
				continue;
				IL_72:
				num3 = num2;
				num = 6;
			}
			IL_68:
			if (true)
			{
			}
			IL_B7:
			this.Capacity = num3;
		}

		// Token: 0x060045C2 RID: 17858 RVA: 0x0040B480 File Offset: 0x0040A480
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

		// Token: 0x060045C3 RID: 17859 RVA: 0x0040B4D4 File Offset: 0x0040A4D4
		public bool Contains(object key)
		{
			int num = 1;
			bool result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_83;
				case 2:
				{
					TKey key2 = (TKey)((object)key);
					result = this.ContainsKey(key2);
					num = 0;
					continue;
				}
				case 3:
					goto IL_4E;
				}
				if (true)
				{
				}
				if (key is TKey)
				{
					num = 2;
				}
				else
				{
					result = false;
					num = 3;
				}
			}
			IL_4E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_83:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			return result;
		}

		// Token: 0x060045C4 RID: 17860 RVA: 0x0040B568 File Offset: 0x0040A568
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

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060045C5 RID: 17861 RVA: 0x0040B5B0 File Offset: 0x0040A5B0
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

		// Token: 0x060045C6 RID: 17862 RVA: 0x0040B5F8 File Offset: 0x0040A5F8
		public void Remove(object key)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_36;
					default:
						goto IL_6B;
					}
					break;
				case 1:
					this.Remove((TKey)((object)key));
					num = 0;
					continue;
				}
				goto IL_1C;
				IL_36:
				num = 1;
				continue;
				IL_1C:
				if (true)
				{
				}
				if (key is TKey)
				{
					goto IL_36;
				}
				return;
			}
			IL_6B:
			if (false)
			{
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060045C7 RID: 17863 RVA: 0x0040B678 File Offset: 0x0040A678
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

		// Token: 0x1700054A RID: 1354
		public object this[object key]
		{
			get
			{
				int num = 2;
				TValue tvalue;
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
						case 0:
							tvalue = default(TValue);
							goto IL_74;
						case 1:
							goto IL_5E;
						case 3:
							goto IL_7C;
						}
						if (!(key is TKey))
						{
							num = 0;
							continue;
						}
						num = 1;
						continue;
					}
					IL_74:
					num = 3;
				}
				IL_5E:
				TValue tvalue2 = this[(TKey)((object)key)];
				goto IL_89;
				IL_7C:
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

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x0040B7AC File Offset: 0x0040A7AC
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

		// Token: 0x060045CB RID: 17867 RVA: 0x0040B7F0 File Offset: 0x0040A7F0
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

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x0040B838 File Offset: 0x0040A838
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

		// Token: 0x060045CD RID: 17869 RVA: 0x0040B880 File Offset: 0x0040A880
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

		// Token: 0x060045CE RID: 17870 RVA: 0x0040B8D0 File Offset: 0x0040A8D0
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			int num = 2;
			bool result;
			for (;;)
			{
				TValue tvalue;
				switch (num)
				{
				case 0:
					goto IL_9A;
				case 1:
					if (true)
					{
					}
					result = tvalue.Equals(item.Value);
					num = 0;
					continue;
				case 3:
					goto IL_57;
				}
				if (this.TryGetValue(item.Key, out tvalue))
				{
					num = 1;
				}
				else
				{
					result = false;
					num = 3;
				}
			}
			IL_57:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_9A:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			return result;
		}

		// Token: 0x060045CF RID: 17871 RVA: 0x0040B97C File Offset: 0x0040A97C
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

		// Token: 0x060045D0 RID: 17872 RVA: 0x0040B9C4 File Offset: 0x0040A9C4
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

		// Token: 0x04003670 RID: 13936
		private const int ᜀ = 16;

		// Token: 0x04003671 RID: 13937
		private TKey[] ᜁ;

		// Token: 0x04003672 RID: 13938
		private Dictionary<TKey, TValue> ᜂ;

		// Token: 0x04003673 RID: 13939
		private int ᜃ;

		// Token: 0x04003674 RID: 13940
		private int ᜄ;

		// Token: 0x04003675 RID: 13941
		private IComparer<TKey> ᜅ;

		// Token: 0x04003676 RID: 13942
		private TypedSortedListEx<TKey, TValue>.ᜃ ᜆ;

		// Token: 0x04003677 RID: 13943
		private TypedSortedListEx<TKey, TValue>.ᜂ ᜇ;

		// Token: 0x02000537 RID: 1335
		[Serializable]
		private class ᜁ : IEnumerator<KeyValuePair<ᜀ, ᜁ>>, IDocCloneable
		{
			// Token: 0x060045D1 RID: 17873 RVA: 0x0040BA0C File Offset: 0x0040AA0C
			internal ᜁ(TypedSortedListEx<ᜀ, ᜁ> A_0, int A_1, int A_2)
			{
				this.ᜀ = A_0;
				this.ᜃ = A_1;
				this.ᜄ = A_1;
				this.ᜅ = A_1 + A_2;
				this.ᜆ = this.ᜀ.ᜄ;
				this.ᜇ = false;
			}

			// Token: 0x060045D2 RID: 17874 RVA: 0x0040BA58 File Offset: 0x0040AA58
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

			// Token: 0x060045D3 RID: 17875 RVA: 0x0040BA94 File Offset: 0x0040AA94
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

			// Token: 0x060045D4 RID: 17876 RVA: 0x0040BAD8 File Offset: 0x0040AAD8
			public virtual ᜀ ᜃ()
			{
				int num = 0;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					}
					if (false)
					{
					}
					switch (num)
					{
					case 1:
						goto IL_61;
					case 2:
						if (!this.ᜇ)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_91;
					case 3:
						goto IL_89;
					}
					if (this.ᜆ != this.ᜀ.ᜄ)
					{
						num = 1;
					}
					else
					{
						num = 2;
					}
				}
				IL_61:
				throw new InvalidOperationException();
				IL_89:
				throw new InvalidOperationException();
				IL_91:
				return this.ᜁ;
			}

			// Token: 0x060045D5 RID: 17877 RVA: 0x0040BB7C File Offset: 0x0040AB7C
			public virtual bool MoveNext()
			{
				int num = 3;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					}
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_D6;
					case 1:
						goto IL_61;
					case 2:
						if (this.ᜃ < this.ᜅ)
						{
							num = 0;
							continue;
						}
						goto IL_E6;
					}
					if (this.ᜆ != this.ᜀ.ᜄ)
					{
						num = 1;
					}
					else
					{
						num = 2;
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

			// Token: 0x060045D6 RID: 17878 RVA: 0x0040BC90 File Offset: 0x0040AC90
			public virtual KeyValuePair<ᜀ, ᜁ> ᜂ()
			{
				int num = 3;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_89;
						case 1:
							if (!this.ᜇ)
							{
								num = 0;
								continue;
							}
							goto IL_91;
						case 2:
							goto IL_57;
						}
						if (this.ᜆ != this.ᜀ.ᜄ)
						{
							num = 2;
						}
						else
						{
							if (true)
							{
							}
							num = 1;
						}
						break;
					}
				}
				IL_57:
				throw new InvalidOperationException();
				IL_89:
				throw new InvalidOperationException();
				IL_91:
				return new KeyValuePair<ᜀ, ᜁ>(this.ᜁ, this.ᜂ);
			}

			// Token: 0x060045D7 RID: 17879 RVA: 0x0040BD40 File Offset: 0x0040AD40
			public virtual KeyValuePair<ᜀ, ᜁ> ᜁ()
			{
				if (!this.ᜇ)
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
						throw new InvalidOperationException();
					}
				}
				return new KeyValuePair<ᜀ, ᜁ>(this.ᜁ, this.ᜂ);
			}

			// Token: 0x060045D8 RID: 17880 RVA: 0x0040BD9C File Offset: 0x0040AD9C
			object IEnumerator.ᜄ()
			{
				if (!this.ᜇ)
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
						throw new InvalidOperationException();
					}
				}
				return new KeyValuePair<ᜀ, ᜁ>(this.ᜁ, this.ᜂ);
			}

			// Token: 0x060045D9 RID: 17881 RVA: 0x0040BE00 File Offset: 0x0040AE00
			public virtual object ᜅ()
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_3B;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_79;
						default:
							goto IL_53;
						}
						break;
					case 3:
						goto IL_79;
					}
					if (this.ᜆ != this.ᜀ.ᜄ)
					{
						num = 1;
						continue;
					}
					num = 3;
					continue;
					IL_79:
					if (this.ᜇ)
					{
						goto IL_91;
					}
					num = 2;
				}
				IL_3B:
				throw new InvalidOperationException();
				IL_53:
				if (false)
				{
				}
				if (true)
				{
				}
				throw new InvalidOperationException();
				IL_91:
				return this.ᜂ;
			}

			// Token: 0x060045DA RID: 17882 RVA: 0x0040BEAC File Offset: 0x0040AEAC
			public virtual void Reset()
			{
				if (this.ᜆ != this.ᜀ.ᜄ)
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
						throw new InvalidOperationException();
					}
				}
				this.ᜃ = this.ᜄ;
				this.ᜇ = false;
				this.ᜁ = default(ᜀ);
				this.ᜂ = default(ᜁ);
			}

			// Token: 0x04003678 RID: 13944
			private TypedSortedListEx<ᜀ, ᜁ> ᜀ;

			// Token: 0x04003679 RID: 13945
			private ᜀ ᜁ;

			// Token: 0x0400367A RID: 13946
			private ᜁ ᜂ;

			// Token: 0x0400367B RID: 13947
			private int ᜃ;

			// Token: 0x0400367C RID: 13948
			private int ᜄ;

			// Token: 0x0400367D RID: 13949
			private int ᜅ;

			// Token: 0x0400367E RID: 13950
			private int ᜆ;

			// Token: 0x0400367F RID: 13951
			private bool ᜇ;
		}

		// Token: 0x02000538 RID: 1336
		private class ᜀ : IEnumerator<ᜀ>
		{
			// Token: 0x060045DB RID: 17883 RVA: 0x0040BF30 File Offset: 0x0040AF30
			public ᜀ(TypedSortedListEx<ᜀ, ᜁ> A_0)
			{
				int a_ = 19;
				this.ᜁ = -1;
				base..ctor();
				if (A_0 == null)
				{
					throw new ArgumentNullException(ClipboardData.b("ᕸቺ๼୾", a_));
				}
				this.ᜀ = A_0;
				this.ᜂ = this.ᜀ.ᜄ;
			}

			// Token: 0x060045DC RID: 17884 RVA: 0x0040BF84 File Offset: 0x0040AF84
			public ᜀ ᜀ()
			{
				int a_ = 5;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						goto IL_83;
					case 2:
						if (this.ᜁ >= this.ᜀ.ᜃ)
						{
							num = 1;
							continue;
						}
						goto IL_D9;
					case 4:
						goto IL_56;
					case 5:
						if (this.ᜁ >= 0)
						{
							num = 0;
							continue;
						}
						goto IL_B7;
					}
					if (this.ᜂ != this.ᜀ.ᜄ)
					{
						num = 4;
					}
					else
					{
						num = 5;
					}
				}
				IL_56:
				if (true)
				{
				}
				goto IL_A3;
				IL_83:
				goto IL_B7;
				IL_A3:
				throw new InvalidOperationException(ClipboardData.b("㭪౬ᵮᑰᵲŴ坶᩸ᑺᅼ፾권떔漢쒠잢", a_));
				IL_B7:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A3;
				default:
					if (false)
					{
					}
					throw new InvalidOperationException();
				}
				IL_D9:
				return this.ᜀ.ᜁ[this.ᜁ];
			}

			// Token: 0x060045DD RID: 17885 RVA: 0x0040C080 File Offset: 0x0040B080
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

			// Token: 0x060045DE RID: 17886 RVA: 0x0040C0BC File Offset: 0x0040B0BC
			object IEnumerator.ᜁ()
			{
				int a_ = 18;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜁ >= this.ᜀ.ᜃ)
						{
							num = 1;
							continue;
						}
						num = 4;
						continue;
					case 1:
						goto IL_9F;
					case 3:
						num = 0;
						continue;
					case 4:
						if (this.ᜂ != this.ᜀ.ᜄ)
						{
							num = 5;
							continue;
						}
						goto IL_D9;
					case 5:
						goto IL_71;
					}
					if (this.ᜁ < 0)
					{
						goto IL_73;
					}
					num = 3;
				}
				IL_71:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_9F:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					throw new InvalidOperationException(ClipboardData.b("⡷᭹๻᭽ꒃﶓ秊몙ﾝ펟芡잣캥즧쒩쮫쮭풯", a_));
				}
				IL_73:
				throw new InvalidOperationException();
				IL_D9:
				return this.ᜀ.ᜁ[this.ᜁ];
			}

			// Token: 0x060045DF RID: 17887 RVA: 0x0040C1C0 File Offset: 0x0040B1C0
			public bool MoveNext()
			{
				int a_ = 12;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜁ = -1;
						num = 2;
						continue;
					case 1:
						if (this.ᜁ < 0)
						{
							num = 6;
							continue;
						}
						goto IL_113;
					case 2:
						goto IL_E8;
					case 3:
						goto IL_5D;
					case 4:
						if (this.ᜁ < this.ᜀ.ᜃ)
						{
							goto IL_131;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_113;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 5:
						goto IL_5B;
					case 6:
						if (true)
						{
						}
						this.ᜁ = 0;
						num = 8;
						continue;
					case 8:
						goto IL_5D;
					}
					if (this.ᜂ != this.ᜀ.ᜄ)
					{
						num = 5;
						continue;
					}
					num = 1;
					continue;
					IL_5D:
					num = 4;
					continue;
					IL_113:
					this.ᜁ++;
					num = 3;
				}
				IL_5B:
				throw new InvalidOperationException(ClipboardData.b("≱ᕳѵᵷᑹࡻ幽ﾏﲑ뒓聯벛ﶝ좟쎡쪣솥춧캩", a_));
				IL_E8:
				IL_131:
				return this.ᜁ >= 0;
			}

			// Token: 0x060045E0 RID: 17888 RVA: 0x0040C30C File Offset: 0x0040B30C
			public void Reset()
			{
				int a_ = 2;
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
						throw new InvalidOperationException(ClipboardData.b("㡧୩ṫ୭ṯٱ味ᕵ᝷ᙹၻ᭽ꪉﮋ늑ﺕ聯ﮛﮝ쒟", a_));
					}
				}
				this.ᜁ = -1;
			}

			// Token: 0x04003680 RID: 13952
			private TypedSortedListEx<ᜀ, ᜁ> ᜀ;

			// Token: 0x04003681 RID: 13953
			private int ᜁ;

			// Token: 0x04003682 RID: 13954
			private int ᜂ;
		}

		// Token: 0x02000539 RID: 1337
		[DefaultMember("Item")]
		[Serializable]
		private class ᜃ : IList<ᜀ>
		{
			// Token: 0x060045E1 RID: 17889 RVA: 0x0040C380 File Offset: 0x0040B380
			internal ᜃ(TypedSortedListEx<ᜀ, ᜁ> A_0)
			{
				this.ᜀ = A_0;
			}

			// Token: 0x060045E2 RID: 17890 RVA: 0x0040C39C File Offset: 0x0040B39C
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

			// Token: 0x060045E3 RID: 17891 RVA: 0x0040C3E4 File Offset: 0x0040B3E4
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

			// Token: 0x060045E4 RID: 17892 RVA: 0x0040C420 File Offset: 0x0040B420
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

			// Token: 0x060045E5 RID: 17893 RVA: 0x0040C45C File Offset: 0x0040B45C
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

			// Token: 0x060045E6 RID: 17894 RVA: 0x0040C4A4 File Offset: 0x0040B4A4
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

			// Token: 0x060045E7 RID: 17895 RVA: 0x0040C4EC File Offset: 0x0040B4EC
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

			// Token: 0x060045E8 RID: 17896 RVA: 0x0040C52C File Offset: 0x0040B52C
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

			// Token: 0x060045E9 RID: 17897 RVA: 0x0040C56C File Offset: 0x0040B56C
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

			// Token: 0x060045EA RID: 17898 RVA: 0x0040C5B4 File Offset: 0x0040B5B4
			public virtual void CopyTo(ᜀ[] array, int arrayIndex)
			{
				int a_ = 10;
				while (array == null)
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
						throw new ArgumentException(ClipboardData.b("ᅯqٳ᝵ŷ", a_));
					}
				}
				Array.Copy(this.ᜀ.ᜁ, 0, array, arrayIndex, this.ᜀ.Count);
			}

			// Token: 0x060045EB RID: 17899 RVA: 0x0040C630 File Offset: 0x0040B630
			public virtual void ᜀ(Array A_0, int A_1)
			{
				int a_ = 9;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6D;
					case 1:
						if (A_0.Rank != 1)
						{
							num = 0;
							continue;
						}
						goto IL_A7;
					case 2:
						goto IL_34;
					}
					if (A_0 == null)
					{
						num = 2;
					}
					else
					{
						num = 1;
					}
				}
				IL_34:
				throw new ArgumentNullException(ClipboardData.b("๮ͰŲᑴ๶", a_));
				IL_6D:
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
					throw new ArgumentException(ClipboardData.b("๮ͰŲᑴ๶", a_));
				}
				IL_A7:
				Array.Copy(this.ᜀ.ᜁ, 0, A_0, A_1, this.ᜀ.Count);
			}

			// Token: 0x060045EC RID: 17900 RVA: 0x0040C704 File Offset: 0x0040B704
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

			// Token: 0x060045ED RID: 17901 RVA: 0x0040C744 File Offset: 0x0040B744
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

			// Token: 0x060045EE RID: 17902 RVA: 0x0040C78C File Offset: 0x0040B78C
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

			// Token: 0x060045EF RID: 17903 RVA: 0x0040C7CC File Offset: 0x0040B7CC
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

			// Token: 0x060045F0 RID: 17904 RVA: 0x0040C814 File Offset: 0x0040B814
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

			// Token: 0x060045F1 RID: 17905 RVA: 0x0040C85C File Offset: 0x0040B85C
			public virtual int ᜀ(ᜀ A_0)
			{
				int a_ = 14;
				int num = 3;
				for (;;)
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
						int num2;
						switch (num)
						{
						case 0:
							return -1;
						case 1:
							goto IL_67;
						case 2:
							if (num2 < 0)
							{
								goto IL_A3;
							}
							return num2;
						case 3:
							if (true)
							{
							}
							break;
						}
						if (A_0 == null)
						{
							num = 1;
							continue;
						}
						num2 = Array.BinarySearch<ᜀ>(this.ᜀ.ᜁ, 0, this.ᜀ.Count, A_0, this.ᜀ.ᜅ);
						num = 2;
						continue;
					}
					}
					IL_A3:
					num = 0;
				}
				IL_67:
				throw new ArgumentNullException(ClipboardData.b("έ፵ŷ", a_));
			}

			// Token: 0x060045F2 RID: 17906 RVA: 0x0040C930 File Offset: 0x0040B930
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

			// Token: 0x060045F3 RID: 17907 RVA: 0x0040C970 File Offset: 0x0040B970
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

			// Token: 0x04003683 RID: 13955
			private TypedSortedListEx<ᜀ, ᜁ> ᜀ;
		}

		// Token: 0x0200053A RID: 1338
		[DefaultMember("Item")]
		[Serializable]
		private class ᜂ : IList<ᜁ>
		{
			// Token: 0x060045F4 RID: 17908 RVA: 0x0040C9B0 File Offset: 0x0040B9B0
			internal ᜂ(TypedSortedListEx<ᜀ, ᜁ> A_0)
			{
				this.ᜀ = A_0;
				this.ᜀ();
			}

			// Token: 0x060045F5 RID: 17909 RVA: 0x0040C9D0 File Offset: 0x0040B9D0
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

			// Token: 0x060045F6 RID: 17910 RVA: 0x0040CA74 File Offset: 0x0040BA74
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

			// Token: 0x060045F7 RID: 17911 RVA: 0x0040CABC File Offset: 0x0040BABC
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

			// Token: 0x060045F8 RID: 17912 RVA: 0x0040CAF8 File Offset: 0x0040BAF8
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

			// Token: 0x060045F9 RID: 17913 RVA: 0x0040CB34 File Offset: 0x0040BB34
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

			// Token: 0x060045FA RID: 17914 RVA: 0x0040CB7C File Offset: 0x0040BB7C
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

			// Token: 0x060045FB RID: 17915 RVA: 0x0040CBC4 File Offset: 0x0040BBC4
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

			// Token: 0x060045FC RID: 17916 RVA: 0x0040CC04 File Offset: 0x0040BC04
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

			// Token: 0x060045FD RID: 17917 RVA: 0x0040CC44 File Offset: 0x0040BC44
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

			// Token: 0x060045FE RID: 17918 RVA: 0x0040CC8C File Offset: 0x0040BC8C
			public virtual void CopyTo(ᜁ[] array, int arrayIndex)
			{
				int a_ = 7;
				for (;;)
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (array.Rank != 1)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							goto IL_A7;
						case 1:
							goto IL_91;
						case 2:
							goto IL_34;
						}
						if (array == null)
						{
							num = 2;
						}
						else
						{
							num = 0;
						}
					}
					IL_34:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				if (false)
				{
				}
				throw new ArgumentNullException(ClipboardData.b("౬ᵮͰቲ౴", a_));
				IL_91:
				throw new ArgumentException(ClipboardData.b("౬ᵮͰŲᑴ๶", a_));
				IL_A7:
				Array.Copy(this.ᜁ, 0, array, arrayIndex, this.ᜀ.Count);
			}

			// Token: 0x060045FF RID: 17919 RVA: 0x0040CD5C File Offset: 0x0040BD5C
			public virtual void ᜀ(Array A_0, int A_1)
			{
				for (;;)
				{
					IL_00:
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (A_0.Rank != 1)
							{
								num = 1;
								continue;
							}
							goto IL_7C;
						case 1:
							goto IL_7A;
						case 2:
							num = 0;
							continue;
						case 3:
							if (true)
							{
							}
							break;
						}
						if (A_0 == null)
						{
							goto IL_7C;
						}
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
							break;
						}
					}
				}
				IL_7A:
				throw new ArgumentException();
				IL_7C:
				Array.Copy(this.ᜁ, 0, A_0, A_1, this.ᜀ.Count);
			}

			// Token: 0x06004600 RID: 17920 RVA: 0x0040CE00 File Offset: 0x0040BE00
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

			// Token: 0x06004601 RID: 17921 RVA: 0x0040CE40 File Offset: 0x0040BE40
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

			// Token: 0x06004602 RID: 17922 RVA: 0x0040CE88 File Offset: 0x0040BE88
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

			// Token: 0x06004603 RID: 17923 RVA: 0x0040CED0 File Offset: 0x0040BED0
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

			// Token: 0x06004604 RID: 17924 RVA: 0x0040CF10 File Offset: 0x0040BF10
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

			// Token: 0x06004605 RID: 17925 RVA: 0x0040CF5C File Offset: 0x0040BF5C
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

			// Token: 0x06004606 RID: 17926 RVA: 0x0040CFB0 File Offset: 0x0040BFB0
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

			// Token: 0x06004607 RID: 17927 RVA: 0x0040CFF0 File Offset: 0x0040BFF0
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

			// Token: 0x04003684 RID: 13956
			private TypedSortedListEx<ᜀ, ᜁ> ᜀ;

			// Token: 0x04003685 RID: 13957
			private ᜁ[] ᜁ;
		}
	}
}
