using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Resources;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000351 RID: 849
	[DebuggerDisplay("Name={Name}")]
	internal abstract class SchemaElement
	{
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x00091C9A File Offset: 0x0008FE9A
		internal int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x00091CA2 File Offset: 0x0008FEA2
		internal int LinePosition
		{
			get
			{
				return this._linePosition;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x00091CAA File Offset: 0x0008FEAA
		// (set) Token: 0x06001E46 RID: 7750 RVA: 0x00091CB2 File Offset: 0x0008FEB2
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x00091CBB File Offset: 0x0008FEBB
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x00091CC3 File Offset: 0x0008FEC3
		internal DocumentationElement Documentation { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x00091CCC File Offset: 0x0008FECC
		// (set) Token: 0x06001E4A RID: 7754 RVA: 0x00091CD4 File Offset: 0x0008FED4
		internal SchemaElement ParentElement { get; private set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00091CDD File Offset: 0x0008FEDD
		// (set) Token: 0x06001E4C RID: 7756 RVA: 0x00091CE5 File Offset: 0x0008FEE5
		internal Schema Schema
		{
			get
			{
				return this._schema;
			}
			set
			{
				this._schema = value;
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x00091CEE File Offset: 0x0008FEEE
		public virtual string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001E4E RID: 7758 RVA: 0x00091CF6 File Offset: 0x0008FEF6
		public virtual string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x00091CFE File Offset: 0x0008FEFE
		public List<MetadataProperty> OtherContent
		{
			get
			{
				if (this._otherContent == null)
				{
					this._otherContent = new List<MetadataProperty>();
				}
				return this._otherContent;
			}
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00091D19 File Offset: 0x0008FF19
		internal virtual void Validate()
		{
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00091D1B File Offset: 0x0008FF1B
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, int lineNumber, int linePosition, object message)
		{
			this.AddError(errorCode, severity, this.SchemaLocation, lineNumber, linePosition, message);
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x00091D30 File Offset: 0x0008FF30
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, XmlReader reader, object message)
		{
			int lineNumber;
			int linePosition;
			SchemaElement.GetPositionInfo(reader, out lineNumber, out linePosition);
			this.AddError(errorCode, severity, this.SchemaLocation, lineNumber, linePosition, message);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00091D59 File Offset: 0x0008FF59
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, object message)
		{
			this.AddError(errorCode, severity, this.SchemaLocation, this.LineNumber, this.LinePosition, message);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00091D76 File Offset: 0x0008FF76
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, SchemaElement element, object message)
		{
			this.AddError(errorCode, severity, element.Schema.Location, element.LineNumber, element.LinePosition, message);
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00091D9C File Offset: 0x0008FF9C
		internal void Parse(XmlReader reader)
		{
			this.GetPositionInfo(reader);
			bool flag = !reader.IsEmptyElement;
			bool flag2 = reader.MoveToFirstAttribute();
			while (flag2)
			{
				this.ParseAttribute(reader);
				flag2 = reader.MoveToNextAttribute();
			}
			this.HandleAttributesComplete();
			bool flag3 = !flag;
			bool flag4 = false;
			while (!flag3)
			{
				if (flag4)
				{
					flag4 = false;
					reader.Skip();
					if (reader.EOF)
					{
						break;
					}
				}
				else if (!reader.Read())
				{
					break;
				}
				switch (reader.NodeType)
				{
				case XmlNodeType.Element:
					flag4 = this.ParseElement(reader);
					continue;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.SignificantWhitespace:
					this.ParseText(reader);
					continue;
				case XmlNodeType.EntityReference:
				case XmlNodeType.DocumentType:
					flag4 = true;
					continue;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Notation:
				case XmlNodeType.Whitespace:
				case XmlNodeType.XmlDeclaration:
					continue;
				case XmlNodeType.EndElement:
					flag3 = true;
					continue;
				}
				this.AddError(ErrorCode.UnexpectedXmlNodeType, EdmSchemaErrorSeverity.Error, reader, Strings.UnexpectedXmlNodeType(reader.NodeType));
				flag4 = true;
			}
			this.HandleChildElementsComplete();
			if (reader.EOF && reader.Depth > 0)
			{
				this.AddError(ErrorCode.MalformedXml, EdmSchemaErrorSeverity.Error, 0, 0, Strings.MalformedXml(this.LineNumber, this.LinePosition));
			}
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00091ED8 File Offset: 0x000900D8
		internal void GetPositionInfo(XmlReader reader)
		{
			SchemaElement.GetPositionInfo(reader, out this._lineNumber, out this._linePosition);
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00091EEC File Offset: 0x000900EC
		internal static void GetPositionInfo(XmlReader reader, out int lineNumber, out int linePosition)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				lineNumber = xmlLineInfo.LineNumber;
				linePosition = xmlLineInfo.LinePosition;
				return;
			}
			lineNumber = 0;
			linePosition = 0;
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00091F22 File Offset: 0x00090122
		internal virtual void ResolveTopLevelNames()
		{
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00091F24 File Offset: 0x00090124
		internal virtual void ResolveSecondLevelNames()
		{
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00091F28 File Offset: 0x00090128
		internal SchemaElement(SchemaElement parentElement, IDbDependencyResolver resolver = null)
		{
			this._resolver = (resolver ?? DbConfiguration.DependencyResolver);
			if (parentElement != null)
			{
				this.ParentElement = parentElement;
				for (SchemaElement schemaElement = parentElement; schemaElement != null; schemaElement = schemaElement.ParentElement)
				{
					Schema schema = schemaElement as Schema;
					if (schema != null)
					{
						this.Schema = schema;
						break;
					}
				}
				if (this.Schema == null)
				{
					throw new InvalidOperationException(Strings.AllElementsMustBeInSchema);
				}
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00091F89 File Offset: 0x00090189
		internal SchemaElement(SchemaElement parentElement, string name, IDbDependencyResolver resolver = null) : this(parentElement, resolver)
		{
			this._name = name;
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00091F9A File Offset: 0x0009019A
		protected virtual void HandleAttributesComplete()
		{
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00091F9C File Offset: 0x0009019C
		protected virtual void HandleChildElementsComplete()
		{
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x00091FA0 File Offset: 0x000901A0
		protected string HandleUndottedNameAttribute(XmlReader reader, string field)
		{
			string result = field;
			if (!Utils.GetUndottedName(this.Schema, reader, out result))
			{
				return result;
			}
			return result;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x00091FC4 File Offset: 0x000901C4
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "field")]
		protected ReturnValue<string> HandleDottedNameAttribute(XmlReader reader, string field)
		{
			ReturnValue<string> returnValue = new ReturnValue<string>();
			string value;
			if (!Utils.GetDottedName(this.Schema, reader, out value))
			{
				return returnValue;
			}
			returnValue.Value = value;
			return returnValue;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x00091FF4 File Offset: 0x000901F4
		internal bool HandleIntAttribute(XmlReader reader, ref int field)
		{
			int num;
			if (!Utils.GetInt(this.Schema, reader, out num))
			{
				return false;
			}
			field = num;
			return true;
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x00092018 File Offset: 0x00090218
		internal bool HandleByteAttribute(XmlReader reader, ref byte field)
		{
			byte b;
			if (!Utils.GetByte(this.Schema, reader, out b))
			{
				return false;
			}
			field = b;
			return true;
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x0009203C File Offset: 0x0009023C
		internal bool HandleBoolAttribute(XmlReader reader, ref bool field)
		{
			bool flag;
			if (!Utils.GetBool(this.Schema, reader, out flag))
			{
				return false;
			}
			field = flag;
			return true;
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x0009205F File Offset: 0x0009025F
		protected virtual void SkipThroughElement(XmlReader reader)
		{
			this.Parse(reader);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x00092068 File Offset: 0x00090268
		protected virtual void SkipElement(XmlReader reader)
		{
			using (XmlReader xmlReader = reader.ReadSubtree())
			{
				while (xmlReader.Read())
				{
				}
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x000920A0 File Offset: 0x000902A0
		protected string SchemaLocation
		{
			get
			{
				if (this.Schema != null)
				{
					return this.Schema.Location;
				}
				return null;
			}
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000920B7 File Offset: 0x000902B7
		protected virtual bool HandleText(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000920BA File Offset: 0x000902BA
		internal virtual SchemaElement Clone(SchemaElement parentElement)
		{
			throw Error.NotImplemented();
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x000920C1 File Offset: 0x000902C1
		private void HandleDocumentationElement(XmlReader reader)
		{
			this.Documentation = new DocumentationElement(this);
			this.Documentation.Parse(reader);
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x000920DB File Offset: 0x000902DB
		protected virtual void HandleNameAttribute(XmlReader reader)
		{
			this.Name = this.HandleUndottedNameAttribute(reader, this.Name);
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000920F0 File Offset: 0x000902F0
		private void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, string sourceLocation, int lineNumber, int linePosition, object message)
		{
			string text = message as string;
			EdmSchemaError error;
			if (text != null)
			{
				error = new EdmSchemaError(text, (int)errorCode, severity, sourceLocation, lineNumber, linePosition);
			}
			else
			{
				Exception ex = message as Exception;
				if (ex != null)
				{
					error = new EdmSchemaError(ex.Message, (int)errorCode, severity, sourceLocation, lineNumber, linePosition, ex);
				}
				else
				{
					error = new EdmSchemaError(message.ToString(), (int)errorCode, severity, sourceLocation, lineNumber, linePosition);
				}
			}
			this.Schema.AddError(error);
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0009215C File Offset: 0x0009035C
		private void ParseAttribute(XmlReader reader)
		{
			string namespaceURI = reader.NamespaceURI;
			if (namespaceURI == "http://schemas.microsoft.com/ado/2009/02/edm/annotation" && reader.LocalName == "UseStrongSpatialTypes" && !this.ProhibitAttribute(namespaceURI, reader.LocalName) && this.HandleAttribute(reader))
			{
				return;
			}
			if (!this.Schema.IsParseableXmlNamespace(namespaceURI, true))
			{
				this.AddOtherContent(reader);
				return;
			}
			if (!this.ProhibitAttribute(namespaceURI, reader.LocalName) && this.HandleAttribute(reader))
			{
				return;
			}
			if ((reader.SchemaInfo == null || reader.SchemaInfo.Validity != XmlSchemaValidity.Invalid) && (string.IsNullOrEmpty(namespaceURI) || this.Schema.IsParseableXmlNamespace(namespaceURI, true)))
			{
				this.AddError(ErrorCode.UnexpectedXmlAttribute, EdmSchemaErrorSeverity.Error, reader, Strings.UnexpectedXmlAttribute(reader.Name));
			}
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0009221B File Offset: 0x0009041B
		protected virtual bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return false;
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0009221E File Offset: 0x0009041E
		internal static bool CanHandleAttribute(XmlReader reader, string localName)
		{
			return reader.NamespaceURI.Length == 0 && reader.LocalName == localName;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0009223B File Offset: 0x0009043B
		protected virtual bool HandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Name"))
			{
				this.HandleNameAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00092274 File Offset: 0x00090474
		private bool AddOtherContent(XmlReader reader)
		{
			int lineNumber;
			int linePosition;
			SchemaElement.GetPositionInfo(reader, out lineNumber, out linePosition);
			MetadataProperty property;
			if (reader.NodeType == XmlNodeType.Element)
			{
				if (this._schema.SchemaVersion == 1.0 || this._schema.SchemaVersion == 1.1)
				{
					return true;
				}
				if (this._schema.SchemaVersion >= 2.0 && reader.NamespaceURI == "http://schemas.microsoft.com/ado/2006/04/codegeneration")
				{
					this.AddError(ErrorCode.NoCodeGenNamespaceInStructuralAnnotation, EdmSchemaErrorSeverity.Error, lineNumber, linePosition, Strings.NoCodeGenNamespaceInStructuralAnnotation("http://schemas.microsoft.com/ado/2006/04/codegeneration"));
					return true;
				}
				using (XmlReader xmlReader = reader.ReadSubtree())
				{
					xmlReader.Read();
					using (StringReader stringReader = new StringReader(xmlReader.ReadOuterXml()))
					{
						XElement xelement = XElement.Load(stringReader);
						property = SchemaElement.CreateMetadataPropertyFromXmlElement(xelement.Name.NamespaceName, xelement.Name.LocalName, xelement);
					}
					goto IL_120;
				}
			}
			if (reader.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return true;
			}
			property = this.CreateMetadataPropertyFromXmlAttribute(reader.NamespaceURI, reader.LocalName, reader.Value);
			IL_120:
			if (!this.OtherContent.Exists((MetadataProperty mp) => mp.Identity == property.Identity))
			{
				this.OtherContent.Add(property);
			}
			else
			{
				this.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, lineNumber, linePosition, Strings.DuplicateAnnotation(property.Identity, this.FQName));
			}
			return false;
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x00092410 File Offset: 0x00090610
		internal static MetadataProperty CreateMetadataPropertyFromXmlElement(string xmlNamespaceUri, string elementName, XElement value)
		{
			return MetadataProperty.CreateAnnotation(xmlNamespaceUri + ":" + elementName, value);
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00092424 File Offset: 0x00090624
		internal MetadataProperty CreateMetadataPropertyFromXmlAttribute(string xmlNamespaceUri, string attributeName, string value)
		{
			Func<IMetadataAnnotationSerializer> service = this._resolver.GetService(attributeName);
			object value2 = (service == null) ? value : service().Deserialize(attributeName, value);
			return MetadataProperty.CreateAnnotation(xmlNamespaceUri + ":" + attributeName, value2);
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x00092464 File Offset: 0x00090664
		private bool ParseElement(XmlReader reader)
		{
			string namespaceURI = reader.NamespaceURI;
			if (!this.Schema.IsParseableXmlNamespace(namespaceURI, true) && this.ParentElement != null)
			{
				return this.AddOtherContent(reader);
			}
			if (this.HandleElement(reader))
			{
				return false;
			}
			if (string.IsNullOrEmpty(namespaceURI) || this.Schema.IsParseableXmlNamespace(reader.NamespaceURI, false))
			{
				this.AddError(ErrorCode.UnexpectedXmlElement, EdmSchemaErrorSeverity.Error, reader, Strings.UnexpectedXmlElement(reader.Name));
			}
			return true;
		}

		// Token: 0x06001E73 RID: 7795 RVA: 0x000924D4 File Offset: 0x000906D4
		protected bool CanHandleElement(XmlReader reader, string localName)
		{
			return reader.NamespaceURI == this.Schema.SchemaXmlNamespace && reader.LocalName == localName;
		}

		// Token: 0x06001E74 RID: 7796 RVA: 0x000924FC File Offset: 0x000906FC
		protected virtual bool HandleElement(XmlReader reader)
		{
			if (this.CanHandleElement(reader, "Documentation"))
			{
				this.HandleDocumentationElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06001E75 RID: 7797 RVA: 0x00092516 File Offset: 0x00090716
		private void ParseText(XmlReader reader)
		{
			if (this.HandleText(reader))
			{
				return;
			}
			if (reader.Value != null && reader.Value.Trim().Length == 0)
			{
				return;
			}
			this.AddError(ErrorCode.TextNotAllowed, EdmSchemaErrorSeverity.Error, reader, Strings.TextNotAllowed(reader.Value));
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x00092552 File Offset: 0x00090752
		[Conditional("DEBUG")]
		internal static void AssertReaderConsidersSchemaInvalid(XmlReader reader)
		{
		}

		// Token: 0x04000A5D RID: 2653
		internal const string XmlNamespaceNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000A5E RID: 2654
		protected const int MaxValueVersionComponent = 32767;

		// Token: 0x04000A5F RID: 2655
		private Schema _schema;

		// Token: 0x04000A60 RID: 2656
		private int _lineNumber;

		// Token: 0x04000A61 RID: 2657
		private int _linePosition;

		// Token: 0x04000A62 RID: 2658
		private string _name;

		// Token: 0x04000A63 RID: 2659
		private List<MetadataProperty> _otherContent;

		// Token: 0x04000A64 RID: 2660
		private readonly IDbDependencyResolver _resolver;
	}
}
