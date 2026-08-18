using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic.Utils;
using System.Linq.Expressions;

namespace System.Dynamic
{
	// Token: 0x020000BE RID: 190
	[__DynamicallyInvokable]
	public sealed class CallInfo
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x000111F1 File Offset: 0x0000F3F1
		[__DynamicallyInvokable]
		public CallInfo(int argCount, params string[] argNames) : this(argCount, argNames)
		{
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000111FC File Offset: 0x0000F3FC
		[__DynamicallyInvokable]
		public CallInfo(int argCount, IEnumerable<string> argNames)
		{
			ContractUtils.RequiresNotNull(argNames, "argNames");
			ReadOnlyCollection<string> readOnlyCollection = argNames.ToReadOnly<string>();
			if (argCount < readOnlyCollection.Count)
			{
				throw Error.ArgCntMustBeGreaterThanNameCnt();
			}
			ContractUtils.RequiresNotNullItems<string>(readOnlyCollection, "argNames");
			this._argCount = argCount;
			this._argNames = readOnlyCollection;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00011249 File Offset: 0x0000F449
		[__DynamicallyInvokable]
		public int ArgumentCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this._argCount;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00011251 File Offset: 0x0000F451
		[__DynamicallyInvokable]
		public ReadOnlyCollection<string> ArgumentNames
		{
			[__DynamicallyInvokable]
			get
			{
				return this._argNames;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00011259 File Offset: 0x0000F459
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this._argCount ^ this._argNames.ListHashCode<string>();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00011270 File Offset: 0x0000F470
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			CallInfo callInfo = obj as CallInfo;
			return this._argCount == callInfo._argCount && this._argNames.ListEquals(callInfo._argNames);
		}

		// Token: 0x0400059A RID: 1434
		private readonly int _argCount;

		// Token: 0x0400059B RID: 1435
		private readonly ReadOnlyCollection<string> _argNames;
	}
}
