using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000559 RID: 1369
	public sealed class PersonalizationEntry
	{
		// Token: 0x060045A5 RID: 17829 RVA: 0x000E5A39 File Offset: 0x000E3C39
		public PersonalizationEntry(object value, PersonalizationScope scope) : this(value, scope, false)
		{
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x000E5A44 File Offset: 0x000E3C44
		public PersonalizationEntry(object value, PersonalizationScope scope, bool isSensitive)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			this._value = value;
			this._scope = scope;
			this._isSensitive = isSensitive;
		}

		// Token: 0x1700148A RID: 5258
		// (get) Token: 0x060045A7 RID: 17831 RVA: 0x000E5A67 File Offset: 0x000E3C67
		// (set) Token: 0x060045A8 RID: 17832 RVA: 0x000E5A6F File Offset: 0x000E3C6F
		public PersonalizationScope Scope
		{
			get
			{
				return this._scope;
			}
			set
			{
				if (value < PersonalizationScope.User || value > PersonalizationScope.Shared)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._scope = value;
			}
		}

		// Token: 0x1700148B RID: 5259
		// (get) Token: 0x060045A9 RID: 17833 RVA: 0x000E5A8B File Offset: 0x000E3C8B
		// (set) Token: 0x060045AA RID: 17834 RVA: 0x000E5A93 File Offset: 0x000E3C93
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x1700148C RID: 5260
		// (get) Token: 0x060045AB RID: 17835 RVA: 0x000E5A9C File Offset: 0x000E3C9C
		// (set) Token: 0x060045AC RID: 17836 RVA: 0x000E5AA4 File Offset: 0x000E3CA4
		public bool IsSensitive
		{
			get
			{
				return this._isSensitive;
			}
			set
			{
				this._isSensitive = value;
			}
		}

		// Token: 0x04002674 RID: 9844
		private PersonalizationScope _scope;

		// Token: 0x04002675 RID: 9845
		private object _value;

		// Token: 0x04002676 RID: 9846
		private bool _isSensitive;
	}
}
