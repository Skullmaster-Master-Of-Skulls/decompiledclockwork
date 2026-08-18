using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200003D RID: 61
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class Resources
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x0000676C File Offset: 0x0000496C
		internal Resources()
		{
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00006774 File Offset: 0x00004974
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.AspNet.Identity.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000067B3 File Offset: 0x000049B3
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x000067BA File Offset: 0x000049BA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000067C2 File Offset: 0x000049C2
		internal static string DefaultError
		{
			get
			{
				return Resources.ResourceManager.GetString("DefaultError", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000067D8 File Offset: 0x000049D8
		internal static string DuplicateEmail
		{
			get
			{
				return Resources.ResourceManager.GetString("DuplicateEmail", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000067EE File Offset: 0x000049EE
		internal static string DuplicateName
		{
			get
			{
				return Resources.ResourceManager.GetString("DuplicateName", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00006804 File Offset: 0x00004A04
		internal static string ExternalLoginExists
		{
			get
			{
				return Resources.ResourceManager.GetString("ExternalLoginExists", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000681A File Offset: 0x00004A1A
		internal static string InvalidEmail
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidEmail", Resources.resourceCulture);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00006830 File Offset: 0x00004A30
		internal static string InvalidToken
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidToken", Resources.resourceCulture);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006846 File Offset: 0x00004A46
		internal static string InvalidUserName
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidUserName", Resources.resourceCulture);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000685C File Offset: 0x00004A5C
		internal static string LockoutNotEnabled
		{
			get
			{
				return Resources.ResourceManager.GetString("LockoutNotEnabled", Resources.resourceCulture);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006872 File Offset: 0x00004A72
		internal static string NoTokenProvider
		{
			get
			{
				return Resources.ResourceManager.GetString("NoTokenProvider", Resources.resourceCulture);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00006888 File Offset: 0x00004A88
		internal static string NoTwoFactorProvider
		{
			get
			{
				return Resources.ResourceManager.GetString("NoTwoFactorProvider", Resources.resourceCulture);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000689E File Offset: 0x00004A9E
		internal static string PasswordMismatch
		{
			get
			{
				return Resources.ResourceManager.GetString("PasswordMismatch", Resources.resourceCulture);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000068B4 File Offset: 0x00004AB4
		internal static string PasswordRequireDigit
		{
			get
			{
				return Resources.ResourceManager.GetString("PasswordRequireDigit", Resources.resourceCulture);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000068CA File Offset: 0x00004ACA
		internal static string PasswordRequireLower
		{
			get
			{
				return Resources.ResourceManager.GetString("PasswordRequireLower", Resources.resourceCulture);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000068E0 File Offset: 0x00004AE0
		internal static string PasswordRequireNonLetterOrDigit
		{
			get
			{
				return Resources.ResourceManager.GetString("PasswordRequireNonLetterOrDigit", Resources.resourceCulture);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000068F6 File Offset: 0x00004AF6
		internal static string PasswordRequireUpper
		{
			get
			{
				return Resources.ResourceManager.GetString("PasswordRequireUpper", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000690C File Offset: 0x00004B0C
		internal static string PasswordTooShort
		{
			get
			{
				return Resources.ResourceManager.GetString("PasswordTooShort", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006922 File Offset: 0x00004B22
		internal static string PropertyTooShort
		{
			get
			{
				return Resources.ResourceManager.GetString("PropertyTooShort", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006938 File Offset: 0x00004B38
		internal static string RoleNotFound
		{
			get
			{
				return Resources.ResourceManager.GetString("RoleNotFound", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000694E File Offset: 0x00004B4E
		internal static string StoreNotIQueryableRoleStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIQueryableRoleStore", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006964 File Offset: 0x00004B64
		internal static string StoreNotIQueryableUserStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIQueryableUserStore", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000697A File Offset: 0x00004B7A
		internal static string StoreNotIUserClaimStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserClaimStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00006990 File Offset: 0x00004B90
		internal static string StoreNotIUserConfirmationStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserConfirmationStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000069A6 File Offset: 0x00004BA6
		internal static string StoreNotIUserEmailStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserEmailStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000069BC File Offset: 0x00004BBC
		internal static string StoreNotIUserLockoutStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserLockoutStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000069D2 File Offset: 0x00004BD2
		internal static string StoreNotIUserLoginStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserLoginStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000069E8 File Offset: 0x00004BE8
		internal static string StoreNotIUserPasswordStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserPasswordStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000069FE File Offset: 0x00004BFE
		internal static string StoreNotIUserPhoneNumberStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserPhoneNumberStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006A14 File Offset: 0x00004C14
		internal static string StoreNotIUserRoleStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserRoleStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00006A2A File Offset: 0x00004C2A
		internal static string StoreNotIUserSecurityStampStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserSecurityStampStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006A40 File Offset: 0x00004C40
		internal static string StoreNotIUserTwoFactorStore
		{
			get
			{
				return Resources.ResourceManager.GetString("StoreNotIUserTwoFactorStore", Resources.resourceCulture);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006A56 File Offset: 0x00004C56
		internal static string UserAlreadyHasPassword
		{
			get
			{
				return Resources.ResourceManager.GetString("UserAlreadyHasPassword", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006A6C File Offset: 0x00004C6C
		internal static string UserAlreadyInRole
		{
			get
			{
				return Resources.ResourceManager.GetString("UserAlreadyInRole", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006A82 File Offset: 0x00004C82
		internal static string UserIdNotFound
		{
			get
			{
				return Resources.ResourceManager.GetString("UserIdNotFound", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006A98 File Offset: 0x00004C98
		internal static string UserNameNotFound
		{
			get
			{
				return Resources.ResourceManager.GetString("UserNameNotFound", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006AAE File Offset: 0x00004CAE
		internal static string UserNotInRole
		{
			get
			{
				return Resources.ResourceManager.GetString("UserNotInRole", Resources.resourceCulture);
			}
		}

		// Token: 0x0400002B RID: 43
		private static ResourceManager resourceMan;

		// Token: 0x0400002C RID: 44
		private static CultureInfo resourceCulture;
	}
}
