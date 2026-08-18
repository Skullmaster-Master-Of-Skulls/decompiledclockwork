using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014CD RID: 5325
	internal class FontSizeAdjustMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D5B2 RID: 54706 RVA: 0x002F3E08 File Offset: 0x002F2008
		public new static PropertyMaker Maker(string propName)
		{
			return new FontSizeAdjustMaker(propName);
		}

		// Token: 0x0600D5B3 RID: 54707 RVA: 0x002F3E10 File Offset: 0x002F2010
		protected FontSizeAdjustMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5B4 RID: 54708 RVA: 0x002F3E19 File Offset: 0x002F2019
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5B5 RID: 54709 RVA: 0x002F3E1C File Offset: 0x002F201C
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A72 RID: 14962
		private Property m_defaultProp;
	}
}
