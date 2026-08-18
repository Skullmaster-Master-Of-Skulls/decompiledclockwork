using System;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000787 RID: 1927
	internal class InternalSqlNonSetQuery : InternalSqlQuery
	{
		// Token: 0x06005733 RID: 22323 RVA: 0x0017834C File Offset: 0x0017654C
		internal InternalSqlNonSetQuery(InternalContext internalContext, Type elementType, string sql, object[] parameters) : this(internalContext, elementType, sql, null, parameters)
		{
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x0017836D File Offset: 0x0017656D
		private InternalSqlNonSetQuery(InternalContext internalContext, Type elementType, string sql, bool? streaming, object[] parameters) : base(sql, streaming, parameters)
		{
			this._internalContext = internalContext;
			this._elementType = elementType;
		}

		// Token: 0x06005735 RID: 22325 RVA: 0x00178388 File Offset: 0x00176588
		public override InternalSqlQuery AsNoTracking()
		{
			return this;
		}

		// Token: 0x06005736 RID: 22326 RVA: 0x0017838C File Offset: 0x0017658C
		public override InternalSqlQuery AsStreaming()
		{
			if (base.Streaming == null || !base.Streaming.Value)
			{
				return new InternalSqlNonSetQuery(this._internalContext, this._elementType, base.Sql, new bool?(true), base.Parameters);
			}
			return this;
		}

		// Token: 0x06005737 RID: 22327 RVA: 0x001783DE File Offset: 0x001765DE
		public override IEnumerator GetEnumerator()
		{
			return this._internalContext.ExecuteSqlQuery(this._elementType, base.Sql, base.Streaming, base.Parameters);
		}

		// Token: 0x06005738 RID: 22328 RVA: 0x00178403 File Offset: 0x00176603
		public override IDbAsyncEnumerator GetAsyncEnumerator()
		{
			return this._internalContext.ExecuteSqlQueryAsync(this._elementType, base.Sql, base.Streaming, base.Parameters);
		}

		// Token: 0x04002325 RID: 8997
		private readonly InternalContext _internalContext;

		// Token: 0x04002326 RID: 8998
		private readonly Type _elementType;
	}
}
