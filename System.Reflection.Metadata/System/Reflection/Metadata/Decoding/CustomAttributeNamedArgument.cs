using System;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000140 RID: 320
	internal struct CustomAttributeNamedArgument<TType>
	{
		// Token: 0x06000A57 RID: 2647 RVA: 0x0001DF25 File Offset: 0x0001C125
		public CustomAttributeNamedArgument(string name, CustomAttributeNamedArgumentKind kind, TType type, object value)
		{
			this._name = name;
			this._kind = kind;
			this._type = type;
			this._value = value;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0001DF44 File Offset: 0x0001C144
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0001DF4C File Offset: 0x0001C14C
		public CustomAttributeNamedArgumentKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0001DF54 File Offset: 0x0001C154
		public TType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0001DF5C File Offset: 0x0001C15C
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040008BF RID: 2239
		private readonly string _name;

		// Token: 0x040008C0 RID: 2240
		private readonly CustomAttributeNamedArgumentKind _kind;

		// Token: 0x040008C1 RID: 2241
		private readonly TType _type;

		// Token: 0x040008C2 RID: 2242
		private readonly object _value;
	}
}
