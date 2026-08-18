using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.text
{
	// Token: 0x020003BC RID: 956
	public class Meta : IElement
	{
		// Token: 0x06002138 RID: 8504 RVA: 0x000C9684 File Offset: 0x000C8684
		public Meta(int type, string content)
		{
			this.type = type;
			this.content = new StringBuilder(content);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x000C969F File Offset: 0x000C869F
		public Meta(string tag, string content)
		{
			this.type = Meta.GetType(tag);
			this.content = new StringBuilder(content);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x000C96C0 File Offset: 0x000C86C0
		public bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				result = listener.Add(this);
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x000C96F0 File Offset: 0x000C86F0
		public int Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x000C96F8 File Offset: 0x000C86F8
		public List<Chunk> Chunks
		{
			get
			{
				return new List<Chunk>();
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x000C96FF File Offset: 0x000C86FF
		public bool IsContent()
		{
			return false;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x000C9702 File Offset: 0x000C8702
		public bool IsNestable()
		{
			return false;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x000C9705 File Offset: 0x000C8705
		public StringBuilder Append(string str)
		{
			return this.content.Append(str);
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x000C9713 File Offset: 0x000C8713
		public string Content
		{
			get
			{
				return this.content.ToString();
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x000C9720 File Offset: 0x000C8720
		public virtual string Name
		{
			get
			{
				switch (this.type)
				{
				case 1:
					return "title";
				case 2:
					return "subject";
				case 3:
					return "keywords";
				case 4:
					return "author";
				case 5:
					return "producer";
				case 6:
					return "creationdate";
				default:
					return "unknown";
				}
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000C9780 File Offset: 0x000C8780
		public static int GetType(string tag)
		{
			if ("subject".Equals(tag))
			{
				return 2;
			}
			if ("keywords".Equals(tag))
			{
				return 3;
			}
			if ("author".Equals(tag))
			{
				return 4;
			}
			if ("title".Equals(tag))
			{
				return 1;
			}
			if ("producer".Equals(tag))
			{
				return 5;
			}
			if ("creationdate".Equals(tag))
			{
				return 6;
			}
			return 0;
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000C97E8 File Offset: 0x000C87E8
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x040016EF RID: 5871
		private int type;

		// Token: 0x040016F0 RID: 5872
		private StringBuilder content;
	}
}
