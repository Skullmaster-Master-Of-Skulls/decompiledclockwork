using System;
using System.Diagnostics;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000024 RID: 36
	[DebuggerDisplay("{Name} = {Value}")]
	public sealed class ConfigurationEnumValue
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00005FAE File Offset: 0x00004FAE
		internal ConfigurationEnumValue(IAppHostConstantValue value)
		{
			this._value = value;
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00005FBD File Offset: 0x00004FBD
		public string Name
		{
			get
			{
				return this._value.Name;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00005FCA File Offset: 0x00004FCA
		public long Value
		{
			get
			{
				return (long)((ulong)this._value.Value);
			}
		}

		// Token: 0x04000066 RID: 102
		private IAppHostConstantValue _value;
	}
}
