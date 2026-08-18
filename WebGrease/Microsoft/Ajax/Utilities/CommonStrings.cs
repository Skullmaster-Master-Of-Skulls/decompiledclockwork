using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x02000023 RID: 35
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class CommonStrings
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00006D58 File Offset: 0x00004F58
		internal CommonStrings()
		{
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00006D60 File Offset: 0x00004F60
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(CommonStrings.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("WebGrease.Ajax.Utilities.CommonStrings", typeof(CommonStrings).Assembly);
					CommonStrings.resourceMan = resourceManager;
				}
				return CommonStrings.resourceMan;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00006D9F File Offset: 0x00004F9F
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00006DA6 File Offset: 0x00004FA6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return CommonStrings.resourceCulture;
			}
			set
			{
				CommonStrings.resourceCulture = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00006DAE File Offset: 0x00004FAE
		internal static string ContextSeparator
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("ContextSeparator", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00006DC4 File Offset: 0x00004FC4
		internal static string FallbackEncodingFailed
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("FallbackEncodingFailed", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00006DDA File Offset: 0x00004FDA
		internal static string InvalidJSONOutput
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("InvalidJSONOutput", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00006DF0 File Offset: 0x00004FF0
		internal static string Severity0
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("Severity0", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00006E06 File Offset: 0x00005006
		internal static string Severity1
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("Severity1", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00006E1C File Offset: 0x0000501C
		internal static string Severity2
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("Severity2", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00006E32 File Offset: 0x00005032
		internal static string Severity3
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("Severity3", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00006E48 File Offset: 0x00005048
		internal static string Severity4
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("Severity4", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00006E5E File Offset: 0x0000505E
		internal static string SeverityUnknown
		{
			get
			{
				return CommonStrings.ResourceManager.GetString("SeverityUnknown", CommonStrings.resourceCulture);
			}
		}

		// Token: 0x04000081 RID: 129
		private static ResourceManager resourceMan;

		// Token: 0x04000082 RID: 130
		private static CultureInfo resourceCulture;
	}
}
