using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Data.Metadata.Edm;
using System.Text;

namespace System.Data.Mapping.ViewGeneration
{
	// Token: 0x0200026D RID: 621
	internal class ViewGenResults : InternalBase
	{
		// Token: 0x06002617 RID: 9751 RVA: 0x0009127A File Offset: 0x0008F47A
		internal ViewGenResults()
		{
			this.m_views = new KeyToListMap<EntitySetBase, GeneratedView>(EqualityComparer<EntitySetBase>.Default);
			this.m_errorLog = new ErrorLog();
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x0009129D File Offset: 0x0008F49D
		internal KeyToListMap<EntitySetBase, GeneratedView> Views
		{
			get
			{
				return this.m_views;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002619 RID: 9753 RVA: 0x000912A5 File Offset: 0x0008F4A5
		internal IEnumerable<EdmSchemaError> Errors
		{
			get
			{
				return this.m_errorLog.Errors;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x000912B2 File Offset: 0x0008F4B2
		internal bool HasErrors
		{
			get
			{
				return this.m_errorLog.Count > 0;
			}
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000912C2 File Offset: 0x0008F4C2
		internal void AddErrors(ErrorLog errorLog)
		{
			this.m_errorLog.Merge(errorLog);
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000912D0 File Offset: 0x0008F4D0
		internal string ErrorsToString()
		{
			return this.m_errorLog.ToString();
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000912DD File Offset: 0x0008F4DD
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append(this.m_errorLog.Count);
			builder.Append(" ");
			this.m_errorLog.ToCompactString(builder);
		}

		// Token: 0x04001192 RID: 4498
		private KeyToListMap<EntitySetBase, GeneratedView> m_views;

		// Token: 0x04001193 RID: 4499
		private ErrorLog m_errorLog;
	}
}
