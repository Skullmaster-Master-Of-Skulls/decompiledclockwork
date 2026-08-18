using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020015A5 RID: 5541
	internal class TextIndentMaker : LengthProperty.Maker
	{
		// Token: 0x0600D8B5 RID: 55477 RVA: 0x002F96BD File Offset: 0x002F78BD
		public new static PropertyMaker Maker(string propName)
		{
			return new TextIndentMaker(propName);
		}

		// Token: 0x0600D8B6 RID: 55478 RVA: 0x002F96C5 File Offset: 0x002F78C5
		protected TextIndentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D8B7 RID: 55479 RVA: 0x002F96CE File Offset: 0x002F78CE
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D8B8 RID: 55480 RVA: 0x002F96D1 File Offset: 0x002F78D1
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003BB9 RID: 15289
		private Property m_defaultProp;
	}
}
