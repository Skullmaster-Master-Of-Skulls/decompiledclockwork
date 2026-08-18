using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005F1 RID: 1521
	public static class SerializableAdapter
	{
		// Token: 0x060030DB RID: 12507 RVA: 0x00043020 File Offset: 0x00041220
		public static string Serialize<T>(this T value) where T : class
		{
			bool flag = value == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				XmlWriterSettings settings = new XmlWriterSettings
				{
					Encoding = new UnicodeEncoding(false, false),
					Indent = false,
					OmitXmlDeclaration = false
				};
				using (StringWriter stringWriter = new StringWriter())
				{
					using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings))
					{
						xmlSerializer.Serialize(xmlWriter, value);
					}
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000430DC File Offset: 0x000412DC
		public static T Deserialize<T>(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				XmlReaderSettings settings = new XmlReaderSettings();
				using (StringReader stringReader = new StringReader(xml))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader, settings))
					{
						result = (T)((object)xmlSerializer.Deserialize(xmlReader));
					}
				}
			}
			return result;
		}
	}
}
