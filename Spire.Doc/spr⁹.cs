using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using Spire.Doc;
using Spire.Doc.Documents;

// Token: 0x020003B7 RID: 951
[ToolboxItem(false)]
internal class spr\u2079 : Component
{
	// Token: 0x060035B4 RID: 13748 RVA: 0x003255DC File Offset: 0x003245DC
	private List<PageSetup> ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x060035B5 RID: 13749 RVA: 0x00325620 File Offset: 0x00324620
	internal void ᜀ(int A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x060035B6 RID: 13750 RVA: 0x00325664 File Offset: 0x00324664
	public spr\u2079()
	{
		this.ᜁ = new List<PageSetup>();
	}

	// Token: 0x060035B7 RID: 13751 RVA: 0x0032568C File Offset: 0x0032468C
	public void ᜀ(Document A_0, Stream A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			this.ᜂ = A_0;
			spr\u1A69 spr_u1A = new spr\u1A69();
			try
			{
				this.ᜂ.OperationType = DocumentOperationType.Layout;
				spr_u1A.ᜁ(this.ᜂ);
				this.ᜂ.OperationType = DocumentOperationType.None;
				this.ᜀ(spr_u1A, A_1);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9B;
					case 2:
						((IDisposable)spr_u1A).Dispose();
						num = 0;
						continue;
					}
					if (spr_u1A == null)
					{
						goto IL_A5;
					}
					num = 2;
				}
				IL_9B:
				if (true)
				{
				}
				IL_A5:;
			}
			break;
		}
		}
	}

	// Token: 0x060035B8 RID: 13752 RVA: 0x00325750 File Offset: 0x00324750
	public void ᜀ(Document A_0, Stream A_1, ToPdfParameterList A_2)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			this.ᜂ = A_0;
			spr\u1A69 spr_u1A = new spr\u1A69();
			try
			{
				if (true)
				{
				}
				this.ᜂ.OperationType = DocumentOperationType.Layout;
				spr_u1A.ᜀ(A_2);
				spr_u1A.ᜁ(this.ᜂ);
				this.ᜂ.OperationType = DocumentOperationType.None;
				this.ᜀ(spr_u1A, A_1, A_2.EmbeddedFontNameList);
			}
			finally
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B0;
					case 2:
						((IDisposable)spr_u1A).Dispose();
						num = 0;
						continue;
					}
					if (spr_u1A == null)
					{
						break;
					}
					num = 2;
				}
				IL_B0:;
			}
			break;
		}
		}
	}

	// Token: 0x060035B9 RID: 13753 RVA: 0x00325820 File Offset: 0x00324820
	public void ᜀ(Document A_0, Stream A_1, List<string> A_2)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ = A_0;
			spr\u1A69 spr_u1A = new spr\u1A69();
			try
			{
				this.ᜂ.OperationType = DocumentOperationType.Layout;
				spr_u1A.ᜀ(new ToPdfParameterList());
				spr_u1A.ᜁ(this.ᜂ);
				this.ᜂ.OperationType = DocumentOperationType.None;
				this.ᜀ(spr_u1A, A_1, A_2);
			}
			finally
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						((IDisposable)spr_u1A).Dispose();
						num = 2;
						continue;
					case 2:
						goto IL_AF;
					}
					if (spr_u1A == null)
					{
						break;
					}
					num = 1;
				}
				IL_AF:;
			}
			break;
		}
		}
	}

	// Token: 0x060035BA RID: 13754 RVA: 0x003258F0 File Offset: 0x003248F0
	public void ᜀ(string A_0, string A_1)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (false)
			{
			}
			Document a_ = new Document(A_0, FileFormat.Auto);
			FileStream fileStream = new FileStream(A_1, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
			try
			{
				if (true)
				{
				}
				this.ᜀ(a_, fileStream);
			}
			finally
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_84;
					case 1:
						((IDisposable)fileStream).Dispose();
						num = 0;
						continue;
					}
					if (fileStream == null)
					{
						break;
					}
					num = 1;
				}
				IL_84:;
			}
			break;
		}
		}
	}

	// Token: 0x060035BB RID: 13755 RVA: 0x00325994 File Offset: 0x00324994
	public void ᜀ(Stream A_0, Stream A_1)
	{
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		Document a_ = new Document(A_0, FileFormat.Auto);
		this.ᜀ(a_, A_1);
	}

	// Token: 0x060035BC RID: 13756 RVA: 0x003259E0 File Offset: 0x003249E0
	private sprἣ ᜀ(spr\u204A A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr᱖ a_ = new spr᱖();
		return new sprἣ(A_0, a_);
	}

	// Token: 0x060035BD RID: 13757 RVA: 0x00325A2C File Offset: 0x00324A2C
	private void ᜀ(spr\u1A69 A_0)
	{
		for (;;)
		{
			IL_22:
			int num = 0;
			for (;;)
			{
				IL_24:
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (true)
						{
						}
						goto IL_36;
					case 1:
					{
						if (num >= A_0.ᜤ().Count)
						{
							num2 = 2;
							continue;
						}
						spr\u1F89 spr_u1F = A_0.ᜤ()[num];
						this.ᜀ().Add(spr_u1F.ᜂ());
						num++;
						num2 = 3;
						continue;
					}
					case 2:
						return;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24;
						default:
							if (false)
							{
							}
							goto IL_36;
						}
						break;
					}
					goto IL_22;
					IL_36:
					num2 = 1;
				}
			}
		}
	}

	// Token: 0x060035BE RID: 13758 RVA: 0x00325AD8 File Offset: 0x00324AD8
	private spr\u204A ᜀ(BuiltinDocumentProperties A_0)
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
		spr\u204A spr_u204A = new spr\u204A();
		spr_u204A.ᜀ(A_0.Author);
		spr_u204A.ᜀ(A_0.CreateDate);
		spr_u204A.ᜆ(A_0.Company);
		spr_u204A.ᜄ(A_0.Keywords);
		spr_u204A.ᜉ(A_0.Subject);
		spr_u204A.ᜈ(A_0.Title);
		return spr_u204A;
	}

	// Token: 0x060035BF RID: 13759 RVA: 0x00325B64 File Offset: 0x00324B64
	private void ᜀ(List<Dictionary<string, RectangleF>> A_0, spr\u250B A_1)
	{
		int num;
		int num2;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
					goto IL_8D;
				case 1:
				{
					if (num2 >= A_0.Count)
					{
						num = 4;
						continue;
					}
					Dictionary<string, RectangleF>.Enumerator enumerator = A_0[num2].GetEnumerator();
					num = 3;
					continue;
				}
				case 2:
					goto IL_8D;
				case 3:
					try
					{
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								goto IL_12A;
							case 2:
								num = 1;
								continue;
							case 4:
							{
								Dictionary<string, RectangleF>.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 2;
									continue;
								}
								KeyValuePair<string, RectangleF> keyValuePair = enumerator.Current;
								RectangleF value = keyValuePair.Value;
								string key = keyValuePair.Key;
								key.Equals(string.Empty);
								num = 3;
								continue;
							}
							}
							IL_D5:
							num = 4;
							continue;
							goto IL_D5;
						}
						IL_12A:
						goto IL_66;
					}
					finally
					{
						Dictionary<string, RectangleF>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					return;
					IL_66:
					num2++;
					num = 2;
					continue;
				case 4:
					return;
				}
				goto IL_51;
				IL_8D:
				num = 1;
			}
			return;
		default:
			if (false)
			{
			}
			num = 0;
			switch (num)
			{
			}
			break;
		}
		IL_51:
		if (true)
		{
		}
		num2 = 0;
		num = 0;
		goto IL_36;
	}

	// Token: 0x060035C0 RID: 13760 RVA: 0x00325CC0 File Offset: 0x00324CC0
	private void ᜀ(spr\u1A69 A_0, Stream A_1)
	{
		int num;
		int num2;
		sprἣ sprἣ;
		int count;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
					try
					{
						MemoryStream memoryStream;
						this.ᜃ.ᜀ(num2, 1, ImageType.Metafile, memoryStream, false, false, GraphicsUnit.Point);
						spr\u2002 a_ = new spr\u2002(memoryStream);
						sprᤔ sprᤔ = new sprᤔ();
						sprᤔ.ᜀ(A_0.\u171E().ᜀ()[num2].ᜂ());
						spr\u2507 spr_u = new spr\u2507(a_, sprᤔ);
						spr\u24A6 a_2 = spr_u.ᜀ(this.ᜀ()[num2].PageSize, true);
						spr\u250B spr_u250B = new spr\u250B(this.ᜀ()[num2].PageSize.Width, this.ᜀ()[num2].PageSize.Height);
						spr_u250B.ᜁ(a_2);
						sprἣ.ᜀ(spr_u250B);
						this.ᜃ.\u171E().ᜀ()[num2].ᜀ().Dispose();
						this.ᜃ.\u171E().ᜀ()[num2].ᜀ(null);
						goto IL_9F;
					}
					finally
					{
						num = 0;
						for (;;)
						{
							MemoryStream memoryStream;
							switch (num)
							{
							case 1:
								((IDisposable)memoryStream).Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_221;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 1;
						}
						IL_221:;
					}
					goto IL_224;
					IL_9F:
					if (true)
					{
					}
					num2++;
					num = 1;
					continue;
				case 1:
					goto IL_CB;
				case 2:
					goto IL_EA;
				case 3:
					goto IL_CB;
				case 4:
				{
					if (num2 >= count)
					{
						num = 2;
						continue;
					}
					MemoryStream memoryStream = new MemoryStream();
					num = 0;
					continue;
				}
				}
				goto IL_51;
				IL_CB:
				num = 4;
			}
			IL_EA:
			IL_224:
			A_0.ᜠ();
			sprἣ.ᜁ();
			sprἣ.ᜀ().ᜀ(A_1);
			return;
		default:
			if (false)
			{
			}
			num = 0;
			switch (num)
			{
			}
			break;
		}
		IL_51:
		this.ᜃ = A_0;
		this.ᜀ(this.ᜃ);
		count = this.ᜃ.ᜤ().Count;
		spr\u204A a_3 = this.ᜀ(this.ᜂ.BuiltinDocumentProperties);
		sprἣ = this.ᜀ(a_3);
		num2 = 0;
		num = 3;
		goto IL_36;
	}

	// Token: 0x060035C1 RID: 13761 RVA: 0x00325F1C File Offset: 0x00324F1C
	private void ᜀ(spr\u1A69 A_0, Stream A_1, List<string> A_2)
	{
		int num;
		int num2;
		sprἣ sprἣ;
		int count;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_3E:
				switch (num)
				{
				case 0:
					goto IL_B7;
				case 1:
					goto IL_D6;
				case 2:
					try
					{
						MemoryStream memoryStream;
						A_0.ᜀ(num2, 1, ImageType.Metafile, memoryStream, false, false, GraphicsUnit.Pixel);
						spr\u2002 a_ = new spr\u2002(memoryStream);
						spr\u2507 spr_u = new spr\u2507(a_, new sprᤔ());
						spr\u24A6 a_2 = spr_u.ᜀ(this.ᜀ()[num2].PageSize, true);
						spr\u250B spr_u250B = new spr\u250B(this.ᜀ()[num2].PageSize.Width, this.ᜀ()[num2].PageSize.Height);
						spr_u250B.ᜁ(a_2);
						sprἣ.ᜀ(spr_u250B);
						A_0.\u171E().ᜀ()[num2].ᜀ().Dispose();
						A_0.\u171E().ᜀ()[num2].ᜀ(null);
						goto IL_96;
					}
					finally
					{
						num = 2;
						for (;;)
						{
							MemoryStream memoryStream;
							switch (num)
							{
							case 0:
								((IDisposable)memoryStream).Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_1DD;
							}
							if (memoryStream == null)
							{
								break;
							}
							num = 0;
						}
						IL_1DD:;
					}
					goto IL_1E0;
					IL_96:
					num2++;
					num = 3;
					continue;
				case 3:
					goto IL_B7;
				case 4:
				{
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					MemoryStream memoryStream = new MemoryStream();
					num = 2;
					continue;
				}
				}
				goto IL_59;
				IL_B7:
				num = 4;
			}
			IL_D6:
			IL_1E0:
			A_0.ᜠ();
			sprἣ.ᜁ();
			sprἣ.ᜀ().ᜀ(A_1);
			return;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			num = 0;
			switch (num)
			{
			}
			break;
		}
		IL_59:
		this.ᜀ(A_0);
		count = A_0.ᜤ().Count;
		spr\u204A a_3 = this.ᜀ(this.ᜂ.BuiltinDocumentProperties);
		sprἣ = this.ᜀ(a_3);
		num2 = 0;
		num = 0;
		goto IL_3E;
	}

	// Token: 0x040028F9 RID: 10489
	private const ImageType ᜀ = ImageType.Metafile;

	// Token: 0x040028FA RID: 10490
	private List<PageSetup> ᜁ;

	// Token: 0x040028FB RID: 10491
	private Document ᜂ;

	// Token: 0x040028FC RID: 10492
	private spr\u1A69 ᜃ;

	// Token: 0x040028FD RID: 10493
	private int ᜄ = 80;
}
