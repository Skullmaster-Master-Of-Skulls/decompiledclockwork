using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000799 RID: 1945
	internal static class SharedUtils
	{
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003C0A RID: 15370 RVA: 0x00100A8C File Offset: 0x000FFA8C
		private static object InternalSyncObject
		{
			get
			{
				if (SharedUtils.s_InternalSyncObject == null)
				{
					object value = new object();
					Interlocked.CompareExchange(ref SharedUtils.s_InternalSyncObject, value, null);
				}
				return SharedUtils.s_InternalSyncObject;
			}
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x00100AB8 File Offset: 0x000FFAB8
		internal static Win32Exception CreateSafeWin32Exception()
		{
			return SharedUtils.CreateSafeWin32Exception(0);
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x00100AC0 File Offset: 0x000FFAC0
		internal static Win32Exception CreateSafeWin32Exception(int error)
		{
			Win32Exception result = null;
			SecurityPermission securityPermission = new SecurityPermission(PermissionState.Unrestricted);
			securityPermission.Assert();
			try
			{
				if (error == 0)
				{
					result = new Win32Exception();
				}
				else
				{
					result = new Win32Exception(error);
				}
			}
			finally
			{
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x00100B08 File Offset: 0x000FFB08
		internal static int CurrentEnvironment
		{
			get
			{
				if (SharedUtils.environment == 0)
				{
					lock (SharedUtils.InternalSyncObject)
					{
						if (SharedUtils.environment == 0)
						{
							if (Environment.OSVersion.Platform == PlatformID.Win32NT)
							{
								if (Environment.OSVersion.Version.Major >= 5)
								{
									SharedUtils.environment = 1;
								}
								else
								{
									SharedUtils.environment = 2;
								}
							}
							else
							{
								SharedUtils.environment = 3;
							}
						}
					}
				}
				return SharedUtils.environment;
			}
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x00100B84 File Offset: 0x000FFB84
		internal static void CheckEnvironment()
		{
			if (SharedUtils.CurrentEnvironment == 3)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x00100B9E File Offset: 0x000FFB9E
		internal static void CheckNtEnvironment()
		{
			if (SharedUtils.CurrentEnvironment == 2)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x00100BB8 File Offset: 0x000FFBB8
		internal static void EnterMutex(string name, ref Mutex mutex)
		{
			string mutexName;
			if (SharedUtils.CurrentEnvironment == 1)
			{
				mutexName = "Global\\" + name;
			}
			else
			{
				mutexName = name;
			}
			SharedUtils.EnterMutexWithoutGlobal(mutexName, ref mutex);
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00100BE8 File Offset: 0x000FFBE8
		internal static void EnterMutexWithoutGlobal(string mutexName, ref Mutex mutex)
		{
			MutexSecurity mutexSecurity = new MutexSecurity();
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
			mutexSecurity.AddAccessRule(new MutexAccessRule(identity, MutexRights.Modify | MutexRights.Synchronize, AccessControlType.Allow));
			bool flag;
			Mutex mutexIn = new Mutex(false, mutexName, ref flag, mutexSecurity);
			SharedUtils.SafeWaitForMutex(mutexIn, ref mutex);
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x00100C29 File Offset: 0x000FFC29
		private static bool SafeWaitForMutex(Mutex mutexIn, ref Mutex mutexOut)
		{
			while (SharedUtils.SafeWaitForMutexOnce(mutexIn, ref mutexOut))
			{
				if (mutexOut != null)
				{
					return true;
				}
				Thread.Sleep(0);
			}
			return false;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00100C44 File Offset: 0x000FFC44
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static bool SafeWaitForMutexOnce(Mutex mutexIn, ref Mutex mutexOut)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			bool result;
			try
			{
			}
			finally
			{
				Thread.BeginCriticalRegion();
				Thread.BeginThreadAffinity();
				int num = SharedUtils.WaitForSingleObjectDontCallThis(mutexIn.SafeWaitHandle, 500);
				int num2 = num;
				if (num2 != 0 && num2 != 128)
				{
					result = (num2 == 258);
				}
				else
				{
					mutexOut = mutexIn;
					result = true;
				}
				if (mutexOut == null)
				{
					Thread.EndThreadAffinity();
					Thread.EndCriticalRegion();
				}
			}
			return result;
		}

		// Token: 0x06003C14 RID: 15380
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", EntryPoint = "WaitForSingleObject", ExactSpelling = true, SetLastError = true)]
		private static extern int WaitForSingleObjectDontCallThis(SafeWaitHandle handle, int timeout);

		// Token: 0x06003C15 RID: 15381 RVA: 0x00100CB8 File Offset: 0x000FFCB8
		internal static string GetLatestBuildDllDirectory(string machineName)
		{
			string result = "";
			RegistryKey registryKey = null;
			RegistryKey registryKey2 = null;
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.Unrestricted);
			registryPermission.Assert();
			try
			{
				if (machineName.Equals("."))
				{
					return SharedUtils.GetLocalBuildDirectory();
				}
				registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName);
				if (registryKey == null)
				{
					throw new InvalidOperationException(SR.GetString("RegKeyMissingShort", new object[]
					{
						"HKEY_LOCAL_MACHINE",
						machineName
					}));
				}
				registryKey2 = registryKey.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework");
				if (registryKey2 != null)
				{
					string text = (string)registryKey2.GetValue("InstallRoot");
					if (text != null && text != string.Empty)
					{
						Version version = Environment.Version;
						string text2 = "v" + version.ToString(2);
						string text3 = null;
						RegistryKey registryKey3 = registryKey2.OpenSubKey("policy\\" + text2);
						if (registryKey3 != null)
						{
							try
							{
								text3 = (string)registryKey3.GetValue("Version");
								if (text3 == null)
								{
									string[] valueNames = registryKey3.GetValueNames();
									for (int i = 0; i < valueNames.Length; i++)
									{
										string text4 = text2 + "." + valueNames[i].Replace('-', '.');
										if (string.Compare(text4, text3, StringComparison.Ordinal) > 0)
										{
											text3 = text4;
										}
									}
								}
							}
							finally
							{
								registryKey3.Close();
							}
							if (text3 != null && text3 != string.Empty)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.Append(text);
								if (!text.EndsWith("\\", StringComparison.Ordinal))
								{
									stringBuilder.Append("\\");
								}
								stringBuilder.Append(text3);
								stringBuilder.Append("\\");
								result = stringBuilder.ToString();
							}
						}
					}
				}
			}
			catch
			{
			}
			finally
			{
				if (registryKey2 != null)
				{
					registryKey2.Close();
				}
				if (registryKey != null)
				{
					registryKey.Close();
				}
				CodeAccessPermission.RevertAssert();
			}
			return result;
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x00100EDC File Offset: 0x000FFEDC
		private static string GetLocalBuildDirectory()
		{
			int num = 264;
			int num2 = 25;
			StringBuilder stringBuilder = new StringBuilder(num);
			StringBuilder stringBuilder2 = new StringBuilder(num2);
			uint num3;
			uint num4;
			uint requestedRuntimeInfo;
			for (requestedRuntimeInfo = NativeMethods.GetRequestedRuntimeInfo(null, null, null, 0U, 65U, stringBuilder, num, out num3, stringBuilder2, num2, out num4); requestedRuntimeInfo == 122U; requestedRuntimeInfo = NativeMethods.GetRequestedRuntimeInfo(null, null, null, 0U, 0U, stringBuilder, num, out num3, stringBuilder2, num2, out num4))
			{
				num *= 2;
				num2 *= 2;
				stringBuilder = new StringBuilder(num);
				stringBuilder2 = new StringBuilder(num2);
			}
			if (requestedRuntimeInfo != 0U)
			{
				throw SharedUtils.CreateSafeWin32Exception();
			}
			stringBuilder.Append(stringBuilder2);
			return stringBuilder.ToString();
		}

		// Token: 0x040034A4 RID: 13476
		internal const int UnknownEnvironment = 0;

		// Token: 0x040034A5 RID: 13477
		internal const int W2kEnvironment = 1;

		// Token: 0x040034A6 RID: 13478
		internal const int NtEnvironment = 2;

		// Token: 0x040034A7 RID: 13479
		internal const int NonNtEnvironment = 3;

		// Token: 0x040034A8 RID: 13480
		private static int environment;

		// Token: 0x040034A9 RID: 13481
		private static object s_InternalSyncObject;
	}
}
