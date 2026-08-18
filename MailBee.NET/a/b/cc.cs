using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002D7 RID: 727
	internal class cc : IEnumerator<he>
	{
		// Token: 0x06001997 RID: 6551 RVA: 0x000719CC File Offset: 0x000709CC
		public cc(e9 A_0, int A_1)
		{
			this.c = A_0;
			this.b = A_1;
			try
			{
				this.a = A_0.ik();
			}
			catch (IOException ex)
			{
				throw new Exception(ex.Message);
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00071A18 File Offset: 0x00070A18
		public bool d()
		{
			return this.b != -2;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00071A28 File Offset: 0x00070A28
		public he a()
		{
			if (this.b == -2)
			{
				throw new IndexOutOfRangeException("Can't read past the end of the stream");
			}
			he result;
			try
			{
				this.a.a(this.b);
				he he = this.c.ie(this.b);
				this.b = this.c.ih(this.b);
				result = he;
			}
			catch (IOException ex)
			{
				throw new RuntimeException(ex.Message);
			}
			return result;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00071AA4 File Offset: 0x00070AA4
		public void b()
		{
			throw new NotImplementedException("Unsupported Operations!");
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00071AB0 File Offset: 0x00070AB0
		public he get_Current()
		{
			return this.d;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00071AB8 File Offset: 0x00070AB8
		object IEnumerator.e()
		{
			return this.d;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00071AC0 File Offset: 0x00070AC0
		void IEnumerator.f()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00071AC8 File Offset: 0x00070AC8
		bool IEnumerator.c()
		{
			if (this.b == -2)
			{
				return false;
			}
			bool result;
			try
			{
				this.a.a(this.b);
				this.d = this.c.ie(this.b);
				this.b = this.c.ih(this.b);
				result = true;
			}
			catch (IOException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00071B3C File Offset: 0x00070B3C
		public void Dispose()
		{
		}

		// Token: 0x04001275 RID: 4725
		private d7 a;

		// Token: 0x04001276 RID: 4726
		private int b;

		// Token: 0x04001277 RID: 4727
		private e9 c;

		// Token: 0x04001278 RID: 4728
		private he d;
	}
}
