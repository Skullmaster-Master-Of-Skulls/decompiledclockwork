using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TechnoPro.ClockWorkServer.Client.Services.Properties
{
	// Token: 0x02000172 RID: 370
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x00025466 File Offset: 0x00023666
		internal Resources()
		{
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00025470 File Offset: 0x00023670
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("TechnoPro.ClockWorkServer.Client.Services.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x000254B8 File Offset: 0x000236B8
		// (set) Token: 0x06000E65 RID: 3685 RVA: 0x000254CF File Offset: 0x000236CF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000026 RID: 38
		private static ResourceManager resourceMan;

		// Token: 0x04000027 RID: 39
		private static CultureInfo resourceCulture;
	}
}
