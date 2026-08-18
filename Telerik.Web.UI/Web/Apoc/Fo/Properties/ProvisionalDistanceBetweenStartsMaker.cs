using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200155E RID: 5470
	internal class ProvisionalDistanceBetweenStartsMaker : LengthProperty.Maker
	{
		// Token: 0x0600D7C9 RID: 55241 RVA: 0x002F8372 File Offset: 0x002F6572
		public new static PropertyMaker Maker(string propName)
		{
			return new ProvisionalDistanceBetweenStartsMaker(propName);
		}

		// Token: 0x0600D7CA RID: 55242 RVA: 0x002F837A File Offset: 0x002F657A
		protected ProvisionalDistanceBetweenStartsMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D7CB RID: 55243 RVA: 0x002F8383 File Offset: 0x002F6583
		public override bool IsInherited()
		{
			return true;
		}

		// Token: 0x0600D7CC RID: 55244 RVA: 0x002F8386 File Offset: 0x002F6586
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "24pt", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B3C RID: 15164
		private Property m_defaultProp;
	}
}
