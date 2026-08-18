using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005D RID: 93
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigXsltTransform : Transform
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00010948 File Offset: 0x0000EB48
		public XmlDsigXsltTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xslt-19991116";
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000109B4 File Offset: 0x0000EBB4
		public XmlDsigXsltTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xslt-19991116";
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00010A25 File Offset: 0x0000EC25
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00010A2D File Offset: 0x0000EC2D
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010A38 File Offset: 0x0000EC38
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
			XmlElement xmlElement = null;
			int num = 0;
			foreach (object obj in nodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!(xmlNode is XmlWhitespace))
				{
					if (xmlNode is XmlElement)
					{
						if (num != 0)
						{
							throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
						}
						xmlElement = (xmlNode as XmlElement);
						num++;
					}
					else
					{
						num++;
					}
				}
			}
			if (num != 1 || xmlElement == null)
			{
				throw new CryptographicException(SecurityResources.GetResourceString("Cryptography_Xml_UnknownTransform"));
			}
			this._xslNodes = nodeList;
			this._xslFragment = xmlElement.OuterXml.Trim(null);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00010B04 File Offset: 0x0000ED04
		protected override XmlNodeList GetInnerXml()
		{
			return this._xslNodes;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010B0C File Offset: 0x0000ED0C
		public override void LoadInput(object obj)
		{
			if (this._inputStream != null)
			{
				this._inputStream.Close();
			}
			this._inputStream = new MemoryStream();
			if (obj is Stream)
			{
				this._inputStream = (Stream)obj;
				return;
			}
			if (!(obj is XmlNodeList))
			{
				if (obj is XmlDocument)
				{
					CanonicalXml canonicalXml = new CanonicalXml((XmlDocument)obj, null, this._includeComments);
					byte[] bytes = canonicalXml.GetBytes();
					if (bytes == null)
					{
						return;
					}
					this._inputStream.Write(bytes, 0, bytes.Length);
					this._inputStream.Flush();
					this._inputStream.Position = 0L;
				}
				return;
			}
			CanonicalXml canonicalXml2 = new CanonicalXml((XmlNodeList)obj, null, this._includeComments);
			byte[] bytes2 = canonicalXml2.GetBytes();
			if (bytes2 == null)
			{
				return;
			}
			this._inputStream.Write(bytes2, 0, bytes2.Length);
			this._inputStream.Flush();
			this._inputStream.Position = 0L;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010BEC File Offset: 0x0000EDEC
		public override object GetOutput()
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.XmlResolver = null;
			xmlReaderSettings.MaxCharactersFromEntities = Utils.GetMaxCharactersFromEntities();
			xmlReaderSettings.MaxCharactersInDocument = Utils.GetMaxCharactersInDocument();
			object result;
			using (StringReader stringReader = new StringReader(this._xslFragment))
			{
				XmlReader stylesheet = XmlReader.Create(stringReader, xmlReaderSettings, null);
				xslCompiledTransform.Load(stylesheet, XsltSettings.Default, null);
				XmlReader reader = XmlReader.Create(this._inputStream, xmlReaderSettings, base.BaseURI);
				XPathDocument input = new XPathDocument(reader, XmlSpace.Preserve);
				MemoryStream memoryStream = new MemoryStream();
				XmlWriter results = new XmlTextWriter(memoryStream, null);
				xslCompiledTransform.Transform(input, null, results);
				memoryStream.Position = 0L;
				result = memoryStream;
			}
			return result;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00010CAC File Offset: 0x0000EEAC
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return (Stream)this.GetOutput();
		}

		// Token: 0x0400046D RID: 1133
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x0400046E RID: 1134
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x0400046F RID: 1135
		private XmlNodeList _xslNodes;

		// Token: 0x04000470 RID: 1136
		private string _xslFragment;

		// Token: 0x04000471 RID: 1137
		private Stream _inputStream;

		// Token: 0x04000472 RID: 1138
		private bool _includeComments;
	}
}
