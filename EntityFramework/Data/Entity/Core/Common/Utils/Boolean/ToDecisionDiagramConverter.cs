using System;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000320 RID: 800
	internal class ToDecisionDiagramConverter<T_Identifier> : Visitor<T_Identifier, Vertex>
	{
		// Token: 0x06001B9C RID: 7068 RVA: 0x0008821A File Offset: 0x0008641A
		private ToDecisionDiagramConverter(ConversionContext<T_Identifier> context)
		{
			this._context = context;
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0008822C File Offset: 0x0008642C
		internal static Vertex TranslateToRobdd(BoolExpr<T_Identifier> expr, ConversionContext<T_Identifier> context)
		{
			ToDecisionDiagramConverter<T_Identifier> visitor = new ToDecisionDiagramConverter<T_Identifier>(context);
			return expr.Accept<Vertex>(visitor);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x00088247 File Offset: 0x00086447
		internal override Vertex VisitTrue(TrueExpr<T_Identifier> expression)
		{
			return Vertex.One;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0008824E File Offset: 0x0008644E
		internal override Vertex VisitFalse(FalseExpr<T_Identifier> expression)
		{
			return Vertex.Zero;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x00088255 File Offset: 0x00086455
		internal override Vertex VisitTerm(TermExpr<T_Identifier> expression)
		{
			return this._context.TranslateTermToVertex(expression);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x00088263 File Offset: 0x00086463
		internal override Vertex VisitNot(NotExpr<T_Identifier> expression)
		{
			return this._context.Solver.Not(expression.Child.Accept<Vertex>(this));
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0008828A File Offset: 0x0008648A
		internal override Vertex VisitAnd(AndExpr<T_Identifier> expression)
		{
			return this._context.Solver.And(from child in expression.Children
			select child.Accept<Vertex>(this));
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000882BC File Offset: 0x000864BC
		internal override Vertex VisitOr(OrExpr<T_Identifier> expression)
		{
			return this._context.Solver.Or(from child in expression.Children
			select child.Accept<Vertex>(this));
		}

		// Token: 0x040009B0 RID: 2480
		private readonly ConversionContext<T_Identifier> _context;
	}
}
