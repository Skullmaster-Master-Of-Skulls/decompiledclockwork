using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000279 RID: 633
	internal sealed class BoundConstants
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x0004A49C File Offset: 0x0004869C
		internal int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x0004A4A9 File Offset: 0x000486A9
		internal object[] ToArray()
		{
			return this._values.ToArray();
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0004A4B8 File Offset: 0x000486B8
		internal void AddReference(object value, Type type)
		{
			if (!this._indexes.ContainsKey(value))
			{
				this._indexes.Add(value, this._values.Count);
				this._values.Add(value);
			}
			Helpers.IncrementCount<BoundConstants.TypedConstant>(new BoundConstants.TypedConstant(value, type), this._references);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x0004A508 File Offset: 0x00048708
		internal void EmitConstant(LambdaCompiler lc, object value, Type type)
		{
			if (!lc.CanEmitBoundConstants)
			{
				throw Error.CannotCompileConstant(value);
			}
			LocalBuilder local;
			if (this._cache.TryGetValue(new BoundConstants.TypedConstant(value, type), out local))
			{
				lc.IL.Emit(OpCodes.Ldloc, local);
				return;
			}
			BoundConstants.EmitConstantsArray(lc);
			this.EmitConstantFromArray(lc, value, type);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0004A55C File Offset: 0x0004875C
		internal void EmitCacheConstants(LambdaCompiler lc)
		{
			int num = 0;
			foreach (KeyValuePair<BoundConstants.TypedConstant, int> keyValuePair in this._references)
			{
				if (!lc.CanEmitBoundConstants)
				{
					throw Error.CannotCompileConstant(keyValuePair.Key.Value);
				}
				if (BoundConstants.ShouldCache(keyValuePair.Value))
				{
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			BoundConstants.EmitConstantsArray(lc);
			this._cache.Clear();
			foreach (KeyValuePair<BoundConstants.TypedConstant, int> keyValuePair2 in this._references)
			{
				if (BoundConstants.ShouldCache(keyValuePair2.Value))
				{
					if (--num > 0)
					{
						lc.IL.Emit(OpCodes.Dup);
					}
					LocalBuilder localBuilder = lc.IL.DeclareLocal(keyValuePair2.Key.Type);
					this.EmitConstantFromArray(lc, keyValuePair2.Key.Value, localBuilder.LocalType);
					lc.IL.Emit(OpCodes.Stloc, localBuilder);
					this._cache.Add(keyValuePair2.Key, localBuilder);
				}
			}
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x0004A6AC File Offset: 0x000488AC
		private static bool ShouldCache(int refCount)
		{
			return refCount > 2;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x0004A6B2 File Offset: 0x000488B2
		private static void EmitConstantsArray(LambdaCompiler lc)
		{
			lc.EmitClosureArgument();
			lc.IL.Emit(OpCodes.Ldfld, typeof(Closure).GetField("Constants"));
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0004A6E0 File Offset: 0x000488E0
		private void EmitConstantFromArray(LambdaCompiler lc, object value, Type type)
		{
			int count;
			if (!this._indexes.TryGetValue(value, out count))
			{
				this._indexes.Add(value, count = this._values.Count);
				this._values.Add(value);
			}
			lc.IL.EmitInt(count);
			lc.IL.Emit(OpCodes.Ldelem_Ref);
			if (type.IsValueType)
			{
				lc.IL.Emit(OpCodes.Unbox_Any, type);
				return;
			}
			if (type != typeof(object))
			{
				lc.IL.Emit(OpCodes.Castclass, type);
			}
		}

		// Token: 0x04000B30 RID: 2864
		private readonly List<object> _values = new List<object>();

		// Token: 0x04000B31 RID: 2865
		private readonly Dictionary<object, int> _indexes = new Dictionary<object, int>(ReferenceEqualityComparer<object>.Instance);

		// Token: 0x04000B32 RID: 2866
		private readonly Dictionary<BoundConstants.TypedConstant, int> _references = new Dictionary<BoundConstants.TypedConstant, int>();

		// Token: 0x04000B33 RID: 2867
		private readonly Dictionary<BoundConstants.TypedConstant, LocalBuilder> _cache = new Dictionary<BoundConstants.TypedConstant, LocalBuilder>();

		// Token: 0x02000449 RID: 1097
		private struct TypedConstant : IEquatable<BoundConstants.TypedConstant>
		{
			// Token: 0x06001FA9 RID: 8105 RVA: 0x0006EE01 File Offset: 0x0006D001
			internal TypedConstant(object value, Type type)
			{
				this.Value = value;
				this.Type = type;
			}

			// Token: 0x06001FAA RID: 8106 RVA: 0x0006EE11 File Offset: 0x0006D011
			public override int GetHashCode()
			{
				return RuntimeHelpers.GetHashCode(this.Value) ^ this.Type.GetHashCode();
			}

			// Token: 0x06001FAB RID: 8107 RVA: 0x0006EE2A File Offset: 0x0006D02A
			public bool Equals(BoundConstants.TypedConstant other)
			{
				return this.Value == other.Value && this.Type.Equals(other.Type);
			}

			// Token: 0x06001FAC RID: 8108 RVA: 0x0006EE4D File Offset: 0x0006D04D
			public override bool Equals(object obj)
			{
				return obj is BoundConstants.TypedConstant && this.Equals((BoundConstants.TypedConstant)obj);
			}

			// Token: 0x040012C8 RID: 4808
			internal readonly object Value;

			// Token: 0x040012C9 RID: 4809
			internal readonly Type Type;
		}
	}
}
