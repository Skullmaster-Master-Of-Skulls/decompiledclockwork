using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001478 RID: 5240
	internal class BorderBottomColorMaker : GenericColor
	{
		// Token: 0x0600D470 RID: 54384 RVA: 0x002F1AF8 File Offset: 0x002EFCF8
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderBottomColorMaker(propName);
		}

		// Token: 0x0600D471 RID: 54385 RVA: 0x002F1B00 File Offset: 0x002EFD00
		protected BorderBottomColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D472 RID: 54386 RVA: 0x002F1B09 File Offset: 0x002EFD09
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D473 RID: 54387 RVA: 0x002F1B0C File Offset: 0x002EFD0C
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmAbsToRel(3));
			stringBuilder.Append("-color");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D474 RID: 54388 RVA: 0x002F1B6C File Offset: 0x002EFD6C
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
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-color");
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

		// Token: 0x0600D475 RID: 54389 RVA: 0x002F1C05 File Offset: 0x002EFE05
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039CE RID: 14798
		private Property m_defaultProp;
	}
}
