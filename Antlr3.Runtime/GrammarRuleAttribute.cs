using System;

namespace Antlr.Runtime
{
	// Token: 0x0200001F RID: 31
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class GrammarRuleAttribute : Attribute
	{
		// Token: 0x06000180 RID: 384 RVA: 0x00005044 File Offset: 0x00003244
		public GrammarRuleAttribute(string name)
		{
			this._name = name;
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005053 File Offset: 0x00003253
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly string _name;
	}
}
