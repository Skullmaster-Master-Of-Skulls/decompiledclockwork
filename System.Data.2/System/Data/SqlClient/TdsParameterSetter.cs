using System;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000233 RID: 563
	internal class TdsParameterSetter : SmiTypedGetterSetter
	{
		// Token: 0x060022E4 RID: 8932 RVA: 0x000F1A88 File Offset: 0x000F0E88
		internal TdsParameterSetter(TdsParserStateObject stateObj, SmiMetaData md)
		{
			this._target = new TdsRecordBufferSetter(stateObj, md);
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000F1AA8 File Offset: 0x000F0EA8
		internal override bool CanGet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060022E6 RID: 8934 RVA: 0x000F1AB8 File Offset: 0x000F0EB8
		internal override bool CanSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000F1AC8 File Offset: 0x000F0EC8
		internal override SmiTypedGetterSetter GetTypedGetterSetter(SmiEventSink sink, int ordinal)
		{
			return this._target;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000F1ADC File Offset: 0x000F0EDC
		public override void SetDBNull(SmiEventSink sink, int ordinal)
		{
			this._target.EndElements(sink);
		}

		// Token: 0x04001533 RID: 5427
		private TdsRecordBufferSetter _target;
	}
}
