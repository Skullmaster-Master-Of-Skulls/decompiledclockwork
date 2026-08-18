using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000194 RID: 404
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class SyndicationPerson : IExtensibleSyndicationObject
	{
		// Token: 0x06000CDC RID: 3292 RVA: 0x0002DAF1 File Offset: 0x0002BCF1
		public SyndicationPerson() : this(null)
		{
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002DAFA File Offset: 0x0002BCFA
		public SyndicationPerson(string email) : this(email, null, null)
		{
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002DB05 File Offset: 0x0002BD05
		public SyndicationPerson(string email, string name, string uri)
		{
			this.name = name;
			this.email = email;
			this.uri = uri;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002DB24 File Offset: 0x0002BD24
		protected SyndicationPerson(SyndicationPerson source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.email = source.email;
			this.name = source.name;
			this.uri = source.uri;
			this.extensions = source.extensions.Clone();
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0002DB7F File Offset: 0x0002BD7F
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002DB8C File Offset: 0x0002BD8C
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0002DB99 File Offset: 0x0002BD99
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0002DBA1 File Offset: 0x0002BDA1
		public string Email
		{
			get
			{
				return this.email;
			}
			set
			{
				this.email = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002DBAA File Offset: 0x0002BDAA
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x0002DBB2 File Offset: 0x0002BDB2
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0002DBBB File Offset: 0x0002BDBB
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x0002DBC3 File Offset: 0x0002BDC3
		public string Uri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		public virtual SyndicationPerson Clone()
		{
			return new SyndicationPerson(this);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002DBD7 File Offset: 0x0002BDD7
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0002DBDA File Offset: 0x0002BDDA
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002DBE8 File Offset: 0x0002BDE8
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0002DBF6 File Offset: 0x0002BDF6
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002DC05 File Offset: 0x0002BE05
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x040016C8 RID: 5832
		private string email;

		// Token: 0x040016C9 RID: 5833
		private ExtensibleSyndicationObject extensions;

		// Token: 0x040016CA RID: 5834
		private string name;

		// Token: 0x040016CB RID: 5835
		private string uri;
	}
}
