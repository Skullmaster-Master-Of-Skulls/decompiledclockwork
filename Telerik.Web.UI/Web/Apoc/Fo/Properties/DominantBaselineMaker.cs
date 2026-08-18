using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C0 RID: 5312
	internal class DominantBaselineMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D57D RID: 54653 RVA: 0x002F3920 File Offset: 0x002F1B20
		public new static PropertyMaker Maker(string propName)
		{
			return new DominantBaselineMaker(propName);
		}

		// Token: 0x0600D57E RID: 54654 RVA: 0x002F3928 File Offset: 0x002F1B28
		protected DominantBaselineMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D57F RID: 54655 RVA: 0x002F3931 File Offset: 0x002F1B31
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D580 RID: 54656 RVA: 0x002F3934 File Offset: 0x002F1B34
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A65 RID: 14949
		private Property m_defaultProp;
	}
}
