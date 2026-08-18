using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014FE RID: 5374
	internal class InitialPageNumberMaker : StringProperty.Maker
	{
		// Token: 0x0600D66F RID: 54895 RVA: 0x002F66E5 File Offset: 0x002F48E5
		public new static PropertyMaker Maker(string propName)
		{
			return new InitialPageNumberMaker(propName);
		}

		// Token: 0x0600D670 RID: 54896 RVA: 0x002F66ED File Offset: 0x002F48ED
		protected InitialPageNumberMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D671 RID: 54897 RVA: 0x002F66F6 File Offset: 0x002F48F6
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D672 RID: 54898 RVA: 0x002F66F9 File Offset: 0x002F48F9
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AC8 RID: 15048
		private Property m_defaultProp;
	}
}
