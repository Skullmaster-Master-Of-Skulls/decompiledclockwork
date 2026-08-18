using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Internal.Web.Utils
{
	// Token: 0x02000002 RID: 2
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class CommonResources
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal CommonResources()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020E8 File Offset: 0x000002E8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(CommonResources.resourceMan, null))
				{
					string text = (from s in Assembly.GetExecutingAssembly().GetManifestResourceNames()
					where s.EndsWith("CommonResources.resources", StringComparison.OrdinalIgnoreCase)
					select s).Single<string>();
					text = text.Substring(0, text.Length - 10);
					ResourceManager resourceManager = new ResourceManager(text, typeof(CommonResources).Assembly);
					CommonResources.resourceMan = resourceManager;
				}
				return CommonResources.resourceMan;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002166 File Offset: 0x00000366
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000216D File Offset: 0x0000036D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return CommonResources.resourceCulture;
			}
			set
			{
				CommonResources.resourceCulture = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002175 File Offset: 0x00000375
		internal static string Argument_Cannot_Be_Null_Or_Empty
		{
			get
			{
				return CommonResources.ResourceManager.GetString("Argument_Cannot_Be_Null_Or_Empty", CommonResources.resourceCulture);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000218B File Offset: 0x0000038B
		internal static string Argument_Must_Be_Between
		{
			get
			{
				return CommonResources.ResourceManager.GetString("Argument_Must_Be_Between", CommonResources.resourceCulture);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000021A1 File Offset: 0x000003A1
		internal static string Argument_Must_Be_Enum_Member
		{
			get
			{
				return CommonResources.ResourceManager.GetString("Argument_Must_Be_Enum_Member", CommonResources.resourceCulture);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021B7 File Offset: 0x000003B7
		internal static string Argument_Must_Be_GreaterThan
		{
			get
			{
				return CommonResources.ResourceManager.GetString("Argument_Must_Be_GreaterThan", CommonResources.resourceCulture);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000021CD File Offset: 0x000003CD
		internal static string Argument_Must_Be_GreaterThanOrEqualTo
		{
			get
			{
				return CommonResources.ResourceManager.GetString("Argument_Must_Be_GreaterThanOrEqualTo", CommonResources.resourceCulture);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000021E3 File Offset: 0x000003E3
		internal static string Argument_Must_Be_LessThan
		{
			get
			{
				return CommonResources.ResourceManager.GetString("Argument_Must_Be_LessThan", CommonResources.resourceCulture);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021F9 File Offset: 0x000003F9
		internal static string Argument_Must_Be_LessThanOrEqualTo
		{
			get
			{
				return CommonResources.ResourceManager.GetString("Argument_Must_Be_LessThanOrEqualTo", CommonResources.resourceCulture);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000220F File Offset: 0x0000040F
		internal static string Argument_Must_Be_Null_Or_Non_Empty
		{
			get
			{
				return CommonResources.ResourceManager.GetString("Argument_Must_Be_Null_Or_Non_Empty", CommonResources.resourceCulture);
			}
		}

		// Token: 0x04000001 RID: 1
		private static ResourceManager resourceMan;

		// Token: 0x04000002 RID: 2
		private static CultureInfo resourceCulture;
	}
}
