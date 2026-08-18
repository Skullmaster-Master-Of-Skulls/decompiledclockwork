using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F8 RID: 5368
	internal class HyphenationKeepMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D657 RID: 54871 RVA: 0x002F657D File Offset: 0x002F477D
		public new static PropertyMaker Maker(string propName)
		{
			return new HyphenationKeepMaker(propName);
		}

		// Token: 0x0600D658 RID: 54872 RVA: 0x002F6585 File Offset: 0x002F4785
		protected HyphenationKeepMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D659 RID: 54873 RVA: 0x002F658E File Offset: 0x002F478E
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D65A RID: 54874 RVA: 0x002F6591 File Offset: 0x002F4791
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AC2 RID: 15042
		private Property m_defaultProp;
	}
}
