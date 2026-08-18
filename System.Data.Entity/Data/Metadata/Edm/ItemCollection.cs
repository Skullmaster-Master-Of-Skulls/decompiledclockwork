using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001DE RID: 478
	[CLSCompliant(false)]
	public abstract class ItemCollection : ReadOnlyMetadataCollection<GlobalItem>
	{
		// Token: 0x06002027 RID: 8231 RVA: 0x0007032B File Offset: 0x0006E52B
		internal ItemCollection(DataSpace dataspace) : base(new MetadataCollection<GlobalItem>())
		{
			this._space = dataspace;
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x0007033F File Offset: 0x0006E53F
		public DataSpace DataSpace
		{
			get
			{
				return this._space;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x00070348 File Offset: 0x0006E548
		internal Dictionary<string, ReadOnlyCollection<EdmFunction>> FunctionLookUpTable
		{
			get
			{
				if (this._functionLookUpTable == null)
				{
					Dictionary<string, ReadOnlyCollection<EdmFunction>> value = ItemCollection.PopulateFunctionLookUpTable(this);
					Interlocked.CompareExchange<Dictionary<string, ReadOnlyCollection<EdmFunction>>>(ref this._functionLookUpTable, value, null);
				}
				return this._functionLookUpTable;
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00070378 File Offset: 0x0006E578
		internal void AddInternal(GlobalItem item)
		{
			base.Source.Add(item);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00070386 File Offset: 0x0006E586
		internal bool AtomicAddRange(List<GlobalItem> items)
		{
			return base.Source.AtomicAddRange(items);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00070399 File Offset: 0x0006E599
		public T GetItem<T>(string identity) where T : GlobalItem
		{
			return this.GetItem<T>(identity, false);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000703A3 File Offset: 0x0006E5A3
		public bool TryGetItem<T>(string identity, out T item) where T : GlobalItem
		{
			return this.TryGetItem<T>(identity, false, out item);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000703B0 File Offset: 0x0006E5B0
		public bool TryGetItem<T>(string identity, bool ignoreCase, out T item) where T : GlobalItem
		{
			GlobalItem globalItem = null;
			this.TryGetValue(identity, ignoreCase, out globalItem);
			item = (globalItem as T);
			return item != null;
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000703EC File Offset: 0x0006E5EC
		public T GetItem<T>(string identity, bool ignoreCase) where T : GlobalItem
		{
			T result;
			if (this.TryGetItem<T>(identity, ignoreCase, out result))
			{
				return result;
			}
			throw EntityUtil.ItemInvalidIdentity(identity, "identity");
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00070414 File Offset: 0x0006E614
		public virtual ReadOnlyCollection<T> GetItems<T>() where T : GlobalItem
		{
			Memoizer<Type, ICollection> itemsCache = this._itemsCache;
			if (this._itemsCache == null || this._itemCount != base.Count)
			{
				Memoizer<Type, ICollection> value = new Memoizer<Type, ICollection>(new Func<Type, ICollection>(this.InternalGetItems), null);
				Interlocked.CompareExchange<Memoizer<Type, ICollection>>(ref this._itemsCache, value, itemsCache);
				this._itemCount = base.Count;
			}
			ICollection collection = this._itemsCache.Evaluate(typeof(T));
			return collection as ReadOnlyCollection<T>;
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x0007048C File Offset: 0x0006E68C
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal ICollection InternalGetItems(Type type)
		{
			MethodInfo method = typeof(ItemCollection).GetMethod("GenericGetItems", BindingFlags.Static | BindingFlags.NonPublic);
			MethodInfo methodInfo = method.MakeGenericMethod(new Type[]
			{
				type
			});
			return methodInfo.Invoke(null, new object[]
			{
				this
			}) as ICollection;
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000704D8 File Offset: 0x0006E6D8
		private static ReadOnlyCollection<TItem> GenericGetItems<TItem>(ItemCollection collection) where TItem : GlobalItem
		{
			List<TItem> list = new List<TItem>();
			foreach (GlobalItem globalItem in collection)
			{
				TItem titem = globalItem as TItem;
				if (titem != null)
				{
					list.Add(titem);
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00070548 File Offset: 0x0006E748
		public EdmType GetType(string name, string namespaceName)
		{
			return this.GetType(name, namespaceName, false);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00070553 File Offset: 0x0006E753
		public bool TryGetType(string name, string namespaceName, out EdmType type)
		{
			return this.TryGetType(name, namespaceName, false, out type);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x0007055F File Offset: 0x0006E75F
		public EdmType GetType(string name, string namespaceName, bool ignoreCase)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			EntityUtil.GenericCheckArgumentNull<string>(namespaceName, "namespaceName");
			return this.GetItem<EdmType>(EdmType.CreateEdmTypeIdentity(namespaceName, name), ignoreCase);
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00070588 File Offset: 0x0006E788
		public bool TryGetType(string name, string namespaceName, bool ignoreCase, out EdmType type)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			EntityUtil.GenericCheckArgumentNull<string>(namespaceName, "namespaceName");
			GlobalItem globalItem = null;
			this.TryGetValue(EdmType.CreateEdmTypeIdentity(namespaceName, name), ignoreCase, out globalItem);
			type = (globalItem as EdmType);
			return type != null;
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000705CF File Offset: 0x0006E7CF
		public ReadOnlyCollection<EdmFunction> GetFunctions(string functionName)
		{
			return this.GetFunctions(functionName, false);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000705D9 File Offset: 0x0006E7D9
		public ReadOnlyCollection<EdmFunction> GetFunctions(string functionName, bool ignoreCase)
		{
			return ItemCollection.GetFunctions(this.FunctionLookUpTable, functionName, ignoreCase);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000705E8 File Offset: 0x0006E7E8
		protected static ReadOnlyCollection<EdmFunction> GetFunctions(Dictionary<string, ReadOnlyCollection<EdmFunction>> functionCollection, string functionName, bool ignoreCase)
		{
			ReadOnlyCollection<EdmFunction> readOnlyCollection;
			if (!functionCollection.TryGetValue(functionName, out readOnlyCollection))
			{
				return Helper.EmptyEdmFunctionReadOnlyCollection;
			}
			if (ignoreCase)
			{
				return readOnlyCollection;
			}
			return ItemCollection.GetCaseSensitiveFunctions(readOnlyCollection, functionName);
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00070614 File Offset: 0x0006E814
		internal static ReadOnlyCollection<EdmFunction> GetCaseSensitiveFunctions(ReadOnlyCollection<EdmFunction> functionOverloads, string functionName)
		{
			List<EdmFunction> list = new List<EdmFunction>(functionOverloads.Count);
			for (int i = 0; i < functionOverloads.Count; i++)
			{
				if (functionOverloads[i].FullName == functionName)
				{
					list.Add(functionOverloads[i]);
				}
			}
			if (list.Count != functionOverloads.Count)
			{
				functionOverloads = list.AsReadOnly();
			}
			return functionOverloads;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00070678 File Offset: 0x0006E878
		internal bool TryGetFunction(string functionName, TypeUsage[] parameterTypes, bool ignoreCase, out EdmFunction function)
		{
			EntityUtil.GenericCheckArgumentNull<string>(functionName, "functionName");
			EntityUtil.GenericCheckArgumentNull<TypeUsage[]>(parameterTypes, "parameterTypes");
			string identity = EdmFunction.BuildIdentity(functionName, parameterTypes);
			GlobalItem globalItem = null;
			function = null;
			if (this.TryGetValue(identity, ignoreCase, out globalItem) && Helper.IsEdmFunction(globalItem))
			{
				function = (EdmFunction)globalItem;
				return true;
			}
			return false;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000706CB File Offset: 0x0006E8CB
		public EntityContainer GetEntityContainer(string name)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			return this.GetEntityContainer(name, false);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000706E1 File Offset: 0x0006E8E1
		public bool TryGetEntityContainer(string name, out EntityContainer entityContainer)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			return this.TryGetEntityContainer(name, false, out entityContainer);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000706F8 File Offset: 0x0006E8F8
		public EntityContainer GetEntityContainer(string name, bool ignoreCase)
		{
			EntityContainer entityContainer = this.GetValue(name, ignoreCase) as EntityContainer;
			if (entityContainer != null)
			{
				return entityContainer;
			}
			throw EntityUtil.ItemInvalidIdentity(name, "name");
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00070724 File Offset: 0x0006E924
		public bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			EntityUtil.GenericCheckArgumentNull<string>(name, "name");
			GlobalItem globalItem = null;
			if (this.TryGetValue(name, ignoreCase, out globalItem) && Helper.IsEntityContainer(globalItem))
			{
				entityContainer = (EntityContainer)globalItem;
				return true;
			}
			entityContainer = null;
			return false;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0003BCEB File Offset: 0x00039EEB
		internal virtual PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0005AF88 File Offset: 0x00059188
		internal virtual bool MetadataEquals(ItemCollection other)
		{
			return this == other;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00070764 File Offset: 0x0006E964
		private static Dictionary<string, ReadOnlyCollection<EdmFunction>> PopulateFunctionLookUpTable(ItemCollection itemCollection)
		{
			Dictionary<string, List<EdmFunction>> dictionary = new Dictionary<string, List<EdmFunction>>(StringComparer.OrdinalIgnoreCase);
			foreach (EdmFunction edmFunction in itemCollection.GetItems<EdmFunction>())
			{
				List<EdmFunction> list;
				if (!dictionary.TryGetValue(edmFunction.FullName, out list))
				{
					list = new List<EdmFunction>();
					dictionary[edmFunction.FullName] = list;
				}
				list.Add(edmFunction);
			}
			Dictionary<string, ReadOnlyCollection<EdmFunction>> dictionary2 = new Dictionary<string, ReadOnlyCollection<EdmFunction>>(StringComparer.OrdinalIgnoreCase);
			foreach (List<EdmFunction> list2 in dictionary.Values)
			{
				dictionary2.Add(list2[0].FullName, new ReadOnlyCollection<EdmFunction>(list2.ToArray()));
			}
			return dictionary2;
		}

		// Token: 0x04000E3C RID: 3644
		private readonly DataSpace _space;

		// Token: 0x04000E3D RID: 3645
		private Dictionary<string, ReadOnlyCollection<EdmFunction>> _functionLookUpTable;

		// Token: 0x04000E3E RID: 3646
		private Memoizer<Type, ICollection> _itemsCache;

		// Token: 0x04000E3F RID: 3647
		private int _itemCount;
	}
}
