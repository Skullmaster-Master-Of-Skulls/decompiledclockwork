using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F2 RID: 5362
	internal class GroupingSeparatorMaker : CharacterProperty.Maker
	{
		// Token: 0x0600D63F RID: 54847 RVA: 0x002F63FD File Offset: 0x002F45FD
		public new static PropertyMaker Maker(string propName)
		{
			return new GroupingSeparatorMaker(propName);
		}

		// Token: 0x0600D640 RID: 54848 RVA: 0x002F6405 File Offset: 0x002F4605
		protected GroupingSeparatorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D641 RID: 54849 RVA: 0x002F640E File Offset: 0x002F460E
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D642 RID: 54850 RVA: 0x002F6411 File Offset: 0x002F4611
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AB9 RID: 15033
		private Property m_defaultProp;
	}
}
