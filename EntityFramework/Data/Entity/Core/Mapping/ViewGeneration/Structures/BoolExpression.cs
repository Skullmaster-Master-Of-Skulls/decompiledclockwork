using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000455 RID: 1109
	internal class BoolExpression : InternalBase
	{
		// Token: 0x060028B4 RID: 10420 RVA: 0x000C5C78 File Offset: 0x000C3E78
		internal static BoolExpression CreateLiteral(BoolLiteral literal, MemberDomainMap memberDomainMap)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> domainBoolExpression = literal.GetDomainBoolExpression(memberDomainMap);
			return new BoolExpression(domainBoolExpression, memberDomainMap);
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000C5C94 File Offset: 0x000C3E94
		internal BoolExpression Create(BoolLiteral literal)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> domainBoolExpression = literal.GetDomainBoolExpression(this.m_memberDomainMap);
			return new BoolExpression(domainBoolExpression, this.m_memberDomainMap);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000C5CBC File Offset: 0x000C3EBC
		internal static BoolExpression CreateNot(BoolExpression expression)
		{
			return new BoolExpression(ExprType.Not, new BoolExpression[]
			{
				expression
			});
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000C5CDB File Offset: 0x000C3EDB
		internal static BoolExpression CreateAnd(params BoolExpression[] children)
		{
			return new BoolExpression(ExprType.And, children);
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x000C5CE4 File Offset: 0x000C3EE4
		internal static BoolExpression CreateOr(params BoolExpression[] children)
		{
			return new BoolExpression(ExprType.Or, children);
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x000C5CF0 File Offset: 0x000C3EF0
		internal static BoolExpression CreateAndNot(BoolExpression e1, BoolExpression e2)
		{
			return BoolExpression.CreateAnd(new BoolExpression[]
			{
				e1,
				BoolExpression.CreateNot(e2)
			});
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000C5D17 File Offset: 0x000C3F17
		internal BoolExpression Create(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			return new BoolExpression(expression, this.m_memberDomainMap);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x000C5D25 File Offset: 0x000C3F25
		private BoolExpression(bool isTrue)
		{
			if (isTrue)
			{
				this.m_tree = TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
				return;
			}
			this.m_tree = FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000C5D48 File Offset: 0x000C3F48
		private BoolExpression(ExprType opType, IEnumerable<BoolExpression> children)
		{
			List<BoolExpression> list = new List<BoolExpression>(children);
			foreach (BoolExpression boolExpression in children)
			{
				if (boolExpression.m_memberDomainMap != null)
				{
					this.m_memberDomainMap = boolExpression.m_memberDomainMap;
					break;
				}
			}
			switch (opType)
			{
			case ExprType.And:
				this.m_tree = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(BoolExpression.ToBoolExprList(list));
				return;
			case ExprType.Not:
				this.m_tree = new NotExpr<DomainConstraint<BoolLiteral, Constant>>(list[0].m_tree);
				return;
			case ExprType.Or:
				this.m_tree = new OrExpr<DomainConstraint<BoolLiteral, Constant>>(BoolExpression.ToBoolExprList(list));
				return;
			default:
				return;
			}
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000C5DFC File Offset: 0x000C3FFC
		internal BoolExpression(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr, MemberDomainMap memberDomainMap)
		{
			this.m_tree = expr;
			this.m_memberDomainMap = memberDomainMap;
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x000C5FC4 File Offset: 0x000C41C4
		internal IEnumerable<BoolExpression> Atoms
		{
			get
			{
				IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> atoms = BoolExpression.TermVisitor.GetTerms(this.m_tree, false);
				foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> atom in atoms)
				{
					yield return new BoolExpression(atom, this.m_memberDomainMap);
				}
				yield break;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x000C5FE4 File Offset: 0x000C41E4
		internal BoolLiteral AsLiteral
		{
			get
			{
				TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr = this.m_tree as TermExpr<DomainConstraint<BoolLiteral, Constant>>;
				if (termExpr == null)
				{
					return null;
				}
				return BoolExpression.GetBoolLiteral(termExpr);
			}
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000C600C File Offset: 0x000C420C
		internal static BoolLiteral GetBoolLiteral(TermExpr<DomainConstraint<BoolLiteral, Constant>> term)
		{
			DomainConstraint<BoolLiteral, Constant> identifier = term.Identifier;
			DomainVariable<BoolLiteral, Constant> variable = identifier.Variable;
			return variable.Identifier;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x000C602D File Offset: 0x000C422D
		internal bool IsTrue
		{
			get
			{
				return this.m_tree.ExprType == ExprType.True;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000C603D File Offset: 0x000C423D
		internal bool IsFalse
		{
			get
			{
				return this.m_tree.ExprType == ExprType.False;
			}
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000C604D File Offset: 0x000C424D
		internal bool IsAlwaysTrue()
		{
			this.InitializeConverter();
			return this.m_converter.Vertex.IsOne();
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x000C6065 File Offset: 0x000C4265
		internal bool IsSatisfiable()
		{
			return !this.IsUnsatisfiable();
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x000C6070 File Offset: 0x000C4270
		internal bool IsUnsatisfiable()
		{
			this.InitializeConverter();
			return this.m_converter.Vertex.IsZero();
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000C6088 File Offset: 0x000C4288
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Tree
		{
			get
			{
				return this.m_tree;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x000C6090 File Offset: 0x000C4290
		internal IEnumerable<DomainConstraint<BoolLiteral, Constant>> VariableConstraints
		{
			get
			{
				return LeafVisitor<DomainConstraint<BoolLiteral, Constant>>.GetLeaves(this.m_tree);
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x000C60A5 File Offset: 0x000C42A5
		internal IEnumerable<DomainVariable<BoolLiteral, Constant>> Variables
		{
			get
			{
				return from domainConstraint in this.VariableConstraints
				select domainConstraint.Variable;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x000C627C File Offset: 0x000C447C
		internal IEnumerable<MemberRestriction> MemberRestrictions
		{
			get
			{
				foreach (DomainVariable<BoolLiteral, Constant> var in this.Variables)
				{
					MemberRestriction variableCondition = var.Identifier as MemberRestriction;
					if (variableCondition != null)
					{
						yield return variableCondition;
					}
				}
				yield break;
			}
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000C6424 File Offset: 0x000C4624
		private static IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> ToBoolExprList(IEnumerable<BoolExpression> nodes)
		{
			foreach (BoolExpression node in nodes)
			{
				yield return node.m_tree;
			}
			yield break;
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x000C644C File Offset: 0x000C464C
		internal bool RepresentsAllTypeConditions
		{
			get
			{
				return this.MemberRestrictions.All((MemberRestriction var) => var is TypeRestriction);
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000C64B8 File Offset: 0x000C46B8
		internal BoolExpression RemapLiterals(Dictionary<BoolLiteral, BoolLiteral> remap)
		{
			BooleanExpressionTermRewriter<DomainConstraint<BoolLiteral, Constant>, DomainConstraint<BoolLiteral, Constant>> visitor = new BooleanExpressionTermRewriter<DomainConstraint<BoolLiteral, Constant>, DomainConstraint<BoolLiteral, Constant>>(delegate(TermExpr<DomainConstraint<BoolLiteral, Constant>> term)
			{
				BoolLiteral boolLiteral;
				if (!remap.TryGetValue(BoolExpression.GetBoolLiteral(term), out boolLiteral))
				{
					return term;
				}
				return boolLiteral.GetDomainBoolExpression(this.m_memberDomainMap);
			});
			return new BoolExpression(this.m_tree.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(visitor), this.m_memberDomainMap);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000C6502 File Offset: 0x000C4702
		internal virtual void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			BoolExpression.RequiredSlotsVisitor.GetRequiredSlots(this.m_tree, projectedSlotMap, requiredSlots);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x000C6511 File Offset: 0x000C4711
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias)
		{
			return BoolExpression.AsEsqlVisitor.AsEsql(this.m_tree, builder, blockAlias);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x000C6520 File Offset: 0x000C4720
		internal DbExpression AsCqt(DbExpression row)
		{
			return BoolExpression.AsCqtVisitor.AsCqt(this.m_tree, row);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000C652E File Offset: 0x000C472E
		internal StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool writeRoundtrippingMessage)
		{
			if (writeRoundtrippingMessage)
			{
				builder.AppendLine(Strings.Viewgen_ConfigurationErrorMsg(blockAlias));
				builder.Append("  ");
			}
			return BoolExpression.AsUserStringVisitor.AsUserString(this.m_tree, builder, blockAlias);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000C6559 File Offset: 0x000C4759
		internal override void ToCompactString(StringBuilder builder)
		{
			BoolExpression.CompactStringVisitor.ToBuilder(this.m_tree, builder);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x000C6568 File Offset: 0x000C4768
		internal BoolExpression RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr = BoolExpression.RemapBoolVisitor.RemapExtentTreeNodes(this.m_tree, this.m_memberDomainMap, remap);
			return new BoolExpression(expr, this.m_memberDomainMap);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x000C6594 File Offset: 0x000C4794
		internal static List<BoolExpression> AddConjunctionToBools(List<BoolExpression> bools, BoolExpression conjunct)
		{
			List<BoolExpression> list = new List<BoolExpression>();
			foreach (BoolExpression boolExpression in bools)
			{
				if (boolExpression == null)
				{
					list.Add(null);
				}
				else
				{
					list.Add(BoolExpression.CreateAnd(new BoolExpression[]
					{
						boolExpression,
						conjunct
					}));
				}
			}
			return list;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x000C660C File Offset: 0x000C480C
		private void InitializeConverter()
		{
			if (this.m_converter != null)
			{
				return;
			}
			this.m_converter = new Converter<DomainConstraint<BoolLiteral, Constant>>(this.m_tree, IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext());
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x000C6634 File Offset: 0x000C4834
		internal BoolExpression MakeCopy()
		{
			return this.Create(this.m_tree.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(BoolExpression._copyVisitorInstance));
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x000C665C File Offset: 0x000C485C
		internal void ExpensiveSimplify()
		{
			if (!this.IsFinal())
			{
				this.m_tree = this.m_tree.Simplify();
				return;
			}
			this.InitializeConverter();
			this.m_tree = this.m_tree.ExpensiveSimplify(out this.m_converter);
			this.FixDomainMap(this.m_memberDomainMap);
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000C66AC File Offset: 0x000C48AC
		internal void FixDomainMap(MemberDomainMap domainMap)
		{
			this.m_tree = BoolExpression.FixRangeVisitor.FixRange(this.m_tree, domainMap);
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000C66C0 File Offset: 0x000C48C0
		private bool IsFinal()
		{
			return this.m_memberDomainMap != null && BoolExpression.IsFinalVisitor.IsFinal(this.m_tree);
		}

		// Token: 0x04000F40 RID: 3904
		private BoolExpr<DomainConstraint<BoolLiteral, Constant>> m_tree;

		// Token: 0x04000F41 RID: 3905
		private readonly MemberDomainMap m_memberDomainMap;

		// Token: 0x04000F42 RID: 3906
		private Converter<DomainConstraint<BoolLiteral, Constant>> m_converter;

		// Token: 0x04000F43 RID: 3907
		internal static readonly IEqualityComparer<BoolExpression> EqualityComparer = new BoolExpression.BoolComparer();

		// Token: 0x04000F44 RID: 3908
		internal static readonly BoolExpression True = new BoolExpression(true);

		// Token: 0x04000F45 RID: 3909
		internal static readonly BoolExpression False = new BoolExpression(false);

		// Token: 0x04000F46 RID: 3910
		private static readonly BoolExpression.CopyVisitor _copyVisitorInstance = new BoolExpression.CopyVisitor();

		// Token: 0x02000456 RID: 1110
		private class CopyVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
		}

		// Token: 0x02000457 RID: 1111
		private class BoolComparer : IEqualityComparer<BoolExpression>
		{
			// Token: 0x060028DD RID: 10461 RVA: 0x000C670B File Offset: 0x000C490B
			public bool Equals(BoolExpression left, BoolExpression right)
			{
				return object.ReferenceEquals(left, right) || (left != null && right != null && left.m_tree.Equals(right.m_tree));
			}

			// Token: 0x060028DE RID: 10462 RVA: 0x000C6731 File Offset: 0x000C4931
			public int GetHashCode(BoolExpression expression)
			{
				return expression.m_tree.GetHashCode();
			}
		}

		// Token: 0x02000458 RID: 1112
		private class FixRangeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060028E0 RID: 10464 RVA: 0x000C6746 File Offset: 0x000C4946
			private FixRangeVisitor(MemberDomainMap memberDomainMap)
			{
				this.m_memberDomainMap = memberDomainMap;
			}

			// Token: 0x060028E1 RID: 10465 RVA: 0x000C6758 File Offset: 0x000C4958
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberDomainMap memberDomainMap)
			{
				BoolExpression.FixRangeVisitor visitor = new BoolExpression.FixRangeVisitor(memberDomainMap);
				return expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(visitor);
			}

			// Token: 0x060028E2 RID: 10466 RVA: 0x000C6778 File Offset: 0x000C4978
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				return boolLiteral.FixRange(expression.Identifier.Range, this.m_memberDomainMap);
			}

			// Token: 0x04000F49 RID: 3913
			private readonly MemberDomainMap m_memberDomainMap;
		}

		// Token: 0x02000459 RID: 1113
		private class IsFinalVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, bool>
		{
			// Token: 0x060028E3 RID: 10467 RVA: 0x000C67A8 File Offset: 0x000C49A8
			internal static bool IsFinal(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolExpression.IsFinalVisitor visitor = new BoolExpression.IsFinalVisitor();
				return expression.Accept<bool>(visitor);
			}

			// Token: 0x060028E4 RID: 10468 RVA: 0x000C67C2 File Offset: 0x000C49C2
			internal override bool VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return true;
			}

			// Token: 0x060028E5 RID: 10469 RVA: 0x000C67C5 File Offset: 0x000C49C5
			internal override bool VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return true;
			}

			// Token: 0x060028E6 RID: 10470 RVA: 0x000C67C8 File Offset: 0x000C49C8
			internal override bool VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				MemberRestriction memberRestriction = boolLiteral as MemberRestriction;
				return memberRestriction == null || memberRestriction.IsComplete;
			}

			// Token: 0x060028E7 RID: 10471 RVA: 0x000C67F1 File Offset: 0x000C49F1
			internal override bool VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return expression.Child.Accept<bool>(this);
			}

			// Token: 0x060028E8 RID: 10472 RVA: 0x000C67FF File Offset: 0x000C49FF
			internal override bool VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression);
			}

			// Token: 0x060028E9 RID: 10473 RVA: 0x000C6808 File Offset: 0x000C4A08
			internal override bool VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression);
			}

			// Token: 0x060028EA RID: 10474 RVA: 0x000C6814 File Offset: 0x000C4A14
			private bool VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				bool flag = true;
				bool result = true;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					if (!(boolExpr is FalseExpr<DomainConstraint<BoolLiteral, Constant>>) && !(boolExpr is TrueExpr<DomainConstraint<BoolLiteral, Constant>>))
					{
						bool flag2 = boolExpr.Accept<bool>(this);
						if (flag)
						{
							result = flag2;
						}
						flag = false;
					}
				}
				return result;
			}
		}

		// Token: 0x0200045A RID: 1114
		private class RemapBoolVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060028EC RID: 10476 RVA: 0x000C6890 File Offset: 0x000C4A90
			private RemapBoolVisitor(MemberDomainMap memberDomainMap, Dictionary<MemberPath, MemberPath> remap)
			{
				this.m_remap = remap;
				this.m_memberDomainMap = memberDomainMap;
			}

			// Token: 0x060028ED RID: 10477 RVA: 0x000C68A8 File Offset: 0x000C4AA8
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> RemapExtentTreeNodes(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberDomainMap memberDomainMap, Dictionary<MemberPath, MemberPath> remap)
			{
				BoolExpression.RemapBoolVisitor visitor = new BoolExpression.RemapBoolVisitor(memberDomainMap, remap);
				return expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(visitor);
			}

			// Token: 0x060028EE RID: 10478 RVA: 0x000C68C8 File Offset: 0x000C4AC8
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				BoolLiteral boolLiteral2 = boolLiteral.RemapBool(this.m_remap);
				return boolLiteral2.GetDomainBoolExpression(this.m_memberDomainMap);
			}

			// Token: 0x04000F4A RID: 3914
			private readonly Dictionary<MemberPath, MemberPath> m_remap;

			// Token: 0x04000F4B RID: 3915
			private readonly MemberDomainMap m_memberDomainMap;
		}

		// Token: 0x0200045B RID: 1115
		private class RequiredSlotsVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060028EF RID: 10479 RVA: 0x000C68F5 File Offset: 0x000C4AF5
			private RequiredSlotsVisitor(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
			{
				this.m_projectedSlotMap = projectedSlotMap;
				this.m_requiredSlots = requiredSlots;
			}

			// Token: 0x060028F0 RID: 10480 RVA: 0x000C690C File Offset: 0x000C4B0C
			internal static void GetRequiredSlots(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
			{
				BoolExpression.RequiredSlotsVisitor visitor = new BoolExpression.RequiredSlotsVisitor(projectedSlotMap, requiredSlots);
				expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(visitor);
			}

			// Token: 0x060028F1 RID: 10481 RVA: 0x000C692C File Offset: 0x000C4B2C
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				boolLiteral.GetRequiredSlots(this.m_projectedSlotMap, this.m_requiredSlots);
				return expression;
			}

			// Token: 0x04000F4C RID: 3916
			private readonly MemberProjectionIndex m_projectedSlotMap;

			// Token: 0x04000F4D RID: 3917
			private readonly bool[] m_requiredSlots;
		}

		// Token: 0x0200045C RID: 1116
		private abstract class AsCqlVisitor<T_Return> : Visitor<DomainConstraint<BoolLiteral, Constant>, T_Return>
		{
			// Token: 0x060028F2 RID: 10482 RVA: 0x000C6953 File Offset: 0x000C4B53
			protected AsCqlVisitor()
			{
				this.m_skipIsNotNull = true;
			}

			// Token: 0x060028F3 RID: 10483 RVA: 0x000C6964 File Offset: 0x000C4B64
			internal override T_Return VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				return this.BooleanLiteralAsCql(boolLiteral, this.m_skipIsNotNull);
			}

			// Token: 0x060028F4 RID: 10484
			protected abstract T_Return BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull);

			// Token: 0x060028F5 RID: 10485 RVA: 0x000C6985 File Offset: 0x000C4B85
			internal override T_Return VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_skipIsNotNull = false;
				return this.NotExprAsCql(expression);
			}

			// Token: 0x060028F6 RID: 10486
			protected abstract T_Return NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression);

			// Token: 0x04000F4E RID: 3918
			private bool m_skipIsNotNull;
		}

		// Token: 0x0200045D RID: 1117
		private sealed class AsEsqlVisitor : BoolExpression.AsCqlVisitor<StringBuilder>
		{
			// Token: 0x060028F7 RID: 10487 RVA: 0x000C6998 File Offset: 0x000C4B98
			internal static StringBuilder AsEsql(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder, string blockAlias)
			{
				BoolExpression.AsEsqlVisitor visitor = new BoolExpression.AsEsqlVisitor(builder, blockAlias);
				return expression.Accept<StringBuilder>(visitor);
			}

			// Token: 0x060028F8 RID: 10488 RVA: 0x000C69B4 File Offset: 0x000C4BB4
			private AsEsqlVisitor(StringBuilder builder, string blockAlias)
			{
				this.m_builder = builder;
				this.m_blockAlias = blockAlias;
			}

			// Token: 0x060028F9 RID: 10489 RVA: 0x000C69CA File Offset: 0x000C4BCA
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x060028FA RID: 10490 RVA: 0x000C69E3 File Offset: 0x000C4BE3
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x060028FB RID: 10491 RVA: 0x000C69FC File Offset: 0x000C4BFC
			protected override StringBuilder BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull)
			{
				return literal.AsEsql(this.m_builder, this.m_blockAlias, skipIsNotNull);
			}

			// Token: 0x060028FC RID: 10492 RVA: 0x000C6A11 File Offset: 0x000C4C11
			protected override StringBuilder NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("NOT(");
				expression.Child.Accept<StringBuilder>(this);
				this.m_builder.Append(")");
				return this.m_builder;
			}

			// Token: 0x060028FD RID: 10493 RVA: 0x000C6A48 File Offset: 0x000C4C48
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.And);
			}

			// Token: 0x060028FE RID: 10494 RVA: 0x000C6A52 File Offset: 0x000C4C52
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.Or);
			}

			// Token: 0x060028FF RID: 10495 RVA: 0x000C6A5C File Offset: 0x000C4C5C
			private StringBuilder VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression, ExprType kind)
			{
				this.m_builder.Append('(');
				bool flag = true;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					if (!flag)
					{
						if (kind == ExprType.And)
						{
							this.m_builder.Append(" AND ");
						}
						else
						{
							this.m_builder.Append(" OR ");
						}
					}
					flag = false;
					boolExpr.Accept<StringBuilder>(this);
				}
				this.m_builder.Append(')');
				return this.m_builder;
			}

			// Token: 0x04000F4F RID: 3919
			private readonly StringBuilder m_builder;

			// Token: 0x04000F50 RID: 3920
			private readonly string m_blockAlias;
		}

		// Token: 0x0200045E RID: 1118
		private sealed class AsCqtVisitor : BoolExpression.AsCqlVisitor<DbExpression>
		{
			// Token: 0x06002900 RID: 10496 RVA: 0x000C6B00 File Offset: 0x000C4D00
			internal static DbExpression AsCqt(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, DbExpression row)
			{
				BoolExpression.AsCqtVisitor visitor = new BoolExpression.AsCqtVisitor(row);
				return expression.Accept<DbExpression>(visitor);
			}

			// Token: 0x06002901 RID: 10497 RVA: 0x000C6B1B File Offset: 0x000C4D1B
			private AsCqtVisitor(DbExpression row)
			{
				this.m_row = row;
			}

			// Token: 0x06002902 RID: 10498 RVA: 0x000C6B2A File Offset: 0x000C4D2A
			internal override DbExpression VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return DbExpressionBuilder.True;
			}

			// Token: 0x06002903 RID: 10499 RVA: 0x000C6B31 File Offset: 0x000C4D31
			internal override DbExpression VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return DbExpressionBuilder.False;
			}

			// Token: 0x06002904 RID: 10500 RVA: 0x000C6B38 File Offset: 0x000C4D38
			protected override DbExpression BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull)
			{
				return literal.AsCqt(this.m_row, skipIsNotNull);
			}

			// Token: 0x06002905 RID: 10501 RVA: 0x000C6B48 File Offset: 0x000C4D48
			protected override DbExpression NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				DbExpression argument = expression.Child.Accept<DbExpression>(this);
				return argument.Not();
			}

			// Token: 0x06002906 RID: 10502 RVA: 0x000C6B68 File Offset: 0x000C4D68
			internal override DbExpression VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.And));
			}

			// Token: 0x06002907 RID: 10503 RVA: 0x000C6B8C File Offset: 0x000C4D8C
			internal override DbExpression VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
			}

			// Token: 0x06002908 RID: 10504 RVA: 0x000C6BB0 File Offset: 0x000C4DB0
			private DbExpression VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression, Func<DbExpression, DbExpression, DbExpression> op)
			{
				DbExpression dbExpression = null;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					if (dbExpression == null)
					{
						dbExpression = boolExpr.Accept<DbExpression>(this);
					}
					else
					{
						dbExpression = op(dbExpression, boolExpr.Accept<DbExpression>(this));
					}
				}
				return dbExpression;
			}

			// Token: 0x04000F51 RID: 3921
			private readonly DbExpression m_row;
		}

		// Token: 0x0200045F RID: 1119
		private class AsUserStringVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, StringBuilder>
		{
			// Token: 0x06002909 RID: 10505 RVA: 0x000C6C1C File Offset: 0x000C4E1C
			private AsUserStringVisitor(StringBuilder builder, string blockAlias)
			{
				this.m_builder = builder;
				this.m_blockAlias = blockAlias;
				this.m_skipIsNotNull = true;
			}

			// Token: 0x0600290A RID: 10506 RVA: 0x000C6C3C File Offset: 0x000C4E3C
			internal static StringBuilder AsUserString(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder, string blockAlias)
			{
				BoolExpression.AsUserStringVisitor visitor = new BoolExpression.AsUserStringVisitor(builder, blockAlias);
				return expression.Accept<StringBuilder>(visitor);
			}

			// Token: 0x0600290B RID: 10507 RVA: 0x000C6C58 File Offset: 0x000C4E58
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x0600290C RID: 10508 RVA: 0x000C6C71 File Offset: 0x000C4E71
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x0600290D RID: 10509 RVA: 0x000C6C8C File Offset: 0x000C4E8C
			internal override StringBuilder VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				if (boolLiteral is ScalarRestriction || boolLiteral is TypeRestriction)
				{
					return boolLiteral.AsUserString(this.m_builder, Strings.ViewGen_EntityInstanceToken, this.m_skipIsNotNull);
				}
				return boolLiteral.AsUserString(this.m_builder, this.m_blockAlias, this.m_skipIsNotNull);
			}

			// Token: 0x0600290E RID: 10510 RVA: 0x000C6CE0 File Offset: 0x000C4EE0
			internal override StringBuilder VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_skipIsNotNull = false;
				TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr = expression.Child as TermExpr<DomainConstraint<BoolLiteral, Constant>>;
				if (termExpr != null)
				{
					BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(termExpr);
					return boolLiteral.AsNegatedUserString(this.m_builder, this.m_blockAlias, this.m_skipIsNotNull);
				}
				this.m_builder.Append("NOT(");
				expression.Child.Accept<StringBuilder>(this);
				this.m_builder.Append(")");
				return this.m_builder;
			}

			// Token: 0x0600290F RID: 10511 RVA: 0x000C6D58 File Offset: 0x000C4F58
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.And);
			}

			// Token: 0x06002910 RID: 10512 RVA: 0x000C6D62 File Offset: 0x000C4F62
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.Or);
			}

			// Token: 0x06002911 RID: 10513 RVA: 0x000C6D6C File Offset: 0x000C4F6C
			private StringBuilder VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression, ExprType kind)
			{
				this.m_builder.Append('(');
				bool flag = true;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					if (!flag)
					{
						if (kind == ExprType.And)
						{
							this.m_builder.Append(" AND ");
						}
						else
						{
							this.m_builder.Append(" OR ");
						}
					}
					flag = false;
					boolExpr.Accept<StringBuilder>(this);
				}
				this.m_builder.Append(')');
				return this.m_builder;
			}

			// Token: 0x04000F52 RID: 3922
			private readonly StringBuilder m_builder;

			// Token: 0x04000F53 RID: 3923
			private readonly string m_blockAlias;

			// Token: 0x04000F54 RID: 3924
			private bool m_skipIsNotNull;
		}

		// Token: 0x02000460 RID: 1120
		private class TermVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>
		{
			// Token: 0x06002912 RID: 10514 RVA: 0x000C6E10 File Offset: 0x000C5010
			[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "allowAllOperators", Scope = "member", Target = "System.Data.Entity.Core.Mapping.ViewGeneration.Structures.BoolExpression+TermVisitor.#.ctor(System.Boolean)")]
			private TermVisitor(bool allowAllOperators)
			{
			}

			// Token: 0x06002913 RID: 10515 RVA: 0x000C6E18 File Offset: 0x000C5018
			internal static IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> GetTerms(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, bool allowAllOperators)
			{
				BoolExpression.TermVisitor visitor = new BoolExpression.TermVisitor(allowAllOperators);
				return expression.Accept<IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>(visitor);
			}

			// Token: 0x06002914 RID: 10516 RVA: 0x000C6ED4 File Offset: 0x000C50D4
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield break;
			}

			// Token: 0x06002915 RID: 10517 RVA: 0x000C6F94 File Offset: 0x000C5194
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield break;
			}

			// Token: 0x06002916 RID: 10518 RVA: 0x000C7088 File Offset: 0x000C5288
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield return expression;
				yield break;
			}

			// Token: 0x06002917 RID: 10519 RVA: 0x000C70AC File Offset: 0x000C52AC
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}

			// Token: 0x06002918 RID: 10520 RVA: 0x000C72DC File Offset: 0x000C54DC
			private IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTreeNode(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> child in expression.Children)
				{
					foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> result in child.Accept<IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>(this))
					{
						yield return result;
					}
				}
				yield break;
			}

			// Token: 0x06002919 RID: 10521 RVA: 0x000C7300 File Offset: 0x000C5500
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}

			// Token: 0x0600291A RID: 10522 RVA: 0x000C7309 File Offset: 0x000C5509
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}
		}

		// Token: 0x02000461 RID: 1121
		private class CompactStringVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, StringBuilder>
		{
			// Token: 0x0600291B RID: 10523 RVA: 0x000C7312 File Offset: 0x000C5512
			private CompactStringVisitor(StringBuilder builder)
			{
				this.m_builder = builder;
			}

			// Token: 0x0600291C RID: 10524 RVA: 0x000C7324 File Offset: 0x000C5524
			internal static StringBuilder ToBuilder(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder)
			{
				BoolExpression.CompactStringVisitor visitor = new BoolExpression.CompactStringVisitor(builder);
				return expression.Accept<StringBuilder>(visitor);
			}

			// Token: 0x0600291D RID: 10525 RVA: 0x000C733F File Offset: 0x000C553F
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x0600291E RID: 10526 RVA: 0x000C7358 File Offset: 0x000C5558
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x0600291F RID: 10527 RVA: 0x000C7374 File Offset: 0x000C5574
			internal override StringBuilder VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				boolLiteral.ToCompactString(this.m_builder);
				return this.m_builder;
			}

			// Token: 0x06002920 RID: 10528 RVA: 0x000C739A File Offset: 0x000C559A
			internal override StringBuilder VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("NOT(");
				expression.Child.Accept<StringBuilder>(this);
				this.m_builder.Append(")");
				return this.m_builder;
			}

			// Token: 0x06002921 RID: 10529 RVA: 0x000C73D1 File Offset: 0x000C55D1
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, "AND");
			}

			// Token: 0x06002922 RID: 10530 RVA: 0x000C73DF File Offset: 0x000C55DF
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, "OR");
			}

			// Token: 0x06002923 RID: 10531 RVA: 0x000C73F0 File Offset: 0x000C55F0
			private StringBuilder VisitAndOr(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression, string opAsString)
			{
				List<string> list = new List<string>();
				StringBuilder builder = this.m_builder;
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					this.m_builder = new StringBuilder();
					boolExpr.Accept<StringBuilder>(this);
					list.Add(this.m_builder.ToString());
				}
				this.m_builder = builder;
				this.m_builder.Append('(');
				StringUtil.ToSeparatedStringSorted(this.m_builder, list, " " + opAsString + " ");
				this.m_builder.Append(')');
				return this.m_builder;
			}

			// Token: 0x04000F55 RID: 3925
			private StringBuilder m_builder;
		}
	}
}
