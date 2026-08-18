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
	// Token: 0x02000074 RID: 116
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class CommonWebApiResources
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		internal CommonWebApiResources()
		{
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000FC44 File Offset: 0x0000DE44
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000FCC0 File Offset: 0x0000DEC0
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000FCC7 File Offset: 0x0000DEC7
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

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000FCCF File Offset: 0x0000DECF
		internal static string ArgumentInvalidAbsoluteUri
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidAbsoluteUri", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0000FCE5 File Offset: 0x0000DEE5
		internal static string ArgumentInvalidHttpUriScheme
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentInvalidHttpUriScheme", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000FCFB File Offset: 0x0000DEFB
		internal static string ArgumentMustBeGreaterThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeGreaterThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000FD11 File Offset: 0x0000DF11
		internal static string ArgumentMustBeLessThanOrEqualTo
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentMustBeLessThanOrEqualTo", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000FD27 File Offset: 0x0000DF27
		internal static string ArgumentNullOrEmpty
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentNullOrEmpty", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000FD3D File Offset: 0x0000DF3D
		internal static string ArgumentUriHasQueryOrFragment
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("ArgumentUriHasQueryOrFragment", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000FD53 File Offset: 0x0000DF53
		internal static string InvalidEnumArgument
		{
			get
			{
				return CommonWebApiResources.ResourceManager.GetString("InvalidEnumArgument", CommonWebApiResources.resourceCulture);
			}
		}

		// Token: 0x0400018F RID: 399
		private static ResourceManager resourceMan;

		// Token: 0x04000190 RID: 400
		private static CultureInfo resourceCulture;
	}
}
