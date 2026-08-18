using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x020002B5 RID: 693
	internal sealed class XmlSerializerCompilerParameters
	{
		// Token: 0x0600213A RID: 8506 RVA: 0x0009D799 File Offset: 0x0009C799
		private XmlSerializerCompilerParameters(CompilerParameters parameters, bool needTempDirAccess)
		{
			this.needTempDirAccess = needTempDirAccess;
			this.parameters = parameters;
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x0009D7AF File Offset: 0x0009C7AF
		internal bool IsNeedTempDirAccess
		{
			get
			{
				return this.needTempDirAccess;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x0009D7B7 File Offset: 0x0009C7B7
		internal CompilerParameters CodeDomParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0009D7C0 File Offset: 0x0009C7C0
		internal static XmlSerializerCompilerParameters Create(string location)
		{
			CompilerParameters compilerParameters = new CompilerParameters();
			compilerParameters.GenerateInMemory = true;
			if (string.IsNullOrEmpty(location))
			{
				XmlSerializerSection xmlSerializerSection = ConfigurationManager.GetSection(ConfigurationStrings.XmlSerializerSectionPath) as XmlSerializerSection;
				location = ((xmlSerializerSection == null) ? location : xmlSerializerSection.TempFilesLocation);
				if (!string.IsNullOrEmpty(location))
				{
					location = location.Trim();
				}
			}
			compilerParameters.TempFiles = new TempFileCollection(location);
			return new XmlSerializerCompilerParameters(compilerParameters, string.IsNullOrEmpty(location));
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0009D828 File Offset: 0x0009C828
		internal static XmlSerializerCompilerParameters Create(CompilerParameters parameters, bool needTempDirAccess)
		{
			return new XmlSerializerCompilerParameters(parameters, needTempDirAccess);
		}

		// Token: 0x04001443 RID: 5187
		private bool needTempDirAccess;

		// Token: 0x04001444 RID: 5188
		private CompilerParameters parameters;
	}
}
