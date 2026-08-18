using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200004B RID: 75
	internal static class SchemaUtility
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0000A664 File Offset: 0x00008864
		public static XmlQualifiedName EnsureProbeMatchSchema(DiscoveryVersion discoveryVersion, XmlSchemaSet schemaSet)
		{
			if (discoveryVersion == DiscoveryVersion.WSDiscoveryApril2005 || discoveryVersion == DiscoveryVersion.WSDiscoveryCD1)
			{
				EndpointAddressAugust2004.GetSchema(schemaSet);
			}
			else if (discoveryVersion == DiscoveryVersion.WSDiscovery11)
			{
				EndpointAddress10.GetSchema(schemaSet);
			}
			SchemaUtility.SchemaTypes schemaTypes = SchemaUtility.SchemaTypes.ProbeType | SchemaUtility.SchemaTypes.ResolveType;
			SchemaUtility.SchemaElements elementsFound = SchemaUtility.SchemaElements.None;
			XmlSchema xmlSchema = null;
			ICollection collection = schemaSet.Schemas(discoveryVersion.Namespace);
			if (collection == null || collection.Count == 0)
			{
				xmlSchema = SchemaUtility.CreateSchema(discoveryVersion);
				SchemaUtility.AddImport(xmlSchema, discoveryVersion.Implementation.WsaNamespace);
				schemaSet.Add(xmlSchema);
			}
			else
			{
				foreach (object obj in collection)
				{
					XmlSchema xmlSchema2 = (XmlSchema)obj;
					xmlSchema = xmlSchema2;
					if (xmlSchema2.SchemaTypes.Contains(discoveryVersion.Implementation.QualifiedNames.ProbeMatchType))
					{
						schemaTypes |= SchemaUtility.SchemaTypes.ProbeMatchType;
						break;
					}
					SchemaUtility.LocateSchemaTypes(discoveryVersion, xmlSchema2, ref schemaTypes);
					SchemaUtility.LocateSchemaElements(discoveryVersion, xmlSchema2, ref elementsFound);
				}
			}
			if ((schemaTypes & SchemaUtility.SchemaTypes.ProbeMatchType) != SchemaUtility.SchemaTypes.ProbeMatchType)
			{
				SchemaUtility.AddSchemaTypes(discoveryVersion, schemaTypes, xmlSchema);
				SchemaUtility.AddElements(discoveryVersion, elementsFound, xmlSchema);
				schemaSet.Reprocess(xmlSchema);
			}
			return discoveryVersion.Implementation.QualifiedNames.ProbeMatchType;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000A790 File Offset: 0x00008990
		public static XmlQualifiedName EnsureProbeSchema(DiscoveryVersion discoveryVersion, XmlSchemaSet schemaSet)
		{
			SchemaUtility.SchemaTypes schemaTypes = SchemaUtility.SchemaTypes.ProbeMatchType | SchemaUtility.SchemaTypes.ResolveType;
			SchemaUtility.SchemaElements elementsFound = SchemaUtility.SchemaElements.XAddrs | SchemaUtility.SchemaElements.MetadataVersion;
			XmlSchema xmlSchema = null;
			ICollection collection = schemaSet.Schemas(discoveryVersion.Namespace);
			if (collection == null || collection.Count == 0)
			{
				xmlSchema = SchemaUtility.CreateSchema(discoveryVersion);
				schemaSet.Add(xmlSchema);
			}
			else
			{
				foreach (object obj in collection)
				{
					XmlSchema xmlSchema2 = (XmlSchema)obj;
					xmlSchema = xmlSchema2;
					if (xmlSchema2.SchemaTypes.Contains(discoveryVersion.Implementation.QualifiedNames.ProbeType))
					{
						schemaTypes |= SchemaUtility.SchemaTypes.ProbeType;
						break;
					}
					SchemaUtility.LocateSchemaTypes(discoveryVersion, xmlSchema2, ref schemaTypes);
					SchemaUtility.LocateSchemaElements(discoveryVersion, xmlSchema2, ref elementsFound);
				}
			}
			if ((schemaTypes & SchemaUtility.SchemaTypes.ProbeType) != SchemaUtility.SchemaTypes.ProbeType)
			{
				SchemaUtility.AddSchemaTypes(discoveryVersion, schemaTypes, xmlSchema);
				SchemaUtility.AddElements(discoveryVersion, elementsFound, xmlSchema);
				schemaSet.Reprocess(xmlSchema);
			}
			return discoveryVersion.Implementation.QualifiedNames.ProbeType;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000A880 File Offset: 0x00008A80
		public static XmlQualifiedName EnsureResolveSchema(DiscoveryVersion discoveryVersion, XmlSchemaSet schemaSet)
		{
			SchemaUtility.SchemaTypes schemaTypes = SchemaUtility.SchemaTypes.QNameListType | SchemaUtility.SchemaTypes.UriListType | SchemaUtility.SchemaTypes.ScopesType | SchemaUtility.SchemaTypes.ProbeType | SchemaUtility.SchemaTypes.ProbeMatchType;
			if (discoveryVersion == DiscoveryVersion.WSDiscoveryApril2005 || discoveryVersion == DiscoveryVersion.WSDiscoveryCD1)
			{
				EndpointAddressAugust2004.GetSchema(schemaSet);
			}
			else if (discoveryVersion == DiscoveryVersion.WSDiscovery11)
			{
				EndpointAddress10.GetSchema(schemaSet);
			}
			XmlSchema xmlSchema = null;
			ICollection collection = schemaSet.Schemas(discoveryVersion.Namespace);
			if (collection == null || collection.Count == 0)
			{
				xmlSchema = SchemaUtility.CreateSchema(discoveryVersion);
				SchemaUtility.AddImport(xmlSchema, discoveryVersion.Implementation.WsaNamespace);
				schemaSet.Add(xmlSchema);
			}
			else
			{
				foreach (object obj in collection)
				{
					XmlSchema xmlSchema2 = (XmlSchema)obj;
					xmlSchema = xmlSchema2;
					if (xmlSchema2.SchemaTypes.Contains(discoveryVersion.Implementation.QualifiedNames.ResolveType))
					{
						schemaTypes |= SchemaUtility.SchemaTypes.ResolveType;
						break;
					}
				}
			}
			if ((schemaTypes & SchemaUtility.SchemaTypes.ResolveType) != SchemaUtility.SchemaTypes.ResolveType)
			{
				SchemaUtility.AddSchemaTypes(discoveryVersion, schemaTypes, xmlSchema);
				schemaSet.Reprocess(xmlSchema);
			}
			return discoveryVersion.Implementation.QualifiedNames.ResolveType;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000A988 File Offset: 0x00008B88
		public static XmlQualifiedName EnsureAppSequenceSchema(DiscoveryVersion discoveryVersion, XmlSchemaSet schemaSet)
		{
			bool flag = true;
			XmlSchema schema = null;
			ICollection collection = schemaSet.Schemas(discoveryVersion.Namespace);
			if (collection == null || collection.Count == 0)
			{
				schema = SchemaUtility.CreateSchema(discoveryVersion);
				schemaSet.Add(schema);
			}
			else
			{
				foreach (object obj in collection)
				{
					XmlSchema xmlSchema = (XmlSchema)obj;
					schema = xmlSchema;
					if (xmlSchema.SchemaTypes.Contains(discoveryVersion.Implementation.QualifiedNames.AppSequenceType))
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				SchemaUtility.AddAppSequenceType(discoveryVersion, schema);
				schemaSet.Reprocess(schema);
			}
			return discoveryVersion.Implementation.QualifiedNames.AppSequenceType;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000AA50 File Offset: 0x00008C50
		private static void AddElements(DiscoveryVersion discoveryVersion, SchemaUtility.SchemaElements elementsFound, XmlSchema discoverySchema)
		{
			if ((elementsFound & SchemaUtility.SchemaElements.Types) == SchemaUtility.SchemaElements.None)
			{
				SchemaUtility.AddTypesElement(discoveryVersion, discoverySchema);
			}
			if ((elementsFound & SchemaUtility.SchemaElements.Scopes) == SchemaUtility.SchemaElements.None)
			{
				SchemaUtility.AddScopesElement(discoveryVersion, discoverySchema);
			}
			if ((elementsFound & SchemaUtility.SchemaElements.XAddrs) == SchemaUtility.SchemaElements.None)
			{
				SchemaUtility.AddXAddrsElement(discoveryVersion, discoverySchema);
			}
			if ((elementsFound & SchemaUtility.SchemaElements.MetadataVersion) == SchemaUtility.SchemaElements.None)
			{
				SchemaUtility.AddMetadataVersionSchemaElement(discoveryVersion, discoverySchema);
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000AA84 File Offset: 0x00008C84
		private static void AddAppSequenceType(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = "AppSequenceType";
			XmlSchemaComplexContent xmlSchemaComplexContent = new XmlSchemaComplexContent();
			xmlSchemaComplexType.ContentModel = xmlSchemaComplexContent;
			XmlSchemaComplexContentRestriction xmlSchemaComplexContentRestriction = new XmlSchemaComplexContentRestriction();
			xmlSchemaComplexContent.Content = xmlSchemaComplexContentRestriction;
			xmlSchemaComplexContentRestriction.BaseTypeName = discoveryVersion.Implementation.QualifiedNames.AnyType;
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = "InstanceId";
			xmlSchemaAttribute.SchemaTypeName = discoveryVersion.Implementation.QualifiedNames.UnsignedIntType;
			xmlSchemaAttribute.Use = XmlSchemaUse.Required;
			XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
			xmlSchemaAttribute2.Name = "SequenceId";
			xmlSchemaAttribute2.SchemaTypeName = discoveryVersion.Implementation.QualifiedNames.AnyUriType;
			XmlSchemaAttribute xmlSchemaAttribute3 = new XmlSchemaAttribute();
			xmlSchemaAttribute3.Name = "MessageNumber";
			xmlSchemaAttribute3.SchemaTypeName = discoveryVersion.Implementation.QualifiedNames.UnsignedIntType;
			xmlSchemaAttribute3.Use = XmlSchemaUse.Required;
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.Namespace = "##other";
			xmlSchemaAnyAttribute.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaComplexContentRestriction.Attributes.Add(xmlSchemaAttribute);
			xmlSchemaComplexContentRestriction.Attributes.Add(xmlSchemaAttribute2);
			xmlSchemaComplexContentRestriction.Attributes.Add(xmlSchemaAttribute3);
			xmlSchemaComplexContentRestriction.AnyAttribute = xmlSchemaAnyAttribute;
			schema.Items.Add(xmlSchemaComplexType);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		private static void AddImport(XmlSchema schema, string importNamespace)
		{
			XmlSchemaImport xmlSchemaImport = new XmlSchemaImport();
			xmlSchemaImport.Namespace = importNamespace;
			schema.Includes.Add(xmlSchemaImport);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000ABE0 File Offset: 0x00008DE0
		private static void AddMetadataVersionSchemaElement(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = "MetadataVersion";
			xmlSchemaElement.SchemaTypeName = discoveryVersion.Implementation.QualifiedNames.UnsignedIntType;
			schema.Items.Add(xmlSchemaElement);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000AC24 File Offset: 0x00008E24
		private static void AddResolveType(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = "ResolveType";
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.RefName = discoveryVersion.Implementation.QualifiedNames.EprElement;
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "##other";
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaSequence.Items.Add(xmlSchemaElement);
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.Namespace = "##other";
			xmlSchemaAnyAttribute.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			xmlSchemaComplexType.AnyAttribute = xmlSchemaAnyAttribute;
			schema.Items.Add(xmlSchemaComplexType);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		private static void AddProbeMatchType(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = "ProbeMatchType";
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.RefName = discoveryVersion.Implementation.QualifiedNames.EprElement;
			XmlSchemaElement xmlSchemaElement2 = new XmlSchemaElement();
			xmlSchemaElement2.RefName = discoveryVersion.Implementation.QualifiedNames.TypesElement;
			xmlSchemaElement2.MinOccurs = 0m;
			XmlSchemaElement xmlSchemaElement3 = new XmlSchemaElement();
			xmlSchemaElement3.RefName = discoveryVersion.Implementation.QualifiedNames.ScopesElement;
			xmlSchemaElement3.MinOccurs = 0m;
			XmlSchemaElement xmlSchemaElement4 = new XmlSchemaElement();
			xmlSchemaElement4.RefName = discoveryVersion.Implementation.QualifiedNames.XAddrsElement;
			xmlSchemaElement4.MinOccurs = 0m;
			XmlSchemaElement xmlSchemaElement5 = new XmlSchemaElement();
			xmlSchemaElement5.RefName = discoveryVersion.Implementation.QualifiedNames.MetadataVersionElement;
			xmlSchemaElement5.MinOccurs = 0m;
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "##other";
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaSequence.Items.Add(xmlSchemaElement);
			xmlSchemaSequence.Items.Add(xmlSchemaElement2);
			xmlSchemaSequence.Items.Add(xmlSchemaElement3);
			xmlSchemaSequence.Items.Add(xmlSchemaElement4);
			xmlSchemaSequence.Items.Add(xmlSchemaElement5);
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.Namespace = "##other";
			xmlSchemaAnyAttribute.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			xmlSchemaComplexType.AnyAttribute = xmlSchemaAnyAttribute;
			schema.Items.Add(xmlSchemaComplexType);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000AE90 File Offset: 0x00009090
		private static void AddProbeType(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = "ProbeType";
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.RefName = discoveryVersion.Implementation.QualifiedNames.TypesElement;
			xmlSchemaElement.MinOccurs = 0m;
			XmlSchemaElement xmlSchemaElement2 = new XmlSchemaElement();
			xmlSchemaElement2.RefName = discoveryVersion.Implementation.QualifiedNames.ScopesElement;
			xmlSchemaElement2.MinOccurs = 0m;
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "##other";
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaSequence.Items.Add(xmlSchemaElement);
			xmlSchemaSequence.Items.Add(xmlSchemaElement2);
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.Namespace = "##other";
			xmlSchemaAnyAttribute.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			xmlSchemaComplexType.AnyAttribute = xmlSchemaAnyAttribute;
			schema.Items.Add(xmlSchemaComplexType);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000AF9C File Offset: 0x0000919C
		private static void AddQNameListType(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.Name = "QNameListType";
			xmlSchemaSimpleType.Content = new XmlSchemaSimpleTypeList
			{
				ItemTypeName = discoveryVersion.Implementation.QualifiedNames.QNameType
			};
			schema.Items.Add(xmlSchemaSimpleType);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000AFEC File Offset: 0x000091EC
		private static void AddSchemaTypes(DiscoveryVersion discoveryVersion, SchemaUtility.SchemaTypes typesFound, XmlSchema discoverySchema)
		{
			if ((typesFound & SchemaUtility.SchemaTypes.ProbeMatchType) == SchemaUtility.SchemaTypes.None)
			{
				SchemaUtility.AddProbeMatchType(discoveryVersion, discoverySchema);
			}
			if ((typesFound & SchemaUtility.SchemaTypes.ProbeType) == SchemaUtility.SchemaTypes.None)
			{
				SchemaUtility.AddProbeType(discoveryVersion, discoverySchema);
			}
			if ((typesFound & SchemaUtility.SchemaTypes.ResolveType) == SchemaUtility.SchemaTypes.None)
			{
				SchemaUtility.AddResolveType(discoveryVersion, discoverySchema);
			}
			if ((typesFound & SchemaUtility.SchemaTypes.QNameListType) == SchemaUtility.SchemaTypes.None)
			{
				SchemaUtility.AddQNameListType(discoveryVersion, discoverySchema);
			}
			if ((typesFound & SchemaUtility.SchemaTypes.ScopesType) == SchemaUtility.SchemaTypes.None)
			{
				SchemaUtility.AddScopesType(discoveryVersion, discoverySchema);
			}
			if ((typesFound & SchemaUtility.SchemaTypes.UriListType) == SchemaUtility.SchemaTypes.None)
			{
				SchemaUtility.AddUriListType(discoveryVersion, discoverySchema);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000B044 File Offset: 0x00009244
		private static void AddScopesElement(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = "Scopes";
			xmlSchemaElement.SchemaTypeName = discoveryVersion.Implementation.QualifiedNames.ScopesType;
			schema.Items.Add(xmlSchemaElement);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000B088 File Offset: 0x00009288
		private static void AddScopesType(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			xmlSchemaComplexType.Name = "ScopesType";
			XmlSchemaSimpleContent xmlSchemaSimpleContent = new XmlSchemaSimpleContent();
			XmlSchemaSimpleContentExtension xmlSchemaSimpleContentExtension = new XmlSchemaSimpleContentExtension();
			xmlSchemaSimpleContentExtension.BaseTypeName = discoveryVersion.Implementation.QualifiedNames.UriListType;
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = "MatchBy";
			xmlSchemaAttribute.SchemaTypeName = discoveryVersion.Implementation.QualifiedNames.AnyUriType;
			XmlSchemaAnyAttribute xmlSchemaAnyAttribute = new XmlSchemaAnyAttribute();
			xmlSchemaAnyAttribute.Namespace = "##other";
			xmlSchemaAnyAttribute.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSimpleContentExtension.Attributes.Add(xmlSchemaAttribute);
			xmlSchemaSimpleContentExtension.AnyAttribute = xmlSchemaAnyAttribute;
			xmlSchemaSimpleContent.Content = xmlSchemaSimpleContentExtension;
			xmlSchemaComplexType.ContentModel = xmlSchemaSimpleContent;
			schema.Items.Add(xmlSchemaComplexType);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000B13C File Offset: 0x0000933C
		private static void AddTypesElement(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = "Types";
			xmlSchemaElement.SchemaTypeName = discoveryVersion.Implementation.QualifiedNames.QNameListType;
			schema.Items.Add(xmlSchemaElement);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000B180 File Offset: 0x00009380
		private static void AddUriListType(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaSimpleType xmlSchemaSimpleType = new XmlSchemaSimpleType();
			xmlSchemaSimpleType.Name = "UriListType";
			xmlSchemaSimpleType.Content = new XmlSchemaSimpleTypeList
			{
				ItemTypeName = discoveryVersion.Implementation.QualifiedNames.AnyUriType
			};
			schema.Items.Add(xmlSchemaSimpleType);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000B1D0 File Offset: 0x000093D0
		private static void AddXAddrsElement(DiscoveryVersion discoveryVersion, XmlSchema schema)
		{
			XmlSchemaElement xmlSchemaElement = new XmlSchemaElement();
			xmlSchemaElement.Name = "XAddrs";
			xmlSchemaElement.SchemaTypeName = discoveryVersion.Implementation.QualifiedNames.UriListType;
			schema.Items.Add(xmlSchemaElement);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000B214 File Offset: 0x00009414
		private static XmlSchema CreateSchema(DiscoveryVersion discoveryVersion)
		{
			XmlSchema xmlSchema = new XmlSchema();
			xmlSchema.TargetNamespace = discoveryVersion.Namespace;
			xmlSchema.Namespaces.Add("tns", discoveryVersion.Namespace);
			xmlSchema.ElementFormDefault = XmlSchemaForm.Qualified;
			xmlSchema.BlockDefault = XmlSchemaDerivationMethod.All;
			return xmlSchema;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000B25C File Offset: 0x0000945C
		private static void LocateSchemaElements(DiscoveryVersion discoveryVersion, XmlSchema schema, ref SchemaUtility.SchemaElements elementsFound)
		{
			if ((elementsFound & SchemaUtility.SchemaElements.Types) != SchemaUtility.SchemaElements.Types && schema.Elements.Contains(discoveryVersion.Implementation.QualifiedNames.TypesElement))
			{
				elementsFound |= SchemaUtility.SchemaElements.Types;
			}
			if ((elementsFound & SchemaUtility.SchemaElements.Scopes) != SchemaUtility.SchemaElements.Scopes && schema.Elements.Contains(discoveryVersion.Implementation.QualifiedNames.ScopesElement))
			{
				elementsFound |= SchemaUtility.SchemaElements.Scopes;
			}
			if ((elementsFound & SchemaUtility.SchemaElements.XAddrs) != SchemaUtility.SchemaElements.XAddrs && schema.Elements.Contains(discoveryVersion.Implementation.QualifiedNames.XAddrsElement))
			{
				elementsFound |= SchemaUtility.SchemaElements.XAddrs;
			}
			if ((elementsFound & SchemaUtility.SchemaElements.MetadataVersion) != SchemaUtility.SchemaElements.MetadataVersion && schema.Elements.Contains(discoveryVersion.Implementation.QualifiedNames.MetadataVersionElement))
			{
				elementsFound |= SchemaUtility.SchemaElements.MetadataVersion;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000B314 File Offset: 0x00009514
		private static void LocateSchemaTypes(DiscoveryVersion discoveryVersion, XmlSchema schema, ref SchemaUtility.SchemaTypes typesFound)
		{
			if ((typesFound & SchemaUtility.SchemaTypes.QNameListType) != SchemaUtility.SchemaTypes.QNameListType && schema.SchemaTypes.Contains(discoveryVersion.Implementation.QualifiedNames.QNameListType))
			{
				typesFound |= SchemaUtility.SchemaTypes.QNameListType;
			}
			if ((typesFound & SchemaUtility.SchemaTypes.UriListType) != SchemaUtility.SchemaTypes.UriListType && schema.SchemaTypes.Contains(discoveryVersion.Implementation.QualifiedNames.UriListType))
			{
				typesFound |= SchemaUtility.SchemaTypes.UriListType;
			}
			if ((typesFound & SchemaUtility.SchemaTypes.ScopesType) != SchemaUtility.SchemaTypes.ScopesType && schema.SchemaTypes.Contains(discoveryVersion.Implementation.QualifiedNames.ScopesType))
			{
				typesFound |= SchemaUtility.SchemaTypes.ScopesType;
			}
		}

		// Token: 0x020000EA RID: 234
		[Flags]
		private enum SchemaTypes
		{
			// Token: 0x04000289 RID: 649
			None = 0,
			// Token: 0x0400028A RID: 650
			QNameListType = 1,
			// Token: 0x0400028B RID: 651
			UriListType = 2,
			// Token: 0x0400028C RID: 652
			ScopesType = 4,
			// Token: 0x0400028D RID: 653
			ProbeType = 8,
			// Token: 0x0400028E RID: 654
			ProbeMatchType = 16,
			// Token: 0x0400028F RID: 655
			ResolveType = 32
		}

		// Token: 0x020000EB RID: 235
		[Flags]
		private enum SchemaElements
		{
			// Token: 0x04000291 RID: 657
			None = 0,
			// Token: 0x04000292 RID: 658
			Scopes = 1,
			// Token: 0x04000293 RID: 659
			Types = 2,
			// Token: 0x04000294 RID: 660
			XAddrs = 4,
			// Token: 0x04000295 RID: 661
			MetadataVersion = 8
		}
	}
}
