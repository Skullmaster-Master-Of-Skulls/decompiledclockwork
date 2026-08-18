using System;
using System.Collections;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000183 RID: 387
	internal class BaseValidator
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x00057780 File Offset: 0x00056780
		public BaseValidator(BaseValidator other)
		{
			this.reader = other.reader;
			this.schemaCollection = other.schemaCollection;
			this.eventHandler = other.eventHandler;
			this.nameTable = other.nameTable;
			this.schemaNames = other.schemaNames;
			this.positionInfo = other.positionInfo;
			this.xmlResolver = other.xmlResolver;
			this.baseUri = other.baseUri;
			this.elementName = other.elementName;
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000577FF File Offset: 0x000567FF
		public BaseValidator(XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, ValidationEventHandler eventHandler)
		{
			this.reader = reader;
			this.schemaCollection = schemaCollection;
			this.eventHandler = eventHandler;
			this.nameTable = reader.NameTable;
			this.positionInfo = PositionInfo.GetPositionInfo(reader);
			this.elementName = new XmlQualifiedName();
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0005783F File Offset: 0x0005683F
		public XmlValidatingReaderImpl Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x00057847 File Offset: 0x00056847
		public XmlSchemaCollection SchemaCollection
		{
			get
			{
				return this.schemaCollection;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x0005784F File Offset: 0x0005684F
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x00057858 File Offset: 0x00056858
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

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x000578AC File Offset: 0x000568AC
		public PositionInfo PositionInfo
		{
			get
			{
				return this.positionInfo;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x000578B4 File Offset: 0x000568B4
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x000578BC File Offset: 0x000568BC
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

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x000578C5 File Offset: 0x000568C5
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x000578CD File Offset: 0x000568CD
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

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x000578D6 File Offset: 0x000568D6
		// (set) Token: 0x0600147B RID: 5243 RVA: 0x000578DE File Offset: 0x000568DE
		public ValidationEventHandler EventHandler
		{
			get
			{
				return this.eventHandler;
			}
			set
			{
				this.eventHandler = value;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x000578E7 File Offset: 0x000568E7
		// (set) Token: 0x0600147D RID: 5245 RVA: 0x000578EF File Offset: 0x000568EF
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

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x000578F8 File Offset: 0x000568F8
		public virtual bool PreserveWhitespace
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000578FB File Offset: 0x000568FB
		public virtual void Validate()
		{
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000578FD File Offset: 0x000568FD
		public virtual void CompleteValidation()
		{
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x000578FF File Offset: 0x000568FF
		public virtual object FindId(string name)
		{
			return null;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00057904 File Offset: 0x00056904
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

		// Token: 0x06001483 RID: 5251 RVA: 0x00057A14 File Offset: 0x00056A14
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
			}
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00057A8C File Offset: 0x00056A8C
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

		// Token: 0x06001485 RID: 5253 RVA: 0x00057ADC File Offset: 0x00056ADC
		protected void SendValidationEvent(string code)
		{
			this.SendValidationEvent(code, string.Empty);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00057AEA File Offset: 0x00056AEA
		protected void SendValidationEvent(string code, string[] args)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00057B1A File Offset: 0x00056B1A
		protected void SendValidationEvent(string code, string arg)
		{
			this.SendValidationEvent(new XmlSchemaException(code, arg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00057B4C File Offset: 0x00056B4C
		protected void SendValidationEvent(string code, string arg1, string arg2)
		{
			this.SendValidationEvent(new XmlSchemaException(code, new string[]
			{
				arg1,
				arg2
			}, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition));
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00057B96 File Offset: 0x00056B96
		protected void SendValidationEvent(XmlSchemaException e)
		{
			this.SendValidationEvent(e, XmlSeverityType.Error);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00057BA0 File Offset: 0x00056BA0
		protected void SendValidationEvent(string code, string msg, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, msg, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00057BD1 File Offset: 0x00056BD1
		protected void SendValidationEvent(string code, string[] args, XmlSeverityType severity)
		{
			this.SendValidationEvent(new XmlSchemaException(code, args, this.reader.BaseURI, this.positionInfo.LineNumber, this.positionInfo.LinePosition), severity);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00057C02 File Offset: 0x00056C02
		protected void SendValidationEvent(XmlSchemaException e, XmlSeverityType severity)
		{
			if (this.eventHandler != null)
			{
				this.eventHandler(this.reader, new ValidationEventArgs(e, severity));
				return;
			}
			if (severity == XmlSeverityType.Error)
			{
				throw e;
			}
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00057C2C File Offset: 0x00056C2C
		protected static void ProcessEntity(SchemaInfo sinfo, string name, object sender, ValidationEventHandler eventhandler, string baseUri, int lineNumber, int linePosition)
		{
			SchemaEntity schemaEntity = (SchemaEntity)sinfo.GeneralEntities[new XmlQualifiedName(name)];
			XmlSchemaException ex = null;
			if (schemaEntity == null)
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

		// Token: 0x0600148E RID: 5262 RVA: 0x00057CA0 File Offset: 0x00056CA0
		public static BaseValidator CreateInstance(ValidationType valType, XmlValidatingReaderImpl reader, XmlSchemaCollection schemaCollection, ValidationEventHandler eventHandler, bool processIdentityConstraints)
		{
			switch (valType)
			{
			case ValidationType.None:
				return new BaseValidator(reader, schemaCollection, eventHandler);
			case ValidationType.Auto:
				return new AutoValidator(reader, schemaCollection, eventHandler);
			case ValidationType.DTD:
				return new DtdValidator(reader, eventHandler, processIdentityConstraints);
			case ValidationType.XDR:
				return new XdrValidator(reader, schemaCollection, eventHandler);
			case ValidationType.Schema:
				return new XsdValidator(reader, schemaCollection, eventHandler);
			default:
				return null;
			}
		}

		// Token: 0x04000C6A RID: 3178
		private XmlSchemaCollection schemaCollection;

		// Token: 0x04000C6B RID: 3179
		private ValidationEventHandler eventHandler;

		// Token: 0x04000C6C RID: 3180
		private XmlNameTable nameTable;

		// Token: 0x04000C6D RID: 3181
		private SchemaNames schemaNames;

		// Token: 0x04000C6E RID: 3182
		private PositionInfo positionInfo;

		// Token: 0x04000C6F RID: 3183
		private XmlResolver xmlResolver;

		// Token: 0x04000C70 RID: 3184
		private Uri baseUri;

		// Token: 0x04000C71 RID: 3185
		protected SchemaInfo schemaInfo;

		// Token: 0x04000C72 RID: 3186
		protected XmlValidatingReaderImpl reader;

		// Token: 0x04000C73 RID: 3187
		protected XmlQualifiedName elementName;

		// Token: 0x04000C74 RID: 3188
		protected ValidationState context;

		// Token: 0x04000C75 RID: 3189
		protected StringBuilder textValue;

		// Token: 0x04000C76 RID: 3190
		protected string textString;

		// Token: 0x04000C77 RID: 3191
		protected bool hasSibling;

		// Token: 0x04000C78 RID: 3192
		protected bool checkDatatype;
	}
}
