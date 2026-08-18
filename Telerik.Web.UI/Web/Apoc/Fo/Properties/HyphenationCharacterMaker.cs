using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F7 RID: 5367
	internal class HyphenationCharacterMaker : CharacterProperty.Maker
	{
		// Token: 0x0600D653 RID: 54867 RVA: 0x002F6541 File Offset: 0x002F4741
		public new static PropertyMaker Maker(string propName)
		{
			return new HyphenationCharacterMaker(propName);
		}

		// Token: 0x0600D654 RID: 54868 RVA: 0x002F6549 File Offset: 0x002F4749
		protected HyphenationCharacterMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D655 RID: 54869 RVA: 0x002F6552 File Offset: 0x002F4752
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D656 RID: 54870 RVA: 0x002F6555 File Offset: 0x002F4755
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "-", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AC1 RID: 15041
		private Property m_defaultProp;
	}
}
