using System;
using System.Text;
using System.util;

namespace iTextSharp.text.xml.xmp
{
	// Token: 0x020000BC RID: 188
	public abstract class XmpSchema : Properties
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001E56F File Offset: 0x0001D56F
		public XmpSchema(string xmlns)
		{
			this.xmlns = xmlns;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001E580 File Offset: 0x0001D580
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object p in base.Keys)
			{
				this.Process(stringBuilder, p);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001E5E0 File Offset: 0x0001D5E0
		protected void Process(StringBuilder buf, object p)
		{
			buf.Append('<');
			buf.Append(p);
			buf.Append('>');
			buf.Append(this[p.ToString()]);
			buf.Append("</");
			buf.Append(p);
			buf.Append('>');
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001E637 File Offset: 0x0001D637
		public string Xmlns
		{
			get
			{
				return this.xmlns;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001E63F File Offset: 0x0001D63F
		public void AddProperty(string key, string value)
		{
			this[key] = value;
		}

		// Token: 0x17000110 RID: 272
		public override string this[string key]
		{
			set
			{
				base[key] = XmpSchema.Escape(value);
			}
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001E658 File Offset: 0x0001D658
		public void SetProperty(string key, XmpArray value)
		{
			base[key] = value.ToString();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001E667 File Offset: 0x0001D667
		public void SetProperty(string key, LangAlt value)
		{
			base[key] = value.ToString();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001E678 File Offset: 0x0001D678
		public static string Escape(string content)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < content.Length; i++)
			{
				char c = content[i];
				if (c != '"')
				{
					switch (c)
					{
					case '&':
						stringBuilder.Append("&amp;");
						break;
					case '\'':
						stringBuilder.Append("&apos;");
						break;
					default:
						switch (c)
						{
						case '<':
							stringBuilder.Append("&lt;");
							goto IL_96;
						case '>':
							stringBuilder.Append("&gt;");
							goto IL_96;
						}
						stringBuilder.Append(content[i]);
						break;
					}
				}
				else
				{
					stringBuilder.Append("&quot;");
				}
				IL_96:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002D1 RID: 721
		protected string xmlns;
	}
}
