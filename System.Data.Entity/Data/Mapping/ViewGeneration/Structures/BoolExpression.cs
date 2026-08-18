using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Common.Utils.Boolean;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200029B RID: 667
	internal class BoolExpression : InternalBase
	{
		// Token: 0x0600279C RID: 10140 RVA: 0x00099FF8 File Offset: 0x000981F8
		internal static BoolExpression CreateLiteral(BoolLiteral literal, MemberDomainMap memberDomainMap)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> domainBoolExpression = literal.GetDomainBoolExpression(memberDomainMap);
			return new BoolExpression(domainBoolExpression, memberDomainMap);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x0009A014 File Offset: 0x00098214
		internal BoolExpression Create(BoolLiteral literal)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> domainBoolExpression = literal.GetDomainBoolExpression(this.m_memberDomainMap);
			return new BoolExpression(domainBoolExpression, this.m_memberDomainMap);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0009A03A File Offset: 0x0009823A
		internal static BoolExpression CreateNot(BoolExpression expression)
		{
			return new BoolExpression(ExprType.Not, new BoolExpression[]
			{
				expression
			});
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x0009A04C File Offset: 0x0009824C
		internal static BoolExpression CreateAnd(params BoolExpression[] children)
		{
			return new BoolExpression(ExprType.And, children);
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x0009A055 File Offset: 0x00098255
		internal static BoolExpression CreateOr(params BoolExpression[] children)
		{
			return new BoolExpression(ExprType.Or, children);
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x0009A05E File Offset: 0x0009825E
		internal static BoolExpression CreateAndNot(BoolExpression e1, BoolExpression e2)
		{
			return BoolExpression.CreateAnd(new BoolExpression[]
			{
				e1,
				BoolExpression.CreateNot(e2)
			});
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x0009A078 File Offset: 0x00098278
		internal BoolExpression Create(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			return new BoolExpression(expression, this.m_memberDomainMap);
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0009A086 File Offset: 0x00098286
		private BoolExpression(bool isTrue)
		{
			if (isTrue)
			{
				this.m_tree = TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
				return;
			}
			this.m_tree = FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x0009A0A8 File Offset: 0x000982A8
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
				this.m_tree = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(this.ToBoolExprList(list));
				return;
			case ExprType.Not:
				this.m_tree = new NotExpr<DomainConstraint<BoolLiteral, Constant>>(list[0].m_tree);
				return;
			case ExprType.Or:
				this.m_tree = new OrExpr<DomainConstraint<BoolLiteral, Constant>>(this.ToBoolExprList(list));
				return;
			default:
				return;
			}
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x0009A15C File Offset: 0x0009835C
		internal BoolExpression(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr, MemberDomainMap memberDomainMap)
		{
			this.m_tree = expr;
			this.m_memberDomainMap = memberDomainMap;
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x0009A174 File Offset: 0x00098374
		internal IEnumerable<BoolExpression> Atoms
		{
			get
			{
				IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> terms = BoolExpression.TermVisitor.GetTerms(this.m_tree, false);
				foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> expr in terms)
				{
					yield return new BoolExpression(expr, this.m_memberDomainMap);
				}
				IEnumerator<TermExpr<DomainConstraint<BoolLiteral, Constant>>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060027A7 RID: 10151 RVA: 0x0009A194 File Offset: 0x00098394
		internal IEnumerable<BoolLiteral> Leaves
		{
			get
			{
				IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> terms = BoolExpression.TermVisitor.GetTerms(this.m_tree, true);
				foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr in terms)
				{
					yield return termExpr.Identifier.Variable.Identifier;
				}
				IEnumerator<TermExpr<DomainConstraint<BoolLiteral, Constant>>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x0009A1B4 File Offset: 0x000983B4
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

		// Token: 0x060027A9 RID: 10153 RVA: 0x0009A1DC File Offset: 0x000983DC
		internal static BoolLiteral GetBoolLiteral(TermExpr<DomainConstraint<BoolLiteral, Constant>> term)
		{
			DomainConstraint<BoolLiteral, Constant> identifier = term.Identifier;
			DomainVariable<BoolLiteral, Constant> variable = identifier.Variable;
			return variable.Identifier;
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x0009A1FD File Offset: 0x000983FD
		internal bool IsTrue
		{
			get
			{
				return this.m_tree.ExprType == ExprType.True;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x0009A20D File Offset: 0x0009840D
		internal bool IsFalse
		{
			get
			{
				return this.m_tree.ExprType == ExprType.False;
			}
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x0009A21D File Offset: 0x0009841D
		internal bool IsAlwaysTrue()
		{
			this.InitializeConverter();
			return this.m_converter.Vertex.IsOne();
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0009A235 File Offset: 0x00098435
		internal bool IsSatisfiable()
		{
			return !this.IsUnsatisfiable();
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x0009A240 File Offset: 0x00098440
		internal bool IsUnsatisfiable()
		{
			this.InitializeConverter();
			return this.m_converter.Vertex.IsZero();
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x0009A258 File Offset: 0x00098458
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Tree
		{
			get
			{
				return this.m_tree;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x0009A260 File Offset: 0x00098460
		internal IEnumerable<DomainConstraint<BoolLiteral, Constant>> VariableConstraints
		{
			get
			{
				return LeafVisitor<DomainConstraint<BoolLiteral, Constant>>.GetLeaves(this.m_tree);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x0009A26D File Offset: 0x0009846D
		internal IEnumerable<DomainVariable<BoolLiteral, Constant>> Variables
		{
			get
			{
				return from domainConstraint in this.VariableConstraints
				select domainConstraint.Variable;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x0009A29C File Offset: 0x0009849C
		internal IEnumerable<MemberRestriction> MemberRestrictions
		{
			get
			{
				foreach (DomainVariable<BoolLiteral, Constant> domainVariable in this.Variables)
				{
					MemberRestriction memberRestriction = domainVariable.Identifier as MemberRestriction;
					if (memberRestriction != null)
					{
						yield return memberRestriction;
					}
				}
				IEnumerator<DomainVariable<BoolLiteral, Constant>> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x0009A2B9 File Offset: 0x000984B9
		private IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> ToBoolExprList(IEnumerable<BoolExpression> nodes)
		{
			foreach (BoolExpression boolExpression in nodes)
			{
				yield return boolExpression.m_tree;
			}
			IEnumerator<BoolExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x060027B4 RID: 10164 RVA: 0x0009A2C9 File Offset: 0x000984C9
		internal bool RepresentsAllTypeConditions
		{
			get
			{
				return this.MemberRestrictions.All((MemberRestriction var) => var is TypeRestriction);
			}
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x0009A2F8 File Offset: 0x000984F8
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

		// Token: 0x060027B6 RID: 10166 RVA: 0x0009A342 File Offset: 0x00098542
		internal virtual void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			BoolExpression.RequiredSlotsVisitor.GetRequiredSlots(this.m_tree, projectedSlotMap, requiredSlots);
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x0009A351 File Offset: 0x00098551
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias)
		{
			return BoolExpression.AsEsqlVisitor.AsEsql(this.m_tree, builder, blockAlias);
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x0009A360 File Offset: 0x00098560
		internal DbExpression AsCqt(DbExpression row)
		{
			return BoolExpression.AsCqtVisitor.AsCqt(this.m_tree, row);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x0009A36E File Offset: 0x0009856E
		internal StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool writeRoundtrippingMessage)
		{
			if (writeRoundtrippingMessage)
			{
				builder.AppendLine(Strings.Viewgen_ConfigurationErrorMsg(blockAlias));
				builder.Append("  ");
			}
			return BoolExpression.AsUserStringVisitor.AsUserString(this.m_tree, builder, blockAlias);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x0009A399 File Offset: 0x00098599
		internal override void ToCompactString(StringBuilder builder)
		{
			BoolExpression.CompactStringVisitor.ToBuilder(this.m_tree, builder);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0009A3A8 File Offset: 0x000985A8
		internal BoolExpression RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr = BoolExpression.RemapBoolVisitor.RemapExtentTreeNodes(this.m_tree, this.m_memberDomainMap, remap);
			return new BoolExpression(expr, this.m_memberDomainMap);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0009A3D4 File Offset: 0x000985D4
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

		// Token: 0x060027BD RID: 10173 RVA: 0x0009A448 File Offset: 0x00098648
		private void InitializeConverter()
		{
			if (this.m_converter != null)
			{
				return;
			}
			this.m_converter = new Converter<DomainConstraint<BoolLiteral, Constant>>(this.m_tree, IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext());
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x0009A470 File Offset: 0x00098670
		internal BoolExpression MakeCopy()
		{
			return this.Create(this.m_tree.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(BoolExpression.CopyVisitorInstance));
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0009A498 File Offset: 0x00098698
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

		// Token: 0x060027C0 RID: 10176 RVA: 0x0009A4E8 File Offset: 0x000986E8
		internal void FixDomainMap(MemberDomainMap domainMap)
		{
			this.m_tree = BoolExpression.FixRangeVisitor.FixRange(this.m_tree, domainMap);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x0009A4FC File Offset: 0x000986FC
		private bool IsFinal()
		{
			return this.m_memberDomainMap != null && BoolExpression.IsFinalVisitor.IsFinal(this.m_tree);
		}

		// Token: 0x04001228 RID: 4648
		private BoolExpr<DomainConstraint<BoolLiteral, Constant>> m_tree;

		// Token: 0x04001229 RID: 4649
		private readonly MemberDomainMap m_memberDomainMap;

		// Token: 0x0400122A RID: 4650
		private Converter<DomainConstraint<BoolLiteral, Constant>> m_converter;

		// Token: 0x0400122B RID: 4651
		internal static readonly IEqualityComparer<BoolExpression> EqualityComparer = new BoolExpression.BoolComparer();

		// Token: 0x0400122C RID: 4652
		internal static readonly BoolExpression True = new BoolExpression(true);

		// Token: 0x0400122D RID: 4653
		internal static readonly BoolExpression False = new BoolExpression(false);

		// Token: 0x0400122E RID: 4654
		private static readonly BoolExpression.CopyVisitor CopyVisitorInstance = new BoolExpression.CopyVisitor();

		// Token: 0x020005BF RID: 1471
		private class CopyVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
		}

		// Token: 0x020005C0 RID: 1472
		private class BoolComparer : IEqualityComparer<BoolExpression>
		{
			// Token: 0x060040D4 RID: 16596 RVA: 0x000EDDA6 File Offset: 0x000EBFA6
			public bool Equals(BoolExpression left, BoolExpression right)
			{
				return left == right || (left != null && right != null && left.m_tree.Equals(right.m_tree));
			}

			// Token: 0x060040D5 RID: 16597 RVA: 0x000EDDC7 File Offset: 0x000EBFC7
			public int GetHashCode(BoolExpression expression)
			{
				return expression.m_tree.GetHashCode();
			}
		}

		// Token: 0x020005C1 RID: 1473
		private class FixRangeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060040D7 RID: 16599 RVA: 0x000EDDD4 File Offset: 0x000EBFD4
			private FixRangeVisitor(MemberDomainMap memberDomainMap)
			{
				this.m_memberDomainMap = memberDomainMap;
			}

			// Token: 0x060040D8 RID: 16600 RVA: 0x000EDDE4 File Offset: 0x000EBFE4
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberDomainMap memberDomainMap)
			{
				BoolExpression.FixRangeVisitor visitor = new BoolExpression.FixRangeVisitor(memberDomainMap);
				return expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(visitor);
			}

			// Token: 0x060040D9 RID: 16601 RVA: 0x000EDE04 File Offset: 0x000EC004
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				return boolLiteral.FixRange(expression.Identifier.Range, this.m_memberDomainMap);
			}

			// Token: 0x04001D3C RID: 7484
			private MemberDomainMap m_memberDomainMap;
		}

		// Token: 0x020005C2 RID: 1474
		private class IsFinalVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, bool>
		{
			// Token: 0x060040DA RID: 16602 RVA: 0x000EDE34 File Offset: 0x000EC034
			internal static bool IsFinal(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolExpression.IsFinalVisitor visitor = new BoolExpression.IsFinalVisitor();
				return expression.Accept<bool>(visitor);
			}

			// Token: 0x060040DB RID: 16603 RVA: 0x00017938 File Offset: 0x00015B38
			internal override bool VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return true;
			}

			// Token: 0x060040DC RID: 16604 RVA: 0x00017938 File Offset: 0x00015B38
			internal override bool VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return true;
			}

			// Token: 0x060040DD RID: 16605 RVA: 0x000EDE50 File Offset: 0x000EC050
			internal override bool VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				MemberRestriction memberRestriction = boolLiteral as MemberRestriction;
				return memberRestriction == null || memberRestriction.IsComplete;
			}

			// Token: 0x060040DE RID: 16606 RVA: 0x000EDE79 File Offset: 0x000EC079
			internal override bool VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return expression.Child.Accept<bool>(this);
			}

			// Token: 0x060040DF RID: 16607 RVA: 0x000EDE87 File Offset: 0x000EC087
			internal override bool VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression);
			}

			// Token: 0x060040E0 RID: 16608 RVA: 0x000EDE87 File Offset: 0x000EC087
			internal override bool VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression);
			}

			// Token: 0x060040E1 RID: 16609 RVA: 0x000EDE90 File Offset: 0x000EC090
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

		// Token: 0x020005C3 RID: 1475
		private class RemapBoolVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060040E3 RID: 16611 RVA: 0x000EDF0C File Offset: 0x000EC10C
			private RemapBoolVisitor(MemberDomainMap memberDomainMap, Dictionary<MemberPath, MemberPath> remap)
			{
				this.m_remap = remap;
				this.m_memberDomainMap = memberDomainMap;
			}

			// Token: 0x060040E4 RID: 16612 RVA: 0x000EDF24 File Offset: 0x000EC124
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> RemapExtentTreeNodes(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberDomainMap memberDomainMap, Dictionary<MemberPath, MemberPath> remap)
			{
				BoolExpression.RemapBoolVisitor visitor = new BoolExpression.RemapBoolVisitor(memberDomainMap, remap);
				return expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(visitor);
			}

			// Token: 0x060040E5 RID: 16613 RVA: 0x000EDF44 File Offset: 0x000EC144
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				BoolLiteral boolLiteral2 = boolLiteral.RemapBool(this.m_remap);
				return boolLiteral2.GetDomainBoolExpression(this.m_memberDomainMap);
			}

			// Token: 0x04001D3D RID: 7485
			private Dictionary<MemberPath, MemberPath> m_remap;

			// Token: 0x04001D3E RID: 7486
			private MemberDomainMap m_memberDomainMap;
		}

		// Token: 0x020005C4 RID: 1476
		private class RequiredSlotsVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
		{
			// Token: 0x060040E6 RID: 16614 RVA: 0x000EDF71 File Offset: 0x000EC171
			private RequiredSlotsVisitor(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
			{
				this.m_projectedSlotMap = projectedSlotMap;
				this.m_requiredSlots = requiredSlots;
			}

			// Token: 0x060040E7 RID: 16615 RVA: 0x000EDF88 File Offset: 0x000EC188
			internal static void GetRequiredSlots(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
			{
				BoolExpression.RequiredSlotsVisitor visitor = new BoolExpression.RequiredSlotsVisitor(projectedSlotMap, requiredSlots);
				expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(visitor);
			}

			// Token: 0x060040E8 RID: 16616 RVA: 0x000EDFA8 File Offset: 0x000EC1A8
			internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				boolLiteral.GetRequiredSlots(this.m_projectedSlotMap, this.m_requiredSlots);
				return expression;
			}

			// Token: 0x04001D3F RID: 7487
			private MemberProjectionIndex m_projectedSlotMap;

			// Token: 0x04001D40 RID: 7488
			private bool[] m_requiredSlots;
		}

		// Token: 0x020005C5 RID: 1477
		private sealed class AsEsqlVisitor : BoolExpression.AsCqlVisitor<StringBuilder>
		{
			// Token: 0x060040E9 RID: 16617 RVA: 0x000EDFD0 File Offset: 0x000EC1D0
			internal static StringBuilder AsEsql(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder, string blockAlias)
			{
				BoolExpression.AsEsqlVisitor visitor = new BoolExpression.AsEsqlVisitor(builder, blockAlias);
				return expression.Accept<StringBuilder>(visitor);
			}

			// Token: 0x060040EA RID: 16618 RVA: 0x000EDFEC File Offset: 0x000EC1EC
			private AsEsqlVisitor(StringBuilder builder, string blockAlias)
			{
				this.m_builder = builder;
				this.m_blockAlias = blockAlias;
			}

			// Token: 0x060040EB RID: 16619 RVA: 0x000EE002 File Offset: 0x000EC202
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x060040EC RID: 16620 RVA: 0x000EE01B File Offset: 0x000EC21B
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x060040ED RID: 16621 RVA: 0x000EE034 File Offset: 0x000EC234
			protected override StringBuilder BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull)
			{
				return literal.AsEsql(this.m_builder, this.m_blockAlias, skipIsNotNull);
			}

			// Token: 0x060040EE RID: 16622 RVA: 0x000EE049 File Offset: 0x000EC249
			protected override StringBuilder NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("NOT(");
				expression.Child.Accept<StringBuilder>(this);
				this.m_builder.Append(")");
				return this.m_builder;
			}

			// Token: 0x060040EF RID: 16623 RVA: 0x000EE080 File Offset: 0x000EC280
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.And);
			}

			// Token: 0x060040F0 RID: 16624 RVA: 0x000EE08A File Offset: 0x000EC28A
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.Or);
			}

			// Token: 0x060040F1 RID: 16625 RVA: 0x000EE094 File Offset: 0x000EC294
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

			// Token: 0x04001D41 RID: 7489
			private readonly StringBuilder m_builder;

			// Token: 0x04001D42 RID: 7490
			private readonly string m_blockAlias;
		}

		// Token: 0x020005C6 RID: 1478
		private sealed class AsCqtVisitor : BoolExpression.AsCqlVisitor<DbExpression>
		{
			// Token: 0x060040F2 RID: 16626 RVA: 0x000EE138 File Offset: 0x000EC338
			internal static DbExpression AsCqt(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, DbExpression row)
			{
				BoolExpression.AsCqtVisitor visitor = new BoolExpression.AsCqtVisitor(row);
				return expression.Accept<DbExpression>(visitor);
			}

			// Token: 0x060040F3 RID: 16627 RVA: 0x000EE153 File Offset: 0x000EC353
			private AsCqtVisitor(DbExpression row)
			{
				this.m_row = row;
			}

			// Token: 0x060040F4 RID: 16628 RVA: 0x000EE162 File Offset: 0x000EC362
			internal override DbExpression VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return DbExpressionBuilder.True;
			}

			// Token: 0x060040F5 RID: 16629 RVA: 0x000EE169 File Offset: 0x000EC369
			internal override DbExpression VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return DbExpressionBuilder.False;
			}

			// Token: 0x060040F6 RID: 16630 RVA: 0x000EE170 File Offset: 0x000EC370
			protected override DbExpression BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull)
			{
				return literal.AsCqt(this.m_row, skipIsNotNull);
			}

			// Token: 0x060040F7 RID: 16631 RVA: 0x000EE180 File Offset: 0x000EC380
			protected override DbExpression NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				DbExpression argument = expression.Child.Accept<DbExpression>(this);
				return argument.Not();
			}

			// Token: 0x060040F8 RID: 16632 RVA: 0x000EE1A0 File Offset: 0x000EC3A0
			internal override DbExpression VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.And));
			}

			// Token: 0x060040F9 RID: 16633 RVA: 0x000EE1C4 File Offset: 0x000EC3C4
			internal override DbExpression VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, new Func<DbExpression, DbExpression, DbExpression>(DbExpressionBuilder.Or));
			}

			// Token: 0x060040FA RID: 16634 RVA: 0x000EE1E8 File Offset: 0x000EC3E8
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

			// Token: 0x04001D43 RID: 7491
			private readonly DbExpression m_row;
		}

		// Token: 0x020005C7 RID: 1479
		private abstract class AsCqlVisitor<T_Return> : Visitor<DomainConstraint<BoolLiteral, Constant>, T_Return>
		{
			// Token: 0x060040FB RID: 16635 RVA: 0x000EE254 File Offset: 0x000EC454
			protected AsCqlVisitor()
			{
				this.m_skipIsNotNull = true;
			}

			// Token: 0x060040FC RID: 16636 RVA: 0x000EE264 File Offset: 0x000EC464
			internal override T_Return VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				return this.BooleanLiteralAsCql(boolLiteral, this.m_skipIsNotNull);
			}

			// Token: 0x060040FD RID: 16637
			protected abstract T_Return BooleanLiteralAsCql(BoolLiteral literal, bool skipIsNotNull);

			// Token: 0x060040FE RID: 16638 RVA: 0x000EE285 File Offset: 0x000EC485
			internal override T_Return VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_skipIsNotNull = false;
				return this.NotExprAsCql(expression);
			}

			// Token: 0x060040FF RID: 16639
			protected abstract T_Return NotExprAsCql(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression);

			// Token: 0x04001D44 RID: 7492
			private bool m_skipIsNotNull;
		}

		// Token: 0x020005C8 RID: 1480
		private class AsUserStringVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, StringBuilder>
		{
			// Token: 0x06004100 RID: 16640 RVA: 0x000EE295 File Offset: 0x000EC495
			private AsUserStringVisitor(StringBuilder builder, string blockAlias)
			{
				this.m_builder = builder;
				this.m_blockAlias = blockAlias;
				this.m_skipIsNotNull = true;
			}

			// Token: 0x06004101 RID: 16641 RVA: 0x000EE2B4 File Offset: 0x000EC4B4
			internal static StringBuilder AsUserString(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder, string blockAlias)
			{
				BoolExpression.AsUserStringVisitor visitor = new BoolExpression.AsUserStringVisitor(builder, blockAlias);
				return expression.Accept<StringBuilder>(visitor);
			}

			// Token: 0x06004102 RID: 16642 RVA: 0x000EE2D0 File Offset: 0x000EC4D0
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x06004103 RID: 16643 RVA: 0x000EE2E9 File Offset: 0x000EC4E9
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x06004104 RID: 16644 RVA: 0x000EE304 File Offset: 0x000EC504
			internal override StringBuilder VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				if (boolLiteral is ScalarRestriction || boolLiteral is TypeRestriction)
				{
					return boolLiteral.AsUserString(this.m_builder, Strings.ViewGen_EntityInstanceToken, this.m_skipIsNotNull);
				}
				return boolLiteral.AsUserString(this.m_builder, this.m_blockAlias, this.m_skipIsNotNull);
			}

			// Token: 0x06004105 RID: 16645 RVA: 0x000EE358 File Offset: 0x000EC558
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

			// Token: 0x06004106 RID: 16646 RVA: 0x000EE3D0 File Offset: 0x000EC5D0
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.And);
			}

			// Token: 0x06004107 RID: 16647 RVA: 0x000EE3DA File Offset: 0x000EC5DA
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, ExprType.Or);
			}

			// Token: 0x06004108 RID: 16648 RVA: 0x000EE3E4 File Offset: 0x000EC5E4
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

			// Token: 0x04001D45 RID: 7493
			private StringBuilder m_builder;

			// Token: 0x04001D46 RID: 7494
			private string m_blockAlias;

			// Token: 0x04001D47 RID: 7495
			private bool m_skipIsNotNull;
		}

		// Token: 0x020005C9 RID: 1481
		private class TermVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>
		{
			// Token: 0x06004109 RID: 16649 RVA: 0x000EE488 File Offset: 0x000EC688
			private TermVisitor(bool allowAllOperators)
			{
				this.m_allowAllOperators = allowAllOperators;
			}

			// Token: 0x0600410A RID: 16650 RVA: 0x000EE498 File Offset: 0x000EC698
			internal static IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> GetTerms(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, bool allowAllOperators)
			{
				BoolExpression.TermVisitor visitor = new BoolExpression.TermVisitor(allowAllOperators);
				return expression.Accept<IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>(visitor);
			}

			// Token: 0x0600410B RID: 16651 RVA: 0x000EE4B3 File Offset: 0x000EC6B3
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield break;
			}

			// Token: 0x0600410C RID: 16652 RVA: 0x000EE4BC File Offset: 0x000EC6BC
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield break;
			}

			// Token: 0x0600410D RID: 16653 RVA: 0x000EE4C5 File Offset: 0x000EC6C5
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				yield return expression;
				yield break;
			}

			// Token: 0x0600410E RID: 16654 RVA: 0x000EE4D5 File Offset: 0x000EC6D5
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}

			// Token: 0x0600410F RID: 16655 RVA: 0x000EE4DE File Offset: 0x000EC6DE
			private IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitTreeNode(TreeExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr in expression.Children)
				{
					foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr in boolExpr.Accept<IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>>>(this))
					{
						yield return termExpr;
					}
					IEnumerator<TermExpr<DomainConstraint<BoolLiteral, Constant>>> enumerator2 = null;
				}
				HashSet<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>.Enumerator enumerator = default(HashSet<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06004110 RID: 16656 RVA: 0x000EE4D5 File Offset: 0x000EC6D5
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}

			// Token: 0x06004111 RID: 16657 RVA: 0x000EE4D5 File Offset: 0x000EC6D5
			internal override IEnumerable<TermExpr<DomainConstraint<BoolLiteral, Constant>>> VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitTreeNode(expression);
			}

			// Token: 0x04001D48 RID: 7496
			private bool m_allowAllOperators;
		}

		// Token: 0x020005CA RID: 1482
		private class CompactStringVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, StringBuilder>
		{
			// Token: 0x06004112 RID: 16658 RVA: 0x000EE4F5 File Offset: 0x000EC6F5
			private CompactStringVisitor(StringBuilder builder)
			{
				this.m_builder = builder;
			}

			// Token: 0x06004113 RID: 16659 RVA: 0x000EE504 File Offset: 0x000EC704
			internal static StringBuilder ToBuilder(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression, StringBuilder builder)
			{
				BoolExpression.CompactStringVisitor visitor = new BoolExpression.CompactStringVisitor(builder);
				return expression.Accept<StringBuilder>(visitor);
			}

			// Token: 0x06004114 RID: 16660 RVA: 0x000EE51F File Offset: 0x000EC71F
			internal override StringBuilder VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("True");
				return this.m_builder;
			}

			// Token: 0x06004115 RID: 16661 RVA: 0x000EE538 File Offset: 0x000EC738
			internal override StringBuilder VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("False");
				return this.m_builder;
			}

			// Token: 0x06004116 RID: 16662 RVA: 0x000EE554 File Offset: 0x000EC754
			internal override StringBuilder VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				BoolLiteral boolLiteral = BoolExpression.GetBoolLiteral(expression);
				boolLiteral.ToCompactString(this.m_builder);
				return this.m_builder;
			}

			// Token: 0x06004117 RID: 16663 RVA: 0x000EE57A File Offset: 0x000EC77A
			internal override StringBuilder VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				this.m_builder.Append("NOT(");
				expression.Child.Accept<StringBuilder>(this);
				this.m_builder.Append(")");
				return this.m_builder;
			}

			// Token: 0x06004118 RID: 16664 RVA: 0x000EE5B1 File Offset: 0x000EC7B1
			internal override StringBuilder VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, "AND");
			}

			// Token: 0x06004119 RID: 16665 RVA: 0x000EE5BF File Offset: 0x000EC7BF
			internal override StringBuilder VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return this.VisitAndOr(expression, "OR");
			}

			// Token: 0x0600411A RID: 16666 RVA: 0x000EE5D0 File Offset: 0x000EC7D0
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

			// Token: 0x04001D49 RID: 7497
			private StringBuilder m_builder;
		}
	}
}
