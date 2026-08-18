using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000925 RID: 2341
	public sealed class FileSystemAuditRule : AuditRule
	{
		// Token: 0x06005486 RID: 21638 RVA: 0x00132572 File Offset: 0x00131572
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, AuditFlags flags) : this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0013257F File Offset: 0x0013157F
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(identity, FileSystemAuditRule.AccessMaskFromRights(fileSystemRights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x00132594 File Offset: 0x00131594
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, AuditFlags flags) : this(new NTAccount(identity), fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x001325A6 File Offset: 0x001315A6
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : this(new NTAccount(identity), FileSystemAuditRule.AccessMaskFromRights(fileSystemRights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x001325C0 File Offset: 0x001315C0
		internal FileSystemAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x001325D4 File Offset: 0x001315D4
		private static int AccessMaskFromRights(FileSystemRights fileSystemRights)
		{
			if (fileSystemRights < (FileSystemRights)0 || fileSystemRights > FileSystemRights.FullControl)
			{
				throw new ArgumentOutOfRangeException("fileSystemRights", Environment.GetResourceString("Argument_InvalidEnumValue", new object[]
				{
					fileSystemRights,
					"FileSystemRights"
				}));
			}
			return (int)fileSystemRights;
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x0600548C RID: 21644 RVA: 0x0013261C File Offset: 0x0013161C
		public FileSystemRights FileSystemRights
		{
			get
			{
				return FileSystemAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}
	}
}
