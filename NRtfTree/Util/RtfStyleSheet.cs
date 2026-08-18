using System;
using Net.Sgoliver.NRtfTree.Core;

namespace Net.Sgoliver.NRtfTree.Util
{
	// Token: 0x02000012 RID: 18
	public class RtfStyleSheet
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00005E31 File Offset: 0x00004031
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00005E39 File Offset: 0x00004039
		public int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005E42 File Offset: 0x00004042
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00005E4A File Offset: 0x0000404A
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005E53 File Offset: 0x00004053
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00005E5B File Offset: 0x0000405B
		public RtfStyleSheetType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005E64 File Offset: 0x00004064
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00005E6C File Offset: 0x0000406C
		public bool Additive
		{
			get
			{
				return this.additive;
			}
			set
			{
				this.additive = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005E75 File Offset: 0x00004075
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00005E7D File Offset: 0x0000407D
		public int BasedOn
		{
			get
			{
				return this.basedOn;
			}
			set
			{
				this.basedOn = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005E86 File Offset: 0x00004086
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00005E8E File Offset: 0x0000408E
		public int Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005E97 File Offset: 0x00004097
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00005E9F File Offset: 0x0000409F
		public bool AutoUpdate
		{
			get
			{
				return this.autoUpdate;
			}
			set
			{
				this.autoUpdate = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005EA8 File Offset: 0x000040A8
		// (set) Token: 0x060000FE RID: 254 RVA: 0x00005EB0 File Offset: 0x000040B0
		public bool Hidden
		{
			get
			{
				return this.hidden;
			}
			set
			{
				this.hidden = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005EB9 File Offset: 0x000040B9
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005EC1 File Offset: 0x000040C1
		public int Link
		{
			get
			{
				return this.link;
			}
			set
			{
				this.link = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005ECA File Offset: 0x000040CA
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005ED2 File Offset: 0x000040D2
		public bool Locked
		{
			get
			{
				return this.locked;
			}
			set
			{
				this.locked = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005EDB File Offset: 0x000040DB
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005EE3 File Offset: 0x000040E3
		public bool Personal
		{
			get
			{
				return this.personal;
			}
			set
			{
				this.personal = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005EEC File Offset: 0x000040EC
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00005EF4 File Offset: 0x000040F4
		public bool Compose
		{
			get
			{
				return this.compose;
			}
			set
			{
				this.compose = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005EFD File Offset: 0x000040FD
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00005F05 File Offset: 0x00004105
		public bool Reply
		{
			get
			{
				return this.reply;
			}
			set
			{
				this.reply = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005F0E File Offset: 0x0000410E
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00005F16 File Offset: 0x00004116
		public int Styrsid
		{
			get
			{
				return this.styrsid;
			}
			set
			{
				this.styrsid = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005F1F File Offset: 0x0000411F
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00005F27 File Offset: 0x00004127
		public bool SemiHidden
		{
			get
			{
				return this.semiHidden;
			}
			set
			{
				this.semiHidden = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005F30 File Offset: 0x00004130
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005F38 File Offset: 0x00004138
		public RtfNodeCollection KeyCode
		{
			get
			{
				return this.keyCode;
			}
			set
			{
				this.keyCode = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005F41 File Offset: 0x00004141
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00005F49 File Offset: 0x00004149
		public RtfNodeCollection Formatting
		{
			get
			{
				return this.formatting;
			}
			set
			{
				this.formatting = value;
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005F54 File Offset: 0x00004154
		public RtfStyleSheet()
		{
			this.keyCode = null;
			this.formatting = new RtfNodeCollection();
		}

		// Token: 0x04000051 RID: 81
		private int index;

		// Token: 0x04000052 RID: 82
		private string name = "";

		// Token: 0x04000053 RID: 83
		private RtfStyleSheetType type = RtfStyleSheetType.Paragraph;

		// Token: 0x04000054 RID: 84
		private bool additive;

		// Token: 0x04000055 RID: 85
		private int basedOn = -1;

		// Token: 0x04000056 RID: 86
		private int next = -1;

		// Token: 0x04000057 RID: 87
		private bool autoUpdate;

		// Token: 0x04000058 RID: 88
		private bool hidden;

		// Token: 0x04000059 RID: 89
		private int link = -1;

		// Token: 0x0400005A RID: 90
		private bool locked;

		// Token: 0x0400005B RID: 91
		private bool personal;

		// Token: 0x0400005C RID: 92
		private bool compose;

		// Token: 0x0400005D RID: 93
		private bool reply;

		// Token: 0x0400005E RID: 94
		private int styrsid = -1;

		// Token: 0x0400005F RID: 95
		private bool semiHidden;

		// Token: 0x04000060 RID: 96
		private RtfNodeCollection keyCode;

		// Token: 0x04000061 RID: 97
		private RtfNodeCollection formatting;
	}
}
