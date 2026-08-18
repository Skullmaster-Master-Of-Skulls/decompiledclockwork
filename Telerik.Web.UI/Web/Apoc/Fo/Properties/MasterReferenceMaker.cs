using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200152D RID: 5421
	internal class MasterReferenceMaker : StringProperty.Maker
	{
		// Token: 0x0600D71D RID: 55069 RVA: 0x002F7577 File Offset: 0x002F5777
		public new static PropertyMaker Maker(string propName)
		{
			return new MasterReferenceMaker(propName);
		}

		// Token: 0x0600D71E RID: 55070 RVA: 0x002F757F File Offset: 0x002F577F
		protected MasterReferenceMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D71F RID: 55071 RVA: 0x002F7588 File Offset: 0x002F5788
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D720 RID: 55072 RVA: 0x002F758B File Offset: 0x002F578B
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AFD RID: 15101
		private Property m_defaultProp;
	}
}
