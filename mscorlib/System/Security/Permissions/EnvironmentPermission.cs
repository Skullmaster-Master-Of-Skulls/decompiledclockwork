using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000629 RID: 1577
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060038CB RID: 14539 RVA: 0x000BF244 File Offset: 0x000BE244
		public EnvironmentPermission(PermissionState state)
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

		// Token: 0x060038CC RID: 14540 RVA: 0x000BF272 File Offset: 0x000BE272
		public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
		{
			this.SetPathList(flag, pathList);
		}

		// Token: 0x060038CD RID: 14541 RVA: 0x000BF282 File Offset: 0x000BE282
		public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			this.VerifyFlag(flag);
			this.m_unrestricted = false;
			if ((flag & EnvironmentPermissionAccess.Read) != EnvironmentPermissionAccess.NoAccess)
			{
				this.m_read = null;
			}
			if ((flag & EnvironmentPermissionAccess.Write) != EnvironmentPermissionAccess.NoAccess)
			{
				this.m_write = null;
			}
			this.AddPathList(flag, pathList);
		}

		// Token: 0x060038CE RID: 14542 RVA: 0x000BF2B4 File Offset: 0x000BE2B4
		public void AddPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			this.VerifyFlag(flag);
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					this.m_read = new EnvironmentStringExpressionSet();
				}
				this.m_read.AddExpressions(pathList);
			}
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Write))
			{
				if (this.m_write == null)
				{
					this.m_write = new EnvironmentStringExpressionSet();
				}
				this.m_write.AddExpressions(pathList);
			}
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x000BF31C File Offset: 0x000BE31C
		public string GetPathList(EnvironmentPermissionAccess flag)
		{
			this.VerifyFlag(flag);
			this.ExclusiveFlag(flag);
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					return "";
				}
				return this.m_read.ToString();
			}
			else
			{
				if (!this.FlagIsSet(flag, EnvironmentPermissionAccess.Write))
				{
					return "";
				}
				if (this.m_write == null)
				{
					return "";
				}
				return this.m_write.ToString();
			}
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000BF384 File Offset: 0x000BE384
		private void VerifyFlag(EnvironmentPermissionAccess flag)
		{
			if ((flag & ~(EnvironmentPermissionAccess.Read | EnvironmentPermissionAccess.Write)) != EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					(int)flag
				}));
			}
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000BF3C2 File Offset: 0x000BE3C2
		private void ExclusiveFlag(EnvironmentPermissionAccess flag)
		{
			if (flag == EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
			if ((flag & flag - 1) != EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x000BF3EE File Offset: 0x000BE3EE
		private bool FlagIsSet(EnvironmentPermissionAccess flag, EnvironmentPermissionAccess question)
		{
			return (flag & question) != EnvironmentPermissionAccess.NoAccess;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000BF3F9 File Offset: 0x000BE3F9
		private bool IsEmpty()
		{
			return !this.m_unrestricted && (this.m_read == null || this.m_read.IsEmpty()) && (this.m_write == null || this.m_write.IsEmpty());
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000BF42F File Offset: 0x000BE42F
		public bool IsUnrestricted()
		{
			return this.m_unrestricted;
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000BF438 File Offset: 0x000BE438
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			bool result;
			try
			{
				EnvironmentPermission environmentPermission = (EnvironmentPermission)target;
				if (environmentPermission.IsUnrestricted())
				{
					result = true;
				}
				else if (this.IsUnrestricted())
				{
					result = false;
				}
				else
				{
					result = ((this.m_read == null || this.m_read.IsSubsetOf(environmentPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(environmentPermission.m_write)));
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			return result;
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000BF4F0 File Offset: 0x000BE4F0
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
			EnvironmentPermission environmentPermission = (EnvironmentPermission)target;
			if (environmentPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			StringExpressionSet stringExpressionSet = (this.m_read == null) ? null : this.m_read.Intersect(environmentPermission.m_read);
			StringExpressionSet stringExpressionSet2 = (this.m_write == null) ? null : this.m_write.Intersect(environmentPermission.m_write);
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
			{
				return null;
			}
			return new EnvironmentPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2
			};
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000BF5D4 File Offset: 0x000BE5D4
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
			EnvironmentPermission environmentPermission = (EnvironmentPermission)other;
			if (this.IsUnrestricted() || environmentPermission.IsUnrestricted())
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			StringExpressionSet stringExpressionSet = (this.m_read == null) ? environmentPermission.m_read : this.m_read.Union(environmentPermission.m_read);
			StringExpressionSet stringExpressionSet2 = (this.m_write == null) ? environmentPermission.m_write : this.m_write.Union(environmentPermission.m_write);
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
			{
				return null;
			}
			return new EnvironmentPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2
			};
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000BF6C0 File Offset: 0x000BE6C0
		public override IPermission Copy()
		{
			EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
			if (this.m_unrestricted)
			{
				environmentPermission.m_unrestricted = true;
			}
			else
			{
				environmentPermission.m_unrestricted = false;
				if (this.m_read != null)
				{
					environmentPermission.m_read = this.m_read.Copy();
				}
				if (this.m_write != null)
				{
					environmentPermission.m_write = this.m_write.Copy();
				}
			}
			return environmentPermission;
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000BF720 File Offset: 0x000BE720
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.EnvironmentPermission");
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
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000BF7B4 File Offset: 0x000BE7B4
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
			string text = esd.Attribute("Read");
			if (text != null)
			{
				this.m_read = new EnvironmentStringExpressionSet(text);
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				this.m_write = new EnvironmentStringExpressionSet(text);
			}
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000BF823 File Offset: 0x000BE823
		int IBuiltInPermission.GetTokenIndex()
		{
			return EnvironmentPermission.GetTokenIndex();
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000BF82A File Offset: 0x000BE82A
		internal static int GetTokenIndex()
		{
			return 0;
		}

		// Token: 0x04001D83 RID: 7555
		private StringExpressionSet m_read;

		// Token: 0x04001D84 RID: 7556
		private StringExpressionSet m_write;

		// Token: 0x04001D85 RID: 7557
		private bool m_unrestricted;
	}
}
