using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001579 RID: 5497
	internal class SourceDocumentMaker : StringProperty.Maker
	{
		// Token: 0x0600D831 RID: 55345 RVA: 0x002F8B5E File Offset: 0x002F6D5E
		public new static PropertyMaker Maker(string propName)
		{
			return new SourceDocumentMaker(propName);
		}

		// Token: 0x0600D832 RID: 55346 RVA: 0x002F8B66 File Offset: 0x002F6D66
		protected SourceDocumentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D833 RID: 55347 RVA: 0x002F8B6F File Offset: 0x002F6D6F
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D834 RID: 55348 RVA: 0x002F8B72 File Offset: 0x002F6D72
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "none", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B76 RID: 15222
		private Property m_defaultProp;
	}
}
