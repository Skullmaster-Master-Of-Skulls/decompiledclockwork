using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Internal;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Util;

namespace System.Web.Configuration
{
	// Token: 0x02000741 RID: 1857
	[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
	internal sealed class RemoteWebConfigurationHost : DelegatingConfigHost
	{
		// Token: 0x06005969 RID: 22889 RVA: 0x00137EA1 File Offset: 0x001360A1
		internal RemoteWebConfigurationHost()
		{
		}

		// Token: 0x0600596A RID: 22890 RVA: 0x00137EA9 File Offset: 0x001360A9
		public override void Init(IInternalConfigRoot configRoot, params object[] hostInitParams)
		{
			throw ExceptionUtil.UnexpectedError("RemoteWebConfigurationHost::Init");
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x00137EB8 File Offset: 0x001360B8
		public override void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
		{
			WebLevel webLevel = (WebLevel)hostInitConfigurationParams[0];
			string path = (string)hostInitConfigurationParams[2];
			string site = (string)hostInitConfigurationParams[3];
			if (locationSubPath == null)
			{
				locationSubPath = (string)hostInitConfigurationParams[4];
			}
			string server = (string)hostInitConfigurationParams[5];
			string fullUserName = (string)hostInitConfigurationParams[6];
			string password = (string)hostInitConfigurationParams[7];
			IntPtr intPtr = (IntPtr)hostInitConfigurationParams[8];
			configPath = null;
			locationConfigPath = null;
			this._Server = server;
			this._Username = RemoteWebConfigurationHost.GetUserNameFromFullName(fullUserName);
			this._Domain = RemoteWebConfigurationHost.GetDomainFromFullName(fullUserName);
			this._Password = password;
			this._Identity = ((intPtr == IntPtr.Zero) ? null : new WindowsIdentity(intPtr));
			this._PathMap = new Hashtable(StringComparer.OrdinalIgnoreCase);
			string filePaths;
			try
			{
				WindowsImpersonationContext windowsImpersonationContext = (this._Identity != null) ? this._Identity.Impersonate() : null;
				try
				{
					IRemoteWebConfigurationHostServer remoteWebConfigurationHostServer = RemoteWebConfigurationHost.CreateRemoteObject(server, this._Username, this._Domain, password);
					try
					{
						filePaths = remoteWebConfigurationHostServer.GetFilePaths((int)webLevel, path, site, locationSubPath);
					}
					finally
					{
						while (Marshal.ReleaseComObject(remoteWebConfigurationHostServer) > 0)
						{
						}
					}
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
					}
				}
			}
			catch
			{
				throw;
			}
			if (filePaths == null)
			{
				throw ExceptionUtil.UnexpectedError("RemoteWebConfigurationHost::InitForConfiguration");
			}
			string[] array = filePaths.Split(RemoteWebConfigurationHostServer.FilePathsSeparatorParams);
			if (array.Length < 7 || (array.Length - 5) % 2 != 0)
			{
				throw ExceptionUtil.UnexpectedError("RemoteWebConfigurationHost::InitForConfiguration");
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length == 0)
				{
					array[i] = null;
				}
			}
			string text = array[0];
			string text2 = array[1];
			string text3 = array[2];
			configPath = array[3];
			locationConfigPath = array[4];
			this._ConfigPath = configPath;
			WebConfigurationFileMap webConfigurationFileMap = new WebConfigurationFileMap();
			VirtualPath v = VirtualPath.CreateAbsoluteAllowNull(text);
			webConfigurationFileMap.Site = text3;
			for (int j = 5; j < array.Length; j += 2)
			{
				string text4 = array[j];
				string text5 = array[j + 1];
				this._PathMap.Add(text4, text5);
				if (WebConfigurationHost.IsMachineConfigPath(text4))
				{
					webConfigurationFileMap.MachineConfigFilename = text5;
				}
				else
				{
					string virtualDirectory;
					bool isAppRoot;
					if (WebConfigurationHost.IsRootWebConfigPath(text4))
					{
						virtualDirectory = null;
						isAppRoot = false;
					}
					else
					{
						string text6;
						VirtualPath virtualPath;
						WebConfigurationHost.GetSiteIDAndVPathFromConfigPath(text4, out text6, out virtualPath);
						virtualDirectory = VirtualPath.GetVirtualPathString(virtualPath);
						isAppRoot = (virtualPath == v);
					}
					webConfigurationFileMap.VirtualDirectories.Add(virtualDirectory, new VirtualDirectoryMapping(Path.GetDirectoryName(text5), isAppRoot));
				}
			}
			WebConfigurationHost webConfigurationHost = new WebConfigurationHost();
			webConfigurationHost.Init(root, new object[]
			{
				true,
				new UserMapPath(webConfigurationFileMap, false),
				null,
				text,
				text2,
				text3
			});
			base.Host = webConfigurationHost;
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x00138174 File Offset: 0x00136374
		public override bool IsConfigRecordRequired(string configPath)
		{
			return configPath.Length <= this._ConfigPath.Length;
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x0013818C File Offset: 0x0013638C
		public override string GetStreamName(string configPath)
		{
			return (string)this._PathMap[configPath];
		}

		// Token: 0x0600596E RID: 22894 RVA: 0x001381A0 File Offset: 0x001363A0
		public override object GetStreamVersion(string streamName)
		{
			WindowsImpersonationContext windowsImpersonationContext = null;
			bool exists;
			long fileSize;
			long fileTime;
			long fileTime2;
			try
			{
				if (this._Identity != null)
				{
					windowsImpersonationContext = this._Identity.Impersonate();
				}
				try
				{
					IRemoteWebConfigurationHostServer remoteWebConfigurationHostServer = RemoteWebConfigurationHost.CreateRemoteObject(this._Server, this._Username, this._Domain, this._Password);
					try
					{
						remoteWebConfigurationHostServer.GetFileDetails(streamName, out exists, out fileSize, out fileTime, out fileTime2);
					}
					finally
					{
						while (Marshal.ReleaseComObject(remoteWebConfigurationHostServer) > 0)
						{
						}
					}
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
					}
				}
			}
			catch
			{
				throw;
			}
			return new FileDetails(exists, fileSize, DateTime.FromFileTimeUtc(fileTime), DateTime.FromFileTimeUtc(fileTime2));
		}

		// Token: 0x0600596F RID: 22895 RVA: 0x00138250 File Offset: 0x00136450
		public override Stream OpenStreamForRead(string streamName)
		{
			RemoteWebConfigurationHostStream remoteWebConfigurationHostStream = new RemoteWebConfigurationHostStream(false, this._Server, streamName, null, this._Username, this._Domain, this._Password, this._Identity);
			if (remoteWebConfigurationHostStream == null || remoteWebConfigurationHostStream.Length < 1L)
			{
				return null;
			}
			return remoteWebConfigurationHostStream;
		}

		// Token: 0x06005970 RID: 22896 RVA: 0x00138294 File Offset: 0x00136494
		public override Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
		{
			RemoteWebConfigurationHostStream remoteWebConfigurationHostStream = new RemoteWebConfigurationHostStream(true, this._Server, streamName, templateStreamName, this._Username, this._Domain, this._Password, this._Identity);
			writeContext = remoteWebConfigurationHostStream;
			return remoteWebConfigurationHostStream;
		}

		// Token: 0x06005971 RID: 22897 RVA: 0x00006164 File Offset: 0x00004364
		public override void DeleteStream(string StreamName)
		{
		}

		// Token: 0x06005972 RID: 22898 RVA: 0x001382CC File Offset: 0x001364CC
		public override void WriteCompleted(string streamName, bool success, object writeContext)
		{
			if (success)
			{
				RemoteWebConfigurationHostStream remoteWebConfigurationHostStream = (RemoteWebConfigurationHostStream)writeContext;
				remoteWebConfigurationHostStream.FlushForWriteCompleted();
			}
		}

		// Token: 0x06005973 RID: 22899 RVA: 0x00007722 File Offset: 0x00005922
		public override bool IsFile(string StreamName)
		{
			return false;
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool PrefetchAll(string configPath, string StreamName)
		{
			return true;
		}

		// Token: 0x06005975 RID: 22901 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool PrefetchSection(string sectionGroupName, string sectionName)
		{
			return true;
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x001382E9 File Offset: 0x001364E9
		public override void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
		{
			WebConfigurationHost.StaticGetRestrictedPermissions(configRecord, out permissionSet, out isHostReady);
		}

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x06005977 RID: 22903 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool IsRemote
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x001382F3 File Offset: 0x001364F3
		public override string DecryptSection(string encryptedXmlString, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return this.CallEncryptOrDecrypt(false, encryptedXmlString, protectionProvider, protectedConfigSection);
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x001382FF File Offset: 0x001364FF
		public override string EncryptSection(string clearTextXmlString, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			return this.CallEncryptOrDecrypt(true, clearTextXmlString, protectionProvider, protectedConfigSection);
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x0013830C File Offset: 0x0013650C
		private string CallEncryptOrDecrypt(bool doEncrypt, string xmlString, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedConfigSection)
		{
			string result = null;
			WindowsImpersonationContext windowsImpersonationContext = null;
			string assemblyQualifiedName = protectionProvider.GetType().AssemblyQualifiedName;
			ProviderSettings providerSettings = protectedConfigSection.Providers[protectionProvider.Name];
			if (providerSettings == null)
			{
				throw ExceptionUtil.ParameterInvalid("protectionProvider");
			}
			NameValueCollection nameValueCollection = providerSettings.Parameters;
			if (nameValueCollection == null)
			{
				nameValueCollection = new NameValueCollection();
			}
			string[] allKeys = nameValueCollection.AllKeys;
			string[] array = new string[allKeys.Length];
			for (int i = 0; i < allKeys.Length; i++)
			{
				array[i] = nameValueCollection[allKeys[i]];
			}
			if (this._Identity != null)
			{
				windowsImpersonationContext = this._Identity.Impersonate();
			}
			try
			{
				try
				{
					IRemoteWebConfigurationHostServer remoteWebConfigurationHostServer = RemoteWebConfigurationHost.CreateRemoteObject(this._Server, this._Username, this._Domain, this._Password);
					try
					{
						result = remoteWebConfigurationHostServer.DoEncryptOrDecrypt(doEncrypt, xmlString, protectionProvider.Name, assemblyQualifiedName, allKeys, array);
					}
					finally
					{
						while (Marshal.ReleaseComObject(remoteWebConfigurationHostServer) > 0)
						{
						}
					}
				}
				finally
				{
					if (windowsImpersonationContext != null)
					{
						windowsImpersonationContext.Undo();
					}
				}
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x00138424 File Offset: 0x00136624
		private static string GetUserNameFromFullName(string fullUserName)
		{
			if (string.IsNullOrEmpty(fullUserName))
			{
				return null;
			}
			if (fullUserName.Contains("@"))
			{
				return fullUserName;
			}
			string[] array = fullUserName.Split(new char[]
			{
				'\\'
			});
			if (array.Length == 1)
			{
				return fullUserName;
			}
			return array[1];
		}

		// Token: 0x0600597C RID: 22908 RVA: 0x00138468 File Offset: 0x00136668
		private static string GetDomainFromFullName(string fullUserName)
		{
			if (string.IsNullOrEmpty(fullUserName))
			{
				return null;
			}
			if (fullUserName.Contains("@"))
			{
				return null;
			}
			string[] array = fullUserName.Split(new char[]
			{
				'\\'
			});
			if (array.Length == 1)
			{
				return ".";
			}
			return array[0];
		}

		// Token: 0x0600597D RID: 22909 RVA: 0x001384B0 File Offset: 0x001366B0
		internal static IRemoteWebConfigurationHostServer CreateRemoteObject(string server, string username, string domain, string password)
		{
			IRemoteWebConfigurationHostServer result;
			try
			{
				if (string.IsNullOrEmpty(username))
				{
					result = RemoteWebConfigurationHost.CreateRemoteObjectUsingGetTypeFromCLSID(server);
				}
				else if (IntPtr.Size == 8)
				{
					result = RemoteWebConfigurationHost.CreateRemoteObjectOn64BitPlatform(server, username, domain, password);
				}
				else
				{
					result = RemoteWebConfigurationHost.CreateRemoteObjectOn32BitPlatform(server, username, domain, password);
				}
			}
			catch (COMException ex)
			{
				if (ex.ErrorCode == -2147221164)
				{
					throw new Exception(SR.GetString("Make_sure_remote_server_is_enabled_for_config_access"));
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600597E RID: 22910 RVA: 0x00138520 File Offset: 0x00136720
		private static IRemoteWebConfigurationHostServer CreateRemoteObjectUsingGetTypeFromCLSID(string server)
		{
			Type typeFromCLSID = Type.GetTypeFromCLSID(typeof(RemoteWebConfigurationHostServer).GUID, server, true);
			return (IRemoteWebConfigurationHostServer)Activator.CreateInstance(typeFromCLSID);
		}

		// Token: 0x0600597F RID: 22911 RVA: 0x00138550 File Offset: 0x00136750
		private static IRemoteWebConfigurationHostServer CreateRemoteObjectOn32BitPlatform(string server, string username, string domain, string password)
		{
			MULTI_QI[] array = new MULTI_QI[1];
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			Guid guid = typeof(RemoteWebConfigurationHostServer).GUID;
			IntPtr intPtr3 = IntPtr.Zero;
			IRemoteWebConfigurationHostServer result;
			try
			{
				intPtr = Marshal.AllocCoTaskMem(16);
				Marshal.StructureToPtr(typeof(IRemoteWebConfigurationHostServer).GUID, intPtr, false);
				array[0] = new MULTI_QI(intPtr);
				COAUTHIDENTITY structure = new COAUTHIDENTITY(username, domain, password);
				intPtr3 = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
				Marshal.StructureToPtr(structure, intPtr3, false);
				COAUTHINFO structure2 = new COAUTHINFO(RpcAuthent.WinNT, RpcAuthor.None, null, RpcLevel.Default, RpcImpers.Impersonate, intPtr3);
				intPtr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure2));
				Marshal.StructureToPtr(structure2, intPtr2, false);
				COSERVERINFO srv = new COSERVERINFO(server, intPtr2);
				int num = UnsafeNativeMethods.CoCreateInstanceEx(ref guid, IntPtr.Zero, 16, srv, 1, array);
				if (num == -2147221164)
				{
					throw new Exception(SR.GetString("Make_sure_remote_server_is_enabled_for_config_access"));
				}
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				if (array[0].hr < 0)
				{
					Marshal.ThrowExceptionForHR(array[0].hr);
				}
				num = UnsafeNativeMethods.CoSetProxyBlanket(array[0].pItf, RpcAuthent.WinNT, RpcAuthor.None, null, RpcLevel.Default, RpcImpers.Impersonate, intPtr3, 0);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				result = (IRemoteWebConfigurationHostServer)Marshal.GetObjectForIUnknown(array[0].pItf);
			}
			finally
			{
				if (array[0].pItf != IntPtr.Zero)
				{
					Marshal.Release(array[0].pItf);
					array[0].pItf = IntPtr.Zero;
				}
				array[0].piid = IntPtr.Zero;
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.DestroyStructure(intPtr2, typeof(COAUTHINFO));
					Marshal.FreeCoTaskMem(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.DestroyStructure(intPtr3, typeof(COAUTHIDENTITY));
					Marshal.FreeCoTaskMem(intPtr3);
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return result;
		}

		// Token: 0x06005980 RID: 22912 RVA: 0x00138778 File Offset: 0x00136978
		private static IRemoteWebConfigurationHostServer CreateRemoteObjectOn64BitPlatform(string server, string username, string domain, string password)
		{
			MULTI_QI_X64[] array = new MULTI_QI_X64[1];
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			Guid guid = typeof(RemoteWebConfigurationHostServer).GUID;
			IntPtr intPtr3 = IntPtr.Zero;
			IRemoteWebConfigurationHostServer result;
			try
			{
				intPtr = Marshal.AllocCoTaskMem(16);
				Marshal.StructureToPtr(typeof(IRemoteWebConfigurationHostServer).GUID, intPtr, false);
				array[0] = new MULTI_QI_X64(intPtr);
				COAUTHIDENTITY_X64 structure = new COAUTHIDENTITY_X64(username, domain, password);
				intPtr3 = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
				Marshal.StructureToPtr(structure, intPtr3, false);
				COAUTHINFO_X64 structure2 = new COAUTHINFO_X64(RpcAuthent.WinNT, RpcAuthor.None, null, RpcLevel.Default, RpcImpers.Impersonate, intPtr3);
				intPtr2 = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure2));
				Marshal.StructureToPtr(structure2, intPtr2, false);
				COSERVERINFO_X64 srv = new COSERVERINFO_X64(server, intPtr2);
				int num = UnsafeNativeMethods.CoCreateInstanceEx(ref guid, IntPtr.Zero, 16, srv, 1, array);
				if (num == -2147221164)
				{
					throw new Exception(SR.GetString("Make_sure_remote_server_is_enabled_for_config_access"));
				}
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				if (array[0].hr < 0)
				{
					Marshal.ThrowExceptionForHR(array[0].hr);
				}
				num = UnsafeNativeMethods.CoSetProxyBlanket(array[0].pItf, RpcAuthent.WinNT, RpcAuthor.None, null, RpcLevel.Default, RpcImpers.Impersonate, intPtr3, 0);
				if (num < 0)
				{
					Marshal.ThrowExceptionForHR(num);
				}
				result = (IRemoteWebConfigurationHostServer)Marshal.GetObjectForIUnknown(array[0].pItf);
			}
			finally
			{
				if (array[0].pItf != IntPtr.Zero)
				{
					Marshal.Release(array[0].pItf);
					array[0].pItf = IntPtr.Zero;
				}
				array[0].piid = IntPtr.Zero;
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.DestroyStructure(intPtr2, typeof(COAUTHINFO_X64));
					Marshal.FreeCoTaskMem(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.DestroyStructure(intPtr3, typeof(COAUTHIDENTITY_X64));
					Marshal.FreeCoTaskMem(intPtr3);
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
			return result;
		}

		// Token: 0x04002F65 RID: 12133
		private const string KEY_MACHINE = "MACHINE";

		// Token: 0x04002F66 RID: 12134
		private static object s_version = new object();

		// Token: 0x04002F67 RID: 12135
		private string _Server;

		// Token: 0x04002F68 RID: 12136
		private string _Username;

		// Token: 0x04002F69 RID: 12137
		private string _Domain;

		// Token: 0x04002F6A RID: 12138
		private string _Password;

		// Token: 0x04002F6B RID: 12139
		private WindowsIdentity _Identity;

		// Token: 0x04002F6C RID: 12140
		private Hashtable _PathMap;

		// Token: 0x04002F6D RID: 12141
		private string _ConfigPath;
	}
}
