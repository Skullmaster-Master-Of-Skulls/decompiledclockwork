using System;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000279 RID: 633
	internal class c0 : i8
	{
		// Token: 0x06001696 RID: 5782 RVA: 0x000678B4 File Offset: 0x000668B4
		internal c0(di A_0) : base(A_0, new fb())
		{
			if (this.b != 188)
			{
				throw new MailBeePstParsingException(Resources.Instance.ErrorDesc_OutlookPstUnableToCreateBcTable, 1210);
			}
			i8.a a = this.a(this.g);
			byte[] array = new byte[a.a()];
			a.c.a((long)a.a);
			a.c.b(array);
			this.h = array.Length / (this.e + this.f);
			this.l.Append("Number of entries: ");
			this.l.Append(this.h);
			this.l.Append("\n");
			int num = 0;
			int i = 0;
			while (i < this.h)
			{
				e2 e = new e2();
				e.d = i;
				e.e = (int)ii.b(array, num, num + 2);
				e.f = (int)ii.b(array, num + 2, num + 4);
				e.g = (int)ii.b(array, num + 4, num + 8);
				switch (e.f)
				{
				case 1:
				case 3:
				case 4:
				case 10:
					goto IL_169;
				case 2:
					e.g &= 65535;
					goto IL_169;
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 12:
				case 13:
					goto IL_193;
				case 11:
					e.g &= 255;
					e.i = true;
					break;
				default:
					goto IL_193;
				}
				IL_203:
				num += 8;
				this.a.a(e.e, e);
				this.l.Append(e.ToString());
				this.l.Append("\n\n");
				i++;
				continue;
				IL_169:
				e.i = true;
				goto IL_203;
				IL_193:
				e.i = true;
				i8.a a2 = this.a(e.g);
				if (a2 != null)
				{
					byte[] array2 = new byte[a2.a()];
					a2.c.a((long)a2.a);
					a2.c.b(array2);
					e.h = array2;
					e.a(a2.c.c());
					e.i = false;
					goto IL_203;
				}
				goto IL_203;
			}
			this.c();
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00067B16 File Offset: 0x00066B16
		public new gs a()
		{
			return this.a;
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00067B1E File Offset: 0x00066B1E
		public override string ToString()
		{
			return this.l.ToString();
		}

		// Token: 0x040010DF RID: 4319
		private new gs a = new gs();
	}
}
