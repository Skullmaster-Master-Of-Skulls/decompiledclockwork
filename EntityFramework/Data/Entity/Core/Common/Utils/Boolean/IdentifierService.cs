using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x0200030F RID: 783
	internal abstract class IdentifierService<T_Identifier>
	{
		// Token: 0x06001B34 RID: 6964 RVA: 0x00087520 File Offset: 0x00085720
		private static IdentifierService<T_Identifier> GetIdentifierService()
		{
			Type typeFromHandle = typeof(T_Identifier);
			if (typeFromHandle.IsGenericType() && typeFromHandle.GetGenericTypeDefinition() == typeof(DomainConstraint<, >))
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

		// Token: 0x06001B35 RID: 6965 RVA: 0x0008759E File Offset: 0x0008579E
		private IdentifierService()
		{
		}

		// Token: 0x06001B36 RID: 6966
		internal abstract Literal<T_Identifier> NegateLiteral(Literal<T_Identifier> literal);

		// Token: 0x06001B37 RID: 6967
		internal abstract ConversionContext<T_Identifier> CreateConversionContext();

		// Token: 0x06001B38 RID: 6968
		internal abstract BoolExpr<T_Identifier> LocalSimplify(BoolExpr<T_Identifier> expression);

		// Token: 0x04000998 RID: 2456
		internal static readonly IdentifierService<T_Identifier> Instance = IdentifierService<T_Identifier>.GetIdentifierService();

		// Token: 0x02000310 RID: 784
		private class GenericIdentifierService : IdentifierService<T_Identifier>
		{
			// Token: 0x06001B3A RID: 6970 RVA: 0x000875B2 File Offset: 0x000857B2
			internal override Literal<T_Identifier> NegateLiteral(Literal<T_Identifier> literal)
			{
				return new Literal<T_Identifier>(literal.Term, !literal.IsTermPositive);
			}

			// Token: 0x06001B3B RID: 6971 RVA: 0x000875C8 File Offset: 0x000857C8
			internal override ConversionContext<T_Identifier> CreateConversionContext()
			{
				return new GenericConversionContext<T_Identifier>();
			}

			// Token: 0x06001B3C RID: 6972 RVA: 0x000875CF File Offset: 0x000857CF
			internal override BoolExpr<T_Identifier> LocalSimplify(BoolExpr<T_Identifier> expression)
			{
				return expression.Accept<BoolExpr<T_Identifier>>(Simplifier<T_Identifier>.Instance);
			}
		}

		// Token: 0x02000311 RID: 785
		private class DomainConstraintIdentifierService<T_Variable, T_Element> : IdentifierService<DomainConstraint<T_Variable, T_Element>>
		{
			// Token: 0x06001B3E RID: 6974 RVA: 0x000875E4 File Offset: 0x000857E4
			internal override Literal<DomainConstraint<T_Variable, T_Element>> NegateLiteral(Literal<DomainConstraint<T_Variable, T_Element>> literal)
			{
				TermExpr<DomainConstraint<T_Variable, T_Element>> term = new TermExpr<DomainConstraint<T_Variable, T_Element>>(literal.Term.Identifier.InvertDomainConstraint());
				return new Literal<DomainConstraint<T_Variable, T_Element>>(term, literal.IsTermPositive);
			}

			// Token: 0x06001B3F RID: 6975 RVA: 0x00087613 File Offset: 0x00085813
			internal override ConversionContext<DomainConstraint<T_Variable, T_Element>> CreateConversionContext()
			{
				return new DomainConstraintConversionContext<T_Variable, T_Element>();
			}

			// Token: 0x06001B40 RID: 6976 RVA: 0x0008761A File Offset: 0x0008581A
			internal override BoolExpr<DomainConstraint<T_Variable, T_Element>> LocalSimplify(BoolExpr<DomainConstraint<T_Variable, T_Element>> expression)
			{
				expression = NegationPusher.EliminateNot<T_Variable, T_Element>(expression);
				return expression.Accept<BoolExpr<DomainConstraint<T_Variable, T_Element>>>(Simplifier<DomainConstraint<T_Variable, T_Element>>.Instance);
			}
		}
	}
}
