using System;
using iTextSharp.text.error_messages;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000459 RID: 1113
	public class PdfVisibilityExpression : PdfArray
	{
		// Token: 0x06002599 RID: 9625 RVA: 0x000E3DF4 File Offset: 0x000E2DF4
		public PdfVisibilityExpression(int type)
		{
			switch (type)
			{
			case -1:
				base.Add(PdfName.NOT);
				return;
			case 0:
				base.Add(PdfName.OR);
				return;
			case 1:
				base.Add(PdfName.AND);
				return;
			default:
				throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
			}
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000E3E55 File Offset: 0x000E2E55
		public override void Add(int index, PdfObject element)
		{
			throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000E3E66 File Offset: 0x000E2E66
		public override bool Add(PdfObject obj)
		{
			if (obj is PdfLayer)
			{
				return base.Add(((PdfLayer)obj).Ref);
			}
			if (obj is PdfVisibilityExpression)
			{
				return base.Add(obj);
			}
			throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000E3EA1 File Offset: 0x000E2EA1
		public override void AddFirst(PdfObject obj)
		{
			throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000E3EB2 File Offset: 0x000E2EB2
		public override bool Add(float[] values)
		{
			throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000E3EC3 File Offset: 0x000E2EC3
		public override bool Add(int[] values)
		{
			throw new ArgumentException(MessageLocalization.GetComposedMessage("illegal.ve.value"));
		}

		// Token: 0x04001A33 RID: 6707
		public const int OR = 0;

		// Token: 0x04001A34 RID: 6708
		public const int AND = 1;

		// Token: 0x04001A35 RID: 6709
		public const int NOT = -1;
	}
}
