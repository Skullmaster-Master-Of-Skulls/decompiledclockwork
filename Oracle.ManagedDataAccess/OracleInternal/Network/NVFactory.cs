using System;
using System.Text;

namespace OracleInternal.Network
{
	// Token: 0x02000157 RID: 343
	internal class NVFactory
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x00091EF4 File Offset: 0x000900F4
		internal static NVPair CreateNVPair(string nvString)
		{
			NVTokens nvtokens = new NVTokens();
			nvtokens.ParseTokens(nvString);
			return NVFactory.ReadTopLevelNVPair(nvtokens);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x00091F18 File Offset: 0x00090118
		private static NVPair ReadTopLevelNVPair(NVTokens nvt)
		{
			int token = nvt.Token;
			nvt.EatToken();
			if (token != 1)
			{
				NVFactory.GetContext(nvt);
				throw new NetworkException(303);
			}
			string text = NVFactory.ReadNVLiteral(nvt);
			NVPair nvpair = new NVPair(text);
			if ((token = nvt.Token) == 3)
			{
				while (token == 8 || token == 3)
				{
					text += nvt.PopLiteral();
					token = nvt.Token;
				}
				nvpair.Name = text;
				return NVFactory.ReadRightHandSide(nvpair, nvt);
			}
			return NVFactory.ReadRightHandSide(nvpair, nvt);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x00091F98 File Offset: 0x00090198
		private static NVPair ReadNVPair(NVTokens nvt)
		{
			int token = nvt.Token;
			nvt.EatToken();
			if (token != 1 && token != 3)
			{
				NVFactory.GetContext(nvt);
				throw new NetworkException(303);
			}
			string name = NVFactory.ReadNVLiteral(nvt);
			NVPair nvp = new NVPair(name);
			return NVFactory.ReadRightHandSide(nvp, nvt);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x00091FE4 File Offset: 0x000901E4
		private static NVPair ReadRightHandSide(NVPair nvp, NVTokens nvt)
		{
			int token;
			switch (nvt.Token)
			{
			case 2:
			case 3:
				nvp.Atom = nvp.Name;
				break;
			case 4:
				nvt.EatToken();
				token = nvt.Token;
				if (token == 8)
				{
					nvp.Atom = NVFactory.ReadNVAtom(nvt);
				}
				else
				{
					NVFactory.ReadNVList(nvt, nvp);
				}
				break;
			default:
				NVFactory.GetContext(nvt);
				throw new NetworkException(303);
			}
			token = nvt.Token;
			if (token == 2)
			{
				nvt.EatToken();
			}
			else if (token != 3)
			{
				string literal = nvt.Literal;
				NVFactory.GetContext(nvt);
				throw new NetworkException(303);
			}
			return nvp;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0009208C File Offset: 0x0009028C
		private static string ReadNVLiteral(NVTokens nvt)
		{
			int token = nvt.Token;
			if (token != 8)
			{
				NVFactory.GetContext(nvt);
				throw new NetworkException(303);
			}
			return nvt.PopLiteral();
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000920BC File Offset: 0x000902BC
		private static string ReadNVAtom(NVTokens nvt)
		{
			int token = nvt.Token;
			if (token != 8)
			{
				NVFactory.GetContext(nvt);
				throw new NetworkException(303);
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (token != 2)
			{
				stringBuilder = stringBuilder.Append(nvt.PopLiteral());
				token = nvt.Token;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0009210C File Offset: 0x0009030C
		private static void ReadNVList(NVTokens nvt, NVPair parent)
		{
			int token = nvt.Token;
			if (token != 1 && token != 3)
			{
				return;
			}
			NVPair nvpair = NVFactory.ReadNVPair(nvt);
			parent.AddListElement(nvpair);
			if ((token == 3 || nvpair.Name == nvpair.Atom) && parent.ListType != NVPair.LIST_COMMASEP)
			{
				parent.ListType = NVPair.LIST_COMMASEP;
			}
			NVFactory.ReadNVList(nvt, parent);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00092168 File Offset: 0x00090368
		private static string GetContext(NVTokens nvt)
		{
			return string.Concat(new string[]
			{
				" ",
				nvt.PopLiteral(),
				" ",
				nvt.PopLiteral(),
				" ",
				nvt.PopLiteral()
			});
		}
	}
}
