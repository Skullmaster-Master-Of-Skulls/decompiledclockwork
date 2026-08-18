using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001531 RID: 5425
	internal class MediaUsageMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D731 RID: 55089 RVA: 0x002F76BB File Offset: 0x002F58BB
		public new static PropertyMaker Maker(string propName)
		{
			return new MediaUsageMaker(propName);
		}

		// Token: 0x0600D732 RID: 55090 RVA: 0x002F76C3 File Offset: 0x002F58C3
		protected MediaUsageMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D733 RID: 55091 RVA: 0x002F76CC File Offset: 0x002F58CC
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D734 RID: 55092 RVA: 0x002F76CF File Offset: 0x002F58CF
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B03 RID: 15107
		private Property m_defaultProp;
	}
}
