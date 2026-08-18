using System;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000141 RID: 321
	internal struct CustomAttributeTypedArgument<TType>
	{
		// Token: 0x06000A5C RID: 2652 RVA: 0x0001DF64 File Offset: 0x0001C164
		public CustomAttributeTypedArgument(TType type, object value)
		{
			this._type = type;
			this._value = value;
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x0001DF74 File Offset: 0x0001C174
		public TType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0001DF7C File Offset: 0x0001C17C
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040008C3 RID: 2243
		private readonly TType _type;

		// Token: 0x040008C4 RID: 2244
		private readonly object _value;
	}
}
