using System;
using System.Globalization;
using iTextSharp.text.error_messages;

namespace iTextSharp.text
{
	// Token: 0x02000640 RID: 1600
	public class PageSize
	{
		// Token: 0x0600361F RID: 13855 RVA: 0x001517D0 File Offset: 0x001507D0
		public static Rectangle GetRectangle(string name)
		{
			name = name.Trim().ToUpper(CultureInfo.InvariantCulture);
			int num = name.IndexOf(' ');
			if (num == -1)
			{
				try
				{
					return (Rectangle)typeof(PageSize).GetField(name).GetValue(null);
				}
				catch (Exception)
				{
					throw new ArgumentException(MessageLocalization.GetComposedMessage("can.t.find.page.size.1", name));
				}
			}
			Rectangle result;
			try
			{
				string s = name.Substring(0, num);
				string s2 = name.Substring(num + 1);
				result = new Rectangle(float.Parse(s, NumberFormatInfo.InvariantInfo), float.Parse(s2, NumberFormatInfo.InvariantInfo));
			}
			catch (Exception ex)
			{
				throw new ArgumentException(MessageLocalization.GetComposedMessage("1.is.not.a.valid.page.size.format.2", name, ex.Message));
			}
			return result;
		}

		// Token: 0x04002467 RID: 9319
		public static readonly Rectangle LETTER = new RectangleReadOnly(612f, 792f);

		// Token: 0x04002468 RID: 9320
		public static readonly Rectangle NOTE = new RectangleReadOnly(540f, 720f);

		// Token: 0x04002469 RID: 9321
		public static readonly Rectangle LEGAL = new RectangleReadOnly(612f, 1008f);

		// Token: 0x0400246A RID: 9322
		public static readonly Rectangle TABLOID = new RectangleReadOnly(792f, 1224f);

		// Token: 0x0400246B RID: 9323
		public static readonly Rectangle EXECUTIVE = new RectangleReadOnly(522f, 756f);

		// Token: 0x0400246C RID: 9324
		public static readonly Rectangle POSTCARD = new RectangleReadOnly(283f, 416f);

		// Token: 0x0400246D RID: 9325
		public static readonly Rectangle A0 = new RectangleReadOnly(2384f, 3370f);

		// Token: 0x0400246E RID: 9326
		public static readonly Rectangle A1 = new RectangleReadOnly(1684f, 2384f);

		// Token: 0x0400246F RID: 9327
		public static readonly Rectangle A2 = new RectangleReadOnly(1191f, 1684f);

		// Token: 0x04002470 RID: 9328
		public static readonly Rectangle A3 = new RectangleReadOnly(842f, 1191f);

		// Token: 0x04002471 RID: 9329
		public static readonly Rectangle A4 = new RectangleReadOnly(595f, 842f);

		// Token: 0x04002472 RID: 9330
		public static readonly Rectangle A5 = new RectangleReadOnly(420f, 595f);

		// Token: 0x04002473 RID: 9331
		public static readonly Rectangle A6 = new RectangleReadOnly(297f, 420f);

		// Token: 0x04002474 RID: 9332
		public static readonly Rectangle A7 = new RectangleReadOnly(210f, 297f);

		// Token: 0x04002475 RID: 9333
		public static readonly Rectangle A8 = new RectangleReadOnly(148f, 210f);

		// Token: 0x04002476 RID: 9334
		public static readonly Rectangle A9 = new RectangleReadOnly(105f, 148f);

		// Token: 0x04002477 RID: 9335
		public static readonly Rectangle A10 = new RectangleReadOnly(73f, 105f);

		// Token: 0x04002478 RID: 9336
		public static readonly Rectangle B0 = new RectangleReadOnly(2834f, 4008f);

		// Token: 0x04002479 RID: 9337
		public static readonly Rectangle B1 = new RectangleReadOnly(2004f, 2834f);

		// Token: 0x0400247A RID: 9338
		public static readonly Rectangle B2 = new RectangleReadOnly(1417f, 2004f);

		// Token: 0x0400247B RID: 9339
		public static readonly Rectangle B3 = new RectangleReadOnly(1000f, 1417f);

		// Token: 0x0400247C RID: 9340
		public static readonly Rectangle B4 = new RectangleReadOnly(708f, 1000f);

		// Token: 0x0400247D RID: 9341
		public static readonly Rectangle B5 = new RectangleReadOnly(498f, 708f);

		// Token: 0x0400247E RID: 9342
		public static readonly Rectangle B6 = new RectangleReadOnly(354f, 498f);

		// Token: 0x0400247F RID: 9343
		public static readonly Rectangle B7 = new RectangleReadOnly(249f, 354f);

		// Token: 0x04002480 RID: 9344
		public static readonly Rectangle B8 = new RectangleReadOnly(175f, 249f);

		// Token: 0x04002481 RID: 9345
		public static readonly Rectangle B9 = new RectangleReadOnly(124f, 175f);

		// Token: 0x04002482 RID: 9346
		public static readonly Rectangle B10 = new RectangleReadOnly(87f, 124f);

		// Token: 0x04002483 RID: 9347
		public static readonly Rectangle ARCH_E = new RectangleReadOnly(2592f, 3456f);

		// Token: 0x04002484 RID: 9348
		public static readonly Rectangle ARCH_D = new RectangleReadOnly(1728f, 2592f);

		// Token: 0x04002485 RID: 9349
		public static readonly Rectangle ARCH_C = new RectangleReadOnly(1296f, 1728f);

		// Token: 0x04002486 RID: 9350
		public static readonly Rectangle ARCH_B = new RectangleReadOnly(864f, 1296f);

		// Token: 0x04002487 RID: 9351
		public static readonly Rectangle ARCH_A = new RectangleReadOnly(648f, 864f);

		// Token: 0x04002488 RID: 9352
		public static readonly Rectangle FLSA = new RectangleReadOnly(612f, 936f);

		// Token: 0x04002489 RID: 9353
		public static readonly Rectangle FLSE = new RectangleReadOnly(648f, 936f);

		// Token: 0x0400248A RID: 9354
		public static readonly Rectangle HALFLETTER = new RectangleReadOnly(396f, 612f);

		// Token: 0x0400248B RID: 9355
		public static readonly Rectangle _11X17 = new RectangleReadOnly(792f, 1224f);

		// Token: 0x0400248C RID: 9356
		public static readonly Rectangle ID_1 = new RectangleReadOnly(242.65f, 153f);

		// Token: 0x0400248D RID: 9357
		public static readonly Rectangle ID_2 = new RectangleReadOnly(297f, 210f);

		// Token: 0x0400248E RID: 9358
		public static readonly Rectangle ID_3 = new RectangleReadOnly(354f, 249f);

		// Token: 0x0400248F RID: 9359
		public static readonly Rectangle LEDGER = new RectangleReadOnly(1224f, 792f);

		// Token: 0x04002490 RID: 9360
		public static readonly Rectangle CROWN_QUARTO = new RectangleReadOnly(535f, 697f);

		// Token: 0x04002491 RID: 9361
		public static readonly Rectangle LARGE_CROWN_QUARTO = new RectangleReadOnly(569f, 731f);

		// Token: 0x04002492 RID: 9362
		public static readonly Rectangle DEMY_QUARTO = new RectangleReadOnly(620f, 782f);

		// Token: 0x04002493 RID: 9363
		public static readonly Rectangle ROYAL_QUARTO = new RectangleReadOnly(671f, 884f);

		// Token: 0x04002494 RID: 9364
		public static readonly Rectangle CROWN_OCTAVO = new RectangleReadOnly(348f, 527f);

		// Token: 0x04002495 RID: 9365
		public static readonly Rectangle LARGE_CROWN_OCTAVO = new RectangleReadOnly(365f, 561f);

		// Token: 0x04002496 RID: 9366
		public static readonly Rectangle DEMY_OCTAVO = new RectangleReadOnly(391f, 612f);

		// Token: 0x04002497 RID: 9367
		public static readonly Rectangle ROYAL_OCTAVO = new RectangleReadOnly(442f, 663f);

		// Token: 0x04002498 RID: 9368
		public static readonly Rectangle SMALL_PAPERBACK = new RectangleReadOnly(314f, 504f);

		// Token: 0x04002499 RID: 9369
		public static readonly Rectangle PENGUIN_SMALL_PAPERBACK = new RectangleReadOnly(314f, 513f);

		// Token: 0x0400249A RID: 9370
		public static readonly Rectangle PENGUIN_LARGE_PAPERBACK = new RectangleReadOnly(365f, 561f);
	}
}
