using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003F2 RID: 1010
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ExpressionBindingCollection : ICollection, IEnumerable
	{
		// Token: 0x060031EE RID: 12782 RVA: 0x000DBD86 File Offset: 0x000DAD86
		public ExpressionBindingCollection()
		{
			this.bindings = new Hashtable(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000DBD9E File Offset: 0x000DAD9E
		public int Count
		{
			get
			{
				return this.bindings.Count;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000DBDAB File Offset: 0x000DADAB
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000DBDAE File Offset: 0x000DADAE
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x000DBDB4 File Offset: 0x000DADB4
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

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000DBE50 File Offset: 0x000DAE50
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

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x000DBE70 File Offset: 0x000DAE70
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000AFE RID: 2814
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

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060031F6 RID: 12790 RVA: 0x000DBE99 File Offset: 0x000DAE99
		// (remove) Token: 0x060031F7 RID: 12791 RVA: 0x000DBEB2 File Offset: 0x000DAEB2
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

		// Token: 0x060031F8 RID: 12792 RVA: 0x000DBECB File Offset: 0x000DAECB
		public void Add(ExpressionBinding binding)
		{
			this.bindings[binding.PropertyName] = binding;
			this.RemovedBindingsTable.Remove(binding.PropertyName);
			this.OnChanged();
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000DBEF6 File Offset: 0x000DAEF6
		public bool Contains(string propName)
		{
			return this.bindings.Contains(propName);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000DBF04 File Offset: 0x000DAF04
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

		// Token: 0x060031FB RID: 12795 RVA: 0x000DBF98 File Offset: 0x000DAF98
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000DBFC8 File Offset: 0x000DAFC8
		public void CopyTo(ExpressionBinding[] array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000DBFF8 File Offset: 0x000DAFF8
		public IEnumerator GetEnumerator()
		{
			return this.bindings.Values.GetEnumerator();
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x000DC00A File Offset: 0x000DB00A
		private void OnChanged()
		{
			if (this.changedEvent != null)
			{
				this.changedEvent(this, EventArgs.Empty);
			}
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000DC025 File Offset: 0x000DB025
		public void Remove(string propertyName)
		{
			this.Remove(propertyName, true);
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x000DC02F File Offset: 0x000DB02F
		public void Remove(ExpressionBinding binding)
		{
			this.Remove(binding.PropertyName, true);
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x000DC03E File Offset: 0x000DB03E
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

		// Token: 0x040022EA RID: 8938
		private EventHandler changedEvent;

		// Token: 0x040022EB RID: 8939
		private Hashtable bindings;

		// Token: 0x040022EC RID: 8940
		private Hashtable removedBindings;
	}
}
