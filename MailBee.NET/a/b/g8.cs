using System;
using System.Collections.Generic;
using MailBee;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002E3 RID: 739
	internal class g8 : ed, bw
	{
		// Token: 0x06001A15 RID: 6677 RVA: 0x0007350C File Offset: 0x0007250C
		public g8(string A_0)
		{
			this.a = new List<ed>();
			this.b = new List<string>();
			base.a(A_0);
			this.oo(0);
			base.b(1);
			base.c(0);
			base.a(1);
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x00073558 File Offset: 0x00072558
		public g8(int A_0, byte[] A_1, int A_2) : base(A_0, A_1, A_2)
		{
			this.a = new List<ed>();
			this.b = new List<string>();
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0007357C File Offset: 0x0007257C
		public bool a(ed A_0, string A_1)
		{
			string text = A_0.f();
			A_0.a(A_1);
			string item = A_0.f();
			bool result;
			if (this.b.Contains(item))
			{
				A_0.a(text);
				result = false;
			}
			else
			{
				this.b.Add(item);
				this.b.Remove(text);
				result = true;
			}
			return result;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000735D3 File Offset: 0x000725D3
		public bool a(ed A_0)
		{
			bool flag = this.a.Remove(A_0);
			if (flag)
			{
				this.b.Remove(A_0.f());
			}
			return flag;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000735F6 File Offset: 0x000725F6
		public override bool lj()
		{
			return true;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000735FC File Offset: 0x000725FC
		public override void lk()
		{
			if (this.a.Count > 0)
			{
				ed[] array = new ed[this.a.Count];
				this.a.CopyTo(array, 0);
				Array.Sort<ed>(array, new g8.a());
				int num = array.Length / 2;
				base.e(array[num].b());
				array[0].x(null);
				array[0].z(null);
				for (int i = 1; i < num; i++)
				{
					array[i].x(array[i - 1]);
					array[i].z(null);
				}
				if (num != 0)
				{
					array[num].x(array[num - 1]);
				}
				if (num != array.Length - 1)
				{
					array[num].z(array[num + 1]);
					for (int j = num + 1; j < array.Length - 1; j++)
					{
						array[j].x(null);
						array[j].z(array[j + 1]);
					}
					array[array.Length - 1].x(null);
					array[array.Length - 1].z(null);
					return;
				}
				array[num].z(null);
			}
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000736FC File Offset: 0x000726FC
		public IEnumerator<ed> om()
		{
			return this.a.GetEnumerator();
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x00073710 File Offset: 0x00072710
		public void on(ed A_0)
		{
			string text = A_0.f();
			if (this.b.Contains(text))
			{
				throw new MailBeeOutlookMsgBuildingException(string.Format(Resources.Instance.ErrorDesc_OleDocDuplicateName0, text), 1201);
			}
			this.b.Add(text);
			this.a.Add(A_0);
		}

		// Token: 0x040012A4 RID: 4772
		private new List<ed> a;

		// Token: 0x040012A5 RID: 4773
		private new List<string> b;

		// Token: 0x020002E6 RID: 742
		public new class a : IComparer<ed>
		{
			// Token: 0x06001A43 RID: 6723 RVA: 0x00073DD8 File Offset: 0x00072DD8
			public override bool Equals(object o)
			{
				return this == o;
			}

			// Token: 0x06001A44 RID: 6724 RVA: 0x00073DDE File Offset: 0x00072DDE
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06001A45 RID: 6725 RVA: 0x00073DE8 File Offset: 0x00072DE8
			public int Compare(ed o1, ed o2)
			{
				string value = "_VBA_PROJECT";
				string text = o1.f();
				string text2 = o2.f();
				int num = text.Length - text2.Length;
				if (num == 0)
				{
					if (text.Equals(value, StringComparison.CurrentCulture))
					{
						num = 1;
					}
					else if (text2.Equals(value, StringComparison.CurrentCulture))
					{
						num = -1;
					}
					else if (text.StartsWith("__", StringComparison.Ordinal) && text2.StartsWith("__", StringComparison.Ordinal))
					{
						num = string.Compare(text, text2, StringComparison.OrdinalIgnoreCase);
					}
					else if (text.StartsWith("__", StringComparison.Ordinal))
					{
						num = 1;
					}
					else if (text2.StartsWith("__", StringComparison.Ordinal))
					{
						num = -1;
					}
					else
					{
						num = string.Compare(text, text2, StringComparison.OrdinalIgnoreCase);
					}
				}
				return num;
			}
		}
	}
}
