using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014A9 RID: 5289
	internal class CaseNameMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D522 RID: 54562 RVA: 0x002F339C File Offset: 0x002F159C
		public new static PropertyMaker Maker(string propName)
		{
			return new CaseNameMaker(propName);
		}

		// Token: 0x0600D523 RID: 54563 RVA: 0x002F33A4 File Offset: 0x002F15A4
		protected CaseNameMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D524 RID: 54564 RVA: 0x002F33AD File Offset: 0x002F15AD
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D525 RID: 54565 RVA: 0x002F33B0 File Offset: 0x002F15B0
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x040039EF RID: 14831
		private Property m_defaultProp;
	}
}
