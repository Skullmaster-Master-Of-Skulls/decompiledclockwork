using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x02000117 RID: 279
	public sealed class DbQueryCommandTree : DbCommandTree
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x000280A4 File Offset: 0x000262A4
		public DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query, bool validate, bool useDatabaseNullSemantics) : base(metadata, dataSpace, useDatabaseNullSemantics)
		{
			Check.NotNull<DbExpression>(query, "query");
			if (validate)
			{
				DbExpressionValidator dbExpressionValidator = new DbExpressionValidator(metadata, dataSpace);
				dbExpressionValidator.ValidateExpression(query, "query");
				this._parameters = new ReadOnlyCollection<DbParameterReferenceExpression>((from paramInfo in dbExpressionValidator.Parameters
				select paramInfo.Value).ToList<DbParameterReferenceExpression>());
			}
			this._query = query;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0002811E File Offset: 0x0002631E
		public DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query, bool validate) : this(metadata, dataSpace, query, validate, true)
		{
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0002812C File Offset: 0x0002632C
		public DbQueryCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query) : this(metadata, dataSpace, query, true, true)
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00028139 File Offset: 0x00026339
		public DbExpression Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00028141 File Offset: 0x00026341
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Query;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00028157 File Offset: 0x00026357
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			if (this._parameters == null)
			{
				this._parameters = ParameterRetriever.GetParameters(this);
			}
			return from p in this._parameters
			select new KeyValuePair<string, TypeUsage>(p.ParameterName, p.ResultType);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00028195 File Offset: 0x00026395
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.Query != null)
			{
				dumper.Dump(this.Query, "Query");
			}
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000281B0 File Offset: 0x000263B0
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000281B9 File Offset: 0x000263B9
		internal static DbQueryCommandTree FromValidExpression(MetadataWorkspace metadata, DataSpace dataSpace, DbExpression query, bool useDatabaseNullSemantics)
		{
			return new DbQueryCommandTree(metadata, dataSpace, query, false, useDatabaseNullSemantics);
		}

		// Token: 0x0400024E RID: 590
		private readonly DbExpression _query;

		// Token: 0x0400024F RID: 591
		private ReadOnlyCollection<DbParameterReferenceExpression> _parameters;
	}
}
