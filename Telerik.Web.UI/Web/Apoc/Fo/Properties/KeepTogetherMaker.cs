using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001508 RID: 5384
	internal class KeepTogetherMaker : GenericKeep
	{
		// Token: 0x0600D695 RID: 54933 RVA: 0x002F6B4B File Offset: 0x002F4D4B
		public new static PropertyMaker Maker(string propName)
		{
			return new KeepTogetherMaker(propName);
		}

		// Token: 0x0600D696 RID: 54934 RVA: 0x002F6B53 File Offset: 0x002F4D53
		protected KeepTogetherMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D697 RID: 54935 RVA: 0x002F6B5C File Offset: 0x002F4D5C
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D698 RID: 54936 RVA: 0x002F6B5F File Offset: 0x002F4D5F
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003ACF RID: 15055
		private Property m_defaultProp;
	}
}
