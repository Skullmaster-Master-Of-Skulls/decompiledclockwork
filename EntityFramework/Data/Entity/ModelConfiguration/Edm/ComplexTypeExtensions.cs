using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000814 RID: 2068
	internal static class ComplexTypeExtensions
	{
		// Token: 0x06005CF6 RID: 23798 RVA: 0x001913B4 File Offset: 0x0018F5B4
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by test code.")]
		public static EdmProperty AddComplexProperty(this ComplexType complexType, string name, ComplexType targetComplexType)
		{
			EdmProperty edmProperty = EdmProperty.CreateComplex(name, targetComplexType);
			complexType.AddMember(edmProperty);
			return edmProperty;
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x001913D1 File Offset: 0x0018F5D1
		public static object GetConfiguration(this ComplexType complexType)
		{
			return complexType.Annotations.GetConfiguration();
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x001913DE File Offset: 0x0018F5DE
		public static Type GetClrType(this ComplexType complexType)
		{
			return complexType.Annotations.GetClrType();
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x001913EB File Offset: 0x0018F5EB
		internal static IEnumerable<ComplexType> ToHierarchy(this ComplexType edmType)
		{
			return EdmType.SafeTraverseHierarchy<ComplexType>(edmType);
		}
	}
}
