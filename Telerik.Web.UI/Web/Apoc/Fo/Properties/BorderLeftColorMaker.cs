using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001488 RID: 5256
	internal class BorderLeftColorMaker : GenericColor
	{
		// Token: 0x0600D4A9 RID: 54441 RVA: 0x002F22DC File Offset: 0x002F04DC
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderLeftColorMaker(propName);
		}

		// Token: 0x0600D4AA RID: 54442 RVA: 0x002F22E4 File Offset: 0x002F04E4
		protected BorderLeftColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4AB RID: 54443 RVA: 0x002F22ED File Offset: 0x002F04ED
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D4AC RID: 54444 RVA: 0x002F22F0 File Offset: 0x002F04F0
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmAbsToRel(0));
			stringBuilder.Append("-color");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4AD RID: 54445 RVA: 0x002F2350 File Offset: 0x002F0550
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

		// Token: 0x0600D4AE RID: 54446 RVA: 0x002F23E9 File Offset: 0x002F05E9
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039D8 RID: 14808
		private Property m_defaultProp;
	}
}
