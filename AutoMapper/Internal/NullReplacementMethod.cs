using System;

namespace AutoMapper.Internal
{
	// Token: 0x020000B1 RID: 177
	public class NullReplacementMethod : IValueResolver
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x00013D1F File Offset: 0x00011F1F
		public NullReplacementMethod(object nullSubstitute)
		{
			this._nullSubstitute = nullSubstitute;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00013D2E File Offset: 0x00011F2E
		public ResolutionResult Resolve(ResolutionResult source)
		{
			if (this._nullSubstitute == null)
			{
				return source;
			}
			if (source.Value != null)
			{
				return source;
			}
			return source.New(this._nullSubstitute);
		}

		// Token: 0x040000ED RID: 237
		private readonly object _nullSubstitute;
	}
}
