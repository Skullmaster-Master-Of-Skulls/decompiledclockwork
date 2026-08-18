using System;
using System.IO;
using System.Security.Permissions;

namespace System.EnterpriseServices.Internal
{
	// Token: 0x020000D1 RID: 209
	internal class HomePage
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000E204 File Offset: 0x0000D204
		public void Create(string FilePath, string VirtualRoot, string PageName, string DiscoRef)
		{
			try
			{
				if (!FilePath.EndsWith("/", StringComparison.Ordinal) && !FilePath.EndsWith("\\", StringComparison.Ordinal))
				{
					FilePath += "\\";
				}
				if (!File.Exists(FilePath + PageName))
				{
					SecurityPermission securityPermission = new SecurityPermission(SecurityPermissionFlag.RemotingConfiguration);
					securityPermission.Demand();
					string str = FilePath + "web.config";
					string text = "<%@ Import Namespace=\"System.Collections\" %>\r\n";
					text += "<%@ Import Namespace=\"System.IO\" %>\r\n";
					text += "<%@ Import Namespace=\"System.Xml.Serialization\" %>\r\n";
					text += "<%@ Import Namespace=\"System.Xml\" %>\r\n";
					text += "<%@ Import Namespace=\"System.Xml.Schema\" %>\r\n";
					text += "<%@ Import Namespace=\"System.Web.Services.Description\" %>\r\n";
					text += "<%@ Import Namespace=\"System\" %>\r\n";
					text += "<%@ Import Namespace=\"System.Globalization\" %>\r\n";
					text += "<%@ Import Namespace=\"System.Resources\" %>\r\n";
					text += "<%@ Import Namespace=\"System.Diagnostics\" %>\r\n";
					text += "<html>\r\n";
					text += "<script language=\"C#\" runat=\"server\">\r\n";
					text += "    string soapNs = \"http://schemas.xmlsoap.org/soap/envelope/\";\r\n";
					text += "    string soapEncNs = \"http://schemas.xmlsoap.org/soap/encoding/\";\r\n";
					text += "    string urtNs = \"urn:schemas-microsoft-com:urt-types\";\r\n";
					text += "    string wsdlNs = \"http://schemas.xmlsoap.org/wsdl/\";\r\n";
					text = text + "    string VRoot = \"" + VirtualRoot + "\";\r\n";
					text += "    string ServiceName() { return VRoot; }\r\n";
					text += "\r\n";
					text += "   XmlNode GetNextNamedSiblingNode(XmlNode inNode, string name)\r\n";
					text += "    {\r\n";
					text += "       if (inNode == null ) return inNode;\r\n";
					text += "      if (inNode.Name == name) return inNode;\r\n";
					text += "       XmlNode newNode = inNode.NextSibling;\r\n";
					text += "       if (newNode == null) return newNode;\r\n";
					text += "       if (newNode.Name == name ) return newNode;\r\n";
					text += "       bool found = false;\r\n";
					text += "       while (!found)\r\n";
					text += "       {\r\n";
					text += "           XmlNode oldNode = newNode;\r\n";
					text += "           newNode = oldNode.NextSibling;\r\n";
					text += "           if (null == newNode || newNode == oldNode)\r\n";
					text += "           {\r\n";
					text += "               newNode = null;\r\n";
					text += "               break;\r\n";
					text += "           }\r\n";
					text += "           if (newNode.Name == name) found = true;\r\n";
					text += "       }\r\n";
					text += "       return newNode;\r\n";
					text += "   }\r\n";
					text += "\r\n";
					text += "   string GetNodes()\r\n";
					text += "   {\r\n";
					text += "       string retval = \"\";\r\n";
					text += "       XmlDocument configXml = new XmlDocument();\r\n";
					text = text + "      configXml.Load(@\"" + str + "\");\r\n";
					text += "       XmlNode node= configXml.DocumentElement;\r\n";
					text += "        node = GetNextNamedSiblingNode(node,\"configuration\");\r\n";
					text += "        node = GetNextNamedSiblingNode(node.FirstChild, \"system.runtime.remoting\");\r\n";
					text += "        node = GetNextNamedSiblingNode(node.FirstChild, \"application\");\r\n";
					text += "        node = GetNextNamedSiblingNode(node.FirstChild, \"service\");\r\n";
					text += "        node = GetNextNamedSiblingNode(node.FirstChild, \"wellknown\");\r\n";
					text += "       while (node != null)\r\n";
					text += "       {\r\n";
					text += "           XmlNode attribType = node.Attributes.GetNamedItem(\"objectUri\");\r\n";
					text += "           retval += \"<a href=\" + attribType.Value + \"?WSDL>\" + attribType.Value +\"?WSDL</a><br><br>\";\r\n";
					text += "           node = GetNextNamedSiblingNode(node.NextSibling, \"wellknown\");\r\n";
					text += "       }\r\n";
					text += "        return retval;\r\n";
					text += "    }\r\n";
					text += "\r\n";
					text += "</script>\r\n";
					text += "<title><% = ServiceName() %></title>\r\n";
					text += "<head>\r\n";
					text = text + "<link type='text/xml' rel='alternate' href='" + DiscoRef + "' />\r\n";
					text += "\r\n";
					text += "   <style type=\"text/css\">\r\n";
					text += " \r\n";
					text += "       BODY { color: #000000; background-color: white; font-family: \"Verdana\"; margin-left: 0px; margin-top: 0px; }\r\n";
					text += "       #content { margin-left: 30px; font-size: .70em; padding-bottom: 2em; }\r\n";
					text += "       A:link { color: #336699; font-weight: bold; text-decoration: underline; }\r\n";
					text += "       A:visited { color: #6699cc; font-weight: bold; text-decoration: underline; }\r\n";
					text += "       A:active { color: #336699; font-weight: bold; text-decoration: underline; }\r\n";
					text += "       A:hover { color: cc3300; font-weight: bold; text-decoration: underline; }\r\n";
					text += "       P { color: #000000; margin-top: 0px; margin-bottom: 12px; font-family: \"Verdana\"; }\r\n";
					text += "       pre { background-color: #e5e5cc; padding: 5px; font-family: \"Courier New\"; font-size: x-small; margin-top: -5px; border: 1px #f0f0e0 solid; }\r\n";
					text += "       td { color: #000000; font-family: verdana; font-size: .7em; }\r\n";
					text += "       h2 { font-size: 1.5em; font-weight: bold; margin-top: 25px; margin-bottom: 10px; border-top: 1px solid #003366; margin-left: -15px; color: #003366; }\r\n";
					text += "       h3 { font-size: 1.1em; color: #000000; margin-left: -15px; margin-top: 10px; margin-bottom: 10px; }\r\n";
					text += "       ul, ol { margin-top: 10px; margin-left: 20px; }\r\n";
					text += "       li { margin-top: 10px; color: #000000; }\r\n";
					text += "       font.value { color: darkblue; font: bold; }\r\n";
					text += "       font.key { color: darkgreen; font: bold; }\r\n";
					text += "       .heading1 { color: #ffffff; font-family: \"Tahoma\"; font-size: 26px; font-weight: normal; background-color: #003366; margin-top: 0px; margin-bottom: 0px; margin-left: 0px; padding-top: 10px; padding-bottom: 3px; padding-left: 15px; width: 105%; }\r\n";
					text += "       .button { background-color: #dcdcdc; font-family: \"Verdana\"; font-size: 1em; border-top: #cccccc 1px solid; border-bottom: #666666 1px solid; border-left: #cccccc 1px solid; border-right: #666666 1px solid; }\r\n";
					text += "       .frmheader { color: #000000; background: #dcdcdc; font-family: \"Verdana\"; font-size: .7em; font-weight: normal; border-bottom: 1px solid #dcdcdc; padding-top: 2px; padding-bottom: 2px; }\r\n";
					text += "       .frmtext { font-family: \"Verdana\"; font-size: .7em; margin-top: 8px; margin-bottom: 0px; margin-left: 32px; }\r\n";
					text += "       .frmInput { font-family: \"Verdana\"; font-size: 1em; }\r\n";
					text += "       .intro { margin-left: -15px; }\r\n";
					text += " \r\n";
					text += "    </style>\r\n";
					text += "\r\n";
					text += "</head>\r\n";
					text += "<body>\r\n";
					text += "<p class=\"heading1\"><% = ServiceName() %></p><br>\r\n";
					text += "<% = GetNodes() %>\r\n";
					text += "</body>\r\n";
					text += "</html>\r\n";
					FileStream fileStream = new FileStream(FilePath + PageName, FileMode.Create);
					StreamWriter streamWriter = new StreamWriter(fileStream);
					streamWriter.Write(text);
					streamWriter.Close();
					fileStream.Close();
				}
			}
			catch (Exception ex)
			{
				ComSoapPublishError.Report(ex.ToString());
			}
			catch
			{
				ComSoapPublishError.Report(Resource.FormatString("Err_NonClsException", "HomePage.Create"));
			}
		}
	}
}
