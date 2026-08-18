using System;
using System.Collections;
using System.Text;
using a;
using a.i;
using MailBee.Mime;

namespace MailBee.ImapMail
{
	// Token: 0x02000173 RID: 371
	public class Envelope
	{
		// Token: 0x06000C95 RID: 3221 RVA: 0x0003204C File Offset: 0x0003104C
		internal Envelope()
		{
			this.c = Global.DefaultEncoding;
			this.a = true;
			this.b = false;
			this.d = -1;
			this.o = DateTime.MinValue;
			this.p = null;
			this.q = null;
			this.r = null;
			this.s = null;
			this.t = null;
			this.u = null;
			this.v = null;
			this.w = null;
			this.x = null;
			this.y = false;
			this.e = new MessageFlagSet();
			this.f = DateTime.MinValue;
			this.g = -1;
			this.h = -1L;
			this.i = null;
			this.j = null;
			this.k = null;
			this.l = null;
			this.m = null;
			this.n = null;
			this.z = null;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x00032128 File Offset: 0x00031128
		internal Envelope(DateTime A_0, string A_1, EmailAddress A_2, EmailAddress A_3, EmailAddressCollection A_4, EmailAddressCollection A_5, EmailAddressCollection A_6, EmailAddressCollection A_7, string A_8, string A_9, bool A_10)
		{
			this.a = true;
			this.b = false;
			this.d = -1;
			this.o = A_0;
			this.p = A_1;
			this.q = A_2;
			this.r = A_3;
			this.s = A_4;
			this.t = A_5;
			this.u = A_6;
			this.v = A_7;
			this.w = A_8;
			this.x = A_9;
			this.y = A_10;
			this.e = new MessageFlagSet();
			this.f = DateTime.MinValue;
			this.g = -1;
			this.h = -1L;
			this.i = null;
			this.j = null;
			this.k = null;
			this.l = null;
			this.m = null;
			this.n = null;
			this.z = null;
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000321FB File Offset: 0x000311FB
		internal void a(Encoding A_0)
		{
			this.c = A_0;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00032204 File Offset: 0x00031204
		internal void a(int A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003220D File Offset: 0x0003120D
		internal void a(string A_0, string A_1, string[] A_2)
		{
			this.i = A_0;
			this.j = A_1;
			this.k = A_2;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00032224 File Offset: 0x00031224
		internal void a(MessageFlagSet A_0, DateTime A_1, int A_2, long A_3)
		{
			this.e = A_0;
			this.f = A_1;
			this.g = A_2;
			this.h = A_3;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00032243 File Offset: 0x00031243
		// (set) Token: 0x06000C9C RID: 3228 RVA: 0x0003224B File Offset: 0x0003124B
		public bool SafeMode
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00032254 File Offset: 0x00031254
		public int MessageNumber
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0003225C File Offset: 0x0003125C
		public MessageFlagSet Flags
		{
			get
			{
				if (this.a && this.e == null)
				{
					return new MessageFlagSet();
				}
				return this.e;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0003227A File Offset: 0x0003127A
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x00032282 File Offset: 0x00031282
		public bool DatesAsUtc
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x0003228B File Offset: 0x0003128B
		public DateTime DateReceived
		{
			get
			{
				if (this.f == DateTime.MinValue)
				{
					return DateTime.MinValue;
				}
				if (this.b)
				{
					return this.f;
				}
				return this.f.ToLocalTime();
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000322BF File Offset: 0x000312BF
		public int Size
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x000322C7 File Offset: 0x000312C7
		public long Uid
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x000322CF File Offset: 0x000312CF
		public string GmailMessageID
		{
			get
			{
				return this.i;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x000322D7 File Offset: 0x000312D7
		public string GmailThreadID
		{
			get
			{
				return this.j;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x000322DF File Offset: 0x000312DF
		public string[] GmailLabels
		{
			get
			{
				return this.k;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000322E8 File Offset: 0x000312E8
		public string[] GetUtf7DecodedGmailLabels()
		{
			if (this.k != null)
			{
				string[] array = new string[this.k.Length];
				for (int i = 0; i < this.k.Length; i++)
				{
					array[i] = ImapUtils.FromUtf7String(this.k[i]);
				}
				return array;
			}
			return null;
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00032331 File Offset: 0x00031331
		internal void a(ImapBodyStructure A_0)
		{
			this.l = A_0;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0003233A File Offset: 0x0003133A
		public ImapBodyStructure BodyStructure
		{
			get
			{
				return this.l;
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00032342 File Offset: 0x00031342
		internal void a(HeaderCollection A_0)
		{
			this.m = A_0;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x0003234B File Offset: 0x0003134B
		public HeaderCollection ExtraHeaders
		{
			get
			{
				return this.m;
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00032354 File Offset: 0x00031354
		public object GetEnvelopeItem(string name, bool stringsAsBytes)
		{
			if (name == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (this.z == null)
			{
				return null;
			}
			name = name.ToUpper();
			object obj = this.z[name];
			if (obj == null)
			{
				return null;
			}
			return this.a(obj, stringsAsBytes, this.c);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003239E File Offset: 0x0003139E
		internal void a(MailMessage A_0)
		{
			this.n = A_0;
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x000323A7 File Offset: 0x000313A7
		public MailMessage MessagePreview
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x000323AF File Offset: 0x000313AF
		public DateTime Date
		{
			get
			{
				if (this.o == DateTime.MinValue)
				{
					return DateTime.MinValue;
				}
				if (this.b)
				{
					return this.o;
				}
				return this.o.ToLocalTime();
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x000323E3 File Offset: 0x000313E3
		public string Subject
		{
			get
			{
				if (this.a && this.p == null)
				{
					return string.Empty;
				}
				return this.p;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x00032401 File Offset: 0x00031401
		public EmailAddress From
		{
			get
			{
				if (this.a && this.q == null)
				{
					return new EmailAddress();
				}
				return this.q;
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0003241F File Offset: 0x0003141F
		public EmailAddress Sender
		{
			get
			{
				if (this.a && this.r == null)
				{
					return new EmailAddress();
				}
				return this.r;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0003243D File Offset: 0x0003143D
		public EmailAddressCollection ReplyTo
		{
			get
			{
				if (this.a && this.s == null)
				{
					return new EmailAddressCollection();
				}
				return this.s;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0003245B File Offset: 0x0003145B
		public EmailAddressCollection To
		{
			get
			{
				if (this.a && this.t == null)
				{
					return new EmailAddressCollection();
				}
				return this.t;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x00032479 File Offset: 0x00031479
		public EmailAddressCollection Cc
		{
			get
			{
				if (this.a && this.u == null)
				{
					return new EmailAddressCollection();
				}
				return this.u;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00032497 File Offset: 0x00031497
		public EmailAddressCollection Bcc
		{
			get
			{
				if (this.a && this.v == null)
				{
					return new EmailAddressCollection();
				}
				return this.v;
			}
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000324B8 File Offset: 0x000314B8
		public EmailAddressCollection GetAllRecipients()
		{
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			if (this.t != null)
			{
				emailAddressCollection.Add(this.t);
			}
			if (this.u != null)
			{
				emailAddressCollection.Add(this.u);
			}
			if (this.v != null)
			{
				emailAddressCollection.Add(this.v);
			}
			return emailAddressCollection;
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x00032508 File Offset: 0x00031508
		public string InReplyTo
		{
			get
			{
				if (this.a && this.w == null)
				{
					return string.Empty;
				}
				return this.w;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00032526 File Offset: 0x00031526
		public string MessageID
		{
			get
			{
				if (this.a && this.x == null)
				{
					return string.Empty;
				}
				return this.x;
			}
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00032544 File Offset: 0x00031544
		internal void a(bool A_0)
		{
			this.y = A_0;
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0003254D File Offset: 0x0003154D
		public bool IsValid
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00032555 File Offset: 0x00031555
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0003255D File Offset: 0x0003155D
		internal Hashtable KeyValueList
		{
			get
			{
				return this.z;
			}
			set
			{
				this.z = value;
			}
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x00032568 File Offset: 0x00031568
		internal static Envelope c(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null || A_0.Count < 10)
			{
				return null;
			}
			bool a_ = true;
			DateTime a_2 = DateTime.MinValue;
			if (A_0[0] != null)
			{
				try
				{
					a_2 = global::a.i.k.a(((ao)A_0[0]).a(Encoding.GetEncoding(1252)), global::a.i.g.b);
				}
				catch
				{
					a_ = false;
				}
			}
			string a_3 = null;
			if (A_0[1] != null)
			{
				try
				{
					a_3 = global::a.i.h.c(((ao)A_0[1]).a(A_1));
				}
				catch
				{
					a_ = false;
				}
			}
			EmailAddressCollection emailAddressCollection;
			if (A_0[2] == null)
			{
				emailAddressCollection = null;
			}
			else
			{
				emailAddressCollection = Envelope.a(A_0[2] as ArrayList, A_1);
				if (emailAddressCollection == null)
				{
					a_ = false;
				}
			}
			EmailAddress a_4 = null;
			if (emailAddressCollection != null && emailAddressCollection.Count > 0)
			{
				a_4 = emailAddressCollection[0];
			}
			if (A_0[3] != null)
			{
				emailAddressCollection = Envelope.a(A_0[3] as ArrayList, A_1);
				if (emailAddressCollection == null)
				{
					a_ = false;
				}
			}
			EmailAddress a_5 = null;
			if (emailAddressCollection != null && emailAddressCollection.Count > 0)
			{
				a_5 = emailAddressCollection[0];
			}
			EmailAddressCollection emailAddressCollection2 = null;
			if (A_0[4] != null)
			{
				emailAddressCollection2 = Envelope.a(A_0[4] as ArrayList, A_1);
				if (emailAddressCollection2 == null)
				{
					a_ = false;
				}
			}
			EmailAddressCollection emailAddressCollection3 = null;
			if (A_0[5] != null)
			{
				emailAddressCollection3 = Envelope.a(A_0[5] as ArrayList, A_1);
				if (emailAddressCollection3 == null)
				{
					a_ = false;
				}
			}
			EmailAddressCollection emailAddressCollection4 = null;
			if (A_0[6] != null)
			{
				emailAddressCollection4 = Envelope.a(A_0[6] as ArrayList, A_1);
				if (emailAddressCollection4 == null)
				{
					a_ = false;
				}
			}
			EmailAddressCollection emailAddressCollection5 = null;
			if (A_0[7] != null)
			{
				emailAddressCollection5 = Envelope.a(A_0[7] as ArrayList, A_1);
				if (emailAddressCollection5 == null)
				{
					a_ = false;
				}
			}
			string a_6 = null;
			if (A_0[8] != null)
			{
				try
				{
					a_6 = ((ao)A_0[8]).a(A_1);
				}
				catch
				{
					a_ = false;
				}
			}
			string a_7 = null;
			if (A_0[9] != null)
			{
				try
				{
					a_7 = ((ao)A_0[9]).a(Encoding.GetEncoding(1252));
				}
				catch
				{
					a_ = false;
				}
			}
			return new Envelope(a_2, a_3, a_4, a_5, emailAddressCollection2, emailAddressCollection3, emailAddressCollection4, emailAddressCollection5, a_6, a_7, a_);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000327A0 File Offset: 0x000317A0
		private static EmailAddress b(ArrayList A_0, Encoding A_1)
		{
			if (A_0.Count < 4)
			{
				return null;
			}
			string displayName = null;
			string text = null;
			string text2 = null;
			if (A_0[0] != null)
			{
				try
				{
					displayName = global::a.i.h.c(((ao)A_0[0]).a(A_1));
				}
				catch
				{
					return null;
				}
			}
			if (A_0[2] != null)
			{
				try
				{
					text = ((ao)A_0[2]).a(A_1);
				}
				catch
				{
					return null;
				}
			}
			if (A_0[3] != null)
			{
				try
				{
					text2 = ((ao)A_0[3]).a(A_1);
				}
				catch
				{
					return null;
				}
			}
			if (text == null || text == string.Empty)
			{
				return null;
			}
			string email;
			if (text2 == null || text2 == string.Empty)
			{
				email = text;
			}
			else
			{
				email = text + "@" + text2;
			}
			return new EmailAddress(email, displayName);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0003289C File Offset: 0x0003189C
		private static EmailAddressCollection a(ArrayList A_0, Encoding A_1)
		{
			if (A_0 == null)
			{
				return null;
			}
			EmailAddressCollection emailAddressCollection = new EmailAddressCollection();
			for (int i = 0; i < A_0.Count; i++)
			{
				ArrayList arrayList = A_0[i] as ArrayList;
				if (arrayList != null)
				{
					EmailAddress emailAddress = Envelope.b(arrayList, A_1);
					if (emailAddress != null)
					{
						emailAddressCollection.Add(emailAddress);
					}
				}
			}
			return emailAddressCollection;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000328E8 File Offset: 0x000318E8
		private object a(object A_0, bool A_1, Encoding A_2)
		{
			if (A_0 == null)
			{
				return null;
			}
			ArrayList arrayList = A_0 as ArrayList;
			if (arrayList != null)
			{
				for (int i = 0; i < arrayList.Count; i++)
				{
					arrayList[i] = this.a(arrayList[i], A_1, A_2);
				}
				return arrayList;
			}
			ao ao = A_0 as ao;
			if (ao == null)
			{
				return A_0;
			}
			if (A_1)
			{
				return ao.c();
			}
			return ao.a(A_2);
		}

		// Token: 0x040008A9 RID: 2217
		private bool a;

		// Token: 0x040008AA RID: 2218
		private bool b;

		// Token: 0x040008AB RID: 2219
		private Encoding c;

		// Token: 0x040008AC RID: 2220
		private int d;

		// Token: 0x040008AD RID: 2221
		private MessageFlagSet e;

		// Token: 0x040008AE RID: 2222
		private DateTime f;

		// Token: 0x040008AF RID: 2223
		private int g;

		// Token: 0x040008B0 RID: 2224
		private long h;

		// Token: 0x040008B1 RID: 2225
		private string i;

		// Token: 0x040008B2 RID: 2226
		private string j;

		// Token: 0x040008B3 RID: 2227
		private string[] k;

		// Token: 0x040008B4 RID: 2228
		private ImapBodyStructure l;

		// Token: 0x040008B5 RID: 2229
		private HeaderCollection m;

		// Token: 0x040008B6 RID: 2230
		private MailMessage n;

		// Token: 0x040008B7 RID: 2231
		private DateTime o;

		// Token: 0x040008B8 RID: 2232
		private string p;

		// Token: 0x040008B9 RID: 2233
		private EmailAddress q;

		// Token: 0x040008BA RID: 2234
		private EmailAddress r;

		// Token: 0x040008BB RID: 2235
		private EmailAddressCollection s;

		// Token: 0x040008BC RID: 2236
		private EmailAddressCollection t;

		// Token: 0x040008BD RID: 2237
		private EmailAddressCollection u;

		// Token: 0x040008BE RID: 2238
		private EmailAddressCollection v;

		// Token: 0x040008BF RID: 2239
		private string w;

		// Token: 0x040008C0 RID: 2240
		private string x;

		// Token: 0x040008C1 RID: 2241
		private bool y;

		// Token: 0x040008C2 RID: 2242
		private Hashtable z;
	}
}
