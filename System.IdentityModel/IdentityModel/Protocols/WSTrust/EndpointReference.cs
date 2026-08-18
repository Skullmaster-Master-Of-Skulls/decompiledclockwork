using System;
using System.Collections.ObjectModel;
using System.Xml;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F2 RID: 498
	public class EndpointReference
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x0004709C File Offset: 0x0004529C
		public Collection<XmlElement> Details
		{
			get
			{
				return this._details;
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000470A4 File Offset: 0x000452A4
		public EndpointReference(string uri)
		{
			if (uri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uri");
			}
			Uri uri2 = new Uri(uri);
			if (!uri2.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("uri", SR.GetString("ID0013"));
			}
			this.uri = uri2;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00047105 File Offset: 0x00045305
		public Uri Uri
		{
			get
			{
				return this.uri;
			}
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00047110 File Offset: 0x00045310
		public void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("writer");
			}
			writer.WriteStartElement("wsa", "EndpointReference", "http://www.w3.org/2005/08/addressing");
			writer.WriteStartElement("wsa", "Address", "http://www.w3.org/2005/08/addressing");
			writer.WriteString(this.Uri.AbsoluteUri);
			writer.WriteEndElement();
			foreach (XmlElement xmlElement in this._details)
			{
				xmlElement.WriteTo(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000471B8 File Offset: 0x000453B8
		public static EndpointReference ReadFrom(XmlReader reader)
		{
			return EndpointReference.ReadFrom(XmlDictionaryReader.CreateDictionaryReader(reader));
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000471C8 File Offset: 0x000453C8
		public static EndpointReference ReadFrom(XmlDictionaryReader reader)
		{
			if (reader == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
			}
			reader.ReadFullStartElement();
			reader.MoveToContent();
			if ((reader.IsNamespaceUri("http://www.w3.org/2005/08/addressing") || reader.IsNamespaceUri("http://schemas.xmlsoap.org/ws/2004/08/addressing")) && (reader.IsStartElement("Address", "http://www.w3.org/2005/08/addressing") || reader.IsStartElement("Address", "http://schemas.xmlsoap.org/ws/2004/08/addressing")))
			{
				EndpointReference endpointReference = new EndpointReference(reader.ReadElementContentAsString());
				while (reader.IsStartElement())
				{
					bool isEmptyElement = reader.IsEmptyElement;
					XmlReader reader2 = reader.ReadSubtree();
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.PreserveWhitespace = true;
					xmlDocument.Load(reader2);
					endpointReference._details.Add(xmlDocument.DocumentElement);
					if (!isEmptyElement)
					{
						reader.ReadEndElement();
					}
				}
				reader.ReadEndElement();
				return endpointReference;
			}
			return null;
		}

		// Token: 0x04000E6A RID: 3690
		private Collection<XmlElement> _details = new Collection<XmlElement>();

		// Token: 0x04000E6B RID: 3691
		private Uri uri;
	}
}
