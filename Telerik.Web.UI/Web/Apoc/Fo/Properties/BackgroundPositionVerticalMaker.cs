using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001458 RID: 5208
	internal class BackgroundPositionVerticalMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3F1 RID: 54257 RVA: 0x002F09D4 File Offset: 0x002EEBD4
		public new static PropertyMaker Maker(string propName)
		{
			return new BackgroundPositionVerticalMaker(propName);
		}

		// Token: 0x0600D3F2 RID: 54258 RVA: 0x002F09DC File Offset: 0x002EEBDC
		protected BackgroundPositionVerticalMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3F3 RID: 54259 RVA: 0x002F09E5 File Offset: 0x002EEBE5
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3F4 RID: 54260 RVA: 0x002F09E8 File Offset: 0x002EEBE8
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0%", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0400398C RID: 14732
		private Property m_defaultProp;
	}
}
