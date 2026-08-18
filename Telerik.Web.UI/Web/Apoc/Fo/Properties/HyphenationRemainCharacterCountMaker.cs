using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014FB RID: 5371
	internal class HyphenationRemainCharacterCountMaker : NumberProperty.Maker
	{
		// Token: 0x0600D663 RID: 54883 RVA: 0x002F6631 File Offset: 0x002F4831
		public new static PropertyMaker Maker(string propName)
		{
			return new HyphenationRemainCharacterCountMaker(propName);
		}

		// Token: 0x0600D664 RID: 54884 RVA: 0x002F6639 File Offset: 0x002F4839
		protected HyphenationRemainCharacterCountMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D665 RID: 54885 RVA: 0x002F6642 File Offset: 0x002F4842
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D666 RID: 54886 RVA: 0x002F6645 File Offset: 0x002F4845
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "2", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AC5 RID: 15045
		private Property m_defaultProp;
	}
}
