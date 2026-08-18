using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005CE RID: 1486
	public static class TemplateAdapter
	{
		// Token: 0x06002FCF RID: 12239 RVA: 0x0003A3FC File Offset: 0x000385FC
		public static XElement TemplateToXElement(this Template template)
		{
			bool flag = template == null;
			if (flag)
			{
				template = new Template();
			}
			XElement xelement = new XElement("Template", new object[]
			{
				new XAttribute("TemplateId", template.TemplateId.ToString()),
				new XAttribute("Title", template.TemplateTitle ?? ""),
				new XAttribute("Type", ((int)template.TemplateType).ToString()),
				new XAttribute("OrderNum", template.OrderNum.ToString()),
				new XElement("TemplateGroup", new object[]
				{
					new XAttribute("GroupTitle", (template.Group == null) ? "" : (template.Group.Title ?? "")),
					new XAttribute("GroupId", (template.Group == null) ? "" : (template.Group.TemplateGroupId ?? "")),
					new XAttribute("GroupOrderNum", (template.Group == null) ? "" : template.Group.OrderNum.ToString())
				})
			});
			bool flag2 = template.Document != null;
			if (flag2)
			{
				xelement.Add(new XElement("Document", new object[]
				{
					new XAttribute("Filename", template.Document.FileName ?? ""),
					new XAttribute("Filesize", template.Document.FileSize.ToString()),
					new XElement("DocumentBinary", (template.Document.ByteArray == null) ? "" : Convert.ToBase64String(template.Document.ByteArray))
				}));
			}
			bool flag3 = template.EmailBehindDocumentTemplate != null;
			if (flag3)
			{
				xelement.Add(template.EmailBehindDocumentTemplate.ToEmailXElement("EmailBehind"));
			}
			bool flag4 = template.EmailTemplate != null;
			if (flag4)
			{
				xelement.Add(template.EmailTemplate.ToEmailXElement("Email"));
			}
			return xelement;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x0003A668 File Offset: 0x00038868
		public static string TemplateToXml(this Template template)
		{
			XElement xelement = template.TemplateToXElement();
			return xelement.ToString();
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x0003A688 File Offset: 0x00038888
		public static string TemplatesToXml(this IList<Template> templates)
		{
			bool flag = templates == null;
			if (flag)
			{
				templates = new List<Template>();
			}
			XElement xelement = new XElement("Templates");
			foreach (Template template in templates)
			{
				xelement.Add(template.TemplateToXElement());
			}
			return xelement.ToString();
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x0003A704 File Offset: 0x00038904
		public static TemplateCollection TemplatesFromXml(this string xml)
		{
			return xml.TemplatesFromXml(true);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x0003A720 File Offset: 0x00038920
		public static TemplateCollection TemplatesFromXml(this string xml, bool LoadBinary)
		{
			XDocument xdocument = XDocument.Parse(xml ?? "");
			IList<TemplateGroup> groups = new List<TemplateGroup>();
			List<Template> templates = (from t in xdocument.Descendants("Template")
			let tDocument = t.Element("Document")
			let tDocumentFilename = (tDocument == null) ? null : tDocument.Attribute("Filename")
			let tDocumentFileSize = (tDocument == null) ? null : tDocument.Attribute("Filesize")
			let tDocumentBinary = (tDocument == null) ? null : tDocument.Element("DocumentBinary")
			let tEmailBehind = t.Element("EmailBehind")
			let tEmail = t.Element("Email")
			let tGroup = t.Element("TemplateGroup")
			let tTitle = t.Attribute("Title")
			let tOrderNum = t.Attribute("OrderNum")
			let tTemplateId = t.Attribute("TemplateId")
			select new
			{
				<>h__TransparentIdentifier9 = <>h__TransparentIdentifier9,
				tTemplateType = t.Attribute("Type")
			}).Select(delegate(<>h__TransparentIdentifier10)
			{
				Template template = new Template();
				template.TemplateTitle = ((<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.tTitle == null) ? "" : (<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.tTitle.Value ?? ""));
				template.TemplateType = TemplateAdapter.GetTemplateTypeFromAttribute(<>h__TransparentIdentifier10.tTemplateType);
				template.TemplateId = ((<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.tTemplateId == null || string.IsNullOrEmpty(<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.tTemplateId.Value)) ? 0 : TemplateAdapter.GetIntFromString(<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.tTemplateId.Value, 0));
				template.OrderNum = ((<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.tOrderNum == null || string.IsNullOrEmpty(<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.tOrderNum.Value)) ? 0 : TemplateAdapter.GetIntFromString(<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.tOrderNum.Value, 0));
				template.Group = TemplateAdapter.GetTemplateGroup(<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.tGroup, ref groups);
				BinaryFile document;
				if (<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.<>h__TransparentIdentifier0.tDocument != null)
				{
					BinaryFile binaryFile = new BinaryFile();
					binaryFile.FileName = ((<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.tDocumentFilename != null && <>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.tDocumentFilename.Value != null) ? <>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.tDocumentFilename.Value : "");
					binaryFile.FileSize = TemplateAdapter.GetIntFromString((<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.tDocumentFileSize != null && <>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.tDocumentFileSize.Value != null) ? <>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.tDocumentFileSize.Value : "", 0);
					document = binaryFile;
					binaryFile.ByteArray = ((!LoadBinary || <>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.tDocumentBinary == null || string.IsNullOrEmpty(<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.tDocumentBinary.Value)) ? null : Convert.FromBase64String(<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.tDocumentBinary.Value));
				}
				else
				{
					document = null;
				}
				template.Document = document;
				template.EmailBehindDocumentTemplate = ((<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.tEmailBehind == null) ? null : <>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.tEmailBehind.ToString().EmailFromXml("EmailBehind"));
				template.EmailTemplate = ((<>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.tEmail == null) ? null : <>h__TransparentIdentifier10.<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.tEmail.ToString().EmailFromXml("Email"));
				return template;
			}).ToList<Template>();
			return new TemplateCollection
			{
				Groups = groups,
				Templates = templates
			};
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x0003A928 File Offset: 0x00038B28
		private static eTemplateType GetTemplateTypeFromAttribute(XAttribute xa)
		{
			bool flag = xa == null || string.IsNullOrEmpty(xa.Value);
			eTemplateType result;
			if (flag)
			{
				result = eTemplateType.Unknown;
			}
			else
			{
				int intFromString = TemplateAdapter.GetIntFromString(xa.Value, 0);
				bool flag2 = intFromString < 1;
				if (flag2)
				{
					result = eTemplateType.Unknown;
				}
				else
				{
					bool flag3 = !Enum.IsDefined(typeof(eTemplateType), intFromString);
					if (flag3)
					{
						result = eTemplateType.Unknown;
					}
					else
					{
						result = (eTemplateType)intFromString;
					}
				}
			}
			return result;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x0003A990 File Offset: 0x00038B90
		private static TemplateGroup GetTemplateGroup(XElement element, ref IList<TemplateGroup> groups)
		{
			bool flag = element == null;
			TemplateGroup result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XAttribute xattribute = element.Attribute("GroupId");
				XAttribute xattribute2 = element.Attribute("GroupTitle");
				XAttribute xattribute3 = element.Attribute("GroupOrderNum");
				TemplateGroup grp = new TemplateGroup
				{
					TemplateGroupId = ((xattribute == null) ? "" : (xattribute.Value ?? "")),
					Title = ((xattribute2 == null) ? "" : (xattribute2.Value ?? "")),
					OrderNum = ((xattribute3 == null || string.IsNullOrEmpty(xattribute3.Value)) ? 0 : TemplateAdapter.GetIntFromString(xattribute3.Value, 0))
				};
				bool flag2 = string.IsNullOrEmpty(grp.TemplateGroupId);
				if (flag2)
				{
					result = grp;
				}
				else
				{
					TemplateGroup templateGroup = groups.FirstOrDefault((TemplateGroup g) => g.TemplateGroupId != null && g.TemplateGroupId.Equals(grp.TemplateGroupId, StringComparison.OrdinalIgnoreCase));
					bool flag3 = templateGroup != null;
					if (flag3)
					{
						result = templateGroup;
					}
					else
					{
						groups.Add(grp);
						result = grp;
					}
				}
			}
			return result;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x0003AABC File Offset: 0x00038CBC
		private static int GetIntFromString(string s, int defaultValue = 0)
		{
			bool flag = string.IsNullOrEmpty(s);
			int result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				int num;
				bool flag2 = !int.TryParse(s, out num);
				if (flag2)
				{
					result = defaultValue;
				}
				else
				{
					result = num;
				}
			}
			return result;
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0003AAF0 File Offset: 0x00038CF0
		public static Template TemplateFromXml(this string xml)
		{
			TemplateCollection templateCollection = xml.TemplatesFromXml();
			return (templateCollection == null || templateCollection.Templates == null || templateCollection.Templates.Count < 1) ? null : templateCollection.Templates[0];
		}
	}
}
