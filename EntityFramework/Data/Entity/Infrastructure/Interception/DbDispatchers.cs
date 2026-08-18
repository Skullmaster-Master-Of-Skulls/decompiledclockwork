using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200017E RID: 382
	public class DbDispatchers
	{
		// Token: 0x06000D09 RID: 3337 RVA: 0x0003BAA4 File Offset: 0x00039CA4
		internal DbDispatchers()
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0003BB04 File Offset: 0x00039D04
		internal virtual DbCommandTreeDispatcher CommandTree
		{
			get
			{
				return this._commandTreeDispatcher;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0003BB0C File Offset: 0x00039D0C
		public virtual DbCommandDispatcher Command
		{
			get
			{
				return this._commandDispatcher;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0003BB14 File Offset: 0x00039D14
		public virtual DbTransactionDispatcher Transaction
		{
			get
			{
				return this._transactionDispatcher;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0003BB1C File Offset: 0x00039D1C
		public virtual DbConnectionDispatcher Connection
		{
			get
			{
				return this._dbConnectionDispatcher;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0003BB24 File Offset: 0x00039D24
		internal virtual DbConfigurationDispatcher Configuration
		{
			get
			{
				return this._configurationDispatcher;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0003BB2C File Offset: 0x00039D2C
		internal virtual CancelableEntityConnectionDispatcher CancelableEntityConnection
		{
			get
			{
				return this._cancelableEntityConnectionDispatcher;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0003BB34 File Offset: 0x00039D34
		internal virtual CancelableDbCommandDispatcher CancelableCommand
		{
			get
			{
				return this._cancelableCommandDispatcher;
			}
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003BB3C File Offset: 0x00039D3C
		internal virtual void AddInterceptor(IDbInterceptor interceptor)
		{
			this._commandTreeDispatcher.InternalDispatcher.Add(interceptor);
			this._commandDispatcher.InternalDispatcher.Add(interceptor);
			this._transactionDispatcher.InternalDispatcher.Add(interceptor);
			this._dbConnectionDispatcher.InternalDispatcher.Add(interceptor);
			this._cancelableEntityConnectionDispatcher.InternalDispatcher.Add(interceptor);
			this._cancelableCommandDispatcher.InternalDispatcher.Add(interceptor);
			this._configurationDispatcher.InternalDispatcher.Add(interceptor);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003BBC0 File Offset: 0x00039DC0
		internal virtual void RemoveInterceptor(IDbInterceptor interceptor)
		{
			this._commandTreeDispatcher.InternalDispatcher.Remove(interceptor);
			this._commandDispatcher.InternalDispatcher.Remove(interceptor);
			this._transactionDispatcher.InternalDispatcher.Remove(interceptor);
			this._dbConnectionDispatcher.InternalDispatcher.Remove(interceptor);
			this._cancelableEntityConnectionDispatcher.InternalDispatcher.Remove(interceptor);
			this._cancelableCommandDispatcher.InternalDispatcher.Remove(interceptor);
			this._configurationDispatcher.InternalDispatcher.Remove(interceptor);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003BC44 File Offset: 0x00039E44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003BC4C File Offset: 0x00039E4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003BC55 File Offset: 0x00039E55
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003BC5D File Offset: 0x00039E5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000386 RID: 902
		private readonly DbCommandTreeDispatcher _commandTreeDispatcher = new DbCommandTreeDispatcher();

		// Token: 0x04000387 RID: 903
		private readonly DbCommandDispatcher _commandDispatcher = new DbCommandDispatcher();

		// Token: 0x04000388 RID: 904
		private readonly DbTransactionDispatcher _transactionDispatcher = new DbTransactionDispatcher();

		// Token: 0x04000389 RID: 905
		private readonly DbConnectionDispatcher _dbConnectionDispatcher = new DbConnectionDispatcher();

		// Token: 0x0400038A RID: 906
		private readonly DbConfigurationDispatcher _configurationDispatcher = new DbConfigurationDispatcher();

		// Token: 0x0400038B RID: 907
		private readonly CancelableEntityConnectionDispatcher _cancelableEntityConnectionDispatcher = new CancelableEntityConnectionDispatcher();

		// Token: 0x0400038C RID: 908
		private readonly CancelableDbCommandDispatcher _cancelableCommandDispatcher = new CancelableDbCommandDispatcher();
	}
}
