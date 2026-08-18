using System;
using System.IO;
using System.Security.Permissions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000B3 RID: 179
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class XmlDsigXsltTransform : Transform
	{
		// Token: 0x06000410 RID: 1040 RVA: 0x0001510C File Offset: 0x0001410C
		public XmlDsigXsltTransform()
		{
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xslt-19991116";
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001517C File Offset: 0x0001417C
		public XmlDsigXsltTransform(bool includeComments)
		{
			this._includeComments = includeComments;
			base.Algorithm = "http://www.w3.org/TR/1999/REC-xslt-19991116";
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000151F1 File Offset: 0x000141F1
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000151F9 File Offset: 0x000141F9
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00015204 File Offset: 0x00014204
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

		// Token: 0x06000415 RID: 1045 RVA: 0x000152D0 File Offset: 0x000142D0
		protected override XmlNodeList GetInnerXml()
		{
			return this._xslNodes;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000152D8 File Offset: 0x000142D8
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

		// Token: 0x06000417 RID: 1047 RVA: 0x000153B8 File Offset: 0x000143B8
		public override object GetOutput()
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.XmlResolver = null;
			xmlReaderSettings.MaxCharactersFromEntities = Utils.GetMaxCharactersFromEntities();
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

		// Token: 0x06000418 RID: 1048 RVA: 0x0001546C File Offset: 0x0001446C
		public override object GetOutput(Type type)
		{
			if (type != typeof(Stream) && !type.IsSubclassOf(typeof(Stream)))
			{
				throw new ArgumentException(SecurityResources.GetResourceString("Cryptography_Xml_TransformIncorrectInputType"), "type");
			}
			return (Stream)this.GetOutput();
		}

		// Token: 0x0400057A RID: 1402
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument),
			typeof(XmlNodeList)
		};

		// Token: 0x0400057B RID: 1403
		private Type[] _outputTypes = new Type[]
		{
			typeof(Stream)
		};

		// Token: 0x0400057C RID: 1404
		private XmlNodeList _xslNodes;

		// Token: 0x0400057D RID: 1405
		private string _xslFragment;

		// Token: 0x0400057E RID: 1406
		private Stream _inputStream;

		// Token: 0x0400057F RID: 1407
		private bool _includeComments;
	}
}
