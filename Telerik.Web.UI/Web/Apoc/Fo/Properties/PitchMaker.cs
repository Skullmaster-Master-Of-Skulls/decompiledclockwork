using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001557 RID: 5463
	internal class PitchMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7AF RID: 55215 RVA: 0x002F815C File Offset: 0x002F635C
		public new static PropertyMaker Maker(string propName)
		{
			return new PitchMaker(propName);
		}

		// Token: 0x0600D7B0 RID: 55216 RVA: 0x002F8164 File Offset: 0x002F6364
		protected PitchMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7B1 RID: 55217 RVA: 0x002F816D File Offset: 0x002F636D
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D7B2 RID: 55218 RVA: 0x002F8170 File Offset: 0x002F6370
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "medium", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B2B RID: 15147
		private Property m_defaultProp;
	}
}
