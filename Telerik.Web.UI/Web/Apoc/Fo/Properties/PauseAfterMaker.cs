using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001554 RID: 5460
	internal class PauseAfterMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7A3 RID: 55203 RVA: 0x002F80A8 File Offset: 0x002F62A8
		public new static PropertyMaker Maker(string propName)
		{
			return new PauseAfterMaker(propName);
		}

		// Token: 0x0600D7A4 RID: 55204 RVA: 0x002F80B0 File Offset: 0x002F62B0
		protected PauseAfterMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7A5 RID: 55205 RVA: 0x002F80B9 File Offset: 0x002F62B9
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7A6 RID: 55206 RVA: 0x002F80BC File Offset: 0x002F62BC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B28 RID: 15144
		private Property m_defaultProp;
	}
}
