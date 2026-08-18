using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000019 RID: 25
	[SuppressMessage("Microsoft.Performance", "CA1815:OverrideEqualsAndOperatorEqualsOnValueTypes")]
	public class EdmFunctionPayload
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004F0F File Offset: 0x0000310F
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00004F17 File Offset: 0x00003117
		public string Schema { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004F20 File Offset: 0x00003120
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00004F28 File Offset: 0x00003128
		public string StoreFunctionName { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00004F31 File Offset: 0x00003131
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00004F39 File Offset: 0x00003139
		public string CommandText { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00004F42 File Offset: 0x00003142
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00004F4A File Offset: 0x0000314A
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public IList<EntitySet> EntitySets { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004F53 File Offset: 0x00003153
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00004F5B File Offset: 0x0000315B
		public bool? IsAggregate { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004F64 File Offset: 0x00003164
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00004F6C File Offset: 0x0000316C
		public bool? IsBuiltIn { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004F75 File Offset: 0x00003175
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00004F7D File Offset: 0x0000317D
		public bool? IsNiladic { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004F86 File Offset: 0x00003186
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004F8E File Offset: 0x0000318E
		public bool? IsComposable { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004F97 File Offset: 0x00003197
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004F9F File Offset: 0x0000319F
		public bool? IsFromProviderManifest { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004FA8 File Offset: 0x000031A8
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004FB0 File Offset: 0x000031B0
		public bool? IsCachedStoreFunction { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004FB9 File Offset: 0x000031B9
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004FC1 File Offset: 0x000031C1
		public bool? IsFunctionImport { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004FCA File Offset: 0x000031CA
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00004FD2 File Offset: 0x000031D2
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public IList<FunctionParameter> ReturnParameters { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004FDB File Offset: 0x000031DB
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00004FE3 File Offset: 0x000031E3
		public ParameterTypeSemantics? ParameterTypeSemantics { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004FEC File Offset: 0x000031EC
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004FF4 File Offset: 0x000031F4
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public IList<FunctionParameter> Parameters { get; set; }
	}
}
