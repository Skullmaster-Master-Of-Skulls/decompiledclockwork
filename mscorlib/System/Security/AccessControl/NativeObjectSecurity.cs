using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000915 RID: 2325
	public abstract class NativeObjectSecurity : CommonObjectSecurity
	{
		// Token: 0x0600543C RID: 21564 RVA: 0x00131B70 File Offset: 0x00130B70
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType) : base(isContainer)
		{
			if (!Win32.IsLsaPolicySupported())
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresNT"));
			}
			this._resourceType = resourceType;
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00131BCE File Offset: 0x00130BCE
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext) : this(isContainer, resourceType)
		{
			this._exceptionContext = exceptionContext;
			this._exceptionFromErrorCode = exceptionFromErrorCode;
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x00131BE7 File Offset: 0x00130BE7
		internal NativeObjectSecurity(ResourceType resourceType, CommonSecurityDescriptor securityDescriptor) : this(resourceType, securityDescriptor, null)
		{
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x00131BF4 File Offset: 0x00130BF4
		internal NativeObjectSecurity(ResourceType resourceType, CommonSecurityDescriptor securityDescriptor, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode) : base(securityDescriptor)
		{
			if (!Win32.IsLsaPolicySupported())
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresNT"));
			}
			this._resourceType = resourceType;
			this._exceptionFromErrorCode = exceptionFromErrorCode;
		}

		// Token: 0x06005440 RID: 21568 RVA: 0x00131C5C File Offset: 0x00130C5C
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext) : this(resourceType, NativeObjectSecurity.CreateInternal(resourceType, isContainer, name, null, includeSections, true, exceptionFromErrorCode, exceptionContext), exceptionFromErrorCode)
		{
		}

		// Token: 0x06005441 RID: 21569 RVA: 0x00131C82 File Offset: 0x00130C82
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections) : this(isContainer, resourceType, name, includeSections, null, null)
		{
		}

		// Token: 0x06005442 RID: 21570 RVA: 0x00131C94 File Offset: 0x00130C94
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext) : this(resourceType, NativeObjectSecurity.CreateInternal(resourceType, isContainer, null, handle, includeSections, false, exceptionFromErrorCode, exceptionContext), exceptionFromErrorCode)
		{
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x00131CBA File Offset: 0x00130CBA
		protected NativeObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle handle, AccessControlSections includeSections) : this(isContainer, resourceType, handle, includeSections, null, null)
		{
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00131CCC File Offset: 0x00130CCC
		private static CommonSecurityDescriptor CreateInternal(ResourceType resourceType, bool isContainer, string name, SafeHandle handle, AccessControlSections includeSections, bool createByName, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
		{
			if (createByName && name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (!createByName && handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			RawSecurityDescriptor rawSecurityDescriptor;
			int securityInfo = Win32.GetSecurityInfo(resourceType, name, handle, includeSections, out rawSecurityDescriptor);
			if (securityInfo != 0)
			{
				Exception ex = null;
				if (exceptionFromErrorCode != null)
				{
					ex = exceptionFromErrorCode(securityInfo, name, handle, exceptionContext);
				}
				if (ex == null)
				{
					if (securityInfo == 5)
					{
						ex = new UnauthorizedAccessException();
					}
					else if (securityInfo == 1307)
					{
						ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidOwner"));
					}
					else if (securityInfo == 1308)
					{
						ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidGroup"));
					}
					else if (securityInfo == 87)
					{
						ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", new object[]
						{
							securityInfo
						}));
					}
					else if (securityInfo == 123)
					{
						ex = new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
					}
					else if (securityInfo == 2)
					{
						ex = ((name == null) ? new FileNotFoundException() : new FileNotFoundException(name));
					}
					else if (securityInfo == 1350)
					{
						ex = new NotSupportedException(Environment.GetResourceString("AccessControl_NoAssociatedSecurity"));
					}
					else
					{
						ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", new object[]
						{
							securityInfo
						}));
					}
				}
				throw ex;
			}
			return new CommonSecurityDescriptor(isContainer, false, rawSecurityDescriptor, true);
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x00131E18 File Offset: 0x00130E18
		private void Persist(string name, SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
		{
			base.WriteLock();
			try
			{
				SecurityInfos securityInfos = (SecurityInfos)0;
				SecurityIdentifier owner = null;
				SecurityIdentifier group = null;
				SystemAcl sacl = null;
				DiscretionaryAcl dacl = null;
				if ((includeSections & AccessControlSections.Owner) != AccessControlSections.None && this._securityDescriptor.Owner != null)
				{
					securityInfos |= SecurityInfos.Owner;
					owner = this._securityDescriptor.Owner;
				}
				if ((includeSections & AccessControlSections.Group) != AccessControlSections.None && this._securityDescriptor.Group != null)
				{
					securityInfos |= SecurityInfos.Group;
					group = this._securityDescriptor.Group;
				}
				if ((includeSections & AccessControlSections.Audit) != AccessControlSections.None)
				{
					securityInfos |= SecurityInfos.SystemAcl;
					if (this._securityDescriptor.IsSystemAclPresent && this._securityDescriptor.SystemAcl != null && this._securityDescriptor.SystemAcl.Count > 0)
					{
						sacl = this._securityDescriptor.SystemAcl;
					}
					else
					{
						sacl = null;
					}
					if ((this._securityDescriptor.ControlFlags & ControlFlags.SystemAclProtected) != ControlFlags.None)
					{
						securityInfos |= (SecurityInfos)this.ProtectedSystemAcl;
					}
					else
					{
						securityInfos |= (SecurityInfos)this.UnprotectedSystemAcl;
					}
				}
				if ((includeSections & AccessControlSections.Access) != AccessControlSections.None && this._securityDescriptor.IsDiscretionaryAclPresent)
				{
					securityInfos |= SecurityInfos.DiscretionaryAcl;
					if (this._securityDescriptor.DiscretionaryAcl.EveryOneFullAccessForNullDacl)
					{
						dacl = null;
					}
					else
					{
						dacl = this._securityDescriptor.DiscretionaryAcl;
					}
					if ((this._securityDescriptor.ControlFlags & ControlFlags.DiscretionaryAclProtected) != ControlFlags.None)
					{
						securityInfos |= (SecurityInfos)this.ProtectedDiscretionaryAcl;
					}
					else
					{
						securityInfos |= (SecurityInfos)this.UnprotectedDiscretionaryAcl;
					}
				}
				if (securityInfos != (SecurityInfos)0)
				{
					int num = Win32.SetSecurityInfo(this._resourceType, name, handle, securityInfos, owner, group, sacl, dacl);
					if (num != 0)
					{
						Exception ex = null;
						if (this._exceptionFromErrorCode != null)
						{
							ex = this._exceptionFromErrorCode(num, name, handle, exceptionContext);
						}
						if (ex == null)
						{
							if (num == 5)
							{
								ex = new UnauthorizedAccessException();
							}
							else if (num == 1307)
							{
								ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidOwner"));
							}
							else if (num == 1308)
							{
								ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_InvalidGroup"));
							}
							else if (num == 123)
							{
								ex = new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
							}
							else if (num == 6)
							{
								ex = new NotSupportedException(Environment.GetResourceString("AccessControl_InvalidHandle"));
							}
							else if (num == 2)
							{
								ex = new FileNotFoundException();
							}
							else if (num == 1350)
							{
								ex = new NotSupportedException(Environment.GetResourceString("AccessControl_NoAssociatedSecurity"));
							}
							else
							{
								ex = new InvalidOperationException(Environment.GetResourceString("AccessControl_UnexpectedError", new object[]
								{
									num
								}));
							}
						}
						throw ex;
					}
					base.OwnerModified = false;
					base.GroupModified = false;
					base.AccessRulesModified = false;
					base.AuditRulesModified = false;
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x001320B0 File Offset: 0x001310B0
		protected sealed override void Persist(string name, AccessControlSections includeSections)
		{
			this.Persist(name, includeSections, this._exceptionContext);
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x001320C0 File Offset: 0x001310C0
		protected void Persist(string name, AccessControlSections includeSections, object exceptionContext)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Persist(name, null, includeSections, exceptionContext);
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x001320DA File Offset: 0x001310DA
		protected sealed override void Persist(SafeHandle handle, AccessControlSections includeSections)
		{
			this.Persist(handle, includeSections, this._exceptionContext);
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x001320EA File Offset: 0x001310EA
		protected void Persist(SafeHandle handle, AccessControlSections includeSections, object exceptionContext)
		{
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			this.Persist(null, handle, includeSections, exceptionContext);
		}

		// Token: 0x04002B93 RID: 11155
		private readonly ResourceType _resourceType;

		// Token: 0x04002B94 RID: 11156
		private NativeObjectSecurity.ExceptionFromErrorCode _exceptionFromErrorCode;

		// Token: 0x04002B95 RID: 11157
		private object _exceptionContext;

		// Token: 0x04002B96 RID: 11158
		private readonly uint ProtectedDiscretionaryAcl = 2147483648U;

		// Token: 0x04002B97 RID: 11159
		private readonly uint ProtectedSystemAcl = 1073741824U;

		// Token: 0x04002B98 RID: 11160
		private readonly uint UnprotectedDiscretionaryAcl = 536870912U;

		// Token: 0x04002B99 RID: 11161
		private readonly uint UnprotectedSystemAcl = 268435456U;

		// Token: 0x02000916 RID: 2326
		// (Invoke) Token: 0x0600544B RID: 21579
		protected internal delegate Exception ExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context);
	}
}
