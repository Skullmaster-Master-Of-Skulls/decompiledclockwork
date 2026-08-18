using System;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.CompoundFile.Doc.Native;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x0200009F RID: 159
	public class SummaryDocumentProperties : DocumentSerializable
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0001267C File Offset: 0x0001167C
		// (set) Token: 0x0600019D RID: 413 RVA: 0x000126D8 File Offset: 0x000116D8
		public string Author
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(4))
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return null;
					}
				}
				return this[PIDSI.Author].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Author, value);
				this[PIDSI.Author].Text = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00012728 File Offset: 0x00011728
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00012784 File Offset: 0x00011784
		public string ApplicationName
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(18))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						return null;
					}
				}
				return this[PIDSI.Appname].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ(PIDSI.Appname, value);
				this[PIDSI.Appname].Text = value;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000127D8 File Offset: 0x000117D8
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00012834 File Offset: 0x00011834
		public string Title
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(2))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return null;
					}
				}
				return this[PIDSI.Title].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Title, value);
				this[PIDSI.Title].Text = value;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00012884 File Offset: 0x00011884
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000128E0 File Offset: 0x000118E0
		public string Subject
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(3))
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return null;
					}
				}
				return this[PIDSI.Subject].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Subject, value);
				this[PIDSI.Subject].Text = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00012930 File Offset: 0x00011930
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0001298C File Offset: 0x0001198C
		public string Keywords
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(5))
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return null;
					}
				}
				return this[PIDSI.Keywords].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ(PIDSI.Keywords, value);
				this[PIDSI.Keywords].Text = value;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000129DC File Offset: 0x000119DC
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x00012A38 File Offset: 0x00011A38
		public string Comments
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					if (this.m_summaryHash.ContainsKey(6))
					{
						goto IL_40;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_2E;
					}
				}
				IL_2E:
				if (false)
				{
				}
				return null;
				IL_40:
				return this[PIDSI.Comments].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Comments, value);
				this[PIDSI.Comments].Text = value;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00012A88 File Offset: 0x00011A88
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x00012AE4 File Offset: 0x00011AE4
		public string Template
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(7))
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return null;
					}
				}
				return this[PIDSI.Template].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Template, value);
				this[PIDSI.Template].Value = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00012B34 File Offset: 0x00011B34
		// (set) Token: 0x060001AB RID: 427 RVA: 0x00012B90 File Offset: 0x00011B90
		public string LastAuthor
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(8))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return null;
					}
				}
				return this[PIDSI.LastAuthor].Text;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.LastAuthor, value);
				this[PIDSI.LastAuthor].Text = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00012BE0 File Offset: 0x00011BE0
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00012C3C File Offset: 0x00011C3C
		public string RevisionNumber
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(9))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						return null;
					}
				}
				return this[PIDSI.Revnumber].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Revnumber, value);
				this[PIDSI.Revnumber].Value = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00012C90 File Offset: 0x00011C90
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00012D4C File Offset: 0x00011D4C
		public TimeSpan TotalEditingTime
		{
			get
			{
				int num = 2;
				for (;;)
				{
					IL_0A:
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (!(this[PIDSI.EditTime].TimeSpan < TimeSpan.Zero))
						{
							num = 3;
							continue;
						}
						goto IL_5F;
					case 3:
						goto IL_9F;
					}
					while (this.m_summaryHash.ContainsKey(10))
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						if (false)
						{
						}
						num = 0;
						goto IL_0A;
					}
					goto IL_A1;
				}
				IL_5F:
				return TimeSpan.Zero;
				IL_9F:
				return this[PIDSI.EditTime].TimeSpan;
				IL_A1:
				if (true)
				{
				}
				return TimeSpan.MinValue;
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.EditTime, value);
				this[PIDSI.EditTime].Value = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00012DA8 File Offset: 0x00011DA8
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00012E08 File Offset: 0x00011E08
		public DateTime LastPrinted
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(11))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return DateTime.MinValue;
					}
				}
				return this[PIDSI.LastPrinted].DateTime;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ(PIDSI.LastPrinted, value);
				this[PIDSI.LastPrinted].DateTime = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00012E60 File Offset: 0x00011E60
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00012EC0 File Offset: 0x00011EC0
		public DateTime CreateDate
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					if (this.m_summaryHash.ContainsKey(12))
					{
						goto IL_45;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_2F;
					}
				}
				IL_2F:
				if (false)
				{
				}
				return DateTime.Now;
				IL_45:
				return this[PIDSI.Create_dtm].DateTime;
			}
			set
			{
				int a_ = 17;
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value.CompareTo(new DateTime(1900, 12, 31)) > 0)
						{
							goto IL_D5;
						}
						goto IL_9E;
					case 1:
						num = 0;
						continue;
					case 2:
						return;
					case 3:
						goto IL_E0;
					case 4:
						if (this.m_summaryHash.ContainsKey(12))
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D5;
							}
							if (false)
							{
							}
							num = 5;
							continue;
						}
						return;
					case 5:
						this.m_summaryHash.Remove(12);
						num = 2;
						continue;
					}
					if (!value.Equals(default(DateTime)))
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
					IL_D5:
					num = 3;
				}
				IL_9E:
				throw new Exception(ClipboardData.b("㍶ᡸེ᡼彾ꦈﶊ떔얠莢잤슦覨쪪쮬\udbae풰솲閴蚶许钺躼躾ﳄ杻背苎ﻐ韒釔胘苚蓜蛞죠췢", a_));
				IL_E0:
				if (true)
				{
				}
				this.ᜀ(PIDSI.Create_dtm, value);
				this[PIDSI.Create_dtm].DateTime = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00012FF0 File Offset: 0x00011FF0
		// (set) Token: 0x060001B5 RID: 437 RVA: 0x00013050 File Offset: 0x00012050
		public DateTime LastSaveDate
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(13))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						return DateTime.Now;
					}
				}
				if (true)
				{
				}
				return this[PIDSI.LastSave_dtm].DateTime;
			}
			set
			{
				int a_ = 6;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value.CompareTo(new DateTime(1900, 12, 31)) > 0)
						{
							num = 3;
							continue;
						}
						goto IL_91;
					case 1:
						num = 0;
						continue;
					case 2:
						goto IL_8F;
					case 3:
						goto IL_D0;
					case 4:
						if (this.m_summaryHash.ContainsKey(13))
						{
							num = 2;
							continue;
						}
						return;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8F;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 6:
						return;
					}
					if (!value.Equals(default(DateTime)))
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
					IL_8F:
					this.m_summaryHash.Remove(13);
					if (true)
					{
					}
					num = 6;
				}
				IL_91:
				throw new Exception(ClipboardData.b("⡫཭ѯ᝱味ɵᅷ᝹᥻幽ꪉﾋﾏ뢗鍊뺝솟쒡킣쎥\udaa7誩鶫鲭龯膱薳馵覷莹費躽迁觃資軉韍觏译跓ￕ", a_));
				IL_D0:
				this.ᜀ(PIDSI.LastSave_dtm, value);
				this[PIDSI.LastSave_dtm].DateTime = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x0001317C File Offset: 0x0001217C
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000131DC File Offset: 0x000121DC
		public int PageCount
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(14))
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					return int.MinValue;
				}
				return this[PIDSI.Pagecount].ToInt();
			}
			internal set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Pagecount, value);
				this[PIDSI.Pagecount].Int32 = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00013234 File Offset: 0x00012234
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00013294 File Offset: 0x00012294
		public int WordCount
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(15))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						return int.MinValue;
					}
				}
				return this[PIDSI.Wordcount].ToInt();
			}
			internal set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Wordcount, value);
				this[PIDSI.Wordcount].Int32 = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000132EC File Offset: 0x000122EC
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0001334C File Offset: 0x0001234C
		public int CharCount
		{
			get
			{
				for (;;)
				{
					if (true)
					{
					}
					if (!this.m_summaryHash.ContainsKey(16))
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						}
						break;
					}
					goto IL_45;
				}
				if (false)
				{
				}
				return int.MinValue;
				IL_45:
				return this[PIDSI.Charcount].ToInt();
			}
			internal set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Charcount, value);
				this[PIDSI.Charcount].Int32 = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001BC RID: 444 RVA: 0x000133A4 File Offset: 0x000123A4
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00013400 File Offset: 0x00012400
		public string Thumbnail
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(17))
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					return null;
				}
				return this[PIDSI.Thumbnail].Text;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Thumbnail, value);
				this[PIDSI.Thumbnail].Text = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00013454 File Offset: 0x00012454
		// (set) Token: 0x060001BF RID: 447 RVA: 0x000134B4 File Offset: 0x000124B4
		public int DocSecurity
		{
			get
			{
				while (!this.m_summaryHash.ContainsKey(19))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						return int.MinValue;
					}
				}
				return this[PIDSI.Doc_security].ToInt();
			}
			set
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				this.ᜀ(PIDSI.Doc_security, value);
				this[PIDSI.Doc_security].Int32 = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		internal DocumentProperty this[PIDSI A_0]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.m_summaryHash[(int)A_0];
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00013554 File Offset: 0x00012554
		public int Count
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return this.m_summaryHash.Count;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0001359C File Offset: 0x0001259C
		internal Dictionary<int, DocumentProperty> SummaryHash
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.m_summaryHash;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000135E0 File Offset: 0x000125E0
		internal SummaryDocumentProperties() : this(0)
		{
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000135F4 File Offset: 0x000125F4
		internal SummaryDocumentProperties(int A_0) : base(null, null)
		{
			this.m_summaryHash = new Dictionary<int, DocumentProperty>(A_0);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00013618 File Offset: 0x00012618
		private bool ᜀ(int A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return this.m_summaryHash.ContainsKey(A_0);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00013660 File Offset: 0x00012660
		public void Add(int key, DocumentProperty props)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.m_summaryHash.Add(key, props);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000136A8 File Offset: 0x000126A8
		internal void ᜀ(PIDSI A_0, object A_1)
		{
			while (this.m_summaryHash.ContainsKey((int)A_0))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					this[A_0].Value = A_1;
					return;
				}
			}
			DocumentProperty value = new DocumentProperty((BuiltInProperty)A_0, A_1);
			this.m_summaryHash[(int)A_0] = value;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00013718 File Offset: 0x00012718
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 5;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 24;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3BA;
					case 1:
						goto IL_528;
					case 2:
						writer.WriteValue(ClipboardData.b("⹪६ٮհ❲ᱴ᩶ᱸ", a_), this.TotalEditingTime.TotalMinutes.ToString());
						num = 51;
						continue;
					case 3:
						goto IL_36C;
					case 4:
						if (this.ᜀ(14))
						{
							num = 31;
							continue;
						}
						goto IL_4D6;
					case 5:
						goto IL_48B;
					case 6:
						if (this.ᜀ(19))
						{
							num = 26;
							continue;
						}
						return;
					case 7:
						writer.WriteValue(ClipboardData.b("㥪࡬᥮ᡰrᱴᡶ᝸㕺ࡼቾ", a_), this.RevisionNumber);
						num = 38;
						continue;
					case 8:
						goto IL_4D6;
					case 9:
						if (this.ᜀ(2))
						{
							num = 29;
							continue;
						}
						goto IL_36C;
					case 10:
						writer.WriteValue(ClipboardData.b("⡪լ๮Ͱひᩴɶ᝸ེ", a_), this.CharCount);
						num = 0;
						continue;
					case 11:
						if (this.ᜀ(7))
						{
							num = 53;
							continue;
						}
						goto IL_2F1;
					case 12:
						goto IL_639;
					case 13:
						if (this.ᜀ(12))
						{
							num = 44;
							continue;
						}
						goto IL_1E7;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_18E;
						default:
							if (false)
							{
							}
							if (this.ᜀ(18))
							{
								num = 43;
								continue;
							}
							goto IL_4AF;
						}
						break;
					case 15:
						if (this.ᜀ(13))
						{
							num = 52;
							continue;
						}
						goto IL_66B;
					case 16:
						writer.WriteValue(ClipboardData.b("⩪ᡬ᭮ᥰᱲݴ", a_), this.Author);
						num = 42;
						continue;
					case 17:
						if (this.ᜀ(16))
						{
							num = 10;
							continue;
						}
						goto IL_3BA;
					case 18:
						goto IL_16C;
					case 19:
						goto IL_45B;
					case 20:
						if (this.ᜀ(9))
						{
							num = 7;
							continue;
						}
						goto IL_611;
					case 21:
						writer.WriteValue(ClipboardData.b("❪౬ᱮհ㉲tͶᅸᑺོ", a_), this.LastAuthor);
						num = 45;
						continue;
					case 22:
						goto IL_2F1;
					case 23:
						writer.WriteValue(ClipboardData.b("㽪լᩮᱰᅲ᭴ᙶၸ᝺", a_), this.Thumbnail);
						num = 28;
						continue;
					case 24:
						if (this.ᜀ(4))
						{
							num = 16;
							continue;
						}
						goto IL_5A3;
					case 25:
						if (this.ᜀ(8))
						{
							num = 21;
							continue;
						}
						goto IL_433;
					case 26:
						writer.WriteValue(ClipboardData.b("⽪ɬ౮≰ᙲᙴɶ୸ቺॼپ", a_), this.DocSecurity);
						num = 50;
						continue;
					case 27:
						writer.WriteValue(ClipboardData.b("㱪ɬᵮᕰひᩴɶ᝸ེ", a_), this.WordCount);
						num = 12;
						continue;
					case 28:
						goto IL_693;
					case 29:
						writer.WriteValue(ClipboardData.b("㽪Ѭ᭮ᵰᙲ", a_), this.Title);
						num = 3;
						continue;
					case 30:
						if (this.ᜀ(11))
						{
							num = 49;
							continue;
						}
						goto IL_45B;
					case 31:
						writer.WriteValue(ClipboardData.b("㭪౬࡮ᑰひᩴɶ᝸ེ", a_), this.PageCount);
						num = 8;
						continue;
					case 32:
						goto IL_18E;
					case 33:
						if (this.ᜀ(15))
						{
							num = 27;
							continue;
						}
						goto IL_639;
					case 34:
						if (this.ᜀ(5))
						{
							num = 32;
							continue;
						}
						goto IL_528;
					case 35:
						goto IL_66B;
					case 36:
						if (this.ᜀ(17))
						{
							num = 23;
							continue;
						}
						goto IL_693;
					case 37:
						if (this.ᜀ(6))
						{
							num = 46;
							continue;
						}
						goto IL_48B;
					case 38:
						goto IL_611;
					case 39:
						goto IL_4AF;
					case 40:
						writer.WriteValue(ClipboardData.b("㡪ᡬ൮᭰ᙲᙴͶ", a_), this.Subject);
						num = 18;
						continue;
					case 41:
						if (this.ᜀ(10))
						{
							num = 2;
							continue;
						}
						goto IL_144;
					case 42:
						goto IL_5A3;
					case 43:
						writer.WriteValue(ClipboardData.b("⩪ᵬὮᵰᩲᙴᙶ൸ቺቼᅾ쾀", a_), this.ApplicationName);
						num = 39;
						continue;
					case 44:
						if (true)
						{
						}
						writer.WriteValue(ClipboardData.b("⡪Ὤ੮ၰݲၴ㍶ᡸེ᡼", a_), this.CreateDate);
						num = 48;
						continue;
					case 45:
						goto IL_433;
					case 46:
						writer.WriteValue(ClipboardData.b("⡪ɬɮᱰᙲ᭴Ͷ੸", a_), this.Comments);
						num = 5;
						continue;
					case 47:
						if (this.ᜀ(3))
						{
							num = 40;
							continue;
						}
						goto IL_16C;
					case 48:
						goto IL_1E7;
					case 49:
						writer.WriteValue(ClipboardData.b("❪౬ᱮհ⍲ݴṶ᝸ེ᡼᭾", a_), this.LastPrinted);
						num = 19;
						continue;
					case 50:
						return;
					case 51:
						goto IL_144;
					case 52:
						writer.WriteValue(ClipboardData.b("❪౬ᱮհ⁲ᑴŶᱸ㽺ᱼ୾", a_), this.LastSaveDate);
						num = 35;
						continue;
					case 53:
						writer.WriteValue(ClipboardData.b("㽪࡬ɮŰὲᑴͶᱸ", a_), this.Template);
						num = 22;
						continue;
					}
					break;
					IL_144:
					num = 30;
					continue;
					IL_16C:
					num = 34;
					continue;
					IL_18E:
					writer.WriteValue(ClipboardData.b("⁪࡬᙮ٰᱲݴ፶੸", a_), this.Keywords);
					num = 1;
					continue;
					IL_1E7:
					num = 15;
					continue;
					IL_2F1:
					num = 25;
					continue;
					IL_36C:
					num = 47;
					continue;
					IL_3BA:
					num = 36;
					continue;
					IL_433:
					num = 20;
					continue;
					IL_45B:
					num = 13;
					continue;
					IL_48B:
					num = 11;
					continue;
					IL_4AF:
					num = 9;
					continue;
					IL_4D6:
					num = 33;
					continue;
					IL_528:
					num = 37;
					continue;
					IL_5A3:
					num = 14;
					continue;
					IL_611:
					num = 41;
					continue;
					IL_639:
					num = 17;
					continue;
					IL_66B:
					num = 4;
					continue;
					IL_693:
					num = 6;
				}
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00013E08 File Offset: 0x00012E08
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 2;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("㽧թṫ੭㍯ᵱųᡵ౷", a_)))
						{
							num = 43;
							continue;
						}
						goto IL_1A3;
					case 1:
						goto IL_3A7;
					case 2:
						if (reader.HasAttribute(ClipboardData.b("㱧ཀྵūṭᱯ፱s፵", a_)))
						{
							num = 12;
							continue;
						}
						goto IL_349;
					case 3:
						if (reader.HasAttribute(ClipboardData.b("⥧Ὡᡫ٭Ὧq", a_)))
						{
							num = 45;
							continue;
						}
						goto IL_67A;
					case 4:
						goto IL_5E8;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("⭧ᡩ५཭ѯ᝱び᝵౷ό", a_)))
						{
							num = 22;
							continue;
						}
						goto IL_646;
					case 6:
						if (reader.HasAttribute(ClipboardData.b("㡧୩୫୭㍯ᵱųᡵ౷", a_)))
						{
							num = 34;
							continue;
						}
						goto IL_73D;
					case 7:
						if (reader.HasAttribute(ClipboardData.b("㱧ͩᡫɭᕯ", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_3A7;
					case 8:
						goto IL_409;
					case 9:
						this.Title = reader.ReadString(ClipboardData.b("㱧ͩᡫɭᕯ", a_));
						num = 1;
						continue;
					case 10:
						goto IL_25F;
					case 11:
						this.LastSaveDate = reader.ReadDateTime(ClipboardData.b("⑧୩Ὣᩭ⍯፱ɳ፵㱷᭹ࡻ᭽", a_));
						num = 42;
						continue;
					case 12:
						this.Template = reader.ReadString(ClipboardData.b("㱧ཀྵūṭᱯ፱s፵", a_));
						if (true)
						{
						}
						num = 47;
						continue;
					case 13:
						goto IL_1D7;
					case 14:
						if (reader.HasAttribute(ClipboardData.b("⑧୩Ὣᩭ⁯qᵳᡵ౷ό᡻", a_)))
						{
							num = 29;
							continue;
						}
						goto IL_6AE;
					case 15:
						this.Keywords = reader.ReadString(ClipboardData.b("⍧ཀྵᕫᥭὯqၳյ", a_));
						num = 8;
						continue;
					case 16:
						goto IL_3D8;
					case 17:
						goto IL_73D;
					case 18:
						return;
					case 19:
						goto IL_1A3;
					case 20:
						if (reader.HasAttribute(ClipboardData.b("⭧թūͭᕯᱱsյ", a_)))
						{
							num = 49;
							continue;
						}
						goto IL_5AA;
					case 21:
						goto IL_646;
					case 22:
						this.CreateDate = reader.ReadDateTime(ClipboardData.b("⭧ᡩ५཭ѯ᝱び᝵౷ό", a_));
						num = 21;
						continue;
					case 23:
						goto IL_717;
					case 24:
						this.DocSecurity = reader.ReadInt(ClipboardData.b("Ⱨթཫ㵭ᕯᅱųѵᅷ๹ջ", a_));
						num = 18;
						continue;
					case 25:
						this.Subject = reader.ReadString(ClipboardData.b("㭧Ὡ๫ѭᕯᅱs", a_));
						num = 16;
						continue;
					case 26:
						this.RevisionNumber = reader.ReadString(ClipboardData.b("㩧ཀྵᩫݭͯ᭱᭳ᡵ㙷ཹᅻᱽ", a_));
						num = 10;
						continue;
					case 27:
						if (reader.HasAttribute(ClipboardData.b("⥧ᩩᱫɭ᥯ᅱᕳɵᅷᕹቻぽ", a_)))
						{
							num = 40;
							continue;
						}
						goto IL_1D7;
					case 28:
						this.Thumbnail = reader.ReadString(ClipboardData.b("㱧ɩᥫͭቯᱱᕳήᑷ", a_));
						num = 37;
						continue;
					case 29:
						this.LastPrinted = reader.ReadDateTime(ClipboardData.b("⑧୩Ὣᩭ⁯qᵳᡵ౷ό᡻", a_));
						num = 36;
						continue;
					case 30:
						if (reader.HasAttribute(ClipboardData.b("㭧Ὡ๫ѭᕯᅱs", a_)))
						{
							num = 25;
							continue;
						}
						goto IL_3D8;
					case 31:
						goto IL_43D;
					case 32:
						if (reader.HasAttribute(ClipboardData.b("⑧୩Ὣᩭ⍯፱ɳ፵㱷᭹ࡻ᭽", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_11B;
					case 33:
						goto IL_5AA;
					case 34:
						this.ᜀ(PIDSI.Pagecount, reader.ReadInt(ClipboardData.b("㡧୩୫୭㍯ᵱųᡵ౷", a_)));
						num = 17;
						continue;
					case 35:
						if (reader.HasAttribute(ClipboardData.b("⭧ɩ൫ᱭ㍯ᵱųᡵ౷", a_)))
						{
							num = 50;
							continue;
						}
						goto IL_43D;
					case 36:
						goto IL_6AE;
					case 37:
						goto IL_70C;
					case 38:
						if (reader.HasAttribute(ClipboardData.b("⍧ཀྵᕫᥭὯqၳյ", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_409;
					case 39:
						if (reader.HasAttribute(ClipboardData.b("⑧୩Ὣᩭㅯݱsṵ᝷ࡹ", a_)))
						{
							num = 41;
							continue;
						}
						goto IL_5E8;
					case 40:
						this.ApplicationName = reader.ReadString(ClipboardData.b("⥧ᩩᱫɭ᥯ᅱᕳɵᅷᕹቻぽ", a_));
						num = 13;
						continue;
					case 41:
						this.LastAuthor = reader.ReadString(ClipboardData.b("⑧୩Ὣᩭㅯݱsṵ᝷ࡹ", a_));
						num = 4;
						continue;
					case 42:
						goto IL_11B;
					case 43:
						this.ᜀ(PIDSI.Wordcount, reader.ReadInt(ClipboardData.b("㽧թṫ੭㍯ᵱųᡵ౷", a_)));
						num = 19;
						continue;
					case 44:
						if (reader.HasAttribute(ClipboardData.b("㱧ɩᥫͭቯᱱᕳήᑷ", a_)))
						{
							num = 28;
							continue;
						}
						goto IL_70C;
					case 45:
						this.Author = reader.ReadString(ClipboardData.b("⥧Ὡᡫ٭Ὧq", a_));
						num = 46;
						continue;
					case 46:
						goto IL_67A;
					case 47:
						goto IL_349;
					case 48:
						if (reader.HasAttribute(ClipboardData.b("㩧ཀྵᩫݭͯ᭱᭳ᡵ㙷ཹᅻᱽ", a_)))
						{
							num = 26;
							continue;
						}
						goto IL_25F;
					case 49:
						this.Comments = reader.ReadString(ClipboardData.b("⭧թūͭᕯᱱsյ", a_));
						num = 33;
						continue;
					case 50:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_717;
						default:
							if (false)
							{
							}
							this.ᜀ(PIDSI.Charcount, reader.ReadInt(ClipboardData.b("⭧ɩ൫ᱭ㍯ᵱųᡵ౷", a_)));
							num = 31;
							continue;
						}
						break;
					}
					break;
					IL_11B:
					num = 6;
					continue;
					IL_1A3:
					num = 35;
					continue;
					IL_1D7:
					num = 7;
					continue;
					IL_25F:
					reader.HasAttribute(ClipboardData.b("ⵧ๩իᩭ⑯᭱ᥳ፵", a_));
					num = 14;
					continue;
					IL_349:
					num = 39;
					continue;
					IL_3A7:
					num = 30;
					continue;
					IL_3D8:
					num = 38;
					continue;
					IL_409:
					num = 20;
					continue;
					IL_43D:
					num = 44;
					continue;
					IL_5AA:
					num = 2;
					continue;
					IL_5E8:
					num = 48;
					continue;
					IL_646:
					num = 32;
					continue;
					IL_67A:
					num = 27;
					continue;
					IL_6AE:
					num = 5;
					continue;
					IL_70C:
					num = 23;
					continue;
					IL_717:
					if (reader.HasAttribute(ClipboardData.b("Ⱨթཫ㵭ᕯᅱųѵᅷ๹ջ", a_)))
					{
						num = 24;
						continue;
					}
					return;
					IL_73D:
					num = 0;
				}
			}
		}

		// Token: 0x04000990 RID: 2448
		private long \u25D8\u00A3\u00A9\u00AF;

		// Token: 0x04000991 RID: 2449
		private byte \u2609\u00AF\u009B\u008C;

		// Token: 0x04000992 RID: 2450
		private string[] \u2609\u0086ª\u0094;

		// Token: 0x04000993 RID: 2451
		private bool \u25D8\u00A9\u009A\u008D;

		// Token: 0x04000994 RID: 2452
		private bool \u2593\u008A\u0095\u0094;

		// Token: 0x04000995 RID: 2453
		private long[] \u2460\u00A9\u00A3\u007F;

		// Token: 0x04000996 RID: 2454
		protected Dictionary<int, DocumentProperty> m_summaryHash;
	}
}
