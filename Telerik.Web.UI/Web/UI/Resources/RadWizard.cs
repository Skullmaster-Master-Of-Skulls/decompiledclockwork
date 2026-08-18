using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Telerik.Web.UI.Resources
{
	// Token: 0x0200077C RID: 1916
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class RadWizard
	{
		// Token: 0x060043A5 RID: 17317 RVA: 0x000D39A3 File Offset: 0x000D1BA3
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal RadWizard()
		{
		}

		// Token: 0x17001606 RID: 5638
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x000D39AC File Offset: 0x000D1BAC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(RadWizard.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Telerik.Web.UI.Resources.RadWizard", typeof(RadWizard).Assembly);
					RadWizard.resourceMan = resourceManager;
				}
				return RadWizard.resourceMan;
			}
		}

		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x060043A7 RID: 17319 RVA: 0x000D39EB File Offset: 0x000D1BEB
		// (set) Token: 0x060043A8 RID: 17320 RVA: 0x000D39F2 File Offset: 0x000D1BF2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return RadWizard.resourceCulture;
			}
			set
			{
				RadWizard.resourceCulture = value;
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x060043A9 RID: 17321 RVA: 0x000D39FA File Offset: 0x000D1BFA
		internal static string Cancel
		{
			get
			{
				return RadWizard.ResourceManager.GetString("Cancel", RadWizard.resourceCulture);
			}
		}

		// Token: 0x17001609 RID: 5641
		// (get) Token: 0x060043AA RID: 17322 RVA: 0x000D3A10 File Offset: 0x000D1C10
		internal static string Finish
		{
			get
			{
				return RadWizard.ResourceManager.GetString("Finish", RadWizard.resourceCulture);
			}
		}

		// Token: 0x1700160A RID: 5642
		// (get) Token: 0x060043AB RID: 17323 RVA: 0x000D3A26 File Offset: 0x000D1C26
		internal static string Next
		{
			get
			{
				return RadWizard.ResourceManager.GetString("Next", RadWizard.resourceCulture);
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x060043AC RID: 17324 RVA: 0x000D3A3C File Offset: 0x000D1C3C
		internal static string Previous
		{
			get
			{
				return RadWizard.ResourceManager.GetString("Previous", RadWizard.resourceCulture);
			}
		}

		// Token: 0x040011E4 RID: 4580
		private static ResourceManager resourceMan;

		// Token: 0x040011E5 RID: 4581
		private static CultureInfo resourceCulture;
	}
}
