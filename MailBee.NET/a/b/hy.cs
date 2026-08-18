using System;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x02000278 RID: 632
	internal class hy
	{
		// Token: 0x06001691 RID: 5777 RVA: 0x0006753A File Offset: 0x0006653A
		public virtual long j()
		{
			if (this.h.Length != 0)
			{
				return ii.a(this.h);
			}
			return -1L;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00067553 File Offset: 0x00066553
		public virtual string c()
		{
			return this.a(this.f);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00067564 File Offset: 0x00066564
		public virtual string a(int A_0)
		{
			if (A_0 == 31)
			{
				try
				{
					if (this.i)
					{
						return "External string reference!";
					}
					return Encoding.GetEncoding("UTF-16LE").GetString(this.h, 0, this.h.Length);
				}
				catch (IOException)
				{
					return string.Empty;
				}
			}
			if (A_0 == 30)
			{
				return Encoding.Default.GetString(this.h, 0, this.h.Length);
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < this.h.Length; i++)
			{
				int num = (int)(this.h[i] & byte.MaxValue);
				if (char.IsLetterOrDigit((char)num))
				{
					stringBuilder.Append((char)num);
					stringBuilder.Append(" ");
				}
				else
				{
					stringBuilder.Append(". ");
				}
				string text = Convert.ToString(num, 16);
				stringBuilder2.Append(text);
				stringBuilder2.Append(" ");
				if (text.Length > 1)
				{
					stringBuilder.Append(" ");
				}
			}
			stringBuilder.Append("\n");
			stringBuilder.Append("\t");
			stringBuilder.Append(stringBuilder2);
			return new string(stringBuilder.ToString().ToCharArray());
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000676B0 File Offset: 0x000666B0
		public override string ToString()
		{
			string text = bs.a(this.e, this.f);
			if (this.f == 11)
			{
				return text + ((this.g == 0) ? "false" : "true");
			}
			if (this.i)
			{
				return text + string.Format("0x%08X (%d)", this.g, this.g);
			}
			if (this.f == 5 || this.f == 20)
			{
				if (this.h == null)
				{
					return text + "no data";
				}
				if (this.h.Length == 8)
				{
					long num = ii.b(this.h, 0, 8);
					return string.Format("%s0x%016X (%d)", text, num, num);
				}
				return string.Format("%s invalid data length: %d", text, this.h.Length);
			}
			else
			{
				if (this.f == 64)
				{
					long num2 = ii.b(this.h, 4, 8);
					long num3 = ii.b(this.h, 0, 4);
					DateTime time = DateTime.FromFileTime(num2 << 32 | num3);
					TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
					return text + currentTimeZone.ToUniversalTime(time).ToString("yyyyMMdd HHmmss");
				}
				if (this.f != 31)
				{
					return text + this.c();
				}
				string text2;
				try
				{
					text2 = Encoding.GetEncoding("UTF-16LE").GetString(this.h, 0, this.h.Length);
				}
				catch (IOException)
				{
					text2 = "";
				}
				if (text2.Length >= 2 && text2[0] == '\u0001')
				{
					return string.Format("%s [%04X][%04X]%s", new object[]
					{
						text,
						(short)text2[0],
						(short)text2[1],
						text2.Substring(2)
					});
				}
				return text + text2;
			}
		}

		// Token: 0x040010D6 RID: 4310
		public const int a = 31;

		// Token: 0x040010D7 RID: 4311
		public const int b = 30;

		// Token: 0x040010D8 RID: 4312
		public const int c = 258;

		// Token: 0x040010D9 RID: 4313
		public int d;

		// Token: 0x040010DA RID: 4314
		public int e;

		// Token: 0x040010DB RID: 4315
		public int f;

		// Token: 0x040010DC RID: 4316
		public int g;

		// Token: 0x040010DD RID: 4317
		public byte[] h = new byte[0];

		// Token: 0x040010DE RID: 4318
		public bool i;
	}
}
