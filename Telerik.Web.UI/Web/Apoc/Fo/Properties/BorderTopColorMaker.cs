using System;
using System.Text;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200149D RID: 5277
	internal class BorderTopColorMaker : GenericColor
	{
		// Token: 0x0600D4FB RID: 54523 RVA: 0x002F2EA0 File Offset: 0x002F10A0
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderTopColorMaker(propName);
		}

		// Token: 0x0600D4FC RID: 54524 RVA: 0x002F2EA8 File Offset: 0x002F10A8
		protected BorderTopColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4FD RID: 54525 RVA: 0x002F2EB1 File Offset: 0x002F10B1
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D4FE RID: 54526 RVA: 0x002F2EB4 File Offset: 0x002F10B4
		public override Property Compute(PropertyList propertyList)
		{
			FObj parentFObj = propertyList.getParentFObj();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("border-");
			stringBuilder.Append(propertyList.wmAbsToRel(2));
			stringBuilder.Append("-color");
			Property property = propertyList.GetExplicitOrShorthandProperty(stringBuilder.ToString());
			if (property != null)
			{
				property = this.ConvertProperty(property, propertyList, parentFObj);
			}
			return property;
		}

		// Token: 0x0600D4FF RID: 54527 RVA: 0x002F2F14 File Offset: 0x002F1114
		public override Property GetShorthand(PropertyList propertyList)
		{
			Property property = null;
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("border-top");
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

		// Token: 0x0600D500 RID: 54528 RVA: 0x002F2FAD File Offset: 0x002F11AD
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "black", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039E1 RID: 14817
		private Property m_defaultProp;
	}
}
