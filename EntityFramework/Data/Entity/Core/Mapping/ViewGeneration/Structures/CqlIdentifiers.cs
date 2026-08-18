using System;
using System.Data.Entity.Core.Common.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000429 RID: 1065
	internal class CqlIdentifiers : InternalBase
	{
		// Token: 0x06002739 RID: 10041 RVA: 0x000BE34D File Offset: 0x000BC54D
		internal CqlIdentifiers()
		{
			this.m_identifiers = new Set<string>(StringComparer.Ordinal);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x000BE365 File Offset: 0x000BC565
		internal string GetFromVariable(int num)
		{
			return this.GetNonConflictingName("_from", num);
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x000BE373 File Offset: 0x000BC573
		internal string GetBlockAlias(int num)
		{
			return this.GetNonConflictingName("T", num);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000BE381 File Offset: 0x000BC581
		internal string GetBlockAlias()
		{
			return this.GetNonConflictingName("T", -1);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x000BE38F File Offset: 0x000BC58F
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		internal void AddIdentifier(string identifier)
		{
			this.m_identifiers.Add(identifier.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x000BE3A8 File Offset: 0x000BC5A8
		[SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
		private string GetNonConflictingName(string prefix, int number)
		{
			string text = (number < 0) ? prefix : StringUtil.FormatInvariant("{0}{1}", new object[]
			{
				prefix,
				number
			});
			if (!this.m_identifiers.Contains(text.ToLower(CultureInfo.InvariantCulture)))
			{
				return text;
			}
			for (int i = 0; i < 2147483647; i++)
			{
				if (number < 0)
				{
					text = StringUtil.FormatInvariant("{0}_{1}", new object[]
					{
						prefix,
						i
					});
				}
				else
				{
					text = StringUtil.FormatInvariant("{0}_{1}_{2}", new object[]
					{
						prefix,
						i,
						number
					});
				}
				if (!this.m_identifiers.Contains(text.ToLower(CultureInfo.InvariantCulture)))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000BE475 File Offset: 0x000BC675
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_identifiers.ToCompactString(builder);
		}

		// Token: 0x04000EBB RID: 3771
		private readonly Set<string> m_identifiers;
	}
}
