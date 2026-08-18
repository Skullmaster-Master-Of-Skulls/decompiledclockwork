using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Telerik.Charting.Styles.Skins
{
	// Token: 0x020017C6 RID: 6086
	[CompilerGenerated]
	[DebuggerNonUserCode]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	internal class Images
	{
		// Token: 0x0600ECCD RID: 60621 RVA: 0x00360BBC File Offset: 0x0035EDBC
		internal static Image GetImageFromResource(string name, string skinName)
		{
			if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(skinName))
			{
				return null;
			}
			return Images.ResourceManager.GetObject(name + skinName) as Image;
		}

		// Token: 0x1700479F RID: 18335
		// (get) Token: 0x0600ECCE RID: 60622 RVA: 0x00360BE8 File Offset: 0x0035EDE8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Images.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Telerik.Charting.Styles.Skins.Images", typeof(Images).Assembly);
					Images.resourceMan = resourceManager;
				}
				return Images.resourceMan;
			}
		}

		// Token: 0x170047A0 RID: 18336
		// (get) Token: 0x0600ECCF RID: 60623 RVA: 0x00360C27 File Offset: 0x0035EE27
		// (set) Token: 0x0600ECD0 RID: 60624 RVA: 0x00360C2E File Offset: 0x0035EE2E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Images.resourceCulture;
			}
			set
			{
				Images.resourceCulture = value;
			}
		}

		// Token: 0x170047A1 RID: 18337
		// (get) Token: 0x0600ECD1 RID: 60625 RVA: 0x00360C38 File Offset: 0x0035EE38
		internal static Bitmap chartInox
		{
			get
			{
				object @object = Images.ResourceManager.GetObject("chartInox", Images.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170047A2 RID: 18338
		// (get) Token: 0x0600ECD2 RID: 60626 RVA: 0x00360C60 File Offset: 0x0035EE60
		internal static Bitmap chartMac
		{
			get
			{
				object @object = Images.ResourceManager.GetObject("chartMac", Images.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170047A3 RID: 18339
		// (get) Token: 0x0600ECD3 RID: 60627 RVA: 0x00360C88 File Offset: 0x0035EE88
		internal static Bitmap chartMarble
		{
			get
			{
				object @object = Images.ResourceManager.GetObject("chartMarble", Images.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170047A4 RID: 18340
		// (get) Token: 0x0600ECD4 RID: 60628 RVA: 0x00360CB0 File Offset: 0x0035EEB0
		internal static Bitmap chartMetal
		{
			get
			{
				object @object = Images.ResourceManager.GetObject("chartMetal", Images.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170047A5 RID: 18341
		// (get) Token: 0x0600ECD5 RID: 60629 RVA: 0x00360CD8 File Offset: 0x0035EED8
		internal static Bitmap chartWood
		{
			get
			{
				object @object = Images.ResourceManager.GetObject("chartWood", Images.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170047A6 RID: 18342
		// (get) Token: 0x0600ECD6 RID: 60630 RVA: 0x00360D00 File Offset: 0x0035EF00
		internal static Bitmap plotareaInox
		{
			get
			{
				object @object = Images.ResourceManager.GetObject("plotareaInox", Images.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170047A7 RID: 18343
		// (get) Token: 0x0600ECD7 RID: 60631 RVA: 0x00360D28 File Offset: 0x0035EF28
		internal static Bitmap plotareaMarble
		{
			get
			{
				object @object = Images.ResourceManager.GetObject("plotareaMarble", Images.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x170047A8 RID: 18344
		// (get) Token: 0x0600ECD8 RID: 60632 RVA: 0x00360D50 File Offset: 0x0035EF50
		internal static Bitmap plotareaMetal
		{
			get
			{
				object @object = Images.ResourceManager.GetObject("plotareaMetal", Images.resourceCulture);
				return (Bitmap)@object;
			}
		}

		// Token: 0x0400444B RID: 17483
		private static ResourceManager resourceMan;

		// Token: 0x0400444C RID: 17484
		private static CultureInfo resourceCulture;
	}
}
