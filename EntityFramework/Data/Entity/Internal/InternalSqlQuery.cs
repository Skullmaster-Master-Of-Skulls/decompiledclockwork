using System;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000786 RID: 1926
	internal abstract class InternalSqlQuery : IEnumerable, IDbAsyncEnumerable
	{
		// Token: 0x0600572A RID: 22314 RVA: 0x0017830D File Offset: 0x0017650D
		internal InternalSqlQuery(string sql, bool? streaming, object[] parameters)
		{
			this._sql = sql;
			this._parameters = parameters;
			this._streaming = streaming;
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x0600572B RID: 22315 RVA: 0x0017832A File Offset: 0x0017652A
		public string Sql
		{
			get
			{
				return this._sql;
			}
		}

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x0600572C RID: 22316 RVA: 0x00178332 File Offset: 0x00176532
		internal bool? Streaming
		{
			get
			{
				return this._streaming;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600572D RID: 22317 RVA: 0x0017833A File Offset: 0x0017653A
		public object[] Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x0600572E RID: 22318
		public abstract InternalSqlQuery AsNoTracking();

		// Token: 0x0600572F RID: 22319
		public abstract InternalSqlQuery AsStreaming();

		// Token: 0x06005730 RID: 22320
		public abstract IEnumerator GetEnumerator();

		// Token: 0x06005731 RID: 22321
		public abstract IDbAsyncEnumerator GetAsyncEnumerator();

		// Token: 0x06005732 RID: 22322 RVA: 0x00178342 File Offset: 0x00176542
		public override string ToString()
		{
			return this.Sql;
		}

		// Token: 0x04002322 RID: 8994
		private readonly string _sql;

		// Token: 0x04002323 RID: 8995
		private readonly object[] _parameters;

		// Token: 0x04002324 RID: 8996
		private readonly bool? _streaming;
	}
}
