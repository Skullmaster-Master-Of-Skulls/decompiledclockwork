using System;
using System.Reflection;

namespace Telerik.Licensing
{
	// Token: 0x02000427 RID: 1063
	internal class EnvDTEInterop
	{
		// Token: 0x06002626 RID: 9766 RVA: 0x0007D357 File Offset: 0x0007B557
		public EnvDTEInterop(object dte)
		{
			this._dte = dte;
			this.InitializeSolutionName();
			this.InitializeGlobals();
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x0007D374 File Offset: 0x0007B574
		public virtual void SetVariablePerists(object variable, bool persists)
		{
			this._globals.GetType().InvokeMember("VariablePersists", BindingFlags.SetProperty, null, this._globals, new object[]
			{
				variable,
				persists
			});
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x0007D3B8 File Offset: 0x0007B5B8
		public virtual void SetVariable(object key, object variable)
		{
			this._globals.GetType().InvokeMember("VariableValue", BindingFlags.SetProperty, null, this._globals, new object[]
			{
				key,
				variable
			});
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x0007D3F8 File Offset: 0x0007B5F8
		public bool GetViableExists(object variable)
		{
			return (bool)this._globals.GetType().InvokeMember("VariableExists", BindingFlags.GetProperty, null, this._globals, new object[]
			{
				variable
			});
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0007D438 File Offset: 0x0007B638
		public virtual object GetVariable(object key)
		{
			return this._globals.GetType().InvokeMember("VariableValue", BindingFlags.GetProperty, null, this._globals, new object[]
			{
				key
			});
		}

		// Token: 0x0600262B RID: 9771 RVA: 0x0007D472 File Offset: 0x0007B672
		public virtual string GetName()
		{
			return this._solutionName;
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x0007D47A File Offset: 0x0007B67A
		private void InitializeGlobals()
		{
			this._globals = this._solution.GetType().InvokeMember("Globals", BindingFlags.GetProperty, null, this._solution, null);
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x0007D4A4 File Offset: 0x0007B6A4
		private void InitializeSolutionName()
		{
			this._solution = this._dte.GetType().InvokeMember("Solution", BindingFlags.GetProperty, null, this._dte, null);
			this._solutionName = (string)this._solution.GetType().InvokeMember("FullName", BindingFlags.GetProperty, null, this._solution, null);
		}

		// Token: 0x040009B9 RID: 2489
		private readonly object _dte;

		// Token: 0x040009BA RID: 2490
		private string _solutionName;

		// Token: 0x040009BB RID: 2491
		private object _globals;

		// Token: 0x040009BC RID: 2492
		private object _solution;
	}
}
