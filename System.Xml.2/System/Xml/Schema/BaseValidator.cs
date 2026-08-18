using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x020001DF RID: 479
	internal class BaseValidator
	{
		// Token: 0x06001FD8 RID: 8152 RVA: 0x000ABFBC File Offset: 0x000AA1BC
		public BaseValidator(BaseValidator other)
		{
			this.reader = other.reader;
			this.schemaCollection = other.schemaCollection;
			this.eventHandling = other.eventHandling;
			this.nameTable = other.nameTable;
			this.schemaNames = other.schemaNames;
			this.positionInfo = other.positionInfo;
			this.xmlResolver = other.xmlResolver;
			this.baseUri = other.baseUri;
			this.elementName = other.elementName;
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000AC03B File Offset: 0x000AA23B
		public BaseValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling)
		{
			this.reader = reader;
			this.schemaCollection = schemaCollection;
			this.eventHandling = eventHandling;
			this.nameTable = reader.NameTable;
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
			this.elementName = new XmlQualifiedName();
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001FDA RID: 8154 RVA: 0x000AC07B File Offset: 0x000AA27B
		public XmlValidatingReaderImpl Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x000AC083 File Offset: 0x000AA283
		public XmlSchemaCollection SchemaCollection
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x000AC08B File Offset: 0x000AA28B
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x000AC094 File Offset: 0x000AA294
		public SchemaNames SchemaNames
		{
			get
			{
				if (this.schemaNames != null)
				{
					return this.schemaNames;
				}
				if (this.schemaCollection != null)
				{
					this.schemaNames = this.schemaCollection.GetSchemaNames(this.nameTable);
				}
				else
				{
					this.schemaNames = new SchemaNames(this.nameTable);
				}
				return this.schemaNames;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x000AC0E8 File Offset: 0x000AA2E8
		public PositionInfo PositionInfo
		{
			get
			{
				return this.positionInfo;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x000AC0F0 File Offset: 0x000AA2F0
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x000AC0F8 File Offset: 0x000AA2F8
		public XmlResolver XmlResolver
		{
			get
			{
				return this.xmlResolver;
			}
			set
			{
				this.xmlResolver = value;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x000AC101 File Offset: 0x000AA301
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x000AC109 File Offset: 0x000AA309
		public Uri BaseUri
		{
			get
			{
				return this.baseUri;
			}
			set
			{
				this.baseUri = value;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x000AC112 File Offset: 0x000AA312
		public ValidationEventHandler EventHandler
		{
			get
			{
				return (ValidationEventHandler)this.eventHandling.EventHandler;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001FE4 RID: 8164 RVA: 0x000AC124 File Offset: 0x000AA324
		// (set) Token: 0x06001FE5 RID: 8165 RVA: 0x000AC12C File Offset: 0x000AA32C
		public SchemaInfo SchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001FE6 RID: 8166 RVA: 0x000AC135 File Offset: 0x000AA335
		// (set) Token: 0x06001FE7 RID: 8167 RVA: 0x000AC140 File Offset: 0x000AA340
		public IDtdInfo DtdInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				SchemaInfo schemaInfo = value as SchemaInfo;
				if (schemaInfo == null)
				{
					throw new XmlException("Xml_InternalError", string.Empty);
				}
				this.schemaInfo = schemaInfo;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001FE8 RID: 8168 RVA: 0x000AC16E File Offset: 0x000AA36E
		public virtual bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000AC171 File Offset: 0x000AA371
		public virtual void Validate()
		{
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000AC173 File Offset: 0x000AA373
		public virtual void CompleteValidation()
		{
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x000AC175 File Offset: 0x000AA375
		public virtual object FindId(string name)
		{
			return null;
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x000AC178 File Offset: 0x000AA378
		public void ValidateText()
		{
			if (this.context.NeedValidateChildren)
			{
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Sch_ContentInNill", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
					return;
				}
				ContentValidator contentValidator = this.context.ElementDecl.ContentValidator;
				XmlSchemaContentType contentType = contentValidator.ContentType;
				if (contentType == XmlSchemaContentType.ElementOnly)
				{
					ArrayList arrayList = contentValidator.ExpectedElements(this.context, false);
					if (arrayList == null)
					{
						this.SendValidationEvent("Sch_InvalidTextInElement", XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace));
					}
					else
					{
						this.SendValidationEvent("Sch_InvalidTextInElementExpecting", new string[]
						{
							XmlSchemaValidator.BuildElementName(this.context.LocalName, this.context.Namespace),
							XmlSchemaValidator.PrintExpectedElements(arrayList, false)
						});
					}
				}
				else if (contentType == XmlSchemaContentType.Empty)
				{
					this.SendValidationEvent("Sch_InvalidTextInEmpty", string.Empty);
				}
				if (this.checkDatatype)
				{
					this.SaveTextValue(this.reader.Value);
				}
			}
		}

		// Token: 0x06001FED RID: 8173 RVA: 0x000AC288 File Offset: 0x000AA488
		public void ValidateWhitespace()
		{
			if (this.context.NeedValidateChildren)
			{
				XmlSchemaContentType contentType = this.context.ElementDecl.ContentValidator.ContentType;
				if (this.context.IsNill)
				{
					this.SendValidationEvent("Sch_ContentInNill", XmlSchemaValidator.QNameString(this.context.LocalName, this.context.Namespace));
				}
				if (contentType == XmlSchemaContentType.Empty)
				{
					this.SendValidationEvent("Sch_InvalidWhitespaceInEmpty", string.Empty);
				}
				if (this.checkDatatype)
				{
					this.SaveTextValue(this.reader.Value);
				}
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x000AC318 File Offset: 0x000AA518
		private void SaveTextValue(string value)
		{
			if (this.textString.Length == 0)
			{
				this.textString = value;
				return;
			}
			if (!this.hasSibling)
			{
				this.textValue.Append(this.textString);
				this.hasSibling = true;
			}
			this.textValue.Append(value);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000AC368 File Offset: 0x000AA568
		protected void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x000AC376 File Offset: 0x000AA576
		protected void SendValidationEvent(string code, string[] args)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000AC3A6 File Offset: 0x000AA5A6
		protected void SendValidationEvent(string code, string arg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, arg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000AC3D6 File Offset: 0x000AA5D6
		protected void SendValidationEvent(string code, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				arg1,
				arg2
			}, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000AC413 File Offset: 0x000AA613
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x000AC41D File Offset: 0x000AA61D
		protected void SendValidationEvent(string code, string msg, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x000AC44E File Offset: 0x000AA64E
		protected void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x000AC47F File Offset: 0x000AA67F
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (this.eventHandling != null)
			{
				this.eventHandling.SendEvent(e, severity);
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x06001FF7 RID: 8183 RVA: 0x000AC49C File Offset: 0x000AA69C
		protected static void ProcessEntity(SchemaInfo sinfo, string name, object sender, ValidationEventHandler eventhandler, string baseUri, int lineNumber, int linePosition)
		{
			XmlSchemaException ex = null;
			SchemaEntity schemaEntity;
			if (!sinfo.GeneralEntities.TryGetValue(new XmlQualifiedName(name), out schemaEntity))
			{
				ex = new XmlSchemaException("Sch_UndeclaredEntity", name, baseUri, lineNumber, linePosition);
			}
			else if (schemaEntity.NData.IsEmpty)
			{
				ex = new XmlSchemaException("Sch_UnparsedEntityRef", name, baseUri, lineNumber, linePosition);
			}
			if (ex == null)
			{
				return;
			}
			if (eventhandler != null)
			{
				eventhandler(sender, new ValidationEventArgs(ex));
				return;
			}
			throw ex;
		}

		// Token: 0x06001FF8 RID: 8184 RVA: 0x000AC50C File Offset: 0x000AA70C
		protected static void ProcessEntity(SchemaInfo sinfo, string name, IValidationEventHandling eventHandling, string baseUriStr, int lineNumber, int linePosition)
		{
			string text = null;
			SchemaEntity schemaEntity;
			if (!sinfo.GeneralEntities.TryGetValue(new XmlQualifiedName(name), out schemaEntity))
			{
				text = "Sch_UndeclaredEntity";
			}
			else if (schemaEntity.NData.IsEmpty)
			{
				text = "Sch_UnparsedEntityRef";
			}
			if (text == null)
			{
				return;
			}
			XmlSchemaException ex = new XmlSchemaException(text, name, baseUriStr, lineNumber, linePosition);
			if (eventHandling != null)
			{
				eventHandling.SendEvent(ex, XmlSeverityType.Error);
				return;
			}
			throw ex;
		}

		// Token: 0x06001FF9 RID: 8185 RVA: 0x000AC56C File Offset: 0x000AA76C
		public static BaseValidator CreateInstance(ValidationType valType, XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, IValidationEventHandling eventHandling, bool processIdentityConstraints)
		{
			switch (valType)
			{
			case ValidationType.None:
				return new BaseValidator(reader, schemaCollection, eventHandling);
			case ValidationType.Auto:
				return new AutoValidator(reader, schemaCollection, eventHandling);
			case ValidationType.DTD:
				return new DtdValidator(reader, eventHandling, processIdentityConstraints);
			case ValidationType.XDR:
				return new XdrValidator(reader, schemaCollection, eventHandling);
			case ValidationType.Schema:
				return new XsdValidator(reader, schemaCollection, eventHandling);
			default:
				return null;
			}
		}

		// Token: 0x04000D6B RID: 3435
		private XmlSchemaCollection schemaCollection;

		// Token: 0x04000D6C RID: 3436
		private IValidationEventHandling eventHandling;

		// Token: 0x04000D6D RID: 3437
		private XmlNameTable nameTable;

		// Token: 0x04000D6E RID: 3438
		private SchemaNames schemaNames;

		// Token: 0x04000D6F RID: 3439
		private PositionInfo positionInfo;

		// Token: 0x04000D70 RID: 3440
		private XmlResolver xmlResolver;

		// Token: 0x04000D71 RID: 3441
		private Uri baseUri;

		// Token: 0x04000D72 RID: 3442
		protected SchemaInfo schemaInfo;

		// Token: 0x04000D73 RID: 3443
		protected XmlValidatingReaderImpl reader;

		// Token: 0x04000D74 RID: 3444
		protected XmlQualifiedName elementName;

		// Token: 0x04000D75 RID: 3445
		protected ValidationState context;

		// Token: 0x04000D76 RID: 3446
		protected StringBuilder textValue;

		// Token: 0x04000D77 RID: 3447
		protected string textString;

		// Token: 0x04000D78 RID: 3448
		protected bool hasSibling;

		// Token: 0x04000D79 RID: 3449
		protected bool checkDatatype;
	}
}
