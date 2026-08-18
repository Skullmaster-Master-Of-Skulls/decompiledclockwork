using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;
using System.Linq;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x0200025F RID: 607
	internal class DiscriminatorMap
	{
		// Token: 0x06002590 RID: 9616 RVA: 0x0008C204 File Offset: 0x0008A404
		private DiscriminatorMap(DbPropertyExpression discriminator, List<KeyValuePair<object, EntityType>> typeMap, Dictionary<EdmProperty, DbExpression> propertyMap, Dictionary<RelProperty, DbExpression> relPropertyMap, EntitySet entitySet)
		{
			this.Discriminator = discriminator;
			this.TypeMap = typeMap.AsReadOnly();
			this.PropertyMap = propertyMap.ToList<KeyValuePair<EdmProperty, DbExpression>>().AsReadOnly();
			this.RelPropertyMap = relPropertyMap.ToList<KeyValuePair<RelProperty, DbExpression>>().AsReadOnly();
			this.EntitySet = entitySet;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0008C258 File Offset: 0x0008A458
		internal static bool TryCreateDiscriminatorMap(EntitySet entitySet, DbExpression queryView, out DiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			if (queryView.ExpressionKind != DbExpressionKind.Project)
			{
				return false;
			}
			DbProjectExpression dbProjectExpression = (DbProjectExpression)queryView;
			if (dbProjectExpression.Projection.ExpressionKind != DbExpressionKind.Case)
			{
				return false;
			}
			DbCaseExpression dbCaseExpression = (DbCaseExpression)dbProjectExpression.Projection;
			if (dbProjectExpression.Projection.ResultType.EdmType.BuiltInTypeKind != BuiltInTypeKind.EntityType)
			{
				return false;
			}
			if (dbProjectExpression.Input.Expression.ExpressionKind != DbExpressionKind.Filter)
			{
				return false;
			}
			DbFilterExpression filter = (DbFilterExpression)dbProjectExpression.Input.Expression;
			HashSet<object> discriminatorDomain = new HashSet<object>();
			if (!ViewSimplifier.TryMatchDiscriminatorPredicate(filter, delegate(DbComparisonExpression equalsExp, object discriminatorValue)
			{
				discriminatorDomain.Add(discriminatorValue);
			}))
			{
				return false;
			}
			List<KeyValuePair<object, EntityType>> list = new List<KeyValuePair<object, EntityType>>();
			Dictionary<EdmProperty, DbExpression> propertyMap = new Dictionary<EdmProperty, DbExpression>();
			Dictionary<RelProperty, DbExpression> relPropertyMap = new Dictionary<RelProperty, DbExpression>();
			Dictionary<EntityType, List<RelProperty>> typeToRelPropertyMap = new Dictionary<EntityType, List<RelProperty>>();
			DbPropertyExpression discriminator = null;
			EdmProperty edmProperty = null;
			for (int i = 0; i < dbCaseExpression.When.Count; i++)
			{
				DbExpression expression = dbCaseExpression.When[i];
				DbExpression then = dbCaseExpression.Then[i];
				string variableName = dbProjectExpression.Input.VariableName;
				DbPropertyExpression dbPropertyExpression;
				object obj;
				if (!ViewSimplifier.TryMatchPropertyEqualsValue(expression, variableName, out dbPropertyExpression, out obj))
				{
					return false;
				}
				if (edmProperty == null)
				{
					edmProperty = (EdmProperty)dbPropertyExpression.Property;
				}
				else if (edmProperty != dbPropertyExpression.Property)
				{
					return false;
				}
				discriminator = dbPropertyExpression;
				EntityType value;
				if (!DiscriminatorMap.TryMatchEntityTypeConstructor(then, propertyMap, relPropertyMap, typeToRelPropertyMap, out value))
				{
					return false;
				}
				list.Add(new KeyValuePair<object, EntityType>(obj, value));
				discriminatorDomain.Remove(obj);
			}
			if (1 != discriminatorDomain.Count)
			{
				return false;
			}
			EntityType value2;
			if (dbCaseExpression.Else == null || !DiscriminatorMap.TryMatchEntityTypeConstructor(dbCaseExpression.Else, propertyMap, relPropertyMap, typeToRelPropertyMap, out value2))
			{
				return false;
			}
			list.Add(new KeyValuePair<object, EntityType>(discriminatorDomain.Single<object>(), value2));
			if (!DiscriminatorMap.CheckForMissingRelProperties(relPropertyMap, typeToRelPropertyMap))
			{
				return false;
			}
			IEnumerable<object> source = from map in list
			select map.Key;
			int num = source.Distinct(TrailingSpaceComparer.Instance).Count<object>();
			int count = list.Count;
			if (num != count)
			{
				return false;
			}
			discriminatorMap = new DiscriminatorMap(discriminator, list, propertyMap, relPropertyMap, entitySet);
			return true;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0008C488 File Offset: 0x0008A688
		private static bool CheckForMissingRelProperties(Dictionary<RelProperty, DbExpression> relPropertyMap, Dictionary<EntityType, List<RelProperty>> typeToRelPropertyMap)
		{
			foreach (RelProperty relProperty in relPropertyMap.Keys)
			{
				foreach (KeyValuePair<EntityType, List<RelProperty>> keyValuePair in typeToRelPropertyMap)
				{
					if (keyValuePair.Key.IsSubtypeOf(relProperty.FromEnd.TypeUsage.EdmType) && !keyValuePair.Value.Contains(relProperty))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0008C540 File Offset: 0x0008A740
		private static bool TryMatchEntityTypeConstructor(DbExpression then, Dictionary<EdmProperty, DbExpression> propertyMap, Dictionary<RelProperty, DbExpression> relPropertyMap, Dictionary<EntityType, List<RelProperty>> typeToRelPropertyMap, out EntityType entityType)
		{
			if (then.ExpressionKind != DbExpressionKind.NewInstance)
			{
				entityType = null;
				return false;
			}
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)then;
			entityType = (EntityType)dbNewInstanceExpression.ResultType.EdmType;
			for (int i = 0; i < entityType.Properties.Count; i++)
			{
				EdmProperty key = entityType.Properties[i];
				DbExpression dbExpression = dbNewInstanceExpression.Arguments[i];
				DbExpression y;
				if (propertyMap.TryGetValue(key, out y))
				{
					if (!DiscriminatorMap.ExpressionsCompatible(dbExpression, y))
					{
						return false;
					}
				}
				else
				{
					propertyMap.Add(key, dbExpression);
				}
			}
			if (dbNewInstanceExpression.HasRelatedEntityReferences)
			{
				List<RelProperty> list;
				if (!typeToRelPropertyMap.TryGetValue(entityType, out list))
				{
					list = new List<RelProperty>();
					typeToRelPropertyMap[entityType] = list;
				}
				foreach (DbRelatedEntityRef dbRelatedEntityRef in dbNewInstanceExpression.RelatedEntityReferences)
				{
					RelProperty relProperty = new RelProperty((RelationshipType)dbRelatedEntityRef.TargetEnd.DeclaringType, dbRelatedEntityRef.SourceEnd, dbRelatedEntityRef.TargetEnd);
					DbExpression targetEntityReference = dbRelatedEntityRef.TargetEntityReference;
					DbExpression y2;
					if (relPropertyMap.TryGetValue(relProperty, out y2))
					{
						if (!DiscriminatorMap.ExpressionsCompatible(targetEntityReference, y2))
						{
							return false;
						}
					}
					else
					{
						relPropertyMap.Add(relProperty, targetEntityReference);
					}
					list.Add(relProperty);
				}
				return true;
			}
			return true;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0008C698 File Offset: 0x0008A898
		private static bool ExpressionsCompatible(DbExpression x, DbExpression y)
		{
			if (x.ExpressionKind != y.ExpressionKind)
			{
				return false;
			}
			DbExpressionKind expressionKind = x.ExpressionKind;
			if (expressionKind <= DbExpressionKind.Property)
			{
				if (expressionKind != DbExpressionKind.NewInstance)
				{
					if (expressionKind == DbExpressionKind.Property)
					{
						DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)x;
						DbPropertyExpression dbPropertyExpression2 = (DbPropertyExpression)y;
						return dbPropertyExpression.Property == dbPropertyExpression2.Property && DiscriminatorMap.ExpressionsCompatible(dbPropertyExpression.Instance, dbPropertyExpression2.Instance);
					}
				}
				else
				{
					DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)x;
					DbNewInstanceExpression dbNewInstanceExpression2 = (DbNewInstanceExpression)y;
					if (!dbNewInstanceExpression.ResultType.EdmType.EdmEquals(dbNewInstanceExpression2.ResultType.EdmType))
					{
						return false;
					}
					for (int i = 0; i < dbNewInstanceExpression.Arguments.Count; i++)
					{
						if (!DiscriminatorMap.ExpressionsCompatible(dbNewInstanceExpression.Arguments[i], dbNewInstanceExpression2.Arguments[i]))
						{
							return false;
						}
					}
					return true;
				}
			}
			else
			{
				if (expressionKind == DbExpressionKind.Ref)
				{
					DbRefExpression dbRefExpression = (DbRefExpression)x;
					DbRefExpression dbRefExpression2 = (DbRefExpression)y;
					return dbRefExpression.EntitySet.EdmEquals(dbRefExpression2.EntitySet) && DiscriminatorMap.ExpressionsCompatible(dbRefExpression.Argument, dbRefExpression2.Argument);
				}
				if (expressionKind == DbExpressionKind.VariableReference)
				{
					return ((DbVariableReferenceExpression)x).VariableName == ((DbVariableReferenceExpression)y).VariableName;
				}
			}
			return false;
		}

		// Token: 0x04001139 RID: 4409
		internal readonly DbPropertyExpression Discriminator;

		// Token: 0x0400113A RID: 4410
		internal readonly ReadOnlyCollection<KeyValuePair<object, EntityType>> TypeMap;

		// Token: 0x0400113B RID: 4411
		internal readonly ReadOnlyCollection<KeyValuePair<EdmProperty, DbExpression>> PropertyMap;

		// Token: 0x0400113C RID: 4412
		internal readonly ReadOnlyCollection<KeyValuePair<RelProperty, DbExpression>> RelPropertyMap;

		// Token: 0x0400113D RID: 4413
		internal readonly EntitySet EntitySet;
	}
}
