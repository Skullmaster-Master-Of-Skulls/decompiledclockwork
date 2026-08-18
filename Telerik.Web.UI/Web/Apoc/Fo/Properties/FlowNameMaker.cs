using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C9 RID: 5321
	internal class FlowNameMaker : StringProperty.Maker
	{
		// Token: 0x0600D5A2 RID: 54690 RVA: 0x002F3CBE File Offset: 0x002F1EBE
		public new static PropertyMaker Maker(string propName)
		{
			return new FlowNameMaker(propName);
		}

		// Token: 0x0600D5A3 RID: 54691 RVA: 0x002F3CC6 File Offset: 0x002F1EC6
		protected FlowNameMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5A4 RID: 54692 RVA: 0x002F3CCF File Offset: 0x002F1ECF
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D5A5 RID: 54693 RVA: 0x002F3CD2 File Offset: 0x002F1ED2
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A6E RID: 14958
		private Property m_defaultProp;
	}
}
