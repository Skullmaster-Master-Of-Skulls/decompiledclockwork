using System;
using System.Xml;
using iTextSharp.text;
using MailBee;

namespace a.c
{
	// Token: 0x02000229 RID: 553
	internal abstract class j
	{
		// Token: 0x0600128F RID: 4751 RVA: 0x000523E2 File Offset: 0x000513E2
		public j(s A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.b = A_0;
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000523FC File Offset: 0x000513FC
		public s a()
		{
			return this.b;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00052404 File Offset: 0x00051404
		public Font g(XmlNode A_0)
		{
			Font font = this.a().a(A_0.Name);
			if (font != null)
			{
				return font;
			}
			return this.a().o();
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00052434 File Offset: 0x00051434
		public static Color a(string A_0, Color A_1)
		{
			A_0 = A_0.Trim().ToLower();
			if (A_0.StartsWith("#"))
			{
				A_0 = A_0.Replace("#", "0x");
				try
				{
					A_1 = new Color(Convert.ToInt32(A_0, 16));
				}
				catch (ArgumentException)
				{
				}
				return A_1;
			}
			uint num = global::b.a(A_0);
			if (num <= 1169454059U)
			{
				if (num <= 96429129U)
				{
					if (num != 17824943U)
					{
						if (num != 18738364U)
						{
							if (num == 96429129U)
							{
								if (A_0 == "yellow")
								{
									return Color.YELLOW;
								}
							}
						}
						else if (A_0 == "green")
						{
							return Color.GREEN;
						}
					}
					else if (A_0 == "light_gray")
					{
						return Color.LIGHT_GRAY;
					}
				}
				else if (num != 576586605U)
				{
					if (num != 1089765596U)
					{
						if (num == 1169454059U)
						{
							if (A_0 == "orange")
							{
								return Color.ORANGE;
							}
						}
					}
					else if (A_0 == "red")
					{
						return Color.RED;
					}
				}
				else if (A_0 == "pink")
				{
					return Color.PINK;
				}
			}
			else if (num <= 1676028392U)
			{
				if (num != 1231115066U)
				{
					if (num != 1452231588U)
					{
						if (num == 1676028392U)
						{
							if (A_0 == "magenta")
							{
								return Color.MAGENTA;
							}
						}
					}
					else if (A_0 == "black")
					{
						return Color.BLACK;
					}
				}
				else if (A_0 == "cyan")
				{
					return Color.CYAN;
				}
			}
			else if (num <= 2197550541U)
			{
				if (num != 1862053077U)
				{
					if (num == 2197550541U)
					{
						if (A_0 == "blue")
						{
							return Color.BLUE;
						}
					}
				}
				else if (A_0 == "dark_gray")
				{
					return Color.DARK_GRAY;
				}
			}
			else if (num != 3130700698U)
			{
				if (num == 3724674918U)
				{
					if (A_0 == "white")
					{
						return Color.WHITE;
					}
				}
			}
			else if (A_0 == "gray")
			{
				return Color.GRAY;
			}
			return A_1;
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00052698 File Offset: 0x00051698
		public static string a(string A_0)
		{
			string a = A_0.ToLower();
			if (a == "times new roman")
			{
				return "Times-Roman";
			}
			if (a == "courier new")
			{
				return "Courier";
			}
			if (a == "helvetica")
			{
				return "Helvetica";
			}
			if (!(a == "symbol"))
			{
				return A_0;
			}
			return "Symbol";
		}

		// Token: 0x04000F3E RID: 3902
		protected s b;
	}
}
