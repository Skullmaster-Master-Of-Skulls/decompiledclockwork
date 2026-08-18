using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200148B RID: 5259
	internal class BorderLeftStyleMaker : GenericBorderStyle
	{
		// Token: 0x0600D4B3 RID: 54451 RVA: 0x002F242D File Offset: 0x002F062D
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderLeftStyleMaker(propName);
		}

		// Token: 0x0600D4B4 RID: 54452 RVA: 0x002F2435 File Offset: 0x002F0635
		protected BorderLeftStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4B5 RID: 54453 RVA: 0x002F2440 File Offset: 0x002F0640
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmAbsToRel(0));
			stringBuilder.Append("-style");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4B6 RID: 54454 RVA: 0x002F24A0 File Offset: 0x002F06A0
		public override Property GetShorthand(PropertyList propertyList)
		{
			Property property = null;
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-left");
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
