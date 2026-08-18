using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015B2 RID: 5554
	internal class WhiteSpaceMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8E6 RID: 55526 RVA: 0x002F9A7C File Offset: 0x002F7C7C
		public new static PropertyMaker Maker(string propName)
		{
			return new WhiteSpaceMaker(propName);
		}

		// Token: 0x0600D8E7 RID: 55527 RVA: 0x002F9A84 File Offset: 0x002F7C84
		protected WhiteSpaceMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8E8 RID: 55528 RVA: 0x002F9A8D File Offset: 0x002F7C8D
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8E9 RID: 55529 RVA: 0x002F9A90 File Offset: 0x002F7C90
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "normal", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD4 RID: 15316
		private Property m_defaultProp;
	}
}
