using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200067A RID: 1658
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	public static class Executor
	{
		// Token: 0x06003D2A RID: 15658 RVA: 0x000FB9CC File Offset: 0x000F9BCC
		internal static string GetRuntimeInstallDirectory()
		{
			return RuntimeEnvironment.GetRuntimeDirectory();
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x000FB9D3 File Offset: 0x000F9BD3
		private static FileStream CreateInheritedFile(string file)
		{
			return new FileStream(file, FileMode.CreateNew, FileAccess.Write, FileShare.Read | FileShare.Inheritable);
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000FB9E0 File Offset: 0x000F9BE0
		public static void ExecWait(string cmd, TempFileCollection tempFiles)
		{
			string text = null;
			string text2 = null;
			Executor.ExecWaitWithCapture(cmd, tempFiles, ref text, ref text2);
		}

		// Token: 0x06003D2D RID: 15661 RVA: 0x000FB9FD File Offset: 0x000F9BFD
		public static int ExecWaitWithCapture(string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(null, cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName, null);
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x000FBA0F File Offset: 0x000F9C0F
		public static int ExecWaitWithCapture(string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(null, cmd, currentDir, tempFiles, ref outputName, ref errorName, null);
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x000FBA1E File Offset: 0x000F9C1E
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(new SafeUserTokenHandle(userToken, false), cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName, null);
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x000FBA37 File Offset: 0x000F9C37
		public static int ExecWaitWithCapture(IntPtr userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName)
		{
			return Executor.ExecWaitWithCapture(new SafeUserTokenHandle(userToken, false), cmd, Environment.CurrentDirectory, tempFiles, ref outputName, ref errorName, null);
		}

		// Token: 0x06003D31 RID: 15665 RVA: 0x000FBA54 File Offset: 0x000F9C54
		internal static int ExecWaitWithCapture(SafeUserTokenHandle userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName, string trueCmdLine)
		{
			int result = 0;
			try
			{
				WindowsImpersonationContext impersonation = Executor.RevertImpersonation();
				try
				{
					result = Executor.ExecWaitWithCaptureUnimpersonated(userToken, cmd, currentDir, tempFiles, ref outputName, ref errorName, trueCmdLine);
				}
				finally
				{
					Executor.ReImpersonate(impersonation);
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000FBAA4 File Offset: 0x000F9CA4
		private unsafe static int ExecWaitWithCaptureUnimpersonated(SafeUserTokenHandle userToken, string cmd, string currentDir, TempFileCollection tempFiles, ref string outputName, ref string errorName, string trueCmdLine)
		{
			IntSecurity.UnmanagedCode.Demand();
			if (outputName == null || outputName.Length == 0)
			{
				outputName = tempFiles.AddExtension("out");
			}
			if (errorName == null || errorName.Length == 0)
			{
				errorName = tempFiles.AddExtension("err");
			}
			FileStream fileStream = Executor.CreateInheritedFile(outputName);
			FileStream fileStream2 = Executor.CreateInheritedFile(errorName);
			bool flag = false;
			SafeNativeMethods.PROCESS_INFORMATION process_INFORMATION = new SafeNativeMethods.PROCESS_INFORMATION();
			SafeProcessHandle safeProcessHandle = new SafeProcessHandle();
			SafeThreadHandle safeThreadHandle = new SafeThreadHandle();
			SafeUserTokenHandle safeUserTokenHandle = null;
			try
			{
				StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8);
				streamWriter.Write(currentDir);
				streamWriter.Write("> ");
				streamWriter.WriteLine((trueCmdLine != null) ? trueCmdLine : cmd);
				streamWriter.WriteLine();
				streamWriter.WriteLine();
				streamWriter.Flush();
				NativeMethods.STARTUPINFO startupinfo = new NativeMethods.STARTUPINFO();
				startupinfo.cb = Marshal.SizeOf(startupinfo);
				startupinfo.dwFlags = 257;
				startupinfo.wShowWindow = 0;
				startupinfo.hStdOutput = fileStream.SafeFileHandle;
				startupinfo.hStdError = fileStream2.SafeFileHandle;
				startupinfo.hStdInput = new SafeFileHandle(UnsafeNativeMethods.GetStdHandle(-10), false);
				StringDictionary stringDictionary = new StringDictionary();
				foreach (object obj in Environment.GetEnvironmentVariables())
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					stringDictionary[(string)dictionaryEntry.Key] = (string)dictionaryEntry.Value;
				}
				stringDictionary["_ClrRestrictSecAttributes"] = "1";
				byte[] array = EnvironmentBlock.ToByteArray(stringDictionary, false);
				try
				{
					byte[] array2;
					byte* value;
					if ((array2 = array) == null || array2.Length == 0)
					{
						value = null;
					}
					else
					{
						value = &array2[0];
					}
					IntPtr intPtr = new IntPtr((void*)value);
					if (userToken == null || userToken.IsInvalid)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
							goto IL_322;
						}
						finally
						{
							flag = NativeMethods.CreateProcess(null, new StringBuilder(cmd), null, null, true, 0, intPtr, currentDir, startupinfo, process_INFORMATION);
							if (process_INFORMATION.hProcess != (IntPtr)0 && process_INFORMATION.hProcess != NativeMethods.INVALID_HANDLE_VALUE)
							{
								safeProcessHandle.InitialSetHandle(process_INFORMATION.hProcess);
							}
							if (process_INFORMATION.hThread != (IntPtr)0 && process_INFORMATION.hThread != NativeMethods.INVALID_HANDLE_VALUE)
							{
								safeThreadHandle.InitialSetHandle(process_INFORMATION.hThread);
							}
						}
					}
					flag = SafeUserTokenHandle.DuplicateTokenEx(userToken, 983551, null, 2, 1, out safeUserTokenHandle);
					if (flag)
					{
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							flag = NativeMethods.CreateProcessAsUser(safeUserTokenHandle, null, cmd, null, null, true, 0, new HandleRef(null, intPtr), currentDir, startupinfo, process_INFORMATION);
							if (process_INFORMATION.hProcess != (IntPtr)0 && process_INFORMATION.hProcess != NativeMethods.INVALID_HANDLE_VALUE)
							{
								safeProcessHandle.InitialSetHandle(process_INFORMATION.hProcess);
							}
							if (process_INFORMATION.hThread != (IntPtr)0 && process_INFORMATION.hThread != NativeMethods.INVALID_HANDLE_VALUE)
							{
								safeThreadHandle.InitialSetHandle(process_INFORMATION.hThread);
							}
						}
					}
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			finally
			{
				if (!flag && safeUserTokenHandle != null && !safeUserTokenHandle.IsInvalid)
				{
					safeUserTokenHandle.Close();
					safeUserTokenHandle = null;
				}
				fileStream.Close();
				fileStream2.Close();
			}
			IL_322:
			if (flag)
			{
				try
				{
					ProcessWaitHandle processWaitHandle = null;
					bool flag2;
					try
					{
						processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
						flag2 = processWaitHandle.WaitOne(600000, false);
					}
					finally
					{
						if (processWaitHandle != null)
						{
							processWaitHandle.Close();
						}
					}
					if (!flag2)
					{
						throw new ExternalException(SR.GetString("ExecTimeout", new object[]
						{
							cmd
						}), 258);
					}
					int result = 259;
					if (!NativeMethods.GetExitCodeProcess(safeProcessHandle, out result))
					{
						throw new ExternalException(SR.GetString("ExecCantGetRetCode", new object[]
						{
							cmd
						}), Marshal.GetLastWin32Error());
					}
					return result;
				}
				finally
				{
					safeProcessHandle.Close();
					safeThreadHandle.Close();
					if (safeUserTokenHandle != null && !safeUserTokenHandle.IsInvalid)
					{
						safeUserTokenHandle.Close();
					}
				}
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 8)
			{
				throw new OutOfMemoryException();
			}
			Win32Exception inner = new Win32Exception(lastWin32Error);
			ExternalException ex = new ExternalException(SR.GetString("ExecCantExec", new object[]
			{
				cmd
			}), inner);
			throw ex;
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000FBF68 File Offset: 0x000FA168
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		[SecurityPermission(SecurityAction.Assert, ControlPrincipal = true, UnmanagedCode = true)]
		internal static WindowsImpersonationContext RevertImpersonation()
		{
			return WindowsIdentity.Impersonate(new IntPtr(0));
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000FBF75 File Offset: 0x000FA175
		internal static void ReImpersonate(WindowsImpersonationContext impersonation)
		{
			impersonation.Undo();
		}

		// Token: 0x04002C9E RID: 11422
		private const int ProcessTimeOut = 600000;
	}
}
