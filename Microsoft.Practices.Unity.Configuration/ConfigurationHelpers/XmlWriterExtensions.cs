using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x02000015 RID: 21
	public static class XmlWriterExtensions
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x000043B1 File Offset: 0x000025B1
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static XmlWriter WriteElement(this XmlWriter writer, string elementName, Action<XmlWriter> writeContent)
		{
			Guard.ArgumentNotNull(writer, "writer");
			Guard.ArgumentNotNull(writeContent, "writeContent");
			writer.WriteStartElement(elementName);
			writeContent(writer);
			writer.WriteEndElement();
			return writer;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000043DE File Offset: 0x000025DE
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		public static XmlWriter WriteAttributeIfNotEmpty(this XmlWriter writer, string attributeName, string attributeValue)
		{
			Guard.ArgumentNotNull(writer, "writer");
			if (!string.IsNullOrEmpty(attributeValue))
			{
				writer.WriteAttributeString(attributeName, attributeValue);
			}
			return writer;
		}
	}
}
