using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Data;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web.UI.Design.Util;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x02000111 RID: 273
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class SqlDataSourceDesigner : DataSourceDesigner
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0003DE20 File Offset: 0x0003C020
		public override bool CanConfigure
		{
			get
			{
				IDataEnvironment dataEnvironment = (IDataEnvironment)base.Component.Site.GetService(typeof(IDataEnvironment));
				return dataEnvironment != null;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x0003DE54 File Offset: 0x0003C054
		public override bool CanRefreshSchema
		{
			get
			{
				string connectionString = this.ConnectionString;
				return connectionString != null && connectionString.Trim().Length != 0 && this.SelectCommand.Trim().Length != 0;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0003DE8D File Offset: 0x0003C08D
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0003DE95 File Offset: 0x0003C095
		public string ConnectionString
		{
			get
			{
				return this.GetConnectionString();
			}
			set
			{
				if (value != this.ConnectionString)
				{
					this.SqlDataSource.ConnectionString = value;
					this.UpdateDesignTimeHtml();
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0000445B File Offset: 0x0000265B
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x00003937 File Offset: 0x00001B37
		[Category("Data")]
		[DefaultValue(DataSourceOperation.Delete)]
		[SRDescription("SqlDataSourceDesigner_DeleteQuery")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor(typeof(SqlDataSourceQueryEditor), typeof(UITypeEditor))]
		[MergableProperty(false)]
		[TypeConverter(typeof(SqlDataSourceQueryConverter))]
		public DataSourceOperation DeleteQuery
		{
			get
			{
				return DataSourceOperation.Delete;
			}
			set
			{
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00003B0F File Offset: 0x00001D0F
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x00003937 File Offset: 0x00001B37
		[Category("Data")]
		[DefaultValue(DataSourceOperation.Insert)]
		[SRDescription("SqlDataSourceDesigner_InsertQuery")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor(typeof(SqlDataSourceQueryEditor), typeof(UITypeEditor))]
		[MergableProperty(false)]
		[TypeConverter(typeof(SqlDataSourceQueryConverter))]
		public DataSourceOperation InsertQuery
		{
			get
			{
				return DataSourceOperation.Insert;
			}
			set
			{
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0003DEC2 File Offset: 0x0003C0C2
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0003DECF File Offset: 0x0003C0CF
		public string ProviderName
		{
			get
			{
				return this.SqlDataSource.ProviderName;
			}
			set
			{
				if (value != this.ProviderName)
				{
					this.SqlDataSource.ProviderName = value;
					this.UpdateDesignTimeHtml();
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0003DEFC File Offset: 0x0003C0FC
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0003DF25 File Offset: 0x0003C125
		internal bool SaveConfiguredConnectionState
		{
			get
			{
				object obj = base.DesignerState["SaveConfiguredConnectionState"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.DesignerState["SaveConfiguredConnectionState"] = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0003DF3D File Offset: 0x0003C13D
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x0003DF4A File Offset: 0x0003C14A
		public string SelectCommand
		{
			get
			{
				return this.SqlDataSource.SelectCommand;
			}
			set
			{
				if (value != this.SelectCommand)
				{
					this.SqlDataSource.SelectCommand = value;
					this.UpdateDesignTimeHtml();
					this.OnDataSourceChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00009D4C File Offset: 0x00007F4C
		// (set) Token: 0x060009E6 RID: 2534 RVA: 0x00003937 File Offset: 0x00001B37
		[Category("Data")]
		[DefaultValue(DataSourceOperation.Select)]
		[SRDescription("SqlDataSourceDesigner_SelectQuery")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor(typeof(SqlDataSourceQueryEditor), typeof(UITypeEditor))]
		[MergableProperty(false)]
		[TypeConverter(typeof(SqlDataSourceQueryConverter))]
		public DataSourceOperation SelectQuery
		{
			get
			{
				return DataSourceOperation.Select;
			}
			set
			{
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0003DF77 File Offset: 0x0003C177
		internal SqlDataSource SqlDataSource
		{
			get
			{
				return (SqlDataSource)base.Component;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x0003DF84 File Offset: 0x0003C184
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0003DF9B File Offset: 0x0003C19B
		internal Hashtable TableQueryState
		{
			get
			{
				return base.DesignerState["TableQueryState"] as Hashtable;
			}
			set
			{
				base.DesignerState["TableQueryState"] = value;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x00003937 File Offset: 0x00001B37
		[Category("Data")]
		[DefaultValue(DataSourceOperation.Update)]
		[SRDescription("SqlDataSourceDesigner_UpdateQuery")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor(typeof(SqlDataSourceQueryEditor), typeof(UITypeEditor))]
		[MergableProperty(false)]
		[TypeConverter(typeof(SqlDataSourceQueryConverter))]
		public DataSourceOperation UpdateQuery
		{
			get
			{
				return DataSourceOperation.Update;
			}
			set
			{
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0003DFB4 File Offset: 0x0003C1B4
		internal DbCommand BuildSelectCommand(DbProviderFactory factory, DbConnection connection, string commandText, ParameterCollection parameters, SqlDataSourceCommandType commandType)
		{
			DbCommand dbCommand = SqlDataSourceDesigner.CreateCommand(factory, commandText, connection);
			if (parameters != null && parameters.Count > 0)
			{
				IOrderedDictionary values = parameters.GetValues(null, null);
				string parameterPrefix = SqlDataSourceDesigner.GetParameterPrefix(factory);
				for (int i = 0; i < parameters.Count; i++)
				{
					Parameter parameter = parameters[i];
					DbParameter dbParameter = SqlDataSourceDesigner.CreateParameter(factory);
					dbParameter.ParameterName = parameterPrefix + parameter.Name;
					if (parameter.DbType != DbType.Object)
					{
						SqlParameter sqlParameter = dbParameter as SqlParameter;
						if (sqlParameter == null)
						{
							dbParameter.DbType = parameter.DbType;
						}
						else if (parameter.DbType == DbType.Date)
						{
							sqlParameter.SqlDbType = SqlDbType.Date;
						}
						else if (parameter.DbType == DbType.Time)
						{
							sqlParameter.SqlDbType = SqlDbType.Time;
						}
						else
						{
							dbParameter.DbType = parameter.DbType;
						}
					}
					else
					{
						if (parameter.Type != TypeCode.Empty && parameter.Type != TypeCode.DBNull)
						{
							dbParameter.DbType = parameter.GetDatabaseType();
						}
						if (parameter.Type == TypeCode.Empty && SqlDataSourceDesigner.ProviderRequiresDbTypeSet(factory))
						{
							dbParameter.DbType = DbType.Object;
						}
					}
					dbParameter.Value = values[i];
					if (dbParameter.Value == null)
					{
						dbParameter.Value = DBNull.Value;
					}
					if (Parameter.ConvertDbTypeToTypeCode(dbParameter.DbType) == TypeCode.String)
					{
						if (dbParameter.Value is string && dbParameter.Value != null)
						{
							dbParameter.Size = ((string)dbParameter.Value).Length;
						}
						else
						{
							dbParameter.Size = 1;
						}
					}
					dbCommand.Parameters.Add(dbParameter);
				}
			}
			dbCommand.CommandType = SqlDataSourceDesigner.GetCommandType(commandType);
			return dbCommand;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0003E154 File Offset: 0x0003C354
		public override void Configure()
		{
			try
			{
				this.SuppressDataSourceEvents();
				ControlDesigner.InvokeTransactedChange(base.Component, new TransactedChangeCallback(this.ConfigureDataSourceChangeCallback), null, SR.GetString("DataSource_ConfigureTransactionDescription"));
			}
			finally
			{
				this.ResumeDataSourceEvents();
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0003E1A4 File Offset: 0x0003C3A4
		private bool ConfigureDataSourceChangeCallback(object context)
		{
			IServiceProvider site = base.Component.Site;
			IDataEnvironment dataEnvironment = (IDataEnvironment)site.GetService(typeof(IDataEnvironment));
			if (dataEnvironment == null)
			{
				return false;
			}
			IDataSourceViewSchema schema = this.GetView("DefaultView").Schema;
			bool flag = false;
			if (schema == null)
			{
				this._forceSchemaRetrieval = true;
				schema = this.GetView("DefaultView").Schema;
				this._forceSchemaRetrieval = false;
				if (schema != null)
				{
					flag = true;
				}
			}
			SqlDataSourceWizardForm form = this.CreateConfigureDataSourceWizardForm(site, dataEnvironment);
			DialogResult dialogResult = UIServiceHelper.ShowDialog(site, form);
			if (dialogResult == DialogResult.OK)
			{
				this.OnComponentChanged(this, new ComponentChangedEventArgs(base.Component, null, null, null));
				IDataSourceViewSchema viewSchema = null;
				try
				{
					this._forceSchemaRetrieval = true;
					viewSchema = this.GetView("DefaultView").Schema;
				}
				finally
				{
					this._forceSchemaRetrieval = false;
				}
				if (!flag && !DataSourceDesigner.ViewSchemasEquivalent(schema, viewSchema))
				{
					this.OnSchemaRefreshed(EventArgs.Empty);
				}
				this.OnDataSourceChanged(EventArgs.Empty);
				return true;
			}
			return false;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0003E29C File Offset: 0x0003C49C
		internal static bool ConnectionsEqual(DesignerDataConnection connection1, DesignerDataConnection connection2)
		{
			if (connection1 == null || connection2 == null)
			{
				return false;
			}
			if (connection1.ConnectionString != connection2.ConnectionString)
			{
				return false;
			}
			string a = (connection1.ProviderName.Trim().Length == 0) ? "System.Data.SqlClient" : connection1.ProviderName;
			string b = (connection2.ProviderName.Trim().Length == 0) ? "System.Data.SqlClient" : connection2.ProviderName;
			return a == b;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0003E30D File Offset: 0x0003C50D
		internal static TypeCode ConvertDbTypeToTypeCode(DbType dbType)
		{
			return Parameter.ConvertDbTypeToTypeCode(dbType);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0003E315 File Offset: 0x0003C515
		internal static DbType ConvertTypeCodeToDbType(TypeCode typeCode)
		{
			return Parameter.ConvertTypeCodeToDbType(typeCode);
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0003E320 File Offset: 0x0003C520
		internal void CopyList(ICollection source, IList dest)
		{
			dest.Clear();
			foreach (object obj in source)
			{
				ICloneable cloneable = (ICloneable)obj;
				object obj2 = cloneable.Clone();
				base.RegisterClone(cloneable, obj2);
				dest.Add(obj2);
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0003E38C File Offset: 0x0003C58C
		internal virtual SqlDataSourceWizardForm CreateConfigureDataSourceWizardForm(IServiceProvider serviceProvider, IDataEnvironment dataEnvironment)
		{
			return new SqlDataSourceWizardForm(serviceProvider, this, dataEnvironment);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0003E398 File Offset: 0x0003C598
		internal static DbCommand CreateCommand(DbProviderFactory factory, string commandText, DbConnection connection)
		{
			DbCommand dbCommand = factory.CreateCommand();
			dbCommand.CommandText = commandText;
			dbCommand.Connection = connection;
			return dbCommand;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0003E3BC File Offset: 0x0003C5BC
		internal static DbDataAdapter CreateDataAdapter(DbProviderFactory factory, DbCommand command)
		{
			DbDataAdapter dbDataAdapter = factory.CreateDataAdapter();
			((IDbDataAdapter)dbDataAdapter).SelectCommand = command;
			return dbDataAdapter;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
		internal static DbParameter CreateParameter(DbProviderFactory factory)
		{
			return factory.CreateParameter();
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0003E3E0 File Offset: 0x0003C5E0
		internal static Parameter CreateParameter(DbProviderFactory factory, string name, DbType dbType)
		{
			if (SqlDataSourceDesigner.IsNewSqlServer2008Type(factory, dbType))
			{
				return new Parameter(name, dbType);
			}
			return new Parameter(name, SqlDataSourceDesigner.ConvertDbTypeToTypeCode(dbType));
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0003E3FF File Offset: 0x0003C5FF
		protected virtual SqlDesignerDataSourceView CreateView(string viewName)
		{
			return new SqlDesignerDataSourceView(this, viewName);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0003E408 File Offset: 0x0003C608
		protected virtual void DeriveParameters(string providerName, DbCommand command)
		{
			if (string.Equals(providerName, "System.Data.Odbc", StringComparison.OrdinalIgnoreCase))
			{
				OdbcCommandBuilder.DeriveParameters((OdbcCommand)command);
				return;
			}
			if (string.Equals(providerName, "System.Data.OleDb", StringComparison.OrdinalIgnoreCase))
			{
				OleDbCommandBuilder.DeriveParameters((OleDbCommand)command);
				return;
			}
			if (string.Equals(providerName, "System.Data.SqlClient", StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(providerName))
			{
				SqlCommandBuilder.DeriveParameters((SqlCommand)command);
				return;
			}
			UIServiceHelper.ShowError(this.SqlDataSource.Site, SR.GetString("SqlDataSourceDesigner_InferStoredProcedureNotSupported", new object[]
			{
				providerName
			}));
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0003E48F File Offset: 0x0003C68F
		private static CommandType GetCommandType(SqlDataSourceCommandType commandType)
		{
			if (commandType == SqlDataSourceCommandType.Text)
			{
				return CommandType.Text;
			}
			return CommandType.StoredProcedure;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0003E497 File Offset: 0x0003C697
		protected virtual string GetConnectionString()
		{
			return this.SqlDataSource.ConnectionString;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0003E4A4 File Offset: 0x0003C6A4
		internal static DbProviderFactory GetDbProviderFactory(string providerName)
		{
			if (providerName.Length == 0)
			{
				providerName = "System.Data.SqlClient";
			}
			return DbProviderFactories.GetFactory(providerName);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0003E4C8 File Offset: 0x0003C6C8
		internal static DbConnection GetDesignTimeConnection(IServiceProvider serviceProvider, DesignerDataConnection connection)
		{
			if (serviceProvider != null)
			{
				IDataEnvironment dataEnvironment = (IDataEnvironment)serviceProvider.GetService(typeof(IDataEnvironment));
				if (dataEnvironment != null)
				{
					if (string.IsNullOrEmpty(connection.ProviderName))
					{
						connection = new DesignerDataConnection(connection.Name, "System.Data.SqlClient", connection.ConnectionString);
					}
					return dataEnvironment.GetDesignTimeConnection(connection);
				}
			}
			return null;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0003E51F File Offset: 0x0003C71F
		public override DesignerDataSourceView GetView(string viewName)
		{
			if (string.IsNullOrEmpty(viewName))
			{
				viewName = "DefaultView";
			}
			if (string.Equals(viewName, "DefaultView", StringComparison.OrdinalIgnoreCase))
			{
				if (this._view == null)
				{
					this._view = this.CreateView(viewName);
				}
				return this._view;
			}
			return null;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0003E55B File Offset: 0x0003C75B
		public override string[] GetViewNames()
		{
			return new string[]
			{
				"DefaultView"
			};
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0003E56B File Offset: 0x0003C76B
		internal static string GetParameterPlaceholderPrefix(DbProviderFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			if (factory == SqlClientFactory.Instance || SqlDataSourceDesigner.IsSqlCeClientFactory(factory))
			{
				return "@";
			}
			if (factory == OracleClientFactory.Instance)
			{
				return ":";
			}
			return "?";
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0003E5A4 File Offset: 0x0003C7A4
		internal static string GetParameterPrefix(DbProviderFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			if (factory == SqlClientFactory.Instance || SqlDataSourceDesigner.IsSqlCeClientFactory(factory))
			{
				return "@";
			}
			return string.Empty;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0003E5CF File Offset: 0x0003C7CF
		private static string[] GetParameterPrefixes()
		{
			return new string[]
			{
				"@",
				"?",
				":"
			};
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		protected internal virtual Parameter[] InferParameterNames(DesignerDataConnection connection, string commandText, SqlDataSourceCommandType commandType)
		{
			Cursor value = Cursor.Current;
			Parameter[] result;
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (commandText.Length == 0)
				{
					UIServiceHelper.ShowError(this.SqlDataSource.Site, SR.GetString("SqlDataSourceDesigner_NoCommand"));
					result = null;
				}
				else if (commandType == SqlDataSourceCommandType.Text)
				{
					result = SqlDataSourceParameterParser.ParseCommandText(connection.ProviderName, commandText);
				}
				else
				{
					DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(connection.ProviderName);
					DbConnection dbConnection = null;
					try
					{
						dbConnection = SqlDataSourceDesigner.GetDesignTimeConnection(base.Component.Site, connection);
					}
					catch (Exception ex)
					{
						if (dbConnection == null)
						{
							UIServiceHelper.ShowError(this.SqlDataSource.Site, ex, SR.GetString("SqlDataSourceDesigner_CouldNotCreateConnection"));
							return null;
						}
					}
					if (dbConnection == null)
					{
						UIServiceHelper.ShowError(this.SqlDataSource.Site, SR.GetString("SqlDataSourceDesigner_CouldNotCreateConnection"));
						result = null;
					}
					else
					{
						DbCommand dbCommand = this.BuildSelectCommand(dbProviderFactory, dbConnection, commandText, null, commandType);
						dbCommand.CommandType = CommandType.StoredProcedure;
						try
						{
							this.DeriveParameters(connection.ProviderName, dbCommand);
						}
						catch (Exception ex2)
						{
							UIServiceHelper.ShowError(this.SqlDataSource.Site, SR.GetString("SqlDataSourceDesigner_InferStoredProcedureError", new object[]
							{
								ex2.Message
							}));
							return null;
						}
						finally
						{
							if (dbCommand.Connection.State == ConnectionState.Open)
							{
								dbConnection.Close();
							}
						}
						int count = dbCommand.Parameters.Count;
						Parameter[] array = new Parameter[count];
						for (int i = 0; i < count; i++)
						{
							IDataParameter dataParameter = dbCommand.Parameters[i];
							if (dataParameter != null)
							{
								string name = SqlDataSourceDesigner.StripParameterPrefix(dataParameter.ParameterName);
								array[i] = SqlDataSourceDesigner.CreateParameter(dbProviderFactory, name, dataParameter.DbType);
								array[i].Direction = dataParameter.Direction;
							}
						}
						result = array;
					}
				}
			}
			finally
			{
				Cursor.Current = value;
			}
			return result;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0003E80C File Offset: 0x0003CA0C
		internal static bool IsNewSqlServer2008Type(DbProviderFactory factory, DbType type)
		{
			return factory is SqlClientFactory && (type == DbType.Date || type == DbType.DateTime2 || type == DbType.DateTimeOffset || type == DbType.Time);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0003E830 File Offset: 0x0003CA30
		internal DataTable LoadSchema()
		{
			if (!this._forceSchemaRetrieval)
			{
				object obj = base.DesignerState["DataSourceSchemaConnectionStringHash"];
				string text = base.DesignerState["DataSourceSchemaProviderName"] as string;
				string a = base.DesignerState["DataSourceSchemaSelectMethod"] as string;
				if (string.IsNullOrEmpty(text))
				{
					text = "System.Data.SqlClient";
				}
				if (string.IsNullOrEmpty(this.ConnectionString))
				{
					return null;
				}
				DesignerDataConnection designerDataConnection = new DesignerDataConnection(string.Empty, this.ProviderName, this.ConnectionString);
				string connectionString = designerDataConnection.ConnectionString;
				int hashCode = connectionString.GetHashCode();
				string text2 = designerDataConnection.ProviderName;
				string selectCommand = this.SelectCommand;
				if (string.IsNullOrEmpty(text2))
				{
					text2 = "System.Data.SqlClient";
				}
				if (obj == null || (int)obj != hashCode || !string.Equals(text, text2, StringComparison.OrdinalIgnoreCase) || !string.Equals(a, selectCommand, StringComparison.Ordinal))
				{
					return null;
				}
			}
			DataTable dataTable = base.DesignerState["DataSourceSchema"] as DataTable;
			if (dataTable != null)
			{
				dataTable.TableName = "DefaultView";
				return dataTable;
			}
			return null;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0003E93C File Offset: 0x0003CB3C
		protected override void PreFilterProperties(IDictionary properties)
		{
			base.PreFilterProperties(properties);
			PropertyDescriptor propertyDescriptor;
			foreach (string key in SqlDataSourceDesigner._hiddenProperties)
			{
				propertyDescriptor = (PropertyDescriptor)properties[key];
				if (propertyDescriptor != null)
				{
					properties[key] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
					{
						BrowsableAttribute.No
					});
				}
			}
			properties["DeleteQuery"] = TypeDescriptor.CreateProperty(base.GetType(), "DeleteQuery", typeof(DataSourceOperation), new Attribute[0]);
			properties["InsertQuery"] = TypeDescriptor.CreateProperty(base.GetType(), "InsertQuery", typeof(DataSourceOperation), new Attribute[0]);
			properties["SelectQuery"] = TypeDescriptor.CreateProperty(base.GetType(), "SelectQuery", typeof(DataSourceOperation), new Attribute[0]);
			properties["UpdateQuery"] = TypeDescriptor.CreateProperty(base.GetType(), "UpdateQuery", typeof(DataSourceOperation), new Attribute[0]);
			propertyDescriptor = (PropertyDescriptor)properties["ConnectionString"];
			properties["ConnectionString"] = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[0]);
			propertyDescriptor = (PropertyDescriptor)properties["ProviderName"];
			properties["ProviderName"] = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[0]);
			propertyDescriptor = (PropertyDescriptor)properties["SelectCommand"];
			properties["SelectCommand"] = TypeDescriptor.CreateProperty(base.GetType(), propertyDescriptor, new Attribute[]
			{
				BrowsableAttribute.No
			});
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0003EAD7 File Offset: 0x0003CCD7
		private static bool ProviderRequiresDbTypeSet(DbProviderFactory factory)
		{
			return factory == OleDbFactory.Instance || factory == OdbcFactory.Instance;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0003EAEC File Offset: 0x0003CCEC
		public override void RefreshSchema(bool preferSilent)
		{
			try
			{
				this.SuppressDataSourceEvents();
				IServiceProvider site = this.SqlDataSource.Site;
				if (!this.CanRefreshSchema)
				{
					if (!preferSilent)
					{
						UIServiceHelper.ShowError(site, SR.GetString("SqlDataSourceDesigner_RefreshSchemaRequiresSettings"));
					}
				}
				else
				{
					IDataSourceViewSchema schema = this.GetView("DefaultView").Schema;
					bool flag = false;
					if (schema == null)
					{
						this._forceSchemaRetrieval = true;
						schema = this.GetView("DefaultView").Schema;
						this._forceSchemaRetrieval = false;
						flag = true;
					}
					DesignerDataConnection connection = new DesignerDataConnection(string.Empty, this.ProviderName, this.ConnectionString);
					bool flag2;
					if (preferSilent)
					{
						flag2 = this.RefreshSchema(connection, this.SelectCommand, this.SqlDataSource.SelectCommandType, this.SqlDataSource.SelectParameters, true);
					}
					else
					{
						Parameter[] array = this.InferParameterNames(connection, this.SelectCommand, this.SqlDataSource.SelectCommandType);
						if (array == null)
						{
							return;
						}
						ParameterCollection parameterCollection = new ParameterCollection();
						ParameterCollection parameterCollection2 = new ParameterCollection();
						foreach (object obj in this.SqlDataSource.SelectParameters)
						{
							ICloneable cloneable = (ICloneable)obj;
							parameterCollection2.Add((Parameter)cloneable.Clone());
						}
						foreach (Parameter parameter in array)
						{
							if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
							{
								Parameter parameter2 = parameterCollection2[parameter.Name];
								if (parameter2 != null)
								{
									parameter.DefaultValue = parameter2.DefaultValue;
									if (parameter.DbType == DbType.Object && parameter.Type == TypeCode.Empty)
									{
										parameter.DbType = parameter2.DbType;
										parameter.Type = parameter2.Type;
									}
									parameterCollection2.Remove(parameter2);
								}
								parameterCollection.Add(parameter);
							}
						}
						if (parameterCollection.Count > 0)
						{
							SqlDataSourceRefreshSchemaForm form = new SqlDataSourceRefreshSchemaForm(site, this, parameterCollection);
							DialogResult dialogResult = UIServiceHelper.ShowDialog(site, form);
							flag2 = (dialogResult == DialogResult.OK);
						}
						else
						{
							flag2 = this.RefreshSchema(connection, this.SelectCommand, this.SqlDataSource.SelectCommandType, parameterCollection, false);
						}
					}
					if (flag2)
					{
						IDataSourceViewSchema schema2 = this.GetView("DefaultView").Schema;
						if (flag && DataSourceDesigner.ViewSchemasEquivalent(schema, schema2))
						{
							this.OnDataSourceChanged(EventArgs.Empty);
						}
						else if (!DataSourceDesigner.ViewSchemasEquivalent(schema, schema2))
						{
							this.OnSchemaRefreshed(EventArgs.Empty);
						}
					}
				}
			}
			finally
			{
				this.ResumeDataSourceEvents();
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0003ED94 File Offset: 0x0003CF94
		internal bool RefreshSchema(DesignerDataConnection connection, string commandText, SqlDataSourceCommandType commandType, ParameterCollection parameters, bool preferSilent)
		{
			IServiceProvider site = this.SqlDataSource.Site;
			DbCommand dbCommand = null;
			try
			{
				DbProviderFactory dbProviderFactory = SqlDataSourceDesigner.GetDbProviderFactory(connection.ProviderName);
				DbConnection designTimeConnection = SqlDataSourceDesigner.GetDesignTimeConnection(base.Component.Site, connection);
				if (designTimeConnection == null)
				{
					if (!preferSilent)
					{
						UIServiceHelper.ShowError(this.SqlDataSource.Site, SR.GetString("SqlDataSourceDesigner_CouldNotCreateConnection"));
					}
					return false;
				}
				dbCommand = this.BuildSelectCommand(dbProviderFactory, designTimeConnection, commandText, parameters, commandType);
				DbDataAdapter dbDataAdapter = SqlDataSourceDesigner.CreateDataAdapter(dbProviderFactory, dbCommand);
				dbDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				DataSet dataSet = new DataSet();
				dbDataAdapter.FillSchema(dataSet, SchemaType.Source, "DefaultView");
				DataTable dataTable = dataSet.Tables["DefaultView"];
				if (dataTable == null)
				{
					if (!preferSilent)
					{
						UIServiceHelper.ShowError(site, SR.GetString("SqlDataSourceDesigner_CannotGetSchema"));
					}
					return false;
				}
				this.SaveSchema(connection, commandText, dataTable);
				return true;
			}
			catch (Exception ex)
			{
				if (!preferSilent)
				{
					UIServiceHelper.ShowError(site, ex, SR.GetString("SqlDataSourceDesigner_CannotGetSchema"));
				}
			}
			finally
			{
				if (dbCommand != null && dbCommand.Connection.State == ConnectionState.Open)
				{
					dbCommand.Connection.Close();
				}
			}
			return false;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0003EEC4 File Offset: 0x0003D0C4
		private void SaveSchema(DesignerDataConnection connection, string selectCommand, DataTable schemaTable)
		{
			base.DesignerState["DataSourceSchema"] = schemaTable;
			base.DesignerState["DataSourceSchemaConnectionStringHash"] = connection.ConnectionString.GetHashCode();
			base.DesignerState["DataSourceSchemaProviderName"] = connection.ProviderName;
			base.DesignerState["DataSourceSchemaSelectMethod"] = selectCommand;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0003EF2C File Offset: 0x0003D12C
		internal static string StripParameterPrefix(string parameterName)
		{
			foreach (string text in SqlDataSourceDesigner.GetParameterPrefixes())
			{
				if (parameterName.StartsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					return parameterName.Substring(text.Length);
				}
			}
			return parameterName;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0003EF69 File Offset: 0x0003D169
		internal static bool SupportsNamedParameters(DbProviderFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}
			return factory == SqlClientFactory.Instance || factory == OracleClientFactory.Instance || SqlDataSourceDesigner.IsSqlCeClientFactory(factory);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0003EF94 File Offset: 0x0003D194
		private static bool IsSqlCeClientFactory(DbProviderFactory factory)
		{
			return factory.GetType().FullName == "System.Data.SqlServerCe.SqlCeProviderFactory";
		}

		// Token: 0x040005F3 RID: 1523
		internal const string AspNetDatabaseObjectPrefix = "AspNet_";

		// Token: 0x040005F4 RID: 1524
		internal const string DefaultProviderName = "System.Data.SqlClient";

		// Token: 0x040005F5 RID: 1525
		internal const string DefaultViewName = "DefaultView";

		// Token: 0x040005F6 RID: 1526
		private const string DesignerStateDataSourceSchemaKey = "DataSourceSchema";

		// Token: 0x040005F7 RID: 1527
		private const string DesignerStateDataSourceSchemaConnectionStringHashKey = "DataSourceSchemaConnectionStringHash";

		// Token: 0x040005F8 RID: 1528
		private const string DesignerStateDataSourceSchemaProviderNameKey = "DataSourceSchemaProviderName";

		// Token: 0x040005F9 RID: 1529
		private const string DesignerStateDataSourceSchemaSelectCommandKey = "DataSourceSchemaSelectMethod";

		// Token: 0x040005FA RID: 1530
		private const string DesignerStateTableQueryStateKey = "TableQueryState";

		// Token: 0x040005FB RID: 1531
		private const string DesignerStateSaveConfiguredConnectionStateKey = "SaveConfiguredConnectionState";

		// Token: 0x040005FC RID: 1532
		private static readonly string[] _hiddenProperties = new string[]
		{
			"DeleteCommand",
			"DeleteParameters",
			"InsertCommand",
			"InsertParameters",
			"SelectParameters",
			"UpdateCommand",
			"UpdateParameters"
		};

		// Token: 0x040005FD RID: 1533
		private DesignerDataSourceView _view;

		// Token: 0x040005FE RID: 1534
		private bool _forceSchemaRetrieval;
	}
}
