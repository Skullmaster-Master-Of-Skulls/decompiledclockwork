using System;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x0200139D RID: 5021
	internal class BoxPropShorthandParser : GenericShorthandParser
	{
		// Token: 0x0600D106 RID: 53510 RVA: 0x002E421C File Offset: 0x002E241C
		public BoxPropShorthandParser(ListProperty listprop) : base(listprop)
		{
		}

		// Token: 0x0600D107 RID: 53511 RVA: 0x002E4228 File Offset: 0x002E2428
		protected override Property convertValueForProperty(string propName, PropertyMaker maker, PropertyList propertyList)
		{
			Property property = null;
			if (propName.IndexOf("-top") >= 0)
			{
				property = base.getElement(0);
			}
			else if (propName.IndexOf("-right") >= 0)
			{
				property = base.getElement((base.count() > 1) ? 1 : 0);
			}
			else if (propName.IndexOf("-bottom") >= 0)
			{
				property = base.getElement((base.count() > 2) ? 2 : 0);
			}
			else if (propName.IndexOf("-left") >= 0)
			{
				property = base.getElement((base.count() > 3) ? 3 : ((base.count() > 1) ? 1 : 0));
			}
			if (property != null)
			{
				return maker.ConvertShorthandProperty(propertyList, property, null);
			}
			return property;
		}
	}
}
