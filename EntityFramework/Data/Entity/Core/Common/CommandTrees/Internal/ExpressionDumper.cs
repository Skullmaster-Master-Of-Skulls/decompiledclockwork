using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees.Internal
{
	// Token: 0x02000127 RID: 295
	[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
	internal abstract class ExpressionDumper : DbExpressionVisitor
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x0002F27A File Offset: 0x0002D47A
		internal void Begin(string name)
		{
			this.Begin(name, null);
		}

		// Token: 0x0600093C RID: 2364
		internal abstract void Begin(string name, Dictionary<string, object> attrs);

		// Token: 0x0600093D RID: 2365
		internal abstract void End(string name);

		// Token: 0x0600093E RID: 2366 RVA: 0x0002F284 File Offset: 0x0002D484
		internal void Dump(DbExpression target)
		{
			target.Accept(this);
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0002F28D File Offset: 0x0002D48D
		internal void Dump(DbExpression e, string name)
		{
			this.Begin(name);
			this.Dump(e);
			this.End(name);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0002F2A4 File Offset: 0x0002D4A4
		internal void Dump(DbExpressionBinding binding, string name)
		{
			this.Begin(name);
			this.Dump(binding);
			this.End(name);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		internal void Dump(DbExpressionBinding binding)
		{
			this.Begin("DbExpressionBinding", "VariableName", binding.VariableName);
			this.Begin("Expression");
			this.Dump(binding.Expression);
			this.End("Expression");
			this.End("DbExpressionBinding");
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0002F30C File Offset: 0x0002D50C
		internal void Dump(DbGroupExpressionBinding binding, string name)
		{
			this.Begin(name);
			this.Dump(binding);
			this.End(name);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002F324 File Offset: 0x0002D524
		internal void Dump(DbGroupExpressionBinding binding)
		{
			this.Begin("DbGroupExpressionBinding", "VariableName", binding.VariableName, "GroupVariableName", binding.GroupVariableName);
			this.Begin("Expression");
			this.Dump(binding.Expression);
			this.End("Expression");
			this.End("DbGroupExpressionBinding");
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0002F380 File Offset: 0x0002D580
		internal void Dump(IEnumerable<DbExpression> exprs, string pluralName, string singularName)
		{
			this.Begin(pluralName);
			foreach (DbExpression target in exprs)
			{
				this.Begin(singularName);
				this.Dump(target);
				this.End(singularName);
			}
			this.End(pluralName);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002F3E4 File Offset: 0x0002D5E4
		internal void Dump(IEnumerable<FunctionParameter> paramList)
		{
			this.Begin("Parameters");
			foreach (FunctionParameter functionParameter in paramList)
			{
				this.Begin("Parameter", "Name", functionParameter.Name);
				this.Dump(functionParameter.TypeUsage, "ParameterType");
				this.End("Parameter");
			}
			this.End("Parameters");
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0002F470 File Offset: 0x0002D670
		internal void Dump(TypeUsage type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002F488 File Offset: 0x0002D688
		internal void Dump(TypeUsage type)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (Facet facet in type.Facets)
			{
				dictionary.Add(facet.Name, facet.Value);
			}
			this.Begin("TypeUsage", dictionary);
			this.Dump(type.EdmType);
			this.End("TypeUsage");
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x0002F510 File Offset: 0x0002D710
		internal void Dump(EdmType type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x0002F528 File Offset: 0x0002D728
		internal void Dump(EdmType type)
		{
			this.Begin("EdmType", "BuiltInTypeKind", Enum.GetName(typeof(BuiltInTypeKind), type.BuiltInTypeKind), "Namespace", type.NamespaceName, "Name", type.Name);
			this.End("EdmType");
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x0002F580 File Offset: 0x0002D780
		internal void Dump(RelationshipType type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0002F597 File Offset: 0x0002D797
		internal void Dump(RelationshipType type)
		{
			this.Begin("RelationshipType", "Namespace", type.NamespaceName, "Name", type.Name);
			this.End("RelationshipType");
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0002F5C8 File Offset: 0x0002D7C8
		internal void Dump(EdmFunction function)
		{
			this.Begin("Function", "Name", function.Name, "Namespace", function.NamespaceName);
			this.Dump(function.Parameters);
			if (function.ReturnParameters.Count == 1)
			{
				this.Dump(function.ReturnParameters[0].TypeUsage, "ReturnType");
			}
			else
			{
				this.Begin("ReturnTypes");
				foreach (FunctionParameter functionParameter in function.ReturnParameters)
				{
					this.Dump(functionParameter.TypeUsage, functionParameter.Name);
				}
				this.End("ReturnTypes");
			}
			this.End("Function");
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
		internal void Dump(EdmProperty prop)
		{
			this.Begin("Property", "Name", prop.Name, "Nullable", prop.Nullable);
			this.Dump(prop.DeclaringType, "DeclaringType");
			this.Dump(prop.TypeUsage, "PropertyType");
			this.End("Property");
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0002F700 File Offset: 0x0002D900
		internal void Dump(RelationshipEndMember end, string name)
		{
			this.Begin(name);
			this.Begin("RelationshipEndMember", "Name", end.Name, "RelationshipMultiplicity", Enum.GetName(typeof(RelationshipMultiplicity), end.RelationshipMultiplicity));
			this.Dump(end.DeclaringType, "DeclaringRelation");
			this.Dump(end.TypeUsage, "EndType");
			this.End("RelationshipEndMember");
			this.End(name);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0002F780 File Offset: 0x0002D980
		internal void Dump(NavigationProperty navProp, string name)
		{
			this.Begin(name);
			this.Begin("NavigationProperty", "Name", navProp.Name, "RelationshipTypeName", navProp.RelationshipType.FullName, "ToEndMemberName", navProp.ToEndMember.Name);
			this.Dump(navProp.DeclaringType, "DeclaringType");
			this.Dump(navProp.TypeUsage, "PropertyType");
			this.End("NavigationProperty");
			this.End(name);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0002F800 File Offset: 0x0002DA00
		internal void Dump(DbLambda lambda)
		{
			this.Begin("DbLambda");
			this.Dump(lambda.Variables.Cast<DbExpression>(), "Variables", "Variable");
			this.Dump(lambda.Body, "Body");
			this.End("DbLambda");
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0002F84F File Offset: 0x0002DA4F
		private void Begin(DbExpression expr)
		{
			this.Begin(expr, new Dictionary<string, object>());
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002F860 File Offset: 0x0002DA60
		private void Begin(DbExpression expr, Dictionary<string, object> attrs)
		{
			attrs.Add("DbExpressionKind", Enum.GetName(typeof(DbExpressionKind), expr.ExpressionKind));
			this.Begin(expr.GetType().Name, attrs);
			this.Dump(expr.ResultType, "ResultType");
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002F8B8 File Offset: 0x0002DAB8
		private void Begin(DbExpression expr, string attributeName, object attributeValue)
		{
			this.Begin(expr, new Dictionary<string, object>
			{
				{
					attributeName,
					attributeValue
				}
			});
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0002F8DC File Offset: 0x0002DADC
		private void Begin(string expr, string attributeName, object attributeValue)
		{
			this.Begin(expr, new Dictionary<string, object>
			{
				{
					attributeName,
					attributeValue
				}
			});
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0002F900 File Offset: 0x0002DB00
		private void Begin(string expr, string attributeName1, object attributeValue1, string attributeName2, object attributeValue2)
		{
			this.Begin(expr, new Dictionary<string, object>
			{
				{
					attributeName1,
					attributeValue1
				},
				{
					attributeName2,
					attributeValue2
				}
			});
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0002F930 File Offset: 0x0002DB30
		private void Begin(string expr, string attributeName1, object attributeValue1, string attributeName2, object attributeValue2, string attributeName3, object attributeValue3)
		{
			this.Begin(expr, new Dictionary<string, object>
			{
				{
					attributeName1,
					attributeValue1
				},
				{
					attributeName2,
					attributeValue2
				},
				{
					attributeName3,
					attributeValue3
				}
			});
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0002F967 File Offset: 0x0002DB67
		private void End(DbExpression expr)
		{
			this.End(expr.GetType().Name);
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0002F97A File Offset: 0x0002DB7A
		private void BeginUnary(DbUnaryExpression e)
		{
			this.Begin(e);
			this.Begin("Argument");
			this.Dump(e.Argument);
			this.End("Argument");
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x0002F9A8 File Offset: 0x0002DBA8
		private void BeginBinary(DbBinaryExpression e)
		{
			this.Begin(e);
			this.Begin("Left");
			this.Dump(e.Left);
			this.End("Left");
			this.Begin("Right");
			this.Dump(e.Right);
			this.End("Right");
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002FA00 File Offset: 0x0002DC00
		public override void Visit(DbExpression e)
		{
			Check.NotNull<DbExpression>(e, "e");
			this.Begin(e);
			this.End(e);
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002FA1C File Offset: 0x0002DC1C
		public override void Visit(DbConstantExpression e)
		{
			Check.NotNull<DbConstantExpression>(e, "e");
			this.Begin(e, new Dictionary<string, object>
			{
				{
					"Value",
					e.Value
				}
			});
			this.End(e);
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0002FA5B File Offset: 0x0002DC5B
		public override void Visit(DbNullExpression e)
		{
			Check.NotNull<DbNullExpression>(e, "e");
			this.Begin(e);
			this.End(e);
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0002FA78 File Offset: 0x0002DC78
		public override void Visit(DbVariableReferenceExpression e)
		{
			Check.NotNull<DbVariableReferenceExpression>(e, "e");
			this.Begin(e, new Dictionary<string, object>
			{
				{
					"VariableName",
					e.VariableName
				}
			});
			this.End(e);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002FAB8 File Offset: 0x0002DCB8
		public override void Visit(DbParameterReferenceExpression e)
		{
			Check.NotNull<DbParameterReferenceExpression>(e, "e");
			this.Begin(e, new Dictionary<string, object>
			{
				{
					"ParameterName",
					e.ParameterName
				}
			});
			this.End(e);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002FAF7 File Offset: 0x0002DCF7
		public override void Visit(DbFunctionExpression e)
		{
			Check.NotNull<DbFunctionExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Function);
			this.Dump(e.Arguments, "Arguments", "Argument");
			this.End(e);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002FB35 File Offset: 0x0002DD35
		public override void Visit(DbLambdaExpression expression)
		{
			Check.NotNull<DbLambdaExpression>(expression, "expression");
			this.Begin(expression);
			this.Dump(expression.Lambda);
			this.Dump(expression.Arguments, "Arguments", "Argument");
			this.End(expression);
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002FB74 File Offset: 0x0002DD74
		public override void Visit(DbPropertyExpression e)
		{
			Check.NotNull<DbPropertyExpression>(e, "e");
			this.Begin(e);
			RelationshipEndMember relationshipEndMember = e.Property as RelationshipEndMember;
			if (relationshipEndMember != null)
			{
				this.Dump(relationshipEndMember, "Property");
			}
			else if (Helper.IsNavigationProperty(e.Property))
			{
				this.Dump((NavigationProperty)e.Property, "Property");
			}
			else
			{
				this.Dump((EdmProperty)e.Property);
			}
			if (e.Instance != null)
			{
				this.Dump(e.Instance, "Instance");
			}
			this.End(e);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002FC07 File Offset: 0x0002DE07
		public override void Visit(DbComparisonExpression e)
		{
			Check.NotNull<DbComparisonExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002FC24 File Offset: 0x0002DE24
		public override void Visit(DbLikeExpression e)
		{
			Check.NotNull<DbLikeExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Argument, "Argument");
			this.Dump(e.Pattern, "Pattern");
			this.Dump(e.Escape, "Escape");
			this.End(e);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002FC80 File Offset: 0x0002DE80
		public override void Visit(DbLimitExpression e)
		{
			Check.NotNull<DbLimitExpression>(e, "e");
			this.Begin(e, "WithTies", e.WithTies);
			this.Dump(e.Argument, "Argument");
			this.Dump(e.Limit, "Limit");
			this.End(e);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002FCD9 File Offset: 0x0002DED9
		public override void Visit(DbIsNullExpression e)
		{
			Check.NotNull<DbIsNullExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002FCF5 File Offset: 0x0002DEF5
		public override void Visit(DbArithmeticExpression e)
		{
			Check.NotNull<DbArithmeticExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Arguments, "Arguments", "Argument");
			this.End(e);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002FD27 File Offset: 0x0002DF27
		public override void Visit(DbAndExpression e)
		{
			Check.NotNull<DbAndExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002FD43 File Offset: 0x0002DF43
		public override void Visit(DbOrExpression e)
		{
			Check.NotNull<DbOrExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002FD5F File Offset: 0x0002DF5F
		public override void Visit(DbInExpression e)
		{
			Check.NotNull<DbInExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Item);
			this.Dump(e.List, "List", "Item");
			this.End(e);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002FD9D File Offset: 0x0002DF9D
		public override void Visit(DbNotExpression e)
		{
			Check.NotNull<DbNotExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002FDB9 File Offset: 0x0002DFB9
		public override void Visit(DbDistinctExpression e)
		{
			Check.NotNull<DbDistinctExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002FDD5 File Offset: 0x0002DFD5
		public override void Visit(DbElementExpression e)
		{
			Check.NotNull<DbElementExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002FDF1 File Offset: 0x0002DFF1
		public override void Visit(DbIsEmptyExpression e)
		{
			Check.NotNull<DbIsEmptyExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002FE0D File Offset: 0x0002E00D
		public override void Visit(DbUnionAllExpression e)
		{
			Check.NotNull<DbUnionAllExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002FE29 File Offset: 0x0002E029
		public override void Visit(DbIntersectExpression e)
		{
			Check.NotNull<DbIntersectExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002FE45 File Offset: 0x0002E045
		public override void Visit(DbExceptExpression e)
		{
			Check.NotNull<DbExceptExpression>(e, "e");
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0002FE61 File Offset: 0x0002E061
		public override void Visit(DbTreatExpression e)
		{
			Check.NotNull<DbTreatExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002FE7D File Offset: 0x0002E07D
		public override void Visit(DbIsOfExpression e)
		{
			Check.NotNull<DbIsOfExpression>(e, "e");
			this.BeginUnary(e);
			this.Dump(e.OfType, "OfType");
			this.End(e);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002FEAA File Offset: 0x0002E0AA
		public override void Visit(DbCastExpression e)
		{
			Check.NotNull<DbCastExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0002FEC8 File Offset: 0x0002E0C8
		public override void Visit(DbCaseExpression e)
		{
			Check.NotNull<DbCaseExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.When, "Whens", "When");
			this.Dump(e.Then, "Thens", "Then");
			this.Dump(e.Else, "Else");
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0002FF25 File Offset: 0x0002E125
		public override void Visit(DbOfTypeExpression e)
		{
			Check.NotNull<DbOfTypeExpression>(e, "e");
			this.BeginUnary(e);
			this.Dump(e.OfType, "OfType");
			this.End(e);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002FF54 File Offset: 0x0002E154
		public override void Visit(DbNewInstanceExpression e)
		{
			Check.NotNull<DbNewInstanceExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Arguments, "Arguments", "Argument");
			if (e.HasRelatedEntityReferences)
			{
				this.Begin("RelatedEntityReferences");
				foreach (DbRelatedEntityRef dbRelatedEntityRef in e.RelatedEntityReferences)
				{
					this.Begin("DbRelatedEntityRef");
					this.Dump(dbRelatedEntityRef.SourceEnd, "SourceEnd");
					this.Dump(dbRelatedEntityRef.TargetEnd, "TargetEnd");
					this.Dump(dbRelatedEntityRef.TargetEntityReference, "TargetEntityReference");
					this.End("DbRelatedEntityRef");
				}
				this.End("RelatedEntityReferences");
			}
			this.End(e);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00030034 File Offset: 0x0002E234
		public override void Visit(DbRelationshipNavigationExpression e)
		{
			Check.NotNull<DbRelationshipNavigationExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.NavigateFrom, "NavigateFrom");
			this.Dump(e.NavigateTo, "NavigateTo");
			this.Dump(e.Relationship, "Relationship");
			this.Dump(e.NavigationSource, "NavigationSource");
			this.End(e);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0003009F File Offset: 0x0002E29F
		public override void Visit(DbRefExpression e)
		{
			Check.NotNull<DbRefExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000300BB File Offset: 0x0002E2BB
		public override void Visit(DbDerefExpression e)
		{
			Check.NotNull<DbDerefExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000300D7 File Offset: 0x0002E2D7
		public override void Visit(DbRefKeyExpression e)
		{
			Check.NotNull<DbRefKeyExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public override void Visit(DbEntityRefExpression e)
		{
			Check.NotNull<DbEntityRefExpression>(e, "e");
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00030110 File Offset: 0x0002E310
		public override void Visit(DbScanExpression e)
		{
			Check.NotNull<DbScanExpression>(e, "e");
			this.Begin(e);
			this.Begin("Target", "Name", e.Target.Name, "Container", e.Target.EntityContainer.Name);
			this.Dump(e.Target.ElementType, "TargetElementType");
			this.End("Target");
			this.End(e);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00030188 File Offset: 0x0002E388
		public override void Visit(DbFilterExpression e)
		{
			Check.NotNull<DbFilterExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Predicate, "Predicate");
			this.End(e);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000301C6 File Offset: 0x0002E3C6
		public override void Visit(DbProjectExpression e)
		{
			Check.NotNull<DbProjectExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Projection, "Projection");
			this.End(e);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00030204 File Offset: 0x0002E404
		public override void Visit(DbCrossJoinExpression e)
		{
			Check.NotNull<DbCrossJoinExpression>(e, "e");
			this.Begin(e);
			this.Begin("Inputs");
			foreach (DbExpressionBinding binding in e.Inputs)
			{
				this.Dump(binding, "Input");
			}
			this.End("Inputs");
			this.End(e);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00030288 File Offset: 0x0002E488
		public override void Visit(DbJoinExpression e)
		{
			Check.NotNull<DbJoinExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Left, "Left");
			this.Dump(e.Right, "Right");
			this.Dump(e.JoinCondition, "JoinCondition");
			this.End(e);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000302E2 File Offset: 0x0002E4E2
		public override void Visit(DbApplyExpression e)
		{
			Check.NotNull<DbApplyExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Apply, "Apply");
			this.End(e);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00030320 File Offset: 0x0002E520
		public override void Visit(DbGroupByExpression e)
		{
			Check.NotNull<DbGroupByExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Keys, "Keys", "Key");
			this.Begin("Aggregates");
			foreach (DbAggregate dbAggregate in e.Aggregates)
			{
				DbFunctionAggregate dbFunctionAggregate = dbAggregate as DbFunctionAggregate;
				if (dbFunctionAggregate != null)
				{
					this.Begin("DbFunctionAggregate");
					this.Dump(dbFunctionAggregate.Function);
					this.Dump(dbFunctionAggregate.Arguments, "Arguments", "Argument");
					this.End("DbFunctionAggregate");
				}
				else
				{
					DbGroupAggregate dbGroupAggregate = dbAggregate as DbGroupAggregate;
					this.Begin("DbGroupAggregate");
					this.Dump(dbGroupAggregate.Arguments, "Arguments", "Argument");
					this.End("DbGroupAggregate");
				}
			}
			this.End("Aggregates");
			this.End(e);
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00030438 File Offset: 0x0002E638
		protected virtual void Dump(IList<DbSortClause> sortOrder)
		{
			this.Begin("SortOrder");
			foreach (DbSortClause dbSortClause in sortOrder)
			{
				string text = dbSortClause.Collation;
				if (text == null)
				{
					text = "";
				}
				this.Begin("DbSortClause", "Ascending", dbSortClause.Ascending, "Collation", text);
				this.Dump(dbSortClause.Expression, "Expression");
				this.End("DbSortClause");
			}
			this.End("SortOrder");
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x000304DC File Offset: 0x0002E6DC
		public override void Visit(DbSkipExpression e)
		{
			Check.NotNull<DbSkipExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.SortOrder);
			this.Dump(e.Count, "Count");
			this.End(e);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00030531 File Offset: 0x0002E731
		public override void Visit(DbSortExpression e)
		{
			Check.NotNull<DbSortExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.SortOrder);
			this.End(e);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0003056A File Offset: 0x0002E76A
		public override void Visit(DbQuantifierExpression e)
		{
			Check.NotNull<DbQuantifierExpression>(e, "e");
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Predicate, "Predicate");
			this.End(e);
		}
	}
}
