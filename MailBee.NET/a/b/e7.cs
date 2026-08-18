using System;
using System.Collections.Generic;
using System.IO;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002F1 RID: 753
	internal class e7
	{
		// Token: 0x06001A90 RID: 6800 RVA: 0x00074A90 File Offset: 0x00073A90
		public e7(y A_0, int A_1, int[] A_2, int A_3, int A_4, dc A_5) : this(A_0)
		{
			e7.a(A_1);
			i4[] array = new i4[A_1];
			int num = Math.Min(A_1, A_2.Length);
			int i;
			for (i = 0; i < num; i++)
			{
				int num2 = A_2[i];
				if (num2 > A_5.fe())
				{
					throw new MailBeeOutlookMsgParsingException(string.Concat(new object[]
					{
						"Your file contains ",
						A_5.fe(),
						" sectors, but the initial DIFAT array at index ",
						i,
						" referenced block # ",
						num2,
						". This isn't allowed and  your file is corrupt"
					}), 1200);
				}
				array[i] = (i4)A_5.fb(num2);
			}
			if (i < A_1)
			{
				if (A_4 < 0)
				{
					throw new MailBeeOutlookMsgParsingException(Resources.Instance.ErrorDesc_OleDocBatCountExceedsLimit, 1200);
				}
				int num3 = A_4;
				int val = gx.c();
				int a_ = gx.b();
				for (int j = 0; j < A_3; j++)
				{
					num = Math.Min(A_1 - i, val);
					byte[] a_2 = A_5.fb(num3).bv();
					int num4 = 0;
					for (int k = 0; k < num; k++)
					{
						array[i++] = (i4)A_5.fb(p.i(a_2, num4));
						num4 += 4;
					}
					num3 = p.i(a_2, a_);
					if (num3 == -2)
					{
						break;
					}
				}
			}
			if (i != A_1)
			{
				throw new MailBeeOutlookMsgParsingException(Resources.Instance.ErrorDesc_OleDocCouldNotFindAllBlocks, 1200);
			}
			this.a(array, A_5);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00074C03 File Offset: 0x00073C03
		public e7(y A_0, bn[] A_1, dc A_2) : this(A_0)
		{
			this.a(A_1, A_2);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00074C14 File Offset: 0x00073C14
		public e7(y A_0)
		{
			this.d = A_0;
			this.c = new List<int>();
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00074C30 File Offset: 0x00073C30
		public bn[] a(int A_0, int A_1, dc A_2)
		{
			List<bn> list = new List<bn>();
			int num = A_0;
			bool flag = true;
			while (num != -2)
			{
				try
				{
					bn item = A_2.fb(num);
					list.Add(item);
					num = this.c[num];
					flag = false;
				}
				catch (Exception)
				{
					if (num == A_1)
					{
						e7.a.iv(5, "Warning, header block comes after data blocks in POIFS block listing");
						num = -2;
					}
					else
					{
						if (num != 0 || !flag)
						{
							throw;
						}
						e7.a.iv(5, "Warning, incorrectly terminated empty data blocks in POIFS block listing (should end at -2, ended at 0)");
						num = -2;
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00074CC4 File Offset: 0x00073CC4
		public bool b(int A_0)
		{
			bool result = false;
			try
			{
				result = (this.c[A_0] != -1);
			}
			catch (IndexOutOfRangeException)
			{
			}
			return result;
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00074CFC File Offset: 0x00073CFC
		public int c(int A_0)
		{
			if (this.b(A_0))
			{
				return this.c[A_0];
			}
			throw new IOException("index " + A_0 + " is unused");
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00074D30 File Offset: 0x00073D30
		private void a(bn[] A_0, dc A_1)
		{
			int num = this.d.e();
			for (int i = 0; i < A_0.Length; i++)
			{
				byte[] a_ = A_0[i].bv();
				int num2 = 0;
				for (int j = 0; j < num; j++)
				{
					int num3 = p.i(a_, num2);
					if (num3 == -1)
					{
						A_1.fa(this.c.Count);
					}
					this.c.Add(num3);
					num2 += 4;
				}
				A_0[i] = null;
			}
			A_1.fd(this);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00074DB0 File Offset: 0x00073DB0
		public static void a(int A_0)
		{
			if (A_0 <= 0)
			{
				throw new MailBeeOutlookMsgParsingException(string.Format(Resources.Instance.ErrorDesc_OleDocIllegalBlockCount0, A_0), 1200);
			}
			if (A_0 > 65535)
			{
				throw new MailBeeOutlookMsgParsingException(string.Concat(new object[]
				{
					"Block count ",
					A_0,
					" is too high. POI maximum is ",
					65535,
					"."
				}), 1200);
			}
		}

		// Token: 0x040012E6 RID: 4838
		private static dm a = gn.a(typeof(e7));

		// Token: 0x040012E7 RID: 4839
		private const int b = 65535;

		// Token: 0x040012E8 RID: 4840
		private List<int> c;

		// Token: 0x040012E9 RID: 4841
		private y d;
	}
}
