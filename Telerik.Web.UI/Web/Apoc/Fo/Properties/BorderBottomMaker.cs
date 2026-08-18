using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001479 RID: 5241
	internal class BorderBottomMaker : ListProperty.Maker
	{
		// Token: 0x0600D476 RID: 54390 RVA: 0x002F1C2D File Offset: 0x002EFE2D
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderBottomMaker(propName);
		}

		// Token: 0x0600D477 RID: 54391 RVA: 0x002F1C35 File Offset: 0x002EFE35
		protected BorderBottomMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D478 RID: 54392 RVA: 0x002F1C3E File Offset: 0x002EFE3E
		public override bool IsInherited()
		{
			return false;
		}
	}
}
