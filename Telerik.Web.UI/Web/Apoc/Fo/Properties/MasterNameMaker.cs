using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200152C RID: 5420
	internal class MasterNameMaker : StringProperty.Maker
	{
		// Token: 0x0600D719 RID: 55065 RVA: 0x002F753B File Offset: 0x002F573B
		public new static PropertyMaker Maker(string propName)
		{
			return new MasterNameMaker(propName);
		}

		// Token: 0x0600D71A RID: 55066 RVA: 0x002F7543 File Offset: 0x002F5743
		protected MasterNameMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D71B RID: 55067 RVA: 0x002F754C File Offset: 0x002F574C
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D71C RID: 55068 RVA: 0x002F754F File Offset: 0x002F574F
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AFC RID: 15100
		private Property m_defaultProp;
	}
}
