using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Practices.Unity.Configuration.Properties
{
	// Token: 0x02000034 RID: 52
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class Resources
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00005DDC File Offset: 0x00003FDC
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal Resources()
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00005DE4 File Offset: 0x00003FE4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.Practices.Unity.Configuration.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00005E23 File Offset: 0x00004023
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00005E2A File Offset: 0x0000402A
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00005E32 File Offset: 0x00004032
		internal static string CannotCreateContainerConfiguringElement
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotCreateContainerConfiguringElement", Resources.resourceCulture);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00005E48 File Offset: 0x00004048
		internal static string CannotCreateExtensionConfigurationElement
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotCreateExtensionConfigurationElement", Resources.resourceCulture);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00005E5E File Offset: 0x0000405E
		internal static string CannotCreateInjectionMemberElement
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotCreateInjectionMemberElement", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00005E74 File Offset: 0x00004074
		internal static string CannotCreateParameterValueElement
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotCreateParameterValueElement", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00005E8A File Offset: 0x0000408A
		internal static string CouldNotResolveType
		{
			get
			{
				return Resources.ResourceManager.GetString("CouldNotResolveType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00005EA0 File Offset: 0x000040A0
		internal static string DependencyForGenericParameterWithTypeSet
		{
			get
			{
				return Resources.ResourceManager.GetString("DependencyForGenericParameterWithTypeSet", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00005EB6 File Offset: 0x000040B6
		internal static string DependencyForOptionalGenericParameterWithTypeSet
		{
			get
			{
				return Resources.ResourceManager.GetString("DependencyForOptionalGenericParameterWithTypeSet", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00005ECC File Offset: 0x000040CC
		internal static string DestinationNameFormat
		{
			get
			{
				return Resources.ResourceManager.GetString("DestinationNameFormat", Resources.resourceCulture);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00005EE2 File Offset: 0x000040E2
		internal static string DuplicateParameterValueElement
		{
			get
			{
				return Resources.ResourceManager.GetString("DuplicateParameterValueElement", Resources.resourceCulture);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00005EF8 File Offset: 0x000040F8
		internal static string ElementTypeNotRegistered
		{
			get
			{
				return Resources.ResourceManager.GetString("ElementTypeNotRegistered", Resources.resourceCulture);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00005F0E File Offset: 0x0000410E
		internal static string ElementWithAttributesAndParameterValueElements
		{
			get
			{
				return Resources.ResourceManager.GetString("ElementWithAttributesAndParameterValueElements", Resources.resourceCulture);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005F24 File Offset: 0x00004124
		internal static string ExtensionTypeNotFound
		{
			get
			{
				return Resources.ResourceManager.GetString("ExtensionTypeNotFound", Resources.resourceCulture);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005F3A File Offset: 0x0000413A
		internal static string ExtensionTypeNotValid
		{
			get
			{
				return Resources.ResourceManager.GetString("ExtensionTypeNotValid", Resources.resourceCulture);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00005F50 File Offset: 0x00004150
		internal static string InvalidExtensionElementType
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidExtensionElementType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00005F66 File Offset: 0x00004166
		internal static string InvalidValueAttributes
		{
			get
			{
				return Resources.ResourceManager.GetString("InvalidValueAttributes", Resources.resourceCulture);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00005F7C File Offset: 0x0000417C
		internal static string NoMatchingConstructor
		{
			get
			{
				return Resources.ResourceManager.GetString("NoMatchingConstructor", Resources.resourceCulture);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00005F92 File Offset: 0x00004192
		internal static string NoMatchingMethod
		{
			get
			{
				return Resources.ResourceManager.GetString("NoMatchingMethod", Resources.resourceCulture);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00005FA8 File Offset: 0x000041A8
		internal static string NoSuchContainer
		{
			get
			{
				return Resources.ResourceManager.GetString("NoSuchContainer", Resources.resourceCulture);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00005FBE File Offset: 0x000041BE
		internal static string NoSuchProperty
		{
			get
			{
				return Resources.ResourceManager.GetString("NoSuchProperty", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00005FD4 File Offset: 0x000041D4
		internal static string NotAnArray
		{
			get
			{
				return Resources.ResourceManager.GetString("NotAnArray", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00005FEA File Offset: 0x000041EA
		internal static string Parameter
		{
			get
			{
				return Resources.ResourceManager.GetString("Parameter", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00006000 File Offset: 0x00004200
		internal static string Property
		{
			get
			{
				return Resources.ResourceManager.GetString("Property", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00006016 File Offset: 0x00004216
		internal static string RequiredPropertyMissing
		{
			get
			{
				return Resources.ResourceManager.GetString("RequiredPropertyMissing", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x0000602C File Offset: 0x0000422C
		internal static string ValueNotAllowedForGenericArrayType
		{
			get
			{
				return Resources.ResourceManager.GetString("ValueNotAllowedForGenericArrayType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006042 File Offset: 0x00004242
		internal static string ValueNotAllowedForGenericParameterType
		{
			get
			{
				return Resources.ResourceManager.GetString("ValueNotAllowedForGenericParameterType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006058 File Offset: 0x00004258
		internal static string ValueNotAllowedForOpenGenericType
		{
			get
			{
				return Resources.ResourceManager.GetString("ValueNotAllowedForOpenGenericType", Resources.resourceCulture);
			}
		}

		// Token: 0x0400005B RID: 91
		private static ResourceManager resourceMan;

		// Token: 0x0400005C RID: 92
		private static CultureInfo resourceCulture;
	}
}
