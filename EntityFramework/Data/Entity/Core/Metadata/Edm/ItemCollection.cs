using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200020D RID: 525
	public abstract class ItemCollection : ReadOnlyMetadataCollection<GlobalItem>
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x0004FC29 File Offset: 0x0004DE29
		internal ItemCollection()
		{
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004FC31 File Offset: 0x0004DE31
		internal ItemCollection(DataSpace dataspace) : base(new MetadataCollection<GlobalItem>())
		{
			this._space = dataspace;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0004FC45 File Offset: 0x0004DE45
		public DataSpace DataSpace
		{
			get
			{
				return this._space;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x0004FC50 File Offset: 0x0004DE50
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

		// Token: 0x06001329 RID: 4905 RVA: 0x0004FC80 File Offset: 0x0004DE80
		internal void AddInternal(GlobalItem item)
		{
			base.Source.Add(item);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004FC8E File Offset: 0x0004DE8E
		internal void AddRange(List<GlobalItem> items)
		{
			base.Source.AddRange(items);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004FC9C File Offset: 0x0004DE9C
		public T GetItem<T>(string identity) where T : GlobalItem
		{
			return this.GetItem<T>(identity, false);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004FCA6 File Offset: 0x0004DEA6
		public bool TryGetItem<T>(string identity, out T item) where T : GlobalItem
		{
			return this.TryGetItem<T>(identity, false, out item);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004FCB4 File Offset: 0x0004DEB4
		public bool TryGetItem<T>(string identity, bool ignoreCase, out T item) where T : GlobalItem
		{
			GlobalItem globalItem = null;
			this.TryGetValue(identity, ignoreCase, out globalItem);
			item = (globalItem as T);
			return item != null;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004FCF0 File Offset: 0x0004DEF0
		public T GetItem<T>(string identity, bool ignoreCase) where T : GlobalItem
		{
			T result;
			if (this.TryGetItem<T>(identity, ignoreCase, out result))
			{
				return result;
			}
			throw new ArgumentException(Strings.ItemInvalidIdentity(identity), "identity");
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004FD1C File Offset: 0x0004DF1C
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

		// Token: 0x06001330 RID: 4912 RVA: 0x0004FD94 File Offset: 0x0004DF94
		internal ICollection InternalGetItems(Type type)
		{
			MethodInfo onlyDeclaredMethod = typeof(ItemCollection).GetOnlyDeclaredMethod("GenericGetItems");
			MethodInfo methodInfo = onlyDeclaredMethod.MakeGenericMethod(new Type[]
			{
				type
			});
			return methodInfo.Invoke(null, new object[]
			{
				this
			}) as ICollection;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004FDE4 File Offset: 0x0004DFE4
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
			return new ReadOnlyCollection<TItem>(list);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0004FE54 File Offset: 0x0004E054
		public EdmType GetType(string name, string namespaceName)
		{
			return this.GetType(name, namespaceName, false);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0004FE5F File Offset: 0x0004E05F
		public bool TryGetType(string name, string namespaceName, out EdmType type)
		{
			return this.TryGetType(name, namespaceName, false, out type);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0004FE6B File Offset: 0x0004E06B
		public EdmType GetType(string name, string namespaceName, bool ignoreCase)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<string>(namespaceName, "namespaceName");
			return this.GetItem<EdmType>(EdmType.CreateEdmTypeIdentity(namespaceName, name), ignoreCase);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004FE94 File Offset: 0x0004E094
		public bool TryGetType(string name, string namespaceName, bool ignoreCase, out EdmType type)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<string>(namespaceName, "namespaceName");
			GlobalItem globalItem = null;
			this.TryGetValue(EdmType.CreateEdmTypeIdentity(namespaceName, name), ignoreCase, out globalItem);
			type = (globalItem as EdmType);
			return type != null;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004FEDE File Offset: 0x0004E0DE
		public ReadOnlyCollection<EdmFunction> GetFunctions(string functionName)
		{
			return this.GetFunctions(functionName, false);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0004FEE8 File Offset: 0x0004E0E8
		public ReadOnlyCollection<EdmFunction> GetFunctions(string functionName, bool ignoreCase)
		{
			return ItemCollection.GetFunctions(this.FunctionLookUpTable, functionName, ignoreCase);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0004FEF8 File Offset: 0x0004E0F8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
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

		// Token: 0x06001339 RID: 4921 RVA: 0x0004FF24 File Offset: 0x0004E124
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
				functionOverloads = new ReadOnlyCollection<EdmFunction>(list);
			}
			return functionOverloads;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004FF88 File Offset: 0x0004E188
		internal bool TryGetFunction(string functionName, TypeUsage[] parameterTypes, bool ignoreCase, out EdmFunction function)
		{
			Check.NotNull<string>(functionName, "functionName");
			Check.NotNull<TypeUsage[]>(parameterTypes, "parameterTypes");
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

		// Token: 0x0600133B RID: 4923 RVA: 0x0004FFDB File Offset: 0x0004E1DB
		public EntityContainer GetEntityContainer(string name)
		{
			Check.NotNull<string>(name, "name");
			return this.GetEntityContainer(name, false);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004FFF1 File Offset: 0x0004E1F1
		public bool TryGetEntityContainer(string name, out EntityContainer entityContainer)
		{
			Check.NotNull<string>(name, "name");
			return this.TryGetEntityContainer(name, false, out entityContainer);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00050008 File Offset: 0x0004E208
		public EntityContainer GetEntityContainer(string name, bool ignoreCase)
		{
			EntityContainer entityContainer = this.GetValue(name, ignoreCase) as EntityContainer;
			if (entityContainer != null)
			{
				return entityContainer;
			}
			throw new ArgumentException(Strings.ItemInvalidIdentity(name), "name");
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00050038 File Offset: 0x0004E238
		public bool TryGetEntityContainer(string name, bool ignoreCase, out EntityContainer entityContainer)
		{
			Check.NotNull<string>(name, "name");
			GlobalItem globalItem = null;
			if (this.TryGetValue(name, ignoreCase, out globalItem) && Helper.IsEntityContainer(globalItem))
			{
				entityContainer = (EntityContainer)globalItem;
				return true;
			}
			entityContainer = null;
			return false;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00050075 File Offset: 0x0004E275
		internal virtual PrimitiveType GetMappedPrimitiveType(PrimitiveTypeKind primitiveTypeKind)
		{
			throw Error.NotSupported();
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0005007C File Offset: 0x0004E27C
		internal virtual bool MetadataEquals(ItemCollection other)
		{
			return object.ReferenceEquals(this, other);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00050088 File Offset: 0x0004E288
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

		// Token: 0x04000598 RID: 1432
		private readonly DataSpace _space;

		// Token: 0x04000599 RID: 1433
		private Dictionary<string, ReadOnlyCollection<EdmFunction>> _functionLookUpTable;

		// Token: 0x0400059A RID: 1434
		private Memoizer<Type, ICollection> _itemsCache;

		// Token: 0x0400059B RID: 1435
		private int _itemCount;
	}
}
