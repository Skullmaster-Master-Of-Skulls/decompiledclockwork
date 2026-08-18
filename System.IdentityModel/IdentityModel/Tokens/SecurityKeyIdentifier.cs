using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000169 RID: 361
	public class SecurityKeyIdentifier : IEnumerable<SecurityKeyIdentifierClause>, IEnumerable
	{
		// Token: 0x06000B5F RID: 2911 RVA: 0x00036858 File Offset: 0x00034A58
		public SecurityKeyIdentifier()
		{
			this.clauses = new List<SecurityKeyIdentifierClause>(2);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0003686C File Offset: 0x00034A6C
		public SecurityKeyIdentifier(params SecurityKeyIdentifierClause[] clauses)
		{
			if (clauses == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("clauses");
			}
			this.clauses = new List<SecurityKeyIdentifierClause>(clauses.Length);
			for (int i = 0; i < clauses.Length; i++)
			{
				this.Add(clauses[i]);
			}
		}

		// Token: 0x170002C6 RID: 710
		public SecurityKeyIdentifierClause this[int index]
		{
			get
			{
				return this.clauses[index];
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x000368C8 File Offset: 0x00034AC8
		public bool CanCreateKey
		{
			get
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i].CanCreateKey)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x000368F7 File Offset: 0x00034AF7
		public int Count
		{
			get
			{
				return this.clauses.Count;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00036904 File Offset: 0x00034B04
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0003690C File Offset: 0x00034B0C
		public void Add(SecurityKeyIdentifierClause clause)
		{
			if (this.isReadOnly)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ObjectIsReadOnly")));
			}
			if (clause == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("clause"));
			}
			this.clauses.Add(clause);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00036960 File Offset: 0x00034B60
		public SecurityKey CreateKey()
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].CanCreateKey)
				{
					return this[i].CreateKey();
				}
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("KeyIdentifierCannotCreateKey")));
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000369B4 File Offset: 0x00034BB4
		public TClause Find<TClause>() where TClause : SecurityKeyIdentifierClause
		{
			TClause result;
			if (!this.TryFind<TClause>(out result))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("NoKeyIdentifierClauseFound", new object[]
				{
					typeof(TClause)
				}), "TClause"));
			}
			return result;
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000369FE File Offset: 0x00034BFE
		public IEnumerator<SecurityKeyIdentifierClause> GetEnumerator()
		{
			return this.clauses.GetEnumerator();
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00036A10 File Offset: 0x00034C10
		public void MakeReadOnly()
		{
			this.isReadOnly = true;
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00036A1C File Offset: 0x00034C1C
		public override string ToString()
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				stringWriter.WriteLine("SecurityKeyIdentifier");
				stringWriter.WriteLine("    (");
				stringWriter.WriteLine("    IsReadOnly = {0},", this.IsReadOnly);
				stringWriter.WriteLine("    Count = {0}{1}", this.Count, (this.Count > 0) ? "," : "");
				for (int i = 0; i < this.Count; i++)
				{
					stringWriter.WriteLine("    Clause[{0}] = {1}{2}", i, this[i], (i < this.Count - 1) ? "," : "");
				}
				stringWriter.WriteLine("    )");
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00036AFC File Offset: 0x00034CFC
		public bool TryFind<TClause>(out TClause clause) where TClause : SecurityKeyIdentifierClause
		{
			for (int i = 0; i < this.clauses.Count; i++)
			{
				TClause tclause = this.clauses[i] as TClause;
				if (tclause != null)
				{
					clause = tclause;
					return true;
				}
			}
			clause = default(TClause);
			return false;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00036B4F File Offset: 0x00034D4F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000C1E RID: 3102
		private const int InitialSize = 2;

		// Token: 0x04000C1F RID: 3103
		private readonly List<SecurityKeyIdentifierClause> clauses;

		// Token: 0x04000C20 RID: 3104
		private bool isReadOnly;
	}
}
