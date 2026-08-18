using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001590 RID: 5520
	internal class StartsRowMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D86A RID: 55402 RVA: 0x002F8FD9 File Offset: 0x002F71D9
		public new static PropertyMaker Maker(string propName)
		{
			return new StartsRowMaker(propName);
		}

		// Token: 0x0600D86B RID: 55403 RVA: 0x002F8FE1 File Offset: 0x002F71E1
		protected StartsRowMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D86C RID: 55404 RVA: 0x002F8FEA File Offset: 0x002F71EA
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D86D RID: 55405 RVA: 0x002F8FED File Offset: 0x002F71ED
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "false", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B84 RID: 15236
		private Property m_defaultProp;
	}
}
