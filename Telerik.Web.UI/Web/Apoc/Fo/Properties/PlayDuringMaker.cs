using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001559 RID: 5465
	internal class PlayDuringMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7B7 RID: 55223 RVA: 0x002F81D4 File Offset: 0x002F63D4
		public new static PropertyMaker Maker(string propName)
		{
			return new PlayDuringMaker(propName);
		}

		// Token: 0x0600D7B8 RID: 55224 RVA: 0x002F81DC File Offset: 0x002F63DC
		protected PlayDuringMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7B9 RID: 55225 RVA: 0x002F81E5 File Offset: 0x002F63E5
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7BA RID: 55226 RVA: 0x002F81E8 File Offset: 0x002F63E8
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B2D RID: 15149
		private Property m_defaultProp;
	}
}
