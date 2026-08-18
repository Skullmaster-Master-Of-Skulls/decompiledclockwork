using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001589 RID: 5513
	internal class SpeakMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D84C RID: 55372 RVA: 0x002F8CC2 File Offset: 0x002F6EC2
		public new static PropertyMaker Maker(string propName)
		{
			return new SpeakMaker(propName);
		}

		// Token: 0x0600D84D RID: 55373 RVA: 0x002F8CCA File Offset: 0x002F6ECA
		protected SpeakMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D84E RID: 55374 RVA: 0x002F8CD3 File Offset: 0x002F6ED3
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D84F RID: 55375 RVA: 0x002F8CD6 File Offset: 0x002F6ED6
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "normal", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B7D RID: 15229
		private Property m_defaultProp;
	}
}
