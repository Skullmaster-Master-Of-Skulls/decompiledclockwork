using System;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal.Linq;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000788 RID: 1928
	internal class InternalSqlSetQuery : InternalSqlQuery
	{
		// Token: 0x06005739 RID: 22329 RVA: 0x00178428 File Offset: 0x00176628
		internal InternalSqlSetQuery(IInternalSet set, string sql, bool isNoTracking, object[] parameters) : this(set, sql, isNoTracking, null, parameters)
		{
		}

		// Token: 0x0600573A RID: 22330 RVA: 0x00178449 File Offset: 0x00176649
		private InternalSqlSetQuery(IInternalSet set, string sql, bool isNoTracking, bool? streaming, object[] parameters) : base(sql, streaming, parameters)
		{
			this._set = set;
			this._isNoTracking = isNoTracking;
		}

		// Token: 0x0600573B RID: 22331 RVA: 0x00178464 File Offset: 0x00176664
		public override InternalSqlQuery AsNoTracking()
		{
			if (!this._isNoTracking)
			{
				return new InternalSqlSetQuery(this._set, base.Sql, true, base.Streaming, base.Parameters);
			}
			return this;
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x0600573C RID: 22332 RVA: 0x0017848E File Offset: 0x0017668E
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public bool IsNoTracking
		{
			get
			{
				return this._isNoTracking;
			}
		}

		// Token: 0x0600573D RID: 22333 RVA: 0x00178498 File Offset: 0x00176698
		public override InternalSqlQuery AsStreaming()
		{
			if (base.Streaming == null || !base.Streaming.Value)
			{
				return new InternalSqlSetQuery(this._set, base.Sql, this._isNoTracking, new bool?(true), base.Parameters);
			}
			return this;
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x001784EA File Offset: 0x001766EA
		public override IEnumerator GetEnumerator()
		{
			return this._set.ExecuteSqlQuery(base.Sql, this._isNoTracking, base.Streaming, base.Parameters);
		}

		// Token: 0x0600573F RID: 22335 RVA: 0x0017850F File Offset: 0x0017670F
		public override IDbAsyncEnumerator GetAsyncEnumerator()
		{
			return this._set.ExecuteSqlQueryAsync(base.Sql, this._isNoTracking, base.Streaming, base.Parameters);
		}

		// Token: 0x04002327 RID: 8999
		private readonly IInternalSet _set;

		// Token: 0x04002328 RID: 9000
		private readonly bool _isNoTracking;
	}
}
