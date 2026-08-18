using System;

namespace MailBee.Html
{
	// Token: 0x0200000C RID: 12
	public class Rule
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000059FE File Offset: 0x000049FE
		internal Rule(TagRuleTypes A_0, string A_1, TagAttributeCollection A_2)
		{
			if (A_1 == null || A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.a = A_0;
			this.b = A_1;
			this.c = A_2;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005A34 File Offset: 0x00004A34
		internal Rule(TagRuleTypes A_0, string A_1, TagAttributeCollection A_2, TagAttributeCollection A_3, TagAttributeCollection A_4, bool A_5) : this(A_0, A_1, A_2)
		{
			if (A_5)
			{
				if (A_3 == null || A_4 == null)
				{
					throw new MailBeeInvalidArgumentException(21);
				}
				if (A_3.Count != 1 && A_3.Count != A_4.Count)
				{
					throw new MailBeeInvalidArgumentException(20);
				}
			}
			this.d = A_3;
			this.e = A_4;
			this.h = A_5;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00005A98 File Offset: 0x00004A98
		internal Rule(TagRuleTypes A_0, string A_1, TagAttributeCollection A_2, Element A_3) : this(A_0, A_1, A_2)
		{
			if (A_3 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.f = A_3;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00005AB7 File Offset: 0x00004AB7
		internal Rule(TagRuleTypes A_0, string A_1, TagAttributeCollection A_2, string A_3, bool A_4) : this(A_0, A_1, A_2)
		{
			if (A_3 == null || (A_3 == string.Empty && A_4))
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.g = A_3;
			this.i = A_4;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00005AEF File Offset: 0x00004AEF
		public TagRuleTypes RuleType
		{
			get
			{
				return this.a;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00005AF7 File Offset: 0x00004AF7
		public string TagName
		{
			get
			{
				return this.b;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00005AFF File Offset: 0x00004AFF
		public TagAttributeCollection TagAttributes
		{
			get
			{
				return this.c;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00005B07 File Offset: 0x00004B07
		internal TagAttributeCollection AttrsToAdd
		{
			get
			{
				return this.d;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00005B0F File Offset: 0x00004B0F
		internal TagAttributeCollection AttrsToRemove
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005B17 File Offset: 0x00004B17
		internal Element ReplaceElem
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00005B1F File Offset: 0x00004B1F
		internal string ReplaceStr
		{
			get
			{
				return this.g;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00005B27 File Offset: 0x00004B27
		internal bool ReplaceMode
		{
			get
			{
				return this.h;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00005B2F File Offset: 0x00004B2F
		internal bool ReplaceTagDefinitionOnly
		{
			get
			{
				return this.i;
			}
		}

		// Token: 0x04000048 RID: 72
		private TagRuleTypes a;

		// Token: 0x04000049 RID: 73
		private string b;

		// Token: 0x0400004A RID: 74
		private TagAttributeCollection c;

		// Token: 0x0400004B RID: 75
		private TagAttributeCollection d;

		// Token: 0x0400004C RID: 76
		private TagAttributeCollection e;

		// Token: 0x0400004D RID: 77
		private Element f;

		// Token: 0x0400004E RID: 78
		private string g;

		// Token: 0x0400004F RID: 79
		private bool h;

		// Token: 0x04000050 RID: 80
		private bool i;
	}
}
