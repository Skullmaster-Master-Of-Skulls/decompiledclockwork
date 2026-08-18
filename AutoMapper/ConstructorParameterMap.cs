using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x0200000B RID: 11
	public class ConstructorParameterMap
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002F03 File Offset: 0x00001103
		public ConstructorParameterMap(ParameterInfo parameter, IValueResolver[] sourceResolvers, bool canResolve)
		{
			this.Parameter = parameter;
			this.SourceResolvers = sourceResolvers;
			this.CanResolve = canResolve;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002F20 File Offset: 0x00001120
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002F28 File Offset: 0x00001128
		public ParameterInfo Parameter { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002F31 File Offset: 0x00001131
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002F39 File Offset: 0x00001139
		public IValueResolver[] SourceResolvers { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002F42 File Offset: 0x00001142
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002F4A File Offset: 0x0000114A
		public bool CanResolve { get; set; }

		// Token: 0x06000049 RID: 73 RVA: 0x00002F54 File Offset: 0x00001154
		public ResolutionResult ResolveValue(ResolutionContext context)
		{
			ResolutionResult seed = new ResolutionResult(context);
			return this.SourceResolvers.Aggregate(seed, (ResolutionResult current, IValueResolver resolver) => resolver.Resolve(current));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002F93 File Offset: 0x00001193
		public void ResolveUsing(IEnumerable<IMemberGetter> members)
		{
			this.SourceResolvers = members.ToArray<IMemberGetter>();
		}
	}
}
