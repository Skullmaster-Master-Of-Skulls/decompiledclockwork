using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014C4 RID: 5316
	internal class EndsRowMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D58F RID: 54671 RVA: 0x002F3B81 File Offset: 0x002F1D81
		public new static PropertyMaker Maker(string propName)
		{
			return new EndsRowMaker(propName);
		}

		// Token: 0x0600D590 RID: 54672 RVA: 0x002F3B89 File Offset: 0x002F1D89
		protected EndsRowMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D591 RID: 54673 RVA: 0x002F3B92 File Offset: 0x002F1D92
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D592 RID: 54674 RVA: 0x002F3B95 File Offset: 0x002F1D95
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "false", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003A69 RID: 14953
		private Property m_defaultProp;
	}
}
