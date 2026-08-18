using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200150D RID: 5389
	internal class KeepWithNextMaker : GenericKeep
	{
		// Token: 0x0600D69D RID: 54941 RVA: 0x002F6BA7 File Offset: 0x002F4DA7
		public new static PropertyMaker Maker(string propName)
		{
			return new KeepWithNextMaker(propName);
		}

		// Token: 0x0600D69E RID: 54942 RVA: 0x002F6BAF File Offset: 0x002F4DAF
		protected KeepWithNextMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D69F RID: 54943 RVA: 0x002F6BB8 File Offset: 0x002F4DB8
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D6A0 RID: 54944 RVA: 0x002F6BBB File Offset: 0x002F4DBB
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003AD0 RID: 15056
		private Property m_defaultProp;
	}
}
