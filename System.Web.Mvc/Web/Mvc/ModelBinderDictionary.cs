using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x02000183 RID: 387
	public class ModelBinderDictionary : IDictionary<Type, IModelBinder>, ICollection<KeyValuePair<Type, IModelBinder>>, IEnumerable<KeyValuePair<Type, IModelBinder>>, IEnumerable
	{
		// Token: 0x06000A8B RID: 2699 RVA: 0x0001CEA0 File Offset: 0x0001B0A0
		public ModelBinderDictionary() : this(ModelBinderProviders.BinderProviders)
		{
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0001CEAD File Offset: 0x0001B0AD
		internal ModelBinderDictionary(ModelBinderProviderCollection modelBinderProviders)
		{
			this._modelBinderProviders = modelBinderProviders;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0001CEC7 File Offset: 0x0001B0C7
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0001CED4 File Offset: 0x0001B0D4
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x0001CEEF File Offset: 0x0001B0EF
		public IModelBinder DefaultBinder
		{
			get
			{
				if (this._defaultBinder == null)
				{
					this._defaultBinder = new DefaultModelBinder();
				}
				return this._defaultBinder;
			}
			set
			{
				this._defaultBinder = value;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0001CEF8 File Offset: 0x0001B0F8
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).IsReadOnly;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0001CF05 File Offset: 0x0001B105
		public ICollection<Type> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x0001CF12 File Offset: 0x0001B112
		public ICollection<IModelBinder> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		// Token: 0x1700026E RID: 622
		public IModelBinder this[Type key]
		{
			get
			{
				IModelBinder result;
				this._innerDictionary.TryGetValue(key, out result);
				return result;
			}
			set
			{
				this._innerDictionary[key] = value;
			}
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0001CF4C File Offset: 0x0001B14C
		public void Add(KeyValuePair<Type, IModelBinder> item)
		{
			((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).Add(item);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0001CF5A File Offset: 0x0001B15A
		public void Add(Type key, IModelBinder value)
		{
			this._innerDictionary.Add(key, value);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0001CF69 File Offset: 0x0001B169
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0001CF76 File Offset: 0x0001B176
		public bool Contains(KeyValuePair<Type, IModelBinder> item)
		{
			return ((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).Contains(item);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0001CF84 File Offset: 0x0001B184
		public bool ContainsKey(Type key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0001CF92 File Offset: 0x0001B192
		public void CopyTo(KeyValuePair<Type, IModelBinder>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0001CFA1 File Offset: 0x0001B1A1
		public IModelBinder GetBinder(Type modelType)
		{
			return this.GetBinder(modelType, true);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001CFAB File Offset: 0x0001B1AB
		public virtual IModelBinder GetBinder(Type modelType, bool fallbackToDefault)
		{
			if (modelType == null)
			{
				throw new ArgumentNullException("modelType");
			}
			return this.GetBinder(modelType, fallbackToDefault ? this.DefaultBinder : null);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0001D008 File Offset: 0x0001B208
		private IModelBinder GetBinder(Type modelType, IModelBinder fallbackBinder)
		{
			IModelBinder modelBinder = this._modelBinderProviders.GetBinder(modelType);
			if (modelBinder != null)
			{
				return modelBinder;
			}
			if (this._innerDictionary.TryGetValue(modelType, out modelBinder))
			{
				return modelBinder;
			}
			modelBinder = ModelBinders.GetBinderFromAttributes(modelType, delegate(Type errorModel)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, MvcResources.ModelBinderDictionary_MultipleAttributes, new object[]
				{
					errorModel.FullName
				}));
			});
			return modelBinder ?? fallbackBinder;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0001D063 File Offset: 0x0001B263
		public IEnumerator<KeyValuePair<Type, IModelBinder>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0001D075 File Offset: 0x0001B275
		public bool Remove(KeyValuePair<Type, IModelBinder> item)
		{
			return ((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).Remove(item);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0001D083 File Offset: 0x0001B283
		public bool Remove(Type key)
		{
			return this._innerDictionary.Remove(key);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0001D091 File Offset: 0x0001B291
		public bool TryGetValue(Type key, out IModelBinder value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._innerDictionary).GetEnumerator();
		}

		// Token: 0x040002D3 RID: 723
		private readonly Dictionary<Type, IModelBinder> _innerDictionary = new Dictionary<Type, IModelBinder>();

		// Token: 0x040002D4 RID: 724
		private IModelBinder _defaultBinder;

		// Token: 0x040002D5 RID: 725
		private ModelBinderProviderCollection _modelBinderProviders;
	}
}
