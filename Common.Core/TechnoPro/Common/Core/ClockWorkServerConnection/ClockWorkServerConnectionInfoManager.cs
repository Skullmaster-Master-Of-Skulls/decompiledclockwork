using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.ClockWorkServerConnection;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServer;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.ClockWorkServerConnection
{
	// Token: 0x0200011E RID: 286
	public class ClockWorkServerConnectionInfoManager : IClockWorkServerConnectionInfoManager, IBaseOperationContext<ClockWorkServerOperationContext>
	{
		// Token: 0x06000C16 RID: 3094 RVA: 0x00054BAD File Offset: 0x00052DAD
		public ClockWorkServerConnectionInfoManager(ClockWorkServerOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00054BC0 File Offset: 0x00052DC0
		public ClockWorkServerConnectionInfo GetClockWorkServerConnectionInfo()
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string text = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"TcpHostname"
			});
			string text2 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"HttpHostname"
			});
			bool flag = string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2);
			ClockWorkServerConnectionInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int tcpPort = registryHelper.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"TcpPort"
				});
				int httpPort = registryHelper.ReadLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"HttpPort"
				});
				string text3 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"IdentityDNS"
				});
				string text4 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"CertificatePublicKey"
				});
				string text5 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"CertificateSubjectName"
				});
				string thumbprint = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"CertificateThumbprint"
				});
				string value = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"IISVersion"
				});
				result = new ClockWorkServerConnectionInfo
				{
					ClockWorkServerInstanceName = this.OpContext.ClockWorkServerInstanceName,
					TcpHostname = text,
					TcpPort = tcpPort,
					HttpHostname = text2,
					HttpPort = httpPort,
					IISVersion = ((!string.IsNullOrEmpty(value) && Enum.IsDefined(typeof(InternetInformationServicesVersion), value)) ? ((InternetInformationServicesVersion)Enum.Parse(typeof(InternetInformationServicesVersion), value)) : InternetInformationServicesVersion.IIS7),
					VirtualDirectory = this.OpContext.ClockWorkServerVirtualDirectory,
					IdentityDNS = (string.IsNullOrEmpty(text3) ? text2 : text3),
					Certificate = new CertificateInfo
					{
						CertificatePublicKey = (string.IsNullOrEmpty(text4) ? string.Empty : text4),
						SubjectName = (string.IsNullOrEmpty(text5) ? string.Empty : text5),
						IdentityDNS = (string.IsNullOrEmpty(text3) ? text2 : text3),
						Thumbprint = thumbprint
					}
				};
			}
			return result;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00054EB4 File Offset: 0x000530B4
		public void SaveClockWorkServerConnectionInfo(ClockWorkServerConnectionInfo clockWorkServerConnectionInfo)
		{
			CWLogger.Logger.Info("ClockWorkServerConnectionInfoManager::SaveClockWorkServerConnectionInfo: Saving server info to registry");
			RegistryHelper registryHelper = new RegistryHelper();
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clockWorkServerConnectionInfo.ClockWorkServerInstanceName.ToString(), new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"ClockWorkServerInstanceName"
			});
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clockWorkServerConnectionInfo.TcpHostname, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"TcpHostname"
			});
			registryHelper.WriteLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, (clockWorkServerConnectionInfo.TcpPort > 0) ? clockWorkServerConnectionInfo.TcpPort : 808, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"TcpPort"
			});
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clockWorkServerConnectionInfo.HttpHostname, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"HttpHostname"
			});
			registryHelper.WriteLocalMachineRegistry<int>(eRegWow64Options.KEY_WOW64_32KEY, (clockWorkServerConnectionInfo.HttpPort > 0) ? clockWorkServerConnectionInfo.HttpPort : 80, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"HttpPort"
			});
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, string.IsNullOrEmpty(clockWorkServerConnectionInfo.IdentityDNS) ? clockWorkServerConnectionInfo.HttpHostname : clockWorkServerConnectionInfo.IdentityDNS, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"IdentityDNS"
			});
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clockWorkServerConnectionInfo.Certificate.CertificatePublicKey ?? string.Empty, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"CertificatePublicKey"
			});
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clockWorkServerConnectionInfo.Certificate.SubjectName ?? string.Empty, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"CertificateSubjectName"
			});
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clockWorkServerConnectionInfo.Certificate.Thumbprint ?? string.Empty, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"CertificateThumbprint"
			});
			registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clockWorkServerConnectionInfo.IISVersion.ToString(), new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"IISVersion"
			});
			bool flag = !string.IsNullOrEmpty(clockWorkServerConnectionInfo.Certificate.Thumbprint);
			if (flag)
			{
				registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, "FindByThumbprint", new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"x509FindType"
				});
				registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, clockWorkServerConnectionInfo.Certificate.Thumbprint, new string[]
				{
					"ClockWorkServer Application",
					this.OpContext.ClockWorkServerVirtualDirectory,
					"x509FindValue"
				});
				CWLogger.Logger.Info("ClockWorkServerConnectionInfoManager::SaveClockWorkServerConnectionInfo: Cert thumbprint={0}", clockWorkServerConnectionInfo.Certificate.Thumbprint);
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(clockWorkServerConnectionInfo.Certificate.CertificatePublicKey);
				if (flag2)
				{
					X509Certificate2 x509Certificate = new X509Certificate2(Convert.FromBase64String(clockWorkServerConnectionInfo.Certificate.CertificatePublicKey));
					registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, "FindByThumbprint", new string[]
					{
						"ClockWorkServer Application",
						this.OpContext.ClockWorkServerVirtualDirectory,
						"x509FindType"
					});
					registryHelper.WriteLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, x509Certificate.Thumbprint, new string[]
					{
						"ClockWorkServer Application",
						this.OpContext.ClockWorkServerVirtualDirectory,
						"x509FindValue"
					});
					CWLogger.Logger.Info("ClockWorkServerConnectionInfoManager::SaveClockWorkServerConnectionInfo: Cert2 thumbprint={0}", x509Certificate.Thumbprint);
				}
			}
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000552D0 File Offset: 0x000534D0
		public ClockWorkServerInfo GetClockWorkServerInfo()
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string serverVersion = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkServer Application",
				this.OpContext.ClockWorkServerVirtualDirectory,
				"Version"
			});
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_DATABASE_NAME, false);
			string settingValue_String2 = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Department_Description, false);
			Image settingValue = webSettingManager.GetSettingValue<Image>(Setting.GENERAL_DepartmentLogoImage);
			return new ClockWorkServerInfo
			{
				DepartmentTitle = settingValue_String,
				DepartmentDescription = settingValue_String2,
				ServerVersion = serverVersion,
				PreferredBindingType = this.GetClockWorkServerPreferedBindingType(),
				DepartmentLogoImage = settingValue
			};
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000553AC File Offset: 0x000535AC
		[DebuggerStepThrough]
		public Task<ClockWorkServerInfo> GetClockWorkServerInfoAsync()
		{
			ClockWorkServerConnectionInfoManager.<GetClockWorkServerInfoAsync>d__4 <GetClockWorkServerInfoAsync>d__ = new ClockWorkServerConnectionInfoManager.<GetClockWorkServerInfoAsync>d__4();
			<GetClockWorkServerInfoAsync>d__.<>t__builder = AsyncTaskMethodBuilder<ClockWorkServerInfo>.Create();
			<GetClockWorkServerInfoAsync>d__.<>4__this = this;
			<GetClockWorkServerInfoAsync>d__.<>1__state = -1;
			<GetClockWorkServerInfoAsync>d__.<>t__builder.Start<ClockWorkServerConnectionInfoManager.<GetClockWorkServerInfoAsync>d__4>(ref <GetClockWorkServerInfoAsync>d__);
			return <GetClockWorkServerInfoAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x000553F0 File Offset: 0x000535F0
		public eBindingType GetClockWorkServerPreferedBindingType()
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_ClockWorkServer_PreferredBindingType, false);
			eBindingType result;
			bool flag = string.IsNullOrEmpty(settingValue_String) || !Enum.TryParse<eBindingType>(settingValue_String, out result);
			if (flag)
			{
				result = eBindingType.Unspecified;
			}
			return result;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x00055445 File Offset: 0x00053645
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0005544D File Offset: 0x0005364D
		public ClockWorkServerOperationContext OpContext { get; set; }
	}
}
