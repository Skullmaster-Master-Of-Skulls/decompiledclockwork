using System;
using System.Data.Metadata.Edm;
using System.Globalization;

namespace System.Data.Mapping
{
	// Token: 0x02000248 RID: 584
	internal sealed class StorageModificationFunctionParameterBinding
	{
		// Token: 0x0600247F RID: 9343 RVA: 0x000842E3 File Offset: 0x000824E3
		internal StorageModificationFunctionParameterBinding(FunctionParameter parameter, StorageModificationFunctionMemberPath memberPath, bool isCurrent)
		{
			this.Parameter = EntityUtil.CheckArgumentNull<FunctionParameter>(parameter, "parameter");
			this.MemberPath = EntityUtil.CheckArgumentNull<StorageModificationFunctionMemberPath>(memberPath, "memberPath");
			this.IsCurrent = isCurrent;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x00084314 File Offset: 0x00082514
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}->{1}{2}", new object[]
			{
				this.Parameter,
				this.IsCurrent ? "+" : "-",
				this.MemberPath
			});
		}

		// Token: 0x04001032 RID: 4146
		internal readonly FunctionParameter Parameter;

		// Token: 0x04001033 RID: 4147
		internal readonly StorageModificationFunctionMemberPath MemberPath;

		// Token: 0x04001034 RID: 4148
		internal readonly bool IsCurrent;
	}
}
