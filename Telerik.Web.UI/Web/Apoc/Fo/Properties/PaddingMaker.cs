using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001547 RID: 5447
	internal class PaddingMaker : ListProperty.Maker
	{
		// Token: 0x0600D776 RID: 55158 RVA: 0x002F7D01 File Offset: 0x002F5F01
		public new static PropertyMaker Maker(string propName)
		{
			return new PaddingMaker(propName);
		}

		// Token: 0x0600D777 RID: 55159 RVA: 0x002F7D09 File Offset: 0x002F5F09
		protected PaddingMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D778 RID: 55160 RVA: 0x002F7D12 File Offset: 0x002F5F12
		public override bool IsInherited()
		{
			return false;
		}
	}
}
