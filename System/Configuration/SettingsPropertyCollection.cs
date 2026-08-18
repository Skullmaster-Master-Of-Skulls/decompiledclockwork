using System;
using System.Collections;

namespace System.Configuration
{
	// Token: 0x02000715 RID: 1813
	public class SettingsPropertyCollection : ICloneable, ICollection, IEnumerable
	{
		// Token: 0x06003785 RID: 14213 RVA: 0x000EB73C File Offset: 0x000EA73C
		public SettingsPropertyCollection()
		{
			this._Hashtable = new Hashtable(10, CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000EB75C File Offset: 0x000EA75C
		public void Add(SettingsProperty property)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnAdd(property);
			this._Hashtable.Add(property.Name, property);
			try
			{
				this.OnAddComplete(property);
			}
			catch
			{
				this._Hashtable.Remove(property.Name);
				throw;
			}
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000EB7C0 File Offset: 0x000EA7C0
		public void Remove(string name)
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			SettingsProperty settingsProperty = (SettingsProperty)this._Hashtable[name];
			if (settingsProperty == null)
			{
				return;
			}
			this.OnRemove(settingsProperty);
			this._Hashtable.Remove(name);
			try
			{
				this.OnRemoveComplete(settingsProperty);
			}
			catch
			{
				this._Hashtable.Add(name, settingsProperty);
				throw;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		public SettingsProperty this[string name]
		{
			get
			{
				return this._Hashtable[name] as SettingsProperty;
			}
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x000EB843 File Offset: 0x000EA843
		public IEnumerator GetEnumerator()
		{
			return this._Hashtable.Values.GetEnumerator();
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x000EB855 File Offset: 0x000EA855
		public object Clone()
		{
			return new SettingsPropertyCollection(this._Hashtable);
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x000EB862 File Offset: 0x000EA862
		public void SetReadOnly()
		{
			if (this._ReadOnly)
			{
				return;
			}
			this._ReadOnly = true;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x000EB874 File Offset: 0x000EA874
		public void Clear()
		{
			if (this._ReadOnly)
			{
				throw new NotSupportedException();
			}
			this.OnClear();
			this._Hashtable.Clear();
			this.OnClearComplete();
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x000EB89B File Offset: 0x000EA89B
		protected virtual void OnAdd(SettingsProperty property)
		{
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x000EB89D File Offset: 0x000EA89D
		protected virtual void OnAddComplete(SettingsProperty property)
		{
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000EB89F File Offset: 0x000EA89F
		protected virtual void OnClear()
		{
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000EB8A1 File Offset: 0x000EA8A1
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000EB8A3 File Offset: 0x000EA8A3
		protected virtual void OnRemove(SettingsProperty property)
		{
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000EB8A5 File Offset: 0x000EA8A5
		protected virtual void OnRemoveComplete(SettingsProperty property)
		{
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000EB8A7 File Offset: 0x000EA8A7
		public int Count
		{
			get
			{
				return this._Hashtable.Count;
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000EB8B4 File Offset: 0x000EA8B4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06003795 RID: 14229 RVA: 0x000EB8B7 File Offset: 0x000EA8B7
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000EB8BA File Offset: 0x000EA8BA
		public void CopyTo(Array array, int index)
		{
			this._Hashtable.Values.CopyTo(array, index);
		}

		// Token: 0x06003797 RID: 14231 RVA: 0x000EB8CE File Offset: 0x000EA8CE
		private SettingsPropertyCollection(Hashtable h)
		{
			this._Hashtable = (Hashtable)h.Clone();
		}

		// Token: 0x040031E0 RID: 12768
		private Hashtable _Hashtable;

		// Token: 0x040031E1 RID: 12769
		private bool _ReadOnly;
	}
}
