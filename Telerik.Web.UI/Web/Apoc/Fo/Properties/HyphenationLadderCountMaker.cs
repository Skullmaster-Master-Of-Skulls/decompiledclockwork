using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F9 RID: 5369
	internal class HyphenationLadderCountMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D65B RID: 54875 RVA: 0x002F65B9 File Offset: 0x002F47B9
		public new static PropertyMaker Maker(string propName)
		{
			return new HyphenationLadderCountMaker(propName);
		}

		// Token: 0x0600D65C RID: 54876 RVA: 0x002F65C1 File Offset: 0x002F47C1
		protected HyphenationLadderCountMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D65D RID: 54877 RVA: 0x002F65CA File Offset: 0x002F47CA
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D65E RID: 54878 RVA: 0x002F65CD File Offset: 0x002F47CD
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "no-limit", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AC3 RID: 15043
		private Property m_defaultProp;
	}
}
