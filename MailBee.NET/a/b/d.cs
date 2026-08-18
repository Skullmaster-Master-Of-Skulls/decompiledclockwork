using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x02000297 RID: 663
	internal class d : g7
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x0006A16C File Offset: 0x0006916C
		public d()
		{
			this.a = true;
			this.b = null;
			this.c = -1L;
			this.b = new ArrayList();
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0006A19C File Offset: 0x0006919C
		public d(g7 A_0)
		{
			this.a(A_0.e());
			em[] array = A_0.aj();
			fu[] array2 = new fu[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new fu(array[i]);
			}
			this.a(array2);
			this.am(A_0.al());
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0006A1FE File Offset: 0x000691FE
		public new void a(ar A_0)
		{
			this.b = A_0;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0006A208 File Offset: 0x00069208
		public new void a(byte[] A_0)
		{
			ar ar = base.e();
			if (ar == null)
			{
				ar = new ar();
				this.a(ar);
			}
			ar.a(A_0);
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0006A234 File Offset: 0x00069234
		public new void a(em[] A_0)
		{
			this.e = A_0;
			this.b = new ArrayList();
			for (int i = 0; i < A_0.Length; i++)
			{
				this.b.Add(A_0[i]);
			}
			this.a = true;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x0006A277 File Offset: 0x00069277
		public new void a(int A_0, string A_1)
		{
			this.a(A_0, 31L, A_1);
			this.a = true;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0006A28B File Offset: 0x0006928B
		public new void a(int A_0, int A_1)
		{
			this.a(A_0, 3L, A_1);
			this.a = true;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0006A2A3 File Offset: 0x000692A3
		public new void a(int A_0, long A_1)
		{
			this.a(A_0, 20L, A_1);
			this.a = true;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0006A2BC File Offset: 0x000692BC
		public new void b(int A_0, bool A_1)
		{
			this.a(A_0, 11L, A_1);
			this.a = true;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0006A2D8 File Offset: 0x000692D8
		public new void a(int A_0, long A_1, object A_2)
		{
			fu fu = new fu();
			fu.a((long)A_0);
			fu.b(A_1);
			fu.b(A_2);
			this.a(fu);
			this.a = true;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0006A310 File Offset: 0x00069310
		public new void a(em A_0)
		{
			long a_ = A_0.e();
			this.a(a_);
			this.b.Add(A_0);
			this.a = true;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0006A340 File Offset: 0x00069340
		public new void a(long A_0)
		{
			IEnumerator enumerator = this.b.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((em)enumerator.Current).e() == A_0)
				{
					this.b.Remove(enumerator.Current);
					break;
				}
			}
			this.a = true;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0006A390 File Offset: 0x00069390
		protected new void a(int A_0, bool A_1)
		{
			this.a(A_0, 11L, A_1);
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0006A3A4 File Offset: 0x000693A4
		public override int ah()
		{
			if (this.a)
			{
				try
				{
					this.d = this.b();
					this.a = false;
				}
				catch (Exception)
				{
					throw;
				}
			}
			return this.d;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0006A3E8 File Offset: 0x000693E8
		private new int b()
		{
			int result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				this.a(memoryStream);
				this.c = a8.a(memoryStream.ToArray());
				result = this.c.Length;
			}
			return result;
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0006A43C File Offset: 0x0006943C
		public new int a(Stream A_0)
		{
			if (!this.a && this.c != null)
			{
				A_0.Write(this.c, 0, this.c.Length);
				return this.c.Length;
			}
			int result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					int num = 0;
					num += 8 + this.ai() * 2 * 4;
					int num2 = -1;
					if (this.ak(0L) != null)
					{
						object obj = this.ak(1L);
						if (obj != null)
						{
							if (!(obj is int))
							{
								throw new IllegalPropertySetDataException("The codepage property (ID = 1) must be an Integer object.");
							}
						}
						else
						{
							this.a(1, 2L, 1200);
						}
						num2 = this.a();
					}
					this.b.Sort(new d.a());
					for (int i = 0; i < this.b.Count; i++)
					{
						fu fu = (fu)this.b[i];
						bool flag = fu.e() != 0L;
						h7.a(memoryStream2, (uint)fu.e());
						h7.a(memoryStream2, (uint)num);
						if (flag)
						{
							num += fu.a(memoryStream, this.a());
						}
						else
						{
							if (num2 == -1)
							{
								throw new IllegalPropertySetDataException("Codepage (property 1) is undefined.");
							}
							num += global::a.b.d.a(memoryStream, this.a, num2);
						}
					}
					memoryStream.Flush();
					memoryStream2.Flush();
					byte[] array = memoryStream2.ToArray();
					byte[] array2 = memoryStream.ToArray();
					h7.b(A_0, 8 + array.Length + array2.Length);
					h7.b(A_0, this.ai());
					A_0.Write(array, 0, array.Length);
					A_0.Write(array2, 0, array2.Length);
					result = 8 + array.Length + array2.Length;
				}
			}
			return result;
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0006A61C File Offset: 0x0006961C
		private new static int a(Stream A_0, IDictionary A_1, int A_2)
		{
			int num = h7.a(A_0, (uint)A_1.Count);
			foreach (object value in A_1.Keys)
			{
				long num2 = Convert.ToInt64(value, CultureInfo.InvariantCulture);
				string text = (string)A_1[num2];
				if (text == null)
				{
					text = (string)A_1[(int)num2];
				}
				if (A_2 == 1200)
				{
					int i = text.Length + 1;
					if (i % 2 == 1)
					{
						i++;
					}
					num += h7.a(A_0, (uint)num2);
					num += h7.a(A_0, (uint)i);
					byte[] bytes = Encoding.GetEncoding(A_2).GetBytes(text);
					for (int j = 0; j < bytes.Length; j++)
					{
						A_0.WriteByte(bytes[j]);
						num++;
					}
					for (i -= text.Length; i > 0; i--)
					{
						A_0.WriteByte(0);
						A_0.WriteByte(0);
						num += 2;
					}
				}
				else
				{
					num += h7.a(A_0, (uint)num2);
					num += h7.a(A_0, (uint)(text.Length + 1));
					try
					{
						byte[] bytes2 = Encoding.GetEncoding(A_2).GetBytes(text);
						for (int k = 0; k < bytes2.Length; k++)
						{
							A_0.WriteByte(bytes2[k]);
							num++;
						}
					}
					catch (Exception a_)
					{
						throw new IllegalPropertySetDataException(a_);
					}
					A_0.WriteByte(0);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0006A790 File Offset: 0x00069790
		public override int ai()
		{
			return this.b.Count;
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0006A79D File Offset: 0x0006979D
		public override em[] aj()
		{
			this.c();
			return this.e;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x0006A7AB File Offset: 0x000697AB
		public new void c()
		{
			this.e = (em[])this.b.ToArray(typeof(em));
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0006A7CD File Offset: 0x000697CD
		public override object ak(long A_0)
		{
			this.c();
			return base.ak(A_0);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0006A7DC File Offset: 0x000697DC
		public override IDictionary al()
		{
			return this.a;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0006A7E4 File Offset: 0x000697E4
		public override void am(IDictionary A_0)
		{
			if (A_0 != null)
			{
				IEnumerator enumerator = A_0.Keys.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!(enumerator.Current is long) && !(enumerator.Current is int))
					{
						throw new IllegalPropertySetDataException(string.Concat(new object[]
						{
							"Dictionary keys must be of type long. but it's ",
							enumerator.Current,
							",",
							enumerator.Current.GetType().Name,
							" now"
						}));
					}
				}
				this.a = A_0;
				this.a(0, -1L, A_0);
				if (this.ak(1L) == null)
				{
					this.a(1, 2L, 1200);
					return;
				}
			}
			else
			{
				this.a(0L);
			}
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0006A8A4 File Offset: 0x000698A4
		public new void a(int A_0, object A_1)
		{
			if (A_1 is string)
			{
				this.a(A_0, (string)A_1);
				return;
			}
			if (A_1 is long)
			{
				this.a(A_0, (long)A_1);
				return;
			}
			if (A_1 is int)
			{
				this.a(A_0, A_1);
				return;
			}
			if (A_1 is short)
			{
				this.a(A_0, (int)((short)A_1));
				return;
			}
			if (A_1 is bool)
			{
				this.b(A_0, (bool)A_1);
				return;
			}
			if (A_1 is DateTime)
			{
				this.a(A_0, 64L, A_1);
				return;
			}
			throw new HPSFRuntimeException("HPSF does not support properties of type " + A_1.GetType().Name + ".");
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0006A950 File Offset: 0x00069950
		public new void d()
		{
			foreach (em em in this.aj())
			{
				this.a(em.e());
			}
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0006A982 File Offset: 0x00069982
		public new int a()
		{
			return base.a();
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0006A98A File Offset: 0x0006998A
		public new void a(int A_0)
		{
			this.a(1, 2L, A_0);
		}

		// Token: 0x04001156 RID: 4438
		private new bool a = true;

		// Token: 0x04001157 RID: 4439
		private new ArrayList b;

		// Token: 0x04001158 RID: 4440
		private new byte[] c;

		// Token: 0x02000299 RID: 665
		private new class a : IComparer
		{
			// Token: 0x06001776 RID: 6006 RVA: 0x0006B038 File Offset: 0x0006A038
			int IComparer.a(object A_0, object A_1)
			{
				em em = (em)A_0;
				em em2 = (em)A_1;
				if (em.e() < em2.e())
				{
					return -1;
				}
				if (em.e() == em2.e())
				{
					return 0;
				}
				return 1;
			}
		}
	}
}
