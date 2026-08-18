using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000664 RID: 1636
	[ComVisible(true)]
	[Serializable]
	public sealed class RegistryPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06003B0B RID: 15115 RVA: 0x000C7D89 File Offset: 0x000C6D89
		public RegistryPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_unrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_unrestricted = false;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x06003B0C RID: 15116 RVA: 0x000C7DB7 File Offset: 0x000C6DB7
		public RegistryPermission(RegistryPermissionAccess access, string pathList)
		{
			this.SetPathList(access, pathList);
		}

		// Token: 0x06003B0D RID: 15117 RVA: 0x000C7DC7 File Offset: 0x000C6DC7
		public RegistryPermission(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			this.m_unrestricted = false;
			this.AddPathList(access, control, pathList);
		}

		// Token: 0x06003B0E RID: 15118 RVA: 0x000C7DDF File Offset: 0x000C6DDF
		public void SetPathList(RegistryPermissionAccess access, string pathList)
		{
			this.VerifyAccess(access);
			this.m_unrestricted = false;
			if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
			{
				this.m_read = null;
			}
			if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
			{
				this.m_write = null;
			}
			if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess)
			{
				this.m_create = null;
			}
			this.AddPathList(access, pathList);
		}

		// Token: 0x06003B0F RID: 15119 RVA: 0x000C7E1B File Offset: 0x000C6E1B
		internal void SetPathList(AccessControlActions control, string pathList)
		{
			this.m_unrestricted = false;
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				this.m_viewAcl = null;
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				this.m_changeAcl = null;
			}
			this.AddPathList(RegistryPermissionAccess.NoAccess, control, pathList);
		}

		// Token: 0x06003B10 RID: 15120 RVA: 0x000C7E45 File Offset: 0x000C6E45
		public void AddPathList(RegistryPermissionAccess access, string pathList)
		{
			this.AddPathList(access, AccessControlActions.None, pathList);
		}

		// Token: 0x06003B11 RID: 15121 RVA: 0x000C7E50 File Offset: 0x000C6E50
		public void AddPathList(RegistryPermissionAccess access, AccessControlActions control, string pathList)
		{
			this.VerifyAccess(access);
			if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_read == null)
				{
					this.m_read = new StringExpressionSet();
				}
				this.m_read.AddExpressions(pathList);
			}
			if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_write == null)
				{
					this.m_write = new StringExpressionSet();
				}
				this.m_write.AddExpressions(pathList);
			}
			if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_create == null)
				{
					this.m_create = new StringExpressionSet();
				}
				this.m_create.AddExpressions(pathList);
			}
			if ((control & AccessControlActions.View) != AccessControlActions.None)
			{
				if (this.m_viewAcl == null)
				{
					this.m_viewAcl = new StringExpressionSet();
				}
				this.m_viewAcl.AddExpressions(pathList);
			}
			if ((control & AccessControlActions.Change) != AccessControlActions.None)
			{
				if (this.m_changeAcl == null)
				{
					this.m_changeAcl = new StringExpressionSet();
				}
				this.m_changeAcl.AddExpressions(pathList);
			}
		}

		// Token: 0x06003B12 RID: 15122 RVA: 0x000C7F18 File Offset: 0x000C6F18
		public string GetPathList(RegistryPermissionAccess access)
		{
			this.VerifyAccess(access);
			this.ExclusiveAccess(access);
			if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_read == null)
				{
					return "";
				}
				return this.m_read.ToString();
			}
			else if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
			{
				if (this.m_write == null)
				{
					return "";
				}
				return this.m_write.ToString();
			}
			else
			{
				if ((access & RegistryPermissionAccess.Create) == RegistryPermissionAccess.NoAccess)
				{
					return "";
				}
				if (this.m_create == null)
				{
					return "";
				}
				return this.m_create.ToString();
			}
		}

		// Token: 0x06003B13 RID: 15123 RVA: 0x000C7F98 File Offset: 0x000C6F98
		private void VerifyAccess(RegistryPermissionAccess access)
		{
			if ((access & ~(RegistryPermissionAccess.Read | RegistryPermissionAccess.Write | RegistryPermissionAccess.Create)) != RegistryPermissionAccess.NoAccess)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					(int)access
				}));
			}
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x000C7FD6 File Offset: 0x000C6FD6
		private void ExclusiveAccess(RegistryPermissionAccess access)
		{
			if (access == RegistryPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
			if ((access & access - 1) != RegistryPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x000C8004 File Offset: 0x000C7004
		private bool IsEmpty()
		{
			return !this.m_unrestricted && (this.m_read == null || this.m_read.IsEmpty()) && (this.m_write == null || this.m_write.IsEmpty()) && (this.m_create == null || this.m_create.IsEmpty()) && (this.m_viewAcl == null || this.m_viewAcl.IsEmpty()) && (this.m_changeAcl == null || this.m_changeAcl.IsEmpty());
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x000C8084 File Offset: 0x000C7084
		public bool IsUnrestricted()
		{
			return this.m_unrestricted;
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x000C808C File Offset: 0x000C708C
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			RegistryPermission registryPermission = target as RegistryPermission;
			if (registryPermission == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[]
				{
					base.GetType().FullName
				}));
			}
			return registryPermission.IsUnrestricted() || (!this.IsUnrestricted() && ((this.m_read == null || this.m_read.IsSubsetOf(registryPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(registryPermission.m_write)) && (this.m_create == null || this.m_create.IsSubsetOf(registryPermission.m_create)) && (this.m_viewAcl == null || this.m_viewAcl.IsSubsetOf(registryPermission.m_viewAcl))) && (this.m_changeAcl == null || this.m_changeAcl.IsSubsetOf(registryPermission.m_changeAcl)));
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000C8170 File Offset: 0x000C7170
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			RegistryPermission registryPermission = (RegistryPermission)target;
			if (registryPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			StringExpressionSet stringExpressionSet = (this.m_read == null) ? null : this.m_read.Intersect(registryPermission.m_read);
			StringExpressionSet stringExpressionSet2 = (this.m_write == null) ? null : this.m_write.Intersect(registryPermission.m_write);
			StringExpressionSet stringExpressionSet3 = (this.m_create == null) ? null : this.m_create.Intersect(registryPermission.m_create);
			StringExpressionSet stringExpressionSet4 = (this.m_viewAcl == null) ? null : this.m_viewAcl.Intersect(registryPermission.m_viewAcl);
			StringExpressionSet stringExpressionSet5 = (this.m_changeAcl == null) ? null : this.m_changeAcl.Intersect(registryPermission.m_changeAcl);
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()) && (stringExpressionSet3 == null || stringExpressionSet3.IsEmpty()) && (stringExpressionSet4 == null || stringExpressionSet4.IsEmpty()) && (stringExpressionSet5 == null || stringExpressionSet5.IsEmpty()))
			{
				return null;
			}
			return new RegistryPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2,
				m_create = stringExpressionSet3,
				m_viewAcl = stringExpressionSet4,
				m_changeAcl = stringExpressionSet5
			};
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000C82F0 File Offset: 0x000C72F0
		public override IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(other))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			RegistryPermission registryPermission = (RegistryPermission)other;
			if (this.IsUnrestricted() || registryPermission.IsUnrestricted())
			{
				return new RegistryPermission(PermissionState.Unrestricted);
			}
			StringExpressionSet stringExpressionSet = (this.m_read == null) ? registryPermission.m_read : this.m_read.Union(registryPermission.m_read);
			StringExpressionSet stringExpressionSet2 = (this.m_write == null) ? registryPermission.m_write : this.m_write.Union(registryPermission.m_write);
			StringExpressionSet stringExpressionSet3 = (this.m_create == null) ? registryPermission.m_create : this.m_create.Union(registryPermission.m_create);
			StringExpressionSet stringExpressionSet4 = (this.m_viewAcl == null) ? registryPermission.m_viewAcl : this.m_viewAcl.Union(registryPermission.m_viewAcl);
			StringExpressionSet stringExpressionSet5 = (this.m_changeAcl == null) ? registryPermission.m_changeAcl : this.m_changeAcl.Union(registryPermission.m_changeAcl);
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()) && (stringExpressionSet3 == null || stringExpressionSet3.IsEmpty()) && (stringExpressionSet4 == null || stringExpressionSet4.IsEmpty()) && (stringExpressionSet5 == null || stringExpressionSet5.IsEmpty()))
			{
				return null;
			}
			return new RegistryPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2,
				m_create = stringExpressionSet3,
				m_viewAcl = stringExpressionSet4,
				m_changeAcl = stringExpressionSet5
			};
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000C8488 File Offset: 0x000C7488
		public override IPermission Copy()
		{
			RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
			if (this.m_unrestricted)
			{
				registryPermission.m_unrestricted = true;
			}
			else
			{
				registryPermission.m_unrestricted = false;
				if (this.m_read != null)
				{
					registryPermission.m_read = this.m_read.Copy();
				}
				if (this.m_write != null)
				{
					registryPermission.m_write = this.m_write.Copy();
				}
				if (this.m_create != null)
				{
					registryPermission.m_create = this.m_create.Copy();
				}
				if (this.m_viewAcl != null)
				{
					registryPermission.m_viewAcl = this.m_viewAcl.Copy();
				}
				if (this.m_changeAcl != null)
				{
					registryPermission.m_changeAcl = this.m_changeAcl.Copy();
				}
			}
			return registryPermission;
		}

		// Token: 0x06003B1B RID: 15131 RVA: 0x000C8538 File Offset: 0x000C7538
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.RegistryPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_read != null && !this.m_read.IsEmpty())
				{
					securityElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
				}
				if (this.m_write != null && !this.m_write.IsEmpty())
				{
					securityElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
				}
				if (this.m_create != null && !this.m_create.IsEmpty())
				{
					securityElement.AddAttribute("Create", SecurityElement.Escape(this.m_create.ToString()));
				}
				if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
				{
					securityElement.AddAttribute("ViewAccessControl", SecurityElement.Escape(this.m_viewAcl.ToString()));
				}
				if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
				{
					securityElement.AddAttribute("ChangeAccessControl", SecurityElement.Escape(this.m_changeAcl.ToString()));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06003B1C RID: 15132 RVA: 0x000C8660 File Offset: 0x000C7660
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_unrestricted = true;
				return;
			}
			this.m_unrestricted = false;
			this.m_read = null;
			this.m_write = null;
			this.m_create = null;
			this.m_viewAcl = null;
			this.m_changeAcl = null;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				this.m_read = new StringExpressionSet(text);
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				this.m_write = new StringExpressionSet(text);
			}
			text = esd.Attribute("Create");
			if (text != null)
			{
				this.m_create = new StringExpressionSet(text);
			}
			text = esd.Attribute("ViewAccessControl");
			if (text != null)
			{
				this.m_viewAcl = new StringExpressionSet(text);
			}
			text = esd.Attribute("ChangeAccessControl");
			if (text != null)
			{
				this.m_changeAcl = new StringExpressionSet(text);
			}
		}

		// Token: 0x06003B1D RID: 15133 RVA: 0x000C8735 File Offset: 0x000C7735
		int IBuiltInPermission.GetTokenIndex()
		{
			return RegistryPermission.GetTokenIndex();
		}

		// Token: 0x06003B1E RID: 15134 RVA: 0x000C873C File Offset: 0x000C773C
		internal static int GetTokenIndex()
		{
			return 5;
		}

		// Token: 0x04001E8E RID: 7822
		private StringExpressionSet m_read;

		// Token: 0x04001E8F RID: 7823
		private StringExpressionSet m_write;

		// Token: 0x04001E90 RID: 7824
		private StringExpressionSet m_create;

		// Token: 0x04001E91 RID: 7825
		[OptionalField(VersionAdded = 2)]
		private StringExpressionSet m_viewAcl;

		// Token: 0x04001E92 RID: 7826
		[OptionalField(VersionAdded = 2)]
		private StringExpressionSet m_changeAcl;

		// Token: 0x04001E93 RID: 7827
		private bool m_unrestricted;
	}
}
