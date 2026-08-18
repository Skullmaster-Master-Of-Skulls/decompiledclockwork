using System;
using System.Security.Cryptography;
using System.Web.Configuration;

namespace System.Web.Security.Cryptography
{
	// Token: 0x02000602 RID: 1538
	internal sealed class MachineKeyDataProtectorFactory : IDataProtectorFactory
	{
		// Token: 0x06004DA1 RID: 19873 RVA: 0x0010D82C File Offset: 0x0010BA2C
		public MachineKeyDataProtectorFactory(MachineKeySection machineKeySection)
		{
			this._machineKeySection = machineKeySection;
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x0010D83B File Offset: 0x0010BA3B
		public DataProtector GetDataProtector(Purpose purpose)
		{
			if (this._dataProtectorFactory == null)
			{
				this._dataProtectorFactory = this.GetDataProtectorFactory();
			}
			return this._dataProtectorFactory(purpose);
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x0010D860 File Offset: 0x0010BA60
		private Func<Purpose, DataProtector> GetDataProtectorFactory()
		{
			string applicationName = this._machineKeySection.ApplicationName;
			string dataProtectorTypeName = this._machineKeySection.DataProtectorType;
			Func<Purpose, DataProtector> func = delegate(Purpose purpose)
			{
				DataProtector result;
				using (new ApplicationImpersonationContext())
				{
					result = DataProtector.Create(dataProtectorTypeName, applicationName, purpose.PrimaryPurpose, purpose.SpecificPurposes);
				}
				return result;
			};
			Exception innerException = null;
			try
			{
				DataProtector dataProtector = func(MachineKeyDataProtectorFactory._creationTestingPurpose);
				if (dataProtector != null)
				{
					IDisposable disposable = dataProtector as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
					return func;
				}
			}
			catch (Exception ex)
			{
				innerException = ex;
			}
			throw ConfigUtil.MakeConfigurationErrorsException(SR.GetString("MachineKeyDataProtectorFactory_FactoryCreationFailed"), innerException, this._machineKeySection.ElementInformation.Properties["dataProtectorType"]);
		}

		// Token: 0x04002964 RID: 10596
		private static readonly Purpose _creationTestingPurpose = new Purpose("test-1", new string[]
		{
			"test-2",
			"test-3"
		});

		// Token: 0x04002965 RID: 10597
		private Func<Purpose, DataProtector> _dataProtectorFactory;

		// Token: 0x04002966 RID: 10598
		private readonly MachineKeySection _machineKeySection;
	}
}
