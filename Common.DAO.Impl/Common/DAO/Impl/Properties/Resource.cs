using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace TechnoPro.Common.DAO.Impl.Properties
{
	// Token: 0x0200006E RID: 110
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resource
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		internal Resource()
		{
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000160F0 File Offset: 0x000142F0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resource.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("TechnoPro.Common.DAO.Impl.Properties.Resource", typeof(Resource).Assembly);
					Resource.resourceMan = resourceManager;
				}
				return Resource.resourceMan;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00016138 File Offset: 0x00014338
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0001614F File Offset: 0x0001434F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resource.resourceCulture;
			}
			set
			{
				Resource.resourceCulture = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00016158 File Offset: 0x00014358
		internal static Icon clock
		{
			get
			{
				object @object = Resource.ResourceManager.GetObject("clock", Resource.resourceCulture);
				return (Icon)@object;
			}
		}

		// Token: 0x0400011A RID: 282
		private static ResourceManager resourceMan;

		// Token: 0x0400011B RID: 283
		private static CultureInfo resourceCulture;
	}
}
