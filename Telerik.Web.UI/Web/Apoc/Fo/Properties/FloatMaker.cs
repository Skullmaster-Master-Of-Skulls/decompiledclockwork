using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C8 RID: 5320
	internal class FloatMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D59E RID: 54686 RVA: 0x002F3C82 File Offset: 0x002F1E82
		public new static PropertyMaker Maker(string propName)
		{
			return new FloatMaker(propName);
		}

		// Token: 0x0600D59F RID: 54687 RVA: 0x002F3C8A File Offset: 0x002F1E8A
		protected FloatMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D5A0 RID: 54688 RVA: 0x002F3C93 File Offset: 0x002F1E93
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D5A1 RID: 54689 RVA: 0x002F3C96 File Offset: 0x002F1E96
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A6D RID: 14957
		private Property m_defaultProp;
	}
}
