using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ClockWorkAPI.Properties
{
	// Token: 0x0200000B RID: 11
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002C34 File Offset: 0x00001C34
		internal Resources()
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002C40 File Offset: 0x00001C40
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("ClockWorkAPI.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002C8C File Offset: 0x00001C8C
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002CA3 File Offset: 0x00001CA3
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

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002CAC File Offset: 0x00001CAC
		internal static Bitmap check2
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("check2", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002CDC File Offset: 0x00001CDC
		internal static Bitmap delete
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("delete", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002D0C File Offset: 0x00001D0C
		internal static Bitmap delete2
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("delete2", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002D3C File Offset: 0x00001D3C
		internal static Bitmap key1
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("key1", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002D6C File Offset: 0x00001D6C
		internal static Bitmap star_yellow_new
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("star_yellow_new", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x0400000E RID: 14
		private static ResourceManager resourceMan;

		// Token: 0x0400000F RID: 15
		private static CultureInfo resourceCulture;
	}
}
