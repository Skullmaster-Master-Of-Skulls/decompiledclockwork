using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.WebPages.Razor.Resources
{
	// Token: 0x02000010 RID: 16
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class RazorWebResources
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003662 File Offset: 0x00001862
		internal RazorWebResources()
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000366C File Offset: 0x0000186C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(RazorWebResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.WebPages.Razor.Resources.RazorWebResources", typeof(RazorWebResources).Assembly);
					RazorWebResources.resourceMan = resourceManager;
				}
				return RazorWebResources.resourceMan;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000036AB File Offset: 0x000018AB
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000036B2 File Offset: 0x000018B2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return RazorWebResources.resourceCulture;
			}
			set
			{
				RazorWebResources.resourceCulture = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000036BA File Offset: 0x000018BA
		internal static string BuildProvider_No_CodeLanguageService_For_Path
		{
			get
			{
				return RazorWebResources.ResourceManager.GetString("BuildProvider_No_CodeLanguageService_For_Path", RazorWebResources.resourceCulture);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000036D0 File Offset: 0x000018D0
		internal static string Could_Not_Locate_FactoryType
		{
			get
			{
				return RazorWebResources.ResourceManager.GetString("Could_Not_Locate_FactoryType", RazorWebResources.resourceCulture);
			}
		}

		// Token: 0x04000045 RID: 69
		private static ResourceManager resourceMan;

		// Token: 0x04000046 RID: 70
		private static CultureInfo resourceCulture;
	}
}
