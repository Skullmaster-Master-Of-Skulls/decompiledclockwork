using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015BA RID: 5562
	internal class WritingModeMaker : EnumProperty.Maker
	{
		// Token: 0x0600D904 RID: 55556 RVA: 0x002F9C4A File Offset: 0x002F7E4A
		public new static PropertyMaker Maker(string propName)
		{
			return new WritingModeMaker(propName);
		}

		// Token: 0x0600D905 RID: 55557 RVA: 0x002F9C52 File Offset: 0x002F7E52
		protected WritingModeMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D906 RID: 55558 RVA: 0x002F9C5B File Offset: 0x002F7E5B
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D907 RID: 55559 RVA: 0x002F9C5E File Offset: 0x002F7E5E
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "lr-tb", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x0600D908 RID: 55560 RVA: 0x002F9C88 File Offset: 0x002F7E88
		public override Property CheckEnumValues(string value)
		{
			if (value.Equals("lr-tb"))
			{
				return WritingModeMaker.s_propLR_TB;
			}
			if (value.Equals("rl-tb"))
			{
				return WritingModeMaker.s_propRL_TB;
			}
			if (value.Equals("tb-rl"))
			{
				return WritingModeMaker.s_propTB_RL;
			}
			return base.CheckEnumValues(value);
		}

		// Token: 0x04003BE1 RID: 15329
		protected static readonly EnumProperty s_propLR_TB = new EnumProperty(41);

		// Token: 0x04003BE2 RID: 15330
		protected static readonly EnumProperty s_propRL_TB = new EnumProperty(65);

		// Token: 0x04003BE3 RID: 15331
		protected static readonly EnumProperty s_propTB_RL = new EnumProperty(76);

		// Token: 0x04003BE4 RID: 15332
		private Property m_defaultProp;
	}
}
