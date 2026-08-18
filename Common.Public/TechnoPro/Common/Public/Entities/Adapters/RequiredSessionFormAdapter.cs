using System;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.RequiredSessionForm;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005CB RID: 1483
	public static class RequiredSessionFormAdapter
	{
		// Token: 0x06002FC7 RID: 12231 RVA: 0x00039F5C File Offset: 0x0003815C
		public static string ToXml(this RequiredSessionFormItem item)
		{
			return new RequiredSessionFormItem[]
			{
				item
			}.ToXml();
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00039F80 File Offset: 0x00038180
		public static string ToXml(this RequiredSessionFormItem[] items)
		{
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), Array.Empty<object>());
			int ctr = 0;
			xdocument.Add(new XElement("requiredforms", items.Select(delegate(RequiredSessionFormItem g)
			{
				XName name = "requiredform";
				object[] array = new object[8];
				int num = 0;
				XName name2 = "id";
				int ctr = ctr;
				ctr++;
				array[num] = new XAttribute(name2, ctr);
				array[1] = new XAttribute("name", g.Name ?? "");
				array[2] = new XAttribute("screennum", g.ScreenNum);
				array[3] = new XAttribute("disabled", g.Disabled.ToString());
				array[4] = new XAttribute("title", g.Title ?? "");
				array[5] = new XAttribute("ordernum", g.OrderNum.ToString());
				array[6] = new XElement("intro", g.Intro ?? "");
				array[7] = g.EmailTemplate.ToEmailXElement("emailtemplate");
				return new XElement(name, array);
			})));
			return xdocument.ToString();
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00039FEC File Offset: 0x000381EC
		public static RequiredSessionFormItem[] RequiredSessionsFormItemFromXml(this string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			RequiredSessionFormItem[] result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xml);
				result = (from <>h__TransparentIdentifier3 in (from <>h__TransparentIdentifier1 in (from e in xdocument.Descendants("requiredform")
				select new
				{
					e = e,
					attrName = e.Attribute("name")
				}).Select(delegate(<>h__TransparentIdentifier0)
				{
					XAttribute attrName = attrName;
					return new
					{
						<>h__TransparentIdentifier0 = <>h__TransparentIdentifier0,
						name = (((attrName != null) ? attrName.Value : null) ?? "")
					};
				})
				select new
				{
					<>h__TransparentIdentifier1 = <>h__TransparentIdentifier1,
					attrId = e.Attribute("id")
				}).Select(delegate(<>h__TransparentIdentifier2)
				{
					XAttribute attrId = attrId;
					return new
					{
						<>h__TransparentIdentifier2 = <>h__TransparentIdentifier2,
						id = ((attrId != null) ? attrId.GetIntFromAttribute(0) : 0)
					};
				})
				let attrDisabled = e.Attribute("disabled")
				let attrTitle = e.Attribute("title")
				let attrScreenNum = e.Attribute("screennum")
				let attrOrderNum = e.Attribute("ordernum")
				let elementIntro = e.Element("intro")
				select new
				{
					<>h__TransparentIdentifier8 = <>h__TransparentIdentifier8,
					elementEmailTemplate = e.Element("emailtemplate")
				}).Select(delegate(<>h__TransparentIdentifier9)
				{
					RequiredSessionFormItem requiredSessionFormItem = new RequiredSessionFormItem();
					requiredSessionFormItem.Name = ((<>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.name.Length > 0) ? <>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.<>h__TransparentIdentifier2.<>h__TransparentIdentifier1.name : <>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.id.ToString());
					requiredSessionFormItem.RequiredSessionFormItemId = <>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.<>h__TransparentIdentifier3.id;
					XAttribute attrDisabled = <>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.<>h__TransparentIdentifier4.attrDisabled;
					requiredSessionFormItem.Disabled = (attrDisabled != null && attrDisabled.GetBoolFromAttribute(false));
					XAttribute attrScreenNum = <>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.attrScreenNum;
					requiredSessionFormItem.ScreenNum = ((attrScreenNum != null) ? attrScreenNum.GetIntFromAttribute(0) : 0);
					XAttribute attrTitle = <>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.<>h__TransparentIdentifier6.<>h__TransparentIdentifier5.attrTitle;
					requiredSessionFormItem.Title = (((attrTitle != null) ? attrTitle.Value : null) ?? "");
					XAttribute attrOrderNum = <>h__TransparentIdentifier9.<>h__TransparentIdentifier8.<>h__TransparentIdentifier7.attrOrderNum;
					requiredSessionFormItem.OrderNum = ((attrOrderNum != null) ? attrOrderNum.GetIntFromAttribute(0) : 0);
					XElement elementIntro = <>h__TransparentIdentifier9.<>h__TransparentIdentifier8.elementIntro;
					requiredSessionFormItem.Intro = (((elementIntro != null) ? elementIntro.Value : null) ?? "");
					XElement elementEmailTemplate = <>h__TransparentIdentifier9.elementEmailTemplate;
					requiredSessionFormItem.EmailTemplate = ((elementEmailTemplate != null) ? elementEmailTemplate.EmailFromXml() : null);
					return requiredSessionFormItem;
				}).ToArray<RequiredSessionFormItem>();
			}
			return result;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x0003A1B8 File Offset: 0x000383B8
		public static RequiredSessionFormItem RequiredSessionFormItemFromXml(this string xml)
		{
			RequiredSessionFormItem[] array = xml.RequiredSessionsFormItemFromXml();
			return (array != null) ? array.FirstOrDefault<RequiredSessionFormItem>() : null;
		}
	}
}
