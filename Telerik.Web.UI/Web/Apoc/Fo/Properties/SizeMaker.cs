using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001578 RID: 5496
	internal class SizeMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D82D RID: 55341 RVA: 0x002F8B22 File Offset: 0x002F6D22
		public new static PropertyMaker Maker(string propName)
		{
			return new SizeMaker(propName);
		}

		// Token: 0x0600D82E RID: 55342 RVA: 0x002F8B2A File Offset: 0x002F6D2A
		protected SizeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D82F RID: 55343 RVA: 0x002F8B33 File Offset: 0x002F6D33
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D830 RID: 55344 RVA: 0x002F8B36 File Offset: 0x002F6D36
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B75 RID: 15221
		private Property m_defaultProp;
	}
}
