using System;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x02000085 RID: 133
	internal struct ElementWithAlgorithmAttribute
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x00011C70 File Offset: 0x0000FE70
		public ElementWithAlgorithmAttribute(XmlDictionaryString elementName)
		{
			if (elementName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("elementName"));
			}
			this.elementName = elementName;
			this.algorithm = null;
			this.algorithmDictionaryString = null;
			this.prefix = "";
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00011CAA File Offset: 0x0000FEAA
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00011CB2 File Offset: 0x0000FEB2
		public string Algorithm
		{
			get
			{
				return this.algorithm;
			}
			set
			{
				this.algorithm = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00011CBB File Offset: 0x0000FEBB
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x00011CC3 File Offset: 0x0000FEC3
		public XmlDictionaryString AlgorithmDictionaryString
		{
			get
			{
				return this.algorithmDictionaryString;
			}
			set
			{
				this.algorithmDictionaryString = value;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00011CCC File Offset: 0x0000FECC
		public void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager)
		{
			reader.MoveToStartElement(this.elementName, dictionaryManager.XmlSignatureDictionary.Namespace);
			this.prefix = reader.Prefix;
			bool isEmptyElement = reader.IsEmptyElement;
			this.algorithm = reader.GetAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
			if (this.algorithm == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("RequiredAttributeMissing", new object[]
				{
					dictionaryManager.XmlSignatureDictionary.Algorithm,
					this.elementName
				})));
			}
			reader.Read();
			reader.MoveToContent();
			if (!isEmptyElement)
			{
				reader.MoveToContent();
				reader.ReadEndElement();
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00011D78 File Offset: 0x0000FF78
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			writer.WriteStartElement(this.prefix, this.elementName, dictionaryManager.XmlSignatureDictionary.Namespace);
			writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
			if (this.algorithmDictionaryString != null)
			{
				writer.WriteString(this.algorithmDictionaryString);
			}
			else
			{
				writer.WriteString(this.algorithm);
			}
			writer.WriteEndAttribute();
			writer.WriteEndElement();
		}

		// Token: 0x040003C5 RID: 965
		private readonly XmlDictionaryString elementName;

		// Token: 0x040003C6 RID: 966
		private string algorithm;

		// Token: 0x040003C7 RID: 967
		private XmlDictionaryString algorithmDictionaryString;

		// Token: 0x040003C8 RID: 968
		private string prefix;
	}
}
