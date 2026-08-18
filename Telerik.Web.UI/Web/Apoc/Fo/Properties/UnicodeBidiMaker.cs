using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015AA RID: 5546
	internal class UnicodeBidiMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8CA RID: 55498 RVA: 0x002F97EC File Offset: 0x002F79EC
		public new static PropertyMaker Maker(string propName)
		{
			return new UnicodeBidiMaker(propName);
		}

		// Token: 0x0600D8CB RID: 55499 RVA: 0x002F97F4 File Offset: 0x002F79F4
		protected UnicodeBidiMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8CC RID: 55500 RVA: 0x002F97FD File Offset: 0x002F79FD
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8CD RID: 55501 RVA: 0x002F9800 File Offset: 0x002F7A00
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "normal", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BBE RID: 15294
		private Property m_defaultProp;
	}
}
