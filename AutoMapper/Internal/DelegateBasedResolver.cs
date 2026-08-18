using System;

namespace AutoMapper.Internal
{
	// Token: 0x02000099 RID: 153
	public class DelegateBasedResolver<TSource> : IValueResolver
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x00012694 File Offset: 0x00010894
		public DelegateBasedResolver(Func<ResolutionResult, object> method)
		{
			this._method = method;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000126A4 File Offset: 0x000108A4
		public ResolutionResult Resolve(ResolutionResult source)
		{
			if (source.Value != null && !(source.Value is TSource))
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"Expected obj to be of type ",
					typeof(TSource),
					" but was ",
					source.Value.GetType()
				}));
			}
			object value = this._method(source);
			return source.New(value);
		}

		// Token: 0x040000D9 RID: 217
		private readonly Func<ResolutionResult, object> _method;
	}
}
