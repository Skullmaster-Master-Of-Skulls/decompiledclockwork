using System;

namespace AutoMapper
{
	// Token: 0x02000053 RID: 83
	public class NullReferenceExceptionSwallowingResolver : IMemberResolver, IValueResolver
	{
		// Token: 0x0600032F RID: 815 RVA: 0x00007ECA File Offset: 0x000060CA
		public NullReferenceExceptionSwallowingResolver(IMemberResolver inner)
		{
			this._inner = inner;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00007EDC File Offset: 0x000060DC
		public ResolutionResult Resolve(ResolutionResult source)
		{
			ResolutionResult result;
			try
			{
				result = this._inner.Resolve(source);
			}
			catch (NullReferenceException)
			{
				result = source.New(null, this.MemberType);
			}
			return result;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00007F1C File Offset: 0x0000611C
		public Type MemberType
		{
			get
			{
				return this._inner.MemberType;
			}
		}

		// Token: 0x040000A2 RID: 162
		private readonly IMemberResolver _inner;
	}
}
