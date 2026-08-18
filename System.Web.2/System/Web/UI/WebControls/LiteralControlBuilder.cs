using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200045A RID: 1114
	public class LiteralControlBuilder : ControlBuilder
	{
		// Token: 0x060035F8 RID: 13816 RVA: 0x00007722 File Offset: 0x00005922
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000AEAC7 File Offset: 0x000ACCC7
		public override void AppendLiteralString(string s)
		{
			if (Util.IsWhiteSpaceString(s))
			{
				base.AppendLiteralString(s);
				return;
			}
			base.PreprocessAttribute(string.Empty, "text", s, false, 0, 0);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000AEAED File Offset: 0x000ACCED
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
			throw new HttpException(SR.GetString("Control_does_not_allow_children", new object[]
			{
				base.ControlType.ToString()
			}));
		}
	}
}
