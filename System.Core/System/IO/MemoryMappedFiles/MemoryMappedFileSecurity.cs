using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x020000A9 RID: 169
	public class MemoryMappedFileSecurity : ObjectSecurity<MemoryMappedFileRights>
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000DDD1 File Offset: 0x0000BFD1
		public MemoryMappedFileSecurity() : base(false, ResourceType.KernelObject)
		{
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000DDDB File Offset: 0x0000BFDB
		[SecuritySafeCritical]
		internal MemoryMappedFileSecurity(SafeMemoryMappedFileHandle safeHandle, AccessControlSections includeSections) : base(false, ResourceType.KernelObject, safeHandle, includeSections)
		{
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000DDE7 File Offset: 0x0000BFE7
		[SecuritySafeCritical]
		internal void PersistHandle(SafeHandle handle)
		{
			base.Persist(handle);
		}
	}
}
