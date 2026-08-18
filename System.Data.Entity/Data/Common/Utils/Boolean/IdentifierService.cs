using System;
using System.Runtime.CompilerServices;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A6 RID: 934
	internal abstract class IdentifierService<T_Identifier>
	{
		// Token: 0x0600336E RID: 13166 RVA: 0x000C8250 File Offset: 0x000C6450
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static IdentifierService<T_Identifier> GetIdentifierService()
		{
			Type typeFromHandle = typeof(T_Identifier);
			if (typeFromHandle.IsGenericType && typeFromHandle.GetGenericTypeDefinition() == typeof(DomainConstraint<, >))
			{
				Type[] genericArguments = typeFromHandle.GetGenericArguments();
				Type type = genericArguments[0];
				Type type2 = genericArguments[1];
				return (IdentifierService<T_Identifier>)Activator.CreateInstance(typeof(IdentifierService<>.DomainConstraintIdentifierService<, >).MakeGenericType(new Type[]
				{
					typeFromHandle,
					type,
					type2
				}));
			}
			return new IdentifierService<T_Identifier>.GenericIdentifierService();
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x00002050 File Offset: 0x00000250
		private IdentifierService()
		{
		}

		// Token: 0x06003370 RID: 13168
		internal abstract Literal<T_Identifier> NegateLiteral(Literal<T_Identifier> literal);

		// Token: 0x06003371 RID: 13169
		internal abstract ConversionContext<T_Identifier> CreateConversionContext();

		// Token: 0x06003372 RID: 13170
		internal abstract BoolExpr<T_Identifier> LocalSimplify(BoolExpr<T_Identifier> expression);

		// Token: 0x04001688 RID: 5768
		internal static readonly IdentifierService<T_Identifier> Instance = IdentifierService<T_Identifier>.GetIdentifierService();

		// Token: 0x02000684 RID: 1668
		private class GenericIdentifierService : IdentifierService<T_Identifier>
		{
			// Token: 0x060044F7 RID: 17655 RVA: 0x000F8C95 File Offset: 0x000F6E95
			internal override Literal<T_Identifier> NegateLiteral(Literal<T_Identifier> literal)
			{
				return new Literal<T_Identifier>(literal.Term, !literal.IsTermPositive);
			}

			// Token: 0x060044F8 RID: 17656 RVA: 0x000F8CAB File Offset: 0x000F6EAB
			internal override ConversionContext<T_Identifier> CreateConversionContext()
			{
				return new GenericConversionContext<T_Identifier>();
			}

			// Token: 0x060044F9 RID: 17657 RVA: 0x000F8CB2 File Offset: 0x000F6EB2
			internal override BoolExpr<T_Identifier> LocalSimplify(BoolExpr<T_Identifier> expression)
			{
				return expression.Accept<BoolExpr<T_Identifier>>(Simplifier<T_Identifier>.Instance);
			}
		}

		// Token: 0x02000685 RID: 1669
		private class DomainConstraintIdentifierService<T_Variable, T_Element> : IdentifierService<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x060044FB RID: 17659 RVA: 0x000F8CC8 File Offset: 0x000F6EC8
			internal override Literal<DomainConstraint<T_Variable, T_Element>> NegateLiteral(Literal<DomainConstraint<T_Variable, T_Element>> literal)
			{
				TermExpr<DomainConstraint<T_Variable, T_Element>> term = new TermExpr<DomainConstraint<T_Variable, T_Element>>(literal.Term.Identifier.InvertDomainConstraint());
				return new Literal<DomainConstraint<T_Variable, T_Element>>(term, literal.IsTermPositive);
			}

			// Token: 0x060044FC RID: 17660 RVA: 0x000F8CF7 File Offset: 0x000F6EF7
			internal override ConversionContext<DomainConstraint<T_Variable, T_Element>> CreateConversionContext()
			{
				return new DomainConstraintConversionContext<T_Variable, T_Element>();
			}

			// Token: 0x060044FD RID: 17661 RVA: 0x000F8CFE File Offset: 0x000F6EFE
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> LocalSimplify(BoolExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				expression = NegationPusher.EliminateNot<T_Variable, T_Element>(expression);
				return expression.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(Simplifier<DomainConstraint<T_Variable, T_Element>>.Instance);
			}
		}
	}
}
