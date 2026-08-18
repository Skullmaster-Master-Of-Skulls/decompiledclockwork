using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x020003C8 RID: 968
	public sealed class ModificationFunctionParameterBinding : MappingItem
	{
		// Token: 0x06002361 RID: 9057 RVA: 0x000A50D8 File Offset: 0x000A32D8
		public ModificationFunctionParameterBinding(FunctionParameter parameter, ModificationFunctionMemberPath memberPath, bool isCurrent)
		{
			Check.NotNull<FunctionParameter>(parameter, "parameter");
			Check.NotNull<ModificationFunctionMemberPath>(memberPath, "memberPath");
			this._parameter = parameter;
			this._memberPath = memberPath;
			this._isCurrent = isCurrent;
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x000A510D File Offset: 0x000A330D
		public FunctionParameter Parameter
		{
			get
			{
				return this._parameter;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x000A5115 File Offset: 0x000A3315
		public ModificationFunctionMemberPath MemberPath
		{
			get
			{
				return this._memberPath;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06002364 RID: 9060 RVA: 0x000A511D File Offset: 0x000A331D
		public bool IsCurrent
		{
			get
			{
				return this._isCurrent;
			}
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x000A5128 File Offset: 0x000A3328
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}->{1}{2}", new object[]
			{
				this.Parameter,
				this.IsCurrent ? "+" : "-",
				this.MemberPath
			});
		}

		// Token: 0x06002366 RID: 9062 RVA: 0x000A5175 File Offset: 0x000A3375
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._memberPath);
			base.SetReadOnly();
		}

		// Token: 0x04000C70 RID: 3184
		private readonly FunctionParameter _parameter;

		// Token: 0x04000C71 RID: 3185
		private readonly ModificationFunctionMemberPath _memberPath;

		// Token: 0x04000C72 RID: 3186
		private readonly bool _isCurrent;
	}
}
