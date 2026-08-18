using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using ClockWorkLogger;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Http;
using Google.Apis.Json;
using Google.Apis.Services;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.GoogleCalendar.Impl.Adapters
{
	// Token: 0x0200000B RID: 11
	public static class CalendarServiceAdapter
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003F30 File Offset: 0x00002130
		public static CalendarService CreateGoogleCalendarService(this SyncOperationContext OpContext)
		{
			string[] scopes = new string[]
			{
				CalendarService.Scope.Calendar,
				CalendarService.Scope.CalendarReadonly
			};
			string text;
			if (OpContext == null)
			{
				text = null;
			}
			else
			{
				SyncApplicationSettings syncSettings = OpContext.SyncSettings;
				if (syncSettings == null)
				{
					text = null;
				}
				else
				{
					SyncApplicationConnection syncConnection = syncSettings.SyncConnection;
					if (syncConnection == null)
					{
						text = null;
					}
					else
					{
						SyncApplicationConnection.ServiceAccountCredentials serviceCredentials = syncConnection.ServiceCredentials;
						text = ((serviceCredentials != null) ? serviceCredentials.ServiceAccountPKCS12Filename : null);
					}
				}
			}
			string text2 = text;
			bool flag = string.IsNullOrEmpty(text2) || !File.Exists(text2);
			CalendarService result;
			if (flag)
			{
				CWLogger.Logger.Error(string.IsNullOrEmpty(text2) ? "There is not service key file" : ("Service key file does not exist at '" + text2 + "'"));
				result = null;
			}
			else
			{
				IConfigurableHttpClientInitializer httpClientInitializer = null;
				bool flag2 = Path.GetExtension(text2).ToLower() == ".json";
				if (flag2)
				{
					using (FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read))
					{
						JsonCredentialParameters jsonCredentialParameters = NewtonsoftJsonSerializer.Instance.Deserialize<JsonCredentialParameters>(fileStream);
						bool flag3 = jsonCredentialParameters.Type == "service_account" && !string.IsNullOrEmpty(jsonCredentialParameters.ClientEmail) && !string.IsNullOrEmpty(jsonCredentialParameters.PrivateKey);
						if (!flag3)
						{
							CWLogger.Logger.Error("Credential parameters missing in key file '" + text2 + "'");
							return null;
						}
						ServiceAccountCredential.Initializer initializer = new ServiceAccountCredential.Initializer(jsonCredentialParameters.ClientEmail)
						{
							Scopes = scopes
						};
						httpClientInitializer = new ServiceAccountCredential(initializer.FromPrivateKey(jsonCredentialParameters.PrivateKey));
					}
				}
				else
				{
					bool flag4 = Path.GetExtension(text2).ToLower() == ".p12";
					if (flag4)
					{
						X509Certificate2 certificate = new X509Certificate2(text2, "notasecret", X509KeyStorageFlags.Exportable);
						httpClientInitializer = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(OpContext.SyncSettings.SyncConnection.ServiceCredentials.ServiceAccountEmail)
						{
							Scopes = scopes
						}.FromCertificate(certificate));
					}
				}
				result = new CalendarService(new BaseClientService.Initializer
				{
					HttpClientInitializer = httpClientInitializer,
					ApplicationName = "ClockWork Google App Sync"
				});
			}
			return result;
		}
	}
}
