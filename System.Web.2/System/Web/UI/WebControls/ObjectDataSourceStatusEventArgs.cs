using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000490 RID: 1168
	public class ObjectDataSourceStatusEventArgs : EventArgs
	{
		// Token: 0x060039BD RID: 14781 RVA: 0x000BAE8C File Offset: 0x000B908C
		public ObjectDataSourceStatusEventArgs(object returnValue, IDictionary outputParameters) : this(returnValue, outputParameters, null)
		{
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x000BAE97 File Offset: 0x000B9097
		public ObjectDataSourceStatusEventArgs(object returnValue, IDictionary outputParameters, Exception exception)
		{
			this._returnValue = returnValue;
			this._outputParameters = outputParameters;
			this._exception = exception;
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x060039BF RID: 14783 RVA: 0x000BAEBB File Offset: 0x000B90BB
		public IDictionary OutputParameters
		{
			get
			{
				return this._outputParameters;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x060039C0 RID: 14784 RVA: 0x000BAEC3 File Offset: 0x000B90C3
		public Exception Exception
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x060039C1 RID: 14785 RVA: 0x000BAECB File Offset: 0x000B90CB
		// (set) Token: 0x060039C2 RID: 14786 RVA: 0x000BAED3 File Offset: 0x000B90D3
		public bool ExceptionHandled
		{
			get
			{
				return this._exceptionHandled;
			}
			set
			{
				this._exceptionHandled = value;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x060039C3 RID: 14787 RVA: 0x000BAEDC File Offset: 0x000B90DC
		public object ReturnValue
		{
			get
			{
				return this._returnValue;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x060039C4 RID: 14788 RVA: 0x000BAEE4 File Offset: 0x000B90E4
		// (set) Token: 0x060039C5 RID: 14789 RVA: 0x000BAEEC File Offset: 0x000B90EC
		public int AffectedRows
		{
			get
			{
				return this._affectedRows;
			}
			set
			{
				this._affectedRows = value;
			}
		}

		// Token: 0x040022C0 RID: 8896
		private object _returnValue;

		// Token: 0x040022C1 RID: 8897
		private IDictionary _outputParameters;

		// Token: 0x040022C2 RID: 8898
		private Exception _exception;

		// Token: 0x040022C3 RID: 8899
		private bool _exceptionHandled;

		// Token: 0x040022C4 RID: 8900
		private int _affectedRows = -1;
	}
}
