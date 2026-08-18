using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x020014DE RID: 5342
	internal class GenericCondPadding : GenericCondLength
	{
		// Token: 0x0600D5F4 RID: 54772 RVA: 0x002F5C28 File Offset: 0x002F3E28
		public new static PropertyMaker Maker(string propName)
		{
			return new GenericCondPadding(propName);
		}

		// Token: 0x0600D5F5 RID: 54773 RVA: 0x002F5C30 File Offset: 0x002F3E30
		protected GenericCondPadding(string name) : base(name)
		{
		}

		// Token: 0x0600D5F6 RID: 54774 RVA: 0x002F5C39 File Offset: 0x002F3E39
		public override bool IsInherited()
		{
			return false;
		}

		// Token: 0x0600D5F7 RID: 54775 RVA: 0x002F5C3C File Offset: 0x002F3E3C
		protected override string getDefaultForLength()
		{
			return "0pt";
		}
	}
}
