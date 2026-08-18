using System;
using System.Collections.Generic;
using System.Dynamic.Utils;
using System.Reflection.Emit;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000280 RID: 640
	internal sealed class LabelInfo
	{
		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x0004D03E File Offset: 0x0004B23E
		internal Label Label
		{
			get
			{
				this.EnsureLabelAndValue();
				return this._label;
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0004D04C File Offset: 0x0004B24C
		internal LabelInfo(ILGenerator il, LabelTarget node, bool canReturn)
		{
			this._ilg = il;
			this._node = node;
			this._canReturn = canReturn;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0004D08A File Offset: 0x0004B28A
		internal bool CanReturn
		{
			get
			{
				return this._canReturn;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x0004D092 File Offset: 0x0004B292
		internal bool CanBranch
		{
			get
			{
				return this._opCode != OpCodes.Leave;
			}
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x0004D0A4 File Offset: 0x0004B2A4
		internal void Reference(LabelScopeInfo block)
		{
			this._references.Add(block);
			if (this._definitions.Count > 0)
			{
				this.ValidateJump(block);
			}
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x0004D0C8 File Offset: 0x0004B2C8
		internal void Define(LabelScopeInfo block)
		{
			for (LabelScopeInfo labelScopeInfo = block; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (labelScopeInfo.ContainsTarget(this._node))
				{
					throw Error.LabelTargetAlreadyDefined(this._node.Name);
				}
			}
			this._definitions.Add(block);
			block.AddLabelInfo(this._node, this);
			if (this._definitions.Count == 1)
			{
				using (List<LabelScopeInfo>.Enumerator enumerator = this._references.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						LabelScopeInfo reference = enumerator.Current;
						this.ValidateJump(reference);
					}
					return;
				}
			}
			if (this._acrossBlockJump)
			{
				throw Error.AmbiguousJump(this._node.Name);
			}
			this._labelDefined = false;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0004D190 File Offset: 0x0004B390
		private void ValidateJump(LabelScopeInfo reference)
		{
			this._opCode = (this._canReturn ? OpCodes.Ret : OpCodes.Br);
			for (LabelScopeInfo labelScopeInfo = reference; labelScopeInfo != null; labelScopeInfo = labelScopeInfo.Parent)
			{
				if (this._definitions.Contains(labelScopeInfo))
				{
					return;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Finally || labelScopeInfo.Kind == LabelScopeKind.Filter)
				{
					break;
				}
				if (labelScopeInfo.Kind == LabelScopeKind.Try || labelScopeInfo.Kind == LabelScopeKind.Catch)
				{
					this._opCode = OpCodes.Leave;
				}
			}
			this._acrossBlockJump = true;
			if (this._node != null && this._node.Type != typeof(void))
			{
				throw Error.NonLocalJumpWithValue(this._node.Name);
			}
			if (this._definitions.Count > 1)
			{
				throw Error.AmbiguousJump(this._node.Name);
			}
			LabelScopeInfo labelScopeInfo2 = this._definitions.First<LabelScopeInfo>();
			LabelScopeInfo labelScopeInfo3 = Helpers.CommonNode<LabelScopeInfo>(labelScopeInfo2, reference, (LabelScopeInfo b) => b.Parent);
			this._opCode = (this._canReturn ? OpCodes.Ret : OpCodes.Br);
			for (LabelScopeInfo labelScopeInfo4 = reference; labelScopeInfo4 != labelScopeInfo3; labelScopeInfo4 = labelScopeInfo4.Parent)
			{
				if (labelScopeInfo4.Kind == LabelScopeKind.Finally)
				{
					throw Error.ControlCannotLeaveFinally();
				}
				if (labelScopeInfo4.Kind == LabelScopeKind.Filter)
				{
					throw Error.ControlCannotLeaveFilterTest();
				}
				if (labelScopeInfo4.Kind == LabelScopeKind.Try || labelScopeInfo4.Kind == LabelScopeKind.Catch)
				{
					this._opCode = OpCodes.Leave;
				}
			}
			LabelScopeInfo labelScopeInfo5 = labelScopeInfo2;
			while (labelScopeInfo5 != labelScopeInfo3)
			{
				if (!labelScopeInfo5.CanJumpInto)
				{
					if (labelScopeInfo5.Kind == LabelScopeKind.Expression)
					{
						throw Error.ControlCannotEnterExpression();
					}
					throw Error.ControlCannotEnterTry();
				}
				else
				{
					labelScopeInfo5 = labelScopeInfo5.Parent;
				}
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0004D327 File Offset: 0x0004B527
		internal void ValidateFinish()
		{
			if (this._references.Count > 0 && this._definitions.Count == 0)
			{
				throw Error.LabelTargetUndefined(this._node.Name);
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0004D358 File Offset: 0x0004B558
		internal void EmitJump()
		{
			if (this._opCode == OpCodes.Ret)
			{
				this._ilg.Emit(OpCodes.Ret);
				return;
			}
			this.StoreValue();
			this._ilg.Emit(this._opCode, this.Label);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0004D3A5 File Offset: 0x0004B5A5
		private void StoreValue()
		{
			this.EnsureLabelAndValue();
			if (this._value != null)
			{
				this._ilg.Emit(OpCodes.Stloc, this._value);
			}
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0004D3CB File Offset: 0x0004B5CB
		internal void Mark()
		{
			if (this._canReturn)
			{
				if (!this._labelDefined)
				{
					return;
				}
				this._ilg.Emit(OpCodes.Ret);
			}
			else
			{
				this.StoreValue();
			}
			this.MarkWithEmptyStack();
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0004D3FC File Offset: 0x0004B5FC
		internal void MarkWithEmptyStack()
		{
			this._ilg.MarkLabel(this.Label);
			if (this._value != null)
			{
				this._ilg.Emit(OpCodes.Ldloc, this._value);
			}
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x0004D430 File Offset: 0x0004B630
		private void EnsureLabelAndValue()
		{
			if (!this._labelDefined)
			{
				this._labelDefined = true;
				this._label = this._ilg.DefineLabel();
				if (this._node != null && this._node.Type != typeof(void))
				{
					this._value = this._ilg.DeclareLocal(this._node.Type);
				}
			}
		}

		// Token: 0x04000B4C RID: 2892
		private readonly LabelTarget _node;

		// Token: 0x04000B4D RID: 2893
		private Label _label;

		// Token: 0x04000B4E RID: 2894
		private bool _labelDefined;

		// Token: 0x04000B4F RID: 2895
		private LocalBuilder _value;

		// Token: 0x04000B50 RID: 2896
		private readonly Set<LabelScopeInfo> _definitions = new Set<LabelScopeInfo>();

		// Token: 0x04000B51 RID: 2897
		private readonly List<LabelScopeInfo> _references = new List<LabelScopeInfo>();

		// Token: 0x04000B52 RID: 2898
		private readonly bool _canReturn;

		// Token: 0x04000B53 RID: 2899
		private bool _acrossBlockJump;

		// Token: 0x04000B54 RID: 2900
		private OpCode _opCode = OpCodes.Leave;

		// Token: 0x04000B55 RID: 2901
		private readonly ILGenerator _ilg;
	}
}
