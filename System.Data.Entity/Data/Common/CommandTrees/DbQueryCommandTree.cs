using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003EB RID: 1003
	public sealed class DbQueryCommandTree : DbCommandTree
	{
		// Token: 0x060035D0 RID: 13776 RVA: 0x000CFDFC File Offset: 0x000CDFFC
		private DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query, bool validate) : base(metadata, dataSpace)
		{
			EntityUtil.CheckArgumentNull<DbExpression>(query, "query");
			if (validate)
			{
				DbExpressionValidator dbExpressionValidator = new DbExpressionValidator(metadata, dataSpace);
				dbExpressionValidator.ValidateExpression(query, "query");
				this._parameters = (from paramInfo in dbExpressionValidator.Parameters
				select paramInfo.Value).ToList<DbParameterReferenceExpression>().AsReadOnly();
			}
			this._query = query;
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000CFE76 File Offset: 0x000CE076
		internal DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query) : this(metadata, dataSpace, query, true)
		{
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000CFE82 File Offset: 0x000CE082
		public DbExpression Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000173E2 File Offset: 0x000155E2
		internal override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Query;
			}
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000CFE8A File Offset: 0x000CE08A
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			if (this._parameters == null)
			{
				this._parameters = ParameterRetriever.GetParameters(this);
			}
			return from p in this._parameters
			select new KeyValuePair<string, TypeUsage>(p.ParameterName, p.ResultType);
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000CFECA File Offset: 0x000CE0CA
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.Query != null)
			{
				dumper.Dump(this.Query, "Query");
			}
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000CFEE5 File Offset: 0x000CE0E5
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000CFEEE File Offset: 0x000CE0EE
		internal static DbQueryCommandTree FromValidExpression(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query)
		{
			return new DbQueryCommandTree(metadata, dataSpace, query, false);
		}

		// Token: 0x040017AF RID: 6063
		private readonly DbExpression _query;

		// Token: 0x040017B0 RID: 6064
		private ReadOnlyCollection<DbParameterReferenceExpression> _parameters;
	}
}
