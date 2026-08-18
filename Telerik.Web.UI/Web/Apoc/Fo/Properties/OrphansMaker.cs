using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001539 RID: 5433
	internal class OrphansMaker : NumberProperty.Maker
	{
		// Token: 0x0600D750 RID: 55120 RVA: 0x002F78DA File Offset: 0x002F5ADA
		public new static PropertyMaker Maker(string propName)
		{
			return new OrphansMaker(propName);
		}

		// Token: 0x0600D751 RID: 55121 RVA: 0x002F78E2 File Offset: 0x002F5AE2
		protected OrphansMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D752 RID: 55122 RVA: 0x002F78EB File Offset: 0x002F5AEB
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D753 RID: 55123 RVA: 0x002F78EE File Offset: 0x002F5AEE
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "2", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B10 RID: 15120
		private Property m_defaultProp;
	}
}
