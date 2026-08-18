using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200084A RID: 2122
	internal class PipeSharedMemory : IDisposable
	{
		// Token: 0x06004F6E RID: 20334 RVA: 0x001225BE File Offset: 0x001207BE
		private PipeSharedMemory(SafeFileMappingHandle fileMapping, Uri pipeUri) : this(fileMapping, pipeUri, null)
		{
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x001225C9 File Offset: 0x001207C9
		private PipeSharedMemory(SafeFileMappingHandle fileMapping, Uri pipeUri, string pipeName)
		{
			this.pipeName = pipeName;
			this.fileMapping = fileMapping;
			this.pipeUri = pipeUri;
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x001225E8 File Offset: 0x001207E8
		public static PipeSharedMemory Create(List<SecurityIdentifier> allowedSids, Uri pipeUri, string sharedMemoryName)
		{
			PipeSharedMemory result;
			if (PipeSharedMemory.TryCreate(allowedSids, pipeUri, sharedMemoryName, out result))
			{
				return result;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeSharedMemory.CreatePipeNameInUseException(5, pipeUri));
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x00122614 File Offset: 0x00120814
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public unsafe static bool TryCreate(List<SecurityIdentifier> allowedSids, Uri pipeUri, string sharedMemoryName, out PipeSharedMemory result)
		{
			Guid pipeGuid = Guid.NewGuid();
			string text = PipeSharedMemory.BuildPipeName(pipeGuid.ToString());
			byte[] array;
			try
			{
				array = SecurityDescriptorHelper.FromSecurityIdentifiers(allowedSids, int.MinValue);
			}
			catch (Win32Exception ex)
			{
				Exception ex2 = new PipeException(ex.Message, ex);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(ex2.Message, ex2));
			}
			result = null;
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
			UnsafeNativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new UnsafeNativeMethods.SECURITY_ATTRIBUTES();
			security_ATTRIBUTES.lpSecurityDescriptor = (IntPtr)((void*)value);
			SafeFileMappingHandle safeFileMappingHandle = UnsafeNativeMethods.CreateFileMapping((IntPtr)(-1), security_ATTRIBUTES, 4, 0, sizeof(PipeSharedMemory.SharedMemoryContents), sharedMemoryName);
			int lastWin32Error = Marshal.GetLastWin32Error();
			array2 = null;
			if (safeFileMappingHandle.IsInvalid)
			{
				safeFileMappingHandle.SetHandleAsInvalid();
				if (lastWin32Error == 5)
				{
					return false;
				}
				Exception ex3 = new PipeException(SR.GetString("PipeNameCantBeReserved", new object[]
				{
					pipeUri.AbsoluteUri,
					PipeError.GetErrorString(lastWin32Error)
				}), lastWin32Error);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAccessDeniedException(ex3.Message, ex3));
			}
			else
			{
				if (lastWin32Error == 183)
				{
					safeFileMappingHandle.Close();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeSharedMemory.CreatePipeNameInUseException(lastWin32Error, pipeUri));
				}
				PipeSharedMemory pipeSharedMemory = new PipeSharedMemory(safeFileMappingHandle, pipeUri, text);
				bool flag = true;
				bool result2;
				try
				{
					pipeSharedMemory.InitializeContents(pipeGuid);
					flag = false;
					result = pipeSharedMemory;
					if (TD.PipeSharedMemoryCreatedIsEnabled())
					{
						TD.PipeSharedMemoryCreated(sharedMemoryName);
					}
					result2 = true;
				}
				finally
				{
					if (flag)
					{
						pipeSharedMemory.Dispose();
					}
				}
				return result2;
			}
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x001227A0 File Offset: 0x001209A0
		public static PipeSharedMemory Open(string sharedMemoryName, Uri pipeUri)
		{
			SafeFileMappingHandle safeFileMappingHandle = UnsafeNativeMethods.OpenFileMapping(4, false, sharedMemoryName);
			if (!safeFileMappingHandle.IsInvalid)
			{
				return new PipeSharedMemory(safeFileMappingHandle, pipeUri);
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			safeFileMappingHandle.SetHandleAsInvalid();
			if (lastWin32Error != 2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeSharedMemory.CreatePipeNameCannotBeAccessedException(lastWin32Error, pipeUri));
			}
			safeFileMappingHandle = UnsafeNativeMethods.OpenFileMapping(4, false, "Global\\" + sharedMemoryName);
			if (!safeFileMappingHandle.IsInvalid)
			{
				return new PipeSharedMemory(safeFileMappingHandle, pipeUri);
			}
			lastWin32Error = Marshal.GetLastWin32Error();
			safeFileMappingHandle.SetHandleAsInvalid();
			if (lastWin32Error == 2)
			{
				return null;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeSharedMemory.CreatePipeNameCannotBeAccessedException(lastWin32Error, pipeUri));
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x0012282E File Offset: 0x00120A2E
		public void Dispose()
		{
			if (this.fileMapping != null)
			{
				this.fileMapping.Close();
				this.fileMapping = null;
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06004F74 RID: 20340 RVA: 0x0012284C File Offset: 0x00120A4C
		public unsafe string PipeName
		{
			[SecuritySafeCritical]
			[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
			get
			{
				if (this.pipeName == null)
				{
					SafeViewOfFileHandle view = this.GetView(false);
					try
					{
						PipeSharedMemory.SharedMemoryContents* ptr = (PipeSharedMemory.SharedMemoryContents*)((void*)view.DangerousGetHandle());
						if (ptr->isInitialized)
						{
							Thread.MemoryBarrier();
							this.pipeNameGuidPart = ptr->pipeGuid.ToString();
							this.pipeName = PipeSharedMemory.BuildPipeName(this.pipeNameGuidPart);
						}
					}
					finally
					{
						view.Close();
					}
				}
				return this.pipeName;
			}
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x001228CC File Offset: 0x00120ACC
		internal string GetPipeName(AppContainerInfo appInfo)
		{
			if (appInfo == null)
			{
				return this.PipeName;
			}
			if (this.PipeName != null)
			{
				return string.Format(CultureInfo.InvariantCulture, "\\\\.\\pipe\\Sessions\\{0}\\{1}\\{2}", new object[]
				{
					appInfo.SessionId,
					appInfo.NamedObjectPath,
					this.pipeNameGuidPart
				});
			}
			return null;
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x00122924 File Offset: 0x00120B24
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private unsafe void InitializeContents(Guid pipeGuid)
		{
			SafeViewOfFileHandle view = this.GetView(true);
			try
			{
				PipeSharedMemory.SharedMemoryContents* ptr = (PipeSharedMemory.SharedMemoryContents*)((void*)view.DangerousGetHandle());
				ptr->pipeGuid = pipeGuid;
				Thread.MemoryBarrier();
				ptr->isInitialized = true;
			}
			finally
			{
				view.Close();
			}
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x00122974 File Offset: 0x00120B74
		public static Exception CreatePipeNameInUseException(int error, Uri pipeUri)
		{
			Exception ex = new PipeException(SR.GetString("PipeNameInUse", new object[]
			{
				pipeUri.AbsoluteUri
			}), error);
			return new AddressAlreadyInUseException(ex.Message, ex);
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x001229B0 File Offset: 0x00120BB0
		private static Exception CreatePipeNameCannotBeAccessedException(int error, Uri pipeUri)
		{
			Exception innerException = new PipeException(SR.GetString("PipeNameCanNotBeAccessed", new object[]
			{
				PipeError.GetErrorString(error)
			}), error);
			return new AddressAccessDeniedException(SR.GetString("PipeNameCanNotBeAccessed2", new object[]
			{
				pipeUri.AbsoluteUri
			}), innerException);
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x001229FC File Offset: 0x00120BFC
		private SafeViewOfFileHandle GetView(bool writable)
		{
			SafeViewOfFileHandle safeViewOfFileHandle = UnsafeNativeMethods.MapViewOfFile(this.fileMapping, writable ? 2 : 4, 0, 0, (IntPtr)sizeof(PipeSharedMemory.SharedMemoryContents));
			if (safeViewOfFileHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				safeViewOfFileHandle.SetHandleAsInvalid();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(PipeSharedMemory.CreatePipeNameCannotBeAccessedException(lastWin32Error, this.pipeUri));
			}
			return safeViewOfFileHandle;
		}

		// Token: 0x06004F7A RID: 20346 RVA: 0x00122A55 File Offset: 0x00120C55
		private static string BuildPipeName(string pipeGuid)
		{
			return (AppContainerInfo.IsRunningInAppContainer ? "\\\\.\\pipe\\Local\\" : "\\\\.\\pipe\\") + pipeGuid;
		}

		// Token: 0x0400314E RID: 12622
		internal const string PipePrefix = "\\\\.\\pipe\\";

		// Token: 0x0400314F RID: 12623
		internal const string PipeLocalPrefix = "\\\\.\\pipe\\Local\\";

		// Token: 0x04003150 RID: 12624
		private SafeFileMappingHandle fileMapping;

		// Token: 0x04003151 RID: 12625
		private string pipeName;

		// Token: 0x04003152 RID: 12626
		private string pipeNameGuidPart;

		// Token: 0x04003153 RID: 12627
		private Uri pipeUri;

		// Token: 0x02000D38 RID: 3384
		private struct SharedMemoryContents
		{
			// Token: 0x0400475D RID: 18269
			public bool isInitialized;

			// Token: 0x0400475E RID: 18270
			public Guid pipeGuid;
		}
	}
}
