using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x0200013C RID: 316
	internal sealed class XmlSerializerCompilerParameters
	{
		// Token: 0x060016E4 RID: 5860 RVA: 0x000659F7 File Offset: 0x00063BF7
		private XmlSerializerCompilerParameters(CompilerParameters parameters, bool needTempDirAccess)
		{
			this.needTempDirAccess = needTempDirAccess;
			this.parameters = parameters;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00065A0D File Offset: 0x00063C0D
		internal bool IsNeedTempDirAccess
		{
			get
			{
				return this.needTempDirAccess;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060016E6 RID: 5862 RVA: 0x00065A15 File Offset: 0x00063C15
		internal CompilerParameters CodeDomParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00065A20 File Offset: 0x00063C20
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

		// Token: 0x060016E8 RID: 5864 RVA: 0x00065A88 File Offset: 0x00063C88
		internal static XmlSerializerCompilerParameters Create(CompilerParameters parameters, bool needTempDirAccess)
		{
			return new XmlSerializerCompilerParameters(parameters, needTempDirAccess);
		}

		// Token: 0x04000AA8 RID: 2728
		private bool needTempDirAccess;

		// Token: 0x04000AA9 RID: 2729
		private CompilerParameters parameters;
	}
}
