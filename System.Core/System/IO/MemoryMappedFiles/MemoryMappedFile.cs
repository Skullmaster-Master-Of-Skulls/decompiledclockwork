using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x020000A5 RID: 165
	public class MemoryMappedFile : IDisposable
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0000D043 File Offset: 0x0000B243
		[SecurityCritical]
		private MemoryMappedFile(SafeMemoryMappedFileHandle handle)
		{
			this._handle = handle;
			this._leaveOpen = true;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000D059 File Offset: 0x0000B259
		[SecurityCritical]
		private MemoryMappedFile(SafeMemoryMappedFileHandle handle, FileStream fileStream, bool leaveOpen)
		{
			this._handle = handle;
			this._fileStream = fileStream;
			this._leaveOpen = leaveOpen;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000D076 File Offset: 0x0000B276
		public static MemoryMappedFile OpenExisting(string mapName)
		{
			return MemoryMappedFile.OpenExisting(mapName, MemoryMappedFileRights.ReadWrite, HandleInheritability.None);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000D080 File Offset: 0x0000B280
		public static MemoryMappedFile OpenExisting(string mapName, MemoryMappedFileRights desiredAccessRights)
		{
			return MemoryMappedFile.OpenExisting(mapName, desiredAccessRights, HandleInheritability.None);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000D08C File Offset: 0x0000B28C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static MemoryMappedFile OpenExisting(string mapName, MemoryMappedFileRights desiredAccessRights, HandleInheritability inheritability)
		{
			if (mapName == null)
			{
				throw new ArgumentNullException("mapName", SR.GetString("ArgumentNull_MapName"));
			}
			if (mapName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_MapNameEmptyString"));
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability");
			}
			if ((desiredAccessRights & ~(MemoryMappedFileRights.CopyOnWrite | MemoryMappedFileRights.Write | MemoryMappedFileRights.Read | MemoryMappedFileRights.Execute | MemoryMappedFileRights.Delete | MemoryMappedFileRights.ReadPermissions | MemoryMappedFileRights.ChangePermissions | MemoryMappedFileRights.TakeOwnership | MemoryMappedFileRights.AccessSystemSecurity)) != (MemoryMappedFileRights)0)
			{
				throw new ArgumentOutOfRangeException("desiredAccessRights");
			}
			SafeMemoryMappedFileHandle handle = MemoryMappedFile.OpenCore(mapName, inheritability, (int)desiredAccessRights, false);
			return new MemoryMappedFile(handle);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000D100 File Offset: 0x0000B300
		public static MemoryMappedFile CreateFromFile(string path)
		{
			return MemoryMappedFile.CreateFromFile(path, FileMode.Open, null, 0L, MemoryMappedFileAccess.ReadWrite);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000D10D File Offset: 0x0000B30D
		public static MemoryMappedFile CreateFromFile(string path, FileMode mode)
		{
			return MemoryMappedFile.CreateFromFile(path, mode, null, 0L, MemoryMappedFileAccess.ReadWrite);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000D11A File Offset: 0x0000B31A
		public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName)
		{
			return MemoryMappedFile.CreateFromFile(path, mode, mapName, 0L, MemoryMappedFileAccess.ReadWrite);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000D127 File Offset: 0x0000B327
		public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName, long capacity)
		{
			return MemoryMappedFile.CreateFromFile(path, mode, mapName, capacity, MemoryMappedFileAccess.ReadWrite);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000D134 File Offset: 0x0000B334
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static MemoryMappedFile CreateFromFile(string path, FileMode mode, string mapName, long capacity, MemoryMappedFileAccess access)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (mapName != null && mapName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_MapNameEmptyString"));
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.GetString("ArgumentOutOfRange_PositiveOrDefaultCapacityRequired"));
			}
			if (access < MemoryMappedFileAccess.ReadWrite || access > MemoryMappedFileAccess.ReadWriteExecute)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (mode == FileMode.Append)
			{
				throw new ArgumentException(SR.GetString("Argument_NewMMFAppendModeNotAllowed"), "mode");
			}
			if (access == MemoryMappedFileAccess.Write)
			{
				throw new ArgumentException(SR.GetString("Argument_NewMMFWriteAccessNotAllowed"), "access");
			}
			bool existed = File.Exists(path);
			FileStream fileStream = new FileStream(path, mode, MemoryMappedFile.GetFileStreamFileSystemRights(access), FileShare.None, 4096, FileOptions.None);
			if (capacity == 0L && fileStream.Length == 0L)
			{
				MemoryMappedFile.CleanupFile(fileStream, existed, path);
				throw new ArgumentException(SR.GetString("Argument_EmptyFile"));
			}
			if (access == MemoryMappedFileAccess.Read && capacity > fileStream.Length)
			{
				MemoryMappedFile.CleanupFile(fileStream, existed, path);
				throw new ArgumentException(SR.GetString("Argument_ReadAccessWithLargeCapacity"));
			}
			if (capacity == 0L)
			{
				capacity = fileStream.Length;
			}
			if (fileStream.Length > capacity)
			{
				MemoryMappedFile.CleanupFile(fileStream, existed, path);
				throw new ArgumentOutOfRangeException("capacity", SR.GetString("ArgumentOutOfRange_CapacityGEFileSizeRequired"));
			}
			SafeMemoryMappedFileHandle handle = null;
			try
			{
				handle = MemoryMappedFile.CreateCore(fileStream.SafeFileHandle, mapName, HandleInheritability.None, null, access, MemoryMappedFileOptions.None, capacity);
			}
			catch
			{
				MemoryMappedFile.CleanupFile(fileStream, existed, path);
				throw;
			}
			return new MemoryMappedFile(handle, fileStream, false);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
		public static MemoryMappedFile CreateFromFile(FileStream fileStream, string mapName, long capacity, MemoryMappedFileAccess access, HandleInheritability inheritability, bool leaveOpen)
		{
			return MemoryMappedFile.CreateFromFile(fileStream, mapName, capacity, access, null, inheritability, leaveOpen);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000D2B0 File Offset: 0x0000B4B0
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static MemoryMappedFile CreateFromFile(FileStream fileStream, string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability, bool leaveOpen)
		{
			if (fileStream == null)
			{
				throw new ArgumentNullException("fileStream", SR.GetString("ArgumentNull_FileStream"));
			}
			if (mapName != null && mapName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_MapNameEmptyString"));
			}
			if (capacity < 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.GetString("ArgumentOutOfRange_PositiveOrDefaultCapacityRequired"));
			}
			if (capacity == 0L && fileStream.Length == 0L)
			{
				throw new ArgumentException(SR.GetString("Argument_EmptyFile"));
			}
			if (access < MemoryMappedFileAccess.ReadWrite || access > MemoryMappedFileAccess.ReadWriteExecute)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (access == MemoryMappedFileAccess.Write)
			{
				throw new ArgumentException(SR.GetString("Argument_NewMMFWriteAccessNotAllowed"), "access");
			}
			if (access == MemoryMappedFileAccess.Read && capacity > fileStream.Length)
			{
				throw new ArgumentException(SR.GetString("Argument_ReadAccessWithLargeCapacity"));
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability");
			}
			fileStream.Flush();
			if (capacity == 0L)
			{
				capacity = fileStream.Length;
			}
			if (fileStream.Length > capacity)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.GetString("ArgumentOutOfRange_CapacityGEFileSizeRequired"));
			}
			SafeMemoryMappedFileHandle handle = MemoryMappedFile.CreateCore(fileStream.SafeFileHandle, mapName, inheritability, memoryMappedFileSecurity, access, MemoryMappedFileOptions.None, capacity);
			return new MemoryMappedFile(handle, fileStream, leaveOpen);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000D3CF File Offset: 0x0000B5CF
		public static MemoryMappedFile CreateNew(string mapName, long capacity)
		{
			return MemoryMappedFile.CreateNew(mapName, capacity, MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.None, null, HandleInheritability.None);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000D3DC File Offset: 0x0000B5DC
		public static MemoryMappedFile CreateNew(string mapName, long capacity, MemoryMappedFileAccess access)
		{
			return MemoryMappedFile.CreateNew(mapName, capacity, access, MemoryMappedFileOptions.None, null, HandleInheritability.None);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000D3E9 File Offset: 0x0000B5E9
		public static MemoryMappedFile CreateNew(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, HandleInheritability inheritability)
		{
			return MemoryMappedFile.CreateNew(mapName, capacity, access, options, null, inheritability);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static MemoryMappedFile CreateNew(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability)
		{
			if (mapName != null && mapName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_MapNameEmptyString"));
			}
			if (capacity <= 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.GetString("ArgumentOutOfRange_NeedPositiveNumber"));
			}
			if (IntPtr.Size == 4 && capacity > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("capacity", SR.GetString("ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed"));
			}
			if (access < MemoryMappedFileAccess.ReadWrite || access > MemoryMappedFileAccess.ReadWriteExecute)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (access == MemoryMappedFileAccess.Write)
			{
				throw new ArgumentException(SR.GetString("Argument_NewMMFWriteAccessNotAllowed"), "access");
			}
			if ((options & ~MemoryMappedFileOptions.DelayAllocatePages) != MemoryMappedFileOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability");
			}
			SafeMemoryMappedFileHandle handle = MemoryMappedFile.CreateCore(new SafeFileHandle(new IntPtr(-1), true), mapName, inheritability, memoryMappedFileSecurity, access, options, capacity);
			return new MemoryMappedFile(handle);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000D4D1 File Offset: 0x0000B6D1
		public static MemoryMappedFile CreateOrOpen(string mapName, long capacity)
		{
			return MemoryMappedFile.CreateOrOpen(mapName, capacity, MemoryMappedFileAccess.ReadWrite, MemoryMappedFileOptions.None, null, HandleInheritability.None);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000D4DE File Offset: 0x0000B6DE
		public static MemoryMappedFile CreateOrOpen(string mapName, long capacity, MemoryMappedFileAccess access)
		{
			return MemoryMappedFile.CreateOrOpen(mapName, capacity, access, MemoryMappedFileOptions.None, null, HandleInheritability.None);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000D4EB File Offset: 0x0000B6EB
		public static MemoryMappedFile CreateOrOpen(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, HandleInheritability inheritability)
		{
			return MemoryMappedFile.CreateOrOpen(mapName, capacity, access, options, null, inheritability);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public static MemoryMappedFile CreateOrOpen(string mapName, long capacity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, MemoryMappedFileSecurity memoryMappedFileSecurity, HandleInheritability inheritability)
		{
			if (mapName == null)
			{
				throw new ArgumentNullException("mapName", SR.GetString("ArgumentNull_MapName"));
			}
			if (mapName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_MapNameEmptyString"));
			}
			if (capacity <= 0L)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.GetString("ArgumentOutOfRange_NeedPositiveNumber"));
			}
			if (IntPtr.Size == 4 && capacity > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("capacity", SR.GetString("ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed"));
			}
			if (access < MemoryMappedFileAccess.ReadWrite || access > MemoryMappedFileAccess.ReadWriteExecute)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if ((options & ~MemoryMappedFileOptions.DelayAllocatePages) != MemoryMappedFileOptions.None)
			{
				throw new ArgumentOutOfRangeException("options");
			}
			if (inheritability < HandleInheritability.None || inheritability > HandleInheritability.Inheritable)
			{
				throw new ArgumentOutOfRangeException("inheritability");
			}
			SafeMemoryMappedFileHandle handle;
			if (access == MemoryMappedFileAccess.Write)
			{
				handle = MemoryMappedFile.OpenCore(mapName, inheritability, MemoryMappedFile.GetFileMapAccess(access), true);
			}
			else
			{
				handle = MemoryMappedFile.CreateOrOpenCore(new SafeFileHandle(new IntPtr(-1), true), mapName, inheritability, memoryMappedFileSecurity, access, options, capacity);
			}
			return new MemoryMappedFile(handle);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
		[SecurityCritical]
		private static SafeMemoryMappedFileHandle CreateCore(SafeFileHandle fileHandle, string mapName, HandleInheritability inheritability, MemoryMappedFileSecurity memoryMappedFileSecurity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, long capacity)
		{
			SafeMemoryMappedFileHandle safeMemoryMappedFileHandle = null;
			object obj;
			UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs = MemoryMappedFile.GetSecAttrs(inheritability, memoryMappedFileSecurity, out obj);
			int dwMaximumSizeLow = (int)(capacity & (long)((ulong)-1));
			int dwMaximumSizeHigh = (int)(capacity >> 32);
			try
			{
				safeMemoryMappedFileHandle = UnsafeNativeMethods.CreateFileMapping(fileHandle, secAttrs, MemoryMappedFile.GetPageAccess(access) | (int)options, dwMaximumSizeHigh, dwMaximumSizeLow, mapName);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (!safeMemoryMappedFileHandle.IsInvalid && lastWin32Error == 183)
				{
					safeMemoryMappedFileHandle.Dispose();
					__Error.WinIOError(lastWin32Error, string.Empty);
				}
				else if (safeMemoryMappedFileHandle.IsInvalid)
				{
					__Error.WinIOError(lastWin32Error, string.Empty);
				}
			}
			finally
			{
				if (obj != null)
				{
					((GCHandle)obj).Free();
				}
			}
			return safeMemoryMappedFileHandle;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000D68C File Offset: 0x0000B88C
		[SecurityCritical]
		private static SafeMemoryMappedFileHandle OpenCore(string mapName, HandleInheritability inheritability, int desiredAccessRights, bool createOrOpen)
		{
			SafeMemoryMappedFileHandle safeMemoryMappedFileHandle = UnsafeNativeMethods.OpenFileMapping(desiredAccessRights, (inheritability & HandleInheritability.Inheritable) > HandleInheritability.None, mapName);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (safeMemoryMappedFileHandle.IsInvalid)
			{
				if (createOrOpen && lastWin32Error == 2)
				{
					throw new ArgumentException(SR.GetString("Argument_NewMMFWriteAccessNotAllowed"), "access");
				}
				__Error.WinIOError(lastWin32Error, string.Empty);
			}
			return safeMemoryMappedFileHandle;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000D6E0 File Offset: 0x0000B8E0
		[SecurityCritical]
		private static SafeMemoryMappedFileHandle CreateOrOpenCore(SafeFileHandle fileHandle, string mapName, HandleInheritability inheritability, MemoryMappedFileSecurity memoryMappedFileSecurity, MemoryMappedFileAccess access, MemoryMappedFileOptions options, long capacity)
		{
			SafeMemoryMappedFileHandle safeMemoryMappedFileHandle = null;
			object obj;
			UnsafeNativeMethods.SECURITY_ATTRIBUTES secAttrs = MemoryMappedFile.GetSecAttrs(inheritability, memoryMappedFileSecurity, out obj);
			int dwMaximumSizeLow = (int)(capacity & (long)((ulong)-1));
			int dwMaximumSizeHigh = (int)(capacity >> 32);
			try
			{
				int i = 14;
				int num = 0;
				while (i > 0)
				{
					safeMemoryMappedFileHandle = UnsafeNativeMethods.CreateFileMapping(fileHandle, secAttrs, MemoryMappedFile.GetPageAccess(access) | (int)options, dwMaximumSizeHigh, dwMaximumSizeLow, mapName);
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (!safeMemoryMappedFileHandle.IsInvalid)
					{
						break;
					}
					if (lastWin32Error != 5)
					{
						__Error.WinIOError(lastWin32Error, string.Empty);
					}
					safeMemoryMappedFileHandle.SetHandleAsInvalid();
					safeMemoryMappedFileHandle = UnsafeNativeMethods.OpenFileMapping(MemoryMappedFile.GetFileMapAccess(access), (inheritability & HandleInheritability.Inheritable) > HandleInheritability.None, mapName);
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					if (!safeMemoryMappedFileHandle.IsInvalid)
					{
						break;
					}
					if (lastWin32Error2 != 2)
					{
						__Error.WinIOError(lastWin32Error2, string.Empty);
					}
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
				}
				if (safeMemoryMappedFileHandle == null || safeMemoryMappedFileHandle.IsInvalid)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_CantCreateFileMapping"));
				}
			}
			finally
			{
				if (obj != null)
				{
					((GCHandle)obj).Free();
				}
			}
			return safeMemoryMappedFileHandle;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000D7EC File Offset: 0x0000B9EC
		public MemoryMappedViewStream CreateViewStream()
		{
			return this.CreateViewStream(0L, 0L, MemoryMappedFileAccess.ReadWrite);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000D7F9 File Offset: 0x0000B9F9
		public MemoryMappedViewStream CreateViewStream(long offset, long size)
		{
			return this.CreateViewStream(offset, size, MemoryMappedFileAccess.ReadWrite);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000D804 File Offset: 0x0000BA04
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public MemoryMappedViewStream CreateViewStream(long offset, long size, MemoryMappedFileAccess access)
		{
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (size < 0L)
			{
				throw new ArgumentOutOfRangeException("size", SR.GetString("ArgumentOutOfRange_PositiveOrDefaultSizeRequired"));
			}
			if (access < MemoryMappedFileAccess.ReadWrite || access > MemoryMappedFileAccess.ReadWriteExecute)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (IntPtr.Size == 4 && size > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("size", SR.GetString("ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed"));
			}
			MemoryMappedView view = MemoryMappedView.CreateView(this._handle, access, offset, size);
			return new MemoryMappedViewStream(view);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000D88F File Offset: 0x0000BA8F
		public MemoryMappedViewAccessor CreateViewAccessor()
		{
			return this.CreateViewAccessor(0L, 0L, MemoryMappedFileAccess.ReadWrite);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000D89C File Offset: 0x0000BA9C
		public MemoryMappedViewAccessor CreateViewAccessor(long offset, long size)
		{
			return this.CreateViewAccessor(offset, size, MemoryMappedFileAccess.ReadWrite);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public MemoryMappedViewAccessor CreateViewAccessor(long offset, long size, MemoryMappedFileAccess access)
		{
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (size < 0L)
			{
				throw new ArgumentOutOfRangeException("size", SR.GetString("ArgumentOutOfRange_PositiveOrDefaultSizeRequired"));
			}
			if (access < MemoryMappedFileAccess.ReadWrite || access > MemoryMappedFileAccess.ReadWriteExecute)
			{
				throw new ArgumentOutOfRangeException("access");
			}
			if (IntPtr.Size == 4 && size > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("size", SR.GetString("ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed"));
			}
			MemoryMappedView view = MemoryMappedView.CreateView(this._handle, access, offset, size);
			return new MemoryMappedViewAccessor(view);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000D933 File Offset: 0x0000BB33
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000D944 File Offset: 0x0000BB44
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			try
			{
				if (this._handle != null && !this._handle.IsClosed)
				{
					this._handle.Dispose();
				}
			}
			finally
			{
				if (this._fileStream != null && !this._leaveOpen)
				{
					this._fileStream.Dispose();
				}
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000D9A0 File Offset: 0x0000BBA0
		public SafeMemoryMappedFileHandle SafeMemoryMappedFileHandle
		{
			[SecurityCritical]
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				return this._handle;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000D9A8 File Offset: 0x0000BBA8
		[SecurityCritical]
		public MemoryMappedFileSecurity GetAccessControl()
		{
			if (this._handle.IsClosed)
			{
				__Error.FileNotOpen();
			}
			return new MemoryMappedFileSecurity(this._handle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000D9C9 File Offset: 0x0000BBC9
		[SecurityCritical]
		public void SetAccessControl(MemoryMappedFileSecurity memoryMappedFileSecurity)
		{
			if (memoryMappedFileSecurity == null)
			{
				throw new ArgumentNullException("memoryMappedFileSecurity");
			}
			if (this._handle.IsClosed)
			{
				__Error.FileNotOpen();
			}
			memoryMappedFileSecurity.PersistHandle(this._handle);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		[SecurityCritical]
		internal static int GetSystemPageAllocationGranularity()
		{
			UnsafeNativeMethods.SYSTEM_INFO system_INFO = default(UnsafeNativeMethods.SYSTEM_INFO);
			UnsafeNativeMethods.GetSystemInfo(ref system_INFO);
			return system_INFO.dwAllocationGranularity;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0000DA1A File Offset: 0x0000BC1A
		internal static int GetPageAccess(MemoryMappedFileAccess access)
		{
			if (access == MemoryMappedFileAccess.Read)
			{
				return 2;
			}
			if (access == MemoryMappedFileAccess.ReadWrite)
			{
				return 4;
			}
			if (access == MemoryMappedFileAccess.CopyOnWrite)
			{
				return 8;
			}
			if (access == MemoryMappedFileAccess.ReadExecute)
			{
				return 32;
			}
			if (access == MemoryMappedFileAccess.ReadWriteExecute)
			{
				return 64;
			}
			throw new ArgumentOutOfRangeException("access");
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000DA45 File Offset: 0x0000BC45
		internal static int GetFileMapAccess(MemoryMappedFileAccess access)
		{
			if (access == MemoryMappedFileAccess.Read)
			{
				return 4;
			}
			if (access == MemoryMappedFileAccess.Write)
			{
				return 2;
			}
			if (access == MemoryMappedFileAccess.ReadWrite)
			{
				return 6;
			}
			if (access == MemoryMappedFileAccess.CopyOnWrite)
			{
				return 1;
			}
			if (access == MemoryMappedFileAccess.ReadExecute)
			{
				return 36;
			}
			if (access == MemoryMappedFileAccess.ReadWriteExecute)
			{
				return 38;
			}
			throw new ArgumentOutOfRangeException("access");
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000DA76 File Offset: 0x0000BC76
		private static FileSystemRights GetFileStreamFileSystemRights(MemoryMappedFileAccess access)
		{
			switch (access)
			{
			case MemoryMappedFileAccess.ReadWrite:
				return FileSystemRights.ReadData | FileSystemRights.WriteData;
			case MemoryMappedFileAccess.Read:
			case MemoryMappedFileAccess.CopyOnWrite:
				return FileSystemRights.ReadData;
			case MemoryMappedFileAccess.Write:
				return FileSystemRights.WriteData;
			case MemoryMappedFileAccess.ReadExecute:
				return FileSystemRights.ReadData | FileSystemRights.ExecuteFile;
			case MemoryMappedFileAccess.ReadWriteExecute:
				return FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.ExecuteFile;
			default:
				throw new ArgumentOutOfRangeException("access");
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000DAAE File Offset: 0x0000BCAE
		internal static FileAccess GetFileAccess(MemoryMappedFileAccess access)
		{
			if (access == MemoryMappedFileAccess.Read)
			{
				return FileAccess.Read;
			}
			if (access == MemoryMappedFileAccess.Write)
			{
				return FileAccess.Write;
			}
			if (access == MemoryMappedFileAccess.ReadWrite)
			{
				return FileAccess.ReadWrite;
			}
			if (access == MemoryMappedFileAccess.CopyOnWrite)
			{
				return FileAccess.ReadWrite;
			}
			if (access == MemoryMappedFileAccess.ReadExecute)
			{
				return FileAccess.Read;
			}
			if (access == MemoryMappedFileAccess.ReadWriteExecute)
			{
				return FileAccess.ReadWrite;
			}
			throw new ArgumentOutOfRangeException("access");
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		[SecurityCritical]
		private unsafe static UnsafeNativeMethods.SECURITY_ATTRIBUTES GetSecAttrs(HandleInheritability inheritability, MemoryMappedFileSecurity memoryMappedFileSecurity, out object pinningHandle)
		{
			pinningHandle = null;
			UnsafeNativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if ((inheritability & HandleInheritability.Inheritable) != HandleInheritability.None || memoryMappedFileSecurity != null)
			{
				security_ATTRIBUTES = new UnsafeNativeMethods.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf(security_ATTRIBUTES);
				if ((inheritability & HandleInheritability.Inheritable) != HandleInheritability.None)
				{
					security_ATTRIBUTES.bInheritHandle = 1;
				}
				if (memoryMappedFileSecurity != null)
				{
					byte[] securityDescriptorBinaryForm = memoryMappedFileSecurity.GetSecurityDescriptorBinaryForm();
					pinningHandle = GCHandle.Alloc(securityDescriptorBinaryForm, GCHandleType.Pinned);
					byte[] array;
					byte* pSecurityDescriptor;
					if ((array = securityDescriptorBinaryForm) == null || array.Length == 0)
					{
						pSecurityDescriptor = null;
					}
					else
					{
						pSecurityDescriptor = &array[0];
					}
					security_ATTRIBUTES.pSecurityDescriptor = pSecurityDescriptor;
					array = null;
				}
			}
			return security_ATTRIBUTES;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000DB52 File Offset: 0x0000BD52
		private static void CleanupFile(FileStream fileStream, bool existed, string path)
		{
			fileStream.Close();
			if (!existed)
			{
				File.Delete(path);
			}
		}

		// Token: 0x04000525 RID: 1317
		private SafeMemoryMappedFileHandle _handle;

		// Token: 0x04000526 RID: 1318
		private bool _leaveOpen;

		// Token: 0x04000527 RID: 1319
		private FileStream _fileStream;

		// Token: 0x04000528 RID: 1320
		internal const int DefaultSize = 0;
	}
}
