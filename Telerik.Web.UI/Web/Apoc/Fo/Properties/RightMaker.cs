using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200156D RID: 5485
	internal class RightMaker : LengthProperty.Maker
	{
		// Token: 0x0600D802 RID: 55298 RVA: 0x002F87AA File Offset: 0x002F69AA
		public new static PropertyMaker Maker(string propName)
		{
			return new RightMaker(propName);
		}

		// Token: 0x0600D803 RID: 55299 RVA: 0x002F87B2 File Offset: 0x002F69B2
		protected RightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D804 RID: 55300 RVA: 0x002F87BB File Offset: 0x002F69BB
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D805 RID: 55301 RVA: 0x002F87BE File Offset: 0x002F69BE
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D806 RID: 55302 RVA: 0x002F87C1 File Offset: 0x002F69C1
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B5A RID: 15194
		private Property m_defaultProp;
	}
}
