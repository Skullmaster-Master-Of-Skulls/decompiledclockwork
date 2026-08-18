using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x0200050B RID: 1291
	internal class RegBlobWebProxyDataBuilder : WebProxyDataBuilder
	{
		// Token: 0x06002815 RID: 10261 RVA: 0x000A5482 File Offset: 0x000A4482
		public RegBlobWebProxyDataBuilder(string connectoid, SafeRegistryHandle registry)
		{
			this.m_Registry = registry;
			this.m_Connectoid = connectoid;
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000A5498 File Offset: 0x000A4498
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings")]
		private bool ReadRegSettings()
		{
			SafeRegistryHandle safeRegistryHandle = null;
			RegistryKey registryKey = null;
			try
			{
				bool flag = true;
				registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings");
				if (registryKey != null)
				{
					object value = registryKey.GetValue("ProxySettingsPerUser");
					if (value != null && value.GetType() == typeof(int) && (int)value == 0)
					{
						flag = false;
					}
				}
				uint num;
				if (flag)
				{
					if (this.m_Registry != null)
					{
						num = this.m_Registry.RegOpenKeyEx("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections", 0U, 131097U, out safeRegistryHandle);
					}
					else
					{
						num = 1168U;
					}
				}
				else
				{
					num = SafeRegistryHandle.RegOpenKeyEx(UnsafeNclNativeMethods.RegistryHelper.HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections", 0U, 131097U, out safeRegistryHandle);
				}
				if (num != 0U)
				{
					safeRegistryHandle = null;
				}
				object obj;
				if (safeRegistryHandle != null && safeRegistryHandle.QueryValue((this.m_Connectoid != null) ? this.m_Connectoid : "DefaultConnectionSettings", out obj) == 0U)
				{
					this.m_RegistryBytes = (byte[])obj;
				}
			}
			catch (Exception exception)
			{
				if (NclUtilities.IsFatal(exception))
				{
					throw;
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
				if (safeRegistryHandle != null)
				{
					safeRegistryHandle.RegCloseKey();
				}
			}
			return this.m_RegistryBytes != null;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000A55BC File Offset: 0x000A45BC
		public string ReadString()
		{
			string result = null;
			int num = this.ReadInt32();
			if (num > 0)
			{
				int num2 = this.m_RegistryBytes.Length - this.m_ByteOffset;
				if (num >= num2)
				{
					num = num2;
				}
				result = Encoding.UTF8.GetString(this.m_RegistryBytes, this.m_ByteOffset, num);
				this.m_ByteOffset += num;
			}
			return result;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000A5614 File Offset: 0x000A4614
		internal unsafe int ReadInt32()
		{
			int result = 0;
			int num = this.m_RegistryBytes.Length - this.m_ByteOffset;
			if (num >= 4)
			{
				fixed (byte* registryBytes = this.m_RegistryBytes)
				{
					if (sizeof(IntPtr) == 4)
					{
						result = ((int*)registryBytes)[this.m_ByteOffset / 4];
					}
					else
					{
						result = Marshal.ReadInt32((IntPtr)((void*)registryBytes), this.m_ByteOffset);
					}
				}
				this.m_ByteOffset += 4;
			}
			return result;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000A5690 File Offset: 0x000A4690
		protected override void BuildInternal()
		{
			bool flag = this.ReadRegSettings();
			if (flag)
			{
				flag = (this.ReadInt32() >= 60);
			}
			if (!flag)
			{
				base.SetAutoDetectSettings(true);
				return;
			}
			this.ReadInt32();
			RegBlobWebProxyDataBuilder.ProxyTypeFlags proxyTypeFlags = (RegBlobWebProxyDataBuilder.ProxyTypeFlags)this.ReadInt32();
			string addressString = this.ReadString();
			string bypassListString = this.ReadString();
			if ((proxyTypeFlags & RegBlobWebProxyDataBuilder.ProxyTypeFlags.PROXY_TYPE_PROXY) != (RegBlobWebProxyDataBuilder.ProxyTypeFlags)0)
			{
				base.SetProxyAndBypassList(addressString, bypassListString);
			}
			base.SetAutoDetectSettings((proxyTypeFlags & RegBlobWebProxyDataBuilder.ProxyTypeFlags.PROXY_TYPE_AUTO_DETECT) != (RegBlobWebProxyDataBuilder.ProxyTypeFlags)0);
			string autoProxyUrl = this.ReadString();
			if ((proxyTypeFlags & RegBlobWebProxyDataBuilder.ProxyTypeFlags.PROXY_TYPE_AUTO_PROXY_URL) != (RegBlobWebProxyDataBuilder.ProxyTypeFlags)0)
			{
				base.SetAutoProxyUrl(autoProxyUrl);
			}
		}

		// Token: 0x0400275B RID: 10075
		internal const string PolicyKey = "SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";

		// Token: 0x0400275C RID: 10076
		internal const string ProxyKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections";

		// Token: 0x0400275D RID: 10077
		private const string DefaultConnectionSettings = "DefaultConnectionSettings";

		// Token: 0x0400275E RID: 10078
		private const string ProxySettingsPerUser = "ProxySettingsPerUser";

		// Token: 0x0400275F RID: 10079
		private const int IE50StrucSize = 60;

		// Token: 0x04002760 RID: 10080
		private byte[] m_RegistryBytes;

		// Token: 0x04002761 RID: 10081
		private int m_ByteOffset;

		// Token: 0x04002762 RID: 10082
		private string m_Connectoid;

		// Token: 0x04002763 RID: 10083
		private SafeRegistryHandle m_Registry;

		// Token: 0x0200050C RID: 1292
		[Flags]
		private enum ProxyTypeFlags
		{
			// Token: 0x04002765 RID: 10085
			PROXY_TYPE_DIRECT = 1,
			// Token: 0x04002766 RID: 10086
			PROXY_TYPE_PROXY = 2,
			// Token: 0x04002767 RID: 10087
			PROXY_TYPE_AUTO_PROXY_URL = 4,
			// Token: 0x04002768 RID: 10088
			PROXY_TYPE_AUTO_DETECT = 8
		}
	}
}
