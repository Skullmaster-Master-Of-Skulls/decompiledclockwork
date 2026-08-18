using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200152B RID: 5419
	internal class MarkerClassNameMaker : StringProperty.Maker
	{
		// Token: 0x0600D715 RID: 55061 RVA: 0x002F74FF File Offset: 0x002F56FF
		public new static PropertyMaker Maker(string propName)
		{
			return new MarkerClassNameMaker(propName);
		}

		// Token: 0x0600D716 RID: 55062 RVA: 0x002F7507 File Offset: 0x002F5707
		protected MarkerClassNameMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D717 RID: 55063 RVA: 0x002F7510 File Offset: 0x002F5710
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D718 RID: 55064 RVA: 0x002F7513 File Offset: 0x002F5713
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AFB RID: 15099
		private Property m_defaultProp;
	}
}
