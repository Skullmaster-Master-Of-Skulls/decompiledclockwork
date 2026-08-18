using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200149C RID: 5276
	internal class BorderStyleMaker : ListProperty.Maker
	{
		// Token: 0x0600D4F8 RID: 54520 RVA: 0x002F2E8C File Offset: 0x002F108C
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderStyleMaker(propName);
		}

		// Token: 0x0600D4F9 RID: 54521 RVA: 0x002F2E94 File Offset: 0x002F1094
		protected BorderStyleMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4FA RID: 54522 RVA: 0x002F2E9D File Offset: 0x002F109D
		public override bool IsInherited()
		{
			return false;
		}
	}
}
