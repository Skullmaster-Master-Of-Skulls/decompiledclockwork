using System;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015D8 RID: 5592
	internal class AreaClass
	{
		// Token: 0x0600DA09 RID: 55817 RVA: 0x002FC9CC File Offset: 0x002FABCC
		public static string setAreaClass(string areaClass)
		{
			if (areaClass.Equals(AreaClass.XSL_NORMAL) || areaClass.Equals(AreaClass.XSL_ABSOLUTE) || areaClass.Equals(AreaClass.XSL_FOOTNOTE) || areaClass.Equals(AreaClass.XSL_SIDE_FLOAT) || areaClass.Equals(AreaClass.XSL_BEFORE_FLOAT))
			{
				return areaClass;
			}
			throw new ApocException("Unknown area class '" + areaClass + "'");
		}

		// Token: 0x04003C7A RID: 15482
		public static string UNASSIGNED = "unassigned";

		// Token: 0x04003C7B RID: 15483
		public static string XSL_NORMAL = "xsl-normal";

		// Token: 0x04003C7C RID: 15484
		public static string XSL_ABSOLUTE = "xsl-absolute";

		// Token: 0x04003C7D RID: 15485
		public static string XSL_FOOTNOTE = "xsl-footnote";

		// Token: 0x04003C7E RID: 15486
		public static string XSL_SIDE_FLOAT = "xsl-side-float";

		// Token: 0x04003C7F RID: 15487
		public static string XSL_BEFORE_FLOAT = "xsl-before-float";
	}
}
