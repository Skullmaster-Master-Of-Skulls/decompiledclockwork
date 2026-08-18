using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003D1 RID: 977
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBindingCollection : ICollection, IEnumerable
	{
		// Token: 0x06002FAD RID: 12205 RVA: 0x000D4212 File Offset: 0x000D3212
		public DataBindingCollection()
		{
			this.bindings = new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000D422A File Offset: 0x000D322A
		public int Count
		{
			get
			{
				return this.bindings.Count;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000D4237 File Offset: 0x000D3237
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x000D423A File Offset: 0x000D323A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000D4240 File Offset: 0x000D3240
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

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000D42DC File Offset: 0x000D32DC
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

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000D42FC File Offset: 0x000D32FC
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000A5D RID: 2653
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

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x06002FB5 RID: 12213 RVA: 0x000D4325 File Offset: 0x000D3325
		// (remove) Token: 0x06002FB6 RID: 12214 RVA: 0x000D433E File Offset: 0x000D333E
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

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000D4357 File Offset: 0x000D3357
		public void Add(DataBinding binding)
		{
			this.bindings[binding.PropertyName] = binding;
			this.RemovedBindingsTable.Remove(binding.PropertyName);
			this.OnChanged();
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000D4382 File Offset: 0x000D3382
		public bool Contains(string propertyName)
		{
			return this.bindings.Contains(propertyName);
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000D4390 File Offset: 0x000D3390
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

		// Token: 0x06002FBA RID: 12218 RVA: 0x000D4424 File Offset: 0x000D3424
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000D4454 File Offset: 0x000D3454
		public IEnumerator GetEnumerator()
		{
			return this.bindings.Values.GetEnumerator();
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000D4466 File Offset: 0x000D3466
		private void OnChanged()
		{
			if (this.changedEvent != null)
			{
				this.changedEvent(this, EventArgs.Empty);
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000D4481 File Offset: 0x000D3481
		public void Remove(string propertyName)
		{
			this.Remove(propertyName, true);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000D448B File Offset: 0x000D348B
		public void Remove(DataBinding binding)
		{
			this.Remove(binding.PropertyName, true);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000D449A File Offset: 0x000D349A
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

		// Token: 0x040021EF RID: 8687
		private EventHandler changedEvent;

		// Token: 0x040021F0 RID: 8688
		private Hashtable bindings;

		// Token: 0x040021F1 RID: 8689
		private Hashtable removedBindings;
	}
}
