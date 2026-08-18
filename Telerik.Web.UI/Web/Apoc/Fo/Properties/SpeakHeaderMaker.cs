using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001588 RID: 5512
	internal class SpeakHeaderMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D848 RID: 55368 RVA: 0x002F8C86 File Offset: 0x002F6E86
		public new static PropertyMaker Maker(string propName)
		{
			return new SpeakHeaderMaker(propName);
		}

		// Token: 0x0600D849 RID: 55369 RVA: 0x002F8C8E File Offset: 0x002F6E8E
		protected SpeakHeaderMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D84A RID: 55370 RVA: 0x002F8C97 File Offset: 0x002F6E97
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D84B RID: 55371 RVA: 0x002F8C9A File Offset: 0x002F6E9A
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "once", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B7C RID: 15228
		private Property m_defaultProp;
	}
}
