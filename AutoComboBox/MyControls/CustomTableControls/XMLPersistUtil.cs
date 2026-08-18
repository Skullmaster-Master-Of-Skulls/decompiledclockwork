using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000095 RID: 149
	public class XMLPersistUtil
	{
		// Token: 0x060005C5 RID: 1477 RVA: 0x0002FC50 File Offset: 0x0002EC50
		public static string Marshall(TablePersistObject t)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(TablePersistObject));
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlSerializer.Serialize(xmlTextWriter, t);
			memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
			return new UTF8Encoding().GetString(memoryStream.ToArray());
		}
	}
}
