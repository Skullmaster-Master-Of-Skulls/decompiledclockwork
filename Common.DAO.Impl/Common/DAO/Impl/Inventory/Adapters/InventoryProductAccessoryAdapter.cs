using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.DAO.Impl.Inventory.Adapters
{
	// Token: 0x020000C5 RID: 197
	public static class InventoryProductAccessoryAdapter
	{
		// Token: 0x0600054F RID: 1359 RVA: 0x00033144 File Offset: 0x00031344
		public static string ToXml(this IList<InventoryProductAccessory> accessories)
		{
			return new XElement("accessories", from accessory in accessories
			select new XElement("add", new object[]
			{
				new XAttribute("name", accessory.Name),
				new XAttribute("description", accessory.Description ?? string.Empty)
			})).ToString();
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00033190 File Offset: 0x00031390
		public static IList<InventoryProductAccessory> ToAccessoryList(this string xmlAccessories)
		{
			bool flag = string.IsNullOrEmpty(xmlAccessories);
			IList<InventoryProductAccessory> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				XDocument xdocument = XDocument.Parse(xmlAccessories);
				XElement xelement = xdocument.Element("accessories");
				bool flag2 = xelement == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					result = (from accessory in xelement.Descendants("add")
					select new InventoryProductAccessory
					{
						Name = accessory.Attribute("name").Value,
						Description = accessory.Attribute("description").Value
					}).ToList<InventoryProductAccessory>();
				}
			}
			return result;
		}
	}
}
