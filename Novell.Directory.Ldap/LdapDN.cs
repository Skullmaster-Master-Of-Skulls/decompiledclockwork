using System;
using System.Text;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002E RID: 46
	public class LdapDN
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000AC2C File Offset: 0x00009C2C
		[CLSCompliant(false)]
		public static bool equals(string dn1, string dn2)
		{
			DN dn3 = new DN(dn1);
			DN toDN = new DN(dn2);
			return dn3.Equals(toDN);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000AC54 File Offset: 0x00009C54
		public static string escapeRDN(string rdn)
		{
			StringBuilder stringBuilder = new StringBuilder(rdn);
			int i = 0;
			while (i < stringBuilder.Length && stringBuilder[i] != '=')
			{
				i++;
			}
			if (i == stringBuilder.Length)
			{
				throw new ArgumentException("Could not parse RDN: Attribute type and name must be separated by an equal symbol, '='");
			}
			i++;
			if (stringBuilder[i] == ' ' || stringBuilder[i] == '#')
			{
				stringBuilder.Insert(i++, '\\');
			}
			while (i < stringBuilder.Length)
			{
				if (stringBuilder[i] == ',' || stringBuilder[i] == '+' || stringBuilder[i] == '"' || stringBuilder[i] == '\\' || stringBuilder[i] == '<' || stringBuilder[i] == '>' || stringBuilder[i] == ';')
				{
					stringBuilder.Insert(i++, '\\');
				}
				i++;
			}
			if (stringBuilder[stringBuilder.Length - 1] == ' ')
			{
				stringBuilder.Insert(stringBuilder.Length - 1, '\\');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000AD58 File Offset: 0x00009D58
		public static string[] explodeDN(string dn, bool noTypes)
		{
			DN dn2 = new DN(dn);
			return dn2.explodeDN(noTypes);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000AD78 File Offset: 0x00009D78
		public static string[] explodeRDN(string rdn, bool noTypes)
		{
			RDN rdn2 = new RDN(rdn);
			return rdn2.explodeRDN(noTypes);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000AD98 File Offset: 0x00009D98
		public static bool isValid(string dn)
		{
			try
			{
				new DN(dn);
			}
			catch (ArgumentException ex)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000ADD4 File Offset: 0x00009DD4
		public static string normalize(string dn)
		{
			DN dn2 = new DN(dn);
			return dn2.ToString();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000ADF4 File Offset: 0x00009DF4
		public static string unescapeRDN(string rdn)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < rdn.Length && rdn[i] != '=')
			{
				i++;
			}
			if (i == rdn.Length)
			{
				throw new ArgumentException("Could not parse rdn: Attribute type and name must be separated by an equal symbol, '='");
			}
			i++;
			if (rdn[i] == '\\' && i + 1 < rdn.Length - 1 && (rdn[i + 1] == ' ' || rdn[i + 1] == '#'))
			{
				i++;
			}
			while (i < rdn.Length)
			{
				if (rdn[i] != '\\' || i == rdn.Length - 1)
				{
					goto IL_105;
				}
				if (rdn[i + 1] != ',' && rdn[i + 1] != '+' && rdn[i + 1] != '"' && rdn[i + 1] != '\\' && rdn[i + 1] != '<' && rdn[i + 1] != '>' && rdn[i + 1] != ';')
				{
					if (rdn[i + 1] != ' ' || i + 2 != rdn.Length)
					{
						goto IL_105;
					}
				}
				IL_113:
				i++;
				continue;
				IL_105:
				stringBuilder.Append(rdn[i]);
				goto IL_113;
			}
			return stringBuilder.ToString();
		}
	}
}
