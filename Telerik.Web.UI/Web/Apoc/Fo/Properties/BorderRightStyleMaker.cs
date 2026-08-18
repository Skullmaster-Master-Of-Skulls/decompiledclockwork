using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001491 RID: 5265
	internal class BorderRightStyleMaker : GenericBorderStyle
	{
		// Token: 0x0600D4C8 RID: 54472 RVA: 0x002F27AD File Offset: 0x002F09AD
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderRightStyleMaker(propName);
		}

		// Token: 0x0600D4C9 RID: 54473 RVA: 0x002F27B5 File Offset: 0x002F09B5
		protected BorderRightStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4CA RID: 54474 RVA: 0x002F27C0 File Offset: 0x002F09C0
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmAbsToRel(1));
			stringBuilder.Append("-style");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4CB RID: 54475 RVA: 0x002F2820 File Offset: 0x002F0A20
		public override Property GetShorthand(PropertyList propertyList)
		{
			Property property = null;
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-right");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser = new GenericShorthandParser(listProperty);
					property = shorthandParser.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-style");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser2 = new BoxPropShorthandParser(listProperty);
					property = shorthandParser2.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser3 = new GenericShorthandParser(listProperty);
					property = shorthandParser3.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			return property;
		}
	}
}
