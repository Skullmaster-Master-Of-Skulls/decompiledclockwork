using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TechnoPro.Common.Core.Updates.Resources
{
	// Token: 0x0200000E RID: 14
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class UpdateResources
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00004ECE File Offset: 0x000030CE
		internal UpdateResources()
		{
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004ED8 File Offset: 0x000030D8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = UpdateResources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("TechnoPro.Common.Core.Updates.Resources.UpdateResources", typeof(UpdateResources).Assembly);
					UpdateResources.resourceMan = resourceManager;
				}
				return UpdateResources.resourceMan;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004F20 File Offset: 0x00003120
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00004F37 File Offset: 0x00003137
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return UpdateResources.resourceCulture;
			}
			set
			{
				UpdateResources.resourceCulture = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00004F40 File Offset: 0x00003140
		internal static string Database_patch_5_13_04_01
		{
			get
			{
				return UpdateResources.ResourceManager.GetString("Database_patch_5_13_04_01", UpdateResources.resourceCulture);
			}
		}

		// Token: 0x0400001B RID: 27
		private static ResourceManager resourceMan;

		// Token: 0x0400001C RID: 28
		private static CultureInfo resourceCulture;
	}
}
