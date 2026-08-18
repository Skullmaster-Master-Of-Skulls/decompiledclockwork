using System;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000383 RID: 899
	internal sealed class ReturnValue<T>
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x00099A95 File Offset: 0x00097C95
		internal bool Succeeded
		{
			get
			{
				return this._succeeded;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x00099A9D File Offset: 0x00097C9D
		// (set) Token: 0x0600208E RID: 8334 RVA: 0x00099AA5 File Offset: 0x00097CA5
		internal T Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this._succeeded = true;
			}
		}

		// Token: 0x04000B89 RID: 2953
		private bool _succeeded;

		// Token: 0x04000B8A RID: 2954
		private T _value;
	}
}
