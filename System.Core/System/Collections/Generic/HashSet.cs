using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Collections.Generic
{
	// Token: 0x02000095 RID: 149
	[DebuggerTypeProxy(typeof(HashSetDebugView<>))]
	[DebuggerDisplay("Count = {Count}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public class HashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable, ISerializable, IDeserializationCallback, ISet<T>, IReadOnlyCollection<T>
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000A28C File Offset: 0x0000848C
		[__DynamicallyInvokable]
		public HashSet() : this(EqualityComparer<T>.Default)
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000A299 File Offset: 0x00008499
		public HashSet(int capacity) : this(capacity, EqualityComparer<T>.Default)
		{
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000A2A7 File Offset: 0x000084A7
		[__DynamicallyInvokable]
		public HashSet(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			this.m_comparer = comparer;
			this.m_lastIndex = 0;
			this.m_count = 0;
			this.m_freeList = -1;
			this.m_version = 0;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000A2DC File Offset: 0x000084DC
		[__DynamicallyInvokable]
		public HashSet(IEnumerable<T> collection) : this(collection, EqualityComparer<T>.Default)
		{
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000A2EC File Offset: 0x000084EC
		[__DynamicallyInvokable]
		public HashSet(IEnumerable<T> collection, IEqualityComparer<T> comparer) : this(comparer)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			HashSet<T> hashSet = collection as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				this.CopyFrom(hashSet);
				return;
			}
			ICollection<T> collection2 = collection as ICollection<T>;
			int capacity = (collection2 == null) ? 0 : collection2.Count;
			this.Initialize(capacity);
			this.UnionWith(collection);
			if (this.m_count > 0 && this.m_slots.Length / this.m_count > 3)
			{
				this.TrimExcess();
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000A36C File Offset: 0x0000856C
		private void CopyFrom(HashSet<T> source)
		{
			int count = source.m_count;
			if (count == 0)
			{
				return;
			}
			int num = source.m_buckets.Length;
			int num2 = HashHelpers.ExpandPrime(count + 1);
			if (num2 >= num)
			{
				this.m_buckets = (int[])source.m_buckets.Clone();
				this.m_slots = (HashSet<T>.Slot[])source.m_slots.Clone();
				this.m_lastIndex = source.m_lastIndex;
				this.m_freeList = source.m_freeList;
			}
			else
			{
				int lastIndex = source.m_lastIndex;
				HashSet<T>.Slot[] slots = source.m_slots;
				this.Initialize(count);
				int num3 = 0;
				for (int i = 0; i < lastIndex; i++)
				{
					int hashCode = slots[i].hashCode;
					if (hashCode >= 0)
					{
						this.AddValue(num3, hashCode, slots[i].value);
						num3++;
					}
				}
				this.m_lastIndex = num3;
			}
			this.m_count = count;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000A44B File Offset: 0x0000864B
		protected HashSet(SerializationInfo info, StreamingContext context)
		{
			this.m_siInfo = info;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000A45A File Offset: 0x0000865A
		public HashSet(int capacity, IEqualityComparer<T> comparer) : this(comparer)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity");
			}
			if (capacity > 0)
			{
				this.Initialize(capacity);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000A47D File Offset: 0x0000867D
		[__DynamicallyInvokable]
		void ICollection<!0>.Add(T item)
		{
			this.AddIfNotPresent(item);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000A488 File Offset: 0x00008688
		[__DynamicallyInvokable]
		public void Clear()
		{
			if (this.m_lastIndex > 0)
			{
				Array.Clear(this.m_slots, 0, this.m_lastIndex);
				Array.Clear(this.m_buckets, 0, this.m_buckets.Length);
				this.m_lastIndex = 0;
				this.m_count = 0;
				this.m_freeList = -1;
			}
			this.m_version++;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000A4E8 File Offset: 0x000086E8
		[__DynamicallyInvokable]
		public bool Contains(T item)
		{
			if (this.m_buckets != null)
			{
				int num = this.InternalGetHashCode(item);
				for (int i = this.m_buckets[num % this.m_buckets.Length] - 1; i >= 0; i = this.m_slots[i].next)
				{
					if (this.m_slots[i].hashCode == num && this.m_comparer.Equals(this.m_slots[i].value, item))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000A567 File Offset: 0x00008767
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.CopyTo(array, arrayIndex, this.m_count);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000A578 File Offset: 0x00008778
		[__DynamicallyInvokable]
		public bool Remove(T item)
		{
			if (this.m_buckets != null)
			{
				int num = this.InternalGetHashCode(item);
				int num2 = num % this.m_buckets.Length;
				int num3 = -1;
				for (int i = this.m_buckets[num2] - 1; i >= 0; i = this.m_slots[i].next)
				{
					if (this.m_slots[i].hashCode == num && this.m_comparer.Equals(this.m_slots[i].value, item))
					{
						if (num3 < 0)
						{
							this.m_buckets[num2] = this.m_slots[i].next + 1;
						}
						else
						{
							this.m_slots[num3].next = this.m_slots[i].next;
						}
						this.m_slots[i].hashCode = -1;
						this.m_slots[i].value = default(T);
						this.m_slots[i].next = this.m_freeList;
						this.m_count--;
						this.m_version++;
						if (this.m_count == 0)
						{
							this.m_lastIndex = 0;
							this.m_freeList = -1;
						}
						else
						{
							this.m_freeList = i;
						}
						return true;
					}
					num3 = i;
				}
			}
			return false;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000A6CA File Offset: 0x000088CA
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000A6D2 File Offset: 0x000088D2
		[__DynamicallyInvokable]
		bool ICollection<!0>.IsReadOnly
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000A6D5 File Offset: 0x000088D5
		[__DynamicallyInvokable]
		public HashSet<T>.Enumerator GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A6DD File Offset: 0x000088DD
		[__DynamicallyInvokable]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000A6EA File Offset: 0x000088EA
		[__DynamicallyInvokable]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new HashSet<T>.Enumerator(this);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000A6F8 File Offset: 0x000088F8
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Version", this.m_version);
			info.AddValue("Comparer", HashHelpers.GetEqualityComparerForSerialization(this.m_comparer), typeof(IEqualityComparer<T>));
			info.AddValue("Capacity", (this.m_buckets == null) ? 0 : this.m_buckets.Length);
			if (this.m_buckets != null)
			{
				T[] array = new T[this.m_count];
				this.CopyTo(array);
				info.AddValue("Elements", array, typeof(T[]));
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000A794 File Offset: 0x00008994
		public virtual void OnDeserialization(object sender)
		{
			if (this.m_siInfo == null)
			{
				return;
			}
			int @int = this.m_siInfo.GetInt32("Capacity");
			this.m_comparer = (IEqualityComparer<T>)this.m_siInfo.GetValue("Comparer", typeof(IEqualityComparer<T>));
			this.m_freeList = -1;
			if (@int != 0)
			{
				this.m_buckets = new int[@int];
				this.m_slots = new HashSet<T>.Slot[@int];
				T[] array = (T[])this.m_siInfo.GetValue("Elements", typeof(T[]));
				if (array == null)
				{
					throw new SerializationException(SR.GetString("Serialization_MissingKeys"));
				}
				for (int i = 0; i < array.Length; i++)
				{
					this.AddIfNotPresent(array[i]);
				}
			}
			else
			{
				this.m_buckets = null;
			}
			this.m_version = this.m_siInfo.GetInt32("Version");
			this.m_siInfo = null;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000A877 File Offset: 0x00008A77
		[__DynamicallyInvokable]
		public bool Add(T item)
		{
			return this.AddIfNotPresent(item);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000A880 File Offset: 0x00008A80
		public bool TryGetValue(T equalValue, out T actualValue)
		{
			if (this.m_buckets != null)
			{
				int num = this.InternalIndexOf(equalValue);
				if (num >= 0)
				{
					actualValue = this.m_slots[num].value;
					return true;
				}
			}
			actualValue = default(T);
			return false;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		[__DynamicallyInvokable]
		public void UnionWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (T value in other)
			{
				this.AddIfNotPresent(value);
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000A91C File Offset: 0x00008B1C
		[__DynamicallyInvokable]
		public void IntersectWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.m_count == 0)
			{
				return;
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					this.Clear();
					return;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					this.IntersectWithHashSetWithSameEC(hashSet);
					return;
				}
			}
			this.IntersectWithEnumerable(other);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A97C File Offset: 0x00008B7C
		[__DynamicallyInvokable]
		public void ExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.m_count == 0)
			{
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			foreach (T item in other)
			{
				this.Remove(item);
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A9E8 File Offset: 0x00008BE8
		[__DynamicallyInvokable]
		public void SymmetricExceptWith(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.m_count == 0)
			{
				this.UnionWith(other);
				return;
			}
			if (other == this)
			{
				this.Clear();
				return;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				this.SymmetricExceptWithUniqueHashSet(hashSet);
				return;
			}
			this.SymmetricExceptWithEnumerable(other);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000AA40 File Offset: 0x00008C40
		[__DynamicallyInvokable]
		public bool IsSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.m_count == 0)
			{
				return true;
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				return this.m_count <= hashSet.Count && this.IsSubsetOfHashSetWithSameEC(hashSet);
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this.m_count && elementCount.unfoundCount >= 0;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		[__DynamicallyInvokable]
		public bool IsProperSubsetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (this.m_count == 0)
				{
					return collection.Count > 0;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					return this.m_count < hashSet.Count && this.IsSubsetOfHashSetWithSameEC(hashSet);
				}
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, false);
			return elementCount.uniqueCount == this.m_count && elementCount.unfoundCount > 0;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000AB3C File Offset: 0x00008D3C
		[__DynamicallyInvokable]
		public bool IsSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return true;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet) && hashSet.Count > this.m_count)
				{
					return false;
				}
			}
			return this.ContainsAllElements(other);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000AB98 File Offset: 0x00008D98
		[__DynamicallyInvokable]
		public bool IsProperSupersetOf(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.m_count == 0)
			{
				return false;
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null)
			{
				if (collection.Count == 0)
				{
					return true;
				}
				HashSet<T> hashSet = other as HashSet<T>;
				if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
				{
					return hashSet.Count < this.m_count && this.ContainsAllElements(hashSet);
				}
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount < this.m_count && elementCount.unfoundCount == 0;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000AC20 File Offset: 0x00008E20
		[__DynamicallyInvokable]
		public bool Overlaps(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this.m_count == 0)
			{
				return false;
			}
			foreach (T item in other)
			{
				if (this.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000AC8C File Offset: 0x00008E8C
		[__DynamicallyInvokable]
		public bool SetEquals(IEnumerable<T> other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			HashSet<T> hashSet = other as HashSet<T>;
			if (hashSet != null && HashSet<T>.AreEqualityComparersEqual(this, hashSet))
			{
				return this.m_count == hashSet.Count && this.ContainsAllElements(hashSet);
			}
			ICollection<T> collection = other as ICollection<T>;
			if (collection != null && this.m_count == 0 && collection.Count > 0)
			{
				return false;
			}
			HashSet<T>.ElementCount elementCount = this.CheckUniqueAndUnfoundElements(other, true);
			return elementCount.uniqueCount == this.m_count && elementCount.unfoundCount == 0;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000AD11 File Offset: 0x00008F11
		[__DynamicallyInvokable]
		public void CopyTo(T[] array)
		{
			this.CopyTo(array, 0, this.m_count);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000AD24 File Offset: 0x00008F24
		[__DynamicallyInvokable]
		public void CopyTo(T[] array, int arrayIndex, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (arrayIndex > array.Length || count > array.Length - arrayIndex)
			{
				throw new ArgumentException(SR.GetString("Arg_ArrayPlusOffTooSmall"));
			}
			int num = 0;
			int num2 = 0;
			while (num2 < this.m_lastIndex && num < count)
			{
				if (this.m_slots[num2].hashCode >= 0)
				{
					array[arrayIndex + num] = this.m_slots[num2].value;
					num++;
				}
				num2++;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		[__DynamicallyInvokable]
		public int RemoveWhere(Predicate<T> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			int num = 0;
			for (int i = 0; i < this.m_lastIndex; i++)
			{
				if (this.m_slots[i].hashCode >= 0)
				{
					T value = this.m_slots[i].value;
					if (match(value) && this.Remove(value))
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000AE43 File Offset: 0x00009043
		[__DynamicallyInvokable]
		public IEqualityComparer<T> Comparer
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_comparer;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000AE4C File Offset: 0x0000904C
		[__DynamicallyInvokable]
		public void TrimExcess()
		{
			if (this.m_count == 0)
			{
				this.m_buckets = null;
				this.m_slots = null;
				this.m_version++;
				return;
			}
			int prime = HashHelpers.GetPrime(this.m_count);
			HashSet<T>.Slot[] array = new HashSet<T>.Slot[prime];
			int[] array2 = new int[prime];
			int num = 0;
			for (int i = 0; i < this.m_lastIndex; i++)
			{
				if (this.m_slots[i].hashCode >= 0)
				{
					array[num] = this.m_slots[i];
					int num2 = array[num].hashCode % prime;
					array[num].next = array2[num2] - 1;
					array2[num2] = num + 1;
					num++;
				}
			}
			this.m_lastIndex = num;
			this.m_slots = array;
			this.m_buckets = array2;
			this.m_freeList = -1;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000AF21 File Offset: 0x00009121
		public static IEqualityComparer<HashSet<T>> CreateSetComparer()
		{
			return new HashSetEqualityComparer<T>();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000AF28 File Offset: 0x00009128
		private void Initialize(int capacity)
		{
			int prime = HashHelpers.GetPrime(capacity);
			this.m_buckets = new int[prime];
			this.m_slots = new HashSet<T>.Slot[prime];
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000AF54 File Offset: 0x00009154
		private void IncreaseCapacity()
		{
			int num = HashHelpers.ExpandPrime(this.m_count);
			if (num <= this.m_count)
			{
				throw new ArgumentException(SR.GetString("Arg_HSCapacityOverflow"));
			}
			this.SetCapacity(num, false);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000AF90 File Offset: 0x00009190
		private void SetCapacity(int newSize, bool forceNewHashCodes)
		{
			HashSet<T>.Slot[] array = new HashSet<T>.Slot[newSize];
			if (this.m_slots != null)
			{
				Array.Copy(this.m_slots, 0, array, 0, this.m_lastIndex);
			}
			if (forceNewHashCodes)
			{
				for (int i = 0; i < this.m_lastIndex; i++)
				{
					if (array[i].hashCode != -1)
					{
						array[i].hashCode = this.InternalGetHashCode(array[i].value);
					}
				}
			}
			int[] array2 = new int[newSize];
			for (int j = 0; j < this.m_lastIndex; j++)
			{
				int num = array[j].hashCode % newSize;
				array[j].next = array2[num] - 1;
				array2[num] = j + 1;
			}
			this.m_slots = array;
			this.m_buckets = array2;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000B050 File Offset: 0x00009250
		private bool AddIfNotPresent(T value)
		{
			if (this.m_buckets == null)
			{
				this.Initialize(0);
			}
			int num = this.InternalGetHashCode(value);
			int num2 = num % this.m_buckets.Length;
			int num3 = 0;
			for (int i = this.m_buckets[num % this.m_buckets.Length] - 1; i >= 0; i = this.m_slots[i].next)
			{
				if (this.m_slots[i].hashCode == num && this.m_comparer.Equals(this.m_slots[i].value, value))
				{
					return false;
				}
				num3++;
			}
			int num4;
			if (this.m_freeList >= 0)
			{
				num4 = this.m_freeList;
				this.m_freeList = this.m_slots[num4].next;
			}
			else
			{
				if (this.m_lastIndex == this.m_slots.Length)
				{
					this.IncreaseCapacity();
					num2 = num % this.m_buckets.Length;
				}
				num4 = this.m_lastIndex;
				this.m_lastIndex++;
			}
			this.m_slots[num4].hashCode = num;
			this.m_slots[num4].value = value;
			this.m_slots[num4].next = this.m_buckets[num2] - 1;
			this.m_buckets[num2] = num4 + 1;
			this.m_count++;
			this.m_version++;
			if (num3 > 100 && HashHelpers.IsWellKnownEqualityComparer(this.m_comparer))
			{
				this.m_comparer = (IEqualityComparer<T>)HashHelpers.GetRandomizedEqualityComparer(this.m_comparer);
				this.SetCapacity(this.m_buckets.Length, true);
			}
			return true;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000B1EC File Offset: 0x000093EC
		private void AddValue(int index, int hashCode, T value)
		{
			int num = hashCode % this.m_buckets.Length;
			this.m_slots[index].hashCode = hashCode;
			this.m_slots[index].value = value;
			this.m_slots[index].next = this.m_buckets[num] - 1;
			this.m_buckets[num] = index + 1;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000B250 File Offset: 0x00009450
		private bool ContainsAllElements(IEnumerable<T> other)
		{
			foreach (T item in other)
			{
				if (!this.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000B2A4 File Offset: 0x000094A4
		private bool IsSubsetOfHashSetWithSameEC(HashSet<T> other)
		{
			foreach (T item in this)
			{
				if (!other.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000B2FC File Offset: 0x000094FC
		private void IntersectWithHashSetWithSameEC(HashSet<T> other)
		{
			for (int i = 0; i < this.m_lastIndex; i++)
			{
				if (this.m_slots[i].hashCode >= 0)
				{
					T value = this.m_slots[i].value;
					if (!other.Contains(value))
					{
						this.Remove(value);
					}
				}
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000B354 File Offset: 0x00009554
		[SecuritySafeCritical]
		private unsafe void IntersectWithEnumerable(IEnumerable<T> other)
		{
			int lastIndex = this.m_lastIndex;
			int num = BitHelper.ToIntArrayLength(lastIndex);
			BitHelper bitHelper;
			if (num <= 100)
			{
				int* bitArrayPtr = stackalloc int[checked(unchecked((UIntPtr)num) * 4)];
				bitHelper = new BitHelper(bitArrayPtr, num);
			}
			else
			{
				int[] bitArray = new int[num];
				bitHelper = new BitHelper(bitArray, num);
			}
			foreach (T item in other)
			{
				int num2 = this.InternalIndexOf(item);
				if (num2 >= 0)
				{
					bitHelper.MarkBit(num2);
				}
			}
			for (int i = 0; i < lastIndex; i++)
			{
				if (this.m_slots[i].hashCode >= 0 && !bitHelper.IsMarked(i))
				{
					this.Remove(this.m_slots[i].value);
				}
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000B430 File Offset: 0x00009630
		private int InternalIndexOf(T item)
		{
			int num = this.InternalGetHashCode(item);
			for (int i = this.m_buckets[num % this.m_buckets.Length] - 1; i >= 0; i = this.m_slots[i].next)
			{
				if (this.m_slots[i].hashCode == num && this.m_comparer.Equals(this.m_slots[i].value, item))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000B4A8 File Offset: 0x000096A8
		private void SymmetricExceptWithUniqueHashSet(HashSet<T> other)
		{
			foreach (T t in other)
			{
				if (!this.Remove(t))
				{
					this.AddIfNotPresent(t);
				}
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000B500 File Offset: 0x00009700
		[SecuritySafeCritical]
		private unsafe void SymmetricExceptWithEnumerable(IEnumerable<T> other)
		{
			int lastIndex = this.m_lastIndex;
			int num = BitHelper.ToIntArrayLength(lastIndex);
			BitHelper bitHelper;
			checked
			{
				BitHelper bitHelper2;
				if (num <= 50)
				{
					int* bitArrayPtr = stackalloc int[unchecked((UIntPtr)num) * 4];
					bitHelper = new BitHelper(bitArrayPtr, num);
					int* bitArrayPtr2 = stackalloc int[unchecked((UIntPtr)num) * 4];
					bitHelper2 = new BitHelper(bitArrayPtr2, num);
				}
				else
				{
					int[] bitArray = new int[num];
					bitHelper = new BitHelper(bitArray, num);
					int[] bitArray2 = new int[num];
					bitHelper2 = new BitHelper(bitArray2, num);
				}
				foreach (T value in other)
				{
					int num2 = 0;
					bool flag = this.AddOrGetLocation(value, out num2);
					if (flag)
					{
						bitHelper2.MarkBit(num2);
					}
					else if (num2 < lastIndex && !bitHelper2.IsMarked(num2))
					{
						bitHelper.MarkBit(num2);
					}
				}
			}
			for (int i = 0; i < lastIndex; i++)
			{
				if (bitHelper.IsMarked(i))
				{
					this.Remove(this.m_slots[i].value);
				}
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000B608 File Offset: 0x00009808
		private bool AddOrGetLocation(T value, out int location)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this.m_buckets.Length;
			for (int i = this.m_buckets[num % this.m_buckets.Length] - 1; i >= 0; i = this.m_slots[i].next)
			{
				if (this.m_slots[i].hashCode == num && this.m_comparer.Equals(this.m_slots[i].value, value))
				{
					location = i;
					return false;
				}
			}
			int num3;
			if (this.m_freeList >= 0)
			{
				num3 = this.m_freeList;
				this.m_freeList = this.m_slots[num3].next;
			}
			else
			{
				if (this.m_lastIndex == this.m_slots.Length)
				{
					this.IncreaseCapacity();
					num2 = num % this.m_buckets.Length;
				}
				num3 = this.m_lastIndex;
				this.m_lastIndex++;
			}
			this.m_slots[num3].hashCode = num;
			this.m_slots[num3].value = value;
			this.m_slots[num3].next = this.m_buckets[num2] - 1;
			this.m_buckets[num2] = num3 + 1;
			this.m_count++;
			this.m_version++;
			location = num3;
			return true;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000B758 File Offset: 0x00009958
		[SecuritySafeCritical]
		private unsafe HashSet<T>.ElementCount CheckUniqueAndUnfoundElements(IEnumerable<T> other, bool returnIfUnfound)
		{
			HashSet<T>.ElementCount result;
			if (this.m_count == 0)
			{
				int num = 0;
				using (IEnumerator<T> enumerator = other.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T t = enumerator.Current;
						num++;
					}
				}
				result.uniqueCount = 0;
				result.unfoundCount = num;
				return result;
			}
			int lastIndex = this.m_lastIndex;
			int num2 = BitHelper.ToIntArrayLength(lastIndex);
			BitHelper bitHelper;
			if (num2 <= 100)
			{
				int* bitArrayPtr = stackalloc int[checked(unchecked((UIntPtr)num2) * 4)];
				bitHelper = new BitHelper(bitArrayPtr, num2);
			}
			else
			{
				int[] bitArray = new int[num2];
				bitHelper = new BitHelper(bitArray, num2);
			}
			int num3 = 0;
			int num4 = 0;
			foreach (T item in other)
			{
				int num5 = this.InternalIndexOf(item);
				if (num5 >= 0)
				{
					if (!bitHelper.IsMarked(num5))
					{
						bitHelper.MarkBit(num5);
						num4++;
					}
				}
				else
				{
					num3++;
					if (returnIfUnfound)
					{
						break;
					}
				}
			}
			result.uniqueCount = num4;
			result.unfoundCount = num3;
			return result;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000B880 File Offset: 0x00009A80
		internal T[] ToArray()
		{
			T[] array = new T[this.Count];
			this.CopyTo(array);
			return array;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		internal static bool HashSetEquals(HashSet<T> set1, HashSet<T> set2, IEqualityComparer<T> comparer)
		{
			if (set1 == null)
			{
				return set2 == null;
			}
			if (set2 == null)
			{
				return false;
			}
			if (!HashSet<T>.AreEqualityComparersEqual(set1, set2))
			{
				foreach (T x in set2)
				{
					bool flag = false;
					foreach (T y in set1)
					{
						if (comparer.Equals(x, y))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
				}
				return true;
			}
			if (set1.Count != set2.Count)
			{
				return false;
			}
			foreach (T item in set2)
			{
				if (!set1.Contains(item))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		private static bool AreEqualityComparersEqual(HashSet<T> set1, HashSet<T> set2)
		{
			return set1.Comparer.Equals(set2.Comparer);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000B9C7 File Offset: 0x00009BC7
		private int InternalGetHashCode(T item)
		{
			if (item == null)
			{
				return 0;
			}
			return this.m_comparer.GetHashCode(item) & int.MaxValue;
		}

		// Token: 0x040004CC RID: 1228
		private const int Lower31BitMask = 2147483647;

		// Token: 0x040004CD RID: 1229
		private const int StackAllocThreshold = 100;

		// Token: 0x040004CE RID: 1230
		private const int ShrinkThreshold = 3;

		// Token: 0x040004CF RID: 1231
		private const string CapacityName = "Capacity";

		// Token: 0x040004D0 RID: 1232
		private const string ElementsName = "Elements";

		// Token: 0x040004D1 RID: 1233
		private const string ComparerName = "Comparer";

		// Token: 0x040004D2 RID: 1234
		private const string VersionName = "Version";

		// Token: 0x040004D3 RID: 1235
		private int[] m_buckets;

		// Token: 0x040004D4 RID: 1236
		private HashSet<T>.Slot[] m_slots;

		// Token: 0x040004D5 RID: 1237
		private int m_count;

		// Token: 0x040004D6 RID: 1238
		private int m_lastIndex;

		// Token: 0x040004D7 RID: 1239
		private int m_freeList;

		// Token: 0x040004D8 RID: 1240
		private IEqualityComparer<T> m_comparer;

		// Token: 0x040004D9 RID: 1241
		private int m_version;

		// Token: 0x040004DA RID: 1242
		private SerializationInfo m_siInfo;

		// Token: 0x02000308 RID: 776
		internal struct ElementCount
		{
			// Token: 0x04000E1F RID: 3615
			internal int uniqueCount;

			// Token: 0x04000E20 RID: 3616
			internal int unfoundCount;
		}

		// Token: 0x02000309 RID: 777
		internal struct Slot
		{
			// Token: 0x04000E21 RID: 3617
			internal int hashCode;

			// Token: 0x04000E22 RID: 3618
			internal int next;

			// Token: 0x04000E23 RID: 3619
			internal T value;
		}

		// Token: 0x0200030A RID: 778
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		[Serializable]
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06001A7B RID: 6779 RVA: 0x00060FED File Offset: 0x0005F1ED
			internal Enumerator(HashSet<T> set)
			{
				this.set = set;
				this.index = 0;
				this.version = set.m_version;
				this.current = default(T);
			}

			// Token: 0x06001A7C RID: 6780 RVA: 0x00061015 File Offset: 0x0005F215
			[__DynamicallyInvokable]
			public void Dispose()
			{
			}

			// Token: 0x06001A7D RID: 6781 RVA: 0x00061018 File Offset: 0x0005F218
			[__DynamicallyInvokable]
			public bool MoveNext()
			{
				if (this.version != this.set.m_version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				while (this.index < this.set.m_lastIndex)
				{
					if (this.set.m_slots[this.index].hashCode >= 0)
					{
						this.current = this.set.m_slots[this.index].value;
						this.index++;
						return true;
					}
					this.index++;
				}
				this.index = this.set.m_lastIndex + 1;
				this.current = default(T);
				return false;
			}

			// Token: 0x170004ED RID: 1261
			// (get) Token: 0x06001A7E RID: 6782 RVA: 0x000610D8 File Offset: 0x0005F2D8
			[__DynamicallyInvokable]
			public T Current
			{
				[__DynamicallyInvokable]
				get
				{
					return this.current;
				}
			}

			// Token: 0x170004EE RID: 1262
			// (get) Token: 0x06001A7F RID: 6783 RVA: 0x000610E0 File Offset: 0x0005F2E0
			[__DynamicallyInvokable]
			object IEnumerator.Current
			{
				[__DynamicallyInvokable]
				get
				{
					if (this.index == 0 || this.index == this.set.m_lastIndex + 1)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.Current;
				}
			}

			// Token: 0x06001A80 RID: 6784 RVA: 0x0006111A File Offset: 0x0005F31A
			[__DynamicallyInvokable]
			void IEnumerator.Reset()
			{
				if (this.version != this.set.m_version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				this.index = 0;
				this.current = default(T);
			}

			// Token: 0x04000E24 RID: 3620
			private HashSet<T> set;

			// Token: 0x04000E25 RID: 3621
			private int index;

			// Token: 0x04000E26 RID: 3622
			private int version;

			// Token: 0x04000E27 RID: 3623
			private T current;
		}
	}
}
