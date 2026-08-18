using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015B5 RID: 5557
	internal class WidthMaker : LengthProperty.Maker
	{
		// Token: 0x0600D8F2 RID: 55538 RVA: 0x002F9B30 File Offset: 0x002F7D30
		public new static PropertyMaker Maker(string propName)
		{
			return new WidthMaker(propName);
		}

		// Token: 0x0600D8F3 RID: 55539 RVA: 0x002F9B38 File Offset: 0x002F7D38
		protected WidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8F4 RID: 55540 RVA: 0x002F9B41 File Offset: 0x002F7D41
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8F5 RID: 55541 RVA: 0x002F9B44 File Offset: 0x002F7D44
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D8F6 RID: 55542 RVA: 0x002F9B47 File Offset: 0x002F7D47
		public override IPercentBase GetPercentBase(FObj fo, PropertyList propertyList)
		{
			return new LengthBase(fo, propertyList, 3);
		}

		// Token: 0x0600D8F7 RID: 55543 RVA: 0x002F9B51 File Offset: 0x002F7D51
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD7 RID: 15319
		private Property m_defaultProp;
	}
}
