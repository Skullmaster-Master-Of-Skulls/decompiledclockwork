using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;

namespace System.Security.AccessControl
{
	// Token: 0x02000913 RID: 2323
	public abstract class ObjectSecurity
	{
		// Token: 0x060053F7 RID: 21495 RVA: 0x0013064A File Offset: 0x0012F64A
		private ObjectSecurity()
		{
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x00130660 File Offset: 0x0012F660
		protected ObjectSecurity(bool isContainer, bool isDS) : this()
		{
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 5);
			this._securityDescriptor = new CommonSecurityDescriptor(isContainer, isDS, ControlFlags.None, null, null, null, discretionaryAcl);
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x0013068E File Offset: 0x0012F68E
		internal ObjectSecurity(CommonSecurityDescriptor securityDescriptor) : this()
		{
			if (securityDescriptor == null)
			{
				throw new ArgumentNullException("securityDescriptor");
			}
			this._securityDescriptor = securityDescriptor;
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x001306AC File Offset: 0x0012F6AC
		private void UpdateWithNewSecurityDescriptor(RawSecurityDescriptor newOne, AccessControlSections includeSections)
		{
			if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None)
			{
				this._ownerModified = true;
				this._securityDescriptor.Owner = newOne.Owner;
			}
			if ((includeSections & AccessControlSections.Group) != AccessControlSections.None)
			{
				this._groupModified = true;
				this._securityDescriptor.Group = newOne.Group;
			}
			if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
			{
				this._saclModified = true;
				if (newOne.SystemAcl != null)
				{
					this._securityDescriptor.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, newOne.SystemAcl, true);
				}
				else
				{
					this._securityDescriptor.SystemAcl = null;
				}
				this._securityDescriptor.UpdateControlFlags(ObjectSecurity.SACL_CONTROL_FLAGS, newOne.ControlFlags & ObjectSecurity.SACL_CONTROL_FLAGS);
			}
			if ((includeSections & AccessControlSections.Access) != AccessControlSections.None)
			{
				this._daclModified = true;
				if (newOne.DiscretionaryAcl != null)
				{
					this._securityDescriptor.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, newOne.DiscretionaryAcl, true);
				}
				else
				{
					this._securityDescriptor.DiscretionaryAcl = null;
				}
				ControlFlags controlFlags = this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclPresent;
				this._securityDescriptor.UpdateControlFlags(ObjectSecurity.DACL_CONTROL_FLAGS, (newOne.ControlFlags | controlFlags) & ObjectSecurity.DACL_CONTROL_FLAGS);
			}
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x001307C5 File Offset: 0x0012F7C5
		protected void ReadLock()
		{
			this._lock.AcquireReaderLock(-1);
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x001307D3 File Offset: 0x0012F7D3
		protected void ReadUnlock()
		{
			this._lock.ReleaseReaderLock();
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x001307E0 File Offset: 0x0012F7E0
		protected void WriteLock()
		{
			this._lock.AcquireWriterLock(-1);
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x001307EE File Offset: 0x0012F7EE
		protected void WriteUnlock()
		{
			this._lock.ReleaseWriterLock();
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x001307FB File Offset: 0x0012F7FB
		// (set) Token: 0x06005400 RID: 21504 RVA: 0x0013082D File Offset: 0x0012F82D
		protected bool OwnerModified
		{
			get
			{
				if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
				}
				return this._ownerModified;
			}
			set
			{
				if (!this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
				}
				this._ownerModified = value;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06005401 RID: 21505 RVA: 0x00130853 File Offset: 0x0012F853
		// (set) Token: 0x06005402 RID: 21506 RVA: 0x00130885 File Offset: 0x0012F885
		protected bool GroupModified
		{
			get
			{
				if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
				}
				return this._groupModified;
			}
			set
			{
				if (!this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
				}
				this._groupModified = value;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06005403 RID: 21507 RVA: 0x001308AB File Offset: 0x0012F8AB
		// (set) Token: 0x06005404 RID: 21508 RVA: 0x001308DD File Offset: 0x0012F8DD
		protected bool AuditRulesModified
		{
			get
			{
				if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
				}
				return this._saclModified;
			}
			set
			{
				if (!this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
				}
				this._saclModified = value;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06005405 RID: 21509 RVA: 0x00130903 File Offset: 0x0012F903
		// (set) Token: 0x06005406 RID: 21510 RVA: 0x00130935 File Offset: 0x0012F935
		protected bool AccessRulesModified
		{
			get
			{
				if (!this._lock.IsReaderLockHeld && !this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForReadOrWrite"));
				}
				return this._daclModified;
			}
			set
			{
				if (!this._lock.IsWriterLockHeld)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustLockForWrite"));
				}
				this._daclModified = value;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06005407 RID: 21511 RVA: 0x0013095B File Offset: 0x0012F95B
		protected bool IsContainer
		{
			get
			{
				return this._securityDescriptor.IsContainer;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x00130968 File Offset: 0x0012F968
		protected bool IsDS
		{
			get
			{
				return this._securityDescriptor.IsDS;
			}
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x00130975 File Offset: 0x0012F975
		protected virtual void Persist(string name, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x0013097C File Offset: 0x0012F97C
		protected virtual void Persist(bool enableOwnershipPrivilege, string name, AccessControlSections includeSections)
		{
			Privilege privilege = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (enableOwnershipPrivilege)
				{
					privilege = new Privilege("SeTakeOwnershipPrivilege");
					try
					{
						privilege.Enable();
					}
					catch (PrivilegeNotHeldException)
					{
					}
				}
				this.Persist(name, includeSections);
			}
			catch
			{
				if (privilege != null)
				{
					privilege.Revert();
				}
				throw;
			}
			finally
			{
				if (privilege != null)
				{
					privilege.Revert();
				}
			}
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x001309F4 File Offset: 0x0012F9F4
		protected virtual void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600540C RID: 21516 RVA: 0x001309FC File Offset: 0x0012F9FC
		public IdentityReference GetOwner(Type targetType)
		{
			this.ReadLock();
			IdentityReference result;
			try
			{
				if (this._securityDescriptor.Owner == null)
				{
					result = null;
				}
				else
				{
					result = this._securityDescriptor.Owner.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return result;
		}

		// Token: 0x0600540D RID: 21517 RVA: 0x00130A54 File Offset: 0x0012FA54
		public void SetOwner(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this._securityDescriptor.Owner = (identity.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier);
				this._ownerModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x0600540E RID: 21518 RVA: 0x00130ABC File Offset: 0x0012FABC
		public IdentityReference GetGroup(Type targetType)
		{
			this.ReadLock();
			IdentityReference result;
			try
			{
				if (this._securityDescriptor.Group == null)
				{
					result = null;
				}
				else
				{
					result = this._securityDescriptor.Group.Translate(targetType);
				}
			}
			finally
			{
				this.ReadUnlock();
			}
			return result;
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x00130B14 File Offset: 0x0012FB14
		public void SetGroup(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this._securityDescriptor.Group = (identity.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier);
				this._groupModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x00130B7C File Offset: 0x0012FB7C
		public virtual void PurgeAccessRules(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this._securityDescriptor.PurgeAccessControl(identity.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier);
				this._daclModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x00130BE4 File Offset: 0x0012FBE4
		public virtual void PurgeAuditRules(IdentityReference identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.WriteLock();
			try
			{
				this._securityDescriptor.PurgeAudit(identity.Translate(typeof(SecurityIdentifier)) as SecurityIdentifier);
				this._saclModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06005412 RID: 21522 RVA: 0x00130C4C File Offset: 0x0012FC4C
		public bool AreAccessRulesProtected
		{
			get
			{
				this.ReadLock();
				bool result;
				try
				{
					result = ((this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) != ControlFlags.None);
				}
				finally
				{
					this.ReadUnlock();
				}
				return result;
			}
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x00130C94 File Offset: 0x0012FC94
		public void SetAccessRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this._securityDescriptor.SetDiscretionaryAclProtection(isProtected, preserveInheritance);
				this._daclModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06005414 RID: 21524 RVA: 0x00130CD4 File Offset: 0x0012FCD4
		public bool AreAuditRulesProtected
		{
			get
			{
				this.ReadLock();
				bool result;
				try
				{
					result = ((this._securityDescriptor.ControlFlags & ControlFlags.SystemAclProtected) != ControlFlags.None);
				}
				finally
				{
					this.ReadUnlock();
				}
				return result;
			}
		}

		// Token: 0x06005415 RID: 21525 RVA: 0x00130D1C File Offset: 0x0012FD1C
		public void SetAuditRuleProtection(bool isProtected, bool preserveInheritance)
		{
			this.WriteLock();
			try
			{
				this._securityDescriptor.SetSystemAclProtection(isProtected, preserveInheritance);
				this._saclModified = true;
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06005416 RID: 21526 RVA: 0x00130D5C File Offset: 0x0012FD5C
		public bool AreAccessRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isDiscretionaryAclCanonical;
				try
				{
					isDiscretionaryAclCanonical = this._securityDescriptor.IsDiscretionaryAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isDiscretionaryAclCanonical;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x00130D98 File Offset: 0x0012FD98
		public bool AreAuditRulesCanonical
		{
			get
			{
				this.ReadLock();
				bool isSystemAclCanonical;
				try
				{
					isSystemAclCanonical = this._securityDescriptor.IsSystemAclCanonical;
				}
				finally
				{
					this.ReadUnlock();
				}
				return isSystemAclCanonical;
			}
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x00130DD4 File Offset: 0x0012FDD4
		public static bool IsSddlConversionSupported()
		{
			return Win32.IsSddlConversionSupported();
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x00130DDC File Offset: 0x0012FDDC
		public string GetSecurityDescriptorSddlForm(AccessControlSections includeSections)
		{
			this.ReadLock();
			string sddlForm;
			try
			{
				sddlForm = this._securityDescriptor.GetSddlForm(includeSections);
			}
			finally
			{
				this.ReadUnlock();
			}
			return sddlForm;
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x00130E18 File Offset: 0x0012FE18
		public void SetSecurityDescriptorSddlForm(string sddlForm)
		{
			this.SetSecurityDescriptorSddlForm(sddlForm, AccessControlSections.All);
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x00130E24 File Offset: 0x0012FE24
		public void SetSecurityDescriptorSddlForm(string sddlForm, AccessControlSections includeSections)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			if ((includeSections & AccessControlSections.All) == AccessControlSections.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "includeSections");
			}
			this.WriteLock();
			try
			{
				this.UpdateWithNewSecurityDescriptor(new RawSecurityDescriptor(sddlForm), includeSections);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x00130E88 File Offset: 0x0012FE88
		public byte[] GetSecurityDescriptorBinaryForm()
		{
			this.ReadLock();
			byte[] result;
			try
			{
				byte[] array = new byte[this._securityDescriptor.BinaryLength];
				this._securityDescriptor.GetBinaryForm(array, 0);
				result = array;
			}
			finally
			{
				this.ReadUnlock();
			}
			return result;
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x00130ED8 File Offset: 0x0012FED8
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm)
		{
			this.SetSecurityDescriptorBinaryForm(binaryForm, AccessControlSections.All);
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x00130EE4 File Offset: 0x0012FEE4
		public void SetSecurityDescriptorBinaryForm(byte[] binaryForm, AccessControlSections includeSections)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if ((includeSections & AccessControlSections.All) == AccessControlSections.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumAtLeastOneFlag"), "includeSections");
			}
			this.WriteLock();
			try
			{
				this.UpdateWithNewSecurityDescriptor(new RawSecurityDescriptor(binaryForm, 0), includeSections);
			}
			finally
			{
				this.WriteUnlock();
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600541F RID: 21535
		public abstract Type AccessRightType { get; }

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06005420 RID: 21536
		public abstract Type AccessRuleType { get; }

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06005421 RID: 21537
		public abstract Type AuditRuleType { get; }

		// Token: 0x06005422 RID: 21538
		protected abstract bool ModifyAccess(AccessControlModification modification, AccessRule rule, out bool modified);

		// Token: 0x06005423 RID: 21539
		protected abstract bool ModifyAudit(AccessControlModification modification, AuditRule rule, out bool modified);

		// Token: 0x06005424 RID: 21540 RVA: 0x00130F48 File Offset: 0x0012FF48
		public virtual bool ModifyAccessRule(AccessControlModification modification, AccessRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AccessRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAccessRuleType"), "rule");
			}
			this.WriteLock();
			bool result;
			try
			{
				result = this.ModifyAccess(modification, rule, out modified);
			}
			finally
			{
				this.WriteUnlock();
			}
			return result;
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x00130FB8 File Offset: 0x0012FFB8
		public virtual bool ModifyAuditRule(AccessControlModification modification, AuditRule rule, out bool modified)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			if (!this.AuditRuleType.IsAssignableFrom(rule.GetType()))
			{
				throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidAuditRuleType"), "rule");
			}
			this.WriteLock();
			bool result;
			try
			{
				result = this.ModifyAudit(modification, rule, out modified);
			}
			finally
			{
				this.WriteUnlock();
			}
			return result;
		}

		// Token: 0x06005426 RID: 21542
		public abstract AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type);

		// Token: 0x06005427 RID: 21543
		public abstract AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags);

		// Token: 0x04002B8B RID: 11147
		private readonly ReaderWriterLock _lock = new ReaderWriterLock();

		// Token: 0x04002B8C RID: 11148
		internal CommonSecurityDescriptor _securityDescriptor;

		// Token: 0x04002B8D RID: 11149
		private bool _ownerModified;

		// Token: 0x04002B8E RID: 11150
		private bool _groupModified;

		// Token: 0x04002B8F RID: 11151
		private bool _saclModified;

		// Token: 0x04002B90 RID: 11152
		private bool _daclModified;

		// Token: 0x04002B91 RID: 11153
		private static readonly ControlFlags SACL_CONTROL_FLAGS = ControlFlags.SystemAclPresent | ControlFlags.SystemAclAutoInherited | ControlFlags.SystemAclProtected;

		// Token: 0x04002B92 RID: 11154
		private static readonly ControlFlags DACL_CONTROL_FLAGS = ControlFlags.DiscretionaryAclPresent | ControlFlags.DiscretionaryAclAutoInherited | ControlFlags.DiscretionaryAclProtected;
	}
}
