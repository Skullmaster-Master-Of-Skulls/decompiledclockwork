using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005E1 RID: 1505
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class HelpKeywordAttribute : Attribute
	{
		// Token: 0x060037D9 RID: 14297 RVA: 0x000F1269 File Offset: 0x000EF469
		public HelpKeywordAttribute()
		{
		}

		// Token: 0x060037DA RID: 14298 RVA: 0x000F1271 File Offset: 0x000EF471
		public HelpKeywordAttribute(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			this.contextKeyword = keyword;
		}

		// Token: 0x060037DB RID: 14299 RVA: 0x000F128E File Offset: 0x000EF48E
		public HelpKeywordAttribute(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			this.contextKeyword = t.FullName;
		}

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x060037DC RID: 14300 RVA: 0x000F12B6 File Offset: 0x000EF4B6
		public string HelpKeyword
		{
			get
			{
				return this.contextKeyword;
			}
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000F12BE File Offset: 0x000EF4BE
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is HelpKeywordAttribute && ((HelpKeywordAttribute)obj).HelpKeyword == this.HelpKeyword);
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000F12E9 File Offset: 0x000EF4E9
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000F12F1 File Offset: 0x000EF4F1
		public override bool IsDefaultAttribute()
		{
			return this.Equals(HelpKeywordAttribute.Default);
		}

		// Token: 0x04002B0E RID: 11022
		public static readonly HelpKeywordAttribute Default = new HelpKeywordAttribute();

		// Token: 0x04002B0F RID: 11023
		private string contextKeyword;
	}
}
