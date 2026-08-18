using System;
using System.ComponentModel;
using System.Transactions;

namespace System.Data.Common
{
	// Token: 0x02000129 RID: 297
	public abstract class DbConnection : Component, IDbConnection, IDisposable
	{
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001358 RID: 4952
		// (set) Token: 0x06001359 RID: 4953
		[ResCategory("DataCategory_Data")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[RecommendedAsConfigurable(true)]
		public abstract string ConnectionString { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600135A RID: 4954 RVA: 0x0023AC68 File Offset: 0x0023A068
		[ResCategory("DataCategory_Data")]
		public virtual int ConnectionTimeout
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600135B RID: 4955
		[ResCategory("DataCategory_Data")]
		public abstract string Database { get; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600135C RID: 4956
		[ResCategory("DataCategory_Data")]
		public abstract string DataSource { get; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0023AC78 File Offset: 0x0023A078
		protected virtual DbProviderFactory DbProviderFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600135E RID: 4958 RVA: 0x0023AC88 File Offset: 0x0023A088
		internal DbProviderFactory ProviderFactory
		{
			get
			{
				return this.DbProviderFactory;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600135F RID: 4959
		[Browsable(false)]
		public abstract string ServerVersion { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001360 RID: 4960
		[Browsable(false)]
		[ResDescription("DbConnection_State")]
		public abstract ConnectionState State { get; }

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001361 RID: 4961 RVA: 0x0023ACA8 File Offset: 0x0023A0A8
		// (remove) Token: 0x06001362 RID: 4962 RVA: 0x0023ACD8 File Offset: 0x0023A0D8
		[ResDescription("DbConnection_StateChange")]
		[ResCategory("DataCategory_StateChange")]
		public virtual event StateChangeEventHandler StateChange
		{
			add
			{
				this._stateChangeEventHandler = (StateChangeEventHandler)Delegate.Combine(this._stateChangeEventHandler, value);
			}
			remove
			{
				this._stateChangeEventHandler = (StateChangeEventHandler)Delegate.Remove(this._stateChangeEventHandler, value);
			}
		}

		// Token: 0x06001363 RID: 4963
		protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

		// Token: 0x06001364 RID: 4964 RVA: 0x0023AD08 File Offset: 0x0023A108
		public DbTransaction BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0023AD28 File Offset: 0x0023A128
		public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0023AD48 File Offset: 0x0023A148
		IDbTransaction IDbConnection.BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0023AD68 File Offset: 0x0023A168
		IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		// Token: 0x06001368 RID: 4968
		public abstract void Close();

		// Token: 0x06001369 RID: 4969
		public abstract void ChangeDatabase(string databaseName);

		// Token: 0x0600136A RID: 4970 RVA: 0x0023AD88 File Offset: 0x0023A188
		public DbCommand CreateCommand()
		{
			return this.CreateDbCommand();
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0023ADA8 File Offset: 0x0023A1A8
		IDbCommand IDbConnection.CreateCommand()
		{
			return this.CreateDbCommand();
		}

		// Token: 0x0600136C RID: 4972
		protected abstract DbCommand CreateDbCommand();

		// Token: 0x0600136D RID: 4973 RVA: 0x0023ADC8 File Offset: 0x0023A1C8
		public virtual void EnlistTransaction(Transaction transaction)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0023ADE8 File Offset: 0x0023A1E8
		public virtual DataTable GetSchema()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0023AE08 File Offset: 0x0023A208
		public virtual DataTable GetSchema(string collectionName)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0023AE28 File Offset: 0x0023A228
		public virtual DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0023AE48 File Offset: 0x0023A248
		protected virtual void OnStateChange(StateChangeEventArgs stateChange)
		{
			StateChangeEventHandler stateChangeEventHandler = this._stateChangeEventHandler;
			if (stateChangeEventHandler != null)
			{
				stateChangeEventHandler(this, stateChange);
			}
		}

		// Token: 0x06001372 RID: 4978
		public abstract void Open();

		// Token: 0x04000C07 RID: 3079
		private StateChangeEventHandler _stateChangeEventHandler;
	}
}
