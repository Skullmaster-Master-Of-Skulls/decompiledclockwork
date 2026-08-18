using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014F4 RID: 5364
	internal class HeightMaker : LengthProperty.Maker
	{
		// Token: 0x0600D647 RID: 54855 RVA: 0x002F6475 File Offset: 0x002F4675
		public new static PropertyMaker Maker(string propName)
		{
			return new HeightMaker(propName);
		}

		// Token: 0x0600D648 RID: 54856 RVA: 0x002F647D File Offset: 0x002F467D
		protected HeightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D649 RID: 54857 RVA: 0x002F6486 File Offset: 0x002F4686
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D64A RID: 54858 RVA: 0x002F6489 File Offset: 0x002F4689
		protected override bool IsAutoLengthAllowed()
		{
			return true;
		}

		// Token: 0x0600D64B RID: 54859 RVA: 0x002F648C File Offset: 0x002F468C
		public override Property Make(PropertyList propertyList)
		{
			if (this.m_defaultProp == null)
			{
				this.m_defaultProp = this.Make(propertyList, "auto", propertyList.getParentFObj());
			}
			return this.m_defaultProp;
		}

		// Token: 0x04003ABB RID: 15035
		private Property m_defaultProp;
	}
}
