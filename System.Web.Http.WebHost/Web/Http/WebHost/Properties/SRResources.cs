using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace System.Web.Http.WebHost.Properties
{
	// Token: 0x02000023 RID: 35
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[CompilerGenerated]
	[DebuggerNonUserCode]
	internal class SRResources
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00004D2E File Offset: 0x00002F2E
		internal SRResources()
		{
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004D38 File Offset: 0x00002F38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(SRResources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("System.Web.Http.WebHost.Properties.SRResources", typeof(SRResources).Assembly);
					SRResources.resourceMan = resourceManager;
				}
				return SRResources.resourceMan;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004D77 File Offset: 0x00002F77
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004D7E File Offset: 0x00002F7E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return SRResources.resourceCulture;
			}
			set
			{
				SRResources.resourceCulture = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004D86 File Offset: 0x00002F86
		internal static string RequestBodyAlreadyRead
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestBodyAlreadyRead", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004D9C File Offset: 0x00002F9C
		internal static string RequestBodyAlreadyReadInMode
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestBodyAlreadyReadInMode", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004DB2 File Offset: 0x00002FB2
		internal static string RequestStreamCannotBeReadBufferless
		{
			get
			{
				return SRResources.ResourceManager.GetString("RequestStreamCannotBeReadBufferless", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004DC8 File Offset: 0x00002FC8
		internal static string Route_ValidationMustBeStringOrCustomConstraint
		{
			get
			{
				return SRResources.ResourceManager.GetString("Route_ValidationMustBeStringOrCustomConstraint", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004DDE File Offset: 0x00002FDE
		internal static string RouteCollectionNotSupported
		{
			get
			{
				return SRResources.ResourceManager.GetString("RouteCollectionNotSupported", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004DF4 File Offset: 0x00002FF4
		internal static string RouteCollectionOutOfRange
		{
			get
			{
				return SRResources.ResourceManager.GetString("RouteCollectionOutOfRange", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004E0A File Offset: 0x0000300A
		internal static string RouteCollectionUseDirectly
		{
			get
			{
				return SRResources.ResourceManager.GetString("RouteCollectionUseDirectly", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004E20 File Offset: 0x00003020
		internal static string Serialize_Response_Failed
		{
			get
			{
				return SRResources.ResourceManager.GetString("Serialize_Response_Failed", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004E36 File Offset: 0x00003036
		internal static string Serialize_Response_Failed_MediaType
		{
			get
			{
				return SRResources.ResourceManager.GetString("Serialize_Response_Failed_MediaType", SRResources.resourceCulture);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004E4C File Offset: 0x0000304C
		internal static string TypeCache_DoNotModify
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypeCache_DoNotModify", SRResources.resourceCulture);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004E62 File Offset: 0x00003062
		internal static string TypePropertyMustNotBeNull
		{
			get
			{
				return SRResources.ResourceManager.GetString("TypePropertyMustNotBeNull", SRResources.resourceCulture);
			}
		}

		// Token: 0x04000039 RID: 57
		private static ResourceManager resourceMan;

		// Token: 0x0400003A RID: 58
		private static CultureInfo resourceCulture;
	}
}
