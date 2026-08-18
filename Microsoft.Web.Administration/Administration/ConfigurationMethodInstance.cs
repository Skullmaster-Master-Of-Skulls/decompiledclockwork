using System;
using System.ComponentModel;
using Microsoft.Web.Administration.Interop;

namespace Microsoft.Web.Administration
{
	// Token: 0x02000038 RID: 56
	public sealed class ConfigurationMethodInstance
	{
		// Token: 0x060001DA RID: 474 RVA: 0x0000701E File Offset: 0x0000601E
		internal ConfigurationMethodInstance(IAppHostMethodInstance method)
		{
			this._method = method;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007030 File Offset: 0x00006030
		public ConfigurationElement Input
		{
			get
			{
				if (this._input == null)
				{
					IAppHostElement input = this._method.Input;
					if (input == null)
					{
						return null;
					}
					this._input = new ConfigurationElement();
					this._input.InitializeMethodElement(input);
				}
				return this._input;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00007074 File Offset: 0x00006074
		public ConfigurationElement Output
		{
			get
			{
				if (this._output == null)
				{
					IAppHostElement output = this._method.Output;
					if (output == null)
					{
						return null;
					}
					this._output = new ConfigurationElement();
					this._output.InitializeMethodElement(output);
				}
				return this._output;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000070B7 File Offset: 0x000060B7
		public void Execute()
		{
			this._method.Execute();
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000070C4 File Offset: 0x000060C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object GetMetadata(string metadataType)
		{
			return this._method.GetMetadata(metadataType);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000070D2 File Offset: 0x000060D2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void SetMetadata(string metadataType, object value)
		{
			this._method.SetMetadata(metadataType, value);
		}

		// Token: 0x0400009E RID: 158
		private IAppHostMethodInstance _method;

		// Token: 0x0400009F RID: 159
		private ConfigurationElement _input;

		// Token: 0x040000A0 RID: 160
		private ConfigurationElement _output;
	}
}
