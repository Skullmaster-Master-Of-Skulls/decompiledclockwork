using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x020001E7 RID: 487
	internal class FragmentQueryKBChaseSupport : FragmentQueryKB
	{
		// Token: 0x0600111B RID: 4379 RVA: 0x00048D78 File Offset: 0x00046F78
		internal FragmentQueryKBChaseSupport()
		{
			this._chase = new FragmentQueryKBChaseSupport.AtomicConditionRuleChase(this);
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00048DA0 File Offset: 0x00046FA0
		internal Dictionary<TermExpr<DomainConstraint<BoolLiteral, Constant>>, BoolExpr<DomainConstraint<BoolLiteral, Constant>>> Implications
		{
			get
			{
				if (this._implications == null)
				{
					this._implications = new Dictionary<TermExpr<DomainConstraint<BoolLiteral, Constant>>, BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
					foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact in base.Facts)
					{
						this.CacheFact(fact);
					}
				}
				return this._implications;
			}
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00048E08 File Offset: 0x00047008
		internal override void AddFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			base.AddFact(fact);
			this._kbSize += fact.CountTerms();
			if (this._implications != null)
			{
				this.CacheFact(fact);
			}
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00048E34 File Offset: 0x00047034
		private void CacheFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>.Implication implication = fact as KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>.Implication;
			KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>.Equivalence equivalence = fact as KnowledgeBase<DomainConstraint<BoolLiteral, Constant>>.Equivalence;
			if (implication != null)
			{
				this.CacheImplication(implication.Condition, implication.Implies);
				return;
			}
			if (equivalence != null)
			{
				this.CacheImplication(equivalence.Left, equivalence.Right);
				this.CacheImplication(equivalence.Right, equivalence.Left);
				return;
			}
			this.CacheResidualFact(fact);
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00048E94 File Offset: 0x00047094
		private IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> ResidueInternal
		{
			get
			{
				if (this._residueSize < 0 && this._residualFacts.Count > 0)
				{
					this.PrepareResidue();
				}
				return this._residualFacts;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00048EB9 File Offset: 0x000470B9
		private int ResidueSize
		{
			get
			{
				if (this._residueSize < 0)
				{
					this.PrepareResidue();
				}
				return this._residueSize;
			}
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00048ED0 File Offset: 0x000470D0
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Chase(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr;
			this.Implications.TryGetValue(expression, out boolExpr);
			return new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
			{
				expression,
				boolExpr ?? TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value
			});
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00048F0C File Offset: 0x0004710C
		internal bool IsSatisfiable(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			ConversionContext<DomainConstraint<BoolLiteral, Constant>> context = IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext();
			Converter<DomainConstraint<BoolLiteral, Constant>> converter = new Converter<DomainConstraint<BoolLiteral, Constant>>(expression, context);
			if (converter.Vertex.IsZero())
			{
				return false;
			}
			if (base.KbExpression.ExprType == ExprType.True)
			{
				return true;
			}
			int num = expression.CountTerms() + this._kbSize;
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr = converter.Dnf.Expr;
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr2 = (FragmentQueryKBChaseSupport.Normalizer.EstimateNnfAndSplitTermCount(expr) > FragmentQueryKBChaseSupport.Normalizer.EstimateNnfAndSplitTermCount(expression)) ? expression : expr;
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = this._chase.Chase(FragmentQueryKBChaseSupport.Normalizer.ToNnfAndSplitRange(expr2));
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr3;
			if (boolExpr.CountTerms() + this.ResidueSize > num)
			{
				expr3 = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
				{
					base.KbExpression,
					expression
				});
			}
			else
			{
				expr3 = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(this.ResidueInternal)
				{
					boolExpr
				});
				context = IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext();
			}
			return !new Converter<DomainConstraint<BoolLiteral, Constant>>(expr3, context).Vertex.IsZero();
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00048FFF File Offset: 0x000471FF
		internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Chase(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
		{
			if (this.Implications.Count != 0)
			{
				return this._chase.Chase(FragmentQueryKBChaseSupport.Normalizer.ToNnfAndSplitRange(expression));
			}
			return expression;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00049024 File Offset: 0x00047224
		private void CacheImplication(BoolExpr<DomainConstraint<BoolLiteral, Constant>> condition, BoolExpr<DomainConstraint<BoolLiteral, Constant>> implies)
		{
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = FragmentQueryKBChaseSupport.Normalizer.ToDnf(condition, false);
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> implies2 = FragmentQueryKBChaseSupport.Normalizer.ToNnfAndSplitRange(implies);
			switch (boolExpr.ExprType)
			{
			case ExprType.Or:
				using (HashSet<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>.Enumerator enumerator = ((OrExpr<DomainConstraint<BoolLiteral, Constant>>)boolExpr).Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr2 = enumerator.Current;
						if (boolExpr2.ExprType != ExprType.Term)
						{
							this.CacheResidualFact(new OrExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
							{
								new NotExpr<DomainConstraint<BoolLiteral, Constant>>(boolExpr2),
								implies
							}));
						}
						else
						{
							this.CacheNormalizedImplication((TermExpr<DomainConstraint<BoolLiteral, Constant>>)boolExpr2, implies2);
						}
					}
					return;
				}
				break;
			case ExprType.Term:
				break;
			default:
				this.CacheResidualFact(new OrExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
				{
					new NotExpr<DomainConstraint<BoolLiteral, Constant>>(condition),
					implies
				}));
				return;
			}
			this.CacheNormalizedImplication((TermExpr<DomainConstraint<BoolLiteral, Constant>>)boolExpr, implies2);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0004910C File Offset: 0x0004730C
		private void CacheNormalizedImplication(TermExpr<DomainConstraint<BoolLiteral, Constant>> condition, BoolExpr<DomainConstraint<BoolLiteral, Constant>> implies)
		{
			foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr in this.Implications.Keys)
			{
				if (termExpr.Identifier.Variable.Equals(condition.Identifier.Variable) && !termExpr.Identifier.Range.SetEquals(condition.Identifier.Range))
				{
					this.CacheResidualFact(new OrExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
					{
						new NotExpr<DomainConstraint<BoolLiteral, Constant>>(condition),
						implies
					}));
					return;
				}
			}
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr = new Converter<DomainConstraint<BoolLiteral, Constant>>(this.Chase(implies), IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext()).Dnf.Expr;
			FragmentQueryKBChaseSupport fragmentQueryKBChaseSupport = new FragmentQueryKBChaseSupport();
			fragmentQueryKBChaseSupport.Implications[condition] = expr;
			bool flag = true;
			foreach (TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr2 in new Set<TermExpr<DomainConstraint<BoolLiteral, Constant>>>(this.Implications.Keys))
			{
				BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = fragmentQueryKBChaseSupport.Chase(this.Implications[termExpr2]);
				if (termExpr2.Equals(condition))
				{
					flag = false;
					boolExpr = new AndExpr<DomainConstraint<BoolLiteral, Constant>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
					{
						boolExpr,
						expr
					});
				}
				this.Implications[termExpr2] = new Converter<DomainConstraint<BoolLiteral, Constant>>(boolExpr, IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext()).Dnf.Expr;
			}
			if (flag)
			{
				this.Implications[condition] = expr;
			}
			this._residueSize = -1;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000492BC File Offset: 0x000474BC
		private void CacheResidualFact(BoolExpr<DomainConstraint<BoolLiteral, Constant>> fact)
		{
			this._residualFacts.Add(fact);
			this._residueSize = -1;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x000492D4 File Offset: 0x000474D4
		private void PrepareResidue()
		{
			int num = 0;
			if (this.Implications.Count > 0 && this._residualFacts.Count > 0)
			{
				Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> set = new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
				foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression in this._residualFacts)
				{
					BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr = new Converter<DomainConstraint<BoolLiteral, Constant>>(this.Chase(expression), IdentifierService<DomainConstraint<BoolLiteral, Constant>>.Instance.CreateConversionContext()).Dnf.Expr;
					set.Add(expr);
					num += expr.CountTerms();
					this._residueSize = num;
				}
				this._residualFacts = set;
			}
			this._residueSize = num;
		}

		// Token: 0x0400051C RID: 1308
		private Dictionary<TermExpr<DomainConstraint<BoolLiteral, Constant>>, BoolExpr<DomainConstraint<BoolLiteral, Constant>>> _implications;

		// Token: 0x0400051D RID: 1309
		private readonly FragmentQueryKBChaseSupport.AtomicConditionRuleChase _chase;

		// Token: 0x0400051E RID: 1310
		private Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> _residualFacts = new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();

		// Token: 0x0400051F RID: 1311
		private int _kbSize;

		// Token: 0x04000520 RID: 1312
		private int _residueSize = -1;

		// Token: 0x020001E8 RID: 488
		private static class Normalizer
		{
			// Token: 0x06001128 RID: 4392 RVA: 0x00049390 File Offset: 0x00047590
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> ToNnfAndSplitRange(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr)
			{
				return expr.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(FragmentQueryKBChaseSupport.Normalizer.NonNegatedTreeVisitor.Instance);
			}

			// Token: 0x06001129 RID: 4393 RVA: 0x0004939D File Offset: 0x0004759D
			internal static int EstimateNnfAndSplitTermCount(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr)
			{
				return expr.Accept<int>(FragmentQueryKBChaseSupport.Normalizer.NonNegatedNnfSplitCounter.Instance);
			}

			// Token: 0x0600112A RID: 4394 RVA: 0x000493AA File Offset: 0x000475AA
			internal static BoolExpr<DomainConstraint<BoolLiteral, Constant>> ToDnf(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expr, bool isNnf)
			{
				if (!isNnf)
				{
					expr = FragmentQueryKBChaseSupport.Normalizer.ToNnfAndSplitRange(expr);
				}
				return expr.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(FragmentQueryKBChaseSupport.Normalizer.DnfTreeVisitor.Instance);
			}

			// Token: 0x020001EB RID: 491
			private class NonNegatedTreeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x0600113A RID: 4410 RVA: 0x000495D4 File Offset: 0x000477D4
				private NonNegatedTreeVisitor()
				{
				}

				// Token: 0x0600113B RID: 4411 RVA: 0x000495DC File Offset: 0x000477DC
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expr)
				{
					return expr.Child.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(FragmentQueryKBChaseSupport.Normalizer.NegatedTreeVisitor.Instance);
				}

				// Token: 0x0600113C RID: 4412 RVA: 0x000495F0 File Offset: 0x000477F0
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					switch (expression.Identifier.Range.Count)
					{
					case 0:
						return FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
					case 1:
						return expression;
					default:
					{
						List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> list = new List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
						DomainVariable<BoolLiteral, Constant> variable = expression.Identifier.Variable;
						foreach (Constant constant in expression.Identifier.Range)
						{
							list.Add(new DomainConstraint<BoolLiteral, Constant>(variable, new Set<Constant>(new Constant[]
							{
								constant
							}, Constant.EqualityComparer)));
						}
						return new OrExpr<DomainConstraint<BoolLiteral, Constant>>(list);
					}
					}
				}

				// Token: 0x04000521 RID: 1313
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.NonNegatedTreeVisitor Instance = new FragmentQueryKBChaseSupport.Normalizer.NonNegatedTreeVisitor();
			}

			// Token: 0x020001EC RID: 492
			private class NegatedTreeVisitor : Visitor<DomainConstraint<BoolLiteral, Constant>, BoolExpr<DomainConstraint<BoolLiteral, Constant>>>
			{
				// Token: 0x0600113E RID: 4414 RVA: 0x000496BC File Offset: 0x000478BC
				private NegatedTreeVisitor()
				{
				}

				// Token: 0x0600113F RID: 4415 RVA: 0x000496C4 File Offset: 0x000478C4
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTrue(TrueExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
				}

				// Token: 0x06001140 RID: 4416 RVA: 0x000496CB File Offset: 0x000478CB
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitFalse(FalseExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return TrueExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
				}

				// Token: 0x06001141 RID: 4417 RVA: 0x000496D2 File Offset: 0x000478D2
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression.Child.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(FragmentQueryKBChaseSupport.Normalizer.NonNegatedTreeVisitor.Instance);
				}

				// Token: 0x06001142 RID: 4418 RVA: 0x000496ED File Offset: 0x000478ED
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return new OrExpr<DomainConstraint<BoolLiteral, Constant>>(from child in expression.Children
					select child.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(this));
				}

				// Token: 0x06001143 RID: 4419 RVA: 0x00049714 File Offset: 0x00047914
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitOr(OrExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return new AndExpr<DomainConstraint<BoolLiteral, Constant>>(from child in expression.Children
					select child.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(this));
				}

				// Token: 0x06001144 RID: 4420 RVA: 0x00049734 File Offset: 0x00047934
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					DomainConstraint<BoolLiteral, Constant> domainConstraint = expression.Identifier.InvertDomainConstraint();
					if (domainConstraint.Range.Count == 0)
					{
						return FalseExpr<DomainConstraint<BoolLiteral, Constant>>.Value;
					}
					List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> list = new List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
					DomainVariable<BoolLiteral, Constant> variable = domainConstraint.Variable;
					foreach (Constant constant in domainConstraint.Range)
					{
						list.Add(new DomainConstraint<BoolLiteral, Constant>(variable, new Set<Constant>(new Constant[]
						{
							constant
						}, Constant.EqualityComparer)));
					}
					return new OrExpr<DomainConstraint<BoolLiteral, Constant>>(list);
				}

				// Token: 0x04000522 RID: 1314
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.NegatedTreeVisitor Instance = new FragmentQueryKBChaseSupport.Normalizer.NegatedTreeVisitor();
			}

			// Token: 0x020001EE RID: 494
			private class NonNegatedNnfSplitCounter : TermCounter<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x06001152 RID: 4434 RVA: 0x00049894 File Offset: 0x00047A94
				private NonNegatedNnfSplitCounter()
				{
				}

				// Token: 0x06001153 RID: 4435 RVA: 0x0004989C File Offset: 0x00047A9C
				internal override int VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expr)
				{
					return expr.Child.Accept<int>(FragmentQueryKBChaseSupport.Normalizer.NegatedNnfSplitCountEstimator.Instance);
				}

				// Token: 0x06001154 RID: 4436 RVA: 0x000498AE File Offset: 0x00047AAE
				internal override int VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression.Identifier.Range.Count;
				}

				// Token: 0x04000524 RID: 1316
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.NonNegatedNnfSplitCounter Instance = new FragmentQueryKBChaseSupport.Normalizer.NonNegatedNnfSplitCounter();
			}

			// Token: 0x020001EF RID: 495
			private class NegatedNnfSplitCountEstimator : TermCounter<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x06001156 RID: 4438 RVA: 0x000498CC File Offset: 0x00047ACC
				private NegatedNnfSplitCountEstimator()
				{
				}

				// Token: 0x06001157 RID: 4439 RVA: 0x000498D4 File Offset: 0x00047AD4
				internal override int VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression.Child.Accept<int>(FragmentQueryKBChaseSupport.Normalizer.NonNegatedNnfSplitCounter.Instance);
				}

				// Token: 0x06001158 RID: 4440 RVA: 0x000498E6 File Offset: 0x00047AE6
				internal override int VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression.Identifier.Variable.Domain.Count - expression.Identifier.Range.Count;
				}

				// Token: 0x04000525 RID: 1317
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.NegatedNnfSplitCountEstimator Instance = new FragmentQueryKBChaseSupport.Normalizer.NegatedNnfSplitCountEstimator();
			}

			// Token: 0x020001F0 RID: 496
			private class DnfTreeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x0600115A RID: 4442 RVA: 0x0004991A File Offset: 0x00047B1A
				private DnfTreeVisitor()
				{
				}

				// Token: 0x0600115B RID: 4443 RVA: 0x00049922 File Offset: 0x00047B22
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return expression;
				}

				// Token: 0x0600115C RID: 4444 RVA: 0x000499A4 File Offset: 0x00047BA4
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitAnd(AndExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = base.VisitAnd(expression);
					TreeExpr<DomainConstraint<BoolLiteral, Constant>> treeExpr = boolExpr as TreeExpr<DomainConstraint<BoolLiteral, Constant>>;
					if (treeExpr == null)
					{
						return boolExpr;
					}
					Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> set = new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
					Set<Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>> set2 = new Set<Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>>();
					foreach (BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr2 in treeExpr.Children)
					{
						OrExpr<DomainConstraint<BoolLiteral, Constant>> orExpr = boolExpr2 as OrExpr<DomainConstraint<BoolLiteral, Constant>>;
						if (orExpr != null)
						{
							set2.Add(new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(orExpr.Children));
						}
						else
						{
							set.Add(boolExpr2);
						}
					}
					set2.Add(new Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
					{
						new AndExpr<DomainConstraint<BoolLiteral, Constant>>(set)
					}));
					IEnumerable<IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>> seed = new IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>[]
					{
						Enumerable.Empty<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>()
					};
					IEnumerable<IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>> enumerable = set2.Aggregate(seed, (IEnumerable<IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>> accumulator, Set<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> bucket) => from accseq in accumulator
					from item in bucket
					select accseq.Concat(new BoolExpr<DomainConstraint<BoolLiteral, Constant>>[]
					{
						item
					}));
					List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> list = new List<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>();
					foreach (IEnumerable<BoolExpr<DomainConstraint<BoolLiteral, Constant>>> children in enumerable)
					{
						list.Add(new AndExpr<DomainConstraint<BoolLiteral, Constant>>(children));
					}
					return new OrExpr<DomainConstraint<BoolLiteral, Constant>>(list);
				}

				// Token: 0x04000526 RID: 1318
				internal static readonly FragmentQueryKBChaseSupport.Normalizer.DnfTreeVisitor Instance = new FragmentQueryKBChaseSupport.Normalizer.DnfTreeVisitor();
			}
		}

		// Token: 0x020001F1 RID: 497
		private class AtomicConditionRuleChase
		{
			// Token: 0x06001160 RID: 4448 RVA: 0x00049AF8 File Offset: 0x00047CF8
			internal AtomicConditionRuleChase(FragmentQueryKBChaseSupport kb)
			{
				this._visitor = new FragmentQueryKBChaseSupport.AtomicConditionRuleChase.NonNegatedDomainConstraintTreeVisitor(kb);
			}

			// Token: 0x06001161 RID: 4449 RVA: 0x00049B0C File Offset: 0x00047D0C
			internal BoolExpr<DomainConstraint<BoolLiteral, Constant>> Chase(BoolExpr<DomainConstraint<BoolLiteral, Constant>> expression)
			{
				return expression.Accept<BoolExpr<DomainConstraint<BoolLiteral, Constant>>>(this._visitor);
			}

			// Token: 0x04000529 RID: 1321
			private readonly FragmentQueryKBChaseSupport.AtomicConditionRuleChase.NonNegatedDomainConstraintTreeVisitor _visitor;

			// Token: 0x020001F2 RID: 498
			private class NonNegatedDomainConstraintTreeVisitor : BasicVisitor<DomainConstraint<BoolLiteral, Constant>>
			{
				// Token: 0x06001162 RID: 4450 RVA: 0x00049B1A File Offset: 0x00047D1A
				internal NonNegatedDomainConstraintTreeVisitor(FragmentQueryKBChaseSupport kb)
				{
					this._kb = kb;
				}

				// Token: 0x06001163 RID: 4451 RVA: 0x00049B29 File Offset: 0x00047D29
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitTerm(TermExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return this._kb.Chase(expression);
				}

				// Token: 0x06001164 RID: 4452 RVA: 0x00049B37 File Offset: 0x00047D37
				internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> VisitNot(NotExpr<DomainConstraint<BoolLiteral, Constant>> expression)
				{
					return base.VisitNot(expression);
				}

				// Token: 0x0400052A RID: 1322
				private readonly FragmentQueryKBChaseSupport _kb;
			}
		}
	}
}
