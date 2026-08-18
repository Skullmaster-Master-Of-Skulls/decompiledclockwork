using System;
using System.Text;

namespace MailBee.Html
{
	// Token: 0x0200000F RID: 15
	public class TagAttribute
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00005D44 File Offset: 0x00004D44
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00005D4C File Offset: 0x00004D4C
		public string Name
		{
			get
			{
				return this.a;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				this.a = value;
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00005D74 File Offset: 0x00004D74
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00005D7C File Offset: 0x00004D7C
		public string Value
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00005D8C File Offset: 0x00004D8C
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00005DCA File Offset: 0x00004DCA
		public string Definition
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(this.Name);
				if (this.Value != null)
				{
					stringBuilder.Append("=").Append(this.Value);
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					throw new MailBeeInvalidArgumentException(22);
				}
				this.a(value);
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x17000033 RID: 51
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005DF2 File Offset: 0x00004DF2
		private bool IsRebuildNeeded
		{
			set
			{
				if (this.ParentCollection != null)
				{
					this.ParentCollection.IsRebuildNeeded = value;
				}
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005E08 File Offset: 0x00004E08
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00005E10 File Offset: 0x00004E10
		internal TagAttributeCollection ParentCollection
		{
			get
			{
				return this.c;
			}
			set
			{
				this.c = value;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005E19 File Offset: 0x00004E19
		public TagAttribute()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005E21 File Offset: 0x00004E21
		public TagAttribute(string attribute)
		{
			if (attribute == null || attribute == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.a(attribute);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005E48 File Offset: 0x00004E48
		internal TagAttribute(string A_0, TagAttributeCollection A_1)
		{
			this.a(A_0);
			this.ParentCollection = A_1;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005E60 File Offset: 0x00004E60
		public string GetProtocol()
		{
			int num = this.Value.IndexOf(':');
			if (num == -1)
			{
				return string.Empty;
			}
			int num2 = 0;
			while (this.Value[num2] == '"' || this.Value[num2] == '\'')
			{
				num2++;
			}
			return this.Value.Substring(num2, num - 1).Trim();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005EC1 File Offset: 0x00004EC1
		public void Remove()
		{
			this.ParentCollection.Remove(this);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005ED0 File Offset: 0x00004ED0
		private void a(string A_0)
		{
			int num = A_0.IndexOf('=');
			if (num == -1)
			{
				this.a = A_0.Trim();
				this.b = null;
				return;
			}
			this.a = A_0.Substring(0, num).Trim();
			this.b = A_0.Substring(num + 1, A_0.Length - num - 1).Trim();
		}

		// Token: 0x04000051 RID: 81
		private string a;

		// Token: 0x04000052 RID: 82
		private string b;

		// Token: 0x04000053 RID: 83
		private TagAttributeCollection c;
	}
}
