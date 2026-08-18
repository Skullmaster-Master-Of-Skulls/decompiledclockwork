using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200030A RID: 778
	[DebuggerDisplay("Name={Name}")]
	internal abstract class SchemaElement
	{
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002E33 RID: 11827 RVA: 0x000AF038 File Offset: 0x000AD238
		internal int LineNumber
		{
			get
			{
				return this._lineNumber;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000AF040 File Offset: 0x000AD240
		internal int LinePosition
		{
			get
			{
				return this._linePosition;
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x000AF048 File Offset: 0x000AD248
		// (set) Token: 0x06002E36 RID: 11830 RVA: 0x000AF050 File Offset: 0x000AD250
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

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x000AF059 File Offset: 0x000AD259
		// (set) Token: 0x06002E38 RID: 11832 RVA: 0x000AF061 File Offset: 0x000AD261
		internal DocumentationElement Documentation
		{
			get
			{
				return this._documentation;
			}
			set
			{
				this._documentation = value;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x000AF06A File Offset: 0x000AD26A
		// (set) Token: 0x06002E3A RID: 11834 RVA: 0x000AF072 File Offset: 0x000AD272
		internal SchemaElement ParentElement
		{
			get
			{
				return this._parentElement;
			}
			private set
			{
				this._parentElement = value;
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000AF07B File Offset: 0x000AD27B
		// (set) Token: 0x06002E3C RID: 11836 RVA: 0x000AF083 File Offset: 0x000AD283
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

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x000A9050 File Offset: 0x000A7250
		public virtual string FQName
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x000A9050 File Offset: 0x000A7250
		public virtual string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x000AF08C File Offset: 0x000AD28C
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

		// Token: 0x06002E40 RID: 11840 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void Validate()
		{
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000AF0A7 File Offset: 0x000AD2A7
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, int lineNumber, int linePosition, object message)
		{
			this.AddError(errorCode, severity, this.SchemaLocation, lineNumber, linePosition, message);
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000AF0BC File Offset: 0x000AD2BC
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, XmlReader reader, object message)
		{
			int lineNumber;
			int linePosition;
			SchemaElement.GetPositionInfo(reader, out lineNumber, out linePosition);
			this.AddError(errorCode, severity, this.SchemaLocation, lineNumber, linePosition, message);
		}

		// Token: 0x06002E43 RID: 11843 RVA: 0x000AF0E5 File Offset: 0x000AD2E5
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, object message)
		{
			this.AddError(errorCode, severity, this.SchemaLocation, this.LineNumber, this.LinePosition, message);
		}

		// Token: 0x06002E44 RID: 11844 RVA: 0x000AF102 File Offset: 0x000AD302
		internal void AddError(ErrorCode errorCode, EdmSchemaErrorSeverity severity, SchemaElement element, object message)
		{
			this.AddError(errorCode, severity, element.Schema.Location, element.LineNumber, element.LinePosition, message);
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x000AF128 File Offset: 0x000AD328
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

		// Token: 0x06002E46 RID: 11846 RVA: 0x000AF264 File Offset: 0x000AD464
		internal void GetPositionInfo(XmlReader reader)
		{
			SchemaElement.GetPositionInfo(reader, out this._lineNumber, out this._linePosition);
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000AF278 File Offset: 0x000AD478
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

		// Token: 0x06002E48 RID: 11848 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void ResolveTopLevelNames()
		{
		}

		// Token: 0x06002E49 RID: 11849 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal virtual void ResolveSecondLevelNames()
		{
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x000AF2B0 File Offset: 0x000AD4B0
		internal SchemaElement(SchemaElement parentElement)
		{
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
					throw EntityUtil.InvalidOperation(Strings.AllElementsMustBeInSchema);
				}
			}
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x000AF301 File Offset: 0x000AD501
		internal SchemaElement(SchemaElement parentElement, string name) : this(parentElement)
		{
			this._name = name;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void HandleAttributesComplete()
		{
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x000089D0 File Offset: 0x00006BD0
		protected virtual void HandleChildElementsComplete()
		{
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x000AF314 File Offset: 0x000AD514
		protected string HandleUndottedNameAttribute(XmlReader reader, string field)
		{
			string result = field;
			bool undottedName = Utils.GetUndottedName(this.Schema, reader, out result);
			return result;
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x000AF338 File Offset: 0x000AD538
		protected ReturnValue<string> HandleDottedNameAttribute(XmlReader reader, string field, Func<object, string> errorFormat)
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

		// Token: 0x06002E50 RID: 11856 RVA: 0x000AF368 File Offset: 0x000AD568
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

		// Token: 0x06002E51 RID: 11857 RVA: 0x000AF38C File Offset: 0x000AD58C
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

		// Token: 0x06002E52 RID: 11858 RVA: 0x000AF3B0 File Offset: 0x000AD5B0
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

		// Token: 0x06002E53 RID: 11859 RVA: 0x000AF3D3 File Offset: 0x000AD5D3
		protected virtual void SkipThroughElement(XmlReader reader)
		{
			this.Parse(reader);
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x000AF3DC File Offset: 0x000AD5DC
		protected void SkipElement(XmlReader reader)
		{
			using (XmlReader xmlReader = reader.ReadSubtree())
			{
				while (xmlReader.Read())
				{
				}
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000AF414 File Offset: 0x000AD614
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

		// Token: 0x06002E56 RID: 11862 RVA: 0x000173E2 File Offset: 0x000155E2
		protected virtual bool HandleText(XmlReader reader)
		{
			return false;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x000AB00C File Offset: 0x000A920C
		internal virtual SchemaElement Clone(SchemaElement parentElement)
		{
			throw Error.NotImplemented();
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x000AF42B File Offset: 0x000AD62B
		private void HandleDocumentationElement(XmlReader reader)
		{
			this.Documentation = new DocumentationElement(this);
			this.Documentation.Parse(reader);
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000AF445 File Offset: 0x000AD645
		protected virtual void HandleNameAttribute(XmlReader reader)
		{
			this.Name = this.HandleUndottedNameAttribute(reader, this.Name);
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000AF45C File Offset: 0x000AD65C
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

		// Token: 0x06002E5B RID: 11867 RVA: 0x000AF4C8 File Offset: 0x000AD6C8
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

		// Token: 0x06002E5C RID: 11868 RVA: 0x000173E2 File Offset: 0x000155E2
		protected virtual bool ProhibitAttribute(string namespaceUri, string localName)
		{
			return false;
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x000AF587 File Offset: 0x000AD787
		internal static bool CanHandleAttribute(XmlReader reader, string localName)
		{
			return reader.NamespaceURI.Length == 0 && reader.LocalName == localName;
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x000AF5A4 File Offset: 0x000AD7A4
		protected virtual bool HandleAttribute(XmlReader reader)
		{
			if (SchemaElement.CanHandleAttribute(reader, "Name"))
			{
				this.HandleNameAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x000AF5C0 File Offset: 0x000AD7C0
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
					XElement xelement = XElement.Load(new StringReader(xmlReader.ReadOuterXml()));
					property = SchemaElement.CreateMetadataPropertyFromOtherNamespaceXmlArtifact(xelement.Name.NamespaceName, xelement.Name.LocalName, xelement);
					goto IL_10E;
				}
			}
			if (reader.NamespaceURI == "http://www.w3.org/2000/xmlns/")
			{
				return true;
			}
			property = SchemaElement.CreateMetadataPropertyFromOtherNamespaceXmlArtifact(reader.NamespaceURI, reader.LocalName, reader.Value);
			IL_10E:
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

		// Token: 0x06002E60 RID: 11872 RVA: 0x000AF73C File Offset: 0x000AD93C
		internal static MetadataProperty CreateMetadataPropertyFromOtherNamespaceXmlArtifact(string xmlNamespaceUri, string artifactName, object value)
		{
			return new MetadataProperty(xmlNamespaceUri + ":" + artifactName, TypeUsage.Create(EdmProviderManifest.Instance.GetPrimitiveType(PrimitiveTypeKind.String)), value);
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x000AF770 File Offset: 0x000AD970
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

		// Token: 0x06002E62 RID: 11874 RVA: 0x000AF7E0 File Offset: 0x000AD9E0
		protected bool CanHandleElement(XmlReader reader, string localName)
		{
			return reader.NamespaceURI == this.Schema.SchemaXmlNamespace && reader.LocalName == localName;
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000AF808 File Offset: 0x000ADA08
		protected virtual bool HandleElement(XmlReader reader)
		{
			if (this.CanHandleElement(reader, "Documentation"))
			{
				this.HandleDocumentationElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000AF822 File Offset: 0x000ADA22
		private void ParseText(XmlReader reader)
		{
			if (this.HandleText(reader))
			{
				return;
			}
			if (reader.Value == null || reader.Value.Trim().Length != 0)
			{
				this.AddError(ErrorCode.TextNotAllowed, EdmSchemaErrorSeverity.Error, reader, Strings.TextNotAllowed(reader.Value));
			}
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000089D0 File Offset: 0x00006BD0
		[Conditional("DEBUG")]
		internal static void AssertReaderConsidersSchemaInvalid(XmlReader reader)
		{
		}

		// Token: 0x04001415 RID: 5141
		internal const string XmlNamespaceNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04001416 RID: 5142
		private SchemaElement _parentElement;

		// Token: 0x04001417 RID: 5143
		private Schema _schema;

		// Token: 0x04001418 RID: 5144
		private int _lineNumber;

		// Token: 0x04001419 RID: 5145
		private int _linePosition;

		// Token: 0x0400141A RID: 5146
		private string _name;

		// Token: 0x0400141B RID: 5147
		private DocumentationElement _documentation;

		// Token: 0x0400141C RID: 5148
		private List<MetadataProperty> _otherContent;

		// Token: 0x0400141D RID: 5149
		protected const int MaxValueVersionComponent = 32767;
	}
}
