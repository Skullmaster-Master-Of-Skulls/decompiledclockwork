using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TechnoPro.Common.DAO.Impl.Properties
{
	// Token: 0x0200006D RID: 109
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	public class Resources
	{
		// Token: 0x06000293 RID: 659 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		internal Resources()
		{
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00016060 File Offset: 0x00014260
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("TechnoPro.Common.DAO.Impl.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000160A8 File Offset: 0x000142A8
		// (set) Token: 0x06000296 RID: 662 RVA: 0x000160BF File Offset: 0x000142BF
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000160C8 File Offset: 0x000142C8
		public static string TproTemplates
		{
			get
			{
				return Resources.ResourceManager.GetString("TproTemplates", Resources.resourceCulture);
			}
		}

		// Token: 0x04000118 RID: 280
		private static ResourceManager resourceMan;

		// Token: 0x04000119 RID: 281
		private static CultureInfo resourceCulture;
	}
}
