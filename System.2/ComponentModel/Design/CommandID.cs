using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005CB RID: 1483
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class CommandID
	{
		// Token: 0x06003765 RID: 14181 RVA: 0x000F06EC File Offset: 0x000EE8EC
		public CommandID(Guid menuGroup, int commandID)
		{
			this.menuGroup = menuGroup;
			this.commandID = commandID;
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06003766 RID: 14182 RVA: 0x000F0702 File Offset: 0x000EE902
		public virtual int ID
		{
			get
			{
				return this.commandID;
			}
		}

		// Token: 0x06003767 RID: 14183 RVA: 0x000F070C File Offset: 0x000EE90C
		public override bool Equals(object obj)
		{
			if (!(obj is CommandID))
			{
				return false;
			}
			CommandID commandID = (CommandID)obj;
			return commandID.menuGroup.Equals(this.menuGroup) && commandID.commandID == this.commandID;
		}

		// Token: 0x06003768 RID: 14184 RVA: 0x000F0750 File Offset: 0x000EE950
		public override int GetHashCode()
		{
			return this.menuGroup.GetHashCode() << 2 | this.commandID;
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x06003769 RID: 14185 RVA: 0x000F077A File Offset: 0x000EE97A
		public virtual Guid Guid
		{
			get
			{
				return this.menuGroup;
			}
		}

		// Token: 0x0600376A RID: 14186 RVA: 0x000F0784 File Offset: 0x000EE984
		public override string ToString()
		{
			return this.menuGroup.ToString() + " : " + this.commandID.ToString(CultureInfo.CurrentCulture);
		}

		// Token: 0x04002AF0 RID: 10992
		private readonly Guid menuGroup;

		// Token: 0x04002AF1 RID: 10993
		private readonly int commandID;
	}
}
