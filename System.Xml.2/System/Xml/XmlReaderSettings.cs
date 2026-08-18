using System;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Xml.Schema;
using System.Xml.XmlConfiguration;
using Microsoft.Win32;

namespace System.Xml
{
	// Token: 0x020000D4 RID: 212
	[__DynamicallyInvokable]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public sealed class XmlReaderSettings
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x00022744 File Offset: 0x00020944
		[__DynamicallyInvokable]
		public XmlReaderSettings()
		{
			this.Initialize();
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00022752 File Offset: 0x00020952
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public XmlReaderSettings(XmlResolver resolver)
		{
			this.Initialize(resolver);
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00022761 File Offset: 0x00020961
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00022769 File Offset: 0x00020969
		[__DynamicallyInvokable]
		public bool Async
		{
			[__DynamicallyInvokable]
			get
			{
				return this.useAsync;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("Async");
				this.useAsync = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0002277D File Offset: 0x0002097D
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00022785 File Offset: 0x00020985
		[__DynamicallyInvokable]
		public XmlNameTable NameTable
		{
			[__DynamicallyInvokable]
			get
			{
				return this.nameTable;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("NameTable");
				this.nameTable = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00022799 File Offset: 0x00020999
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x000227A1 File Offset: 0x000209A1
		internal bool IsXmlResolverSet { get; set; }

		// Token: 0x170001C5 RID: 453
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x000227AA File Offset: 0x000209AA
		public XmlResolver XmlResolver
		{
			set
			{
				this.CheckReadOnly("XmlResolver");
				this.xmlResolver = value;
				this.IsXmlResolverSet = true;
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000227C5 File Offset: 0x000209C5
		internal XmlResolver GetXmlResolver()
		{
			return this.xmlResolver;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000227CD File Offset: 0x000209CD
		internal XmlResolver GetXmlResolver_CheckConfig()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver && !this.IsXmlResolverSet)
			{
				return null;
			}
			return this.xmlResolver;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x000227E6 File Offset: 0x000209E6
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x000227EE File Offset: 0x000209EE
		[__DynamicallyInvokable]
		public int LineNumberOffset
		{
			[__DynamicallyInvokable]
			get
			{
				return this.lineNumberOffset;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("LineNumberOffset");
				this.lineNumberOffset = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00022802 File Offset: 0x00020A02
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0002280A File Offset: 0x00020A0A
		[__DynamicallyInvokable]
		public int LinePositionOffset
		{
			[__DynamicallyInvokable]
			get
			{
				return this.linePositionOffset;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("LinePositionOffset");
				this.linePositionOffset = value;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x0002281E File Offset: 0x00020A1E
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x00022826 File Offset: 0x00020A26
		[__DynamicallyInvokable]
		public ConformanceLevel ConformanceLevel
		{
			[__DynamicallyInvokable]
			get
			{
				return this.conformanceLevel;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00022849 File Offset: 0x00020A49
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00022851 File Offset: 0x00020A51
		[__DynamicallyInvokable]
		public bool CheckCharacters
		{
			[__DynamicallyInvokable]
			get
			{
				return this.checkCharacters;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("CheckCharacters");
				this.checkCharacters = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00022865 File Offset: 0x00020A65
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0002286D File Offset: 0x00020A6D
		[__DynamicallyInvokable]
		public long MaxCharactersInDocument
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxCharactersInDocument;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x00022891 File Offset: 0x00020A91
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x00022899 File Offset: 0x00020A99
		[__DynamicallyInvokable]
		public long MaxCharactersFromEntities
		{
			[__DynamicallyInvokable]
			get
			{
				return this.maxCharactersFromEntities;
			}
			[__DynamicallyInvokable]
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

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000228BD File Offset: 0x00020ABD
		// (set) Token: 0x06000A1B RID: 2587 RVA: 0x000228C5 File Offset: 0x00020AC5
		[__DynamicallyInvokable]
		public bool IgnoreWhitespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ignoreWhitespace;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("IgnoreWhitespace");
				this.ignoreWhitespace = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x000228D9 File Offset: 0x00020AD9
		// (set) Token: 0x06000A1D RID: 2589 RVA: 0x000228E1 File Offset: 0x00020AE1
		[__DynamicallyInvokable]
		public bool IgnoreProcessingInstructions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ignorePIs;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("IgnoreProcessingInstructions");
				this.ignorePIs = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x000228F5 File Offset: 0x00020AF5
		// (set) Token: 0x06000A1F RID: 2591 RVA: 0x000228FD File Offset: 0x00020AFD
		[__DynamicallyInvokable]
		public bool IgnoreComments
		{
			[__DynamicallyInvokable]
			get
			{
				return this.ignoreComments;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("IgnoreComments");
				this.ignoreComments = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00022911 File Offset: 0x00020B11
		// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0002291C File Offset: 0x00020B1C
		[Obsolete("Use XmlReaderSettings.DtdProcessing property instead.")]
		public bool ProhibitDtd
		{
			get
			{
				return this.dtdProcessing == DtdProcessing.Prohibit;
			}
			set
			{
				this.CheckReadOnly("ProhibitDtd");
				this.dtdProcessing = (value ? DtdProcessing.Prohibit : DtdProcessing.Parse);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00022936 File Offset: 0x00020B36
		// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0002293E File Offset: 0x00020B3E
		[__DynamicallyInvokable]
		public DtdProcessing DtdProcessing
		{
			[__DynamicallyInvokable]
			get
			{
				return this.dtdProcessing;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("DtdProcessing");
				if (value > DtdProcessing.Parse)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.dtdProcessing = value;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00022961 File Offset: 0x00020B61
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00022969 File Offset: 0x00020B69
		[__DynamicallyInvokable]
		public bool CloseInput
		{
			[__DynamicallyInvokable]
			get
			{
				return this.closeInput;
			}
			[__DynamicallyInvokable]
			set
			{
				this.CheckReadOnly("CloseInput");
				this.closeInput = value;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0002297D File Offset: 0x00020B7D
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00022985 File Offset: 0x00020B85
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

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x000229A8 File Offset: 0x00020BA8
		// (set) Token: 0x06000A29 RID: 2601 RVA: 0x000229B0 File Offset: 0x00020BB0
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

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x000229D4 File Offset: 0x00020BD4
		// (set) Token: 0x06000A2B RID: 2603 RVA: 0x000229EF File Offset: 0x00020BEF
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
		// (add) Token: 0x06000A2C RID: 2604 RVA: 0x00022A03 File Offset: 0x00020C03
		// (remove) Token: 0x06000A2D RID: 2605 RVA: 0x00022A27 File Offset: 0x00020C27
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

		// Token: 0x06000A2E RID: 2606 RVA: 0x00022A4B File Offset: 0x00020C4B
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.CheckReadOnly("Reset");
			this.Initialize();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00022A60 File Offset: 0x00020C60
		[__DynamicallyInvokable]
		public XmlReaderSettings Clone()
		{
			XmlReaderSettings xmlReaderSettings = base.MemberwiseClone() as XmlReaderSettings;
			xmlReaderSettings.ReadOnly = false;
			return xmlReaderSettings;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00022A81 File Offset: 0x00020C81
		internal ValidationEventHandler GetEventHandler()
		{
			return this.valEventHandler;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00022A8C File Offset: 0x00020C8C
		internal XmlReader CreateReader(string inputUri, XmlParserContext inputContext)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (inputUri.Length == 0)
			{
				throw new ArgumentException(Res.GetString("XmlConvert_BadUri"), "inputUri");
			}
			XmlResolver xmlResolver = this.GetXmlResolver();
			if (xmlResolver == null)
			{
				xmlResolver = XmlReaderSettings.CreateDefaultResolver();
			}
			XmlReader xmlReader = new XmlTextReaderImpl(inputUri, this, inputContext, xmlResolver);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00022B00 File Offset: 0x00020D00
		internal XmlReader CreateReader(Stream input, Uri baseUri, string baseUriString, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (baseUriString == null)
			{
				if (baseUri == null)
				{
					baseUriString = string.Empty;
				}
				else
				{
					baseUriString = baseUri.ToString();
				}
			}
			XmlReader xmlReader = new XmlTextReaderImpl(input, null, 0, this, baseUri, baseUriString, inputContext, this.closeInput);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00022B6C File Offset: 0x00020D6C
		internal XmlReader CreateReader(TextReader input, string baseUriString, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (baseUriString == null)
			{
				baseUriString = string.Empty;
			}
			XmlReader xmlReader = new XmlTextReaderImpl(input, this, baseUriString, inputContext);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00022BBB File Offset: 0x00020DBB
		internal XmlReader CreateReader(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return this.AddValidationAndConformanceWrapper(reader);
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00022BD2 File Offset: 0x00020DD2
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x00022BDA File Offset: 0x00020DDA
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

		// Token: 0x06000A37 RID: 2615 RVA: 0x00022BE3 File Offset: 0x00020DE3
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("Xml_ReadOnlyProperty", base.GetType().Name + "." + propertyName);
			}
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00022C0E File Offset: 0x00020E0E
		private void Initialize()
		{
			this.Initialize(null);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x00022C18 File Offset: 0x00020E18
		private void Initialize(XmlResolver resolver)
		{
			this.nameTable = null;
			if (!XmlReaderSettings.EnableLegacyXmlSettings())
			{
				this.xmlResolver = resolver;
				this.maxCharactersFromEntities = 10000000L;
			}
			else
			{
				this.xmlResolver = ((resolver == null) ? XmlReaderSettings.CreateDefaultResolver() : resolver);
				this.maxCharactersFromEntities = 0L;
			}
			this.lineNumberOffset = 0;
			this.linePositionOffset = 0;
			this.checkCharacters = true;
			this.conformanceLevel = ConformanceLevel.Document;
			this.ignoreWhitespace = false;
			this.ignorePIs = false;
			this.ignoreComments = false;
			this.dtdProcessing = DtdProcessing.Prohibit;
			this.closeInput = false;
			this.maxCharactersInDocument = 0L;
			this.schemas = null;
			this.validationType = ValidationType.None;
			this.validationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints;
			this.validationFlags |= XmlSchemaValidationFlags.AllowXmlAttributes;
			this.useAsync = false;
			this.isReadOnly = false;
			this.IsXmlResolverSet = false;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00022CE1 File Offset: 0x00020EE1
		private static XmlResolver CreateDefaultResolver()
		{
			return new XmlUrlResolver();
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00022CE8 File Offset: 0x00020EE8
		internal XmlReader AddValidation(XmlReader reader)
		{
			if (this.validationType == ValidationType.Schema)
			{
				XmlResolver xmlResolver = this.GetXmlResolver_CheckConfig();
				if (xmlResolver == null && !this.IsXmlResolverSet && !XmlReaderSettings.EnableLegacyXmlSettings())
				{
					xmlResolver = new XmlUrlResolver();
				}
				reader = new XsdValidatingReader(reader, xmlResolver, this);
			}
			else if (this.validationType == ValidationType.DTD)
			{
				reader = this.CreateDtdValidatingReader(reader);
			}
			return reader;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00022D3C File Offset: 0x00020F3C
		private XmlReader AddValidationAndConformanceWrapper(XmlReader reader)
		{
			if (this.validationType == ValidationType.DTD)
			{
				reader = this.CreateDtdValidatingReader(reader);
			}
			reader = this.AddConformanceWrapper(reader);
			if (this.validationType == ValidationType.Schema)
			{
				reader = new XsdValidatingReader(reader, this.GetXmlResolver_CheckConfig(), this);
			}
			return reader;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00022D72 File Offset: 0x00020F72
		private XmlValidatingReaderImpl CreateDtdValidatingReader(XmlReader baseReader)
		{
			return new XmlValidatingReaderImpl(baseReader, this.GetEventHandler(), (this.ValidationFlags & XmlSchemaValidationFlags.ProcessIdentityConstraints) > XmlSchemaValidationFlags.None);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00022D8C File Offset: 0x00020F8C
		internal XmlReader AddConformanceWrapper(XmlReader baseReader)
		{
			XmlReaderSettings settings = baseReader.Settings;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool ignorePis = false;
			DtdProcessing dtdProcessing = (DtdProcessing)(-1);
			bool flag4 = false;
			if (settings == null)
			{
				if (this.conformanceLevel != ConformanceLevel.Auto && this.conformanceLevel != XmlReader.GetV1ConformanceLevel(baseReader))
				{
					throw new InvalidOperationException(Res.GetString("Xml_IncompatibleConformanceLevel", new object[]
					{
						this.conformanceLevel.ToString()
					}));
				}
				XmlTextReader xmlTextReader = baseReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					XmlValidatingReader xmlValidatingReader = baseReader as XmlValidatingReader;
					if (xmlValidatingReader != null)
					{
						xmlTextReader = (XmlTextReader)xmlValidatingReader.Reader;
					}
				}
				if (this.ignoreWhitespace)
				{
					WhitespaceHandling whitespaceHandling = WhitespaceHandling.All;
					if (xmlTextReader != null)
					{
						whitespaceHandling = xmlTextReader.WhitespaceHandling;
					}
					if (whitespaceHandling == WhitespaceHandling.All)
					{
						flag2 = true;
						flag4 = true;
					}
				}
				if (this.ignoreComments)
				{
					flag3 = true;
					flag4 = true;
				}
				if (this.ignorePIs)
				{
					ignorePis = true;
					flag4 = true;
				}
				DtdProcessing dtdProcessing2 = DtdProcessing.Parse;
				if (xmlTextReader != null)
				{
					dtdProcessing2 = xmlTextReader.DtdProcessing;
				}
				if ((this.dtdProcessing == DtdProcessing.Prohibit && dtdProcessing2 != DtdProcessing.Prohibit) || (this.dtdProcessing == DtdProcessing.Ignore && dtdProcessing2 == DtdProcessing.Parse))
				{
					dtdProcessing = this.dtdProcessing;
					flag4 = true;
				}
			}
			else
			{
				if (this.conformanceLevel != settings.ConformanceLevel && this.conformanceLevel != ConformanceLevel.Auto)
				{
					throw new InvalidOperationException(Res.GetString("Xml_IncompatibleConformanceLevel", new object[]
					{
						this.conformanceLevel.ToString()
					}));
				}
				if (this.checkCharacters && !settings.CheckCharacters)
				{
					flag = true;
					flag4 = true;
				}
				if (this.ignoreWhitespace && !settings.IgnoreWhitespace)
				{
					flag2 = true;
					flag4 = true;
				}
				if (this.ignoreComments && !settings.IgnoreComments)
				{
					flag3 = true;
					flag4 = true;
				}
				if (this.ignorePIs && !settings.IgnoreProcessingInstructions)
				{
					ignorePis = true;
					flag4 = true;
				}
				if ((this.dtdProcessing == DtdProcessing.Prohibit && settings.DtdProcessing != DtdProcessing.Prohibit) || (this.dtdProcessing == DtdProcessing.Ignore && settings.DtdProcessing == DtdProcessing.Parse))
				{
					dtdProcessing = this.dtdProcessing;
					flag4 = true;
				}
			}
			if (!flag4)
			{
				return baseReader;
			}
			IXmlNamespaceResolver xmlNamespaceResolver = baseReader as IXmlNamespaceResolver;
			if (xmlNamespaceResolver != null)
			{
				return new XmlCharCheckingReaderWithNS(baseReader, xmlNamespaceResolver, flag, flag2, flag3, ignorePis, dtdProcessing);
			}
			return new XmlCharCheckingReader(baseReader, flag, flag2, flag3, ignorePis, dtdProcessing);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00022F88 File Offset: 0x00021188
		internal static bool EnableLegacyXmlSettings()
		{
			if (XmlReaderSettings.s_enableLegacyXmlSettings != null)
			{
				return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
			}
			if (!BinaryCompatibility.TargetsAtLeast_Desktop_V4_5_2)
			{
				XmlReaderSettings.s_enableLegacyXmlSettings = new bool?(true);
				return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
			}
			bool value = false;
			if (!XmlReaderSettings.ReadSettingsFromRegistry(Registry.LocalMachine, ref value))
			{
				XmlReaderSettings.ReadSettingsFromRegistry(Registry.CurrentUser, ref value);
			}
			XmlReaderSettings.s_enableLegacyXmlSettings = new bool?(value);
			return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00022FFC File Offset: 0x000211FC
		[SecuritySafeCritical]
		[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool ReadSettingsFromRegistry(RegistryKey hive, ref bool value)
		{
			try
			{
				using (RegistryKey registryKey = hive.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\XML", false))
				{
					if (registryKey != null && registryKey.GetValueKind("EnableLegacyXmlSettings") == RegistryValueKind.DWord)
					{
						value = ((int)registryKey.GetValue("EnableLegacyXmlSettings") == 1);
						return true;
					}
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x0400033B RID: 827
		private bool useAsync;

		// Token: 0x0400033C RID: 828
		private XmlNameTable nameTable;

		// Token: 0x0400033D RID: 829
		private XmlResolver xmlResolver;

		// Token: 0x0400033E RID: 830
		private int lineNumberOffset;

		// Token: 0x0400033F RID: 831
		private int linePositionOffset;

		// Token: 0x04000340 RID: 832
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000341 RID: 833
		private bool checkCharacters;

		// Token: 0x04000342 RID: 834
		private long maxCharactersInDocument;

		// Token: 0x04000343 RID: 835
		private long maxCharactersFromEntities;

		// Token: 0x04000344 RID: 836
		private bool ignoreWhitespace;

		// Token: 0x04000345 RID: 837
		private bool ignorePIs;

		// Token: 0x04000346 RID: 838
		private bool ignoreComments;

		// Token: 0x04000347 RID: 839
		private DtdProcessing dtdProcessing;

		// Token: 0x04000348 RID: 840
		private ValidationType validationType;

		// Token: 0x04000349 RID: 841
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x0400034A RID: 842
		private XmlSchemaSet schemas;

		// Token: 0x0400034B RID: 843
		private ValidationEventHandler valEventHandler;

		// Token: 0x0400034C RID: 844
		private bool closeInput;

		// Token: 0x0400034D RID: 845
		private bool isReadOnly;

		// Token: 0x0400034F RID: 847
		private static bool? s_enableLegacyXmlSettings;
	}
}
