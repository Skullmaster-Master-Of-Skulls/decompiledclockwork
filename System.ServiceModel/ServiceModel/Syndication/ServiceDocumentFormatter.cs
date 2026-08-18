using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x020001A5 RID: 421
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[DataContract]
	public abstract class ServiceDocumentFormatter
	{
		// Token: 0x06000DBA RID: 3514 RVA: 0x0003122B File Offset: 0x0002F42B
		protected ServiceDocumentFormatter()
		{
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00031233 File Offset: 0x0002F433
		protected ServiceDocumentFormatter(ServiceDocument documentToWrite)
		{
			if (documentToWrite == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("documentToWrite");
			}
			this.document = documentToWrite;
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00031255 File Offset: 0x0002F455
		public ServiceDocument Document
		{
			get
			{
				return this.document;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000DBD RID: 3517
		public abstract string Version { get; }

		// Token: 0x06000DBE RID: 3518
		public abstract bool CanRead(XmlReader reader);

		// Token: 0x06000DBF RID: 3519
		public abstract void ReadFrom(XmlReader reader);

		// Token: 0x06000DC0 RID: 3520
		public abstract void WriteTo(XmlWriter writer);

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003125D File Offset: 0x0002F45D
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, CategoriesDocument categories)
		{
			if (categories == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("categories");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			categories.LoadElementExtensions(buffer);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00031280 File Offset: 0x0002F480
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, ResourceCollectionInfo collection)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("collection");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			collection.LoadElementExtensions(buffer);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000312A3 File Offset: 0x0002F4A3
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, Workspace workspace)
		{
			if (workspace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("workspace");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			workspace.LoadElementExtensions(buffer);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x000312C6 File Offset: 0x0002F4C6
		internal static void LoadElementExtensions(XmlBuffer buffer, XmlDictionaryWriter writer, ServiceDocument document)
		{
			if (document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("document");
			}
			SyndicationFeedFormatter.CloseBuffer(buffer, writer);
			document.LoadElementExtensions(buffer);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000312E9 File Offset: 0x0002F4E9
		protected static SyndicationCategory CreateCategory(InlineCategoriesDocument inlineCategories)
		{
			if (inlineCategories == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("inlineCategories");
			}
			return inlineCategories.CreateCategory();
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00031304 File Offset: 0x0002F504
		protected static ResourceCollectionInfo CreateCollection(Workspace workspace)
		{
			if (workspace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("workspace");
			}
			return workspace.CreateResourceCollection();
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003131F File Offset: 0x0002F51F
		protected static InlineCategoriesDocument CreateInlineCategories(ResourceCollectionInfo collection)
		{
			return collection.CreateInlineCategoriesDocument();
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00031327 File Offset: 0x0002F527
		protected static ReferencedCategoriesDocument CreateReferencedCategories(ResourceCollectionInfo collection)
		{
			return collection.CreateReferencedCategoriesDocument();
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0003132F File Offset: 0x0002F52F
		protected static Workspace CreateWorkspace(ServiceDocument document)
		{
			if (document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("document");
			}
			return document.CreateWorkspace();
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0003134A File Offset: 0x0002F54A
		protected static void LoadElementExtensions(XmlReader reader, CategoriesDocument categories, int maxExtensionSize)
		{
			if (categories == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("categories");
			}
			categories.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00031367 File Offset: 0x0002F567
		protected static void LoadElementExtensions(XmlReader reader, ResourceCollectionInfo collection, int maxExtensionSize)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("collection");
			}
			collection.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00031384 File Offset: 0x0002F584
		protected static void LoadElementExtensions(XmlReader reader, Workspace workspace, int maxExtensionSize)
		{
			if (workspace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("workspace");
			}
			workspace.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000313A1 File Offset: 0x0002F5A1
		protected static void LoadElementExtensions(XmlReader reader, ServiceDocument document, int maxExtensionSize)
		{
			if (document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("document");
			}
			document.LoadElementExtensions(reader, maxExtensionSize);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000313BE File Offset: 0x0002F5BE
		protected static bool TryParseAttribute(string name, string ns, string value, ServiceDocument document, string version)
		{
			if (document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("document");
			}
			return document.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000313DE File Offset: 0x0002F5DE
		protected static bool TryParseAttribute(string name, string ns, string value, ResourceCollectionInfo collection, string version)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("collection");
			}
			return collection.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x000313FE File Offset: 0x0002F5FE
		protected static bool TryParseAttribute(string name, string ns, string value, CategoriesDocument categories, string version)
		{
			if (categories == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("categories");
			}
			return categories.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0003141E File Offset: 0x0002F61E
		protected static bool TryParseAttribute(string name, string ns, string value, Workspace workspace, string version)
		{
			if (workspace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("workspace");
			}
			return workspace.TryParseAttribute(name, ns, value, version);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003143E File Offset: 0x0002F63E
		protected static bool TryParseElement(XmlReader reader, ResourceCollectionInfo collection, string version)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("collection");
			}
			return collection.TryParseElement(reader, version);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0003145B File Offset: 0x0002F65B
		protected static bool TryParseElement(XmlReader reader, ServiceDocument document, string version)
		{
			if (document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("document");
			}
			return document.TryParseElement(reader, version);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00031478 File Offset: 0x0002F678
		protected static bool TryParseElement(XmlReader reader, Workspace workspace, string version)
		{
			if (workspace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("workspace");
			}
			return workspace.TryParseElement(reader, version);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00031495 File Offset: 0x0002F695
		protected static bool TryParseElement(XmlReader reader, CategoriesDocument categories, string version)
		{
			if (categories == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("categories");
			}
			return categories.TryParseElement(reader, version);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x000314B2 File Offset: 0x0002F6B2
		protected static void WriteAttributeExtensions(XmlWriter writer, ServiceDocument document, string version)
		{
			if (document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("document");
			}
			document.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x000314CF File Offset: 0x0002F6CF
		protected static void WriteAttributeExtensions(XmlWriter writer, Workspace workspace, string version)
		{
			if (workspace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("workspace");
			}
			workspace.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000314EC File Offset: 0x0002F6EC
		protected static void WriteAttributeExtensions(XmlWriter writer, ResourceCollectionInfo collection, string version)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("collection");
			}
			collection.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00031509 File Offset: 0x0002F709
		protected static void WriteAttributeExtensions(XmlWriter writer, CategoriesDocument categories, string version)
		{
			if (categories == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("categories");
			}
			categories.WriteAttributeExtensions(writer, version);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00031526 File Offset: 0x0002F726
		protected static void WriteElementExtensions(XmlWriter writer, ServiceDocument document, string version)
		{
			if (document == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("document");
			}
			document.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00031543 File Offset: 0x0002F743
		protected static void WriteElementExtensions(XmlWriter writer, Workspace workspace, string version)
		{
			if (workspace == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("workspace");
			}
			workspace.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00031560 File Offset: 0x0002F760
		protected static void WriteElementExtensions(XmlWriter writer, ResourceCollectionInfo collection, string version)
		{
			if (collection == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("collection");
			}
			collection.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003157D File Offset: 0x0002F77D
		protected static void WriteElementExtensions(XmlWriter writer, CategoriesDocument categories, string version)
		{
			if (categories == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("categories");
			}
			categories.WriteElementExtensions(writer, version);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003159A File Offset: 0x0002F79A
		protected virtual ServiceDocument CreateDocumentInstance()
		{
			return new ServiceDocument();
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x000315A1 File Offset: 0x0002F7A1
		protected virtual void SetDocument(ServiceDocument document)
		{
			this.document = document;
		}

		// Token: 0x0400171A RID: 5914
		private ServiceDocument document;
	}
}
