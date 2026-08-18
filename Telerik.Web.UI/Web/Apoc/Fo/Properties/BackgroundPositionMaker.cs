using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001457 RID: 5207
	internal class BackgroundPositionMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D3ED RID: 54253 RVA: 0x002F0998 File Offset: 0x002EEB98
		public new static PropertyMaker Maker(string propName)
		{
			return new BackgroundPositionMaker(propName);
		}

		// Token: 0x0600D3EE RID: 54254 RVA: 0x002F09A0 File Offset: 0x002EEBA0
		protected BackgroundPositionMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D3EF RID: 54255 RVA: 0x002F09A9 File Offset: 0x002EEBA9
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D3F0 RID: 54256 RVA: 0x002F09AC File Offset: 0x002EEBAC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0%", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0400398B RID: 14731
		private Property m_defaultProp;
	}
}
