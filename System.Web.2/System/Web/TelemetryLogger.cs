using System;
using System.Diagnostics.Tracing;
using System.Text;
using System.Web.Security.Cryptography;

namespace System.Web
{
	// Token: 0x0200001E RID: 30
	internal static class TelemetryLogger
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x00003CFC File Offset: 0x00001EFC
		public static void LogHttpHandler(Type httpHandlerType)
		{
			if (httpHandlerType == null)
			{
				return;
			}
			try
			{
				TelemetryLogger.s_TelemetryLogger.Write<HttpHandlerTelemetryData>(TelemetryLogger.HttpHandlerEventName, TelemetryEventSource.MeasuresOptions(), new HttpHandlerTelemetryData
				{
					AppID = TelemetryLogger.s_AppID,
					HttpHandlerType = TelemetryLogger.GetHashCode(httpHandlerType.AssemblyQualifiedName)
				});
			}
			catch
			{
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003D64 File Offset: 0x00001F64
		public static void LogTargetFramework(Version targetFrameworkVersion)
		{
			if (targetFrameworkVersion == null)
			{
				return;
			}
			try
			{
				TelemetryLogger.s_TelemetryLogger.Write<TargetFrameworkTelemetryData>(TelemetryLogger.TargetFrameworkEventName, TelemetryEventSource.MeasuresOptions(), new TargetFrameworkTelemetryData
				{
					AppID = TelemetryLogger.s_AppID,
					TargetFramework = targetFrameworkVersion.ToString()
				});
			}
			catch
			{
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003DC8 File Offset: 0x00001FC8
		public static void LogProvider(Type providerType)
		{
			if (providerType == null)
			{
				return;
			}
			try
			{
				TelemetryLogger.s_TelemetryLogger.Write<ProviderTelemetryData>(TelemetryLogger.ProviderEventName, TelemetryEventSource.MeasuresOptions(), new ProviderTelemetryData
				{
					AppID = TelemetryLogger.s_AppID,
					ProviderType = TelemetryLogger.GetHashCode(providerType.AssemblyQualifiedName)
				});
			}
			catch
			{
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003E30 File Offset: 0x00002030
		private static string GetAppID()
		{
			if (HttpRuntime.AppDomainAppId != null)
			{
				return TelemetryLogger.GetHashCode(HttpRuntime.AppDomainAppId);
			}
			return string.Empty;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003E4C File Offset: 0x0000204C
		private static string GetHashCode(string str)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(str);
			return Convert.ToBase64String(CryptoUtil.ComputeSHA256Hash(bytes));
		}

		// Token: 0x040000F1 RID: 241
		private static readonly string WebFormsProviderName = "Microsoft.DOTNET.ASPNET.WebForms";

		// Token: 0x040000F2 RID: 242
		private static readonly string HttpHandlerEventName = "HandlerMapped";

		// Token: 0x040000F3 RID: 243
		private static readonly string TargetFrameworkEventName = "TargetFrameworkSet";

		// Token: 0x040000F4 RID: 244
		private static readonly string ProviderEventName = "ProviderInitialized";

		// Token: 0x040000F5 RID: 245
		private static EventSource s_TelemetryLogger = new TelemetryEventSource(TelemetryLogger.WebFormsProviderName);

		// Token: 0x040000F6 RID: 246
		private static readonly string s_AppID = TelemetryLogger.GetAppID();
	}
}
