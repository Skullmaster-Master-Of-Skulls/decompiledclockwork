using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200149E RID: 5278
	internal class BorderTopMaker : ListProperty.Maker
	{
		// Token: 0x0600D501 RID: 54529 RVA: 0x002F2FD5 File Offset: 0x002F11D5
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderTopMaker(propName);
		}

		// Token: 0x0600D502 RID: 54530 RVA: 0x002F2FDD File Offset: 0x002F11DD
		protected BorderTopMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D503 RID: 54531 RVA: 0x002F2FE6 File Offset: 0x002F11E6
		public override bool IsInherited()
		{
			return false;
		}
	}
}
