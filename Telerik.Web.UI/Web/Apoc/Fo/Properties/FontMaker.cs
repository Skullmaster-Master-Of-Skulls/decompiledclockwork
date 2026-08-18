using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014CB RID: 5323
	internal class FontMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D5AA RID: 54698 RVA: 0x002F3D90 File Offset: 0x002F1F90
		public new static PropertyMaker Maker(string propName)
		{
			return new FontMaker(propName);
		}

		// Token: 0x0600D5AB RID: 54699 RVA: 0x002F3D98 File Offset: 0x002F1F98
		protected FontMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5AC RID: 54700 RVA: 0x002F3DA1 File Offset: 0x002F1FA1
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D5AD RID: 54701 RVA: 0x002F3DA4 File Offset: 0x002F1FA4
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A70 RID: 14960
		private Property m_defaultProp;
	}
}
