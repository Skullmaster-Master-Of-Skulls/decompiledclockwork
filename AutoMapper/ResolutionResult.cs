using System;

namespace AutoMapper
{
	// Token: 0x02000038 RID: 56
	public class ResolutionResult
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00005BBE File Offset: 0x00003DBE
		public ResolutionResult(ResolutionContext context) : this(context.SourceValue, context, context.SourceType)
		{
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00005BD3 File Offset: 0x00003DD3
		private ResolutionResult(object value, ResolutionContext context, Type memberType)
		{
			this.Value = value;
			this.Context = context;
			this.Type = ResolutionResult.ResolveType(value, memberType);
			this.MemberType = memberType;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00005BFD File Offset: 0x00003DFD
		private ResolutionResult(object value, ResolutionContext context)
		{
			this.Value = value;
			this.Context = context;
			this.Type = ResolutionResult.ResolveType(value, typeof(object));
			this.MemberType = this.Type;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00005C35 File Offset: 0x00003E35
		public object Value { get; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00005C3D File Offset: 0x00003E3D
		public Type Type { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00005C45 File Offset: 0x00003E45
		public Type MemberType { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00005C4D File Offset: 0x00003E4D
		public ResolutionContext Context { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00005C55 File Offset: 0x00003E55
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00005C5D File Offset: 0x00003E5D
		public bool ShouldIgnore { get; set; }

		// Token: 0x06000263 RID: 611 RVA: 0x00005C66 File Offset: 0x00003E66
		private static Type ResolveType(object value, Type memberType)
		{
			if (value != null)
			{
				return value.GetType();
			}
			return memberType;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00005C73 File Offset: 0x00003E73
		public ResolutionResult Ignore()
		{
			return new ResolutionResult(this.Context)
			{
				ShouldIgnore = true
			};
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00005C87 File Offset: 0x00003E87
		public ResolutionResult New(object value)
		{
			return new ResolutionResult(value, this.Context);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00005C95 File Offset: 0x00003E95
		public ResolutionResult New(object value, Type memberType)
		{
			return new ResolutionResult(value, this.Context, memberType);
		}
	}
}
