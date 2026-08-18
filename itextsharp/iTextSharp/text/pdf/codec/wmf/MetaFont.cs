using System;
using System.Globalization;
using System.Text;

namespace iTextSharp.text.pdf.codec.wmf
{
	// Token: 0x020004A7 RID: 1191
	public class MetaFont : MetaObject
	{
		// Token: 0x06002844 RID: 10308 RVA: 0x000F2E80 File Offset: 0x000F1E80
		public MetaFont()
		{
			this.type = 3;
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000F2E9C File Offset: 0x000F1E9C
		public void Init(InputMeta meta)
		{
			this.height = Math.Abs(meta.ReadShort());
			meta.Skip(2);
			this.angle = (float)((double)meta.ReadShort() / 1800.0 * 3.141592653589793);
			meta.Skip(2);
			this.bold = ((meta.ReadShort() >= 600) ? 1 : 0);
			this.italic = ((meta.ReadByte() != 0) ? 2 : 0);
			this.underline = (meta.ReadByte() != 0);
			this.strikeout = (meta.ReadByte() != 0);
			this.charset = meta.ReadByte();
			meta.Skip(3);
			this.pitchAndFamily = meta.ReadByte();
			byte[] array = new byte[32];
			int i;
			for (i = 0; i < 32; i++)
			{
				int num = meta.ReadByte();
				if (num == 0)
				{
					break;
				}
				array[i] = (byte)num;
			}
			try
			{
				this.faceName = Encoding.GetEncoding(1252).GetString(array, 0, i);
			}
			catch
			{
				this.faceName = Encoding.ASCII.GetString(array, 0, i);
			}
			this.faceName = this.faceName.ToLower(CultureInfo.InvariantCulture);
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x000F2FD0 File Offset: 0x000F1FD0
		public BaseFont Font
		{
			get
			{
				if (this.font != null)
				{
					return this.font;
				}
				Font font = FontFactory.GetFont(this.faceName, "Cp1252", true, 10f, ((this.italic != 0) ? 2 : 0) | ((this.bold != 0) ? 1 : 0));
				this.font = font.BaseFont;
				if (this.font != null)
				{
					return this.font;
				}
				string name;
				if (this.faceName.IndexOf("courier") != -1 || this.faceName.IndexOf("terminal") != -1 || this.faceName.IndexOf("fixedsys") != -1)
				{
					name = MetaFont.fontNames[this.italic + this.bold];
				}
				else if (this.faceName.IndexOf("ms sans serif") != -1 || this.faceName.IndexOf("arial") != -1 || this.faceName.IndexOf("system") != -1)
				{
					name = MetaFont.fontNames[4 + this.italic + this.bold];
				}
				else if (this.faceName.IndexOf("arial black") != -1)
				{
					name = MetaFont.fontNames[4 + this.italic + 1];
				}
				else if (this.faceName.IndexOf("times") != -1 || this.faceName.IndexOf("ms serif") != -1 || this.faceName.IndexOf("roman") != -1)
				{
					name = MetaFont.fontNames[8 + this.italic + this.bold];
				}
				else if (this.faceName.IndexOf("symbol") != -1)
				{
					name = MetaFont.fontNames[12];
				}
				else
				{
					int num = this.pitchAndFamily & 3;
					switch (this.pitchAndFamily >> 4 & 7)
					{
					case 1:
						name = MetaFont.fontNames[8 + this.italic + this.bold];
						break;
					case 2:
					case 4:
					case 5:
						name = MetaFont.fontNames[4 + this.italic + this.bold];
						break;
					case 3:
						name = MetaFont.fontNames[this.italic + this.bold];
						break;
					default:
					{
						int num2 = num;
						if (num2 == 1)
						{
							name = MetaFont.fontNames[this.italic + this.bold];
						}
						else
						{
							name = MetaFont.fontNames[4 + this.italic + this.bold];
						}
						break;
					}
					}
				}
				this.font = BaseFont.CreateFont(name, "Cp1252", false);
				return this.font;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x000F3243 File Offset: 0x000F2243
		public float Angle
		{
			get
			{
				return this.angle;
			}
		}

		// Token: 0x06002848 RID: 10312 RVA: 0x000F324B File Offset: 0x000F224B
		public bool IsUnderline()
		{
			return this.underline;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x000F3253 File Offset: 0x000F2253
		public bool IsStrikeout()
		{
			return this.strikeout;
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x000F325B File Offset: 0x000F225B
		public float GetFontSize(MetaState state)
		{
			return Math.Abs(state.TransformY(this.height) - state.TransformY(0)) * Document.WmfFontCorrection;
		}

		// Token: 0x04001BA2 RID: 7074
		internal const int MARKER_BOLD = 1;

		// Token: 0x04001BA3 RID: 7075
		internal const int MARKER_ITALIC = 2;

		// Token: 0x04001BA4 RID: 7076
		internal const int MARKER_COURIER = 0;

		// Token: 0x04001BA5 RID: 7077
		internal const int MARKER_HELVETICA = 4;

		// Token: 0x04001BA6 RID: 7078
		internal const int MARKER_TIMES = 8;

		// Token: 0x04001BA7 RID: 7079
		internal const int MARKER_SYMBOL = 12;

		// Token: 0x04001BA8 RID: 7080
		internal const int DEFAULT_PITCH = 0;

		// Token: 0x04001BA9 RID: 7081
		internal const int FIXED_PITCH = 1;

		// Token: 0x04001BAA RID: 7082
		internal const int VARIABLE_PITCH = 2;

		// Token: 0x04001BAB RID: 7083
		internal const int FF_DONTCARE = 0;

		// Token: 0x04001BAC RID: 7084
		internal const int FF_ROMAN = 1;

		// Token: 0x04001BAD RID: 7085
		internal const int FF_SWISS = 2;

		// Token: 0x04001BAE RID: 7086
		internal const int FF_MODERN = 3;

		// Token: 0x04001BAF RID: 7087
		internal const int FF_SCRIPT = 4;

		// Token: 0x04001BB0 RID: 7088
		internal const int FF_DECORATIVE = 5;

		// Token: 0x04001BB1 RID: 7089
		internal const int BOLDTHRESHOLD = 600;

		// Token: 0x04001BB2 RID: 7090
		internal const int nameSize = 32;

		// Token: 0x04001BB3 RID: 7091
		internal const int ETO_OPAQUE = 2;

		// Token: 0x04001BB4 RID: 7092
		internal const int ETO_CLIPPED = 4;

		// Token: 0x04001BB5 RID: 7093
		private static string[] fontNames = new string[]
		{
			"Courier",
			"Courier-Bold",
			"Courier-Oblique",
			"Courier-BoldOblique",
			"Helvetica",
			"Helvetica-Bold",
			"Helvetica-Oblique",
			"Helvetica-BoldOblique",
			"Times-Roman",
			"Times-Bold",
			"Times-Italic",
			"Times-BoldItalic",
			"Symbol",
			"ZapfDingbats"
		};

		// Token: 0x04001BB6 RID: 7094
		private int height;

		// Token: 0x04001BB7 RID: 7095
		private float angle;

		// Token: 0x04001BB8 RID: 7096
		private int bold;

		// Token: 0x04001BB9 RID: 7097
		private int italic;

		// Token: 0x04001BBA RID: 7098
		private bool underline;

		// Token: 0x04001BBB RID: 7099
		private bool strikeout;

		// Token: 0x04001BBC RID: 7100
		private int charset;

		// Token: 0x04001BBD RID: 7101
		private int pitchAndFamily;

		// Token: 0x04001BBE RID: 7102
		private string faceName = "arial";

		// Token: 0x04001BBF RID: 7103
		private BaseFont font;
	}
}
