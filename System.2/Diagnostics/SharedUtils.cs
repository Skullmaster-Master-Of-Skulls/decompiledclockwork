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
	// Token: 0x02000506 RID: 1286
	internal static class SharedUtils
	{
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000DE4A8 File Offset: 0x000DC6A8
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

		// Token: 0x060030F0 RID: 12528 RVA: 0x000DE4D4 File Offset: 0x000DC6D4
		internal static Win32Exception CreateSafeWin32Exception()
		{
			return SharedUtils.CreateSafeWin32Exception(0);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000DE4DC File Offset: 0x000DC6DC
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

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000DE524 File Offset: 0x000DC724
		internal static int CurrentEnvironment
		{
			get
			{
				if (SharedUtils.environment == 0)
				{
					object internalSyncObject = SharedUtils.InternalSyncObject;
					lock (internalSyncObject)
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

		// Token: 0x060030F3 RID: 12531 RVA: 0x000DE5B4 File Offset: 0x000DC7B4
		internal static void CheckEnvironment()
		{
			if (SharedUtils.CurrentEnvironment == 3)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
		}

		// Token: 0x060030F4 RID: 12532 RVA: 0x000DE5CE File Offset: 0x000DC7CE
		internal static void CheckNtEnvironment()
		{
			if (SharedUtils.CurrentEnvironment == 2)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
			}
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x000DE5E8 File Offset: 0x000DC7E8
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

		// Token: 0x060030F6 RID: 12534 RVA: 0x000DE618 File Offset: 0x000DC818
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true)]
		internal static void EnterMutexWithoutGlobal(string mutexName, ref Mutex mutex)
		{
			MutexSecurity mutexSecurity = new MutexSecurity();
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
			mutexSecurity.AddAccessRule(new MutexAccessRule(identity, MutexRights.Modify | MutexRights.Synchronize, AccessControlType.Allow));
			bool flag;
			Mutex mutexIn = new Mutex(false, mutexName, ref flag, mutexSecurity);
			SharedUtils.SafeWaitForMutex(mutexIn, ref mutex);
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x000DE659 File Offset: 0x000DC859
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

		// Token: 0x060030F8 RID: 12536 RVA: 0x000DE674 File Offset: 0x000DC874
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
				if (num != 0 && num != 128)
				{
					result = (num == 258);
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

		// Token: 0x060030F9 RID: 12537
		[SuppressUnmanagedCodeSecurity]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("kernel32.dll", EntryPoint = "WaitForSingleObject", ExactSpelling = true, SetLastError = true)]
		private static extern int WaitForSingleObjectDontCallThis(SafeWaitHandle handle, int timeout);

		// Token: 0x060030FA RID: 12538 RVA: 0x000DE6E8 File Offset: 0x000DC8E8
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
						string text2 = "v" + Environment.Version.Major.ToString() + "." + Environment.Version.Minor.ToString();
						RegistryKey registryKey3 = registryKey2.OpenSubKey("policy");
						string text3 = null;
						if (registryKey3 != null)
						{
							try
							{
								RegistryKey registryKey4 = registryKey3.OpenSubKey(text2);
								if (registryKey4 != null)
								{
									try
									{
										text3 = text2 + "." + SharedUtils.GetLargestBuildNumberFromKey(registryKey4).ToString();
										goto IL_284;
									}
									finally
									{
										registryKey4.Close();
									}
								}
								string[] subKeyNames = registryKey3.GetSubKeyNames();
								int[] array = new int[]
								{
									-1,
									-1,
									-1
								};
								foreach (string text4 in subKeyNames)
								{
									if (text4.Length > 1 && text4[0] == 'v' && text4.Contains("."))
									{
										int[] array2 = new int[]
										{
											-1,
											-1,
											-1
										};
										string[] array3 = text4.Substring(1).Split(new char[]
										{
											'.'
										});
										if (array3.Length == 2 && int.TryParse(array3[0], out array2[0]) && int.TryParse(array3[1], out array2[1]))
										{
											RegistryKey registryKey5 = registryKey3.OpenSubKey(text4);
											if (registryKey5 != null)
											{
												try
												{
													array2[2] = SharedUtils.GetLargestBuildNumberFromKey(registryKey5);
													if (array2[0] > array[0] || (array2[0] == array[0] && array2[1] > array[1]))
													{
														array = array2;
													}
												}
												finally
												{
													registryKey5.Close();
												}
											}
										}
									}
								}
								text3 = string.Concat(new string[]
								{
									"v",
									array[0].ToString(),
									".",
									array[1].ToString(),
									".",
									array[2].ToString()
								});
								IL_284:;
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

		// Token: 0x060030FB RID: 12539 RVA: 0x000DEA74 File Offset: 0x000DCC74
		private static int GetLargestBuildNumberFromKey(RegistryKey rootKey)
		{
			int num = -1;
			string[] valueNames = rootKey.GetValueNames();
			for (int i = 0; i < valueNames.Length; i++)
			{
				int num2;
				if (int.TryParse(valueNames[i], out num2))
				{
					num = ((num > num2) ? num : num2);
				}
			}
			return num;
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000DEAAE File Offset: 0x000DCCAE
		private static string GetLocalBuildDirectory()
		{
			return RuntimeEnvironment.GetRuntimeDirectory();
		}

		// Token: 0x040028DA RID: 10458
		internal const int UnknownEnvironment = 0;

		// Token: 0x040028DB RID: 10459
		internal const int W2kEnvironment = 1;

		// Token: 0x040028DC RID: 10460
		internal const int NtEnvironment = 2;

		// Token: 0x040028DD RID: 10461
		internal const int NonNtEnvironment = 3;

		// Token: 0x040028DE RID: 10462
		private static volatile int environment;

		// Token: 0x040028DF RID: 10463
		private static object s_InternalSyncObject;
	}
}
