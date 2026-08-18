using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms
{
	// Token: 0x02000136 RID: 310
	public abstract class BindingManagerBase
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001FD14 File Offset: 0x0001DF14
		public BindingsCollection Bindings
		{
			get
			{
				if (this.bindings == null)
				{
					this.bindings = new ListManagerBindingsCollection(this);
					this.bindings.CollectionChanging += this.OnBindingsCollectionChanging;
					this.bindings.CollectionChanged += this.OnBindingsCollectionChanged;
				}
				return this.bindings;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0001FD69 File Offset: 0x0001DF69
		protected internal void OnBindingComplete(BindingCompleteEventArgs args)
		{
			if (this.onBindingCompleteHandler != null)
			{
				this.onBindingCompleteHandler(this, args);
			}
		}

		// Token: 0x06000B26 RID: 2854
		protected internal abstract void OnCurrentChanged(EventArgs e);

		// Token: 0x06000B27 RID: 2855
		protected internal abstract void OnCurrentItemChanged(EventArgs e);

		// Token: 0x06000B28 RID: 2856 RVA: 0x0001FD80 File Offset: 0x0001DF80
		protected internal void OnDataError(Exception e)
		{
			if (this.onDataErrorHandler != null)
			{
				this.onDataErrorHandler(this, new BindingManagerDataErrorEventArgs(e));
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B29 RID: 2857
		public abstract object Current { get; }

		// Token: 0x06000B2A RID: 2858
		internal abstract void SetDataSource(object dataSource);

		// Token: 0x06000B2B RID: 2859 RVA: 0x00002843 File Offset: 0x00000A43
		public BindingManagerBase()
		{
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		internal BindingManagerBase(object dataSource)
		{
			this.SetDataSource(dataSource);
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B2D RID: 2861
		internal abstract Type BindType { get; }

		// Token: 0x06000B2E RID: 2862
		internal abstract PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001FDAB File Offset: 0x0001DFAB
		public virtual PropertyDescriptorCollection GetItemProperties()
		{
			return this.GetItemProperties(null);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		protected internal virtual PropertyDescriptorCollection GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
		{
			IList list = null;
			if (this is CurrencyManager)
			{
				list = ((CurrencyManager)this).List;
			}
			if (list is ITypedList)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[listAccessors.Count];
				listAccessors.CopyTo(array, 0);
				return ((ITypedList)list).GetItemProperties(array);
			}
			return this.GetItemProperties(this.BindType, 0, dataSources, listAccessors);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001FE10 File Offset: 0x0001E010
		protected virtual PropertyDescriptorCollection GetItemProperties(Type listType, int offset, ArrayList dataSources, ArrayList listAccessors)
		{
			if (listAccessors.Count < offset)
			{
				return null;
			}
			if (listAccessors.Count != offset)
			{
				PropertyInfo[] properties = listType.GetProperties();
				if (typeof(IList).IsAssignableFrom(listType))
				{
					PropertyDescriptorCollection propertyDescriptorCollection = null;
					for (int i = 0; i < properties.Length; i++)
					{
						if ("Item".Equals(properties[i].Name) && properties[i].PropertyType != typeof(object))
						{
							propertyDescriptorCollection = TypeDescriptor.GetProperties(properties[i].PropertyType, new Attribute[]
							{
								new BrowsableAttribute(true)
							});
						}
					}
					if (propertyDescriptorCollection == null)
					{
						IList list;
						if (offset == 0)
						{
							list = (this.DataSource as IList);
						}
						else
						{
							list = (dataSources[offset - 1] as IList);
						}
						if (list != null && list.Count > 0)
						{
							propertyDescriptorCollection = TypeDescriptor.GetProperties(list[0]);
						}
					}
					if (propertyDescriptorCollection != null)
					{
						for (int j = 0; j < propertyDescriptorCollection.Count; j++)
						{
							if (propertyDescriptorCollection[j].Equals(listAccessors[offset]))
							{
								return this.GetItemProperties(propertyDescriptorCollection[j].PropertyType, offset + 1, dataSources, listAccessors);
							}
						}
					}
				}
				else
				{
					for (int k = 0; k < properties.Length; k++)
					{
						if (properties[k].Name.Equals(((PropertyDescriptor)listAccessors[offset]).Name))
						{
							return this.GetItemProperties(properties[k].PropertyType, offset + 1, dataSources, listAccessors);
						}
					}
				}
				return null;
			}
			if (!typeof(IList).IsAssignableFrom(listType))
			{
				return TypeDescriptor.GetProperties(listType);
			}
			PropertyInfo[] properties2 = listType.GetProperties();
			for (int l = 0; l < properties2.Length; l++)
			{
				if ("Item".Equals(properties2[l].Name) && properties2[l].PropertyType != typeof(object))
				{
					return TypeDescriptor.GetProperties(properties2[l].PropertyType, new Attribute[]
					{
						new BrowsableAttribute(true)
					});
				}
			}
			IList list2 = dataSources[offset - 1] as IList;
			if (list2 != null && list2.Count > 0)
			{
				return TypeDescriptor.GetProperties(list2[0]);
			}
			return null;
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06000B32 RID: 2866 RVA: 0x0002003A File Offset: 0x0001E23A
		// (remove) Token: 0x06000B33 RID: 2867 RVA: 0x00020053 File Offset: 0x0001E253
		public event BindingCompleteEventHandler BindingComplete
		{
			add
			{
				this.onBindingCompleteHandler = (BindingCompleteEventHandler)Delegate.Combine(this.onBindingCompleteHandler, value);
			}
			remove
			{
				this.onBindingCompleteHandler = (BindingCompleteEventHandler)Delegate.Remove(this.onBindingCompleteHandler, value);
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000B34 RID: 2868 RVA: 0x0002006C File Offset: 0x0001E26C
		// (remove) Token: 0x06000B35 RID: 2869 RVA: 0x00020085 File Offset: 0x0001E285
		public event EventHandler CurrentChanged
		{
			add
			{
				this.onCurrentChangedHandler = (EventHandler)Delegate.Combine(this.onCurrentChangedHandler, value);
			}
			remove
			{
				this.onCurrentChangedHandler = (EventHandler)Delegate.Remove(this.onCurrentChangedHandler, value);
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000B36 RID: 2870 RVA: 0x0002009E File Offset: 0x0001E29E
		// (remove) Token: 0x06000B37 RID: 2871 RVA: 0x000200B7 File Offset: 0x0001E2B7
		public event EventHandler CurrentItemChanged
		{
			add
			{
				this.onCurrentItemChangedHandler = (EventHandler)Delegate.Combine(this.onCurrentItemChangedHandler, value);
			}
			remove
			{
				this.onCurrentItemChangedHandler = (EventHandler)Delegate.Remove(this.onCurrentItemChangedHandler, value);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000B38 RID: 2872 RVA: 0x000200D0 File Offset: 0x0001E2D0
		// (remove) Token: 0x06000B39 RID: 2873 RVA: 0x000200E9 File Offset: 0x0001E2E9
		public event BindingManagerDataErrorEventHandler DataError
		{
			add
			{
				this.onDataErrorHandler = (BindingManagerDataErrorEventHandler)Delegate.Combine(this.onDataErrorHandler, value);
			}
			remove
			{
				this.onDataErrorHandler = (BindingManagerDataErrorEventHandler)Delegate.Remove(this.onDataErrorHandler, value);
			}
		}

		// Token: 0x06000B3A RID: 2874
		internal abstract string GetListName();

		// Token: 0x06000B3B RID: 2875
		public abstract void CancelCurrentEdit();

		// Token: 0x06000B3C RID: 2876
		public abstract void EndCurrentEdit();

		// Token: 0x06000B3D RID: 2877
		public abstract void AddNew();

		// Token: 0x06000B3E RID: 2878
		public abstract void RemoveAt(int index);

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B3F RID: 2879
		// (set) Token: 0x06000B40 RID: 2880
		public abstract int Position { get; set; }

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x06000B41 RID: 2881 RVA: 0x00020102 File Offset: 0x0001E302
		// (remove) Token: 0x06000B42 RID: 2882 RVA: 0x0002011B File Offset: 0x0001E31B
		public event EventHandler PositionChanged
		{
			add
			{
				this.onPositionChangedHandler = (EventHandler)Delegate.Combine(this.onPositionChangedHandler, value);
			}
			remove
			{
				this.onPositionChangedHandler = (EventHandler)Delegate.Remove(this.onPositionChangedHandler, value);
			}
		}

		// Token: 0x06000B43 RID: 2883
		protected abstract void UpdateIsBinding();

		// Token: 0x06000B44 RID: 2884
		protected internal abstract string GetListName(ArrayList listAccessors);

		// Token: 0x06000B45 RID: 2885
		public abstract void SuspendBinding();

		// Token: 0x06000B46 RID: 2886
		public abstract void ResumeBinding();

		// Token: 0x06000B47 RID: 2887 RVA: 0x00020134 File Offset: 0x0001E334
		protected void PullData()
		{
			bool flag;
			this.PullData(out flag);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0002014C File Offset: 0x0001E34C
		internal void PullData(out bool success)
		{
			success = true;
			this.pullingData = true;
			try
			{
				this.UpdateIsBinding();
				int count = this.Bindings.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.Bindings[i].PullData())
					{
						success = false;
					}
				}
			}
			finally
			{
				this.pullingData = false;
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000201B4 File Offset: 0x0001E3B4
		protected void PushData()
		{
			bool flag;
			this.PushData(out flag);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000201CC File Offset: 0x0001E3CC
		internal void PushData(out bool success)
		{
			success = true;
			if (this.pullingData)
			{
				return;
			}
			this.UpdateIsBinding();
			int count = this.Bindings.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.Bindings[i].PushData())
				{
					success = false;
				}
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B4B RID: 2891
		internal abstract object DataSource { get; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B4C RID: 2892
		internal abstract bool IsBinding { get; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00020219 File Offset: 0x0001E419
		public bool IsBindingSuspended
		{
			get
			{
				return !this.IsBinding;
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000B4E RID: 2894
		public abstract int Count { get; }

		// Token: 0x06000B4F RID: 2895 RVA: 0x00020224 File Offset: 0x0001E424
		private void OnBindingsCollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			Binding binding = e.Element as Binding;
			switch (e.Action)
			{
			case CollectionChangeAction.Add:
				binding.BindingComplete += this.Binding_BindingComplete;
				return;
			case CollectionChangeAction.Remove:
				binding.BindingComplete -= this.Binding_BindingComplete;
				return;
			case CollectionChangeAction.Refresh:
				foreach (object obj in this.bindings)
				{
					Binding binding2 = (Binding)obj;
					binding2.BindingComplete += this.Binding_BindingComplete;
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000202DC File Offset: 0x0001E4DC
		private void OnBindingsCollectionChanging(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Refresh)
			{
				foreach (object obj in this.bindings)
				{
					Binding binding = (Binding)obj;
					binding.BindingComplete -= this.Binding_BindingComplete;
				}
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0002034C File Offset: 0x0001E54C
		internal void Binding_BindingComplete(object sender, BindingCompleteEventArgs args)
		{
			this.OnBindingComplete(args);
		}

		// Token: 0x040006C4 RID: 1732
		private BindingsCollection bindings;

		// Token: 0x040006C5 RID: 1733
		private bool pullingData;

		// Token: 0x040006C6 RID: 1734
		protected EventHandler onCurrentChangedHandler;

		// Token: 0x040006C7 RID: 1735
		protected EventHandler onPositionChangedHandler;

		// Token: 0x040006C8 RID: 1736
		private BindingCompleteEventHandler onBindingCompleteHandler;

		// Token: 0x040006C9 RID: 1737
		internal EventHandler onCurrentItemChangedHandler;

		// Token: 0x040006CA RID: 1738
		internal BindingManagerDataErrorEventHandler onDataErrorHandler;
	}
}
