using System;
using System.Collections;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x0200139C RID: 5020
	internal class GenericShorthandParser : IShorthandParser
	{
		// Token: 0x0600D101 RID: 53505 RVA: 0x002E411B File Offset: 0x002E231B
		public GenericShorthandParser(ListProperty listprop)
		{
			this.list = listprop.GetList();
		}

		// Token: 0x0600D102 RID: 53506 RVA: 0x002E412F File Offset: 0x002E232F
		protected Property getElement(int index)
		{
			if (this.list.Count > index)
			{
				return (Property)this.list[index];
			}
			return null;
		}

		// Token: 0x0600D103 RID: 53507 RVA: 0x002E4152 File Offset: 0x002E2352
		protected int count()
		{
			return this.list.Count;
		}

		// Token: 0x0600D104 RID: 53508 RVA: 0x002E4160 File Offset: 0x002E2360
		public Property GetValueForProperty(string propName, PropertyMaker maker, PropertyList propertyList)
		{
			if (this.count() == 1)
			{
				string @string = ((Property)this.list[0]).GetString();
				if (@string != null && @string.Equals("inherit"))
				{
					return propertyList.GetFromParentProperty(propName);
				}
			}
			return this.convertValueForProperty(propName, maker, propertyList);
		}

		// Token: 0x0600D105 RID: 53509 RVA: 0x002E41B0 File Offset: 0x002E23B0
		protected virtual Property convertValueForProperty(string propName, PropertyMaker maker, PropertyList propertyList)
		{
			foreach (object obj in this.list)
			{
				Property prop = (Property)obj;
				Property property = maker.ConvertShorthandProperty(propertyList, prop, null);
				if (property != null)
				{
					return property;
				}
			}
			return null;
		}

		// Token: 0x0400381B RID: 14363
		protected ArrayList list;
	}
}
