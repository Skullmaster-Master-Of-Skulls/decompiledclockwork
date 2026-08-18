using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014A8 RID: 5288
	internal class CaptionSideMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D51E RID: 54558 RVA: 0x002F3360 File Offset: 0x002F1560
		public new static PropertyMaker Maker(string propName)
		{
			return new CaptionSideMaker(propName);
		}

		// Token: 0x0600D51F RID: 54559 RVA: 0x002F3368 File Offset: 0x002F1568
		protected CaptionSideMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D520 RID: 54560 RVA: 0x002F3371 File Offset: 0x002F1571
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D521 RID: 54561 RVA: 0x002F3374 File Offset: 0x002F1574
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "before", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039EE RID: 14830
		private Property m_defaultProp;
	}
}
