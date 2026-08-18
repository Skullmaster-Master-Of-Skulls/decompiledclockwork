using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x0200031B RID: 795
	internal class TdsParameterSetter : SmiTypedGetterSetter
	{
		// Token: 0x060029AE RID: 10670 RVA: 0x002B5438 File Offset: 0x002B4838
		internal TdsParameterSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._target = new TdsRecordBufferSetter(stateObj, md);
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x002B5458 File Offset: 0x002B4858
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x002B5468 File Offset: 0x002B4868
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029B1 RID: 10673 RVA: 0x002B5478 File Offset: 0x002B4878
		internal override SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			return this._target;
		}

		// Token: 0x060029B2 RID: 10674 RVA: 0x002B5498 File Offset: 0x002B4898
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._target.EndElements(sink);
		}

		// Token: 0x04001B45 RID: 6981
		private TdsRecordBufferSetter _target;
	}
}
