using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Compilation.WCFModel.DataSvcMapFileXmlSerializer
{
	// Token: 0x0200002E RID: 46
	internal class XmlSerializationWriterDataSvcMapFileImpl : XmlSerializationWriter
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x0000B616 File Offset: 0x00009816
		public void Write9_ReferenceGroup(object o)
		{
			base.WriteStartDocument();
			if (o == null)
			{
				base.WriteNullTagLiteral("ReferenceGroup", "urn:schemas-microsoft-com:xml-dataservicemap");
				return;
			}
			base.TopLevelElement();
			this.Write8_DataSvcMapFileImpl("ReferenceGroup", "urn:schemas-microsoft-com:xml-dataservicemap", (DataSvcMapFileImpl)o, true, false);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000B650 File Offset: 0x00009850
		private void Write8_DataSvcMapFileImpl(string n, string ns, DataSvcMapFileImpl o, bool isNullable, bool needType)
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
				if (!(type == typeof(DataSvcMapFileImpl)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("DataSvcMapFileImpl", "urn:schemas-microsoft-com:xml-dataservicemap");
			}
			base.WriteAttribute("ID", "", o.ID);
			List<MetadataSource> metadataSourceList = o.MetadataSourceList;
			if (metadataSourceList != null)
			{
				base.WriteStartElement("MetadataSources", "urn:schemas-microsoft-com:xml-dataservicemap", null, false);
				for (int i = 0; i < ((ICollection)metadataSourceList).Count; i++)
				{
					this.Write2_MetadataSource("MetadataSource", "urn:schemas-microsoft-com:xml-dataservicemap", metadataSourceList[i], true, false);
				}
				base.WriteEndElement();
			}
			List<MetadataFile> metadataList = o.MetadataList;
			if (metadataList != null)
			{
				base.WriteStartElement("Metadata", "urn:schemas-microsoft-com:xml-dataservicemap", null, false);
				for (int j = 0; j < ((ICollection)metadataList).Count; j++)
				{
					this.Write5_MetadataFile("MetadataFile", "urn:schemas-microsoft-com:xml-dataservicemap", metadataList[j], true, false);
				}
				base.WriteEndElement();
			}
			List<ExtensionFile> extensions = o.Extensions;
			if (extensions != null)
			{
				base.WriteStartElement("Extensions", "urn:schemas-microsoft-com:xml-dataservicemap", null, false);
				for (int k = 0; k < ((ICollection)extensions).Count; k++)
				{
					this.Write6_ExtensionFile("ExtensionFile", "urn:schemas-microsoft-com:xml-dataservicemap", extensions[k], true, false);
				}
				base.WriteEndElement();
			}
			List<Parameter> parameters = o.Parameters;
			if (parameters != null)
			{
				base.WriteStartElement("Parameters", "urn:schemas-microsoft-com:xml-dataservicemap", null, false);
				for (int l = 0; l < ((ICollection)parameters).Count; l++)
				{
					this.Write7_Parameter("Parameter", "urn:schemas-microsoft-com:xml-dataservicemap", parameters[l], true, false);
				}
				base.WriteEndElement();
			}
			base.WriteEndElement(o);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000B818 File Offset: 0x00009A18
		private void Write7_Parameter(string n, string ns, Parameter o, bool isNullable, bool needType)
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
				if (!(type == typeof(Parameter)))
				{
					throw base.CreateUnknownTypeException(o);
				}
			}
			base.WriteStartElement(n, ns, o, false, null);
			if (needType)
			{
				base.WriteXsiType("Parameter", "urn:schemas-microsoft-com:xml-dataservicemap");
			}
			base.WriteAttribute("Name", "", o.Name);
			base.WriteAttribute("Value", "", o.Value);
			base.WriteEndElement(o);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000B8AC File Offset: 0x00009AAC
		private void Write6_ExtensionFile(string n, string ns, ExtensionFile o, bool isNullable, bool needType)
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
				base.WriteXsiType("ExtensionFile", "urn:schemas-microsoft-com:xml-dataservicemap");
			}
			base.WriteAttribute("FileName", "", o.FileName);
			base.WriteAttribute("Name", "", o.Name);
			base.WriteEndElement(o);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000B940 File Offset: 0x00009B40
		private void Write5_MetadataFile(string n, string ns, MetadataFile o, bool isNullable, bool needType)
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
				base.WriteXsiType("MetadataFile", "urn:schemas-microsoft-com:xml-dataservicemap");
			}
			base.WriteAttribute("FileName", "", o.FileName);
			base.WriteAttribute("MetadataType", "", this.Write4_MetadataType(o.FileType));
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

		// Token: 0x060001E5 RID: 485 RVA: 0x0000BA84 File Offset: 0x00009C84
		private string Write4_MetadataType(MetadataFile.MetadataType v)
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

		// Token: 0x060001E6 RID: 486 RVA: 0x0000BB0C File Offset: 0x00009D0C
		private void Write2_MetadataSource(string n, string ns, MetadataSource o, bool isNullable, bool needType)
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
				base.WriteXsiType("MetadataSource", "urn:schemas-microsoft-com:xml-dataservicemap");
			}
			base.WriteAttribute("Address", "", o.Address);
			base.WriteAttribute("Protocol", "", o.Protocol);
			base.WriteAttribute("SourceId", "", XmlConvert.ToString(o.SourceId));
			base.WriteEndElement(o);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000032F4 File Offset: 0x000014F4
		protected override void InitCallbacks()
		{
		}
	}
}
