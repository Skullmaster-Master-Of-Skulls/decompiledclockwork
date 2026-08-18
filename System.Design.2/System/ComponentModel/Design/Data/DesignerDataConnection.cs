using System;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001FC RID: 508
	public sealed class DesignerDataConnection
	{
		// Token: 0x06001334 RID: 4916 RVA: 0x0006F27C File Offset: 0x0006D47C
		public DesignerDataConnection(string name, string providerName, string connectionString) : this(name, providerName, connectionString, false)
		{
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0006F288 File Offset: 0x0006D488
		public DesignerDataConnection(string name, string providerName, string connectionString, bool isConfigured)
		{
			this._name = name;
			this._providerName = providerName;
			this._connectionString = connectionString;
			this._isConfigured = isConfigured;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x0006F2AD File Offset: 0x0006D4AD
		public string ConnectionString
		{
			get
			{
				return this._connectionString;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0006F2B5 File Offset: 0x0006D4B5
		public bool IsConfigured
		{
			get
			{
				return this._isConfigured;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x0006F2BD File Offset: 0x0006D4BD
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x0006F2C5 File Offset: 0x0006D4C5
		public string ProviderName
		{
			get
			{
				return this._providerName;
			}
		}

		// Token: 0x04000A60 RID: 2656
		private string _connectionString;

		// Token: 0x04000A61 RID: 2657
		private bool _isConfigured;

		// Token: 0x04000A62 RID: 2658
		private string _name;

		// Token: 0x04000A63 RID: 2659
		private string _providerName;
	}
}
