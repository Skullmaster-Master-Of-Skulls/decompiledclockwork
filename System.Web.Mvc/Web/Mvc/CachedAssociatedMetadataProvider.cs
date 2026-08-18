using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Caching;

namespace System.Web.Mvc
{
	// Token: 0x0200006D RID: 109
	public abstract class CachedAssociatedMetadataProvider<TModelMetadata> : AssociatedMetadataProvider where TModelMetadata : ModelMetadata
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000099D9 File Offset: 0x00007BD9
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x000099E1 File Offset: 0x00007BE1
		protected internal CacheItemPolicy CacheItemPolicy
		{
			get
			{
				return this._cacheItemPolicy;
			}
			set
			{
				this._cacheItemPolicy = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000099EC File Offset: 0x00007BEC
		protected string CacheKeyPrefix
		{
			get
			{
				if (this._cacheKeyPrefix == null)
				{
					this._cacheKeyPrefix = "MetadataPrototypes::" + base.GetType().GUID.ToString("B");
				}
				return this._cacheKeyPrefix;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00009A2F File Offset: 0x00007C2F
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00009A40 File Offset: 0x00007C40
		protected internal ObjectCache PrototypeCache
		{
			get
			{
				return this._prototypeCache ?? MemoryCache.Default;
			}
			set
			{
				this._prototypeCache = value;
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00009A4C File Offset: 0x00007C4C
		protected sealed override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
		{
			Type type = containerType ?? modelType;
			string cacheKey = this.GetCacheKey(type, propertyName);
			TModelMetadata tmodelMetadata = this.PrototypeCache.Get(cacheKey, null) as TModelMetadata;
			if (tmodelMetadata == null)
			{
				tmodelMetadata = this.CreateMetadataPrototype(attributes, containerType, modelType, propertyName);
				this.PrototypeCache.Add(cacheKey, tmodelMetadata, this.CacheItemPolicy, null);
			}
			return this.CreateMetadataFromPrototype(tmodelMetadata, modelAccessor);
		}

		// Token: 0x060002F6 RID: 758
		protected abstract TModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName);

		// Token: 0x060002F7 RID: 759
		protected abstract TModelMetadata CreateMetadataFromPrototype(TModelMetadata prototype, Func<object> modelAccessor);

		// Token: 0x060002F8 RID: 760 RVA: 0x00009ABF File Offset: 0x00007CBF
		internal string GetCacheKey(Type type, string propertyName = null)
		{
			propertyName = (propertyName ?? string.Empty);
			return this.CacheKeyPrefix + CachedAssociatedMetadataProvider<TModelMetadata>.GetTypeId(type) + propertyName;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00009ADF File Offset: 0x00007CDF
		public sealed override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, string propertyName)
		{
			return base.GetMetadataForProperty(modelAccessor, containerType, propertyName);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00009AEA File Offset: 0x00007CEA
		protected sealed override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType, PropertyDescriptor propertyDescriptor)
		{
			return base.GetMetadataForProperty(modelAccessor, containerType, propertyDescriptor);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00009AF5 File Offset: 0x00007CF5
		public sealed override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
		{
			return base.GetMetadataForProperties(container, containerType);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00009AFF File Offset: 0x00007CFF
		public sealed override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
		{
			return base.GetMetadataForType(modelAccessor, modelType);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00009B2B File Offset: 0x00007D2B
		private static string GetTypeId(Type type)
		{
			return CachedAssociatedMetadataProvider<TModelMetadata>._typeIds.GetOrAdd(type, (Type _) => Guid.NewGuid().ToString("B"));
		}

		// Token: 0x040000AB RID: 171
		private static ConcurrentDictionary<Type, string> _typeIds = new ConcurrentDictionary<Type, string>();

		// Token: 0x040000AC RID: 172
		private string _cacheKeyPrefix;

		// Token: 0x040000AD RID: 173
		private CacheItemPolicy _cacheItemPolicy = new CacheItemPolicy
		{
			SlidingExpiration = TimeSpan.FromMinutes(20.0)
		};

		// Token: 0x040000AE RID: 174
		private ObjectCache _prototypeCache;
	}
}
