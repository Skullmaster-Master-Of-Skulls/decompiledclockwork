using System;

namespace System.ComponentModel
{
	// Token: 0x020005C0 RID: 1472
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ParenthesizePropertyNameAttribute : Attribute
	{
		// Token: 0x0600372B RID: 14123 RVA: 0x000EFF23 File Offset: 0x000EE123
		public ParenthesizePropertyNameAttribute() : this(false)
		{
		}

		// Token: 0x0600372C RID: 14124 RVA: 0x000EFF2C File Offset: 0x000EE12C
		public ParenthesizePropertyNameAttribute(bool needParenthesis)
		{
			this.needParenthesis = needParenthesis;
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x000EFF3B File Offset: 0x000EE13B
		public bool NeedParenthesis
		{
			get
			{
				return this.needParenthesis;
			}
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x000EFF43 File Offset: 0x000EE143
		public override bool Equals(object o)
		{
			return o is ParenthesizePropertyNameAttribute && ((ParenthesizePropertyNameAttribute)o).NeedParenthesis == this.needParenthesis;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x000EFF62 File Offset: 0x000EE162
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x000EFF6A File Offset: 0x000EE16A
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParenthesizePropertyNameAttribute.Default);
		}

		// Token: 0x04002AD4 RID: 10964
		public static readonly ParenthesizePropertyNameAttribute Default = new ParenthesizePropertyNameAttribute();

		// Token: 0x04002AD5 RID: 10965
		private bool needParenthesis;
	}
}
