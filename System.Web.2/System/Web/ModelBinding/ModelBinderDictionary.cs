using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.ModelBinding
{
	// Token: 0x02000656 RID: 1622
	public class ModelBinderDictionary : IDictionary<Type, IModelBinder>, ICollection<KeyValuePair<Type, IModelBinder>>, IEnumerable<KeyValuePair<Type, IModelBinder>>, IEnumerable
	{
		// Token: 0x06004FA9 RID: 20393 RVA: 0x001148CC File Offset: 0x00112ACC
		public ModelBinderDictionary() : this(ModelBinderProviders.Providers)
		{
		}

		// Token: 0x06004FAA RID: 20394 RVA: 0x001148D9 File Offset: 0x00112AD9
		internal ModelBinderDictionary(ModelBinderProviderCollection modelBinderProviders)
		{
			this._modelBinderProviders = modelBinderProviders;
		}

		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06004FAB RID: 20395 RVA: 0x001148F3 File Offset: 0x00112AF3
		public int Count
		{
			get
			{
				return this._innerDictionary.Count;
			}
		}

		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06004FAC RID: 20396 RVA: 0x00114900 File Offset: 0x00112B00
		// (set) Token: 0x06004FAD RID: 20397 RVA: 0x0011491B File Offset: 0x00112B1B
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

		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x00114924 File Offset: 0x00112B24
		public bool IsReadOnly
		{
			get
			{
				return ((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).IsReadOnly;
			}
		}

		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06004FAF RID: 20399 RVA: 0x00114931 File Offset: 0x00112B31
		public ICollection<Type> Keys
		{
			get
			{
				return this._innerDictionary.Keys;
			}
		}

		// Token: 0x170016F9 RID: 5881
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

		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06004FB2 RID: 20402 RVA: 0x0011496C File Offset: 0x00112B6C
		public ICollection<IModelBinder> Values
		{
			get
			{
				return this._innerDictionary.Values;
			}
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x00114979 File Offset: 0x00112B79
		public void Add(KeyValuePair<Type, IModelBinder> item)
		{
			((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).Add(item);
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x00114987 File Offset: 0x00112B87
		public void Add(Type key, IModelBinder value)
		{
			this._innerDictionary.Add(key, value);
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x00114996 File Offset: 0x00112B96
		public void Clear()
		{
			this._innerDictionary.Clear();
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x001149A3 File Offset: 0x00112BA3
		public bool Contains(KeyValuePair<Type, IModelBinder> item)
		{
			return ((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).Contains(item);
		}

		// Token: 0x06004FB7 RID: 20407 RVA: 0x001149B1 File Offset: 0x00112BB1
		public bool ContainsKey(Type key)
		{
			return this._innerDictionary.ContainsKey(key);
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x001149BF File Offset: 0x00112BBF
		public void CopyTo(KeyValuePair<Type, IModelBinder>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).CopyTo(array, arrayIndex);
		}

		// Token: 0x06004FB9 RID: 20409 RVA: 0x001149CE File Offset: 0x00112BCE
		public IEnumerator<KeyValuePair<Type, IModelBinder>> GetEnumerator()
		{
			return this._innerDictionary.GetEnumerator();
		}

		// Token: 0x06004FBA RID: 20410 RVA: 0x001149E0 File Offset: 0x00112BE0
		public bool Remove(KeyValuePair<Type, IModelBinder> item)
		{
			return ((ICollection<KeyValuePair<Type, IModelBinder>>)this._innerDictionary).Remove(item);
		}

		// Token: 0x06004FBB RID: 20411 RVA: 0x001149EE File Offset: 0x00112BEE
		public bool Remove(Type key)
		{
			return this._innerDictionary.Remove(key);
		}

		// Token: 0x06004FBC RID: 20412 RVA: 0x001149FC File Offset: 0x00112BFC
		public bool TryGetValue(Type key, out IModelBinder value)
		{
			return this._innerDictionary.TryGetValue(key, out value);
		}

		// Token: 0x06004FBD RID: 20413 RVA: 0x00114A0B File Offset: 0x00112C0B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this._innerDictionary).GetEnumerator();
		}

		// Token: 0x04002A8E RID: 10894
		private IModelBinder _defaultBinder;

		// Token: 0x04002A8F RID: 10895
		private readonly Dictionary<Type, IModelBinder> _innerDictionary = new Dictionary<Type, IModelBinder>();

		// Token: 0x04002A90 RID: 10896
		private ModelBinderProviderCollection _modelBinderProviders;
	}
}
