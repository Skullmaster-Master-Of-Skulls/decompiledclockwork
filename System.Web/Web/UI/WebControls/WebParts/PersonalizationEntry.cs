using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006DF RID: 1759
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PersonalizationEntry
	{
		// Token: 0x06005660 RID: 22112 RVA: 0x0015CB71 File Offset: 0x0015BB71
		public PersonalizationEntry(object value, PersonalizationScope scope) : this(value, scope, false)
		{
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x0015CB7C File Offset: 0x0015BB7C
		public PersonalizationEntry(object value, PersonalizationScope scope, bool isSensitive)
		{
			PersonalizationProviderHelper.CheckPersonalizationScope(scope);
			this._value = value;
			this._scope = scope;
			this._isSensitive = isSensitive;
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06005662 RID: 22114 RVA: 0x0015CB9F File Offset: 0x0015BB9F
		// (set) Token: 0x06005663 RID: 22115 RVA: 0x0015CBA7 File Offset: 0x0015BBA7
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

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06005664 RID: 22116 RVA: 0x0015CBC3 File Offset: 0x0015BBC3
		// (set) Token: 0x06005665 RID: 22117 RVA: 0x0015CBCB File Offset: 0x0015BBCB
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

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06005666 RID: 22118 RVA: 0x0015CBD4 File Offset: 0x0015BBD4
		// (set) Token: 0x06005667 RID: 22119 RVA: 0x0015CBDC File Offset: 0x0015BBDC
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

		// Token: 0x04002F57 RID: 12119
		private PersonalizationScope _scope;

		// Token: 0x04002F58 RID: 12120
		private object _value;

		// Token: 0x04002F59 RID: 12121
		private bool _isSensitive;
	}
}
