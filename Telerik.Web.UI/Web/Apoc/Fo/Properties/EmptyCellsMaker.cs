using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C2 RID: 5314
	internal class EmptyCellsMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D585 RID: 54661 RVA: 0x002F3998 File Offset: 0x002F1B98
		public new static PropertyMaker Maker(string propName)
		{
			return new EmptyCellsMaker(propName);
		}

		// Token: 0x0600D586 RID: 54662 RVA: 0x002F39A0 File Offset: 0x002F1BA0
		protected EmptyCellsMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D587 RID: 54663 RVA: 0x002F39A9 File Offset: 0x002F1BA9
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D588 RID: 54664 RVA: 0x002F39AC File Offset: 0x002F1BAC
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "show", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A67 RID: 14951
		private Property m_defaultProp;
	}
}
