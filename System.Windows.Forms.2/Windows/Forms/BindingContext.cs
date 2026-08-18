using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000135 RID: 309
	[DefaultEvent("CollectionChanged")]
	public class BindingContext : ICollection, IEnumerable
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0001F854 File Offset: 0x0001DA54
		int ICollection.Count
		{
			get
			{
				this.ScrubWeakRefs();
				return this.listManagers.Count;
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0001F867 File Offset: 0x0001DA67
		void ICollection.CopyTo(Array ar, int index)
		{
			IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			this.listManagers.CopyTo(ar, index);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0001F886 File Offset: 0x0001DA86
		IEnumerator IEnumerable.GetEnumerator()
		{
			IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			return this.listManagers.GetEnumerator();
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00011A20 File Offset: 0x0000FC20
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00015ECC File Offset: 0x000140CC
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0001F8A3 File Offset: 0x0001DAA3
		public BindingContext()
		{
			this.listManagers = new Hashtable();
		}

		// Token: 0x170002C6 RID: 710
		public BindingManagerBase this[object dataSource]
		{
			get
			{
				return this[dataSource, ""];
			}
		}

		// Token: 0x170002C7 RID: 711
		public BindingManagerBase this[object dataSource, string dataMember]
		{
			get
			{
				return this.EnsureListManager(dataSource, dataMember);
			}
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001F8CE File Offset: 0x0001DACE
		protected internal void Add(object dataSource, BindingManagerBase listManager)
		{
			this.AddCore(dataSource, listManager);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, dataSource));
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0001F8E5 File Offset: 0x0001DAE5
		protected virtual void AddCore(object dataSource, BindingManagerBase listManager)
		{
			if (dataSource == null)
			{
				throw new ArgumentNullException("dataSource");
			}
			if (listManager == null)
			{
				throw new ArgumentNullException("listManager");
			}
			this.listManagers[this.GetKey(dataSource, "")] = new WeakReference(listManager, false);
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000B16 RID: 2838 RVA: 0x0001F921 File Offset: 0x0001DB21
		// (remove) Token: 0x06000B17 RID: 2839 RVA: 0x000072B6 File Offset: 0x000054B6
		[SRDescription("collectionChangedEventDescr")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				throw new NotImplementedException();
			}
			remove
			{
			}
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0001F928 File Offset: 0x0001DB28
		protected internal void Clear()
		{
			this.ClearCore();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0001F93D File Offset: 0x0001DB3D
		protected virtual void ClearCore()
		{
			this.listManagers.Clear();
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0001F94A File Offset: 0x0001DB4A
		public bool Contains(object dataSource)
		{
			return this.Contains(dataSource, "");
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0001F958 File Offset: 0x0001DB58
		public bool Contains(object dataSource, string dataMember)
		{
			return this.listManagers.ContainsKey(this.GetKey(dataSource, dataMember));
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0001F96D File Offset: 0x0001DB6D
		internal BindingContext.HashKey GetKey(object dataSource, string dataMember)
		{
			return new BindingContext.HashKey(dataSource, dataMember);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000072B6 File Offset: 0x000054B6
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0001F976 File Offset: 0x0001DB76
		protected internal void Remove(object dataSource)
		{
			this.RemoveCore(dataSource);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, dataSource));
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0001F98C File Offset: 0x0001DB8C
		protected virtual void RemoveCore(object dataSource)
		{
			this.listManagers.Remove(this.GetKey(dataSource, ""));
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
		internal BindingManagerBase EnsureListManager(object dataSource, string dataMember)
		{
			BindingManagerBase bindingManagerBase = null;
			if (dataMember == null)
			{
				dataMember = "";
			}
			if (dataSource is ICurrencyManagerProvider)
			{
				bindingManagerBase = (dataSource as ICurrencyManagerProvider).GetRelatedCurrencyManager(dataMember);
				if (bindingManagerBase != null)
				{
					return bindingManagerBase;
				}
			}
			BindingContext.HashKey key = this.GetKey(dataSource, dataMember);
			WeakReference weakReference = this.listManagers[key] as WeakReference;
			if (weakReference != null)
			{
				bindingManagerBase = (BindingManagerBase)weakReference.Target;
			}
			if (bindingManagerBase != null)
			{
				return bindingManagerBase;
			}
			if (dataMember.Length == 0)
			{
				if (dataSource is IList || dataSource is IListSource)
				{
					bindingManagerBase = new CurrencyManager(dataSource);
				}
				else
				{
					bindingManagerBase = new PropertyManager(dataSource);
				}
			}
			else
			{
				int num = dataMember.LastIndexOf(".");
				string dataMember2 = (num == -1) ? "" : dataMember.Substring(0, num);
				string text = dataMember.Substring(num + 1);
				BindingManagerBase bindingManagerBase2 = this.EnsureListManager(dataSource, dataMember2);
				PropertyDescriptor propertyDescriptor = bindingManagerBase2.GetItemProperties().Find(text, true);
				if (propertyDescriptor == null)
				{
					throw new ArgumentException(SR.GetString("RelatedListManagerChild", new object[]
					{
						text
					}));
				}
				if (typeof(IList).IsAssignableFrom(propertyDescriptor.PropertyType))
				{
					bindingManagerBase = new RelatedCurrencyManager(bindingManagerBase2, text);
				}
				else
				{
					bindingManagerBase = new RelatedPropertyManager(bindingManagerBase2, text);
				}
			}
			if (weakReference == null)
			{
				this.listManagers.Add(key, new WeakReference(bindingManagerBase, false));
			}
			else
			{
				weakReference.Target = bindingManagerBase;
			}
			IntSecurity.UnmanagedCode.Demand();
			this.ScrubWeakRefs();
			return bindingManagerBase;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001FB00 File Offset: 0x0001DD00
		private static void CheckPropertyBindingCycles(BindingContext newBindingContext, Binding propBinding)
		{
			if (newBindingContext == null || propBinding == null)
			{
				return;
			}
			if (newBindingContext.Contains(propBinding.BindableComponent, ""))
			{
				BindingManagerBase bindingManagerBase = newBindingContext.EnsureListManager(propBinding.BindableComponent, "");
				for (int i = 0; i < bindingManagerBase.Bindings.Count; i++)
				{
					Binding binding = bindingManagerBase.Bindings[i];
					if (binding.DataSource == propBinding.BindableComponent)
					{
						if (propBinding.BindToObject.BindingMemberInfo.BindingMember.Equals(binding.PropertyName))
						{
							throw new ArgumentException(SR.GetString("DataBindingCycle", new object[]
							{
								binding.PropertyName
							}), "propBinding");
						}
					}
					else if (propBinding.BindToObject.BindingManagerBase is PropertyManager)
					{
						BindingContext.CheckPropertyBindingCycles(newBindingContext, binding);
					}
				}
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001FBD0 File Offset: 0x0001DDD0
		private void ScrubWeakRefs()
		{
			ArrayList arrayList = null;
			foreach (object obj in this.listManagers)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				WeakReference weakReference = (WeakReference)dictionaryEntry.Value;
				if (weakReference.Target == null)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			if (arrayList != null)
			{
				foreach (object key in arrayList)
				{
					this.listManagers.Remove(key);
				}
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		public static void UpdateBinding(BindingContext newBindingContext, Binding binding)
		{
			BindingManagerBase bindingManagerBase = binding.BindingManagerBase;
			if (bindingManagerBase != null)
			{
				bindingManagerBase.Bindings.Remove(binding);
			}
			if (newBindingContext != null)
			{
				if (binding.BindToObject.BindingManagerBase is PropertyManager)
				{
					BindingContext.CheckPropertyBindingCycles(newBindingContext, binding);
				}
				BindToObject bindToObject = binding.BindToObject;
				BindingManagerBase bindingManagerBase2 = newBindingContext.EnsureListManager(bindToObject.DataSource, bindToObject.BindingMemberInfo.BindingPath);
				bindingManagerBase2.Bindings.Add(binding);
			}
		}

		// Token: 0x040006C3 RID: 1731
		private Hashtable listManagers;

		// Token: 0x0200061B RID: 1563
		internal class HashKey
		{
			// Token: 0x06006302 RID: 25346 RVA: 0x0016E52C File Offset: 0x0016C72C
			internal HashKey(object dataSource, string dataMember)
			{
				if (dataSource == null)
				{
					throw new ArgumentNullException("dataSource");
				}
				if (dataMember == null)
				{
					dataMember = "";
				}
				this.wRef = new WeakReference(dataSource, false);
				this.dataSourceHashCode = dataSource.GetHashCode();
				this.dataMember = dataMember.ToLower(CultureInfo.InvariantCulture);
			}

			// Token: 0x06006303 RID: 25347 RVA: 0x0016E581 File Offset: 0x0016C781
			public override int GetHashCode()
			{
				return this.dataSourceHashCode * this.dataMember.GetHashCode();
			}

			// Token: 0x06006304 RID: 25348 RVA: 0x0016E598 File Offset: 0x0016C798
			public override bool Equals(object target)
			{
				if (target is BindingContext.HashKey)
				{
					BindingContext.HashKey hashKey = (BindingContext.HashKey)target;
					return this.wRef.Target == hashKey.wRef.Target && this.dataMember == hashKey.dataMember;
				}
				return false;
			}

			// Token: 0x04003920 RID: 14624
			private WeakReference wRef;

			// Token: 0x04003921 RID: 14625
			private int dataSourceHashCode;

			// Token: 0x04003922 RID: 14626
			private string dataMember;
		}
	}
}
