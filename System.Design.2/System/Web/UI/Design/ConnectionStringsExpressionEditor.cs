using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design
{
	// Token: 0x02000014 RID: 20
	public class ConnectionStringsExpressionEditor : ExpressionEditor
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003944 File Offset: 0x00001B44
		private ConnectionStringSettingsCollection GetConnectionStringSettingsCollection(IServiceProvider serviceProvider)
		{
			if (serviceProvider != null)
			{
				IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
				if (webApplication != null)
				{
					Configuration configuration = webApplication.OpenWebConfiguration(true);
					if (configuration != null)
					{
						ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
						if (connectionStringsSection != null)
						{
							return connectionStringsSection.ConnectionStrings;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003994 File Offset: 0x00001B94
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			return new ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet(expression, this, serviceProvider);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000039A0 File Offset: 0x00001BA0
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			Pair pair = (Pair)parseTimeData;
			string a = (string)pair.First;
			bool flag = (bool)pair.Second;
			ConnectionStringSettingsCollection connectionStringSettingsCollection = this.GetConnectionStringSettingsCollection(serviceProvider);
			ConnectionStringSettings connectionStringSettings = null;
			foreach (object obj in connectionStringSettingsCollection)
			{
				ConnectionStringSettings connectionStringSettings2 = (ConnectionStringSettings)obj;
				if (string.Equals(a, connectionStringSettings2.Name, StringComparison.OrdinalIgnoreCase))
				{
					connectionStringSettings = connectionStringSettings2;
					break;
				}
			}
			if (connectionStringSettings == null)
			{
				return null;
			}
			if (flag)
			{
				return connectionStringSettings.ConnectionString;
			}
			return connectionStringSettings.ProviderName;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003A50 File Offset: 0x00001C50
		private static string ParseExpression(string expression, out bool isConnectionString)
		{
			isConnectionString = true;
			expression = expression.Trim();
			if (expression.EndsWith(".connectionstring", StringComparison.OrdinalIgnoreCase))
			{
				return expression.Substring(0, expression.Length - ".connectionstring".Length);
			}
			if (expression.EndsWith(".providername", StringComparison.OrdinalIgnoreCase))
			{
				isConnectionString = false;
				return expression.Substring(0, expression.Length - ".providername".Length);
			}
			return expression;
		}

		// Token: 0x020003A1 RID: 929
		private class ConnectionStringsExpressionEditorSheet : ExpressionEditorSheet
		{
			// Token: 0x060025A8 RID: 9640 RVA: 0x000EBB98 File Offset: 0x000E9D98
			public ConnectionStringsExpressionEditorSheet(string expression, ConnectionStringsExpressionEditor owner, IServiceProvider serviceProvider) : base(serviceProvider)
			{
				this._owner = owner;
				bool flag;
				this._connectionName = ConnectionStringsExpressionEditor.ParseExpression(expression, out flag);
				this._connectionType = (flag ? ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionType.ConnectionString : ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionType.ProviderName);
			}

			// Token: 0x170007ED RID: 2029
			// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000EBBCE File Offset: 0x000E9DCE
			// (set) Token: 0x060025AA RID: 9642 RVA: 0x000EBBD6 File Offset: 0x000E9DD6
			[DefaultValue("")]
			[SRDescription("ConnectionStringsExpressionEditor_ConnectionName")]
			[TypeConverter(typeof(ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionStringsTypeConverter))]
			public string ConnectionName
			{
				get
				{
					return this._connectionName;
				}
				set
				{
					this._connectionName = value;
				}
			}

			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x060025AB RID: 9643 RVA: 0x000EBBDF File Offset: 0x000E9DDF
			public override bool IsValid
			{
				get
				{
					return !string.IsNullOrEmpty(this.ConnectionName);
				}
			}

			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x060025AC RID: 9644 RVA: 0x000EBBEF File Offset: 0x000E9DEF
			// (set) Token: 0x060025AD RID: 9645 RVA: 0x000EBBF7 File Offset: 0x000E9DF7
			[DefaultValue(ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionType.ConnectionString)]
			[SRDescription("ConnectionStringsExpressionEditor_ConnectionType")]
			public ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionType Type
			{
				get
				{
					return this._connectionType;
				}
				set
				{
					this._connectionType = value;
				}
			}

			// Token: 0x060025AE RID: 9646 RVA: 0x000EBC00 File Offset: 0x000E9E00
			public override string GetExpression()
			{
				if (string.IsNullOrEmpty(this._connectionName))
				{
					return string.Empty;
				}
				string text = this._connectionName;
				if (this.Type == ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionType.ProviderName)
				{
					text += ".ProviderName";
				}
				return text;
			}

			// Token: 0x04001B7F RID: 7039
			private string _connectionName;

			// Token: 0x04001B80 RID: 7040
			private ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionType _connectionType;

			// Token: 0x04001B81 RID: 7041
			private ConnectionStringsExpressionEditor _owner;

			// Token: 0x020005BB RID: 1467
			public enum ConnectionType
			{
				// Token: 0x040022BE RID: 8894
				ConnectionString,
				// Token: 0x040022BF RID: 8895
				ProviderName
			}

			// Token: 0x020005BC RID: 1468
			private class ConnectionStringsTypeConverter : TypeConverter
			{
				// Token: 0x060033D5 RID: 13269 RVA: 0x00010631 File Offset: 0x0000E831
				public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
				{
					return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
				}

				// Token: 0x060033D6 RID: 13270 RVA: 0x0011B61A File Offset: 0x0011981A
				public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
				{
					if (!(value is string))
					{
						return base.ConvertFrom(context, culture, value);
					}
					if (string.Equals((string)value, ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionStringsTypeConverter.NoConnectionName, StringComparison.OrdinalIgnoreCase))
					{
						return string.Empty;
					}
					return value;
				}

				// Token: 0x060033D7 RID: 13271 RVA: 0x00010664 File Offset: 0x0000E864
				public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
				{
					return destType == typeof(string) || base.CanConvertTo(context, destType);
				}

				// Token: 0x060033D8 RID: 13272 RVA: 0x0011B648 File Offset: 0x00119848
				public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
				{
					if (!(value is string))
					{
						return base.ConvertTo(context, culture, value, destinationType);
					}
					if (((string)value).Length == 0)
					{
						return ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet.ConnectionStringsTypeConverter.NoConnectionName;
					}
					return value;
				}

				// Token: 0x060033D9 RID: 13273 RVA: 0x0000445B File Offset: 0x0000265B
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return false;
				}

				// Token: 0x060033DA RID: 13274 RVA: 0x0011B674 File Offset: 0x00119874
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					if (context != null)
					{
						ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet connectionStringsExpressionEditorSheet = (ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet)context.Instance;
						ConnectionStringsExpressionEditor owner = connectionStringsExpressionEditorSheet._owner;
						ConnectionStringSettingsCollection connectionStringSettingsCollection = owner.GetConnectionStringSettingsCollection(connectionStringsExpressionEditorSheet.ServiceProvider);
						if (connectionStringSettingsCollection != null)
						{
							return connectionStringSettingsCollection.Count > 0;
						}
					}
					return base.GetStandardValuesSupported(context);
				}

				// Token: 0x060033DB RID: 13275 RVA: 0x0011B6B8 File Offset: 0x001198B8
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					if (context != null)
					{
						ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet connectionStringsExpressionEditorSheet = (ConnectionStringsExpressionEditor.ConnectionStringsExpressionEditorSheet)context.Instance;
						ConnectionStringsExpressionEditor owner = connectionStringsExpressionEditorSheet._owner;
						ConnectionStringSettingsCollection connectionStringSettingsCollection = owner.GetConnectionStringSettingsCollection(connectionStringsExpressionEditorSheet.ServiceProvider);
						if (connectionStringSettingsCollection != null)
						{
							ArrayList arrayList = new ArrayList();
							foreach (object obj in connectionStringSettingsCollection)
							{
								ConnectionStringSettings connectionStringSettings = (ConnectionStringSettings)obj;
								arrayList.Add(connectionStringSettings.Name);
							}
							arrayList.Sort();
							arrayList.Add(string.Empty);
							return new TypeConverter.StandardValuesCollection(arrayList);
						}
					}
					return base.GetStandardValues(context);
				}

				// Token: 0x040022C0 RID: 8896
				private static readonly string NoConnectionName = "(None)";
			}
		}
	}
}
