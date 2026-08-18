using System;
using System.IO;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Xml;
using Microsoft.Win32;

namespace System.Configuration.Internal
{
	// Token: 0x020000BC RID: 188
	internal sealed class InternalConfigHost : IInternalConfigHost, IInternalConfigurationBuilderHost
	{
		// Token: 0x06000750 RID: 1872 RVA: 0x000115BE File Offset: 0x0000F7BE
		internal InternalConfigHost()
		{
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001F9F0 File Offset: 0x0001DBF0
		void IInternalConfigHost.Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
		{
			this._configRoot = configRoot;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001F9F9 File Offset: 0x0001DBF9
		void IInternalConfigHost.InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot configRoot, params object[] hostInitConfigurationParams)
		{
			this._configRoot = configRoot;
			configPath = null;
			locationConfigPath = null;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000874E File Offset: 0x0000694E
		bool IInternalConfigHost.IsConfigRecordRequired(string configPath)
		{
			return true;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00008751 File Offset: 0x00006951
		bool IInternalConfigHost.IsInitDelayed(IInternalConfigRecord configRecord)
		{
			return false;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00005E74 File Offset: 0x00004074
		void IInternalConfigHost.RequireCompleteInit(IInternalConfigRecord configRecord)
		{
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00008751 File Offset: 0x00006951
		public bool IsSecondaryRoot(string configPath)
		{
			return false;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001FA09 File Offset: 0x0001DC09
		string IInternalConfigHost.GetStreamName(string configPath)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.GetStreamName");
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001FA18 File Offset: 0x0001DC18
		[FileIOPermission(SecurityAction.Assert, AllFiles = FileIOPermissionAccess.PathDiscovery)]
		internal static string StaticGetStreamNameForConfigSource(string streamName, string configSource)
		{
			if (!Path.IsPathRooted(streamName))
			{
				throw ExceptionUtil.ParameterInvalid("streamName");
			}
			streamName = Path.GetFullPath(streamName);
			string directoryOrRootName = UrlPath.GetDirectoryOrRootName(streamName);
			string text = Path.Combine(directoryOrRootName, configSource);
			text = Path.GetFullPath(text);
			string directoryOrRootName2 = UrlPath.GetDirectoryOrRootName(text);
			if (!UrlPath.IsEqualOrSubdirectory(directoryOrRootName, directoryOrRootName2))
			{
				throw new ArgumentException(SR.GetString("Config_source_not_under_config_dir", new object[]
				{
					configSource
				}));
			}
			return text;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001FA81 File Offset: 0x0001DC81
		string IInternalConfigHost.GetStreamNameForConfigSource(string streamName, string configSource)
		{
			return InternalConfigHost.StaticGetStreamNameForConfigSource(streamName, configSource);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		internal static object StaticGetStreamVersion(string streamName)
		{
			bool exists = false;
			long fileSize = 0L;
			DateTime utcCreationTime = DateTime.MinValue;
			DateTime utcLastWriteTime = DateTime.MinValue;
			UnsafeNativeMethods.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA;
			if (UnsafeNativeMethods.GetFileAttributesEx(streamName, 0, out win32_FILE_ATTRIBUTE_DATA) && (win32_FILE_ATTRIBUTE_DATA.fileAttributes & 16) == 0)
			{
				exists = true;
				fileSize = (long)((ulong)win32_FILE_ATTRIBUTE_DATA.fileSizeHigh << 32 | (ulong)win32_FILE_ATTRIBUTE_DATA.fileSizeLow);
				utcCreationTime = DateTime.FromFileTimeUtc((long)((ulong)win32_FILE_ATTRIBUTE_DATA.ftCreationTimeHigh << 32 | (ulong)win32_FILE_ATTRIBUTE_DATA.ftCreationTimeLow));
				utcLastWriteTime = DateTime.FromFileTimeUtc((long)((ulong)win32_FILE_ATTRIBUTE_DATA.ftLastWriteTimeHigh << 32 | (ulong)win32_FILE_ATTRIBUTE_DATA.ftLastWriteTimeLow));
			}
			return new FileVersion(exists, fileSize, utcCreationTime, utcLastWriteTime);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001FB15 File Offset: 0x0001DD15
		object IInternalConfigHost.GetStreamVersion(string streamName)
		{
			return InternalConfigHost.StaticGetStreamVersion(streamName);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001FB1D File Offset: 0x0001DD1D
		internal static Stream StaticOpenStreamForRead(string streamName)
		{
			if (string.IsNullOrEmpty(streamName))
			{
				throw ExceptionUtil.UnexpectedError("InternalConfigHost::StaticOpenStreamForRead");
			}
			if (!FileUtil.FileExists(streamName, true))
			{
				return null;
			}
			return new FileStream(streamName, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001FB46 File Offset: 0x0001DD46
		Stream IInternalConfigHost.OpenStreamForRead(string streamName)
		{
			return ((IInternalConfigHost)this).OpenStreamForRead(streamName, false);
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001FB50 File Offset: 0x0001DD50
		Stream IInternalConfigHost.OpenStreamForRead(string streamName, bool assertPermissions)
		{
			Stream result = null;
			bool flag = false;
			if (assertPermissions || !this._configRoot.IsDesignTime)
			{
				new FileIOPermission(FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery, streamName).Assert();
				flag = true;
			}
			try
			{
				result = InternalConfigHost.StaticOpenStreamForRead(streamName);
			}
			finally
			{
				if (flag)
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			return result;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001FBA4 File Offset: 0x0001DDA4
		internal static Stream StaticOpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			bool flag = false;
			if (string.IsNullOrEmpty(streamName))
			{
				throw new ConfigurationErrorsException(SR.GetString("Config_no_stream_to_write"));
			}
			string directoryName = Path.GetDirectoryName(streamName);
			try
			{
				if (!Directory.Exists(directoryName))
				{
					if (assertPermissions)
					{
						new FileIOPermission(PermissionState.Unrestricted).Assert();
						flag = true;
					}
					Directory.CreateDirectory(directoryName);
				}
			}
			catch
			{
			}
			finally
			{
				if (flag)
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			WriteFileContext writeFileContext = null;
			flag = false;
			if (assertPermissions)
			{
				new FileIOPermission(FileIOPermissionAccess.AllAccess, directoryName).Assert();
				flag = true;
			}
			Stream result;
			try
			{
				writeFileContext = new WriteFileContext(streamName, templateStreamName);
				if (File.Exists(streamName))
				{
					FileInfo fileInfo = new FileInfo(streamName);
					FileAttributes attributes = fileInfo.Attributes;
					if ((attributes & (FileAttributes.ReadOnly | FileAttributes.Hidden)) != (FileAttributes)0)
					{
						throw new IOException(SR.GetString("Config_invalid_attributes_for_write", new object[]
						{
							streamName
						}));
					}
				}
				try
				{
					result = new FileStream(writeFileContext.TempNewFilename, FileMode.Create, FileAccess.Write, FileShare.Read);
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_write_failed", new object[]
					{
						streamName
					}), inner);
				}
			}
			catch
			{
				if (writeFileContext != null)
				{
					writeFileContext.Complete(streamName, false);
				}
				throw;
			}
			finally
			{
				if (flag)
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			writeContext = writeFileContext;
			return result;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001FCE4 File Offset: 0x0001DEE4
		Stream IInternalConfigHost.OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			return ((IInternalConfigHost)this).OpenStreamForWrite(streamName, templateStreamName, ref writeContext, false);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
		Stream IInternalConfigHost.OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
		{
			return InternalConfigHost.StaticOpenStreamForWrite(streamName, templateStreamName, ref writeContext, assertPermissions);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001FCFC File Offset: 0x0001DEFC
		internal static void StaticWriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
			WriteFileContext writeFileContext = (WriteFileContext)writeContext;
			bool flag = false;
			if (assertPermissions)
			{
				string directoryName = Path.GetDirectoryName(streamName);
				string[] pathList = new string[]
				{
					streamName,
					writeFileContext.TempNewFilename,
					directoryName
				};
				FileIOPermission fileIOPermission = new FileIOPermission(FileIOPermissionAccess.AllAccess, AccessControlActions.View | AccessControlActions.Change, pathList);
				fileIOPermission.Assert();
				flag = true;
			}
			try
			{
				writeFileContext.Complete(streamName, success);
			}
			finally
			{
				if (flag)
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001FD6C File Offset: 0x0001DF6C
		void IInternalConfigHost.WriteCompleted(string streamName, bool success, object writeContext)
		{
			((IInternalConfigHost)this).WriteCompleted(streamName, success, writeContext, false);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001FD78 File Offset: 0x0001DF78
		void IInternalConfigHost.WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
		{
			InternalConfigHost.StaticWriteCompleted(streamName, success, writeContext, assertPermissions);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001FD84 File Offset: 0x0001DF84
		internal static void StaticDeleteStream(string streamName)
		{
			File.Delete(streamName);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		void IInternalConfigHost.DeleteStream(string streamName)
		{
			InternalConfigHost.StaticDeleteStream(streamName);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001FD94 File Offset: 0x0001DF94
		internal static bool StaticIsFile(string streamName)
		{
			return Path.IsPathRooted(streamName);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		bool IInternalConfigHost.IsFile(string streamName)
		{
			return InternalConfigHost.StaticIsFile(streamName);
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x00008751 File Offset: 0x00006951
		bool IInternalConfigHost.SupportsChangeNotifications
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001FDA4 File Offset: 0x0001DFA4
		object IInternalConfigHost.StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.StartMonitoringStreamForChanges");
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001FDB0 File Offset: 0x0001DFB0
		void IInternalConfigHost.StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.StopMonitoringStreamForChanges");
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00008751 File Offset: 0x00006951
		bool IInternalConfigHost.SupportsRefresh
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x00008751 File Offset: 0x00006951
		bool IInternalConfigHost.SupportsPath
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000874E File Offset: 0x0000694E
		bool IInternalConfigHost.IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
		{
			return true;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00005E74 File Offset: 0x00004074
		void IInternalConfigHost.VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
		{
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00008751 File Offset: 0x00006951
		bool IInternalConfigHost.SupportsLocation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		bool IInternalConfigHost.IsAboveApplication(string configPath)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.IsAboveApplication");
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		string IInternalConfigHost.GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.GetConfigPathFromLocationSubPath");
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		bool IInternalConfigHost.IsLocationApplicable(string configPath)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.IsLocationApplicable");
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		bool IInternalConfigHost.IsTrustedConfigPath(string configPath)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.IsTrustedConfigPath");
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001FDEC File Offset: 0x0001DFEC
		bool IInternalConfigHost.IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
		{
			return TypeUtil.IsCallerFullTrust;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001FDF3 File Offset: 0x0001DFF3
		void IInternalConfigHost.GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			permissionSet = null;
			isHostReady = true;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000088C2 File Offset: 0x00006AC2
		IDisposable IInternalConfigHost.Impersonate()
		{
			return null;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00008751 File Offset: 0x00006951
		bool IInternalConfigHost.PrefetchAll(string configPath, string streamName)
		{
			return false;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00008751 File Offset: 0x00006951
		bool IInternalConfigHost.PrefetchSection(string sectionGroupName, string sectionName)
		{
			return false;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001FDFB File Offset: 0x0001DFFB
		object IInternalConfigHost.CreateDeprecatedConfigContext(string configPath)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.CreateDeprecatedConfigContext");
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001FE07 File Offset: 0x0001E007
		object IInternalConfigHost.CreateConfigurationContext(string configPath, string locationSubPath)
		{
			throw ExceptionUtil.UnexpectedError("IInternalConfigHost.CreateConfigurationContext");
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001FE13 File Offset: 0x0001E013
		string IInternalConfigHost.DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return ProtectedConfigurationSection.DecryptSection(encryptedXml, protectionProvider);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001FE1C File Offset: 0x0001E01C
		string IInternalConfigHost.EncryptSection(string clearTextXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return ProtectedConfigurationSection.EncryptSection(clearTextXml, protectionProvider);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001FE25 File Offset: 0x0001E025
		Type IInternalConfigHost.GetConfigType(string typeName, bool throwOnError)
		{
			return Type.GetType(typeName, throwOnError);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001FE2E File Offset: 0x0001E02E
		string IInternalConfigHost.GetConfigTypeName(Type t)
		{
			return t.AssemblyQualifiedName;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00008751 File Offset: 0x00006951
		bool IInternalConfigHost.IsRemote
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001FE36 File Offset: 0x0001E036
		XmlNode IInternalConfigurationBuilderHost.ProcessRawXml(XmlNode rawXml, ConfigurationBuilder builder)
		{
			if (builder != null)
			{
				return builder.ProcessRawXml(rawXml);
			}
			return rawXml;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001FE44 File Offset: 0x0001E044
		ConfigurationSection IInternalConfigurationBuilderHost.ProcessConfigurationSection(ConfigurationSection configSection, ConfigurationBuilder builder)
		{
			if (builder != null)
			{
				return builder.ProcessConfigurationSection(configSection);
			}
			return configSection;
		}

		// Token: 0x04000455 RID: 1109
		private IInternalConfigRoot _configRoot;

		// Token: 0x04000456 RID: 1110
		private const FileAttributes InvalidAttributesForWrite = FileAttributes.ReadOnly | FileAttributes.Hidden;
	}
}
