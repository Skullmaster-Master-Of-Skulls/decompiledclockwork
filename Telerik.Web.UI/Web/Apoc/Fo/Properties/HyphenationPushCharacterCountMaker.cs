using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014FA RID: 5370
	internal class HyphenationPushCharacterCountMaker : NumberProperty.Maker
	{
		// Token: 0x0600D65F RID: 54879 RVA: 0x002F65F5 File Offset: 0x002F47F5
		public new static PropertyMaker Maker(string propName)
		{
			return new HyphenationPushCharacterCountMaker(propName);
		}

		// Token: 0x0600D660 RID: 54880 RVA: 0x002F65FD File Offset: 0x002F47FD
		protected HyphenationPushCharacterCountMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D661 RID: 54881 RVA: 0x002F6606 File Offset: 0x002F4806
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D662 RID: 54882 RVA: 0x002F6609 File Offset: 0x002F4809
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "2", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AC4 RID: 15044
		private Property m_defaultProp;
	}
}
