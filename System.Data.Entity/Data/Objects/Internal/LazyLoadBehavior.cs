using System;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Reflection;

namespace System.Data.Objects.Internal
{
	// Token: 0x0200017C RID: 380
	internal sealed class LazyLoadBehavior
	{
		// Token: 0x06001BA0 RID: 7072 RVA: 0x0005F49C File Offset: 0x0005D69C
		internal static Func<TProxy, TItem, bool> GetInterceptorDelegate<TProxy, TItem>(EdmMember member, Func<object, object> getEntityWrapperDelegate) where TProxy : class where TItem : class
		{
			Func<TProxy, TItem, bool> result = (TProxy proxy, TItem item) => true;
			if (member.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty)
			{
				NavigationProperty navProperty = (NavigationProperty)member;
				RelationshipMultiplicity relationshipMultiplicity = navProperty.ToEndMember.RelationshipMultiplicity;
				if (relationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					result = ((TProxy proxy, TItem item) => LazyLoadBehavior.LoadProperty<TItem>(item, navProperty.RelationshipType.Identity, navProperty.ToEndMember.Identity, false, getEntityWrapperDelegate(proxy)));
				}
				else
				{
					result = ((TProxy proxy, TItem item) => LazyLoadBehavior.LoadProperty<TItem>(item, navProperty.RelationshipType.Identity, navProperty.ToEndMember.Identity, true, getEntityWrapperDelegate(proxy)));
				}
			}
			return result;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0005F520 File Offset: 0x0005D720
		internal static bool IsLazyLoadCandidate(EntityType ospaceEntityType, EdmMember member)
		{
			bool result = false;
			if (member.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty)
			{
				NavigationProperty navigationProperty = (NavigationProperty)member;
				RelationshipMultiplicity relationshipMultiplicity = navigationProperty.ToEndMember.RelationshipMultiplicity;
				PropertyInfo topProperty = EntityUtil.GetTopProperty(ospaceEntityType.ClrType, member.Name);
				Type propertyType = topProperty.PropertyType;
				if (relationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					Type type;
					result = EntityUtil.TryGetICollectionElementType(propertyType, out type);
				}
				else if (relationshipMultiplicity == RelationshipMultiplicity.One || relationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0005F580 File Offset: 0x0005D780
		private static bool LoadProperty<TItem>(TItem propertyValue, string relationshipName, string targetRoleName, bool mustBeNull, object wrapperObject) where TItem : class
		{
			IEntityWrapper entityWrapper = (IEntityWrapper)wrapperObject;
			if (entityWrapper != null && entityWrapper.Context != null)
			{
				RelationshipManager relationshipManager = entityWrapper.RelationshipManager;
				if (relationshipManager != null && (!mustBeNull || propertyValue == null))
				{
					RelatedEnd relatedEndInternal = relationshipManager.GetRelatedEndInternal(relationshipName, targetRoleName);
					relatedEndInternal.DeferredLoad();
				}
			}
			return propertyValue != null;
		}
	}
}
