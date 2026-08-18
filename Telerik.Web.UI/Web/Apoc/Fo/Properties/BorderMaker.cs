using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x0200148D RID: 5261
	internal class BorderMaker : ListProperty.Maker
	{
		// Token: 0x0600D4BB RID: 54459 RVA: 0x002F2645 File Offset: 0x002F0845
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderMaker(propName);
		}

		// Token: 0x0600D4BC RID: 54460 RVA: 0x002F264D File Offset: 0x002F084D
		protected BorderMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4BD RID: 54461 RVA: 0x002F2656 File Offset: 0x002F0856
		public override bool IsInherited()
		{
			return false;
		}
	}
}
