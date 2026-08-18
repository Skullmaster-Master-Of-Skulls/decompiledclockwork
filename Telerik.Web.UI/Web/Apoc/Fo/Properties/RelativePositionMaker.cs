using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001565 RID: 5477
	internal class RelativePositionMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D7E4 RID: 55268 RVA: 0x002F852A File Offset: 0x002F672A
		public new static PropertyMaker Maker(string propName)
		{
			return new RelativePositionMaker(propName);
		}

		// Token: 0x0600D7E5 RID: 55269 RVA: 0x002F8532 File Offset: 0x002F6732
		protected RelativePositionMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7E6 RID: 55270 RVA: 0x002F853B File Offset: 0x002F673B
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7E7 RID: 55271 RVA: 0x002F853E File Offset: 0x002F673E
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "static", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B46 RID: 15174
		private Property m_defaultProp;
	}
}
