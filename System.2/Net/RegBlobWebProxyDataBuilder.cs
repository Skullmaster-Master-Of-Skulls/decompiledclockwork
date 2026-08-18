using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x020001E4 RID: 484
	internal class RegBlobWebProxyDataBuilder : WebProxyDataBuilder
	{
		// Token: 0x060012DF RID: 4831 RVA: 0x00063D60 File Offset: 0x00061F60
		public RegBlobWebProxyDataBuilder(string connectoid, SafeRegistryHandle registry)
		{
			this.m_Registry = registry;
			this.m_Connectoid = connectoid;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00063D78 File Offset: 0x00061F78
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

		// Token: 0x060012E1 RID: 4833 RVA: 0x00063E98 File Offset: 0x00062098
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

		// Token: 0x060012E2 RID: 4834 RVA: 0x00063EF0 File Offset: 0x000620F0
		internal unsafe int ReadInt32()
		{
			int result = 0;
			int num = this.m_RegistryBytes.Length - this.m_ByteOffset;
			if (num >= 4)
			{
				byte[] array;
				byte* ptr;
				if ((array = this.m_RegistryBytes) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				if (sizeof(IntPtr) == 4)
				{
					result = *(int*)(ptr + this.m_ByteOffset);
				}
				else
				{
					result = Marshal.ReadInt32((IntPtr)((void*)ptr), this.m_ByteOffset);
				}
				array = null;
				this.m_ByteOffset += 4;
			}
			return result;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00063F68 File Offset: 0x00062168
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
			base.SetAutoDetectSettings((proxyTypeFlags & RegBlobWebProxyDataBuilder.ProxyTypeFlags.PROXY_TYPE_AUTO_DETECT) > (RegBlobWebProxyDataBuilder.ProxyTypeFlags)0);
			string autoProxyUrl = this.ReadString();
			if ((proxyTypeFlags & RegBlobWebProxyDataBuilder.ProxyTypeFlags.PROXY_TYPE_AUTO_PROXY_URL) != (RegBlobWebProxyDataBuilder.ProxyTypeFlags)0)
			{
				base.SetAutoProxyUrl(autoProxyUrl);
			}
		}

		// Token: 0x0400152B RID: 5419
		internal const string PolicyKey = "SOFTWARE\\Policies\\Microsoft\\Windows\\CurrentVersion\\Internet Settings";

		// Token: 0x0400152C RID: 5420
		internal const string ProxyKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\Connections";

		// Token: 0x0400152D RID: 5421
		private const string DefaultConnectionSettings = "DefaultConnectionSettings";

		// Token: 0x0400152E RID: 5422
		private const string ProxySettingsPerUser = "ProxySettingsPerUser";

		// Token: 0x0400152F RID: 5423
		private const int IE50StrucSize = 60;

		// Token: 0x04001530 RID: 5424
		private byte[] m_RegistryBytes;

		// Token: 0x04001531 RID: 5425
		private int m_ByteOffset;

		// Token: 0x04001532 RID: 5426
		private string m_Connectoid;

		// Token: 0x04001533 RID: 5427
		private SafeRegistryHandle m_Registry;

		// Token: 0x02000756 RID: 1878
		[Flags]
		private enum ProxyTypeFlags
		{
			// Token: 0x0400321D RID: 12829
			PROXY_TYPE_DIRECT = 1,
			// Token: 0x0400321E RID: 12830
			PROXY_TYPE_PROXY = 2,
			// Token: 0x0400321F RID: 12831
			PROXY_TYPE_AUTO_PROXY_URL = 4,
			// Token: 0x04003220 RID: 12832
			PROXY_TYPE_AUTO_DETECT = 8
		}
	}
}
