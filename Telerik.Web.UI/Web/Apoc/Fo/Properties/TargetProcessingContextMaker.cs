using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200159B RID: 5531
	internal class TargetProcessingContextMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D88F RID: 55439 RVA: 0x002F9219 File Offset: 0x002F7419
		public new static PropertyMaker Maker(string propName)
		{
			return new TargetProcessingContextMaker(propName);
		}

		// Token: 0x0600D890 RID: 55440 RVA: 0x002F9221 File Offset: 0x002F7421
		protected TargetProcessingContextMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D891 RID: 55441 RVA: 0x002F922A File Offset: 0x002F742A
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D892 RID: 55442 RVA: 0x002F922D File Offset: 0x002F742D
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "document-root", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B90 RID: 15248
		private Property m_defaultProp;
	}
}
