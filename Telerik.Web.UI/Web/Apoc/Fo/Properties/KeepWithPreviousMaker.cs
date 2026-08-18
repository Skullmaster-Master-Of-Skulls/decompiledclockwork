using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001512 RID: 5394
	internal class KeepWithPreviousMaker : GenericKeep
	{
		// Token: 0x0600D6A5 RID: 54949 RVA: 0x002F6C03 File Offset: 0x002F4E03
		public new static PropertyMaker Maker(string propName)
		{
			return new KeepWithPreviousMaker(propName);
		}

		// Token: 0x0600D6A6 RID: 54950 RVA: 0x002F6C0B File Offset: 0x002F4E0B
		protected KeepWithPreviousMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6A7 RID: 54951 RVA: 0x002F6C14 File Offset: 0x002F4E14
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D6A8 RID: 54952 RVA: 0x002F6C17 File Offset: 0x002F4E17
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AD1 RID: 15057
		private Property m_defaultProp;
	}
}
