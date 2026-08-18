using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001566 RID: 5478
	internal class RenderingIntentMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7E8 RID: 55272 RVA: 0x002F8566 File Offset: 0x002F6766
		public new static PropertyMaker Maker(string propName)
		{
			return new RenderingIntentMaker(propName);
		}

		// Token: 0x0600D7E9 RID: 55273 RVA: 0x002F856E File Offset: 0x002F676E
		protected RenderingIntentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7EA RID: 55274 RVA: 0x002F8577 File Offset: 0x002F6777
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7EB RID: 55275 RVA: 0x002F857A File Offset: 0x002F677A
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B47 RID: 15175
		private Property m_defaultProp;
	}
}
