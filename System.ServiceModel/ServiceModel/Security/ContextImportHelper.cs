using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x020002B4 RID: 692
	internal static class ContextImportHelper
	{
		// Token: 0x0600159F RID: 5535 RVA: 0x000521E8 File Offset: 0x000503E8
		internal static XmlDictionaryReader CreateSplicedReader(byte[] decryptedBuffer, XmlAttributeHolder[] outerContext1, XmlAttributeHolder[] outerContext2, XmlAttributeHolder[] outerContext3, XmlDictionaryReaderQuotas quotas)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlDictionaryWriter xmlDictionaryWriter = XmlDictionaryWriter.CreateTextWriter(memoryStream);
			xmlDictionaryWriter.WriteStartElement("x");
			ContextImportHelper.WriteNamespaceDeclarations(outerContext1, xmlDictionaryWriter);
			xmlDictionaryWriter.WriteStartElement("y");
			ContextImportHelper.WriteNamespaceDeclarations(outerContext2, xmlDictionaryWriter);
			xmlDictionaryWriter.WriteStartElement("z");
			ContextImportHelper.WriteNamespaceDeclarations(outerContext3, xmlDictionaryWriter);
			xmlDictionaryWriter.WriteString(" ");
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.Flush();
			byte[] buffer = ContextImportHelper.SpliceBuffers(decryptedBuffer, memoryStream.GetBuffer(), (int)memoryStream.Length, 3);
			XmlDictionaryReader xmlDictionaryReader = XmlDictionaryReader.CreateTextReader(buffer, quotas);
			xmlDictionaryReader.ReadStartElement("x");
			xmlDictionaryReader.ReadStartElement("y");
			xmlDictionaryReader.ReadStartElement("z");
			if (xmlDictionaryReader.NodeType != XmlNodeType.Element)
			{
				xmlDictionaryReader.MoveToContent();
			}
			return xmlDictionaryReader;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x000522AB File Offset: 0x000504AB
		internal static string GetPrefixIfNamespaceDeclaration(string prefix, string localName)
		{
			if (prefix == "xmlns")
			{
				return localName;
			}
			if (prefix.Length == 0 && localName == "xmlns")
			{
				return string.Empty;
			}
			return null;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x000522D8 File Offset: 0x000504D8
		private static bool IsNamespaceDeclaration(string prefix, string localName)
		{
			return ContextImportHelper.GetPrefixIfNamespaceDeclaration(prefix, localName) != null;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x000522E4 File Offset: 0x000504E4
		internal static byte[] SpliceBuffers(byte[] middle, byte[] wrapper, int wrapperLength, int wrappingDepth)
		{
			int num = 0;
			int i;
			for (i = wrapperLength - 1; i >= 0; i--)
			{
				if (wrapper[i] == 60)
				{
					num++;
					if (num == wrappingDepth)
					{
						break;
					}
				}
			}
			byte[] array = DiagnosticUtility.Utility.AllocateByteArray(checked(middle.Length + wrapperLength - 1));
			int num2 = 0;
			int num3 = i - 1;
			Buffer.BlockCopy(wrapper, 0, array, num2, num3);
			num2 += num3;
			num3 = middle.Length;
			Buffer.BlockCopy(middle, 0, array, num2, num3);
			num2 += num3;
			num3 = wrapperLength - i;
			Buffer.BlockCopy(wrapper, i, array, num2, num3);
			return array;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00052360 File Offset: 0x00050560
		private static void WriteNamespaceDeclarations(XmlAttributeHolder[] attributes, XmlWriter writer)
		{
			if (attributes != null)
			{
				foreach (XmlAttributeHolder xmlAttributeHolder in attributes)
				{
					if (ContextImportHelper.IsNamespaceDeclaration(xmlAttributeHolder.Prefix, xmlAttributeHolder.LocalName))
					{
						xmlAttributeHolder.WriteTo(writer);
					}
				}
			}
		}
	}
}
