using System;
using System.IO;

namespace a.b
{
	// Token: 0x020003A4 RID: 932
	internal sealed class bu : da
	{
		// Token: 0x060021AB RID: 8619 RVA: 0x0008A080 File Offset: 0x00089080
		public bu(string A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("rtf");
			}
			this.a = new StringReader(A_0);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0008A0A2 File Offset: 0x000890A2
		public bu(TextReader A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("rtf");
			}
			this.a = A_0;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0008A0BF File Offset: 0x000890BF
		public bu(Stream A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("rtf");
			}
			this.a = new StreamReader(A_0, b3.l);
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0008A0E6 File Offset: 0x000890E6
		public TextReader d8()
		{
			return this.a;
		}

		// Token: 0x04001592 RID: 5522
		private readonly TextReader a;
	}
}
