using System;
using System.Globalization;
using System.IO;
using System.Text;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x0200028F RID: 655
	internal class f9 : df
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x00069596 File Offset: 0x00068596
		public f9(df A_0) : base(A_0)
		{
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0006959F File Offset: 0x0006859F
		public f9(POIDocument A_0) : base(A_0)
		{
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x000695A8 File Offset: 0x000685A8
		public f9(POIFSFileSystem A_0) : base(new f9.a(A_0))
		{
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000695B6 File Offset: 0x000685B6
		public f9(h0 A_0) : base(new f9.a(A_0))
		{
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000695C4 File Offset: 0x000685C4
		public new string a()
		{
			DocumentSummaryInformation documentSummaryInformation = this.a.DocumentSummaryInformation;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(f9.a(documentSummaryInformation));
			m m = (documentSummaryInformation == null) ? null : documentSummaryInformation.CustomProperties;
			if (m != null)
			{
				foreach (object obj in m.c())
				{
					string text = obj.ToString();
					string str = f9.a(m.a(text));
					stringBuilder.Append(text + " = " + str + "\n");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00069652 File Offset: 0x00068652
		public string b()
		{
			return f9.a(this.a.SummaryInformation);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00069664 File Offset: 0x00068664
		private new static string a(SpecialPropertySet A_0)
		{
			if (A_0 == null)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			ch ch = A_0.PropertySetIDMap;
			em[] array = A_0.Properties;
			for (int i = 0; i < array.Length; i++)
			{
				string str = array[i].e().ToString(CultureInfo.InvariantCulture);
				object obj = ch.a(array[i].e());
				if (obj != null)
				{
					str = obj.ToString();
				}
				string str2 = f9.a(array[i].c());
				stringBuilder.Append(str + " = " + str2 + "\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00069700 File Offset: 0x00068700
		private new static string a(object A_0)
		{
			if (A_0 == null)
			{
				return "(not set)";
			}
			if (!(A_0 is byte[]))
			{
				return A_0.ToString();
			}
			byte[] array = (byte[])A_0;
			if (array.Length == 0)
			{
				return "";
			}
			if (array.Length == 1)
			{
				return array[0].ToString(CultureInfo.InvariantCulture);
			}
			if (array.Length == 2)
			{
				return p.g(array).ToString(CultureInfo.InvariantCulture);
			}
			if (array.Length == 4)
			{
				return p.e(array).ToString(CultureInfo.InvariantCulture);
			}
			return array.ToString();
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00069788 File Offset: 0x00068788
		public override string k5()
		{
			return this.b() + this.a();
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0006979B File Offset: 0x0006879B
		public override df im()
		{
			throw new InvalidOperationException("You already have the Metadata Text Extractor, not recursing!");
		}

		// Token: 0x02000291 RID: 657
		private new class a : POIDocument
		{
			// Token: 0x0600171B RID: 5915 RVA: 0x000697CA File Offset: 0x000687CA
			public a(h0 A_0) : base(A_0.m())
			{
			}

			// Token: 0x0600171C RID: 5916 RVA: 0x000697D8 File Offset: 0x000687D8
			public a(POIFSFileSystem A_0) : base(A_0)
			{
			}

			// Token: 0x0600171D RID: 5917 RVA: 0x000697E1 File Offset: 0x000687E1
			public override void kp(Stream A_0)
			{
				throw new InvalidOperationException("Unable to write, only for properties!");
			}
		}
	}
}
