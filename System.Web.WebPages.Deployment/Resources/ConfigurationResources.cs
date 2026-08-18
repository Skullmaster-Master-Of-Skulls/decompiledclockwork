using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.WebPages.Deployment.Resources
{
	// Token: 0x0200000A RID: 10
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	internal class ConfigurationResources
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002DF1 File Offset: 0x00000FF1
		internal ConfigurationResources()
		{
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002DFC File Offset: 0x00000FFC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(ConfigurationResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.WebPages.Deployment.Resources.ConfigurationResources", typeof(ConfigurationResources).Assembly);
					ConfigurationResources.resourceMan = resourceManager;
				}
				return ConfigurationResources.resourceMan;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002E3B File Offset: 0x0000103B
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002E42 File Offset: 0x00001042
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return ConfigurationResources.resourceCulture;
			}
			set
			{
				ConfigurationResources.resourceCulture = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002E4A File Offset: 0x0000104A
		internal static string InstallPathNotFound
		{
			get
			{
				return ConfigurationResources.ResourceManager.GetString("InstallPathNotFound", ConfigurationResources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002E60 File Offset: 0x00001060
		internal static string WebPagesImplicitVersionFailure
		{
			get
			{
				return ConfigurationResources.ResourceManager.GetString("WebPagesImplicitVersionFailure", ConfigurationResources.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002E76 File Offset: 0x00001076
		internal static string WebPagesRegistryKeyDoesNotExist
		{
			get
			{
				return ConfigurationResources.ResourceManager.GetString("WebPagesRegistryKeyDoesNotExist", ConfigurationResources.resourceCulture);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002E8C File Offset: 0x0000108C
		internal static string WebPagesVersionChanges
		{
			get
			{
				return ConfigurationResources.ResourceManager.GetString("WebPagesVersionChanges", ConfigurationResources.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002EA2 File Offset: 0x000010A2
		internal static string WebPagesVersionConflict
		{
			get
			{
				return ConfigurationResources.ResourceManager.GetString("WebPagesVersionConflict", ConfigurationResources.resourceCulture);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002EB8 File Offset: 0x000010B8
		internal static string WebPagesVersionNotFound
		{
			get
			{
				return ConfigurationResources.ResourceManager.GetString("WebPagesVersionNotFound", ConfigurationResources.resourceCulture);
			}
		}

		// Token: 0x04000018 RID: 24
		private static ResourceManager resourceMan;

		// Token: 0x04000019 RID: 25
		private static CultureInfo resourceCulture;
	}
}
