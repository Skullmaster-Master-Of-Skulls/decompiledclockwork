using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001EB RID: 491
	public sealed class StatementContext
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0006A4A6 File Offset: 0x000686A6
		public ObjectStatementCollection StatementCollection
		{
			get
			{
				if (this._statements == null)
				{
					this._statements = new ObjectStatementCollection();
				}
				return this._statements;
			}
		}

		// Token: 0x04000A06 RID: 2566
		private ObjectStatementCollection _statements;
	}
}
