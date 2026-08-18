using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F1 RID: 497
	public class ContextItem
	{
		// Token: 0x06001090 RID: 4240 RVA: 0x00046F99 File Offset: 0x00045199
		public ContextItem(Uri name) : this(name, null)
		{
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00046FA3 File Offset: 0x000451A3
		public ContextItem(Uri name, string value) : this(name, value, null)
		{
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00046FB0 File Offset: 0x000451B0
		public ContextItem(Uri name, string value, Uri scope)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("name");
			}
			if (!name.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("name", SR.GetString("ID0013"));
			}
			if (scope != null && !scope.IsAbsoluteUri)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("scope", SR.GetString("ID0013"));
			}
			this._name = name;
			this._scope = scope;
			this._value = value;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x0004703E File Offset: 0x0004523E
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x00047046 File Offset: 0x00045246
		public Uri Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0004704F File Offset: 0x0004524F
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00047057 File Offset: 0x00045257
		public Uri Scope
		{
			get
			{
				return this._scope;
			}
			set
			{
				if (value != null && !value.IsAbsoluteUri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID0013"));
				}
				this._scope = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x0004708B File Offset: 0x0004528B
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x00047093 File Offset: 0x00045293
		public string Value
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

		// Token: 0x04000E67 RID: 3687
		private Uri _name;

		// Token: 0x04000E68 RID: 3688
		private Uri _scope;

		// Token: 0x04000E69 RID: 3689
		private string _value;
	}
}
