using System;
using System.Diagnostics;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000044 RID: 68
	internal class DiagnosticTraceSource : TraceSource
	{
		// Token: 0x060002BE RID: 702 RVA: 0x0000BCA2 File Offset: 0x00009EA2
		internal DiagnosticTraceSource(string name) : base(name)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000BCAB File Offset: 0x00009EAB
		protected override string[] GetSupportedAttributes()
		{
			return new string[]
			{
				"propagateActivity"
			};
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000BCBC File Offset: 0x00009EBC
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000BCF1 File Offset: 0x00009EF1
		internal bool PropagateActivity
		{
			get
			{
				bool result = false;
				string value = base.Attributes["propagateActivity"];
				if (!string.IsNullOrEmpty(value) && !bool.TryParse(value, out result))
				{
					result = false;
				}
				return result;
			}
			set
			{
				base.Attributes["propagateActivity"] = value.ToString();
			}
		}

		// Token: 0x04000129 RID: 297
		private const string PropagateActivityValue = "propagateActivity";
	}
}
