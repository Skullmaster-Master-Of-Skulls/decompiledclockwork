using System;
using System.Collections;
using System.IO;
using System.Text;

namespace a.h
{
	// Token: 0x020001F9 RID: 505
	internal class b
	{
		// Token: 0x06001033 RID: 4147 RVA: 0x000449CB File Offset: 0x000439CB
		public ArrayList c()
		{
			return this.a;
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x000449D3 File Offset: 0x000439D3
		public void a(ArrayList A_0)
		{
			this.a = A_0;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x000449DC File Offset: 0x000439DC
		public string d()
		{
			if (this.b == null)
			{
				try
				{
					if (this.d != null)
					{
						this.b = (string)this.d.a(14087);
						if (this.b == null)
						{
							this.b = (string)this.d.a(14084);
						}
					}
					if (this.b == null)
					{
						m m = this.a(36865);
						if (m != null)
						{
							this.b = (string)m.g();
						}
					}
					if (this.b == null)
					{
						m m2 = this.a(32784);
						if (m2 != null)
						{
							this.b = (string)m2.g();
						}
					}
				}
				catch (IOException)
				{
					this.b = null;
				}
			}
			return this.b;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x00044AAC File Offset: 0x00043AAC
		public void b(string A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00044AB5 File Offset: 0x00043AB5
		public n a()
		{
			return this.c;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x00044AC0 File Offset: 0x00043AC0
		public void a(n A_0)
		{
			if (this.c != null)
			{
				try
				{
					this.c.Close();
				}
				catch (IOException)
				{
				}
			}
			this.c = A_0;
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x00044AFC File Offset: 0x00043AFC
		public i f()
		{
			return this.e;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00044B04 File Offset: 0x00043B04
		public void a(i A_0)
		{
			this.e = A_0;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00044B0D File Offset: 0x00043B0D
		public b()
		{
			this.a = new ArrayList();
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00044B20 File Offset: 0x00043B20
		public m a(int A_0)
		{
			return m.a(this.a, A_0);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00044B2E File Offset: 0x00043B2E
		public void a(m A_0)
		{
			this.a.Add(A_0);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00044B40 File Offset: 0x00043B40
		public void e()
		{
			for (int i = 0; i < this.a.Count; i++)
			{
				((m)this.a[i]).e();
			}
			if (this.c != null)
			{
				this.c.Close();
			}
			if (this.d != null)
			{
				this.d.b();
			}
			if (this.e != null)
			{
				this.e.a();
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00044BB4 File Offset: 0x00043BB4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Attachment:");
			for (int i = 0; i < this.a.Count; i++)
			{
				stringBuilder.Append("\n  ").Append(this.a[i]);
			}
			if (this.a() != null)
			{
				stringBuilder.Append("\n  data=").Append(this.a());
			}
			if (this.b() != null)
			{
				g[] array = this.b().c();
				stringBuilder.Append("\n  MAPIProps=");
				for (int j = 0; j < array.Length; j++)
				{
					stringBuilder.Append("\n    ").Append(array[j]);
				}
			}
			if (this.f() != null)
			{
				stringBuilder.Append("\n  Nested Message:").Append(this.f());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00044C8C File Offset: 0x00043C8C
		public h b()
		{
			return this.d;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00044C94 File Offset: 0x00043C94
		public void a(h A_0)
		{
			this.d = A_0;
			if (this.d != null)
			{
				g g = A_0.b(14081);
				if (g != null && g.b() > 0)
				{
					a a = g.e()[0];
					if (a != null)
					{
						n n = a.a();
						if (g.f() == 13)
						{
							n.b(16);
						}
						this.c = n;
						object obj = a.f();
						if (obj is k)
						{
							k k = (k)obj;
							try
							{
								k.a(true);
								this.e = new i(k);
								return;
							}
							finally
							{
								k.d();
							}
						}
						if (obj is n)
						{
							((n)obj).Close();
						}
					}
				}
			}
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00044D50 File Offset: 0x00043D50
		public void a(string A_0)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(A_0, FileMode.Create);
				this.a(fileStream);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00044D8C File Offset: 0x00043D8C
		public void a(Stream A_0)
		{
			if (this.c == null)
			{
				return;
			}
			n n = new n(this.c);
			try
			{
				byte[] array = new byte[4096];
				int count;
				while ((count = n.Read(array, 0, array.Length)) != 0)
				{
					A_0.Write(array, 0, count);
				}
			}
			finally
			{
				n.Close();
			}
		}

		// Token: 0x04000BEA RID: 3050
		private ArrayList a;

		// Token: 0x04000BEB RID: 3051
		private string b;

		// Token: 0x04000BEC RID: 3052
		private n c;

		// Token: 0x04000BED RID: 3053
		private h d;

		// Token: 0x04000BEE RID: 3054
		private i e;
	}
}
