using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x0200027B RID: 635
	internal sealed class CompilerScope
	{
		// Token: 0x0600168A RID: 5770 RVA: 0x0004A7B4 File Offset: 0x000489B4
		internal CompilerScope(object node, bool isMethod)
		{
			this.Node = node;
			this.IsMethod = isMethod;
			IList<ParameterExpression> variables = CompilerScope.GetVariables(node);
			this.Definitions = new Dictionary<ParameterExpression, VariableStorageKind>(variables.Count);
			foreach (ParameterExpression key in variables)
			{
				this.Definitions.Add(key, VariableStorageKind.Local);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600168B RID: 5771 RVA: 0x0004A844 File Offset: 0x00048A44
		internal HoistedLocals NearestHoistedLocals
		{
			get
			{
				return this._hoistedLocals ?? this._closureHoistedLocals;
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0004A858 File Offset: 0x00048A58
		internal CompilerScope Enter(LambdaCompiler lc, CompilerScope parent)
		{
			this.SetParent(lc, parent);
			this.AllocateLocals(lc);
			if (this.IsMethod && this._closureHoistedLocals != null)
			{
				this.EmitClosureAccess(lc, this._closureHoistedLocals);
			}
			this.EmitNewHoistedLocals(lc);
			if (this.IsMethod)
			{
				this.EmitCachedVariables();
			}
			return this;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0004A8A8 File Offset: 0x00048AA8
		internal CompilerScope Exit()
		{
			if (!this.IsMethod)
			{
				foreach (CompilerScope.Storage storage in this._locals.Values)
				{
					storage.FreeLocal();
				}
			}
			CompilerScope parent = this._parent;
			this._parent = null;
			this._hoistedLocals = null;
			this._closureHoistedLocals = null;
			this._locals.Clear();
			return parent;
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0004A930 File Offset: 0x00048B30
		internal void EmitVariableAccess(LambdaCompiler lc, ReadOnlyCollection<ParameterExpression> vars)
		{
			if (this.NearestHoistedLocals != null)
			{
				List<long> list = new List<long>(vars.Count);
				foreach (ParameterExpression key in vars)
				{
					ulong num = 0UL;
					HoistedLocals hoistedLocals = this.NearestHoistedLocals;
					while (!hoistedLocals.Indexes.ContainsKey(key))
					{
						num += 1UL;
						hoistedLocals = hoistedLocals.Parent;
					}
					ulong item = num << 32 | (ulong)hoistedLocals.Indexes[key];
					list.Add((long)item);
				}
				if (list.Count > 0)
				{
					this.EmitGet(this.NearestHoistedLocals.SelfVariable);
					lc.EmitConstantArray<long>(list.ToArray());
					lc.IL.Emit(OpCodes.Call, typeof(RuntimeOps).GetMethod("CreateRuntimeVariables", new Type[]
					{
						typeof(object[]),
						typeof(long[])
					}));
					return;
				}
			}
			lc.IL.Emit(OpCodes.Call, typeof(RuntimeOps).GetMethod("CreateRuntimeVariables", Type.EmptyTypes));
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0004AA64 File Offset: 0x00048C64
		internal void AddLocal(LambdaCompiler gen, ParameterExpression variable)
		{
			this._locals.Add(variable, new CompilerScope.LocalStorage(gen, variable));
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0004AA79 File Offset: 0x00048C79
		internal void EmitGet(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitLoad();
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0004AA87 File Offset: 0x00048C87
		internal void EmitSet(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitStore();
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0004AA95 File Offset: 0x00048C95
		internal void EmitAddressOf(ParameterExpression variable)
		{
			this.ResolveVariable(variable).EmitAddress();
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0004AAA3 File Offset: 0x00048CA3
		private CompilerScope.Storage ResolveVariable(ParameterExpression variable)
		{
			return this.ResolveVariable(variable, this.NearestHoistedLocals);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0004AAB4 File Offset: 0x00048CB4
		private CompilerScope.Storage ResolveVariable(ParameterExpression variable, HoistedLocals hoistedLocals)
		{
			for (CompilerScope compilerScope = this; compilerScope != null; compilerScope = compilerScope._parent)
			{
				CompilerScope.Storage result;
				if (compilerScope._locals.TryGetValue(variable, out result))
				{
					return result;
				}
				if (compilerScope.IsMethod)
				{
					break;
				}
			}
			for (HoistedLocals hoistedLocals2 = hoistedLocals; hoistedLocals2 != null; hoistedLocals2 = hoistedLocals2.Parent)
			{
				int index;
				if (hoistedLocals2.Indexes.TryGetValue(variable, out index))
				{
					return new CompilerScope.ElementBoxStorage(this.ResolveVariable(hoistedLocals2.SelfVariable, hoistedLocals), index, variable);
				}
			}
			throw Error.UndefinedVariable(variable.Name, variable.Type, this.CurrentLambdaName);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x0004AB34 File Offset: 0x00048D34
		private void SetParent(LambdaCompiler lc, CompilerScope parent)
		{
			this._parent = parent;
			if (this.NeedsClosure && this._parent != null)
			{
				this._closureHoistedLocals = this._parent.NearestHoistedLocals;
			}
			ReadOnlyCollection<ParameterExpression> readOnlyCollection = (from p in this.GetVariables()
			where this.Definitions[p] == VariableStorageKind.Hoisted
			select p).ToReadOnly<ParameterExpression>();
			if (readOnlyCollection.Count > 0)
			{
				this._hoistedLocals = new HoistedLocals(this._closureHoistedLocals, readOnlyCollection);
				this.AddLocal(lc, this._hoistedLocals.SelfVariable);
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0004ABB4 File Offset: 0x00048DB4
		private void EmitNewHoistedLocals(LambdaCompiler lc)
		{
			if (this._hoistedLocals == null)
			{
				return;
			}
			lc.IL.EmitInt(this._hoistedLocals.Variables.Count);
			lc.IL.Emit(OpCodes.Newarr, typeof(object));
			int num = 0;
			foreach (ParameterExpression parameterExpression in this._hoistedLocals.Variables)
			{
				lc.IL.Emit(OpCodes.Dup);
				lc.IL.EmitInt(num++);
				Type type = typeof(StrongBox<>).MakeGenericType(new Type[]
				{
					parameterExpression.Type
				});
				if (this.IsMethod && lc.Parameters.Contains(parameterExpression))
				{
					int index = lc.Parameters.IndexOf(parameterExpression);
					lc.EmitLambdaArgument(index);
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(new Type[]
					{
						parameterExpression.Type
					}));
				}
				else if (parameterExpression == this._hoistedLocals.ParentVariable)
				{
					this.ResolveVariable(parameterExpression, this._closureHoistedLocals).EmitLoad();
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(new Type[]
					{
						parameterExpression.Type
					}));
				}
				else
				{
					lc.IL.Emit(OpCodes.Newobj, type.GetConstructor(Type.EmptyTypes));
				}
				if (this.ShouldCache(parameterExpression))
				{
					lc.IL.Emit(OpCodes.Dup);
					this.CacheBoxToLocal(lc, parameterExpression);
				}
				lc.IL.Emit(OpCodes.Stelem_Ref);
			}
			this.EmitSet(this._hoistedLocals.SelfVariable);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0004AD88 File Offset: 0x00048F88
		private void EmitCachedVariables()
		{
			if (this.ReferenceCount == null)
			{
				return;
			}
			foreach (KeyValuePair<ParameterExpression, int> keyValuePair in this.ReferenceCount)
			{
				if (this.ShouldCache(keyValuePair.Key, keyValuePair.Value))
				{
					CompilerScope.ElementBoxStorage elementBoxStorage = this.ResolveVariable(keyValuePair.Key) as CompilerScope.ElementBoxStorage;
					if (elementBoxStorage != null)
					{
						elementBoxStorage.EmitLoadBox();
						this.CacheBoxToLocal(elementBoxStorage.Compiler, keyValuePair.Key);
					}
				}
			}
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0004AE24 File Offset: 0x00049024
		private bool ShouldCache(ParameterExpression v, int refCount)
		{
			return refCount > 2 && !this._locals.ContainsKey(v);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x0004AE3C File Offset: 0x0004903C
		private bool ShouldCache(ParameterExpression v)
		{
			int refCount;
			return this.ReferenceCount != null && this.ReferenceCount.TryGetValue(v, out refCount) && this.ShouldCache(v, refCount);
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0004AE70 File Offset: 0x00049070
		private void CacheBoxToLocal(LambdaCompiler lc, ParameterExpression v)
		{
			CompilerScope.LocalBoxStorage localBoxStorage = new CompilerScope.LocalBoxStorage(lc, v);
			localBoxStorage.EmitStoreBox();
			this._locals.Add(v, localBoxStorage);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x0004AE98 File Offset: 0x00049098
		private void EmitClosureAccess(LambdaCompiler lc, HoistedLocals locals)
		{
			if (locals == null)
			{
				return;
			}
			this.EmitClosureToVariable(lc, locals);
			while ((locals = locals.Parent) != null)
			{
				ParameterExpression selfVariable = locals.SelfVariable;
				CompilerScope.LocalStorage localStorage = new CompilerScope.LocalStorage(lc, selfVariable);
				localStorage.EmitStore(this.ResolveVariable(selfVariable));
				this._locals.Add(selfVariable, localStorage);
			}
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0004AEE8 File Offset: 0x000490E8
		private void EmitClosureToVariable(LambdaCompiler lc, HoistedLocals locals)
		{
			lc.EmitClosureArgument();
			lc.IL.Emit(OpCodes.Ldfld, typeof(Closure).GetField("Locals"));
			this.AddLocal(lc, locals.SelfVariable);
			this.EmitSet(locals.SelfVariable);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0004AF38 File Offset: 0x00049138
		private void AllocateLocals(LambdaCompiler lc)
		{
			foreach (ParameterExpression parameterExpression in this.GetVariables())
			{
				if (this.Definitions[parameterExpression] == VariableStorageKind.Local)
				{
					CompilerScope.Storage value;
					if (this.IsMethod && lc.Parameters.Contains(parameterExpression))
					{
						value = new CompilerScope.ArgumentStorage(lc, parameterExpression);
					}
					else
					{
						value = new CompilerScope.LocalStorage(lc, parameterExpression);
					}
					this._locals.Add(parameterExpression, value);
				}
			}
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0004AFC4 File Offset: 0x000491C4
		private IList<ParameterExpression> GetVariables()
		{
			IList<ParameterExpression> variables = CompilerScope.GetVariables(this.Node);
			if (this.MergedScopes == null)
			{
				return variables;
			}
			List<ParameterExpression> list = new List<ParameterExpression>(variables);
			foreach (object scope in this.MergedScopes)
			{
				list.AddRange(CompilerScope.GetVariables(scope));
			}
			return list;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x0004B034 File Offset: 0x00049234
		private static IList<ParameterExpression> GetVariables(object scope)
		{
			LambdaExpression lambdaExpression = scope as LambdaExpression;
			if (lambdaExpression != null)
			{
				return lambdaExpression.Parameters;
			}
			BlockExpression blockExpression = scope as BlockExpression;
			if (blockExpression != null)
			{
				return blockExpression.Variables;
			}
			return new ParameterExpression[]
			{
				((CatchBlock)scope).Variable
			};
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0004B078 File Offset: 0x00049278
		private string CurrentLambdaName
		{
			get
			{
				LambdaExpression lambdaExpression;
				do
				{
					lambdaExpression = (this.Node as LambdaExpression);
				}
				while (lambdaExpression == null);
				return lambdaExpression.Name;
			}
		}

		// Token: 0x04000B37 RID: 2871
		private CompilerScope _parent;

		// Token: 0x04000B38 RID: 2872
		internal readonly object Node;

		// Token: 0x04000B39 RID: 2873
		internal readonly bool IsMethod;

		// Token: 0x04000B3A RID: 2874
		internal bool NeedsClosure;

		// Token: 0x04000B3B RID: 2875
		internal readonly Dictionary<ParameterExpression, VariableStorageKind> Definitions = new Dictionary<ParameterExpression, VariableStorageKind>();

		// Token: 0x04000B3C RID: 2876
		internal Dictionary<ParameterExpression, int> ReferenceCount;

		// Token: 0x04000B3D RID: 2877
		internal Set<object> MergedScopes;

		// Token: 0x04000B3E RID: 2878
		private HoistedLocals _hoistedLocals;

		// Token: 0x04000B3F RID: 2879
		private HoistedLocals _closureHoistedLocals;

		// Token: 0x04000B40 RID: 2880
		private readonly Dictionary<ParameterExpression, CompilerScope.Storage> _locals = new Dictionary<ParameterExpression, CompilerScope.Storage>();

		// Token: 0x0200044A RID: 1098
		private abstract class Storage
		{
			// Token: 0x06001FAD RID: 8109 RVA: 0x0006EE65 File Offset: 0x0006D065
			internal Storage(LambdaCompiler compiler, ParameterExpression variable)
			{
				this.Compiler = compiler;
				this.Variable = variable;
			}

			// Token: 0x06001FAE RID: 8110
			internal abstract void EmitLoad();

			// Token: 0x06001FAF RID: 8111
			internal abstract void EmitAddress();

			// Token: 0x06001FB0 RID: 8112
			internal abstract void EmitStore();

			// Token: 0x06001FB1 RID: 8113 RVA: 0x0006EE7B File Offset: 0x0006D07B
			internal virtual void EmitStore(CompilerScope.Storage value)
			{
				value.EmitLoad();
				this.EmitStore();
			}

			// Token: 0x06001FB2 RID: 8114 RVA: 0x0006EE89 File Offset: 0x0006D089
			internal virtual void FreeLocal()
			{
			}

			// Token: 0x040012CA RID: 4810
			internal readonly LambdaCompiler Compiler;

			// Token: 0x040012CB RID: 4811
			internal readonly ParameterExpression Variable;
		}

		// Token: 0x0200044B RID: 1099
		private sealed class LocalStorage : CompilerScope.Storage
		{
			// Token: 0x06001FB3 RID: 8115 RVA: 0x0006EE8B File Offset: 0x0006D08B
			internal LocalStorage(LambdaCompiler compiler, ParameterExpression variable) : base(compiler, variable)
			{
				this._local = compiler.GetNamedLocal(variable.IsByRef ? variable.Type.MakeByRefType() : variable.Type, variable);
			}

			// Token: 0x06001FB4 RID: 8116 RVA: 0x0006EEBD File Offset: 0x0006D0BD
			internal override void EmitLoad()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._local);
			}

			// Token: 0x06001FB5 RID: 8117 RVA: 0x0006EEDA File Offset: 0x0006D0DA
			internal override void EmitStore()
			{
				this.Compiler.IL.Emit(OpCodes.Stloc, this._local);
			}

			// Token: 0x06001FB6 RID: 8118 RVA: 0x0006EEF7 File Offset: 0x0006D0F7
			internal override void EmitAddress()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloca, this._local);
			}

			// Token: 0x040012CC RID: 4812
			private readonly LocalBuilder _local;
		}

		// Token: 0x0200044C RID: 1100
		private sealed class ArgumentStorage : CompilerScope.Storage
		{
			// Token: 0x06001FB7 RID: 8119 RVA: 0x0006EF14 File Offset: 0x0006D114
			internal ArgumentStorage(LambdaCompiler compiler, ParameterExpression p) : base(compiler, p)
			{
				this._argument = compiler.GetLambdaArgument(compiler.Parameters.IndexOf(p));
			}

			// Token: 0x06001FB8 RID: 8120 RVA: 0x0006EF36 File Offset: 0x0006D136
			internal override void EmitLoad()
			{
				this.Compiler.IL.EmitLoadArg(this._argument);
			}

			// Token: 0x06001FB9 RID: 8121 RVA: 0x0006EF4E File Offset: 0x0006D14E
			internal override void EmitStore()
			{
				this.Compiler.IL.EmitStoreArg(this._argument);
			}

			// Token: 0x06001FBA RID: 8122 RVA: 0x0006EF66 File Offset: 0x0006D166
			internal override void EmitAddress()
			{
				this.Compiler.IL.EmitLoadArgAddress(this._argument);
			}

			// Token: 0x040012CD RID: 4813
			private readonly int _argument;
		}

		// Token: 0x0200044D RID: 1101
		private sealed class ElementBoxStorage : CompilerScope.Storage
		{
			// Token: 0x06001FBB RID: 8123 RVA: 0x0006EF80 File Offset: 0x0006D180
			internal ElementBoxStorage(CompilerScope.Storage array, int index, ParameterExpression variable) : base(array.Compiler, variable)
			{
				this._array = array;
				this._index = index;
				this._boxType = typeof(StrongBox<>).MakeGenericType(new Type[]
				{
					variable.Type
				});
				this._boxValueField = this._boxType.GetField("Value");
			}

			// Token: 0x06001FBC RID: 8124 RVA: 0x0006EFE2 File Offset: 0x0006D1E2
			internal override void EmitLoad()
			{
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldfld, this._boxValueField);
			}

			// Token: 0x06001FBD RID: 8125 RVA: 0x0006F008 File Offset: 0x0006D208
			internal override void EmitStore()
			{
				LocalBuilder local = this.Compiler.GetLocal(this.Variable.Type);
				this.Compiler.IL.Emit(OpCodes.Stloc, local);
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldloc, local);
				this.Compiler.FreeLocal(local);
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001FBE RID: 8126 RVA: 0x0006F085 File Offset: 0x0006D285
			internal override void EmitStore(CompilerScope.Storage value)
			{
				this.EmitLoadBox();
				value.EmitLoad();
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001FBF RID: 8127 RVA: 0x0006F0AE File Offset: 0x0006D2AE
			internal override void EmitAddress()
			{
				this.EmitLoadBox();
				this.Compiler.IL.Emit(OpCodes.Ldflda, this._boxValueField);
			}

			// Token: 0x06001FC0 RID: 8128 RVA: 0x0006F0D4 File Offset: 0x0006D2D4
			internal void EmitLoadBox()
			{
				this._array.EmitLoad();
				this.Compiler.IL.EmitInt(this._index);
				this.Compiler.IL.Emit(OpCodes.Ldelem_Ref);
				this.Compiler.IL.Emit(OpCodes.Castclass, this._boxType);
			}

			// Token: 0x040012CE RID: 4814
			private readonly int _index;

			// Token: 0x040012CF RID: 4815
			private readonly CompilerScope.Storage _array;

			// Token: 0x040012D0 RID: 4816
			private readonly Type _boxType;

			// Token: 0x040012D1 RID: 4817
			private readonly FieldInfo _boxValueField;
		}

		// Token: 0x0200044E RID: 1102
		private sealed class LocalBoxStorage : CompilerScope.Storage
		{
			// Token: 0x06001FC1 RID: 8129 RVA: 0x0006F134 File Offset: 0x0006D334
			internal LocalBoxStorage(LambdaCompiler compiler, ParameterExpression variable) : base(compiler, variable)
			{
				this._boxType = typeof(StrongBox<>).MakeGenericType(new Type[]
				{
					variable.Type
				});
				this._boxValueField = this._boxType.GetField("Value");
				this._boxLocal = compiler.GetNamedLocal(this._boxType, variable);
			}

			// Token: 0x06001FC2 RID: 8130 RVA: 0x0006F196 File Offset: 0x0006D396
			internal override void EmitLoad()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldfld, this._boxValueField);
			}

			// Token: 0x06001FC3 RID: 8131 RVA: 0x0006F1CE File Offset: 0x0006D3CE
			internal override void EmitAddress()
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldflda, this._boxValueField);
			}

			// Token: 0x06001FC4 RID: 8132 RVA: 0x0006F208 File Offset: 0x0006D408
			internal override void EmitStore()
			{
				LocalBuilder local = this.Compiler.GetLocal(this.Variable.Type);
				this.Compiler.IL.Emit(OpCodes.Stloc, local);
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				this.Compiler.IL.Emit(OpCodes.Ldloc, local);
				this.Compiler.FreeLocal(local);
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001FC5 RID: 8133 RVA: 0x0006F29A File Offset: 0x0006D49A
			internal override void EmitStore(CompilerScope.Storage value)
			{
				this.Compiler.IL.Emit(OpCodes.Ldloc, this._boxLocal);
				value.EmitLoad();
				this.Compiler.IL.Emit(OpCodes.Stfld, this._boxValueField);
			}

			// Token: 0x06001FC6 RID: 8134 RVA: 0x0006F2D8 File Offset: 0x0006D4D8
			internal void EmitStoreBox()
			{
				this.Compiler.IL.Emit(OpCodes.Stloc, this._boxLocal);
			}

			// Token: 0x040012D2 RID: 4818
			private readonly LocalBuilder _boxLocal;

			// Token: 0x040012D3 RID: 4819
			private readonly Type _boxType;

			// Token: 0x040012D4 RID: 4820
			private readonly FieldInfo _boxValueField;
		}
	}
}
