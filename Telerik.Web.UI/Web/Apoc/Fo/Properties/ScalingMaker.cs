using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001573 RID: 5491
	internal class ScalingMaker : EnumProperty.Maker
	{
		// Token: 0x0600D817 RID: 55319 RVA: 0x002F89AD File Offset: 0x002F6BAD
		public new static PropertyMaker Maker(string propName)
		{
			return new ScalingMaker(propName);
		}

		// Token: 0x0600D818 RID: 55320 RVA: 0x002F89B5 File Offset: 0x002F6BB5
		protected ScalingMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D819 RID: 55321 RVA: 0x002F89BE File Offset: 0x002F6BBE
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D81A RID: 55322 RVA: 0x002F89C1 File Offset: 0x002F6BC1
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("uniform"))
			{
				return ScalingMaker.s_propUNIFORM;
			}
			if (value.Equals("non-uniform"))
			{
				return ScalingMaker.s_propNON_UNIFORM;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x0600D81B RID: 55323 RVA: 0x002F89F0 File Offset: 0x002F6BF0
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "uniform", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B6E RID: 15214
		protected static readonly EnumProperty s_propUNIFORM = new EnumProperty(83);

		// Token: 0x04003B6F RID: 15215
		protected static readonly EnumProperty s_propNON_UNIFORM = new EnumProperty(50);

		// Token: 0x04003B70 RID: 15216
		private Property m_defaultProp;
	}
}
