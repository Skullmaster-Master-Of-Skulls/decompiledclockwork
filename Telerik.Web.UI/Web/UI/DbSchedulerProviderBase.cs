using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020012DC RID: 4828
	public abstract class DbSchedulerProviderBase : SchedulerProviderBase
	{
		// Token: 0x1700417C RID: 16764
		// (get) Token: 0x0600CAB6 RID: 51894 RVA: 0x002D4431 File Offset: 0x002D2631
		// (set) Token: 0x0600CAB7 RID: 51895 RVA: 0x002D4439 File Offset: 0x002D2639
		public virtual DbProviderFactory DbFactory
		{
			get
			{
				return this._dbFactory;
			}
			set
			{
				this._dbFactory = value;
			}
		}

		// Token: 0x1700417D RID: 16765
		// (get) Token: 0x0600CAB8 RID: 51896 RVA: 0x002D4442 File Offset: 0x002D2642
		// (set) Token: 0x0600CAB9 RID: 51897 RVA: 0x002D444A File Offset: 0x002D264A
		public virtual bool PersistChanges
		{
			get
			{
				return this._persistChanges;
			}
			set
			{
				this._persistChanges = value;
			}
		}

		// Token: 0x1700417E RID: 16766
		// (get) Token: 0x0600CABA RID: 51898 RVA: 0x002D4453 File Offset: 0x002D2653
		// (set) Token: 0x0600CABB RID: 51899 RVA: 0x002D445B File Offset: 0x002D265B
		public virtual string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
			set
			{
				this._connectionString = value;
			}
		}

		// Token: 0x0600CABC RID: 51900 RVA: 0x002D4464 File Offset: 0x002D2664
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "DbSchedulerProvider";
			}
			base.Initialize(name, config);
			string text = config["connectionStringName"];
			if (string.IsNullOrEmpty(text))
			{
				throw new ProviderException("Missing connection string name. Please specify it with the connectionStringName property.");
			}
			ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[text];
			this._dbFactory = DbProviderFactories.GetFactory(connectionStringSettings.ProviderName);
			this._connectionString = connectionStringSettings.ConnectionString;
			string value = config["persistChanges"];
			if (!string.IsNullOrEmpty(value))
			{
				if (!bool.TryParse(value, out this._persistChanges))
				{
					throw new ProviderException("Invalid value for persistChanges attribute. Use 'True' or 'False'.");
				}
			}
			else
			{
				this._persistChanges = true;
			}
		}

		// Token: 0x0600CABD RID: 51901 RVA: 0x002D4514 File Offset: 0x002D2714
		protected virtual DbConnection OpenConnection()
		{
			DbConnection dbConnection = this.DbFactory.CreateConnection();
			dbConnection.ConnectionString = this.ConnectionString;
			dbConnection.Open();
			return dbConnection;
		}

		// Token: 0x0600CABE RID: 51902 RVA: 0x002D4540 File Offset: 0x002D2740
		protected virtual DbParameter CreateParameter(string name, object value)
		{
			DbParameter dbParameter = this.DbFactory.CreateParameter();
			dbParameter.ParameterName = name;
			dbParameter.Value = ((value != null) ? value : DBNull.Value);
			return dbParameter;
		}

		// Token: 0x04003537 RID: 13623
		private DbProviderFactory _dbFactory;

		// Token: 0x04003538 RID: 13624
		private bool _persistChanges;

		// Token: 0x04003539 RID: 13625
		private string _connectionString;
	}
}
