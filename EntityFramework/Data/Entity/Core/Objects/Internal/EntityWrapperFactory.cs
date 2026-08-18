using System;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000585 RID: 1413
	internal class EntityWrapperFactory
	{
		// Token: 0x0600372E RID: 14126 RVA: 0x00105CD8 File Offset: 0x00103ED8
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

		// Token: 0x0600372F RID: 14127 RVA: 0x00105D3C File Offset: 0x00103F3C
		private static Func<object, IEntityWrapper> CreateWrapperDelegate(Type entityType)
		{
			bool flag = typeof(IEntityWithRelationships).IsAssignableFrom(entityType);
			bool flag2 = typeof(IEntityWithChangeTracker).IsAssignableFrom(entityType);
			bool flag3 = typeof(IEntityWithKey).IsAssignableFrom(entityType);
			bool flag4 = EntityProxyFactory.IsProxyType(entityType);
			MethodInfo methodInfo;
			if (flag && flag2 && flag3 && !flag4)
			{
				methodInfo = EntityWrapperFactory.CreateWrapperDelegateTypedLightweightMethod;
			}
			else if (flag)
			{
				methodInfo = EntityWrapperFactory.CreateWrapperDelegateTypedWithRelationshipsMethod;
			}
			else
			{
				methodInfo = EntityWrapperFactory.CreateWrapperDelegateTypedWithoutRelationshipsMethod;
			}
			methodInfo = methodInfo.MakeGenericMethod(new Type[]
			{
				entityType
			});
			return (Func<object, IEntityWrapper>)methodInfo.Invoke(null, new object[0]);
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x00105DF4 File Offset: 0x00103FF4
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedLightweight<TEntity>() where TEntity : class, IEntityWithRelationships, IEntityWithKey, IEntityWithChangeTracker
		{
			bool overridesEquals = typeof(TEntity).OverridesEqualsOrGetHashCode();
			return (object entity) => new LightweightEntityWrapper<TEntity>((TEntity)((object)entity), overridesEquals);
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x00105E58 File Offset: 0x00104058
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedWithRelationships<TEntity>() where TEntity : class, IEntityWithRelationships
		{
			bool overridesEquals = typeof(TEntity).OverridesEqualsOrGetHashCode();
			Func<object, IPropertyAccessorStrategy> propertyAccessorStrategy;
			Func<object, IEntityKeyStrategy> keyStrategy;
			Func<object, IChangeTrackingStrategy> changeTrackingStrategy;
			EntityWrapperFactory.CreateStrategies<TEntity>(out propertyAccessorStrategy, out changeTrackingStrategy, out keyStrategy);
			return (object entity) => new EntityWrapperWithRelationships<TEntity>((TEntity)((object)entity), propertyAccessorStrategy, changeTrackingStrategy, keyStrategy, overridesEquals);
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x00105ED0 File Offset: 0x001040D0
		private static Func<object, IEntityWrapper> CreateWrapperDelegateTypedWithoutRelationships<TEntity>() where TEntity : class
		{
			bool overridesEquals = typeof(TEntity).OverridesEqualsOrGetHashCode();
			Func<object, IPropertyAccessorStrategy> propertyAccessorStrategy;
			Func<object, IEntityKeyStrategy> keyStrategy;
			Func<object, IChangeTrackingStrategy> changeTrackingStrategy;
			EntityWrapperFactory.CreateStrategies<TEntity>(out propertyAccessorStrategy, out changeTrackingStrategy, out keyStrategy);
			return (object entity) => new EntityWrapperWithoutRelationships<TEntity>((TEntity)((object)entity), propertyAccessorStrategy, changeTrackingStrategy, keyStrategy, overridesEquals);
		}

		// Token: 0x06003733 RID: 14131 RVA: 0x00105F1C File Offset: 0x0010411C
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

		// Token: 0x06003734 RID: 14132 RVA: 0x00105FAC File Offset: 0x001041AC
		internal IEntityWrapper WrapEntityUsingContext(object entity, ObjectContext context)
		{
			EntityEntry entityEntry;
			return this.WrapEntityUsingStateManagerGettingEntry(entity, (context == null) ? null : context.ObjectStateManager, out entityEntry);
		}

		// Token: 0x06003735 RID: 14133 RVA: 0x00105FCE File Offset: 0x001041CE
		internal IEntityWrapper WrapEntityUsingContextGettingEntry(object entity, ObjectContext context, out EntityEntry existingEntry)
		{
			return this.WrapEntityUsingStateManagerGettingEntry(entity, (context == null) ? null : context.ObjectStateManager, out existingEntry);
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x00105FE4 File Offset: 0x001041E4
		internal IEntityWrapper WrapEntityUsingStateManager(object entity, ObjectStateManager stateManager)
		{
			EntityEntry entityEntry;
			return this.WrapEntityUsingStateManagerGettingEntry(entity, stateManager, out entityEntry);
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x00105FFC File Offset: 0x001041FC
		internal virtual IEntityWrapper WrapEntityUsingStateManagerGettingEntry(object entity, ObjectStateManager stateManager, out EntityEntry existingEntry)
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
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNull);
			}
			IEntityWrapper wrappedOwner = relationshipManager.WrappedOwner;
			if (!object.ReferenceEquals(wrappedOwner.Entity, entity))
			{
				throw new InvalidOperationException(Strings.RelationshipManager_InvalidRelationshipManagerOwner);
			}
			return wrappedOwner;
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x001060E0 File Offset: 0x001042E0
		internal virtual void UpdateNoTrackingWrapper(IEntityWrapper wrapper, ObjectContext context, EntitySet entitySet)
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

		// Token: 0x06003739 RID: 14137 RVA: 0x00106121 File Offset: 0x00104321
		internal static Func<object, IPropertyAccessorStrategy> GetPocoPropertyAccessorStrategyFunc()
		{
			return (object entity) => new PocoPropertyAccessorStrategy(entity);
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x00106143 File Offset: 0x00104343
		internal static Func<object, IPropertyAccessorStrategy> GetNullPropertyAccessorStrategyFunc()
		{
			return (object entity) => null;
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x0010616F File Offset: 0x0010436F
		internal static Func<object, IChangeTrackingStrategy> GetEntityWithChangeTrackerStrategyFunc()
		{
			return (object entity) => new EntityWithChangeTrackerStrategy((IEntityWithChangeTracker)entity);
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x00106195 File Offset: 0x00104395
		internal static Func<object, IChangeTrackingStrategy> GetSnapshotChangeTrackingStrategyFunc()
		{
			return (object entity) => SnapshotChangeTrackingStrategy.Instance;
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x001061C1 File Offset: 0x001043C1
		internal static Func<object, IEntityKeyStrategy> GetEntityWithKeyStrategyStrategyFunc()
		{
			return (object entity) => new EntityWithKeyStrategy((IEntityWithKey)entity);
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x001061E7 File Offset: 0x001043E7
		internal static Func<object, IEntityKeyStrategy> GetPocoEntityKeyStrategyFunc()
		{
			return (object entity) => new PocoEntityKeyStrategy();
		}

		// Token: 0x04001535 RID: 5429
		private static readonly Memoizer<Type, Func<object, IEntityWrapper>> _delegateCache = new Memoizer<Type, Func<object, IEntityWrapper>>(new Func<Type, Func<object, IEntityWrapper>>(EntityWrapperFactory.CreateWrapperDelegate), null);

		// Token: 0x04001536 RID: 5430
		internal static readonly MethodInfo CreateWrapperDelegateTypedLightweightMethod = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("CreateWrapperDelegateTypedLightweight");

		// Token: 0x04001537 RID: 5431
		internal static readonly MethodInfo CreateWrapperDelegateTypedWithRelationshipsMethod = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("CreateWrapperDelegateTypedWithRelationships");

		// Token: 0x04001538 RID: 5432
		internal static readonly MethodInfo CreateWrapperDelegateTypedWithoutRelationshipsMethod = typeof(EntityWrapperFactory).GetOnlyDeclaredMethod("CreateWrapperDelegateTypedWithoutRelationships");
	}
}
