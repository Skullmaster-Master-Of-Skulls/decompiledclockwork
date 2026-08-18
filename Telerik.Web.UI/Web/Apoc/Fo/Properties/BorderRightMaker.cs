using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200148F RID: 5263
	internal class BorderRightMaker : ListProperty.Maker
	{
		// Token: 0x0600D4C4 RID: 54468 RVA: 0x002F2791 File Offset: 0x002F0991
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderRightMaker(propName);
		}

		// Token: 0x0600D4C5 RID: 54469 RVA: 0x002F2799 File Offset: 0x002F0999
		protected BorderRightMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4C6 RID: 54470 RVA: 0x002F27A2 File Offset: 0x002F09A2
		public override bool IsInherited()
		{
			return false;
		}
	}
}
