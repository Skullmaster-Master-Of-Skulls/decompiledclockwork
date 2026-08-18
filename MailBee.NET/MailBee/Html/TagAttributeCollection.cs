using System;
using System.Collections;
using System.Text;

namespace MailBee.Html
{
	// Token: 0x02000010 RID: 16
	public class TagAttributeCollection : CollectionBase
	{
		// Token: 0x17000035 RID: 53
		public TagAttribute this[int index]
		{
			get
			{
				if (this.IsReparseNeeded)
				{
					this.d();
				}
				return (TagAttribute)base.List[index];
			}
			set
			{
				if (value == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				base.List[index] = value;
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005F71 File Offset: 0x00004F71
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00005F87 File Offset: 0x00004F87
		internal string Definition
		{
			get
			{
				if (this.IsRebuildNeeded)
				{
					this.e();
				}
				return this.a;
			}
			set
			{
				base.List.Clear();
				this.a = value;
				this.IsReparseNeeded = true;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00005FA2 File Offset: 0x00004FA2
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00005FAA File Offset: 0x00004FAA
		internal bool IsReparseNeeded
		{
			get
			{
				return this.b;
			}
			set
			{
				if (!this.c)
				{
					this.b = value;
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00005FBB File Offset: 0x00004FBB
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00005FC3 File Offset: 0x00004FC3
		internal bool IsRebuildNeeded
		{
			get
			{
				return this.d;
			}
			set
			{
				if (!this.e)
				{
					this.d = value;
					if (this.d && this.ParentElement != null)
					{
						this.ParentElement.IsRebuildNeeded = value;
					}
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005FF0 File Offset: 0x00004FF0
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00005FF8 File Offset: 0x00004FF8
		internal Element ParentElement
		{
			get
			{
				return this.f;
			}
			set
			{
				this.f = value;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006001 File Offset: 0x00005001
		public TagAttributeCollection()
		{
			this.ParentElement = null;
			this.a = string.Empty;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006022 File Offset: 0x00005022
		internal TagAttributeCollection(Element A_0)
		{
			this.ParentElement = A_0;
			this.a = string.Empty;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006043 File Offset: 0x00005043
		internal TagAttributeCollection(string A_0, Element A_1)
		{
			this.ParentElement = A_1;
			this.a = A_0;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006060 File Offset: 0x00005060
		public void Add(TagAttribute attr)
		{
			if (attr == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Add(attr);
			attr.ParentCollection = this;
			this.IsRebuildNeeded = true;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006088 File Offset: 0x00005088
		public void Add(TagAttribute attr, int index)
		{
			if (attr == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Insert(index, attr);
			attr.ParentCollection = this;
			this.IsRebuildNeeded = true;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000060B0 File Offset: 0x000050B0
		public void AddRange(TagAttributeCollection attrs)
		{
			if (attrs == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			for (int i = 0; i < attrs.Count; i++)
			{
				base.List.Add(attrs[i]);
				attrs[i].ParentCollection = this;
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00006100 File Offset: 0x00005100
		public void AddRange(TagAttributeCollection attrs, int srcIndex, int count, int destIndex)
		{
			if (attrs == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			for (int i = 0; i < count; i++)
			{
				base.List.Insert(destIndex + i, attrs[srcIndex + i]);
				attrs[i].ParentCollection = this;
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00006150 File Offset: 0x00005150
		public bool Remove(TagAttribute attr)
		{
			if (attr == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			base.List.Remove(attr);
			this.IsRebuildNeeded = true;
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006171 File Offset: 0x00005171
		public new void RemoveAt(int index)
		{
			base.List.RemoveAt(index);
			this.IsRebuildNeeded = true;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006188 File Offset: 0x00005188
		public void RemoveByName(string attrName)
		{
			if (attrName == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			bool flag = false;
			string text = attrName.ToLower();
			for (int i = base.List.Count - 1; i >= 0; i--)
			{
				TagAttribute tagAttribute = this[i];
				if (tagAttribute.Name.ToLower() == text)
				{
					base.List.Remove(tagAttribute);
					flag = true;
				}
			}
			if (flag)
			{
				this.IsRebuildNeeded = true;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000061F4 File Offset: 0x000051F4
		public void RemoveAll()
		{
			base.List.Clear();
			this.IsRebuildNeeded = true;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006208 File Offset: 0x00005208
		internal void d()
		{
			this.e = true;
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			string text = null;
			int num = 0;
			for (int i = 0; i < this.a.Length; i++)
			{
				stringBuilder.Append(this.a[i]);
				switch (num)
				{
				case 0:
					if (stringBuilder.ToString().Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty) != string.Empty && this.a[i].ToString().IndexOfAny(TagAttributeCollection.h) != -1)
					{
						if (this.a[i] == '=')
						{
							num = 2;
						}
						else
						{
							num = 1;
						}
					}
					break;
				case 1:
					if (this.a[i] == '=')
					{
						num = 2;
					}
					else if (this.a[i].ToString().IndexOfAny(TagAttributeCollection.g) != -1)
					{
						num = 0;
						this.Add(new TagAttribute(stringBuilder.ToString(), this));
						stringBuilder = new StringBuilder();
					}
					break;
				case 2:
					if (text == null && this.a.Substring(i, 1).IndexOfAny(TagAttributeCollection.g) == -1)
					{
						text = ((this.a[i] == '"' || this.a[i] == '\'') ? this.a[i].ToString() : " ");
					}
					else if ((text == " " && this.a.Substring(i, 1).IndexOfAny(TagAttributeCollection.g) != -1) || ((text == "'" || text == "\"") && text == this.a[i].ToString() && this.a[i - 1] != '\\'))
					{
						num = 0;
						this.Add(new TagAttribute(stringBuilder.ToString().Trim(), this));
						stringBuilder = new StringBuilder(string.Empty);
						text = null;
					}
					break;
				}
				if (i == this.a.Length - 1 && stringBuilder.ToString().Trim() != string.Empty && stringBuilder.ToString().Replace(" ", string.Empty).Replace("\t", string.Empty).Replace("\n", string.Empty).Replace("\r", string.Empty) != string.Empty)
				{
					this.Add(new TagAttribute(stringBuilder.ToString(), this));
				}
			}
			this.IsReparseNeeded = false;
			this.e = false;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000064EC File Offset: 0x000054EC
		internal void e()
		{
			this.c = true;
			bool flag = false;
			StringBuilder stringBuilder = new StringBuilder(string.Empty);
			if (base.Count > 0)
			{
				foreach (object obj in base.List)
				{
					TagAttribute tagAttribute = (TagAttribute)obj;
					if (tagAttribute.Name == "/")
					{
						flag = true;
					}
					else
					{
						stringBuilder.Append(" ").Append(tagAttribute.Name);
						if (tagAttribute.Value != null)
						{
							stringBuilder.Append("=").Append(tagAttribute.Value);
						}
					}
				}
				if (flag)
				{
					stringBuilder.Append(" ").Append("/");
				}
			}
			this.Definition = stringBuilder.ToString();
			this.c = false;
			this.IsRebuildNeeded = false;
			this.IsReparseNeeded = false;
		}

		// Token: 0x04000054 RID: 84
		private string a;

		// Token: 0x04000055 RID: 85
		private bool b = true;

		// Token: 0x04000056 RID: 86
		private bool c;

		// Token: 0x04000057 RID: 87
		private bool d;

		// Token: 0x04000058 RID: 88
		private bool e;

		// Token: 0x04000059 RID: 89
		private Element f;

		// Token: 0x0400005A RID: 90
		private static readonly char[] g = " \t\r\n".ToCharArray();

		// Token: 0x0400005B RID: 91
		private static readonly char[] h = " \t\r\n=".ToCharArray();

		// Token: 0x02000011 RID: 17
		private enum a
		{
			// Token: 0x0400005D RID: 93
			a,
			// Token: 0x0400005E RID: 94
			b,
			// Token: 0x0400005F RID: 95
			c
		}
	}
}
