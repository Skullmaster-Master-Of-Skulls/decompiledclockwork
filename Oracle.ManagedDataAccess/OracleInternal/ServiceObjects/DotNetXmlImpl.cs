using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Oracle.ManagedDataAccess.Types;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001A3 RID: 419
	internal class DotNetXmlImpl
	{
		// Token: 0x06000FBA RID: 4026 RVA: 0x000A2FD4 File Offset: 0x000A11D4
		internal static string ConvertXmlReaderToString(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			xmlDocument.Load(reader);
			return xmlDocument.OuterXml;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000A2FFC File Offset: 0x000A11FC
		internal static string ConvertXmlDocToString(XmlDocument xmlDoc)
		{
			return xmlDoc.OuterXml;
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000A3004 File Offset: 0x000A1204
		internal static string GetRootElement(XmlDocument xmlDoc)
		{
			return xmlDoc.DocumentElement.Name;
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000A3014 File Offset: 0x000A1214
		internal static string Extract(XmlDocument xmlDoc, string xpathExpr, string nsMap)
		{
			XmlNamespaceManager nsMgr = DotNetXmlImpl.StringToNsMgr(xmlDoc.NameTable, nsMap);
			return DotNetXmlImpl.Extract(xmlDoc, xpathExpr, nsMgr);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000A3038 File Offset: 0x000A1238
		internal static string Extract(XmlDocument xmlDoc, string xpathExpr, XmlNamespaceManager nsMgr)
		{
			XmlNode xmlNode = xmlDoc.SelectSingleNode(xpathExpr, nsMgr);
			if (xmlNode == null)
			{
				return string.Empty;
			}
			if (xmlNode.NodeType == XmlNodeType.Attribute)
			{
				return xmlNode.Value;
			}
			string text = xmlNode.OuterXml;
			XmlNode nextSibling = xmlNode.NextSibling;
			if (nextSibling is XmlWhitespace)
			{
				text += nextSibling.OuterXml;
			}
			return text;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000A3090 File Offset: 0x000A1290
		internal static bool IsExists(XmlDocument xmlDoc, string xpathExpr, XmlNamespaceManager nsMgr)
		{
			bool result = false;
			try
			{
				XmlNode xmlNode = xmlDoc.SelectSingleNode(xpathExpr, nsMgr);
				result = (xmlNode != null);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000A30CC File Offset: 0x000A12CC
		internal static bool IsExists(XmlDocument xmlDoc, string xpathExpr, string nsMap)
		{
			XmlNamespaceManager nsMgr = DotNetXmlImpl.StringToNsMgr(xmlDoc.NameTable, nsMap);
			return DotNetXmlImpl.IsExists(xmlDoc, xpathExpr, nsMgr);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000A30F0 File Offset: 0x000A12F0
		internal static string Transform(XmlReader xslReader, XmlDocument xmlDoc, string paramMap)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
				{
					OmitXmlDeclaration = true,
					ConformanceLevel = ConformanceLevel.Auto
				}))
				{
					XslCompiledTransform xslCompiledTransform = new XslCompiledTransform();
					xslCompiledTransform.Load(xslReader);
					xslCompiledTransform.Transform(xmlDoc.CreateNavigator(), null, xmlWriter);
					result = stringWriter.ToString();
				}
			}
			return result;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000A3178 File Offset: 0x000A1378
		internal static string Transform(OracleXmlType xslDoc, XmlDocument xmlDoc, string paramMap)
		{
			XmlReader xmlReader = xslDoc.GetXmlReader();
			return DotNetXmlImpl.Transform(xmlReader, xmlDoc, paramMap);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000A3194 File Offset: 0x000A1394
		internal static string Transform(string xslDoc, XmlDocument xmlDoc, string paramMap)
		{
			XmlTextReader xslReader = new XmlTextReader(new StringReader(xslDoc));
			return DotNetXmlImpl.Transform(xslReader, xmlDoc, paramMap);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000A31B8 File Offset: 0x000A13B8
		internal static void Update(XmlDocument xmlDoc, string xpathExpr, XmlNamespaceManager nsMgr, string val, ref bool bValueIsFragment)
		{
			if (string.IsNullOrEmpty(val))
			{
				XmlNodeList xmlNodeList = xmlDoc.SelectNodes(xpathExpr, nsMgr);
				for (int i = xmlNodeList.Count - 1; i > -1; i--)
				{
					xmlNodeList[i].ParentNode.RemoveChild(xmlNodeList[i]);
				}
				if (bValueIsFragment && xmlDoc.FirstChild.ChildNodes.Count == 1)
				{
					XmlNode newChild = xmlDoc.DocumentElement.ChildNodes[0];
					xmlDoc.RemoveChild(xmlDoc.DocumentElement);
					xmlDoc.AppendChild(newChild);
					bValueIsFragment = false;
				}
				return;
			}
			XPathNodeIterator xpathNodeIterator;
			try
			{
				XPathNavigator xpathNavigator = xmlDoc.CreateNavigator();
				xpathNodeIterator = xpathNavigator.Select(xpathExpr, nsMgr);
			}
			catch (Exception)
			{
				return;
			}
			bool flag = false;
			if (!bValueIsFragment && xpathExpr.StartsWith("/"))
			{
				if (xpathExpr.Length > 2)
				{
					if (xpathExpr.IndexOf("/", 2) == -1)
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
			}
			if (xpathNodeIterator != null && xpathNodeIterator.Count > 0)
			{
				int j = xpathNodeIterator.Count;
				IEnumerator enumerator = xpathNodeIterator.GetEnumerator();
				enumerator.MoveNext();
				XPathNavigator xpathNavigator2 = (XPathNavigator)enumerator.Current;
				string name = xpathNavigator2.Name;
				XPathNavigator xpathNavigator3 = null;
				while (j > 0)
				{
					if (xpathNavigator2.Name == name)
					{
						if (xpathNavigator2.NodeType != XPathNodeType.Root)
						{
							if (!(xpathNavigator2.Name == xmlDoc.DocumentElement.Name) || !flag)
							{
								if (xpathNavigator2.NodeType == XPathNodeType.Attribute)
								{
									xpathNavigator2.SetValue(val);
								}
								else
								{
									if (xpathNavigator3 == null)
									{
										XmlDocumentFragment xmlDocumentFragment = xmlDoc.CreateDocumentFragment();
										xmlDocumentFragment.InnerXml = val;
										xpathNavigator3 = xmlDocumentFragment.CreateNavigator();
									}
									xpathNavigator2.ReplaceSelf(xpathNavigator3);
								}
								j--;
								goto IL_1E0;
							}
						}
						XmlDocument xmlDocument;
						try
						{
							XmlTypeReader xmlReader = new XmlTypeReader(new StringReader(val));
							XmlDocumentFragment xmlDocumentFragment;
							xmlDocument = DotNetXmlImpl.GetXmlDocument(xmlReader, val, out xmlDocumentFragment, out bValueIsFragment, false);
						}
						catch (Exception)
						{
							return;
						}
						XmlNode newChild2 = xmlDoc.ImportNode(xmlDocument.DocumentElement, true);
						xmlDoc.RemoveChild(xmlDoc.DocumentElement);
						xmlDoc.AppendChild(newChild2);
						return;
					}
					IL_1E0:
					xpathNavigator2.MoveToNext();
				}
			}
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000A33D4 File Offset: 0x000A15D4
		internal static void Update(XmlDocument xmlDoc, string xpathExpr, string nsMap, string val, ref bool bValueIsFragment)
		{
			XmlNamespaceManager nsMgr = DotNetXmlImpl.StringToNsMgr(xmlDoc.NameTable, nsMap);
			DotNetXmlImpl.Update(xmlDoc, xpathExpr, nsMgr, val, ref bValueIsFragment);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000A33FC File Offset: 0x000A15FC
		internal static XmlDocument GetXmlDocument(XmlTypeReader xmlReader, string value, out XmlDocumentFragment xmlDocFragmentInternal, out bool bIsFragment, bool bThrowException)
		{
			XmlDocument xmlDocument = null;
			xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			bIsFragment = false;
			xmlDocFragmentInternal = null;
			try
			{
				xmlDocument.Load(xmlReader);
			}
			catch (Exception)
			{
				if (bThrowException)
				{
					throw;
				}
				xmlDocument.LoadXml("<OracleInternalXmlRoot/>");
				xmlDocFragmentInternal = xmlDocument.CreateDocumentFragment();
				xmlDocFragmentInternal.InnerXml = value;
				xmlDocument.DocumentElement.AppendChild(xmlDocFragmentInternal);
				bIsFragment = true;
			}
			return xmlDocument;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000A3470 File Offset: 0x000A1670
		internal static string NsMgrToString(XmlNamespaceManager nsMgr)
		{
			string text = null;
			if (nsMgr != null)
			{
				text = string.Empty;
				foreach (object obj in nsMgr)
				{
					string text2 = (string)obj;
					string text3 = nsMgr.LookupNamespace(text2);
					if ((text2 != null && text2.Length != 0) || (text3 != null && text3.Length != 0))
					{
						StringBuilder stringBuilder = new StringBuilder(text, 1024);
						if (text != null && text.Length != 0)
						{
							stringBuilder.Append(' ');
						}
						stringBuilder.Append("xmlns:");
						stringBuilder.Append(text2);
						stringBuilder.Append('=');
						stringBuilder.Append(text3);
						text = stringBuilder.ToString();
					}
				}
			}
			return text;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000A3544 File Offset: 0x000A1744
		internal static XmlNamespaceManager StringToNsMgr(XmlNameTable nameTable, string nsMap)
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(nameTable);
			if (string.IsNullOrEmpty(nsMap))
			{
				return xmlNamespaceManager;
			}
			string xml = "<Namespace1 " + nsMap + " ></Namespace1>";
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			foreach (object obj in xmlDocument.DocumentElement.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				if (xmlNamespaceManager.LookupNamespace(xmlAttribute.LocalName) == null)
				{
					xmlNamespaceManager.AddNamespace(xmlAttribute.LocalName, xmlAttribute.Value);
				}
			}
			return xmlNamespaceManager;
		}
	}
}
