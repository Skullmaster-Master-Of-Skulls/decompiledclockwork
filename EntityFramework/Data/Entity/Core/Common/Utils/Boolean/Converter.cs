using System;
using System.Linq;

namespace System.Data.Entity.Core.Common.Utils.Boolean
{
	// Token: 0x02000306 RID: 774
	internal sealed class Converter<T_Identifier>
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x00086A8D File Offset: 0x00084C8D
		internal Converter(BoolExpr<T_Identifier> expr, ConversionContext<T_Identifier> context)
		{
			this._context = (context ?? IdentifierService<T_Identifier>.Instance.CreateConversionContext());
			this._vertex = ToDecisionDiagramConverter<T_Identifier>.TranslateToRobdd(expr, this._context);
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x00086ABC File Offset: 0x00084CBC
		internal Vertex Vertex
		{
			get
			{
				return this._vertex;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00086AC4 File Offset: 0x00084CC4
		internal DnfSentence<T_Identifier> Dnf
		{
			get
			{
				this.InitializeNormalForms();
				return this._dnf;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x00086AD2 File Offset: 0x00084CD2
		internal CnfSentence<T_Identifier> Cnf
		{
			get
			{
				this.InitializeNormalForms();
				return this._cnf;
			}
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00086AE0 File Offset: 0x00084CE0
		private void InitializeNormalForms()
		{
			if (this._cnf == null)
			{
				if (this._vertex.IsOne())
				{
					this._cnf = new CnfSentence<T_Identifier>(Set<CnfClause<T_Identifier>>.Empty);
					DnfClause<T_Identifier> element = new DnfClause<T_Identifier>(Set<Literal<T_Identifier>>.Empty);
					this._dnf = new DnfSentence<T_Identifier>(new Set<DnfClause<T_Identifier>>
					{
						element
					}.MakeReadOnly());
					return;
				}
				if (this._vertex.IsZero())
				{
					CnfClause<T_Identifier> element2 = new CnfClause<T_Identifier>(Set<Literal<T_Identifier>>.Empty);
					this._cnf = new CnfSentence<T_Identifier>(new Set<CnfClause<T_Identifier>>
					{
						element2
					}.MakeReadOnly());
					this._dnf = new DnfSentence<T_Identifier>(Set<DnfClause<T_Identifier>>.Empty);
					return;
				}
				Set<DnfClause<T_Identifier>> set = new Set<DnfClause<T_Identifier>>();
				Set<CnfClause<T_Identifier>> set2 = new Set<CnfClause<T_Identifier>>();
				Set<Literal<T_Identifier>> path = new Set<Literal<T_Identifier>>();
				this.FindAllPaths(this._vertex, set2, set, path);
				this._cnf = new CnfSentence<T_Identifier>(set2.MakeReadOnly());
				this._dnf = new DnfSentence<T_Identifier>(set.MakeReadOnly());
			}
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00086BDC File Offset: 0x00084DDC
		private void FindAllPaths(Vertex vertex, Set<CnfClause<T_Identifier>> cnfClauses, Set<DnfClause<T_Identifier>> dnfClauses, Set<Literal<T_Identifier>> path)
		{
			if (vertex.IsOne())
			{
				DnfClause<T_Identifier> element = new DnfClause<T_Identifier>(path);
				dnfClauses.Add(element);
				return;
			}
			if (vertex.IsZero())
			{
				CnfClause<T_Identifier> element2 = new CnfClause<T_Identifier>(new Set<Literal<T_Identifier>>(from l in path
				select l.MakeNegated()));
				cnfClauses.Add(element2);
				return;
			}
			foreach (LiteralVertexPair<T_Identifier> literalVertexPair in this._context.GetSuccessors(vertex))
			{
				path.Add(literalVertexPair.Literal);
				this.FindAllPaths(literalVertexPair.Vertex, cnfClauses, dnfClauses, path);
				path.Remove(literalVertexPair.Literal);
			}
		}

		// Token: 0x0400097C RID: 2428
		private readonly Vertex _vertex;

		// Token: 0x0400097D RID: 2429
		private readonly ConversionContext<T_Identifier> _context;

		// Token: 0x0400097E RID: 2430
		private DnfSentence<T_Identifier> _dnf;

		// Token: 0x0400097F RID: 2431
		private CnfSentence<T_Identifier> _cnf;
	}
}
