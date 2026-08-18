using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Management
{
	// Token: 0x02000173 RID: 371
	public sealed class RegiisUtility : IRegiisUtility
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x0003D330 File Offset: 0x0003B530
		public void RegisterSystemWebAssembly(int doReg, out IntPtr exception)
		{
			exception = IntPtr.Zero;
			try
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				RegistrationServices registrationServices = new RegistrationServices();
				if (doReg != 0)
				{
					if (!registrationServices.RegisterAssembly(executingAssembly, AssemblyRegistrationFlags.None))
					{
						exception = Marshal.StringToBSTR(new Exception(SR.GetString("Unable_To_Register_Assembly", new object[]
						{
							executingAssembly.FullName
						})).ToString());
					}
				}
				else if (!registrationServices.UnregisterAssembly(executingAssembly))
				{
					exception = Marshal.StringToBSTR(new Exception(SR.GetString("Unable_To_UnRegister_Assembly", new object[]
					{
						executingAssembly.FullName
					})).ToString());
				}
			}
			catch (Exception ex)
			{
				exception = Marshal.StringToBSTR(ex.ToString());
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0003D3E0 File Offset: 0x0003B5E0
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void RegisterAsnetMmcAssembly(int doReg, string typeName, string binaryDirectory, out IntPtr exception)
		{
			exception = IntPtr.Zero;
			try
			{
				Assembly assembly = Assembly.GetAssembly(Type.GetType(typeName, true));
				RegistrationServices registrationServices = new RegistrationServices();
				if (doReg != 0)
				{
					if (!registrationServices.RegisterAssembly(assembly, AssemblyRegistrationFlags.None))
					{
						exception = Marshal.StringToBSTR(new Exception(SR.GetString("Unable_To_Register_Assembly", new object[]
						{
							assembly.FullName
						})).ToString());
					}
					TypeLibConverter typeLibConverter = new TypeLibConverter();
					ConversionEventSink notifySink = new ConversionEventSink();
					RegiisUtility.IRegisterCreateITypeLib registerCreateITypeLib = (RegiisUtility.IRegisterCreateITypeLib)typeLibConverter.ConvertAssemblyToTypeLib(assembly, Path.Combine(binaryDirectory, "AspNetMMCExt.tlb"), TypeLibExporterFlags.None, notifySink);
					registerCreateITypeLib.SaveAllChanges();
				}
				else
				{
					if (!registrationServices.UnregisterAssembly(assembly))
					{
						exception = Marshal.StringToBSTR(new Exception(SR.GetString("Unable_To_UnRegister_Assembly", new object[]
						{
							assembly.FullName
						})).ToString());
					}
					try
					{
						File.Delete(Path.Combine(binaryDirectory, "AspNetMMCExt.tlb"));
					}
					catch
					{
					}
				}
			}
			catch (Exception ex)
			{
				exception = Marshal.StringToBSTR(ex.ToString());
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0003D4EC File Offset: 0x0003B6EC
		public void ProtectedConfigAction(long options, string firstArgument, string secondArgument, string providerName, string appPath, string site, string cspOrLocation, int keySize, out IntPtr exception)
		{
			exception = IntPtr.Zero;
			try
			{
				if ((options & 4294967296L) != 0L)
				{
					this.DoProtectSection(firstArgument, providerName, appPath, site, cspOrLocation, (options & 8796093022208L) != 0L);
				}
				else if ((options & 8589934592L) != 0L)
				{
					this.DoUnprotectSection(firstArgument, appPath, site, cspOrLocation, (options & 8796093022208L) != 0L);
				}
				else if ((options & 1125899906842624L) != 0L)
				{
					this.DoProtectSectionFile(firstArgument, secondArgument, providerName);
				}
				else if ((options & 2251799813685248L) != 0L)
				{
					this.DoUnprotectSectionFile(firstArgument, secondArgument);
				}
				else if ((options & 17179869184L) != 0L)
				{
					this.DoKeyCreate(firstArgument, cspOrLocation, options, keySize);
				}
				else if ((options & 34359738368L) != 0L)
				{
					this.DoKeyDelete(firstArgument, cspOrLocation, options);
				}
				else if ((options & 274877906944L) != 0L)
				{
					this.DoKeyExport(firstArgument, secondArgument, cspOrLocation, options);
				}
				else if ((options & 549755813888L) != 0L)
				{
					this.DoKeyImport(firstArgument, secondArgument, cspOrLocation, options);
				}
				else if ((options & 68719476736L) != 0L || (options & 137438953472L) != 0L)
				{
					this.DoKeyAclChange(firstArgument, secondArgument, cspOrLocation, options);
				}
				else
				{
					exception = Marshal.StringToBSTR(SR.GetString("Command_not_recognized"));
				}
			}
			catch (Exception exception2)
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.GetExceptionMessage(exception2, stringBuilder);
				exception = Marshal.StringToBSTR(stringBuilder.ToString());
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0003D678 File Offset: 0x0003B878
		private void GetExceptionMessage(Exception exception, StringBuilder sb)
		{
			if (sb.Length != 0)
			{
				sb.Append("\n\r");
			}
			if (exception is ConfigurationErrorsException)
			{
				using (IEnumerator enumerator = ((ConfigurationErrorsException)exception).Errors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ConfigurationErrorsException ex = (ConfigurationErrorsException)obj;
						sb.Append(ex.Message);
						sb.Append("\n\r");
						if (ex.InnerException != null)
						{
							sb.Append("\n\r");
							sb.Append(ex.InnerException.Message);
							sb.Append("\n\r");
						}
					}
					return;
				}
			}
			sb.Append(exception.Message);
			sb.Append("\n\r");
			if (exception.InnerException != null)
			{
				this.GetExceptionMessage(exception.InnerException, sb);
			}
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0003D768 File Offset: 0x0003B968
		private void DoProtectSection(string configSection, string providerName, string appPath, string site, string location, bool useMachineConfig)
		{
			Configuration configuration;
			ConfigurationSection configSection2 = this.GetConfigSection(configSection, appPath, site, location, useMachineConfig, out configuration);
			if (configSection2 == null)
			{
				throw new Exception(SR.GetString("Configuration_Section_not_found", new object[]
				{
					configSection
				}));
			}
			configSection2.SectionInformation.ProtectSection(providerName);
			configuration.Save();
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0003D7B8 File Offset: 0x0003B9B8
		private void DoUnprotectSection(string configSection, string appPath, string site, string location, bool useMachineConfig)
		{
			Configuration configuration;
			ConfigurationSection configSection2 = this.GetConfigSection(configSection, appPath, site, location, useMachineConfig, out configuration);
			if (configSection2 == null)
			{
				throw new Exception(SR.GetString("Configuration_Section_not_found", new object[]
				{
					configSection
				}));
			}
			configSection2.SectionInformation.UnprotectSection();
			configuration.Save();
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0003D804 File Offset: 0x0003BA04
		private void DoProtectSectionFile(string configSection, string dirName, string providerName)
		{
			Configuration configuration;
			ConfigurationSection configSectionFile = this.GetConfigSectionFile(configSection, dirName, out configuration);
			if (configSectionFile == null)
			{
				throw new Exception(SR.GetString("Configuration_Section_not_found", new object[]
				{
					configSection
				}));
			}
			configSectionFile.SectionInformation.ProtectSection(providerName);
			configuration.Save();
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0003D84C File Offset: 0x0003BA4C
		private void DoUnprotectSectionFile(string configSection, string dirName)
		{
			Configuration configuration;
			ConfigurationSection configSectionFile = this.GetConfigSectionFile(configSection, dirName, out configuration);
			if (configSectionFile == null)
			{
				throw new Exception(SR.GetString("Configuration_Section_not_found", new object[]
				{
					configSection
				}));
			}
			configSectionFile.SectionInformation.UnprotectSection();
			configuration.Save();
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0003D894 File Offset: 0x0003BA94
		private ConfigurationSection GetConfigSectionFile(string configSection, string dirName, out Configuration config)
		{
			if (dirName == ".")
			{
				dirName = Environment.CurrentDirectory;
			}
			else
			{
				if (!Path.IsPathRooted(dirName))
				{
					dirName = Path.Combine(Environment.CurrentDirectory, dirName);
				}
				if (!Directory.Exists(dirName))
				{
					throw new Exception(SR.GetString("Configuration_for_physical_path_not_found", new object[]
					{
						dirName
					}));
				}
			}
			WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
			string text = dirName.Replace('\\', '/');
			if (text.Length > 2 && text[1] == ':')
			{
				text = text.Substring(2);
			}
			else if (text.StartsWith("//", StringComparison.Ordinal))
			{
				text = "/";
			}
			webConfigurationFileMap.VirtualDirectories.Add(text, new VirtualDirectoryMapping(dirName, true));
			try
			{
				config = WebConfigurationManager.OpenMappedWebConfiguration(webConfigurationFileMap, text);
			}
			catch (Exception innerException)
			{
				throw new Exception(SR.GetString("Configuration_for_physical_path_not_found", new object[]
				{
					dirName
				}), innerException);
			}
			return config.GetSection(configSection);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0003D984 File Offset: 0x0003BB84
		private ConfigurationSection GetConfigSection(string configSection, string appPath, string site, string location, bool useMachineConfig, out Configuration config)
		{
			if (string.IsNullOrEmpty(appPath))
			{
				appPath = null;
			}
			if (string.IsNullOrEmpty(location))
			{
				location = null;
			}
			try
			{
				if (useMachineConfig)
				{
					config = WebConfigurationManager.OpenMachineConfiguration(location);
				}
				else
				{
					config = WebConfigurationManager.OpenWebConfiguration(appPath, site, location);
				}
			}
			catch (Exception innerException)
			{
				if (useMachineConfig)
				{
					throw new Exception(SR.GetString("Configuration_for_machine_config_not_found"), innerException);
				}
				throw new Exception(SR.GetString("Configuration_for_path_not_found", new object[]
				{
					appPath,
					string.IsNullOrEmpty(site) ? SR.GetString("DefaultSiteName") : site
				}), innerException);
			}
			return config.GetSection(configSection);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0003DA2C File Offset: 0x0003BC2C
		private void DoKeyCreate(string containerName, string csp, long options, int keySize)
		{
			if (containerName == null || containerName.Length < 1)
			{
				containerName = "NetFrameworkConfigurationKey";
			}
			uint num = (uint)UnsafeNativeMethods.DoesKeyContainerExist(containerName, csp, ((options & 17592186044416L) == 0L) ? 1 : 0);
			if (num == 0U)
			{
				throw new Exception(SR.GetString("RSA_Key_Container_already_exists"));
			}
			if (num == 2147942405U)
			{
				throw new Exception(SR.GetString("RSA_Key_Container_access_denied"));
			}
			if (num != 2148073494U)
			{
				Marshal.ThrowExceptionForHR((int)num);
				return;
			}
			RsaProtectedConfigurationProvider rsaProtectedConfigurationProvider = this.CreateRSAProvider(containerName, csp, options);
			try
			{
				rsaProtectedConfigurationProvider.AddKey(keySize, (options & 70368744177664L) != 0L);
			}
			catch
			{
				rsaProtectedConfigurationProvider.DeleteKey();
				throw;
			}
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0003DAE0 File Offset: 0x0003BCE0
		private void DoKeyDelete(string containerName, string csp, long options)
		{
			if (containerName == null || containerName.Length < 1)
			{
				containerName = "NetFrameworkConfigurationKey";
			}
			RegiisUtility.MakeSureContainerExists(containerName, csp, (options & 17592186044416L) == 0L);
			RsaProtectedConfigurationProvider rsaProtectedConfigurationProvider = this.CreateRSAProvider(containerName, csp, options);
			rsaProtectedConfigurationProvider.DeleteKey();
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0003DB28 File Offset: 0x0003BD28
		private void DoKeyExport(string containerName, string fileName, string csp, long options)
		{
			if (!Path.IsPathRooted(fileName))
			{
				fileName = Path.Combine(Environment.CurrentDirectory, fileName);
			}
			if (!Directory.Exists(Path.GetDirectoryName(fileName)))
			{
				throw new DirectoryNotFoundException();
			}
			if (containerName == null || containerName.Length < 1)
			{
				containerName = "NetFrameworkConfigurationKey";
			}
			RegiisUtility.MakeSureContainerExists(containerName, csp, (options & 17592186044416L) == 0L);
			RsaProtectedConfigurationProvider rsaProtectedConfigurationProvider = this.CreateRSAProvider(containerName, csp, options);
			rsaProtectedConfigurationProvider.ExportKey(fileName, (options & 281474976710656L) != 0L);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0003DBAC File Offset: 0x0003BDAC
		private void DoKeyImport(string containerName, string fileName, string csp, long options)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException();
			}
			if (containerName == null || containerName.Length < 1)
			{
				containerName = "NetFrameworkConfigurationKey";
			}
			RsaProtectedConfigurationProvider rsaProtectedConfigurationProvider = this.CreateRSAProvider(containerName, csp, options);
			rsaProtectedConfigurationProvider.ImportKey(fileName, (options & 70368744177664L) != 0L);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0003DBFC File Offset: 0x0003BDFC
		private void DoKeyAclChange(string containerName, string account, string csp, long options)
		{
			if (containerName == null || containerName.Length < 1)
			{
				containerName = "NetFrameworkConfigurationKey";
			}
			RegiisUtility.MakeSureContainerExists(containerName, csp, (options & 17592186044416L) == 0L);
			int num = 0;
			if ((options & 68719476736L) != 0L)
			{
				num |= 1;
			}
			if ((options & 17592186044416L) == 0L)
			{
				num |= 2;
			}
			if ((options & 140737488355328L) != 0L)
			{
				num |= 4;
			}
			int num2 = UnsafeNativeMethods.ChangeAccessToKeyContainer(containerName, account, csp, num);
			if (num2 != 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0003DC80 File Offset: 0x0003BE80
		private RsaProtectedConfigurationProvider CreateRSAProvider(string containerName, string csp, long options)
		{
			RsaProtectedConfigurationProvider rsaProtectedConfigurationProvider = new RsaProtectedConfigurationProvider();
			rsaProtectedConfigurationProvider.Initialize("foo", new NameValueCollection
			{
				{
					"keyContainerName",
					containerName
				},
				{
					"cspProviderName",
					csp
				},
				{
					"useMachineContainer",
					((options & 17592186044416L) != 0L) ? "false" : "true"
				}
			});
			return rsaProtectedConfigurationProvider;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0003DCE4 File Offset: 0x0003BEE4
		private static void MakeSureContainerExists(string containerName, string csp, bool machineContainer)
		{
			uint num = (uint)UnsafeNativeMethods.DoesKeyContainerExist(containerName, csp, machineContainer ? 1 : 0);
			if (num == 0U)
			{
				return;
			}
			if (num == 2147942405U)
			{
				throw new Exception(SR.GetString("RSA_Key_Container_access_denied"));
			}
			if (num != 2148073494U)
			{
				Marshal.ThrowExceptionForHR((int)num);
				return;
			}
			throw new Exception(SR.GetString("RSA_Key_Container_not_found"));
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0003DD3C File Offset: 0x0003BF3C
		public void RemoveBrowserCaps(out IntPtr exception)
		{
			try
			{
				BrowserCapabilitiesCodeGenerator browserCapabilitiesCodeGenerator = new BrowserCapabilitiesCodeGenerator();
				browserCapabilitiesCodeGenerator.UninstallInternal();
				exception = IntPtr.Zero;
			}
			catch (Exception ex)
			{
				exception = Marshal.StringToBSTR(ex.Message);
			}
		}

		// Token: 0x04001546 RID: 5446
		private const int WATSettingLocalOnly = 0;

		// Token: 0x04001547 RID: 5447
		private const int WATSettingRequireSSL = 1;

		// Token: 0x04001548 RID: 5448
		private const int WATSettingAuthSettings = 2;

		// Token: 0x04001549 RID: 5449
		private const int WATSettingAuthMode = 3;

		// Token: 0x0400154A RID: 5450
		private const int WATSettingMax = 4;

		// Token: 0x0400154B RID: 5451
		private const int WATValueDoNothing = 0;

		// Token: 0x0400154C RID: 5452
		private const int WATValueTrue = 1;

		// Token: 0x0400154D RID: 5453
		private const int WATValueFalse = 2;

		// Token: 0x0400154E RID: 5454
		private const int WATValueHosted = 3;

		// Token: 0x0400154F RID: 5455
		private const int WATValueLocal = 4;

		// Token: 0x04001550 RID: 5456
		private const int WATValueForms = 5;

		// Token: 0x04001551 RID: 5457
		private const int WATValueWindows = 6;

		// Token: 0x04001552 RID: 5458
		private const string DefaultRsaKeyContainerName = "NetFrameworkConfigurationKey";

		// Token: 0x04001553 RID: 5459
		private const string NewLine = "\n\r";

		// Token: 0x04001554 RID: 5460
		private const long DO_RSA_ENCRYPT = 4294967296L;

		// Token: 0x04001555 RID: 5461
		private const long DO_RSA_DECRYPT = 8589934592L;

		// Token: 0x04001556 RID: 5462
		private const long DO_RSA_ADD_KEY = 17179869184L;

		// Token: 0x04001557 RID: 5463
		private const long DO_RSA_DEL_KEY = 34359738368L;

		// Token: 0x04001558 RID: 5464
		private const long DO_RSA_ACL_KEY_ADD = 68719476736L;

		// Token: 0x04001559 RID: 5465
		private const long DO_RSA_ACL_KEY_DEL = 137438953472L;

		// Token: 0x0400155A RID: 5466
		private const long DO_RSA_EXPORT_KEY = 274877906944L;

		// Token: 0x0400155B RID: 5467
		private const long DO_RSA_IMPORT_KEY = 549755813888L;

		// Token: 0x0400155C RID: 5468
		private const long DO_RSA_PKM = 8796093022208L;

		// Token: 0x0400155D RID: 5469
		private const long DO_RSA_PKU = 17592186044416L;

		// Token: 0x0400155E RID: 5470
		private const long DO_RSA_EXPORTABLE = 70368744177664L;

		// Token: 0x0400155F RID: 5471
		private const long DO_RSA_FULL_ACCESS = 140737488355328L;

		// Token: 0x04001560 RID: 5472
		private const long DO_RSA_PRIVATE = 281474976710656L;

		// Token: 0x04001561 RID: 5473
		private const long DO_RSA_ENCRYPT_FILE = 1125899906842624L;

		// Token: 0x04001562 RID: 5474
		private const long DO_RSA_DECRYPT_FILE = 2251799813685248L;

		// Token: 0x0200090D RID: 2317
		[Guid("00020406-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComVisible(false)]
		[ComImport]
		private interface IRegisterCreateITypeLib
		{
			// Token: 0x060068EE RID: 26862
			void CreateTypeInfo();

			// Token: 0x060068EF RID: 26863
			void SetName();

			// Token: 0x060068F0 RID: 26864
			void SetVersion();

			// Token: 0x060068F1 RID: 26865
			void SetGuid();

			// Token: 0x060068F2 RID: 26866
			void SetDocString();

			// Token: 0x060068F3 RID: 26867
			void SetHelpFileName();

			// Token: 0x060068F4 RID: 26868
			void SetHelpContext();

			// Token: 0x060068F5 RID: 26869
			void SetLcid();

			// Token: 0x060068F6 RID: 26870
			void SetLibFlags();

			// Token: 0x060068F7 RID: 26871
			void SaveAllChanges();
		}
	}
}
