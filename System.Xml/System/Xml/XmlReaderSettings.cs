using System;
using System.Xml.Schema;
using System.Xml.XmlConfiguration;

namespace System.Xml
{
	// Token: 0x0200007E RID: 126
	public sealed class XmlReaderSettings
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x00016DC7 File Offset: 0x00015DC7
		public XmlReaderSettings()
		{
			this.Reset();
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00016DD5 File Offset: 0x00015DD5
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x00016DDD File Offset: 0x00015DDD
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
			set
			{
				this.CheckReadOnly("NameTable");
				this.nameTable = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00016DF1 File Offset: 0x00015DF1
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00016DF9 File Offset: 0x00015DF9
		internal bool IsXmlResolverSet
		{
			get
			{
				return this.isXmlResolverSet;
			}
			private set
			{
				this.isXmlResolverSet = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x00016E02 File Offset: 0x00015E02
		public XmlResolver XmlResolver
		{
			set
			{
				this.CheckReadOnly("XmlResolver");
				this.xmlResolver = value;
				this.IsXmlResolverSet = true;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00016E1D File Offset: 0x00015E1D
		internal XmlResolver GetXmlResolver()
		{
			return this.xmlResolver;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00016E25 File Offset: 0x00015E25
		internal XmlResolver GetXmlResolver_CheckConfig()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver && !this.IsXmlResolverSet)
			{
				return null;
			}
			return this.xmlResolver;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00016E3E File Offset: 0x00015E3E
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00016E46 File Offset: 0x00015E46
		public int LineNumberOffset
		{
			get
			{
				return this.lineNumberOffset;
			}
			set
			{
				this.CheckReadOnly("LineNumberOffset");
				if (this.lineNumberOffset < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.lineNumberOffset = value;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00016E6E File Offset: 0x00015E6E
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00016E76 File Offset: 0x00015E76
		public int LinePositionOffset
		{
			get
			{
				return this.linePositionOffset;
			}
			set
			{
				this.CheckReadOnly("LinePositionOffset");
				if (this.linePositionOffset < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.linePositionOffset = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00016E9E File Offset: 0x00015E9E
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00016EA6 File Offset: 0x00015EA6
		public ConformanceLevel ConformanceLevel
		{
			get
			{
				return this.conformanceLevel;
			}
			set
			{
				this.CheckReadOnly("ConformanceLevel");
				if (value > ConformanceLevel.Document)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.conformanceLevel = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00016EC9 File Offset: 0x00015EC9
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x00016ED1 File Offset: 0x00015ED1
		public bool CheckCharacters
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.CheckReadOnly("CheckCharacters");
				this.checkCharacters = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00016EE5 File Offset: 0x00015EE5
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00016EED File Offset: 0x00015EED
		public long MaxCharactersInDocument
		{
			get
			{
				return this.maxCharactersInDocument;
			}
			set
			{
				this.CheckReadOnly("MaxCharactersInDocument");
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxCharactersInDocument = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00016F11 File Offset: 0x00015F11
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00016F19 File Offset: 0x00015F19
		public long MaxCharactersFromEntities
		{
			get
			{
				return this.maxCharactersFromEntities;
			}
			set
			{
				this.CheckReadOnly("MaxCharactersFromEntities");
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxCharactersFromEntities = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00016F3D File Offset: 0x00015F3D
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00016F45 File Offset: 0x00015F45
		public ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				this.CheckReadOnly("ValidationType");
				if (value > ValidationType.Schema)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.validationType = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x00016F68 File Offset: 0x00015F68
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x00016F70 File Offset: 0x00015F70
		public XmlSchemaValidationFlags ValidationFlags
		{
			get
			{
				return this.validationFlags;
			}
			set
			{
				this.CheckReadOnly("ValidationFlags");
				if (value > (XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.validationFlags = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x00016F94 File Offset: 0x00015F94
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x00016FAF File Offset: 0x00015FAF
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaSet();
				}
				return this.schemas;
			}
			set
			{
				this.CheckReadOnly("Schemas");
				this.schemas = value;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060005B8 RID: 1464 RVA: 0x00016FC3 File Offset: 0x00015FC3
		// (remove) Token: 0x060005B9 RID: 1465 RVA: 0x00016FE7 File Offset: 0x00015FE7
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.CheckReadOnly("ValidationEventHandler");
				this.valEventHandler = (ValidationEventHandler)Delegate.Combine(this.valEventHandler, value);
			}
			remove
			{
				this.CheckReadOnly("ValidationEventHandler");
				this.valEventHandler = (ValidationEventHandler)Delegate.Remove(this.valEventHandler, value);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001700B File Offset: 0x0001600B
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00017013 File Offset: 0x00016013
		public bool IgnoreWhitespace
		{
			get
			{
				return this.ignoreWhitespace;
			}
			set
			{
				this.CheckReadOnly("IgnoreWhitespace");
				this.ignoreWhitespace = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00017027 File Offset: 0x00016027
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0001702F File Offset: 0x0001602F
		public bool IgnoreProcessingInstructions
		{
			get
			{
				return this.ignorePIs;
			}
			set
			{
				this.CheckReadOnly("IgnoreProcessingInstructions");
				this.ignorePIs = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x00017043 File Offset: 0x00016043
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0001704B File Offset: 0x0001604B
		public bool IgnoreComments
		{
			get
			{
				return this.ignoreComments;
			}
			set
			{
				this.CheckReadOnly("IgnoreComments");
				this.ignoreComments = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0001705F File Offset: 0x0001605F
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x00017067 File Offset: 0x00016067
		public bool ProhibitDtd
		{
			get
			{
				return this.prohibitDtd;
			}
			set
			{
				this.CheckReadOnly("ProhibitDtd");
				this.prohibitDtd = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0001707B File Offset: 0x0001607B
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00017083 File Offset: 0x00016083
		public bool CloseInput
		{
			get
			{
				return this.closeInput;
			}
			set
			{
				this.CheckReadOnly("CloseInput");
				this.closeInput = value;
			}
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00017098 File Offset: 0x00016098
		public void Reset()
		{
			this.CheckReadOnly("Reset");
			this.nameTable = null;
			this.xmlResolver = XmlReaderSettings.CreateDefaultResolver();
			this.lineNumberOffset = 0;
			this.linePositionOffset = 0;
			this.checkCharacters = true;
			this.conformanceLevel = ConformanceLevel.Document;
			this.schemas = null;
			this.validationType = ValidationType.None;
			this.validationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints;
			this.validationFlags |= XmlSchemaValidationFlags.AllowXmlAttributes;
			this.ignoreWhitespace = false;
			this.ignorePIs = false;
			this.ignoreComments = false;
			this.prohibitDtd = true;
			this.closeInput = false;
			this.maxCharactersFromEntities = 0L;
			this.maxCharactersInDocument = 0L;
			this.isReadOnly = false;
			this.IsXmlResolverSet = false;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00017143 File Offset: 0x00016143
		private static XmlResolver CreateDefaultResolver()
		{
			return new XmlUrlResolver();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001714C File Offset: 0x0001614C
		public XmlReaderSettings Clone()
		{
			XmlReaderSettings xmlReaderSettings = base.MemberwiseClone() as XmlReaderSettings;
			xmlReaderSettings.isReadOnly = false;
			return xmlReaderSettings;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0001716D File Offset: 0x0001616D
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00017175 File Offset: 0x00016175
		internal bool ReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0001717E File Offset: 0x0001617E
		internal ValidationEventHandler GetEventHandler()
		{
			return this.valEventHandler;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00017186 File Offset: 0x00016186
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("Xml_ReadOnlyProperty", "XmlReaderSettings." + propertyName);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x000171A6 File Offset: 0x000161A6
		internal bool CanResolveExternals
		{
			get
			{
				return !this.prohibitDtd && this.xmlResolver != null;
			}
		}

		// Token: 0x0400063F RID: 1599
		private XmlNameTable nameTable;

		// Token: 0x04000640 RID: 1600
		private XmlResolver xmlResolver;

		// Token: 0x04000641 RID: 1601
		private int lineNumberOffset;

		// Token: 0x04000642 RID: 1602
		private int linePositionOffset;

		// Token: 0x04000643 RID: 1603
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000644 RID: 1604
		private bool checkCharacters;

		// Token: 0x04000645 RID: 1605
		private long maxCharactersInDocument;

		// Token: 0x04000646 RID: 1606
		private long maxCharactersFromEntities;

		// Token: 0x04000647 RID: 1607
		private ValidationType validationType;

		// Token: 0x04000648 RID: 1608
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x04000649 RID: 1609
		private XmlSchemaSet schemas;

		// Token: 0x0400064A RID: 1610
		private ValidationEventHandler valEventHandler;

		// Token: 0x0400064B RID: 1611
		private bool ignoreWhitespace;

		// Token: 0x0400064C RID: 1612
		private bool ignorePIs;

		// Token: 0x0400064D RID: 1613
		private bool ignoreComments;

		// Token: 0x0400064E RID: 1614
		private bool prohibitDtd;

		// Token: 0x0400064F RID: 1615
		private bool closeInput;

		// Token: 0x04000650 RID: 1616
		private bool isReadOnly;

		// Token: 0x04000651 RID: 1617
		private bool isXmlResolverSet;
	}
}
