using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Http.Properties
{
	// Token: 0x020001A9 RID: 425
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	[DebuggerNonUserCode]
	internal class CommonWebApiResources
	{
		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002457C File Offset: 0x0002277C
		internal CommonWebApiResources()
		{
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00024594 File Offset: 0x00022794
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(CommonWebApiResources.resourceMan, null))
				{
					Assembly assembly = typeof(CommonWebApiResources).Assembly;
					string text = (from s in assembly.GetManifestResourceNames()
					where s.EndsWith("CommonWebApiResources.resources", StringComparison.OrdinalIgnoreCase)
					select s).Single<string>();
					text = text.Substring(0, text.Length - 10);
					ResourceManager resourceManager = new ResourceManager(text, assembly);
					CommonWebApiResources.resourceMan = resourceManager;
				}
				return CommonWebApiResources.resourceMan;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x00024610 File Offset: 0x00022810
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x00024617 File Offset: 0x00022817
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return CommonWebApiResources.resourceCulture;
			}
			set
			{
				CommonWebApiResources.resourceCulture = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0002461F File Offset: 0x0002281F
		internal static string ArgumentInvalidAbsoluteUri
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidAbsoluteUri", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00024635 File Offset: 0x00022835
		internal static string ArgumentInvalidHttpUriScheme
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidHttpUriScheme", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0002464B File Offset: 0x0002284B
		internal static string ArgumentMustBeGreaterThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00024661 File Offset: 0x00022861
		internal static string ArgumentMustBeLessThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeLessThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x00024677 File Offset: 0x00022877
		internal static string ArgumentNullOrEmpty
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentNullOrEmpty", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0002468D File Offset: 0x0002288D
		internal static string ArgumentUriHasQueryOrFragment
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentUriHasQueryOrFragment", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000246A3 File Offset: 0x000228A3
		internal static string InvalidEnumArgument
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("InvalidEnumArgument", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x0400032D RID: 813
		private static ResourceManager resourceMan;

		// Token: 0x0400032E RID: 814
		private static CultureInfo resourceCulture;
	}
}
