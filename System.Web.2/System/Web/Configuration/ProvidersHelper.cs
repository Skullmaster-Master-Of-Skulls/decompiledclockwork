using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Security.Permissions;

namespace System.Web.Configuration
{
	// Token: 0x0200073F RID: 1855
	public static class ProvidersHelper
	{
		// Token: 0x06005960 RID: 22880 RVA: 0x001379C8 File Offset: 0x00135BC8
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		public static ProviderBase InstantiateProvider(ProviderSettings providerSettings, Type providerType)
		{
			ProviderBase providerBase = null;
			try
			{
				string text = (providerSettings.Type == null) ? null : providerSettings.Type.Trim();
				if (string.IsNullOrEmpty(text))
				{
					throw new ArgumentException(SR.GetString("Provider_no_type_name"));
				}
				Type type = ConfigUtil.GetType(text, "type", providerSettings, true, true);
				if (!providerType.IsAssignableFrom(type))
				{
					throw new ArgumentException(SR.GetString("Provider_must_implement_type", new object[]
					{
						providerType.ToString()
					}));
				}
				providerBase = (ProviderBase)HttpRuntime.CreatePublicInstanceByWebObjectActivator(type);
				NameValueCollection parameters = providerSettings.Parameters;
				NameValueCollection nameValueCollection = new NameValueCollection(parameters.Count, StringComparer.Ordinal);
				foreach (object obj in parameters)
				{
					string name = (string)obj;
					nameValueCollection[name] = parameters[name];
				}
				providerBase.Initialize(providerSettings.Name, nameValueCollection);
				TelemetryLogger.LogProvider(type);
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationException)
				{
					throw;
				}
				throw new ConfigurationErrorsException(ex.Message, ex, providerSettings.ElementInformation.Properties["type"].Source, providerSettings.ElementInformation.Properties["type"].LineNumber);
			}
			return providerBase;
		}

		// Token: 0x06005961 RID: 22881 RVA: 0x00137B30 File Offset: 0x00135D30
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		internal static ProviderBase InstantiateProvider(NameValueCollection providerSettings, Type providerType)
		{
			ProviderBase providerBase = null;
			try
			{
				string andRemoveStringValue = ProvidersHelper.GetAndRemoveStringValue(providerSettings, "name");
				string andRemoveStringValue2 = ProvidersHelper.GetAndRemoveStringValue(providerSettings, "type");
				if (string.IsNullOrEmpty(andRemoveStringValue2))
				{
					throw new ArgumentException(SR.GetString("Provider_no_type_name"));
				}
				Type type = ConfigUtil.GetType(andRemoveStringValue2, "type", null, null, true, true);
				if (!providerType.IsAssignableFrom(type))
				{
					throw new ArgumentException(SR.GetString("Provider_must_implement_type", new object[]
					{
						providerType.ToString()
					}));
				}
				providerBase = (ProviderBase)HttpRuntime.CreatePublicInstanceByWebObjectActivator(type);
				NameValueCollection nameValueCollection = new NameValueCollection(providerSettings.Count, StringComparer.Ordinal);
				foreach (object obj in providerSettings)
				{
					string name = (string)obj;
					nameValueCollection[name] = providerSettings[name];
				}
				providerBase.Initialize(andRemoveStringValue, nameValueCollection);
				TelemetryLogger.LogProvider(type);
			}
			catch (Exception ex)
			{
				if (ex is ConfigurationException)
				{
					throw;
				}
				throw new ConfigurationErrorsException(ex.Message, ex);
			}
			return providerBase;
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x00137C58 File Offset: 0x00135E58
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		public static void InstantiateProviders(ProviderSettingsCollection configProviders, ProviderCollection providers, Type providerType)
		{
			foreach (object obj in configProviders)
			{
				ProviderSettings providerSettings = (ProviderSettings)obj;
				providers.Add(ProvidersHelper.InstantiateProvider(providerSettings, providerType));
			}
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x00137CB4 File Offset: 0x00135EB4
		private static string GetAndRemoveStringValue(NameValueCollection collection, string key)
		{
			string text = collection[key];
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Trim();
			}
			collection.Remove(key);
			return text;
		}
	}
}
