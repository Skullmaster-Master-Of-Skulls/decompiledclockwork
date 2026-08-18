using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200093D RID: 2365
	public sealed class CommonSecurityDescriptor : GenericSecurityDescriptor
	{
		// Token: 0x06005543 RID: 21827 RVA: 0x00134EE4 File Offset: 0x00133EE4
		private void CreateFromParts(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			if (systemAcl != null && systemAcl.IsContainer != isContainer)
			{
				throw new ArgumentException(Environment.GetResourceString(isContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "systemAcl");
			}
			if (discretionaryAcl != null && discretionaryAcl.IsContainer != isContainer)
			{
				throw new ArgumentException(Environment.GetResourceString(isContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "discretionaryAcl");
			}
			this._isContainer = isContainer;
			if (systemAcl != null && systemAcl.IsDS != isDS)
			{
				throw new ArgumentException(Environment.GetResourceString(isDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "systemAcl");
			}
			if (discretionaryAcl != null && discretionaryAcl.IsDS != isDS)
			{
				throw new ArgumentException(Environment.GetResourceString(isDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "discretionaryAcl");
			}
			this._isDS = isDS;
			this._sacl = systemAcl;
			if (discretionaryAcl == null)
			{
				discretionaryAcl = DiscretionaryAcl.CreateAllowEveryoneFullAccess(this._isDS, this._isContainer);
			}
			this._dacl = discretionaryAcl;
			ControlFlags controlFlags = flags | ControlFlags.DiscretionaryAclPresent;
			if (systemAcl == null)
			{
				controlFlags &= ~ControlFlags.SystemAclPresent;
			}
			else
			{
				controlFlags |= ControlFlags.SystemAclPresent;
			}
			this._rawSd = new RawSecurityDescriptor(controlFlags, owner, group, (systemAcl == null) ? null : systemAcl.RawAcl, discretionaryAcl.RawAcl);
		}

		// Token: 0x06005544 RID: 21828 RVA: 0x00135013 File Offset: 0x00134013
		public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.CreateFromParts(isContainer, isDS, flags, owner, group, systemAcl, discretionaryAcl);
		}

		// Token: 0x06005545 RID: 21829 RVA: 0x0013502C File Offset: 0x0013402C
		private CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl) : this(isContainer, isDS, flags, owner, group, (systemAcl == null) ? null : new SystemAcl(isContainer, isDS, systemAcl), (discretionaryAcl == null) ? null : new DiscretionaryAcl(isContainer, isDS, discretionaryAcl))
		{
		}

		// Token: 0x06005546 RID: 21830 RVA: 0x00135066 File Offset: 0x00134066
		public CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor) : this(isContainer, isDS, rawSecurityDescriptor, false)
		{
		}

		// Token: 0x06005547 RID: 21831 RVA: 0x00135074 File Offset: 0x00134074
		internal CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor, bool trusted)
		{
			if (rawSecurityDescriptor == null)
			{
				throw new ArgumentNullException("rawSecurityDescriptor");
			}
			this.CreateFromParts(isContainer, isDS, rawSecurityDescriptor.ControlFlags, rawSecurityDescriptor.Owner, rawSecurityDescriptor.Group, (rawSecurityDescriptor.SystemAcl == null) ? null : new SystemAcl(isContainer, isDS, rawSecurityDescriptor.SystemAcl, trusted), (rawSecurityDescriptor.DiscretionaryAcl == null) ? null : new DiscretionaryAcl(isContainer, isDS, rawSecurityDescriptor.DiscretionaryAcl, trusted));
		}

		// Token: 0x06005548 RID: 21832 RVA: 0x001350E3 File Offset: 0x001340E3
		public CommonSecurityDescriptor(bool isContainer, bool isDS, string sddlForm) : this(isContainer, isDS, new RawSecurityDescriptor(sddlForm), true)
		{
		}

		// Token: 0x06005549 RID: 21833 RVA: 0x001350F4 File Offset: 0x001340F4
		public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset) : this(isContainer, isDS, new RawSecurityDescriptor(binaryForm, offset), true)
		{
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x0600554A RID: 21834 RVA: 0x00135107 File Offset: 0x00134107
		internal sealed override GenericAcl GenericSacl
		{
			get
			{
				return this._sacl;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x0600554B RID: 21835 RVA: 0x0013510F File Offset: 0x0013410F
		internal sealed override GenericAcl GenericDacl
		{
			get
			{
				return this._dacl;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x0600554C RID: 21836 RVA: 0x00135117 File Offset: 0x00134117
		public bool IsContainer
		{
			get
			{
				return this._isContainer;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x0600554D RID: 21837 RVA: 0x0013511F File Offset: 0x0013411F
		public bool IsDS
		{
			get
			{
				return this._isDS;
			}
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x0600554E RID: 21838 RVA: 0x00135127 File Offset: 0x00134127
		public override ControlFlags ControlFlags
		{
			get
			{
				return this._rawSd.ControlFlags;
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x0600554F RID: 21839 RVA: 0x00135134 File Offset: 0x00134134
		// (set) Token: 0x06005550 RID: 21840 RVA: 0x00135141 File Offset: 0x00134141
		public override SecurityIdentifier Owner
		{
			get
			{
				return this._rawSd.Owner;
			}
			set
			{
				this._rawSd.Owner = value;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06005551 RID: 21841 RVA: 0x0013514F File Offset: 0x0013414F
		// (set) Token: 0x06005552 RID: 21842 RVA: 0x0013515C File Offset: 0x0013415C
		public override SecurityIdentifier Group
		{
			get
			{
				return this._rawSd.Group;
			}
			set
			{
				this._rawSd.Group = value;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06005553 RID: 21843 RVA: 0x0013516A File Offset: 0x0013416A
		// (set) Token: 0x06005554 RID: 21844 RVA: 0x00135174 File Offset: 0x00134174
		public SystemAcl SystemAcl
		{
			get
			{
				return this._sacl;
			}
			set
			{
				if (value != null)
				{
					if (value.IsContainer != this.IsContainer)
					{
						throw new ArgumentException(Environment.GetResourceString(this.IsContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "value");
					}
					if (value.IsDS != this.IsDS)
					{
						throw new ArgumentException(Environment.GetResourceString(this.IsDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "value");
					}
				}
				this._sacl = value;
				if (this._sacl != null)
				{
					this._rawSd.SystemAcl = this._sacl.RawAcl;
					this.AddControlFlags(ControlFlags.SystemAclPresent);
					return;
				}
				this._rawSd.SystemAcl = null;
				this.RemoveControlFlags(ControlFlags.SystemAclPresent);
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06005555 RID: 21845 RVA: 0x0013522A File Offset: 0x0013422A
		// (set) Token: 0x06005556 RID: 21846 RVA: 0x00135234 File Offset: 0x00134234
		public DiscretionaryAcl DiscretionaryAcl
		{
			get
			{
				return this._dacl;
			}
			set
			{
				if (value != null)
				{
					if (value.IsContainer != this.IsContainer)
					{
						throw new ArgumentException(Environment.GetResourceString(this.IsContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "value");
					}
					if (value.IsDS != this.IsDS)
					{
						throw new ArgumentException(Environment.GetResourceString(this.IsDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "value");
					}
				}
				if (value == null)
				{
					this._dacl = DiscretionaryAcl.CreateAllowEveryoneFullAccess(this.IsDS, this.IsContainer);
				}
				else
				{
					this._dacl = value;
				}
				this._rawSd.DiscretionaryAcl = this._dacl.RawAcl;
				this.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06005557 RID: 21847 RVA: 0x001352E8 File Offset: 0x001342E8
		public bool IsSystemAclCanonical
		{
			get
			{
				return this.SystemAcl == null || this.SystemAcl.IsCanonical;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06005558 RID: 21848 RVA: 0x001352FF File Offset: 0x001342FF
		public bool IsDiscretionaryAclCanonical
		{
			get
			{
				return this.DiscretionaryAcl == null || this.DiscretionaryAcl.IsCanonical;
			}
		}

		// Token: 0x06005559 RID: 21849 RVA: 0x00135316 File Offset: 0x00134316
		public void SetSystemAclProtection(bool isProtected, bool preserveInheritance)
		{
			if (!isProtected)
			{
				this.RemoveControlFlags(ControlFlags.SystemAclProtected);
				return;
			}
			if (!preserveInheritance && this.SystemAcl != null)
			{
				this.SystemAcl.RemoveInheritedAces();
			}
			this.AddControlFlags(ControlFlags.SystemAclProtected);
		}

		// Token: 0x0600555A RID: 21850 RVA: 0x00135348 File Offset: 0x00134348
		public void SetDiscretionaryAclProtection(bool isProtected, bool preserveInheritance)
		{
			if (!isProtected)
			{
				this.RemoveControlFlags(ControlFlags.DiscretionaryAclProtected);
			}
			else
			{
				if (!preserveInheritance && this.DiscretionaryAcl != null)
				{
					this.DiscretionaryAcl.RemoveInheritedAces();
				}
				this.AddControlFlags(ControlFlags.DiscretionaryAclProtected);
			}
			if (this.DiscretionaryAcl != null && this.DiscretionaryAcl.EveryOneFullAccessForNullDacl)
			{
				this.DiscretionaryAcl.EveryOneFullAccessForNullDacl = false;
			}
		}

		// Token: 0x0600555B RID: 21851 RVA: 0x001353A7 File Offset: 0x001343A7
		public void PurgeAccessControl(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (this.DiscretionaryAcl != null)
			{
				this.DiscretionaryAcl.Purge(sid);
			}
		}

		// Token: 0x0600555C RID: 21852 RVA: 0x001353D1 File Offset: 0x001343D1
		public void PurgeAudit(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (this.SystemAcl != null)
			{
				this.SystemAcl.Purge(sid);
			}
		}

		// Token: 0x0600555D RID: 21853 RVA: 0x001353FC File Offset: 0x001343FC
		internal void UpdateControlFlags(ControlFlags flagsToUpdate, ControlFlags newFlags)
		{
			ControlFlags flags = newFlags | (this._rawSd.ControlFlags & ~flagsToUpdate);
			this._rawSd.SetFlags(flags);
		}

		// Token: 0x0600555E RID: 21854 RVA: 0x00135426 File Offset: 0x00134426
		internal void AddControlFlags(ControlFlags flags)
		{
			this._rawSd.SetFlags(this._rawSd.ControlFlags | flags);
		}

		// Token: 0x0600555F RID: 21855 RVA: 0x00135440 File Offset: 0x00134440
		internal void RemoveControlFlags(ControlFlags flags)
		{
			this._rawSd.SetFlags(this._rawSd.ControlFlags & ~flags);
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06005560 RID: 21856 RVA: 0x0013545B File Offset: 0x0013445B
		internal bool IsSystemAclPresent
		{
			get
			{
				return (this._rawSd.ControlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06005561 RID: 21857 RVA: 0x00135471 File Offset: 0x00134471
		internal bool IsDiscretionaryAclPresent
		{
			get
			{
				return (this._rawSd.ControlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None;
			}
		}

		// Token: 0x04002C5B RID: 11355
		private bool _isContainer;

		// Token: 0x04002C5C RID: 11356
		private bool _isDS;

		// Token: 0x04002C5D RID: 11357
		private RawSecurityDescriptor _rawSd;

		// Token: 0x04002C5E RID: 11358
		private SystemAcl _sacl;

		// Token: 0x04002C5F RID: 11359
		private DiscretionaryAcl _dacl;
	}
}
