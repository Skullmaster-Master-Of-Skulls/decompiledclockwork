using System;
using System.Collections;
using System.Xml;
using iTextSharp.text.pdf;

namespace a.c
{
	// Token: 0x0200023D RID: 573
	internal class p : j
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x00055F10 File Offset: 0x00054F10
		public p(s A_0, u A_1) : this(A_0, A_1, null)
		{
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00055F1B File Offset: 0x00054F1B
		public p(s A_0, u A_1, string A_2) : base(A_0)
		{
			this.c = A_1;
			this.b = A_2;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00055F4C File Offset: 0x00054F4C
		public int d(XmlNode A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in A_0.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name.ToLower() == this.b.ToLower())
					{
						return this.c(xmlNode);
					}
				}
				return 0;
			}
			return this.c(A_0);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00055FD8 File Offset: 0x00054FD8
		public string f(XmlNode A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in A_0.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name.ToLower() == this.b.ToLower())
					{
						return this.a(xmlNode);
					}
				}
				return null;
			}
			return this.a(A_0);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00056064 File Offset: 0x00055064
		public new void b(string A_0)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			ArrayList arrayList = new ArrayList();
			string[] array = A_0.Split(new char[]
			{
				','
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					':'
				});
				int num4 = 1;
				if (array3[0] == "?")
				{
					try
					{
						num4 = int.Parse(array3[1]);
					}
					catch (Exception)
					{
					}
					num += num4;
				}
				else
				{
					int num5 = 0;
					try
					{
						num5 = int.Parse(array3[0]);
					}
					catch (Exception)
					{
					}
					num2 += num5;
				}
			}
			num3 = (int)this.c.k() - num2;
			array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array4 = array2[i].Split(new char[]
				{
					':'
				});
				int num6 = 1;
				try
				{
					num6 = int.Parse(array4[1]);
				}
				catch (Exception)
				{
				}
				int num7 = 0;
				if (array4[0] == "?")
				{
					num7 = num3 / num * num6;
				}
				else
				{
					try
					{
						num7 = int.Parse(array4[0]);
					}
					catch (Exception)
					{
					}
				}
				for (int j = 0; j < num6; j++)
				{
					arrayList.Add(num7 / num6);
				}
			}
			this.e = (int[])arrayList.ToArray(typeof(int));
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000561F8 File Offset: 0x000551F8
		public void a(string A_0)
		{
			A_0 = A_0.Replace("%", string.Empty);
			ArrayList arrayList = new ArrayList();
			string[] array = A_0.Split(new char[]
			{
				','
			});
			int num = 0;
			int num2 = 0;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new char[]
				{
					':'
				});
				if (array3[0] == "?")
				{
					int num3 = 1;
					try
					{
						num3 = int.Parse(array3[1]);
					}
					catch (Exception)
					{
					}
					num2 += num3;
				}
				else
				{
					try
					{
						num += int.Parse(array3[0]);
					}
					catch (Exception)
					{
					}
				}
			}
			int num4 = 0;
			if (num2 > 0)
			{
				num4 = (100 - num) / num2;
			}
			array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array4 = array2[i].Split(new char[]
				{
					':'
				});
				int num5 = 1;
				try
				{
					num5 = int.Parse(array4[1]);
				}
				catch (Exception)
				{
				}
				int num6 = 0;
				if (array4[0] == "?")
				{
					num6 = num4 * num5;
				}
				else
				{
					try
					{
						num6 = int.Parse(array4[0]);
					}
					catch (Exception)
					{
					}
				}
				for (int j = 0; j < num5; j++)
				{
					arrayList.Add((float)num6 / (float)num5 * 6f);
				}
			}
			this.d = (float[])arrayList.ToArray(typeof(float));
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00056398 File Offset: 0x00055398
		public new float[] b()
		{
			return this.d;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000563A0 File Offset: 0x000553A0
		public int[] c()
		{
			return this.e;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000563A8 File Offset: 0x000553A8
		public int[] e(XmlNode A_0)
		{
			if (this.b != null)
			{
				foreach (object obj in A_0.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Name.ToLower() == this.b.ToLower())
					{
						return this.b(xmlNode);
					}
				}
				return new int[0];
			}
			return this.b(A_0);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0005643C File Offset: 0x0005543C
		public new void b(XmlNode A_0, PdfPTable A_1, u A_2)
		{
			if (this.b != null)
			{
				using (IEnumerator enumerator = A_0.ChildNodes.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlNode xmlNode = (XmlNode)obj;
						if (xmlNode.Name.ToLower() == this.b.ToLower())
						{
							this.a(xmlNode, A_1, A_2);
						}
					}
					return;
				}
			}
			this.a(A_0, A_1, A_2);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x000564C8 File Offset: 0x000554C8
		private void a(XmlNode A_0, PdfPTable A_1, u A_2)
		{
			foreach (object obj in A_0.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name.ToLower() == "tr")
				{
					new u(xmlNode, A_2);
					r r = new r(this.b);
					r.a(this.d);
					r.a(this.e);
					r.a(xmlNode, A_1, A_2);
				}
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00056564 File Offset: 0x00055564
		private int c(XmlNode A_0)
		{
			foreach (object obj in A_0.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name.ToLower() == "tr")
				{
					int num = new r(this.b).a(xmlNode);
					if (num != 0)
					{
						return num;
					}
				}
			}
			return 0;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000565EC File Offset: 0x000555EC
		private new int[] b(XmlNode A_0)
		{
			foreach (object obj in A_0.ChildNodes)
			{
				XmlNode a_ = (XmlNode)obj;
				r r = new r(this.b);
				r.c(a_);
				int[] array = r.b(a_);
				if (array.Length != 0)
				{
					return array;
				}
			}
			return new int[0];
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0005666C File Offset: 0x0005566C
		private string a(XmlNode A_0)
		{
			foreach (object obj in A_0.ChildNodes)
			{
				XmlNode a_ = (XmlNode)obj;
				string text = new r(this.b).c(a_);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x04000F7B RID: 3963
		private new string b;

		// Token: 0x04000F7C RID: 3964
		private u c;

		// Token: 0x04000F7D RID: 3965
		private float[] d = new float[0];

		// Token: 0x04000F7E RID: 3966
		private int[] e = new int[0];
	}
}
