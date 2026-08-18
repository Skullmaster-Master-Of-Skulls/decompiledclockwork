using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001482 RID: 5250
	internal class BorderEndPrecedenceMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D499 RID: 54425 RVA: 0x002F20F9 File Offset: 0x002F02F9
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderEndPrecedenceMaker(propName);
		}

		// Token: 0x0600D49A RID: 54426 RVA: 0x002F2101 File Offset: 0x002F0301
		protected BorderEndPrecedenceMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D49B RID: 54427 RVA: 0x002F210A File Offset: 0x002F030A
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D49C RID: 54428 RVA: 0x002F210D File Offset: 0x002F030D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039D7 RID: 14807
		private Property m_defaultProp;
	}
}
