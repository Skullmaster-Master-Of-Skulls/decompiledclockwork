using System;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200017A RID: 378
	internal static class EntityWrapperFactory
	{
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0005F003 File Offset: 0x0005D203
		internal static IEntityWrapper NullWrapper
		{
			get
			{
				return NullEntityWrapper.NullWrapper;
			}
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x0005F00C File Offset: 0x0005D20C
		internal static IEntityWrapper CreateNewWrapper(object entity, EntityKey key)
		{
			if (entity == null)
			{
				return NullEntityWrapper.NullWrapper;
			}
			IEntityWrapper entityWrapper = EntityWrapperFactory._delegateCache.Evaluate(entity.GetType())(entity);
			entityWrapper.RelationshipManager.SetWrappedOwner(entityWrapper, entity);
			if (key != null && entityWrapper.EntityKey == null)
			{
				entityWrapper.EntityKey = key;
			}
			EntityProxyTypeInfo entityProxyTypeInfo;
			if (EntityProxyFactory.TryGetProxyType(entity.GetType(), out entityProxyTypeInfo))
			{
				entityProxyTypeInfo.SetEntityWrapper(entityWrapper);
			}
			return entityWrapper;
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x0005F070 File Offset: 0x0005D270
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static Func<object, IEntityWrapper> CreateWrapperDelegate(Type entityType)
		{
			bool flag = typeof(IEntityWithRelationships).IsAssignableFrom(entityType);
			bool flag2 = typeof(IEntityWithChangeTracker).IsAssignableFrom(entityType);
			bool flag3 = typeof(IEntityWithKey).IsAssignableFrom(entityType);
			bool flag4 = EntityProxyFactory.IsProxyType(entityType);
			MethodInfo methodInfo;
			if (flag && flag2 && flag3 && !flag4)
			{
				methodInfo = typeof(EntityWrapperFactory).GetMethod("CreateWrapperDelegateTypedLightweight", BindingFlags.Static | BindingFlags.NonPublic);
			}
			else if (flag)
			{
				methodInfo = typeof(EntityWrapperFactory).GetMethod("CreateWrapperDelegateTypedWithRelationships", BindingFlags.Static | BindingFlags.NonPublic);
			}
			else
			{
				methodInfo = typeof(EntityWrapperFactory).GetMethod("CreateWrapperDelegateTypedWithoutRelationships", BindingFlags.Static | BindingFlags.NonPublic);
			}
			methodInfo = methodInfo.MakeGenericMethod(new Type[]
			{
				entityType
			});
			return (Func<object, IEntityWrapper>)methodInfo.Invoke(null, new object[0]);
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0005F136 File Offset: 0x0005D336
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedLightweight<TEntity>() where TEntity : IEntityWithRelationships, IEntityWithKey, IEntityWithChangeTracker
		{
			return (object entity) => new LightweightEntityWrapper<TEntity>((TEntity)((object)entity));
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x0005F158 File Offset: 0x0005D358
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedWithRelationships<TEntity>() where TEntity : IEntityWithRelationships
		{
			Func<object, IPropertyAccessorStrategy> propertyAccessorStrategy;
			Func<object, IChangeTrackingStrategy> changeTrackingStrategy;
			Func<object, IEntityKeyStrategy> keyStrategy;
			EntityWrapperFactory.CreateStrategies<TEntity>(out propertyAccessorStrategy, out changeTrackingStrategy, out keyStrategy);
			return (object entity) => new EntityWrapperWithRelationships<TEntity>((TEntity)((object)entity), propertyAccessorStrategy, changeTrackingStrategy, keyStrategy);
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x0005F190 File Offset: 0x0005D390
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedWithoutRelationships<TEntity>()
		{
			Func<object, IPropertyAccessorStrategy> propertyAccessorStrategy;
			Func<object, IChangeTrackingStrategy> changeTrackingStrategy;
			Func<object, IEntityKeyStrategy> keyStrategy;
			EntityWrapperFactory.CreateStrategies<TEntity>(out propertyAccessorStrategy, out changeTrackingStrategy, out keyStrategy);
			return (object entity) => new EntityWrapperWithoutRelationships<TEntity>((TEntity)((object)entity), propertyAccessorStrategy, changeTrackingStrategy, keyStrategy);
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0005F1C8 File Offset: 0x0005D3C8
		private static void CreateStrategies<TEntity>(out Func<object, IPropertyAccessorStrategy> createPropertyAccessorStrategy, out Func<object, IChangeTrackingStrategy> createChangeTrackingStrategy, out Func<object, IEntityKeyStrategy> createKeyStrategy)
		{
			Type typeFromHandle = typeof(TEntity);
			bool flag = typeof(IEntityWithRelationships).IsAssignableFrom(typeFromHandle);
			bool flag2 = typeof(IEntityWithChangeTracker).IsAssignableFrom(typeFromHandle);
			bool flag3 = typeof(IEntityWithKey).IsAssignableFrom(typeFromHandle);
			bool flag4 = EntityProxyFactory.IsProxyType(typeFromHandle);
			if (!flag || flag4)
			{
				createPropertyAccessorStrategy = EntityWrapperFactory.GetPocoPropertyAccessorStrategyFunc();
			}
			else
			{
				createPropertyAccessorStrategy = EntityWrapperFactory.GetNullPropertyAccessorStrategyFunc();
			}
			if (flag2)
			{
				createChangeTrackingStrategy = EntityWrapperFactory.GetEntityWithChangeTrackerStrategyFunc();
			}
			else
			{
				createChangeTrackingStrategy = EntityWrapperFactory.GetSnapshotChangeTrackingStrategyFunc();
			}
			if (flag3)
			{
				createKeyStrategy = EntityWrapperFactory.GetEntityWithKeyStrategyStrategyFunc();
				return;
			}
			createKeyStrategy = EntityWrapperFactory.GetPocoEntityKeyStrategyFunc();
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0005F25C File Offset: 0x0005D45C
		internal static IEntityWrapper WrapEntityUsingContext(object entity, ObjectContext context)
		{
			EntityEntry entityEntry;
			return EntityWrapperFactory.WrapEntityUsingStateManagerGettingEntry(entity, (context == null) ? null : context.ObjectStateManager, out entityEntry);
		}

		// Token: 0x06001B78 RID: 7032 RVA: 0x0005F27D File Offset: 0x0005D47D
		internal static IEntityWrapper WrapEntityUsingContextGettingEntry(object entity, ObjectContext context, out EntityEntry existingEntry)
		{
			return EntityWrapperFactory.WrapEntityUsingStateManagerGettingEntry(entity, (context == null) ? null : context.ObjectStateManager, out existingEntry);
		}

		// Token: 0x06001B79 RID: 7033 RVA: 0x0005F294 File Offset: 0x0005D494
		internal static IEntityWrapper WrapEntityUsingStateManager(object entity, ObjectStateManager stateManager)
		{
			EntityEntry entityEntry;
			return EntityWrapperFactory.WrapEntityUsingStateManagerGettingEntry(entity, stateManager, out entityEntry);
		}

		// Token: 0x06001B7A RID: 7034 RVA: 0x0005F2AC File Offset: 0x0005D4AC
		internal static IEntityWrapper WrapEntityUsingStateManagerGettingEntry(object entity, ObjectStateManager stateManager, out EntityEntry existingEntry)
		{
			IEntityWrapper entityWrapper = null;
			existingEntry = null;
			if (entity == null)
			{
				return NullEntityWrapper.NullWrapper;
			}
			if (stateManager != null)
			{
				existingEntry = stateManager.FindEntityEntry(entity);
				if (existingEntry != null)
				{
					return existingEntry.WrappedEntity;
				}
				if (stateManager.TransactionManager.TrackProcessedEntities && stateManager.TransactionManager.WrappedEntities.TryGetValue(entity, out entityWrapper))
				{
					return entityWrapper;
				}
			}
			IEntityWithRelationships entityWithRelationships = entity as IEntityWithRelationships;
			if (entityWithRelationships == null)
			{
				EntityProxyFactory.TryGetProxyWrapper(entity, out entityWrapper);
				if (entityWrapper == null)
				{
					IEntityWithKey entityWithKey = entity as IEntityWithKey;
					entityWrapper = EntityWrapperFactory.CreateNewWrapper(entity, (entityWithKey == null) ? null : entityWithKey.EntityKey);
				}
				if (stateManager != null && stateManager.TransactionManager.TrackProcessedEntities)
				{
					stateManager.TransactionManager.WrappedEntities.Add(entity, entityWrapper);
				}
				return entityWrapper;
			}
			RelationshipManager relationshipManager = entityWithRelationships.RelationshipManager;
			if (relationshipManager == null)
			{
				throw EntityUtil.UnexpectedNullRelationshipManager();
			}
			IEntityWrapper wrappedOwner = relationshipManager.WrappedOwner;
			if (wrappedOwner.Entity != entity)
			{
				throw EntityUtil.InvalidRelationshipManagerOwner();
			}
			return wrappedOwner;
		}

		// Token: 0x06001B7B RID: 7035 RVA: 0x0005F381 File Offset: 0x0005D581
		internal static void UpdateNoTrackingWrapper(IEntityWrapper wrapper, ObjectContext context, EntitySet entitySet)
		{
			if (wrapper.EntityKey == null)
			{
				wrapper.EntityKey = context.ObjectStateManager.CreateEntityKey(entitySet, wrapper.Entity);
			}
			if (wrapper.Context == null)
			{
				wrapper.AttachContext(context, entitySet, MergeOption.NoTracking);
			}
		}

		// Token: 0x06001B7C RID: 7036 RVA: 0x0005F3BA File Offset: 0x0005D5BA
		internal static Func<object, IPropertyAccessorStrategy> GetPocoPropertyAccessorStrategyFunc()
		{
			return (object entity) => new PocoPropertyAccessorStrategy(entity);
		}

		// Token: 0x06001B7D RID: 7037 RVA: 0x0005F3DB File Offset: 0x0005D5DB
		internal static Func<object, IPropertyAccessorStrategy> GetNullPropertyAccessorStrategyFunc()
		{
			return (object entity) => null;
		}

		// Token: 0x06001B7E RID: 7038 RVA: 0x0005F3FC File Offset: 0x0005D5FC
		internal static Func<object, IChangeTrackingStrategy> GetEntityWithChangeTrackerStrategyFunc()
		{
			return (object entity) => new EntityWithChangeTrackerStrategy((IEntityWithChangeTracker)entity);
		}

		// Token: 0x06001B7F RID: 7039 RVA: 0x0005F41D File Offset: 0x0005D61D
		internal static Func<object, IChangeTrackingStrategy> GetSnapshotChangeTrackingStrategyFunc()
		{
			return (object entity) => SnapshotChangeTrackingStrategy.Instance;
		}

		// Token: 0x06001B80 RID: 7040 RVA: 0x0005F43E File Offset: 0x0005D63E
		internal static Func<object, IEntityKeyStrategy> GetEntityWithKeyStrategyStrategyFunc()
		{
			return (object entity) => new EntityWithKeyStrategy((IEntityWithKey)entity);
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0005F45F File Offset: 0x0005D65F
		internal static Func<object, IEntityKeyStrategy> GetPocoEntityKeyStrategyFunc()
		{
			return (object entity) => new PocoEntityKeyStrategy();
		}

		// Token: 0x04000B7B RID: 2939
		private static readonly Memoizer<Type, Func<object, IEntityWrapper>> _delegateCache = new Memoizer<Type, Func<object, IEntityWrapper>>(new Func<Type, Func<object, IEntityWrapper>>(EntityWrapperFactory.CreateWrapperDelegate), null);
	}
}
