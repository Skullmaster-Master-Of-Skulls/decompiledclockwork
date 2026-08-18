using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001555 RID: 5461
	internal class PauseBeforeMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7A7 RID: 55207 RVA: 0x002F80E4 File Offset: 0x002F62E4
		public new static PropertyMaker Maker(string propName)
		{
			return new PauseBeforeMaker(propName);
		}

		// Token: 0x0600D7A8 RID: 55208 RVA: 0x002F80EC File Offset: 0x002F62EC
		protected PauseBeforeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7A9 RID: 55209 RVA: 0x002F80F5 File Offset: 0x002F62F5
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7AA RID: 55210 RVA: 0x002F80F8 File Offset: 0x002F62F8
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B29 RID: 15145
		private Property m_defaultProp;
	}
}
