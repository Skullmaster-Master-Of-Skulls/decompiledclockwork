using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014BD RID: 5309
	internal class DirectionMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D572 RID: 54642 RVA: 0x002F380D File Offset: 0x002F1A0D
		public new static PropertyMaker Maker(string propName)
		{
			return new DirectionMaker(propName);
		}

		// Token: 0x0600D573 RID: 54643 RVA: 0x002F3815 File Offset: 0x002F1A15
		protected DirectionMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D574 RID: 54644 RVA: 0x002F381E File Offset: 0x002F1A1E
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D575 RID: 54645 RVA: 0x002F3821 File Offset: 0x002F1A21
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "ltr", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A5B RID: 14939
		private Property m_defaultProp;
	}
}
