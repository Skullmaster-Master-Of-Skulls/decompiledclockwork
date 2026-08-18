using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel.Activation;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000849 RID: 2121
	internal static class SecurityDescriptorHelper
	{
		// Token: 0x06004F6A RID: 20330 RVA: 0x00122476 File Offset: 0x00120676
		internal static byte[] FromSecurityIdentifiers(List<SecurityIdentifier> allowedSids, int accessRights)
		{
			if (allowedSids == null)
			{
				if (accessRights == -1073741824)
				{
					return SecurityDescriptorHelper.worldCreatorOwnerWithReadAndWriteDescriptorDenyNetwork;
				}
				if (accessRights == -2147483648)
				{
					return SecurityDescriptorHelper.worldCreatorOwnerWithReadDescriptorDenyNetwork;
				}
			}
			return SecurityDescriptorHelper.FromSecurityIdentifiersFull(allowedSids, accessRights);
		}

		// Token: 0x06004F6B RID: 20331 RVA: 0x001224A0 File Offset: 0x001206A0
		private static byte[] FromSecurityIdentifiersFull(List<SecurityIdentifier> allowedSids, int accessRights)
		{
			int capacity = (allowedSids == null) ? 3 : (2 + allowedSids.Count);
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(false, false, capacity);
			discretionaryAcl.AddAccess(AccessControlType.Deny, new SecurityIdentifier(WellKnownSidType.NetworkSid, null), 268435456, InheritanceFlags.None, PropagationFlags.None);
			int accessMask = SecurityDescriptorHelper.GenerateClientAccessRights(accessRights);
			if (allowedSids == null)
			{
				discretionaryAcl.AddAccess(AccessControlType.Allow, new SecurityIdentifier(WellKnownSidType.WorldSid, null), accessMask, InheritanceFlags.None, PropagationFlags.None);
			}
			else
			{
				for (int i = 0; i < allowedSids.Count; i++)
				{
					SecurityIdentifier sid = allowedSids[i];
					discretionaryAcl.AddAccess(AccessControlType.Allow, sid, accessMask, InheritanceFlags.None, PropagationFlags.None);
				}
			}
			discretionaryAcl.AddAccess(AccessControlType.Allow, SecurityDescriptorHelper.GetProcessLogonSid(), accessRights, InheritanceFlags.None, PropagationFlags.None);
			if (AppContainerInfo.IsRunningInAppContainer)
			{
				discretionaryAcl.AddAccess(AccessControlType.Allow, AppContainerInfo.GetCurrentAppContainerSid(), accessRights, InheritanceFlags.None, PropagationFlags.None);
			}
			CommonSecurityDescriptor commonSecurityDescriptor = new CommonSecurityDescriptor(false, false, ControlFlags.None, null, null, null, discretionaryAcl);
			byte[] array = new byte[commonSecurityDescriptor.BinaryLength];
			commonSecurityDescriptor.GetBinaryForm(array, 0);
			return array;
		}

		// Token: 0x06004F6C RID: 20332 RVA: 0x00122570 File Offset: 0x00120770
		private static int GenerateClientAccessRights(int accessRights)
		{
			int num = accessRights;
			if ((num & 1073741824) != 0)
			{
				num &= -1073741825;
				num |= 274;
			}
			return num & -5;
		}

		// Token: 0x06004F6D RID: 20333 RVA: 0x001225A0 File Offset: 0x001207A0
		private static SecurityIdentifier GetProcessLogonSid()
		{
			int id = Process.GetCurrentProcess().Id;
			return Utility.GetLogonSidForPid(id);
		}

		// Token: 0x0400314C RID: 12620
		private static byte[] worldCreatorOwnerWithReadAndWriteDescriptorDenyNetwork = SecurityDescriptorHelper.FromSecurityIdentifiersFull(null, -1073741824);

		// Token: 0x0400314D RID: 12621
		private static byte[] worldCreatorOwnerWithReadDescriptorDenyNetwork = SecurityDescriptorHelper.FromSecurityIdentifiersFull(null, int.MinValue);
	}
}
