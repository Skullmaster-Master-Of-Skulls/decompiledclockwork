using System;
using System.Dynamic;
using System.Web.WebPages.Resources;

namespace System.Web.WebPages
{
	// Token: 0x0200006E RID: 110
	internal class DynamicHttpApplicationState : DynamicObject
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000A673 File Offset: 0x00008873
		public DynamicHttpApplicationState(HttpApplicationStateBase state)
		{
			this._state = state;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000A682 File Offset: 0x00008882
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = this._state[binder.Name];
			return true;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000A698 File Offset: 0x00008898
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			this._state[binder.Name] = value;
			return true;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			if (indexes == null || indexes.Length != 1)
			{
				throw new ArgumentException(WebPageResources.DynamicDictionary_InvalidNumberOfIndexes);
			}
			result = null;
			string text = indexes[0] as string;
			if (text != null)
			{
				result = this._state[text];
			}
			else
			{
				if (!(indexes[0] is int))
				{
					throw new ArgumentException(WebPageResources.DynamicHttpApplicationState_UseOnlyStringOrIntToGet);
				}
				result = this._state[(int)indexes[0]];
			}
			return true;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000A720 File Offset: 0x00008920
		public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
		{
			if (indexes == null || indexes.Length != 1)
			{
				throw new ArgumentException(WebPageResources.DynamicDictionary_InvalidNumberOfIndexes);
			}
			string text = indexes[0] as string;
			if (text != null)
			{
				this._state[text] = value;
				return true;
			}
			throw new ArgumentException(WebPageResources.DynamicHttpApplicationState_UseOnlyStringToSet);
		}

		// Token: 0x040000E3 RID: 227
		private HttpApplicationStateBase _state;
	}
}
