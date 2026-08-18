using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.DataExport.CollectionEditors;

// Token: 0x02000129 RID: 297
internal sealed class sprỎ
{
	// Token: 0x0600070A RID: 1802 RVA: 0x00044270 File Offset: 0x00043270
	private sprỎ()
	{
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x00044284 File Offset: 0x00043284
	public static Color ᜅ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return Color.FromArgb(127, 157, 185);
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x000442D0 File Offset: 0x000432D0
	public static Color ᜄ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return Color.FromArgb(201, 199, 186);
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00044320 File Offset: 0x00043320
	public static Color ᜃ()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return Color.FromArgb(28, 81, 128);
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0004436C File Offset: 0x0004336C
	public static Color ᜂ()
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		return Color.FromArgb(202, 200, 187);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x000443BC File Offset: 0x000433BC
	public static Color ᜁ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return Color.FromArgb(236, 233, 216);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0004440C File Offset: 0x0004340C
	public static Color ᜀ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return Color.FromArgb(161, 161, 146);
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0004445C File Offset: 0x0004345C
	public static StringFormat ᜀ(ContentAlignment A_0)
	{
		int a_ = 19;
		int num = 24;
		StringFormat stringFormat;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 13;
				continue;
			case 1:
				if (A_0 <= ContentAlignment.BottomLeft)
				{
					num = 17;
					continue;
				}
				num = 19;
				continue;
			case 2:
				return stringFormat;
			case 3:
				goto IL_DF;
			case 4:
				if (A_0 != ContentAlignment.BottomRight)
				{
					num = 28;
					continue;
				}
				stringFormat.LineAlignment = StringAlignment.Far;
				stringFormat.Alignment = StringAlignment.Far;
				num = 12;
				continue;
			case 5:
				return stringFormat;
			case 6:
				num = 29;
				continue;
			case 7:
				return stringFormat;
			case 8:
				num = 14;
				continue;
			case 9:
				if (A_0 <= ContentAlignment.MiddleCenter)
				{
					num = 26;
					continue;
				}
				num = 1;
				continue;
			case 10:
				return stringFormat;
			case 11:
				return stringFormat;
			case 12:
				return stringFormat;
			case 13:
				return stringFormat;
			case 14:
				return stringFormat;
			case 15:
				if (A_0 != ContentAlignment.BottomLeft)
				{
					num = 0;
					continue;
				}
				stringFormat.LineAlignment = StringAlignment.Far;
				stringFormat.Alignment = StringAlignment.Near;
				num = 21;
				continue;
			case 16:
				if (A_0 != ContentAlignment.MiddleRight)
				{
					num = 30;
					continue;
				}
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.Alignment = StringAlignment.Far;
				num = 3;
				continue;
			case 17:
				num = 16;
				continue;
			case 18:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_174;
				default:
					goto IL_105;
				}
				break;
			case 19:
				if (A_0 != ContentAlignment.BottomCenter)
				{
					num = 27;
					continue;
				}
				stringFormat.LineAlignment = StringAlignment.Far;
				stringFormat.Alignment = StringAlignment.Center;
				num = 10;
				continue;
			case 20:
				if (A_0 != ContentAlignment.MiddleLeft)
				{
					num = 6;
					continue;
				}
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.Alignment = StringAlignment.Near;
				num = 2;
				continue;
			case 21:
				return stringFormat;
			case 22:
				num = 20;
				continue;
			case 23:
				return stringFormat;
			case 25:
				switch (A_0)
				{
				case ContentAlignment.TopLeft:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Near;
					num = 7;
					continue;
				case ContentAlignment.TopCenter:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Center;
					num = 23;
					continue;
				case (ContentAlignment)3:
					return stringFormat;
				case ContentAlignment.TopRight:
					stringFormat.LineAlignment = StringAlignment.Near;
					stringFormat.Alignment = StringAlignment.Far;
					num = 5;
					continue;
				}
				goto IL_174;
			case 26:
				num = 25;
				continue;
			case 27:
				num = 4;
				continue;
			case 28:
				num = 18;
				continue;
			case 29:
				if (A_0 != ContentAlignment.MiddleCenter)
				{
					num = 8;
					continue;
				}
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.Alignment = StringAlignment.Center;
				num = 11;
				continue;
			case 30:
				num = 15;
				continue;
			case 31:
				goto IL_C1;
			}
			if (!Enum.IsDefined(typeof(ContentAlignment), (int)A_0))
			{
				num = 31;
				continue;
			}
			stringFormat = new StringFormat();
			num = 9;
			continue;
			IL_174:
			if (true)
			{
			}
			num = 22;
		}
		IL_C1:
		throw new InvalidEnumArgumentException(HyperlinksCollectionEditor.b("䰮帰崲䄴制圸伺簼匾⡀⑂⭄⩆ⱈ╊㥌", a_), (int)A_0, typeof(ContentAlignment));
		IL_DF:
		return stringFormat;
		IL_105:
		if (false)
		{
		}
		return stringFormat;
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00044814 File Offset: 0x00043814
	public static void ᜀ(Graphics A_0, Pen A_1, Rectangle A_2, Size A_3)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		SmoothingMode smoothingMode = A_0.SmoothingMode;
		A_0.SmoothingMode = SmoothingMode.AntiAlias;
		A_0.DrawLine(A_1, A_2.Left + A_3.Width / 2, A_2.Top, A_2.Right - A_3.Width / 2, A_2.Top);
		A_0.DrawArc(A_1, A_2.Right - A_3.Width, A_2.Top, A_3.Width, A_3.Height, 270, 90);
		A_0.DrawLine(A_1, A_2.Right, A_2.Top + A_3.Height / 2, A_2.Right, A_2.Bottom - A_3.Height / 2);
		A_0.DrawArc(A_1, A_2.Right - A_3.Width, A_2.Bottom - A_3.Height, A_3.Width, A_3.Height, 0, 90);
		A_0.DrawLine(A_1, A_2.Right - A_3.Width / 2, A_2.Bottom, A_2.Left + A_3.Width / 2, A_2.Bottom);
		A_0.DrawArc(A_1, A_2.Left, A_2.Bottom - A_3.Height, A_3.Width, A_3.Height, 90, 90);
		A_0.DrawLine(A_1, A_2.Left, A_2.Bottom - A_3.Height / 2, A_2.Left, A_2.Top + A_3.Height / 2);
		A_0.DrawArc(A_1, A_2.Left, A_2.Top, A_3.Width, A_3.Height, 180, 90);
		A_0.SmoothingMode = smoothingMode;
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00044A04 File Offset: 0x00043A04
	public static void ᜀ(Graphics A_0, int A_1, int A_2, int A_3, int A_4)
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.DrawRectangle(new Pen(sprỎ.ᜅ(), 0f), A_1, A_2, A_3, A_4);
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00044A5C File Offset: 0x00043A5C
	public static void ᜀ(Graphics A_0, Rectangle A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		A_0.FillRectangle(new SolidBrush(SystemColors.Window), A_1.X - 2, A_1.Y, 2, A_1.Height + 1);
	}
}
