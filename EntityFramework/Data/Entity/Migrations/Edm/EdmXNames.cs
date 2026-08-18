using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace System.Data.Entity.Migrations.Edm
{
	// Token: 0x020006E3 RID: 1763
	internal static class EdmXNames
	{
		// Token: 0x060046E1 RID: 18145 RVA: 0x0014FEB7 File Offset: 0x0014E0B7
		public static string ActionAttribute(this XElement element)
		{
			return (string)element.Attribute("Action");
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x0014FECE File Offset: 0x0014E0CE
		public static string ColumnNameAttribute(this XElement element)
		{
			return (string)element.Attribute("ColumnName");
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x0014FEE5 File Offset: 0x0014E0E5
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static string EntitySetAttribute(this XElement element)
		{
			return (string)element.Attribute("EntitySet");
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x0014FEFC File Offset: 0x0014E0FC
		public static string NameAttribute(this XElement element)
		{
			return (string)element.Attribute("Name");
		}

		// Token: 0x060046E5 RID: 18149 RVA: 0x0014FF13 File Offset: 0x0014E113
		public static string NamespaceAttribute(this XElement element)
		{
			return (string)element.Attribute("Namespace");
		}

		// Token: 0x060046E6 RID: 18150 RVA: 0x0014FF2A File Offset: 0x0014E12A
		public static string EntityTypeAttribute(this XElement element)
		{
			return (string)element.Attribute("EntityType");
		}

		// Token: 0x060046E7 RID: 18151 RVA: 0x0014FF41 File Offset: 0x0014E141
		public static string FromRoleAttribute(this XElement element)
		{
			return (string)element.Attribute("FromRole");
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x0014FF58 File Offset: 0x0014E158
		public static string ToRoleAttribute(this XElement element)
		{
			return (string)element.Attribute("ToRole");
		}

		// Token: 0x060046E9 RID: 18153 RVA: 0x0014FF6F File Offset: 0x0014E16F
		public static string NullableAttribute(this XElement element)
		{
			return (string)element.Attribute("Nullable");
		}

		// Token: 0x060046EA RID: 18154 RVA: 0x0014FF86 File Offset: 0x0014E186
		public static string MaxLengthAttribute(this XElement element)
		{
			return (string)element.Attribute("MaxLength");
		}

		// Token: 0x060046EB RID: 18155 RVA: 0x0014FF9D File Offset: 0x0014E19D
		public static string MultiplicityAttribute(this XElement element)
		{
			return (string)element.Attribute("Multiplicity");
		}

		// Token: 0x060046EC RID: 18156 RVA: 0x0014FFB4 File Offset: 0x0014E1B4
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static string FixedLengthAttribute(this XElement element)
		{
			return (string)element.Attribute("FixedLength");
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x0014FFCB File Offset: 0x0014E1CB
		public static string PrecisionAttribute(this XElement element)
		{
			return (string)element.Attribute("Precision");
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x0014FFE2 File Offset: 0x0014E1E2
		public static string ProviderAttribute(this XElement element)
		{
			return (string)element.Attribute("Provider");
		}

		// Token: 0x060046EF RID: 18159 RVA: 0x0014FFF9 File Offset: 0x0014E1F9
		public static string ProviderManifestTokenAttribute(this XElement element)
		{
			return (string)element.Attribute("ProviderManifestToken");
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x00150010 File Offset: 0x0014E210
		public static string RelationshipAttribute(this XElement element)
		{
			return (string)element.Attribute("Relationship");
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x00150027 File Offset: 0x0014E227
		public static string ScaleAttribute(this XElement element)
		{
			return (string)element.Attribute("Scale");
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x0015003E File Offset: 0x0014E23E
		public static string StoreGeneratedPatternAttribute(this XElement element)
		{
			return (string)element.Attribute("StoreGeneratedPattern");
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x00150055 File Offset: 0x0014E255
		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		public static string UnicodeAttribute(this XElement element)
		{
			return (string)element.Attribute("Unicode");
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x0015006C File Offset: 0x0014E26C
		public static string RoleAttribute(this XElement element)
		{
			return (string)element.Attribute("Role");
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x00150083 File Offset: 0x0014E283
		public static string SchemaAttribute(this XElement element)
		{
			return (string)element.Attribute("Schema");
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x0015009A File Offset: 0x0014E29A
		public static string StoreEntitySetAttribute(this XElement element)
		{
			return (string)element.Attribute("StoreEntitySet");
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x001500B1 File Offset: 0x0014E2B1
		public static string TableAttribute(this XElement element)
		{
			return (string)element.Attribute("Table");
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x001500C8 File Offset: 0x0014E2C8
		public static string TypeAttribute(this XElement element)
		{
			return (string)element.Attribute("Type");
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x001500DF File Offset: 0x0014E2DF
		public static string TypeNameAttribute(this XElement element)
		{
			return (string)element.Attribute("TypeName");
		}

		// Token: 0x060046FA RID: 18170 RVA: 0x001500F6 File Offset: 0x0014E2F6
		public static string ValueAttribute(this XElement element)
		{
			return (string)element.Attribute("Value");
		}

		// Token: 0x040019F0 RID: 6640
		private static readonly XNamespace _csdlNamespaceV2 = XNamespace.Get("http://schemas.microsoft.com/ado/2008/09/edm");

		// Token: 0x040019F1 RID: 6641
		private static readonly XNamespace _mslNamespaceV2 = XNamespace.Get("http://schemas.microsoft.com/ado/2008/09/mapping/cs");

		// Token: 0x040019F2 RID: 6642
		private static readonly XNamespace _ssdlNamespaceV2 = XNamespace.Get("http://schemas.microsoft.com/ado/2009/02/edm/ssdl");

		// Token: 0x040019F3 RID: 6643
		private static readonly XNamespace _csdlNamespaceV3 = XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/edm");

		// Token: 0x040019F4 RID: 6644
		private static readonly XNamespace _mslNamespaceV3 = XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/mapping/cs");

		// Token: 0x040019F5 RID: 6645
		private static readonly XNamespace _ssdlNamespaceV3 = XNamespace.Get("http://schemas.microsoft.com/ado/2009/11/edm/ssdl");

		// Token: 0x020006E4 RID: 1764
		public static class Csdl
		{
			// Token: 0x060046FC RID: 18172 RVA: 0x00150178 File Offset: 0x0014E378
			private static IEnumerable<XName> Names(string elementName)
			{
				return new List<XName>
				{
					EdmXNames._csdlNamespaceV3 + elementName,
					EdmXNames._csdlNamespaceV2 + elementName
				};
			}

			// Token: 0x040019F6 RID: 6646
			public static readonly IEnumerable<XName> AssociationNames = EdmXNames.Csdl.Names("Association");

			// Token: 0x040019F7 RID: 6647
			public static readonly IEnumerable<XName> ComplexTypeNames = EdmXNames.Csdl.Names("ComplexType");

			// Token: 0x040019F8 RID: 6648
			public static readonly IEnumerable<XName> EndNames = EdmXNames.Csdl.Names("End");

			// Token: 0x040019F9 RID: 6649
			public static readonly IEnumerable<XName> EntityContainerNames = EdmXNames.Csdl.Names("EntityContainer");

			// Token: 0x040019FA RID: 6650
			public static readonly IEnumerable<XName> EntitySetNames = EdmXNames.Csdl.Names("EntitySet");

			// Token: 0x040019FB RID: 6651
			public static readonly IEnumerable<XName> EntityTypeNames = EdmXNames.Csdl.Names("EntityType");

			// Token: 0x040019FC RID: 6652
			public static readonly IEnumerable<XName> NavigationPropertyNames = EdmXNames.Csdl.Names("NavigationProperty");

			// Token: 0x040019FD RID: 6653
			public static readonly IEnumerable<XName> PropertyNames = EdmXNames.Csdl.Names("Property");

			// Token: 0x040019FE RID: 6654
			public static readonly IEnumerable<XName> SchemaNames = EdmXNames.Csdl.Names("Schema");
		}

		// Token: 0x020006E5 RID: 1765
		public static class Msl
		{
			// Token: 0x060046FE RID: 18174 RVA: 0x00150244 File Offset: 0x0014E444
			private static IEnumerable<XName> Names(string elementName)
			{
				return new List<XName>
				{
					EdmXNames._mslNamespaceV3 + elementName,
					EdmXNames._mslNamespaceV2 + elementName
				};
			}

			// Token: 0x040019FF RID: 6655
			public static readonly IEnumerable<XName> AssociationSetMappingNames = EdmXNames.Msl.Names("AssociationSetMapping");

			// Token: 0x04001A00 RID: 6656
			public static readonly IEnumerable<XName> ComplexPropertyNames = EdmXNames.Msl.Names("ComplexProperty");

			// Token: 0x04001A01 RID: 6657
			public static readonly IEnumerable<XName> ConditionNames = EdmXNames.Msl.Names("Condition");

			// Token: 0x04001A02 RID: 6658
			public static readonly IEnumerable<XName> EntityContainerMappingNames = EdmXNames.Msl.Names("EntityContainerMapping");

			// Token: 0x04001A03 RID: 6659
			public static readonly IEnumerable<XName> EntitySetMappingNames = EdmXNames.Msl.Names("EntitySetMapping");

			// Token: 0x04001A04 RID: 6660
			public static readonly IEnumerable<XName> EntityTypeMappingNames = EdmXNames.Msl.Names("EntityTypeMapping");

			// Token: 0x04001A05 RID: 6661
			public static readonly IEnumerable<XName> MappingNames = EdmXNames.Msl.Names("Mapping");

			// Token: 0x04001A06 RID: 6662
			public static readonly IEnumerable<XName> MappingFragmentNames = EdmXNames.Msl.Names("MappingFragment");

			// Token: 0x04001A07 RID: 6663
			public static readonly IEnumerable<XName> ScalarPropertyNames = EdmXNames.Msl.Names("ScalarProperty");
		}

		// Token: 0x020006E6 RID: 1766
		public static class Ssdl
		{
			// Token: 0x06004700 RID: 18176 RVA: 0x00150310 File Offset: 0x0014E510
			private static IEnumerable<XName> Names(string elementName)
			{
				return new List<XName>
				{
					EdmXNames._ssdlNamespaceV3 + elementName,
					EdmXNames._ssdlNamespaceV2 + elementName
				};
			}

			// Token: 0x04001A08 RID: 6664
			public static readonly IEnumerable<XName> AssociationNames = EdmXNames.Ssdl.Names("Association");

			// Token: 0x04001A09 RID: 6665
			public static readonly IEnumerable<XName> DependentNames = EdmXNames.Ssdl.Names("Dependent");

			// Token: 0x04001A0A RID: 6666
			public static readonly IEnumerable<XName> EndNames = EdmXNames.Ssdl.Names("End");

			// Token: 0x04001A0B RID: 6667
			public static readonly IEnumerable<XName> EntityContainerNames = EdmXNames.Ssdl.Names("EntityContainer");

			// Token: 0x04001A0C RID: 6668
			public static readonly IEnumerable<XName> EntitySetNames = EdmXNames.Ssdl.Names("EntitySet");

			// Token: 0x04001A0D RID: 6669
			public static readonly IEnumerable<XName> EntityTypeNames = EdmXNames.Ssdl.Names("EntityType");

			// Token: 0x04001A0E RID: 6670
			public static readonly IEnumerable<XName> KeyNames = EdmXNames.Ssdl.Names("Key");

			// Token: 0x04001A0F RID: 6671
			public static readonly IEnumerable<XName> OnDeleteNames = EdmXNames.Ssdl.Names("OnDelete");

			// Token: 0x04001A10 RID: 6672
			public static readonly IEnumerable<XName> PrincipalNames = EdmXNames.Ssdl.Names("Principal");

			// Token: 0x04001A11 RID: 6673
			public static readonly IEnumerable<XName> PropertyNames = EdmXNames.Ssdl.Names("Property");

			// Token: 0x04001A12 RID: 6674
			public static readonly IEnumerable<XName> PropertyRefNames = EdmXNames.Ssdl.Names("PropertyRef");

			// Token: 0x04001A13 RID: 6675
			public static readonly IEnumerable<XName> SchemaNames = EdmXNames.Ssdl.Names("Schema");
		}
	}
}
