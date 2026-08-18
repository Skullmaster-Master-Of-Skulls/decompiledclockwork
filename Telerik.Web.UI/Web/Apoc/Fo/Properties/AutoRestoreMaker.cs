using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200144D RID: 5197
	internal class AutoRestoreMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3C7 RID: 54215 RVA: 0x002EFB6C File Offset: 0x002EDD6C
		public new static PropertyMaker Maker(string propName)
		{
			return new AutoRestoreMaker(propName);
		}

		// Token: 0x0600D3C8 RID: 54216 RVA: 0x002EFB74 File Offset: 0x002EDD74
		protected AutoRestoreMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3C9 RID: 54217 RVA: 0x002EFB7D File Offset: 0x002EDD7D
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D3CA RID: 54218 RVA: 0x002EFB80 File Offset: 0x002EDD80
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "false", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003982 RID: 14722
		private Property m_defaultProp;
	}
}
