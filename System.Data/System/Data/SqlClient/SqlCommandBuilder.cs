using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Sql;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020002C8 RID: 712
	public sealed class SqlCommandBuilder : DbCommandBuilder
	{
		// Token: 0x06002444 RID: 9284 RVA: 0x00296518 File Offset: 0x00295918
		public SqlCommandBuilder()
		{
			GC.SuppressFinalize(this);
			base.QuotePrefix = "[";
			base.QuoteSuffix = "]";
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00296548 File Offset: 0x00295948
		public SqlCommandBuilder(SqlDataAdapter adapter) : this()
		{
			this.DataAdapter = adapter;
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06002446 RID: 9286 RVA: 0x00296568 File Offset: 0x00295968
		// (set) Token: 0x06002447 RID: 9287 RVA: 0x00296578 File Offset: 0x00295978
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override CatalogLocation CatalogLocation
		{
			get
			{
				return CatalogLocation.Start;
			}
			set
			{
				if (CatalogLocation.Start != value)
				{
					throw ADP.SingleValuedProperty("CatalogLocation", "Start");
				}
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06002448 RID: 9288 RVA: 0x002965A8 File Offset: 0x002959A8
		// (set) Token: 0x06002449 RID: 9289 RVA: 0x002965C8 File Offset: 0x002959C8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string CatalogSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if ("." != value)
				{
					throw ADP.SingleValuedProperty("CatalogSeparator", ".");
				}
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x002965F8 File Offset: 0x002959F8
		// (set) Token: 0x0600244B RID: 9291 RVA: 0x00296618 File Offset: 0x00295A18
		[ResCategory("DataCategory_Update")]
		[ResDescription("SqlCommandBuilder_DataAdapter")]
		[DefaultValue(null)]
		public new SqlDataAdapter DataAdapter
		{
			get
			{
				return (SqlDataAdapter)base.DataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x00296638 File Offset: 0x00295A38
		// (set) Token: 0x0600244D RID: 9293 RVA: 0x00296658 File Offset: 0x00295A58
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string QuotePrefix
		{
			get
			{
				return base.QuotePrefix;
			}
			set
			{
				if ("[" != value && "\"" != value)
				{
					throw ADP.DoubleValuedProperty("QuotePrefix", "[", "\"");
				}
				base.QuotePrefix = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600244E RID: 9294 RVA: 0x002966A8 File Offset: 0x00295AA8
		// (set) Token: 0x0600244F RID: 9295 RVA: 0x002966C8 File Offset: 0x00295AC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string QuoteSuffix
		{
			get
			{
				return base.QuoteSuffix;
			}
			set
			{
				if ("]" != value && "\"" != value)
				{
					throw ADP.DoubleValuedProperty("QuoteSuffix", "]", "\"");
				}
				base.QuoteSuffix = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002450 RID: 9296 RVA: 0x00296718 File Offset: 0x00295B18
		// (set) Token: 0x06002451 RID: 9297 RVA: 0x00296738 File Offset: 0x00295B38
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string SchemaSeparator
		{
			get
			{
				return ".";
			}
			set
			{
				if ("." != value)
				{
					throw ADP.SingleValuedProperty("SchemaSeparator", ".");
				}
			}
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00296768 File Offset: 0x00295B68
		private void SqlRowUpdatingHandler(object sender, SqlRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00296788 File Offset: 0x00295B88
		public new SqlCommand GetInsertCommand()
		{
			return (SqlCommand)base.GetInsertCommand();
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x002967A8 File Offset: 0x00295BA8
		public new SqlCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x002967C8 File Offset: 0x00295BC8
		public new SqlCommand GetUpdateCommand()
		{
			return (SqlCommand)base.GetUpdateCommand();
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x002967E8 File Offset: 0x00295BE8
		public new SqlCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00296808 File Offset: 0x00295C08
		public new SqlCommand GetDeleteCommand()
		{
			return (SqlCommand)base.GetDeleteCommand();
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x00296828 File Offset: 0x00295C28
		public new SqlCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00296848 File Offset: 0x00295C48
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			SqlParameter sqlParameter = (SqlParameter)parameter;
			object obj = datarow[SchemaTableColumn.ProviderType];
			sqlParameter.SqlDbType = (SqlDbType)obj;
			sqlParameter.Offset = 0;
			if (sqlParameter.SqlDbType == SqlDbType.Udt && !sqlParameter.SourceColumnNullMapping)
			{
				sqlParameter.UdtTypeName = (datarow["DataTypeName"] as string);
			}
			else
			{
				sqlParameter.UdtTypeName = string.Empty;
			}
			object obj2 = datarow[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				sqlParameter.PrecisionInternal = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = datarow[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				sqlParameter.ScaleInternal = ((byte.MaxValue != b2) ? b2 : 0);
			}
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00296918 File Offset: 0x00295D18
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00296948 File Offset: 0x00295D48
		protected override string GetParameterName(string parameterName)
		{
			return "@" + parameterName;
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00296968 File Offset: 0x00295D68
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00296998 File Offset: 0x00295D98
		private void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
		{
			if (("\"" == quotePrefix && "\"" != quoteSuffix) || ("[" == quotePrefix && "]" != quoteSuffix))
			{
				throw ADP.InvalidPrefixSuffix();
			}
		}

		// Token: 0x0600245E RID: 9310 RVA: 0x002969E8 File Offset: 0x00295DE8
		public static void DeriveParameters(SqlCommand command)
		{
			SqlConnection.ExecutePermission.Demand();
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			SNIHandle target = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				target = SqlInternalConnection.GetBestEffortCleanupTarget(command.Connection);
				command.DeriveParameters();
			}
			catch (OutOfMemoryException e)
			{
				if (command != null && command.Connection != null)
				{
					command.Connection.Abort(e);
				}
				throw;
			}
			catch (StackOverflowException e2)
			{
				if (command != null && command.Connection != null)
				{
					command.Connection.Abort(e2);
				}
				throw;
			}
			catch (ThreadAbortException e3)
			{
				if (command != null && command.Connection != null)
				{
					command.Connection.Abort(e3);
				}
				SqlInternalConnection.BestEffortCleanup(target);
				throw;
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00296AD8 File Offset: 0x00295ED8
		protected override DataTable GetSchemaTable(DbCommand srcCommand)
		{
			SqlCommand sqlCommand = srcCommand as SqlCommand;
			SqlNotificationRequest notification = sqlCommand.Notification;
			bool notificationAutoEnlist = sqlCommand.NotificationAutoEnlist;
			sqlCommand.Notification = null;
			sqlCommand.NotificationAutoEnlist = false;
			DataTable schemaTable;
			try
			{
				using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
				{
					schemaTable = sqlDataReader.GetSchemaTable();
				}
			}
			finally
			{
				sqlCommand.Notification = notification;
				sqlCommand.NotificationAutoEnlist = notificationAutoEnlist;
			}
			return schemaTable;
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00296B78 File Offset: 0x00295F78
		protected override DbCommand InitializeCommand(DbCommand command)
		{
			SqlCommand sqlCommand = (SqlCommand)base.InitializeCommand(command);
			sqlCommand.NotificationAutoEnlist = false;
			return sqlCommand;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x00296BA8 File Offset: 0x00295FA8
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			return ADP.BuildQuotedString(quotePrefix, quoteSuffix, unquotedIdentifier);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00296BE8 File Offset: 0x00295FE8
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SqlDataAdapter)adapter).RowUpdating -= this.SqlRowUpdatingHandler;
				return;
			}
			((SqlDataAdapter)adapter).RowUpdating += this.SqlRowUpdatingHandler;
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00296C38 File Offset: 0x00296038
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			string result;
			ADP.RemoveStringQuotes(quotePrefix, quoteSuffix, quotedIdentifier, out result);
			return result;
		}
	}
}
