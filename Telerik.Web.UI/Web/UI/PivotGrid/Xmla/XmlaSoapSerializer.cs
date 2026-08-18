using System;
using System.Xml.Linq;

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D93 RID: 3475
	internal class XmlaSoapSerializer
	{
		// Token: 0x0600812A RID: 33066 RVA: 0x001D7D40 File Offset: 0x001D5F40
		public static XElement Serialize(object objectToSerialize)
		{
			IXmlaMethod xmlaMethod = objectToSerialize as IXmlaMethod;
			if (xmlaMethod != null)
			{
				return XmlaSoapSerializer.SerializeXmlaMethod(xmlaMethod);
			}
			SoapEnvelope soapEnvelope = objectToSerialize as SoapEnvelope;
			if (soapEnvelope != null)
			{
				return XmlaSoapSerializer.SerializeSoapEnvelope(soapEnvelope);
			}
			return null;
		}

		// Token: 0x0600812B RID: 33067 RVA: 0x001D7D70 File Offset: 0x001D5F70
		private static XElement SerializeSoapEnvelope(SoapEnvelope envelope)
		{
			XAttribute xattribute = new XAttribute(XNamespace.Xmlns + "soap", "http://schemas.xmlsoap.org/soap/envelope/");
			XElement xelement = new XElement(XName.Get("Header", "http://schemas.xmlsoap.org/soap/envelope/"));
			XElement xelement2 = new XElement(XName.Get("Body", "http://schemas.xmlsoap.org/soap/envelope/"), XElement.Parse(envelope.Body.Content));
			return new XElement(XName.Get("Envelope", "http://schemas.xmlsoap.org/soap/envelope/"), new object[]
			{
				xattribute,
				xelement,
				xelement2
			});
		}

		// Token: 0x0600812C RID: 33068 RVA: 0x001D7E00 File Offset: 0x001D6000
		private static XElement SerializeXmlaMethod(IXmlaMethod method)
		{
			XmlaMethodExecute xmlaMethodExecute = method as XmlaMethodExecute;
			if (xmlaMethodExecute != null)
			{
				return XmlaSoapSerializer.SerializeXmlaMethodExecute(xmlaMethodExecute);
			}
			XmlaMethodDiscover xmlaMethodDiscover = method as XmlaMethodDiscover;
			if (xmlaMethodDiscover != null)
			{
				return XmlaSoapSerializer.SerializeXmlaMethodDiscover(xmlaMethodDiscover);
			}
			return null;
		}

		// Token: 0x0600812D RID: 33069 RVA: 0x001D7E30 File Offset: 0x001D6030
		private static XElement SerializeXmlaCommand(IXmlaCommand command)
		{
			XElement content = XmlaSoapSerializer.SerializeXmlaCommandContent(command);
			return new XElement(XName.Get("Command", "urn:schemas-microsoft-com:xml-analysis"), content);
		}

		// Token: 0x0600812E RID: 33070 RVA: 0x001D7E5C File Offset: 0x001D605C
		private static XElement SerializeXmlaCommandContent(IXmlaCommand command)
		{
			XmlaTextBodyCommand xmlaTextBodyCommand = command as XmlaTextBodyCommand;
			if (xmlaTextBodyCommand != null)
			{
				return XmlaSoapSerializer.SerializeXmlaTextBodyCommand(xmlaTextBodyCommand);
			}
			return null;
		}

		// Token: 0x0600812F RID: 33071 RVA: 0x001D7E7C File Offset: 0x001D607C
		private static XElement SerializeXmlaTextBodyCommand(XmlaTextBodyCommand command)
		{
			return new XElement(XName.Get(command.Name, "urn:schemas-microsoft-com:xml-analysis"), command.Body);
		}

		// Token: 0x06008130 RID: 33072 RVA: 0x001D7EA8 File Offset: 0x001D60A8
		private static XElement SerializeXmlaMethodDiscover(XmlaMethodDiscover method)
		{
			XElement xelement = new XElement(XName.Get("RequestType", "urn:schemas-microsoft-com:xml-analysis"), method.RequestType);
			XElement xelement2 = XmlaSoapSerializer.SerializeXmlaMethodRestictions(method);
			XElement xelement3 = XmlaSoapSerializer.SerializeXmlaMethodProperties(method);
			return new XElement(XName.Get("Discover", "urn:schemas-microsoft-com:xml-analysis"), new object[]
			{
				xelement,
				xelement2,
				xelement3
			});
		}

		// Token: 0x06008131 RID: 33073 RVA: 0x001D7F08 File Offset: 0x001D6108
		private static XElement SerializeXmlaMethodExecute(XmlaMethodExecute method)
		{
			XElement xelement = XmlaSoapSerializer.SerializeXmlaCommand(method.Command);
			XElement xelement2 = XmlaSoapSerializer.SerializeXmlaMethodProperties(method);
			return new XElement(XName.Get("Execute", "urn:schemas-microsoft-com:xml-analysis"), new object[]
			{
				xelement,
				xelement2
			});
		}

		// Token: 0x06008132 RID: 33074 RVA: 0x001D7F4C File Offset: 0x001D614C
		private static XElement SerializeXmlaMethodProperties(IXmlaMethod method)
		{
			XElement content = XmlaSoapSerializer.SerializeXmlaMethodPropertyList(method);
			return new XElement(XName.Get("Properties", "urn:schemas-microsoft-com:xml-analysis"), content);
		}

		// Token: 0x06008133 RID: 33075 RVA: 0x001D7F78 File Offset: 0x001D6178
		private static XElement SerializeXmlaMethodPropertyList(IXmlaMethod method)
		{
			XElement xelement = new XElement(XName.Get("PropertyList", "urn:schemas-microsoft-com:xml-analysis"));
			foreach (IXmlaMethodProperty property in method.Properties)
			{
				XElement content = XmlaSoapSerializer.SerializeXmlaMethodProperty(property);
				xelement.Add(content);
			}
			return xelement;
		}

		// Token: 0x06008134 RID: 33076 RVA: 0x001D7FE4 File Offset: 0x001D61E4
		private static XElement SerializeXmlaMethodProperty(IXmlaMethodProperty property)
		{
			return new XElement(XName.Get(property.Name, "urn:schemas-microsoft-com:xml-analysis"), property.Value);
		}

		// Token: 0x06008135 RID: 33077 RVA: 0x001D8010 File Offset: 0x001D6210
		private static XElement SerializeXmlaMethodRestictions(XmlaMethodDiscover method)
		{
			XElement content = XmlaSoapSerializer.SerializeXmlaMethodRestictionList(method);
			return new XElement(XName.Get("Restrictions", "urn:schemas-microsoft-com:xml-analysis"), content);
		}

		// Token: 0x06008136 RID: 33078 RVA: 0x001D803C File Offset: 0x001D623C
		private static XElement SerializeXmlaMethodRestictionList(XmlaMethodDiscover method)
		{
			XElement xelement = new XElement(XName.Get("RestrictionList", "urn:schemas-microsoft-com:xml-analysis"));
			foreach (XmlaRestrictionProperty property in method.Restrictions)
			{
				XElement content = XmlaSoapSerializer.SerializeXmlaMethodRestriction(property);
				xelement.Add(content);
			}
			return xelement;
		}

		// Token: 0x06008137 RID: 33079 RVA: 0x001D80A8 File Offset: 0x001D62A8
		private static XElement SerializeXmlaMethodRestriction(XmlaRestrictionProperty property)
		{
			return new XElement(XName.Get(property.Name, "urn:schemas-microsoft-com:xml-analysis"), property.Value);
		}

		// Token: 0x040023A7 RID: 9127
		public const string ExecuteMethodNamespace = "urn:schemas-microsoft-com:xml-analysis";

		// Token: 0x040023A8 RID: 9128
		public const string SoapEnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
	}
}
