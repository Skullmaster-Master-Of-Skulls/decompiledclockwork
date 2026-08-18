using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x02000126 RID: 294
	public abstract class DbCommand : Component, IDbCommand, IDisposable
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060012E0 RID: 4832
		// (set) Token: 0x060012E1 RID: 4833
		[DefaultValue("")]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_CommandText")]
		[RefreshProperties(RefreshProperties.All)]
		public abstract string CommandText { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060012E2 RID: 4834
		// (set) Token: 0x060012E3 RID: 4835
		[ResCategory("DataCategory_Data")]
		[ResDescription("DbCommand_CommandTimeout")]
		public abstract int CommandTimeout { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060012E4 RID: 4836
		// (set) Token: 0x060012E5 RID: 4837
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		[ResDescription("DbCommand_CommandType")]
		[ResCategory("DataCategory_Data")]
		public abstract CommandType CommandType { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x00238B48 File Offset: 0x00237F48
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x00238B68 File Offset: 0x00237F68
		[ResCategory("DataCategory_Data")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DbCommand_Connection")]
		[DefaultValue(null)]
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00238B88 File Offset: 0x00237F88
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x00238BA8 File Offset: 0x00237FA8
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

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060012EA RID: 4842
		// (set) Token: 0x060012EB RID: 4843
		protected abstract DbConnection DbConnection { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060012EC RID: 4844
		protected abstract DbParameterCollection DbParameterCollection { get; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060012ED RID: 4845
		// (set) Token: 0x060012EE RID: 4846
		protected abstract DbTransaction DbTransaction { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060012EF RID: 4847
		// (set) Token: 0x060012F0 RID: 4848
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignOnly(true)]
		[DefaultValue(true)]
		[Browsable(false)]
		public abstract bool DesignTimeVisible { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00238BC8 File Offset: 0x00237FC8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResCategory("DataCategory_Data")]
		[Browsable(false)]
		[ResDescription("DbCommand_Parameters")]
		public DbParameterCollection Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x00238BE8 File Offset: 0x00237FE8
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.DbParameterCollection;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00238C08 File Offset: 0x00238008
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x00238C28 File Offset: 0x00238028
		[Browsable(false)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DbCommand_Transaction")]
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

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00238C48 File Offset: 0x00238048
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x00238C68 File Offset: 0x00238068
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

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060012F7 RID: 4855
		// (set) Token: 0x060012F8 RID: 4856
		[ResDescription("DbCommand_UpdatedRowSource")]
		[ResCategory("DataCategory_Update")]
		[DefaultValue(UpdateRowSource.Both)]
		public abstract UpdateRowSource UpdatedRowSource { get; set; }

		// Token: 0x060012F9 RID: 4857
		public abstract void Cancel();

		// Token: 0x060012FA RID: 4858 RVA: 0x00238C88 File Offset: 0x00238088
		public DbParameter CreateParameter()
		{
			return this.CreateDbParameter();
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00238CA8 File Offset: 0x002380A8
		IDbDataParameter IDbCommand.CreateParameter()
		{
			return this.CreateDbParameter();
		}

		// Token: 0x060012FC RID: 4860
		protected abstract DbParameter CreateDbParameter();

		// Token: 0x060012FD RID: 4861
		protected abstract DbDataReader ExecuteDbDataReader(CommandBehavior behavior);

		// Token: 0x060012FE RID: 4862
		public abstract int ExecuteNonQuery();

		// Token: 0x060012FF RID: 4863 RVA: 0x00238CC8 File Offset: 0x002380C8
		public DbDataReader ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00238CE8 File Offset: 0x002380E8
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteDbDataReader(CommandBehavior.Default);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00238D08 File Offset: 0x00238108
		public DbDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00238D28 File Offset: 0x00238128
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteDbDataReader(behavior);
		}

		// Token: 0x06001303 RID: 4867
		public abstract object ExecuteScalar();

		// Token: 0x06001304 RID: 4868
		public abstract void Prepare();
	}
}
