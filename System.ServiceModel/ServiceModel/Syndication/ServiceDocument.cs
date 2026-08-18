using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200019F RID: 415
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ServiceDocument : IExtensibleSyndicationObject
	{
		// Token: 0x06000D5A RID: 3418 RVA: 0x00030BA4 File Offset: 0x0002EDA4
		public ServiceDocument() : this(null)
		{
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00030BB0 File Offset: 0x0002EDB0
		public ServiceDocument(IEnumerable<Workspace> workspaces)
		{
			if (workspaces != null)
			{
				this.workspaces = new NullNotAllowedCollection<Workspace>();
				foreach (Workspace item in workspaces)
				{
					this.workspaces.Add(item);
				}
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00030C14 File Offset: 0x0002EE14
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00030C21 File Offset: 0x0002EE21
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00030C29 File Offset: 0x0002EE29
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

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00030C32 File Offset: 0x0002EE32
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00030C3F File Offset: 0x0002EE3F
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x00030C47 File Offset: 0x0002EE47
		public string Language
		{
			get
			{
				return this.language;
			}
			set
			{
				this.language = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x00030C50 File Offset: 0x0002EE50
		public Collection<Workspace> Workspaces
		{
			get
			{
				if (this.workspaces == null)
				{
					this.workspaces = new NullNotAllowedCollection<Workspace>();
				}
				return this.workspaces;
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00030C6B File Offset: 0x0002EE6B
		public static ServiceDocument Load(XmlReader reader)
		{
			return ServiceDocument.Load<ServiceDocument>(reader);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00030C74 File Offset: 0x0002EE74
		public static TServiceDocument Load<TServiceDocument>(XmlReader reader) where TServiceDocument : ServiceDocument, new()
		{
			AtomPub10ServiceDocumentFormatter<TServiceDocument> atomPub10ServiceDocumentFormatter = new AtomPub10ServiceDocumentFormatter<TServiceDocument>();
			atomPub10ServiceDocumentFormatter.ReadFrom(reader);
			return (TServiceDocument)((object)atomPub10ServiceDocumentFormatter.Document);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00030C99 File Offset: 0x0002EE99
		public ServiceDocumentFormatter GetFormatter()
		{
			return new AtomPub10ServiceDocumentFormatter(this);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00030CA1 File Offset: 0x0002EEA1
		public void Save(XmlWriter writer)
		{
			new AtomPub10ServiceDocumentFormatter(this).WriteTo(writer);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00030CAF File Offset: 0x0002EEAF
		protected internal virtual Workspace CreateWorkspace()
		{
			return new Workspace();
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00030CB6 File Offset: 0x0002EEB6
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00030CB9 File Offset: 0x0002EEB9
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00030CBC File Offset: 0x0002EEBC
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00030CCA File Offset: 0x0002EECA
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00030CD8 File Offset: 0x0002EED8
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00030CE7 File Offset: 0x0002EEE7
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x04001704 RID: 5892
		private Uri baseUri;

		// Token: 0x04001705 RID: 5893
		private ExtensibleSyndicationObject extensions;

		// Token: 0x04001706 RID: 5894
		private string language;

		// Token: 0x04001707 RID: 5895
		private Collection<Workspace> workspaces;
	}
}
