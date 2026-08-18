using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004E4 RID: 1252
	internal class PerformanceMonitor
	{
		// Token: 0x06002F72 RID: 12146 RVA: 0x000D6169 File Offset: 0x000D4369
		internal PerformanceMonitor(string machineName)
		{
			this.machineName = machineName;
			this.Init();
		}

		// Token: 0x06002F73 RID: 12147 RVA: 0x000D6180 File Offset: 0x000D4380
		private void Init()
		{
			try
			{
				if (this.machineName != "." && string.Compare(this.machineName, PerformanceCounterLib.ComputerName, StringComparison.OrdinalIgnoreCase) != 0)
				{
					new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
					this.perfDataKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.PerformanceData, this.machineName);
				}
				else
				{
					this.perfDataKey = Registry.PerformanceData;
				}
			}
			catch (UnauthorizedAccessException)
			{
				throw new Win32Exception(5);
			}
			catch (IOException e)
			{
				throw new Win32Exception(Marshal.GetHRForException(e));
			}
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000D6214 File Offset: 0x000D4414
		internal void Close()
		{
			if (this.perfDataKey != null)
			{
				this.perfDataKey.Close();
			}
			this.perfDataKey = null;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000D6230 File Offset: 0x000D4430
		internal byte[] GetData(string item)
		{
			int i = 17;
			int num = 0;
			int num2 = 0;
			new RegistryPermission(PermissionState.Unrestricted).Assert();
			while (i > 0)
			{
				try
				{
					return (byte[])this.perfDataKey.GetValue(item);
				}
				catch (IOException e)
				{
					num2 = Marshal.GetHRForException(e);
					if (num2 <= 167)
					{
						if (num2 != 6)
						{
							if (num2 != 21 && num2 != 167)
							{
								goto IL_A1;
							}
							goto IL_89;
						}
					}
					else if (num2 <= 258)
					{
						if (num2 != 170 && num2 != 258)
						{
							goto IL_A1;
						}
						goto IL_89;
					}
					else if (num2 != 1722 && num2 != 1726)
					{
						goto IL_A1;
					}
					this.Init();
					IL_89:
					i--;
					if (num == 0)
					{
						num = 10;
					}
					else
					{
						Thread.Sleep(num);
						num *= 2;
					}
					continue;
					IL_A1:
					throw SharedUtils.CreateSafeWin32Exception(num2);
				}
				catch (InvalidCastException innerException)
				{
					throw new InvalidOperationException(SR.GetString("CounterDataCorrupt", new object[]
					{
						this.perfDataKey.ToString()
					}), innerException);
				}
			}
			throw SharedUtils.CreateSafeWin32Exception(num2);
		}

		// Token: 0x040027F0 RID: 10224
		private RegistryKey perfDataKey;

		// Token: 0x040027F1 RID: 10225
		private string machineName;
	}
}
