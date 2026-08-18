using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014A3 RID: 5283
	internal class BottomMaker : LengthProperty.Maker
	{
		// Token: 0x0600D510 RID: 54544 RVA: 0x002F321D File Offset: 0x002F141D
		public new static PropertyMaker Maker(string propName)
		{
			return new BottomMaker(propName);
		}

		// Token: 0x0600D511 RID: 54545 RVA: 0x002F3225 File Offset: 0x002F1425
		protected BottomMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D512 RID: 54546 RVA: 0x002F322E File Offset: 0x002F142E
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D513 RID: 54547 RVA: 0x002F3231 File Offset: 0x002F1431
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D514 RID: 54548 RVA: 0x002F3234 File Offset: 0x002F1434
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039E2 RID: 14818
		private Property m_defaultProp;
	}
}
