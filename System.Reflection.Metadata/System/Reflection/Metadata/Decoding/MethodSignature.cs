using System;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x0200014D RID: 333
	internal struct MethodSignature<TType>
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x0001E5B6 File Offset: 0x0001C7B6
		public MethodSignature(SignatureHeader header, TType returnType, int requiredParameterCount, int genericParameterCount, ImmutableArray<TType> parameterTypes)
		{
			this._header = header;
			this._returnType = returnType;
			this._genericParameterCount = genericParameterCount;
			this._requiredParameterCount = requiredParameterCount;
			this._parameterTypes = parameterTypes;
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0001E5DD File Offset: 0x0001C7DD
		public SignatureHeader Header
		{
			get
			{
				return this._header;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0001E5E5 File Offset: 0x0001C7E5
		public TType ReturnType
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0001E5ED File Offset: 0x0001C7ED
		public int RequiredParameterCount
		{
			get
			{
				return this._requiredParameterCount;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0001E5F5 File Offset: 0x0001C7F5
		public int GenericParameterCount
		{
			get
			{
				return this._genericParameterCount;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0001E5FD File Offset: 0x0001C7FD
		public ImmutableArray<TType> ParameterTypes
		{
			get
			{
				return this._parameterTypes;
			}
		}

		// Token: 0x040008D4 RID: 2260
		private readonly SignatureHeader _header;

		// Token: 0x040008D5 RID: 2261
		private readonly TType _returnType;

		// Token: 0x040008D6 RID: 2262
		private readonly int _requiredParameterCount;

		// Token: 0x040008D7 RID: 2263
		private readonly int _genericParameterCount;

		// Token: 0x040008D8 RID: 2264
		private readonly ImmutableArray<TType> _parameterTypes;
	}
}
