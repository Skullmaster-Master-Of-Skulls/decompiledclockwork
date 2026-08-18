using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200154F RID: 5455
	internal class PageBreakInsideMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D78E RID: 55182 RVA: 0x002F7F19 File Offset: 0x002F6119
		public new static PropertyMaker Maker(string propName)
		{
			return new PageBreakInsideMaker(propName);
		}

		// Token: 0x0600D78F RID: 55183 RVA: 0x002F7F21 File Offset: 0x002F6121
		protected PageBreakInsideMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D790 RID: 55184 RVA: 0x002F7F2A File Offset: 0x002F612A
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D791 RID: 55185 RVA: 0x002F7F2D File Offset: 0x002F612D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B1C RID: 15132
		private Property m_defaultProp;
	}
}
