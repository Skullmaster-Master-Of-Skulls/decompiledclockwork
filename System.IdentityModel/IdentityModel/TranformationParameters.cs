using System;
using System.Security.Cryptography;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000AF RID: 175
	internal class TranformationParameters
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000145BA File Offset: 0x000127BA
		public string CanonicalizationAlgorithm
		{
			get
			{
				return XD.SecurityAlgorithmDictionary.ExclusiveC14n.Value;
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000145CC File Offset: 0x000127CC
		public void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager)
		{
			reader.MoveToContent();
			reader.MoveToStartElement("TransformationParameters", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			string prefix = reader.Prefix;
			bool isEmptyElement = reader.IsEmptyElement;
			reader.ReadStartElement();
			if (reader.IsStartElement(dictionaryManager.XmlSignatureDictionary.CanonicalizationMethod, dictionaryManager.XmlSignatureDictionary.Namespace))
			{
				string attribute = reader.GetAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
				bool isEmptyElement2 = reader.IsEmptyElement;
				reader.ReadStartElement();
				if (attribute == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("RequiredAttributeMissing", new object[]
					{
						dictionaryManager.XmlSignatureDictionary.Algorithm,
						dictionaryManager.XmlSignatureDictionary.CanonicalizationMethod
					})));
				}
				if (attribute != this.CanonicalizationAlgorithm)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CryptographicException(SR.GetString("AlgorithmMismatchForTransform")));
				}
				if (!isEmptyElement2)
				{
					reader.MoveToContent();
					reader.ReadEndElement();
				}
			}
			if (!isEmptyElement)
			{
				reader.MoveToContent();
				reader.ReadEndElement();
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000146CC File Offset: 0x000128CC
		public void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager)
		{
			writer.WriteStartElement("o", "TransformationParameters", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
			writer.WriteStartElement(dictionaryManager.XmlSignatureDictionary.Prefix.Value, dictionaryManager.XmlSignatureDictionary.CanonicalizationMethod, dictionaryManager.XmlSignatureDictionary.Namespace);
			writer.WriteStartAttribute(dictionaryManager.XmlSignatureDictionary.Algorithm, null);
			writer.WriteString(dictionaryManager.SecurityAlgorithmDictionary.ExclusiveC14n);
			writer.WriteEndAttribute();
			writer.WriteEndElement();
			writer.WriteEndElement();
		}
	}
}
