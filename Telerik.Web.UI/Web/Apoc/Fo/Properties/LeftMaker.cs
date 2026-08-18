using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200151E RID: 5406
	internal class LeftMaker : LengthProperty.Maker
	{
		// Token: 0x0600D6DC RID: 55004 RVA: 0x002F7124 File Offset: 0x002F5324
		public new static PropertyMaker Maker(string propName)
		{
			return new LeftMaker(propName);
		}

		// Token: 0x0600D6DD RID: 55005 RVA: 0x002F712C File Offset: 0x002F532C
		protected LeftMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D6DE RID: 55006 RVA: 0x002F7135 File Offset: 0x002F5335
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D6DF RID: 55007 RVA: 0x002F7138 File Offset: 0x002F5338
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D6E0 RID: 55008 RVA: 0x002F713B File Offset: 0x002F533B
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AE9 RID: 15081
		private Property m_defaultProp;
	}
}
