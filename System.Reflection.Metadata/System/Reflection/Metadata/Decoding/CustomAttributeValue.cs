using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000142 RID: 322
	internal struct CustomAttributeValue<TType>
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x0001DF84 File Offset: 0x0001C184
		public CustomAttributeValue(ImmutableArray<CustomAttributeTypedArgument<TType>> fixedArguments, ImmutableArray<CustomAttributeNamedArgument<TType>> namedArguments)
		{
			this._fixedArguments = fixedArguments;
			this._namedArguments = namedArguments;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x0001DF94 File Offset: 0x0001C194
		public ImmutableArray<CustomAttributeTypedArgument<TType>> FixedArguments
		{
			get
			{
				return this._fixedArguments;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0001DF9C File Offset: 0x0001C19C
		public ImmutableArray<CustomAttributeNamedArgument<TType>> NamedArguments
		{
			get
			{
				return this._namedArguments;
			}
		}

		// Token: 0x040008C5 RID: 2245
		private readonly ImmutableArray<CustomAttributeTypedArgument<TType>> _fixedArguments;

		// Token: 0x040008C6 RID: 2246
		private readonly ImmutableArray<CustomAttributeNamedArgument<TType>> _namedArguments;
	}
}
