using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C1 RID: 5313
	internal class ElevationMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D581 RID: 54657 RVA: 0x002F395C File Offset: 0x002F1B5C
		public new static PropertyMaker Maker(string propName)
		{
			return new ElevationMaker(propName);
		}

		// Token: 0x0600D582 RID: 54658 RVA: 0x002F3964 File Offset: 0x002F1B64
		protected ElevationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D583 RID: 54659 RVA: 0x002F396D File Offset: 0x002F1B6D
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D584 RID: 54660 RVA: 0x002F3970 File Offset: 0x002F1B70
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "level", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A66 RID: 14950
		private Property m_defaultProp;
	}
}
