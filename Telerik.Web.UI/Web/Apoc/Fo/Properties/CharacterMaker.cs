using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014AB RID: 5291
	internal class CharacterMaker : CharacterProperty.Maker
	{
		// Token: 0x0600D52A RID: 54570 RVA: 0x002F3414 File Offset: 0x002F1614
		public new static PropertyMaker Maker(string propName)
		{
			return new CharacterMaker(propName);
		}

		// Token: 0x0600D52B RID: 54571 RVA: 0x002F341C File Offset: 0x002F161C
		protected CharacterMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D52C RID: 54572 RVA: 0x002F3425 File Offset: 0x002F1625
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D52D RID: 54573 RVA: 0x002F3428 File Offset: 0x002F1628
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039F1 RID: 14833
		private Property m_defaultProp;
	}
}
