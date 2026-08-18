using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TechnoPro.Common.DAO.Reports.Impl.Properties
{
	// Token: 0x0200000B RID: 11
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	public class Resources
	{
		// Token: 0x0600005C RID: 92 RVA: 0x0000834B File Offset: 0x0000654B
		internal Resources()
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00008358 File Offset: 0x00006558
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("TechnoPro.Common.DAO.Reports.Impl.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000083A0 File Offset: 0x000065A0
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000083B7 File Offset: 0x000065B7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000083C0 File Offset: 0x000065C0
		public static string Reports2_0
		{
			get
			{
				return Resources.ResourceManager.GetString("Reports2_0", Resources.resourceCulture);
			}
		}

		// Token: 0x0400002C RID: 44
		private static ResourceManager resourceMan;

		// Token: 0x0400002D RID: 45
		private static CultureInfo resourceCulture;
	}
}
