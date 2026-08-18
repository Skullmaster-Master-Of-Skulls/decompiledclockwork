using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001AC RID: 428
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "categories", Namespace = "http://www.w3.org/2007/app")]
	public class AtomPub10CategoriesDocumentFormatter : CategoriesDocumentFormatter, IXmlSerializable
	{
		// Token: 0x06000E13 RID: 3603 RVA: 0x00032804 File Offset: 0x00030A04
		public AtomPub10CategoriesDocumentFormatter() : this(typeof(InlineCategoriesDocument), typeof(ReferencedCategoriesDocument))
		{
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00032820 File Offset: 0x00030A20
		public AtomPub10CategoriesDocumentFormatter(Type inlineDocumentType, Type referencedDocumentType)
		{
			if (inlineDocumentType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inlineDocumentType");
			}
			if (!typeof(InlineCategoriesDocument).IsAssignableFrom(inlineDocumentType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("inlineDocumentType", SR.GetString("InvalidObjectTypePassed", new object[]
				{
					"inlineDocumentType",
					"InlineCategoriesDocument"
				}));
			}
			if (referencedDocumentType == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("referencedDocumentType");
			}
			if (!typeof(ReferencedCategoriesDocument).IsAssignableFrom(referencedDocumentType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("referencedDocumentType", SR.GetString("InvalidObjectTypePassed", new object[]
				{
					"referencedDocumentType",
					"ReferencedCategoriesDocument"
				}));
			}
			this.maxExtensionSize = int.MaxValue;
			this.preserveAttributeExtensions = true;
			this.preserveElementExtensions = true;
			this.inlineDocumentType = inlineDocumentType;
			this.referencedDocumentType = referencedDocumentType;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00032910 File Offset: 0x00030B10
		public AtomPub10CategoriesDocumentFormatter(CategoriesDocument documentToWrite) : base(documentToWrite)
		{
			this.maxExtensionSize = int.MaxValue;
			this.preserveAttributeExtensions = true;
			this.preserveElementExtensions = true;
			if (documentToWrite.IsInline)
			{
				this.inlineDocumentType = documentToWrite.GetType();
				this.referencedDocumentType = typeof(ReferencedCategoriesDocument);
				return;
			}
			this.referencedDocumentType = documentToWrite.GetType();
			this.inlineDocumentType = typeof(InlineCategoriesDocument);
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0003297E File Offset: 0x00030B7E
		public override string Version
		{
			get
			{
				return "http://www.w3.org/2007/app";
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00032985 File Offset: 0x00030B85
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("categories", "http://www.w3.org/2007/app");
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x000329AA File Offset: 0x00030BAA
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000329AD File Offset: 0x00030BAD
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			AtomPub10CategoriesDocumentFormatter.TraceCategoriesDocumentReadBegin();
			this.ReadDocument(reader);
			AtomPub10CategoriesDocumentFormatter.TraceCategoriesDocumentReadEnd();
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000329D4 File Offset: 0x00030BD4
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (base.Document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("DocumentFormatterDoesNotHaveDocument")));
			}
			AtomPub10CategoriesDocumentFormatter.TraceCategoriesDocumentWriteBegin();
			this.WriteDocument(writer);
			AtomPub10CategoriesDocumentFormatter.TraceCategoriesDocumentWriteEnd();
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00032A28 File Offset: 0x00030C28
		public override void ReadFrom(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			if (!this.CanRead(reader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnknownDocumentXml", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			AtomPub10CategoriesDocumentFormatter.TraceCategoriesDocumentReadBegin();
			this.ReadDocument(reader);
			AtomPub10CategoriesDocumentFormatter.TraceCategoriesDocumentReadEnd();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00032A94 File Offset: 0x00030C94
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (base.Document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("DocumentFormatterDoesNotHaveDocument")));
			}
			AtomPub10CategoriesDocumentFormatter.TraceCategoriesDocumentWriteBegin();
			writer.WriteStartElement("app", "categories", "http://www.w3.org/2007/app");
			this.WriteDocument(writer);
			writer.WriteEndElement();
			AtomPub10CategoriesDocumentFormatter.TraceCategoriesDocumentWriteEnd();
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00032B02 File Offset: 0x00030D02
		internal static void TraceCategoriesDocumentReadBegin()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983082, SR.GetString("TraceCodeSyndicationReadCategoriesDocumentBegin"));
			}
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00032B20 File Offset: 0x00030D20
		internal static void TraceCategoriesDocumentReadEnd()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983083, SR.GetString("TraceCodeSyndicationReadCategoriesDocumentEnd"));
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00032B3E File Offset: 0x00030D3E
		internal static void TraceCategoriesDocumentWriteBegin()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983086, SR.GetString("TraceCodeSyndicationWriteCategoriesDocumentBegin"));
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00032B5C File Offset: 0x00030D5C
		internal static void TraceCategoriesDocumentWriteEnd()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983087, SR.GetString("TraceCodeSyndicationWriteCategoriesDocumentEnd"));
			}
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00032B7A File Offset: 0x00030D7A
		protected override InlineCategoriesDocument CreateInlineCategoriesDocument()
		{
			if (this.inlineDocumentType == typeof(InlineCategoriesDocument))
			{
				return new InlineCategoriesDocument();
			}
			return (InlineCategoriesDocument)Activator.CreateInstance(this.inlineDocumentType);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00032BA9 File Offset: 0x00030DA9
		protected override ReferencedCategoriesDocument CreateReferencedCategoriesDocument()
		{
			if (this.referencedDocumentType == typeof(ReferencedCategoriesDocument))
			{
				return new ReferencedCategoriesDocument();
			}
			return (ReferencedCategoriesDocument)Activator.CreateInstance(this.referencedDocumentType);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00032BD8 File Offset: 0x00030DD8
		private void ReadDocument(XmlReader reader)
		{
			try
			{
				SyndicationFeedFormatter.MoveToStartElement(reader);
				this.SetDocument(AtomPub10ServiceDocumentFormatter.ReadCategories(reader, null, () => this.CreateInlineCategoriesDocument(), () => this.CreateReferencedCategoriesDocument(), this.Version, this.preserveElementExtensions, this.preserveAttributeExtensions, this.maxExtensionSize));
			}
			catch (FormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDocument"), innerException));
			}
			catch (ArgumentException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDocument"), innerException2));
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x00032C80 File Offset: 0x00030E80
		private void WriteDocument(XmlWriter writer)
		{
			writer.WriteAttributeString("a10", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2005/Atom");
			AtomPub10ServiceDocumentFormatter.WriteCategoriesInnerXml(writer, base.Document, null, this.Version);
		}

		// Token: 0x04001729 RID: 5929
		private Type inlineDocumentType;

		// Token: 0x0400172A RID: 5930
		private int maxExtensionSize;

		// Token: 0x0400172B RID: 5931
		private bool preserveAttributeExtensions;

		// Token: 0x0400172C RID: 5932
		private bool preserveElementExtensions;

		// Token: 0x0400172D RID: 5933
		private Type referencedDocumentType;
	}
}
