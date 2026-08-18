using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.AspNet.Identity.EntityFramework
{
	// Token: 0x0200000D RID: 13
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class IdentityResources
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00007238 File Offset: 0x00005438
		internal IdentityResources()
		{
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00007240 File Offset: 0x00005440
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(IdentityResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.AspNet.Identity.EntityFramework.IdentityResources", typeof(IdentityResources).Assembly);
					IdentityResources.resourceMan = resourceManager;
				}
				return IdentityResources.resourceMan;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000727F File Offset: 0x0000547F
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00007286 File Offset: 0x00005486
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return IdentityResources.resourceCulture;
			}
			set
			{
				IdentityResources.resourceCulture = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000728E File Offset: 0x0000548E
		internal static string DbValidationFailed
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("DbValidationFailed", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000072A4 File Offset: 0x000054A4
		internal static string DuplicateEmail
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("DuplicateEmail", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000072BA File Offset: 0x000054BA
		internal static string DuplicateUserName
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("DuplicateUserName", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000072D0 File Offset: 0x000054D0
		internal static string EntityFailedValidation
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("EntityFailedValidation", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000072E6 File Offset: 0x000054E6
		internal static string ExternalLoginExists
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("ExternalLoginExists", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000072FC File Offset: 0x000054FC
		internal static string IdentityV1SchemaError
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("IdentityV1SchemaError", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00007312 File Offset: 0x00005512
		internal static string IncorrectType
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("IncorrectType", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00007328 File Offset: 0x00005528
		internal static string PropertyCannotBeEmpty
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("PropertyCannotBeEmpty", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000733E File Offset: 0x0000553E
		internal static string RoleAlreadyExists
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("RoleAlreadyExists", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00007354 File Offset: 0x00005554
		internal static string RoleIsNotEmpty
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("RoleIsNotEmpty", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000736A File Offset: 0x0000556A
		internal static string RoleNotFound
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("RoleNotFound", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00007380 File Offset: 0x00005580
		internal static string UserAlreadyInRole
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("UserAlreadyInRole", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00007396 File Offset: 0x00005596
		internal static string UserIdNotFound
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("UserIdNotFound", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000073AC File Offset: 0x000055AC
		internal static string UserLoginAlreadyExists
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("UserLoginAlreadyExists", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000073C2 File Offset: 0x000055C2
		internal static string UserNameNotFound
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("UserNameNotFound", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000073D8 File Offset: 0x000055D8
		internal static string UserNotInRole
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("UserNotInRole", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000073EE File Offset: 0x000055EE
		internal static string ValueCannotBeNullOrEmpty
		{
			get
			{
				return IdentityResources.ResourceManager.GetString("ValueCannotBeNullOrEmpty", IdentityResources.resourceCulture);
			}
		}

		// Token: 0x0400001A RID: 26
		private static ResourceManager resourceMan;

		// Token: 0x0400001B RID: 27
		private static CultureInfo resourceCulture;
	}
}
