using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x0200062B RID: 1579
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060038DD RID: 14557 RVA: 0x000BF82D File Offset: 0x000BE82D
		public FileDialogPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				this.Reset();
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000BF861 File Offset: 0x000BE861
		public FileDialogPermission(FileDialogPermissionAccess access)
		{
			FileDialogPermission.VerifyAccess(access);
			this.access = access;
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x000BF876 File Offset: 0x000BE876
		// (set) Token: 0x060038E0 RID: 14560 RVA: 0x000BF87E File Offset: 0x000BE87E
		public FileDialogPermissionAccess Access
		{
			get
			{
				return this.access;
			}
			set
			{
				FileDialogPermission.VerifyAccess(value);
				this.access = value;
			}
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000BF88D File Offset: 0x000BE88D
		public override IPermission Copy()
		{
			return new FileDialogPermission(this.access);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000BF89C File Offset: 0x000BE89C
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.SetUnrestricted(true);
				return;
			}
			this.access = FileDialogPermissionAccess.None;
			string text = esd.Attribute("Access");
			if (text != null)
			{
				this.access = (FileDialogPermissionAccess)Enum.Parse(typeof(FileDialogPermissionAccess), text);
			}
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000BF8F1 File Offset: 0x000BE8F1
		int IBuiltInPermission.GetTokenIndex()
		{
			return FileDialogPermission.GetTokenIndex();
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000BF8F8 File Offset: 0x000BE8F8
		internal static int GetTokenIndex()
		{
			return 1;
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000BF8FC File Offset: 0x000BE8FC
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
			FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
			FileDialogPermissionAccess fileDialogPermissionAccess = this.access & fileDialogPermission.Access;
			if (fileDialogPermissionAccess == FileDialogPermissionAccess.None)
			{
				return null;
			}
			return new FileDialogPermission(fileDialogPermissionAccess);
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000BF968 File Offset: 0x000BE968
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.access == FileDialogPermissionAccess.None;
			}
			bool result;
			try
			{
				FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
				if (fileDialogPermission.IsUnrestricted())
				{
					result = true;
				}
				else if (this.IsUnrestricted())
				{
					result = false;
				}
				else
				{
					int num = (int)(this.access & FileDialogPermissionAccess.Open);
					int num2 = (int)(this.access & FileDialogPermissionAccess.Save);
					int num3 = (int)(fileDialogPermission.Access & FileDialogPermissionAccess.Open);
					int num4 = (int)(fileDialogPermission.Access & FileDialogPermissionAccess.Save);
					result = (num <= num3 && num2 <= num4);
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

		// Token: 0x060038E7 RID: 14567 RVA: 0x000BFA24 File Offset: 0x000BEA24
		public bool IsUnrestricted()
		{
			return this.access == FileDialogPermissionAccess.OpenSave;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000BFA2F File Offset: 0x000BEA2F
		private void Reset()
		{
			this.access = FileDialogPermissionAccess.None;
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000BFA38 File Offset: 0x000BEA38
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.access = FileDialogPermissionAccess.OpenSave;
			}
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000BFA44 File Offset: 0x000BEA44
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.FileDialogPermission");
			if (!this.IsUnrestricted())
			{
				if (this.access != FileDialogPermissionAccess.None)
				{
					securityElement.AddAttribute("Access", Enum.GetName(typeof(FileDialogPermissionAccess), this.access));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000BFAA8 File Offset: 0x000BEAA8
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), new object[]
				{
					base.GetType().FullName
				}));
			}
			FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
			return new FileDialogPermission(this.access | fileDialogPermission.Access);
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000BFB14 File Offset: 0x000BEB14
		private static void VerifyAccess(FileDialogPermissionAccess access)
		{
			if ((access & ~(FileDialogPermissionAccess.Open | FileDialogPermissionAccess.Save)) != FileDialogPermissionAccess.None)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_EnumIllegalVal"), new object[]
				{
					(int)access
				}));
			}
		}

		// Token: 0x04001D8B RID: 7563
		private FileDialogPermissionAccess access;
	}
}
