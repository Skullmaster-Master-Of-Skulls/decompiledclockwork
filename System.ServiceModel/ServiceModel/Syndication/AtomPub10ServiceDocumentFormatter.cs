using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ServiceModel.Diagnostics;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001A8 RID: 424
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "service", Namespace = "http://www.w3.org/2007/app")]
	public class AtomPub10ServiceDocumentFormatter : ServiceDocumentFormatter, IXmlSerializable
	{
		// Token: 0x06000DE8 RID: 3560 RVA: 0x000315AA File Offset: 0x0002F7AA
		public AtomPub10ServiceDocumentFormatter() : this(typeof(ServiceDocument))
		{
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x000315BC File Offset: 0x0002F7BC
		public AtomPub10ServiceDocumentFormatter(Type documentTypeToCreate)
		{
			if (documentTypeToCreate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("documentTypeToCreate");
			}
			if (!typeof(ServiceDocument).IsAssignableFrom(documentTypeToCreate))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("documentTypeToCreate", SR.GetString("InvalidObjectTypePassed", new object[]
				{
					"documentTypeToCreate",
					"ServiceDocument"
				}));
			}
			this.maxExtensionSize = int.MaxValue;
			this.preserveAttributeExtensions = true;
			this.preserveElementExtensions = true;
			this.documentType = documentTypeToCreate;
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003164A File Offset: 0x0002F84A
		public AtomPub10ServiceDocumentFormatter(ServiceDocument documentToWrite) : base(documentToWrite)
		{
			this.maxExtensionSize = int.MaxValue;
			this.preserveAttributeExtensions = true;
			this.preserveElementExtensions = true;
			this.documentType = documentToWrite.GetType();
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00031678 File Offset: 0x0002F878
		public override string Version
		{
			get
			{
				return "http://www.w3.org/2007/app";
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003167F File Offset: 0x0002F87F
		public override bool CanRead(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			return reader.IsStartElement("service", "http://www.w3.org/2007/app");
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000316A4 File Offset: 0x0002F8A4
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000316A7 File Offset: 0x0002F8A7
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			AtomPub10ServiceDocumentFormatter.TraceServiceDocumentReadBegin();
			this.ReadDocument(reader);
			AtomPub10ServiceDocumentFormatter.TraceServiceDocumentReadEnd();
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x000316D0 File Offset: 0x0002F8D0
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
			AtomPub10ServiceDocumentFormatter.TraceServiceDocumentWriteBegin();
			this.WriteDocument(writer);
			AtomPub10ServiceDocumentFormatter.TraceServiceDocumentWriteEnd();
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00031724 File Offset: 0x0002F924
		public override void ReadFrom(XmlReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.MoveToContent();
			if (!this.CanRead(reader))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("UnknownDocumentXml", new object[]
				{
					reader.LocalName,
					reader.NamespaceURI
				})));
			}
			AtomPub10ServiceDocumentFormatter.TraceServiceDocumentReadBegin();
			this.ReadDocument(reader);
			AtomPub10ServiceDocumentFormatter.TraceServiceDocumentReadEnd();
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00031798 File Offset: 0x0002F998
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
			AtomPub10ServiceDocumentFormatter.TraceServiceDocumentWriteBegin();
			writer.WriteStartElement("app", "service", "http://www.w3.org/2007/app");
			this.WriteDocument(writer);
			writer.WriteEndElement();
			AtomPub10ServiceDocumentFormatter.TraceServiceDocumentWriteEnd();
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00031808 File Offset: 0x0002FA08
		internal static CategoriesDocument ReadCategories(XmlReader reader, Uri baseUri, CreateInlineCategoriesDelegate inlineCategoriesFactory, CreateReferencedCategoriesDelegate referencedCategoriesFactory, string version, bool preserveElementExtensions, bool preserveAttributeExtensions, int maxExtensionSize)
		{
			string attribute = reader.GetAttribute("href", string.Empty);
			if (string.IsNullOrEmpty(attribute))
			{
				InlineCategoriesDocument inlineCategoriesDocument = inlineCategoriesFactory();
				AtomPub10ServiceDocumentFormatter.ReadInlineCategories(reader, inlineCategoriesDocument, baseUri, version, preserveElementExtensions, preserveAttributeExtensions, maxExtensionSize);
				return inlineCategoriesDocument;
			}
			ReferencedCategoriesDocument referencedCategoriesDocument = referencedCategoriesFactory();
			AtomPub10ServiceDocumentFormatter.ReadReferencedCategories(reader, referencedCategoriesDocument, baseUri, new Uri(attribute, UriKind.RelativeOrAbsolute), version, preserveElementExtensions, preserveAttributeExtensions, maxExtensionSize);
			return referencedCategoriesDocument;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x00031866 File Offset: 0x0002FA66
		internal static void TraceServiceDocumentReadBegin()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983080, SR.GetString("TraceCodeSyndicationReadServiceDocumentBegin"));
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00031884 File Offset: 0x0002FA84
		internal static void TraceServiceDocumentReadEnd()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983081, SR.GetString("TraceCodeSyndicationReadServiceDocumentEnd"));
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000318A2 File Offset: 0x0002FAA2
		internal static void TraceServiceDocumentWriteBegin()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983084, SR.GetString("TraceCodeSyndicationWriteServiceDocumentBegin"));
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000318C0 File Offset: 0x0002FAC0
		internal static void TraceServiceDocumentWriteEnd()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 983085, SR.GetString("TraceCodeSyndicationWriteServiceDocumentEnd"));
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000318E0 File Offset: 0x0002FAE0
		internal static void WriteCategoriesInnerXml(XmlWriter writer, CategoriesDocument categories, Uri baseUri, string version)
		{
			Uri baseUriToWrite = FeedUtils.GetBaseUriToWrite(baseUri, categories.BaseUri);
			if (baseUriToWrite != null)
			{
				AtomPub10ServiceDocumentFormatter.WriteXmlBase(writer, baseUriToWrite);
			}
			if (!string.IsNullOrEmpty(categories.Language))
			{
				AtomPub10ServiceDocumentFormatter.WriteXmlLang(writer, categories.Language);
			}
			if (categories.IsInline)
			{
				AtomPub10ServiceDocumentFormatter.WriteInlineCategoriesContent(writer, (InlineCategoriesDocument)categories, version);
				return;
			}
			AtomPub10ServiceDocumentFormatter.WriteReferencedCategoriesContent(writer, (ReferencedCategoriesDocument)categories, version);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x00031946 File Offset: 0x0002FB46
		protected override ServiceDocument CreateDocumentInstance()
		{
			if (this.documentType == typeof(ServiceDocument))
			{
				return new ServiceDocument();
			}
			return (ServiceDocument)Activator.CreateInstance(this.documentType);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00031978 File Offset: 0x0002FB78
		private static void ReadInlineCategories(XmlReader reader, InlineCategoriesDocument inlineCategories, Uri baseUri, string version, bool preserveElementExtensions, bool preserveAttributeExtensions, int maxExtensionSize)
		{
			inlineCategories.BaseUri = baseUri;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "base" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						inlineCategories.BaseUri = FeedUtils.CombineXmlBase(inlineCategories.BaseUri, reader.Value);
					}
					else if (reader.LocalName == "lang" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						inlineCategories.Language = reader.Value;
					}
					else if (reader.LocalName == "fixed" && reader.NamespaceURI == string.Empty)
					{
						inlineCategories.IsFixed = (reader.Value == "yes");
					}
					else if (reader.LocalName == "scheme" && reader.NamespaceURI == string.Empty)
					{
						inlineCategories.Scheme = reader.Value;
					}
					else
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
						{
							string value = reader.Value;
							if (!ServiceDocumentFormatter.TryParseAttribute(localName, namespaceURI, value, inlineCategories, version))
							{
								if (preserveAttributeExtensions)
								{
									inlineCategories.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								}
							}
						}
					}
				}
			}
			SyndicationFeedFormatter.MoveToStartElement(reader);
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				XmlBuffer buffer = null;
				XmlDictionaryWriter xmlDictionaryWriter = null;
				try
				{
					while (reader.IsStartElement())
					{
						if (reader.IsStartElement("category", "http://www.w3.org/2005/Atom"))
						{
							SyndicationCategory syndicationCategory = ServiceDocumentFormatter.CreateCategory(inlineCategories);
							Atom10FeedFormatter.ReadCategory(reader, syndicationCategory, version, preserveAttributeExtensions, preserveElementExtensions, maxExtensionSize);
							if (syndicationCategory.Scheme == null)
							{
								syndicationCategory.Scheme = inlineCategories.Scheme;
							}
							inlineCategories.Categories.Add(syndicationCategory);
						}
						else if (!ServiceDocumentFormatter.TryParseElement(reader, inlineCategories, version))
						{
							if (preserveElementExtensions)
							{
								SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, maxExtensionSize);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								reader.Skip();
							}
						}
					}
					ServiceDocumentFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, inlineCategories);
				}
				finally
				{
					if (xmlDictionaryWriter != null)
					{
						xmlDictionaryWriter.Close();
					}
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00031BBC File Offset: 0x0002FDBC
		private static void ReadReferencedCategories(XmlReader reader, ReferencedCategoriesDocument referencedCategories, Uri baseUri, Uri link, string version, bool preserveElementExtensions, bool preserveAttributeExtensions, int maxExtensionSize)
		{
			referencedCategories.BaseUri = baseUri;
			referencedCategories.Link = link;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "base" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						referencedCategories.BaseUri = FeedUtils.CombineXmlBase(referencedCategories.BaseUri, reader.Value);
					}
					else if (reader.LocalName == "lang" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						referencedCategories.Language = reader.Value;
					}
					else if (!(reader.LocalName == "href") || !(reader.NamespaceURI == string.Empty))
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
						{
							string value = reader.Value;
							if (!ServiceDocumentFormatter.TryParseAttribute(localName, namespaceURI, value, referencedCategories, version))
							{
								if (preserveAttributeExtensions)
								{
									referencedCategories.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								}
							}
						}
					}
				}
			}
			reader.MoveToElement();
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (!isEmptyElement)
			{
				XmlBuffer buffer = null;
				XmlDictionaryWriter xmlDictionaryWriter = null;
				try
				{
					while (reader.IsStartElement())
					{
						if (!ServiceDocumentFormatter.TryParseElement(reader, referencedCategories, version))
						{
							if (preserveElementExtensions)
							{
								SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, maxExtensionSize);
							}
							else
							{
								SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								reader.Skip();
							}
						}
					}
					ServiceDocumentFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, referencedCategories);
				}
				finally
				{
					if (xmlDictionaryWriter != null)
					{
						xmlDictionaryWriter.Close();
					}
				}
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00031D6C File Offset: 0x0002FF6C
		private static void WriteCategories(XmlWriter writer, CategoriesDocument categories, Uri baseUri, string version)
		{
			writer.WriteStartElement("app", "categories", "http://www.w3.org/2007/app");
			AtomPub10ServiceDocumentFormatter.WriteCategoriesInnerXml(writer, categories, baseUri, version);
			writer.WriteEndElement();
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00031D94 File Offset: 0x0002FF94
		private static void WriteInlineCategoriesContent(XmlWriter writer, InlineCategoriesDocument categories, string version)
		{
			if (!string.IsNullOrEmpty(categories.Scheme))
			{
				writer.WriteAttributeString("scheme", categories.Scheme);
			}
			if (categories.IsFixed)
			{
				writer.WriteAttributeString("fixed", "yes");
			}
			ServiceDocumentFormatter.WriteAttributeExtensions(writer, categories, version);
			for (int i = 0; i < categories.Categories.Count; i++)
			{
				Atom10FeedFormatter.WriteCategory(writer, categories.Categories[i], version);
			}
			ServiceDocumentFormatter.WriteElementExtensions(writer, categories, version);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00031E10 File Offset: 0x00030010
		private static void WriteReferencedCategoriesContent(XmlWriter writer, ReferencedCategoriesDocument categories, string version)
		{
			if (categories.Link != null)
			{
				writer.WriteAttributeString("href", FeedUtils.GetUriString(categories.Link));
			}
			ServiceDocumentFormatter.WriteAttributeExtensions(writer, categories, version);
			ServiceDocumentFormatter.WriteElementExtensions(writer, categories, version);
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x00031E46 File Offset: 0x00030046
		private static void WriteXmlBase(XmlWriter writer, Uri baseUri)
		{
			writer.WriteAttributeString("xml", "base", "http://www.w3.org/XML/1998/namespace", FeedUtils.GetUriString(baseUri));
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00031E63 File Offset: 0x00030063
		private static void WriteXmlLang(XmlWriter writer, string lang)
		{
			writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", lang);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00031E7C File Offset: 0x0003007C
		private ResourceCollectionInfo ReadCollection(XmlReader reader, Workspace workspace)
		{
			ResourceCollectionInfo result = ServiceDocumentFormatter.CreateCollection(workspace);
			result.BaseUri = workspace.BaseUri;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "base" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						result.BaseUri = FeedUtils.CombineXmlBase(result.BaseUri, reader.Value);
					}
					else if (reader.LocalName == "href" && reader.NamespaceURI == string.Empty)
					{
						result.Link = new Uri(reader.Value, UriKind.RelativeOrAbsolute);
					}
					else
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
						{
							string value = reader.Value;
							if (!ServiceDocumentFormatter.TryParseAttribute(localName, namespaceURI, value, result, this.Version))
							{
								if (this.preserveAttributeExtensions)
								{
									result.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								}
							}
						}
					}
				}
			}
			XmlBuffer buffer = null;
			XmlDictionaryWriter xmlDictionaryWriter = null;
			reader.ReadStartElement();
			try
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("title", "http://www.w3.org/2005/Atom"))
					{
						result.Title = Atom10FeedFormatter.ReadTextContentFrom(reader, "//app:service/app:workspace/app:collection/atom:title[@type]", this.preserveAttributeExtensions);
					}
					else if (reader.IsStartElement("categories", "http://www.w3.org/2007/app"))
					{
						result.Categories.Add(AtomPub10ServiceDocumentFormatter.ReadCategories(reader, result.BaseUri, () => ServiceDocumentFormatter.CreateInlineCategories(result), () => ServiceDocumentFormatter.CreateReferencedCategories(result), this.Version, this.preserveElementExtensions, this.preserveAttributeExtensions, this.maxExtensionSize));
					}
					else if (reader.IsStartElement("accept", "http://www.w3.org/2007/app"))
					{
						result.Accepts.Add(reader.ReadElementString());
					}
					else if (!ServiceDocumentFormatter.TryParseElement(reader, result, this.Version))
					{
						if (this.preserveElementExtensions)
						{
							SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
						}
						else
						{
							SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
							reader.Skip();
						}
					}
				}
				ServiceDocumentFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, result);
			}
			finally
			{
				if (xmlDictionaryWriter != null)
				{
					xmlDictionaryWriter.Close();
				}
			}
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00032120 File Offset: 0x00030320
		private void ReadDocument(XmlReader reader)
		{
			ServiceDocument serviceDocument = this.CreateDocumentInstance();
			try
			{
				SyndicationFeedFormatter.MoveToStartElement(reader);
				bool isEmptyElement = reader.IsEmptyElement;
				if (reader.HasAttributes)
				{
					while (reader.MoveToNextAttribute())
					{
						if (reader.LocalName == "lang" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
						{
							serviceDocument.Language = reader.Value;
						}
						else if (reader.LocalName == "base" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
						{
							serviceDocument.BaseUri = new Uri(reader.Value, UriKind.RelativeOrAbsolute);
						}
						else
						{
							string namespaceURI = reader.NamespaceURI;
							string localName = reader.LocalName;
							if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
							{
								string value = reader.Value;
								if (!ServiceDocumentFormatter.TryParseAttribute(localName, namespaceURI, value, serviceDocument, this.Version))
								{
									if (this.preserveAttributeExtensions)
									{
										serviceDocument.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
									}
									else
									{
										SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
									}
								}
							}
						}
					}
				}
				XmlBuffer buffer = null;
				XmlDictionaryWriter xmlDictionaryWriter = null;
				reader.ReadStartElement();
				if (!isEmptyElement)
				{
					try
					{
						while (reader.IsStartElement())
						{
							if (reader.IsStartElement("workspace", "http://www.w3.org/2007/app"))
							{
								serviceDocument.Workspaces.Add(this.ReadWorkspace(reader, serviceDocument));
							}
							else if (!ServiceDocumentFormatter.TryParseElement(reader, serviceDocument, this.Version))
							{
								if (this.preserveElementExtensions)
								{
									SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
									reader.Skip();
								}
							}
						}
						ServiceDocumentFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, serviceDocument);
					}
					finally
					{
						if (xmlDictionaryWriter != null)
						{
							xmlDictionaryWriter.Close();
						}
					}
				}
				reader.ReadEndElement();
			}
			catch (FormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDocument"), innerException));
			}
			catch (ArgumentException innerException2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(FeedUtils.AddLineInfo(reader, "ErrorParsingDocument"), innerException2));
			}
			this.SetDocument(serviceDocument);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00032360 File Offset: 0x00030560
		private Workspace ReadWorkspace(XmlReader reader, ServiceDocument document)
		{
			Workspace workspace = ServiceDocumentFormatter.CreateWorkspace(document);
			workspace.BaseUri = document.BaseUri;
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "base" && reader.NamespaceURI == "http://www.w3.org/XML/1998/namespace")
					{
						workspace.BaseUri = FeedUtils.CombineXmlBase(workspace.BaseUri, reader.Value);
					}
					else
					{
						string namespaceURI = reader.NamespaceURI;
						string localName = reader.LocalName;
						if (!FeedUtils.IsXmlns(localName, namespaceURI) && !FeedUtils.IsXmlSchemaType(localName, namespaceURI))
						{
							string value = reader.Value;
							if (!ServiceDocumentFormatter.TryParseAttribute(localName, namespaceURI, value, workspace, this.Version))
							{
								if (this.preserveAttributeExtensions)
								{
									workspace.AttributeExtensions.Add(new XmlQualifiedName(reader.LocalName, reader.NamespaceURI), reader.Value);
								}
								else
								{
									SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
								}
							}
						}
					}
				}
			}
			XmlBuffer buffer = null;
			XmlDictionaryWriter xmlDictionaryWriter = null;
			reader.ReadStartElement();
			try
			{
				while (reader.IsStartElement())
				{
					if (reader.IsStartElement("title", "http://www.w3.org/2005/Atom"))
					{
						workspace.Title = Atom10FeedFormatter.ReadTextContentFrom(reader, "//app:service/app:workspace/atom:title[@type]", this.preserveAttributeExtensions);
					}
					else if (reader.IsStartElement("collection", "http://www.w3.org/2007/app"))
					{
						workspace.Collections.Add(this.ReadCollection(reader, workspace));
					}
					else if (!ServiceDocumentFormatter.TryParseElement(reader, workspace, this.Version))
					{
						if (this.preserveElementExtensions)
						{
							SyndicationFeedFormatter.CreateBufferIfRequiredAndWriteNode(ref buffer, ref xmlDictionaryWriter, reader, this.maxExtensionSize);
						}
						else
						{
							SyndicationFeedFormatter.TraceSyndicationElementIgnoredOnRead(reader);
							reader.Skip();
						}
					}
				}
				ServiceDocumentFormatter.LoadElementExtensions(buffer, xmlDictionaryWriter, workspace);
			}
			finally
			{
				if (xmlDictionaryWriter != null)
				{
					xmlDictionaryWriter.Close();
				}
			}
			reader.ReadEndElement();
			return workspace;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00032514 File Offset: 0x00030714
		private void WriteCollection(XmlWriter writer, ResourceCollectionInfo collection, Uri baseUri)
		{
			writer.WriteStartElement("app", "collection", "http://www.w3.org/2007/app");
			Uri baseUriToWrite = FeedUtils.GetBaseUriToWrite(baseUri, collection.BaseUri);
			if (baseUriToWrite != null)
			{
				baseUri = collection.BaseUri;
				AtomPub10ServiceDocumentFormatter.WriteXmlBase(writer, baseUriToWrite);
			}
			if (collection.Link != null)
			{
				writer.WriteAttributeString("href", FeedUtils.GetUriString(collection.Link));
			}
			ServiceDocumentFormatter.WriteAttributeExtensions(writer, collection, this.Version);
			if (collection.Title != null)
			{
				collection.Title.WriteTo(writer, "title", "http://www.w3.org/2005/Atom");
			}
			for (int i = 0; i < collection.Accepts.Count; i++)
			{
				writer.WriteElementString("app", "accept", "http://www.w3.org/2007/app", collection.Accepts[i]);
			}
			for (int j = 0; j < collection.Categories.Count; j++)
			{
				AtomPub10ServiceDocumentFormatter.WriteCategories(writer, collection.Categories[j], baseUri, this.Version);
			}
			ServiceDocumentFormatter.WriteElementExtensions(writer, collection, this.Version);
			writer.WriteEndElement();
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x00032624 File Offset: 0x00030824
		private void WriteDocument(XmlWriter writer)
		{
			writer.WriteAttributeString("a10", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2005/Atom");
			if (!string.IsNullOrEmpty(base.Document.Language))
			{
				AtomPub10ServiceDocumentFormatter.WriteXmlLang(writer, base.Document.Language);
			}
			Uri baseUri = base.Document.BaseUri;
			if (baseUri != null)
			{
				AtomPub10ServiceDocumentFormatter.WriteXmlBase(writer, baseUri);
			}
			ServiceDocumentFormatter.WriteAttributeExtensions(writer, base.Document, this.Version);
			for (int i = 0; i < base.Document.Workspaces.Count; i++)
			{
				this.WriteWorkspace(writer, base.Document.Workspaces[i], baseUri);
			}
			ServiceDocumentFormatter.WriteElementExtensions(writer, base.Document, this.Version);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000326E0 File Offset: 0x000308E0
		private void WriteWorkspace(XmlWriter writer, Workspace workspace, Uri baseUri)
		{
			writer.WriteStartElement("app", "workspace", "http://www.w3.org/2007/app");
			Uri baseUriToWrite = FeedUtils.GetBaseUriToWrite(baseUri, workspace.BaseUri);
			if (baseUriToWrite != null)
			{
				baseUri = workspace.BaseUri;
				AtomPub10ServiceDocumentFormatter.WriteXmlBase(writer, baseUriToWrite);
			}
			ServiceDocumentFormatter.WriteAttributeExtensions(writer, workspace, this.Version);
			if (workspace.Title != null)
			{
				workspace.Title.WriteTo(writer, "title", "http://www.w3.org/2005/Atom");
			}
			for (int i = 0; i < workspace.Collections.Count; i++)
			{
				this.WriteCollection(writer, workspace.Collections[i], baseUri);
			}
			ServiceDocumentFormatter.WriteElementExtensions(writer, workspace, this.Version);
			writer.WriteEndElement();
		}

		// Token: 0x0400171B RID: 5915
		private Type documentType;

		// Token: 0x0400171C RID: 5916
		private int maxExtensionSize;

		// Token: 0x0400171D RID: 5917
		private bool preserveAttributeExtensions;

		// Token: 0x0400171E RID: 5918
		private bool preserveElementExtensions;
	}
}
