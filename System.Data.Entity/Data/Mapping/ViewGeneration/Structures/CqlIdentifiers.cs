using System;
using System.Data.Common.Utils;
using System.Globalization;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Structures
{
	// Token: 0x0200029A RID: 666
	internal class CqlIdentifiers : InternalBase
	{
		// Token: 0x06002795 RID: 10133 RVA: 0x00099ECB File Offset: 0x000980CB
		internal CqlIdentifiers()
		{
			this.m_identifiers = new Set<string>(StringComparer.Ordinal);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x00099EE3 File Offset: 0x000980E3
		internal string GetFromVariable(int num)
		{
			return this.GetNonConflictingName("_from", num);
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00099EF1 File Offset: 0x000980F1
		internal string GetBlockAlias(int num)
		{
			return this.GetNonConflictingName("T", num);
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00099EFF File Offset: 0x000980FF
		internal string GetBlockAlias()
		{
			return this.GetNonConflictingName("T", -1);
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00099F0D File Offset: 0x0009810D
		internal void AddIdentifier(string identifier)
		{
			this.m_identifiers.Add(identifier.ToLower(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00099F28 File Offset: 0x00098128
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

		// Token: 0x0600279B RID: 10139 RVA: 0x00099FEA File Offset: 0x000981EA
		internal override void ToCompactString(StringBuilder builder)
		{
			this.m_identifiers.ToCompactString(builder);
		}

		// Token: 0x04001227 RID: 4647
		private Set<string> m_identifiers;
	}
}
