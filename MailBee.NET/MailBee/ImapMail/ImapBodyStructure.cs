using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using a;
using a.i;

namespace MailBee.ImapMail
{
	// Token: 0x0200017B RID: 379
	public class ImapBodyStructure
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x00034EA4 File Offset: 0x00033EA4
		internal ImapBodyStructure(string A_0, string A_1, Encoding A_2, StringDictionary A_3, string A_4, string A_5, string A_6, string A_7, StringDictionary A_8, string A_9, string A_10, string A_11, int A_12, int A_13, string A_14, ImapBodyStructureCollection A_15, Envelope A_16, ImapBodyStructure A_17)
		{
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
			this.d = A_3;
			this.e = A_4;
			this.f = A_5;
			this.g = A_6;
			this.h = A_7;
			this.i = A_8;
			this.j = A_9;
			this.k = A_10;
			this.l = A_11;
			this.m = A_12;
			this.n = A_13;
			this.o = A_14;
			this.p = A_15;
			this.q = A_16;
			this.r = A_17;
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00034F44 File Offset: 0x00033F44
		public string ContentType
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x00034F4C File Offset: 0x00033F4C
		public string Charset
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x00034F54 File Offset: 0x00033F54
		internal Encoding CharsetEncoding
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x00034F5C File Offset: 0x00033F5C
		public StringDictionary BodyParams
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x00034F64 File Offset: 0x00033F64
		public string ContentID
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x00034F6C File Offset: 0x00033F6C
		public string Description
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x00034F74 File Offset: 0x00033F74
		public string MailEncodingName
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x00034F7C File Offset: 0x00033F7C
		public string Disposition
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x00034F84 File Offset: 0x00033F84
		public StringDictionary DispositionParams
		{
			get
			{
				return this.i;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x00034F8C File Offset: 0x00033F8C
		public string Filename
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x00034F94 File Offset: 0x00033F94
		public string SafeFilename
		{
			get
			{
				string a_ = (this.d != null && this.d.ContainsKey("name")) ? this.d["name"] : null;
				if (this.a != null)
				{
					string text = this.a;
				}
				else
				{
					string empty = string.Empty;
				}
				return global::a.i.k.a(this.j, a_, this.a, null);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x00034FF9 File Offset: 0x00033FF9
		public string Language
		{
			get
			{
				return this.k;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x00035001 File Offset: 0x00034001
		public string Location
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x00035009 File Offset: 0x00034009
		public int Size
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00035011 File Offset: 0x00034011
		public int TextLineCount
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x00035019 File Offset: 0x00034019
		public string PartID
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00035021 File Offset: 0x00034021
		public ImapBodyStructureCollection SubParts
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x00035029 File Offset: 0x00034029
		public ImapBodyStructure EncapsulatedBodyStructure
		{
			get
			{
				return this.r;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00035031 File Offset: 0x00034031
		public Envelope EncapsulatedEnvelope
		{
			get
			{
				return this.q;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00035039 File Offset: 0x00034039
		public bool IsMultipart
		{
			get
			{
				return this.p != null;
			}
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00035044 File Offset: 0x00034044
		public ImapBodyStructureCollection GetAllParts()
		{
			ImapBodyStructureCollection imapBodyStructureCollection = new ImapBodyStructureCollection();
			imapBodyStructureCollection.a(this);
			if (this.p != null)
			{
				this.a(this, imapBodyStructureCollection);
			}
			return imapBodyStructureCollection;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00035070 File Offset: 0x00034070
		private void a(ImapBodyStructure A_0, ImapBodyStructureCollection A_1)
		{
			foreach (object obj in A_0.p)
			{
				ImapBodyStructure imapBodyStructure = (ImapBodyStructure)obj;
				A_1.a(imapBodyStructure);
				if (imapBodyStructure.p != null)
				{
					this.a(imapBodyStructure, A_1);
				}
			}
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000350DC File Offset: 0x000340DC
		internal static ImapBodyStructure b(ArrayList A_0, Encoding A_1)
		{
			return ImapBodyStructure.a(A_0, string.Empty, A_1);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000350EC File Offset: 0x000340EC
		private static ImapBodyStructure a(ArrayList A_0, string A_1, Encoding A_2)
		{
			if (A_0 == null || A_0.Count < 2)
			{
				return null;
			}
			string text = null;
			if (A_0[0] is ao)
			{
				text = ((ao)A_0[0]).a(A_2);
			}
			string text2 = null;
			ImapBodyStructureCollection imapBodyStructureCollection = null;
			StringDictionary stringDictionary = null;
			string text3 = null;
			string a_ = null;
			string a_2 = null;
			string a_3 = null;
			int a_4 = 0;
			int a_5 = 0;
			Envelope a_6 = null;
			ImapBodyStructure imapBodyStructure = null;
			int num = 0;
			if (text == null)
			{
				if (!(A_0[0] is ArrayList))
				{
					return null;
				}
				text = "multipart";
				string str;
				if (A_1 == string.Empty || A_1[A_1.Length - 1] == '.')
				{
					str = A_1;
					A_1 += "TEXT";
				}
				else
				{
					str = A_1 + ".";
				}
				imapBodyStructureCollection = new ImapBodyStructureCollection();
				int num2 = 1;
				while (num < A_0.Count && A_0[num] is ArrayList)
				{
					ImapBodyStructure imapBodyStructure2 = ImapBodyStructure.a(A_0[num] as ArrayList, str + num2.ToString(), A_2);
					if (imapBodyStructure2 == null)
					{
						return null;
					}
					if (text3 == null)
					{
						text3 = imapBodyStructure2.Charset;
						if (text3 != null)
						{
							A_2 = imapBodyStructure2.CharsetEncoding;
						}
					}
					imapBodyStructureCollection.a(imapBodyStructure2);
					num++;
					num2++;
				}
				if (A_0.Count > num)
				{
					try
					{
						text2 = ((ao)A_0[num]).a(A_2);
					}
					catch
					{
						return null;
					}
					text2 = text2.ToLower();
					num++;
				}
				if (A_0.Count > num)
				{
					ArrayList arrayList = A_0[num] as ArrayList;
					if (arrayList != null)
					{
						stringDictionary = ImapBodyStructure.a(arrayList, A_2);
					}
				}
				num++;
			}
			else
			{
				if (A_0.Count < 7)
				{
					return null;
				}
				text = text.ToLower();
				try
				{
					text2 = ((ao)A_0[1]).a(A_2);
				}
				catch
				{
					return null;
				}
				text2 = text2.ToLower();
				ArrayList arrayList2 = A_0[2] as ArrayList;
				if (arrayList2 != null)
				{
					stringDictionary = ImapBodyStructure.a(arrayList2, A_2);
					if (stringDictionary != null)
					{
						text3 = stringDictionary["charset"];
						if (text3 != null)
						{
							try
							{
								A_2 = global::a.i.h.a(Encoding.GetEncoding(text3), A_2);
							}
							catch
							{
							}
						}
					}
				}
				if (A_0[3] != null)
				{
					try
					{
						a_ = ((ao)A_0[3]).a(A_2);
					}
					catch
					{
						return null;
					}
				}
				if (A_0[4] != null)
				{
					try
					{
						a_2 = ((ao)A_0[4]).a(A_2);
					}
					catch
					{
						return null;
					}
				}
				if (A_0[5] != null)
				{
					try
					{
						a_3 = ((ao)A_0[5]).a(A_2);
					}
					catch
					{
						return null;
					}
				}
				try
				{
					a_4 = int.Parse(((ao)A_0[6]).a(Encoding.ASCII));
				}
				catch
				{
					a_4 = -1;
				}
				if (A_1 == string.Empty || A_1[A_1.Length - 1] == '.')
				{
					A_1 += "1";
				}
				num = 7;
				if (text == "text")
				{
					if (A_0.Count > num)
					{
						try
						{
							a_5 = int.Parse(((ao)A_0[num]).a(Encoding.ASCII));
							goto IL_31A;
						}
						catch
						{
							a_5 = -1;
							goto IL_31A;
						}
					}
					a_5 = -1;
					IL_31A:
					num++;
				}
				else if (text == "message" && text2 == "rfc822")
				{
					if (A_0.Count > num + 2)
					{
						imapBodyStructure = ImapBodyStructure.a(A_0[num + 1] as ArrayList, A_1 + ".", A_2);
						a_6 = Envelope.c(A_0[num] as ArrayList, (imapBodyStructure == null) ? A_2 : imapBodyStructure.CharsetEncoding);
						try
						{
							a_5 = int.Parse(((ao)A_0[num + 2]).a(Encoding.ASCII));
							goto IL_3C0;
						}
						catch
						{
							a_5 = -1;
							goto IL_3C0;
						}
					}
					a_5 = -1;
					IL_3C0:
					num += 3;
				}
				num++;
			}
			string a_7 = text + "/" + text2;
			string a_8 = null;
			StringDictionary stringDictionary2 = null;
			string text4 = null;
			if (A_0.Count > num)
			{
				ArrayList arrayList3 = A_0[num] as ArrayList;
				if (arrayList3 != null && arrayList3.Count > 1)
				{
					if (arrayList3[0] != null)
					{
						try
						{
							a_8 = ((ao)arrayList3[0]).a(A_2);
						}
						catch
						{
							return null;
						}
					}
					ArrayList arrayList4 = arrayList3[1] as ArrayList;
					if (arrayList4 != null)
					{
						stringDictionary2 = ImapBodyStructure.a(arrayList4, A_2);
						if (stringDictionary2 != null)
						{
							text4 = stringDictionary2["filename"];
							if (text4 == null)
							{
								text4 = stringDictionary2["filename*"];
								if (text4 != null)
								{
									global::a.i.j j = global::a.i.j.a("filename*=" + text4, A_2);
									if (j.b("filename") != null)
									{
										text4 = j.b("filename").c();
									}
								}
							}
							if (text4 != null)
							{
								text4 = global::a.i.h.c(text4);
							}
						}
					}
				}
				num++;
			}
			string a_9 = null;
			if (A_0.Count > num)
			{
				if (A_0[num] != null)
				{
					if (A_0[num] is ArrayList)
					{
						ArrayList arrayList5 = (ArrayList)A_0[num];
						string[] array = new string[arrayList5.Count];
						try
						{
							for (int i = 0; i < arrayList5.Count; i++)
							{
								array[i] = ((ao)arrayList5[i]).a(A_2);
							}
							a_9 = string.Join(",", array);
							goto IL_56F;
						}
						catch
						{
							goto IL_56F;
						}
					}
					try
					{
						a_9 = ((ao)A_0[num]).a(A_2);
					}
					catch
					{
					}
				}
				IL_56F:
				num++;
			}
			string a_10 = null;
			if (A_0.Count > num)
			{
				if (A_0[num] != null)
				{
					try
					{
						a_10 = ((ao)A_0[num]).a(A_2);
					}
					catch
					{
					}
				}
				num++;
			}
			return new ImapBodyStructure(a_7, text3, A_2, stringDictionary, a_, a_2, a_3, a_8, stringDictionary2, text4, a_9, a_10, a_4, a_5, A_1, imapBodyStructureCollection, a_6, imapBodyStructure);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00035770 File Offset: 0x00034770
		private static StringDictionary a(ArrayList A_0, Encoding A_1)
		{
			if (A_0.Count % 2 != 0)
			{
				return null;
			}
			StringDictionary stringDictionary = new StringDictionary();
			for (int i = 0; i < A_0.Count; i += 2)
			{
				try
				{
					stringDictionary.Add(((ao)A_0[i]).a(A_1).ToLower(), ((ao)A_0[i + 1]).a(A_1));
				}
				catch
				{
				}
			}
			return stringDictionary;
		}

		// Token: 0x040008F0 RID: 2288
		private string a;

		// Token: 0x040008F1 RID: 2289
		private string b;

		// Token: 0x040008F2 RID: 2290
		private Encoding c;

		// Token: 0x040008F3 RID: 2291
		private StringDictionary d;

		// Token: 0x040008F4 RID: 2292
		private string e;

		// Token: 0x040008F5 RID: 2293
		private string f;

		// Token: 0x040008F6 RID: 2294
		private string g;

		// Token: 0x040008F7 RID: 2295
		private string h;

		// Token: 0x040008F8 RID: 2296
		private StringDictionary i;

		// Token: 0x040008F9 RID: 2297
		private string j;

		// Token: 0x040008FA RID: 2298
		private string k;

		// Token: 0x040008FB RID: 2299
		private string l;

		// Token: 0x040008FC RID: 2300
		private int m;

		// Token: 0x040008FD RID: 2301
		private int n;

		// Token: 0x040008FE RID: 2302
		private string o;

		// Token: 0x040008FF RID: 2303
		private ImapBodyStructureCollection p;

		// Token: 0x04000900 RID: 2304
		private Envelope q;

		// Token: 0x04000901 RID: 2305
		private ImapBodyStructure r;
	}
}
