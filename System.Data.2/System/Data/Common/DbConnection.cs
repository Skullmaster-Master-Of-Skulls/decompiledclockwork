using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.Common
{
	// Token: 0x020002E3 RID: 739
	public abstract class DbConnection : Component, IDbConnection, IDisposable
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002E8C RID: 11916
		// (set) Token: 0x06002E8D RID: 11917
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[SettingsBindable(true)]
		[DefaultValue("")]
		[RecommendedAsConfigurable(true)]
		public abstract string ConnectionString { get; set; }

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002E8E RID: 11918 RVA: 0x00127BFC File Offset: 0x00126FFC
		[ResCategory("DataCategory_Data")]
		public virtual int ConnectionTimeout
		{
			get
			{
				return 15;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002E8F RID: 11919
		[ResCategory("DataCategory_Data")]
		public abstract string Database { get; }

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002E90 RID: 11920
		[ResCategory("DataCategory_Data")]
		public abstract string DataSource { get; }

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x00127C0C File Offset: 0x0012700C
		protected virtual DbProviderFactory DbProviderFactory
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002E92 RID: 11922 RVA: 0x00127C1C File Offset: 0x0012701C
		internal DbProviderFactory ProviderFactory
		{
			get
			{
				return this.DbProviderFactory;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06002E93 RID: 11923
		[Browsable(false)]
		public abstract string ServerVersion { get; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06002E94 RID: 11924
		[Browsable(false)]
		[ResDescription("DbConnection_State")]
		public abstract ConnectionState State { get; }

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002E95 RID: 11925 RVA: 0x00127C30 File Offset: 0x00127030
		// (remove) Token: 0x06002E96 RID: 11926 RVA: 0x00127C54 File Offset: 0x00127054
		[ResCategory("DataCategory_StateChange")]
		[ResDescription("DbConnection_StateChange")]
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

		// Token: 0x06002E97 RID: 11927
		protected abstract DbTransaction BeginDbTransaction(IsolationLevel isolationLevel);

		// Token: 0x06002E98 RID: 11928 RVA: 0x00127C78 File Offset: 0x00127078
		public DbTransaction BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x00127C8C File Offset: 0x0012708C
		public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x00127CA0 File Offset: 0x001270A0
		IDbTransaction IDbConnection.BeginTransaction()
		{
			return this.BeginDbTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x00127CB4 File Offset: 0x001270B4
		IDbTransaction IDbConnection.BeginTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginDbTransaction(isolationLevel);
		}

		// Token: 0x06002E9C RID: 11932
		public abstract void Close();

		// Token: 0x06002E9D RID: 11933
		public abstract void ChangeDatabase(string databaseName);

		// Token: 0x06002E9E RID: 11934 RVA: 0x00127CC8 File Offset: 0x001270C8
		public DbCommand CreateCommand()
		{
			return this.CreateDbCommand();
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x00127CDC File Offset: 0x001270DC
		IDbCommand IDbConnection.CreateCommand()
		{
			return this.CreateDbCommand();
		}

		// Token: 0x06002EA0 RID: 11936
		protected abstract DbCommand CreateDbCommand();

		// Token: 0x06002EA1 RID: 11937 RVA: 0x00127CF0 File Offset: 0x001270F0
		public virtual void EnlistTransaction(Transaction transaction)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x00127D04 File Offset: 0x00127104
		public virtual DataTable GetSchema()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x00127D18 File Offset: 0x00127118
		public virtual DataTable GetSchema(string collectionName)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x00127D2C File Offset: 0x0012712C
		public virtual DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x00127D40 File Offset: 0x00127140
		protected virtual void OnStateChange(StateChangeEventArgs stateChange)
		{
			if (this._supressStateChangeForReconnection)
			{
				return;
			}
			StateChangeEventHandler stateChangeEventHandler = this._stateChangeEventHandler;
			if (stateChangeEventHandler != null)
			{
				stateChangeEventHandler(this, stateChange);
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x00127D68 File Offset: 0x00127168
		// (set) Token: 0x06002EA7 RID: 11943 RVA: 0x00127D7C File Offset: 0x0012717C
		internal bool ForceNewConnection { get; set; }

		// Token: 0x06002EA8 RID: 11944
		public abstract void Open();

		// Token: 0x06002EA9 RID: 11945 RVA: 0x00127D90 File Offset: 0x00127190
		public Task OpenAsync()
		{
			return this.OpenAsync(CancellationToken.None);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x00127DA8 File Offset: 0x001271A8
		public virtual Task OpenAsync(CancellationToken cancellationToken)
		{
			TaskCompletionSource<object> taskCompletionSource = new TaskCompletionSource<object>();
			if (cancellationToken.IsCancellationRequested)
			{
				taskCompletionSource.SetCanceled();
			}
			else
			{
				try
				{
					this.Open();
					taskCompletionSource.SetResult(null);
				}
				catch (Exception exception)
				{
					taskCompletionSource.SetException(exception);
				}
			}
			return taskCompletionSource.Task;
		}

		// Token: 0x04001CBD RID: 7357
		private StateChangeEventHandler _stateChangeEventHandler;

		// Token: 0x04001CBE RID: 7358
		internal bool _supressStateChangeForReconnection;
	}
}
