using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200144B RID: 5195
	internal class AlignmentAdjustMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3BF RID: 54207 RVA: 0x002EFAF4 File Offset: 0x002EDCF4
		public new static PropertyMaker Maker(string propName)
		{
			return new AlignmentAdjustMaker(propName);
		}

		// Token: 0x0600D3C0 RID: 54208 RVA: 0x002EFAFC File Offset: 0x002EDCFC
		protected AlignmentAdjustMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3C1 RID: 54209 RVA: 0x002EFB05 File Offset: 0x002EDD05
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3C2 RID: 54210 RVA: 0x002EFB08 File Offset: 0x002EDD08
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003980 RID: 14720
		private Property m_defaultProp;
	}
}
