using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Optimization.Resources
{
	// Token: 0x0200003A RID: 58
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class OptimizationResources
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00006220 File Offset: 0x00004420
		internal OptimizationResources()
		{
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006228 File Offset: 0x00004428
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(OptimizationResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.Optimization.Resources.OptimizationResources", typeof(OptimizationResources).Assembly);
					OptimizationResources.resourceMan = resourceManager;
				}
				return OptimizationResources.resourceMan;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006267 File Offset: 0x00004467
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000626E File Offset: 0x0000446E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return OptimizationResources.resourceCulture;
			}
			set
			{
				OptimizationResources.resourceCulture = value;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006276 File Offset: 0x00004476
		internal static string BundleDirectory_does_not_exist
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("BundleDirectory_does_not_exist", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000628C File Offset: 0x0000448C
		internal static string CdnFallBackScriptString
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("CdnFallBackScriptString", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000062A2 File Offset: 0x000044A2
		internal static string DynamicFolderBundle_InvalidPath
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("DynamicFolderBundle_InvalidPath", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000062B8 File Offset: 0x000044B8
		internal static string File_does_not_exist
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("File_does_not_exist", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000062CE File Offset: 0x000044CE
		internal static string InvalidOptimizationMode
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("InvalidOptimizationMode", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000062E4 File Offset: 0x000044E4
		internal static string InvalidPattern
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("InvalidPattern", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000062FA File Offset: 0x000044FA
		internal static string InvalidWildcardSearchPattern
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("InvalidWildcardSearchPattern", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006310 File Offset: 0x00004510
		internal static string MinifyError
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("MinifyError", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006326 File Offset: 0x00004526
		internal static string Parameter_NullOrEmpty
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("Parameter_NullOrEmpty", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000633C File Offset: 0x0000453C
		internal static string Property_NullOrEmpty
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("Property_NullOrEmpty", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006352 File Offset: 0x00004552
		internal static string Type_doesnt_inherit_from_type
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("Type_doesnt_inherit_from_type", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006368 File Offset: 0x00004568
		internal static string UrlMappings_only_app_relative_url_allowed
		{
			get
			{
				return OptimizationResources.ResourceManager.GetString("UrlMappings_only_app_relative_url_allowed", OptimizationResources.resourceCulture);
			}
		}

		// Token: 0x04000084 RID: 132
		private static ResourceManager resourceMan;

		// Token: 0x04000085 RID: 133
		private static CultureInfo resourceCulture;
	}
}
