using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000137 RID: 311
	internal class CodeGeneratorConversionException : Exception
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x00063F34 File Offset: 0x00062134
		public CodeGeneratorConversionException(Type sourceType, Type targetType, bool isAddress, string reason)
		{
			this.sourceType = sourceType;
			this.targetType = targetType;
			this.isAddress = isAddress;
			this.reason = reason;
		}

		// Token: 0x04000A94 RID: 2708
		private Type sourceType;

		// Token: 0x04000A95 RID: 2709
		private Type targetType;

		// Token: 0x04000A96 RID: 2710
		private bool isAddress;

		// Token: 0x04000A97 RID: 2711
		private string reason;
	}
}
