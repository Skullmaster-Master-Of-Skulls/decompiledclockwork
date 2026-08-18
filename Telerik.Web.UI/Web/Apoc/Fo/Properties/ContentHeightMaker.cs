using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014B5 RID: 5301
	internal class ContentHeightMaker : LengthProperty.Maker
	{
		// Token: 0x0600D550 RID: 54608 RVA: 0x002F3627 File Offset: 0x002F1827
		public new static PropertyMaker Maker(string propName)
		{
			return new ContentHeightMaker(propName);
		}

		// Token: 0x0600D551 RID: 54609 RVA: 0x002F362F File Offset: 0x002F182F
		protected ContentHeightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D552 RID: 54610 RVA: 0x002F3638 File Offset: 0x002F1838
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D553 RID: 54611 RVA: 0x002F363B File Offset: 0x002F183B
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D554 RID: 54612 RVA: 0x002F363E File Offset: 0x002F183E
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A53 RID: 14931
		private Property m_defaultProp;
	}
}
