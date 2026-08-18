using System;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000154 RID: 340
	[Serializable]
	internal sealed class NameValuePair
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x00244DC8 File Offset: 0x002441C8
		internal NameValuePair(string name, string value, int length)
		{
			this._name = name;
			this._value = value;
			this._length = length;
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00244DF8 File Offset: 0x002441F8
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00244E18 File Offset: 0x00244218
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00244E38 File Offset: 0x00244238
		// (set) Token: 0x06001581 RID: 5505 RVA: 0x00244E58 File Offset: 0x00244258
		internal NameValuePair Next
		{
			get
			{
				return this._next;
			}
			set
			{
				if (this._next != null || value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NameValuePairNext);
				}
				this._next = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00244E88 File Offset: 0x00244288
		internal string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04000CB0 RID: 3248
		private readonly string _name;

		// Token: 0x04000CB1 RID: 3249
		private readonly string _value;

		// Token: 0x04000CB2 RID: 3250
		[OptionalField(VersionAdded = 2)]
		private readonly int _length;

		// Token: 0x04000CB3 RID: 3251
		private NameValuePair _next;
	}
}
