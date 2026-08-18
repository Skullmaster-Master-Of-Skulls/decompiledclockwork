using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014FC RID: 5372
	internal class IdMaker : StringProperty.Maker
	{
		// Token: 0x0600D667 RID: 54887 RVA: 0x002F666D File Offset: 0x002F486D
		public new static PropertyMaker Maker(string propName)
		{
			return new IdMaker(propName);
		}

		// Token: 0x0600D668 RID: 54888 RVA: 0x002F6675 File Offset: 0x002F4875
		protected IdMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D669 RID: 54889 RVA: 0x002F667E File Offset: 0x002F487E
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D66A RID: 54890 RVA: 0x002F6681 File Offset: 0x002F4881
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AC6 RID: 15046
		private Property m_defaultProp;
	}
}
