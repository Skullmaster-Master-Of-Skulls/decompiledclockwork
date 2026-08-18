using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001571 RID: 5489
	internal class RuleThicknessMaker : LengthProperty.Maker
	{
		// Token: 0x0600D812 RID: 55314 RVA: 0x002F8969 File Offset: 0x002F6B69
		public new static PropertyMaker Maker(string propName)
		{
			return new RuleThicknessMaker(propName);
		}

		// Token: 0x0600D813 RID: 55315 RVA: 0x002F8971 File Offset: 0x002F6B71
		protected RuleThicknessMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D814 RID: 55316 RVA: 0x002F897A File Offset: 0x002F6B7A
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D815 RID: 55317 RVA: 0x002F897D File Offset: 0x002F6B7D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "1.0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B6B RID: 15211
		private Property m_defaultProp;
	}
}
