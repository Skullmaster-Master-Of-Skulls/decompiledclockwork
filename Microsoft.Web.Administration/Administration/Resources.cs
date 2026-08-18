using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000002 RID: 2
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	internal class Resources
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000010D0
		internal Resources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000010D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.Web.Administration.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002117 File Offset: 0x00001117
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000211E File Offset: 0x0000111E
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

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002126 File Offset: 0x00001126
		internal static string ApplicationPathAlreadyExists
		{
			get
			{
				return Resources.ResourceManager.GetString("ApplicationPathAlreadyExists", Resources.resourceCulture);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000113C
		internal static string ApplicationPathCannotContainChars
		{
			get
			{
				return Resources.ResourceManager.GetString("ApplicationPathCannotContainChars", Resources.resourceCulture);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002152 File Offset: 0x00001152
		internal static string ApplicationPathLengthValidation
		{
			get
			{
				return Resources.ResourceManager.GetString("ApplicationPathLengthValidation", Resources.resourceCulture);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002168 File Offset: 0x00001168
		internal static string ApplicationPoolNameCannotContainChars
		{
			get
			{
				return Resources.ResourceManager.GetString("ApplicationPoolNameCannotContainChars", Resources.resourceCulture);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000217E File Offset: 0x0000117E
		internal static string ApplicationPoolNameLengthValidation
		{
			get
			{
				return Resources.ResourceManager.GetString("ApplicationPoolNameLengthValidation", Resources.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002194 File Offset: 0x00001194
		internal static string BindingInvalidCertificateError
		{
			get
			{
				return Resources.ResourceManager.GetString("BindingInvalidCertificateError", Resources.resourceCulture);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021AA File Offset: 0x000011AA
		internal static string BindingInvalidHttpsBinding
		{
			get
			{
				return Resources.ResourceManager.GetString("BindingInvalidHttpsBinding", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021C0 File Offset: 0x000011C0
		internal static string CertificateNotSpecified
		{
			get
			{
				return Resources.ResourceManager.GetString("CertificateNotSpecified", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021D6 File Offset: 0x000011D6
		internal static string ConfigurationReadOnly
		{
			get
			{
				return Resources.ResourceManager.GetString("ConfigurationReadOnly", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021EC File Offset: 0x000011EC
		internal static string ConstructorNotFound
		{
			get
			{
				return Resources.ResourceManager.GetString("ConstructorNotFound", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002202 File Offset: 0x00001202
		internal static string InvalidElementConfigurationObject
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidElementConfigurationObject", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002218 File Offset: 0x00001218
		internal static string InvalidType
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000222E File Offset: 0x0000122E
		internal static string ObjectHasBeenCommited
		{
			get
			{
				return Resources.ResourceManager.GetString("ObjectHasBeenCommited", Resources.resourceCulture);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002244 File Offset: 0x00001244
		internal static string RemoteNotSupported
		{
			get
			{
				return Resources.ResourceManager.GetString("RemoteNotSupported", Resources.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000013 RID: 19 RVA: 0x0000225A File Offset: 0x0000125A
		internal static string SiteNameCannotContainChars
		{
			get
			{
				return Resources.ResourceManager.GetString("SiteNameCannotContainChars", Resources.resourceCulture);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002270 File Offset: 0x00001270
		internal static string SiteNameLengthValidation
		{
			get
			{
				return Resources.ResourceManager.GetString("SiteNameLengthValidation", Resources.resourceCulture);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002286 File Offset: 0x00001286
		internal static string UIntArgumentOutOfRange
		{
			get
			{
				return Resources.ResourceManager.GetString("UIntArgumentOutOfRange", Resources.resourceCulture);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000016 RID: 22 RVA: 0x0000229C File Offset: 0x0000129C
		internal static string UnableToStartAppPoolWasNotStarted
		{
			get
			{
				return Resources.ResourceManager.GetString("UnableToStartAppPoolWasNotStarted", Resources.resourceCulture);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022B2 File Offset: 0x000012B2
		internal static string UnableToStartW3svcNotStarted
		{
			get
			{
				return Resources.ResourceManager.GetString("UnableToStartW3svcNotStarted", Resources.resourceCulture);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022C8 File Offset: 0x000012C8
		internal static string UnableToStartWasNotStarted
		{
			get
			{
				return Resources.ResourceManager.GetString("UnableToStartWasNotStarted", Resources.resourceCulture);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022DE File Offset: 0x000012DE
		internal static string VirtualDirectoryPathCannotContainChars
		{
			get
			{
				return Resources.ResourceManager.GetString("VirtualDirectoryPathCannotContainChars", Resources.resourceCulture);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022F4 File Offset: 0x000012F4
		internal static string VirtualDirectoryPathLengthValidation
		{
			get
			{
				return Resources.ResourceManager.GetString("VirtualDirectoryPathLengthValidation", Resources.resourceCulture);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000230A File Offset: 0x0000130A
		internal static string WebSiteCannotStartBecausePortUsed
		{
			get
			{
				return Resources.ResourceManager.GetString("WebSiteCannotStartBecausePortUsed", Resources.resourceCulture);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager resourceMan;

		// Token: 0x04000002 RID: 2
		private static CultureInfo resourceCulture;
	}
}
