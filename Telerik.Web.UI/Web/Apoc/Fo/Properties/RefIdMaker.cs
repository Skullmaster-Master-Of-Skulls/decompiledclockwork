using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001561 RID: 5473
	internal class RefIdMaker : StringProperty.Maker
	{
		// Token: 0x0600D7D5 RID: 55253 RVA: 0x002F8426 File Offset: 0x002F6626
		public new static PropertyMaker Maker(string propName)
		{
			return new RefIdMaker(propName);
		}

		// Token: 0x0600D7D6 RID: 55254 RVA: 0x002F842E File Offset: 0x002F662E
		protected RefIdMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7D7 RID: 55255 RVA: 0x002F8437 File Offset: 0x002F6637
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D7D8 RID: 55256 RVA: 0x002F843A File Offset: 0x002F663A
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B3F RID: 15167
		private Property m_defaultProp;
	}
}
