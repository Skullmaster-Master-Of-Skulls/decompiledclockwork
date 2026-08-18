using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.CommandTrees.Internal
{
	// Token: 0x02000435 RID: 1077
	internal abstract class ExpressionDumper : DbExpressionVisitor
	{
		// Token: 0x060039B5 RID: 14773 RVA: 0x000D0AC4 File Offset: 0x000CECC4
		internal ExpressionDumper()
		{
		}

		// Token: 0x060039B6 RID: 14774 RVA: 0x000DC38B File Offset: 0x000DA58B
		internal void Begin(string name)
		{
			this.Begin(name, null);
		}

		// Token: 0x060039B7 RID: 14775
		internal abstract void Begin(string name, Dictionary<string, object> attrs);

		// Token: 0x060039B8 RID: 14776
		internal abstract void End(string name);

		// Token: 0x060039B9 RID: 14777 RVA: 0x000DC395 File Offset: 0x000DA595
		internal void Dump(DbExpression target)
		{
			target.Accept(this);
		}

		// Token: 0x060039BA RID: 14778 RVA: 0x000DC39E File Offset: 0x000DA59E
		internal void Dump(DbExpression e, string name)
		{
			this.Begin(name);
			this.Dump(e);
			this.End(name);
		}

		// Token: 0x060039BB RID: 14779 RVA: 0x000DC3B5 File Offset: 0x000DA5B5
		internal void Dump(DbExpressionBinding binding, string name)
		{
			this.Begin(name);
			this.Dump(binding);
			this.End(name);
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x000DC3CC File Offset: 0x000DA5CC
		internal void Dump(DbExpressionBinding binding)
		{
			this.Begin("DbExpressionBinding", "VariableName", binding.VariableName);
			this.Begin("Expression");
			this.Dump(binding.Expression);
			this.End("Expression");
			this.End("DbExpressionBinding");
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x000DC41C File Offset: 0x000DA61C
		internal void Dump(DbGroupExpressionBinding binding, string name)
		{
			this.Begin(name);
			this.Dump(binding);
			this.End(name);
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000DC434 File Offset: 0x000DA634
		internal void Dump(DbGroupExpressionBinding binding)
		{
			this.Begin("DbGroupExpressionBinding", "VariableName", binding.VariableName, "GroupVariableName", binding.GroupVariableName);
			this.Begin("Expression");
			this.Dump(binding.Expression);
			this.End("Expression");
			this.End("DbGroupExpressionBinding");
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000DC490 File Offset: 0x000DA690
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

		// Token: 0x060039C0 RID: 14784 RVA: 0x000DC4F4 File Offset: 0x000DA6F4
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

		// Token: 0x060039C1 RID: 14785 RVA: 0x000DC580 File Offset: 0x000DA780
		internal void Dump(TypeUsage type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000DC598 File Offset: 0x000DA798
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

		// Token: 0x060039C3 RID: 14787 RVA: 0x000DC620 File Offset: 0x000DA820
		internal void Dump(EdmType type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x000DC638 File Offset: 0x000DA838
		internal void Dump(EdmType type)
		{
			this.Begin("EdmType", "BuiltInTypeKind", Enum.GetName(typeof(BuiltInTypeKind), type.BuiltInTypeKind), "Namespace", type.NamespaceName, "Name", type.Name);
			this.End("EdmType");
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x000DC690 File Offset: 0x000DA890
		internal void Dump(RelationshipType type, string name)
		{
			this.Begin(name);
			this.Dump(type);
			this.End(name);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x000DC6A7 File Offset: 0x000DA8A7
		internal void Dump(RelationshipType type)
		{
			this.Begin("RelationshipType", "Namespace", type.NamespaceName, "Name", type.Name);
			this.End("RelationshipType");
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x000DC6D8 File Offset: 0x000DA8D8
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

		// Token: 0x060039C8 RID: 14792 RVA: 0x000DC7B0 File Offset: 0x000DA9B0
		internal void Dump(EdmProperty prop)
		{
			this.Begin("Property", "Name", prop.Name, "Nullable", prop.Nullable);
			this.Dump(prop.DeclaringType, "DeclaringType");
			this.Dump(prop.TypeUsage, "PropertyType");
			this.End("Property");
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000DC810 File Offset: 0x000DAA10
		internal void Dump(RelationshipEndMember end, string name)
		{
			this.Begin(name);
			this.Begin("RelationshipEndMember", "Name", end.Name, "RelationshipMultiplicity", Enum.GetName(typeof(RelationshipMultiplicity), end.RelationshipMultiplicity));
			this.Dump(end.DeclaringType, "DeclaringRelation");
			this.Dump(end.TypeUsage, "EndType");
			this.End("RelationshipEndMember");
			this.End(name);
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x000DC890 File Offset: 0x000DAA90
		internal void Dump(NavigationProperty navProp, string name)
		{
			this.Begin(name);
			this.Begin("NavigationProperty", "Name", navProp.Name, "RelationshipTypeName", navProp.RelationshipType.FullName, "ToEndMemberName", navProp.ToEndMember.Name);
			this.Dump(navProp.DeclaringType, "DeclaringType");
			this.Dump(navProp.TypeUsage, "PropertyType");
			this.End("NavigationProperty");
			this.End(name);
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x000DC910 File Offset: 0x000DAB10
		internal void Dump(DbLambda lambda)
		{
			this.Begin("DbLambda");
			this.Dump(lambda.Variables.Cast<DbExpression>(), "Variables", "Variable");
			this.Dump(lambda.Body, "Body");
			this.End("DbLambda");
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x000DC95F File Offset: 0x000DAB5F
		private void Begin(DbExpression expr)
		{
			this.Begin(expr, new Dictionary<string, object>());
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000DC970 File Offset: 0x000DAB70
		private void Begin(DbExpression expr, Dictionary<string, object> attrs)
		{
			attrs.Add("DbExpressionKind", Enum.GetName(typeof(DbExpressionKind), expr.ExpressionKind));
			this.Begin(expr.GetType().Name, attrs);
			this.Dump(expr.ResultType, "ResultType");
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000DC9C8 File Offset: 0x000DABC8
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

		// Token: 0x060039CF RID: 14799 RVA: 0x000DC9EC File Offset: 0x000DABEC
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

		// Token: 0x060039D0 RID: 14800 RVA: 0x000DCA10 File Offset: 0x000DAC10
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

		// Token: 0x060039D1 RID: 14801 RVA: 0x000DCA40 File Offset: 0x000DAC40
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

		// Token: 0x060039D2 RID: 14802 RVA: 0x000DCA77 File Offset: 0x000DAC77
		private void End(DbExpression expr)
		{
			this.End(expr.GetType().Name);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000DCA8A File Offset: 0x000DAC8A
		private void BeginUnary(DbUnaryExpression e)
		{
			this.Begin(e);
			this.Begin("Argument");
			this.Dump(e.Argument);
			this.End("Argument");
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000DCAB8 File Offset: 0x000DACB8
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

		// Token: 0x060039D5 RID: 14805 RVA: 0x000DCB10 File Offset: 0x000DAD10
		public override void Visit(DbExpression e)
		{
			this.Begin(e);
			this.End(e);
		}

		// Token: 0x060039D6 RID: 14806 RVA: 0x000DCB20 File Offset: 0x000DAD20
		public override void Visit(DbConstantExpression e)
		{
			this.Begin(e, new Dictionary<string, object>
			{
				{
					"Value",
					e.Value
				}
			});
			this.End(e);
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000DCB10 File Offset: 0x000DAD10
		public override void Visit(DbNullExpression e)
		{
			this.Begin(e);
			this.End(e);
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000DCB54 File Offset: 0x000DAD54
		public override void Visit(DbVariableReferenceExpression e)
		{
			this.Begin(e, new Dictionary<string, object>
			{
				{
					"VariableName",
					e.VariableName
				}
			});
			this.End(e);
		}

		// Token: 0x060039D9 RID: 14809 RVA: 0x000DCB88 File Offset: 0x000DAD88
		public override void Visit(DbParameterReferenceExpression e)
		{
			this.Begin(e, new Dictionary<string, object>
			{
				{
					"ParameterName",
					e.ParameterName
				}
			});
			this.End(e);
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000DCBBB File Offset: 0x000DADBB
		public override void Visit(DbFunctionExpression e)
		{
			this.Begin(e);
			this.Dump(e.Function);
			this.Dump(e.Arguments, "Arguments", "Argument");
			this.End(e);
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000DCBED File Offset: 0x000DADED
		public override void Visit(DbLambdaExpression expression)
		{
			this.Begin(expression);
			this.Dump(expression.Lambda);
			this.Dump(expression.Arguments, "Arguments", "Argument");
			this.End(expression);
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000DCC20 File Offset: 0x000DAE20
		public override void Visit(DbPropertyExpression e)
		{
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

		// Token: 0x060039DD RID: 14813 RVA: 0x000DCCA7 File Offset: 0x000DAEA7
		public override void Visit(DbComparisonExpression e)
		{
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000DCCB8 File Offset: 0x000DAEB8
		public override void Visit(DbLikeExpression e)
		{
			this.Begin(e);
			this.Dump(e.Argument, "Argument");
			this.Dump(e.Pattern, "Pattern");
			this.Dump(e.Escape, "Escape");
			this.End(e);
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000DCD08 File Offset: 0x000DAF08
		public override void Visit(DbLimitExpression e)
		{
			this.Begin(e, "WithTies", e.WithTies);
			this.Dump(e.Argument, "Argument");
			this.Dump(e.Limit, "Limit");
			this.End(e);
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbIsNullExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x000DCD65 File Offset: 0x000DAF65
		public override void Visit(DbArithmeticExpression e)
		{
			this.Begin(e);
			this.Dump(e.Arguments, "Arguments", "Argument");
			this.End(e);
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x000DCCA7 File Offset: 0x000DAEA7
		public override void Visit(DbAndExpression e)
		{
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x000DCCA7 File Offset: 0x000DAEA7
		public override void Visit(DbOrExpression e)
		{
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbNotExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039E5 RID: 14821 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbDistinctExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbElementExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039E7 RID: 14823 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbIsEmptyExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039E8 RID: 14824 RVA: 0x000DCCA7 File Offset: 0x000DAEA7
		public override void Visit(DbUnionAllExpression e)
		{
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x060039E9 RID: 14825 RVA: 0x000DCCA7 File Offset: 0x000DAEA7
		public override void Visit(DbIntersectExpression e)
		{
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x060039EA RID: 14826 RVA: 0x000DCCA7 File Offset: 0x000DAEA7
		public override void Visit(DbExceptExpression e)
		{
			this.BeginBinary(e);
			this.End(e);
		}

		// Token: 0x060039EB RID: 14827 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbTreatExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039EC RID: 14828 RVA: 0x000DCD8B File Offset: 0x000DAF8B
		public override void Visit(DbIsOfExpression e)
		{
			this.BeginUnary(e);
			this.Dump(e.OfType, "OfType");
			this.End(e);
		}

		// Token: 0x060039ED RID: 14829 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbCastExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039EE RID: 14830 RVA: 0x000DCDAC File Offset: 0x000DAFAC
		public override void Visit(DbCaseExpression e)
		{
			this.Begin(e);
			this.Dump(e.When, "Whens", "When");
			this.Dump(e.Then, "Thens", "Then");
			this.Dump(e.Else, "Else");
		}

		// Token: 0x060039EF RID: 14831 RVA: 0x000DCDFD File Offset: 0x000DAFFD
		public override void Visit(DbOfTypeExpression e)
		{
			this.BeginUnary(e);
			this.Dump(e.OfType, "OfType");
			this.End(e);
		}

		// Token: 0x060039F0 RID: 14832 RVA: 0x000DCE20 File Offset: 0x000DB020
		public override void Visit(DbNewInstanceExpression e)
		{
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

		// Token: 0x060039F1 RID: 14833 RVA: 0x000DCEF4 File Offset: 0x000DB0F4
		public override void Visit(DbRelationshipNavigationExpression e)
		{
			this.Begin(e);
			this.Dump(e.NavigateFrom, "NavigateFrom");
			this.Dump(e.NavigateTo, "NavigateTo");
			this.Dump(e.Relationship, "Relationship");
			this.Dump(e.NavigationSource, "NavigationSource");
			this.End(e);
		}

		// Token: 0x060039F2 RID: 14834 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbRefExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039F3 RID: 14835 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbDerefExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039F4 RID: 14836 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbRefKeyExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039F5 RID: 14837 RVA: 0x000DCD55 File Offset: 0x000DAF55
		public override void Visit(DbEntityRefExpression e)
		{
			this.BeginUnary(e);
			this.End(e);
		}

		// Token: 0x060039F6 RID: 14838 RVA: 0x000DCF54 File Offset: 0x000DB154
		public override void Visit(DbScanExpression e)
		{
			this.Begin(e);
			this.Begin("Target", "Name", e.Target.Name, "Container", e.Target.EntityContainer.Name);
			this.Dump(e.Target.ElementType, "TargetElementType");
			this.End("Target");
			this.End(e);
		}

		// Token: 0x060039F7 RID: 14839 RVA: 0x000DCFC0 File Offset: 0x000DB1C0
		public override void Visit(DbFilterExpression e)
		{
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Predicate, "Predicate");
			this.End(e);
		}

		// Token: 0x060039F8 RID: 14840 RVA: 0x000DCFF2 File Offset: 0x000DB1F2
		public override void Visit(DbProjectExpression e)
		{
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Projection, "Projection");
			this.End(e);
		}

		// Token: 0x060039F9 RID: 14841 RVA: 0x000DD024 File Offset: 0x000DB224
		public override void Visit(DbCrossJoinExpression e)
		{
			this.Begin(e);
			this.Begin("Inputs");
			foreach (DbExpressionBinding binding in e.Inputs)
			{
				this.Dump(binding, "Input");
			}
			this.End("Inputs");
			this.End(e);
		}

		// Token: 0x060039FA RID: 14842 RVA: 0x000DD09C File Offset: 0x000DB29C
		public override void Visit(DbJoinExpression e)
		{
			this.Begin(e);
			this.Dump(e.Left, "Left");
			this.Dump(e.Right, "Right");
			this.Dump(e.JoinCondition, "JoinCondition");
			this.End(e);
		}

		// Token: 0x060039FB RID: 14843 RVA: 0x000DD0EA File Offset: 0x000DB2EA
		public override void Visit(DbApplyExpression e)
		{
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Apply, "Apply");
			this.End(e);
		}

		// Token: 0x060039FC RID: 14844 RVA: 0x000DD11C File Offset: 0x000DB31C
		public override void Visit(DbGroupByExpression e)
		{
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

		// Token: 0x060039FD RID: 14845 RVA: 0x000DD228 File Offset: 0x000DB428
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

		// Token: 0x060039FE RID: 14846 RVA: 0x000DD2CC File Offset: 0x000DB4CC
		public override void Visit(DbSkipExpression e)
		{
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.SortOrder);
			this.Dump(e.Count, "Count");
			this.End(e);
		}

		// Token: 0x060039FF RID: 14847 RVA: 0x000DD30A File Offset: 0x000DB50A
		public override void Visit(DbSortExpression e)
		{
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.SortOrder);
			this.End(e);
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000DD337 File Offset: 0x000DB537
		public override void Visit(DbQuantifierExpression e)
		{
			this.Begin(e);
			this.Dump(e.Input, "Input");
			this.Dump(e.Predicate, "Predicate");
			this.End(e);
		}
	}
}
