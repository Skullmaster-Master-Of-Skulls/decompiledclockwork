using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000433 RID: 1075
	internal class ViewSimplifier
	{
		// Token: 0x0600399F RID: 14751 RVA: 0x000DAE6C File Offset: 0x000D906C
		internal static DbQueryCommandTree SimplifyView(EntitySetBase extent, DbQueryCommandTree view)
		{
			ViewSimplifier viewSimplifier = new ViewSimplifier(view.MetadataWorkspace, extent);
			view = viewSimplifier.Simplify(view);
			return view;
		}

		// Token: 0x060039A0 RID: 14752 RVA: 0x000DAE90 File Offset: 0x000D9090
		private ViewSimplifier(MetadataWorkspace mws, EntitySetBase viewTarget)
		{
			this.metadata = mws;
			this.extent = viewTarget;
		}

		// Token: 0x060039A1 RID: 14753 RVA: 0x000DAEA8 File Offset: 0x000D90A8
		private DbQueryCommandTree Simplify(DbQueryCommandTree view)
		{
			Func<DbExpression, DbExpression> func = PatternMatchRuleProcessor.Create(new PatternMatchRule[]
			{
				PatternMatchRule.Create(ViewSimplifier.Pattern_CollapseNestedProjection, new Func<DbExpression, DbExpression>(ViewSimplifier.CollapseNestedProjection)),
				PatternMatchRule.Create(ViewSimplifier.Pattern_Case, new Func<DbExpression, DbExpression>(ViewSimplifier.SimplifyCaseStatement)),
				PatternMatchRule.Create(ViewSimplifier.Pattern_NestedTphDiscriminator, new Func<DbExpression, DbExpression>(ViewSimplifier.SimplifyNestedTphDiscriminator)),
				PatternMatchRule.Create(ViewSimplifier.Pattern_EntityConstructor, new Func<DbExpression, DbExpression>(this.AddFkRelatedEntityRefs))
			});
			DbExpression dbExpression = view.Query;
			dbExpression = func(dbExpression);
			view = DbQueryCommandTree.FromValidExpression(view.MetadataWorkspace, view.DataSpace, dbExpression);
			return view;
		}

		// Token: 0x060039A2 RID: 14754 RVA: 0x000DAF4C File Offset: 0x000D914C
		private DbExpression AddFkRelatedEntityRefs(DbExpression viewConstructor)
		{
			if (this.doNotProcess)
			{
				return null;
			}
			if (this.extent.BuiltInTypeKind != BuiltInTypeKind.EntitySet || this.extent.EntityContainer.DataSpace != DataSpace.CSpace)
			{
				this.doNotProcess = true;
				return null;
			}
			EntitySet targetSet = (EntitySet)this.extent;
			Func<AssociationSetEnd, bool> <>9__2;
			List<AssociationSet> list = (from es in targetSet.EntityContainer.BaseEntitySets
			where es.BuiltInTypeKind == BuiltInTypeKind.AssociationSet
			select es).Cast<AssociationSet>().Where(delegate(AssociationSet assocSet)
			{
				if (assocSet.ElementType.IsForeignKey)
				{
					IEnumerable<AssociationSetEnd> associationSetEnds = assocSet.AssociationSetEnds;
					Func<AssociationSetEnd, bool> predicate;
					if ((predicate = <>9__2) == null)
					{
						predicate = (<>9__2 = ((AssociationSetEnd se) => se.EntitySet == targetSet));
					}
					return associationSetEnds.Any(predicate);
				}
				return false;
			}).ToList<AssociationSet>();
			if (list.Count == 0)
			{
				this.doNotProcess = true;
				return null;
			}
			HashSet<Tuple<EntityType, AssociationSetEnd, ReferentialConstraint>> hashSet = new HashSet<Tuple<EntityType, AssociationSetEnd, ReferentialConstraint>>();
			foreach (AssociationSet associationSet in list)
			{
				ReferentialConstraint referentialConstraint = associationSet.ElementType.ReferentialConstraints[0];
				AssociationSetEnd associationSetEnd = associationSet.AssociationSetEnds[referentialConstraint.ToRole.Name];
				if (associationSetEnd.EntitySet == targetSet)
				{
					EntityType item = (EntityType)TypeHelpers.GetEdmType<RefType>(associationSetEnd.CorrespondingAssociationEndMember.TypeUsage).ElementType;
					AssociationSetEnd item2 = associationSet.AssociationSetEnds[referentialConstraint.FromRole.Name];
					hashSet.Add(Tuple.Create<EntityType, AssociationSetEnd, ReferentialConstraint>(item, item2, referentialConstraint));
				}
			}
			if (hashSet.Count == 0)
			{
				this.doNotProcess = true;
				return null;
			}
			DbProjectExpression dbProjectExpression = (DbProjectExpression)viewConstructor;
			List<DbNewInstanceExpression> list2 = new List<DbNewInstanceExpression>();
			List<DbExpression> list3 = null;
			if (dbProjectExpression.Projection.ExpressionKind == DbExpressionKind.Case)
			{
				DbCaseExpression dbCaseExpression = (DbCaseExpression)dbProjectExpression.Projection;
				list3 = new List<DbExpression>(dbCaseExpression.When.Count);
				for (int i = 0; i < dbCaseExpression.When.Count; i++)
				{
					list3.Add(dbCaseExpression.When[i]);
					list2.Add((DbNewInstanceExpression)dbCaseExpression.Then[i]);
				}
				list2.Add((DbNewInstanceExpression)dbCaseExpression.Else);
			}
			else
			{
				list2.Add((DbNewInstanceExpression)dbProjectExpression.Projection);
			}
			bool flag = false;
			for (int j = 0; j < list2.Count; j++)
			{
				DbNewInstanceExpression entityConstructor = list2[j];
				EntityType constructedEntityType = TypeHelpers.GetEdmType<EntityType>(entityConstructor.ResultType);
				List<DbRelatedEntityRef> list4 = (from psdt in hashSet
				where constructedEntityType == psdt.Item1 || constructedEntityType.IsSubtypeOf(psdt.Item1)
				select ViewSimplifier.RelatedEntityRefFromAssociationSetEnd(constructedEntityType, entityConstructor, psdt.Item2, psdt.Item3)).ToList<DbRelatedEntityRef>();
				if (list4.Count > 0)
				{
					if (entityConstructor.HasRelatedEntityReferences)
					{
						list4 = entityConstructor.RelatedEntityReferences.Concat(list4).ToList<DbRelatedEntityRef>();
					}
					entityConstructor = DbExpressionBuilder.CreateNewEntityWithRelationshipsExpression(constructedEntityType, entityConstructor.Arguments, list4);
					list2[j] = entityConstructor;
					flag = true;
				}
			}
			DbExpression result = null;
			if (flag)
			{
				if (list3 != null)
				{
					List<DbExpression> list5 = new List<DbExpression>(list3.Count);
					List<DbExpression> list6 = new List<DbExpression>(list3.Count);
					for (int k = 0; k < list3.Count; k++)
					{
						list5.Add(list3[k]);
						list6.Add(list2[k]);
					}
					result = dbProjectExpression.Input.Project(DbExpressionBuilder.Case(list5, list6, list2[list3.Count]));
				}
				else
				{
					result = dbProjectExpression.Input.Project(list2[0]);
				}
			}
			this.doNotProcess = true;
			return result;
		}

		// Token: 0x060039A3 RID: 14755 RVA: 0x000DB31C File Offset: 0x000D951C
		private static DbRelatedEntityRef RelatedEntityRefFromAssociationSetEnd(EntityType constructedEntityType, DbNewInstanceExpression entityConstructor, AssociationSetEnd principalSetEnd, ReferentialConstraint fkConstraint)
		{
			EntityType entityType = (EntityType)TypeHelpers.GetEdmType<RefType>(fkConstraint.FromRole.TypeUsage).ElementType;
			IEnumerable<Tuple<string, DbExpression>> source = from pv in constructedEntityType.Properties.Select((EdmProperty p, int idx) => Tuple.Create<EdmProperty, DbExpression>(p, entityConstructor.Arguments[idx]))
			join ft in fkConstraint.FromProperties.Select((EdmProperty fp, int idx) => Tuple.Create<EdmProperty, EdmProperty>(fp, fkConstraint.ToProperties[idx])) on pv.Item1 equals ft.Item2
			select Tuple.Create<string, DbExpression>(ft.Item1.Name, pv.Item2);
			IList<DbExpression> keyValues;
			if (fkConstraint.FromProperties.Count == 1)
			{
				Tuple<string, DbExpression> tuple = source.Single<Tuple<string, DbExpression>>();
				keyValues = new DbExpression[]
				{
					tuple.Item2
				};
			}
			else
			{
				Dictionary<string, DbExpression> keyValueMap = source.ToDictionary((Tuple<string, DbExpression> pav) => pav.Item1, (Tuple<string, DbExpression> pav) => pav.Item2, StringComparer.Ordinal);
				keyValues = (from memberName in entityType.KeyMemberNames
				select keyValueMap[memberName]).ToList<DbExpression>();
			}
			DbRefExpression targetEntity = principalSetEnd.EntitySet.CreateRef(entityType, keyValues);
			return DbExpressionBuilder.CreateRelatedEntityRef(fkConstraint.ToRole, fkConstraint.FromRole, targetEntity);
		}

		// Token: 0x060039A4 RID: 14756 RVA: 0x000DB4C8 File Offset: 0x000D96C8
		private static DbExpression SimplifyNestedTphDiscriminator(DbExpression expression)
		{
			DbProjectExpression dbProjectExpression = (DbProjectExpression)expression;
			DbFilterExpression booleanColumnFilter = (DbFilterExpression)dbProjectExpression.Input.Expression;
			DbProjectExpression dbProjectExpression2 = (DbProjectExpression)booleanColumnFilter.Input.Expression;
			DbFilterExpression dbFilterExpression = (DbFilterExpression)dbProjectExpression2.Input.Expression;
			List<DbExpression> list = ViewSimplifier.FlattenOr(booleanColumnFilter.Predicate).ToList<DbExpression>();
			List<DbPropertyExpression> list2 = (from px in list.OfType<DbPropertyExpression>()
			where px.Instance.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)px.Instance).VariableName == booleanColumnFilter.Input.VariableName
			select px).ToList<DbPropertyExpression>();
			if (list.Count != list2.Count)
			{
				return null;
			}
			List<string> list3 = (from px in list2
			select px.Property.Name).ToList<string>();
			Dictionary<object, DbComparisonExpression> discriminatorPredicates = new Dictionary<object, DbComparisonExpression>();
			if (!TypeSemantics.IsEntityType(dbFilterExpression.Input.VariableType) || !ViewSimplifier.TryMatchDiscriminatorPredicate(dbFilterExpression, delegate(DbComparisonExpression compEx, object discValue)
			{
				discriminatorPredicates.Add(discValue, compEx);
			}))
			{
				return null;
			}
			EdmProperty edmProperty = (EdmProperty)((DbPropertyExpression)discriminatorPredicates.First<KeyValuePair<object, DbComparisonExpression>>().Value.Left).Property;
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbProjectExpression2.Projection;
			RowType edmType = TypeHelpers.GetEdmType<RowType>(dbNewInstanceExpression.ResultType);
			Dictionary<string, DbComparisonExpression> dictionary = new Dictionary<string, DbComparisonExpression>();
			Dictionary<string, DbComparisonExpression> dictionary2 = new Dictionary<string, DbComparisonExpression>();
			Dictionary<string, DbExpression> dictionary3 = new Dictionary<string, DbExpression>(dbNewInstanceExpression.Arguments.Count);
			for (int i = 0; i < dbNewInstanceExpression.Arguments.Count; i++)
			{
				string name = edmType.Properties[i].Name;
				DbExpression dbExpression = dbNewInstanceExpression.Arguments[i];
				if (list3.Contains(name))
				{
					if (dbExpression.ExpressionKind != DbExpressionKind.Case)
					{
						return null;
					}
					DbCaseExpression dbCaseExpression = (DbCaseExpression)dbExpression;
					if (dbCaseExpression.When.Count != 1 || !TypeSemantics.IsBooleanType(dbCaseExpression.Then[0].ResultType) || !TypeSemantics.IsBooleanType(dbCaseExpression.Else.ResultType) || dbCaseExpression.Then[0].ExpressionKind != DbExpressionKind.Constant || dbCaseExpression.Else.ExpressionKind != DbExpressionKind.Constant || !(bool)((DbConstantExpression)dbCaseExpression.Then[0]).Value || (bool)((DbConstantExpression)dbCaseExpression.Else).Value)
					{
						return null;
					}
					DbPropertyExpression dbPropertyExpression;
					object key;
					if (!ViewSimplifier.TryMatchPropertyEqualsValue(dbCaseExpression.When[0], dbProjectExpression2.Input.VariableName, out dbPropertyExpression, out key) || dbPropertyExpression.Property != edmProperty || !discriminatorPredicates.ContainsKey(key))
					{
						return null;
					}
					dictionary.Add(name, discriminatorPredicates[key]);
					dictionary2.Add(name, (DbComparisonExpression)dbCaseExpression.When[0]);
				}
				else
				{
					dictionary3.Add(name, dbExpression);
				}
			}
			DbExpression predicate = Helpers.BuildBalancedTreeInPlace<DbExpression>(new List<DbExpression>(dictionary.Values), (DbExpression left, DbExpression right) => left.Or(right));
			dbFilterExpression = dbFilterExpression.Input.Filter(predicate);
			DbCaseExpression dbCaseExpression2 = (DbCaseExpression)dbProjectExpression.Projection;
			List<DbExpression> list4 = new List<DbExpression>(dbCaseExpression2.When.Count);
			List<DbExpression> list5 = new List<DbExpression>(dbCaseExpression2.Then.Count);
			for (int j = 0; j < dbCaseExpression2.When.Count; j++)
			{
				DbPropertyExpression dbPropertyExpression2 = (DbPropertyExpression)dbCaseExpression2.When[j];
				DbNewInstanceExpression original = (DbNewInstanceExpression)dbCaseExpression2.Then[j];
				DbComparisonExpression item;
				if (!dictionary2.TryGetValue(dbPropertyExpression2.Property.Name, out item))
				{
					return null;
				}
				list4.Add(item);
				DbExpression item2 = ViewSimplifier.ValueSubstituter.Substitute(original, dbProjectExpression.Input.VariableName, dictionary3);
				list5.Add(item2);
			}
			DbExpression elseExpression = ViewSimplifier.ValueSubstituter.Substitute(dbCaseExpression2.Else, dbProjectExpression.Input.VariableName, dictionary3);
			DbCaseExpression projection = DbExpressionBuilder.Case(list4, list5, elseExpression);
			return dbFilterExpression.BindAs(dbProjectExpression2.Input.VariableName).Project(projection);
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x000DB8F0 File Offset: 0x000D9AF0
		private static DbExpression SimplifyCaseStatement(DbExpression expression)
		{
			DbCaseExpression dbCaseExpression = (DbCaseExpression)expression;
			bool flag = false;
			List<DbExpression> list = new List<DbExpression>(dbCaseExpression.When.Count);
			foreach (DbExpression dbExpression in dbCaseExpression.When)
			{
				DbExpression item;
				if (ViewSimplifier.TrySimplifyPredicate(dbExpression, out item))
				{
					list.Add(item);
					flag = true;
				}
				else
				{
					list.Add(dbExpression);
				}
			}
			if (!flag)
			{
				return null;
			}
			dbCaseExpression = DbExpressionBuilder.Case(list, dbCaseExpression.Then, dbCaseExpression.Else);
			return dbCaseExpression;
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x000DB98C File Offset: 0x000D9B8C
		private static bool TrySimplifyPredicate(DbExpression predicate, out DbExpression simplified)
		{
			simplified = null;
			if (predicate.ExpressionKind != DbExpressionKind.Case)
			{
				return false;
			}
			DbCaseExpression dbCaseExpression = (DbCaseExpression)predicate;
			if (dbCaseExpression.Then.Count != 1 && dbCaseExpression.Then[0].ExpressionKind == DbExpressionKind.Constant)
			{
				return false;
			}
			DbConstantExpression dbConstantExpression = (DbConstantExpression)dbCaseExpression.Then[0];
			if (!true.Equals(dbConstantExpression.Value))
			{
				return false;
			}
			if (dbCaseExpression.Else != null)
			{
				if (dbCaseExpression.Else.ExpressionKind != DbExpressionKind.Constant)
				{
					return false;
				}
				DbConstantExpression dbConstantExpression2 = (DbConstantExpression)dbCaseExpression.Else;
				if (!false.Equals(dbConstantExpression2.Value))
				{
					return false;
				}
			}
			simplified = dbCaseExpression.When[0];
			return true;
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000DBA40 File Offset: 0x000D9C40
		private static DbExpression CollapseNestedProjection(DbExpression expression)
		{
			DbProjectExpression dbProjectExpression = (DbProjectExpression)expression;
			DbExpression projection = dbProjectExpression.Projection;
			DbProjectExpression dbProjectExpression2 = (DbProjectExpression)dbProjectExpression.Input.Expression;
			DbNewInstanceExpression dbNewInstanceExpression = (DbNewInstanceExpression)dbProjectExpression2.Projection;
			Dictionary<string, DbExpression> dictionary = new Dictionary<string, DbExpression>(dbNewInstanceExpression.Arguments.Count);
			TypeUsage resultType = dbNewInstanceExpression.ResultType;
			RowType rowType = (RowType)resultType.EdmType;
			for (int i = 0; i < rowType.Members.Count; i++)
			{
				dictionary[rowType.Members[i].Name] = dbNewInstanceExpression.Arguments[i];
			}
			ViewSimplifier.ProjectionCollapser projectionCollapser = new ViewSimplifier.ProjectionCollapser(dictionary, dbProjectExpression.Input);
			DbExpression projection2 = projectionCollapser.CollapseProjection(projection);
			if (projectionCollapser.IsDoomed)
			{
				return null;
			}
			return dbProjectExpression2.Input.Project(projection2);
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000DBB18 File Offset: 0x000D9D18
		internal static IEnumerable<DbExpression> FlattenOr(DbExpression expression)
		{
			return Helpers.GetLeafNodes<DbExpression>(expression, (DbExpression exp) => exp.ExpressionKind != DbExpressionKind.Or, delegate(DbExpression exp)
			{
				DbOrExpression dbOrExpression = (DbOrExpression)exp;
				return new DbExpression[]
				{
					dbOrExpression.Left,
					dbOrExpression.Right
				};
			});
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000DBB6C File Offset: 0x000D9D6C
		internal static bool TryMatchDiscriminatorPredicate(DbFilterExpression filter, Action<DbComparisonExpression, object> onMatchedComparison)
		{
			EdmProperty edmProperty = null;
			foreach (DbExpression dbExpression in ViewSimplifier.FlattenOr(filter.Predicate))
			{
				DbPropertyExpression dbPropertyExpression;
				object arg;
				if (!ViewSimplifier.TryMatchPropertyEqualsValue(dbExpression, filter.Input.VariableName, out dbPropertyExpression, out arg))
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
				onMatchedComparison((DbComparisonExpression)dbExpression, arg);
			}
			return true;
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000DBC08 File Offset: 0x000D9E08
		internal static bool TryMatchPropertyEqualsValue(DbExpression expression, string propertyVariable, out DbPropertyExpression property, out object value)
		{
			property = null;
			value = null;
			if (expression.ExpressionKind != DbExpressionKind.Equals)
			{
				return false;
			}
			DbBinaryExpression dbBinaryExpression = (DbBinaryExpression)expression;
			if (dbBinaryExpression.Left.ExpressionKind != DbExpressionKind.Property)
			{
				return false;
			}
			property = (DbPropertyExpression)dbBinaryExpression.Left;
			return ViewSimplifier.TryMatchConstant(dbBinaryExpression.Right, out value) && property.Instance.ExpressionKind == DbExpressionKind.VariableReference && !(((DbVariableReferenceExpression)property.Instance).VariableName != propertyVariable);
		}

		// Token: 0x060039AB RID: 14763 RVA: 0x000DBC88 File Offset: 0x000D9E88
		private static bool TryMatchConstant(DbExpression expression, out object value)
		{
			if (expression.ExpressionKind == DbExpressionKind.Constant)
			{
				value = ((DbConstantExpression)expression).Value;
				return true;
			}
			if (expression.ExpressionKind == DbExpressionKind.Cast && expression.ResultType.EdmType.BuiltInTypeKind == BuiltInTypeKind.PrimitiveType)
			{
				DbCastExpression dbCastExpression = (DbCastExpression)expression;
				if (ViewSimplifier.TryMatchConstant(dbCastExpression.Argument, out value))
				{
					PrimitiveType primitiveType = (PrimitiveType)expression.ResultType.EdmType;
					value = Convert.ChangeType(value, primitiveType.ClrEquivalentType, CultureInfo.InvariantCulture);
					return true;
				}
			}
			value = null;
			return false;
		}

		// Token: 0x04001860 RID: 6240
		private readonly MetadataWorkspace metadata;

		// Token: 0x04001861 RID: 6241
		private readonly EntitySetBase extent;

		// Token: 0x04001862 RID: 6242
		private static readonly Func<DbExpression, bool> Pattern_EntityConstructor = Patterns.MatchProject(Patterns.AnyExpression, Patterns.And(Patterns.MatchEntityType, Patterns.Or(Patterns.MatchNewInstance(), Patterns.MatchCase(Patterns.AnyExpressions, Patterns.MatchForAll(Patterns.MatchNewInstance()), Patterns.MatchNewInstance()))));

		// Token: 0x04001863 RID: 6243
		private bool doNotProcess;

		// Token: 0x04001864 RID: 6244
		private static readonly Func<DbExpression, bool> Pattern_NestedTphDiscriminator = Patterns.MatchProject(Patterns.MatchFilter(Patterns.MatchProject(Patterns.MatchFilter(Patterns.AnyExpression, Patterns.Or(Patterns.MatchKind(DbExpressionKind.Equals), Patterns.MatchKind(DbExpressionKind.Or))), Patterns.And(Patterns.MatchRowType, Patterns.MatchNewInstance(Patterns.MatchForAll(Patterns.Or(Patterns.And(Patterns.MatchNewInstance(), Patterns.MatchComplexType), Patterns.MatchKind(DbExpressionKind.Property), Patterns.MatchKind(DbExpressionKind.Case)))))), Patterns.Or(Patterns.MatchKind(DbExpressionKind.Property), Patterns.MatchKind(DbExpressionKind.Or))), Patterns.And(Patterns.MatchEntityType, Patterns.MatchCase(Patterns.MatchForAll(Patterns.MatchKind(DbExpressionKind.Property)), Patterns.MatchForAll(Patterns.MatchKind(DbExpressionKind.NewInstance)), Patterns.MatchKind(DbExpressionKind.NewInstance))));

		// Token: 0x04001865 RID: 6245
		private static readonly Func<DbExpression, bool> Pattern_Case = Patterns.MatchKind(DbExpressionKind.Case);

		// Token: 0x04001866 RID: 6246
		private static readonly Func<DbExpression, bool> Pattern_CollapseNestedProjection = Patterns.MatchProject(Patterns.MatchProject(Patterns.AnyExpression, Patterns.MatchKind(DbExpressionKind.NewInstance)), Patterns.AnyExpression);

		// Token: 0x020006C2 RID: 1730
		private class ValueSubstituter : DefaultExpressionVisitor
		{
			// Token: 0x060045E4 RID: 17892 RVA: 0x000FA894 File Offset: 0x000F8A94
			internal static DbExpression Substitute(DbExpression original, string referencedVariable, Dictionary<string, DbExpression> propertyValues)
			{
				ViewSimplifier.ValueSubstituter valueSubstituter = new ViewSimplifier.ValueSubstituter(referencedVariable, propertyValues);
				return valueSubstituter.VisitExpression(original);
			}

			// Token: 0x060045E5 RID: 17893 RVA: 0x000FA8B0 File Offset: 0x000F8AB0
			private ValueSubstituter(string varName, Dictionary<string, DbExpression> replValues)
			{
				this.variableName = varName;
				this.replacements = replValues;
			}

			// Token: 0x060045E6 RID: 17894 RVA: 0x000FA8C8 File Offset: 0x000F8AC8
			public override DbExpression Visit(DbPropertyExpression expression)
			{
				DbExpression dbExpression;
				DbExpression result;
				if (expression.Instance.ExpressionKind == DbExpressionKind.VariableReference && ((DbVariableReferenceExpression)expression.Instance).VariableName == this.variableName && this.replacements.TryGetValue(expression.Property.Name, out dbExpression))
				{
					result = dbExpression;
				}
				else
				{
					result = base.Visit(expression);
				}
				return result;
			}

			// Token: 0x04002068 RID: 8296
			private readonly string variableName;

			// Token: 0x04002069 RID: 8297
			private readonly Dictionary<string, DbExpression> replacements;
		}

		// Token: 0x020006C3 RID: 1731
		private class ProjectionCollapser : DefaultExpressionVisitor
		{
			// Token: 0x060045E7 RID: 17895 RVA: 0x000FA92A File Offset: 0x000F8B2A
			internal ProjectionCollapser(Dictionary<string, DbExpression> varRefMemberBindings, DbExpressionBinding outerBinding)
			{
				this.m_varRefMemberBindings = varRefMemberBindings;
				this.m_outerBinding = outerBinding;
			}

			// Token: 0x060045E8 RID: 17896 RVA: 0x000E3370 File Offset: 0x000E1570
			internal DbExpression CollapseProjection(DbExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x060045E9 RID: 17897 RVA: 0x000FA940 File Offset: 0x000F8B40
			public override DbExpression Visit(DbPropertyExpression property)
			{
				if (property.Instance.ExpressionKind == DbExpressionKind.VariableReference && this.IsOuterBindingVarRef((DbVariableReferenceExpression)property.Instance))
				{
					return this.m_varRefMemberBindings[property.Property.Name];
				}
				return base.Visit(property);
			}

			// Token: 0x060045EA RID: 17898 RVA: 0x000FA98D File Offset: 0x000F8B8D
			public override DbExpression Visit(DbVariableReferenceExpression varRef)
			{
				if (this.IsOuterBindingVarRef(varRef))
				{
					this.m_doomed = true;
				}
				return base.Visit(varRef);
			}

			// Token: 0x060045EB RID: 17899 RVA: 0x000FA9A6 File Offset: 0x000F8BA6
			private bool IsOuterBindingVarRef(DbVariableReferenceExpression varRef)
			{
				return varRef.VariableName == this.m_outerBinding.VariableName;
			}

			// Token: 0x17000BCD RID: 3021
			// (get) Token: 0x060045EC RID: 17900 RVA: 0x000FA9BE File Offset: 0x000F8BBE
			internal bool IsDoomed
			{
				get
				{
					return this.m_doomed;
				}
			}

			// Token: 0x0400206A RID: 8298
			private Dictionary<string, DbExpression> m_varRefMemberBindings;

			// Token: 0x0400206B RID: 8299
			private DbExpressionBinding m_outerBinding;

			// Token: 0x0400206C RID: 8300
			private bool m_doomed;
		}
	}
}
