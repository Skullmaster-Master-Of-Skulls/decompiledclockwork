using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001489 RID: 5257
	internal class BorderLeftMaker : ListProperty.Maker
	{
		// Token: 0x0600D4AF RID: 54447 RVA: 0x002F2411 File Offset: 0x002F0611
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderLeftMaker(propName);
		}

		// Token: 0x0600D4B0 RID: 54448 RVA: 0x002F2419 File Offset: 0x002F0619
		protected BorderLeftMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D4B1 RID: 54449 RVA: 0x002F2422 File Offset: 0x002F0622
		public override bool IsInherited()
		{
			return false;
		}
	}
}
