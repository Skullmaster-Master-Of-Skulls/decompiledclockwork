using System;
using Spire.Xls.Core.Parser.Biff_Records.Formula;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000624 RID: 1572
	public class EvaluateEventArgs : EventArgs
	{
		// Token: 0x06005FC2 RID: 24514 RVA: 0x003C858C File Offset: 0x003C758C
		private EvaluateEventArgs()
		{
		}

		// Token: 0x06005FC3 RID: 24515 RVA: 0x003C85A0 File Offset: 0x003C75A0
		internal EvaluateEventArgs(IXLSRange A_0, Ptg[] A_1)
		{
			this.ᜀ = A_0;
			this.ᜁ = A_1;
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06005FC4 RID: 24516 RVA: 0x003C85C4 File Offset: 0x003C75C4
		public IXLSRange Range
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06005FC5 RID: 24517 RVA: 0x003C8608 File Offset: 0x003C7608
		internal Ptg[] PtgArray
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜁ;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06005FC6 RID: 24518 RVA: 0x003C864C File Offset: 0x003C764C
		public new static EvaluateEventArgs Empty
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return new EvaluateEventArgs();
			}
		}

		// Token: 0x04002E1F RID: 11807
		private float \u2593ª\u00A6\u00AE;

		// Token: 0x04002E20 RID: 11808
		private float[] \u25D9\u00A9\u00A2\u00A7;

		// Token: 0x04002E21 RID: 11809
		private string \u25D8\u00A9\u0083\u008E;

		// Token: 0x04002E22 RID: 11810
		private float[] \u25D9\u009D\u0087\u00A8;

		// Token: 0x04002E23 RID: 11811
		private string \u25D8\u00A0\u00A8\u0082;

		// Token: 0x04002E24 RID: 11812
		private IXLSRange ᜀ;

		// Token: 0x04002E25 RID: 11813
		private Ptg[] ᜁ;
	}
}
