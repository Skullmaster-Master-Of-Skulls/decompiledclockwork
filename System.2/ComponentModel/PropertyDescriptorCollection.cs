using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200059D RID: 1437
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class PropertyDescriptorCollection : ICollection, IEnumerable, IList, IDictionary
	{
		// Token: 0x06003559 RID: 13657 RVA: 0x000E838F File Offset: 0x000E658F
		public PropertyDescriptorCollection(PropertyDescriptor[] properties)
		{
			this.properties = properties;
			if (properties == null)
			{
				this.properties = new PropertyDescriptor[0];
				this.propCount = 0;
			}
			else
			{
				this.propCount = properties.Length;
			}
			this.propsOwned = true;
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000E83CD File Offset: 0x000E65CD
		public PropertyDescriptorCollection(PropertyDescriptor[] properties, bool readOnly) : this(properties)
		{
			this.readOnly = readOnly;
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x000E83E0 File Offset: 0x000E65E0
		private PropertyDescriptorCollection(PropertyDescriptor[] properties, int propCount, string[] namedSort, IComparer comparer)
		{
			this.propsOwned = false;
			if (namedSort != null)
			{
				this.namedSort = (string[])namedSort.Clone();
			}
			this.comparer = comparer;
			this.properties = properties;
			this.propCount = propCount;
			this.needSort = true;
		}

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600355C RID: 13660 RVA: 0x000E8432 File Offset: 0x000E6632
		public int Count
		{
			get
			{
				return this.propCount;
			}
		}

		// Token: 0x17000D0B RID: 3339
		public virtual PropertyDescriptor this[int index]
		{
			get
			{
				if (index >= this.propCount)
				{
					throw new IndexOutOfRangeException();
				}
				this.EnsurePropsOwned();
				return this.properties[index];
			}
		}

		// Token: 0x17000D0C RID: 3340
		public virtual PropertyDescriptor this[string name]
		{
			get
			{
				return this.Find(name, false);
			}
		}

		// Token: 0x0600355F RID: 13663 RVA: 0x000E8464 File Offset: 0x000E6664
		public int Add(PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.propCount + 1);
			PropertyDescriptor[] array = this.properties;
			int num = this.propCount;
			this.propCount = num + 1;
			array[num] = value;
			return this.propCount - 1;
		}

		// Token: 0x06003560 RID: 13664 RVA: 0x000E84AE File Offset: 0x000E66AE
		public void Clear()
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.propCount = 0;
			this.cachedFoundProperties = null;
		}

		// Token: 0x06003561 RID: 13665 RVA: 0x000E84CC File Offset: 0x000E66CC
		public bool Contains(PropertyDescriptor value)
		{
			return this.IndexOf(value) >= 0;
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000E84DB File Offset: 0x000E66DB
		public void CopyTo(Array array, int index)
		{
			this.EnsurePropsOwned();
			Array.Copy(this.properties, 0, array, index, this.Count);
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000E84F8 File Offset: 0x000E66F8
		private void EnsurePropsOwned()
		{
			if (!this.propsOwned)
			{
				this.propsOwned = true;
				if (this.properties != null)
				{
					PropertyDescriptor[] destinationArray = new PropertyDescriptor[this.Count];
					Array.Copy(this.properties, 0, destinationArray, 0, this.Count);
					this.properties = destinationArray;
				}
			}
			if (this.needSort)
			{
				this.needSort = false;
				this.InternalSort(this.namedSort);
			}
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000E8560 File Offset: 0x000E6760
		private void EnsureSize(int sizeNeeded)
		{
			if (sizeNeeded <= this.properties.Length)
			{
				return;
			}
			if (this.properties == null || this.properties.Length == 0)
			{
				this.propCount = 0;
				this.properties = new PropertyDescriptor[sizeNeeded];
				return;
			}
			this.EnsurePropsOwned();
			int num = Math.Max(sizeNeeded, this.properties.Length * 2);
			PropertyDescriptor[] destinationArray = new PropertyDescriptor[num];
			Array.Copy(this.properties, 0, destinationArray, 0, this.propCount);
			this.properties = destinationArray;
		}

		// Token: 0x06003565 RID: 13669 RVA: 0x000E85D8 File Offset: 0x000E67D8
		public virtual PropertyDescriptor Find(string name, bool ignoreCase)
		{
			PropertyDescriptor result;
			lock (this)
			{
				PropertyDescriptor propertyDescriptor = null;
				if (this.cachedFoundProperties == null || this.cachedIgnoreCase != ignoreCase)
				{
					this.cachedIgnoreCase = ignoreCase;
					this.cachedFoundProperties = new HybridDictionary(ignoreCase);
				}
				object obj = this.cachedFoundProperties[name];
				if (obj != null)
				{
					result = (PropertyDescriptor)obj;
				}
				else
				{
					for (int i = 0; i < this.propCount; i++)
					{
						if (ignoreCase)
						{
							if (string.Equals(this.properties[i].Name, name, StringComparison.OrdinalIgnoreCase))
							{
								this.cachedFoundProperties[name] = this.properties[i];
								propertyDescriptor = this.properties[i];
								break;
							}
						}
						else if (this.properties[i].Name.Equals(name))
						{
							this.cachedFoundProperties[name] = this.properties[i];
							propertyDescriptor = this.properties[i];
							break;
						}
					}
					result = propertyDescriptor;
				}
			}
			return result;
		}

		// Token: 0x06003566 RID: 13670 RVA: 0x000E86E0 File Offset: 0x000E68E0
		public int IndexOf(PropertyDescriptor value)
		{
			return Array.IndexOf<PropertyDescriptor>(this.properties, value, 0, this.propCount);
		}

		// Token: 0x06003567 RID: 13671 RVA: 0x000E86F8 File Offset: 0x000E68F8
		public void Insert(int index, PropertyDescriptor value)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			this.EnsureSize(this.propCount + 1);
			if (index < this.propCount)
			{
				Array.Copy(this.properties, index, this.properties, index + 1, this.propCount - index);
			}
			this.properties[index] = value;
			this.propCount++;
		}

		// Token: 0x06003568 RID: 13672 RVA: 0x000E8760 File Offset: 0x000E6960
		public void Remove(PropertyDescriptor value)
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

		// Token: 0x06003569 RID: 13673 RVA: 0x000E8790 File Offset: 0x000E6990
		public void RemoveAt(int index)
		{
			if (this.readOnly)
			{
				throw new NotSupportedException();
			}
			if (index < this.propCount - 1)
			{
				Array.Copy(this.properties, index + 1, this.properties, index, this.propCount - index - 1);
			}
			this.properties[this.propCount - 1] = null;
			this.propCount--;
		}

		// Token: 0x0600356A RID: 13674 RVA: 0x000E87F3 File Offset: 0x000E69F3
		public virtual PropertyDescriptorCollection Sort()
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, this.namedSort, this.comparer);
		}

		// Token: 0x0600356B RID: 13675 RVA: 0x000E8812 File Offset: 0x000E6A12
		public virtual PropertyDescriptorCollection Sort(string[] names)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, names, this.comparer);
		}

		// Token: 0x0600356C RID: 13676 RVA: 0x000E882C File Offset: 0x000E6A2C
		public virtual PropertyDescriptorCollection Sort(string[] names, IComparer comparer)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, names, comparer);
		}

		// Token: 0x0600356D RID: 13677 RVA: 0x000E8841 File Offset: 0x000E6A41
		public virtual PropertyDescriptorCollection Sort(IComparer comparer)
		{
			return new PropertyDescriptorCollection(this.properties, this.propCount, this.namedSort, comparer);
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x000E885C File Offset: 0x000E6A5C
		protected void InternalSort(string[] names)
		{
			if (this.properties == null || this.properties.Length == 0)
			{
				return;
			}
			this.InternalSort(this.comparer);
			if (names != null && names.Length != 0)
			{
				ArrayList arrayList = new ArrayList(this.properties);
				int num = 0;
				int num2 = this.properties.Length;
				for (int i = 0; i < names.Length; i++)
				{
					for (int j = 0; j < num2; j++)
					{
						PropertyDescriptor propertyDescriptor = (PropertyDescriptor)arrayList[j];
						if (propertyDescriptor != null && propertyDescriptor.Name.Equals(names[i]))
						{
							this.properties[num++] = propertyDescriptor;
							arrayList[j] = null;
							break;
						}
					}
				}
				for (int k = 0; k < num2; k++)
				{
					if (arrayList[k] != null)
					{
						this.properties[num++] = (PropertyDescriptor)arrayList[k];
					}
				}
			}
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x000E8939 File Offset: 0x000E6B39
		protected void InternalSort(IComparer sorter)
		{
			if (sorter == null)
			{
				TypeDescriptor.SortDescriptorArray(this);
				return;
			}
			Array.Sort(this.properties, sorter);
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000E8954 File Offset: 0x000E6B54
		public virtual IEnumerator GetEnumerator()
		{
			this.EnsurePropsOwned();
			if (this.properties.Length != this.propCount)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[this.propCount];
				Array.Copy(this.properties, 0, array, 0, this.propCount);
				return array.GetEnumerator();
			}
			return this.properties.GetEnumerator();
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x000E89A9 File Offset: 0x000E6BA9
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x000E89B1 File Offset: 0x000E6BB1
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000E89B4 File Offset: 0x000E6BB4
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x000E89B8 File Offset: 0x000E6BB8
		void IDictionary.Add(object key, object value)
		{
			PropertyDescriptor propertyDescriptor = value as PropertyDescriptor;
			if (propertyDescriptor == null)
			{
				throw new ArgumentException("value");
			}
			this.Add(propertyDescriptor);
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000E89E2 File Offset: 0x000E6BE2
		void IDictionary.Clear()
		{
			this.Clear();
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000E89EA File Offset: 0x000E6BEA
		bool IDictionary.Contains(object key)
		{
			return key is string && this[(string)key] != null;
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x000E8A05 File Offset: 0x000E6C05
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new PropertyDescriptorCollection.PropertyDescriptorEnumerator(this);
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06003578 RID: 13688 RVA: 0x000E8A0D File Offset: 0x000E6C0D
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06003579 RID: 13689 RVA: 0x000E8A15 File Offset: 0x000E6C15
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000D12 RID: 3346
		object IDictionary.this[object key]
		{
			get
			{
				if (key is string)
				{
					return this[(string)key];
				}
				return null;
			}
			set
			{
				if (this.readOnly)
				{
					throw new NotSupportedException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				int num = -1;
				if (key is int)
				{
					num = (int)key;
					if (num < 0 || num >= this.propCount)
					{
						throw new IndexOutOfRangeException();
					}
				}
				else
				{
					if (!(key is string))
					{
						throw new ArgumentException("key");
					}
					for (int i = 0; i < this.propCount; i++)
					{
						if (this.properties[i].Name.Equals((string)key))
						{
							num = i;
							break;
						}
					}
				}
				if (num == -1)
				{
					this.Add((PropertyDescriptor)value);
					return;
				}
				this.EnsurePropsOwned();
				this.properties[num] = (PropertyDescriptor)value;
				if (this.cachedFoundProperties != null && key is string)
				{
					this.cachedFoundProperties[key] = value;
				}
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x000E8B14 File Offset: 0x000E6D14
		ICollection IDictionary.Keys
		{
			get
			{
				string[] array = new string[this.propCount];
				for (int i = 0; i < this.propCount; i++)
				{
					array[i] = this.properties[i].Name;
				}
				return array;
			}
		}

		// Token: 0x17000D14 RID: 3348
		// (get) Token: 0x0600357D RID: 13693 RVA: 0x000E8B50 File Offset: 0x000E6D50
		ICollection IDictionary.Values
		{
			get
			{
				if (this.properties.Length != this.propCount)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[this.propCount];
					Array.Copy(this.properties, 0, array, 0, this.propCount);
					return array;
				}
				return (ICollection)this.properties.Clone();
			}
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000E8BA0 File Offset: 0x000E6DA0
		void IDictionary.Remove(object key)
		{
			if (key is string)
			{
				PropertyDescriptor propertyDescriptor = this[(string)key];
				if (propertyDescriptor != null)
				{
					((IList)this).Remove(propertyDescriptor);
				}
			}
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000E8BCC File Offset: 0x000E6DCC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000E8BD4 File Offset: 0x000E6DD4
		int IList.Add(object value)
		{
			return this.Add((PropertyDescriptor)value);
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000E8BE2 File Offset: 0x000E6DE2
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000E8BEA File Offset: 0x000E6DEA
		bool IList.Contains(object value)
		{
			return this.Contains((PropertyDescriptor)value);
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000E8BF8 File Offset: 0x000E6DF8
		int IList.IndexOf(object value)
		{
			return this.IndexOf((PropertyDescriptor)value);
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000E8C06 File Offset: 0x000E6E06
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (PropertyDescriptor)value);
		}

		// Token: 0x17000D15 RID: 3349
		// (get) Token: 0x06003585 RID: 13701 RVA: 0x000E8C15 File Offset: 0x000E6E15
		bool IList.IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000E8C1D File Offset: 0x000E6E1D
		bool IList.IsFixedSize
		{
			get
			{
				return this.readOnly;
			}
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000E8C25 File Offset: 0x000E6E25
		void IList.Remove(object value)
		{
			this.Remove((PropertyDescriptor)value);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x000E8C33 File Offset: 0x000E6E33
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17000D17 RID: 3351
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
				if (index >= this.propCount)
				{
					throw new IndexOutOfRangeException();
				}
				if (value != null && !(value is PropertyDescriptor))
				{
					throw new ArgumentException("value");
				}
				this.EnsurePropsOwned();
				this.properties[index] = (PropertyDescriptor)value;
			}
		}

		// Token: 0x04002A4A RID: 10826
		public static readonly PropertyDescriptorCollection Empty = new PropertyDescriptorCollection(null, true);

		// Token: 0x04002A4B RID: 10827
		private IDictionary cachedFoundProperties;

		// Token: 0x04002A4C RID: 10828
		private bool cachedIgnoreCase;

		// Token: 0x04002A4D RID: 10829
		private PropertyDescriptor[] properties;

		// Token: 0x04002A4E RID: 10830
		private int propCount;

		// Token: 0x04002A4F RID: 10831
		private string[] namedSort;

		// Token: 0x04002A50 RID: 10832
		private IComparer comparer;

		// Token: 0x04002A51 RID: 10833
		private bool propsOwned = true;

		// Token: 0x04002A52 RID: 10834
		private bool needSort;

		// Token: 0x04002A53 RID: 10835
		private bool readOnly;

		// Token: 0x0200089B RID: 2203
		private class PropertyDescriptorEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060045B2 RID: 17842 RVA: 0x00123781 File Offset: 0x00121981
			public PropertyDescriptorEnumerator(PropertyDescriptorCollection owner)
			{
				this.owner = owner;
			}

			// Token: 0x17000FC5 RID: 4037
			// (get) Token: 0x060045B3 RID: 17843 RVA: 0x00123797 File Offset: 0x00121997
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000FC6 RID: 4038
			// (get) Token: 0x060045B4 RID: 17844 RVA: 0x001237A4 File Offset: 0x001219A4
			public DictionaryEntry Entry
			{
				get
				{
					PropertyDescriptor propertyDescriptor = this.owner[this.index];
					return new DictionaryEntry(propertyDescriptor.Name, propertyDescriptor);
				}
			}

			// Token: 0x17000FC7 RID: 4039
			// (get) Token: 0x060045B5 RID: 17845 RVA: 0x001237CF File Offset: 0x001219CF
			public object Key
			{
				get
				{
					return this.owner[this.index].Name;
				}
			}

			// Token: 0x17000FC8 RID: 4040
			// (get) Token: 0x060045B6 RID: 17846 RVA: 0x001237E7 File Offset: 0x001219E7
			public object Value
			{
				get
				{
					return this.owner[this.index].Name;
				}
			}

			// Token: 0x060045B7 RID: 17847 RVA: 0x001237FF File Offset: 0x001219FF
			public bool MoveNext()
			{
				if (this.index < this.owner.Count - 1)
				{
					this.index++;
					return true;
				}
				return false;
			}

			// Token: 0x060045B8 RID: 17848 RVA: 0x00123827 File Offset: 0x00121A27
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x040037EA RID: 14314
			private PropertyDescriptorCollection owner;

			// Token: 0x040037EB RID: 14315
			private int index = -1;
		}
	}
}
