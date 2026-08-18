using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000288 RID: 648
	public sealed class ExpressionBindingCollection : ICollection, IEnumerable
	{
		// Token: 0x06001E86 RID: 7814 RVA: 0x00061E76 File Offset: 0x00060076
		public ExpressionBindingCollection()
		{
			this.bindings = new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x00061E8E File Offset: 0x0006008E
		public int Count
		{
			get
			{
				return this.bindings.Count;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x00061E9C File Offset: 0x0006009C
		public ICollection RemovedBindings
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

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00061F38 File Offset: 0x00060138
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

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000895 RID: 2197
		public ExpressionBinding this[string propertyName]
		{
			get
			{
				object obj = this.bindings[propertyName];
				if (obj != null)
				{
					return (ExpressionBinding)obj;
				}
				return null;
			}
		}

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x06001E8E RID: 7822 RVA: 0x00061F7D File Offset: 0x0006017D
		// (remove) Token: 0x06001E8F RID: 7823 RVA: 0x00061F96 File Offset: 0x00060196
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

		// Token: 0x06001E90 RID: 7824 RVA: 0x00061FAF File Offset: 0x000601AF
		public void Add(ExpressionBinding binding)
		{
			this.bindings[binding.PropertyName] = binding;
			this.RemovedBindingsTable.Remove(binding.PropertyName);
			this.OnChanged();
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00061FDA File Offset: 0x000601DA
		public bool Contains(string propName)
		{
			return this.bindings.Contains(propName);
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00061FE8 File Offset: 0x000601E8
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

		// Token: 0x06001E93 RID: 7827 RVA: 0x00062080 File Offset: 0x00060280
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x000620B0 File Offset: 0x000602B0
		public void CopyTo(ExpressionBinding[] array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000620E0 File Offset: 0x000602E0
		public IEnumerator GetEnumerator()
		{
			return this.bindings.Values.GetEnumerator();
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x000620F2 File Offset: 0x000602F2
		private void OnChanged()
		{
			if (this.changedEvent != null)
			{
				this.changedEvent(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x0006210D File Offset: 0x0006030D
		public void Remove(string propertyName)
		{
			this.Remove(propertyName, true);
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x00062117 File Offset: 0x00060317
		public void Remove(ExpressionBinding binding)
		{
			this.Remove(binding.PropertyName, true);
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x00062126 File Offset: 0x00060326
		public void Remove(string propertyName, bool addToRemovedList)
		{
			if (this.Contains(propertyName))
			{
				if (addToRemovedList && this.bindings.Contains(propertyName))
				{
					this.RemovedBindingsTable[propertyName] = string.Empty;
				}
				this.bindings.Remove(propertyName);
				this.OnChanged();
			}
		}

		// Token: 0x0400199E RID: 6558
		private EventHandler changedEvent;

		// Token: 0x0400199F RID: 6559
		private Hashtable bindings;

		// Token: 0x040019A0 RID: 6560
		private Hashtable removedBindings;
	}
}
