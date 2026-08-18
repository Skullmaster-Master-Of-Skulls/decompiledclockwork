using System;
using System.Collections;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;

// Token: 0x0200029D RID: 669
internal class spr\u24EF : CollectionEx
{
	// Token: 0x060023B0 RID: 9136 RVA: 0x002422E0 File Offset: 0x002412E0
	internal spr\u24EF(Document A_0, OwnerHolder A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060023B1 RID: 9137 RVA: 0x002422F8 File Offset: 0x002412F8
	internal void ᜀ(spr\u24EF A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = base.Count;
			int num2 = 4;
			for (;;)
			{
				DocumentObject documentObject;
				switch (num2)
				{
				case 0:
					goto IL_57;
				case 1:
					if (num >= count)
					{
						num2 = 5;
						continue;
					}
					documentObject = (base.InnerList[num] as DocumentObject).Clone();
					num2 = 3;
					continue;
				case 2:
					goto IL_41;
				case 3:
					if (documentObject == null)
					{
						goto IL_41;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_57;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 4:
					goto IL_BC;
				case 5:
					return;
				case 6:
					goto IL_BC;
				}
				break;
				IL_41:
				num++;
				if (true)
				{
				}
				num2 = 6;
				continue;
				IL_57:
				A_0.ᜁ().Add(documentObject);
				num2 = 2;
				continue;
				IL_BC:
				num2 = 1;
			}
		}
	}

	// Token: 0x060023B2 RID: 9138 RVA: 0x002423E0 File Offset: 0x002413E0
	internal bool ᜂ()
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			IEnumerator enumerator = this.ᜁ().GetEnumerator();
			bool result;
			try
			{
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_EA;
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_AC;
					case 3:
					{
						DocumentObject documentObject;
						if (documentObject is Table)
						{
							num = 2;
							continue;
						}
						break;
					}
					case 4:
					{
						if (!enumerator.MoveNext())
						{
							num = 6;
							continue;
						}
						DocumentObject documentObject = (DocumentObject)enumerator.Current;
						num = 8;
						continue;
					}
					case 6:
						num = 0;
						continue;
					case 7:
						goto IL_B7;
					case 8:
					{
						DocumentObject documentObject;
						if (!(documentObject is Paragraph))
						{
							num = 1;
							continue;
						}
						goto IL_AC;
					}
					}
					IL_68:
					num = 4;
					continue;
					goto IL_68;
					IL_AC:
					result = true;
					num = 7;
				}
				IL_B7:
				return result;
				IL_EA:
				return false;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_131;
						case 1:
							disposable.Dispose();
							num = 0;
							continue;
						case 2:
							if (disposable != null)
							{
								num = 1;
								continue;
							}
							goto IL_14F;
						}
						break;
					}
				}
				IL_131:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				IL_14F:;
			}
			return result;
		}
		}
	}

	// Token: 0x060023B3 RID: 9139 RVA: 0x00242550 File Offset: 0x00241550
	internal int ᜀ()
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			IEnumerator enumerator = this.ᜁ().GetEnumerator();
			int result;
			try
			{
				int num2 = 8;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 3;
						continue;
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num2 = 0;
							continue;
						}
						DocumentObject documentObject = (DocumentObject)enumerator.Current;
						num2 = 5;
						continue;
					}
					case 3:
						goto IL_106;
					case 4:
						num2 = 6;
						continue;
					case 5:
					{
						DocumentObject documentObject;
						if (!(documentObject is Paragraph))
						{
							num2 = 4;
							continue;
						}
						goto IL_C5;
					}
					case 6:
					{
						DocumentObject documentObject;
						if (documentObject is Table)
						{
							num2 = 7;
							continue;
						}
						num = this.ᜁ().IndexOf(documentObject);
						num2 = 2;
						continue;
					}
					case 7:
						goto IL_C5;
					case 9:
						goto IL_D3;
					}
					IL_66:
					num2 = 1;
					continue;
					goto IL_66;
					IL_C5:
					result = num;
					num2 = 9;
				}
				IL_D3:
				return result;
				IL_106:
				return num;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (disposable != null)
							{
								num2 = 1;
								continue;
							}
							goto IL_176;
						case 1:
							disposable.Dispose();
							num2 = 2;
							continue;
						case 2:
							goto IL_150;
						}
						break;
					}
				}
				IL_150:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				IL_176:;
			}
			return result;
		}
		}
	}

	// Token: 0x060023B4 RID: 9140 RVA: 0x002426E8 File Offset: 0x002416E8
	internal IList ᜁ()
	{
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return base.InnerList;
	}
}
