using System;
using System.Collections;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;

// Token: 0x0200022B RID: 555
internal class sprὗ : CollectionEx
{
	// Token: 0x06001AAC RID: 6828 RVA: 0x001BDB9C File Offset: 0x001BCB9C
	internal IList ᜀ()
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

	// Token: 0x06001AAD RID: 6829 RVA: 0x001BDBE0 File Offset: 0x001BCBE0
	internal sprὗ(Document A_0, OwnerHolder A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x001BDBF8 File Offset: 0x001BCBF8
	internal void ᜀ(sprὗ A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = base.Count;
			DocumentObject documentObject;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_8D:
				documentObject = (base.InnerList[num] as DocumentObject).Clone();
				num2 = 2;
				break;
			default:
				if (false)
				{
				}
				num2 = 5;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					A_0.ᜀ().Add(documentObject);
					num2 = 6;
					continue;
				case 2:
					if (documentObject != null)
					{
						num2 = 1;
						continue;
					}
					goto IL_5B;
				case 3:
					goto IL_BF;
				case 4:
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					goto IL_8D;
				case 5:
					if (true)
					{
					}
					goto IL_BF;
				case 6:
					goto IL_5B;
				}
				break;
				IL_5B:
				num++;
				num2 = 3;
				continue;
				IL_BF:
				num2 = 4;
			}
		}
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x001BDCE0 File Offset: 0x001BCCE0
	internal bool ᜂ()
	{
		switch (0)
		{
		default:
		{
			bool result = false;
			IEnumerator enumerator = this.ᜀ().GetEnumerator();
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_F5;
					case 1:
						num = 8;
						continue;
					case 3:
						goto IL_E5;
					case 4:
						if (!enumerator.MoveNext())
						{
							num = 6;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_101;
						default:
						{
							if (false)
							{
							}
							DocumentObject documentObject = (DocumentObject)enumerator.Current;
							num = 7;
							continue;
						}
						}
						break;
					case 5:
						goto IL_101;
					case 6:
						goto IL_F5;
					case 7:
					{
						DocumentObject documentObject;
						if (!(documentObject is Paragraph))
						{
							num = 1;
							continue;
						}
						goto IL_E5;
					}
					case 8:
					{
						DocumentObject documentObject;
						if (documentObject is Table)
						{
							num = 3;
							continue;
						}
						break;
					}
					}
					IL_A4:
					num = 4;
					continue;
					goto IL_A4;
					IL_E5:
					result = true;
					num = 0;
					continue;
					IL_F5:
					num = 5;
				}
				IL_101:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_145;
						case 1:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_147;
						case 2:
							disposable.Dispose();
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_145:
				IL_147:;
			}
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x001BDE50 File Offset: 0x001BCE50
	internal int ᜁ()
	{
		switch (0)
		{
		default:
		{
			int result = 0;
			IEnumerator enumerator = this.ᜀ().GetEnumerator();
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						DocumentObject documentObject;
						if (!(documentObject is Table))
						{
							num = 7;
							continue;
						}
						goto IL_100;
					}
					case 1:
						num = 0;
						continue;
					case 4:
						if (!enumerator.MoveNext())
						{
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10C;
						default:
						{
							if (false)
							{
							}
							DocumentObject documentObject = (DocumentObject)enumerator.Current;
							num = 6;
							continue;
						}
						}
						break;
					case 5:
						goto IL_10C;
					case 6:
					{
						DocumentObject documentObject;
						if (!(documentObject is Paragraph))
						{
							num = 1;
							continue;
						}
						goto IL_100;
					}
					case 7:
					{
						DocumentObject documentObject;
						result = this.ᜀ().IndexOf(documentObject);
						num = 3;
						continue;
					}
					case 8:
						goto IL_100;
					}
					IL_A4:
					num = 4;
					continue;
					goto IL_A4;
					IL_100:
					num = 5;
				}
				IL_10C:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_150;
						case 1:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_152;
						case 2:
							disposable.Dispose();
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_150:
				IL_152:;
			}
			if (true)
			{
			}
			return result;
		}
		}
	}
}
