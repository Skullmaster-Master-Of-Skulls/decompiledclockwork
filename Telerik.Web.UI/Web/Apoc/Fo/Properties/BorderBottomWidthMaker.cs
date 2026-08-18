using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200147D RID: 5245
	internal class BorderBottomWidthMaker : GenericBorderWidth
	{
		// Token: 0x0600D485 RID: 54405 RVA: 0x002F1E5E File Offset: 0x002F005E
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderBottomWidthMaker(propName);
		}

		// Token: 0x0600D486 RID: 54406 RVA: 0x002F1E66 File Offset: 0x002F0066
		protected BorderBottomWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D487 RID: 54407 RVA: 0x002F1E70 File Offset: 0x002F0070
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmAbsToRel(3));
			stringBuilder.Append("-width");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D488 RID: 54408 RVA: 0x002F1ED0 File Offset: 0x002F00D0
		public override Property GetShorthand(PropertyList propertyList)
		{
			Property property = null;
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-bottom");
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
