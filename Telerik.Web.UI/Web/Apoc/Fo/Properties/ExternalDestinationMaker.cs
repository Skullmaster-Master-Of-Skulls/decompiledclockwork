using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C7 RID: 5319
	internal class ExternalDestinationMaker : StringProperty.Maker
	{
		// Token: 0x0600D59A RID: 54682 RVA: 0x002F3C46 File Offset: 0x002F1E46
		public new static PropertyMaker Maker(string propName)
		{
			return new ExternalDestinationMaker(propName);
		}

		// Token: 0x0600D59B RID: 54683 RVA: 0x002F3C4E File Offset: 0x002F1E4E
		protected ExternalDestinationMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D59C RID: 54684 RVA: 0x002F3C57 File Offset: 0x002F1E57
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D59D RID: 54685 RVA: 0x002F3C5A File Offset: 0x002F1E5A
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A6C RID: 14956
		private Property m_defaultProp;
	}
}
