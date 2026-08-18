using System;
using System.Collections;
using System.Text;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace a.c
{
	// Token: 0x0200023F RID: 575
	internal class r : j
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x00056C50 File Offset: 0x00055C50
		public r(s A_0) : base(A_0)
		{
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00056C74 File Offset: 0x00055C74
		public void a(XmlNode A_0, PdfPTable A_1, u A_2)
		{
			XmlNode xmlNode = A_0.Attributes["bgcolor"];
			Color color = null;
			if (xmlNode != null)
			{
				color = j.a(xmlNode.Value, null);
			}
			for (int i = 0; i < A_0.ChildNodes.Count; i++)
			{
				XmlNode xmlNode2 = A_0.ChildNodes[i];
				if (xmlNode2.Name.ToLower() == "td" || xmlNode2.Name.ToLower() == "th")
				{
					u u = new u(xmlNode2, A_2);
					u.c(u.n());
					if (color != null)
					{
						u.d(color);
					}
					b b = new b(this.b);
					if (A_0.ChildNodes.Count == this.c.Length)
					{
						b.a(this.c[i]);
					}
					b.an(xmlNode2, u);
					A_1.AddCell((PdfPCell)b.ao());
				}
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00056D6C File Offset: 0x00055D6C
		public int a(XmlNode A_0)
		{
			int num = 0;
			foreach (object obj in A_0.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name.ToLower() == "td" || xmlNode.Name.ToLower() == "th")
				{
					XmlNode xmlNode2 = xmlNode.Attributes["colspan"];
					if (xmlNode2 != null)
					{
						try
						{
							num += int.Parse(xmlNode2.Value);
							continue;
						}
						catch (Exception)
						{
							num++;
							continue;
						}
					}
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00056E30 File Offset: 0x00055E30
		public new int[] b(XmlNode A_0)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in A_0.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes == null)
				{
					break;
				}
				if (xmlNode.Attributes["colspan"] != null)
				{
					break;
				}
				XmlNode xmlNode2 = xmlNode.Attributes["width"];
				if (xmlNode2 != null)
				{
					try
					{
						arrayList.Add(int.Parse(xmlNode2.Value));
					}
					catch (Exception)
					{
						break;
					}
				}
			}
			return (int[])arrayList.ToArray(typeof(int));
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00056EF8 File Offset: 0x00055EF8
		public string c(XmlNode A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			foreach (object obj in A_0.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string value = "?";
				string text = "1";
				if (xmlNode.Attributes != null)
				{
					XmlNode xmlNode2 = xmlNode.Attributes["width"];
					if (xmlNode2 != null)
					{
						value = xmlNode2.Value.Trim();
						flag = true;
					}
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(value);
					XmlNode xmlNode3 = xmlNode.Attributes["colspan"];
					if (xmlNode3 != null)
					{
						text = xmlNode3.Value.Trim();
						if (text != "1")
						{
							flag = false;
						}
					}
					stringBuilder.Append(':');
					stringBuilder.Append(text);
				}
			}
			if (!flag)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00057008 File Offset: 0x00056008
		public new float[] b()
		{
			return this.b;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00057010 File Offset: 0x00056010
		public void a(float[] A_0)
		{
			this.b = A_0;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00057019 File Offset: 0x00056019
		public int[] c()
		{
			return this.c;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00057021 File Offset: 0x00056021
		public void a(int[] A_0)
		{
			this.c = A_0;
		}

		// Token: 0x04000F81 RID: 3969
		private new float[] b = new float[0];

		// Token: 0x04000F82 RID: 3970
		private int[] c = new int[0];
	}
}
