using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C6 RID: 5318
	internal class ExtentMaker : LengthProperty.Maker
	{
		// Token: 0x0600D596 RID: 54678 RVA: 0x002F3C0A File Offset: 0x002F1E0A
		public new static PropertyMaker Maker(string propName)
		{
			return new ExtentMaker(propName);
		}

		// Token: 0x0600D597 RID: 54679 RVA: 0x002F3C12 File Offset: 0x002F1E12
		protected ExtentMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D598 RID: 54680 RVA: 0x002F3C1B File Offset: 0x002F1E1B
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D599 RID: 54681 RVA: 0x002F3C1E File Offset: 0x002F1E1E
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "0pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A6B RID: 14955
		private Property m_defaultProp;
	}
}
