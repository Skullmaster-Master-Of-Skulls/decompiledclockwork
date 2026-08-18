using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001492 RID: 5266
	internal class BorderRightWidthMaker : GenericBorderWidth
	{
		// Token: 0x0600D4CC RID: 54476 RVA: 0x002F28B9 File Offset: 0x002F0AB9
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderRightWidthMaker(propName);
		}

		// Token: 0x0600D4CD RID: 54477 RVA: 0x002F28C1 File Offset: 0x002F0AC1
		protected BorderRightWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4CE RID: 54478 RVA: 0x002F28CC File Offset: 0x002F0ACC
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmAbsToRel(1));
			stringBuilder.Append("-width");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4CF RID: 54479 RVA: 0x002F292C File Offset: 0x002F0B2C
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
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-width");
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
