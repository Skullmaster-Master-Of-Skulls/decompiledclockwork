using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000270 RID: 624
	public sealed class DataBindingCollection : ICollection, IEnumerable
	{
		// Token: 0x06001DAF RID: 7599 RVA: 0x000606B6 File Offset: 0x0005E8B6
		public DataBindingCollection()
		{
			this.bindings = new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x000606CE File Offset: 0x0005E8CE
		public int Count
		{
			get
			{
				return this.bindings.Count;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x000606DC File Offset: 0x0005E8DC
		public string[] RemovedBindings
		{
			get
			{
				if (this.removedBindings != null)
				{
					ICollection keys = this.removedBindings.Keys;
					int count = keys.Count;
					string[] array = new string[count];
					int num = 0;
					foreach (object obj in keys)
					{
						string text = (string)obj;
						array[num++] = text;
					}
					this.removedBindings.Clear();
					return array;
				}
				return new string[0];
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00060778 File Offset: 0x0005E978
		private Hashtable RemovedBindingsTable
		{
			get
			{
				if (this.removedBindings == null)
				{
					this.removedBindings = new Hashtable(StringComparer.OrdinalIgnoreCase);
				}
				return this.removedBindings;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700085E RID: 2142
		public DataBinding this[string propertyName]
		{
			get
			{
				object obj = this.bindings[propertyName];
				if (obj != null)
				{
					return (DataBinding)obj;
				}
				return null;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06001DB7 RID: 7607 RVA: 0x000607BD File Offset: 0x0005E9BD
		// (remove) Token: 0x06001DB8 RID: 7608 RVA: 0x000607D6 File Offset: 0x0005E9D6
		public event EventHandler Changed
		{
			add
			{
				this.changedEvent = (EventHandler)Delegate.Combine(this.changedEvent, value);
			}
			remove
			{
				this.changedEvent = (EventHandler)Delegate.Remove(this.changedEvent, value);
			}
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x000607EF File Offset: 0x0005E9EF
		public void Add(DataBinding binding)
		{
			this.bindings[binding.PropertyName] = binding;
			this.RemovedBindingsTable.Remove(binding.PropertyName);
			this.OnChanged();
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0006081A File Offset: 0x0005EA1A
		public bool Contains(string propertyName)
		{
			return this.bindings.Contains(propertyName);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00060828 File Offset: 0x0005EA28
		public void Clear()
		{
			ICollection keys = this.bindings.Keys;
			if (keys.Count != 0 && this.removedBindings == null)
			{
				Hashtable removedBindingsTable = this.RemovedBindingsTable;
			}
			foreach (object obj in keys)
			{
				string key = (string)obj;
				this.removedBindings[key] = string.Empty;
			}
			this.bindings.Clear();
			this.OnChanged();
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x000608C0 File Offset: 0x0005EAC0
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x000608F0 File Offset: 0x0005EAF0
		public IEnumerator GetEnumerator()
		{
			return this.bindings.Values.GetEnumerator();
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00060902 File Offset: 0x0005EB02
		private void OnChanged()
		{
			if (this.changedEvent != null)
			{
				this.changedEvent(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0006091D File Offset: 0x0005EB1D
		public void Remove(string propertyName)
		{
			this.Remove(propertyName, true);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00060927 File Offset: 0x0005EB27
		public void Remove(DataBinding binding)
		{
			this.Remove(binding.PropertyName, true);
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00060936 File Offset: 0x0005EB36
		public void Remove(string propertyName, bool addToRemovedList)
		{
			if (this.Contains(propertyName))
			{
				this.bindings.Remove(propertyName);
				if (addToRemovedList)
				{
					this.RemovedBindingsTable[propertyName] = string.Empty;
				}
				this.OnChanged();
			}
		}

		// Token: 0x04001966 RID: 6502
		private EventHandler changedEvent;

		// Token: 0x04001967 RID: 6503
		private Hashtable bindings;

		// Token: 0x04001968 RID: 6504
		private Hashtable removedBindings;
	}
}
