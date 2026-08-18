using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Design;
using System.Globalization;

namespace System.Web.UI.Design
{
	// Token: 0x0200000E RID: 14
	public class AppSettingsExpressionEditor : ExpressionEditor
	{
		// Token: 0x06000024 RID: 36 RVA: 0x00003460 File Offset: 0x00001660
		private KeyValueConfigurationCollection GetAppSettings(IServiceProvider serviceProvider)
		{
			if (serviceProvider != null)
			{
				IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
				if (webApplication != null)
				{
					Configuration configuration = webApplication.OpenWebConfiguration(true);
					if (configuration != null)
					{
						AppSettingsSection appSettings = configuration.AppSettings;
						if (appSettings != null)
						{
							return appSettings.Settings;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000034A6 File Offset: 0x000016A6
		public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		{
			return new AppSettingsExpressionEditor.AppSettingsExpressionEditorSheet(expression, this, serviceProvider);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000034B0 File Offset: 0x000016B0
		public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
		{
			KeyValueConfigurationCollection appSettings = this.GetAppSettings(serviceProvider);
			if (appSettings != null)
			{
				KeyValueConfigurationElement keyValueConfigurationElement = appSettings[expression];
				if (keyValueConfigurationElement != null)
				{
					return keyValueConfigurationElement.Value;
				}
			}
			return null;
		}

		// Token: 0x0200039E RID: 926
		private class AppSettingsExpressionEditorSheet : ExpressionEditorSheet
		{
			// Token: 0x06002592 RID: 9618 RVA: 0x000EB5FA File Offset: 0x000E97FA
			public AppSettingsExpressionEditorSheet(string expression, AppSettingsExpressionEditor owner, IServiceProvider serviceProvider) : base(serviceProvider)
			{
				this._owner = owner;
				this._appSetting = expression;
			}

			// Token: 0x170007E7 RID: 2023
			// (get) Token: 0x06002593 RID: 9619 RVA: 0x000EB611 File Offset: 0x000E9811
			// (set) Token: 0x06002594 RID: 9620 RVA: 0x000EB619 File Offset: 0x000E9819
			[DefaultValue("")]
			[SRDescription("AppSettingExpressionEditor_AppSetting")]
			[TypeConverter(typeof(AppSettingsExpressionEditor.AppSettingsExpressionEditorSheet.AppSettingsTypeConverter))]
			public string AppSetting
			{
				get
				{
					return this._appSetting;
				}
				set
				{
					this._appSetting = value;
				}
			}

			// Token: 0x170007E8 RID: 2024
			// (get) Token: 0x06002595 RID: 9621 RVA: 0x000EB622 File Offset: 0x000E9822
			public override bool IsValid
			{
				get
				{
					return !string.IsNullOrEmpty(this.AppSetting);
				}
			}

			// Token: 0x06002596 RID: 9622 RVA: 0x000EB611 File Offset: 0x000E9811
			public override string GetExpression()
			{
				return this._appSetting;
			}

			// Token: 0x04001B74 RID: 7028
			private AppSettingsExpressionEditor _owner;

			// Token: 0x04001B75 RID: 7029
			private string _appSetting;

			// Token: 0x020005B9 RID: 1465
			private class AppSettingsTypeConverter : TypeConverter
			{
				// Token: 0x060033C8 RID: 13256 RVA: 0x00010631 File Offset: 0x0000E831
				public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
				{
					return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
				}

				// Token: 0x060033C9 RID: 13257 RVA: 0x0011B4DC File Offset: 0x001196DC
				public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
				{
					if (!(value is string))
					{
						return base.ConvertFrom(context, culture, value);
					}
					if (string.Equals((string)value, AppSettingsExpressionEditor.AppSettingsExpressionEditorSheet.AppSettingsTypeConverter.NoAppSetting, StringComparison.OrdinalIgnoreCase))
					{
						return string.Empty;
					}
					return value;
				}

				// Token: 0x060033CA RID: 13258 RVA: 0x00010664 File Offset: 0x0000E864
				public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
				{
					return destType == typeof(string) || base.CanConvertTo(context, destType);
				}

				// Token: 0x060033CB RID: 13259 RVA: 0x0011B50A File Offset: 0x0011970A
				public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
				{
					if (!(value is string))
					{
						return base.ConvertTo(context, culture, value, destinationType);
					}
					if (((string)value).Length == 0)
					{
						return AppSettingsExpressionEditor.AppSettingsExpressionEditorSheet.AppSettingsTypeConverter.NoAppSetting;
					}
					return value;
				}

				// Token: 0x060033CC RID: 13260 RVA: 0x0000445B File Offset: 0x0000265B
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return false;
				}

				// Token: 0x060033CD RID: 13261 RVA: 0x0011B534 File Offset: 0x00119734
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					if (context != null)
					{
						AppSettingsExpressionEditor.AppSettingsExpressionEditorSheet appSettingsExpressionEditorSheet = (AppSettingsExpressionEditor.AppSettingsExpressionEditorSheet)context.Instance;
						AppSettingsExpressionEditor owner = appSettingsExpressionEditorSheet._owner;
						KeyValueConfigurationCollection appSettings = owner.GetAppSettings(appSettingsExpressionEditorSheet.ServiceProvider);
						if (appSettings != null)
						{
							return appSettings.Count > 0;
						}
					}
					return base.GetStandardValuesSupported(context);
				}

				// Token: 0x060033CE RID: 13262 RVA: 0x0011B578 File Offset: 0x00119778
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					if (context != null)
					{
						AppSettingsExpressionEditor.AppSettingsExpressionEditorSheet appSettingsExpressionEditorSheet = (AppSettingsExpressionEditor.AppSettingsExpressionEditorSheet)context.Instance;
						AppSettingsExpressionEditor owner = appSettingsExpressionEditorSheet._owner;
						KeyValueConfigurationCollection appSettings = owner.GetAppSettings(appSettingsExpressionEditorSheet.ServiceProvider);
						if (appSettings != null)
						{
							ArrayList arrayList = new ArrayList(appSettings.AllKeys);
							arrayList.Sort();
							arrayList.Add(string.Empty);
							return new TypeConverter.StandardValuesCollection(arrayList);
						}
					}
					return base.GetStandardValues(context);
				}

				// Token: 0x040022BB RID: 8891
				private static readonly string NoAppSetting = "(None)";
			}
		}
	}
}
