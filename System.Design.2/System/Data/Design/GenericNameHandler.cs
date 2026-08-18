using System;
using System.CodeDom.Compiler;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000245 RID: 581
	internal sealed class GenericNameHandler
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x0007CC44 File Offset: 0x0007AE44
		internal GenericNameHandler(ICollection initialNameSet, CodeDomProvider codeProvider)
		{
			this.validator = new MemberNameValidator(initialNameSet, codeProvider, true);
			this.names = new Hashtable(StringComparer.Ordinal);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0007CC6C File Offset: 0x0007AE6C
		internal string AddParameterNameToList(string originalName, string parameterPrefix)
		{
			if (originalName == null)
			{
				throw new ArgumentNullException("originalName");
			}
			string originalName2 = originalName;
			if (!StringUtil.Empty(parameterPrefix) && originalName.StartsWith(parameterPrefix, StringComparison.Ordinal))
			{
				originalName2 = originalName.Substring(parameterPrefix.Length);
			}
			string newMemberName = this.validator.GetNewMemberName(originalName2);
			this.names.Add(originalName, newMemberName);
			return newMemberName;
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x0007CCC4 File Offset: 0x0007AEC4
		internal string AddNameToList(string originalName)
		{
			if (originalName == null)
			{
				throw new InternalException("Parameter originalName should not be null.");
			}
			string newMemberName = this.validator.GetNewMemberName(originalName);
			this.names.Add(originalName, newMemberName);
			return newMemberName;
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0007CCFA File Offset: 0x0007AEFA
		internal string GetNameFromList(string originalName)
		{
			if (originalName == null)
			{
				throw new InternalException("Parameter originalName should not be null.");
			}
			return (string)this.names[originalName];
		}

		// Token: 0x04000B93 RID: 2963
		private MemberNameValidator validator;

		// Token: 0x04000B94 RID: 2964
		private Hashtable names;
	}
}
