using System;

namespace AutoMapper.Internal
{
	// Token: 0x0200009A RID: 154
	public class DelegateBasedResolver<TSource, TMember> : IMemberResolver, IValueResolver
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x00012716 File Offset: 0x00010916
		public DelegateBasedResolver(Func<TSource, TMember> method)
		{
			this._method = method;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00012728 File Offset: 0x00010928
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
			TMember tmember = this._method((TSource)((object)source.Value));
			return source.New(tmember, this.MemberType);
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x000127AF File Offset: 0x000109AF
		public Type MemberType
		{
			get
			{
				return typeof(TMember);
			}
		}

		// Token: 0x040000DA RID: 218
		private readonly Func<TSource, TMember> _method;
	}
}
