using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.SvcMapFileXmlSerializer
{
	// Token: 0x02000029 RID: 41
	internal class XmlSerializationWriterSvcMapFileImpl : XmlSerializationWriter
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00007F8F File Offset: 0x0000618F
		public void Write16_ReferenceGroup(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("ReferenceGroup", "urn:schemas-microsoft-com:xml-wcfservicemap");
				return;
			}
			base.TopLevelElement();
			this.Write15_SvcMapFileImpl("ReferenceGroup", "urn:schemas-microsoft-com:xml-wcfservicemap", (SvcMapFileImpl)o, true, false);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007FCC File Offset: 0x000061CC
		private void Write15_SvcMapFileImpl(string n, string ns, SvcMapFileImpl o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(SvcMapFileImpl)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("SvcMapFileImpl", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("ID", "", o.ID);
			this.Write9_ClientOptions("ClientOptions", "urn:schemas-microsoft-com:xml-wcfservicemap", o.ClientOptions, false, false);
			List<MetadataSource> metadataSourceList = o.MetadataSourceList;
			if (metadataSourceList != null)
			{
				base.WriteStartElement("MetadataSources", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int i = 0; i < ((ICollection)metadataSourceList).Count; i++)
				{
					this.Write10_MetadataSource("MetadataSource", "urn:schemas-microsoft-com:xml-wcfservicemap", metadataSourceList[i], true, false);
				}
				base.WriteEndElement();
			}
			List<MetadataFile> metadataList = o.MetadataList;
			if (metadataList != null)
			{
				base.WriteStartElement("Metadata", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int j = 0; j < ((ICollection)metadataList).Count; j++)
				{
					this.Write13_MetadataFile("MetadataFile", "urn:schemas-microsoft-com:xml-wcfservicemap", metadataList[j], true, false);
				}
				base.WriteEndElement();
			}
			List<ExtensionFile> extensions = o.Extensions;
			if (extensions != null)
			{
				base.WriteStartElement("Extensions", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int k = 0; k < ((ICollection)extensions).Count; k++)
				{
					this.Write14_ExtensionFile("ExtensionFile", "urn:schemas-microsoft-com:xml-wcfservicemap", extensions[k], true, false);
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008154 File Offset: 0x00006354
		private void Write14_ExtensionFile(string n, string ns, ExtensionFile o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ExtensionFile)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ExtensionFile", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("FileName", "", o.FileName);
			base.WriteAttribute("Name", "", o.Name);
			base.WriteEndElement(o);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000081E8 File Offset: 0x000063E8
		private void Write13_MetadataFile(string n, string ns, MetadataFile o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(MetadataFile)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MetadataFile", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("FileName", "", o.FileName);
			base.WriteAttribute("MetadataType", "", this.Write12_MetadataType(o.FileType));
			base.WriteAttribute("ID", "", o.ID);
			if (o.IgnoreSpecified)
			{
				base.WriteAttribute("Ignore", "", XmlConvert.ToString(o.Ignore));
			}
			if (o.IsMergeResultSpecified)
			{
				base.WriteAttribute("IsMergeResult", "", XmlConvert.ToString(o.IsMergeResult));
			}
			if (o.SourceIdSpecified)
			{
				base.WriteAttribute("SourceId", "", XmlConvert.ToString(o.SourceId));
			}
			base.WriteAttribute("SourceUrl", "", o.SourceUrl);
			bool ignoreSpecified = o.IgnoreSpecified;
			bool isMergeResultSpecified = o.IsMergeResultSpecified;
			bool sourceIdSpecified = o.SourceIdSpecified;
			base.WriteEndElement(o);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000832C File Offset: 0x0000652C
		private string Write12_MetadataType(MetadataFile.MetadataType v)
		{
			string result;
			switch (v)
			{
			case MetadataFile.MetadataType.Unknown:
				result = "Unknown";
				break;
			case MetadataFile.MetadataType.Disco:
				result = "Disco";
				break;
			case MetadataFile.MetadataType.Wsdl:
				result = "Wsdl";
				break;
			case MetadataFile.MetadataType.Schema:
				result = "Schema";
				break;
			case MetadataFile.MetadataType.Policy:
				result = "Policy";
				break;
			case MetadataFile.MetadataType.Xml:
				result = "Xml";
				break;
			case MetadataFile.MetadataType.Edmx:
				result = "Edmx";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Compilation.WCFModel.MetadataFile.MetadataType");
			}
			return result;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000083B4 File Offset: 0x000065B4
		private void Write10_MetadataSource(string n, string ns, MetadataSource o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(MetadataSource)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("MetadataSource", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("Address", "", o.Address);
			base.WriteAttribute("Protocol", "", o.Protocol);
			base.WriteAttribute("SourceId", "", XmlConvert.ToString(o.SourceId));
			base.WriteEndElement(o);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008464 File Offset: 0x00006664
		private void Write9_ClientOptions(string n, string ns, ClientOptions o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ClientOptions)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ClientOptions", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteElementStringRaw("GenerateAsynchronousMethods", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.GenerateAsynchronousMethods));
			if (o.GenerateTaskBasedAsynchronousMethodSpecified)
			{
				base.WriteElementStringRaw("GenerateTaskBasedAsynchronousMethod", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.GenerateTaskBasedAsynchronousMethod));
			}
			base.WriteElementStringRaw("EnableDataBinding", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.EnableDataBinding));
			List<ReferencedType> excludedTypeList = o.ExcludedTypeList;
			if (excludedTypeList != null)
			{
				base.WriteStartElement("ExcludedTypes", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int i = 0; i < ((ICollection)excludedTypeList).Count; i++)
				{
					this.Write2_ReferencedType("ExcludedType", "urn:schemas-microsoft-com:xml-wcfservicemap", excludedTypeList[i], true, false);
				}
				base.WriteEndElement();
			}
			base.WriteElementStringRaw("ImportXmlTypes", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.ImportXmlTypes));
			base.WriteElementStringRaw("GenerateInternalTypes", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.GenerateInternalTypes));
			base.WriteElementStringRaw("GenerateMessageContracts", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.GenerateMessageContracts));
			List<NamespaceMapping> namespaceMappingList = o.NamespaceMappingList;
			if (namespaceMappingList != null)
			{
				base.WriteStartElement("NamespaceMappings", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int j = 0; j < ((ICollection)namespaceMappingList).Count; j++)
				{
					this.Write3_NamespaceMapping("NamespaceMapping", "urn:schemas-microsoft-com:xml-wcfservicemap", namespaceMappingList[j], true, false);
				}
				base.WriteEndElement();
			}
			List<ReferencedCollectionType> collectionMappingList = o.CollectionMappingList;
			if (collectionMappingList != null)
			{
				base.WriteStartElement("CollectionMappings", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int k = 0; k < ((ICollection)collectionMappingList).Count; k++)
				{
					this.Write5_ReferencedCollectionType("CollectionMapping", "urn:schemas-microsoft-com:xml-wcfservicemap", collectionMappingList[k], true, false);
				}
				base.WriteEndElement();
			}
			base.WriteElementStringRaw("GenerateSerializableTypes", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.GenerateSerializableTypes));
			base.WriteElementString("Serializer", "urn:schemas-microsoft-com:xml-wcfservicemap", this.Write6_ProxySerializerType(o.Serializer));
			if (o.UseSerializerForFaultsSpecified)
			{
				base.WriteElementStringRaw("UseSerializerForFaults", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.UseSerializerForFaults));
			}
			if (o.WrappedSpecified)
			{
				base.WriteElementStringRaw("Wrapped", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.Wrapped));
			}
			base.WriteElementStringRaw("ReferenceAllAssemblies", "urn:schemas-microsoft-com:xml-wcfservicemap", XmlConvert.ToString(o.ReferenceAllAssemblies));
			List<ReferencedAssembly> referencedAssemblyList = o.ReferencedAssemblyList;
			if (referencedAssemblyList != null)
			{
				base.WriteStartElement("ReferencedAssemblies", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int l = 0; l < ((ICollection)referencedAssemblyList).Count; l++)
				{
					this.Write7_ReferencedAssembly("ReferencedAssembly", "urn:schemas-microsoft-com:xml-wcfservicemap", referencedAssemblyList[l], true, false);
				}
				base.WriteEndElement();
			}
			List<ReferencedType> referencedDataContractTypeList = o.ReferencedDataContractTypeList;
			if (referencedDataContractTypeList != null)
			{
				base.WriteStartElement("ReferencedDataContractTypes", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int m = 0; m < ((ICollection)referencedDataContractTypeList).Count; m++)
				{
					this.Write2_ReferencedType("ReferencedDataContractType", "urn:schemas-microsoft-com:xml-wcfservicemap", referencedDataContractTypeList[m], true, false);
				}
				base.WriteEndElement();
			}
			List<ContractMapping> serviceContractMappingList = o.ServiceContractMappingList;
			if (serviceContractMappingList != null)
			{
				base.WriteStartElement("ServiceContractMappings", "urn:schemas-microsoft-com:xml-wcfservicemap", null, false);
				for (int num = 0; num < ((ICollection)serviceContractMappingList).Count; num++)
				{
					this.Write8_ContractMapping("ServiceContractMapping", "urn:schemas-microsoft-com:xml-wcfservicemap", serviceContractMappingList[num], true, false);
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008800 File Offset: 0x00006A00
		private void Write8_ContractMapping(string n, string ns, ContractMapping o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ContractMapping)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ContractMapping", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("Name", "", o.Name);
			base.WriteAttribute("TargetNamespace", "", o.TargetNamespace);
			base.WriteAttribute("TypeName", "", o.TypeName);
			base.WriteEndElement(o);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000088AC File Offset: 0x00006AAC
		private void Write2_ReferencedType(string n, string ns, ReferencedType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ReferencedType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ReferencedType", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("TypeName", "", o.TypeName);
			base.WriteEndElement(o);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000892C File Offset: 0x00006B2C
		private void Write7_ReferencedAssembly(string n, string ns, ReferencedAssembly o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ReferencedAssembly)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ReferencedAssembly", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("AssemblyName", "", o.AssemblyName);
			base.WriteEndElement(o);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000089AC File Offset: 0x00006BAC
		private string Write6_ProxySerializerType(ClientOptions.ProxySerializerType v)
		{
			string result;
			switch (v)
			{
			case ClientOptions.ProxySerializerType.Auto:
				result = "Auto";
				break;
			case ClientOptions.ProxySerializerType.DataContractSerializer:
				result = "DataContractSerializer";
				break;
			case ClientOptions.ProxySerializerType.XmlSerializer:
				result = "XmlSerializer";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Compilation.WCFModel.ClientOptions.ProxySerializerType");
			}
			return result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008A04 File Offset: 0x00006C04
		private void Write5_ReferencedCollectionType(string n, string ns, ReferencedCollectionType o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(ReferencedCollectionType)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("ReferencedCollectionType", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("TypeName", "", o.TypeName);
			base.WriteAttribute("Category", "", this.Write4_CollectionCategory(o.Category));
			base.WriteEndElement(o);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008AA0 File Offset: 0x00006CA0
		private string Write4_CollectionCategory(ReferencedCollectionType.CollectionCategory v)
		{
			string result;
			switch (v)
			{
			case ReferencedCollectionType.CollectionCategory.Unknown:
				result = "Unknown";
				break;
			case ReferencedCollectionType.CollectionCategory.List:
				result = "List";
				break;
			case ReferencedCollectionType.CollectionCategory.Dictionary:
				result = "Dictionary";
				break;
			default:
				throw base.CreateInvalidEnumValueException(((long)v).ToString(CultureInfo.InvariantCulture), "System.Web.Compilation.WCFModel.ReferencedCollectionType.CollectionCategory");
			}
			return result;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008AF8 File Offset: 0x00006CF8
		private void Write3_NamespaceMapping(string n, string ns, NamespaceMapping o, bool isNullable, bool needType)
		{
			if (o == null)
			{
				if (isNullable)
				{
					base.WriteNullTagLiteral(n, ns);
				}
				return;
			}
			if (!needType)
			{
				Type type = o.GetType();
				if (!(type == typeof(NamespaceMapping)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("NamespaceMapping", "urn:schemas-microsoft-com:xml-wcfservicemap");
			}
			base.WriteAttribute("TargetNamespace", "", o.TargetNamespace);
			base.WriteAttribute("ClrNamespace", "", o.ClrNamespace);
			base.WriteEndElement(o);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000032F4 File Offset: 0x000014F4
		protected override void InitCallbacks()
		{
		}
	}
}
