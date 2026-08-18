using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration
{
	// Token: 0x020004A4 RID: 1188
	internal class ViewGenResults : InternalBase
	{
		// Token: 0x06002BCB RID: 11211 RVA: 0x000D5BFA File Offset: 0x000D3DFA
		internal ViewGenResults()
		{
			this.m_views = new KeyToListMap<EntitySetBase, GeneratedView>(EqualityComparer<EntitySetBase>.Default);
			this.m_errorLog = new ErrorLog();
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000D5C1D File Offset: 0x000D3E1D
		internal KeyToListMap<EntitySetBase, GeneratedView> Views
		{
			get
			{
				return this.m_views;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x000D5C25 File Offset: 0x000D3E25
		internal IEnumerable<EdmSchemaError> Errors
		{
			get
			{
				return this.m_errorLog.Errors;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x000D5C32 File Offset: 0x000D3E32
		internal bool HasErrors
		{
			get
			{
				return this.m_errorLog.Count > 0;
			}
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000D5C42 File Offset: 0x000D3E42
		internal void AddErrors(ErrorLog errorLog)
		{
			this.m_errorLog.Merge(errorLog);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000D5C50 File Offset: 0x000D3E50
		internal string ErrorsToString()
		{
			return this.m_errorLog.ToString();
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000D5C5D File Offset: 0x000D3E5D
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_errorLog.Count);
			builder.Append(" ");
			this.m_errorLog.ToCompactString(builder);
		}

		// Token: 0x04001035 RID: 4149
		private readonly KeyToListMap<EntitySetBase, GeneratedView> m_views;

		// Token: 0x04001036 RID: 4150
		private readonly ErrorLog m_errorLog;
	}
}
