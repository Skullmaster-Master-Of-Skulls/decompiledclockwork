using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014E7 RID: 5351
	internal class GenericPadding : LengthProperty.Maker
	{
		// Token: 0x0600D613 RID: 54803 RVA: 0x002F5F73 File Offset: 0x002F4173
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericPadding(propName);
		}

		// Token: 0x0600D614 RID: 54804 RVA: 0x002F5F7B File Offset: 0x002F417B
		protected GenericPadding(string name) : base(name)
		{
		}

		// Token: 0x0600D615 RID: 54805 RVA: 0x002F5F84 File Offset: 0x002F4184
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D616 RID: 54806 RVA: 0x002F5F88 File Offset: 0x002F4188
		public override Property GetShorthand(PropertyList propertyList)
		{
			Property property = null;
			if (property == null)
			{
				ListProperty listProperty = (ListProperty)propertyList.GetExplicitProperty("padding");
				if (listProperty != null)
				{
					IShorthandParser shorthandParser = new BoxPropShorthandParser(listProperty);
					property = shorthandParser.GetValueForProperty(base.PropName, this, propertyList);
				}
			}
			return property;
		}

		// Token: 0x0600D617 RID: 54807 RVA: 0x002F5FC5 File Offset: 0x002F41C5
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AA8 RID: 15016
		private Property m_defaultProp;
	}
}
