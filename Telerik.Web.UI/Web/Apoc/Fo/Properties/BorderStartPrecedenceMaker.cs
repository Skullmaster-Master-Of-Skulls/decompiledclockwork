using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001496 RID: 5270
	internal class BorderStartPrecedenceMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D4E8 RID: 54504 RVA: 0x002F2CA9 File Offset: 0x002F0EA9
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderStartPrecedenceMaker(propName);
		}

		// Token: 0x0600D4E9 RID: 54505 RVA: 0x002F2CB1 File Offset: 0x002F0EB1
		protected BorderStartPrecedenceMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4EA RID: 54506 RVA: 0x002F2CBA File Offset: 0x002F0EBA
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D4EB RID: 54507 RVA: 0x002F2CBD File Offset: 0x002F0EBD
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039E0 RID: 14816
		private Property m_defaultProp;
	}
}
