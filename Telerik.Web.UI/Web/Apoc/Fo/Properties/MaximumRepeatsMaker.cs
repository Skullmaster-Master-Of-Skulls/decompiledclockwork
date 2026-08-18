using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200152F RID: 5423
	internal class MaximumRepeatsMaker : StringProperty.Maker
	{
		// Token: 0x0600D727 RID: 55079 RVA: 0x002F7619 File Offset: 0x002F5819
		public new static PropertyMaker Maker(string propName)
		{
			return new MaximumRepeatsMaker(propName);
		}

		// Token: 0x0600D728 RID: 55080 RVA: 0x002F7621 File Offset: 0x002F5821
		protected MaximumRepeatsMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D729 RID: 55081 RVA: 0x002F762A File Offset: 0x002F582A
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D72A RID: 55082 RVA: 0x002F762D File Offset: 0x002F582D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "no-limit", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B00 RID: 15104
		private Property m_defaultProp;
	}
}
