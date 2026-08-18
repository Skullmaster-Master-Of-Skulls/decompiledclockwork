using System;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003C3 RID: 963
	internal class ToDecisionDiagramConverter<T_Identifier> : Visitor<T_Identifier, Vertex>
	{
		// Token: 0x06003420 RID: 13344 RVA: 0x000C9459 File Offset: 0x000C7659
		private ToDecisionDiagramConverter(ConversionContext<T_Identifier> context)
		{
			this._context = context;
		}

		// Token: 0x06003421 RID: 13345 RVA: 0x000C9468 File Offset: 0x000C7668
		internal static Vertex TranslateToRobdd(BoolExpr<T_Identifier> expr, ConversionContext<T_Identifier> context)
		{
			ToDecisionDiagramConverter<T_Identifier> visitor = new ToDecisionDiagramConverter<T_Identifier>(context);
			return expr.Accept<Vertex>(visitor);
		}

		// Token: 0x06003422 RID: 13346 RVA: 0x000C9483 File Offset: 0x000C7683
		internal override Vertex VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return Vertex.One;
		}

		// Token: 0x06003423 RID: 13347 RVA: 0x000C948A File Offset: 0x000C768A
		internal override Vertex VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return Vertex.Zero;
		}

		// Token: 0x06003424 RID: 13348 RVA: 0x000C9491 File Offset: 0x000C7691
		internal override Vertex VisitTerm(TermExpr<T_Identifier> expression)
		{
			return this._context.TranslateTermToVertex(expression);
		}

		// Token: 0x06003425 RID: 13349 RVA: 0x000C949F File Offset: 0x000C769F
		internal override Vertex VisitNot(NotExpr<T_Identifier> expression)
		{
			return this._context.Solver.Not(expression.Child.Accept<Vertex>(this));
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000C94BD File Offset: 0x000C76BD
		internal override Vertex VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this._context.Solver.And(from child in expression.Children
			select child.Accept<Vertex>(this));
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000C94E6 File Offset: 0x000C76E6
		internal override Vertex VisitOr(OrExpr<T_Identifier> expression)
		{
			return this._context.Solver.Or(from child in expression.Children
			select child.Accept<Vertex>(this));
		}

		// Token: 0x040016AE RID: 5806
		private readonly ConversionContext<T_Identifier> _context;
	}
}
