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
	// Token: 0x0200001C RID: 28
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class CommonWebApiResources
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000054FA File Offset: 0x000036FA
		internal CommonWebApiResources()
		{
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005510 File Offset: 0x00003710
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000558C File Offset: 0x0000378C
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005593 File Offset: 0x00003793
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000559B File Offset: 0x0000379B
		internal static string ArgumentInvalidAbsoluteUri
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidAbsoluteUri", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000055B1 File Offset: 0x000037B1
		internal static string ArgumentInvalidHttpUriScheme
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidHttpUriScheme", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000055C7 File Offset: 0x000037C7
		internal static string ArgumentMustBeGreaterThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000055DD File Offset: 0x000037DD
		internal static string ArgumentMustBeLessThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeLessThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000055F3 File Offset: 0x000037F3
		internal static string ArgumentNullOrEmpty
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentNullOrEmpty", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005609 File Offset: 0x00003809
		internal static string ArgumentUriHasQueryOrFragment
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentUriHasQueryOrFragment", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000561F File Offset: 0x0000381F
		internal static string InvalidEnumArgument
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("InvalidEnumArgument", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x04000034 RID: 52
		private static ResourceManager resourceMan;

		// Token: 0x04000035 RID: 53
		private static CultureInfo resourceCulture;
	}
}
