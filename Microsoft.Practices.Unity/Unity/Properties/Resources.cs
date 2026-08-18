using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Practices.Unity.Properties
{
	// Token: 0x020000A3 RID: 163
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	internal class Resources
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x000096A6 File Offset: 0x000078A6
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal Resources()
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x000096B0 File Offset: 0x000078B0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("Microsoft.Practices.Unity.Properties.Resources", typeof(Resources).GetTypeInfo().Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x000096F4 File Offset: 0x000078F4
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x000096FB File Offset: 0x000078FB
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009703 File Offset: 0x00007903
		internal static string AmbiguousInjectionConstructor
		{
			get
			{
				return Resources.ResourceManager.GetString("AmbiguousInjectionConstructor", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00009719 File Offset: 0x00007919
		internal static string ArgumentMustNotBeEmpty
		{
			get
			{
				return Resources.ResourceManager.GetString("ArgumentMustNotBeEmpty", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000972F File Offset: 0x0000792F
		internal static string BuildFailedException
		{
			get
			{
				return Resources.ResourceManager.GetString("BuildFailedException", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00009745 File Offset: 0x00007945
		internal static string CannotConstructAbstractClass
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotConstructAbstractClass", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000975B File Offset: 0x0000795B
		internal static string CannotConstructDelegate
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotConstructDelegate", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00009771 File Offset: 0x00007971
		internal static string CannotConstructInterface
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotConstructInterface", Resources.resourceCulture);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00009787 File Offset: 0x00007987
		internal static string CannotExtractTypeFromBuildKey
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotExtractTypeFromBuildKey", Resources.resourceCulture);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000979D File Offset: 0x0000799D
		internal static string CannotInjectGenericMethod
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotInjectGenericMethod", Resources.resourceCulture);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000097B3 File Offset: 0x000079B3
		internal static string CannotInjectIndexer
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotInjectIndexer", Resources.resourceCulture);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000097C9 File Offset: 0x000079C9
		internal static string CannotInjectMethodWithOutParam
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotInjectMethodWithOutParam", Resources.resourceCulture);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000097DF File Offset: 0x000079DF
		internal static string CannotInjectMethodWithOutParams
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotInjectMethodWithOutParams", Resources.resourceCulture);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000097F5 File Offset: 0x000079F5
		internal static string CannotInjectMethodWithRefParams
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotInjectMethodWithRefParams", Resources.resourceCulture);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000980B File Offset: 0x00007A0B
		internal static string CannotInjectOpenGenericMethod
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotInjectOpenGenericMethod", Resources.resourceCulture);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00009821 File Offset: 0x00007A21
		internal static string CannotInjectStaticMethod
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotInjectStaticMethod", Resources.resourceCulture);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00009837 File Offset: 0x00007A37
		internal static string CannotResolveOpenGenericType
		{
			get
			{
				return Resources.ResourceManager.GetString("CannotResolveOpenGenericType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000984D File Offset: 0x00007A4D
		internal static string ConstructorArgumentResolveOperation
		{
			get
			{
				return Resources.ResourceManager.GetString("ConstructorArgumentResolveOperation", Resources.resourceCulture);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00009863 File Offset: 0x00007A63
		internal static string ConstructorParameterResolutionFailed
		{
			get
			{
				return Resources.ResourceManager.GetString("ConstructorParameterResolutionFailed", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00009879 File Offset: 0x00007A79
		internal static string ExceptionNullParameterValue
		{
			get
			{
				return Resources.ResourceManager.GetString("ExceptionNullParameterValue", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000988F File Offset: 0x00007A8F
		internal static string InvokingConstructorOperation
		{
			get
			{
				return Resources.ResourceManager.GetString("InvokingConstructorOperation", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000098A5 File Offset: 0x00007AA5
		internal static string InvokingMethodOperation
		{
			get
			{
				return Resources.ResourceManager.GetString("InvokingMethodOperation", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000098BB File Offset: 0x00007ABB
		internal static string KeyAlreadyPresent
		{
			get
			{
				return Resources.ResourceManager.GetString("KeyAlreadyPresent", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000098D1 File Offset: 0x00007AD1
		internal static string LifetimeManagerInUse
		{
			get
			{
				return Resources.ResourceManager.GetString("LifetimeManagerInUse", Resources.resourceCulture);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000098E7 File Offset: 0x00007AE7
		internal static string MarkerBuildPlanInvoked
		{
			get
			{
				return Resources.ResourceManager.GetString("MarkerBuildPlanInvoked", Resources.resourceCulture);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000098FD File Offset: 0x00007AFD
		internal static string MethodArgumentResolveOperation
		{
			get
			{
				return Resources.ResourceManager.GetString("MethodArgumentResolveOperation", Resources.resourceCulture);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00009913 File Offset: 0x00007B13
		internal static string MethodParameterResolutionFailed
		{
			get
			{
				return Resources.ResourceManager.GetString("MethodParameterResolutionFailed", Resources.resourceCulture);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00009929 File Offset: 0x00007B29
		internal static string MissingDependency
		{
			get
			{
				return Resources.ResourceManager.GetString("MissingDependency", Resources.resourceCulture);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000993F File Offset: 0x00007B3F
		internal static string MultipleInjectionConstructors
		{
			get
			{
				return Resources.ResourceManager.GetString("MultipleInjectionConstructors", Resources.resourceCulture);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00009955 File Offset: 0x00007B55
		internal static string MustHaveOpenGenericType
		{
			get
			{
				return Resources.ResourceManager.GetString("MustHaveOpenGenericType", Resources.resourceCulture);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000996B File Offset: 0x00007B6B
		internal static string MustHaveSameNumberOfGenericArguments
		{
			get
			{
				return Resources.ResourceManager.GetString("MustHaveSameNumberOfGenericArguments", Resources.resourceCulture);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00009981 File Offset: 0x00007B81
		internal static string NoConstructorFound
		{
			get
			{
				return Resources.ResourceManager.GetString("NoConstructorFound", Resources.resourceCulture);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00009997 File Offset: 0x00007B97
		internal static string NoMatchingGenericArgument
		{
			get
			{
				return Resources.ResourceManager.GetString("NoMatchingGenericArgument", Resources.resourceCulture);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000305 RID: 773 RVA: 0x000099AD File Offset: 0x00007BAD
		internal static string NoOperationExceptionReason
		{
			get
			{
				return Resources.ResourceManager.GetString("NoOperationExceptionReason", Resources.resourceCulture);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000306 RID: 774 RVA: 0x000099C3 File Offset: 0x00007BC3
		internal static string NoSuchConstructor
		{
			get
			{
				return Resources.ResourceManager.GetString("NoSuchConstructor", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000099D9 File Offset: 0x00007BD9
		internal static string NoSuchMethod
		{
			get
			{
				return Resources.ResourceManager.GetString("NoSuchMethod", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000308 RID: 776 RVA: 0x000099EF File Offset: 0x00007BEF
		internal static string NoSuchProperty
		{
			get
			{
				return Resources.ResourceManager.GetString("NoSuchProperty", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000309 RID: 777 RVA: 0x00009A05 File Offset: 0x00007C05
		internal static string NotAGenericType
		{
			get
			{
				return Resources.ResourceManager.GetString("NotAGenericType", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00009A1B File Offset: 0x00007C1B
		internal static string NotAnArrayTypeWithRankOne
		{
			get
			{
				return Resources.ResourceManager.GetString("NotAnArrayTypeWithRankOne", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00009A31 File Offset: 0x00007C31
		internal static string OptionalDependenciesMustBeReferenceTypes
		{
			get
			{
				return Resources.ResourceManager.GetString("OptionalDependenciesMustBeReferenceTypes", Resources.resourceCulture);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00009A47 File Offset: 0x00007C47
		internal static string PropertyNotSettable
		{
			get
			{
				return Resources.ResourceManager.GetString("PropertyNotSettable", Resources.resourceCulture);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00009A5D File Offset: 0x00007C5D
		internal static string PropertyTypeMismatch
		{
			get
			{
				return Resources.ResourceManager.GetString("PropertyTypeMismatch", Resources.resourceCulture);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00009A73 File Offset: 0x00007C73
		internal static string PropertyValueResolutionFailed
		{
			get
			{
				return Resources.ResourceManager.GetString("PropertyValueResolutionFailed", Resources.resourceCulture);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00009A89 File Offset: 0x00007C89
		internal static string ProvidedStringArgMustNotBeEmpty
		{
			get
			{
				return Resources.ResourceManager.GetString("ProvidedStringArgMustNotBeEmpty", Resources.resourceCulture);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00009A9F File Offset: 0x00007C9F
		internal static string ResolutionFailed
		{
			get
			{
				return Resources.ResourceManager.GetString("ResolutionFailed", Resources.resourceCulture);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00009AB5 File Offset: 0x00007CB5
		internal static string ResolutionTraceDetail
		{
			get
			{
				return Resources.ResourceManager.GetString("ResolutionTraceDetail", Resources.resourceCulture);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00009ACB File Offset: 0x00007CCB
		internal static string ResolutionWithMappingTraceDetail
		{
			get
			{
				return Resources.ResourceManager.GetString("ResolutionWithMappingTraceDetail", Resources.resourceCulture);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00009AE1 File Offset: 0x00007CE1
		internal static string ResolvingPropertyValueOperation
		{
			get
			{
				return Resources.ResourceManager.GetString("ResolvingPropertyValueOperation", Resources.resourceCulture);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00009AF7 File Offset: 0x00007CF7
		internal static string SelectedConstructorHasRefParameters
		{
			get
			{
				return Resources.ResourceManager.GetString("SelectedConstructorHasRefParameters", Resources.resourceCulture);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00009B0D File Offset: 0x00007D0D
		internal static string SettingPropertyOperation
		{
			get
			{
				return Resources.ResourceManager.GetString("SettingPropertyOperation", Resources.resourceCulture);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00009B23 File Offset: 0x00007D23
		internal static string TypeIsNotConstructable
		{
			get
			{
				return Resources.ResourceManager.GetString("TypeIsNotConstructable", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00009B39 File Offset: 0x00007D39
		internal static string TypesAreNotAssignable
		{
			get
			{
				return Resources.ResourceManager.GetString("TypesAreNotAssignable", Resources.resourceCulture);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00009B4F File Offset: 0x00007D4F
		internal static string UnknownType
		{
			get
			{
				return Resources.ResourceManager.GetString("UnknownType", Resources.resourceCulture);
			}
		}

		// Token: 0x040000AB RID: 171
		private static ResourceManager resourceMan;

		// Token: 0x040000AC RID: 172
		private static CultureInfo resourceCulture;
	}
}
