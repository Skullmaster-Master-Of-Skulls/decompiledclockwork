using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001556 RID: 5462
	internal class PauseMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7AB RID: 55211 RVA: 0x002F8120 File Offset: 0x002F6320
		public new static PropertyMaker Maker(string propName)
		{
			return new PauseMaker(propName);
		}

		// Token: 0x0600D7AC RID: 55212 RVA: 0x002F8128 File Offset: 0x002F6328
		protected PauseMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7AD RID: 55213 RVA: 0x002F8131 File Offset: 0x002F6331
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7AE RID: 55214 RVA: 0x002F8134 File Offset: 0x002F6334
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B2A RID: 15146
		private Property m_defaultProp;
	}
}
