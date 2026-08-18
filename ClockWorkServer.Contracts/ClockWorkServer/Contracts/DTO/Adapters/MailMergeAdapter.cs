using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.Labels;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C86 RID: 3206
	public static class MailMergeAdapter
	{
		// Token: 0x060042D8 RID: 17112 RVA: 0x00022544 File Offset: 0x00020744
		public static IList<LabelTemplateDTO> ParseLabelTemplatesFromXml(this string Xml)
		{
			bool flag = string.IsNullOrEmpty(Xml);
			IList<LabelTemplateDTO> result;
			if (flag)
			{
				result = new List<LabelTemplateDTO>();
			}
			else
			{
				XDocument xdocument = XDocument.Parse(Xml);
				int num;
				List<LabelTemplateDTO> list = (from lbl in xdocument.Descendants("label")
				select new LabelTemplateDTO
				{
					Name = lbl.Element("name").Value,
					Template = new MailMergeTemplateDTO
					{
						Template = lbl.Element("template").Value,
						AllCaps = lbl.Element("allcaps").Value.Equals("1"),
						FontName = lbl.Element("fontname").Value,
						FontSize = (int.TryParse(lbl.Element("fontsize").Value, out num) ? num : 8)
					},
					DefaultPrinterSettings = new MailMergeDefaultPrinterSettingsDTO
					{
						CopyCount = (int.TryParse(lbl.Element("defaultcopycount").Value, out num) ? num : 1),
						DefaultPageSize = lbl.Element("defaultpapersize").Value,
						Orientation = (Enum.IsDefined(typeof(ePageOrientationDTO), lbl.Element("orientation").Value) ? ((ePageOrientationDTO)Enum.Parse(typeof(ePageOrientationDTO), lbl.Element("orientation").Value)) : ePageOrientationDTO.Portrait),
						PrinterName = lbl.Element("defaultprinter").Value
					}
				}).ToList<LabelTemplateDTO>();
				result = list;
			}
			return result;
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x000225A0 File Offset: 0x000207A0
		public static string GetXml(this IList<LabelTemplateDTO> Templates)
		{
			XDeclaration declaration = new XDeclaration("1.0", "utf-8", "yes");
			object[] array = new object[1];
			array[0] = new XElement("labels", from lbl in Templates
			select new XElement("label", new object[]
			{
				new XElement("name", lbl.Name),
				new XElement("template", lbl.Template.Template),
				new XElement("allcaps", lbl.Template.AllCaps ? "1" : "0"),
				new XElement("fontname", lbl.Template.FontName),
				new XElement("fontsize", lbl.Template.FontSize),
				new XElement("defaultcopycount", lbl.DefaultPrinterSettings.CopyCount.ToString()),
				new XElement("defaultpapersize", lbl.DefaultPrinterSettings.DefaultPageSize),
				new XElement("orientation", lbl.DefaultPrinterSettings.Orientation.ToString()),
				new XElement("defaultprinter", lbl.DefaultPrinterSettings.PrinterName)
			}));
			XDocument xdocument = new XDocument(declaration, array);
			return xdocument.ToString();
		}
	}
}
