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
	// Token: 0x0200002B RID: 43
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class CommonWebApiResources
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00006F4D File Offset: 0x0000514D
		internal CommonWebApiResources()
		{
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006F64 File Offset: 0x00005164
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006FE0 File Offset: 0x000051E0
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00006FE7 File Offset: 0x000051E7
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006FEF File Offset: 0x000051EF
		internal static string ArgumentInvalidAbsoluteUri
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidAbsoluteUri", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007005 File Offset: 0x00005205
		internal static string ArgumentInvalidHttpUriScheme
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidHttpUriScheme", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000701B File Offset: 0x0000521B
		internal static string ArgumentMustBeGreaterThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00007031 File Offset: 0x00005231
		internal static string ArgumentMustBeLessThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeLessThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007047 File Offset: 0x00005247
		internal static string ArgumentNullOrEmpty
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentNullOrEmpty", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000705D File Offset: 0x0000525D
		internal static string ArgumentUriHasQueryOrFragment
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentUriHasQueryOrFragment", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007073 File Offset: 0x00005273
		internal static string InvalidEnumArgument
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("InvalidEnumArgument", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x04000060 RID: 96
		private static ResourceManager resourceMan;

		// Token: 0x04000061 RID: 97
		private static CultureInfo resourceCulture;
	}
}
