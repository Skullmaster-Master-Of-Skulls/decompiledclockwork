using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000793 RID: 1939
	internal class InternalQuery<TElement> : IInternalQuery<TElement>, IInternalQuery
	{
		// Token: 0x060057C9 RID: 22473 RVA: 0x00179B3A File Offset: 0x00177D3A
		public InternalQuery(InternalContext internalContext)
		{
			this._internalContext = internalContext;
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x00179B49 File Offset: 0x00177D49
		public InternalQuery(InternalContext internalContext, ObjectQuery objectQuery)
		{
			this._internalContext = internalContext;
			this._objectQuery = (ObjectQuery<TElement>)objectQuery;
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x00179B64 File Offset: 0x00177D64
		public virtual void ResetQuery()
		{
			this._objectQuery = null;
		}

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x060057CC RID: 22476 RVA: 0x00179B6D File Offset: 0x00177D6D
		public virtual InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x00179B75 File Offset: 0x00177D75
		public virtual IInternalQuery<TElement> Include(string path)
		{
			return new InternalQuery<TElement>(this._internalContext, this._objectQuery.Include(path));
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x00179B8E File Offset: 0x00177D8E
		public virtual IInternalQuery<TElement> AsNoTracking()
		{
			return new InternalQuery<TElement>(this._internalContext, (ObjectQuery)DbHelpers.CreateNoTrackingQuery(this._objectQuery));
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x00179BAB File Offset: 0x00177DAB
		public virtual IInternalQuery<TElement> AsStreaming()
		{
			return new InternalQuery<TElement>(this._internalContext, (ObjectQuery)DbHelpers.CreateStreamingQuery(this._objectQuery));
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x00179BC8 File Offset: 0x00177DC8
		public virtual IInternalQuery<TElement> WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
		{
			return new InternalQuery<TElement>(this._internalContext, (ObjectQuery)DbHelpers.CreateQueryWithExecutionStrategy(this._objectQuery, executionStrategy));
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x060057D1 RID: 22481 RVA: 0x00179BE6 File Offset: 0x00177DE6
		public virtual ObjectQuery<TElement> ObjectQuery
		{
			get
			{
				return this._objectQuery;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x060057D2 RID: 22482 RVA: 0x00179BEE File Offset: 0x00177DEE
		ObjectQuery IInternalQuery.ObjectQuery
		{
			get
			{
				return this.ObjectQuery;
			}
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x00179BF6 File Offset: 0x00177DF6
		protected void InitializeQuery(ObjectQuery<TElement> objectQuery)
		{
			this._objectQuery = objectQuery;
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x00179BFF File Offset: 0x00177DFF
		public override string ToString()
		{
			return this._objectQuery.ToTraceString();
		}

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x060057D5 RID: 22485 RVA: 0x00179C0C File Offset: 0x00177E0C
		public virtual Expression Expression
		{
			get
			{
				return ((IQueryable)this._objectQuery).Expression;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x060057D6 RID: 22486 RVA: 0x00179C19 File Offset: 0x00177E19
		public virtual ObjectQueryProvider ObjectQueryProvider
		{
			get
			{
				return this._objectQuery.ObjectQueryProvider;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x060057D7 RID: 22487 RVA: 0x00179C26 File Offset: 0x00177E26
		public Type ElementType
		{
			get
			{
				return typeof(TElement);
			}
		}

		// Token: 0x060057D8 RID: 22488 RVA: 0x00179C32 File Offset: 0x00177E32
		public virtual IEnumerator<TElement> GetEnumerator()
		{
			this.InternalContext.Initialize();
			return ((IEnumerable<TElement>)this._objectQuery).GetEnumerator();
		}

		// Token: 0x060057D9 RID: 22489 RVA: 0x00179C4A File Offset: 0x00177E4A
		IEnumerator IInternalQuery.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x00179C52 File Offset: 0x00177E52
		public virtual IDbAsyncEnumerator<TElement> GetAsyncEnumerator()
		{
			this.InternalContext.Initialize();
			return ((IDbAsyncEnumerable<TElement>)this._objectQuery).GetAsyncEnumerator();
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x00179C6A File Offset: 0x00177E6A
		IDbAsyncEnumerator IInternalQuery.GetAsyncEnumerator()
		{
			return this.GetAsyncEnumerator();
		}

		// Token: 0x0400234A RID: 9034
		private readonly InternalContext _internalContext;

		// Token: 0x0400234B RID: 9035
		private ObjectQuery<TElement> _objectQuery;
	}
}
