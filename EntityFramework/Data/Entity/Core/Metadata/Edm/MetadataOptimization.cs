using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200002B RID: 43
	internal class MetadataOptimization
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x0000A02A File Offset: 0x0000822A
		internal MetadataOptimization(MetadataWorkspace workspace)
		{
			this._workspace = workspace;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000A04F File Offset: 0x0000824F
		internal IDictionary<Type, EntitySetTypePair> EntitySetMappingCache
		{
			get
			{
				return this._entitySetMappingsCache;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000A070 File Offset: 0x00008270
		private void UpdateEntitySetMappings()
		{
			ObjectItemCollection objectItemCollection = (ObjectItemCollection)this._workspace.GetItemCollection(DataSpace.OSpace);
			ReadOnlyCollection<EntityType> items = this._workspace.GetItems<EntityType>(DataSpace.OSpace);
			Stack<EntityType> stack = new Stack<EntityType>();
			foreach (EntityType objectSpaceType in items)
			{
				stack.Clear();
				EntityType cspaceType = (EntityType)this._workspace.GetEdmSpaceType(objectSpaceType);
				do
				{
					stack.Push(cspaceType);
					cspaceType = (EntityType)cspaceType.BaseType;
				}
				while (cspaceType != null);
				EntitySet entitySet = null;
				while (entitySet == null && stack.Count > 0)
				{
					cspaceType = stack.Pop();
					foreach (EntityContainer entityContainer in this._workspace.GetItems<EntityContainer>(DataSpace.CSpace))
					{
						List<EntitySetBase> list = (from s in entityContainer.BaseEntitySets
						where s.ElementType == cspaceType
						select s).ToList<EntitySetBase>();
						int count = list.Count;
						if (count > 1 || (count == 1 && entitySet != null))
						{
							throw Error.DbContext_MESTNotSupported();
						}
						if (count == 1)
						{
							entitySet = (EntitySet)list[0];
						}
					}
				}
				if (entitySet != null)
				{
					EntityType objectSpaceType2 = (EntityType)this._workspace.GetObjectSpaceType(cspaceType);
					Type clrType = objectItemCollection.GetClrType(objectSpaceType);
					Type clrType2 = objectItemCollection.GetClrType(objectSpaceType2);
					this._entitySetMappingsCache[clrType] = new EntitySetTypePair(entitySet, clrType2);
				}
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000A254 File Offset: 0x00008454
		internal bool TryUpdateEntitySetMappingsForType(Type entityType)
		{
			if (this._entitySetMappingsCache.ContainsKey(entityType))
			{
				return true;
			}
			Type type = entityType;
			do
			{
				this._workspace.LoadFromAssembly(type.Assembly());
				type = type.BaseType();
			}
			while (type != null && type != typeof(object));
			lock (this._entitySetMappingsUpdateLock)
			{
				if (this._entitySetMappingsCache.ContainsKey(entityType))
				{
					return true;
				}
				this.UpdateEntitySetMappings();
			}
			return this._entitySetMappingsCache.ContainsKey(entityType);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000A2FC File Offset: 0x000084FC
		internal AssociationType GetCSpaceAssociationType(AssociationType osAssociationType)
		{
			return this._csAssociationTypes[osAssociationType.Index];
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000A310 File Offset: 0x00008510
		internal AssociationSet FindCSpaceAssociationSet(AssociationType associationType, string endName, EntitySet endEntitySet)
		{
			object[] cspaceAssociationTypeToSetsMap = this.GetCSpaceAssociationTypeToSetsMap();
			int index = associationType.Index;
			object obj = cspaceAssociationTypeToSetsMap[index];
			if (obj == null)
			{
				return null;
			}
			AssociationSet associationSet = obj as AssociationSet;
			if (associationSet == null)
			{
				foreach (AssociationSet associationSet in (AssociationSet[])obj)
				{
					if (associationSet.AssociationSetEnds[endName].EntitySet == endEntitySet)
					{
						return associationSet;
					}
				}
				return null;
			}
			if (associationSet.AssociationSetEnds[endName].EntitySet != endEntitySet)
			{
				return null;
			}
			return associationSet;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000A390 File Offset: 0x00008590
		internal AssociationSet FindCSpaceAssociationSet(AssociationType associationType, string endName, string entitySetName, string entityContainerName, out EntitySet endEntitySet)
		{
			object[] cspaceAssociationTypeToSetsMap = this.GetCSpaceAssociationTypeToSetsMap();
			int index = associationType.Index;
			object obj = cspaceAssociationTypeToSetsMap[index];
			if (obj == null)
			{
				endEntitySet = null;
				return null;
			}
			AssociationSet associationSet = obj as AssociationSet;
			if (associationSet == null)
			{
				foreach (AssociationSet associationSet in (AssociationSet[])obj)
				{
					EntitySet entitySet = associationSet.AssociationSetEnds[endName].EntitySet;
					if (entitySet.Name == entitySetName && entitySet.EntityContainer.Name == entityContainerName)
					{
						endEntitySet = entitySet;
						return associationSet;
					}
				}
				endEntitySet = null;
				return null;
			}
			EntitySet entitySet2 = associationSet.AssociationSetEnds[endName].EntitySet;
			if (entitySet2.Name == entitySetName && entitySet2.EntityContainer.Name == entityContainerName)
			{
				endEntitySet = entitySet2;
				return associationSet;
			}
			endEntitySet = null;
			return null;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000A46C File Offset: 0x0000866C
		internal AssociationType[] GetCSpaceAssociationTypes()
		{
			if (this._csAssociationTypes == null)
			{
				this._csAssociationTypes = MetadataOptimization.IndexCSpaceAssociationTypes(this._workspace.GetItemCollection(DataSpace.CSpace));
			}
			return this._csAssociationTypes;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A49C File Offset: 0x0000869C
		private static AssociationType[] IndexCSpaceAssociationTypes(ItemCollection itemCollection)
		{
			List<AssociationType> list = new List<AssociationType>();
			int num = 0;
			foreach (AssociationType associationType in itemCollection.GetItems<AssociationType>())
			{
				list.Add(associationType);
				associationType.Index = num++;
			}
			return list.ToArray();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A504 File Offset: 0x00008704
		internal object[] GetCSpaceAssociationTypeToSetsMap()
		{
			if (this._csAssociationTypeToSets == null)
			{
				this._csAssociationTypeToSets = MetadataOptimization.MapCSpaceAssociationTypeToSets(this._workspace.GetItemCollection(DataSpace.CSpace), this.GetCSpaceAssociationTypes().Length);
			}
			return this._csAssociationTypeToSets;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A53C File Offset: 0x0000873C
		private static object[] MapCSpaceAssociationTypeToSets(ItemCollection itemCollection, int associationTypeCount)
		{
			object[] array = new object[associationTypeCount];
			foreach (EntityContainer entityContainer in itemCollection.GetItems<EntityContainer>())
			{
				foreach (EntitySetBase entitySetBase in entityContainer.BaseEntitySets)
				{
					AssociationSet associationSet = entitySetBase as AssociationSet;
					if (associationSet != null)
					{
						int index = associationSet.ElementType.Index;
						MetadataOptimization.AddItemAtIndex<AssociationSet>(array, index, associationSet);
					}
				}
			}
			return array;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A5F0 File Offset: 0x000087F0
		internal AssociationType GetOSpaceAssociationType(AssociationType cSpaceAssociationType, Func<AssociationType> initializer)
		{
			AssociationType[] ospaceAssociationTypes = this.GetOSpaceAssociationTypes();
			int index = cSpaceAssociationType.Index;
			Thread.MemoryBarrier();
			AssociationType associationType = ospaceAssociationTypes[index];
			if (associationType == null)
			{
				associationType = initializer();
				associationType.Index = index;
				ospaceAssociationTypes[index] = associationType;
				Thread.MemoryBarrier();
			}
			return associationType;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A633 File Offset: 0x00008833
		internal AssociationType[] GetOSpaceAssociationTypes()
		{
			if (this._osAssociationTypes == null)
			{
				this._osAssociationTypes = new AssociationType[this.GetCSpaceAssociationTypes().Length];
			}
			return this._osAssociationTypes;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A65C File Offset: 0x0000885C
		private static void AddItemAtIndex<T>(object[] array, int index, T newItem) where T : class
		{
			object obj = array[index];
			if (obj == null)
			{
				array[index] = newItem;
				return;
			}
			T t = obj as T;
			if (t != null)
			{
				array[index] = new T[]
				{
					t,
					newItem
				};
				return;
			}
			T[] array2 = (T[])obj;
			int num = array2.Length;
			Array.Resize<T>(ref array2, num + 1);
			array2[num] = newItem;
			array[index] = array2;
		}

		// Token: 0x040000DA RID: 218
		private readonly MetadataWorkspace _workspace;

		// Token: 0x040000DB RID: 219
		private readonly IDictionary<Type, EntitySetTypePair> _entitySetMappingsCache = new Dictionary<Type, EntitySetTypePair>();

		// Token: 0x040000DC RID: 220
		private object _entitySetMappingsUpdateLock = new object();

		// Token: 0x040000DD RID: 221
		private volatile AssociationType[] _csAssociationTypes;

		// Token: 0x040000DE RID: 222
		private volatile AssociationType[] _osAssociationTypes;

		// Token: 0x040000DF RID: 223
		private volatile object[] _csAssociationTypeToSets;
	}
}
