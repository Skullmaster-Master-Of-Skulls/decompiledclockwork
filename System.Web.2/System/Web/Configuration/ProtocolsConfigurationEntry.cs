using System;
using System.Configuration;
using System.Web.Hosting;

namespace System.Web.Configuration
{
	// Token: 0x0200073A RID: 1850
	internal class ProtocolsConfigurationEntry
	{
		// Token: 0x0600593C RID: 22844 RVA: 0x0013756A File Offset: 0x0013576A
		internal ProtocolsConfigurationEntry(string id, string processHandlerType, string appDomainHandlerType, bool validate, string configFileName, int configFileLine)
		{
			this._id = id;
			this._processHandlerTypeName = processHandlerType;
			this._appDomainHandlerTypeName = appDomainHandlerType;
			this._configFileName = configFileName;
			this._configFileLine = configFileLine;
			if (validate)
			{
				this.ValidateTypes();
			}
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x001375A4 File Offset: 0x001357A4
		private void ValidateTypes()
		{
			if (this._typesValidated)
			{
				return;
			}
			Type type;
			try
			{
				type = Type.GetType(this._processHandlerTypeName, true);
			}
			catch (Exception ex)
			{
				throw new ConfigurationErrorsException(ex.Message, ex, this._configFileName, this._configFileLine);
			}
			HandlerBase.CheckAssignableType(this._configFileName, this._configFileLine, typeof(ProcessProtocolHandler), type);
			Type type2;
			try
			{
				type2 = Type.GetType(this._appDomainHandlerTypeName, true);
			}
			catch (Exception ex2)
			{
				throw new ConfigurationErrorsException(ex2.Message, ex2, this._configFileName, this._configFileLine);
			}
			HandlerBase.CheckAssignableType(this._configFileName, this._configFileLine, typeof(AppDomainProtocolHandler), type2);
			this._processHandlerType = type;
			this._appDomainHandlerType = type2;
			this._typesValidated = true;
		}

		// Token: 0x04002F52 RID: 12114
		private string _id;

		// Token: 0x04002F53 RID: 12115
		private string _processHandlerTypeName;

		// Token: 0x04002F54 RID: 12116
		private Type _processHandlerType;

		// Token: 0x04002F55 RID: 12117
		private string _appDomainHandlerTypeName;

		// Token: 0x04002F56 RID: 12118
		private Type _appDomainHandlerType;

		// Token: 0x04002F57 RID: 12119
		private bool _typesValidated;

		// Token: 0x04002F58 RID: 12120
		private string _configFileName;

		// Token: 0x04002F59 RID: 12121
		private int _configFileLine;
	}
}
