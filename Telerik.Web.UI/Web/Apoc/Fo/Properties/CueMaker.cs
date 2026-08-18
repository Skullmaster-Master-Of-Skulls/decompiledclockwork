using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014BB RID: 5307
	internal class CueMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D56A RID: 54634 RVA: 0x002F3795 File Offset: 0x002F1995
		public new static PropertyMaker Maker(string propName)
		{
			return new CueMaker(propName);
		}

		// Token: 0x0600D56B RID: 54635 RVA: 0x002F379D File Offset: 0x002F199D
		protected CueMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D56C RID: 54636 RVA: 0x002F37A6 File Offset: 0x002F19A6
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D56D RID: 54637 RVA: 0x002F37A9 File Offset: 0x002F19A9
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A59 RID: 14937
		private Property m_defaultProp;
	}
}
