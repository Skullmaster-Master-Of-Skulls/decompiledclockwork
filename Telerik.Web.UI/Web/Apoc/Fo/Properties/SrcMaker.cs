using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200158D RID: 5517
	internal class SrcMaker : StringProperty.Maker
	{
		// Token: 0x0600D85C RID: 55388 RVA: 0x002F8DB2 File Offset: 0x002F6FB2
		public new static PropertyMaker Maker(string propName)
		{
			return new SrcMaker(propName);
		}

		// Token: 0x0600D85D RID: 55389 RVA: 0x002F8DBA File Offset: 0x002F6FBA
		protected SrcMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D85E RID: 55390 RVA: 0x002F8DC3 File Offset: 0x002F6FC3
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D85F RID: 55391 RVA: 0x002F8DC6 File Offset: 0x002F6FC6
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B81 RID: 15233
		private Property m_defaultProp;
	}
}
