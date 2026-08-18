using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000186 RID: 390
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class SyndicationCategory : IExtensibleSyndicationObject
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0002B594 File Offset: 0x00029794
		public SyndicationCategory() : this(null)
		{
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002B59D File Offset: 0x0002979D
		public SyndicationCategory(string name) : this(name, null, null)
		{
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002B5A8 File Offset: 0x000297A8
		public SyndicationCategory(string name, string scheme, string label)
		{
			this.name = name;
			this.scheme = scheme;
			this.label = label;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002B5C8 File Offset: 0x000297C8
		protected SyndicationCategory(SyndicationCategory source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			this.label = source.label;
			this.name = source.name;
			this.scheme = source.scheme;
			this.extensions = source.extensions.Clone();
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0002B623 File Offset: 0x00029823
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				return this.extensions.AttributeExtensions;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0002B630 File Offset: 0x00029830
		public SyndicationElementExtensionCollection ElementExtensions
		{
			get
			{
				return this.extensions.ElementExtensions;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0002B63D File Offset: 0x0002983D
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x0002B645 File Offset: 0x00029845
		public string Label
		{
			get
			{
				return this.label;
			}
			set
			{
				this.label = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0002B64E File Offset: 0x0002984E
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x0002B656 File Offset: 0x00029856
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

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002B65F File Offset: 0x0002985F
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x0002B667 File Offset: 0x00029867
		public string Scheme
		{
			get
			{
				return this.scheme;
			}
			set
			{
				this.scheme = value;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002B670 File Offset: 0x00029870
		public virtual SyndicationCategory Clone()
		{
			return new SyndicationCategory(this);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002B678 File Offset: 0x00029878
		protected internal virtual bool TryParseAttribute(string name, string ns, string value, string version)
		{
			return false;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002B67B File Offset: 0x0002987B
		protected internal virtual bool TryParseElement(XmlReader reader, string version)
		{
			return false;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002B67E File Offset: 0x0002987E
		protected internal virtual void WriteAttributeExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteAttributeExtensions(writer);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002B68C File Offset: 0x0002988C
		protected internal virtual void WriteElementExtensions(XmlWriter writer, string version)
		{
			this.extensions.WriteElementExtensions(writer);
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002B69A File Offset: 0x0002989A
		internal void LoadElementExtensions(XmlReader readerOverUnparsedExtensions, int maxExtensionSize)
		{
			this.extensions.LoadElementExtensions(readerOverUnparsedExtensions, maxExtensionSize);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002B6A9 File Offset: 0x000298A9
		internal void LoadElementExtensions(XmlBuffer buffer)
		{
			this.extensions.LoadElementExtensions(buffer);
		}

		// Token: 0x04001691 RID: 5777
		private ExtensibleSyndicationObject extensions;

		// Token: 0x04001692 RID: 5778
		private string label;

		// Token: 0x04001693 RID: 5779
		private string name;

		// Token: 0x04001694 RID: 5780
		private string scheme;
	}
}
