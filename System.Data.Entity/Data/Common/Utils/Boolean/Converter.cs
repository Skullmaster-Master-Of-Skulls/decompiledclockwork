using System;
using System.Linq;

namespace System.Data.Common.Utils.Boolean
{
	// Token: 0x020003A3 RID: 931
	internal sealed class Converter<T_Identifier>
	{
		// Token: 0x06003359 RID: 13145 RVA: 0x000C7E07 File Offset: 0x000C6007
		internal Converter(BoolExpr<T_Identifier> expr, ConversionContext<T_Identifier> context)
		{
			this._context = (context ?? IdentifierService<T_Identifier>.Instance.CreateConversionContext());
			this._vertex = ToDecisionDiagramConverter<T_Identifier>.TranslateToRobdd(expr, this._context);
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000C7E36 File Offset: 0x000C6036
		internal Vertex Vertex
		{
			get
			{
				return this._vertex;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x0600335B RID: 13147 RVA: 0x000C7E3E File Offset: 0x000C603E
		internal DnfSentence<T_Identifier> Dnf
		{
			get
			{
				this.InitializeNormalForms();
				return this._dnf;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x000C7E4C File Offset: 0x000C604C
		internal CnfSentence<T_Identifier> Cnf
		{
			get
			{
				this.InitializeNormalForms();
				return this._cnf;
			}
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x000C7E5C File Offset: 0x000C605C
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

		// Token: 0x0600335E RID: 13150 RVA: 0x000C7F50 File Offset: 0x000C6150
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

		// Token: 0x0400167D RID: 5757
		private readonly Vertex _vertex;

		// Token: 0x0400167E RID: 5758
		private readonly ConversionContext<T_Identifier> _context;

		// Token: 0x0400167F RID: 5759
		private DnfSentence<T_Identifier> _dnf;

		// Token: 0x04001680 RID: 5760
		private CnfSentence<T_Identifier> _cnf;
	}
}
