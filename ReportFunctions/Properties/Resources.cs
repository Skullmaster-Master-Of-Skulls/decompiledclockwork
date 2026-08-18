using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace ReportFunctions.Properties
{
	// Token: 0x0200003E RID: 62
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class Resources
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0004315C File Offset: 0x0004215C
		internal Resources()
		{
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00043168 File Offset: 0x00042168
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("ReportFunctions.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000431B4 File Offset: 0x000421B4
		// (set) Token: 0x0600039C RID: 924 RVA: 0x000431CB File Offset: 0x000421CB
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000431D4 File Offset: 0x000421D4
		internal static Bitmap check
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("check", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00043204 File Offset: 0x00042204
		internal static Bitmap check2
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("check2", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00043234 File Offset: 0x00042234
		internal static Bitmap delete
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("delete", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00043264 File Offset: 0x00042264
		internal static Bitmap delete1
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("delete1", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00043294 File Offset: 0x00042294
		internal static Bitmap delete2
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("delete2", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x000432C4 File Offset: 0x000422C4
		internal static Bitmap delete21
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("delete21", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x000432F4 File Offset: 0x000422F4
		internal static Bitmap document_check
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("document_check", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00043324 File Offset: 0x00042324
		internal static Bitmap document_plain
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("document_plain", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x00043354 File Offset: 0x00042354
		internal static Bitmap mail
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("mail", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00043384 File Offset: 0x00042384
		internal static Bitmap mail_view
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("mail_view", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x000433B4 File Offset: 0x000423B4
		internal static string reportFunctionCodestxt
		{
			get
			{
				return Resources.ResourceManager.GetString("reportFunctionCodestxt", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000433DC File Offset: 0x000423DC
		internal static string searchCustomXml
		{
			get
			{
				return Resources.ResourceManager.GetString("searchCustomXml", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x00043404 File Offset: 0x00042404
		internal static string searchDynamicControls
		{
			get
			{
				return Resources.ResourceManager.GetString("searchDynamicControls", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0004342C File Offset: 0x0004242C
		internal static string searchDynamicScreenControls
		{
			get
			{
				return Resources.ResourceManager.GetString("searchDynamicScreenControls", Resources.resourceCulture);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00043454 File Offset: 0x00042454
		internal static Bitmap star_yellow_new
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("star_yellow_new", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00043484 File Offset: 0x00042484
		internal static Bitmap user1_into
		{
			get
			{
				object @object = Resources.ResourceManager.GetObject("user1_into", Resources.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x040001CA RID: 458
		private static ResourceManager resourceMan;

		// Token: 0x040001CB RID: 459
		private static CultureInfo resourceCulture;
	}
}
