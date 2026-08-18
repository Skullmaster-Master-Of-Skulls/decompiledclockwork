using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001560 RID: 5472
	internal class ReferenceOrientationMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7D1 RID: 55249 RVA: 0x002F83EA File Offset: 0x002F65EA
		public new static PropertyMaker Maker(string propName)
		{
			return new ReferenceOrientationMaker(propName);
		}

		// Token: 0x0600D7D2 RID: 55250 RVA: 0x002F83F2 File Offset: 0x002F65F2
		protected ReferenceOrientationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7D3 RID: 55251 RVA: 0x002F83FB File Offset: 0x002F65FB
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D7D4 RID: 55252 RVA: 0x002F83FE File Offset: 0x002F65FE
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B3E RID: 15166
		private Property m_defaultProp;
	}
}
