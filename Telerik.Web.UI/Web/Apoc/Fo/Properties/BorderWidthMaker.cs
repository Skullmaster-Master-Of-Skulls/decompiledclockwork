using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014A2 RID: 5282
	internal class BorderWidthMaker : ListProperty.Maker
	{
		// Token: 0x0600D50D RID: 54541 RVA: 0x002F3209 File Offset: 0x002F1409
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderWidthMaker(propName);
		}

		// Token: 0x0600D50E RID: 54542 RVA: 0x002F3211 File Offset: 0x002F1411
		protected BorderWidthMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D50F RID: 54543 RVA: 0x002F321A File Offset: 0x002F141A
		public override bool IsInherited()
		{
			return false;
		}
	}
}
