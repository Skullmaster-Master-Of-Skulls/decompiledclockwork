using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000187 RID: 391
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class SyndicationContent
	{
		// Token: 0x06000BA7 RID: 2983 RVA: 0x0002B6B7 File Offset: 0x000298B7
		protected SyndicationContent()
		{
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002B6BF File Offset: 0x000298BF
		protected SyndicationContent(SyndicationContent source)
		{
			this.CopyAttributeExtensions(source);
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0002B6CE File Offset: 0x000298CE
		public Dictionary<XmlQualifiedName, string> AttributeExtensions
		{
			get
			{
				if (this.attributeExtensions == null)
				{
					this.attributeExtensions = new Dictionary<XmlQualifiedName, string>();
				}
				return this.attributeExtensions;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BAA RID: 2986
		public abstract string Type { get; }

		// Token: 0x06000BAB RID: 2987 RVA: 0x0002B6E9 File Offset: 0x000298E9
		public static TextSyndicationContent CreateHtmlContent(string content)
		{
			return new TextSyndicationContent(content, TextSyndicationContentKind.Html);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002B6F2 File Offset: 0x000298F2
		public static TextSyndicationContent CreatePlaintextContent(string content)
		{
			return new TextSyndicationContent(content);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002B6FA File Offset: 0x000298FA
		public static UrlSyndicationContent CreateUrlContent(Uri url, string mediaType)
		{
			return new UrlSyndicationContent(url, mediaType);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002B703 File Offset: 0x00029903
		public static TextSyndicationContent CreateXhtmlContent(string content)
		{
			return new TextSyndicationContent(content, TextSyndicationContentKind.XHtml);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002B70C File Offset: 0x0002990C
		public static XmlSyndicationContent CreateXmlContent(object dataContractObject)
		{
			return new XmlSyndicationContent("text/xml", dataContractObject, null);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002B71A File Offset: 0x0002991A
		public static XmlSyndicationContent CreateXmlContent(object dataContractObject, XmlObjectSerializer dataContractSerializer)
		{
			return new XmlSyndicationContent("text/xml", dataContractObject, dataContractSerializer);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002B728 File Offset: 0x00029928
		public static XmlSyndicationContent CreateXmlContent(XmlReader xmlReader)
		{
			return new XmlSyndicationContent(xmlReader);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0002B730 File Offset: 0x00029930
		public static XmlSyndicationContent CreateXmlContent(object xmlSerializerObject, XmlSerializer serializer)
		{
			return new XmlSyndicationContent("text/xml", xmlSerializerObject, serializer);
		}

		// Token: 0x06000BB3 RID: 2995
		public abstract SyndicationContent Clone();

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002B740 File Offset: 0x00029940
		public void WriteTo(XmlWriter writer, string outerElementName, string outerElementNamespace)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			if (string.IsNullOrEmpty(outerElementName))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("OuterElementNameNotSpecified"));
			}
			writer.WriteStartElement(outerElementName, outerElementNamespace);
			writer.WriteAttributeString("type", string.Empty, this.Type);
			if (this.attributeExtensions != null)
			{
				foreach (XmlQualifiedName xmlQualifiedName in this.attributeExtensions.Keys)
				{
					string value;
					if ((!(xmlQualifiedName.Name == "type") || !(xmlQualifiedName.Namespace == string.Empty)) && this.attributeExtensions.TryGetValue(xmlQualifiedName, out value))
					{
						writer.WriteAttributeString(xmlQualifiedName.Name, xmlQualifiedName.Namespace, value);
					}
				}
			}
			this.WriteContentsTo(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002B83C File Offset: 0x00029A3C
		internal void CopyAttributeExtensions(SyndicationContent source)
		{
			if (source == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("source");
			}
			if (source.attributeExtensions != null)
			{
				foreach (XmlQualifiedName key in source.attributeExtensions.Keys)
				{
					this.AttributeExtensions.Add(key, source.attributeExtensions[key]);
				}
			}
		}

		// Token: 0x06000BB6 RID: 2998
		protected abstract void WriteContentsTo(XmlWriter writer);

		// Token: 0x04001695 RID: 5781
		private Dictionary<XmlQualifiedName, string> attributeExtensions;
	}
}
