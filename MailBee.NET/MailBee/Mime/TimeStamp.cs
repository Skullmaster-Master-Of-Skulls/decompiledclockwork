using System;
using System.Globalization;
using System.Text.RegularExpressions;
using a.i;

namespace MailBee.Mime
{
	// Token: 0x0200056B RID: 1387
	public class TimeStamp
	{
		// Token: 0x06002E15 RID: 11797 RVA: 0x000DE0D8 File Offset: 0x000DD0D8
		internal TimeStamp()
		{
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x000DE12D File Offset: 0x000DD12D
		public int Bias
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000DE135 File Offset: 0x000DD135
		public string By
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x000DE140 File Offset: 0x000DD140
		public DateTime Date
		{
			get
			{
				g a_ = global::a.i.g.a;
				if (this.i != null && this.i.ParentCollection != null && this.i.ParentCollection.MimePart != null && this.i.ParentCollection.MimePart.ParentMessage != null && this.i.ParentCollection.MimePart.ParentMessage.Parser != null && this.i.ParentCollection.MimePart.ParentMessage.Parser.DatesAsUtc)
				{
					a_ = global::a.i.g.b;
				}
				return k.a(this.c, a_);
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000DE1D8 File Offset: 0x000DD1D8
		public string DateAsString
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x000DE1E0 File Offset: 0x000DD1E0
		public string For
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000DE1E8 File Offset: 0x000DD1E8
		public string From
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000DE1F0 File Offset: 0x000DD1F0
		public string IP
		{
			get
			{
				if (this.f == null)
				{
					if (this.e != null && this.e != string.Empty)
					{
						this.f = TimeStamp.a(this.e);
					}
					if (this.f == null)
					{
						this.f = string.Empty;
					}
				}
				return this.f;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000DE249 File Offset: 0x000DD249
		public string ID
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000DE251 File Offset: 0x000DD251
		public string With
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000DE25C File Offset: 0x000DD25C
		internal static TimeStamp a(string A_0, Header A_1)
		{
			TimeStamp timeStamp = new TimeStamp();
			timeStamp.i = A_1;
			char[] trimChars = new char[]
			{
				' ',
				'\t',
				'\r',
				'\n',
				';'
			};
			Match match = m.a.a.Match(A_0);
			if (match.Groups["date"].Value.Length > 0)
			{
				timeStamp.c = match.Groups["date"].Value.Trim(trimChars);
				A_0 = A_0.Substring(0, match.Groups["date"].Index) + A_0.Substring(match.Groups["date"].Index + match.Groups["date"].Length);
			}
			string[] array = m.a.b.Split(A_0);
			for (int i = 0; i < array.Length - 1; i++)
			{
				string text = array[i].Trim();
				if (text.Length != 0)
				{
					string text2 = text.ToLower();
					string text3 = array[i + 1].Trim(trimChars);
					if (text2.StartsWith("("))
					{
						text2 = text2.Substring(1);
						if (text3.EndsWith(")"))
						{
							text3 = text3.Substring(0, text3.Length - 1);
						}
					}
					if (!(text2 == "from"))
					{
						if (!(text2 == "by"))
						{
							if (!(text2 == "via"))
							{
								if (!(text2 == "with"))
								{
									if (!(text2 == "id"))
									{
										if (text2 == "for")
										{
											timeStamp.d = text3;
										}
									}
									else
									{
										timeStamp.g = text3;
									}
								}
								else
								{
									timeStamp.h = text3;
								}
							}
						}
						else
						{
							timeStamp.b = text3;
						}
					}
					else
					{
						timeStamp.e = text3;
					}
				}
			}
			match = m.a.c.Match(timeStamp.c);
			if (match.Success)
			{
				try
				{
					timeStamp.a = int.Parse(match.Value.Remove(3, 2), CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
				}
			}
			return timeStamp;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000DE488 File Offset: 0x000DD488
		internal static string a(string A_0)
		{
			Match match = new Regex("\\b\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\b", RegexOptions.Compiled | RegexOptions.Singleline).Match(A_0);
			if (match != null)
			{
				return match.Value;
			}
			return null;
		}

		// Token: 0x04001FB3 RID: 8115
		private int a;

		// Token: 0x04001FB4 RID: 8116
		private string b = string.Empty;

		// Token: 0x04001FB5 RID: 8117
		private string c = string.Empty;

		// Token: 0x04001FB6 RID: 8118
		private string d = string.Empty;

		// Token: 0x04001FB7 RID: 8119
		private string e = string.Empty;

		// Token: 0x04001FB8 RID: 8120
		private string f;

		// Token: 0x04001FB9 RID: 8121
		private string g = string.Empty;

		// Token: 0x04001FBA RID: 8122
		private string h = string.Empty;

		// Token: 0x04001FBB RID: 8123
		private Header i;
	}
}
