using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Common
{
	// Token: 0x020002DE RID: 734
	public abstract class DbCommand : Component, IDbCommand, IDisposable
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002DE0 RID: 11744
		// (set) Token: 0x06002DE1 RID: 11745
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_CommandText")]
		public abstract string CommandText { get; set; }

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06002DE2 RID: 11746
		// (set) Token: 0x06002DE3 RID: 11747
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_CommandTimeout")]
		public abstract int CommandTimeout { get; set; }

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002DE4 RID: 11748
		// (set) Token: 0x06002DE5 RID: 11749
		[ResDescription("DbCommand_CommandType")]
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[DefaultValue(CommandType.Text)]
		public abstract CommandType CommandType { get; set; }

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x0012582C File Offset: 0x00124C2C
		// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x00125840 File Offset: 0x00124C40
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		[Browsable(false)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_Connection")]
		public DbConnection Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = value;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x00125854 File Offset: 0x00124C54
		// (set) Token: 0x06002DE9 RID: 11753 RVA: 0x00125868 File Offset: 0x00124C68
		IDbConnection IDbCommand.Connection
		{
			get
			{
				return this.DbConnection;
			}
			set
			{
				this.DbConnection = (DbConnection)value;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002DEA RID: 11754
		// (set) Token: 0x06002DEB RID: 11755
		protected abstract DbConnection DbConnection { get; set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002DEC RID: 11756
		protected abstract DbParameterCollection DbParameterCollection { get; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002DED RID: 11757
		// (set) Token: 0x06002DEE RID: 11758
		protected abstract DbTransaction DbTransaction { get; set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002DEF RID: 11759
		// (set) Token: 0x06002DF0 RID: 11760
		[DefaultValue(true)]
		[DesignOnly(true)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract bool DesignTimeVisible { get; set; }

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002DF1 RID: 11761 RVA: 0x00125884 File Offset: 0x00124C84
		[ResDescription("DbCommand_Parameters")]
		[ResCategory("DataCategory_Data")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbParameterCollection Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002DF2 RID: 11762 RVA: 0x00125898 File Offset: 0x00124C98
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002DF3 RID: 11763 RVA: 0x001258AC File Offset: 0x00124CAC
		// (set) Token: 0x06002DF4 RID: 11764 RVA: 0x001258C0 File Offset: 0x00124CC0
		[ResDescription("DbCommand_Transaction")]
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbTransaction Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = value;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002DF5 RID: 11765 RVA: 0x001258D4 File Offset: 0x00124CD4
		// (set) Token: 0x06002DF6 RID: 11766 RVA: 0x001258E8 File Offset: 0x00124CE8
		IDbTransaction IDbCommand.Transaction
		{
			get
			{
				return this.DbTransaction;
			}
			set
			{
				this.DbTransaction = (DbTransaction)value;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002DF7 RID: 11767
		// (set) Token: 0x06002DF8 RID: 11768
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbCommand_UpdatedRowSource")]
		[DefaultValue(UpdateRowSource.Both)]
		public abstract UpdateRowSource UpdatedRowSource { get; set; }

		// Token: 0x06002DF9 RID: 11769 RVA: 0x00125904 File Offset: 0x00124D04
		internal void CancelIgnoreFailure()
		{
			try
			{
				this.Cancel();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002DFA RID: 11770
		public abstract void Cancel();

		// Token: 0x06002DFB RID: 11771 RVA: 0x00125938 File Offset: 0x00124D38
		public DbParameter CreateParameter()
		{
			return this.CreateDbParameter();
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0012594C File Offset: 0x00124D4C
		IDbDataParameter IDbCommand.CreateParameter()
		{
			return this.CreateDbParameter();
		}

		// Token: 0x06002DFD RID: 11773
		protected abstract DbParameter CreateDbParameter();

		// Token: 0x06002DFE RID: 11774
		protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

		// Token: 0x06002DFF RID: 11775
		public abstract int ExecuteNonQuery();

		// Token: 0x06002E00 RID: 11776 RVA: 0x00125960 File Offset: 0x00124D60
		public DbDataReader ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		// Token: 0x06002E01 RID: 11777 RVA: 0x00125974 File Offset: 0x00124D74
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		// Token: 0x06002E02 RID: 11778 RVA: 0x00125988 File Offset: 0x00124D88
		public DbDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		// Token: 0x06002E03 RID: 11779 RVA: 0x0012599C File Offset: 0x00124D9C
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x001259B0 File Offset: 0x00124DB0
		public Task<int> ExecuteNonQueryAsync()
		{
			return this.ExecuteNonQueryAsync(CancellationToken.None);
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x001259C8 File Offset: 0x00124DC8
		public virtual Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<int>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(new Action(this.CancelIgnoreFailure));
			}
			Task<int> result;
			try
			{
				result = Task.FromResult<int>(this.ExecuteNonQuery());
			}
			catch (Exception ex)
			{
				cancellationTokenRegistration.Dispose();
				result = ADP.CreatedTaskWithException<int>(ex);
			}
			return result;
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x00125A48 File Offset: 0x00124E48
		public Task<DbDataReader> ExecuteReaderAsync()
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, CancellationToken.None);
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x00125A64 File Offset: 0x00124E64
		public Task<DbDataReader> ExecuteReaderAsync(CancellationToken cancellationToken)
		{
			return this.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x00125A7C File Offset: 0x00124E7C
		public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior)
		{
			return this.ExecuteReaderAsync(behavior, CancellationToken.None);
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x00125A98 File Offset: 0x00124E98
		public Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			return this.ExecuteDbDataReaderAsync(behavior, cancellationToken);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x00125AB0 File Offset: 0x00124EB0
		protected virtual Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<DbDataReader>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(new Action(this.CancelIgnoreFailure));
			}
			Task<DbDataReader> result;
			try
			{
				result = Task.FromResult<DbDataReader>(this.ExecuteReader(behavior));
			}
			catch (Exception ex)
			{
				cancellationTokenRegistration.Dispose();
				result = ADP.CreatedTaskWithException<DbDataReader>(ex);
			}
			return result;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x00125B30 File Offset: 0x00124F30
		public Task<object> ExecuteScalarAsync()
		{
			return this.ExecuteScalarAsync(CancellationToken.None);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x00125B48 File Offset: 0x00124F48
		public virtual Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return ADP.CreatedTaskWithCancellation<object>();
			}
			CancellationTokenRegistration cancellationTokenRegistration = default(CancellationTokenRegistration);
			if (cancellationToken.CanBeCanceled)
			{
				cancellationTokenRegistration = cancellationToken.Register(new Action(this.CancelIgnoreFailure));
			}
			Task<object> result;
			try
			{
				result = Task.FromResult<object>(this.ExecuteScalar());
			}
			catch (Exception ex)
			{
				cancellationTokenRegistration.Dispose();
				result = ADP.CreatedTaskWithException<object>(ex);
			}
			return result;
		}

		// Token: 0x06002E0D RID: 11789
		public abstract object ExecuteScalar();

		// Token: 0x06002E0E RID: 11790
		public abstract void Prepare();
	}
}
