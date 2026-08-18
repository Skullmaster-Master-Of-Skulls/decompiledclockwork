using System;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000308 RID: 776
	[Serializable]
	internal sealed class NameValuePair
	{
		// Token: 0x06003115 RID: 12565 RVA: 0x001319B4 File Offset: 0x00130DB4
		internal NameValuePair(string name, string value, int length)
		{
			this._name = name;
			this._value = value;
			this._length = length;
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x001319DC File Offset: 0x00130DDC
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x001319F0 File Offset: 0x00130DF0
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x00131A04 File Offset: 0x00130E04
		// (set) Token: 0x06003119 RID: 12569 RVA: 0x00131A18 File Offset: 0x00130E18
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

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x00131A40 File Offset: 0x00130E40
		internal string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001D71 RID: 7537
		private readonly string _name;

		// Token: 0x04001D72 RID: 7538
		private readonly string _value;

		// Token: 0x04001D73 RID: 7539
		[OptionalField(VersionAdded = 2)]
		private readonly int _length;

		// Token: 0x04001D74 RID: 7540
		private NameValuePair _next;
	}
}
