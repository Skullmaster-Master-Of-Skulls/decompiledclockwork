using System;
using Telerik.Web.Apoc.DataTypes;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200159A RID: 5530
	internal class TargetPresentationContextMaker : ToBeImplementedProperty.Maker
	{
		// Token: 0x0600D88B RID: 55435 RVA: 0x002F91DD File Offset: 0x002F73DD
		public new static PropertyMaker Maker(string propName)
		{
			return new TargetPresentationContextMaker(propName);
		}

		// Token: 0x0600D88C RID: 55436 RVA: 0x002F91E5 File Offset: 0x002F73E5
		protected TargetPresentationContextMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D88D RID: 55437 RVA: 0x002F91EE File Offset: 0x002F73EE
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D88E RID: 55438 RVA: 0x002F91F1 File Offset: 0x002F73F1
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "use-target-processing-context", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003B8F RID: 15247
		private Property m_defaultProp;
	}
}
