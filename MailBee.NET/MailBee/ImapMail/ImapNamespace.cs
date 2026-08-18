using System;
using System.Collections;
using System.Text;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x02000192 RID: 402
	public class ImapNamespace
	{
		// Token: 0x06000E6B RID: 3691 RVA: 0x00035AD6 File Offset: 0x00034AD6
		internal ImapNamespace(string A_0, string A_1, ArrayList A_2, bool A_3)
		{
			this.b = A_0;
			this.a = A_1;
			this.c = A_2;
			this.d = A_3;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00035AFB File Offset: 0x00034AFB
		public string Prefix
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00035B03 File Offset: 0x00034B03
		public string Delimiter
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00035B0B File Offset: 0x00034B0B
		public ArrayList AllValues
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00035B13 File Offset: 0x00034B13
		public bool IsValid
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00035B1C File Offset: 0x00034B1C
		internal static ArrayList b(ArrayList A_0, Encoding A_1)
		{
			if (A_0 != null)
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < A_0.Count; i++)
				{
					object value = A_0[i];
					if (A_0[i] is ao)
					{
						value = ((ao)A_0[i]).a(A_1);
					}
					else if (A_0[i] is ArrayList)
					{
						value = ImapNamespace.b((ArrayList)A_0[i], A_1);
					}
					arrayList.Add(value);
				}
				return arrayList;
			}
			return null;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00035B9C File Offset: 0x00034B9C
		internal static ImapNamespace a(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null || A_0.Count < 2)
			{
				return null;
			}
			ArrayList arrayList = ImapNamespace.b(A_0, A_1);
			string a_ = null;
			string a_2 = null;
			bool a_3 = true;
			if (arrayList[0] is string)
			{
				a_ = (string)arrayList[0];
			}
			else
			{
				a_3 = false;
			}
			if (arrayList[1] is string)
			{
				a_2 = (string)arrayList[1];
			}
			else
			{
				a_3 = false;
			}
			return new ImapNamespace(a_, a_2, arrayList, a_3);
		}

		// Token: 0x04000944 RID: 2372
		private string a;

		// Token: 0x04000945 RID: 2373
		private string b;

		// Token: 0x04000946 RID: 2374
		private ArrayList c;

		// Token: 0x04000947 RID: 2375
		private bool d;
	}
}
