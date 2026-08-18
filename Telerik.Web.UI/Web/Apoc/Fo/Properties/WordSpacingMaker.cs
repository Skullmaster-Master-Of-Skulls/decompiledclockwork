using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015B6 RID: 5558
	internal class WordSpacingMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D8F8 RID: 55544 RVA: 0x002F9B79 File Offset: 0x002F7D79
		public new static PropertyMaker Maker(string propName)
		{
			return new WordSpacingMaker(propName);
		}

		// Token: 0x0600D8F9 RID: 55545 RVA: 0x002F9B81 File Offset: 0x002F7D81
		protected WordSpacingMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8FA RID: 55546 RVA: 0x002F9B8A File Offset: 0x002F7D8A
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D8FB RID: 55547 RVA: 0x002F9B8D File Offset: 0x002F7D8D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "normal", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BD8 RID: 15320
		private Property m_defaultProp;
	}
}
