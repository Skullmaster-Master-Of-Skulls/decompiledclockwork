using System;

namespace Telerik.Web.Apoc.Fo.Properties
{
	// Token: 0x02001480 RID: 5248
	internal class BorderColorMaker : ListProperty.Maker
	{
		// Token: 0x0600D490 RID: 54416 RVA: 0x002F1FF6 File Offset: 0x002F01F6
		public new static PropertyMaker Maker(string propName)
		{
			return new BorderColorMaker(propName);
		}

		// Token: 0x0600D491 RID: 54417 RVA: 0x002F1FFE File Offset: 0x002F01FE
		protected BorderColorMaker(string name) : base(name)
		{
		}

		// Token: 0x0600D492 RID: 54418 RVA: 0x002F2007 File Offset: 0x002F0207
		public override bool IsInherited()
		{
			return false;
		}
	}
}
