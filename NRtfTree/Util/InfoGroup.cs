using System;
using System.Text;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000005 RID: 5
	public class InfoGroup
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000036AE File Offset: 0x000018AE
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000036B6 File Offset: 0x000018B6
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000036BF File Offset: 0x000018BF
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000036C7 File Offset: 0x000018C7
		public string Subject
		{
			get
			{
				return this._subject;
			}
			set
			{
				this._subject = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000036D0 File Offset: 0x000018D0
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000036D8 File Offset: 0x000018D8
		public string Author
		{
			get
			{
				return this._author;
			}
			set
			{
				this._author = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000036E1 File Offset: 0x000018E1
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000036E9 File Offset: 0x000018E9
		public string Manager
		{
			get
			{
				return this._manager;
			}
			set
			{
				this._manager = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000036F2 File Offset: 0x000018F2
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000036FA File Offset: 0x000018FA
		public string Company
		{
			get
			{
				return this._company;
			}
			set
			{
				this._company = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003703 File Offset: 0x00001903
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000370B File Offset: 0x0000190B
		public string Operator
		{
			get
			{
				return this._operator;
			}
			set
			{
				this._operator = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003714 File Offset: 0x00001914
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000371C File Offset: 0x0000191C
		public string Category
		{
			get
			{
				return this._category;
			}
			set
			{
				this._category = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003725 File Offset: 0x00001925
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000372D File Offset: 0x0000192D
		public string Keywords
		{
			get
			{
				return this._keywords;
			}
			set
			{
				this._keywords = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003736 File Offset: 0x00001936
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000373E File Offset: 0x0000193E
		public string Comment
		{
			get
			{
				return this._comment;
			}
			set
			{
				this._comment = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003747 File Offset: 0x00001947
		// (set) Token: 0x0600005F RID: 95 RVA: 0x0000374F File Offset: 0x0000194F
		public string DocComment
		{
			get
			{
				return this._doccomm;
			}
			set
			{
				this._doccomm = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003758 File Offset: 0x00001958
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003760 File Offset: 0x00001960
		public string HlinkBase
		{
			get
			{
				return this._hlinkbase;
			}
			set
			{
				this._hlinkbase = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003769 File Offset: 0x00001969
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003771 File Offset: 0x00001971
		public DateTime CreationTime
		{
			get
			{
				return this._creatim;
			}
			set
			{
				this._creatim = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000377A File Offset: 0x0000197A
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003782 File Offset: 0x00001982
		public DateTime RevisionTime
		{
			get
			{
				return this._revtim;
			}
			set
			{
				this._revtim = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000378B File Offset: 0x0000198B
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003793 File Offset: 0x00001993
		public DateTime LastPrintTime
		{
			get
			{
				return this._printim;
			}
			set
			{
				this._printim = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000379C File Offset: 0x0000199C
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000037A4 File Offset: 0x000019A4
		public DateTime BackupTime
		{
			get
			{
				return this._buptim;
			}
			set
			{
				this._buptim = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000037AD File Offset: 0x000019AD
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000037B5 File Offset: 0x000019B5
		public int Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000037BE File Offset: 0x000019BE
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000037C6 File Offset: 0x000019C6
		public int InternalVersion
		{
			get
			{
				return this._vern;
			}
			set
			{
				this._vern = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000037CF File Offset: 0x000019CF
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000037D7 File Offset: 0x000019D7
		public int EditingTime
		{
			get
			{
				return this._edmins;
			}
			set
			{
				this._edmins = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000037E0 File Offset: 0x000019E0
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000037E8 File Offset: 0x000019E8
		public int NumberOfPages
		{
			get
			{
				return this._nofpages;
			}
			set
			{
				this._nofpages = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000037F1 File Offset: 0x000019F1
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000037F9 File Offset: 0x000019F9
		public int NumberOfWords
		{
			get
			{
				return this._nofwords;
			}
			set
			{
				this._nofwords = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003802 File Offset: 0x00001A02
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000380A File Offset: 0x00001A0A
		public int NumberOfChars
		{
			get
			{
				return this._nofchars;
			}
			set
			{
				this._nofchars = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003813 File Offset: 0x00001A13
		// (set) Token: 0x06000077 RID: 119 RVA: 0x0000381B File Offset: 0x00001A1B
		public int Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003824 File Offset: 0x00001A24
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Title     : " + this.Title);
			stringBuilder.AppendLine("Subject   : " + this.Subject);
			stringBuilder.AppendLine("Author    : " + this.Author);
			stringBuilder.AppendLine("Manager   : " + this.Manager);
			stringBuilder.AppendLine("Company   : " + this.Company);
			stringBuilder.AppendLine("Operator  : " + this.Operator);
			stringBuilder.AppendLine("Category  : " + this.Category);
			stringBuilder.AppendLine("Keywords  : " + this.Keywords);
			stringBuilder.AppendLine("Comment   : " + this.Comment);
			stringBuilder.AppendLine("DComment  : " + this.DocComment);
			stringBuilder.AppendLine("HLinkBase : " + this.HlinkBase);
			stringBuilder.AppendLine("Created   : " + this.CreationTime);
			stringBuilder.AppendLine("Revised   : " + this.RevisionTime);
			stringBuilder.AppendLine("Printed   : " + this.LastPrintTime);
			stringBuilder.AppendLine("Backup    : " + this.BackupTime);
			stringBuilder.AppendLine("Version   : " + this.Version);
			stringBuilder.AppendLine("IVersion  : " + this.InternalVersion);
			stringBuilder.AppendLine("Editing   : " + this.EditingTime);
			stringBuilder.AppendLine("Num Pages : " + this.NumberOfPages);
			stringBuilder.AppendLine("Num Words : " + this.NumberOfWords);
			stringBuilder.AppendLine("Num Chars : " + this.NumberOfChars);
			stringBuilder.AppendLine("Id        : " + this.Id);
			return stringBuilder.ToString();
		}

		// Token: 0x0400000F RID: 15
		private string _title = "";

		// Token: 0x04000010 RID: 16
		private string _subject = "";

		// Token: 0x04000011 RID: 17
		private string _author = "";

		// Token: 0x04000012 RID: 18
		private string _manager = "";

		// Token: 0x04000013 RID: 19
		private string _company = "";

		// Token: 0x04000014 RID: 20
		private string _operator = "";

		// Token: 0x04000015 RID: 21
		private string _category = "";

		// Token: 0x04000016 RID: 22
		private string _keywords = "";

		// Token: 0x04000017 RID: 23
		private string _comment = "";

		// Token: 0x04000018 RID: 24
		private string _doccomm = "";

		// Token: 0x04000019 RID: 25
		private string _hlinkbase = "";

		// Token: 0x0400001A RID: 26
		private DateTime _creatim = DateTime.MinValue;

		// Token: 0x0400001B RID: 27
		private DateTime _revtim = DateTime.MinValue;

		// Token: 0x0400001C RID: 28
		private DateTime _printim = DateTime.MinValue;

		// Token: 0x0400001D RID: 29
		private DateTime _buptim = DateTime.MinValue;

		// Token: 0x0400001E RID: 30
		private int _version = -1;

		// Token: 0x0400001F RID: 31
		private int _vern = -1;

		// Token: 0x04000020 RID: 32
		private int _edmins = -1;

		// Token: 0x04000021 RID: 33
		private int _nofpages = -1;

		// Token: 0x04000022 RID: 34
		private int _nofwords = -1;

		// Token: 0x04000023 RID: 35
		private int _nofchars = -1;

		// Token: 0x04000024 RID: 36
		private int _id = -1;
	}
}
