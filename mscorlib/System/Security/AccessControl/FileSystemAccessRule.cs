using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000924 RID: 2340
	public sealed class FileSystemAccessRule : AccessRule
	{
		// Token: 0x0600547E RID: 21630 RVA: 0x0013247B File Offset: 0x0013147B
		public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, AccessControlType type) : this(identity, FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x0013248F File Offset: 0x0013148F
		public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, AccessControlType type) : this(new NTAccount(identity), FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x001324A8 File Offset: 0x001314A8
		public FileSystemAccessRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(identity, FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x001324BF File Offset: 0x001314BF
		public FileSystemAccessRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : this(new NTAccount(identity), FileSystemAccessRule.AccessMaskFromRights(fileSystemRights, type), false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x001324DB File Offset: 0x001314DB
		internal FileSystemAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x001324EC File Offset: 0x001314EC
		public FileSystemRights FileSystemRights
		{
			get
			{
				return FileSystemAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x001324FC File Offset: 0x001314FC
		internal static int AccessMaskFromRights(FileSystemRights fileSystemRights, AccessControlType controlType)
		{
			if (fileSystemRights < (FileSystemRights)0 || fileSystemRights > FileSystemRights.FullControl)
			{
				throw new ArgumentOutOfRangeException("fileSystemRights", Environment.GetResourceString("Argument_InvalidEnumValue", new object[]
				{
					fileSystemRights,
					"FileSystemRights"
				}));
			}
			if (controlType == AccessControlType.Allow)
			{
				fileSystemRights |= FileSystemRights.Synchronize;
			}
			else if (controlType == AccessControlType.Deny && fileSystemRights != FileSystemRights.FullControl && fileSystemRights != (FileSystemRights.ReadData | FileSystemRights.WriteData | FileSystemRights.AppendData | FileSystemRights.ReadExtendedAttributes | FileSystemRights.WriteExtendedAttributes | FileSystemRights.ExecuteFile | FileSystemRights.ReadAttributes | FileSystemRights.WriteAttributes | FileSystemRights.Delete | FileSystemRights.ReadPermissions | FileSystemRights.ChangePermissions | FileSystemRights.TakeOwnership | FileSystemRights.Synchronize))
			{
				fileSystemRights &= ~FileSystemRights.Synchronize;
			}
			return (int)fileSystemRights;
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0013256F File Offset: 0x0013156F
		internal static FileSystemRights RightsFromAccessMask(int accessMask)
		{
			return (FileSystemRights)accessMask;
		}
	}
}
