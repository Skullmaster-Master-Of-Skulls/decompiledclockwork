using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Sql;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001B3 RID: 435
	public sealed class SqlCommandBuilder : DbCommandBuilder
	{
		// Token: 0x060019FF RID: 6655 RVA: 0x000B98C4 File Offset: 0x000B8CC4
		public SqlCommandBuilder()
		{
			GC.SuppressFinalize(this);
			base.QuotePrefix = "[";
			base.QuoteSuffix = "]";
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000B98F4 File Offset: 0x000B8CF4
		public SqlCommandBuilder(SqlDataAdapter adapter) : this()
		{
			this.DataAdapter = adapter;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06001A01 RID: 6657 RVA: 0x000B9910 File Offset: 0x000B8D10
		// (set) Token: 0x06001A02 RID: 6658 RVA: 0x000B9920 File Offset: 0x000B8D20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06001A03 RID: 6659 RVA: 0x000B9944 File Offset: 0x000B8D44
		// (set) Token: 0x06001A04 RID: 6660 RVA: 0x000B9958 File Offset: 0x000B8D58
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06001A05 RID: 6661 RVA: 0x000B9984 File Offset: 0x000B8D84
		// (set) Token: 0x06001A06 RID: 6662 RVA: 0x000B999C File Offset: 0x000B8D9C
		[ResDescription("SqlCommandBuilder_DataAdapter")]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06001A07 RID: 6663 RVA: 0x000B99B0 File Offset: 0x000B8DB0
		// (set) Token: 0x06001A08 RID: 6664 RVA: 0x000B99C4 File Offset: 0x000B8DC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x000B9A08 File Offset: 0x000B8E08
		// (set) Token: 0x06001A0A RID: 6666 RVA: 0x000B9A1C File Offset: 0x000B8E1C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x000B9A60 File Offset: 0x000B8E60
		// (set) Token: 0x06001A0C RID: 6668 RVA: 0x000B9A74 File Offset: 0x000B8E74
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x06001A0D RID: 6669 RVA: 0x000B9AA0 File Offset: 0x000B8EA0
		private void SqlRowUpdatingHandler(object sender, SqlRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000B9AB4 File Offset: 0x000B8EB4
		public new SqlCommand GetInsertCommand()
		{
			return (SqlCommand)base.GetInsertCommand();
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000B9ACC File Offset: 0x000B8ECC
		public new SqlCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000B9AE8 File Offset: 0x000B8EE8
		public new SqlCommand GetUpdateCommand()
		{
			return (SqlCommand)base.GetUpdateCommand();
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000B9B00 File Offset: 0x000B8F00
		public new SqlCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000B9B1C File Offset: 0x000B8F1C
		public new SqlCommand GetDeleteCommand()
		{
			return (SqlCommand)base.GetDeleteCommand();
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000B9B34 File Offset: 0x000B8F34
		public new SqlCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (SqlCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000B9B50 File Offset: 0x000B8F50
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

		// Token: 0x06001A15 RID: 6677 RVA: 0x000B9C18 File Offset: 0x000B9018
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000B9C3C File Offset: 0x000B903C
		protected override string GetParameterName(string parameterName)
		{
			return "@" + parameterName;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000B9C54 File Offset: 0x000B9054
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "@p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000B9C78 File Offset: 0x000B9078
		private void ConsistentQuoteDelimiters(string quotePrefix, string quoteSuffix)
		{
			if (("\"" == quotePrefix && "\"" != quoteSuffix) || ("[" == quotePrefix && "]" != quoteSuffix))
			{
				throw ADP.InvalidPrefixSuffix();
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000B9CC0 File Offset: 0x000B90C0
		public static void DeriveParameters(SqlCommand command)
		{
			SqlConnection.ExecutePermission.Demand();
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			TdsParser target = null;
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

		// Token: 0x06001A1A RID: 6682 RVA: 0x000B9DA8 File Offset: 0x000B91A8
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

		// Token: 0x06001A1B RID: 6683 RVA: 0x000B9E3C File Offset: 0x000B923C
		protected override DbCommand InitializeCommand(DbCommand command)
		{
			SqlCommand sqlCommand = (SqlCommand)base.InitializeCommand(command);
			sqlCommand.NotificationAutoEnlist = false;
			return sqlCommand;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000B9E60 File Offset: 0x000B9260
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string quoteSuffix = this.QuoteSuffix;
			string quotePrefix = this.QuotePrefix;
			this.ConsistentQuoteDelimiters(quotePrefix, quoteSuffix);
			return ADP.BuildQuotedString(quotePrefix, quoteSuffix, unquotedIdentifier);
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000B9E98 File Offset: 0x000B9298
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((SqlDataAdapter)adapter).RowUpdating -= this.SqlRowUpdatingHandler;
				return;
			}
			((SqlDataAdapter)adapter).RowUpdating += this.SqlRowUpdatingHandler;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000B9EE0 File Offset: 0x000B92E0
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
