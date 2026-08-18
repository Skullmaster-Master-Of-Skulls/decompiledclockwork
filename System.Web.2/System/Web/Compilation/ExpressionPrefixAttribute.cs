using System;

namespace System.Web.Compilation
{
	// Token: 0x0200083D RID: 2109
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExpressionPrefixAttribute : Attribute
	{
		// Token: 0x06006494 RID: 25748 RVA: 0x00160911 File Offset: 0x0015EB11
		public ExpressionPrefixAttribute(string expressionPrefix)
		{
			if (string.IsNullOrEmpty(expressionPrefix))
			{
				throw new ArgumentNullException("expressionPrefix");
			}
			this._expressionPrefix = expressionPrefix;
		}

		// Token: 0x17001C56 RID: 7254
		// (get) Token: 0x06006495 RID: 25749 RVA: 0x00160933 File Offset: 0x0015EB33
		public string ExpressionPrefix
		{
			get
			{
				return this._expressionPrefix;
			}
		}

		// Token: 0x040033E7 RID: 13287
		private string _expressionPrefix;
	}
}
