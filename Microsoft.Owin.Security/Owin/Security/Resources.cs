using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Owin.Security
{
	// Token: 0x02000034 RID: 52
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00004816 File Offset: 0x00002A16
		internal Resources()
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004820 File Offset: 0x00002A20
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.Owin.Security.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DE RID: 222 RVA: 0x0000485F File Offset: 0x00002A5F
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004866 File Offset: 0x00002A66
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

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000486E File Offset: 0x00002A6E
		internal static string Exception_AuthenticationTokenDoesNotProvideSyncMethods
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_AuthenticationTokenDoesNotProvideSyncMethods", Resources.resourceCulture);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004884 File Offset: 0x00002A84
		internal static string Exception_DefaultDpapiRequiresAppNameKey
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_DefaultDpapiRequiresAppNameKey", Resources.resourceCulture);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x0000489A File Offset: 0x00002A9A
		internal static string Exception_MissingDefaultSignInAsAuthenticationType
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_MissingDefaultSignInAsAuthenticationType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000048B0 File Offset: 0x00002AB0
		internal static string Exception_UnhookAuthenticationStateType
		{
			get
			{
				return Resources.ResourceManager.GetString("Exception_UnhookAuthenticationStateType", Resources.resourceCulture);
			}
		}

		// Token: 0x04000052 RID: 82
		private static ResourceManager resourceMan;

		// Token: 0x04000053 RID: 83
		private static CultureInfo resourceCulture;
	}
}
