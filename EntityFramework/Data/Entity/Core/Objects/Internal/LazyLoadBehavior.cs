using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200058B RID: 1419
	internal sealed class LazyLoadBehavior
	{
		// Token: 0x0600376A RID: 14186 RVA: 0x00107670 File Offset: 0x00105870
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

		// Token: 0x0600376B RID: 14187 RVA: 0x00107704 File Offset: 0x00105904
		internal static bool IsLazyLoadCandidate(EntityType ospaceEntityType, EdmMember member)
		{
			bool result = false;
			if (member.BuiltInTypeKind == BuiltInTypeKind.NavigationProperty)
			{
				NavigationProperty navigationProperty = (NavigationProperty)member;
				RelationshipMultiplicity relationshipMultiplicity = navigationProperty.ToEndMember.RelationshipMultiplicity;
				PropertyInfo topProperty = ospaceEntityType.ClrType.GetTopProperty(member.Name);
				Type propertyType = topProperty.PropertyType;
				if (relationshipMultiplicity == RelationshipMultiplicity.Many)
				{
					result = (propertyType.TryGetElementType(typeof(ICollection<>)) != null);
				}
				else if (relationshipMultiplicity == RelationshipMultiplicity.One || relationshipMultiplicity == RelationshipMultiplicity.ZeroOrOne)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600376C RID: 14188 RVA: 0x00107774 File Offset: 0x00105974
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
