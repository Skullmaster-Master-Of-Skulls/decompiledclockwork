using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200156E RID: 5486
	internal class RoleMaker : StringProperty.Maker
	{
		// Token: 0x0600D807 RID: 55303 RVA: 0x002F87E9 File Offset: 0x002F69E9
		public new static PropertyMaker Maker(string propName)
		{
			return new RoleMaker(propName);
		}

		// Token: 0x0600D808 RID: 55304 RVA: 0x002F87F1 File Offset: 0x002F69F1
		protected RoleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D809 RID: 55305 RVA: 0x002F87FA File Offset: 0x002F69FA
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D80A RID: 55306 RVA: 0x002F87FD File Offset: 0x002F69FD
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B5B RID: 15195
		private Property m_defaultProp;
	}
}
