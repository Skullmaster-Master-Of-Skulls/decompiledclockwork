using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x02000432 RID: 1074
	internal class DiscriminatorMap
	{
		// Token: 0x06002774 RID: 10100 RVA: 0x000BF50C File Offset: 0x000BD70C
		private DiscriminatorMap(DbPropertyExpression discriminator, List<KeyValuePair<object, EntityType>> typeMap, Dictionary<EdmProperty, DbExpression> propertyMap, Dictionary<RelProperty, DbExpression> relPropertyMap, EntitySet entitySet)
		{
			this.Discriminator = discriminator;
			this.TypeMap = new ReadOnlyCollection<KeyValuePair<object, EntityType>>(typeMap);
			this.PropertyMap = new ReadOnlyCollection<KeyValuePair<EdmProperty, DbExpression>>(propertyMap.ToList<KeyValuePair<EdmProperty, DbExpression>>());
			this.RelPropertyMap = new ReadOnlyCollection<KeyValuePair<RelProperty, DbExpression>>(relPropertyMap.ToList<KeyValuePair<RelProperty, DbExpression>>());
			this.EntitySet = entitySet;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x000BF580 File Offset: 0x000BD780
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

		// Token: 0x06002776 RID: 10102 RVA: 0x000BF7AC File Offset: 0x000BD9AC
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

		// Token: 0x06002777 RID: 10103 RVA: 0x000BF864 File Offset: 0x000BDA64
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

		// Token: 0x06002778 RID: 10104 RVA: 0x000BF9BC File Offset: 0x000BDBBC
		private static bool ExpressionsCompatible(DbExpression x, DbExpression y)
		{
			if (x.ExpressionKind != y.ExpressionKind)
			{
				return false;
			}
			DbExpressionKind expressionKind = x.ExpressionKind;
			if (expressionKind != DbExpressionKind.NewInstance)
			{
				switch (expressionKind)
				{
				case DbExpressionKind.Property:
				{
					DbPropertyExpression dbPropertyExpression = (DbPropertyExpression)x;
					DbPropertyExpression dbPropertyExpression2 = (DbPropertyExpression)y;
					return dbPropertyExpression.Property == dbPropertyExpression2.Property && DiscriminatorMap.ExpressionsCompatible(dbPropertyExpression.Instance, dbPropertyExpression2.Instance);
				}
				case DbExpressionKind.Ref:
				{
					DbRefExpression dbRefExpression = (DbRefExpression)x;
					DbRefExpression dbRefExpression2 = (DbRefExpression)y;
					return dbRefExpression.EntitySet.EdmEquals(dbRefExpression2.EntitySet) && DiscriminatorMap.ExpressionsCompatible(dbRefExpression.Argument, dbRefExpression2.Argument);
				}
				default:
					return expressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)x).VariableName == ((DbVariableReferenceExpression)y).VariableName;
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

		// Token: 0x04000ED5 RID: 3797
		internal readonly DbPropertyExpression Discriminator;

		// Token: 0x04000ED6 RID: 3798
		internal readonly ReadOnlyCollection<KeyValuePair<object, EntityType>> TypeMap;

		// Token: 0x04000ED7 RID: 3799
		internal readonly ReadOnlyCollection<KeyValuePair<EdmProperty, DbExpression>> PropertyMap;

		// Token: 0x04000ED8 RID: 3800
		internal readonly ReadOnlyCollection<KeyValuePair<RelProperty, DbExpression>> RelPropertyMap;

		// Token: 0x04000ED9 RID: 3801
		internal readonly EntitySet EntitySet;
	}
}
