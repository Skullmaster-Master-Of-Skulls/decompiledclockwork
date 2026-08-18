using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000550 RID: 1360
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class EventDescriptorCollection : ICollection, IEnumerable, IList
	{
		// Token: 0x06003313 RID: 13075 RVA: 0x000E3568 File Offset: 0x000E1768
		public EventDescriptorCollection(EventDescriptor[] events)
		{
			this.events = events;
			if (events == null)
			{
				this.events = new EventDescriptor[0];
				this.eventCount = 0;
			}
			else
			{
				this.eventCount = this.events.Length;
			}
			this.eventsOwned = true;
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x000E35B6 File Offset: 0x000E17B6
		public EventDescriptorCollection(EventDescriptor[] events, bool readOnly) : this(events)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x000E35C8 File Offset: 0x000E17C8
		private EventDescriptorCollection(EventDescriptor[] events, int eventCount, string[] namedSort, IComparer comparer)
		{
			this.eventsOwned = false;
			if (namedSort != null)
			{
				this.namedSort = (string[])namedSort.Clone();
			}
			this.comparer = comparer;
			this.events = events;
			this.eventCount = eventCount;
			this.needSort = true;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000E361A File Offset: 0x000E181A
		public int Count
		{
			get
			{
				return this.eventCount;
			}
		}

		// Token: 0x17000C80 RID: 3200
		public virtual EventDescriptor this[int index]
		{
			get
			{
				if (index >= this.eventCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				return this.events[index];
			}
		}

		// Token: 0x17000C81 RID: 3201
		public virtual EventDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x000E364C File Offset: 0x000E184C
		public int Add(EventDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.eventCount + 1);
			EventDescriptor[] array = this.events;
			int num = this.eventCount;
			this.eventCount = num + 1;
			array[num] = value;
			return this.eventCount - 1;
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x000E3696 File Offset: 0x000E1896
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.eventCount = 0;
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000E36AD File Offset: 0x000E18AD
		public bool Contains(EventDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000E36BC File Offset: 0x000E18BC
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureEventsOwned();
			Array.Copy(this.events, 0, array, index, this.Count);
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000E36D8 File Offset: 0x000E18D8
		private void EnsureEventsOwned()
		{
			if (!this.eventsOwned)
			{
				this.eventsOwned = true;
				if (this.events != null)
				{
					EventDescriptor[] destinationArray = new EventDescriptor[this.Count];
					Array.Copy(this.events, 0, destinationArray, 0, this.Count);
					this.events = destinationArray;
				}
			}
			if (this.needSort)
			{
				this.needSort = false;
				this.InternalSort(this.namedSort);
			}
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000E3740 File Offset: 0x000E1940
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this.events.Length)
			{
				return;
			}
			if (this.events == null || this.events.Length == 0)
			{
				this.eventCount = 0;
				this.events = new EventDescriptor[sizeNeeded];
				return;
			}
			this.EnsureEventsOwned();
			int num = Math.Max(sizeNeeded, this.events.Length * 2);
			EventDescriptor[] destinationArray = new EventDescriptor[num];
			Array.Copy(this.events, 0, destinationArray, 0, this.eventCount);
			this.events = destinationArray;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000E37B8 File Offset: 0x000E19B8
		public virtual EventDescriptor Find(string name, bool ignoreCase)
		{
			EventDescriptor result = null;
			if (ignoreCase)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (string.Equals(this.events[i].Name, name, StringComparison.OrdinalIgnoreCase))
					{
						result = this.events[i];
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < this.Count; j++)
				{
					if (string.Equals(this.events[j].Name, name, StringComparison.Ordinal))
					{
						result = this.events[j];
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x000E3831 File Offset: 0x000E1A31
		public int IndexOf(EventDescriptor value)
		{
			return Array.IndexOf<EventDescriptor>(this.events, value, 0, this.eventCount);
		}

		// Token: 0x06003321 RID: 13089 RVA: 0x000E3848 File Offset: 0x000E1A48
		public void Insert(int index, EventDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.eventCount + 1);
			if (index < this.eventCount)
			{
				Array.Copy(this.events, index, this.events, index + 1, this.eventCount - index);
			}
			this.events[index] = value;
			this.eventCount++;
		}

		// Token: 0x06003322 RID: 13090 RVA: 0x000E38B0 File Offset: 0x000E1AB0
		public void Remove(EventDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			int num = this.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x000E38E0 File Offset: 0x000E1AE0
		public void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.eventCount - 1)
			{
				Array.Copy(this.events, index + 1, this.events, index, this.eventCount - index - 1);
			}
			this.events[this.eventCount - 1] = null;
			this.eventCount--;
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x000E3943 File Offset: 0x000E1B43
		public IEnumerator GetEnumerator()
		{
			if (this.events.Length == this.eventCount)
			{
				return this.events.GetEnumerator();
			}
			return new ArraySubsetEnumerator(this.events, this.eventCount);
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x000E3972 File Offset: 0x000E1B72
		public virtual EventDescriptorCollection Sort()
		{
			return new EventDescriptorCollection(this.events, this.eventCount, this.namedSort, this.comparer);
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x000E3991 File Offset: 0x000E1B91
		public virtual EventDescriptorCollection Sort(string[] names)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, names, this.comparer);
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x000E39AB File Offset: 0x000E1BAB
		public virtual EventDescriptorCollection Sort(string[] names, IComparer comparer)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, names, comparer);
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000E39C0 File Offset: 0x000E1BC0
		public virtual EventDescriptorCollection Sort(IComparer comparer)
		{
			return new EventDescriptorCollection(this.events, this.eventCount, this.namedSort, comparer);
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000E39DC File Offset: 0x000E1BDC
		protected void InternalSort(string[] names)
		{
			if (this.events == null || this.events.Length == 0)
			{
				return;
			}
			this.InternalSort(this.comparer);
			if (names != null && names.Length != 0)
			{
				ArrayList arrayList = new ArrayList(this.events);
				int num = 0;
				int num2 = this.events.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						EventDescriptor eventDescriptor = (EventDescriptor)arrayList[j];
						if (eventDescriptor != null && eventDescriptor.Name.Equals(names[i]))
						{
							this.events[num++] = eventDescriptor;
							arrayList[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (arrayList[k] != null)
					{
						this.events[num++] = (EventDescriptor)arrayList[k];
					}
				}
			}
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000E3AB9 File Offset: 0x000E1CB9
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this.events, sorter);
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000E3AD1 File Offset: 0x000E1CD1
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x000E3AD9 File Offset: 0x000E1CD9
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000E3ADC File Offset: 0x000E1CDC
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x000E3ADF File Offset: 0x000E1CDF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000C85 RID: 3205
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				if (index >= this.eventCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsureEventsOwned();
				this.events[index] = (EventDescriptor)value;
			}
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x000E3B23 File Offset: 0x000E1D23
		int IList.Add(object value)
		{
			return this.Add((EventDescriptor)value);
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x000E3B31 File Offset: 0x000E1D31
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x000E3B39 File Offset: 0x000E1D39
		bool IList.Contains(object value)
		{
			return this.Contains((EventDescriptor)value);
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x000E3B47 File Offset: 0x000E1D47
		int IList.IndexOf(object value)
		{
			return this.IndexOf((EventDescriptor)value);
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x000E3B55 File Offset: 0x000E1D55
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (EventDescriptor)value);
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x000E3B64 File Offset: 0x000E1D64
		void IList.Remove(object value)
		{
			this.Remove((EventDescriptor)value);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x000E3B72 File Offset: 0x000E1D72
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000E3B7B File Offset: 0x000E1D7B
		bool IList.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06003339 RID: 13113 RVA: 0x000E3B83 File Offset: 0x000E1D83
		bool IList.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x040029B1 RID: 10673
		private EventDescriptor[] events;

		// Token: 0x040029B2 RID: 10674
		private string[] namedSort;

		// Token: 0x040029B3 RID: 10675
		private IComparer comparer;

		// Token: 0x040029B4 RID: 10676
		private bool eventsOwned = true;

		// Token: 0x040029B5 RID: 10677
		private bool needSort;

		// Token: 0x040029B6 RID: 10678
		private int eventCount;

		// Token: 0x040029B7 RID: 10679
		private bool readOnly;

		// Token: 0x040029B8 RID: 10680
		public static readonly EventDescriptorCollection Empty = new EventDescriptorCollection(null, true);
	}
}
