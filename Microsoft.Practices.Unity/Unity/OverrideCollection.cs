using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000012 RID: 18
	[SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "Considered, it's fine.")]
	public abstract class OverrideCollection<TOverride, TKey, TValue> : ResolverOverride, IEnumerable<TOverride>, IEnumerable where TOverride : ResolverOverride
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002841 File Offset: 0x00000A41
		public void Add(TKey key, TValue value)
		{
			this.overrides.Add(this.MakeOverride(key, value));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000285B File Offset: 0x00000A5B
		public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
		{
			return this.overrides.GetResolver(context, dependencyType);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000286A File Offset: 0x00000A6A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029B0 File Offset: 0x00000BB0
		public IEnumerator<TOverride> GetEnumerator()
		{
			foreach (ResolverOverride o in this.overrides)
			{
				yield return (TOverride)((object)o);
			}
			yield break;
		}

		// Token: 0x0600003C RID: 60
		protected abstract TOverride MakeOverride(TKey key, TValue value);

		// Token: 0x04000010 RID: 16
		private readonly CompositeResolverOverride overrides = new CompositeResolverOverride();
	}
}
