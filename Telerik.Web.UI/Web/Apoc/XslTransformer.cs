using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Telerik.Web.Apoc
{
	// Token: 0x020016A7 RID: 5799
	public sealed class XslTransformer
	{
		// Token: 0x0600DFF4 RID: 57332 RVA: 0x0031D624 File Offset: 0x0031B824
		private XslTransformer()
		{
		}

		// Token: 0x0600DFF5 RID: 57333 RVA: 0x0031D62C File Offset: 0x0031B82C
		public static Stream Transform(string xmlFile, string xslFile)
		{
			if (!File.Exists(xslFile))
			{
				throw new ApocException(string.Format("XSL file {0} does not exist", xslFile));
			}
			XmlTextReader xmlTextReader = null;
			XmlTextReader xmlTextReader2 = null;
			Stream result;
			try
			{
				xmlTextReader = new XmlTextReader(xmlFile);
				xmlTextReader2 = new XmlTextReader(xslFile);
				XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
				xslCompiledTransform.Load(xmlTextReader2);
				XPathDocument input = new XPathDocument(xmlTextReader);
				MemoryStream memoryStream = new MemoryStream();
				TextWriter textWriter = new StreamWriter(memoryStream);
				xslCompiledTransform.Transform(input, null, textWriter);
				textWriter.Flush();
				memoryStream.Seek(0L, SeekOrigin.Begin);
				result = memoryStream;
			}
			catch (Exception innerException)
			{
				throw new ApocException(innerException);
			}
			finally
			{
				if (xmlTextReader != null && xmlTextReader.ReadState != ReadState.Closed)
				{
					xmlTextReader.Close();
				}
				if (xmlTextReader2 != null && xmlTextReader2.ReadState != ReadState.Closed)
				{
					xmlTextReader2.Close();
				}
			}
			return result;
		}
	}
}
